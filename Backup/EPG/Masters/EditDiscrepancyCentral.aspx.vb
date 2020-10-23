Imports System
Imports System.Data.SqlClient

Public Class EditDiscrepancyCentral
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
                lbProgId.Text = Request.QueryString("ProgId").ToString
                lbLangId.Text = Request.QueryString("LangId").ToString
                lbMode.Text = Request.QueryString("Mode").ToString

                lbProgName.Text = Request.QueryString("ProgName").ToString.Replace("^", "'")
                lbSynopsis.Text = Request.QueryString("Synopsis").ToString.Replace("^", "'")
                lbRegProgName.Text = Request.QueryString("RegProgName").ToString.Replace("^", "'")
                lbRegSynopsis.Text = Request.QueryString("RegSynopsis").ToString.Replace("^", "'")
                lbEpisodeNo.Text = Request.QueryString("episodeNo").ToString.Replace("^", "'")

                lbProgname1.Text = lbProgName.Text
                lbSynopsis1.Text = lbSynopsis.Text

                lbRegional.Text = Request.QueryString("Language").ToString & " " & IIf(lbMode.Text = "Insert", "Programme", "Synopsis")

                If lbMode.Text = "Insert1" Or lbMode.Text = "Insert" Then
                    BtnSubmit.Text = "Insert Programme"
                    lbRegionalProg.Visible = True
                    txtRegProg.Visible = True
                Else
                    BtnSubmit.Text = "Update Synopsis"
                    lbDispProgname.Text = Request.QueryString("Language").ToString & " " & "Programme"

                    'If MyConnection.State = ConnectionState.Closed Then
                    '    MyConnection.Open()
                    'End If

                    'Dim cmd As New SqlCommand("select progName,Synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='" & lbLangId.Text & "' and EpisodeNo='" & lbEpisodeNo.Text & "'", MyConnection)
                    'Dim dr As SqlDataReader
                    'dr = cmd.ExecuteReader
                    'dr.Read()

                    Dim obj As New clsExecute
                    Dim dt As DataTable = obj.executeSQL("select progName,Synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='" & lbLangId.Text & "' and EpisodeNo='" & lbEpisodeNo.Text & "'", False)
                    If dt.Rows.Count > 0 Then
                        txtRegProg.Text = dt.Rows(0)("progName").ToString
                        txtText.Text = dt.Rows(0)("Synopsis").ToString
                    End If

                    'If dr.HasRows Then
                    '    txtRegProg.Text = dr("progName").ToString
                    '    txtText.Text = dr("Synopsis").ToString
                    'End If
                    'dr.Close()
                    'MyConnection.Dispose()
                End If
            Catch ex As Exception
                Logger.LogError("Edit Discrepancy Central", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Try
            If BtnSubmit.Text = "Insert Programme" Then
                exec_ProcRegionalProgramme(lbProgId.Text.Trim, IIf(txtRegProg.Visible = True, txtRegProg.Text.Trim, lbProgName.Text), IIf(lbLangId.Text.Trim = "1", Logger.RemSplCharsEng(txtText.Text.Trim), Logger.RemSplCharsAllLangs(txtText.Text.Trim, Convert.ToInt32(lbLangId.Text))), lbLangId.Text.Trim, "A", lbEpisodeNo.Text)

            ElseIf BtnSubmit.Text = "Update Synopsis" Then
                exec_ProcRegionalSynopsis(lbProgId.Text.Trim, lbProgname1.Text, Logger.RemSplCharsAllLangs(txtText.Text.Trim, Convert.ToInt32(lbLangId.Text)), lbLangId.Text.Trim, "C", lbEpisodeNo.Text)
            End If
            If lbLangId.Text.Trim = "1" Then
                exec_Proc(lbProgId.Text.Trim, Logger.RemSplCharsEng(txtText.Text.Trim), "C")
            End If
            hfSend.Value = "2"

        Catch ex As Exception
            lberror.Text = ex.Message.ToString.Trim
            lberror.Visible = True
            Logger.LogError("Edit Discrepancy Central", "BtnSubmit_Click", ex.Message.ToString, User.Identity.Name)
        Finally
            MyConnection.Close()
        End Try
    End Sub
    Private Sub exec_Proc(ByVal ProgId As Integer, ByVal ProgName As String, ByVal action As String)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            MyDataAdapter = New SqlDataAdapter("sp_mst_program", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = ProgId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = Logger.RemSplCharsEng(ProgName.ToString.Trim)
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetProgram")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Edit Discrepancy Central", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub exec_ProcRegionalProgramme(ByVal ProgId As Integer, ByVal ProgName As String, ByVal Synopsis As String, ByVal LanguageID As Integer, ByVal action As String, ByVal vEpisodeNo As Integer)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            MyDataAdapter = New SqlDataAdapter("sp_mst_ProgramRegional", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = ProgId.ToString.Trim
            If LanguageID = 1 Then
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = Logger.RemSplCharsEng(ProgName.ToString.Trim)
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = IIf(Synopsis.ToString.Length = 0, "", Logger.RemSplCharsEng(Synopsis.ToString.Trim))
            Else
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = Logger.RemSplCharsAllLangs(ProgName.ToString.Trim, Convert.ToInt32(LanguageID.ToString))
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = IIf(Synopsis.ToString.Length = 0, "", Logger.RemSplCharsAllLangs(Synopsis.ToString.Trim, Convert.ToInt32(LanguageID.ToString)))
            End If

            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = LanguageID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@EpisodeNo", SqlDbType.Int, 8)).Value = vEpisodeNo
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetRegionalProgName")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Edit Discrepancy Central", action & " exec_ProcRegionalProgramme", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub exec_ProcRegionalSynopsis(ByVal ProgId As Integer, ByVal progname As String, ByVal synopsis As String, ByVal LanguageID As Integer, ByVal action As String, ByVal vEpisodeNo As Integer)
        Try
            Dim vProgName As String, vSynopsis As String
            If LanguageID = 1 Then
                vProgName = Logger.RemSplCharsEng(progname.ToString.Trim)
                vSynopsis = Logger.RemSplCharsEng(synopsis.ToString.Trim)
            Else
                vProgName = Logger.RemSplCharsAllLangs(progname.ToString.Trim, Convert.ToInt32(LanguageID.ToString))
                vSynopsis = Logger.RemSplCharsAllLangs(synopsis.ToString.Trim, Convert.ToInt32(LanguageID.ToString))
            End If
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("sp_mst_ProgramRegional", "Rowid~ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                        "Int~Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                         "0~" & ProgId & "~" & vProgName & "~" & vSynopsis & "~" & vEpisodeNo & "~" & LanguageID.ToString.Trim & "~" & action.ToString.Trim & "~" & User.Identity.Name, True, False)

        Catch ex As Exception
            Logger.LogError("Edit Discrepancy Central", action & " exec_ProcRegionalSynopsis", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class