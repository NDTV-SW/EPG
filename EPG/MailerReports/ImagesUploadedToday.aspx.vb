
Public Class ImagesUploadedToday
    Inherits System.Web.UI.Page


    Protected Sub grdUsers_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsers.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    Dim imglogo As Image = DirectCast(e.Row.FindControl("imglogo"), Image)
                    Dim hylogo As HyperLink = DirectCast(e.Row.FindControl("hylogo"), HyperLink)
                    Dim lbProgLogo As Label = DirectCast(e.Row.FindControl("lbProgLogo"), Label)


                    Dim objImage As New clsUploadModules


                    imglogo.ImageUrl = "~/uploads/" & lbProgLogo.Text
                    hylogo.NavigateUrl = "~/uploads/" & lbProgLogo.Text

            End Select
        Catch ex As Exception
            Logger.LogError("ImagesUploadedToday.aspx", "grd_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdUsersP_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsersP.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    Dim imglogo As Image = DirectCast(e.Row.FindControl("imglogo"), Image)
                    Dim hylogo As HyperLink = DirectCast(e.Row.FindControl("hylogo"), HyperLink)
                    Dim lbProgLogo As Label = DirectCast(e.Row.FindControl("lbProgLogo"), Label)


                    Dim objImage As New clsUploadModules


                    imglogo.ImageUrl = "~/uploads/portrait/" & lbProgLogo.Text
                    hylogo.NavigateUrl = "~/uploads/portrait/" & lbProgLogo.Text

            End Select
        Catch ex As Exception
            Logger.LogError("ImagesUploadedToday.aspx", "grdP_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
            grd.DataBind()
            grdP.DataBind()
            ddlusers.DataBind()
            ddlusersP.DataBind()
        End If

    End Sub
End Class
