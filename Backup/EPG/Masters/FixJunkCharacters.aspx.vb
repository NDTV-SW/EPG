Imports System
Imports System.Data.SqlClient
Public Class FixJunkCharacters
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

            If Page.IsPostBack = False Then
                Try
                    

                Catch ex As Exception
                    
                    Logger.LogError("FixJunkCharacters", "Page_Load", ex.Message.ToString, User.Identity.Name)
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdProgramCentral_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdJunkData.RowDataBound
        Try
                    Select e.Row.RowType
                '            Case DataControlRowType.Header
                '                e.Row.CssClass = "locked"
                Case DataControlRowType.DataRow
                    Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)
                    Dim lbMstProg As Label = DirectCast(e.Row.FindControl("lbMstProg"), Label)
                    Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                    Dim lbSynopsis As Label = DirectCast(e.Row.FindControl("lbSynopsis"), Label)
                    Dim lbColorCode As Label = DirectCast(e.Row.FindControl("lbColorCode"), Label)
                    Dim lbLanguageId As Label = DirectCast(e.Row.FindControl("lbLanguageId"), Label)

                    Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)

                    Dim hyEdit As HyperLink = DirectCast(e.Row.FindControl("hyEdit"), HyperLink)
                    hyEdit.NavigateUrl = "javascript:openWin('" & lbChannelId.Text.Trim & "','" & lbProgId.Text.Trim & "','" & lbLanguageId.Text & "','Update','English','" & lbProgName.Text.Replace("'", "^") & "','MainContent_grdJunkData_hyEdit_" & e.Row.RowIndex & "')"
                    hyEdit.Enabled = True


                    'lbSynopsis.Text = lbSynopsis.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    '                ' 0 for programmes not in mst_epg, 1 for programmes in mst_epg.
                    '                ' 2 for programmes airing in next 7 days.

                    If lbColorCode.Text.Trim = 1 Then
                        e.Row.BackColor = Drawing.Color.LightBlue
                    ElseIf lbColorCode.Text.Trim = 2 Then
                        e.Row.BackColor = Drawing.Color.LightGreen
                    End If
            End Select
                        Catch ex As Exception
            Logger.LogError("FixJunkCharacters", "grdJunkData_RowDataBound", ex.Message.ToString, User.Identity.Name)
                        End Try
    End Sub

End Class