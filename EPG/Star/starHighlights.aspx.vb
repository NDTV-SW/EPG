Public Class starHighlights
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
          

        End If
    End Sub


    Protected Sub dl1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles dl1.ItemCommand
        If e.CommandName = "channel" Then
            Dim strChannel As String = e.CommandArgument.ToString()
            'sqlDSGrd.SelectParameters("channelid").DefaultValue = strChannel
            lbChannel.Text = strChannel
            grdNew.DataBind()
            'Session("Highlightchannel") = strChannel
            'Response.Redirect("HighlightsChannel.aspx")

            'grd.DataBind()
        End If
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim grdRow As GridViewRow
        Dim sql As String = ""
        For Each grdRow In grdNew.Rows
            Dim chk As CheckBox = TryCast(grdRow.FindControl("chk"), CheckBox)
            If chk.Checked Then
                Dim lbprogid As Label = TryCast(grdRow.FindControl("lbprogid"), Label)
                Dim lbCategory As Label = TryCast(grdRow.FindControl("lbCategory"), Label)
                Dim lbAirDate As Label = TryCast(grdRow.FindControl("lbAirDate"), Label)
                sql = sql & "insert into fpc_highlights(channelid,progid,airdate,category,feed,active)"
                sql = sql & " values('" & lbChannel.Text & "','" & lbprogid.Text & "','" & lbAirDate.Text & "','" & lbCategory.Text & "','" & ddlType.SelectedValue & "',1);"
                chk.Checked = False
            End If
        Next

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL(sql, False)
        grdNew.DataBind()
    End Sub

    Protected Sub grdNew_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdNew.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            btnSave.Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chk As CheckBox = TryCast(e.Row.FindControl("chk"), CheckBox)

            If chk.Checked Then

            End If

            btnSave.Visible = True
        End If
    End Sub
End Class