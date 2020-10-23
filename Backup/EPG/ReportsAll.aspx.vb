Imports System
Imports System.Data.SqlClient
Public Class ReportsAll
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If Not (User.Identity.Name.ToLower = "hemant" Or User.Identity.Name.ToLower = "kautilyar" Or User.Identity.Name.ToLower = "shweta") Then
                TabContainer1.Tabs(6).Visible = False
                TabContainer1.DataBind()
            End If

            '---------Build EPG Transactions TAB Page Load Starts-------------
            ddlChannelId.DataSource = SqlDsChannelMaster
            ddlChannelId.DataTextField = "ChannelId"
            ddlChannelId.DataValueField = "ChannelId"
            ddlChannelId.DataBind()
            ddlChannelId.Items.Insert(0, New ListItem("All channels updated today", "0"))
            ddlChannelId.SelectedIndex = 0
            gridbind()
            '---------Build EPG Transactions TAB Page Load Ends-------------

            '---------Error report Starts-------------
            grdErrorReportBind()
            '---------Error report Ends-------------

            '---------Synopsis Modified report Starts-----------------------
            ddlChannelModSynopsis.DataBind()
            ddlChannelModSynopsis.Items.Insert(0, New ListItem("All Channels", "0"))
            '---------Synopsis Modified report Ends-----------------------
        End If
    End Sub

#Region "Build EPG Transactions TAB"
    Private Sub gridbind()
        Dim sql As String
        If ddlChannelId.SelectedIndex = 0 Then
            sql = "select channelid,CONVERT(varchar,epgdate,106) epgdate1,epgdate,CONVERT(varchar,lastupdate,109) lastupdate1,lastupdate from mst_build_epg_transactions where(Convert(varchar, lastupdate, 112) = Convert(varchar, dbo.GetLocalDate(), 112)) order by channelid asc, epgdate desc"
        Else
            sql = "select top 50 channelid,CONVERT(varchar,epgdate,106) epgdate1,epgdate,CONVERT(varchar,lastupdate,109) lastupdate1,lastupdate from mst_build_epg_transactions where channelid='" & ddlChannelId.SelectedValue.Trim & "' order by lastupdate desc ,epgdate desc"
        End If
        Dim adp As New SqlDataAdapter(sql, ConString)
        Dim ds As New DataSet
        adp.Fill(ds)
        grdEPGTransactions.DataSource = ds
        grdEPGTransactions.DataBind()
    End Sub
    Protected Sub ddlChannelId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelId.SelectedIndexChanged
        gridbind()
    End Sub
#End Region

