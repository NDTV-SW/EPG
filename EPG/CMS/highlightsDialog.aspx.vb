Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class highlightsDialog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                ddlDTHCableOperators.DataBind()
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("highlightsDialog", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Protected Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        bindGrid(False)
    End Sub


    Private Sub bindGrid(ByVal paging As Boolean)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from fn_highlights_cms(20) where rank = 1 and channellanguageid =1", False)

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

    Protected Sub btnHighLight_Click(sender As Object, e As EventArgs) Handles btnHighLight.Click
        Dim row As GridViewRow
        For Each row In grdHighLights.Rows
            Dim chkHighLight As CheckBox = TryCast(row.FindControl("chkHighLight"), CheckBox)
            If chkHighLight.Checked Then
                Dim ChannelId As String = TryCast(row.FindControl("lbChannelId"), Label).Text
                Dim ProgName As String = TryCast(row.FindControl("lbProgName"), Label).Text.Replace("'", "''")
                Dim Synopsis As String = TryCast(row.FindControl("lbSynopsis"), Label).Text.Replace("'", "''")
                Dim StarCast As String = TryCast(row.FindControl("lbStarcast"), Label).Text.Replace("'", "''")
                Dim ISTDate As String = TryCast(row.FindControl("lbISTDate"), Label).Text
                Dim ISTTime As String = TryCast(row.FindControl("lbISTTime"), Label).Text
                'Dim VADate As String = TryCast(row.FindControl("lbVADate"), Label).Text
                'Dim VATime As String = TryCast(row.FindControl("lbVATime"), Label).Text
                Dim Genre As String = TryCast(row.FindControl("lbGenre"), Label).Text
                Dim poster As String = TryCast(row.FindControl("lbPoster"), Label).Text

                Dim progType As String = TryCast(row.FindControl("lbprogtype"), Label).Text
                Dim freshRepeat As String = TryCast(row.FindControl("lbFreshRepeat"), Label).Text
                'Dim FreshAirTimeVA As String = TryCast(row.FindControl("lbFreshAirTimeVA"), Label).Text

                Dim obj As New clsExecute
                obj.executeSQL("insert into mst_highlightsdialog values('" & ChannelId & "','" & ProgName & "','" & Synopsis & "','" & StarCast & "'," &
                               "'" & ISTDate & "','" & ISTTime & "','" & Genre & "','" & poster & "',1,'" & progType & "','" & freshRepeat & "')", False)

                chkHighLight.Checked = False
            End If
        Next
        ExportExcel()
    End Sub

    Private Sub ExportExcel()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select Channelid, ProgName,ProgType,istDate AirDate, isttime AirTime,case when progType='Show' then FreshRepeat else  '' end as FreshRepeat,  Synopsis, Genre, Poster from vw_highlights_dialog order by cast(istdate + ' ' + isttime as datetime),channelid", False)

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

        Dim vaRecipients As String = ConfigurationManager.AppSettings("DialogRecipients").ToString
        Dim vaBCC As String = ConfigurationManager.AppSettings("DialogBCC").ToString
        'Dim vaRecipients As String = "kautilyar@ndtv.com"
        'Dim vaBCC As String = "kautilyar@ndtv.com"
        Logger.mailMessage(vaRecipients, "Dialog EPG Highlights - updated", "Please find attached updated list of Highlights.", vaBCC, newFileName)

        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=" & getXLSFileName())
        'Response.WriteFile(newFileName)
        'Response.End()

    End Sub

    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = "dialogHighlights" & "_" & DateTime.Now.ToString("ddMMMyyyyHHmmss") & ".xls"
        Return strPath & strFileName
    End Function
End Class