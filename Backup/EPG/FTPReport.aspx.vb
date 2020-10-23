Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class FTPReport
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
        If txtFTPDate.Text.Trim = "" Then
            sql = "select top 500 Channelid,Filename,ftpdate,loggedinuser,convert (varchar,ftpdate,100) as ftpdate1 from ftp_records order by ftpdate desc"
        Else
            sql = "select Channelid,Filename,ftpdate,loggedinuser,convert (varchar,ftpdate,100) as ftpdate1 from ftp_records where convert(varchar,ftpdate,101)=  CONVERT(varchar,'" & txtFTPDate.Text.Trim & "',112) order by ftpdate desc"
        End If
        Dim myConnection As New SqlConnection(ConString)
        Dim adp As New SqlDataAdapter(sql, myConnection)
        Dim ds As New DataSet
        adp.Fill(ds)
        grdFTPReport.DataSource = ds
        grdFTPReport.DataBind()
    End Sub
End Class