#Region "XML Mailed"
    Dim Sno As Integer = 1
    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSno"), Label)
                Dim lbName As Label = DirectCast(e.Row.FindControl("lbXMLFileName"), Label)
                Dim lbFTPDateTime As Label = DirectCast(e.Row.FindControl("lbFTPDateTime"), Label)
                Dim lbmaxdateSend As Label = DirectCast(e.Row.FindControl("lbmaxdateSend"), Label)
                Dim lbRowId As Label = DirectCast(e.Row.FindControl("lbRowId"), Label)
                Dim lbXMLDateTime As Label = DirectCast(e.Row.FindControl("lbXMLDateTime"), Label)
                Dim lbXMLFileName As Label = DirectCast(e.Row.FindControl("lbXMLFileName"), Label)
                Dim lbStartDate As Label = DirectCast(e.Row.FindControl("lbStartDate"), Label)
                Dim lbEndDate As Label = DirectCast(e.Row.FindControl("lbEndDate"), Label)
                Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)

                Dim hyXMLMailedOn As HyperLink = DirectCast(e.Row.FindControl("hyXMLMailedOn"), HyperLink)
                Dim hyXMLFileName As HyperLink = DirectCast(e.Row.FindControl("hyXMLFileName"), HyperLink)
                Dim hyChannelID As HyperLink = DirectCast(e.Row.FindControl("hyChannelID"), HyperLink)

                hyChannelID.NavigateUrl = "Javascript:openXmlMailed('" & lbChannelId.Text & "')"
                hyXMLMailedOn.NavigateUrl = "Javascript:openWin('" & lbRowId.Text & "')"
                hyXMLFileName.NavigateUrl = "~/xml/" & lbXMLFileName.Text

                Dim strName As String, strLength As String, strStartDate As String, strEndDate As String
                Dim strSMonth As String, strSDate As String, strSYear As String
                Dim strEMonth As String, strEDate As String, strEYear As String
                lbXMLDateTime.Text = Convert.ToDateTime(lbXMLDateTime.Text).ToString("dd-MMM-yyyy hh:mm:ss tt")
                lbmaxdateSend.Text = Convert.ToDateTime(lbmaxdateSend.Text).ToString("dd-MMM-yyyy")

                'If Convert.ToDateTime(lbmaxdateSend.Text).ToString("dd-MMM-yyyy") < DateAdd(DateInterval.Day, 4, Now()).ToString("dd-MMM-yyyy") Then
                'lbmaxdateSend.ForeColor = Drawing.Color.Red
                'End If

                If Convert.ToDateTime(lbmaxdateSend.Text).ToString("yyyyMMdd") < DateAdd(DateInterval.Day, 4, Now()).ToString("yyyyMMdd") Then
                    lbmaxdateSend.ForeColor = Drawing.Color.Red
                End If

                Try
                    lbFTPDateTime.Text = Convert.ToDateTime(lbFTPDateTime.Text).ToString("dd-MMM-yyyy hh:mm:ss tt")
                Catch
                End Try

                strName = lbName.Text
                strLength = lbName.Text.ToString.Length.ToString

                strStartDate = lbName.Text.Substring(lbName.Text.ToString.Length - 16, 6)
                strEndDate = lbName.Text.Substring(lbName.Text.ToString.Length - 10, 6)

                strSYear = strStartDate.Substring(0, 2)
                strSMonth = strStartDate.Substring(2, 2)
                strSDate = strStartDate.Substring(4, 2)

                strEYear = strEndDate.Substring(0, 2)
                strEMonth = strEndDate.Substring(2, 2)
                strEDate = strEndDate.Substring(4, 2)

                lbStartDate.Text = Convert.ToDateTime(strSMonth & "/" & strSDate & "/" & strSYear).ToString("dd-MMM-yyyy")
                lbEndDate.Text = Convert.ToDateTime(strEMonth & "/" & strEDate & "/" & strEYear).ToString("dd-MMM-yyyy")

                lbSno.Text = Sno.ToString
                Sno = Sno + 1

            End If
        Catch ex As Exception
            Logger.LogError("XML Mailed on", "GridView1_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

#End Region

#Region "Error Report"
    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnView.Click
        grdErrorReportBind()
    End Sub
    Private Sub grdErrorReportBind()
        Dim sql As String = ""
        If txtErrorDate.Text.Trim = "" Then
            sql = "select top 500 ErrorPage,ErrorSource,ErrorType,ErrorMessage,LoggedinUser,ErrorDateTime,convert (varchar,ErrorDateTime,100) ErrorDateTime1 from app_error_logs order by ErrorDateTime desc"
        Else
            sql = "select ErrorPage,ErrorSource,ErrorType,ErrorMessage,LoggedinUser,ErrorDateTime,convert (varchar,ErrorDateTime,100) ErrorDateTime1 from app_error_logs where convert(varchar,ErrorDateTime,101)=  CONVERT(varchar,'" & txtErrorDate.Text.Trim & "',112) order by ErrorDateTime desc"
        End If
        Dim myConnection As New SqlConnection(ConString)
        Dim adp As New SqlDataAdapter(sql, myConnection)
        Dim ds As New DataSet
        adp.Fill(ds)
        grdErrorReport.DataSource = ds
        grdErrorReport.DataBind()
    End Sub

#End Region

#Region "FTP Report"

    Protected Sub btnFTPView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFTPView.Click
        grdFTPReportBind()
    End Sub
    Private Sub grdFTPReportBind()
        Dim sql As String = ""
        If txtFTPDate.Text.Trim = "" Then
            sql = "select top 500 Channelid,Filename,ftpdate,loggedinuser,convert (varchar,ftpdate,100) as ftpdate1 from ftp_records order by ftpdate desc"
        Else
            sql = "select Channelid,Filename,ftpdate,loggedinuser,convert (varchar,ftpdate,100) as ftpdate1 from ftp_records where convert(varchar,ftpdate,101)=  CONVERT(varchar,'" & txtFTPDate.Text.Trim & "',112) order by ftpdate desc"
        End If
        Dim myConnection As New SqlConnection(ConString)
        Dim adp As New SqlDataAdapter(sql, myConnection)
        Dim ds As New DataSet
        adp.Fill(ds)
        grdFTPReport.DataSource = ds
        grdFTPReport.DataBind()
    End Sub
#End Region

#Region "EPG Missing Info"
    Private Sub grdEPGMissingInfo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEPGMissingInfo.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbProgdate As Label = DirectCast(e.Row.FindControl("lbProgdate"), Label)
                If lbProgdate.Text = DateTime.Now.Date.ToString("dd MMM yyyy") Then
                    e.Row.BackColor = Drawing.Color.RosyBrown
                End If
                'e.Row.Cells(0).Text = (Convert.ToDateTime(e.Row.Cells(0).Text)).ToString("dd-MMM-yyyy")
            End If
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "grdSynopsis_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

#End Region

#Region "Channel Onair-OffAir"
    Dim MyConnection As New SqlConnection(ConString)
    Dim flag As Integer = 0
    Protected Sub grdChannelOffAir_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdChannelOffAir.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim lbRowID As Label = DirectCast(grdChannelOffAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowID"), Label)
            Dim lbChannelId As Label = DirectCast(grdChannelOffAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbChannelId"), Label)
            If e.CommandName.ToLower = "onair" Then
                Dim DS As DataSet
                Dim MyDataAdapter As New SqlDataAdapter("update mst_Channel set OnAir=1 where RowId='" & lbRowID.Text & "'", MyConnection)
                MyDataAdapter.SelectCommand.CommandType = CommandType.Text
                DS = New DataSet()
                MyDataAdapter.Fill(DS, "ChannelOffAir")
                MyDataAdapter.Dispose()
                MyConnection.Close()

                Try
                    Dim strRecepient As String, strSubject As String, strBody As String, strAttachment As String, strBcc As String, strRegards As String
                    strRecepient = "epgtech@ndtv.com"
                    strSubject = "Channel " & lbChannelId.Text & " set ON-AIR"
                    strBody = "Hi Team,<br/><br/>Channel <b>" & lbChannelId.Text & "</b> set ON-AIR by <b>" & User.Identity.Name & ".</b><br/><br/>"
                    strRegards = "Regards<br/>Team EPG, NDTV"
                    strBcc = ""
                    strAttachment = ""
                    Logger.mailMessage(strRecepient.Trim, strSubject.Trim, strBody.Trim & strRegards.Trim, strBcc.Trim, strAttachment.Trim)
                Catch ex As Exception
                    Logger.LogError("Channel ONAIR OFFAIR", "Mail", ex.Message.ToString, User.Identity.Name)
                End Try
                Response.Redirect("~/Masters/ChannelOnAirOffAir.aspx")
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", e.CommandName.ToLower & " grdChannelOffAir_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Dim offAirSno As Integer = 1
    Dim onAirSno As Integer = 1
    Private Sub grdChannelOffAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdChannelOffAir.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Try
                    Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSno"), Label)
                    lbSno.Text = offAirSno.ToString
                    offAirSno = offAirSno + 1

                    Dim lbColor As Label = DirectCast(e.Row.FindControl("lbColor"), Label)
                    Dim lbEPGAvailableTill As Label = DirectCast(e.Row.FindControl("lbEPGAvailableTill"), Label)
                    Dim dtEPGAvailableTill As Date = Convert.ToDateTime(lbEPGAvailableTill.Text).ToString


                    If lbColor.Text.ToLower = "green" Then
                        e.Row.BackColor = Drawing.Color.LightGreen
                    End If

                    If dtEPGAvailableTill.ToString("yyyyMMdd") <= DateTime.Now.AddDays(1).ToString("yyyyMMdd") Then
                        e.Row.BackColor = Drawing.Color.IndianRed
                    End If
                Catch
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", "grdChannelOffAir_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdChannelOnAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdChannelOnAir.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Try
                    Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSno"), Label)
                    Dim lbAirtelFTP As Label = DirectCast(e.Row.FindControl("lbAirtelFTP"), Label)

                    lbSno.Text = onAirSno.ToString
                    onAirSno = onAirSno + 1
                    Dim lbEPGAvailableTill As Label = DirectCast(e.Row.FindControl("lbEPGAvailableTill"), Label)
                    Dim dtEPGAvailableTill As Date = Convert.ToDateTime(lbEPGAvailableTill.Text).ToString
                    If dtEPGAvailableTill.ToString("yyyyMMdd") <= DateTime.Now.AddDays(1).ToString("yyyyMMdd") Then
                        e.Row.BackColor = Drawing.Color.IndianRed
                    End If
                    If Not lbAirtelFTP.Text = "True" Then
                        e.Row.BackColor = Drawing.Color.OrangeRed
                    End If
                Catch
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", "grdChannelOnAir_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdChannelOnAir_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdChannelOnAir.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim lbRowID As Label = DirectCast(grdChannelOnAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowID"), Label)
            Dim lbChannelId As Label = DirectCast(grdChannelOnAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbChannelId"), Label)
            If e.CommandName.ToLower = "offair" Then
                Dim DS As DataSet
                Dim MyDataAdapter As New SqlDataAdapter("update mst_Channel set OnAir=0 where RowId='" & lbRowID.Text & "'", MyConnection)
                MyDataAdapter.SelectCommand.CommandType = CommandType.Text
                DS = New DataSet()
                MyDataAdapter.Fill(DS, "ChannelONAir")
                MyDataAdapter.Dispose()
                MyConnection.Close()

                Try
                    Dim strRecepient As String, strSubject As String, strBody As String, strAttachment As String, strBcc As String, strRegards As String
                    strRecepient = "epgtech@ndtv.com"
                    strSubject = "Channel " & lbChannelId.Text & " set OFF-AIR"
                    strBody = "Hi Team,<br/><br/>Channel <b>" & lbChannelId.Text & "</b> set OFF-AIR by <b>" & User.Identity.Name & ".</b><br/><br/>"
                    strRegards = "Regards<br/>Team EPG, NDTV"
                    strBcc = ""
                    strAttachment = ""
                    Logger.mailMessage(strRecepient.Trim, strSubject.Trim, strBody.Trim & strRegards.Trim, strBcc.Trim, strAttachment.Trim)
                Catch
                End Try

                Response.Redirect("~/Masters/ChannelOnAirOffAir.aspx")
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", e.CommandName.ToLower & " grdChannelOnAir_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

#Region "ServiceID Master"
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            txtRowId.Text = "0"
            txtServiceID.Text = ""
            txtChannelID.Text = ""
        Catch ex As Exception
            Logger.LogError("ServiceID", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox(ex.Message.ToString)
        End Try
    End Sub


    Private Sub btnUpdateServiceId_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateServiceId.Click
        Try
            If MyConnection.State = ConnectionState.Closed Then
                MyConnection.Open()
            End If

            Dim cmd As New SqlCommand("update mst_channel set serviceid='" & txtServiceID.Text.Trim.Replace("'", "''") & "' where RowID='" & txtRowId.Text & "'", MyConnection)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            MyConnection.Close()
            MyConnection.Dispose()

            btnCancel.Visible = False
            btnUpdateServiceId.Visible = False

            grdServiceID.SelectedIndex = -1
            grdServiceID.DataBind()

            Try
                Dim strRecepient As String, strSubject As String, strBody As String, strAttachment As String, strBcc As String, strRegards As String
                strRecepient = "epgtech.ndtv.com"
                strSubject = "Service ID updated for channel " & txtChannelID.Text
                strBody = "Hi Team,<br/><br/>Service ID for channel <b>" & txtChannelID.Text & "</b> has been updated to <b>" & txtServiceID.Text & "</b> by <b>" & User.Identity.Name & ".</b><br/><br/>"
                strRegards = "Regards<br/>Team EPG, NDTV"
                strBcc = ""
                strAttachment = ""
                Logger.mailMessage(strRecepient.Trim, strSubject.Trim, strBody.Trim & strRegards.Trim, strBcc.Trim, strAttachment.Trim)
            Catch
            End Try

            txtServiceID.Text = ""
            txtChannelID.Text = ""
            txtRowId.Text = ""
            myMessageBox("ServiceID has been Updated!")
        Catch ex As Exception
            Logger.LogError("ServiceID", "btnUpdateServiceId_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Not Updated! Please see error Report.")
        End Try
    End Sub

    Private Sub grdServiceID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdServiceID.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdServiceID.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbChannelId As Label = DirectCast(grdServiceID.SelectedRow.FindControl("lbChannelId"), Label)
            Dim lbServiceID As Label = DirectCast(grdServiceID.SelectedRow.FindControl("lbServiceID"), Label)

            txtRowId.Text = lbRowId.Text.Trim
            txtChannelID.Text = lbChannelId.Text

            ddlChannel.SelectedValue = lbChannelId.Text
            txtServiceID.Text = lbServiceID.Text

            btnUpdateServiceId.Visible = True
            btnCancel.Visible = True
        Catch ex As Exception
            Logger.LogError("ServiceID", "grdChannelmaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

#Region "Synopsis Modified"
    Protected Sub grdRepModSynopsis_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdRepModSynopsis.RowUpdating
        Try

            Dim txtRegProgName As TextBox = DirectCast(grdRepModSynopsis.Rows(e.RowIndex).FindControl("txtRegProgName"), TextBox)
            Dim txtRegSynopsis As TextBox = DirectCast(grdRepModSynopsis.Rows(e.RowIndex).FindControl("txtRegSynopsis"), TextBox)
            Dim lbProgId As Label = DirectCast(grdRepModSynopsis.Rows(e.RowIndex).FindControl("lbProgId"), Label)

            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection(ConString)
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_ProgramRegional", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = lbProgId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = txtRegProgName.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = txtRegSynopsis.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = ddlLanguage.SelectedValue.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "C"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            Dim DSProgRegional As New DataSet
            MyDataAdapter.Fill(DSProgRegional, "GetRegionalProgName")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("repModSynopsis", "grdRepModSynopsis_RowUpdating", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdRepModSynopsis_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdRepModSynopsis.RowCancelingEdit

    End Sub

    Protected Sub grdRepModSynopsis_RowUpdated(sender As Object, e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles grdRepModSynopsis.RowUpdated
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True
                grdRepModSynopsis.EditIndex = -1
                grdRepModSynopsis.DataBind()
            End If
        Catch ex As Exception
            Logger.LogError("repModSynopsis", "grdRepModSynopsis_RowUpdated", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

   
End Class