Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadRegionalNames2
    Inherits System.Web.UI.Page

    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim adp As New SqlDataAdapter("SELECT LanguageId, FullName FROM mst_Language where active='1' ORDER BY FullName", ConString)
                Dim ds As New DataSet
                adp.Fill(ds)
                ddlLanguage.DataSource = ds
                ddlLanguage.DataTextField = "FullName"
                ddlLanguage.DataValueField = "LanguageId"
                ddlLanguage.DataBind()
                If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER") Or User.IsInRole("USER")) Then
                    ddlChannelName.Visible = True
                    btnUpload.Visible = True
                    FileUpload1.Visible = True
                Else
                    ddlChannelName.Visible = True
                    btnUpload.Visible = False
                    FileUpload1.Visible = False
                    If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                        ddlChannelName.Visible = True
                        btnUpload.Visible = True
                        FileUpload1.Visible = True
                        checkUserType()

                    End If
                End If
                If Page.IsPostBack = False Then
                    ddlLanguage.Items.Insert(0, New ListItem("Select", "0"))
                    ddlChannelName.DataSource = SqlDsChannelMaster
                    ddlChannelName.DataTextField = "ChannelId"
                    ddlChannelName.DataValueField = "ChannelId"
                    ddlChannelName.DataBind()
                    ddlChannelName.Items.Insert(0, New ListItem("Select", "0"))
                End If
                tbInstructions.Visible = False
            End If
        Catch ex As Exception
            Logger.LogError("UploadRegionalNames", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
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
            Logger.LogError("UploadRegionalNames2", "checkUserType", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Dim errFlag As Integer = 0
    Dim uploadCount As Integer

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If ddlChannelName.SelectedIndex = 0 Or ddlLanguage.SelectedIndex = 0 Then
            myErrorBox("Please select Channel And/Or Language to upload")
            Exit Sub
        End If
        lbUploadError.Visible = False
        Dim excelReader As IExcelDataReader
        If (FileUpload1.HasFile AndAlso (IO.Path.GetExtension(FileUpload1.FileName) = ".xlsx" Or IO.Path.GetExtension(FileUpload1.FileName) = ".xls")) Then
            Try
                excelReader = ExcelReaderFactory.CreateBinaryReader(FileUpload1.FileContent)
                excelReader.IsFirstRowAsColumnNames = True
                DeleteEPGData(ddlChannelName.SelectedValue.ToString.Trim)

                If excelReader.AsDataSet.Tables(0).Rows.Count > 0 Then
                    For i = 0 To excelReader.AsDataSet.Tables(0).Rows.Count - 1
                        If Not excelReader.AsDataSet.Tables(0).Rows(i).Item(0).ToString = String.Empty Then
                            If errFlag = 1 Then
                                Exit For
                            End If
                            uploadCount = 0
                            'Dim ImChannelid As String, ImEngProgName As String, ImRegProgName As String, ImRegionalSynopsis As String, ImEpisode As String
                            'ImChannelid = excelReader.AsDataSet.Tables(0).Rows(i).Item(0).ToString
                            'ImEngProgName = excelReader.AsDataSet.Tables(0).Rows(i).Item(1).ToString
                            'ImRegProgName = excelReader.AsDataSet.Tables(0).Rows(i).Item(3).ToString
                            'ImRegionalSynopsis = excelReader.AsDataSet.Tables(0).Rows(i).Item(4).ToString
                            'ImEpisode = excelReader.AsDataSet.Tables(0).Rows(i).Item(5).ToString
                            InsertEPGData(ddlChannelName.SelectedValue.ToString.Trim, excelReader.AsDataSet.Tables(0).Rows(i).Item(0).ToString, ddlLanguage.SelectedValue.ToString.Trim, excelReader.AsDataSet.Tables(0).Rows(i).Item(2).ToString, excelReader.AsDataSet.Tables(0).Rows(i).Item(3).ToString, excelReader.AsDataSet.Tables(0).Rows(i).Item(4).ToString)
                        End If
                    Next
                End If
                myMessageBox("File upload successful.")
            Catch ex As Exception
                Logger.LogError("UploadRegionalNames", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
                lbUploadError.Visible = True
                Exit Sub
            End Try
            grdData.DataBind()
            grdExcelData.DataBind()
        End If
    End Sub

    Private Sub DeleteEPGData(ByVal vChannelId As String)
        Try
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyConnection.Open()
            Dim strSql As New StringBuilder
            strSql.Append("Delete from tmp_programregional where channelId='" & vChannelId.ToString.Trim & "' and languageid='" & ddlLanguage.SelectedValue.ToString.Trim & "'")
            Dim cmd As New SqlCommand(strSql.ToString, MyConnection)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            MyConnection.Close()
            MyConnection.Dispose()
        Catch ex As Exception
            Logger.LogError("UploadRegionalNames", "DeleteEPGData", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub InsertEPGData(ByVal vchannelid As String, ByVal vProgName As String, ByVal vLanguageId As Integer, ByVal vProgramRegionalname As String, ByVal vsynopsis As String, ByVal vEpisode As String)
        If Not (uploadCount = 0) Then
            Exit Sub
        End If
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_epg_programregional", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelID", SqlDbType.NVarChar, 50)).Value = vchannelid.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 4)).Value = Convert.ToInt32(vLanguageId.ToString.Trim)
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 200)).Value = Logger.RemSplCharsEng(vProgName.ToString.Trim)
            If vLanguageId = 1 Then
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@rProgName", SqlDbType.NVarChar, 200)).Value = Logger.RemSplCharsEng(vProgramRegionalname.Trim)
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 500)).Value = Logger.RemSplCharsEng(vsynopsis.Trim)

            Else
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@rProgName", SqlDbType.NVarChar, 200)).Value = Logger.RemSplCharsAllLangs(vProgramRegionalname.Trim, Convert.ToInt32(vLanguageId.ToString))
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 500)).Value = Logger.RemSplCharsAllLangs(vsynopsis.Trim, Convert.ToInt32(vLanguageId.ToString))
            End If
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@EpisodeNo", SqlDbType.Int)).Value = vEpisode
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetUploadRegionalNames")
            MyDataAdapter.Dispose()
            MyConnection.Close()
            uploadCount = uploadCount + 1

        Catch ex As Exception
            Logger.LogError("UploadRegionalNames2", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            errFlag = 1
        End Try
    End Sub

    Private Sub grdExcelData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdExcelData.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(2).Text = ddlLanguage.SelectedItem.Text.ToString & " " & "Name"
                e.Row.Cells(3).Text = ddlLanguage.SelectedItem.Text.ToString & " " & "Synopsis"

            End If
        Catch ex As Exception
            Logger.LogError("UploadRegionalNames2", "grdExcelData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdData.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdData.Rows(index)
            Dim ddlProgram As ComboBox = DirectCast(grdData.Rows(index).FindControl("ddlPrograms"), ComboBox)
            Dim ddlgenre As ComboBox = DirectCast(grdData.Rows(index).FindControl("ddlgenre"), ComboBox)
            Dim ddlrating As ComboBox = DirectCast(grdData.Rows(index).FindControl("ddlrating"), ComboBox)
            Dim ddlseries As ComboBox = DirectCast(grdData.Rows(index).FindControl("ddlseries"), ComboBox)
            Dim ddlactive As ComboBox = DirectCast(grdData.Rows(index).FindControl("ddlactive"), ComboBox)

            If e.CommandName.ToLower = "addnew" Then
                Insert_mstProg(Server.HtmlDecode(ddlChannelName.SelectedValue.ToString.Trim), Server.HtmlDecode(row.Cells(1).Text), ddlgenre.SelectedValue.ToString.Trim, ddlrating.SelectedValue.ToString.Trim, ddlseries.SelectedValue.ToString.Trim, ddlactive.SelectedValue.ToString.Trim)
                grdData.DataBind()
            ElseIf e.CommandName.ToLower = "updt" Then
                updateExcelProg(Server.HtmlDecode(ddlProgram.SelectedItem.ToString), Server.HtmlDecode(row.Cells(0).Text))
                grdData.DataBind()
            End If
            grdExcelData.DataBind()
        Catch ex As Exception
            Logger.LogError("UploadRegionalNames2", e.CommandName.ToLower & " - grdData_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub Insert_mstProg(ByVal vChannelId As String, ByVal vProgName As String, ByVal vGenreId As String, ByVal vRatingId As String, ByVal vSeriesEnabled As String, ByVal vActive As String)
        Try
            Try
                Dim DS As DataSet
                Dim MyDataAdapter As SqlDataAdapter
                Dim MyConnection As New SqlConnection
                MyConnection = New SqlConnection(ConString)
                MyDataAdapter = New SqlDataAdapter("sp_mst_program", MyConnection)
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = 0
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelId", SqlDbType.NVarChar, 50)).Value = vChannelId.Trim
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = Logger.RemSplCharsEng(vProgName.ToString.Trim)
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreID", SqlDbType.Int, 8)).Value = vGenreId.ToString.Trim
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SubGenreID", SqlDbType.Int, 8)).Value = 0
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingID", SqlDbType.VarChar, 10)).Value = vRatingId.ToString.Trim
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Seriesenabled", SqlDbType.Bit)).Value = IIf(vSeriesEnabled.ToString.Trim = "1", True, False)
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@active", SqlDbType.Bit)).Value = IIf(vActive.ToString.Trim = "1", True, False)
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "A"
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
                DS = New DataSet()
                MyDataAdapter.Fill(DS, "GetCompany")
                MyDataAdapter.Dispose()
                MyConnection.Close()
                MyConnection.Open()
                Dim progid As String, strDesc As String
                progid = "0"
                strDesc = ""
                Dim cmd As New SqlCommand("select a.ProgId,b.Description  from mst_program a join map_EPGExcel b on a.ProgName =b.[Program Name] where a.progname='" & Replace(vProgName.ToString, "'", "''") & "' and a.channelid='" & vChannelId.ToString.Trim & "'", MyConnection)
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    progid = dr("ProgId").ToString.Trim
                    strDesc = dr("Description").ToString.Trim
                End If
                cmd.Dispose()

                dr.Close()

                MyConnection.Close()
                MyConnection.Dispose()
                MyConnection = New SqlConnection(ConString)
                Dim DSReg As DataSet
                Dim MyDataAdapterReg As SqlDataAdapter
                MyDataAdapterReg = New SqlDataAdapter("sp_mst_ProgramRegional", MyConnection)
                MyDataAdapterReg.SelectCommand.CommandType = CommandType.StoredProcedure
                MyDataAdapterReg.SelectCommand.Parameters.Add(New SqlParameter("@Rowid", SqlDbType.Int)).Value = 0
                MyDataAdapterReg.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = progid.ToString.Trim
                MyDataAdapterReg.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = Logger.RemSplCharsEng(vProgName.ToString.Trim)
                MyDataAdapterReg.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = Logger.RemSplCharsEng(strDesc)
                MyDataAdapterReg.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = 1
                MyDataAdapterReg.SelectCommand.Parameters.Add(New SqlParameter("@EpisodeNo", SqlDbType.Int)).Value = 0
                MyDataAdapterReg.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "A"
                MyDataAdapterReg.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name

                DSReg = New DataSet()
                MyDataAdapterReg.Fill(DS, "GetRegionalProgName")
                MyDataAdapterReg.Dispose()
                MyConnection.Close()
            Catch ex As Exception
                Logger.LogError("Upload Schedule", "A Insert_mstProg", ex.Message.ToString, User.Identity.Name)
            End Try


        Catch ex As Exception
            Logger.LogError("UploadRegionalNames2", "Insert_mstProg", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub updateExcelProg(ByVal vProgName As String, ByVal vRowId As String)
        Try
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyConnection.Open()
            Dim strSql As New StringBuilder
            strSql.Append("update tmp_programregional set progname='" & vProgName.ToString.Trim.Replace("'", "''") & "' where rowid='" & vRowId.Trim & "'")
            Dim cmd As New SqlCommand(strSql.ToString, MyConnection)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            MyConnection.Close()
            MyConnection.Dispose()
        Catch ex As Exception
            Logger.LogError("UploadRegionalNames2", "updateExcelProg", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ddlrating As ComboBox = DirectCast(e.Row.FindControl("ddlRating"), ComboBox)
                Dim ddlPrograms As ComboBox = DirectCast(e.Row.FindControl("ddlPrograms"), ComboBox)
                Dim ddlGenre As ComboBox = DirectCast(e.Row.FindControl("ddlGenre"), ComboBox)
                ddlGenre.SelectedValue = ddlGenre1.SelectedValue
                txtProgName.Text = Server.HtmlDecode(e.Row.Cells(1).Text).Trim.Substring(0, 2)
                sqlDSPrograms.SelectCommand = "select progid,progname from mst_program where active=1 and channelid='" & ddlChannelName.SelectedValue & "' and progname like '" & txtProgName.Text.Replace("'", "''") & "%' order by progname"
                ddlPrograms.DataSource = sqlDSPrograms
                ddlPrograms.DataTextField = "progname"
                ddlPrograms.DataValueField = "progid"
                ddlPrograms.DataBind()
                If Server.HtmlDecode(e.Row.Cells(0).Text).Trim = "" Then
                    Dim btn As Button = DirectCast(e.Row.FindControl("Btn_AddNew"), Button)
                    btn.Visible = False
                    Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                    btn1.Visible = False
                    tbInstructions.Visible = False
                Else
                    Dim btn As Button = DirectCast(e.Row.FindControl("Btn_AddNew"), Button)
                    btn.Visible = True
                    tbInstructions.Visible = True
                    If Server.HtmlDecode(e.Row.Cells(3).Text).Trim.ToUpper = "INSERT" Then
                        Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                        btn1.Visible = False
                    Else
                        Dim btn1 As Button = DirectCast(e.Row.FindControl("Btn_Updt"), Button)
                        btn1.Visible = True
                    End If
                End If
                ddlrating.SelectedValue = "U"
            End If
        Catch ex As Exception
            Logger.LogError("UploadRegionalNames2", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub ddlGenre1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlGenre1.SelectedIndexChanged
        Try
            grdData.DataBind()
        Catch ex As Exception
            Logger.LogError("UploadRegionalNames2", "ddlGenre1_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcel.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=" & ddlChannelName.SelectedValue.ToString.Trim & "_RegionalNames_" & ddlLanguage.SelectedItem.Text.Trim & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"
        
        Dim sw As System.IO.StringWriter = New System.IO.StringWriter
        Dim hw As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(sw)

        grdExcelData.RenderControl(hw)

        Response.Write(sw.ToString)
        Response.End()
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

End Class
