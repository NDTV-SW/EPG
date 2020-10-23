Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports Excel
Imports AjaxControlToolkit
Imports ExcelLibrary

Public Class ProgramDiscrepancyImportNew
    Inherits System.Web.UI.Page
    'Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Dim flag As Integer = 0
    Dim errflag As Integer

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Private Sub MyNewMessageBox(ByVal messagestr As String)
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "MessageBox", " var r; r=confirm('" & messagestr & "');if (r==true)  {  PageMethods.GetTime(); } else { PageMethods.test2();  }", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim mu As MembershipUser = Membership.GetUser(User.Identity.Name)
            Membership.UpdateUser(mu)


            If Page.IsPostBack = False Then
                ddlLanguageBind()

                Try
                    If IsNothing(Request.QueryString("Mode")) Then
                        Exit Sub
                    End If
                    Dim qs As String
                    Dim onair As Integer, today As Integer, airtelDTH As Integer
                    qs = Request.QueryString("Mode").ToString
                    ddlLanguage.SelectedValue = Request.QueryString("LangId")
                    ddlType.SelectedIndex = Convert.ToInt32(Request.QueryString("option"))
                    onair = Convert.ToInt32(Request.QueryString("onair"))
                    airtelDTH = Convert.ToInt32(Request.QueryString("airtelDTH"))
                    today = Convert.ToInt32(Request.QueryString("today"))
                    If onair = 0 Then
                        chkOnAir.Checked = False
                    Else
                        chkOnAir.Checked = True
                    End If

                    If airtelDTH = 0 Then
                        chkAirtelDTH.Checked = False
                    Else
                        chkAirtelDTH.Checked = True
                    End If

                    If today = 0 Then
                        chkTodayTomorrow.Checked = False
                    Else
                        chkTodayTomorrow.Checked = True
                    End If

                Catch ex As Exception
                End Try
                bindGrid()
                bindMeGrid()
                checkUserType()
            End If
            'grdProgrammaster.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "Page Load", ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

    Private Sub bindGrid()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("rpt_regional_name_synopsis_missing_v1", "languageid~what~onair~sftp~today", "int~varchar~bit~bit~bit", _
                                             ddlLanguage.SelectedValue & "~" & ddlType.SelectedValue & "~" & chkOnAir.Checked & "~" & chkAirtelDTH.Checked & "~" & chkTodayTomorrow.Checked, True, False)
        If dt.Rows.Count > 0 Then
            grdProgrammaster.Visible = True
            grdProgrammaster.DataSource = dt
            grdProgrammaster.DataBind()
        Else
            grdProgrammaster.DataSource = Nothing
            grdProgrammaster.Visible = False
        End If
    End Sub

    Private Sub bindMeGrid()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_epg_import_program_central", "language", "int", ddlLanguage.SelectedValue, True, False)
        grdData.DataSource = dt
        grdData.DataBind()
    End Sub

    Private Sub checkUserType()
        Try
            If (User.IsInRole("ENGLISH")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "ENGLISH") Then
                        ddlLanguage.Items.RemoveAt(i)
                    End If
                Next
            ElseIf (User.IsInRole("HINDI")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "HINDI") Then
                        ddlLanguage.Items.RemoveAt(i)

                    End If
                Next
            ElseIf (User.IsInRole("MARATHI")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "MARATHI") Then
                        ddlLanguage.Items.RemoveAt(i)

                    End If
                Next
            ElseIf (User.IsInRole("TELUGU")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "TELUGU") Then
                        ddlLanguage.Items.RemoveAt(i)

                    End If
                Next
            ElseIf (User.IsInRole("TAMIL")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "TAMIL") Then
                        ddlLanguage.Items.RemoveAt(i)

                    End If
                Next
            End If
        Catch ex As Exception
            Logger.LogError("Program DiscrepancyCentralUpload", "checkUserType", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdProgrammaster_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgrammaster.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(4).Text = ddlLanguage.SelectedItem.Text.ToString & " " & "Programme"
                e.Row.Cells(5).Text = ddlLanguage.SelectedItem.Text.ToString & " " & "Synopsis"
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lbProgid As Label = DirectCast(e.Row.FindControl("lbProgid"), Label)
                Dim lbChannel As Label = DirectCast(e.Row.FindControl("lbChannel"), Label)
                Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                Dim lbSynopsis As Label = DirectCast(e.Row.FindControl("lbSynopsis"), Label)
                Dim lbRegProgName As Label = DirectCast(e.Row.FindControl("lbRegProgName"), Label)
                Dim lbRegSynopsis As Label = DirectCast(e.Row.FindControl("lbRegSynopsis"), Label)
                Dim hyEdit As HyperLink = DirectCast(e.Row.FindControl("hyEdit"), HyperLink)
                Dim lbAirtelFTP As Label = DirectCast(e.Row.FindControl("lbAirtelFTP"), Label)
                Dim lbEpisodeNo As Label = DirectCast(e.Row.FindControl("lbEpisodeNo"), Label)

                If ddlType.SelectedValue = "ProgName" Then
                    hyEdit.NavigateUrl = "javascript:openWin('" & lbProgid.Text.Trim & "','" & ddlLanguage.SelectedValue.Trim & "','Insert','" & lbProgName.Text.Replace("'", "^") & "','" & lbSynopsis.Text.Replace("'", "^") & "','" & lbRegProgName.Text.Replace("'", "^") & "','" & lbRegSynopsis.Text.Replace("'", "^") & "','" & ddlLanguage.SelectedItem.Text.Trim & "','" & ddlType.SelectedIndex & "','" & IIf(chkOnAir.Checked, "1", "0") & "','MainContent_grdProgrammaster_hyEdit_" & e.Row.RowIndex & "','" & IIf(chkTodayTomorrow.Checked, "1", "0") & "','" & IIf(chkAirtelDTH.Checked, "1", "0") & "','" & lbEpisodeNo.Text.Trim & "')"
                ElseIf lbRegProgName.Text = "" Then
                    hyEdit.NavigateUrl = "javascript:openWin('" & lbProgid.Text.Trim & "','" & ddlLanguage.SelectedValue.Trim & "','Insert1','" & lbProgName.Text.Replace("'", "^") & "','" & lbSynopsis.Text.Replace("'", "^") & "','" & lbRegProgName.Text.Replace("'", "^") & "','" & lbRegSynopsis.Text.Replace("'", "^") & "','" & ddlLanguage.SelectedItem.Text.Trim & "','" & ddlType.SelectedIndex & "','" & IIf(chkOnAir.Checked, "1", "0") & "','MainContent_grdProgrammaster_hyEdit_" & e.Row.RowIndex & "','" & IIf(chkTodayTomorrow.Checked, "1", "0") & "','" & IIf(chkAirtelDTH.Checked, "1", "0") & "','" & lbEpisodeNo.Text.Trim & "')"
                Else
                    hyEdit.NavigateUrl = "javascript:openWin('" & lbProgid.Text.Trim & "','" & ddlLanguage.SelectedValue.Trim & "','Update','" & lbRegProgName.Text.Replace("'", "^") & "','" & lbSynopsis.Text.Replace("'", "^") & "','" & lbRegProgName.Text.Replace("'", "^") & "','" & lbRegSynopsis.Text.Replace("'", "^") & "','" & ddlLanguage.SelectedItem.Text.Trim & "','" & ddlType.SelectedIndex & "','" & IIf(chkOnAir.Checked, "1", "0") & "','MainContent_grdProgrammaster_hyEdit_" & e.Row.RowIndex & "','" & IIf(chkTodayTomorrow.Checked, "1", "0") & "','" & IIf(chkAirtelDTH.Checked, "1", "0") & "','" & lbEpisodeNo.Text.Trim & "')"
                End If
                If Not (lbAirtelFTP.Text = "True") Then
                    e.Row.BackColor = Drawing.Color.BlanchedAlmond
                End If
            End If
        Catch ex As Exception
            Logger.LogError("Program Discrepancy Central", "grdProgrammaster_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub exec_ProcRegional(ByVal ProgId As Integer, ByVal ProgName As String, ByVal synopsis As String, ByVal LanguageID As Integer, ByVal ID As Integer, ByVal action As Char, ByVal vEpisodeNo As Integer)
        Try
            Dim obj As New clsExecute
            obj.executeSQL("sp_mst_ProgramRegional", "Rowid~ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                       "Int~Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                         "0~" & ProgId & "~" & ProgName & "~" & synopsis & "~" & vEpisodeNo & "~" & LanguageID.ToString.Trim & "~" & action.ToString.Trim & "~" & User.Identity.Name, True, False)
        Catch ex As Exception
            Logger.LogError("ProgramDiscrepancyImportNew", action & " exec_ProcRegional", "Message:  Action='" & action & "', Progid='" & ProgId & "',ProgName='" & ProgName & "',Synopsis'""',LanguageId='" & LanguageID & "', RowId='" & ID.ToString & "', ActionUser='" & User.Identity.Name & "' ", User.Identity.Name)
            Logger.LogError("ProgramDiscrepancyImportNew", action & " exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Shared Function WriteFile(ByVal FileName As String, ByVal FileData As String) As Integer
        WriteFile = 0
        Dim outStream As StreamWriter
        outStream = New StreamWriter(FileName, False)
        outStream.Write(FileData)
        outStream.Close()
    End Function

    Private Sub ddlLanguageBind()
        Try
            Dim i As Integer
            i = ddlLanguage.Items.Count - 1
            While i > 0
                ddlLanguage.Items.RemoveAt(i)
                i = i - 1
            End While

            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select * from mst_language where active=1", False)
            ddlLanguage.DataSource = dt
            ddlLanguage.DataTextField = "FullName"
            ddlLanguage.DataValueField = "LanguageId"
            ddlLanguage.DataBind()

        Catch ex As Exception
            Logger.LogError("ProgramDiscrepancyImport", "ddlLanguageBind", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        lbPageTitle.Text = "Programme Discrepancies Central (" & ddlType.SelectedItem.Text & ")"
        'lbCount.Text = "Total Records found : " & dt.Rows.Count
        bindGrid()
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        If (ddlLanguage.SelectedValue = "10") Then
            MyNewMessageBox("Please Select Language first to Import.")
            Exit Sub
        End If
        lbUploadError.Visible = False
        Dim excelReader As IExcelDataReader
        If (FileUpload1.HasFile AndAlso (IO.Path.GetExtension(FileUpload1.FileName) = ".xlsx" Or IO.Path.GetExtension(FileUpload1.FileName) = ".xls")) Then
            Try
                excelReader = ExcelReaderFactory.CreateBinaryReader(FileUpload1.FileContent)
                'excelReader.IsFirstRowAsColumnNames = True

                If excelReader.AsDataSet.Tables(0).Rows.Count > 0 Then
                    Dim ImChannelid As String, ImEngProgName As String, ImRegProgName As String, ImRegionalSynopsis As String, ImEpisode As String
                    DeleteEPGData()
                    Dim col1 As String, col2 As String, col3 As String, col4 As String, col5 As String, col6 As String
                    col1 = excelReader.AsDataSet.Tables(0).Rows(0).Item(0).ToString
                    col2 = excelReader.AsDataSet.Tables(0).Rows(0).Item(1).ToString
                    col3 = excelReader.AsDataSet.Tables(0).Rows(0).Item(2).ToString
                    col4 = excelReader.AsDataSet.Tables(0).Rows(0).Item(3).ToString
                    col5 = excelReader.AsDataSet.Tables(0).Rows(0).Item(4).ToString
                    col6 = excelReader.AsDataSet.Tables(0).Rows(0).Item(5).ToString

                    If Not (col1.Contains("Channel") And col2.Contains("English Programme Name") And col3.Contains("English Synopsis") And col4.Contains(ddlLanguage.SelectedItem.Text & " Programme") And col5.Contains(ddlLanguage.SelectedItem.Text & "_Synopsis")) Then
                        MyNewMessageBox("Column Names not correct")
                        Exit Sub
                    End If

                    For i = 1 To excelReader.AsDataSet.Tables(0).Rows.Count - 1
                        If Not excelReader.AsDataSet.Tables(0).Rows(i).Item(3).ToString = String.Empty Then
                            If errflag = 1 Then
                                Exit For
                            End If
                            ImChannelid = excelReader.AsDataSet.Tables(0).Rows(i).Item(0).ToString
                            ImEngProgName = excelReader.AsDataSet.Tables(0).Rows(i).Item(1).ToString
                            ImRegProgName = excelReader.AsDataSet.Tables(0).Rows(i).Item(3).ToString
                            ImRegionalSynopsis = excelReader.AsDataSet.Tables(0).Rows(i).Item(4).ToString
                            ImEpisode = excelReader.AsDataSet.Tables(0).Rows(i).Item(5).ToString
                            uploadCount = 0
                            ImportData(ImChannelid.Trim, ImEngProgName.Trim, ddlLanguage.SelectedValue.Trim, ImRegProgName.Trim, ImRegionalSynopsis.Trim, ImEpisode)
                        End If
                    Next
                End If

                bindMeGrid()
                bindGrid()
                MyNewMessageBox("File upload successful.")

            Catch ex As Exception
                Logger.LogError("ProgramDiscrepancyImport", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
                MyNewMessageBox("File Was not uploaded. Please check error logs.")
                lbUploadError.Visible = True
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub ExecImportPC()
        Try

            Dim obj As New clsExecute
            obj.executeSQL("sp_epg_import_program_central", "Language", "Int", ddlLanguage.SelectedValue, True, False)

        Catch ex As Exception
            Logger.LogError("ProgramDescripencyImport", "ExecImportPC", ex.Message.ToString, User.Identity.Name)
            errflag = 1
        End Try
    End Sub
    Dim uploadCount As Integer

    Private Sub ImportData(ByVal vchannelid As String, ByVal vProgName As String, ByVal vLanguageId As Integer, ByVal vProgramRegionalname As String, ByVal vsynopsis As String, ByVal vEpisode As String)
        If Not (uploadCount = 0) Then
            Exit Sub
        End If

        Try
            Dim Progname, rProgname As String, Synopsis As String
            Progname = Logger.RemSplCharsEng(vProgName.ToString.Trim)
            If vLanguageId = 1 Then
                rProgname = Logger.RemSplCharsEng(vProgramRegionalname.Trim)
                Synopsis = Logger.RemSplCharsEng(vsynopsis.Trim)
            Else
                rProgname = Logger.RemSplCharsAllLangs(vProgramRegionalname.Trim, Convert.ToInt32(vLanguageId.ToString))
                Synopsis = Logger.RemSplCharsAllLangs(vsynopsis.Trim, Convert.ToInt32(vLanguageId.ToString))
            End If

            Dim obj As New clsExecute
            If vEpisode.Trim = "" Then
                vEpisode = 0
            End If
            obj.executeSQL("sp_epg_programregional", "ChannelId~LanguageId~ProgName~rProgName~Synopsis~EpisodeNo", "nVarChar~Int~nVarChar~nVarChar~nVarChar~int", _
                           vchannelid & "~" & vLanguageId & "~" & Progname & "~" & rProgname & "~" & Synopsis & "~" & vEpisode, True, False)

            uploadCount = uploadCount + 1

            'grdData.DataBind()
        Catch ex As Exception
            Logger.LogError("ProgramDescripencyImport", "ImportData", ex.Message.ToString, User.Identity.Name)
            errflag = 1
        End Try
    End Sub

    Private Sub DeleteEPGData()
        Try
            Dim obj As New clsExecute
            obj.executeSQL("Delete from tmp_programregional where languageid='" & ddlLanguage.SelectedValue & "' and Convert(varchar,lastupdate,112) = Convert(varchar,dbo.GetLocalDate(),112)", False)

        Catch ex As Exception
            Logger.LogError("ProgramDiscrepancyImport", "DeleteEPGData", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub ddlLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLanguage.SelectedIndexChanged
        bindGrid()
    End Sub

    Protected Sub btnExportWithTranslation_Click(sender As Object, e As EventArgs) Handles btnExportWithTranslation.Click
        Try
            ExportwithTranslationExcel()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Excel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Excel.Click
        Try
            ExportExcel()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ExportwithTranslationExcel()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("rpt_regional_name_synopsis_missing_v1", "LanguageId~what~OnAir~SFTP~Today", "Int~VarChar~Bit~Bit~Bit", _
                             ddlLanguage.SelectedValue & "~" & ddlType.SelectedValue & "~" & chkOnAir.Checked & "~" & chkAirtelDTH.Checked & "~" & chkTodayTomorrow.Checked, True, False)

        If dt.Rows.Count > 0 Then
            Dim j As Integer = dt.Rows.Count - 1
            Dim i As Integer = j
            While i >= 0
                If dt.Rows(i).Item("Channelid").ToString.ToLower <> "marketing message" And chkMMOnly.Checked = True Then
                    dt.Rows.RemoveAt(i)
                End If
                i = i - 1
            End While
            For i = 0 To dt.Rows.Count - 1
                If Not dt.Rows(i).Item("synopsis").ToString.Trim = "" Then
                    'If dt.Rows(i).Item("Channelid").ToString.ToLower = "star plus" Then
                    Dim strData As String = TranslateFromGoogle.Translate(dt.Rows(i).Item("synopsis"), ddlLanguage.SelectedItem.Text)
                    If strData = dt.Rows(i).Item("synopsis") Then

                    End If
                    Dim tmpStr As String = IIf(strData.Trim = "", dt.Rows(i).Item("regional synopsis"), strData)
                    If tmpStr = "error" Then
                        'Continue For
                    End If
                    dt.Rows(i).Item("regional synopsis") = tmpStr
                    'Try
                    'End If
                End If
            Next i
        End If
        dt.Columns.Remove("Progid")
        dt.Columns.Remove("FTP")
        If ddlType.SelectedValue = "Synopsis_HTRP" Or ddlType.SelectedValue = "Synopsis_LTRP" Then
            dt.Columns.Remove("TRP")
        End If
        dt.Columns("Channelid").ColumnName = "Channel"
        dt.Columns("Program Name").ColumnName = "English Programme Name"
        dt.Columns("Synopsis").ColumnName = "English Synopsis"
        dt.Columns("Regional ProgName").ColumnName = ddlLanguage.SelectedItem.Text & " Programme"
        dt.Columns("Regional Synopsis").ColumnName = ddlLanguage.SelectedItem.Text & "_Synopsis"

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


        Dim newFileName As String = getXLSFileName()
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & ddlType.SelectedItem.Text.Replace(" ", "_").Replace(">", "_") & "_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub
    Private Sub ExportExcel()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("rpt_regional_name_synopsis_missing_v1", "LanguageId~what~OnAir~SFTP~Today", "Int~VarChar~Bit~Bit~Bit", _
                             ddlLanguage.SelectedValue & "~" & ddlType.SelectedValue & "~" & chkOnAir.Checked & "~" & chkAirtelDTH.Checked & "~" & chkTodayTomorrow.Checked, True, False)

        If dt.Rows.Count > 0 Then
            Dim j As Integer = dt.Rows.Count - 1
            Dim i As Integer = j
            While i >= 0
                If dt.Rows(i).Item("Channelid").ToString.ToLower <> "marketing message" And chkMMOnly.Checked = True Then
                    dt.Rows.RemoveAt(i)
                End If
                i = i - 1
            End While



        End If
        dt.Columns.Remove("Progid")
        dt.Columns.Remove("FTP")
        If ddlType.SelectedValue = "Synopsis_HTRP" Or ddlType.SelectedValue = "Synopsis_LTRP" Then
            dt.Columns.Remove("TRP")
        End If
        dt.Columns("Channelid").ColumnName = "Channel"
        dt.Columns("Program Name").ColumnName = "English Programme Name"
        dt.Columns("Synopsis").ColumnName = "English Synopsis"
        dt.Columns("Regional ProgName").ColumnName = ddlLanguage.SelectedItem.Text & " Programme"
        dt.Columns("Regional Synopsis").ColumnName = ddlLanguage.SelectedItem.Text & "_Synopsis"

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

        Dim newFileName As String = getXLSFileName()
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & ddlType.SelectedItem.Text.Replace(" ", "_").Replace(">", "_") & "_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls")
        Response.WriteFile(newFileName)
        Response.End()

    End Sub

    Private Function getXLSFileName() As String
        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = ddlType.SelectedItem.Text.Replace(" ", "_").Replace(">", "_") & "_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls"
        Return strPath & strFileName
    End Function


    Protected Sub chkAirtelDTH_CheckedChanged(sender As Object, e As EventArgs) Handles chkAirtelDTH.CheckedChanged
        bindGrid()
    End Sub
    Protected Sub chkTodayTomorrow_CheckedChanged(sender As Object, e As EventArgs) Handles chkTodayTomorrow.CheckedChanged
        bindGrid()
    End Sub
    Protected Sub chkOnAir_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnAir.CheckedChanged
        bindGrid()
    End Sub

End Class