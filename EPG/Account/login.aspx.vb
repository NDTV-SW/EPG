Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            
        End If
    End Sub
    Protected Sub ValidateUser()
        Dim userId As String = ""
        Dim roles As String = String.Empty
        lbStatus.Visible = False
        Dim boolPasswordChange As Boolean = False

        Dim constr As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Validate_User")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@username", txtInputEmail.Text)
                cmd.Parameters.AddWithValue("@password", FormsAuthentication.HashPasswordForStoringInConfigFile(txtInputPassword.Text, "md5"))
                cmd.Connection = con
                con.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                reader.Read()
                userId = reader("uname").ToString()
                roles = reader("Roles").ToString()
                boolPasswordChange = reader("passwordchangerequired").ToString()

                con.Close()
            End Using
            Select Case userId
                Case "-1"
                    lbStatus.Visible = True
                    lbStatus.Text = "Username and/or password is incorrect."
                    Exit Select
                Case "-2"
                    lbStatus.Visible = True
                    lbStatus.Text = "Account has not been activated."
                    Exit Select
                Case Else
                    Dim ticket As New FormsAuthenticationTicket(1, txtInputEmail.Text, DateTime.Now, DateTime.Now.AddMinutes(2880), True, roles, FormsAuthentication.FormsCookiePath)
                    Dim hash As String = FormsAuthentication.Encrypt(ticket)
                    Dim cookie As New HttpCookie(FormsAuthentication.FormsCookieName, hash)

                    If ticket.IsPersistent Then
                        cookie.Expires = ticket.Expiration
                    End If
                    Response.Cookies.Add(cookie)

                    If boolPasswordChange Then
                        Response.Redirect("changepassword.aspx")
                    Else
                        Response.Redirect(FormsAuthentication.GetRedirectUrl(txtInputEmail.Text, True))
                    End If

                    Exit Select
            End Select
        End Using
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ValidateUser()
    End Sub
End Class