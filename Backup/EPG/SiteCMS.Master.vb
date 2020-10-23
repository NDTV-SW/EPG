Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Data.SqlClient

Public Class SiteCMS
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.User.IsInRole("LOGGER") Then
            Response.Redirect("~/reports/relianceErrorLogging.aspx")
        End If
        SqlConnection.ClearAllPools()
    End Sub

End Class