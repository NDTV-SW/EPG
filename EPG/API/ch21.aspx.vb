Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ch21
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strAPIkey As String = Request.QueryString("apikey")
        If strAPIkey = "E5FA907176D4DCC637C884790EB92831" Then
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select ROW_NUMBER()  OVER(ORDER BY channelid) rowid,channelid, genre=(select genrename from mst_genre where genreid=x.genreid) ,airtelftp,airtelmail, epgtill=(select convert(varchar,max(progdate),101) from mst_epg where channelid=x.channelid) from mst_channel x where onair=1 and active=1 and sendepg=1 order by channelid", False)
            Dim strResult As String = Logger.GetJson(dt)
            Response.Write(strResult)
        Else
            Response.Write("Invalid Key")
        End If
    End Sub

End Class