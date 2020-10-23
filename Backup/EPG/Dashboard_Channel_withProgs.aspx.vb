Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class Dashboard_Channel_withProgs
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Dim prevcolumn As String = ""
    Dim altRow As Integer = 0

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
            Logger.LogError("DashBoard Channel with Programmes", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdDashBoard_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdDashBoard.RowCommand

    End Sub
    Dim lastColor As System.Drawing.Color
    Private Sub grdDashBoard_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDashBoard.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hyXML As HyperLink = DirectCast(e.Row.FindControl("hyXML"), HyperLink)
                If hyXML.NavigateUrl = "" Then
                    hyXML.Visible = False
                Else
                    hyXML.NavigateUrl = "~/xml/" & hyXML.NavigateUrl
                End If

                If e.Row.Cells(2).Text <> e.Row.Cells(3).Text Then
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
                    e.Row.Cells(3).Font.Bold = True
                End If

                If e.Row.Cells(4).Text <> e.Row.Cells(5).Text Then
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                    e.Row.Cells(5).Font.Bold = True
                End If

                If e.Row.Cells(4).Text = "0" Then
                    e.Row.Cells(4).Text = "-----"
                End If
                If e.Row.Cells(5).Text = "0" Then
                    e.Row.Cells(5).Text = "-----"
                End If
                If (Server.HtmlDecode(e.Row.Cells(8).Text).Trim) = "" Then
                    hyXML.Visible = False
                End If
                If prevcolumn = e.Row.Cells(1).Text Then
                    e.Row.Cells(0).Text = ""
                    e.Row.Cells(1).Text = ""
                    e.Row.Cells(6).Text = ""
                    e.Row.Cells(7).Text = ""
                    e.Row.Cells(8).Text = ""
                    e.Row.Cells(9).Text = ""
                    hyXML.Visible = False
                Else
                    altRow = IIf(altRow = 0, 1, 0)
                    prevcolumn = e.Row.Cells(1).Text
                End If
                e.Row.BackColor = IIf(altRow = 0, grdDashBoard.AlternatingRowStyle.BackColor, grdDashBoard.RowStyle.BackColor)
                'e.Row.BackColor = IIf(altRow = 0, System.Drawing.Color.AliceBlue, System.Drawing.Color.White)
                Try
                    If e.Row.Cells(7).Text = "" Then
                        e.Row.BackColor = lastColor
                    Else
                        If Convert.ToDateTime(e.Row.Cells(7).Text).Date > Date.Now.Date Then
                            e.Row.BackColor = Drawing.Color.LightGreen
                            lastColor = Drawing.Color.LightGreen
                        Else
                            e.Row.BackColor = Drawing.Color.LightPink
                            lastColor = Drawing.Color.LightPink
                        End If
                    End If

                Catch
                    e.Row.BackColor = Drawing.Color.LightPink
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("DashBoard Channel with Programmes", "grdDashBoard_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

End Class