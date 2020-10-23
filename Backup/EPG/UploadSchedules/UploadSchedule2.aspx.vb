Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Imports System
Imports System.Net
Imports System.Xml
Imports WinSCP
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.IO.Compression
Imports Microsoft.VisualBasic
Imports System.Threading


Public Class UploadSchedule2
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
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
          
        Catch ex As Exception
            Logger.LogError("Upload Schedule2", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Dim strFirstDate As String = "", strLastdate As String = ""
    Dim dateFlag As Integer = 0
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            Dim obj As New clsExecute
            Dim obj1 As New clsUploadModules
            obj1.DeleteEPGData(txtChannel.Text)

            Dim strPath As String = Server.MapPath("~/Excel/")
            strPath = strPath & DateTime.Now.ToString("ddMMMyyyy_HHmmss") & FileUpload1.FileName
            FileUpload1.SaveAs(strPath)

            Dim MyConnection As System.Data.OleDb.OleDbConnection
            Dim DtSet As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data Source='" & strPath & "';Extended Properties='Excel 12.0; HDR=YES'")

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

            Dim intColumnCount As Integer = 0
            Dim dt As DataTable = DtSet.Tables(0)
            intColumnCount = dt.Columns.Count

            If intColumnCount > 1 Then

                For c As Integer = 1 To intColumnCount - 1

                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Dim vChannelID As String, vProgName As String, vDate As Date
                            Dim vTime As DateTime, vDuration As Integer
                            Dim vNextTime As DateTime

                            vChannelID = txtChannel.Text
                            vDate = Convert.ToDateTime(dt.Columns(c).ToString).Date
                            vTime = Convert.ToDateTime(dt.Rows(i)(0).ToString).ToShortTimeString
                            vProgName = dt.Rows(i)(c).ToString

                            If i < dt.Rows.Count - 1 Then
                                Try
                                    vNextTime = Convert.ToDateTime(dt.Rows(i + 1)(0).ToString).ToShortTimeString
                                    vDuration = (vNextTime.Hour * 60 + vNextTime.Minute) - (vTime.Hour * 60 + vTime.Minute)
                                Catch
                                    i = i + 1
                                End Try
                            Else
                                vDuration = 1440 - (vTime.Hour * 60 + vTime.Minute)
                            End If
                            If i = dt.Rows.Count - 1 And c = intColumnCount - 1 Then
                                InsertEPGData(vChannelID, vProgName, "", vDate, vTime, vDuration, "", "", "", True)
                                myMessageBox("File Uploaded")
                                bindAll()
                            Else
                                InsertEPGData(vChannelID, vProgName, "", vDate, vTime, vDuration, "", "", "", False)
                            End If

                        Next

                    End If
                Next
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule2", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error : " & ex.Message.ToString)
        End Try
    End Sub
    Dim strSql As String
    Private Sub InsertEPGData(ByVal vChannelName As String, ByVal vProgName As String, ByVal vGenre As String, ByVal vDate As Date, ByVal vtime As Date, ByVal vduration As Integer, ByVal VDescription As String, ByVal vEpisodeNo As String, ByVal vShowWiseDesc As String, ByVal boolInsert As Boolean)
        vProgName = Logger.RemSplCharsEng(vProgName).Trim
        VDescription = Logger.RemSplCharsEng(VDescription).Trim
        vShowWiseDesc = Logger.RemSplCharsEng(vShowWiseDesc).Trim
        strSql = strSql & "insert into map_epgexcel(channelID, [Program Name],Genre,Date,Time,Duration, Description, [Episode No],[Show-wise Description]) values('" & vChannelName.ToString.Trim & "','" & vProgName.ToString.Trim.Replace("'", "''") & "','" & vGenre.ToString.Trim & "','" & vDate & "','" & vtime & "','" & vduration & "','" & VDescription.Replace("'", "''").ToString.Trim & "','" & vEpisodeNo.ToString.Trim & "','" & vShowWiseDesc.Replace("'", "''") & "');"
        If boolInsert Then
            Dim obj As New clsExecute
            obj.executeSQL(strSql, False)
            strSql = ""
        End If

    End Sub

    Private Sub MapEPGData(ByVal vChannelName As String, vStartDate As Date, vEndDate As Date)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_insert_map_epgExcel_mod2", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelID", SqlDbType.VarChar, 100)).Value = vChannelName.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@startdate", SqlDbType.Date)).Value = vStartDate
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@enddate", SqlDbType.Date)).Value = vEndDate
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetMapFPC")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Upload Schedule2", "MapEPGData", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule2", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule2", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
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
                    Dim btn2 As Button = DirectCast(e.Row.FindControl("Btn_addnew"), Button)
                    btn1.Visible = False
                    btn2.Visible = False
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
            Logger.LogError("Upload Schedule2", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnBuildEPG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuildEPG.Click
        Try
            ''btnBuildEPG.Enabled = False

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
            Logger.LogError("Upload Schedule2", "btnBuildEPG_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdExcelData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdExcelData.DataBound
        Try

            If grdExcelData.Rows.Count > 0 And grdGenre.Rows.Count > 0 Then
                If Server.HtmlDecode(grdData.Rows(0).Cells(0).Text.Trim).Trim = "" And Server.HtmlDecode(grdGenre.Rows(0).Cells(0).Text.Trim).Trim = "" Then
                    'btnBuildEPG.Enabled = True
                    'bindEpiMissing()
                Else
                    'btnBuildEPG.Enabled = False
                End If
            Else
                'btnBuildEPG.Enabled = False
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule2", "grdExcelData_DataBound", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule2", "grdGenre_RowCommand", ex.Message.ToString, User.Identity.Name)
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
                    'btnBuildEPG.Enabled = True
                    'bindEpiMissing()
                Else
                    ddl.DataSource = SqlDSAvailableGenre
                    ddl.DataValueField = "GenreID"
                    ddl.DataTextField = "GenreName"
                    ddl.DataBind()
                    ddl.Visible = True
                    chkUpdateGenre.Visible = True
                    btnUpdateGenre.Visible = True
                    'btnBuildEPG.Enabled = False
                End If

            End If
           
        Catch ex As Exception
            Logger.LogError("Upload Schedule2", "grdGenre_RowDataBound", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule2", "btnUpdateGenre_Click", ex.Message.ToString, User.Identity.Name)
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
