Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Public Class serveimageDefaultRoute
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim strId As String = TryCast(Page.RouteData.Values("id"), String)
            Dim strFormat As String = TryCast(Page.RouteData.Values("format"), String)
            Dim intWidth As Integer = TryCast(Page.RouteData.Values("width"), String)
            Dim intHeight As Integer = TryCast(Page.RouteData.Values("height"), String)
            Dim strOrientation As String = TryCast(Page.RouteData.Values("orientation"), String)
            Dim strType As String = TryCast(Page.RouteData.Values("type"), String)


            Dim temp As String = Logger.convertHexToString(strId.Replace(".jpeg", "").Replace(".jpg", "").Replace(".png", ""))
            '363330393535

            If strType = "channel" Then
                imgServe(temp, strId, strFormat, intWidth, intHeight, strOrientation, strType)
            ElseIf strType = "channelid" Then
                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL("select channelid from mst_channel where rowid='" & temp & "'", False)
                imgServe(dt.Rows(0)(0).ToString, strId, strFormat, intWidth, intHeight, strOrientation, strType)
            Else
                Dim obj As New clsExecute
                Dim sql As String
                If intWidth < intHeight Then
                    strOrientation = "portrait"
                End If
                If strOrientation = "portrait" Then
                    sql = "select dbo.fn_getlogonames_progid_portrait_act1('" & temp & "',null, 1, NULL) programlogo"
                Else
                    sql = "select dbo.fn_getlogonames_progid_act('" & temp & "',null, 1, NULL) programlogo"
                End If

                Dim dt As DataTable = obj.executeSQL(sql, False)
                imgServe(dt.Rows(0)(0).ToString, strId, strFormat, intWidth, intHeight, strOrientation, strType)
            End If


        Catch ex As Exception
            Dim intWidth As String = TryCast(Page.RouteData.Values("width"), String)
            Dim intHeight As String = TryCast(Page.RouteData.Values("height"), String)

            Dim strPath As String
            strPath = "~/serveimage"

            Dim oBitmap As New Bitmap(Path.Combine(Server.MapPath(strPath), "noimage.png"))

            Dim oBitmap1 As New Bitmap(oBitmap, New Size(intWidth, intHeight))


            Response.ContentType = "image/png"
            oBitmap1.Save(Response.OutputStream, ImageFormat.Png)
            oBitmap1.Dispose()
            'End If

            'Response.Write(Logger.GetJson(Logger.getStatus("internal server error: " & ex.Message.ToString)))
            'Response.ContentType = "application/json"
            'Response.StatusCode = "500"
        End Try
    End Sub

    Private Sub imgServe(proglogo As String, vId As String, format As String, width As Integer, height As Integer, orientation As String, type As String)
        Dim strPath As String
        If type = "channel" Then
            If width = height Then
                strPath = "~/uploads/channellogos/square"
            Else
                strPath = "~/uploads/channellogos/160x90"
            End If
            proglogo = proglogo & "." & format.Replace("jpeg", "png")
        Else
            If width < height Then
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



End Class