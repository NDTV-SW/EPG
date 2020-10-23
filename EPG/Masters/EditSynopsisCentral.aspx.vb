Imports System
Imports System.Data.SqlClient

Public Class EditSynopsisCentral
    Inherits System.Web.UI.Page
    Dim ConString As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ConnectionString)

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                lbChannelId.Text = Request.QueryString("ChannelId").ToString
                lbProgId.Text = Request.QueryString("ProgId").ToString
                lbLangId.Text = Request.QueryString("LangId").ToString
                lbMode.Text = Request.QueryString("Mode").ToString

                lbChannel.Text = Request.QueryString("ChannelId").ToString
                lbProgramme.Text = Request.QueryString("progname").ToString.Replace("^", "'")
                lbLanguage.Text = Request.QueryString("language").ToString
                lbEpisodeNo.Text = Request.QueryString("episodeNo").ToString

                If lbMode.Text = "Insert" Then
                    BtnSubmit.Text = "Insert"
                Else
                    BtnSubmit.Text = "Update"
                    Dim adpProgSyn As New SqlDataAdapter("select progName,synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='" & lbLangId.Text & "' and episodeno='" & lbEpisodeNo.Text & "'", ConString)
                    Dim dt As New DataTable
                    adpProgSyn.Fill(dt)
                    txtProgname.Text = dt.Rows(0)(0).ToString.Replace(" ", " ")
                    txtSynopsis.Text = dt.Rows(0)(1).ToString.Replace(" ", " ")
                End If
            Catch ex As Exception
                Logger.LogError("Edit Program Name", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Try
            If BtnSubmit.Text = "Insert" Then
                exec_ProcRegional(lbProgId.Text.Trim, txtProgname.Text.Trim, txtSynopsis.Text.Trim, lbLangId.Text.Trim, 0, "A", lbEpisodeNo.Text.Trim)
            ElseIf BtnSubmit.Text = "Update" Then
                exec_ProcRegional(lbProgId.Text.Trim, txtProgname.Text.Trim, txtSynopsis.Text.Trim, lbLangId.Text.Trim, 0, "C", lbEpisodeNo.Text.Trim)
            End If
            If lbLangId.Text.Trim = "1" Then
                exec_Proc(lbProgId.Text.Trim, txtProgname.Text.Trim, "C")
            End If
            hfSend.Value = "2"

        Catch ex As Exception
            lberror.Text = ex.Message.ToString.Trim
            lberror.Visible = True
            Logger.LogError("Edit Program Name", "BtnSubmit_Click", ex.Message.ToString, User.Identity.Name)
        Finally
            ConString.Close()
        End Try
    End Sub
    Private Sub exec_Proc(ByVal ProgId As Integer, ByVal ProgName As String, ByVal action As String)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            MyDataAdapter = New SqlDataAdapter("sp_mst_program", ConString)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = ProgId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = Logger.RemSplCharsEng(ProgName.ToString.Trim)
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetProgram")
            MyDataAdapter.Dispose()
            ConString.Close()
        Catch ex As Exception
            Logger.LogError("Edit Program Name", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
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
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("sp_mst_ProgramRegional", "Rowid~ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                        "Int~Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                         "0~" & ProgId & "~" & vProgName & "~" & vSynopsis & "~" & vEpisodeNo & "~" & LanguageID.ToString.Trim & "~" & action.ToString.Trim & "~" & User.Identity.Name, True, False)
        Catch ex As Exception
            Logger.LogError("Edit Program Name", action & " exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

End Class