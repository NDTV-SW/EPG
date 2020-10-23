Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml


Public Class testJSON
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strChannel As String = Request.QueryString("channel")
        Dim obj As New clsExecute
        Dim dt As New DataTable(tableName:="MyTableName")

        Dim intOperatorid As Integer = 214
        Dim dtChannel As DataTable = obj.executeSQL("select serviceid from dthcable_channelmapping where operatorid='" & intOperatorid & "' and onair=1 order by channelid desc", False)

        For i As Integer = 0 To dtChannel.Rows.Count - 1
            dt = obj.executeSQL("select channel_no channelno,channeldesc,serviceid,channelid,channelgenre,convert(varchar,progdate,106) progstartdate,convert(varchar,progenddate,106) progenddate,progstarttime,progendtime,duration,progname,synopsis,episodic_title episodictitle,episodic_synopsis episodicsynopsis,channellogo,programlogo,language,showtype,genre,subgenre,episodeno,starcast,director,release_year releaseyear,writer,is_live islive,episodetype,team1,team2,league,timezone from [dbo].[api_fn_get_richdata_utc]  ('" & intOperatorid & "','" & dtChannel.Rows(i)("serviceid").ToString & "','" & DateTime.Now.ToString("yyyy-MM-dd") & "','" & DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") & "','utc') order by progstartdatetime", False)
            Dim strResult As String = Logger.GetXML(dt)

            Dim strString As String = "<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf
            Response.Write(strString & strResult)
            Response.ContentType = "text/xml"
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Response.StatusCode = "200"

            Dim path As String = ""
            path = Server.MapPath("ACT/")
            WriteFile(path & Regex.Replace(dtChannel.Rows(i)("serviceid").ToString, "[^0-9a-zA-Z]+", "") & DateTime.Now.ToString("yyMMdd") & DateTime.Now.AddDays(6).ToString("yyMMdd") & ".xml", strString & strResult)
        Next
        

    End Sub

    Private Shared Function WriteFile(ByVal FileName As String, ByVal FileData As String) As Integer
        WriteFile = 0
        Dim outStream As StreamWriter
        outStream = New StreamWriter(FileName, False)
        outStream.Write(FileData)
        outStream.Close()
    End Function


End Class