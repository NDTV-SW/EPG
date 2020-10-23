Imports System
Imports System.Data.SqlClient
Public Class TranslatedFiles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then

            End If
        Catch ex As Exception
            Logger.LogError("Translated Files", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grd_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd.RowCommand
        Try
            If e.CommandName = "received" Then
                Dim lbID As Label = TryCast(grd.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbID"), Label)

                Dim obj As New clsExecute

                obj.executeSQL("update mst_translationfiles set filereceived=1,filereceivedat=dbo.getlocaldate(),filereceivedupdatedby='" & User.Identity.Name & "' where id='" & lbID.Text & "'", False)
                grd.DataBind()
            End If

        Catch ex As Exception
            Logger.LogError("Translated Files", "grd_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub





    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim lbGenreId As Label = TryCast(e.Row.FindControl("lbGenreId"), Label)
        End If
    End Sub

End Class