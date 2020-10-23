Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class EPGMissingInfo
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'HttpContext.Current.Session.Add("UserID", User.Identity.Name)
            ''SingleSessionPreparation.CreateAndStoreSessionToken(User.Identity.Name)

            'Dim userid As String = HttpContext.Current.Session("UserID")

            Dim mu As MembershipUser = Membership.GetUser(User.Identity.Name)
            Membership.UpdateUser(mu)

            If mu.Comment = "Need Change Password1" Then
                'Response.Redirect("~/Account/ChangePassword.aspx")
                mu.Comment = "0"
            End If

            Dim pwDateExpire As Integer
            pwDateExpire = DateDiff(DateInterval.Day, mu.LastPasswordChangedDate, Date.Now)

            If pwDateExpire > 30 Then
                'Response.Redirect("~/Account/ChangePassword.aspx")
            End If
            'ddlChannelName.Attributes.Add("onclick", "JavaScript:fnTrapKD('ddlchannelname');")
        Catch ex As Exception
            Logger.LogError("Error report", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdErrorReport_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdErrorReport.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbProgdate As Label = DirectCast(e.Row.FindControl("lbProgdate"), Label)
                If lbProgdate.Text = DateTime.Now.Date.ToString("dd MMM yyyy") Then
                    e.Row.BackColor = Drawing.Color.RosyBrown
                End If
                'e.Row.Cells(0).Text = (Convert.ToDateTime(e.Row.Cells(0).Text)).ToString("dd-MMM-yyyy")
            End If
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "grdSynopsis_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

End Class