
Public Class rptCatchup
    Inherits System.Web.UI.Page
    Dim totalDuration As Integer
    Dim catchupCount As Integer
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("Catchup Report", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCatchupReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCatchupReport.Click
        Try
            grdCatchupReport.DataBind()

        Catch ex As Exception
            Logger.LogError("Catchup Report", "btnView_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdCatchupReport_Databound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCatchupReport.RowDataBound
        Try

            Select Case e.Row.RowType
                Case DataControlRowType.Header
                    totalDuration = 0
                    catchupCount = 0
                Case DataControlRowType.DataRow
                    Dim lbDuration As Label = TryCast(e.Row.FindControl("lbDuration"), Label)
                    totalDuration = totalDuration + Convert.ToInt32(lbDuration.Text)
                    catchupCount = catchupCount + 1
                Case DataControlRowType.Footer
                    e.Row.Cells(1).Text = "Toatal Count : "
                    e.Row.Cells(2).Text = catchupCount
                    e.Row.Cells(5).Text = "Total Duration : "
                    e.Row.Cells(6).Text = totalDuration
            End Select
        Catch ex As Exception
            Logger.LogError("Catchup Report", "grdCatchupReport_Databound", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

End Class