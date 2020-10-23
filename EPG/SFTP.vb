Imports System
Imports WinSCP

Friend Class SFTP
    Public Shared Function AirtelManesar(ByVal FileName As String, ByVal ChannelId As String, ByVal LoggedInUser As String) As Integer

        Try
            ' Setup session options
            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = Protocol.Sftp

                '.HostName = "59.165.174.210"   'TATASKY IP
                '.UserName = "NDTV"             'TATASKY Username
                '.Password = "ndtvftp#9742"     'TATASKY Password

                .HostName = "125.22.32.31"  ' MANESAR IP
                .UserName = "ftpmediae2e"   'Manesar Username
                .Password = "ftpmediae2e"   'Manesar Password
                .SshHostKeyFingerprint = "ssh-rsa 1024 8b:03:55:7f:e3:a2:df:b5:da:a3:73:9c:66:d4:ed:7a"  'Manesar FingerPrint epg.archana
                '.SshHostKeyFingerprint = "ssh-rsa 1024 8b:03:55:7f:e3:a2:df:b5:da:a3:73:9c:66:d4:ed:7a"  'Manesar FingerPrint EPGOPS cloud

            End With

            Using session As Session = New Session
                ' Connect
                session.Open(sessionOptions)
                'Upload files
                Dim transferOptions As New TransferOptions
                transferOptions.TransferMode = TransferMode.Binary

                Dim transferResult As TransferOperationResult
                transferResult = session.PutFiles(filename, "/home/ftpmediae2e/EPG/Data/", False, transferOptions) 'Manesar Folder
                ' Throw on any error
                transferResult.Check()
                ' Print results
                Dim transfer As TransferEventArgs
                For Each transfer In transferResult.Transfers
                    Logger.LogFTP(ChannelId, FileName, LoggedInUser)
                    'Console.WriteLine("Upload of {0} succeeded", transfer.FileName)
                Next
            End Using

            Return 0
        Catch e As Exception
            'Logger.LogError(e.Message.ToString)
            Logger.LogError("SFTP", "MAIN", "Manesar - " & e.Message.ToString, "KAUTILYA")
            'Console.WriteLine("Error: {0}", e)
            Return 1
        End Try

    End Function

    Public Shared Function AirtelManesar2(ByVal FileName As String, ByVal ChannelId As String, ByVal LoggedInUser As String) As Integer

        Try
            ' Setup session options
            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = Protocol.Sftp

                .HostName = "125.22.32.38"  ' Manesar2 IP
                .UserName = "ftpmediae2e"   'Manesar2 Username
                .Password = "Airtel20MnFtpMedia"   'Manesar2 Password
                .SshHostKeyFingerprint = "ssh-rsa 2048 6e:79:2e:da:0a:16:61:d9:d0:2a:ad:81:c5:8e:0b:38"  'Manesar2 FingerPrint epg.archana

            End With

            Using session As Session = New Session
                ' Connect
                session.Open(sessionOptions)
                'Upload files
                Dim transferOptions As New TransferOptions
                transferOptions.TransferMode = TransferMode.Binary

                Dim transferResult As TransferOperationResult
                transferResult = session.PutFiles(FileName, "/home/ftpmediae2e/EPG/Data/", False, transferOptions) 'Manesar2 Folder
                ' Throw on any error
                transferResult.Check()
                ' Print results
                Dim transfer As TransferEventArgs
                For Each transfer In transferResult.Transfers
                    Logger.LogFTP(ChannelId, FileName, LoggedInUser)
                    'Console.WriteLine("Upload of {0} succeeded", transfer.FileName)
                Next
            End Using

            Return 0
        Catch e As Exception
            'Logger.LogError(e.Message.ToString)
            Logger.LogError("SFTP", "MAIN", "Manesar2 - " & e.Message.ToString, "KAUTILYA")
            'Console.WriteLine("Error: {0}", e)
            Return 1
        End Try

    End Function
    Public Shared Function AirtelBangalore(ByVal FileName As String, ByVal ChannelId As String, ByVal LoggedInUser As String) As Integer

        Try
            ' Setup session options
            Dim sessionOptions As New SessionOptions
            With sessionOptions
                .Protocol = Protocol.Sftp
                .HostName = "125.16.143.36"     'BANGALORE IP
                .UserName = "ftpmediae2e"       'BANGALORE Username
                .Password = "ftpmediae2e"       'BANGALORE Password
                .SshHostKeyFingerprint = "ssh-rsa 2048 03:d1:84:d3:19:29:08:60:38:fa:e4:c9:18:ec:66:3b"  'Bangalore FingerPrint
                '.SshHostKeyFingerprint = "ssh-rsa 2048 03:d1:84:d3:19:29:08:60:38:fa:e4:c9:18:ec:66:3b"  'Bangalore FingerPrint EPGOPS cloud
            End With

            Using session As Session = New Session
                ' Connect
                session.Open(sessionOptions)
                'Upload files
                Dim transferOptions As New TransferOptions
                transferOptions.TransferMode = TransferMode.Binary
                Dim transferResult As TransferOperationResult
                transferResult = session.PutFiles(filename, "/home/ftpmediae2e/home/ftpmediae2e/EPG/Data/", False, transferOptions) 'Bangalore Folder
                ' Throw on any error
                transferResult.Check()
                ' Print results
                Dim transfer As TransferEventArgs
                For Each transfer In transferResult.Transfers
                    Logger.LogFTP(ChannelId, FileName, LoggedInUser)
                    'Console.WriteLine("Upload of {0} succeeded", transfer.FileName)
                    'Logger.LogFTP(ChannelId, FileName, DateTime.Now, LoggedInUser)
                    'Logger.LogError("SFTP", "MAIN", "SUCCESSFUL SFTP TRANSFER TO BANGALORE: " & transfer.FileName, "KAUTILYA")
                Next
            End Using
            Return 0
        Catch e As Exception
            'Logger.LogError(e.Message.ToString)
            Logger.LogError("SFTP", "MAIN", "Bangalore- " & e.Message.ToString, "KAUTILYA")
            'Console.WriteLine("Error: {0}", e)
            Return 1
        End Try
    End Function
End Class
