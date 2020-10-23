Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class epgservicesingle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim vChannel As String = ""

        Try
            If Request.RequestType = "GET" Then
                Dim apikey As String = Request.Headers("apikey")
                If apikey = "E5FA907176D4DCC637C884790EB92831" Then
                    'Dim strChannelid As String = Request.Headers("channelid")
                    'Dim strServiceId As String = Request.Headers("serviceid")
                    Dim strOpName As String = Request.Headers("opname")
                    Dim dStartdate As DateTime = Request.Headers("startdate")
                    Dim dEndDate As DateTime = Request.Headers("enddate")
                    Dim intOperatorId As String = Request.Headers("operatorid")
                    Dim strType As String = Request.QueryString("type")
                    Dim boolSynopsis As Boolean = IIf(IsNothing(Request.QueryString("synopsis")), 1, Request.QueryString("synopsis"))
                    Dim strTZ As String = IIf(IsNothing(Request.QueryString("tz")), "ist", Request.QueryString("tz"))

                    If IsNothing(boolSynopsis) Then
                        boolSynopsis = 1
                    End If

                    If IsNothing(strTZ) Then
                        strTZ = "ist"
                    End If

                    Dim obj As New clsExecute
                    Dim objEPGService As New clsEPGService
                    Dim dt As DataTable

                    Dim strSql As String
                    strSql = "select * from fn_consolidatedepg_single ('" & intOperatorId & "','" & DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") & "','" & DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") & "') order by serviceid,startdatetime"

                    Dim strResult As String = ""

                    dt = obj.executeSQL(strSql, False)


                    If strType = "xmltv" Then
                        strResult = objEPGService.gen_XMLTV_Single_XML(dt, dStartdate, dEndDate, strTZ)
                    ElseIf strType = "xmltv1" Then
                        strResult = objEPGService.gen_XMLTV_Single1_XML(dt, dStartdate, dEndDate, strTZ)
                    ElseIf strType = "csvchannels" Then
                        strResult = objEPGService.gen_Single_CSV(dt, dStartdate, dEndDate, boolSynopsis, 1)
                    ElseIf strType = "csvepg" Then
                        strResult = objEPGService.gen_Single_CSV(dt, dStartdate, dEndDate, boolSynopsis, 0)
                    ElseIf strType = "csvepg2" Then
                        strResult = objEPGService.gen_Single_CSV2(dt, dStartdate, dEndDate, boolSynopsis)
                    End If

                    Response.Clear()
                    Response.Write(strResult)

                    If strType.StartsWith("txt") Or strType = "psc" Or strType = "sdf" Or strType = "csv" Then
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