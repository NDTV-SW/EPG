Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Data.OleDb

Public Class uploadExcel
    Inherits System.Web.UI.Page

    Dim myConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Dim loggedInUser As String = ""
    Dim _arrCount As New ArrayList()
    Dim _PrevDept As String = ""

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & messagestr.Replace("'", "") & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            bindMismatchGrid()
        End If
        If HttpContext.Current.User.IsInRole("LOGGER") Then
            GridView1.Columns(6).Visible = False
            GridView1.Columns(7).Visible = False
            btnUpdateMultiple.Visible = False
        End If
    End Sub

    Private Sub clearAll()

    End Sub

    Dim intSno As Integer


    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            Dim obj As New clsExecute
            'obj.executeSQL("delete from map_error_details_BIGTV where 1=1", False)
            If fupUpload.HasFile Then
                Dim FileName As String = Path.GetFileName(fupUpload.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(fupUpload.PostedFile.FileName)
                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")

                Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                fupUpload.SaveAs(FilePath)
                Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text)
                myMessageBox("File Uploaded Successfully")
                Dim strbody As String = "Please check the below URL to Map the errors uploaded.<br/> http://epgops.ndtv.com/reports/uploadExcel.aspx<br/><br/>"
                strbody = strbody & "Also resolve the errors from the below URL<br/>http://epgops.ndtv.com/reports/relianceErrorLogging.aspx"
                Logger.mailMessage("epg@ndtv.com", "New errors logged by Reliance team", strbody, "epg@ndtv.com", "")
            End If
        Catch ex As Exception
            myMessageBox("Error while file upload : " & ex.Message.ToString)
        End Try
    End Sub


    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbChannelMissing As Label = TryCast(e.Row.FindControl("lbChannelMissing"), Label)
            Dim lbEPGProgram As Label = TryCast(e.Row.FindControl("lbEPGProgram"), Label)
            Dim lbInvalidErrorType As Label = TryCast(e.Row.FindControl("lbInvalidErrorType"), Label)

            Dim ddlChannels As DropDownList = DirectCast(e.Row.FindControl("ddlChannels"), DropDownList)
            Dim ddlProgramRDTV As DropDownList = DirectCast(e.Row.FindControl("ddlProgramRDTV"), DropDownList)
            'Dim ddlProgramActual As DropDownList = DirectCast(e.Row.FindControl("ddlProgramActual"), DropDownList)
            Dim ddlErrorTypes As DropDownList = DirectCast(e.Row.FindControl("ddlErrorTypes"), DropDownList)
           

            Dim dt As DataTable
            Dim obj As New clsExecute

            If lbChannelMissing.Text = "Y" Then
                ddlChannels.Visible = True
            Else
                ddlChannels.Visible = False
            End If

            If lbEPGProgram.Text = "Y" And lbChannelMissing.Text = "N" Then
                Dim lbChannelName As Label = TryCast(e.Row.FindControl("lbChannelName"), Label)
                dt = obj.executeSQL("select progid,progname from mst_program where channelid='" & lbChannelName.Text & "' order by 2", False)
                If lbEPGProgram.Text = "Y" Then
                    ddlProgramRDTV.DataTextField = "Progname"
                    ddlProgramRDTV.DataValueField = "Progname"
                    'ddlProgramRDTV.DataValueField = "ProgId"
                    ddlProgramRDTV.DataSource = dt
                    ddlProgramRDTV.DataBind()
                    ddlProgramRDTV.Visible = True
                Else
                    ddlProgramRDTV.Visible = False
                End If
            End If

            If lbInvalidErrorType.Text = "Y" Then
                ddlErrorTypes.Visible = True
            Else
                ddlErrorTypes.Visible = False
            End If
            If HttpContext.Current.User.IsInRole("LOGGER") Then
                ddlChannels.Visible = False
                ddlProgramRDTV.Visible = False
            End If
        End If
    End Sub
    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            If e.CommandName.ToLower = "updatemismatch" Then
                Dim strIsChannelMissing As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbChannelMissing"), Label).Text
                Dim strChannelName As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbChannelName"), Label).Text

                Dim strIsDisplayedOnRDTV As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbEPGProgram"), Label).Text
                Dim strDisplayedOnRDTV As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbDisplayedOnRDTV"), Label).Text.Replace("'", "''")

                Dim strIsInvalidError As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbInvalidErrorType"), Label).Text
                Dim strErrorType As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbErrorType"), Label).Text

                Dim strddlChannels As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("ddlChannels"), DropDownList).SelectedValue.Replace("'", "''")
                Dim strddlProgramRDTV As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("ddlProgramRDTV"), DropDownList).SelectedValue.Replace("'", "''")
                Dim strddlErrorTypes As String = DirectCast(GridView1.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("ddlErrorTypes"), DropDownList).SelectedValue.Replace("'", "''")


                Dim obj As New clsExecute
                If strIsChannelMissing = "Y" Then
                    obj.executeSQL("update map_error_details_BIGTV set channelname='" & strddlChannels & "' where channelName='" & strChannelName & "'", False)
                End If
                If strIsDisplayedOnRDTV = "Y" And strIsChannelMissing = "N" Then
                    obj.executeSQL("update map_error_details_BIGTV set displayedonrdtv='" & strddlProgramRDTV & "' where displayedonrdtv='" & strDisplayedOnRDTV & "' and channelname='" & strChannelName & "'", False)
                End If
                If strIsInvalidError = "Y" Then
                    obj.executeSQL("update map_error_details_BIGTV set errortype='" & strddlErrorTypes & "' where errortype='" & strErrorType & "' and channelname='" & strChannelName & "'", False)
                End If

                'GridView1.PageIndex = 0
                bindMismatchGrid()
            End If
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", e.CommandName.ToLower & " grdSynopsis_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnVerifyMultiple_Click(sender As Object, e As EventArgs) Handles btnUpdateMultiple.Click
        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim chkUpdate As CheckBox = TryCast(GridView1.Rows(i).FindControl("chkUpdate"), CheckBox)


            If chkUpdate.Checked = True Then
                Dim strIsChannelMissing As String = DirectCast(GridView1.Rows(i).FindControl("lbChannelMissing"), Label).Text
                Dim strChannelName As String = DirectCast(GridView1.Rows(i).FindControl("lbChannelName"), Label).Text

                Dim strIsDisplayedOnRDTV As String = DirectCast(GridView1.Rows(i).FindControl("lbEPGProgram"), Label).Text
                Dim strDisplayedOnRDTV As String = DirectCast(GridView1.Rows(i).FindControl("lbDisplayedOnRDTV"), Label).Text.Replace("'", "''")

                Dim strIsInvalidError As String = DirectCast(GridView1.Rows(i).FindControl("lbInvalidErrorType"), Label).Text
                Dim strErrorType As String = DirectCast(GridView1.Rows(i).FindControl("lbErrorType"), Label).Text


                Dim strddlChannels As String = DirectCast(GridView1.Rows(i).FindControl("ddlChannels"), DropDownList).SelectedValue.Replace("'", "''")
                Dim strddlProgramRDTV As String = DirectCast(GridView1.Rows(i).FindControl("ddlProgramRDTV"), DropDownList).SelectedValue.Replace("'", "''")
                Dim strddlErrorTypes As String = DirectCast(GridView1.Rows(i).FindControl("ddlErrorTypes"), DropDownList).SelectedValue.Replace("'", "''")

                Dim obj As New clsExecute
                If strIsChannelMissing = "Y" Then
                    obj.executeSQL("update map_error_details_BIGTV set channelname='" & strddlChannels & "' where channelName='" & strChannelName & "'", False)
                End If
                If strIsDisplayedOnRDTV = "Y" And strIsChannelMissing = "N" Then
                    obj.executeSQL("update map_error_details_BIGTV set displayedonrdtv='" & strddlProgramRDTV & "' where displayedonrdtv='" & strDisplayedOnRDTV & "' and channelname='" & strChannelName & "'", False)
                End If
                If strIsInvalidError = "Y" Then
                    obj.executeSQL("update map_error_details_BIGTV set errortype='" & strddlErrorTypes & "' where errortype='" & strErrorType & "' and channelname='" & strChannelName & "'", False)
                End If

                chkUpdate.Checked = False
            End If

        Next i
        bindMismatchGrid()
    End Sub

    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)

        Dim conStr As String = ""
        Select Case Extension
            Case ".xls"
                'Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings("Excel03ConString") _
                           .ConnectionString
                Exit Select
            Case ".xlsx"
                'Excel 07
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                          .ConnectionString
                Exit Select
        End Select
        conStr = String.Format(conStr, FilePath, isHDR)

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim intSheetCount As Integer = dtExcelSchema.Rows.Count
        ' For k As Integer = 0 To intSheetCount - 1

        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()
        Dim strErrorLines As String = ""
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Try
                    Dim provider As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture
                    Dim StartSlot As String, EndSlot As String, ChannelName As String, actualRunning As String, displayedonRDTV As String, errortype As String, dtErrorDate As Date
                    Try
                        dtErrorDate = Date.ParseExact(dt.Rows(i)(1).ToString, "d-M-yyyy hh:mm:ss tt", provider)
                    Catch ex As Exception
                        dtErrorDate = Date.ParseExact(dt.Rows(i)(1).ToString, "d-M-yyyy", provider)
                    End Try
                    ChannelName = dt.Rows(i)(3).ToString
                    actualRunning = dt.Rows(i)(4).ToString
                    displayedonRDTV = dt.Rows(i)(5).ToString
                    'Try
                    '    errortype = dt.Rows(i)(7).ToString
                    'Catch
                    errortype = "Wrong Programme"
                    'End Try
                    StartSlot = (dt.Rows(i)(6).ToString.Split("-")(0).ToString.Substring(0, 2) & ":" & dt.Rows(i)(6).ToString.Split("-")(0).ToString.Substring(2, 2)).Replace("24:", "00:")
                    EndSlot = (dt.Rows(i)(6).ToString.Split("-")(1).ToString.Substring(0, 2) & ":" & dt.Rows(i)(6).ToString.Split("-")(1).ToString.Substring(2, 2)).Replace("24:", "00:")
                    If i = dt.Rows.Count - 1 Then
                        InsertInTable(dtErrorDate, dt.Rows(i)(2).ToString.Replace(".", ":"), ChannelName, actualRunning, displayedonRDTV, StartSlot, EndSlot, errortype, True)
                    Else
                        InsertInTable(dtErrorDate, dt.Rows(i)(2).ToString.Replace(".", ":"), ChannelName, actualRunning, displayedonRDTV, StartSlot, EndSlot, errortype, False)
                    End If
                Catch ex As Exception
                    If strErrorLines = "" Then
                        strErrorLines = "Error in lines : " & i & vbCrLf
                    Else
                        strErrorLines += ", " & i & vbCrLf
                    End If
                End Try
            Next
        End If
        ' Next k
        bindMismatchGrid()
        If strErrorLines = "" Then
            myMessageBox("File Uploaded Successfully")
        Else
            myMessageBox("Error in following lines. You may insert them separately from logging page." & vbCrLf & strErrorLines)
        End If

        'Bind Data to GridView

    End Sub
    Private Sub bindMismatchGrid()
        Dim obj As New clsExecute
        Dim dt1 As DataTable = obj.executeSQL("map_error_details_BIGTV_mismatches", True)

        'GridView1.Caption = "Mismatch in Excel"
        GridView1.DataSource = dt1
        GridView1.DataBind()
    End Sub
    Dim sqlToExecute As String = ""
    Private Sub InsertInTable(ByVal ErrorDate As DateTime, ByVal Errortime As DateTime, ByVal channelName As String, ByVal actualRunningProgram As String, ByVal displayedOnRDTV As String, ByVal startTimeSlot As DateTime, ByVal EndTimeSlot As DateTime, ByVal Errortype As String, ByVal boolInsert As Boolean)
        sqlToExecute = sqlToExecute & "insert into map_error_details_BIGTV(errordatetime,channelname,actualrunningProgram,displayedOnRDTV,timeslotstart,timeslotend,errortype) values('" & _
                            ErrorDate.ToString("MM/dd/yyyy") & " " & Errortime.ToString("HH:mm:ss") & "','" & channelName.Replace("'", "''") & "','" & actualRunningProgram.Replace("'", "''") & "','" & displayedOnRDTV.Replace("'", "''") & "','" & startTimeSlot & "','" & EndTimeSlot & "','" & Errortype.Replace("'", "''") & "'); "
        If boolInsert Then
            Dim obj As New clsExecute
            obj.executeSQL(sqlToExecute, False)
            sqlToExecute = ""
        End If

    End Sub

    Protected Sub PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        bindMismatchGrid()
    End Sub

End Class