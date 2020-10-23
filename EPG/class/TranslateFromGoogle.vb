Imports System
Imports System.Data.SqlClient
Imports System.Net
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Data.OleDb
Imports System.Runtime.Serialization.Json
Imports Newtonsoft.Json


Public Class TranslateFromGoogle
    
    Shared Function Translate(ByVal strToConvert As String, ByVal langToConvert As String) As String
        langToConvert = langToConvert.ToLower()
        Try
            Select Case langToConvert
                Case "english"
                    langToConvert = "en"
                Case "hindi"
                    langToConvert = "hi"
                Case "tamil"
                    langToConvert = "ta"
                Case "telugu"
                    langToConvert = "te"
                Case "punjabi"
                    langToConvert = "pa"
                Case "marathi"
                    langToConvert = "mr"
                Case "kannada"
                    langToConvert = "kn"
                Case "bengali"
                    langToConvert = "bn"
                Case Else
                    langToConvert = "error"
            End Select

            If langToConvert = "error" Then
                Return "error"
            End If

            'Dim targetURI As New Uri("https://www.googleapis.com/language/translate/v2/languages?key=AIzaSyBvMx0oBlCl9IrodT96N1Qa2EDwgvT5UlI&target=zh-TW")
            Dim targetURI As New Uri("https://www.googleapis.com/language/translate/v2?key=AIzaSyBvMx0oBlCl9IrodT96N1Qa2EDwgvT5UlI&source=en&target=" & langToConvert & "&q=" & strToConvert)
            Dim _WebQuestionRequest As System.Net.WebRequest = System.Net.WebRequest.Create(targetURI)
            Dim _WebQuestionResponse As System.Net.WebResponse = _WebQuestionRequest.GetResponse

            Dim _QuestionReader As New StreamReader(_WebQuestionResponse.GetResponseStream())
            Dim finalQuestionStr As String = _QuestionReader.ReadToEnd()

            finalQuestionStr = finalQuestionStr.Trim

            Dim jQuestionobj As Newtonsoft.Json.Linq.JObject
            Dim jsonQuestionreader As New JsonTextReader(New StringReader(finalQuestionStr))
            jQuestionobj = Newtonsoft.Json.Linq.JObject.Parse(finalQuestionStr)
            Dim Totaldatacount As Integer = jQuestionobj.Item("data").Count
            Dim qcount As Integer = 0
            If jQuestionobj.Item("data").Count >= 1 Then
                strToConvert = jQuestionobj.Item("data").Item("translations").Item(0).Item("translatedText").ToString()
            Else
                strToConvert = "error"
            End If
            Return strToConvert
        Catch ex As Exception
            Logger.LogError("TranslateFromGoogle", "Translate", ex.Message.ToString, "Google")
            Return "error"
        End Try
    End Function

End Class

