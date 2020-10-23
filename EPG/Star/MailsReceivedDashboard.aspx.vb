Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class MailsReceivedDashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then


        End If
    End Sub



    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged


        'Dim obj As New clsExecute
        'lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        'Dim dt As DataTable = obj.executeSQL("SELECT * FROM fpc_channelsummary WHERE id='" & lbID.Text & "'", False)

        'lbID.Text = dt.Rows(0)("id").ToString

    End Sub

    Private Sub clearAll()




    End Sub


    Dim intColCount

    'Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound
    '    If e.Row.RowType = DataControlRowType.Header Then
    '        Dim gridView As GridView = TryCast(sender, GridView)
    '        intColCount = gridView.Columns.Count
    '    End If

    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        For i As Integer = 0 To intColCount - 1
    '            If (e.Row.Cells(i).Text = "False") Then
    '                e.Row.Cells(i).CssClass = "alert-danger"
    '            ElseIf (e.Row.Cells(i).Text = "True") Then
    '                e.Row.Cells(i).CssClass = "alert-success"
    '            End If
    '        Next
    '    End If
    'End Sub
End Class