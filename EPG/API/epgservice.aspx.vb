Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class epgservice
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim vChannel As String = ""
        Try
            If Request.RequestType = "GET" Then
                Dim apikey As String = Request.Headers("apikey")
                If apikey = "E5FA907176D4DCC637C884790EB92831" Then
                    Dim strChannelid As String = Request.Headers("channelid")
                    Dim strServiceId As String = Request.Headers("serviceid")
                    Dim dStartdate As DateTime = Request.Headers("startdate")
                    Dim dEndDate As DateTime = Request.Headers("enddate")
                    Dim intChannelno As String = Request.Headers("channelno")
                    Dim strType As String = Request.QueryString("type")
                    Dim boolSynopsis As Boolean = IIf(IsNothing(Request.QueryString("synopsis")), 1, Request.QueryString("synopsis"))
                    Dim boolTag As Boolean = IIf(IsNothing(Request.QueryString("tag")), 0, Request.QueryString("tag"))
                    Dim boolChannelPrice As Boolean = IIf(IsNothing(Request.QueryString("price")), 0, Request.QueryString("price"))
                    Dim strTZ As String = IIf(IsNothing(Request.QueryString("tz")), "ist", Request.QueryString("tz"))

                    If IsNothing(boolSynopsis) Then
                        boolSynopsis = 1
                    End If


                    If IsNothing(strTZ) Then
                            strTZ = "ist"
                    Else
                        If (strType = "xmltv" Or strType = "txt1") And strTZ = "ist" Then
                            strTZ = "0"
                        End If
                    End If

                    vChannel = strChannelid


                    Dim obj As New clsExecute
                    Dim objEPGService As New clsEPGService
                    Dim dt As DataTable

                    Dim strSql As String
                    strSql = "select * from fn_consolidatedepg ('" & strChannelid & "','" & DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") & "','" & DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") & "') order by sortby"

                    Dim strResult As String = ""

                    If IsNothing(HttpContext.Current.Cache(strChannelid)) Then
                        dt = obj.executeSQL(strSql, False)
                        HttpContext.Current.Cache(strChannelid) = dt

                        Dim dtExpireTime As DateTime = DateTime.Now.Date & " 23:55:00"
                        Dim intExpireMins As Integer = DateDiff(DateInterval.Minute, DateTime.Now, dtExpireTime)

                        HttpContext.Current.Cache.Insert(strChannelid, dt, Nothing, DateTime.Now.AddMinutes(intExpireMins), System.Web.Caching.Cache.NoSlidingExpiration)
                    Else
                        dt = HttpContext.Current.Cache(strChannelid)
                    End If

                    If strType = "xmltv" Then
                        strResult = objEPGService.gen_XMLTV_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis, boolChannelPrice, strTZ)
                    ElseIf strType = "dvb" Then
                        strResult = objEPGService.gen_DVB_XML(dt, strChannelid, strServiceId, dStartdate, dEndDate, boolSynopsis, strTZ, boolChannelPrice)
                    ElseIf strType = "dvb-psi" Then
                        strResult = objEPGService.gen_DVB_PSI_XML(dt, strChannelid, strServiceId, dStartdate, dEndDate, boolSynopsis, strTZ, boolChannelPrice)
                        '--exec sp_epg_xml_standard2_psi 242,'STAR PLUS','2019-01-21','2019-01-21','STAR PLUS'
                    ElseIf strType = "barrowa" Then
                        strResult = objEPGService.gen_Barrowa_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "barrowabex" Then
                        strResult = objEPGService.gen_BarrowaBex_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "basicimport" Then
                        strResult = objEPGService.gen_BasicImport_XML(dt, strChannelid, strServiceId, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "broadcastdata" Then
                        strResult = objEPGService.gen_BroadcastData_XML(dt, strChannelid, strServiceId, dStartdate, dEndDate, boolSynopsis, boolTag, strTZ)
                    ElseIf strType = "listings" Then
                        strResult = objEPGService.gen_Listings_XML(dt, strChannelid, strServiceId, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "programguide" Then
                        strResult = objEPGService.gen_ProgramGuide_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis, boolChannelPrice, strTZ)
                    ElseIf strType = "medianet" Then
                        strResult = objEPGService.gen_MediaNet_XML(dt, strChannelid, strServiceId, dStartdate, dEndDate, boolSynopsis, strTZ)
                    ElseIf strType = "cepg" Then ' for INDEPENDENT TV
                        strResult = objEPGService.gen_CEPG_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis, strTZ)

                    ElseIf strType = "sgi" Then
                        strResult = objEPGService.gen_SGI(dt, strServiceId, dStartdate, dEndDate, boolSynopsis)

                    ElseIf strType = "txt1" Then
                        strResult = objEPGService.gen_TXT1(dt, dStartdate, dEndDate, boolSynopsis, strTZ)
                    ElseIf strType = "txt2" Then
                        strResult = objEPGService.gen_TXT2(dt, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "txt3-bcdate" Then
                        strResult = objEPGService.gen_TXT3_BCDATE(dt, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "txt4-iptv" Then
                        strResult = objEPGService.gen_TXT4_IPTV(dt, strServiceId, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "txt5" Then
                        strResult = objEPGService.gen_TXT5(dt, strServiceId, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "psc" Then
                        strResult = objEPGService.gen_PSC(dt, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "sdf" Then
                        strResult = objEPGService.gen_SDF(dt, strServiceId, dStartdate, dEndDate, boolSynopsis)
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