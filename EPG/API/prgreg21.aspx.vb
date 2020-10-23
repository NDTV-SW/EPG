Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class prgreg21
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strAPIkey As String = Request.QueryString("apikey")
        Dim strChannelid As String = Request.QueryString("channel")
        Dim strLanguageid As String = Request.QueryString("language")

        If strAPIkey = "E5FA907176D4DCC637C884790EB92831" Then
            Dim obj As New clsExecute
            Dim dt As DataTable
            Dim sql As String = ""
            sql = sql & "select * from mst_programregional where progid in (select progid from mst_program where episodicSynopsis=0 and  progid in (select progid from mst_epg where channelid='" & strChannelid & "' and progdate>getdate()-1) and  channelid='" & strChannelid & "') and episodeNo=0 and languageid='" & strLanguageid & "' union select * from mst_programregional where progid in (select progid from mst_program where episodicSynopsis=1 and  progid in (select progid from mst_epg where channelid='" & strChannelid & "' and progdate>getdate()-1) and  channelid='" & strChannelid & "') and languageid='" & strLanguageid & "'"
            dt = obj.executeSQL(sql, False)

            Dim strResult As String = Logger.GetJson(dt)
            Response.Write(strResult)
        Else
            Response.Write("Invalid Key")
        End If
    End Sub



End Class