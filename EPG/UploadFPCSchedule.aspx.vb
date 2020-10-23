Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadFPCSchedule
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Dim flag As Integer = 0
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

        End If
            lbUploadError.Visible = False
        Catch ex As Exception
            Logger.LogError("Uload FPC Schedule", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
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
                    '   excelReader = ExcelReaderFactory.CreateOpenXmlReader(FileUpload1.FileContent)
                    'End If
                    '--------------------------------Added on 25 Feb-----------------------------
                    ' excelReader = Nothing
                    '----------------------------------------------------------------------------
                    excelReader.IsFirstRowAsColumnNames = True
                    DeleteEPGData(ddlChannelName.SelectedValue.ToString.Trim)


                    Dim progtime As Date, progtimeNext As Date
                    Dim duration As Integer = 0
                    Dim sunProg As String, monProg As String, tueProg As String, wedProg As String, thursProg As String, friProg As String, satProg As String
                    Try
                        If excelReader.AsDataSet.Tables(0).Rows.Count - 2 > 0 Then
                            For i = 2 To excelReader.AsDataSet.Tables(0).Rows.Count - 2
                                Dim strProgtime, strNextProgTime As String
                                strProgtime = DateTime.Now.Date & " " & Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i).Item(0)).ToString)
                                progtime = Convert.ToDateTime(strProgtime)
                                Try
                                    strNextProgTime = DateTime.Now.Date & " " & Date.FromOADate(Convert.ToDouble(excelReader.AsDataSet.Tables(0).Rows(i + 1).Item(0)).ToString)
                                    progtimeNext = Convert.ToDateTime(strNextProgTime)
                                    duration = DateDiff(DateInterval.Minute, progtime, progtimeNext)
                                Catch ex As Exception
                                    progtimeNext = DateTime.Now.AddDays(1).Date & " " & "00:00:00 AM"
                                    duration = DateDiff(DateInterval.Minute, progtime, progtimeNext)
                                End Try
                                sunProg = excelReader.AsDataSet.Tables(0).Rows(i).Item(1).ToString
                                monProg = excelReader.AsDataSet.Tables(0).Rows(i).Item(2).ToString
                                tueProg = excelReader.AsDataSet.Tables(0).Rows(i).Item(3).ToString
                                wedProg = excelReader.AsDataSet.Tables(0).Rows(i).Item(4).ToString
                                thursProg = excelReader.AsDataSet.Tables(0).Rows(i).Item(5).ToString
                                friProg = excelReader.AsDataSet.Tables(0).Rows(i).Item(6).ToString
                                satProg = excelReader.AsDataSet.Tables(0).Rows(i).Item(7).ToString
                                InsertEPGData(ddlChannelName.SelectedValue.ToString.Trim, progtime, sunProg, monProg, tueProg, wedProg, thursProg, friProg, satProg, duration, txtStartDate.Text.Trim, txtEndDate.Text.Trim)
                            Next
                        End If
                    Catch ex As Exception
                        Logger.LogError("Uload FPC Schedule", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
                    End Try
                    MapEPGData(ddlChannelName.SelectedValue.ToString.Trim, txtStartDate.Text, txtEndDate.Text)
                    btnUpload.Enabled = False
                    lbUploadError.Visible = True
                    lbUploadError.Text = "The FPC has been uploaded Successfully"
                    If flag = 0 Then
                        myMessageBox("File Uploaded Successfully.")
                    Else
                        myErrorBox("File not uploaded. Please check error report")
                    End If
                    flag = 0
            End If
            End If
        Catch ex As Exception
            Logger.LogError("Uload FPC Schedule", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub DeleteEPGData(ByVal vChannelId As String)
        Try
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyConnection.Open()
            Dim strSql As New StringBuilder
            strSql.Append("Delete from map_epgExcel_mod2 where channelId='" & vChannelId.ToString.Trim & "'")
            Dim cmd As New SqlCommand(strSql.ToString, MyConnection)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            MyConnection.Close()
            MyConnection.Dispose()
        Catch ex As Exception
            Logger.LogError("Uload FPC Schedule", "DeleteEPGData", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub InsertEPGData(ByVal vChannelName As String, ByVal vProgTime As DateTime, ByVal vSunProg As String, ByVal vMonProg As String, ByVal vTueProg As String, ByVal vWedProg As String, ByVal vThursProg As String, ByVal vFriProg As String, ByVal vSatProg As String, ByVal duration As Integer, vStartDate As Date, vEndDate As Date)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_map_epgExcel_mod2", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelID", SqlDbType.VarChar, 100)).Value = vChannelName.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@progtime", SqlDbType.DateTime)).Value = vProgTime
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@sun_prog", SqlDbType.NVarChar, 1000)).Value = vSunProg.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@mon_prog", SqlDbType.NVarChar, 1000)).Value = vMonProg.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@tue_prog", SqlDbType.NVarChar, 1000)).Value = vTueProg.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@wed_prog", SqlDbType.NVarChar, 1000)).Value = vWedProg.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@thu_prog", SqlDbType.NVarChar, 1000)).Value = vThursProg.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@fri_prog", SqlDbType.NVarChar, 1000)).Value = vFriProg.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@sat_prog", SqlDbType.NVarChar, 1000)).Value = vSatProg.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@duration", SqlDbType.Int, 8)).Value = duration
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@startdate", SqlDbType.Date)).Value = vStartDate
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@enddate", SqlDbType.Date)).Value = vEndDate
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetFPC")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Uload FPC Schedule", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        End Try
       
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
            Logger.LogError("Uload FPC Schedule", "MapEPGData", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged
        Try
            lbUploadError.Visible = False
            btnUpload.Enabled = True
        Catch ex As Exception
            Logger.LogError("Uload FPC Schedule", "ddlChannelName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
End Class
