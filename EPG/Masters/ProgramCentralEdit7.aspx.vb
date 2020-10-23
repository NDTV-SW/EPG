Imports System
Imports System.Data.SqlClient
Imports ExcelLibrary
Public Class ProgramCentralEdit7
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

                lbChannel.Text = txtChannel.Text
                getSynopsisList()
                Try
                    Dim channelOnair As String = Request.QueryString("onair").ToString
                    Dim index As String = Request.QueryString("index").ToString
                    txtChannel.Text = index
                    ddlChannelOnair1.SelectedValue = channelOnair
                    grdProgramCentral.DataBind()
                Catch ex As Exception
                    'Logger.LogError("ProgramCentralEdit", "Page_Load", ex.Message.ToString, User.Identity.Name)
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub TextChanged_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        Try
            grdProgramCentral.PageIndex = 0
            grdProgramCentral.DataBind()
            lbChannel.Text = txtChannel.Text
            getSynopsisList()
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "TextChanged_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
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
                    Dim lbArbProg As Label = DirectCast(e.Row.FindControl("lbArbProg"), Label)
                    Dim lbArbSyn As Label = DirectCast(e.Row.FindControl("lbArbSyn"), Label)
                    Dim lbColorCode As Label = DirectCast(e.Row.FindControl("lbColorCode"), Label)
                    Dim lbEpisodeNo As Label = DirectCast(e.Row.FindControl("lbEpisodeNo"), Label)

                    Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
                    Dim lbEngLangId As Label = DirectCast(e.Row.FindControl("lbEngLang"), Label)
                    Dim lbHinLangId As Label = DirectCast(e.Row.FindControl("lbHinLang"), Label)
                    Dim lbBenLangId As Label = DirectCast(e.Row.FindControl("lbBenLang"), Label)
                    Dim lbKanLangId As Label = DirectCast(e.Row.FindControl("lbKanLang"), Label)
                    Dim lbArbLangId As Label = DirectCast(e.Row.FindControl("lbArbLang"), Label)

                    Dim hyEngEdit As HyperLink = DirectCast(e.Row.FindControl("hyEngEdit"), HyperLink)
                    Dim hyHinEdit As HyperLink = DirectCast(e.Row.FindControl("hyHinEdit"), HyperLink)
                    Dim hyBenEdit As HyperLink = DirectCast(e.Row.FindControl("hyBenEdit"), HyperLink)
                    Dim hyKanEdit As HyperLink = DirectCast(e.Row.FindControl("hyKanEdit"), HyperLink)
                    Dim hyArbEdit As HyperLink = DirectCast(e.Row.FindControl("hyArbEdit"), HyperLink)

                    'hyEngEdit.NavigateUrl = "javascript:openWin('" & ddlChannelName.SelectedValue.Trim & "','1','1')"
                    hyEngEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair1.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbEngLangId.Text & "','MainContent_grdProgramCentral_hyEngEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyHinEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair1.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbHinLangId.Text & "','MainContent_grdProgramCentral_hyHinEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyBenEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair1.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbBenLangId.Text & "','MainContent_grdProgramCentral_hyBenEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyKanEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair1.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbKanLangId.Text & "','MainContent_grdProgramCentral_hyKanEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyArbEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair1.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbArbLangId.Text & "','MainContent_grdProgramCentral_hyArbEdit_" & e.Row.RowIndex & "','" & txtChannel.Text & "','" & lbEpisodeNo.Text.Trim & "')"



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
                    If lbArbSyn.Text.ToString.Length <= 10 Then
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
                    If lbBenProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(7).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(7).Font.Bold = True
                    End If
                    If lbKanProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(10).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(10).Font.Bold = True
                    End If

                    If lbArbProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(13).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(13).Font.Bold = True
                    End If

                    lbEngSyn.Text = lbEngSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbHinSyn.Text = lbHinSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbBenSyn.Text = lbBenSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbKanSyn.Text = lbKanSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbArbSyn.Text = lbArbSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
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

    Private Sub exportExcel(ByVal channelid As String, ByVal languagename As String, ByVal languageid As String)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from fn_regional_prog_exportformat('" & channelid & "','" & languageid & "')", False)

        dt.Columns("Channelid").ColumnName = "Channel"
        dt.Columns("Program Name").ColumnName = "English Programme Name"
        dt.Columns("Synopsis").ColumnName = "English Synopsis"
        dt.Columns("Regional ProgName").ColumnName = languagename & " Programme"
        dt.Columns("Regional Synopsis").ColumnName = languagename & "_Synopsis"

        'dt.Columns.Remove("")

        Dim workbook As SpreadSheet.Workbook = New SpreadSheet.Workbook()
        Dim worksheet As SpreadSheet.Worksheet
        Dim iRow As Integer = 0
        Dim iCol As Integer = 0
        Dim sTemp As String = String.Empty
        Dim dTemp As Double = 0
        Dim iTemp As Integer = 0

        Dim count As Integer = 0
        Dim iTotalRows As Integer = 0
        Dim iSheetCount As Integer = 0

        worksheet = New SpreadSheet.Worksheet("Sheet " & iSheetCount.ToString())
        iSheetCount = iSheetCount + 1

        'Write Table Header
        For Each dc As DataColumn In dt.Columns
            worksheet.Cells(iRow, iCol) = New SpreadSheet.Cell(dc.ColumnName)
            iCol = iCol + 1
        Next

        'Write Table Data
        iRow = 2
        For Each dr As DataRow In dt.Rows
            iCol = 0
            For Each dc As DataColumn In dt.Columns
                sTemp = dr(dc.ColumnName).ToString()
                worksheet.Cells(iRow - 1, iCol) = New SpreadSheet.Cell(sTemp)

                iCol = iCol + 1
            Next
            iRow = iRow + 1
        Next


        worksheet.Cells.ColumnWidth(0) = 5000
        worksheet.Cells.ColumnWidth(1) = 7000
        worksheet.Cells.ColumnWidth(2) = 10000
        worksheet.Cells.ColumnWidth(3) = 7000
        worksheet.Cells.ColumnWidth(4) = 10000

        worksheet.SheetType = SpreadSheet.SheetType.Worksheet
        workbook.Worksheets.Add(worksheet)
        iTotalRows = iTotalRows + iRow

        'Bug on Excel Library, min file size must be 7 Kb
        'thus we need to add empty row for safety
        If iTotalRows < 100 Then
            worksheet = New SpreadSheet.Worksheet("Sheet X")
            count = 1
            Do While count < 100
                worksheet.Cells(count, 0) = New SpreadSheet.Cell(" ")
                count = count + 1
            Loop
            workbook.Worksheets.Add(worksheet)
        End If

        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = channelid & "_ProgrammeExport_" & languagename & "_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls"
        'Return strPath & strFileName

        Dim newFileName As String = strPath & strFileName
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & strFileName)
        Response.WriteFile(newFileName)
        Response.End()
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        exportExcel(txtChannel.Text, ddlLanguage.SelectedItem.Text, ddlLanguage.SelectedValue)
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

End Class