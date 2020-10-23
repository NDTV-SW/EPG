Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSubmit_Click(sender As [Object], e As EventArgs) Handles btnSubmit.Click
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select upass from mstusers where uname='" & User.Identity.Name & "'", False)
        If FormsAuthentication.HashPasswordForStoringInConfigFile(txtOldPassword.Text, "md5") = dt.Rows(0)("upass").ToString Then
            obj.executeSQL("update  mstusers set passwordChangeRequired=0,upass='" & FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text, "md5") & "' where uname='" & User.Identity.Name & "'", False)


            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Password update Success !');", True)

            Dim returnUrl As String = Request.QueryString("ReturnUrl")
            If returnUrl Is Nothing Then
                returnUrl = "~/"
            End If
            Response.Redirect(returnUrl)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Invalid Old Password !');", True)
        End If


    End Sub

End Class