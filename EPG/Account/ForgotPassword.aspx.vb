Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ForgotPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSubmit_Click(sender As [Object], e As EventArgs) Handles btnSubmit.Click
        Try


            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select mailid from mstusers where mailid='" & txtMailId.Text & "'", False)
            If dt.Rows.Count = 0 Then
                'lbStatus.Visible = True
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Invalid Mail ID !');", True)
                txtMailId.Text = ""
                Exit Sub
            End If





            Dim strNewPass As String = txtMailId.Text & DateTime.Now.ToString("HHmi")
            Dim strSub As String, strToken As String
            strToken = FormsAuthentication.HashPasswordForStoringInConfigFile(txtMailId.Text & DateTime.Now.ToString(), "md5")
            obj.executeSQL("update mstusers set resetToken='" & strToken & "',resetTokenExpiry=dbo.getlocaldate()+1 where mailid='" & txtMailId.Text & "' ", False)
            strSub = "Hi<br/><br/>"
            strSub = strSub & "Please click on the link below to Reset your Password. <a href='http://epgops.ndtv.com/account/reset.aspx?TokenID=" & strToken & "'>Reset</a><br/>This link is valid for next 24 hours only.<br/>"
            strSub = strSub & "Regards<br/>Team EPG NDTV"


            Logger.mailMessage(dt.Rows(0)(0).ToString, "Reset password", strSub, "sankalp@ndtv.com", "")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Reset link sent to your Mail ID !');", True)
            txtMailId.Text = ""

        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('There was some error !');", True)
        End Try
    End Sub

End Class