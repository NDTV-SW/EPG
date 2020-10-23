Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadSchedule3
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
            Dim mu As MembershipUser = Membership.GetUser(User.Identity.Name)
            Membership.UpdateUser(mu)
            If mu.Comment = "Need Change Password1" Then
                'Response.Redirect("~/Account/ChangePassword.aspx")
                mu.Comment = "0"
            End If
            Dim pwDateExpire As Integer
            pwDateExpire = DateDiff(DateInterval.Day, mu.LastPasswordChangedDate, Date.Now)
            If pwDateExpire > 30 Then
                'Response.Redirect("~/Account/ChangePassword.aspx")
            End If
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
            Logger.LogError("Upload Schedule3", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Dim strFirstDate As String = "", strLastdate As String = ""
    Dim dateFlag As Integer = 0, errFlag As Integer = 0

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If ddlChannelName.SelectedIndex = 0 Then
            myErrorBox("Please select Channel first !")
            Exit Sub
        End If
        Try

            If FileUpload1.FileName = Nothing Then
                'lbUploadError.Visible = True
                Exit Sub
            Else
                'lbUploadError.Visible = False
                Dim excelReader As IExcelDataReader
                If (FileUpload1.HasFile AndAlso (IO.Path.GetExtension(FileUpload1.FileName) = ".xlsx" Or IO.Path.GetExtension(FileUpload1.FileName) = ".xls")) Then
                    'If IO.Path.GetExtension(FileUpload1.FileName) = ".xls" Then
                    excelReader = ExcelReaderFactory.CreateBinaryReader(FileUpload1.FileContent)
                    'ElseIf IO.Path.GetExtension(FileUpload1.FileName) = ".xlsx" Then
                    '    excelReader = ExcelReaderFactory.CreateOpenXmlReader(FileUpload1.FileContent)
                    'End If
                    '--------------------------------Added on 25 Feb-----------------------------
                    ' excelReader = Nothing
                    '----------------------------------------------------------------------------
                    excelReader.IsFirstRowAsColumnNames = True
                    DeleteEPGData(ddlChannelName.SelectedValue.ToString.Trim)

                    Dim progtime As Date, progtimeNext As Date
                    '                    Dim progDate As Date, progNextDate As Date
                    Dim duration As Integer = 0
                    ' Dim a As Date
                    If excelReader.AsDataSet.Tables(0).Rows.Count > 0 Then
                        For i = 0 To excelReader.AsDataSet.Tables(0).Rows.Count - 1
                            If IsDBNull(excelReader.AsDataSet.Tables(0).Rows(i).Item(0)) Then
                                Exit For
                            End If
                            'If dateFlag = 0 Then
                            '    strFirstDate = (Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(0)).ToString)).ToString
                            '    strFirstDate = Logger.GetFormatedDate(strFirstDate)
                            '    dateFlag = 1
                            'End If
                            'strLastdate = (Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(0)).ToString)).ToString
                            'strLastdate = Logger.GetFormatedDate(strLastdate)

                            Dim strProgtime, strNextProgTime As String
                            strProgtime = DateTime.Now.Date & " " & Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(1)).ToString)
                            progtime = Convert.ToDateTime(strProgtime)
                            Try
                                strNextProgTime = DateTime.Now.Date & " " & Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i + 1).Item(1)).ToString)
                                progtimeNext = Convert.ToDateTime(strNextProgTime)
                                duration = DateDiff(DateInterval.Minute, progtime, progtimeNext)
                            Catch ex As Exception
                                progtimeNext = DateTime.Now.AddDays(1).Date & " " & "00:00:00 AM"
                                duration = DateDiff(DateInterval.Minute, progtime, progtimeNext)

                            End Try
                            If duration < 0 Then
                                duration = 1440 + duration
                            End If
                            Dim progDescription As String


                            If IsDBNull(excelReader.AsDataSet.Tables(0).Rows(i).Item(3)) Then
                                progDescription = ""
                            Else
                                progDescription = excelReader.AsDataSet.Tables(0).Rows(i).Item(3)
                            End If
                            If Not IsDBNull(excelReader.AsDataSet.Tables(0).Rows(i).Item(2)) Then
                                'InsertEPGData(ddlChannelName.SelectedValue.ToString.Trim, Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(0)).ToString), Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(1)).ToString), excelReader.AsDataSet.Tables(0).Rows(i).Item(2), progDescription, duration)
                                InsertEPGData(ddlChannelName.SelectedValue.ToString.Trim, excelReader.AsDataSet.Tables(0).Rows(i).Item(0).ToString, Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(1)).ToString).ToShortTimeString, excelReader.AsDataSet.Tables(0).Rows(i).Item(2), progDescription, duration)
                            End If

                        Next
                    End If
                    grdData.DataBind()
                    grdGenre.DataBind()
                    grdExcelData.DataBind()
                    btnUpload.Enabled = False
                End If

            End If
            Session("strFirstDate") = strFirstDate.Trim
            Session("strLastdate") = strLastdate.Trim

            If (flag = 0) Then
                myMessageBox("File Uploaded Successfully!")

            End If
            flag = 0

        Catch ex As Exception
            Logger.LogError("Upload Schedule3", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub DeleteEPGData(ByVal vChannelId As String)
        Try
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyConnection.Open()
            Dim strSql As New StringBuilder
            strSql.Append("Delete from map_EPGExcel where channelId='" & vChannelId.ToString.Trim & "'")
            Dim cmd As New SqlCommand(strSql.ToString, MyConnection)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            MyConnection.Close()
            MyConnection.Dispose()
        Catch ex As Exception
            Logger.LogError("Upload Schedule3", "DeleteEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        End Try
    End Sub

    Private Sub InsertEPGData(ByVal vChannelName As String, ByVal vProgDate As String, ByVal vProgTime As String, ByVal vProgName As String, ByVal vDescription As String, ByVal vDuration As String)
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)

        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            MyDataAdapter = New SqlDataAdapter("sp_map_epgExcel", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelID", SqlDbType.NVarChar, 100)).Value = vChannelName.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 500)).Value = Logger.RemSplCharsEng(vProgName)
            'MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgDate", SqlDbType.DateTime)).Value = vProgDate
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgDate", SqlDbType.VarChar, 50)).Value = vProgDate.ToString
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Description", SqlDbType.NVarChar, 1000)).Value = Logger.RemSplCharsEng(vDescription.Trim)
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgTime", SqlDbType.VarChar, 50)).Value = vProgTime
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@duration", SqlDbType.Int, 8)).Value = vDuration
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@EpisodeNo", SqlDbType.VarChar)).Value = "0"
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetFPC")
            MyDataAdapter.Dispose()

        Catch ex As Exception
            Logger.LogError("Upload Schedule3", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Dispose()
            MyConnection.Close()
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
                obj1.Insert_mstProg(lbChannelId.Text.Trim, lbExcelProgName.Text.Trim, ddlgenre.SelectedValue.ToString.Trim, ddlrating.SelectedValue.ToString.Trim, ddlseries.SelectedValue.ToString.Trim, ddlactive.SelectedValue.ToString.Trim, IIf(lbEpisodeNo.Text.Trim = "", 0, lbEpisodeNo.Text.Trim), lbStatus.Text, chkIsMovie.Checked, "Upload Schedule3", User.Identity.Name)
                grdData.DataBind()
            ElseIf e.CommandName.ToLower = "updt" Then
                obj1.updateExcelProg(ddlChannelName.SelectedValue, lbExcelProgName.Text.Trim, ddlPrograms.SelectedItem.Text)
                grdData.DataBind()
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
                    Dim btn2 As Button = DirectCast(e.Row.FindControl("Btn_addnew"), Button)
                    btn1.Visible = False
                    btn2.Visible = False
                    btnBuildEPG.Enabled = True
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
            Logger.LogError("Upload Schedule3", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
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
                grdBuildEPGError.Visible = True
                Exit Sub
            End If
            '-----------------------------------END-------------------Error while building EPG. Duration not matching-----------------------

            btnBuildEPG.Enabled = False
            lbEPGBuiltMessage.Visible = True

            myMessageBox("EPG Built Successfully!")

        Catch ex As Exception
            Logger.LogError("Upload Schedule3", "btnBuildEPG_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error in Build EPG. Please check error report.")
        End Try
    End Sub

    Private Sub grdExcelData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdExcelData.DataBound
        Try
            If grdExcelData.Rows.Count > 0 And grdGenre.Rows.Count > 0 Then
                If Server.HtmlDecode(grdData.Rows(0).Cells(0).Text.Trim).Trim = "" And Server.HtmlDecode(grdGenre.Rows(0).Cells(0).Text.Trim).Trim = "" Then
                    btnBuildEPG.Enabled = True
                Else
                    btnBuildEPG.Enabled = False
                End If
            Else
                btnBuildEPG.Enabled = False
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule3", "grdExcelData_DataBound", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Upload Schedule3", e.CommandName.ToLower & " grdGenre_RowCommand", ex.Message.ToString, User.Identity.Name)
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
                Else
                    btnBuildEPG.Enabled = False
                End If
            Else
                btnBuildEPG.Enabled = False
            End If
        Catch ex As Exception
            Logger.LogError("Upload Schedule3", "grdGenre_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged
        btnBuildEPG.Enabled = True
      
        lbEPGBuiltMessage.Visible = False
        grdBuildEPGError.Visible = False

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
            Logger.LogError("Upload Schedule3", "btnUpdateGenre_Click", ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

End Class
