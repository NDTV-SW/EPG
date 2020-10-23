Public Class _DefaultCRM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        Dim sql As String
        If btnAdd.Text.ToUpper = "ADD" Then
            sql = "insert into crm(servicetype,clientname,city,statename,country,subscriberbase,otherinfo,approxdealvalue,dealstatuswon,dealstatuslost,lostreason,lostremarks,active) values"
            sql = sql & "('" & lstLeadType.SelectedValue & "','" & txtName.Text & "','" & txtCity.Text & "','" & txtState.Text & "','" & txtCountry.Text & "',"
            sql = sql & "'" & txtSubscriberBase.Text & "','" & txtOtherInfo.Text & "','" & txtDealvalue.Text & "','" & chkDealWon.Checked & "',"
            sql = sql & "'" & chkDealLost.Checked & "','" & txtLostReason.Text & "','" & txtLostRemarks.Text & "','" & chkActive.Checked & "')"
        Else
            sql = "update crm set servicetype='" & lstLeadType.SelectedValue & "',clientname='" & txtName.Text & "',city='" & txtCity.Text & "'"
            sql = sql & ",statename='" & txtState.Text & "',country='" & txtCountry.Text & "',subscriberbase='" & txtSubscriberBase.Text & "'"
            sql = sql & ",otherinfo='" & txtOtherInfo.Text & "',approxdealvalue='" & txtDealvalue.Text & "',dealstatuswon='" & chkDealWon.Checked & "'"
            sql = sql & ",dealstatuslost='" & chkDealLost.Checked & "',lostreason='" & txtLostReason.Text & "',lostremarks='" & txtLostRemarks.Text & "'"
            sql = sql & ",active='" & chkActive.Checked & "' where id='" & lbID.Text & "'"
        End If

        obj.executeSQL(sql, False)
        If btnAdd.Text.ToUpper = "ADD" Then
            Response.Redirect("contactdetails.aspx?client=" & txtName.Text)
        End If
        clearAll()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Private Sub clearAll()
        lstLeadType.SelectedIndex = 0
        txtName.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtCountry.Text = ""
        txtSubscriberBase.Text = ""
        txtOtherInfo.Text = ""
        txtDealvalue.Text = ""
        chkDealWon.Checked = False
        chkDealLost.Checked = False
        txtLostReason.Text = ""
        txtLostRemarks.Text = ""
        chkActive.Checked = True
        lbID.Text = ""
        btnAdd.Text = "Add"
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub

    Protected Sub chkDealLost_CheckedChanged(sender As Object, e As EventArgs) Handles chkDealLost.CheckedChanged
        If chkDealLost.Checked Then
            divLostR.Visible = True
            divLostRem.Visible = True
        Else
            divLostR.Visible = False
            divLostRem.Visible = False
        End If

    End Sub

    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(8).CssClass = "text-right"

        End If
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from crm where id='" & lbID.Text & "'", False)

        lstLeadType.SelectedValue = dt.Rows(0)("servicetype").ToString
        txtName.Text = dt.Rows(0)("clientname").ToString
        txtCity.Text = dt.Rows(0)("city").ToString
        txtState.Text = dt.Rows(0)("statename").ToString
        txtCountry.Text = dt.Rows(0)("country").ToString
        txtSubscriberBase.Text = dt.Rows(0)("subscriberbase").ToString
        txtOtherInfo.Text = dt.Rows(0)("otherinfo").ToString
        txtDealvalue.Text = dt.Rows(0)("approxdealvalue").ToString
        chkDealWon.Checked = dt.Rows(0)("dealstatuswon").ToString
        chkDealLost.Checked = dt.Rows(0)("dealstatuslost").ToString

        If chkDealLost.Checked Then
            divLostR.Visible = True
            divLostRem.Visible = True
        Else
            divLostR.Visible = False
            divLostRem.Visible = False
        End If


        txtLostReason.Text = dt.Rows(0)("lostreason").ToString
        txtLostRemarks.Text = dt.Rows(0)("lostremarks").ToString
        chkActive.Checked = dt.Rows(0)("active").ToString

        btnAdd.Text = "Update"
    End Sub
End Class