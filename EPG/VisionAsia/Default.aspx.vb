Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Data.OleDb
Imports System.Web.UI.DataVisualization.Charting
Imports ExcelLibrary


Public Class _DefaultVisionAsia
    Inherits System.Web.UI.Page

    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Dim gridSelect As String
    Dim progduration As Integer
    Dim lbduration As Label

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
            gridSelect = "0"
            'lbEPGExists.Visible = False
            If Page.IsPostBack = False Then
                progduration = 0
                tbHead.Visible = False
                btnViewAll.Visible = False
                Dim obj As New Logger
                'lbEPGExists.Visible = True
                'lbEPGExists.Text = obj.GetEpgDates(ddlChannelName.SelectedValue)
                txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
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
        Catch ex As Exception
            Logger.LogError("View EPG", "btnView_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnViewAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewAll.Click
        Try
            tbHead.Visible = True
            gridSelect = "AllBind"
            grdViewEPGAllBind()
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

                    strQuery = "select * from fn_viewepg ('" & ddlChannelName.SelectedValue & "','" & ddlLanguage.SelectedValue & "','" & Convert.ToDateTime(lbProgDate.Text).ToString("yyyy-MM-dd") & "') order by sortby"

                    Dim myConnection As New SqlConnection(ConString)
                    myConnection.Open()
                    Dim adp As New SqlDataAdapter(strQuery, myConnection)
                    Dim dt As New DataTable
                    adp.Fill(dt)

                    'Dim dt As DataTable = clsExecute.executeSQL(strQuery, False)

                    'Dim adp As New SqlDataAdapter(strQuery, ConString)
                    'Dim ds As New DataSet
                    'adp.Fill(ds)

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
                    Dim lbprogTimeGMT As Label = DirectCast(e.Row.FindControl("lbprogTimeGMT"), Label)
                    lbprogDateGMT.Text = Convert.ToDateTime(lbprogDateGMT.Text).ToString("ddMMMyyyy")
                    lbprogTimeGMT.Text = Convert.ToDateTime(lbprogTimeGMT.Text).ToString("HH:mmtt")

                    lbduration = DirectCast(e.Row.FindControl("lbduration"), Label)
                    progduration = progduration + Convert.ToInt32(lbduration.Text)
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

                        lbTotalDuration.ForeColor = System.Drawing.Color.DarkRed
                    Else
                        'lbTotalDuration.ForeColor = System.Drawing.Color.White
                    End If
                    'lbEPGExists.Text = startEPGTime
            End Select
        Catch ex As Exception
            Logger.LogError("View EPG", "grdChildViewEpg_Databound", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
    Private Sub grdViewEPGBind()
        Try

            Dim myConnection As New SqlConnection(ConString)
            myConnection.Open()
            Dim adp As New SqlDataAdapter("select distinct a.channelid,Convert(varchar(11),a.progdate,113) as progdate1,DATENAME(weekday,a.progdate) as 'WeekDay',DATEPART( WW,a.progdate) as WeekNumber,a.progdate from mst_epg a join mst_Channel b on a.ChannelID = b.ChannelID join mst_program c on a.progid = c.progid where a.ChannelID='" & ddlChannelName.Text & "' and a.progdate between '" & txtStartDate.Text & "' and '" & txtEndDate.Text & "' order by progdate,3 asc", myConnection)
            Dim dt As New DataTable
            adp.Fill(dt)

            'Dim dt As DataTable = clsExecute.executeSQL("select distinct a.channelid,Convert(varchar(11),a.progdate,113) as progdate1,DATENAME(weekday,a.progdate) as 'WeekDay',DATEPART( WW,a.progdate) as WeekNumber,a.progdate from mst_epg a join mst_Channel b on a.ChannelID = b.ChannelID join mst_program c on a.progid = c.progid where a.ChannelID='" & ddlChannelName.Text & "' and a.progdate between '" & txtStartDate.Text & "' and '" & txtEndDate.Text & "' order by progdate,3 asc", False)

            'Dim adp As New SqlDataAdapter("select distinct a.channelid,Convert(varchar(11),a.progdate,113) as progdate1,DATENAME(weekday,a.progdate) as 'WeekDay',DATEPART( WW,a.progdate) as WeekNumber,a.progdate from mst_epg a join mst_Channel b on a.ChannelID = b.ChannelID join mst_program c on a.progid = c.progid where a.ChannelID='" & ddlChannelName.Text & "' and a.progdate between '" & txtStartDate.Text & "' and '" & txtEndDate.Text & "' order by progdate,3 asc", ConString)
            'Dim ds As New DataSet
            'adp.Fill(ds)
            grdViewEPG.DataSource = dt
            grdViewEPG.DataBind()
        Catch ex As Exception
            Logger.LogError("View EPG", "grdViewEPGBind", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
    Private Sub grdViewEPGAllBind()
        Try
        Catch ex As Exception
            Logger.LogError("View EPG", "grdViewEPGAllBind", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged
        Try
            Dim obj As New Logger
            'lbEPGExists.Visible = True
            'lbEPGExists.Text = obj.GetEpgDates(ddlChannelName.SelectedValue)
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
            myErrorBox("Please Select Start and End date")
            Exit Sub
        End If
        ExportExcel()

    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtStartDate.Text.Trim = "" Or txtEndDate.Text.Trim = "" Then
            myErrorBox("Please Select Start and End date")
            Exit Sub
        End If
        exportExcelGMT()

    End Sub
    Private Sub exportExcel1()
        Try
            grdViewEPGExport.Visible = True

            'Dim dt As DataTable = clsExecute.executeSQL("sp_export_exportepg_toexcel", "ChannelId~FromDate~ToDate", "VarChar~DateTime~DateTime", ddlChannelName.SelectedValue & "~" & Convert.ToDateTime(txtStartDate.Text.Trim) & "~" & Convert.ToDateTime(txtEndDate.Text.Trim), True)

            Dim cnn As New SqlConnection(ConString)
            Dim dscmd As New SqlDataAdapter("sp_export_exportepg_toexcel", cnn.ConnectionString)
            dscmd.SelectCommand.CommandType = CommandType.StoredProcedure
            dscmd.SelectCommand.Parameters.Add(New SqlParameter("@ChannelId", SqlDbType.VarChar)).Value = ddlChannelName.SelectedValue.ToString.Trim
            dscmd.SelectCommand.Parameters.Add(New SqlParameter("@fromDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtStartDate.Text.Trim)
            dscmd.SelectCommand.Parameters.Add(New SqlParameter("@ToDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtEndDate.Text.Trim)
            ' DataSet

            Dim dt As New DataTable
            dscmd.Fill(dt)

            If dt.Rows.Count < 2 Then
                myErrorBox("No record Found for these Dates")
                Exit Sub
            End If

            grdViewEPGExport.DataSource = dt
            grdViewEPGExport.DataBind()
            Response.ClearContent()

            Dim strFileName As String = ddlChannelName.SelectedValue.ToString.Replace(" ", "") & "_EPG_" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & ".xls"
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
        Dim dt As DataTable = obj.executeSQL("select Progname,AirDate,AirTime,Duration,ProgTimeGMT,Synopsis,EpisodeNo from fn_viewepg_export('" & ddlChannelName.SelectedValue & "','" & ddlLanguage.SelectedValue & "','" & Convert.ToDateTime(txtStartDate.Text.Trim) & "','" & Convert.ToDateTime(txtEndDate.Text.Trim) & "') order by sortby1,sortby2", False)

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
        Response.AddHeader("Content-Disposition", "attachment; filename=" & ddlChannelName.SelectedItem.Text.Replace(" ", "") & "_GMT_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()


    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
    Private Sub ExportExcel()

        'Dim dt As DataTable = clsExecute.executeSQL("sp_export_exportepg_toexcel", "ChannelId~FromDate~ToDate", "VarChar~DateTime~DateTime", ddlChannelName.SelectedValue & "~" & Convert.ToDateTime(txtStartDate.Text.Trim) & "~" & Convert.ToDateTime(txtEndDate.Text.Trim), True)

        Dim dscmd As New SqlDataAdapter("sp_export_exportepg_toexcel", ConString)
        dscmd.SelectCommand.CommandType = CommandType.StoredProcedure
        dscmd.SelectCommand.Parameters.Add(New SqlParameter("@ChannelId", SqlDbType.VarChar)).Value = ddlChannelName.SelectedValue.ToString.Trim
        dscmd.SelectCommand.Parameters.Add(New SqlParameter("@fromDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtStartDate.Text.Trim)
        dscmd.SelectCommand.Parameters.Add(New SqlParameter("@ToDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtEndDate.Text.Trim)

        Dim dt As New DataTable
        dscmd.Fill(dt)


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
        Response.AddHeader("Content-Disposition", "attachment; filename=" & ddlChannelName.SelectedItem.Text.Replace(" ", "") & "_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = ddlChannelName.SelectedItem.Text.Replace(" ", "") & " " & Convert.ToDateTime(txtStartDate.Text).ToString("ddMMMyyyy") & ".xls"
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
                        grdChildViewEpg.Columns(4).Visible = True
                    Else
                        grdChildViewEpg.Columns(5).Visible = False
                        grdChildViewEpg.Columns(4).Visible = False
                    End If

            End Select
        Catch ex As Exception


        End Try
    End Sub


    Protected Sub grdViewEPG_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles grdViewEPG.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim control As New LiteralControl()
            'new litearl control
            control.Text = "(Page " & (grdViewEPG.PageIndex + 1) & " of " & grdViewEPG.PageCount & ")"

            ' add text
            Dim table As Table = TryCast(e.Row.Cells(0).Controls(0), Table)
            ' get the pager table
            Dim newCell As New TableCell()
            'Create new cell
            newCell.Controls.Add(control)
            'Add contol
            'add cell
            table.Rows(0).Cells.Add(newCell)
        End If
    End Sub

    

End Class