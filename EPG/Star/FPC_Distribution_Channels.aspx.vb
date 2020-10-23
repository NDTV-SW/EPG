Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class FPC_Distribution_Channels
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim obj As New clsExecute
        Dim strSql As String = ""
        If btnSave.Text = "SAVE" Then
            strSql = strSql & "insert into fpc_distribution_channels(clientid,channelid,serviceid,onair,lastupdate,displayname,filenaming)"
            strSql = strSql & " values('" & ddlClient.SelectedValue & "','" & txtChannelid.Text & "','" & txtServiceID.Text & "','" & chkOnair.Checked & "',dbo.getlocaldate(),'" & txtDisplayName.Text & "','" & txtFileNaming.Text & "')"
        Else
            strSql = strSql & "update fpc_distribution_channels set "
            strSql = strSql & " clientid='" & ddlClient.SelectedValue & "',"
            strSql = strSql & " channelid='" & txtChannelid.Text & "',"
            strSql = strSql & " serviceid='" & txtServiceID.Text & "',"
            strSql = strSql & " onair='" & chkOnair.Checked & "',"
            strSql = strSql & " displayname='" & txtDisplayName.Text & "',"
            strSql = strSql & " filenaming='" & txtFileNaming.Text & "',"
            strSql = strSql & " lastupdate=dbo.getlocaldate()"
            strSql = strSql & " where id='" & lbID.Text & "'"
        End If
        obj.executeSQL(strSql, False)
        clearAll()
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
       

        Dim obj As New clsExecute
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        Dim dt As DataTable = obj.executeSQL("SELECT * FROM fpc_distribution_channels WHERE id='" & lbID.Text & "'", False)

        
        txtChannelid.Text = dt.Rows(0)("channelid").ToString
        txtServiceID.Text = dt.Rows(0)("serviceid").ToString
        chkOnair.Checked = dt.Rows(0)("onair").ToString
        txtDisplayName.Text = dt.Rows(0)("displayname").ToString
        txtFileNaming.Text = dt.Rows(0)("filenaming").ToString
        lbID.Text = dt.Rows(0)("id").ToString
        btnSave.Text = "UPDATE"
    End Sub

    Private Sub clearAll()
        txtChannelid.Text = ""
        txtServiceID.Text = ""
        chkOnair.Checked = True
        txtDisplayName.Text = ""
        txtFileNaming.Text = ""
        lbID.Text = ""
        btnSave.Text = "SAVE"
        grd.SelectedIndex = -1
        grd.DataBind()

    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function
End Class