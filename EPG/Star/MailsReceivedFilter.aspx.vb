Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class MailsReceivedFilter
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then


        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click

        Dim obj As New clsExecute
        'If btnUpdate.Text = "SAVE" Then
        obj.executeSQL("update fpc_mailattachmentingest set channelid='" & ddlChannel.SelectedValue & "' where id='" & lbID.Text & "'", False)
        obj.executeSQL("insert into fpc_ChannelEPGMailsReceived(channelid,startdate,enddate,mailreceivedat,mailreceivedby,mailsubject,insertedat,insertedby,active)" & _
        " values('" & ddlChannel.SelectedValue & "','" & txtStartDate.Text & "','" & txtEndDate.Text & "','" & lbMailReceivedAt.Text & "','" & lbMailReceivedby.Text & "'," & _
        " '" & lbMailSubject.Text & "',dbo.getlocaldate(),'" & User.Identity.Name & "','1')", False)
        'End If
        clearAll()
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged


        Dim obj As New clsExecute
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        Dim dt As DataTable = obj.executeSQL("SELECT * FROM fpc_mailattachmentingest WHERE id='" & lbID.Text & "'", False)

        lbID.Text = dt.Rows(0)("id").ToString
        lbMailReceivedby.Text = dt.Rows(0)("mailreceivedby").ToString
        lbMailSubject.Text = dt.Rows(0)("mailsubject").ToString
        lbMailReceivedAt.Text = dt.Rows(0)("mailreceivedat").ToString
        lbAttachmentName.Text = dt.Rows(0)("attachmentname").ToString

    End Sub


    Dim intColCount

    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim gridView As GridView = TryCast(sender, GridView)
            intColCount = gridView.Columns.Count
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i As Integer = 0 To intColCount - 1
                If (e.Row.Cells(i).Text = "False") Then
                    e.Row.Cells(i).CssClass = "alert-danger"
                ElseIf (e.Row.Cells(i).Text = "True") Then
                    e.Row.Cells(i).CssClass = "alert-success"
                End If
            Next
        End If
    End Sub

    Private Sub clearAll()
        lbID.Text = ""
        lbMailReceivedby.Text = ""
        lbMailSubject.Text = ""
        lbMailReceivedAt.Text = ""
        lbAttachmentName.Text = ""
        ddlChannel.SelectedValue = 0
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub


End Class