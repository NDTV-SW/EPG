Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit
Imports ExcelLibrary

Public Class CopyLastWeekEPG
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Dim flag As Integer = 0
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
                txtChannel.Visible = True
            ElseIf (User.IsInRole("USER")) Then
                txtChannel.Visible = True
            Else
                txtChannel.Visible = False
            End If
            If Page.IsPostBack = False Then
                lbEPGDates.Visible = False
            Else
                lbEPGDates.Visible = True
                Dim obj As New Logger
                lbEPGDates.Text = obj.GetEpgDates(txtChannel.Text)
                Dim obj1 As New clsExecute
                Dim dt As DataTable = obj1.executeSQL("SELECT CONVERT(varchar, MAX(Progdate+1),101) as MaxDate from mst_epg where ChannelId='" & txtChannel.Text & "'", False)
                If dt.Rows.Count > 0 Then
                    txtStartDate.Text = dt(0)(0).ToString
                End If
            End If
        Catch ex As Exception
            Logger.LogError("CopyLastWeekEPG", "Page_Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    
    
    Private Sub grdCopyLastWeekdata_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCopyLastWeekdata.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                Dim lbSynopsis As Label = DirectCast(e.Row.FindControl("lbSynopsis"), Label)
                Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
                Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)
                Dim lbProgTime As Label = DirectCast(e.Row.FindControl("lbProgTime"), Label)
                Dim lbProgdate As Label = DirectCast(e.Row.FindControl("lbProgdate"), Label)
                Dim lbDuration As Label = DirectCast(e.Row.FindControl("lbDuration"), Label)
                lbProgdate.Text = Convert.ToDateTime(lbProgdate.Text).ToString("dd-MMM-yyyy")
                lbProgTime.Text = Convert.ToDateTime(lbProgTime.Text).ToString("hh:mm")
            End If
        Catch ex As Exception
            Logger.LogError("CopyLastWeekEPG", "grdCopyLastWeekdata_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try

            Dim obj As New clsExecute
            Dim dt As DataTable
            dt = obj.executeSQL("sp_mst_epg_copy_epg", "channelid~startdate~checkdays~ignore", "VarChar~DateTime~Int~bit", txtChannel.Text & "~" & txtStartDate.Text & "~" & ddlDays.SelectedValue & "~" & chkIgnore.Checked, True, True)
            'If chkIgnore.Checked Then
            '    dt = obj.executeSQL("sp_mst_epg_copy_epg", "channelid~startdate~checkdays~ignore", "VarChar~DateTime~Int~bit", txtChannel.Text & "~" & txtStartDate.Text & "~" & ddlDays.SelectedValue & "~1", True, True)
            'Else
            '    dt = obj.executeSQL("sp_mst_epg_copy_epg", "channelid~startdate~checkdays~ignore", "VarChar~DateTime~Int~bit", txtChannel.Text & "~" & txtStartDate.Text & "~" & ddlDays.SelectedValue & "~0", True, True)
            'End If

            If dt.Rows(0)(0).ToString.ToUpper = "ERROR" Then
                lbError.Visible = True
                lbError.Text = "Error/Live Program found in Master EPG for this Channel."
                myErrorBox("Error while copying EPG!")
                chkIgnore.Checked = False
                Exit Sub
            Else
                grdCopyLastWeekdata.DataSource = dt
                grdCopyLastWeekdata.DataBind()
                myMessageBox("EPG Copied Successfully!")
                lbError.Visible = False
                btnCopy.Enabled = False
            End If
            chkIgnore.Checked = False

        Catch ex As Exception
            Logger.LogError("CopyLastWeekEPG", "btnCopy_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error while copying EPG!")
        End Try
    End Sub

    Protected Sub txtChannel_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtChannel.TextChanged
        btnCopy.Enabled = True
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getNonMovieChannels(prefixText, count)
        Return channels
    End Function

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ExportExcel()
    End Sub
    Private Sub ExportExcel()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_export_exportepg_toexcel", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & Convert.ToDateTime(txtStartDate.Text).Date.AddDays(-7) & "~" & Convert.ToDateTime(txtStartDate.Text).Date.AddDays(Convert.ToInt16(ddlDays.SelectedValue) - 1).AddDays(-7), True, False)

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
                If dc.ColumnName.ToUpper = "DATE" Then
                    sTemp = Convert.ToDateTime(dr(dc.ColumnName).ToString()).AddDays(7).ToString("MM/dd/yyyy")
                Else
                    sTemp = dr(dc.ColumnName).ToString()
                End If

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
    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = txtChannel.Text.Replace(" ", "") & " " & Convert.ToDateTime(txtStartDate.Text).ToString("ddMMMyyyy") & ".xls"
        Return strPath & strFileName
    End Function

End Class
