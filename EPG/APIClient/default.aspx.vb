Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class defaultApiClient
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim vChannel As String = ""
        Try
            If Request.RequestType = "GET" Then
                Dim apikey As String = Request.Headers("apikey")
                Dim obj As New clsExecute
                Dim objEPGService As New clsEPGService
                Dim dt As DataTable
                Dim strResult As String = ""
                If apikey = "244-UdDYweWeNOhbWTD2Y2yDoW3XHIcyZOzG" Then

                    Dim intOperatorid As Integer = apikey.Split("-")(0).ToString
                    Dim strType As String = Request.QueryString("type")
                    If strType = "epg" Then
                        Dim strServiceId As String = Request.Headers("channel")
                        Dim strTZ As String = Request.QueryString("tz")

                        Dim strChannelid As String = ""
                        Dim intChannelno As String = 0

                        Dim dtChannel As DataTable = obj.executeSQL("select * from dthcable_channelmapping where operatorid='" & intOperatorid & "' and operatorchannelid='" & strServiceId & "' and onair=1", False)
                        If dtChannel.Rows.Count > 0 Then
                            strChannelid = dtChannel.Rows(0)("channelid").ToString
                            intChannelno = dtChannel.Rows(0)("channelno").ToString
                        End If
                        vChannel = strChannelid
                        Dim strSql As String
                        strSql = "select * from fn_consolidatedepg_rich ('" & strChannelid & "','" & DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") & "','" & DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") & "') order by sortby"

                        If IsNothing(HttpContext.Current.Cache(strChannelid & "_rich")) Then
                            dt = obj.executeSQL(strSql, False)
                            HttpContext.Current.Cache(strChannelid & "_rich") = dt

                            Dim dtExpireTime As DateTime = DateTime.Now.Date & " 23:55:00"
                            Dim intExpireMins As Integer = DateDiff(DateInterval.Minute, DateTime.Now, dtExpireTime)

                            HttpContext.Current.Cache.Insert(strChannelid & "_rich", dt, Nothing, DateTime.Now.AddMinutes(intExpireMins), System.Web.Caching.Cache.NoSlidingExpiration)
                        Else
                            dt = HttpContext.Current.Cache(strChannelid & "_rich")
                        End If

                        strResult = objEPGService.gen_BarrowaRich_XML(dt, strChannelid, strServiceId, strServiceId, intChannelno, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(6).ToString("yyyy-MM-dd"), strTZ)
                    ElseIf strType = "channellist" Then

                        dt = obj.executeSQL("select serviceid from dthcable_channelmapping where operatorid=" & intOperatorid & " and onair=1 order by channelid", False)
                        strResult = objEPGService.get_ClientChannels_XML(intOperatorid, 1, dt)
                    ElseIf strType = "recent" Then

                        dt = obj.executeSQL("select serviceid,lastupdate from api_get_channel_list(" & intOperatorid & ") where lastupdate is not null and  lastupdate >dateadd(mi,-20,dbo.getlocaldate()) order by lastupdate desc", False)
                        strResult = objEPGService.get_ClientChannels_XML(intOperatorid, 0, dt)
                    End If

                    Response.Clear()
                    Response.Write(strResult)

                    If strType.StartsWith("txt") Or strType = "psc" Or strType = "sdf" Then
                        Response.ContentType = "text/plain"
                    Else
                        Response.ContentType = "text/xml"
                    End If
                    Response.StatusCode = "200"
                Else
                    Response.Write(Logger.GetJson(Logger.getStatus("invalid key")))
                    Response.ContentType = "text/xml"
                    Response.StatusCode = "500"
                End If
            End If
        Catch ex As Exception
            Response.Write(Logger.GetXML(Logger.getStatus(vChannel & ": internal server error: " & ex.Message.ToString)))
            Response.ContentType = "application/json"
            Response.StatusCode = "500"
        End Try

    End Sub



End Class