Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ep21
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strAPIkey As String = Request.QueryString("apikey")
        Dim strChannelid As String = Request.QueryString("channel")
        Dim strLanguageid As String = Request.QueryString("language")
        Dim strdate As String = Request.QueryString("date")
        Dim strDays As String = Request.QueryString("days")
        Dim strtype As String = Request.QueryString("type")

        If strAPIkey = "E5FA907176D4DCC637C884790EB92831" Then


            Dim obj As New clsExecute
            Dim dt As DataTable
            If strtype = "1" Then
                dt = obj.executeSQL("select * from fn_viewepg ('" & strChannelid & "','" & strLanguageid & "','" & Convert.ToDateTime(strdate).ToString("yyyy-MM-dd") & "') order by sortby", False)
            Else
                dt = obj.executeSQL("sp_export_exportepg_toexcel", "ChannelId~fromDate~ToDate", "VarChar~DateTime~DateTime", strChannelid & "~" & Convert.ToDateTime(strdate).Date & "~" & Convert.ToDateTime(strdate).AddDays(strDays).Date, True, False)
            End If

            Dim strResult As String = Logger.GetJson(dt)
            Response.Write(strResult)
        Else
            Response.Write("Invalid Key")
        End If
    End Sub

   

End Class