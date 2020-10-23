Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class ViewHighlights
    Inherits System.Web.UI.Page
    'Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("Highlights", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Private Sub bindGrid(ByVal paging As Boolean)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select distinct x.Channelid, x.ProgName,ProgType,istdate [Date], isttime [Time], Synopsis, Genre, poster=(select 'http://epgops.ndtv.com/uploads/' + programlogo from mst_program where progname=x.progname and channelid=x.channelid),episodeno [Episode No],Starcast,subgenre [Sub Genre],Releaseyear [Release Year],x.Duration from mst_highlights x join dthcable_channelmapping y on x.channelid  = y.channelid join mst_program z on x.channelid=z.channelid and x.progname=z.progname where cast(istdate + ' ' + isttime as datetime) > cast (convert(varchar,dbo.GetLocalDate(),106) + ' ' + convert(varchar,dbo.GetLocalDate(),108) as datetime)  And operatorid = 232 order by istdate,isttime", False)
        grdHighLights.DataSource = dt
        grdHighLights.DataBind()

        If paging = False Then
            grdHighLights.PageIndex = 0
        End If
        grdHighLights.SelectedIndex = -1
        grdHighLights.DataBind()
    End Sub

    Protected Sub grdHighLights_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdHighLights.PageIndexChanging
        grdHighLights.PageIndex = e.NewPageIndex
        bindGrid(True)
    End Sub

    Protected Sub grdHighLights_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdHighLights.Sorting
        bindGrid(True)
    End Sub
    Protected Sub grdHighLights_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdHighLights.RowDeleting
        Try
            Dim lbChannelId As Label = DirectCast(grdHighLights.Rows(e.RowIndex).FindControl("lbChannelId"), Label)
            Dim lbProgName As Label = DirectCast(grdHighLights.Rows(e.RowIndex).FindControl("lbProgName"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("delete from mst_highlights where channelid='" & lbChannelId.Text & "' and progname='" & lbProgName.Text.Replace("'", "''") & "'", False)
            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("ViewHighLights", "ViewHighlights_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdHighLights_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdHighLights.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub




    Private Sub ExportExcel()

        Dim obj As New clsExecute
        'Dim strSQl As String = "select distinct Channelid, ProgName,ProgType,istdate [Date], isttime [Time], Synopsis, Genre, Poster,episodeno [Episode No],Starcast,subgenre [Sub Genre],Release_year [Release Year],Duration from vw_spull_highlights order by istdate,isttime,channelid"
        Dim strSQl As String = "select distinct x.Channelid, x.ProgName,ProgType,istdate [Date], isttime [Time], Synopsis, Genre, poster=(select 'http://epgops.ndtv.com/uploads/' + programlogo from mst_program where progname=x.progname and channelid=x.channelid),episodeno [Episode No],Starcast,subgenre [Sub Genre],Releaseyear [Release Year],x.Duration from mst_highlights x join dthcable_channelmapping y on x.channelid  = y.channelid join mst_program z on x.channelid=z.channelid and x.progname=z.progname where cast(istdate + ' ' + isttime as datetime) > cast (convert(varchar,dbo.GetLocalDate(),106) + ' ' + convert(varchar,dbo.GetLocalDate(),108) as datetime)  And operatorid = 232 order by istdate,isttime"
        'Dim dt As DataTable = obj.executeSQL("select distinct  x.channelid, x.progname, x.synopsis,x.starcast,x.istdate,isttime, x.genre, 'http://epgops.ndtv.com/uploads/' + z.programlogo poster,episodeno [Episode No],Starcast,subgenre [Sub Genre],Releaseyear [Release Year] from mst_highlights x join dthcable_channelmapping y on x.channelid  = y.channelid join mst_program z on x.channelid=z.channelid and x.progname=z.progname where cast(istdate + ' ' + isttime as datetime) > cast (convert(varchar,dbo.GetLocalDate(),106) + ' ' + convert(varchar,dbo.GetLocalDate(),108) as datetime)  And operatorid = 232 order by istdate,isttime", False)
        Dim dt As DataTable = obj.executeSQL(strSQl, False)
        'Dim dt As DataTable = obj.executeSQL("select distinct Channelid, ProgName,ProgType,istdate [Date], isttime [Time], Synopsis, Genre, Poster,episodeno [Episode No],Starcast,subgenre [Sub Genre],Releaseyear [Release Year],Duration from vw_highlights_va order by istdate,isttime,channelid", False)

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

        worksheet.Cells.ColumnWidth(0) = 6000
        worksheet.Cells.ColumnWidth(1) = 6000
        worksheet.Cells.ColumnWidth(2) = 5000
        worksheet.Cells.ColumnWidth(3) = 5000
        worksheet.Cells.ColumnWidth(4) = 4000
        worksheet.Cells.ColumnWidth(5) = 4000
        worksheet.Cells.ColumnWidth(6) = 4000
        worksheet.Cells.ColumnWidth(7) = 10000
        worksheet.Cells.ColumnWidth(8) = 4000
        worksheet.Cells.ColumnWidth(9) = 4000
        worksheet.Cells.ColumnWidth(10) = 4000
        worksheet.Cells.ColumnWidth(11) = 4000
        worksheet.Cells.ColumnWidth(12) = 4000
        worksheet.Cells.ColumnWidth(13) = 4000


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

        Dim vaRecipients As String = ConfigurationManager.AppSettings("VisionAsiaRecipients").ToString
        Dim vaBCC As String = ConfigurationManager.AppSettings("VisionAsiaBCC").ToString

        'Dim vaRecipients As String = "avdeshk@ndtv.com"
        'Dim vaBCC As String = "sachint@ndtv.com"

        Logger.mailMessage(vaRecipients, "EPG Highlights - updated", "Please find attached updated list of Highlights.", vaBCC, newFileName)

        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=" & getXLSFileName())
        'Response.WriteFile(newFileName)
        'Response.End()

    End Sub

    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = "spuulHighlights" & "_" & DateTime.Now.ToString("ddMMMyyyyHHmmss") & ".xls"
        Return strPath & strFileName
    End Function

    Protected Sub btnMail_Click(sender As Object, e As EventArgs) Handles btnMail.Click
        ExportExcel()
    End Sub
    Dim strLastProg As String
    Protected Sub grdHighLights_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdHighLights.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            strLastProg = ""
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbProg As Label = TryCast(e.Row.FindControl("lbProgName"), Label)
            If strLastProg = lbProg.Text Then
                e.Row.BackColor = Drawing.Color.Red
            End If
            strLastProg = lbProg.Text
        End If
    End Sub
End Class