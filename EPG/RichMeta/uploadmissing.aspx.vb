
Imports ExcelLibrary
Imports System.Data.OleDb


Public Class uploadmissing
    Inherits System.Web.UI.Page
    Dim strSql As String
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim li As HtmlGenericControl = TryCast(Master.FindControl("liuploadmissing"), HtmlGenericControl)
            li.Attributes.Add("class", "active")
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Private Sub Export()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from v_rich_missingdata", False)

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

        worksheet.Cells.ColumnWidth(0, 1) = 3000
        worksheet.Cells.ColumnWidth(2) = 8000
        worksheet.Cells.ColumnWidth(3, 6) = 3000
        worksheet.Cells.ColumnWidth(7, 12) = 8000
        worksheet.Cells.ColumnWidth(13, 15) = 4000

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

        Dim newFileName As String = getXLSFileName(1)
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)

        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & getXLSFileName(0))
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

    Private Function getXLSFileName(ByVal withPath As Boolean) As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = "richmetamissing" & "_" & DateTime.Now.ToString("ddMMMyyyy_HHmmss") & ".xls"
        If withPath Then
            Return strPath & strFileName
        Else
            Return strFileName
        End If


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

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            strSql = ""

            Dim strPath As String = Server.MapPath("~/Excel/")
            strPath = strPath & DateTime.Now.ToString("ddMMMyyyy_HHmmss") & FileUpload1.FileName
            FileUpload1.SaveAs(strPath)
            uploadRich(strPath)

            lbUploadError.Visible = False
            'btnUpload.Enabled = False
            myMessageBox("File Uploaded Successfully.")

        Catch ex As Exception
            btnUpload.Enabled = True
            lbUploadError.Visible = True
            lbUploadError.Text = "File not uploaded. Please check error report"
            myErrorBox("File not uploaded. Please check error report")

            Logger.LogError("Upload Schedule New EPI", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub uploadRich(ByVal filePath As String)
        Dim i As Integer = 0
        Try


            Dim MyConnection As System.Data.OleDb.OleDbConnection
            Dim DtSet As System.Data.DataSet

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data Source='" & filePath & "';Extended Properties='Excel 12.0; HDR=YES'")
            MyConnection.Open()

            Dim dtExcelSchema As DataTable
            dtExcelSchema = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            MyConnection.Close()

            Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & SheetName & "]", MyConnection)
            MyCommand.TableMappings.Add("Table", "TestTable")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            MyConnection.Close()
            Dim intError As Integer = 0
            Dim dt As DataTable = DtSet.Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Dim vID As String, vType As String, vName As String, vOrigLang As String, vDubbedLang As String, vPG As String, vSeasonno As String
                    Dim vSeasonName As String, vSynopsis As String, vStarcast As String, vDirector As String, vProducer As String, vWriter As String
                    Dim vTrivia As String, vAwards As String, vReleaseYear As String, vCountry As String, vGenre1 As String, vGenre2 As String

                    vID = dt.Rows(i)(0).ToString
                    vType = dt.Rows(i)(1).ToString
                    vName = dt.Rows(i)(2).ToString
                    vOrigLang = dt.Rows(i)(3).ToString
                    vDubbedLang = dt.Rows(i)(4).ToString
                    vPG = dt.Rows(i)(5).ToString
                    vSeasonno = dt.Rows(i)(6).ToString
                    vSeasonName = dt.Rows(i)(7).ToString
                    vSynopsis = dt.Rows(i)(8).ToString
                    vStarcast = dt.Rows(i)(9).ToString
                    vDirector = dt.Rows(i)(10).ToString
                    vProducer = dt.Rows(i)(11).ToString
                    vWriter = dt.Rows(i)(12).ToString
                    vTrivia = dt.Rows(i)(13).ToString
                    vAwards = dt.Rows(i)(14).ToString
                    vReleaseYear = dt.Rows(i)(15).ToString
                    vCountry = dt.Rows(i)(16).ToString
                    vGenre1 = dt.Rows(i)(17).ToString
                    vGenre2 = dt.Rows(i)(18).ToString
                    Try
                        InsertRichData(vID, vType, vName, vOrigLang, vDubbedLang, vPG, vSeasonno, vSeasonName, vSynopsis, vStarcast, vDirector, vProducer, vWriter, vTrivia, vAwards, vReleaseYear, vCountry, vGenre1, vGenre2)
                    Catch ex As Exception
                        Logger.LogError("Upload Schedule New EPI", "uploadChannelSchedule", filePath & "||" & ex.Message.ToString, User.Identity.Name)
                        intError = 1
                    End Try

                Next

            End If
            If intError = 1 Then
                myMessageBox("File uploaded with errors")
            Else
                myMessageBox("File uploaded successfully")
            End If
            grd1.DataBind()
        Catch ex As Exception

            Logger.LogError("Upload Missing", "RichUploadmissing", "Row:" & i & ", " & ex.Message.ToString, User.Identity.Name)
        End Try


    End Sub


    Private Sub InsertRichData(vID As String, vType As String, vName As String, vOrigLang As String, vDubbedLang As String, vPG As String, _
                               vSeasonno As String, vSeasonName As String, vSynopsis As String, vStarcast As String, vDirector As String, _
                               vProducer As String, vWriter As String, vTrivia As String, vAwards As String, vReleaseYear As String, vCountry As String, _
                               vGenre1 As String, vGenre2 As String)
        Dim obj As New clsExecute
        vName = Logger.RemSplCharsEng(vName).Trim.Replace("'", "''")
        vSynopsis = Logger.RemSplCharsEng(vSynopsis).Trim.Replace("'", "''")
        vStarcast = Logger.RemSplCharsEng(vStarcast).Trim.Replace("'", "''")
        vDirector = Logger.RemSplCharsEng(vDirector).Trim.Replace("'", "''")
        vWriter = Logger.RemSplCharsEng(vWriter).Trim.Replace("'", "''")
        vProducer = Logger.RemSplCharsEng(vProducer).Trim.Replace("'", "''")
        vAwards = Logger.RemSplCharsEng(vAwards).Trim.Replace("'", "''")
        vTrivia = Logger.RemSplCharsEng(vTrivia).Trim.Replace("'", "''")

        Dim intLanguageid As Integer = 0, intDubbedLanguageid As Integer = 0
        Dim intGenreId As Integer = 0, intSubGenreId As Integer = 0

        Dim dt As DataTable
        dt = obj.executeSQL("select languageid from mst_language where fullname='" & vOrigLang & "'", False)
        If dt.Rows.Count > 0 Then
            intLanguageid = dt.Rows(0)(0).ToString
        End If
        dt = obj.executeSQL("select languageid from mst_language where fullname='" & vDubbedLang & "'", False)
        If dt.Rows.Count > 0 Then
            intDubbedLanguageid = dt.Rows(0)(0).ToString
        End If
        dt = obj.executeSQL("select genreid from mst_genre where genrename='" & vGenre1 & "'", False)
        If dt.Rows.Count > 0 Then
            intGenreId = dt.Rows(0)(0).ToString
        End If
        dt = obj.executeSQL("select subgenreid from mst_subgenre where genreid='" & intGenreId & "' and subgenrename='" & vGenre2 & "'", False)
        If dt.Rows.Count > 0 Then
            intSubGenreId = dt.Rows(0)(0).ToString
        End If

        If Not vName = "" Then
            strSql = "update richmeta set displayname='" & vName & "',originallanguageid='" & intLanguageid & "',dubbedlanguge='" & intDubbedLanguageid & "'"
            strSql = strSql & ",parentalguide='" & vPG & "',seasonno='" & vSeasonno & "',seasonname='" & vSeasonName & "',synopsis='" & vSynopsis & "'"
            strSql = strSql & ",starcast='" & vStarcast & "',director='" & vDirector & "',producer='" & vProducer & "',writer='" & vWriter & "'"
            strSql = strSql & ",trivia='" & vTrivia & "',awards='" & vAwards & "',releaseyear='" & vReleaseYear & "'"
            strSql = strSql & ",country='" & vCountry & "',genrename1='" & vGenre1 & "',genrename2='" & vGenre2 & "'"
            strSql = strSql & ",genre='" & intGenreId & "',subgenre='" & intSubGenreId & "' where id='" & vID & "'"
            obj.executeSQL(strSql, False)
        End If


    End Sub

End Class