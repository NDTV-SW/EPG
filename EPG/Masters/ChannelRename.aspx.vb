Imports System
Imports System.Data.SqlClient
Public Class ChannelRename
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub bindGrid()
        grd.DataBind()
    End Sub

    Private Sub clearAll()
        txtChannelName.Text = String.Empty
        txtNewChannelName.Text = String.Empty
        grd.DataBind()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        obj.executeSQL("insert into mst_channelrename(oldchannelname,newchannelname,requestedby) values('" & txtChannelName.Text & "','" & txtNewChannelName.Text & "','" & User.Identity.Name & "')", False)
        clearAll()
    End Sub

    Private Sub grd_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Dim lbRowId As Label = DirectCast(grd.Rows(e.RowIndex).FindControl("lbRowId"), Label)

        Dim obj As New clsExecute
        obj.executeSQL("delete from mst_channelrename where rowid='" & lbRowId.Text & "'", False)
        clearAll()
    End Sub
End Class