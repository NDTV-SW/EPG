Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
'Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadSchedule
    Inherits System.Web.UI.Page
    Dim flag As Integer = 0
    Dim obj1 As New clsUploadModules
    Dim obj As New clsExecute

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then

            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Dim strFirstDate As String = "", strLastdate As String = ""
    Dim dateFlag As Integer = 0

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim excelReader As IExcelDataReader = Nothing
        'If ddlChannelName.SelectedIndex = 0 Then
        If txtChannel.Text = "" Then
            myErrorBox("Please select Channel first !")
            Exit Sub
        End If
        Try
            If FileUpload1.FileName = Nothing Then
                Exit Sub
            Else

                If (FileUpload1.HasFile AndAlso (IO.Path.GetExtension(FileUpload1.FileName) = ".xlsx" Or IO.Path.GetExtension(FileUpload1.FileName) = ".xls")) Then
                    excelReader = ExcelReaderFactory.CreateBinaryReader(FileUpload1.FileContent)
                    excelReader.IsFirstRowAsColumnNames = True
                    'obj1.DeleteEPGData(ddlChannelName.SelectedValue)
                    obj1.DeleteEPGData(txtChannel.Text)

                    Dim a As Date
                    If excelReader.AsDataSet.Tables(0).Rows.Count > 0 Then
                        For i = 0 To excelReader.AsDataSet.Tables(0).Rows.Count - 1
                            If IsDBNull(excelReader.AsDataSet.Tables(0).Rows(i).Item(0)) Then
                                Exit For
                            End If

                            If dateFlag = 0 Then
                                Try
                                    strFirstDate = (Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(2)).ToString)).ToString
                                Catch ex As Exception
                                    strFirstDate = Convert.ToDateTime(excelReader.AsDataSet.Tables(0).Rows(i).Item(2).ToString).ToString
                                End Try
                                dateFlag = 1
                            End If

                            Try
                                strLastdate = (Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(2)).ToString)).ToString
                                a = Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(2)).ToString).Date
                            Catch ex As Exception
                                strLastdate = Convert.ToDateTime(excelReader.AsDataSet.Tables(0).Rows(i).Item(2).ToString).ToString
                                a = Convert.ToDateTime(excelReader.AsDataSet.Tables(0).Rows(i).Item(2).ToString).Date
                            End Try

                            If a >= DateTime.Now.AddDays(-1).Date Then

                                Dim last_char As Char
                                Dim vProgName As String = (excelReader.AsDataSet.Tables(0).Rows(i).Item(0)).ToString
                                last_char = vProgName.Substring(vProgName.Length - 1)

                                Dim vChannelID As String = txtChannel.Text 'ddlChannelName.SelectedValue.ToString.Trim
                                Dim vGenre As String = excelReader.AsDataSet.Tables(0).Rows(i).Item(1).ToString
                                Dim vDate As String = a.ToString

                                Dim vTime As String
                                Try
                                    vTime = Convert.ToDateTime(Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(3)).ToString)).ToString("HH:mm:ss")
                                Catch
                                    vTime = Convert.ToDateTime(excelReader.AsDataSet.Tables(0).Rows(i).Item(3)).ToString("HH:mm:ss")
                                End Try

                                Dim vDuration As String = excelReader.AsDataSet.Tables(0).Rows(i).Item(4).ToString
                                Dim vDescription As String = Logger.RemSplCharsEng(excelReader.AsDataSet.Tables(0).Rows(i).Item(5).ToString)
                                Dim vEpisodeNo As String = excelReader.AsDataSet.Tables(0).Rows(i).Item(6).ToString
                                Dim vShowWiseDesc As String = excelReader.AsDataSet.Tables(0).Rows(i).Item(7).ToString

                                If Not Char.IsLetterOrDigit(last_char) Then
                                    If last_char = "+" Then
                                        InsertEPGData(vChannelID, vProgName, vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc, chkIgnoreSpecial.Checked)
                                    Else
                                        InsertEPGData(vChannelID, vProgName.Substring(0, vProgName.Length - 1), vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc, chkIgnoreSpecial.Checked)
                                    End If
                                Else
                                    InsertEPGData(vChannelID, vProgName, vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc, chkIgnoreSpecial.Checked)
                                End If
                            End If
                        Next
                    End If

                End If

            End If

            If flag = 0 Then
                grdData.DataBind()
                grdGenre.DataBind()
                grdExcelData.DataBind()
                lbUploadError.Visible = False
                btnUpload.Enabled = False
                myMessageBox("File Uploaded Successfully.")
            Else
                grdData.DataBind()
                grdGenre.DataBind()
                grdExcelData.DataBind()
                btnUpload.Enabled = True
                'btnBuildEPG.Enabled = False
                lbUploadError.Visible = True
                lbUploadError.Text = "File not uploaded. Please check error report"
                myErrorBox("File not uploaded. Please check error report")
            End If
            flag = 0
            bindAll()
        Catch ex As Exception
            Logger.LogError("Upload Schedule", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        Finally
            If IsNothing(excelReader) Then
                excelReader.Close()
            End If
        End Try
    End Sub

    Private Sub InsertEPGData(ByVal vChannelName As String, ByVal vProgName As String, ByVal vGenre As String, ByVal vDate As Date, ByVal vtime As Date, ByVal vduration As Integer, ByVal VDescription As String, ByVal vEpisodeNo As String, ByVal vShowWiseDesc As String, ByVal vIgnoreSpecial As Boolean)
        Try
            If Not vIgnoreSpecial Then
                vProgName = Logger.RemSplCharsEng(vProgName).Trim
                VDescription = Logger.RemSplCharsEng(VDescription).Trim
                vShowWiseDesc = Logger.RemSplCharsEng(vShowWiseDesc).Trim
            End If
            

            If vduration < 0 Then
                vduration = Math.Abs(Convert.ToInt16(vduration))
            End If
            If Not (vduration = 0) Then
                Dim sql As String = "insert into map_EPGExcel(channelID, [Program Name],Genre,Date,Time,Duration, Description, [Episode No],[Show-wise Description]) values('" & vChannelName & "','" & vProgName.Replace("'", "''") & "','" & vGenre & "','" & vDate & "','" & vtime & "','" & vduration & "','" & VDescription.Replace("'", "''") & "','" & vEpisodeNo & "','" & vShowWiseDesc.Replace("'", "''") & "')"
                obj.executeSQL(sql, False)
            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        End Try
    End Sub

    Private Sub grdEpiMissing_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdEpiMissing.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdEpiMissing.Rows(index)

            Dim lbRowId As Label = DirectCast(grdEpiMissing.Rows(index).FindControl("lbRowId"), Label)
            Dim txtEpisodeNo As TextBox = DirectCast(grdEpiMissing.Rows(index).FindControl("txtEpisodeNo"), TextBox)

            If e.CommandName.ToLower = "updateepi" Then
                obj1.upDateEpisodeinMapEPGExcel(lbRowId.Text, txtEpisodeNo.Text, "Upload Schedule1", User.Identity.Name)
                bindAll()
            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Please check error report.")
        End Try

    End Sub

    Private Sub grdData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdData.Rows(index)
            Dim ddlgenre, ddlrating, ddlseries, ddlactive As DropDownList
            Dim ddlPrograms As ComboBox
            ddlgenre = DirectCast(grdData.Rows(index).FindControl("ddlgenre"), DropDownList)
            ddlrating = DirectCast(grdData.Rows(index).FindControl("ddlrating"), DropDownList)
            ddlseries = DirectCast(grdData.Rows(index).FindControl("ddlseries"), DropDownList)
            ddlactive = DirectCast(grdData.Rows(index).FindControl("ddlactive"), DropDownList)
            ddlPrograms = DirectCast(grdData.Rows(index).FindControl("ddlPrograms"), ComboBox)

            Dim lbChannelId As Label = DirectCast(grdData.Rows(index).FindControl("lbChannelId"), Label)
            Dim lbEpisodeNo As Label = DirectCast(grdData.Rows(index).FindControl("lbEpisodeNo"), Label)
            Dim lbStatus As Label = DirectCast(grdData.Rows(index).FindControl("lbStatus"), Label)
            Dim chkIsMovie As CheckBox = DirectCast(grdData.Rows(index).FindControl("chkIsMovie"), CheckBox)
            Dim txtSearchMovie As TextBox = DirectCast(grdData.Rows(index).FindControl("txtSearchMovie"), TextBox)

            Dim lbExcelProgName As Label = DirectCast(grdData.Rows(index).FindControl("lbExcelProgName"), Label)

            If e.CommandName.ToLower = "addnew" Then
                obj1.Insert_mstProg(lbChannelId.Text.Trim, lbExcelProgName.Text.Trim, ddlgenre.SelectedValue.ToString.Trim, ddlrating.SelectedValue.ToString.Trim, ddlseries.SelectedValue.ToString.Trim, ddlactive.SelectedValue.ToString.Trim, IIf(lbEpisodeNo.Text.Trim = "", 0, lbEpisodeNo.Text.Trim), lbStatus.Text, chkIsMovie.Checked, "Upload Schedule1", User.Identity.Name, chkIgnoreSpecial.Checked)
                grdData.DataBind()
                bindAll()
            ElseIf e.CommandName.ToLower = "updt" Then
                obj1.updateExcelProg(txtChannel.Text, lbExcelProgName.Text, ddlPrograms.SelectedItem.Text)
                grdData.DataBind()
                bindAll()
            ElseIf e.CommandName.ToLower = "updatemovie" Then
                obj1.updateExcelProg(txtChannel.Text, lbExcelProgName.Text, txtSearchMovie.Text)
                grdData.DataBind()
                bindAll()

            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Please check error report.")
        End Try

    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim btn As Button = DirectCast(e.Row.FindControl("Btn_AddNew"), Button)
                Dim ddlgenre As DropDownList = DirectCast(e.Row.FindControl("ddlGenre"), DropDownList)
                Dim ddlrating As DropDownList = DirectCast(e.Row.FindControl("ddlRating"), DropDownList)
                Dim ddlSeries As DropDownList = DirectCast(e.Row.FindControl("ddlSeries"), DropDownList)
                Dim ddlActive As DropDownList = DirectCast(e.Row.FindControl("ddlActive"), DropDownList)
                Dim ddlPrograms As ComboBox = DirectCast(e.Row.FindControl("ddlPrograms"), ComboBox)
                Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)
                Dim lbExcelProgName As Label = DirectCast(e.Row.FindControl("lbExcelProgName"), Label)
                Dim txtSearchMovie As TextBox = DirectCast(e.Row.FindControl("txtSearchMovie"), TextBox)
                Dim btnUpdateMovie As Button = DirectCast(e.Row.FindControl("btnUpdateMovie"), Button)

                txtSearchMovie.Attributes.Add("onkeydown", "javascript:SetContextKey('MainContent_grdData_ACE_txtSearchMovie_" & e.Row.RowIndex & "','" & lbExcelProgName.Text & "');")
                'ACE_txtSearchMovie
                'MainContent_grdData_ACE_txtSearchMovie_0

                If lbExcelProgName.Text.Trim = "" Then
                    btn.Visible = False
                    ddlgenre.Visible = False
                    ddlrating.Visible = False
                    ddlSeries.Visible = False
                    ddlActive.Visible = False
                    ddlPrograms.Visible = False
                    txtSearchMovie.Visible = False
                    btnUpdateMovie.Visible = False
                    Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                    'Dim btn2 As Button = DirectCast(e.Row.FindControl("Btn_addnew"), Button)
                    btn1.Visible = False
                    'btn2.Visible = False
                    'btnBuildEPG.Enabled = True
                    'bindEpiMissing()
                Else
                    txtProgName.Text = lbExcelProgName.Text.Trim.Substring(0, 1)
                   
                    sqlDSPrograms.SelectCommand = "select progid,progname from mst_program where active=1 and channelid='" & txtChannel.Text & "' and progname like '" & txtProgName.Text.Replace("'", "''") & "%' order by progname"
                    ddlPrograms.DataSource = sqlDSPrograms
                    ddlPrograms.DataTextField = "progname"
                    ddlPrograms.DataValueField = "progid"
                    ddlPrograms.DataBind()
                    btn.Visible = True
                    If Server.HtmlDecode(e.Row.Cells(3).Text).Trim.ToUpper = "INSERT" Then
                        Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                        btn1.Visible = False
                    Else
                        Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                        btn1.Visible = True
                    End If
                    ddlgenre.Visible = True
                    ddlrating.Visible = True
                    ddlrating.SelectedValue = "U"
                    ddlSeries.Visible = True
                    ddlActive.Visible = True
                    ddlPrograms.Visible = True
                    'btnBuildEPG.Enabled = False
                End If
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule", "grdData_RowDataBound", ex.InnerException.ToString, User.Identity.Name)
            Logger.LogError("Upload Schedule", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnBuildEPG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuildEPG.Click
        Try
            'btnBuildEPG.Enabled = False

            Dim obj1 As New clsUploadModules
            'Dim boolBuildEPGSuccess As Boolean = obj1.BuildEPG(ddlChannelName.Text.ToString, True, chkTentative.Checked)
            Dim boolBuildEPGSuccess As Boolean = obj1.BuildEPG(txtChannel.Text, True, chkTentative.Checked, User.Identity.Name)

            If Not boolBuildEPGSuccess Then
                myErrorBox("EPG not Built. Please check error logs!")
                Exit Sub
            End If

            '-----------------------------------Error while building EPG. Duration not matching-------------------------------

            'Dim dt As DataTable = obj1.EPGUploadError(ddlChannelName.Text.Trim)
            Dim dt As DataTable = obj1.EPGUploadError(txtChannel.Text)

            grdBuildEPGError.DataSource = dt
            grdBuildEPGError.DataBind()
            Dim i As Integer = 0
            For Each row As GridViewRow In grdBuildEPGError.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    i = i + 1
                End If
            Next
            If i > 0 Then
                myErrorBox("Error in Program Duration. Please see error Table")
                'btnBuildEPG.Enabled = True
                'bindEpiMissing()
                grdBuildEPGError.Visible = True
                Exit Sub
            End If
            '-----------------------------------END-------------------Error while building EPG. Duration not matching-----------------------

            'btnBuildEPG.Enabled = False
            lbEPGBuiltMessage.Visible = True

            myMessageBox("EPG Built Successfully!")

        Catch ex As Exception
            Logger.LogError("Upload Schedule", "btnBuildEPG_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdGenre_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdGenre.RowCommand
        Try
            If e.CommandName.ToLower = "updt" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim row As GridViewRow = grdGenre.Rows(index)
                Dim strReplaceFrom As String = Server.HtmlDecode(row.Cells(0).Text)
                Dim strReplaceTo As String = DirectCast(grdGenre.Rows(index).FindControl("ddl_AvailableGenre"), DropDownList).SelectedItem.Text
                obj1.updateExcelGenre(strReplaceFrom, strReplaceTo)
                grdGenre.DataBind()
                bindAll()
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule", "grdGenre_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdGenre_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGenre.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ddl As DropDownList = DirectCast(e.Row.FindControl("ddl_AvailableGenre"), DropDownList)
                Dim chkUpdateGenre As CheckBox = DirectCast(e.Row.FindControl("chkUpdateGenre"), CheckBox)
                If Server.HtmlDecode(e.Row.Cells(0).Text).Trim = "" Then
                    ddl.Visible = False
                    chkUpdateGenre.Visible = False
                    btnUpdateGenre.Visible = False
                    Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                    btn1.Visible = False
                  
                Else
                    ddl.DataSource = SqlDSAvailableGenre
                    ddl.DataValueField = "GenreID"
                    ddl.DataTextField = "GenreName"
                    ddl.DataBind()
                    ddl.Visible = True
                    chkUpdateGenre.Visible = True
                    btnUpdateGenre.Visible = True

                End If

            End If
          
        Catch ex As Exception
            Logger.LogError("Upload Schedule", "grdGenre_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        btnBuildEPG.Enabled = True
        bindAll()
        'bindEpiMissing()
        btnUpload.Enabled = True
        lbEPGBuiltMessage.Visible = False
        grdBuildEPGError.Visible = False
        If lbIsMovieChannel.Text.ToUpper = "TRUE" Then
            grdData.Columns(2).Visible = True
        Else
            grdData.Columns(2).Visible = False
        End If
    End Sub

    Private Sub bindgrdExcelData()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select [Program Name], Genre, convert(varchar,[Date],106), convert(varchar,[Time],108), Duration, [Description],[Episode No],[Show-wise Description],channelID from map_EPGExcel where channelID=@ChannelId order by date,time", "ChannelId", "varchar", txtChannel.Text, False, False)
        grdExcelData.DataSource = dt
        grdExcelData.DataBind()

    End Sub

    Private Sub bindgrdData()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_epg_validate_progname", "ChannelId", "varchar", txtChannel.Text, True, False)
        grdData.DataSource = dt
        grdData.DataBind()


        Dim dt1 As DataTable = obj.executeSQL("sp_epg_validate_genre", "ChannelId", "varchar", txtChannel.Text, True, False)
        grdGenre.DataSource = dt1
        grdGenre.DataBind()

        If dt.Rows(0)(0).ToString = "" And dt1.Rows(0)(0).ToString = "" Then
            btnBuildEPG.Enabled = True
        Else
            btnBuildEPG.Enabled = False
        End If
    End Sub

  

    Private Sub btnUpdateGenre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateGenre.Click
        Try
            Dim row As GridViewRow
            For Each row In grdGenre.Rows
                Dim chkUpdateGenre As CheckBox = DirectCast(row.FindControl("chkUpdateGenre"), CheckBox)
                If chkUpdateGenre.Checked Then
                    Dim strReplaceFrom As String = Server.HtmlDecode(row.Cells(0).Text)
                    Dim strReplaceTo As String = DirectCast(row.FindControl("ddl_AvailableGenre"), DropDownList).SelectedItem.Text
                    obj1.updateExcelGenre(strReplaceFrom, strReplaceTo)
                End If
            Next row
            grdGenre.DataBind()
            If flag = 0 Then
                myMessageBox("Genre Update successful.")
            Else
                myErrorBox("Please check error report.")
            End If
            flag = 0
            bindAll()
        Catch ex As Exception
            Logger.LogError("Upload Schedule", "btnUpdateGenre_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub bindAll()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select movie_channel,movielangid from mst_channel where channelid='" & txtChannel.Text & "'", False)
        If dt.Rows.Count > 0 Then
            lbIsMovieChannel.Text = dt.Rows(0)("movie_channel").ToString
        End If
        bindgrdData()
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
        bindgrdExcelData()
    End Sub

End Class
