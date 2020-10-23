Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class _DefaultMso
    Inherits System.Web.UI.Page
    Dim myConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Dim loggedInUser As String = ""
    Dim _arrCount As New ArrayList()
    Dim _PrevDept As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub


    Protected Sub grdMSOs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdMSOs.SelectedIndexChanged
        btnAdd.Text = "UPDATE"
        txtRowId.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbID"), Label).Text
        txtOperatorName.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbMSOName"), Label).Text
        txtCity.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbMSOCity"), Label).Text
        txtContactPerson.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbMSOContactPerson"), Label).Text
        txtContactNumber.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbMSOContactNumber"), Label).Text
        txtEmail.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbMSOContactEmail"), Label).Text
        txtServiceStartDate.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbServiceStartDate"), Label).Text
        txtAgreementEndDate.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbAgreementEndDate"), Label).Text
        txtBillingStartDate.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbBillingStartDate"), Label).Text
        txtBillingPerMonth.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbBillingPerMonth"), Label).Text
        txtRemarks.Text = TryCast(grdMSOs.SelectedRow.FindControl("lbMSORemarks"), Label).Text
        chkDealSealed.Checked = TryCast(grdMSOs.SelectedRow.FindControl("chkMSODealSealed"), CheckBox).Checked
        chkAgreementSigned.Checked = TryCast(grdMSOs.SelectedRow.FindControl("chkAgreementSigned"), CheckBox).Checked

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim sql As String = ""
        Dim dt As New DataTable

        If btnAdd.Text = "ADD" Then
            sql = "insert into DISTRIBUTIONMSOTRACK(operatorname,city,contactperson,contactnumber,contactemail,dealsealed,agreementSigned,servicesStartDate,AgreementEndDate,billingStartDate,billingPerMonth,remarks,lastupdated) "
            sql = sql & "values('" & txtOperatorName.Text.Replace("'", "''") & "','" & txtCity.Text.Replace("'", "''") & "','" & txtContactPerson.Text.Replace("'", "''") & "','" & txtContactNumber.Text.Replace("'", "''") & "','" & txtEmail.Text.Replace("'", "''") & "','" & chkDealSealed.Checked & "','" & chkAgreementSigned.Checked & "','" & txtServiceStartDate.Text & "','" & txtAgreementEndDate.Text & "','" & txtBillingStartDate.Text & "','" & txtBillingPerMonth.Text & "','" & txtRemarks.Text.Replace("'", "''") & "',getdate())"
        Else
            sql = "update DISTRIBUTIONMSOTRACK set operatorname='" & txtOperatorName.Text.Replace("'", "''") & "',city='" & txtCity.Text.Replace("'", "''") & "',contactperson='" & txtContactPerson.Text.Replace("'", "''") & "',contactnumber='" & txtContactNumber.Text.Replace("'", "''") & "',contactemail='" & txtEmail.Text.Replace("'", "''") & "',dealSealed='" & chkDealSealed.Checked & "',agreementSigned='" & chkAgreementSigned.Checked & "',ServicesStartDate='" & txtServiceStartDate.Text.Replace("'", "''") & "',AgreementEndDate='" & txtAgreementEndDate.Text & "',billingstartdate='" & txtBillingStartDate.Text.Replace("'", "''") & "',billingpermonth='" & txtBillingPerMonth.Text.Replace("'", "''") & "',remarks='" & txtRemarks.Text.Replace("'", "''") & "',lastupdated=getdate() where rowid='" & txtRowId.Text & "'"
        End If

        Dim adp As New SqlDataAdapter(sql, myConnection)
        adp.Fill(dt)
        adp.Dispose()
        clearAll()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub
    Private Sub clearAll()
        txtRowId.Text = ""
        txtOperatorName.Text = ""
        txtCity.Text = ""
        txtContactPerson.Text = ""
        txtContactNumber.Text = ""
        txtEmail.Text = ""
        txtRemarks.Text = ""
        txtServiceStartDate.Text = ""
        txtAgreementEndDate.Text = ""
        txtBillingStartDate.Text = ""
        txtBillingPerMonth.Text = ""
        chkDealSealed.Checked = False
        chkAgreementSigned.Checked = False
        btnAdd.Text = "ADD"
        grdMSOs.SelectedIndex = -1
        grdMSOs.DataBind()
    End Sub

    Protected Sub grdMSOs_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMSOs.RowDeleting
        Dim strId As String = TryCast(grdMSOs.Rows(e.RowIndex).FindControl("lbId"), Label).Text
        Dim sql As String = ""
        Dim dt As New DataTable
        sql = "delete from DISTRIBUTIONMSOTRACK where rowid='" & strId & "'"
        Dim adp As New SqlDataAdapter(sql, myConnection)
        adp.Fill(dt)
        adp.Dispose()
        clearAll()
    End Sub

    Protected Sub grdMSOs_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdMSOs.RowDeleted
        If e.ExceptionHandled = False Then
            e.ExceptionHandled = True
        End If

    End Sub
    Dim intSno As Integer
    Protected Sub grdMSOs_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMSOs.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1
        End If

    End Sub
End Class