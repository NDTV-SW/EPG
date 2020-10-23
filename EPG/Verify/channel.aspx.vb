Public Class verifychannel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim li As HtmlGenericControl = TryCast(Master.FindControl("liverifychannel"), HtmlGenericControl)
            li.Attributes.Add("class", "active")
        End If
        sqlDS.UpdateParameters("loggedinuser").DefaultValue = User.Identity.Name.ToLower
    End Sub

End Class