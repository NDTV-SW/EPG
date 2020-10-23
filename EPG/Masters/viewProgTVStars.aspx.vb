Imports System
Imports System.Data.SqlClient

Public Class viewProgTVStars
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                Dim strProgid As String = Request.QueryString("progid").ToString
                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL("select * from fn_GetProgTvStars('" & strProgid & "') order by name", False)
                grdData.DataSource = dt
                grdData.DataBind()

            Catch ex As Exception
                Logger.LogError("viewProgTVStars", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Dim intSno As Integer
    Protected Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSNO"), Label)
            lbSno.Text = intSno.ToString
            intSno = intSno + 1

        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

End Class