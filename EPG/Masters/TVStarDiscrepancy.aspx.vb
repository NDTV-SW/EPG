Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class TVStarDiscrepancy
    Inherits System.Web.UI.Page
    Private Sub myErrorBox1(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                ddlGenre.DataBind()
                ddlLanguage.DataBind()
                ddlChannel.DataBind()
                grdData.DataBind()
            End If

        Catch ex As Exception
            Logger.LogError("TV Star Discrepancy", "Page Load", ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

    Dim intSno As Integer
    Protected Sub grdData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSNO"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1

            Dim hyProgName As HyperLink = DirectCast(e.Row.FindControl("hyProgName"), HyperLink)
            Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
            If Not chkMissing.Checked Then
                hyProgName.NavigateUrl = "JavaScript:openWin('" & lbProgId.Text & "')"
            End If

            
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    Protected Sub ddlGenre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGenre.SelectedIndexChanged
        ddlChannel.DataBind()
        grdData.DataBind()
    End Sub

    Protected Sub ddlLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLanguage.SelectedIndexChanged
        ddlChannel.SelectedIndex = 0
        ddlChannel.DataBind()
        grdData.DataBind()
    End Sub

    Protected Sub chkHighTRP_CheckedChanged(sender As Object, e As EventArgs) Handles chkHighTRP.CheckedChanged
        ddlChannel.SelectedIndex = 0
        ddlChannel.DataBind()
        grdData.DataBind()
    End Sub

    Protected Sub chkMissing_CheckedChanged(sender As Object, e As EventArgs) Handles chkMissing.CheckedChanged
        ddlChannel.SelectedIndex = 0
        ddlChannel.DataBind()
        grdData.DataBind()
    End Sub

    Protected Sub chkInEPG_CheckedChanged(sender As Object, e As EventArgs) Handles chkInEPG.CheckedChanged
        ddlChannel.SelectedIndex = 0
        ddlChannel.DataBind()
        grdData.DataBind()
    End Sub
End Class