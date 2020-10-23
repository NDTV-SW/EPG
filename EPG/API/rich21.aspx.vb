Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class rich21
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strAPIkey As String = Request.QueryString("apikey")

        If strAPIkey = "E5FA907176D4DCC637C884790EB92831" Then
            Dim obj As New clsExecute
            Dim dt As DataTable
            dt = obj.executeSQL("select * from richmeta where id in( select distinct richmetaid from mst_program where progid in (select distinct progid from mst_epg where progdate>getdate()-1))", False)

            Dim strResult As String = Logger.GetJson(dt)
            Response.Write(strResult)
        Else
            Response.Write("Invalid Key")
        End If
    End Sub



End Class