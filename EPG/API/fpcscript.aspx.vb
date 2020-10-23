Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class fpcscript
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim apikey As String = Request.Headers("apikey")
            If Request.RequestType = "GET" Then

                Dim channelid As String = Request.QueryString("channelid")
                If apikey = "IjNZwEZXbOygxlL/HhktSA==" Then
                    Dim obj As New clsExecute
                    Dim dt As DataTable = obj.executeSQL("select id,channelid,scriptid from fpc_channelformatmapping where channelid='" & channelid & "'", False)
                    Dim strResult As String = Logger.GetJson(dt)
                    Response.Write(strResult)
                    Response.ContentType = "application/json"
                    Response.StatusCode = "200"
                Else
                    Response.Write(Logger.GetJson(Logger.getStatus("invalid key")))
                    Response.ContentType = "application/json"
                    Response.StatusCode = "500"
                End If
            ElseIf Request.RequestType = "POST" Then
                If apikey = "IjNZwEZXbOygxlL/HhktSA==" Then
                    Dim vChannelId As String = Request.Params("channelid")
                    Dim scriptid As String = Request.Params("scriptid")
                    Dim obj As New clsExecute
                    Dim dt As DataTable = obj.executeSQL("select * from fpc_channelformatmapping where channelid='" & vChannelId & "'", False)
                    If dt.Rows.Count > 0 Then
                        obj.executeSQL("update fpc_channelformatmapping set scriptid='" & scriptid & "' where channelid='" & vChannelId & "'", False)
                    Else
                        obj.executeSQL("insert into fpc_channelformatmapping(channelid,scriptid,lastupdate) values('" & vChannelId & "','" & scriptid & "',dbo.getlocaldate())", False)
                    End If
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