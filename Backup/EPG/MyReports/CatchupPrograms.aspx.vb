Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class CatchupPrograms
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL("select progid,ChannelId,ProgName,convert(varchar,showairtime,108) AirTime from mst_program where catchupflag=1 and ChannelId in (select ChannelId from mst_channel where (airtelFTP=1 or AirtelMail=1)) order by 2,3,4", False)
                grdReport.DataSource = dt
                grdReport.DataBind()
            End If
        Catch ex As Exception
            Logger.LogError("CatchupPrograms", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub




End Class