Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class epgservicesinglerich
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
                    Dim strData As String = Request.QueryString("data")
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

                    'NOT BEING USED FOR DHIRAAGU. THIS IS BEING HANDLED IN THE NDTVCABLEEPG APPLICATION
                    Dim strSql As String
                    strSql = "select * from fn_consolidatedepg_rich_single_dhiraagu1 ('" & intOperatorId & "','" & dStartdate.ToString("yyyy-MM-dd") & "','" & dEndDate.ToString("yyyy-MM-dd") & "') order by serviceid,startdatetime"

                    Dim strResult As String = ""


                    If IsNothing(HttpContext.Current.Cache("singlerich_" & intOperatorId)) Then
                        dt = obj.executeSQL(strSql, False)
                        HttpContext.Current.Cache("singlerich_" & intOperatorId) = dt

                        Dim dtExpireTime As DateTime = DateTime.Now.AddMinutes(60)
                        Dim intExpireMins As Integer = DateDiff(DateInterval.Minute, DateTime.Now, dtExpireTime)

                        HttpContext.Current.Cache.Insert("singlerich_" & intOperatorId, dt, Nothing, DateTime.Now.AddMinutes(intExpireMins), System.Web.Caching.Cache.NoSlidingExpiration)
                    Else
                        dt = HttpContext.Current.Cache("singlerich_" & intOperatorId)
                    End If


                    Dim finalString As String = ""


                    If strType = "generic" Then

                        'strData = "sources"
                        'strResult = objEPGService.gen_Generic_Single_Rich_XML(dt, dStartdate, dEndDate, strData, strTZ)
                        strResult = objEPGService.gen_Generic_Single_Rich_XML(dt, dStartdate, dEndDate, "channels", strTZ)
                        '--------------------Channels------------------------
                        Dim path As String
                        path = Server.MapPath("../XML_Dhiraagu/")
                        Dim filename As String = path & "channels.xml"
                        Dim destFileName As String = path & "channels.xml.gz"
                        WriteFile(filename, strResult)
                        Logger.CompressGzFile(filename, destFileName)
                        Dim _FileInfo As New System.IO.FileInfo(destFileName)
                        finalString = finalString & destFileName & "~"

                        If strData = "programs" Then
                            '--------------------Programs------------------------
                            strResult = objEPGService.gen_Generic_Single_Rich_XML(dt, dStartdate, dEndDate, "programs", strTZ)
                            filename = path & "programs.xml"
                            destFileName = path & "programs.xml.gz"
                            WriteFile(filename, strResult)
                            Logger.CompressGzFile(filename, destFileName)
                            _FileInfo = New System.IO.FileInfo(destFileName)
                            'finalString = finalString & destFileName & "~"
                            finalString = finalString & destFileName
                        End If
                        If strData = "sources" Then
                            '--------------------Sources------------------------
                            strResult = objEPGService.gen_Generic_Single_Rich_XML(dt, dStartdate, dEndDate, "sources", strTZ)
                            filename = path & "schedules.xml"
                            destFileName = path & "schedules.xml.gz"
                            WriteFile(filename, strResult)
                            Logger.CompressGzFile(filename, destFileName)
                            _FileInfo = New System.IO.FileInfo(destFileName)
                            finalString = finalString & destFileName

                        End If
                    End If

                    Response.Clear()
                    Response.Write(finalString)
                    Response.ContentType = "text/plain"
                    Response.StatusCode = "200"
                Else
                    Response.Write(Logger.GetJson(Logger.getStatus("invalid key")))
                    Response.ContentType = "text/xml"
                    Response.StatusCode = "500"
                End If
            End If
        Catch ex As Exception
            Response.Write(Logger.GetXML(Logger.getStatus(vChannel & ": internal server error: " & ex.Message.ToString & ". <br/>Inner Exception:" & ex.InnerException.Message.ToString)))
            Response.ContentType = "application/json"
            Response.StatusCode = "500"
        End Try

    End Sub

    Private Shared Function WriteFile(ByVal FileName As String, ByVal FileData As String) As Integer
        WriteFile = 0
        Dim outStream As StreamWriter
        outStream = New StreamWriter(FileName, False)
        outStream.Write(FileData)
        outStream.Close()
    End Function

End Class