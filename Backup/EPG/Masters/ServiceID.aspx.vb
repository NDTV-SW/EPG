Imports System
Imports System.Data.SqlClient
Public Class ServiceID
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (User.Identity.Name.ToLower = "hemant" Or User.Identity.Name.ToLower = "kautilyar" Or User.Identity.Name.ToLower = "shweta") Then
                Response.Redirect("~/default.aspx")
            End If
        Catch ex As Exception
            Logger.LogError("ServiceID", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    
#Region "ServiceID Master"
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            txtRowId.Text = "0"
            txtServiceID.Text = ""
            txtChannelID.Text = ""
        Catch ex As Exception
            Logger.LogError("ServiceID", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox(ex.Message.ToString)
        End Try
    End Sub


    Private Sub btnUpdateServiceId_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateServiceId.Click
        Try
            If MyConnection.State = ConnectionState.Closed Then
                MyConnection.Open()
            End If

            Dim cmd As New SqlCommand("update mst_channel set serviceid='" & txtServiceID.Text.Trim.Replace("'", "''") & "' where RowID='" & txtRowId.Text & "'", MyConnection)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            MyConnection.Close()
            MyConnection.Dispose()

            btnCancel.Visible = False
            btnUpdateServiceId.Visible = False

            grdServiceID.SelectedIndex = -1
            grdServiceID.DataBind()

            Try
                Dim strRecepient As String, strSubject As String, strBody As String, strAttachment As String, strBcc As String, strRegards As String
                strRecepient = "epgtech.ndtv.com"
                strSubject = "Service ID updated for channel " & txtChannelID.Text
                strBody = "Hi Team,<br/><br/>Service ID for channel <b>" & txtChannelID.Text & "</b> has been updated to <b>" & txtServiceID.Text & "</b> by <b>" & User.Identity.Name & ".</b><br/><br/>"
                strRegards = "Regards<br/>Team EPG, NDTV"
                strBcc = ""
                strAttachment = ""
                Logger.mailMessage(strRecepient.Trim, strSubject.Trim, strBody.Trim & strRegards.Trim, strBcc.Trim, strAttachment.Trim)
            Catch
            End Try

            txtServiceID.Text = ""
            txtChannelID.Text = ""
            txtRowId.Text = ""
            myMessageBox("ServiceID has been Updated!")
        Catch ex As Exception
            Logger.LogError("ServiceID", "btnUpdateServiceId_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Not Updated! Please see error Report.")
        End Try
    End Sub

    Private Sub grdServiceID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdServiceID.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdServiceID.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbChannelId As Label = DirectCast(grdServiceID.SelectedRow.FindControl("lbChannelId"), Label)
            Dim lbServiceID As Label = DirectCast(grdServiceID.SelectedRow.FindControl("lbServiceID"), Label)

            txtRowId.Text = lbRowId.Text.Trim
            txtChannelID.Text = lbChannelId.Text

            ddlChannel.SelectedValue = lbChannelId.Text
            txtServiceID.Text = lbServiceID.Text

            btnUpdateServiceId.Visible = True
            btnCancel.Visible = True
        Catch ex As Exception
            Logger.LogError("ServiceID", "grdChannelmaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

End Class