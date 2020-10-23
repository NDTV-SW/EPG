Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.IsInRole("USER") Then
            'Response.Redirect("~/Default.aspx")
        End If
    End Sub

End Class