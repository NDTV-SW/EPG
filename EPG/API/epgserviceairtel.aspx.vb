Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class epgserviceairtel
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
                    Dim strType As String = Request.QueryString("type")
                    
                    
                    vChannel = strChannelid


                    Dim obj As New clsExecute
                    Dim objEPGService As New clsEPGService
                    Dim dt As DataTable

                    Dim strSql As String
                    'strSql = "select dbo.fn_epg_sdf_airtel2(208,'" & strChannelid & "'','" & dStartdate & "','" & dEndDate & "','" & strServiceId & "')"
                    strSql = "select dbo.[fn_epg_sdf_airtel_new] (208,'" & strChannelid & "','" & dStartdate.Date & "','" & strServiceId & "')"
                    'strSql = "select * from fn_consolidatedepg ('" & strChannelid & "','" & DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") & "','" & DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") & "') order by sortby"
                    dt = obj.executeSQL(strSql, False)

                    Dim strResult As String = dt.Rows(0)(0).ToString

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