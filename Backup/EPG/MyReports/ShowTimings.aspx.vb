Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ShowTimings
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("Images Report", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim strSql As String
        Dim strDateQ As String = ""
        If txtDate.Text.Trim <> "" Then
            strDateQ = " and convert(varchar, progdate,112)='" & Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd") & "' "
        End If
        If rbProgId.Checked Then
            strSql = "select b.ProgId ID,b.ChannelId Channel,b.ProgName Programme,convert(varchar,a.ProgDate,106) Date,convert(varchar,a.ProgTime,108) Time,datename(dw,a.ProgDate) Day,a.episodeno Episode from mst_epg a join mst_program b on a.progid=b.progid "
            strSql = strSql & " and a.ProgID in (" & txtProgIds.Text & ") and a.progdate > dbo.GetLocalDate()-7 "
            strSql = strSql & strDateQ
        ElseIf rbProgName.Checked Then
            strSql = "select b.ProgId ID,b.ChannelId Channel,b.ProgName Programme,convert(varchar,a.ProgDate,106) Date,convert(varchar,a.ProgTime,108) Time,datename(dw,a.ProgDate) Day,a.episodeno Episode from mst_epg a join mst_program b on a.progid=b.progid "
            strSql = strSql & " and b.ProgName like '%" & txtProgIds.Text & "%' and a.progdate > dbo.GetLocalDate()-7 "
            strSql = strSql & strDateQ
        Else
            strSql = ""
        End If
        strSql = strSql & " order by 2,3,a.progdate,a.progtime"
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL(strSql, False)

            grdImagesReport.DataSource = dt
            grdImagesReport.DataBind()
        Catch
        End Try
    End Sub
End Class