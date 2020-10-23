Imports System.Data
Imports System.Data.SqlClient
Imports WinSCP

Public Class FTPXML1
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
            If Page.IsPostBack = False Then
                GridView1Bind()
            End If
        Catch ex As Exception
            Logger.LogError("FTP XML", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub GridView1Bind()
        Try
            Dim adp As New SqlDataAdapter("sp_qry_FTPXML", MyConnection)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            'Dim adp As New SqlDataAdapter("Select X.ChannelId, X.XmlDateTime, X.xmlfilename,X.rowid From aud_epg_xml_ftp X,(Select max(rowid) RowId, ChannelId from aud_epg_xml_ftp where xmlfilename is not null Group by ChannelId) Y where X.ChannelId = Y.ChannelId And X.RowId = Y.RowId and xmlfilename is not null and X.ChannelId in (Select ChannelId from mst_channel where onair = 1) and x.xmlsent=0 order by X.Rowid desc", MyConnection)
            Dim ds As New DataSet
            adp.Fill(ds)
            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Logger.LogError("FTP XML", "GridView1Bind", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Function GetDocuments(physicalPath As String) As System.IO.FileInfo()
        Dim directory As New System.IO.DirectoryInfo(physicalPath)
        If directory.Exists Then
            Return directory.GetFiles()
        Else
            Throw New System.IO.DirectoryNotFoundException(physicalPath)
        End If
    End Function

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lbChannelID As Label = DirectCast(e.Row.FindControl("lbChannelID"), Label)
                Dim lbAirtelFTP As Label = DirectCast(e.Row.FindControl("lbAirtelFTP"), Label)

                Dim lbXmlDateTime As Label = DirectCast(e.Row.FindControl("lbXmlDateTime"), Label)
                Dim hyXmlFileName As HyperLink = DirectCast(e.Row.FindControl("hyXmlFileName"), HyperLink)

                Dim lbStartDate As Label = DirectCast(e.Row.FindControl("lbStartDate"), Label)
                Dim lbEndDate As Label = DirectCast(e.Row.FindControl("lbEndDate"), Label)
                Dim strStartDate As String, strEndDate As String
                Dim strSMonth As String, strSDate As String, strSYear As String
                Dim strEMonth As String, strEDate As String, strEYear As String
                lbAirtelFTP.Font.Bold = True
                'lbAirtelFTP.ForeColor = Drawing.Color.White
                If lbAirtelFTP.Text.ToUpper = "FALSE" Then
                    lbAirtelFTP.Text = lbAirtelFTP.Text.ToUpper
                    e.Row.BackColor = Drawing.Color.LightPink
                Else
                    lbAirtelFTP.Text = lbAirtelFTP.Text.ToUpper
                    e.Row.BackColor = Drawing.Color.LightGreen
                End If

                Dim patha As String = Server.MapPath("~/xml/")
                hyXmlFileName.NavigateUrl = "~/xml/" & hyXmlFileName.Text
                'hyXmlFileName.NavigateUrl = patha & hyXmlFileName.Text

                strStartDate = hyXmlFileName.Text.Substring(hyXmlFileName.Text.ToString.Length - 16, 6)
                strEndDate = hyXmlFileName.Text.Substring(hyXmlFileName.Text.ToString.Length - 10, 6)

                strSYear = strStartDate.Substring(0, 2)
                strSMonth = strStartDate.Substring(2, 2)
                strSDate = strStartDate.Substring(4, 2)

                strEYear = strEndDate.Substring(0, 2)
                strEMonth = strEndDate.Substring(2, 2)
                strEDate = strEndDate.Substring(4, 2)

                lbStartDate.Text = Convert.ToDateTime(strSMonth & "/" & strSDate & "/" & strSYear).ToString("dd-MMM-yyyy")
                lbEndDate.Text = Convert.ToDateTime(strEMonth & "/" & strEDate & "/" & strEYear).ToString("dd-MMM-yyyy")

            End If
        Catch ex As Exception
            'Logger.LogError("Upload Schedule", "GridView1_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnFTP_Click(sender As Object, e As EventArgs) Handles btnFTP.Click, btnFTP1.Click
        Try
            Dim row As GridViewRow
            Dim selectCount As Integer = 0
            For Each row In GridView1.Rows
                Dim chkFTP As CheckBox = DirectCast(row.FindControl("chkFTP"), CheckBox)
                If chkFTP.Checked Then
                    selectCount = selectCount + 1
                End If
            Next row

            If selectCount > 2 Then
                myErrorBox("You cannot FTP more than two files at a time")
                Exit Sub
            End If

            For Each row In GridView1.Rows
                Dim chkFTP As CheckBox = DirectCast(row.FindControl("chkFTP"), CheckBox)
                Dim hyXmlFileName As HyperLink = DirectCast(row.FindControl("hyXmlFileName"), HyperLink)
                Dim lbRowId As Label = DirectCast(row.FindControl("lbRowId"), Label)
                Dim lbChannelID As Label = DirectCast(row.FindControl("lbChannelID"), Label)
                Dim lbAirtelFTP As Label = DirectCast(row.FindControl("lbAirtelFTP"), Label)
                Dim lbAirtelMail As Label = DirectCast(row.FindControl("lbAirtelMail"), Label)
                Dim lbCatchupFlag As Label = DirectCast(row.FindControl("lbCatchupFlag"), Label)

                If chkFTP.Checked Then
                    Dim filename As String = hyXmlFileName.Text
                    Dim patha As String
                    Dim filepath1 As String
                    patha = Server.MapPath("~/xml/")
                    filepath1 = patha + filename

                    'path1 = "~/xml/" & filename
                    'UploadFile(filepath1, "ftp://172.16.0.72/" & filename, "developer", "developer")
                    'UploadFile(filepath1, "sftp://125.22.32.31/home/ftpmediae2e/EPG/Data/" & filename, "ftpmediae2e", "ftpmediae2e")
                    'SFTP.Main(patha & filename)

                    If lbAirtelFTP.Text = True Then
                        'Dim AirtelManesarFlag As Integer
                        Dim AirtelManesar2Flag As Integer, AirtelBangaloreFlag As Integer
                        Dim FTPManesar As String = ConfigurationManager.AppSettings("FTPManesar").ToString
                        Dim FTPManesar2 As String = ConfigurationManager.AppSettings("FTPManesar2").ToString
                        Dim FTPBangalore As String = ConfigurationManager.AppSettings("FTPBangalore").ToString

                        'If FTPManesar = "True" Then
                        '    AirtelManesarFlag = SFTP.AirtelManesar("C:\inetpub\wwwroot\EPG_v2\xml\" & filename, lbChannelID.Text, User.Identity.Name)     'Upload file to Manesar
                        '    If AirtelManesarFlag = 1 Then
                        '        Logger.LogError("FTP XML", "btnFTP_Click", "FTP of File " & filename & " failed to Airtel Manesar.", User.Identity.Name)
                        '        myErrorBox("Files Not Uploaded to Airtel Manesar!")
                        '        Exit Sub
                        '    End If
                        'End If
                        If FTPManesar2 = "True" Then
                            AirtelManesar2Flag = SFTP.AirtelManesar2("C:\inetpub\wwwroot\EPG_v2\xml\" & filename, lbChannelID.Text, User.Identity.Name)     'Upload file to Manesar2
                            If AirtelManesar2Flag = 1 Then
                                Logger.LogError("FTP XML", "btnFTP_Click", "FTP of File " & filename & " failed to Airtel Manesar2.", User.Identity.Name)
                                myErrorBox("Files Not Uploaded to Airtel Manesar2!")
                                Exit Sub
                            End If
                        End If

                        If FTPBangalore = "True" Then
                            AirtelBangaloreFlag = SFTP.AirtelBangalore("C:\inetpub\wwwroot\EPG_v2\xml\" & filename, lbChannelID.Text, User.Identity.Name) 'Upload file to Bangalore
                            If AirtelBangaloreFlag = 1 Then
                                Logger.LogError("FTP XML", "btnFTP_Click", "FTP of File " & filename & " failed to Airtel Bangalore.", User.Identity.Name)
                                myErrorBox("Files Not Uploaded to Airtel Bangalore!")
                                Exit Sub
                            End If
                        End If
                    End If

                    
                    ''clsExecute.executeSQL("update aud_epg_xml_ftp set xmlsent=1,FTPDateTime=dbo.GetLocalDate() where rowid='" & lbRowId.Text & "'", False)
                    'Dim MyDataAdapter As New SqlDataAdapter("update aud_epg_xml_ftp set xmlsent=1,FTPDateTime=dbo.GetLocalDate() where rowid='" & lbRowId.Text & "'", MyConnection)
                    'MyDataAdapter.SelectCommand.CommandType = CommandType.Text
                    'Dim DS As New DataSet()
                    'MyDataAdapter.Fill(DS, "UpdateXMLSent")
                    'MyDataAdapter.Dispose()
                    'MyConnection.Close()

                    Dim xmlStartDate, xmlEndDate As String
                    Dim strSYear As String, strSMonth As String, strSDate As String
                    Dim strEYear As String, strEMonth As String, strEDate As String

                    xmlStartDate = filename.Substring(filename.ToString.Length - 16, 6)
                    xmlEndDate = filename.Substring(filename.ToString.Length - 10, 6)

                    strSYear = xmlStartDate.Substring(0, 2)
                    strSMonth = xmlStartDate.Substring(2, 2)
                    strSDate = xmlStartDate.Substring(4, 2)

                    strEYear = xmlEndDate.Substring(0, 2)
                    strEMonth = xmlEndDate.Substring(2, 2)
                    strEDate = xmlEndDate.Substring(4, 2)

                    Dim dtStartDate As DateTime = Convert.ToDateTime(strSMonth & "/" & strSDate & "/" & strSYear).Date
                    Dim dtEndDate As DateTime = Convert.ToDateTime(strEMonth & "/" & strEDate & "/" & strEYear).Date

                    xmlStartDate = dtStartDate.ToString("dd-MMM-yyyy")
                    xmlEndDate = dtEndDate.ToString("dd-MMM-yyyy")

                    Dim strBody As String, strAttachment As String, strSubject As String

                    strAttachment = "C:\inetpub\wwwroot\EPG_V2\xml\" & filename

                    'Try
                    Dim AirtelFTPRecipients As String = ConfigurationManager.AppSettings("AirtelFTPRecipients").ToString
                    Dim MailEpgTech As String = ConfigurationManager.AppSettings("MailEpgTech").ToString
                    Dim EpgTechRecipients As String = ConfigurationManager.AppSettings("EpgTechRecipients").ToString

                    If lbCatchupFlag.Text = False Then
                        If lbAirtelFTP.Text = True Then
                            strSubject = "EPG XML uploaded for " & lbChannelID.Text & " from " & xmlStartDate & " to " & xmlEndDate
                            strBody = "Hi Team,<br/><br/><b>" & filename & "</b> has been uploaded to Airtel FTP servers. The same is also attached here for your reference.<br/><br/>Regards, <br/>Team EPG, NDTV<br/>+91-8800770099<br/>"
                            Logger.mailMessage(AirtelFTPRecipients, strSubject, strBody, "", strAttachment)
                        Else
                            strSubject = "EPG XML for " & lbChannelID.Text & " from " & xmlStartDate & " to " & xmlEndDate
                            strBody = "Hi Team,<br/><br/>Please find attached EPG XML for " & lbChannelID.Text & " <b>.<br/><br/>Regards, <br/>Team EPG, NDTV<br/>+91-8800770099<br/>"
                            If MailEpgTech = "True" Then
                                Logger.mailMessage(AirtelFTPRecipients, strSubject, strBody, EpgTechRecipients, strAttachment)
                            Else
                                Logger.mailMessage(AirtelFTPRecipients, strSubject, strBody, "", strAttachment)
                            End If
                        End If
                    Else
                        strSubject = "EPG XML for " & lbChannelID.Text & " from " & xmlStartDate & " to " & xmlEndDate
                        strBody = "Hi Team,<br/><br/>Please find attached EPG XML for " & lbChannelID.Text & " <b>.<br/><br/>Regards, <br/>Team EPG, NDTV<br/>+91-8800770099<br/>"
                        If MailEpgTech = "True" Then
                            Logger.mailMessage(AirtelFTPRecipients, strSubject, strBody, EpgTechRecipients, strAttachment)
                        Else
                            Logger.mailMessage(AirtelFTPRecipients, strSubject, strBody, "", strAttachment)
                        End If
                        'Logger.mailMessage("kautilyar@ndtv.com", "Channel " & lbChannelID.Text & " has CatchupFlag enabled. So XML not Mailed", lbChannelID.Text, "", "")
                    End If

                    Dim obj As New clsExecute
                    'obj.executeSQL("update aud_epg_xml_ftp set xmlsent=1,FTPDateTime=dbo.GetLocalDate() where rowid='" & lbRowId.Text & "'", False)
                    obj.executeSQL("sp_aud_epg_xml_ftp", "channelid~startdate~enddate~filename", "varchar~datetime~datetime~varchar", lbChannelID.Text & "~" & dtStartDate & "~" & dtEndDate & "~" & filename, True, False)

                End If
            Next row

            GridView1Bind()
            myMessageBox("File Uploaded Successfully")
        Catch ex As Exception
            Logger.LogError("FTP XML", "btnFTP_Click", ex.Message.ToString, User.Identity.Name)
            myMessageBox("Files Not Uploaded. Please check error Log!")
        End Try
    End Sub
    
End Class