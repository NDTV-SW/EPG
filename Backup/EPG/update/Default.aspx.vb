Imports System.Data
Imports System.Data.SqlClient
Public Class _DefaultMe
    Inherits System.Web.UI.Page

    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim adp As New SqlDataAdapter("sp_updatestats", MyConnection)
        Dim dt As New DataTable
        adp.SelectCommand.CommandTimeout = "99999999"
        adp.Fill(dt)
    End Sub


End Class