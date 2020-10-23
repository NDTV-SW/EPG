Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class reset
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim obj As New clsExecute
            Dim strToken As String = Request.QueryString("TokenID").ToString
            Dim dt As DataTable = obj.executeSQL("select resettoken,resettokenexpiry,case when datediff(mi, dbo.getlocaldate(),resettokenexpiry)>1 then 1 else 0 end valid from mstusers where resetToken='" & strToken & "'", False)
            If dt.Rows.Count = 1 Then
                If dt.Rows(0)("valid") = 1 Then

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Expired Token !');", True)
                    'Response.Redirect("login.aspx")
                End If

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Invalid Token !');", True)
                'Response.Redirect("login.aspx")
            End If
        End If
    End Sub


    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim obj As New clsExecute
        Dim strToken As String = Request.QueryString("TokenID").ToString
        obj.executeSQL("update mstusers set upass='" & FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text, "md5") & "' where resetToken='" & strToken & "'", False)
        obj.executeSQL("update mstusers set resettoken=null,resetTokenExpiry=null where resetToken='" & strToken & "'", False)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Password update Success !');", True)
        'Response.Redirect("~/login.aspx")
    End Sub
End Class