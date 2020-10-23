Imports System
Imports System.Web

Public Class SiteAdmin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            'If Not HttpContext.Current.User.IsInRole("Administrator") Then
            '    Response.Redirect("~/error/accessdenied.aspx")
            'End If
        End If
    End Sub

    
End Class