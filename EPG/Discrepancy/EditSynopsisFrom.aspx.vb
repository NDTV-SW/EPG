Imports System
Imports System.Data.SqlClient

Public Class EditSynopsisFrom
    Inherits System.Web.UI.Page

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try

                'lbChannel.Text = Request.QueryString("channelid").ToString
                lbEpisodeno.Text = Request.QueryString("episodeno").ToString
                lbProgId.Text = Request.QueryString("progid").ToString
                
                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL("select progName,synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='1' and episodeno='" & lbEpisodeno.Text & "'", False)

                lbEngProgName.Text = dt.Rows(0)(0).ToString
                lbEngSynopsis.Text = dt.Rows(0)(1).ToString

                Dim dtregProgSyn As DataTable = obj.executeSQL("select progName,synopsis from mst_programregional where progid='" & lbProgId.Text & "' and LanguageId='1' and episodeno='" & lbEpisodeno.Text & "'", False)

                lbRegProgName.Text = lbLanguage.Text & " Programme"
                lbRegSynopsis.Text = lbLanguage.Text & " Synopsis"

                txtProgname.Text = dtregProgSyn.Rows(0)(0).ToString
                txtSynopsis.Text = dtregProgSyn.Rows(0)(1).ToString

            Catch ex As Exception
                Logger.LogError("EditSynopsisFrom", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Try
            exec_Proc(lbProgId.Text.Trim, txtProgname.Text.Trim, txtSynopsis.Text, 1, "C", lbEpisodeno.Text)
            hfSend.Value = "2"
        Catch ex As Exception
            lberror.Text = ex.Message.ToString.Trim
            lberror.Visible = True
            Logger.LogError("EditSynopsisFrom", "BtnSubmit_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub exec_Proc(ByVal ProgId As Integer, ByVal ProgName As String, ByVal Synopsis As String, ByVal LangID As Integer, ByVal action As String, ByVal vEpisodeNo As Integer)
        Try
            Dim obj As New clsExecute
            obj.executeSQL("sp_mst_ProgramRegional", "ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                        "Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                         ProgId & "~" & ProgName & "~" & Synopsis & "~" & vEpisodeNo & "~" & LangID & "~C~" & User.Identity.Name, True, False)

        Catch ex As Exception
            Logger.LogError("EditSynopsisFrom", "exec_Proc", ex.Message.ToString, User.Identity.Name)
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
            obj.executeSQL("sp_mst_ProgramRegional", "ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                        "Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                         ProgId & "~" & vProgName & "~" & vSynopsis & "~" & vEpisodeNo & "~" & LanguageID.ToString.Trim & "~" & action.ToString.Trim & "~" & User.Identity.Name, True, False)

        Catch ex As Exception
            Logger.LogError("EditSynopsisFrom", action & " exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

End Class