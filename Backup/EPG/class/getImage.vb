Imports System.Drawing

Public Class getImage

    Public Shared Function getImageSize(ByVal imagePath As String) As String
        Try
            Dim imgImage As Image = Image.FromFile(imagePath)

            Dim str As String = imgImage.Width.ToString & " by " & imgImage.Height.ToString
            imgImage.Dispose()
            Return str
        Catch
            Return String.Empty
        End Try
    End Function

End Class

