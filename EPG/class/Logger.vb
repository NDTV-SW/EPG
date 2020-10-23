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
Imports System.Xml
Imports System.Security.Cryptography.X509Certificates
Imports System.Reflection
Imports System.IO.Compression

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
        Dim obj As New clsExecute
        obj.executeSQL("sp_app_error_logs", "ErrorPage~ErrorSource~ErrorMessage~ErrorType~LoggedinUser", "nvarchar~nvarchar~nvarchar~varchar~varchar",
                       ErrorPage & "~" & ErrorSource & "~" & ErrorMessage & "~ERROR~" & LoggedInUser, True, False)
    End Sub

    Public Shared Sub LogFTP(ByVal ChannelId As String, ByVal Filename As String, ByVal LoggedInUser As String)
        Dim obj As New clsExecute
        obj.executeSQL("sp_ftp_records", "Channelid~Filename~LoggedinUser", "nvarchar~nvarchar~varchar",
                       ChannelId & "~" & Filename & "~" & LoggedInUser, True, False)

    End Sub

    Public Shared Sub LogInfo(ByVal ErrorPage As String, ByVal ErrorSource As String, ByVal ErrorMessage As String, ByVal LoggedInUser As String)
        Dim obj As New clsExecute
        obj.executeSQL("sp_app_error_logs", "ErrorPage~ErrorSource~ErrorMessage~ErrorType~LoggedinUser", "nvarchar~nvarchar~nvarchar~varchar~varchar",
                       ErrorPage & "~" & ErrorSource & "~" & ErrorMessage & "~INFO~" & LoggedInUser, True, False)
    End Sub

    Public Function GetEpgDates(ByVal ChannelId As String)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("SELECT CONVERT(varchar, Min(a.Progdate),106) as MinDate, (Select Right(CONVERT(varchar,MIN(b.Progtime),100),7) from mst_epg b where progdate=CONVERT(varchar, Min(a.Progdate),106) and  ChannelId='" & ChannelId.Trim & "') as MinTime, CONVERT(varchar, MAX(a.Progdate),106) as MaxDate, (Select Right(CONVERT(varchar,MAX(b.Progtime),100),7) from mst_epg b where progdate=CONVERT(varchar, MAX(a.Progdate),106) and ChannelId='" & ChannelId.Trim & "') as MaxTime from mst_epg a where ChannelId='" & ChannelId.Trim & "'", False)
        If dt.Rows.Count > 0 Then
            Dim RetString As String = "EPG Exists from " & dt.Rows(0)("MinDate").ToString.Trim & " " & dt.Rows(0)("MinTime").ToString & " to " & dt.Rows(0)("MaxDate").ToString & " " & dt.Rows(0)("MaxTime").ToString & "."
            If RetString = "EPG Exists from   to  ." Then
                RetString = "EPG data not available for this Channel."
            End If
            Return RetString
            If dt.Rows(0)("MinDate").ToString.Trim = "" And dt.Rows(0)("MinTime").ToString.Trim = "" And dt.Rows(0)("MaxDate").ToString.Trim = "" And dt.Rows(0)("MaxTime").ToString.Trim = "" Then
                Return ("EPG data not available for this Channel.")
            End If
        Else
            Return ("EPG data not available for this Channel.")
        End If
    End Function

    Private Shared Function customCertValidation(ByVal sender As Object, _
                                                ByVal cert As X509Certificate, _
                                                ByVal chain As X509Chain, _
                                                ByVal errors As SslPolicyErrors) As Boolean
        Return True
    End Function

    Public Shared Sub mailMessage(ByVal recepient As String, ByVal subject As String, ByVal body As String, ByVal bcc As String, ByVal attachment As String)

        Try
            Dim mail As New MailMessage()
            Dim client As New SmtpClient()
            'SmtpServer.UseDefaultCredentials = False

            'SmtpServer.Credentials = New Net.NetworkCredential("smtpauth@ndtv.com", "arD0r9rNo@p")
            'SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network

            client.Host = "ndtv-com.mail.protection.outlook.com" '221.134.197.201" '"relaysmtp.logix.in"
            client.Port = 25
            client.UseDefaultCredentials = True

            'client.EnableSsl = True

            mail = New MailMessage()
            Dim addrRecipient() As String = recepient.Split(";")

            Dim addBCC() As String = bcc.Split(";")

            mail.From = New MailAddress("epgtech@ndtv.com", "NDTV EPG Team", System.Text.Encoding.UTF8)

            Dim i As Byte
            For i = 0 To addrRecipient.Length - 1
                mail.To.Add(addrRecipient(i))
            Next
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
            
            client.Send(mail)

        Catch ex As Exception
            Logger.LogError("Logger.vb", "mailMessage", ex.Message.ToString, "Mail")

        End Try
    End Sub

    Shared Function GetFormatedDate(ByVal DateValue As String) As String
        Return Regex.Replace(DateValue, "\b(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{2,4})\b", "${month}/${day}/${year}")
    End Function

    Shared Function RemoveDiacriticsEng(accentedStr As String) As String
        Dim tempBytes As Byte()
        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr)

        Dim asciiStr As String = System.Text.Encoding.UTF8.GetString(tempBytes)
        asciiStr = asciiStr.Replace(Chr(34), "'")
        asciiStr = asciiStr.Replace("%", "percent")
        asciiStr = asciiStr.Replace("°", "deg")
        'asciiStr = asciiStr.Replace("$", "USD ")
        Dim rgx As Regex = New Regex("[^A-Za-z0-9_@.,;':?!\\|$%(), *^ /#&+-]")
        asciiStr = rgx.Replace(asciiStr, "")
        Return asciiStr
    End Function

    Shared Function RemoveDiacriticsAll(accentedStr As String) As String
        Dim tempBytes As Byte()
        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr)
        Dim asciiStr As String = System.Text.Encoding.UTF8.GetString(tempBytes)
        asciiStr = asciiStr.Replace(Chr(34), "'")
        Return asciiStr
    End Function

    Shared Function RemSplCharsAllLangs(ByVal specialString As String, ByVal langId As Integer) As String
        Return specialString
    End Function

    Shared Function RemSplCharsEng(ByVal specialString As String) As String
        specialString = RemoveDiacriticsEng(specialString)
        Return specialString
    End Function

    Shared Function GetJson(ByVal dt As DataTable) As String
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        serializer.MaxJsonLength = 50000000
        Dim rows As New List(Of Dictionary(Of String, Object))()
        Dim row As Dictionary(Of String, Object) = Nothing
        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)()
            For Each dc As DataColumn In dt.Columns
                'If dc.ColumnName.Trim() = "channelid" Then
                row.Add(dc.ColumnName.Trim(), dr(dc))
                'End If
            Next
            rows.Add(row)
        Next
        Return serializer.Serialize(rows)
    End Function

    Shared Function GetXML(ByVal dt As DataTable) As String
        dt.TableName = "EPG"
        Dim result As String
        Dim sw As New StringWriter()
        dt.WriteXml(sw)
        result = sw.ToString()
        Return result
    End Function

    Shared Function getStatus(ByVal staus As String) As DataTable
        Dim dt As New DataTable("status")
        Dim dc As New DataColumn("status")
        dc.DataType = System.Type.GetType("System.String")
        dt.Columns.Add(dc)

        Dim dr As DataRow = dt.NewRow
        dr.Item("status") = staus
        dt.Rows.Add(dr)
        Return dt
    End Function

    

    Shared Function convertHexToString(ByVal Hex As String) As String
        Dim text = New System.Text.StringBuilder(Hex.Length \ 2)
        For i As Integer = 0 To Hex.Length - 2 Step 2
            text.Append(Chr(Convert.ToByte(Hex.Substring(i, 2), 16)))
        Next
        Return text.ToString

    End Function

    Shared Function convertStringToHex(ByRef Data As String) As String
        Dim sVal As String
        Dim sHex As String = ""
        While Data.Length > 0
            sVal = Conversion.Hex(Strings.Asc(Data.Substring(0, 1).ToString()))
            Data = Data.Substring(1, Data.Length - 1)
            sHex = sHex & sVal
        End While
        Return sHex
    End Function
    Shared Function convertIntToHex(ByRef Data As Integer) As String
        Return "0X0" & Conversion.Hex(Data)
    End Function

    Public Shared Function whichMethodCalledMe() As String
        Dim stackTrace As New StackTrace
        Dim stackFrame As StackFrame = stackTrace.GetFrame(1)
        Dim methodBase As MethodBase = stackFrame.GetMethod
        Return methodBase.Name
    End Function

    Public Shared Function whichPageCalledMe() As String
        Dim path As String = HttpContext.Current.Request.Path.Substring(1, HttpContext.Current.Request.Path.Length - 1)
        Return path
    End Function


    Public Shared Sub CompressGzFile(ByVal sourceFile As String, ByVal destFile As String)

        Dim destStream As New FileStream(destFile, FileMode.Create, FileAccess.Write, FileShare.Read)
        Dim srcStream As New FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim gz As New GZipStream(destStream, CompressionMode.Compress)

        Dim bytesRead As Integer
        Dim buffer As Byte() = New Byte(10000) {}

        bytesRead = srcStream.Read(buffer, 0, buffer.Length)

        While bytesRead <> 0
            gz.Write(buffer, 0, bytesRead)

            bytesRead = srcStream.Read(buffer, 0, buffer.Length)
        End While

        gz.Close()
        destStream.Close()
        srcStream.Close()
    End Sub

    Public Shared Sub DecompressGzFile(ByVal sourceFile As String, ByVal destFile As String)

        Dim destStream As New FileStream(destFile, FileMode.Create, FileAccess.Write, FileShare.Read)
        Dim srcStream As New FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim gz As New GZipStream(srcStream, CompressionMode.Decompress)

        Dim bytesRead As Integer
        Dim buffer As Byte() = New Byte(10000) {}

        bytesRead = gz.Read(buffer, 0, buffer.Length)

        While bytesRead <> 0
            destStream.Write(buffer, 0, bytesRead)

            bytesRead = gz.Read(buffer, 0, buffer.Length)
        End While

        gz.Close()
        destStream.Close()
        srcStream.Close()

    End Sub
End Class

