Public Class UserMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'bindGrid()
    End Sub
    Private Sub bindGrid()
        Try
    
        Catch ex As Exception
            Logger.LogError("UserMaster", "bindGrid", ex.Message.ToString, user.identity.name)
        End Try
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Dim lbUserName As Label = TryCast(grd.Rows(grd.SelectedIndex).FindControl("lbUserName"), Label)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from mstUsers where uname='" & lbUserName.Text & "'", False)
        txtUserName.Text = dt.Rows(0)("uname").ToString
        txtMailId.Text = dt.Rows(0)("mailid").ToString
        ddlRole.SelectedValue = dt.Rows(0)("roles").ToString
        chkActive.Checked = dt.Rows(0)("active").ToString




        chkPasswordChangeRequired.Checked = dt.Rows(0)("passwordChangeRequired").ToString
        btnSubmit.Text = "Update"
        txtUserName.Enabled = False
        RFV_txtPassword.Enabled = False
        RFV_txtRepeatPassword.Enabled = False
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim obj As New clsExecute
            If btnSubmit.Text.ToUpper = "ADD" Then
                obj.executeSQL("insert into mstusers(uname,upass,active,roles,passwordchangerequired,mailid,addedby,addedat) values('" & txtUserName.Text & "','" & FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "md5") & "','" & chkActive.Checked & "','" & ddlRole.SelectedValue & "','" & chkPasswordChangeRequired.Checked & "','" & txtMailId.Text & "','" & User.Identity.Name & "',dbo.getlocaldate())", False)
            Else
                If txtPassword.Text = "" Then
                    obj.executeSQL("update mstusers set active='" & chkActive.Checked & "',roles='" & ddlRole.SelectedValue & "',passwordchangerequired='" & chkPasswordChangeRequired.Checked & "',mailid='" & txtMailId.Text & "' where uname='" & txtUserName.Text & "'", False)
                Else
                    obj.executeSQL("update mstusers set upass='" & FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "md5") & "',active='" & chkActive.Checked & "',roles='" & ddlRole.SelectedValue & "',passwordchangerequired='" & chkPasswordChangeRequired.Checked & "',mailid='" & txtMailId.Text & "' where uname='" & txtUserName.Text & "'", False)
                End If
            End If
            ClearAll()
        Catch
            lbStatus.Visible = True
        End Try
    End Sub
    Protected Sub ClearAll()
        txtUserName.Text = ""
        txtMailId.Text = ""
        txtPassword.Text = ""
        txtRepeatPassword.Text = ""
        chkActive.Checked = True
        chkPasswordChangeRequired.Checked = True
        btnSubmit.Text = "Add"
        txtUserName.Enabled = True
        RFV_txtPassword.Enabled = True
        RFV_txtRepeatPassword.Enabled = True
        grd.SelectedIndex = -1
        grd.DataBind()
        lbStatus.Visible = False
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearAll()
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Dim lbUserName As Label = TryCast(grd.Rows(e.RowIndex).FindControl("lbUserName"), Label)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("delete from mstUsers where uname='" & lbUserName.Text & "'", False)
        ClearAll()
    End Sub

    Protected Sub grd_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted

        e.ExceptionHandled = True

    End Sub
End Class