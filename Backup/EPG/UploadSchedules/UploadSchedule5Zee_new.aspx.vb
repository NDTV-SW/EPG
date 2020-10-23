Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadSchedule5Zee_new
    Inherits System.Web.UI.Page

    Dim obj1 As New clsUploadModules
    Dim obj As New clsExecute

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("Upload Schedule5ZEE", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Dim strFirstDate As String = "", strLastdate As String = ""
    Dim dateFlag As Integer = 0, errFlag As Integer = 0
    Dim strsql As String
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            Dim obj1 As New clsUploadModules
            obj1.DeleteEPGData(txtChannel.Text)
            strsql = ""
            Dim strPath As String = Server.MapPath("~/Excel/")
            strPath = strPath & DateTime.Now.ToString("ddMMMyyyy_HHmmss") & FileUpload1.FileName
            FileUpload1.SaveAs(strPath)
            uploadChannelSchedule(txtChannel.Text, strPath)

            lbUploadError.Visible = False
            btnUpload.Enabled = False
            myMessageBox("File Uploaded Successfully.")

        Catch ex As Exception
            btnUpload.Enabled = True
            lbUploadError.Visible = True
            lbUploadError.Text = "File not uploaded. Please check error report"
            myErrorBox("File not uploaded. Please check error report")

            Logger.LogError("Upload Schedule 5 new", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Private Sub InsertEPGData(ByVal vChannelName As String, ByVal vProgName As String, ByVal vGenre As String, ByVal vDate As Date, ByVal vtime As Date, ByVal vduration As Integer, ByVal VDescription As String, ByVal vEpisodeNo As String, ByVal vShowWiseDesc As String, ByVal boolInsert As Boolean)
        vProgName = Logger.RemSplCharsEng(vProgName).Trim
        VDescription = Logger.RemSplCharsEng(VDescription).Trim
        vShowWiseDesc = Logger.RemSplCharsEng(vShowWiseDesc).Trim
        Dim newDuration As Integer = 0
        If vduration > 240 Then
            Do While (vduration > 240)
                newDuration = 240
                Dim vtimeDay As Integer = vtime.Date.Day
                strsql = strsql & "insert into map_epgexcel (channelID, [Program Name],Genre,Date,Time,Duration, Description, [Episode No],[Show-wise Description]) values('" & vChannelName.ToString.Trim & "','" & vProgName.ToString.Trim.Replace("'", "''") & "','" & vGenre.ToString.Trim & "','" & vDate & "','" & vtime.ToLongTimeString & "','" & newDuration & "','" & VDescription.Replace("'", "''").ToString.Trim & "','" & vEpisodeNo.ToString.Trim & "','" & vShowWiseDesc.Replace("'", "''") & "');"
                vtime = vtime.AddMinutes(newDuration)
                If vtimeDay <> vtime.Date.Day Then
                    vDate = vDate.AddDays(vtime.Date.Day - vtimeDay)
                End If
                vduration = vduration - newDuration
            Loop
            strsql = strsql & "insert into map_epgexcel (channelID, [Program Name],Genre,Date,Time,Duration, Description, [Episode No],[Show-wise Description]) values('" & vChannelName.ToString.Trim & "','" & vProgName.ToString.Trim.Replace("'", "''") & "','" & vGenre.ToString.Trim & "','" & vDate & "','" & vtime.ToLongTimeString & "','" & vduration & "','" & VDescription.Replace("'", "''").ToString.Trim & "','" & vEpisodeNo.ToString.Trim & "','" & vShowWiseDesc.Replace("'", "''") & "');"
        Else
            strsql = strsql & "insert into map_epgexcel(channelID, [Program Name],Genre,Date,Time,Duration, Description, [Episode No],[Show-wise Description]) values('" & vChannelName.ToString.Trim & "','" & vProgName.ToString.Trim.Replace("'", "''") & "','" & vGenre.ToString.Trim & "','" & vDate & "','" & vtime & "','" & vduration & "','" & VDescription.Replace("'", "''").ToString.Trim & "','" & vEpisodeNo.ToString.Trim & "','" & vShowWiseDesc.Replace("'", "''") & "');"
        End If

        If boolInsert Then
            Dim obj As New clsExecute
            obj.executeSQL(strsql, False)
            strsql = ""
        End If

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
            Logger.LogError("Upload Schedule5ZEE", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
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
                obj1.Insert_mstProg(lbChannelId.Text.Trim, lbExcelProgName.Text.Trim, ddlgenre.SelectedValue.ToString.Trim, ddlrating.SelectedValue.ToString.Trim, ddlseries.SelectedValue.ToString.Trim, ddlactive.SelectedValue.ToString.Trim, IIf(lbEpisodeNo.Text.Trim = "", 0, lbEpisodeNo.Text.Trim), lbStatus.Text, chkIsMovie.Checked, "Upload Schedule1", User.Identity.Name)
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
            Logger.LogError("Upload Schedule5ZEE", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
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
                    Dim btn2 As Button = DirectCast(e.Row.FindControl("Btn_addnew"), Button)
                    btn1.Visible = False
                    btn2.Visible = False

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
            Logger.LogError("Upload Schedule5ZEE", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnBuildEPG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuildEPG.Click
        Try

            Dim obj1 As New clsUploadModules
            Dim boolBuildEPGSuccess As Boolean = obj1.BuildEPG(txtChannel.Text, True, chkTentative.Checked, User.Identity.Name)

            If Not boolBuildEPGSuccess Then
                myErrorBox("EPG not Built. Please check error logs!")
                Exit Sub
            End If

            '-----------------------------------Error while building EPG. Duration not matching-------------------------------

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

                grdBuildEPGError.Visible = True
                Exit Sub
            End If
            '-----------------------------------END-------------------Error while building EPG. Duration not matching-----------------------


            lbEPGBuiltMessage.Visible = True

            myMessageBox("EPG Built Successfully!")

        Catch ex As Exception
            Logger.LogError("Upload Schedule5ZEE", "btnBuildEPG_Click", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule5ZEE", "grdGenre_RowCommand", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule5ZEE", "grdGenre_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        bindAll()
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
            bindAll()
        Catch ex As Exception
            Logger.LogError("Upload Schedule5ZEE", "btnUpdateGenre_Click", ex.Message.ToString, User.Identity.Name)
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
    Private Sub uploadChannelSchedule(ByVal channelid As String, ByVal filePath As String)
      Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim DtSet As System.Data.DataSet

        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
        MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data Source='" & filePath & "';Extended Properties='Excel 12.0; HDR=NO'")
        MyConnection.Open()

        Dim dtExcelSchema As DataTable
        dtExcelSchema = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        MyConnection.Close()


        Dim SheetName As String = dtExcelSchema.Rows(1)("TABLE_NAME").ToString()
        MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & SheetName & "]", MyConnection)
        MyCommand.TableMappings.Add("Table", "TestTable")
        DtSet = New System.Data.DataSet
        MyCommand.Fill(DtSet)
        MyConnection.Close()


        Dim dt As DataTable = DtSet.Tables(0)
        If dt.Rows.Count > 7 Then
            For i As Integer = 7 To dt.Rows.Count - 1
                Dim vChannelID As String, vProgName As String, vNextProgName As String, vGenre As String, vDate As String, vNextDate As String
                Dim vTime As String, vDuration As Integer, vNextDuration As Integer, vDescription As String, vEpisodeNo As String, vNextEpisodeNo As String, vShowWiseDesc As String
                vChannelID = channelid

                Dim dtTempTime As Date, dtTempNextTime As Date

                vProgName = dt.Rows(i)(4).ToString
                vDate = dt.Rows(i)(1).ToString
                vTime = dt.Rows(i)(2).ToString
                dtTempTime = dt.Rows(i)(3).ToString
                vDuration = (dtTempTime.Hour * 60) + (dtTempTime.Minute)
                vGenre = ""
                vDescription = dt.Rows(i)(8).ToString
                vEpisodeNo = 0
                vShowWiseDesc = ""

                If i = 34 Then
                    vEpisodeNo = vEpisodeNo
                End If

                If i < dt.Rows.Count - 1 Then
                    vNextProgName = dt.Rows(i + 1)(4).ToString
                    vNextDate = dt.Rows(i + 1)(1).ToString
                    dtTempNextTime = dt.Rows(i + 1)(2).ToString
                    vNextDuration = (dtTempNextTime.Hour * 60) + (dtTempNextTime.Minute)
                    vNextEpisodeNo = 0
                    If (vDate <> vNextDate) And (vProgName = vNextProgName) And (vEpisodeNo = vNextEpisodeNo) Then
                        vDuration = vDuration + vNextDuration
                        i = i + 1
                    End If
                End If

                If i = dt.Rows.Count - 1 Then

                    Try

                        InsertEPGData(vChannelID, vProgName, vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc, True)
                        bindAll()

                    Catch ex As Exception
                        Logger.LogError("Upload Schedule New", "uploadChannelSchedule", channelid & " || " & filePath & "||" & ex.Message.ToString, User.Identity.Name)
                    End Try
                Else
                    InsertEPGData(vChannelID, vProgName, vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc, False)
                End If

            Next

        End If

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
