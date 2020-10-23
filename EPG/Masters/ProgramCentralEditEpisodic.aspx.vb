Imports System
Imports System.Data.SqlClient
Public Class ProgramCentralEditEpisodic
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)

    'Private Sub myErrorBox(ByVal errorstr As String)
    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    'End Sub

    'Private Sub myMessageBox(ByVal messagestr As String)
    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                Try

                    lbChannel.Text = txtChannel.Text
                    getSynopsisList()
                    txtChannel.Text = Request.QueryString("index").ToString
                    grdProgramCentral.DataBind()
                Catch ex As Exception
                    'Logger.LogError("ProgramCentralEditEpisodic", "Page_Load", ex.Message.ToString, User.Identity.Name)
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("ProgramCentralEditEpisodic", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub txtChannel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        Try
            grdProgramCentral.PageIndex = 0
            grdProgramCentral.DataBind()
            lbChannel.Text = txtChannel.Text
            getSynopsisList()
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "txtChannel_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub getSynopsisList()
        lbSynopsisNeeded.Text = ""
        If MyConnection.State = ConnectionState.Closed Then
            MyConnection.Open()
        End If
        Dim cmd As New SqlCommand("select b.FullName from mst_ChannelRegionalName a join mst_language b on a.LanguageId =b.LanguageID where  SynopsisNeeded =1 and ChannelId='" & lbChannel.Text & "'", MyConnection)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader

        If dr.HasRows Then

            While dr.Read()
                lbSynopsisNeeded.Text = lbSynopsisNeeded.Text & dr("FullName").ToString & ", "
            End While
            lbSynopsisNeeded.Text = lbSynopsisNeeded.Text.Substring(0, lbSynopsisNeeded.Text.Length - 2)
        End If
        dr.Close()
        MyConnection.Dispose()
    End Sub

    Private Sub grdProgramCentral_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgramCentral.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header
                    e.Row.CssClass = "locked"
                Case DataControlRowType.DataRow
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
                    Dim lbColorCode As Label = DirectCast(e.Row.FindControl("lbColorCode"), Label)
                    Dim lbEpisodeNo As Label = DirectCast(e.Row.FindControl("lbEpisodeNo"), Label)

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

                    'hyEngEdit.NavigateUrl = "javascript:openWin('" & txtChannel.SelectedValue.Trim & "','1','1')"
                    hyEngEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbEngLangId.Text & "','MainContent_grdProgramCentral_hyEngEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyHinEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbHinLangId.Text & "','MainContent_grdProgramCentral_hyHinEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyTamEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbTamLangId.Text & "','MainContent_grdProgramCentral_hyTamEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyMarEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbMarLangId.Text & "','MainContent_grdProgramCentral_hyMarEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyTelEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbTelLangId.Text & "','MainContent_grdProgramCentral_hyTelEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"

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
                        e.Row.Cells(2).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(2).Font.Bold = True
                    End If
                    If lbHinSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(5).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(5).Font.Bold = True
                    End If

                    If lbTamSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(8).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(8).Font.Bold = True
                    End If

                    If lbMarSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(11).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(11).Font.Bold = True
                    End If

                    If lbTelProg.Text.ToString.Length <= 10 Then
                        e.Row.Cells(14).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(14).Font.Bold = True
                    End If

                    If lbProgName.Text.ToString.Length <= 5 Then
                        e.Row.Cells(0).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(0).Font.Bold = True
                    End If
                    If lbEngProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(1).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(1).Font.Bold = True
                    End If
                    If lbHinProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(4).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(4).Font.Bold = True
                    End If
                    If lbTamProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(7).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(7).Font.Bold = True
                    End If
                    If lbMarProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(10).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(10).Font.Bold = True
                    End If
                    If lbTelProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(13).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(13).Font.Bold = True
                    End If

                    lbEngSyn.Text = lbEngSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbHinSyn.Text = lbHinSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbTamSyn.Text = lbTamSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbMarSyn.Text = lbMarSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbTelSyn.Text = lbTelSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    ' 0 for programmes not in mst_epg, 1 for programmes in mst_epg.
                    ' 2 for programmes airing in next 7 days.
                    If lbColorCode.Text.Trim = 1 Then
                        e.Row.BackColor = Drawing.Color.LightBlue
                    ElseIf lbColorCode.Text.Trim = 2 Then
                        e.Row.BackColor = Drawing.Color.LightGreen
                    End If
                    '    Case DataControlRowType.Footer

            End Select
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "grdProgramCentral_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannelsEpi(prefixText, count)
        Return channels
    End Function

End Class