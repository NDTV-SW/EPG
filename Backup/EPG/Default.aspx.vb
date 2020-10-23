Public Class _Default
    Inherits System.Web.UI.Page

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim obj As New clsExecute
            obj.executeSQL("insertMissingRegionalNames", True)
        End If
    End Sub

    Protected Sub grdEPGMissing8Days_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEPGMissing8Days.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbnoOfDays As Label = TryCast(e.Row.FindControl("lbnoOfDays"), Label)
            If Convert.ToInt16(lbnoOfDays.Text) < 3 Then
                e.Row.BackColor = Drawing.Color.IndianRed
            End If
        End If
    End Sub
    Protected Sub btnRefreshChannelList_Click(sender As Object, e As EventArgs) Handles btnRefreshChannelList.Click
        Session("ChannelList") = Nothing
        Session("XMLChannelList") = Nothing
    End Sub
End Class