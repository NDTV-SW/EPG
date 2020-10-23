Public Class StatusUpdate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
        End If

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        Dim sql As String
        If btnAdd.Text.ToUpper = "ADD" Then
            sql = "insert into crm_statusupdate(crmid,statusdate,remarks,followup,followupby,followupdate,active) values"
            sql = sql & "('" & ddlClient.SelectedValue & "','" & txtDate.Text & "','" & txtRemarks.Text.Replace("'", "''") & "','" & chkFollowup.Checked & "'"
            sql = sql & ",'" & ddlFollowupBy.SelectedValue & "','" & txtFollowupDate.Text & "','" & chkActive.Checked & "')"
        Else
            sql = "update crm_statusupdate set crmid='" & ddlClient.SelectedValue & "',statusdate='" & txtDate.Text & "',remarks='" & txtRemarks.Text.Replace("'", "''") & "',"
            sql = sql & "followup='" & chkFollowup.Checked & "',followupby='" & ddlFollowupBy.SelectedValue & "',followupdate='" & txtFollowupDate.Text & "',active='" & chkActive.Checked & "' where id='" & lbID.Text & "'"
        End If
        obj.executeSQL(sql, False)
        clearAll()

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Private Sub clearAll()
        'ddlClient.SelectedIndex = 0
        'txtDate.Text = ""
        txtRemarks.Text = ""
        chkFollowup.Checked = False
        ddlFollowupBy.SelectedIndex = 0
        txtFollowupDate.Text = ""
        chkActive.Checked = True
        lbID.Text = ""
        btnAdd.Text = "Add"
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from crm_statusupdate where id='" & lbID.Text & "'", False)
        ddlClient.SelectedValue = dt.Rows(0)("crmid").ToString
        txtDate.Text = Convert.ToDateTime(dt.Rows(0)("statusdate").ToString).ToString("MM/dd/yyyy")
        txtRemarks.Text = dt.Rows(0)("remarks").ToString
        chkFollowup.Checked = dt.Rows(0)("followup").ToString

        If chkFollowup.Checked Then
            divFollowUp.Visible = True
            divFollowUp1.Visible = True
        Else
            divFollowUp.Visible = False
            divFollowUp1.Visible = False
        End If
        Try
            ddlFollowupBy.SelectedValue = dt.Rows(0)("followupby").ToString
        Catch
        End Try
        txtFollowupDate.Text = Convert.ToDateTime(dt.Rows(0)("followupdate").ToString).ToString("MM/dd/yyyy")
        chkActive.Checked = dt.Rows(0)("active").ToString

        btnAdd.Text = "Update"
    End Sub

    Protected Sub chkFollowup_CheckedChanged(sender As Object, e As EventArgs) Handles chkFollowup.CheckedChanged
        If chkFollowup.Checked Then
            divFollowUp.Visible = True
            divFollowUp1.Visible = True
        Else
            divFollowUp.Visible = False
            divFollowUp1.Visible = False
        End If
    End Sub
End Class