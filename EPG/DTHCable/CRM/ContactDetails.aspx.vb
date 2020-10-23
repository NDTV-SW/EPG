Public Class ContactDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ddlClient.DataBind()
            Dim strClient As String = Request.QueryString("client")
            If Not IsNothing(strClient) Then

                If strClient.Length > 0 Then
                    Dim obj As New clsExecute
                    Dim dt As DataTable = obj.executeSQL("select id from crm where clientname='" & strClient & "'", False)
                    If dt.Rows.Count > 0 Then
                        ddlClient.SelectedValue = dt.Rows(0)("id").ToString
                        grd.DataBind()
                    End If

                End If
            End If
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        Dim sql As String
        If btnAdd.Text.ToUpper = "ADD" Then
            sql = "insert into crm_contact(crmid,name,contact,mailid,active) values"
            sql = sql & "('" & ddlClient.SelectedValue & "','" & txtName.Text & "','" & txtContact.Text & "','" & txtMail.Text & "','" & chkActive.Checked & "')"
        Else
            sql = "update crm_contact set crmid='" & ddlClient.SelectedValue & "',name='" & txtName.Text & "',contact='" & txtContact.Text & "',"
            sql = sql & "mailid='" & txtMail.Text & "',active='" & chkActive.Checked & "' where id='" & lbID.Text & "'"
        End If
        obj.executeSQL(sql, False)
        clearAll()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Private Sub clearAll()
        'ddlClient.SelectedIndex = 0
        txtName.Text = ""
        txtContact.Text = ""
        txtMail.Text = ""
        chkActive.Checked = True
        lbID.Text = ""
        btnAdd.Text = "Add"
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from crm_contact where id='" & lbID.Text & "'", False)
        ddlClient.SelectedValue = dt.Rows(0)("crmid").ToString
        txtName.Text = dt.Rows(0)("name").ToString
        txtContact.Text = dt.Rows(0)("contact").ToString
        txtMail.Text = dt.Rows(0)("mailid").ToString
        chkActive.Checked = dt.Rows(0)("active").ToString

        btnAdd.Text = "Update"
    End Sub
End Class