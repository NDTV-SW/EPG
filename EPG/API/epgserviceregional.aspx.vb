Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class epgserviceregional
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

                    If IsNothing(boolSynopsis) Then
                        boolSynopsis = 1
                    End If

                    vChannel = strChannelid


                    Dim obj As New clsExecute
                    Dim objEPGService As New clsEPGService
                    Dim dt As DataTable

                    Dim strSql As String
                    ' '' ''   strSql = "select * from fn_consolidatedepg_regional_san ('" & strChannelid & "','" & DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") & "','" & DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") & "') order by sortby"
                    strSql = "select * from fn_consolidatedepg_regional ('" & strChannelid & "','" & DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") & "','" & DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") & "') order by sortby"

                    Dim strResult As String = ""

                    If IsNothing(HttpContext.Current.Cache(strChannelid & "_regional")) Then
                        dt = obj.executeSQL(strSql, False)
                        HttpContext.Current.Cache(strChannelid & "_regional") = dt

                        Dim dtExpireTime As DateTime = DateTime.Now.Date & " 23:55:00"
                        Dim intExpireMins As Integer = DateDiff(DateInterval.Minute, DateTime.Now, dtExpireTime)

                        HttpContext.Current.Cache.Insert(strChannelid & "_regional", dt, Nothing, DateTime.Now.AddMinutes(intExpireMins), System.Web.Caching.Cache.NoSlidingExpiration)
                    Else
                        dt = HttpContext.Current.Cache(strChannelid & "_regional")
                    End If

                    If strType = "dvbtamil" Then
                        strResult = objEPGService.gen_DVBTamil_XML(dt, strChannelid, strServiceId, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "barrowabexbengali" Then
                        strResult = objEPGService.gen_BarrowaBexBengali_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "barrowabexenglish" Then
                        strResult = objEPGService.gen_BarrowaBexEnglish_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "barrowabexenglishbengali" Then
                        strResult = objEPGService.gen_BarrowaBexEnglishBengali_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "barrowatamil" Then
                        strResult = objEPGService.gen_BarrowaTamil_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "barrowabengali" Then
                        strResult = objEPGService.gen_BarrowaBengali_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis)
                    ElseIf strType = "barrowakannada" Then
                        strResult = objEPGService.gen_BarrowaKannada_XML(dt, strChannelid, strServiceId, intChannelno, dStartdate, dEndDate, boolSynopsis)
                    End If


                    Response.Clear()
                    Response.Write(strResult)
                    If strType.StartsWith("txt") Or strType = "psc" Or strType = "sdf" Then
                        Response.ContentType = "text/plain"
                    Else
                        Response.ContentType = "text/xml"
                        Response.ContentEncoding = System.Text.Encoding.UTF8
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