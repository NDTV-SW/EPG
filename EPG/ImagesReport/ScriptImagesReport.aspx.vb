
Public Class ScriptImagesReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("Images Report", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Dim rowId As Integer
    Dim intImages As Integer

    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            rowId = 0
            intImages = 0


        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            rowId = rowId + 1
            intImages = intImages + e.Row.Cells(2).Text.ToString
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTAL :"
            e.Row.Cells(2).Text = intImages

        End If
    End Sub


    Protected Sub grd_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd.RowCommand

        Dim channelid As String = ""
        If (e.CommandName = "view") Then
            Dim btn As Button = DirectCast(e.CommandSource, Button)
            channelid = btn.CommandArgument
        End If

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select progname,programlogo from mst_program where channelid='" & channelid & "' and " & IIf(chkPortrait.Checked, "programlogoportrait", "programlogo") & " like '%script%' and progid in (select progid from mst_epg where progdate between '" & txtStartDate.Text & "' and '" & txtEndDate.Text & "') order by 1", False)
        grdReport.DataSource = dt
        grdReport.DataBind()

    End Sub
End Class