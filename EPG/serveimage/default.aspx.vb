Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Public Class serveimageDefault
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
            imgServeDefault()


        Catch ex As Exception
            Response.Write(Logger.GetJson(Logger.getStatus("internal server error: " & ex.Message.ToString)))
            Response.ContentType = "application/json"
            Response.StatusCode = "500"
        End Try
    End Sub

    Private Sub imgServe(proglogo As String, format As String, width As Integer, height As Integer, orientation As String, type As String)
        Dim strPath As String
        If type = "channel" Then
            If width = height Then
                strPath = "~/uploads/channellogos/square"
            Else
                strPath = "~/uploads/channellogos/160x90"
            End If

        Else
            If orientation = "portrait" Then
                strPath = "~/uploads/portrait"
            Else
                strPath = "~/uploads"
            End If
        End If
        Dim oBitmap As New Bitmap(Path.Combine(Server.MapPath(strPath), proglogo))

        Dim oBitmap1 As New Bitmap(oBitmap, New Size(width, height))


        Response.ContentType = "image/" & format
        If type = "channel" Then
            oBitmap1.Save(Response.OutputStream, ImageFormat.Png)
        Else
            oBitmap1.Save(Response.OutputStream, ImageFormat.Jpeg)
        End If
        oBitmap1.Dispose()
        'End If

    End Sub

    Private Sub imgServeDefault()
        Dim strPath As String
        strPath = "~/serveimage"

        Dim oBitmap As New Bitmap(Path.Combine(Server.MapPath(strPath), "noimage.png"))

        Dim oBitmap1 As New Bitmap(oBitmap, New Size(204, 204))


        Response.ContentType = "image/png"
        oBitmap1.Save(Response.OutputStream, ImageFormat.Png)
        oBitmap1.Dispose()
        'End If

    End Sub


End Class