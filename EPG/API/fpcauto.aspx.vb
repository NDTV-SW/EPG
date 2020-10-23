Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class fpcauto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.RequestType = "GET" Then
                Dim apikey As String = Request.Headers("apikey")
                Dim currstatus As String = Request.QueryString("status")
                If apikey = "IjNZwEZXbOygxlL/HhktSA==" Then
                    Dim obj As New clsExecute
                    Dim dt As DataTable = obj.executeSQL("select ROW_NUMBER()  OVER(ORDER BY insertedat) rowid, id,channelid,attachmentname,currstatus,convert(varchar,insertedat,111) + ' ' + convert(varchar,insertedat,108) insertedat ,attachmentprocessed,formatprocessed,ready,readyupdatedby,readyupdatedat,readytoupload,readytouploadat,uploaded,uploadedat,mailreceivedby,mailsubject from fpc_mailattachmentingest where ready=1 and currstatus='" & currstatus & "' and s3uploaded=1", False)
                    Dim strResult As String = Logger.GetJson(dt)
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