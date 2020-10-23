Imports System
Imports System.Data.SqlClient
Public Class ChannelMaster_WOI
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        
    End Sub

    Private Sub bindGrid()
        grd.DataBind()
    End Sub

    Private Sub clearAll()
        txtChannel.Text = String.Empty
        txtWoiChannel.Text = String.Empty
        lbID.Text = "0"
        btnAdd.Text = "ADD"
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim onair As Int16
        If chkOnair.Checked Then
            onair = 1
        Else
            onair = 0
        End If

        

        Dim obj As New clsExecute
        If btnAdd.Text.ToUpper = "ADD" Then
            obj.executeSQL("insert into mst_channel_woi(channelid,woichannelid,onair,updatesynopsis) values('" & txtChannel.Text & "','" & txtWoiChannel.Text & "','" & onair & "','" & chkUpdateSynopsis.Checked & "')", False)
        Else
            obj.executeSQL("update mst_channel_woi set channelid='" & txtChannel.Text & "',woichannelid='" & txtWoiChannel.Text & "',onair='" & onair & "',updatesynopsis='" & chkUpdateSynopsis.Checked & "' where rowid='" & lbID.Text & "'", False)
        End If
        clearAll()
    End Sub
    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from mst_channel_woi where rowid='" & lbID.Text & "'", False)
        txtChannel.Text = dt.Rows(0)("channelid").ToString
        txtWoiChannel.Text = dt.Rows(0)("woichannelid").ToString
        chkUpdateSynopsis.Checked = dt.Rows(0)("updatesynopsis").ToString
        If dt.Rows(0)("onair").ToString = "1" Then
            chkOnair.Checked = True
        Else
            chkOnair.Checked = False
        End If

        btnAdd.Text = "UPDATE"
    End Sub
End Class