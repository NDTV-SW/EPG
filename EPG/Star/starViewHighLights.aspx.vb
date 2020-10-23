Imports ExcelLibrary

Public Class starViewHighLights
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If Not User.Identity.Name.ToLower = "kautilyar" Then
                Response.Redirect("defaultDomestic.aspx")
            End If


        End If
    End Sub

    'fpc_highlights


    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        ExportExcel()
    End Sub

    Private Sub ExportExcel()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select a.Channelid,b.progname,a.Category,convert(varchar,a.Airdate,106) + ' ' + convert(varchar,a.Airdate,108) AirTime  from fpc_highlights a join mst_program b on a.progid=b.progid where convert (varchar,airdate,112)>=convert (varchar,dbo.getlocaldate(),112) and feed='" & ddlType.SelectedValue & "' order by Category, airdate", False)
        If dt.Rows.Count > 0 Then


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

            'Dim vaRecipients As String = ConfigurationManager.AppSettings("VisionAsiaRecipients").ToString
            'Dim vaBCC As String = ConfigurationManager.AppSettings("VisionAsiaBCC").ToString
            Dim vaRecipients As String = "sankalp@ndtv.com"
            Dim vaBCC As String = "sankalp@ndtv.com"
            Logger.mailMessage(vaRecipients, ddlType.SelectedValue & " Star EPG Highlights - updated", "Please find attached updated list of " & ddlType.SelectedValue & " Highlights.", vaBCC, newFileName)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Highlights have been mailed !');", True)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('No record found !');", True)
        End If

    End Sub

    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = "starHighlights" & "_" & DateTime.Now.ToString("ddMMMyyyyHHmmss") & ".xls"
        Return strPath & strFileName
    End Function
End Class