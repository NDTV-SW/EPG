Public Class Login
    Inherits System.Web.UI.Page

    Private Sub Login_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
   
    Private Sub LoginUser_LoggedIn(sender As Object, e As System.EventArgs) Handles LoginUser.LoggedIn
        
        'Dim userNameTextBox As TextBox = DirectCast(LoginUser.FindControl("UserName"), TextBox)
        ''SingleSessionPreparation.CreateAndStoreSessionToken(userNameTextBox.Text)

        'Dim mu As MembershipUser = Membership.GetUser(userNameTextBox.Text)
        'Dim isOnline As Boolean = mu.IsOnline
        'If isOnline Then
        '    Response.Write("Already Logged in!")
        'Else
        '    Response.Write("Not Logged in!")
        'End If
    End Sub
End Class