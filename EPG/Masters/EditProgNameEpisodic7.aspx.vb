Imports System
Imports System.Data.SqlClient

Public Class EditProgNameEpisodic7
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

                Dim obj As New clsExecute

                Dim dtEpi As DataTable = obj.executeSQL("select top 1 episodictitle,episodicsynopsis from mst_epg where progid='" & lbProgId.Text & "' and episodeno='" & lbEpisodeNo.Text & "' and (episodictitle is not null or episodicsynopsis is not null)", False)
                If dtEpi.Rows.Count > 0 Then
                    lbEpiProgname.Text = dtEpi.Rows(0).Item("episodictitle").ToString
                    lbEpiSynopsis.Text = dtEpi.Rows(0).Item("episodicsynopsis").ToString
                End If

                Dim dt As DataTable = obj.executeSQL("select top 1 b.episodictitle,a.ChannelId,a.ProgName [engProg],b.progName [regprog], b.synopsis [regsynopsis], b.LanguageId,c.FullName [language] from mst_program a join mst_programregional b on a.progid='" & lbProgId.Text.Trim & "' and a.progid=b.progid join  mst_language c on b.languageid=c.LanguageID and c.LanguageID='" & lbLangId.Text.Trim & "' and b.episodeno='" & lbEpisodeNo.Text & "'", False)

                If dt.Rows.Count > 0 Then
                    BtnSubmit.Text = "Update"

                    txtProgname.Text = dt.Rows(0).Item("regprog").ToString
                    txtSynopsis.Text = dt.Rows(0).Item("regsynopsis").ToString
                    lbChannel.Text = dt.Rows(0).Item("channelid").ToString
                    lbLanguage.Text = dt.Rows(0).Item("language").ToString
                    lbProgramme.Text = dt.Rows(0).Item("engProg").ToString
                    ' '' '' Shashank
                    txtTittle.Text = dt.Rows(0).Item("episodictitle").ToString

                Else
                    Dim dtNew As DataTable = obj.executeSQL("select top 1 ChannelId,ProgName from mst_program where ProgID='" & lbProgId.Text.Trim & "'", False)

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
            Dim obj As New clsExecute
            If BtnSubmit.Text = "Insert" Then
                exec_ProcRegional(lbProgId.Text.Trim, txtProgname.Text.Trim, txtSynopsis.Text.Trim, lbLangId.Text.Trim, 0, "A", lbEpisodeNo.Text.Trim)
            ElseIf BtnSubmit.Text = "Update" Then
                exec_ProcRegional(lbProgId.Text.Trim, txtProgname.Text.Trim, txtSynopsis.Text.Trim, lbLangId.Text.Trim, 0, "C", lbEpisodeNo.Text.Trim)

                If lbLangId.Text = 1 Then
                    obj.executeSQL("update mst_epg set episodictitle='" & txtProgname.Text.Trim.Replace("'", "''") & "',episodicsynopsis='" & txtSynopsis.Text.Trim.Replace("'", "''") & "' where progid='" & lbProgId.Text & "' and episodeno='" & lbEpisodeNo.Text & "'", False)
                End If

            End If
            obj.executeSQL("update mst_programregional set episodictitle=N'" & txtTittle.Text.Trim.Replace("'", "''") & "' where progid='" & lbProgId.Text & "' and episodeno='" & lbEpisodeNo.Text & "' and languageid='" & lbLangId.Text.Trim & "'", False)

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
                vProgName = Logger.RemSplCharsEng(ProgName.Trim)
                vSynopsis = Logger.RemSplCharsEng(synopsis.Trim)
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