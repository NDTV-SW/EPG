Imports System
Imports System.Data.SqlClient

Public Class serviceExecute
    Inherits System.Web.UI.Page

    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ConnectionString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strMode As String = Request.QueryString("mode").ToString
            Select Case (strMode)
                Case "updateVideoURL"
                    Dim strProgId As String = Request.QueryString("progid").ToString
                    Dim strValue As String = Request.QueryString("value").ToString
                    updateVideoURL(strProgId, strValue)
                    Exit Select
                Case ""
                    Dim abc As String = "abc"
                    Exit Select
                Case Else
                    Dim ab As String = "ab"
                    Exit Select
            End Select
        Catch ex As Exception
            strJSONResult = "{ ""result"" : ["
            strJSONResult += "{ ""error"":""" & ex.Message.ToString & """  } ],"
            strJSONResult += """message"": [{ ""success"":""0"" }]}"
            Response.Write(strJSONResult)
        End Try

    End Sub
    Dim strJSONResult As String
    Private Sub updateVideoURL(ByVal progId As String, ByVal VideoUrl As String)
        Try
            Dim obj As New clsExecute
            obj.executeSQL("update mst_program set videourl='" & VideoUrl & "' where progid='" & progId & "'", False)
            strJSONResult = "{ ""result"" : ["
            strJSONResult += "{ ""progid"":""" & progId & """ , ""videoURL"":""" & VideoUrl & """ } ],"
            strJSONResult += """message"": [{ ""success"":""1"" }]}"
            Response.Write(strJSONResult)
        Catch ex As Exception
            strJSONResult = "{ ""result"" : ["
            strJSONResult += "{ ""error"":""" & ex.Message.ToString & """  } ],"
            strJSONResult += """message"": [{ ""success"":""0"" }]}"
            Response.Write(strJSONResult)
        End Try


    End Sub

End Class