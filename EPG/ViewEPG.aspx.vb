Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class ViewEPG
    Inherits System.Web.UI.Page
    Dim gridSelect As String
    Dim progduration As Integer
    Dim lbduration As Label

  
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            gridSelect = "0"
            If Page.IsPostBack = False Then
                lbEPGExists.Visible = False
                progduration = 0
                tbHead.Visible = False
                btnViewAll.Visible = False
                txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
            Else
                Dim obj As New Logger
                lbEPGExists.Visible = True
                lbEPGExists.Text = obj.GetEpgDates(txtChannel.Text)
            End If
        Catch ex As Exception
            Logger.LogError("View EPG", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        Try
            tbHead.Visible = True
            grdViewEPGBind()
            gridSelect = "Bind"
            Dim obj As New Logger
            lbEPGExists.Visible = True
            lbEPGExists.Text = obj.GetEpgDates(txtChannel.Text)
        Catch ex As Exception
            Logger.LogError("View EPG", "btnView_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnViewAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewAll.Click
        Try
            tbHead.Visible = True
            gridSelect = "AllBind"
            'grdViewEPGAllBind()
        Catch ex As Exception
            Logger.LogError("View EPG", "btnViewAll_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdViewEPG_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdViewEPG.PageIndexChanging
        Try
            grdViewEPG.PageIndex = e.NewPageIndex
            grdViewEPGBind()
        Catch ex As Exception
            Logger.LogError("View EPG", "grdViewEPG_PageIndexChanging", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Protected Sub grdViewEpg_Databound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdViewEPG.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header
                Case DataControlRowType.DataRow
                    Dim grdChildViewEpg As GridView
                    Dim lbProgId As Label, lbProgDate As Label, lbWeekDay As Label, lbWeekno As Label
                    lbProgId = TryCast(e.Row.FindControl("lbProgId"), Label)
                    lbProgDate = TryCast(e.Row.FindControl("lbProgDate"), Label)
                    lbWeekDay = TryCast(e.Row.FindControl("lbWeekDay"), Label)
                    lbWeekno = TryCast(e.Row.FindControl("lbWeekNumber"), Label)
                    grdChildViewEpg = TryCast(e.Row.FindControl("grdChildViewEpg"), GridView)
                    Dim strQuery As String

                    ' '' '' Added by Shashank on 10 Oct 2020
                    ' '' '' Sankalp Sir provided  some changes in episodeno to broadepisode

                    ' '' ''If txtChannel.Text = "NGC HD" Or txtChannel.Text = "DISCOVERY HD WORLD" Then
                    ' '' ''    strQuery = "select progid,GenreName,progtime,progname,Synopsis,duration,ProgtimeGMT,broadepisode EpisodeNo,sortby,EpisodicSynopsis,isduplicate,liverepeat,originalrepeat,ratingid,eseason,richmetaid,verified from fn_viewepg ('" & txtChannel.Text & "','" & ddlLanguage1.SelectedValue & "','" & Convert.ToDateTime(lbProgDate.Text).ToString("yyyy-MM-dd") & "') order by sortby"

                    ' '' ''Else
                    ' '' ''    strQuery = "select progid,GenreName,progtime,progname,Synopsis,duration,ProgtimeGMT,episodeno,sortby,EpisodicSynopsis,isduplicate,liverepeat,originalrepeat,ratingid,eseason,richmetaid,verified from fn_viewepg ('" & txtChannel.Text & "','" & ddlLanguage1.SelectedValue & "','" & Convert.ToDateTime(lbProgDate.Text).ToString("yyyy-MM-dd") & "') order by sortby"

                    ' '' ''End If
                    ' '' '' Changes Not Done
                    strQuery = "select * from fn_viewepg ('" & txtChannel.Text & "','" & ddlLanguage1.SelectedValue & "','" & Convert.ToDateTime(lbProgDate.Text).ToString("yyyy-MM-dd") & "') order by sortby"

                    Dim obj As New clsExecute
                    Dim dt As DataTable = obj.executeSQL(strQuery, False)
                    grdChildViewEpg.DataSource = dt
                    grdChildViewEpg.DataBind()

                    lbDateprog.Text = lbProgDate.Text
                    lbWeekDayprog.Text = lbWeekDay.Text
                    lbWeekNumberprog.Text = lbWeekno.Text

            End Select
        Catch ex As Exception
            Logger.LogError("View EPG", "grdViewEpg_Databound", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
    Dim count As Integer
    Dim startEPGTime As String, EndEPGTime As String
    Protected Sub grdChildViewEpg_Databound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header

                Case DataControlRowType.DataRow
                    If count = 1 Then
                        Dim lbprogdate As Label = DirectCast(e.Row.FindControl("lbProgdate"), Label)
                        Dim lbprogtime As Label = DirectCast(e.Row.FindControl("lbprogtime"), Label)
                        startEPGTime = lbprogdate.Text & " " & lbprogtime.Text
                    End If
                    Dim lbprogDateGMT As Label = DirectCast(e.Row.FindControl("lbprogDateGMT"), Label)
                    Dim lbisduplicate As Label = DirectCast(e.Row.FindControl("lbisduplicate"), Label)
                    If lbisduplicate.Text = "1" Then
                        e.Row.BackColor = Drawing.Color.PaleVioletRed
                    End If
                    Dim lbprogTimeGMT As Label = DirectCast(e.Row.FindControl("lbprogTimeGMT"), Label)
                    lbprogDateGMT.Text = Convert.ToDateTime(lbprogDateGMT.Text).ToString("ddMMMyyyy")
                    lbprogTimeGMT.Text = Convert.ToDateTime(lbprogTimeGMT.Text).ToString("HH:mmtt")

                    lbduration = DirectCast(e.Row.FindControl("lbduration"), Label)
                    progduration = progduration + Convert.ToInt32(lbduration.Text)
                    Dim lbEpisodicSynopsis As Label = DirectCast(e.Row.FindControl("lbEpisodicSynopsis"), Label)

                    If lbEpisodicSynopsis.Text = "Y" Then
                        e.Row.Cells(9).BackColor = Drawing.Color.LightSeaGreen
                        'lbEpisodicSynopsis.BackColor = Drawing.Color.LightSeaGreen
                    End If

                    Dim hyRichMetaId As HyperLink = DirectCast(e.Row.FindControl("hyRichMetaId"), HyperLink)
                    Dim chkVerified As CheckBox = DirectCast(e.Row.FindControl("chkVerified"), CheckBox)
                    If chkVerified.Checked Then
                        hyRichMetaId.BackColor = Drawing.Color.LawnGreen
                    End If
                    If hyRichMetaId.Text <> "0" Then
                        e.Row.Cells(0).BackColor = Drawing.Color.LightSeaGreen
                        hyRichMetaId.NavigateUrl = "javascript:openWin('" & hyRichMetaId.Text & "')"

                    End If

                Case DataControlRowType.Footer
                    e.Row.Cells(2).Text = "Total Duration"
                    e.Row.Cells(3).Text = progduration.ToString
                    lbTotalDuration.Text = progduration.ToString
                    If Not lbTotalDuration.Text = "1440" Then
                        If Convert.ToInt32(lbTotalDuration.Text) > 1440 Then
                            lbTotalDuration.Text = lbTotalDuration.Text & " (" & Convert.ToInt32(lbTotalDuration.Text) - 1440 & " minutes more than 24 hours.)"
                        Else
                            lbTotalDuration.Text = lbTotalDuration.Text & " (" & 1440 - Convert.ToInt32(lbTotalDuration.Text) & " minutes less than 24 hours.)"
                        End If

                        lbTotalDuration.ForeColor = System.Drawing.Color.Red
                    Else
                        lbTotalDuration.ForeColor = System.Drawing.Color.White
                    End If
                    lbEPGExists.Text = startEPGTime
            End Select
        Catch ex As Exception
            Logger.LogError("View EPG", "grdChildViewEpg_Databound", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
    Private Sub grdViewEPGBind()
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select distinct a.channelid,Convert(varchar(11),a.progdate,113) as progdate1,DATENAME(weekday,a.progdate) as 'WeekDay',DATEPART( WW,a.progdate) as WeekNumber,a.progdate from mst_epg a join mst_Channel b on a.ChannelID = b.ChannelID join mst_program c on a.progid = c.progid where a.ChannelID='" & txtChannel.Text & "' and a.progdate between '" & txtStartDate.Text & "' and '" & txtEndDate.Text & "' order by progdate,3 asc", False)
            grdViewEPG.DataSource = dt
            grdViewEPG.DataBind()
        Catch ex As Exception
            Logger.LogError("View EPG", "grdViewEPGBind", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
    
    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        Try
            Dim obj As New Logger
            lbEPGExists.Visible = True
            lbEPGExists.Text = obj.GetEpgDates(txtChannel.Text)
        Catch ex As Exception
            Logger.LogError("View EPG", "ddlChannelName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    'Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
    '    If txtStartDate.Text.Trim = "" Or txtEndDate.Text.Trim = "" Then
    '        myErrorBox("Please Select Start and End date")
    '        Exit Sub
    '    End If
    '    exportExcel1()

    'End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtStartDate.Text.Trim = "" Or txtEndDate.Text.Trim = "" Then
            'myErrorBox("Please Select Start and End date")
            Exit Sub
        End If
        ExportExcel()

    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtStartDate.Text.Trim = "" Or txtEndDate.Text.Trim = "" Then
            'myErrorBox("Please Select Start and End date")
            Exit Sub
        End If
        exportExcelGMT()

    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If txtStartDate.Text.Trim = "" Or txtEndDate.Text.Trim = "" Then
            'myErrorBox("Please Select Start and End date")
            Exit Sub
        End If
        ExportExcelEpisodic()

    End Sub


    Private Sub exportExcel1()
        Try
            grdViewEPGExport.Visible = True
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("sp_export_exportepg_toexcel", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & Convert.ToDateTime(txtStartDate.Text).Date & "~" & Convert.ToDateTime(txtEndDate.Text).Date, True, False)

            If dt.Rows.Count < 2 Then
                'myErrorBox("No record Found for these Dates")
                Exit Sub
            End If

            grdViewEPGExport.DataSource = dt
            grdViewEPGExport.DataBind()
            Response.ClearContent()

            Dim strFileName As String = txtChannel.Text.ToString.Replace(" ", "") & "_EPG_" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & ".xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & strFileName)
            Response.Charset = ""
            'Response.ContentType = "application/ms-excel"
            'Response.ContentType = "application/vnd.ms-excel"
            Response.ContentType = "application/vnd.xls"
            'Response.ContentEncoding = New System.Text.UTF8Encoding()
            ' Add the HTML from the GridView to a StringWriter so we can write it out later

            Dim sw As System.IO.StringWriter = New System.IO.StringWriter
            Dim hw As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(sw)

            grdViewEPGExport.RenderControl(hw)

            Response.Write(sw.ToString)
            Response.End()
            grdViewEPGExport.Visible = False
        Catch ex As Exception
            'Logger.LogError("View EPG", "exportExcel1", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub exportExcelGMT()
        grdViewEPGExport.Visible = True

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select Progname,AirDate,AirTime,Duration,ProgTimeGMT,Synopsis,EpisodeNo from fn_viewepg_export('" & txtChannel.Text & "','" & ddlLanguage1.SelectedValue & "','" & Convert.ToDateTime(txtStartDate.Text.Trim) & "','" & Convert.ToDateTime(txtEndDate.Text.Trim) & "') order by sortby1,sortby2", False)

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


        worksheet.Cells.ColumnWidth(0) = 8000
        worksheet.Cells.ColumnWidth(1) = 5000
        worksheet.Cells.ColumnWidth(2) = 4000
        worksheet.Cells.ColumnWidth(3) = 4000
        worksheet.Cells.ColumnWidth(4) = 4000
        worksheet.Cells.ColumnWidth(5) = 8000

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

        Dim newFileName As String = getXLSFileName()
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & txtChannel.Text.Replace(" ", "") & "_GMT_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
    Private Sub ExportExcel()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_export_exportepg_toexcel", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & Convert.ToDateTime(txtStartDate.Text).Date & "~" & Convert.ToDateTime(txtEndDate.Text).Date, True, False)

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


        worksheet.Cells.ColumnWidth(0) = 8000
        worksheet.Cells.ColumnWidth(1) = 5000
        worksheet.Cells.ColumnWidth(2) = 4000
        worksheet.Cells.ColumnWidth(3) = 4000
        worksheet.Cells.ColumnWidth(4) = 4000
        worksheet.Cells.ColumnWidth(5) = 8000

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

        Dim newFileName As String = getXLSFileName()
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & txtChannel.Text.Replace(" ", "") & "_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

    Private Sub ExportExcelEpisodic()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_export_exportepg_toexcel_episodic", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & Convert.ToDateTime(txtStartDate.Text).Date & "~" & Convert.ToDateTime(txtEndDate.Text).Date, True, False)

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


        worksheet.Cells.ColumnWidth(0) = 8000
        worksheet.Cells.ColumnWidth(1) = 5000
        worksheet.Cells.ColumnWidth(2) = 4000
        worksheet.Cells.ColumnWidth(3) = 4000
        worksheet.Cells.ColumnWidth(4) = 4000
        worksheet.Cells.ColumnWidth(5) = 8000

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

        Dim newFileName As String = getXLSFileName()
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & txtChannel.Text.Replace(" ", "") & "_Episodic_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = txtChannel.Text.Replace(" ", "") & " " & Convert.ToDateTime(txtStartDate.Text).ToString("ddMMMyyyy") & ".xls"
        Return strPath & strFileName
    End Function


    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Protected Sub grdViewEPG_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdViewEPG.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow

                    Dim grdChildViewEpg As GridView = DirectCast(e.Row.FindControl("grdChildViewEpg"), GridView)
                    If chkViewGMT.Checked = True Then
                        grdChildViewEpg.Columns(5).Visible = True
                        grdChildViewEpg.Columns(6).Visible = True
                    Else
                        grdChildViewEpg.Columns(5).Visible = False
                        grdChildViewEpg.Columns(6).Visible = False
                    End If

            End Select
        Catch ex As Exception

        End Try
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    Protected Sub btnEntertainment_Click(sender As Object, e As EventArgs) Handles btnEntertainment.Click
        ExportEntertainment()
    End Sub

    Protected Sub btnMovie_Click(sender As Object, e As EventArgs) Handles btnMovie.Click
        ExportMovie()
    End Sub

    Protected Sub btnSports_Click(sender As Object, e As EventArgs) Handles btnSports.Click
        ExportSports()
    End Sub

    Private Sub ExportSports()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_export_exportepg_toexcel_sports", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & Convert.ToDateTime(txtStartDate.Text).Date & "~" & Convert.ToDateTime(txtEndDate.Text).Date, True, False)

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


        worksheet.Cells.ColumnWidth(0, 5) = 4000
        worksheet.Cells.ColumnWidth(6, 7) = 8000
        worksheet.Cells.ColumnWidth(8, 11) = 4000
        worksheet.Cells.ColumnWidth(12, 13) = 8000
        worksheet.Cells.ColumnWidth(14, 15) = 4000
        worksheet.Cells.ColumnWidth(16, 18) = 8000
        worksheet.Cells.ColumnWidth(19, 21) = 4000

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

        Dim newFileName As String = getXLSFileName()
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & txtChannel.Text.Replace(" ", "") & "_Sports_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

    Private Sub ExportMovie()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_export_exportepg_toexcel_movie", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & Convert.ToDateTime(txtStartDate.Text).Date & "~" & Convert.ToDateTime(txtEndDate.Text).Date, True, False)

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


        worksheet.Cells.ColumnWidth(0, 5) = 4000
        worksheet.Cells.ColumnWidth(6) = 8000
        worksheet.Cells.ColumnWidth(7, 10) = 4000
        worksheet.Cells.ColumnWidth(11) = 8000
        worksheet.Cells.ColumnWidth(12, 13) = 4000
        worksheet.Cells.ColumnWidth(14, 16) = 8000
        worksheet.Cells.ColumnWidth(17, 19) = 4000

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

        Dim newFileName As String = getXLSFileName()
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & txtChannel.Text.Replace(" ", "") & "_Movie_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()


    End Sub

    Private Sub ExportEntertainment()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_export_exportepg_toexcel_entertainment", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & Convert.ToDateTime(txtStartDate.Text).Date & "~" & Convert.ToDateTime(txtEndDate.Text).Date, True, False)

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


        worksheet.Cells.ColumnWidth(0, 5) = 4000
        worksheet.Cells.ColumnWidth(6, 7) = 8000
        worksheet.Cells.ColumnWidth(8, 11) = 4000
        worksheet.Cells.ColumnWidth(12, 13) = 8000
        worksheet.Cells.ColumnWidth(14, 15) = 4000
        worksheet.Cells.ColumnWidth(16, 18) = 8000
        worksheet.Cells.ColumnWidth(19, 21) = 4000
        
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

        Dim newFileName As String = getXLSFileName()
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & txtChannel.Text.Replace(" ", "") & "_Entertainment_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

End Class