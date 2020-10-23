Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient
Imports AjaxControlToolkit
Imports System.Globalization

Public Class UploadSchedule8_SP
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
            Logger.LogError("Upload Schedule 8", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Dim dateFlag As Integer = 0, strSql As String

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            strSql = ""
            Dim obj1 As New clsUploadModules
            obj1.DeleteEPGData(txtChannel.Text)

            Dim strPath As String = Server.MapPath("~/Excel/")
            strPath = strPath & DateTime.Now.ToString("ddMMMyyyy_HHmmss") & FileUpload1.FileName
            FileUpload1.SaveAs(strPath)
            uploadChannelSchedule(txtChannel.Text, strPath)

            lbUploadError.Visible = False
            btnUpload.Enabled = False



            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select distinct progdate from swayamprabha_epg order by 1", False)
            If dt.Rows.Count > 0 Then
             
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dtNew As DataTable = obj.executeSQL("select * from swayamprabha_epg where progdate='" & dt.Rows(i)("progdate").ToString & "' order by rowid", False)
                    Dim intDayDurationInSecs As Integer = 86400
                    Dim intCurrDuration As Integer = 0
                    Dim strChannelId As String, strProgname As String, dtDate As DateTime, dtTime As DateTime, intDuration As Integer, strDescription As String
                    Dim sqlEPG As String = ""
                    dtTime = DateTime.Now.Date
                    Dim intTempDuration As Integer = 0
                    While intTempDuration <= intDayDurationInSecs
                        For k As Integer = 0 To dtNew.Rows.Count - 1
                            intTempDuration = intTempDuration + intDuration

                            strChannelId = dtNew.Rows(k)("channelid").ToString
                            strProgname = dtNew.Rows(k)("progname").ToString
                            dtDate = dtNew.Rows(k)("progdate").ToString
                            intDuration = dtNew.Rows(k)("duration").ToString

                            strDescription = dtNew.Rows(k)("synopsis").ToString

                            If intTempDuration <= intDayDurationInSecs Then
                                insertEPG(strChannelId, strProgname, dtDate, dtTime, strDescription, intDuration / 60, False, False)
                            Else
                                If DateTime.Now.Date = dtTime.Date Then
                                    insertEPG(strChannelId, strProgname, dtDate, dtTime, strDescription, intDuration / 60, True, False)
                                Else
                                    insertEPG(strChannelId, strProgname, dtDate, dtTime, strDescription, intDuration / 60, True, True)
                                End If

                                Exit While
                            End If
                            dtTime = dtTime.AddSeconds(intDuration)
                        Next
                    End While

                Next
            End If
            bindAll()
            myMessageBox("File Uploaded Successfully.")

        Catch ex As Exception
            btnUpload.Enabled = True
            lbUploadError.Visible = True
            lbUploadError.Text = "File not uploaded. Please check error report"
            myErrorBox("File not uploaded. Please check error report")

            Logger.LogError("Upload Schedule 8", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Dim strEPGSQL As String
    Private Sub insertEPG(ByVal vChannelid As String, ByVal vProgname As String, ByVal dtDate As DateTime, ByVal dtTime As DateTime, ByVal vDescription As String, ByVal vDuration As Integer, ByVal vBoolInsert As Boolean, ByVal vBoolJustInsert As Boolean)
        If vBoolJustInsert Then
            obj.executeSQL(strEPGSQL, False)
            strEPGSQL = ""
        Else
            strEPGSQL = strEPGSQL & "insert into map_EPGExcel(channelID,[Program name],[Date],[Time],[Description],duration) values" & _
            "('" & vChannelid & "','" & vProgname.Replace("'", "''") & "','" & dtDate & "','" & dtTime & "','" & vDescription.Replace("'", "''") & "','" & vDuration & "');"

            If vBoolInsert Then
                obj.executeSQL(strEPGSQL, False)
                strEPGSQL = ""
            End If
        End If
        

       
    End Sub

    Private Sub uploadChannelSchedule(ByVal channelid As String, ByVal filePath As String)
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

        Dim dt As DataTable = DtSet.Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim vChannelID As String, vProgName As String, vDate As DateTime, vPrevDate As DateTime, vTime As DateTime
                Dim vDuration As DateTime, vSynopsis As String
                vChannelID = channelid
                vProgName = dt.Rows(i)(4).ToString
                vSynopsis = IIf(dt.Rows(i)(6).ToString = "", "", dt.Rows(i)(6).ToString & ", ") & dt.Rows(i)(7).ToString
                Dim provider As CultureInfo = CultureInfo.InvariantCulture
                Try
                    vDate = DateTime.ParseExact(dt.Rows(i)(11).ToString, "d-M-yyyy", provider)
                Catch ex As Exception
                    Try
                        vDate = DateTime.ParseExact(dt.Rows(i)(11).ToString, "dd-M-yyyy", provider)
                    Catch ex1 As Exception
                        Try
                            vDate = DateTime.ParseExact(dt.Rows(i)(11).ToString, "d-MM-yyyy", provider)
                        Catch ex2 As Exception
                            Try
                                vDate = DateTime.ParseExact(dt.Rows(i)(11).ToString, "dd-MM-yyyy", provider)
                            Catch ex3 As Exception

                            End Try
                        End Try
                    End Try
                End Try

                If i > 0 Then


                    Try
                        vPrevDate = DateTime.ParseExact(dt.Rows(i - 1)(11).ToString, "d-M-yyyy", provider)
                    Catch ex As Exception
                        Try
                            vPrevDate = DateTime.ParseExact(dt.Rows(i - 1)(11).ToString, "dd-M-yyyy", provider)
                        Catch ex1 As Exception
                            Try
                                vPrevDate = DateTime.ParseExact(dt.Rows(i - 1)(11).ToString, "d-MM-yyyy", provider)
                            Catch ex2 As Exception
                                Try
                                    vPrevDate = DateTime.ParseExact(dt.Rows(i - 1)(11).ToString, "dd-MM-yyyy", provider)
                                Catch ex3 As Exception

                                End Try
                            End Try
                        End Try
                    End Try
                End If

                vDuration = dt.Rows(i)(5).ToString.Replace(".", ":")
                Dim vDurationSecs As Integer
                vDurationSecs = (vDuration - vDuration.Date).TotalSeconds

                If (i = 0) Or Not (vDate.Date = vPrevDate.Date) Then
                    vTime = "00:00:00"
                Else
                    vTime = vTime.AddSeconds(vDurationSecs)
                End If

                If i = dt.Rows.Count - 1 Then
                    Try
                        InsertEPGData(vChannelID, vProgName, vSynopsis, vDate, vTime, vDurationSecs, True)
                        bindAll()
                    Catch ex As Exception
                        Logger.LogError("Upload Schedule 8", "uploadChannelSchedule", channelid & " || " & filePath & "||" & ex.Message.ToString, User.Identity.Name)
                    End Try
                Else
                    InsertEPGData(vChannelID, vProgName, vSynopsis, vDate, vTime, vDurationSecs, False)
                End If

            Next

        End If

    End Sub
    Private Sub InsertEPGData(ByVal vChannelName As String, ByVal vProgName As String, ByVal vSynopsis As String, ByVal vDate As Date, ByVal vTime As DateTime, ByVal vduration As Integer, ByVal boolInsert As Boolean)
        vProgName = Logger.RemSplCharsEng(vProgName).Trim
        vSynopsis = Logger.RemSplCharsEng(vSynopsis).Trim

        strSql = strSql & "insert into  swayamprabha_epg(channelid ,progdate,progtime,duration ,progname,synopsis) values"
        strSql = strSql & "('" & vChannelName & "','" & vDate & "','" & vTime & "','" & vduration & "','" & vProgName.Replace("'", "''") & "','" & vSynopsis.Replace("'", "''") & "');"

        If boolInsert Then
            Dim obj As New clsExecute
            obj.executeSQL("truncate table swayamprabha_epg", False)
            obj.executeSQL(strSql, False)
            strSql = ""
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
            Logger.LogError("Upload Schedule 8", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
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
                obj1.Insert_mstProg(lbChannelId.Text.Trim, lbExcelProgName.Text.Trim, ddlgenre.SelectedValue.ToString.Trim, ddlrating.SelectedValue.ToString.Trim, ddlseries.SelectedValue.ToString.Trim, ddlactive.SelectedValue.ToString.Trim, IIf(lbEpisodeNo.Text.Trim = "", 0, lbEpisodeNo.Text.Trim), lbStatus.Text, chkIsMovie.Checked, "Upload Schedule1", User.Identity.Name, False)
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
            Logger.LogError("Upload Schedule 8", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Please check error report.")
        End Try

    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim btn As Button = DirectCast(e.Row.FindControl("Btn_AddNew"), Button)
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
                    'btn.Visible = False
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
                Else
                    txtProgName.Text = lbExcelProgName.Text.Trim.Substring(0, 1)

                    sqlDSPrograms.SelectCommand = "select progid,progname from mst_program where active=1 and channelid='" & txtChannel.Text & "' and progname like '" & txtProgName.Text.Replace("'", "''") & "%' order by progname"
                    ddlPrograms.DataSource = sqlDSPrograms
                    ddlPrograms.DataTextField = "progname"
                    ddlPrograms.DataValueField = "progid"
                    ddlPrograms.DataBind()
                    'btn.Visible = True
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
                Dim hyAddNew As HyperLink = TryCast(e.Row.FindControl("hyAddNew"), HyperLink)
                hyAddNew.NavigateUrl = "javascript:BeginProcess('" & lbChannelId.Text & "','" & e.Row.RowIndex & "')"

            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule 8", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule 8", "btnBuildEPG_Click", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule 8", "grdGenre_RowCommand", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule 8", "grdGenre_RowDataBound", ex.Message.ToString, User.Identity.Name)
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
        'DataSourceID="SqlDSgrdData" 
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select [Program Name], Genre, convert(varchar,[Date],106), convert(varchar,[Time],108), Duration, [Description],[Episode No],[Show-wise Description],channelID from map_EPGExcel where channelID=@ChannelId order by date,time", "ChannelId", "varchar", txtChannel.Text, False, False)
        grdExcelData.DataSource = dt
        grdExcelData.DataBind()
        'DataSourceID = "SqlDSgrdGenre"
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

    Private Sub bindgrdGenre()

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
            Logger.LogError("Upload Schedule 8", "btnUpdateGenre_Click", ex.Message.ToString, User.Identity.Name)
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
        bindgrdGenre()
    End Sub


    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    <System.Web.Script.Services.ScriptMethod(),
    System.Web.Services.WebMethod()>
    Public Shared Function SearchMovie(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getMovie(contextKey, prefixText, count)
        Return channels
    End Function

    Protected Sub grdExcelData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdExcelData.PageIndexChanging
        grdExcelData.PageIndex = e.NewPageIndex
        bindgrdExcelData()
    End Sub
    Protected Sub grdDataData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdData.PageIndexChanging
        grdData.PageIndex = e.NewPageIndex
        bindgrdData()
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        bindgrdData()
        bindgrdGenre()
        bindgrdExcelData()
    End Sub
End Class
