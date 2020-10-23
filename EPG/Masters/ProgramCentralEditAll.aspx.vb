Imports System
Imports System.Data.SqlClient
Public Class ProgramCentralEditAll
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                Try
                    Dim strsearch As String = Request.QueryString("search").ToString.Replace("^", "'")
                    chkExact.Checked = Request.QueryString("exact")
                    txtProgName.Text = strsearch
                    grdProgramCentral.DataBind()
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    
    Private Sub grdProgramCentral_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgramCentral.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header
                    e.Row.CssClass = "locked"
                Case DataControlRowType.DataRow
                    
                    Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)
                    Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                    Dim lbEngProg As Label = DirectCast(e.Row.FindControl("lbEngProg"), Label)
                    Dim lbEngSyn As Label = DirectCast(e.Row.FindControl("lbEngSyn"), Label)
                    Dim lbHinProg As Label = DirectCast(e.Row.FindControl("lbHinProg"), Label)
                    Dim lbHinSyn As Label = DirectCast(e.Row.FindControl("lbHinSyn"), Label)
                    Dim lbTamProg As Label = DirectCast(e.Row.FindControl("lbTamProg"), Label)
                    Dim lbTamSyn As Label = DirectCast(e.Row.FindControl("lbTamSyn"), Label)
                    Dim lbMarProg As Label = DirectCast(e.Row.FindControl("lbMarProg"), Label)
                    Dim lbMarSyn As Label = DirectCast(e.Row.FindControl("lbMarSyn"), Label)
                    Dim lbTelProg As Label = DirectCast(e.Row.FindControl("lbTelProg"), Label)
                    Dim lbTelSyn As Label = DirectCast(e.Row.FindControl("lbTelSyn"), Label)
                    Dim lbEpisodeNo As Label = DirectCast(e.Row.FindControl("lbEpisodeNo"), Label)
                    Dim lbInEPG As Label = DirectCast(e.Row.FindControl("lbInEPG"), Label)
                    Dim lbisduplicate As Label = DirectCast(e.Row.FindControl("lbisduplicate"), Label)

                    Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
                    Dim lbEngLangId As Label = DirectCast(e.Row.FindControl("lbEngLang"), Label)
                    Dim lbHinLangId As Label = DirectCast(e.Row.FindControl("lbHinLang"), Label)
                    Dim lbTamLangId As Label = DirectCast(e.Row.FindControl("lbTamLang"), Label)
                    Dim lbMarLangId As Label = DirectCast(e.Row.FindControl("lbMarLang"), Label)
                    Dim lbTelLangId As Label = DirectCast(e.Row.FindControl("lbTelLang"), Label)

                    Dim hyEngEdit As HyperLink = DirectCast(e.Row.FindControl("hyEngEdit"), HyperLink)
                    Dim hyHinEdit As HyperLink = DirectCast(e.Row.FindControl("hyHinEdit"), HyperLink)
                    Dim hyTamEdit As HyperLink = DirectCast(e.Row.FindControl("hyTamEdit"), HyperLink)
                    Dim hyMarEdit As HyperLink = DirectCast(e.Row.FindControl("hyMarEdit"), HyperLink)
                    Dim hyTelEdit As HyperLink = DirectCast(e.Row.FindControl("hyTelEdit"), HyperLink)

                    Dim hyProgName As HyperLink = DirectCast(e.Row.FindControl("hyProgName"), HyperLink)


                    hyProgName.NavigateUrl = "javascript:openProgSchedule('" & lbProgId.Text.Trim & "')"
                    'hyEngEdit.NavigateUrl = "javascript:openWin('" & ddlChannelName.SelectedValue.Trim & "','1','1')"
                    hyEngEdit.NavigateUrl = "javascript:openWin('" & lbChannelId.Text.Trim & "','" & txtProgName.Text.Replace("'", "^") & "','" & lbProgId.Text.Trim & "','" & lbEngLangId.Text & "','" & IIf(lbEngProg.Text.Contains("~"), "Insert", "Update") & "','English','" & lbProgName.Text.Replace("'", "^") & "','MainContent_grdProgramCentral_hyEngEdit_" & e.Row.RowIndex & "','" & lbEpisodeNo.Text.Trim & "','" & chkExact.Checked & "')"
                    hyHinEdit.NavigateUrl = "javascript:openWin('" & lbChannelId.Text.Trim & "','" & txtProgName.Text.Replace("'", "^") & "','" & lbProgId.Text.Trim & "','" & lbHinLangId.Text & "','" & IIf(lbHinProg.Text.Contains("~"), "Insert", "Update") & "','Hindi','" & lbProgName.Text.Replace("'", "^") & "','MainContent_grdProgramCentral_hyHinEdit_" & e.Row.RowIndex & "','" & lbEpisodeNo.Text.Trim & "','" & chkExact.Checked & "')"
                    hyTamEdit.NavigateUrl = "javascript:openWin('" & lbChannelId.Text.Trim & "','" & txtProgName.Text.Replace("'", "^") & "','" & lbProgId.Text.Trim & "','" & lbTamLangId.Text & "','" & IIf(lbTamProg.Text.Contains("~"), "Insert", "Update") & "','Tamil','" & lbProgName.Text.Replace("'", "^") & "','MainContent_grdProgramCentral_hyTamEdit_" & e.Row.RowIndex & "','" & lbEpisodeNo.Text.Trim & "','" & chkExact.Checked & "')"
                    hyMarEdit.NavigateUrl = "javascript:openWin('" & lbChannelId.Text.Trim & "','" & txtProgName.Text.Replace("'", "^") & "','" & lbProgId.Text.Trim & "','" & lbMarLangId.Text & "','" & IIf(lbMarProg.Text.Contains("~"), "Insert", "Update") & "','Marathi','" & lbProgName.Text.Replace("'", "^") & "','MainContent_grdProgramCentral_hyMarEdit_" & e.Row.RowIndex & "','" & lbEpisodeNo.Text.Trim & "','" & chkExact.Checked & "')"
                    hyTelEdit.NavigateUrl = "javascript:openWin('" & lbChannelId.Text.Trim & "','" & txtProgName.Text.Replace("'", "^") & "','" & lbProgId.Text.Trim & "','" & lbTelLangId.Text & "','" & IIf(lbTelProg.Text.Contains("~"), "Insert", "Update") & "','Telugu','" & lbProgName.Text.Replace("'", "^") & "','MainContent_grdProgramCentral_hyTelEdit_" & e.Row.RowIndex & "','" & lbEpisodeNo.Text.Trim & "','" & chkExact.Checked & "')"

                    If (User.IsInRole("ADMIN") Or User.IsInRole("USER") Or User.IsInRole("SUPERUSER")) Then
                        hyEngEdit.Enabled = True
                        hyHinEdit.Enabled = True
                        hyTamEdit.Enabled = True
                        hyMarEdit.Enabled = True
                        hyTelEdit.Enabled = True
                    ElseIf (User.IsInRole("HINDI")) Then
                        hyEngEdit.Enabled = False
                        hyHinEdit.Enabled = True
                        hyTamEdit.Enabled = False
                        hyMarEdit.Enabled = False
                        hyTelEdit.Enabled = False
                    ElseIf (User.IsInRole("ENGLISH")) Then
                        hyEngEdit.Enabled = True
                        hyHinEdit.Enabled = False
                        hyTamEdit.Enabled = False
                        hyMarEdit.Enabled = False
                        hyTelEdit.Enabled = False
                    ElseIf (User.IsInRole("MARATHI")) Then
                        hyEngEdit.Enabled = False
                        hyHinEdit.Enabled = False
                        hyTamEdit.Enabled = False
                        hyMarEdit.Enabled = True
                        hyTelEdit.Enabled = False
                    ElseIf (User.IsInRole("TELUGU")) Then
                        hyEngEdit.Enabled = False
                        hyHinEdit.Enabled = True
                        hyTamEdit.Enabled = False
                        hyMarEdit.Enabled = False
                        hyTelEdit.Enabled = True
                    ElseIf (User.IsInRole("TAMIL")) Then
                        hyEngEdit.Enabled = False
                        hyHinEdit.Enabled = False
                        hyTamEdit.Enabled = True
                        hyMarEdit.Enabled = False
                        hyTelEdit.Enabled = False
                    End If

                    If lbEngSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(3).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(3).Font.Bold = True
                    End If
                    If lbHinSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(6).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(6).Font.Bold = True
                    End If

                    If lbTamSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(9).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(9).Font.Bold = True
                    End If

                    If lbMarSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(12).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(12).Font.Bold = True
                    End If

                    If lbTelProg.Text.ToString.Length <= 10 Then
                        e.Row.Cells(15).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(15).Font.Bold = True
                    End If

                    If lbProgName.Text.ToString.Length <= 5 Then
                        e.Row.Cells(1).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(1).Font.Bold = True
                    End If
                    If lbEngProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(2).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(2).Font.Bold = True
                    End If
                    If lbHinProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(5).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(5).Font.Bold = True
                    End If
                    If lbTamProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(8).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(8).Font.Bold = True
                    End If
                    If lbMarProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(11).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(11).Font.Bold = True
                    End If
                    If lbTelProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(14).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(14).Font.Bold = True
                    End If

                    lbEngSyn.Text = lbEngSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbHinSyn.Text = lbHinSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbTamSyn.Text = lbTamSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbMarSyn.Text = lbMarSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbTelSyn.Text = lbTelSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")

                    If lbisduplicate.Text = "1" Then
                        e.Row.BackColor = Drawing.Color.PaleVioletRed
                    End If

                    If lbInEPG.Text = "1" Then
                        e.Row.BackColor = Drawing.Color.LightGreen

                    End If

                    

                    
            End Select
        Catch ex As Exception
            Logger.LogError("Program Central Search", "grdProgramCentral_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

End Class