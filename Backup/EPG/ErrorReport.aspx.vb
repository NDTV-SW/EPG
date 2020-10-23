Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class ErrorReport
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

            If Page.IsPostBack = False Then
                txtErrorDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                gridBind()
            End If

        Catch ex As Exception
            Logger.LogError("Error report", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnView.Click
        gridBind()
    End Sub
    Private Sub gridBind()

        Dim sql As String = ""
        If txtErrorDate.Text.Trim = "" Then
            sql = "select top 500 ErrorPage,ErrorSource,ErrorType,ErrorMessage,LoggedinUser,ErrorDateTime,convert (varchar,ErrorDateTime,100) ErrorDateTime1 from app_error_logs order by ErrorDateTime desc"
        Else
            sql = "select ErrorPage,ErrorSource,ErrorType,ErrorMessage,LoggedinUser,ErrorDateTime,convert (varchar,ErrorDateTime,100) ErrorDateTime1 from app_error_logs where convert(varchar,ErrorDateTime,101)=  CONVERT(varchar,'" & txtErrorDate.Text.Trim & "',112) order by ErrorDateTime desc"
        End If
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL(sql, False)
        grdErrorReport.DataSource = dt
        grdErrorReport.DataBind()
    End Sub

End Class