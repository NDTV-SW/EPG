Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class fpcstatus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.RequestType = "POST" Then
                Dim apikey As String = Request.Headers("apikey")
                If apikey = "IjNZwEZXbOygxlL/HhktSA==" Then
                    Dim id As Integer = Request.Params("id")
                    Dim status As String = Request.Params("status")
                    Dim obj As New clsExecute
                    obj.executeSQL("update fpc_mailattachmentingest set currstatus='" & status & "',currstatusUpdatedAt=dbo.getlocaldate() where id='" & id & "'", False)
                    obj.executeSQL("insert into fpc_aud_mailattachmentingest_status(ingestid,currstatus) values('" & id & "','" & status & "')", False)
                    Dim strResult As String = Logger.GetJson(Logger.getStatus("record updated"))
                    Response.Write(strResult)
                    Response.ContentType = "application/json"
                    Response.StatusCode = "200"
                Else
                    Response.Write(Logger.GetJson(Logger.getStatus("invalid key")))
                    Response.ContentType = "application/json"
                    Response.StatusCode = "500"
                End If
            End If
        Catch ex As Exception
            Response.Write(Logger.GetJson(Logger.getStatus("internal server error: " & ex.Message.ToString)))
            Response.ContentType = "application/json"
            Response.StatusCode = "500"
        End Try

    End Sub



End Class