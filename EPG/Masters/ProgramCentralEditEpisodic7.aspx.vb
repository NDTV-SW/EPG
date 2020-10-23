Imports System
Imports System.Data.SqlClient
Public Class ProgramCentralEditEpisodic7
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)

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

                    lbChannel.Text = txtChannel.Text
                    getSynopsisList()
                    txtChannel.Text = Request.QueryString("index").ToString
                    grdProgramCentral.DataBind()
                Catch ex As Exception
                    'Logger.LogError("ProgramCentralEditEpisodic7", "Page_Load", ex.Message.ToString, User.Identity.Name)
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("ProgramCentralEditEpisodic7", "Page Load", ex.Message.ToString, User.Identity.Name)
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
                    Dim lbBenProg As Label = DirectCast(e.Row.FindControl("lbBenProg"), Label)
                    Dim lbBenSyn As Label = DirectCast(e.Row.FindControl("lbBenSyn"), Label)
                    Dim lbKanProg As Label = DirectCast(e.Row.FindControl("lbKanProg"), Label)
                    Dim lbKanSyn As Label = DirectCast(e.Row.FindControl("lbKanSyn"), Label)

                    ' '' '' Comment added by Shashank on 22Sept 2020

                    Dim lbArbProg As Label = DirectCast(e.Row.FindControl("lbArbProg"), Label)
                    Dim lbArbSyn As Label = DirectCast(e.Row.FindControl("lbArbSyn"), Label)
                    ' '' ''

                    Dim lbColorCode As Label = DirectCast(e.Row.FindControl("lbColorCode"), Label)
                    Dim lbEpisodeNo As Label = DirectCast(e.Row.FindControl("lbEpisodeNo"), Label)

                    Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
                    Dim lbEngLangId As Label = DirectCast(e.Row.FindControl("lbEngLang"), Label)
                    Dim lbHinLangId As Label = DirectCast(e.Row.FindControl("lbHinLang"), Label)
                    Dim lbBenLangId As Label = DirectCast(e.Row.FindControl("lbBenLang"), Label)
                    Dim lbKanLangId As Label = DirectCast(e.Row.FindControl("lbKanLang"), Label)

                    ' '' ''Comment added by Shashank on 22Sept 2020
                    Dim lbArbLangId As Label = DirectCast(e.Row.FindControl("lbArbLang"), Label)
                    ' '' ''

                    Dim hyEngEdit As HyperLink = DirectCast(e.Row.FindControl("hyEngEdit"), HyperLink)
                    Dim hyHinEdit As HyperLink = DirectCast(e.Row.FindControl("hyHinEdit"), HyperLink)
                    Dim hyBenEdit As HyperLink = DirectCast(e.Row.FindControl("hyBenEdit"), HyperLink)
                    Dim hyKanEdit As HyperLink = DirectCast(e.Row.FindControl("hyKanEdit"), HyperLink)

                    ' '' ''Comment added by Shashank on 22Sept 2020
                    Dim hyArbEdit As HyperLink = DirectCast(e.Row.FindControl("hyArbEdit"), HyperLink)
                    ' '' ''

                    'hyEngEdit.NavigateUrl = "javascript:openWin('" & txtChannel.SelectedValue.Trim & "','1','1')"
                    hyEngEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbEngLangId.Text & "','MainContent_grdProgramCentral_hyEngEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyHinEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbHinLangId.Text & "','MainContent_grdProgramCentral_hyHinEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyBenEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbBenLangId.Text & "','MainContent_grdProgramCentral_hyBenEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyKanEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbKanLangId.Text & "','MainContent_grdProgramCentral_hyKanEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"

                    ' '' ''Comment added by Shashank on 22Sept 2020
                    hyArbEdit.NavigateUrl = "javascript:openWin('" & lbProgId.Text.Trim & "','" & lbArbLangId.Text & "','MainContent_grdProgramCentral_hyArbEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    ' '' ''

                    If lbEngSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(2).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(2).Font.Bold = True
                    End If
                    If lbHinSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(5).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(5).Font.Bold = True
                    End If

                    If lbBenSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(8).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(8).Font.Bold = True
                    End If

                    If lbKanSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(11).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(11).Font.Bold = True
                    End If

                    ' '' ''
                    If lbArbSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(14).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(14).Font.Bold = True
                    End If

                    ' '' ''

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

                    If lbBenProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(7).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(7).Font.Bold = True
                    End If

                    If lbKanProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(10).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(10).Font.Bold = True
                    End If

                    ' '' ''
                    If lbArbProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(13).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(13).Font.Bold = True
                    End If
                    ' '' ''

                    lbEngSyn.Text = lbEngSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbHinSyn.Text = lbHinSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbBenSyn.Text = lbBenSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbKanSyn.Text = lbKanSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")

                    ' '' ''
                    lbArbSyn.Text = lbArbSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    ' '' ''

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