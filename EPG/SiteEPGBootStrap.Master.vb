Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Data.SqlClient

Public Class SiteEPGBootStrap
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.User.IsInRole("LOGGER") Then
            Response.Redirect("~/reports/uploadExcel.aspx")
        End If

        If HttpContext.Current.User.IsInRole("VIEWER") And HttpContext.Current.User.Identity.Name.ToLower = "visionasia" Then
            Response.Redirect("~/VisionAsia/Default.aspx")
        End If
        'SqlConnection.ClearAllPools()
    End Sub

End Class