Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports Amazon.S3
Imports Amazon.S3.Transfer


Public Class clsFTP
    Public Sub clsFTP()

    End Sub
    Public Function doS3Task(ByVal strfilePath As String, ByVal destinationPath As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("insert into mst_DoS3(filepath,destinationfolder) values('" & strfilePath & "','" & destinationPath & "')", False)

            'Dim strAccesskeyName As String = "AKIAJPBM6YSSVEEJQJPQ"
            'Dim strSecretkeyName As String = "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"
            'Dim strBucketName As String = "ureqamedia" & destinationPath
            'Dim FilePath As String = strfilePath
            'Dim NewFilePath As String = strfilePath.Replace(".zip", DateTime.Now.ToString("HHmmss") & ".zip")

            'Dim fileTransferUtility As New TransferUtility(New AmazonS3Client(strAccesskeyName, strSecretkeyName, Amazon.RegionEndpoint.APSoutheast1))

            'fileTransferUtility.Upload(FilePath, strBucketName)
            Return True

        Catch ex As Exception
            'Logger.LogError("Error while FTP for " & client)
            Logger.LogError("clsFTP", "doS3Task", ex.Message.ToString, "S3")

            Return False
        End Try

    End Function


End Class
