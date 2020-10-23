Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Configuration
Imports System.Threading
Imports System.Data.SqlClient
Imports System
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class Logger
    Public Shared LOG_LEVEL As Integer = 0
    Private Shared lastFileName As String
    Private Shared objLogger As New Logger()
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Public Shared Sub myLogMessage(ByVal sender As Page, ByVal messagestr As String)
        sender.ClientScript.RegisterStartupScript(sender.GetType(), "ClientScript", "alert('Message : " & messagestr & "');", True)
    End Sub
    Public Shared Sub myLogError(ByVal sender As Page, ByVal messagestr As String)
        sender.ClientScript.RegisterStartupScript(sender.GetType(), "ClientScript", "alert('Error : " & messagestr & "');", True)
    End Sub

    Public Shared Sub LogError(ByVal ErrorPage As String, ByVal ErrorSource As String, ByVal ErrorMessage As String, ByVal LoggedInUser As String)
        Dim DS As DataSet
        Dim MyDataAdapter As SqlDataAdapter
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
        MyDataAdapter = New SqlDataAdapter("sp_app_error_logs", MyConnection)
        MyDataAdapter.SelectCommand.CommandTimeout = 0
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorPage", SqlDbType.NVarChar, 400)).Value = ErrorPage
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorSource", SqlDbType.NVarChar, 400)).Value = ErrorSource
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorMessage ", SqlDbType.NVarChar, 4000)).Value = ErrorMessage
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorType ", SqlDbType.VarChar, 50)).Value = "ERROR"
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LoggedinUser", SqlDbType.VarChar, 50)).Value = LoggedInUser
        DS = New DataSet()
        MyDataAdapter.Fill(DS, "GetError")
        MyDataAdapter.Dispose()
        MyConnection.Close()
    End Sub

    Public Shared Sub LogFTP(ByVal ChannelId As String, ByVal Filename As String, ByVal LoggedInUser As String)
        Dim DS As DataSet
        Dim MyDataAdapter As SqlDataAdapter
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
        MyDataAdapter = New SqlDataAdapter("sp_ftp_records", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Channelid", SqlDbType.NVarChar, 400)).Value = ChannelId
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Filename", SqlDbType.NVarChar, 400)).Value = Filename
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LoggedinUser", SqlDbType.VarChar, 50)).Value = LoggedInUser
        DS = New DataSet()
        MyDataAdapter.Fill(DS, "GetFTP")
        MyDataAdapter.Dispose()
        MyConnection.Close()
    End Sub

    'Public Shared Sub LogError(ByVal ex As String)

    'End Sub
    'Public Shared Sub LogInfo(ByVal ex As String)

    'End Sub
    Public Shared Sub LogInfo(ByVal ErrorPage As String, ByVal ErrorSource As String, ByVal ErrorMessage As String, ByVal LoggedInUser As String)
        Dim DS As DataSet
        Dim MyDataAdapter As SqlDataAdapter
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
        MyDataAdapter = New SqlDataAdapter("sp_app_error_logs", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorPage", SqlDbType.NVarChar, 400)).Value = ErrorPage
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorSource", SqlDbType.NVarChar, 400)).Value = ErrorSource
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorMessage ", SqlDbType.NVarChar, 4000)).Value = ErrorMessage
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorType ", SqlDbType.VarChar, 50)).Value = "INFO"
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LoggedinUser", SqlDbType.VarChar, 50)).Value = LoggedInUser
        DS = New DataSet()
        MyDataAdapter.Fill(DS, "GetINFO")
        MyDataAdapter.Dispose()
        MyConnection.Close()
    End Sub

    Public Function GetEpgDates(ByVal ChannelId As String)
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        If MyConnection.State = ConnectionState.Closed Then
            MyConnection.Open()
        End If

        Dim cmd As New SqlCommand("SELECT CONVERT(varchar, Min(a.Progdate),106) as MinDate, (Select Right(CONVERT(varchar,MIN(b.Progtime),100),7) from mst_epg b where progdate=CONVERT(varchar, Min(a.Progdate),106) and  ChannelId='" & ChannelId.Trim & "') as MinTime, CONVERT(varchar, MAX(a.Progdate),106) as MaxDate, (Select Right(CONVERT(varchar,MAX(b.Progtime),100),7) from mst_epg b where progdate=CONVERT(varchar, MAX(a.Progdate),106) and ChannelId='" & ChannelId.Trim & "') as MaxTime from mst_epg a where ChannelId='" & ChannelId.Trim & "'", MyConnection)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader

        If dr.HasRows Then
            dr.Read()
            Dim RetString As String = "EPG Exists from " & dr("MinDate").ToString.Trim & " " & dr("MinTime").ToString.Trim & " to " & dr("MaxDate").ToString.Trim & " " & dr("MaxTime").ToString.Trim & "."
            If RetString = "EPG Exists from   to  ." Then
                RetString = "EPG data not available for this Channel."
            End If
            Return RetString
            If dr("MinDate").ToString.Trim = "" And dr("MinTime").ToString.Trim = "" And dr("MaxDate").ToString.Trim = "" And dr("MaxTime").ToString.Trim = "" Then
                Return ("EPG data not available for this Channel.")
            End If
        Else
            Return ("EPG data not available for this Channel.")
        End If
        dr.Close()

    End Function

    Private Shared Function customCertValidation(ByVal sender As Object, _
                                                ByVal cert As X509Certificate, _
                                                ByVal chain As X509Chain, _
                                                ByVal errors As SslPolicyErrors) As Boolean
        Return True
    End Function

    Public Shared Sub mailMessage(ByVal recepient As String, ByVal subject As String, ByVal body As String, ByVal bcc As String, ByVal attachment As String)

        Try
            'If bcc.Trim = "" Then
            '    bcc = "kautilyar@ndtv.com"
            'End If

            Dim mail As New MailMessage()
            Dim SmtpServer As New SmtpClient()
            SmtpServer.UseDefaultCredentials = False

            SmtpServer.Credentials = New Net.NetworkCredential("smtpauth@ndtv.com", "arD0r9rNo@p")
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

            SmtpServer.Host = "relaysmtp.logix.in" '221.134.197.201" '"relaysmtp.logix.in"
            SmtpServer.Port = 587

            SmtpServer.EnableSsl = False

            mail = New MailMessage()
            Dim addrRecipient() As String = recepient.Split(";")

            Dim addBCC() As String = bcc.Split(";")

            mail.From = New MailAddress("epgtech@ndtv.com", "NDTV EPG Team", System.Text.Encoding.UTF8)

            Dim i As Byte
            For i = 0 To addrRecipient.Length - 1
                mail.To.Add(addrRecipient(i))
            Next
            'For i = 0 To addCC.Length - 1
            '    mail.CC.Add(addCC(i))
            'Next
            If Not bcc.Trim = "" Then
                For i = 0 To addBCC.Length - 1
                    mail.Bcc.Add(addBCC(i))
                Next
            End If
            mail.Subject = subject
            mail.Body = "Hi<br/><br/>" & body & "<br/><br/>Regards<br/>Team EPG, NDTV"
            mail.IsBodyHtml = True
            If Not (attachment = "") Then
                Dim Afile As New Attachment(attachment)
                Dim cd As System.Net.Mime.ContentDisposition
                cd = Afile.ContentDisposition
                mail.Attachments.Add(Afile)
            End If
            ServicePointManager.ServerCertificateValidationCallback = _
                    New System.Net.Security.RemoteCertificateValidationCallback(AddressOf customCertValidation)
            SmtpServer.Send(mail)

        Catch ex As Exception
            Logger.LogError("Logger.vb", "mailMessage", ex.Message.ToString, "Mail")
            'Logger.LogError(ex.Message.ToString)
            'MessageBox.Show("error" + ex.Message + "\n" + ex.InnerException.ToString())
        End Try
    End Sub

    'Public Shared Sub mailMessage(ByVal recepient As String, ByVal subject As String, ByVal body As String, ByVal bcc As String, ByVal attachment As String)

    '    Try
    '        'clsExecute.executeSQL("dbmail_v1", "RecipientName~Subject~bcc~body~attachment", "nVarChar~nVarChar~nVarChar~nVarChar~nVarChar", recepient & "~" & subject & "~" & bcc & "~" & body & "~" & attachment, True)

    '        Dim DS As DataSet
    '        Dim MyDataAdapter As SqlDataAdapter
    '        Dim MyConnection As New SqlConnection
    '        MyConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    '        MyDataAdapter = New SqlDataAdapter("dbmail_v1", MyConnection)
    '        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RecipientName", SqlDbType.NVarChar, 1000)).Value = recepient.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Subject", SqlDbType.NVarChar, 2000)).Value = subject.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@bcc", SqlDbType.NVarChar, 1000)).Value = bcc.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Body", SqlDbType.NVarChar, 4000)).Value = body.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@attachment", SqlDbType.NVarChar, 2000)).Value = attachment.Trim
    '        DS = New DataSet()
    '        MyDataAdapter.Fill(DS, "GetMail")
    '        MyDataAdapter.Dispose()
    '        MyConnection.Close()
    '    Catch ex As Exception
    '        Logger.LogError("Logger", "mailMessage", ex.Message.ToString, "Mail")
    '    End Try

    'End Sub

    Shared Function GetFormatedDate(ByVal DateValue As String) As String
        Return Regex.Replace(DateValue, "\b(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{2,4})\b", "${month}/${day}/${year}")
    End Function

   
    Shared Function RemSplCharsAllLangs(ByVal specialString As String, ByVal langId As Integer) As String
        specialString = specialString.Replace("©", "")
        specialString = specialString.Replace("®", "")
        specialString = specialString.Replace("^", "")
        specialString = specialString.Replace("<", "")
        specialString = specialString.Replace(">", "")
        specialString = specialString.Replace("""", "'")
        specialString = specialString.Replace("Š", "")
        specialString = specialString.Replace("Ž", "")
        specialString = specialString.Replace("   ", " ")
        specialString = specialString.Replace("  ", " ")
        specialString = specialString.Replace("’", "'")
        specialString = specialString.Replace("`", "'")
        specialString = specialString.Replace(" ", " ")
        specialString = specialString.Replace("–", "-")
        specialString = specialString.Replace("—", "-")

        'If langId = 1 Then  'English
        '    specialString = specialString.Replace("@", " At ")
        'ElseIf langId = 2 Then  'Hindi
        '    specialString = specialString.Replace("@", " एट ")
        'ElseIf langId = 4 Then  'Marathi
        '    specialString = specialString.Replace("@", " एट ")
        'ElseIf langId = 7 Then  'Tamil
        '    specialString = specialString.Replace("@", " அட் ")
        'ElseIf langId = 8 Then  'Telugu
        '    specialString = specialString.Replace("@", " ఎట్ ")
        'End If

        specialString = specialString.Replace(" ", " ")
        specialString = specialString.Replace("%", "")
        specialString = specialString.Replace("(", "")
        specialString = specialString.Replace(")", "")
        specialString = specialString.Replace("#", "")

        Return specialString
    End Function

    Shared Function RemSplCharsEng(ByVal specialString As String) As String
        'specialString = specialString.Replace("@", " At ")
        specialString = specialString.Replace("%", "percent")
        specialString = specialString.Replace("(", "")
        specialString = specialString.Replace(")", "")
        specialString = specialString.Replace("#", "")
        specialString = specialString.Replace(" ", " ")

        specialString = specialString.Replace("©", "")
        specialString = specialString.Replace("®", "")
        specialString = specialString.Replace("^", "")
        specialString = specialString.Replace("<", "")
        specialString = specialString.Replace(">", "")
        specialString = specialString.Replace("""", "'")
        specialString = specialString.Replace("Š", "")
        specialString = specialString.Replace("Ž", "")

        'specialString = specialString.Replace("&", "and")
        specialString = specialString.Replace("°", "deg")
        specialString = specialString.Replace("   ", " ")
        specialString = specialString.Replace("  ", " ")
        specialString = specialString.Replace("’", "'")
        specialString = specialString.Replace("`", "'")

        specialString = specialString.Replace("Â", "A")
        specialString = specialString.Replace("Ã", "A")
        specialString = specialString.Replace("Ä", "A")
        specialString = specialString.Replace("À", "A")
        specialString = specialString.Replace("Á", "A")
        specialString = specialString.Replace("Å", "A")
        specialString = specialString.Replace("Æ", "AE")

        specialString = specialString.Replace("à", "a")
        specialString = specialString.Replace("á", "a")
        specialString = specialString.Replace("â", "a")
        specialString = specialString.Replace("ã", "a")
        specialString = specialString.Replace("ä", "a")
        specialString = specialString.Replace("å", "a")
        specialString = specialString.Replace("æ", "ae")

        specialString = specialString.Replace("Ç", "C")
        specialString = specialString.Replace("ç", "")

        specialString = specialString.Replace("È", "E")
        specialString = specialString.Replace("É", "E")
        specialString = specialString.Replace("Ê", "E")
        specialString = specialString.Replace("Ë", "E")

        specialString = specialString.Replace("è", "")
        specialString = specialString.Replace("é", "")
        specialString = specialString.Replace("ê", "")
        specialString = specialString.Replace("ë", "")

        specialString = specialString.Replace("Ì", "I")
        specialString = specialString.Replace("Í", "I")
        specialString = specialString.Replace("Î", "I")
        specialString = specialString.Replace("Ï", "I")

        specialString = specialString.Replace("ì", "")
        specialString = specialString.Replace("í", "")
        specialString = specialString.Replace("î", "")
        specialString = specialString.Replace("ï", "")

        specialString = specialString.Replace("Ð", "d")
        specialString = specialString.Replace("Ñ", "N")

        specialString = specialString.Replace("ð", "")
        specialString = specialString.Replace("ñ", "n")

        specialString = specialString.Replace("Ò", "O")
        specialString = specialString.Replace("Ó", "O")
        specialString = specialString.Replace("Ô", "O")
        specialString = specialString.Replace("Õ", "O")
        specialString = specialString.Replace("Ö", "O")
        specialString = specialString.Replace("Ø", "")

        specialString = specialString.Replace("ò", "o")
        specialString = specialString.Replace("ó", "o")
        specialString = specialString.Replace("ô", "o")
        specialString = specialString.Replace("õ", "")
        specialString = specialString.Replace("ö", "")
        specialString = specialString.Replace("ø", "")

        specialString = specialString.Replace("Ù", "U")
        specialString = specialString.Replace("Ú", "U")
        specialString = specialString.Replace("Û", "U")
        specialString = specialString.Replace("Ü", "U")

        specialString = specialString.Replace("ù", "u")
        specialString = specialString.Replace("ú", "u")
        specialString = specialString.Replace("û", "u")
        specialString = specialString.Replace("ü", "u")

        specialString = specialString.Replace("Ý", "Y")
        specialString = specialString.Replace("Þ", "P")
        specialString = specialString.Replace("ß", "B")

        specialString = specialString.Replace("ý", "Y")
        specialString = specialString.Replace("þ", "p")
        specialString = specialString.Replace("ÿ", "y")

        specialString = specialString.Replace("…", ",")
        '     specialString = specialString.Replace(""", "'")
        specialString = specialString.Replace("$", "USD ")
        specialString = specialString.Replace(";s ", "'s ")
        specialString = specialString.Replace(";", ",")
        specialString = specialString.Replace("–", "-")

        specialString = specialString.Replace("â", "a")
        specialString = specialString.Replace("„", ",")
        specialString = specialString.Replace("¢", "c")
        specialString = specialString.Replace("¬", "-")

        specialString = specialString.Replace("—", "-")
        specialString = specialString.Replace("—", "-")
        specialString = specialString.Replace("—", "-")
        Return specialString
    End Function

End Class

