Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class prg21
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strAPIkey As String = Request.QueryString("apikey")
        Dim strChannelid As String = Request.QueryString("channel")

        If strAPIkey = "E5FA907176D4DCC637C884790EB92831" Then
            Dim obj As New clsExecute
            Dim dt As DataTable
            dt = obj.executeSQL("select * from mst_program where  progid in (select distinct progid from mst_epg where channelid='" & strChannelid & "' and progdate>getdate()-1) and channelid='" & strChannelid & "'", False)

            Dim strResult As String = Logger.GetJson(dt)
            Response.Write(strResult)
        Else
            Response.Write("Invalid Key")
        End If
    End Sub



End Class