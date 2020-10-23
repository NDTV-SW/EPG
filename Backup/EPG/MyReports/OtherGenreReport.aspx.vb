Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class OtherGenreReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                gridbind()
            End If
        Catch ex As Exception
            Logger.LogError("OtherGenreReport", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdReport_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdReport.RowCommand
        Try
            Dim lbProgid As Label = TryCast(grdReport.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbProgid"), Label)
            Dim lbProgNewGenre As Label = TryCast(grdReport.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbProgNewGenre"), Label)
            Dim ddlGenre As DropDownList = TryCast(grdReport.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("ddlGenre"), DropDownList)

            If e.CommandName.ToLower = "updategenre" Then
                Dim obj As New clsExecute
                obj.executeSQL("update mst_program set genreid='" & ddlGenre.SelectedValue & "' where progid='" & lbProgid.Text & "'", False)
                gridbind()
            End If

        Catch ex As Exception
            Logger.LogError("OtherGenreReport", "grdReport_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub gridbind()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select *,'Other' currentGenre from fn_get_others_genre (199) order by channelid,progname", False)
        grdReport.DataSource = dt
        grdReport.DataBind()
    End Sub

    Protected Sub grdReport_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdReport.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbProgNewGenre As Label = TryCast(e.Row.FindControl("lbProgNewGenre"), Label)
            Dim ddlGenre As DropDownList = TryCast(e.Row.FindControl("ddlGenre"), DropDownList)
            ddlGenre.SelectedValue = lbProgNewGenre.Text
        End If
    End Sub
End Class