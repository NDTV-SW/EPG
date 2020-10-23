Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class MissingInfoOnChannels
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

        strSql = "SELECT a.ChannelId,b.ProgName, CONVERT(VARCHAR,a.ProgDate,106) as airdate,"
        strSql = strSql & " CONVERT(VARCHAR,a.ProgTime,108) as airtime FROM mst_epg a JOIN mst_program b ON a.ProgID=b.ProgID"
        strSql = strSql & " WHERE a.ChannelId='" & ddlChannel.SelectedValue & "'"
        strSql = strSql & " AND b.ProgName LIKE '" & ddlProgramme.SelectedItem.Text & "%'"
        strSql = strSql & " AND CONVERT(VARCHAR,a.ProgDate,112)>=CONVERT(VARCHAR,dbo.GetLocalDate(),112)"
        strSql = strSql & " ORDER BY a.ProgDate,a.ProgTime"

        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL(strSql, False)

            grdData.DataSource = dt
            grdData.DataBind()
        Catch
        End Try
    End Sub
End Class