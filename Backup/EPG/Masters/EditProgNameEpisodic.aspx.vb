Imports System
Imports System.Data.SqlClient

Public Class EditProgNameEpisodic
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
                lbProgId.Text = Request.QueryString("ProgId").ToString
                lbLangId.Text = Request.QueryString("LangId").ToString
                lbEpisodeNo.Text = Request.QueryString("episodeNo").ToString

                Dim adpProgSyn As New SqlDataAdapter("select top 1 a.ChannelId,a.ProgName [engProg],b.progName [regprog], b.synopsis [regsynopsis], b.LanguageId,c.FullName [language] from mst_program a join mst_programregional b on a.progid='" & lbProgId.Text.Trim & "' and a.progid=b.progid join  mst_language c on b.languageid=c.LanguageID and c.LanguageID='" & lbLangId.Text.Trim & "' and b.episodeno='" & lbEpisodeNo.Text & "'", ConString)
                Dim dt As New DataTable
                adpProgSyn.Fill(dt)
                If dt.Rows.Count > 0 Then
                    BtnSubmit.Text = "Update"

                    txtProgname.Text = dt.Rows(0).Item("regprog").ToString
                    txtSynopsis.Text = dt.Rows(0).Item("regsynopsis").ToString
                    lbChannel.Text = dt.Rows(0).Item("channelid").ToString
                    lbLanguage.Text = dt.Rows(0).Item("language").ToString
                    lbProgramme.Text = dt.Rows(0).Item("engProg").ToString

                Else
                    Dim adpNew As New SqlDataAdapter("select top 1 ChannelId,ProgName from mst_program where ProgID='" & lbProgId.Text.Trim & "'", ConString)
                    Dim dtNew As New DataTable
                    adpNew.Fill(dtNew)
                    If dtNew.Rows.Count > 0 Then
                        BtnSubmit.Text = "Insert"
                        txtProgname.Text = ""
                        txtSynopsis.Text = ""
                        lbChannel.Text = dtNew.Rows(0).Item("channelid").ToString
                        lbProgramme.Text = dtNew.Rows(0).Item("ProgName").ToString
                    Else
                        hfSend.Value = "2"
                    End If

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
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("sp_mst_program", "ProgId~Progname~Action~ActionUser", "Int~nVarChar~Char~VarChar", ProgId & "~" & Logger.RemSplCharsEng(ProgName.ToString.Trim) & "~" & action & "~" & User.Identity.Name, True, False)
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