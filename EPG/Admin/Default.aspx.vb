Imports System
Imports System.Data.SqlClient
Public Class AdminDefault
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Redirect("usermaster.aspx")
        Catch ex As Exception
            Logger.LogError("Admin", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class