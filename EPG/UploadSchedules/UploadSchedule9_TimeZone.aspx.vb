Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
'Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit
Imports ExcelLibrary

Public Class UploadSchedule9_TimeZone
    Inherits System.Web.UI.Page
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then

            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule9", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim excelReader As IExcelDataReader = Nothing
        Try
            If FileUpload1.FileName = Nothing Then
                Exit Sub
            Else

                If (FileUpload1.HasFile AndAlso (IO.Path.GetExtension(FileUpload1.FileName) = ".xlsx" Or IO.Path.GetExtension(FileUpload1.FileName) = ".xls")) Then
                    excelReader = ExcelReaderFactory.CreateBinaryReader(FileUpload1.FileContent)
                    excelReader.IsFirstRowAsColumnNames = True
                    Dim dt As New DataTable
                    dt.Columns.Add("Progname")
                    dt.Columns.Add("Genre")
                    dt.Columns.Add("Date")
                    dt.Columns.Add("Time")
                    dt.Columns.Add("Duration")
                    dt.Columns.Add("Description")
                    dt.Columns.Add("Episode")
                    dt.Columns.Add("ShowWiseDescription")
                    'Dim a As Date
                    If excelReader.AsDataSet.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To excelReader.AsDataSet.Tables(0).Rows.Count - 1
                            Try
                                If IsDBNull(excelReader.AsDataSet.Tables(0).Rows(i).Item(0)) Then
                                    Exit For
                                End If

                                Dim vDate As Date
                                Try
                                    vDate = (Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(1)).ToString)).ToString
                                Catch ex As Exception
                                    vDate = Convert.ToDateTime(excelReader.AsDataSet.Tables(0).Rows(i).Item(1).ToString).ToString
                                End Try

                                Dim vTime As DateTime, vTimeNext As DateTime
                                Try
                                    vTime = Convert.ToDateTime(Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(2)).ToString)).ToShortTimeString
                                    Try
                                        vTimeNext = Convert.ToDateTime(Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i + 1).Item(2)).ToString)).ToShortTimeString
                                    Catch ex As Exception
                                        vTimeNext = "00:00:00"
                                    End Try

                                Catch
                                    vTime = Convert.ToDateTime(excelReader.AsDataSet.Tables(0).Rows(i).Item(2)).ToShortTimeString
                                    Try
                                        vTimeNext = Convert.ToDateTime(excelReader.AsDataSet.Tables(0).Rows(i + 1).Item(2)).ToShortTimeString
                                    Catch ex As Exception
                                        vTimeNext = "00:00:00"
                                    End Try
                                End Try


                                Dim vDuration As Integer
                                vTime = DateTime.Now.Date & " " & vTime
                                Try
                                    vTimeNext = DateTime.Now.Date & " " & vTimeNext.ToShortTimeString
                                    vDuration = DateDiff(DateInterval.Minute, vTime, vTimeNext)
                                Catch ex As Exception
                                    vTimeNext = DateTime.Now.AddDays(1).Date & " " & "00:00:00 AM"
                                    vDuration = DateDiff(DateInterval.Minute, vTime, vTimeNext)
                                End Try

                                If vDuration < 0 Then
                                    vDuration = 1440 + vDuration
                                End If

                                If i = 1170 Then
                                    i = i
                                End If

                                'Try
                                '    vDuration = Convert.ToDateTime(Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(4)).ToString)).ToShortTimeString
                                'Catch
                                '    vDuration = Convert.ToDateTime(excelReader.AsDataSet.Tables(0).Rows(i).Item(4)).ToShortTimeString
                                'End Try

                                Dim vChannelID As String = txtChannel.Text
                                Dim vGenre As String = excelReader.AsDataSet.Tables(0).Rows(i).Item(8).ToString
                                Dim vProgName As String = (excelReader.AsDataSet.Tables(0).Rows(i).Item(5)).ToString


                                Dim vDescription As String = Logger.RemSplCharsEng(excelReader.AsDataSet.Tables(0).Rows(i).Item(5).ToString)
                                Dim vEpisodeNo As String = excelReader.AsDataSet.Tables(0).Rows(i).Item(6).ToString
                                Dim vShowWiseDesc As String = excelReader.AsDataSet.Tables(0).Rows(i).Item(7).ToString

                                Dim vCombinedDateTime As DateTime = vDate.Date & " " & vTime.ToShortTimeString
                                vCombinedDateTime = vCombinedDateTime.AddMinutes(ddlTZ.SelectedValue)
                                Dim row As DataRow = dt.NewRow
                                row("Progname") = vProgName
                                row("Genre") = vGenre
                                row("Date") = vCombinedDateTime.Date.ToString("yyyy-MM-dd")
                                row("Time") = vCombinedDateTime.ToString("HH:mm:ss")
                                row("Duration") = vDuration
                                row("Description") = vDescription
                                row("Episode") = vEpisodeNo
                                row("ShowWiseDescription") = ""
                                dt.Rows.Add(row)
                            Catch ex As Exception
                                i = i

                            End Try
                        Next 
                        ExportExcel(dt)
                    End If

                End If

            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule9", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        Finally
            If IsNothing(excelReader) Then
                excelReader.Close()
            End If
        End Try
    End Sub



    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    Private Sub ExportExcel(ByVal dt As DataTable)


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

    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = txtChannel.Text.Replace(" ", "") & " " & DateTime.Now.ToString("ddMMMyyyy") & ".xls"
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

End Class
