Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadSchedule_Movies
    Inherits System.Web.UI.Page
    Dim flag As Integer = 0
    Dim obj1 As New clsUploadModules
    'Dim obj As New clsExecute

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
            myMessageBox("File Uploaded Successfully.")

        Catch ex As Exception
            btnUpload.Enabled = True
            lbUploadError.Visible = True
            lbUploadError.Text = "File not uploaded. Please check error report"
            myErrorBox("File not uploaded. Please check error report")

            Logger.LogError("Upload Schedule New Sports", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub uploadChannelSchedule(ByVal channelid As String, ByVal filePath As String)
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

            Dim obj As New clsExecute
            Dim dt As DataTable = DtSet.Tables(0)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Dim vChannelID As String, vProgName As String, vNextProgName As String, vGenre As String, vSubGenre As String, vDate As String, vNextDate As String
                    Dim vTime As String, vDuration As Integer, vNextDuration As Integer, vDescription As String, vEpisodeNo As String, vNextEpisodeNo As String
                    Dim vLiveRepeat As String
                    Dim vOrigLang As String, vDubbedLang As String, vStartCast As String, vDirector As String
                    Dim vProducer As String, vReleaseYear As String, vRating As String, vOriginCountry As String


                    vChannelID = channelid
                    vDate = dt.Rows(i)(0).ToString
                    vTime = dt.Rows(i)(1).ToString
                    vDuration = dt.Rows(i)(3).ToString
                    vGenre = dt.Rows(i)(4).ToString
                    vSubGenre = dt.Rows(i)(5).ToString
                    vProgName = dt.Rows(i)(6).ToString
                    vEpisodeNo = dt.Rows(i)(7).ToString
                    If vEpisodeNo = "" Then
                        vEpisodeNo = 0
                    End If

                    Try
                        vLiveRepeat = dt.Rows(i)(8).ToString
                    Catch ex As Exception
                        vLiveRepeat = ""
                    End Try
                    vDescription = dt.Rows(i)(11).ToString
                    vOrigLang = dt.Rows(i)(12).ToString
                    vDubbedLang = dt.Rows(i)(13).ToString
                    vStartCast = dt.Rows(i)(14).ToString
                    vDirector = dt.Rows(i)(15).ToString
                    vProducer = dt.Rows(i)(16).ToString
                    vReleaseYear = dt.Rows(i)(17).ToString
                    vRating = dt.Rows(i)(18).ToString
                    vOriginCountry = dt.Rows(i)(19).ToString

                    vNextProgName = ""
                    If i < dt.Rows.Count - 1 And vProgName.Trim <> "" Then
                        Try
                            vNextDate = dt.Rows(i + 1)(0).ToString
                            vNextProgName = dt.Rows(i + 1)(6).ToString
                            vNextDuration = dt.Rows(i + 1)(3).ToString
                            vNextEpisodeNo = dt.Rows(i + 1)(7).ToString
                            If (vDate <> vNextDate) And (vProgName = vNextProgName) And (vEpisodeNo = vNextEpisodeNo) Then
                                vDuration = vDuration + vNextDuration
                                i = i + 1
                            End If
                        Catch
                            i = i + 1
                        End Try
                    End If
                    If vLiveRepeat.Trim = "P" Or vLiveRepeat.Trim = "T" Then
                        lbEPGBuiltMessage.Text = "Original Repeat cannot have values of P or T"
                        'myMessageBox()
                        myErrorBox("Original Repeat cannot have values of P or T")
                        Exit For
                    End If
                    If i = dt.Rows.Count - 1 Or vNextProgName.Trim = "" Then

                        Try

                            InsertEPGData(vChannelID, vProgName, vGenre, vSubGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vLiveRepeat, vOrigLang, vDubbedLang, vStartCast, vDirector, vProducer, vReleaseYear, vRating, vOriginCountry, True, chkIgnoreSpecial.Checked)
                            bindAll()

                            Dim dtEpi As DataTable = obj.executeSQL("select progid from mst_program where isnull(exception,0)=0 and isnull(episodicsynopsis,0)=0 and channelid='" & vChannelID & "' and progname in(select [program name] from map_EPGExcel where channelid='" & vChannelID & "' and [episode no]>0 and genre not like 'movie%' )", False)
                            If dtEpi.Rows.Count > 0 Then
                                Dim dtStar As DataTable = obj.executeSQL("select genreid,companyid from mst_channel where channelid='" & vChannelID & "'", False)
                                If dtStar.Rows(0)("companyid").ToString = "5" And dtStar.Rows(0)("genreid").ToString = "110" Then
                                    obj.executeSQL("update mst_program set seriesenabled=1, episodicsynopsis=1 where progid in(select progid from mst_program where isnull(exception,0)=0 and isnull(episodicsynopsis,0)=0 and channelid='" & vChannelID & "' and progname in(select [program name] from map_EPGExcel where channelid='" & vChannelID & "' and [episode no]>0 ))", False)
                                    obj.executeSQL("sp_set_seriesid", True)
                                Else
                                    obj.executeSQL("update mst_program set seriesenabled=1, episodicsynopsis=1 where progid in(select progid from mst_program where isnull(exception,0)=0 and isnull(episodicsynopsis,0)=0 and channelid='" & vChannelID & "' and progname in(select [program name] from map_EPGExcel where description<>'' and channelid='" & vChannelID & "' and [episode no]>0 ))", False)
                                    obj.executeSQL("sp_set_seriesid", True)
                                End If
                            End If
                            Exit For
                        Catch ex As Exception
                            Logger.LogError("Upload Schedule New Sports", "uploadChannelSchedule", channelid & " || " & filePath & "||" & ex.Message.ToString, User.Identity.Name)
                        End Try

                    Else
                        InsertEPGData(vChannelID, vProgName, vGenre, vSubGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vLiveRepeat, vOrigLang, vDubbedLang, vStartCast, vDirector, vProducer, vReleaseYear, vRating, vOriginCountry, False, chkIgnoreSpecial.Checked)
                    End If

                Next
                dt = obj.executeSQL("select dbo.fn_get_durationcheck_dates('" & channelid & "')", False)
                If dt.Rows.Count > 0 Then
                    If Not dt.Rows(0)(0).ToString.Trim = "" Then
                        myErrorBox("Error in Program Duration. " & dt.Rows(0)(0).ToString.Trim)
                    End If

                End If
            End If
        Catch ex As Exception

            Logger.LogError("Upload Schedule", "UploadChannelSchedule", "Row:" & i & ", " & ex.Message.ToString, User.Identity.Name)
        End Try


    End Sub


    Private Sub InsertEPGData(vChannelName As String, vProgName As String, vGenre As String, vSubGenre As String, vDate As Date, vtime As Date, vduration As Integer, VDescription As String, ByVal vEpisodeNo As String, ByVal vLiveRepeat As String, vOrigLang As String, vDubbedLang As String, vStartCast As String, vDirector As String, vProducer As String, vReleaseYear As String, vRating As String, vOriginCountry As String, ByVal boolInsert As Boolean, vIgnoreSpecial As Boolean)
        If Not vIgnoreSpecial Then
            vProgName = Logger.RemSplCharsEng(vProgName).Trim
            VDescription = Logger.RemSplCharsEng(VDescription).Trim
        End If

        If VDescription.Length > 1000 Then
            VDescription = VDescription.Substring(0, 995)
        End If
        


        Dim obj As New clsExecute

        If vduration < 0 Then
            vduration = Math.Abs(vduration)
        End If

        If Not (vduration = 0) Then
            strSql = strSql & "insert into map_epgexcel(channelID, [Program Name],Genre,subgenre,Date,Time,Duration, Description, [Episode No],liverepeat,origlang,dubbedlang,actor,director,producer,releaseyear,pg,origincountry) "
            strSql = strSql & "values('" & vChannelName & "','" & vProgName.Trim.Replace("'", "''") & "','" & vGenre.Trim & "','" & vSubGenre.Trim & "'"
            strSql = strSql & ",'" & vDate & "','" & vtime & "','" & vduration & "','" & VDescription.Replace("'", "''").Trim & "'"
            strSql = strSql & ",'" & vEpisodeNo.Trim & "','" & vLiveRepeat & "','" & vOrigLang & "','" & vDubbedLang & "','" & vStartCast.Replace("'", "''") & "','" & vDirector.Replace("'", "''") & "','" & vProducer.Replace("'", "''") & "'"
            strSql = strSql & ",'" & vReleaseYear & "','" & vRating & "','" & vOriginCountry & "');"
        End If

        If boolInsert Then
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
            Logger.LogError("Upload Schedule", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
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
                'Dim txtSearchMovie As TextBox = DirectCast(e.Row.FindControl("txtSearchMovie"), TextBox)
                Dim btnUpdateMovie As Button = DirectCast(e.Row.FindControl("btnUpdateMovie"), Button)

                'txtSearchMovie.Attributes.Add("onkeydown", "javascript:SetContextKey('MainContent_grdData_ACE_txtSearchMovie_" & e.Row.RowIndex & "','" & lbExcelProgName.Text & "');")

                If lbExcelProgName.Text.Trim = "" Then
                    'btn.Visible = False
                    ddlgenre.Visible = False
                    ddlrating.Visible = False
                    ddlSeries.Visible = False
                    ddlActive.Visible = False
                    ddlPrograms.Visible = False
                    'txtSearchMovie.Visible = False
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
            Logger.LogError("Upload Schedule", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnBuildEPG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuildEPG.Click
        Try

            Dim obj1 As New clsUploadModules
            Dim boolBuildEPGSuccess As Boolean = obj1.BuildEPGStar(txtChannel.Text, True, chkTentative.Checked, User.Identity.Name)

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
        grdMissingRichMeta.DataBind()
    End Sub

    Private Sub bindgrdExcelData()
        'DataSourceID="SqlDSgrdData" 
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select [Program Name], Genre, convert(varchar,[Date],106), convert(varchar,[Time],108), Duration, [Description],[Episode No],EpisodicTitle,PG,LiveRepeat,channelID from map_EPGExcel where channelID=@ChannelId order by date,time", "ChannelId", "varchar", txtChannel.Text, False, False)
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


    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    <System.Web.Script.Services.ScriptMethod(),
    System.Web.Services.WebMethod()>
    Public Shared Function SearhRichMeta(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getRichMeta(contextKey, prefixText, count)
        'select type + '~' + name + '~' + convert(varchar(10),id) richmetaname from richmeta where name like '%love%'
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
    End Sub

    Protected Sub btnUpdateRich_Click(sender As Object, e As EventArgs) Handles btnUpdateRich.Click
        Dim obj As New clsExecute

        For Each row In grdMissingRichMeta.Rows
            Dim chkAddRich As CheckBox = DirectCast(row.FindControl("chkAddRich"), CheckBox)
            Dim txtSearchRichMeta As TextBox = DirectCast(row.FindControl("txtSearchRichMeta"), TextBox)
            Dim intRowId As Integer = DirectCast(row.FindControl("lbRowid"), Label).Text
            Dim intProgid As Integer = DirectCast(row.FindControl("lbProgid"), Label).Text
            Dim ddlType As DropDownList = DirectCast(row.FindControl("ddlType"), DropDownList)
            Dim intRichMetaId As Integer = 0

            If chkAddRich.Checked Then
                Dim sql As String
                sql = "insert into richmeta(type,name,synopsis,genrename1,genrename2,parentalguide,releaseyear,starcast,director,producer,originallanguageid,dubbedlanguge,country,imageurl,imageurlportrait) "
                sql = sql & "select '" & ddlType.SelectedValue & "' type,[program name] progname,description,genre,subgenre,pg,releaseyear,actor,director,producer,isnull((select languageid from mst_language where fullname=a.origlang),2)"
                sql = sql & ",isnull((select languageid from mst_language where fullname=a.dubbedlang),2),origincountry,b.programlogo,b.programlogoportrait from map_epgexcel a "
                sql = sql & " join mst_program b on a.[program name]=b.progname and a.channelid=b.channelid where a.rowid='" & intRowId & "'"
                obj.executeSQL(sql, False)
                HttpContext.Current.Session("richmeta") = Nothing
            ElseIf txtSearchRichMeta.Text.Trim.Length > 0 Then
                intRichMetaId = txtSearchRichMeta.Text.Split("~")(2)
                obj.executeSQL("update mst_program set richmetaid='" & intRichMetaId & "' where progid='" & intProgid & "'", False)

                Dim dtProg As DataTable = obj.executeSQL("select programlogo,programlogoportrait from mst_program where progid='" & intProgid & "'", False)
                Dim dtRich As DataTable = obj.executeSQL("select imageurl,imageurlportrait from richmeta where id='" & intRichMetaId & "'", False)

                Dim progProgramLogo As String = dtProg.Rows(0)("programlogo").ToString
                Dim progProgramLogoPortrait As String = dtProg.Rows(0)("programlogoportrait").ToString
                Dim richImageUrl As String = dtRich.Rows(0)("imageurl").ToString
                Dim richImageUrlPortrait As String = dtRich.Rows(0)("imageurlportrait").ToString

                If progProgramLogo.Trim = "" And richImageUrl.Trim <> "" Then
                    obj.executeSQL("update mst_program set programlogo='" & richImageUrl & "' where progid='" & intProgid & "'", False)
                End If
                If progProgramLogoPortrait.Trim = "" And richImageUrlPortrait.Trim <> "" Then
                    obj.executeSQL("update mst_program set programlogoportrait='" & richImageUrlPortrait & "' where progid='" & intProgid & "'", False)
                End If

                If progProgramLogo.Trim <> "" And richImageUrl.Trim = "" Then
                    obj.executeSQL("update richmeta set imageurl='" & progProgramLogo & "' where id='" & intRichMetaId & "'", False)
                End If
                If progProgramLogoPortrait.Trim <> "" And richImageUrlPortrait.Trim = "" Then
                    obj.executeSQL("update richmeta set imageurlportrait='" & progProgramLogoPortrait & "' where id='" & intRichMetaId & "'", False)
                End If

            End If

            'select top 10 programlogo,programlogoportrait,* from mst_program
            'select top 10 imageurl,imageurlportrait,* from richmeta

        Next row
        grdMissingRichMeta.DataBind()
    End Sub

    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Shared Function SearchMovie(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getMovie(contextKey, prefixText, count)
        Return channels
    End Function



End Class
