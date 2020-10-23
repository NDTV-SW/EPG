Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit
Imports Microsoft.VisualBasic.FileIO
Imports System.Globalization

Public Class UploadSchedule7_GMT
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Dim flag As Integer = 0
    Dim obj1 As New clsUploadModules
    Dim obj As New clsExecute

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            

            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
                ddlChannelName.Visible = True
                btnUpload.Enabled = True
                FileUpload1.Visible = True
            ElseIf (User.IsInRole("USER")) Then
                ddlChannelName.Visible = True
                btnUpload.Enabled = True
                FileUpload1.Visible = True
            Else
                ddlChannelName.Visible = False
                btnUpload.Enabled = False
                FileUpload1.Visible = False
                btnBuildEPG.Enabled = False
            End If
            If Page.IsPostBack = False Then

                ddlChannelName.DataSource = SqlDsChannelMaster
                ddlChannelName.DataTextField = "ChannelId"
                ddlChannelName.DataValueField = "ChannelId"
                ddlChannelName.DataBind()
                ddlChannelName.Items.Insert(0, New ListItem("Select", "0"))
            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule7GMT", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Dim strFirstDate As String = "", strLastdate As String = ""
    Dim strFirstTime As String = "", strLastTime As String = "", dtGMTDate As Date
    Dim dateFlag As Integer = 0, boolMovieChannel As Boolean = False

    Private Sub bindEpiMissing()
        'Dim dt As DataTable = obj1.ProgramSeriesEpiMissing(ddlChannelName.SelectedValue)
        'grdEpiMissing.DataSource = dt
        'grdEpiMissing.DataBind()
        'If dt.Rows.Count > 0 Then
        '    btnBuildEPG.Enabled = False
        'Else
        '    btnBuildEPG.Enabled = True
        'End If

    End Sub
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        
        Try
            
            DeleteEPGData(ddlChannelName.SelectedValue.ToString.Trim)

            Dim MyConnection As New SqlConnection(ConString)
            Dim adp As New SqlDataAdapter("Select movie_channel from mst_channel where channelid='" & ddlChannelName.SelectedValue.ToString.Trim & "'", MyConnection)
            Dim dt As New DataTable
            adp.Fill(dt)
            If dt.Rows(0)(0).ToString = "True" Or dt.Rows(0)(0).ToString = "1" Then
                boolMovieChannel = True
            End If

            'Dim fu As FileUpload = FileUpload1
            'Dim sr As StreamReader = New StreamReader(FileUpload1.FileContent)

            Dim parser As New TextFieldParser(FileUpload1.FileContent)
            parser.CommentTokens = New String() {"#"}
            parser.SetDelimiters(New String() {","})
            parser.HasFieldsEnclosedInQuotes = True
            parser.ReadLine()

            While (Not (parser.EndOfData))
                Dim value As String() = parser.ReadFields()
                If dateFlag = 0 Then
                    

                    strFirstDate = value(0).ToString
                    strFirstTime = value(1).ToString
                    Dim date1 As Date
                    Date.TryParseExact(strFirstDate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, date1)
                    strFirstDate = date1.ToString("MM/dd/yyyy")
                    dateFlag = 1
                End If

                strLastdate = value(0).ToString
                strLastTime = value(1).ToString
                Dim date2 As Date
                Date.TryParseExact(strLastdate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, date2)
                strLastdate = date2.ToString("MM/dd/yyyy")

                dtGMTDate = Convert.ToDateTime(strLastdate & " " & strLastTime).AddMinutes(330)

                If dtGMTDate.Date >= DateTime.Now.Date Then
                    Dim last_char As Char
                    Dim vProgName As String = value(2)
                    last_char = vProgName.Substring(vProgName.Length - 1)

                    Dim vChannelID As String = ddlChannelName.SelectedValue.ToString.Trim
                    Dim vGenre As String = value(10)
                    Dim vDate As String = dtGMTDate.ToString("MM/dd/yyyy")
                    Dim vTime As String = dtGMTDate.ToString("HH:mm:ss")
                    Dim vDuration As String = value(9)
                    Dim vDescription As String = Logger.RemSplCharsEng(value(15))
                    Dim vEpisodeNo As String = value(4)
                    Dim vShowWiseDesc As String = value(17)
                    Dim vActor As String = value(12)
                    Dim vDirector As String = value(13)
                    Dim vImageUrl As String = ""

                    'If Not Char.IsLetterOrDigit(last_char) Then
                    '    InsertEPGData(vChannelID, vProgName.Substring(0, vProgName.Length - 1), vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc)
                    'Else
                    '    InsertEPGData(vChannelID, vProgName, vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc)
                    'End If
                    If Not Char.IsLetterOrDigit(last_char) Then
                        If last_char = "+" Then
                            InsertEPGData(vChannelID, vProgName, vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc)
                        Else
                            InsertEPGData(vChannelID, vProgName.Substring(0, vProgName.Length - 1), vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc)
                        End If
                    Else
                        InsertEPGData(vChannelID, vProgName, vGenre, vDate, vTime, vDuration, vDescription, vEpisodeNo, vShowWiseDesc)
                    End If

                    If boolMovieChannel Then
                        InsertWOIMovie(vProgName, vDescription, vActor, vDirector, vImageUrl)
                    End If

                End If

            End While
    

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
                btnBuildEPG.Enabled = False
                lbUploadError.Visible = True
                lbUploadError.Text = "File not uploaded. Please check error report"
                myErrorBox("File not uploaded. Please check error report")
            End If
            flag = 0
        Catch ex As Exception
            Logger.LogError("Upload Schedule7GMT", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub DeleteEPGData(ByVal vChannelId As String)
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        MyConnection.Open()
        Try
            Dim adp As New SqlDataAdapter("Delete from map_EPGExcel where channelId='" & vChannelId.ToString.Trim & "'", MyConnection)
            Dim dt As New DataTable
            adp.Fill(dt)
            'clsExecute.executeSQL("Delete from map_EPGExcel where channelId='" & vChannelId.ToString.Trim & "'", False)
        Catch ex As Exception
            Logger.LogError("Upload Schedule7GMT", "DeleteEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    Private Sub InsertWOIMovie(ByVal vMovieName As String, ByVal vSynopsis As String, ByVal vActor As String, ByVal vDirector As String, ByVal vImage As String)
        Dim MyConnection As New SqlConnection(ConString)
        MyConnection.Open()
        Try
            vMovieName = Logger.RemSplCharsEng(vMovieName).Trim
            vSynopsis = Logger.RemSplCharsEng(vSynopsis).Trim

            Dim strSql As String
            strSql = "insert into mst_woi_moviesdb(moviename,synopsis,languageid,movielangid,starcast,director,TMDBImageURL,lastupdate) values('" & vMovieName.Replace("'", "''") & "','" & vSynopsis.Replace("'", "''") & "',1,1,'" & vActor.Replace("'", "''") & "','" & vDirector.Replace("'", "''") & "','" & vImage & "',dbo.GetLocalDate())"
            Dim adp As New SqlDataAdapter(strSql, MyConnection)
            Dim dt As New DataTable
            adp.Fill(dt)
            'clsExecute.executeSQL(strSql, False)
        Catch ex As Exception
            Logger.LogError("Upload Schedule7GMT", "InsertWOIMovie", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    Private Sub InsertEPGData(ByVal vChannelName As String, ByVal vProgName As String, ByVal vGenre As String, ByVal vDate As Date, ByVal vtime As Date, ByVal vduration As String, ByVal VDescription As String, ByVal vEpisodeNo As String, ByVal vShowWiseDesc As String)
        Dim MyConnection As New SqlConnection(ConString)
        MyConnection.Open()
        Try
            vProgName = Logger.RemSplCharsEng(vProgName).Trim
            VDescription = Logger.RemSplCharsEng(VDescription).Trim
            vShowWiseDesc = Logger.RemSplCharsEng(vShowWiseDesc).Trim

            Dim strSql As String
            strSql = "insert into map_EPGExcel(channelID, [Program Name],Genre,Date,Time,Duration, Description, [Episode No],[Show-wise Description]) values('" & vChannelName.ToString.Trim & "','" & vProgName.ToString.Trim.Replace("'", "''") & "','" & vGenre.ToString.Trim & "','" & vDate & "','" & vtime & "','" & vduration.ToString.Trim & "','" & VDescription.Replace("'", "''").ToString.Trim & "','" & vEpisodeNo.ToString.Trim & "','" & vShowWiseDesc.Replace("'", "''") & "')"
            obj.executeSQL(strSql, False)
        Catch ex As Exception
            Logger.LogError("Upload Schedule7GMT", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
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
                bindEpiMissing()

            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule7GMT", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Please check error report.")
        End Try

    End Sub

    Private Sub grdData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdData.Rows(index)
            Dim ddlgenre, ddlrating, ddlseries, ddlactive, ddlPrograms As ComboBox
            ddlgenre = DirectCast(grdData.Rows(index).FindControl("ddlgenre"), ComboBox)
            ddlrating = DirectCast(grdData.Rows(index).FindControl("ddlrating"), ComboBox)
            ddlseries = DirectCast(grdData.Rows(index).FindControl("ddlseries"), ComboBox)
            ddlactive = DirectCast(grdData.Rows(index).FindControl("ddlactive"), ComboBox)
            ddlPrograms = DirectCast(grdData.Rows(index).FindControl("ddlPrograms"), ComboBox)

            Dim lbChannelId As Label = DirectCast(grdData.Rows(index).FindControl("lbChannelId"), Label)
            Dim lbEpisodeNo As Label = DirectCast(grdData.Rows(index).FindControl("lbEpisodeNo"), Label)
            Dim lbStatus As Label = DirectCast(grdData.Rows(index).FindControl("lbStatus"), Label)
            Dim chkIsMovie As CheckBox = DirectCast(grdData.Rows(index).FindControl("chkIsMovie"), CheckBox)


            Dim lbExcelProgName As Label = DirectCast(grdData.Rows(index).FindControl("lbExcelProgName"), Label)

            If e.CommandName.ToLower = "addnew" Then
                obj1.Insert_mstProg(lbChannelId.Text.Trim, lbExcelProgName.Text.Trim, ddlgenre.SelectedValue.ToString.Trim, ddlrating.SelectedValue.ToString.Trim, ddlseries.SelectedValue.ToString.Trim, ddlactive.SelectedValue.ToString.Trim, IIf(lbEpisodeNo.Text.Trim = "", 0, lbEpisodeNo.Text.Trim), lbStatus.Text, chkIsMovie.Checked, "Upload Schedule7", User.Identity.Name, False)
                grdData.DataBind()
            ElseIf e.CommandName.ToLower = "updt" Then
                obj1.updateExcelProg(ddlChannelName.SelectedValue, lbExcelProgName.Text.Trim, ddlPrograms.SelectedItem.Text)
                grdData.DataBind()
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule7 GMT", e.CommandName.ToLower & " grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Please check error report.")
        End Try

    End Sub

    

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim btn As Button = DirectCast(e.Row.FindControl("Btn_AddNew"), Button)
                Dim ddlgenre As ComboBox = DirectCast(e.Row.FindControl("ddlGenre"), ComboBox)
                Dim ddlrating As ComboBox = DirectCast(e.Row.FindControl("ddlRating"), ComboBox)
                Dim ddlSeries As ComboBox = DirectCast(e.Row.FindControl("ddlSeries"), ComboBox)
                Dim ddlActive As ComboBox = DirectCast(e.Row.FindControl("ddlActive"), ComboBox)
                Dim ddlPrograms As ComboBox = DirectCast(e.Row.FindControl("ddlPrograms"), ComboBox)
                Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)
                Dim lbExcelProgName As Label = DirectCast(e.Row.FindControl("lbExcelProgName"), Label)

                If lbExcelProgName.Text.Trim = "" Then
                    btn.Visible = False
                    ddlgenre.Visible = False
                    ddlrating.Visible = False
                    ddlSeries.Visible = False
                    ddlActive.Visible = False
                    ddlPrograms.Visible = False
                    Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                    'Dim btn2 As Button = DirectCast(e.Row.FindControl("Btn_addnew"), Button)
                    btn1.Visible = False
                    'btn2.Visible = False
                    btnBuildEPG.Enabled = True
                    bindEpiMissing()
                Else
                    txtProgName.Text = lbExcelProgName.Text.Trim.Substring(0, 1)

                    sqlDSPrograms.SelectCommand = "select progid,progname from mst_program where active=1 and channelid='" & ddlChannelName.SelectedValue & "' and progname like '" & txtProgName.Text.Replace("'", "''") & "%' order by progname"
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
                    btnBuildEPG.Enabled = False
                End If
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule7 GMT", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnBuildEPG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuildEPG.Click
        Try
            btnBuildEPG.Enabled = False
            Dim obj1 As New clsUploadModules
            Dim boolBuildEPGSuccess As Boolean = obj1.BuildEPG(ddlChannelName.Text.ToString, True, chkTentative.Checked, User.Identity.Name)
            If Not boolBuildEPGSuccess Then
                myErrorBox("EPG not Built. Please check error logs!")
                Exit Sub
            End If

            '-----------------------------------Error while building EPG. Duration not matching-------------------------------
            Dim obj As New clsExecute
            Dim dt As DataTable = obj1.EPGUploadError(ddlChannelName.Text.Trim)

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
                btnBuildEPG.Enabled = True
                bindEpiMissing()
                grdBuildEPGError.Visible = True
                Exit Sub
            End If
            '-----------------------------------END-------------------Error while building EPG. Duration not matching-----------------------

            btnBuildEPG.Enabled = False
            lbEPGBuiltMessage.Visible = True

            myMessageBox("EPG Built Successfully!")

        Catch ex As Exception
            Logger.LogError("Upload Schedule7 GMT", "btnBuildEPG_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdExcelData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdExcelData.DataBound
        Try
            If grdExcelData.Rows.Count > 0 And grdGenre.Rows.Count > 0 Then
                If Server.HtmlDecode(grdData.Rows(0).Cells(0).Text.Trim).Trim = "" And Server.HtmlDecode(grdGenre.Rows(0).Cells(0).Text.Trim).Trim = "" Then
                    btnBuildEPG.Enabled = True
                    bindEpiMissing()
                Else
                    btnBuildEPG.Enabled = False
                End If
            Else
                btnBuildEPG.Enabled = False
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule7 GMT", "grdExcelData_DataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdGenre_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdGenre.RowCommand
        Try
            If e.CommandName.ToLower = "updt" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim row As GridViewRow = grdGenre.Rows(index)
                Dim strReplaceFrom As String = Server.HtmlDecode(row.Cells(0).Text)
                Dim strReplaceTo As String = DirectCast(grdGenre.Rows(index).FindControl("ddl_AvailableGenre"), ComboBox).SelectedItem.Text
                obj1.updateExcelGenre(strReplaceFrom, strReplaceTo)
                grdGenre.DataBind()

            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule7 GMT", "grdGenre_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdGenre_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGenre.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ddl As ComboBox = DirectCast(e.Row.FindControl("ddl_AvailableGenre"), ComboBox)
                Dim chkUpdateGenre As CheckBox = DirectCast(e.Row.FindControl("chkUpdateGenre"), CheckBox)
                If Server.HtmlDecode(e.Row.Cells(0).Text).Trim = "" Then
                    ddl.Visible = False
                    chkUpdateGenre.Visible = False
                    btnUpdateGenre.Visible = False
                    Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                    btn1.Visible = False
                    btnBuildEPG.Enabled = True
                    bindEpiMissing()
                Else
                    ddl.DataSource = SqlDSAvailableGenre
                    ddl.DataValueField = "GenreID"
                    ddl.DataTextField = "GenreName"
                    ddl.DataBind()
                    ddl.Visible = True
                    chkUpdateGenre.Visible = True
                    btnUpdateGenre.Visible = True
                    btnBuildEPG.Enabled = False
                End If

            End If
            If grdExcelData.Rows.Count > 0 And grdGenre.Rows.Count > 0 Then
                If Server.HtmlDecode(grdData.Rows(0).Cells(0).Text.Trim).Trim = "" And Server.HtmlDecode(grdGenre.Rows(0).Cells(0).Text.Trim).Trim = "" Then
                    btnBuildEPG.Enabled = True
                    bindEpiMissing()
                Else
                    btnBuildEPG.Enabled = False
                End If
            Else
                btnBuildEPG.Enabled = False
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule7 GMT", "grdGenre_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

   
    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged
        Try
            btnBuildEPG.Enabled = True
            bindEpiMissing()
            lbEPGBuiltMessage.Visible = False
            grdBuildEPGError.Visible = False
        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "ddlChannelName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnUpdateGenre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateGenre.Click
        Try
            Dim row As GridViewRow
            For Each row In grdGenre.Rows
                Dim chkUpdateGenre As CheckBox = DirectCast(row.FindControl("chkUpdateGenre"), CheckBox)
                If chkUpdateGenre.Checked Then
                    Dim strReplaceFrom As String = Server.HtmlDecode(row.Cells(0).Text)
                    Dim strReplaceTo As String = DirectCast(row.FindControl("ddl_AvailableGenre"), ComboBox).SelectedItem.Text
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
        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "btnUpdateGenre_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

End Class
