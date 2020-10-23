Imports System
Imports System.Data.SqlClient
Public Class ServiceID
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Not (User.Identity.Name.ToLower = "hemant" Or User.Identity.Name.ToLower = "kautilyar" Or User.Identity.Name.ToLower = "sachint") Then
            'Response.Redirect("~/default.aspx")
            'End If
        Catch ex As Exception
            Logger.LogError("ServiceID", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Protected Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from mst_channel where channelid='" & txtChannel.Text & "'", False)
        obj.executeSQL("update mst_channel set serviceid='" & txtServiceID.Text & "',airtelftp='" & chkAirtelFTP.Checked & "',airtelmail='" & chkAirtelMail.Checked & "',channel_genre='" & ddlChannelGenre.SelectedValue.Replace("'", "''") & "' where channelid='" & txtChannel.Text & "'", False)

        Dim body As String = ""
        body = body & "<style>table {border-collapse: collapse; width='100%'} table, th, td {border: 1px solid black;} th, td {padding: 8px;}</style>"
        body = body & "<table><tr><th colspan='2'>"
        body = body & txtChannel.Text & " : has been updated.</th></tr>"
        body = body & "<tr><td><b>Old Serviceid</b></td><td>" & dt.Rows(0)("serviceid").ToString & "</td></tr>"
        body = body & "<tr><td><b>New Serviceid</b></td><td>" & txtServiceID.Text & "</td></tr>"
        body = body & "<tr><td><b>Old AirtelMail</b></td><td>" & dt.Rows(0)("airtelmail").ToString & "</td></tr>"
        body = body & "<tr><td><b>New AirtelMail</b></td><td>" & chkAirtelMail.Checked & "</td></tr>"
        body = body & "<tr><td><b>Old AirtelFTP</b></td><td>" & dt.Rows(0)("airtelFTP").ToString & "</td></tr>"
        body = body & "<tr><td><b>New AirtelFTP</b></td><td>" & chkAirtelFTP.Checked & "</td></tr>"
        body = body & "<tr><td><b>Old Channel Genre</b></td><td>" & dt.Rows(0)("channel_genre").ToString & "</td></tr>"
        body = body & "<tr><td><b>New Channel Genre</b></td><td>" & ddlChannelGenre.SelectedValue & "</td></tr>"
        body = body & "<tr><td><b>Updated by</b></td><td>" & User.Identity.Name & "</td></tr><table>"
        Logger.mailMessage("epg@ndtv.com ", "Channel ServiceID / Details updated", body, "epgtech@ndtv.com", "")
        clearall()

    End Sub

    
    Private Sub bindGrid()
        If txtChannel.Text.Trim <> "" Then

            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select ChannelId,ServiceId,Airtelmail,AirtelFTP,channel_genre from mst_channel where channelid like '%" & txtChannel.Text & "%' or serviceid like '%" & txtChannel.Text & "%'", False)
            grd.SelectedIndex = -1
            grd.DataSource = dt
            grd.DataBind()
        End If
    End Sub
    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        bindGrid()
    End Sub

    
    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Dim lbChannelid As Label = TryCast(grd.Rows(grd.SelectedIndex).FindControl("lbChannelid"), Label)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from mst_channel where channelid='" & lbChannelid.Text & "'", False)
        txtChannel.Text = dt.Rows(0)("channelid").ToString
        txtServiceID.Text = dt.Rows(0)("serviceID").ToString
        chkAirtelFTP.Checked = dt.Rows(0)("airtelFTP").ToString
        chkAirtelMail.Checked = dt.Rows(0)("airtelMail").ToString
        ddlChannelGenre.SelectedValue = dt.Rows(0)("channel_genre").ToString
        btnupdate.Visible = True
    End Sub
    Private Sub clearall()
        bindGrid()
        txtChannel.Text = ""
        txtServiceID.Text = ""
        chkAirtelFTP.Checked = False
        chkAirtelMail.Checked = False
        btnupdate.Visible = False
    End Sub
End Class