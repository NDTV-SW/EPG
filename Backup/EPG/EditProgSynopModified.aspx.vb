Imports System
Imports System.Data.SqlClient

Public Class EditProgSynopModified
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ConnectionString)

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                lbChannel.Text = Request.QueryString("channelid").ToString
                lbEpisodeno.Text = Request.QueryString("episodeno").ToString
                lbLangId.Text = Request.QueryString("langid").ToString
                lbProgId.Text = Request.QueryString("progid").ToString
                lbLanguage.Text = Request.QueryString("language").ToString

                Dim adp As New SqlDataAdapter("select progName,synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='1' and episodeno='" & lbEpisodeno.Text & "'", MyConnection)
                Dim dt As New DataTable
                adp.Fill(dt)
                'Dim dt As DataTable = clsExecute.executeSQL("select progName,synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='1'", False)
                lbEngProgName.Text = dt.Rows(0)(0).ToString
                lbEngSynopsis.Text = dt.Rows(0)(1).ToString

                Dim adpregProgSyn As New SqlDataAdapter("select progName,synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='" & lbLangId.Text & "' and episodeno='" & lbEpisodeno.Text & "'", MyConnection)
                Dim dtregProgSyn As New DataTable
                adpregProgSyn.Fill(dtregProgSyn)

                'Dim dtregProgSyn As DataTable = clsExecute.executeSQL("select progName,synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='" & lbLangId.Text & "'", False)
                lbRegProgName.Text = lbLanguage.Text & " Programme"
                lbRegSynopsis.Text = lbLanguage.Text & " Synopsis"
                txtProgname.Text = dtregProgSyn.Rows(0)(0).ToString
                txtSynopsis.Text = dtregProgSyn.Rows(0)(1).ToString

            Catch ex As Exception
                Logger.LogError("EditProgSynopsisModified", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Try
            exec_Proc(lbProgId.Text.Trim, txtProgname.Text.Trim, txtSynopsis.Text, lbLangId.Text, "C", lbEpisodeno.Text)
            hfSend.Value = "2"
        Catch ex As Exception
            lberror.Text = ex.Message.ToString.Trim
            lberror.Visible = True
            Logger.LogError("EditProgSynopsisModified", "BtnSubmit_Click", ex.Message.ToString, User.Identity.Name)
        Finally
            MyConnection.Close()
        End Try
    End Sub
    Private Sub exec_Proc(ByVal ProgId As Integer, ByVal ProgName As String, ByVal Synopsis As String, ByVal LangID As Integer, ByVal action As String, ByVal vEpisodeNo As Integer)
        Try
            'clsExecute.executeSQL("sp_mst_ProgramRegional", "Rowid~ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
            '                            "Int~Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
            '                             "0~" & ProgId & "~" & ProgName & "~" & Synopsis & "~" & vEpisodeNo & "~" & LangID & "~C~" & User.Identity.Name, 1)


            Dim MyDataAdapter As SqlDataAdapter
            MyDataAdapter = New SqlDataAdapter("sp_mst_ProgramRegional", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = ProgId
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = ProgName
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = Synopsis
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@EpisodeNo", SqlDbType.Int, 8)).Value = vEpisodeNo
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = LangID
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "C"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            Dim DSProgRegional As New DataSet
            MyDataAdapter.Fill(DSProgRegional, "GetRegionalProgName")
            MyDataAdapter.Dispose()
            MyConnection.Close()

        Catch ex As Exception
            Logger.LogError("EditProgSynopsisModified", "exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
    Private Sub exec_ProcRegional(ByVal ProgId As Integer, ByVal ProgName As String, ByVal synopsis As String, ByVal LanguageID As Integer, ByVal ID As Integer, ByVal action As String, ByVal vEpisodeNo As Integer)
        Try
            Dim vProgName As String, vSynopsis As String
            If LanguageID = 1 Then
                vProgName = Logger.RemSplCharsEng(ProgName.ToString.Trim)
                vSynopsis = Logger.RemSplCharsEng(synopsis.ToString.Trim)
            Else
                vProgName = Logger.RemSplCharsAllLangs(ProgName.ToString.Trim, Convert.ToInt32(LanguageID.ToString))
                vSynopsis = Logger.RemSplCharsAllLangs(synopsis.ToString.Trim, Convert.ToInt32(LanguageID.ToString))
            End If

            'clsExecute.executeSQL("sp_mst_ProgramRegional", "Rowid~ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
            '                            "Int~Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
            '                             "0~" & ProgId & "~" & vProgName & "~" & vSynopsis & "~" & vEpisodeNo & "~" & LanguageID.ToString.Trim & "~" & action.ToString.Trim & "~" & User.Identity.Name, 1)

            Dim MyDataAdapter As SqlDataAdapter
            MyDataAdapter = New SqlDataAdapter("sp_mst_ProgramRegional", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = ProgId
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = ProgName
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = synopsis
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = LanguageID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            Dim DSProgRegional As New DataSet
            MyDataAdapter.Fill(DSProgRegional, "GetRegionalProgName")
            MyDataAdapter.Dispose()
            MyConnection.Close()

        Catch ex As Exception
            Logger.LogError("EditProgSynopsisModified", action & " exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

End Class