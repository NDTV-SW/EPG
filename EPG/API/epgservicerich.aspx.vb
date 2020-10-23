Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class epgservicerich
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim vChannel As String = ""

        Try
            If Request.RequestType = "GET" Then
                Dim apikey As String = Request.Headers("apikey")
                If apikey = "E5FA907176D4DCC637C884790EB92831" Then
                    Dim strChannelid As String = Request.Headers("channelid")
                    Dim strServiceId As String = Request.Headers("serviceid")
                    Dim strOperatorChannel As String = Request.Headers("operatorchannelid")
                    Dim dStartdate As DateTime = Request.Headers("startdate")
                    Dim dEndDate As DateTime = Request.Headers("enddate")
                    Dim intChannelno As String = Request.Headers("channelno")
                    Dim strType As String = Request.QueryString("type")
                    Dim strTZ As String = Request.QueryString("tz")

                    vChannel = strChannelid

                    Dim obj As New clsExecute
                    Dim objEPGService As New clsEPGService
                    Dim dt As DataTable

                    Dim strSql As String
                    If dEndDate > DateTime.Now.AddDays(7) Then
                        strSql = "select * from fn_consolidatedepg_rich ('" & strChannelid & "','" & DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") & "','" & dEndDate.ToString("yyyy-MM-dd") & "') order by sortby"
                        If Not IsNothing(HttpContext.Current.Cache(strChannelid & "_rich")) Then
                            HttpContext.Current.Cache.Remove(strChannelid & "_rich")
                        End If

                    Else
                        strSql = "select * from fn_consolidatedepg_rich ('" & strChannelid & "','" & DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") & "','" & DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") & "') order by sortby"

                    End If


                    Dim strResult As String = ""
                    If IsNothing(HttpContext.Current.Cache(strChannelid & "_rich")) Then
                        dt = obj.executeSQL(strSql, False)
                        HttpContext.Current.Cache(strChannelid & "_rich") = dt

                        Dim dtExpireTime As DateTime = DateTime.Now.Date & " 23:55:00"
                        Dim intExpireMins As Integer = DateDiff(DateInterval.Minute, DateTime.Now, dtExpireTime)

                        HttpContext.Current.Cache.Insert(strChannelid & "_rich", dt, Nothing, DateTime.Now.AddMinutes(intExpireMins), System.Web.Caching.Cache.NoSlidingExpiration)
                    Else
                        dt = HttpContext.Current.Cache(strChannelid & "_rich")
                    End If

                    If strType = "xml" Then
                        strResult = objEPGService.gen_rich_XML(dt, strChannelid, strServiceId, strOperatorChannel, intChannelno, dStartdate, dEndDate, strTZ)
                    ElseIf strType = "actxml" Then
                        strResult = objEPGService.gen_richACT_XML(dt, strChannelid, strServiceId, strOperatorChannel, intChannelno, dStartdate, dEndDate, strTZ)
                    ElseIf strType = "xmltv" Then
                        strResult = objEPGService.gen_richXMLTV_XML(dt, strChannelid, strServiceId, strOperatorChannel, intChannelno, dStartdate, dEndDate, strTZ)
                    ElseIf strType = "barrowaxml" Then
                        strResult = objEPGService.gen_BarrowaRich_XML(dt, strChannelid, strServiceId, strOperatorChannel, intChannelno, dStartdate, dEndDate, strTZ)
                    ElseIf strType = "synergyxml" Then
                        strResult = objEPGService.gen_SynergyRich_XML(dt, strChannelid, strServiceId, strOperatorChannel, intChannelno, dStartdate, dEndDate, strTZ)
                    ElseIf strType = "medianet" Then
                        strResult = objEPGService.gen_MediaNetRich_XML(dt, strChannelid, strServiceId, dStartdate, dEndDate, strTZ)
                    ElseIf strType = "osntxt" Then
                        strResult = objEPGService.gen_OSN_TXT(dt, strServiceId, dStartdate, dEndDate, strTZ, True)
                    End If
                    '
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