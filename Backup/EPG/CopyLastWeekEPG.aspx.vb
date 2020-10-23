Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class CopyLastWeekEPG
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Dim flag As Integer = 0
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
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

            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
                txtChannel.Visible = True
            ElseIf (User.IsInRole("USER")) Then
                txtChannel.Visible = True
            Else
                txtChannel.Visible = False
            End If
            If Page.IsPostBack = False Then

               
                lbEPGDates.Visible = False
            Else
                lbEPGDates.Visible = True
                Dim obj As New Logger
                lbEPGDates.Text = obj.GetEpgDates(txtChannel.Text)


                Dim obj1 As New clsExecute
                Dim dt As DataTable = obj1.executeSQL("SELECT CONVERT(varchar, MAX(Progdate+1),101) as MaxDate from mst_epg where ChannelId='" & txtChannel.Text & "'", False)

                If dt.Rows.Count > 0 Then
                    txtStartDate.Text = dt(0)(0).ToString
                End If

            End If

        Catch ex As Exception
            Logger.LogError("CopyLastWeekEPG", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    
    
    Private Sub grdCopyLastWeekdata_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCopyLastWeekdata.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                Dim lbSynopsis As Label = DirectCast(e.Row.FindControl("lbSynopsis"), Label)
                Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
                Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)
                Dim lbProgTime As Label = DirectCast(e.Row.FindControl("lbProgTime"), Label)
                Dim lbProgdate As Label = DirectCast(e.Row.FindControl("lbProgdate"), Label)
                Dim lbDuration As Label = DirectCast(e.Row.FindControl("lbDuration"), Label)
                lbProgdate.Text = Convert.ToDateTime(lbProgdate.Text).ToString("dd-MMM-yyyy")
                lbProgTime.Text = Convert.ToDateTime(lbProgTime.Text).ToString("hh:mm")
            End If
        Catch ex As Exception
            Logger.LogError("CopyLastWeekEPG", "grdData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            
            Dim rowcount As Integer

            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("sp_mst_epg_copy_epg", "channelid~startdate~checkdays", "VarChar~DateTime~Int", txtChannel.Text & "~" & txtStartDate.Text & "~" & ddlDays.SelectedValue, True, True)
            
            rowcount = dt.Rows(dt.Rows.Count - 1)("data").ToString
            If rowcount > 0 Then
                lbError.Visible = True
                lbError.Text = rowcount & " found in Master EPG for this Channel."
                Exit Sub
            End If
            dt.Rows.RemoveAt(dt.Rows.Count - 1)
            dt.Columns.RemoveAt(dt.Columns.Count - 1)
            grdCopyLastWeekdata.DataSource = dt
            grdCopyLastWeekdata.DataBind()
            

            myMessageBox("EPG Copied Successfully!")
            lbError.Visible = False
            btnCopy.Enabled = False
        Catch ex As Exception
            Logger.LogError("CopyLastWeekEPG", "btnBuildEPG_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error while copying EPG!")
        End Try
    End Sub

    Protected Sub txtChannel_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtChannel.TextChanged
        btnCopy.Enabled = True
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function
End Class
