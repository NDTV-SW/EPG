Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports ExcelLibrary
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadSchedule4WOITest
    Inherits System.Web.UI.Page

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub
    Private Function bindUploaded() As DataTable
        Dim dt As DataTable = Nothing
        Dim obj As New clsExecute
        dt = obj.executeSQL("select channelid Channel,uploadedfilename FileName,CONVERT(varchar,uploadedAt,108) 'Upload At',CONVERT(varchar,startdate,106) 'Start Date',CONVERT(varchar,enddate,106) 'End Date',epgbuilt Built,xmlgenerated 'XML Generated',sameEPG from aud_mst_channel_woi_upload where CONVERT(varchar, uploadedAt,112)=CONVERT(varchar, dbo.getlocaldate(),112) and  CONVERT(varchar, uploadedAt,108)>CONVERT(varchar, dateadd(hh,-" & ddlHour.SelectedValue & ", dbo.getlocaldate()),108) order by uploadedAt desc", False)
        Return dt
    End Function

    Private Sub bindUploadedGrid()
        Dim dt As DataTable = Me.bindUploaded
        grdUploaded.DataSource = dt
        grdUploaded.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindUploadedGrid()
            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOITEST", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        bindAll()
        lbChannel.Text = txtChannel.Text
    End Sub

    Private Sub bindgrdExcelData()
        'DataSourceID="SqlDSgrdData" 
        Dim obj As New clsExecute
        'Dim dt As DataTable = obj.executeSQL("select [Program Name], Genre, convert(varchar,[Date],106), convert(varchar,[Time],108), Duration, [Description],[Episode No] from testtabletemp where channelID=@ChannelId order by Date,time", "ChannelId", "varchar", txtChannel.Text, False, False)
        Dim dt As DataTable = obj.executeSQL("select [Program Name], Genre, convert(varchar,[Date],106), convert(varchar,[Time],108), Duration, [Description],[Episode No] from map_epgexcel where channelID=@ChannelId order by Date,time", "ChannelId", "varchar", txtChannel.Text, False, False)
        grdExcelData.DataSource = dt

        grdExcelData.DataBind()
        'DataSourceID = "SqlDSgrdGenre"
    End Sub


    Private Sub bindAll()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select movie_channel,movielangid from mst_channel where channelid='" & txtChannel.Text & "'", False)
        bindgrdExcelData()
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> _
    Public Shared Function SearchMovie(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getMovie(contextKey, prefixText, count)
        Return channels
    End Function

    Protected Sub grdExcelData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdExcelData.PageIndexChanging
        grdExcelData.PageIndex = e.NewPageIndex
        bindgrdExcelData() 'grdExcelData.DataBind()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try

            Dim strExtension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower.Replace(".", "")
            Dim strPath As String = ConfigurationManager.AppSettings("woi" & strExtension & "uploadpath").ToString()
            FileUpload1.SaveAs(strPath & FileUpload1.FileName)
            myMessageBox(FileUpload1.FileName & " uploaded")
        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOITEST", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Protected Sub ddlHour_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHour.SelectedIndexChanged
        bindUploadedGrid()
    End Sub

    Protected Sub grdUploaded_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUploaded.PageIndexChanging
        grdUploaded.PageIndex = e.NewPageIndex
        bindUploadedGrid()
    End Sub


    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortExpression As String = e.SortExpression
        Dim direction As String = String.Empty
        If SortDirection = SortDirection.Ascending Then
            SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If
        Dim table As DataTable = Me.bindUploaded()
        table.DefaultView.Sort = sortExpression & direction
        grdUploaded.DataSource = table
        grdUploaded.DataBind()
    End Sub

    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
    Private Sub ExportExcel()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select channelid Channel,uploadedfilename FileName,CONVERT(varchar,uploadedAt,108) 'Upload At',CONVERT(varchar,startdate,106) 'Start Date',CONVERT(varchar,enddate,106) 'End Date',epgbuilt Built,xmlgenerated 'XML Generated',sameEPG [Same] from aud_mst_channel_woi_upload where CONVERT(varchar, uploadedAt,112)=CONVERT(varchar, dbo.getlocaldate(),112) and  CONVERT(varchar, uploadedAt,108)>CONVERT(varchar, dateadd(hh,-" & ddlHour.SelectedValue & ", dbo.getlocaldate()),108) order by uploadedAt desc", False)

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
        worksheet.Cells.ColumnWidth(1) = 8000
        worksheet.Cells.ColumnWidth(2) = 3000
        worksheet.Cells.ColumnWidth(3) = 3000
        worksheet.Cells.ColumnWidth(4) = 3000
        worksheet.Cells.ColumnWidth(5) = 2000
        worksheet.Cells.ColumnWidth(6) = 2000
        worksheet.Cells.ColumnWidth(7) = 2000


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
        Response.AddHeader("Content-Disposition", "attachment; filename=WOI_AUtoUpload_" & DateTime.Now.ToString("ddMMMyyyy_HHmmss") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = "WOI_AUtoUpload_" & DateTime.Now.ToString("ddMMMyyyyHHmmss") & ".xls"
        Return strPath & strFileName
    End Function

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ExportExcel()
    End Sub
End Class
