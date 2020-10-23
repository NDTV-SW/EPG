Public Class viewlinks
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim intRichMetaId As Integer = Request.QueryString("id")
            bindGrid(intRichMetaId)
        End If
    End Sub
    Private Sub bindGrid(ByVal intRichMetaId As Integer)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select progid,channelid,progname,richmetaid from mst_program where richmetaid='" & intRichMetaId & "'", False)
        grd.DataSource = dt
        grd.DataBind()
    End Sub

    Protected Sub grd_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grd.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim item As String = e.Row.Cells(2).Text
            For Each button As Button In e.Row.Cells(0).Controls.OfType(Of Button)()
                If button.CommandName = "Delete" Then
                    button.Attributes("onclick") = "if(!confirm('Do you want to delete """ + item + """ ?')){ return false; };"
                End If
            Next

        End If
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Dim obj As New clsExecute
        Dim lbLabel As Label = TryCast(grd.Rows(e.RowIndex).FindControl("lbId"), Label)
        Dim lbRichMetaId As Label = TryCast(grd.Rows(e.RowIndex).FindControl("lbRichMetaId"), Label)

        obj.executeSQL("update mst_program set richmetaid='' where progid='" & lbLabel.Text & "'", False)
        bindGrid(lbRichMetaId.Text)
    End Sub

    Protected Sub grd_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If e.ExceptionHandled = False Then
            e.ExceptionHandled = True
        End If
    End Sub
End Class