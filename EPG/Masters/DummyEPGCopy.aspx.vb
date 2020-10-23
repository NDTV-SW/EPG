Imports System
Imports System.Data.SqlClient
Public Class DummyEPGCopy
    Inherits System.Web.UI.Page
  

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                txtTillDate.Text = DateTime.Now.AddDays(15).ToString("MM/dd/yyyy")
                grd.DataBind()
            End If

        Catch ex As Exception
            Logger.LogError("DummyEPGCopy", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        grd.DataBind()
    End Sub
End Class