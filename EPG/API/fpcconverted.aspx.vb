Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class fpcconverted
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim apikey As String = Request.Headers("apikey")
           If Request.RequestType = "POST" Then
                If apikey = "IjNZwEZXbOygxlL/HhktSA==" Then
                    Dim ingestid As String = Request.Params("ingestid")
                    Dim fpcfilename As String = Request.Params("fpcfilename")
                    Dim obj As New clsExecute
                    obj.executeSQL("insert into fpc_converted(ingestid,fpcfilename) values('" & ingestid & "','" & fpcfilename & "')", False)
                    Dim strResult As String = Logger.GetJson(Logger.getStatus("record inserted"))
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