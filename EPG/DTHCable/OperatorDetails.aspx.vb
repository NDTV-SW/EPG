Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class OperatorDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ddlOperatorParent.DataSource = sqlDSOperatorList
            ddlOperatorParent.DataTextField = "Name"
            ddlOperatorParent.DataValueField = "operatorid"
            ddlOperatorParent.DataBind()
            ddlOperatorParent.Items.Insert(0, New ListItem("Select", "0"))
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            If btnSave.Text = "Update" Then
                Dim sqlString As String


                sqlString = "UPDATE mst_dthcableoperators SET NAME='" & txtName.Text.Trim & "',fullName='" & txtFullName.Text.Trim & "',"
                sqlString = sqlString & "contactPerson='" & txtContactPerson.Text & "',contactEmail='" & txtContactEmail.Text.Trim & "',contactPhone='" & txtContactPhone.Text.Trim & "',"
                sqlString = sqlString & "MailSubject='" & txtMailSubject.Text & "', MailUpdateSubject='" & txtMailUpdateSubject.Text & "',MailBody='" & txtMailBody.Text.Trim & "',sendAttachment='" & chkSendAttachment.Checked & "',"
                sqlString = sqlString & "FTP='" & chkFTP.Checked & "',FTPip='" & txtFTPip.Text.Trim & "',FTPuserName='" & txtFTPuserName.Text.Trim & "',ftpPassword='" & txtFTPPassword.Text.Trim & "',"
                sqlString = sqlString & "FTPport='" & txtFTPPort.Text & "',FTPFingerprint='" & txtFTPfingerPrint.Text & "',parentOperatorid='" & ddlOperatorParent.SelectedValue & "' WHERE OPERATORID='" & lbOperatorID.Text & "'"


                Dim obj As New clsExecute
                obj.executeSQL(sqlString, False)
                clearAll()
                GridView1.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                Dim lbName As Label = DirectCast(e.Row.FindControl("lbName"), Label)
                Dim lbMappingTable As Label = DirectCast(e.Row.FindControl("lbMappingTable"), Label)
                Dim hyView As HyperLink = DirectCast(e.Row.FindControl("hyView"), HyperLink)
                hyView.NavigateUrl = "Operatorchannels.aspx?operator=" & lbName.Text & "&opTable=" & lbMappingTable.Text & ""
            Catch
            End Try
        End If
    End Sub

    Private Sub clearAll()
        txtName.Text = ""

        txtFullName.Text = ""
        txtContactPerson.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtMailSubject.Text = ""
        txtMailUpdateSubject.Text = ""
        txtMailBody.Text = ""
        chkFTP.Checked = False
        txtFTPip.Text = ""
        txtFTPuserName.Text = ""
        txtFTPPassword.Text = ""
        txtFTPfingerPrint.Text = ""
        txtFTPPort.Text = ""
        ddlOperatorParent.SelectedValue = 0
        btnSave.Text = "Save"

        GridView1.SelectedIndex = -1
        GridView1.DataBind()

    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged    
        lbOperatorID.Text = DirectCast(GridView1.SelectedRow.FindControl("lbOperatorID"), Label).Text
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("SELECT * FROM mst_dthcableoperators WHERE OPERATORID='" & lbOperatorID.Text & "'", False)
        
        txtName.Text = dt.Rows(0)("name").ToString
        txtFullName.Text = dt.Rows(0)("fullname").ToString
        txtContactPerson.Text = dt.Rows(0)("contactPerson").ToString
        txtContactEmail.Text = dt.Rows(0)("contactEmail").ToString
        txtContactPhone.Text = dt.Rows(0)("contactPhone").ToString
        txtMailSubject.Text = dt.Rows(0)("mailSubject").ToString
        txtMailUpdateSubject.Text = dt.Rows(0)("mailUpdateSubject").ToString
        txtMailBody.Text = dt.Rows(0)("mailBody").ToString
        chkFTP.Checked = dt.Rows(0)("FTP").ToString
        chkSendAttachment.Checked = dt.Rows(0)("SendAttachment").ToString
        txtFTPip.Text = dt.Rows(0)("FTPip").ToString
        txtFTPuserName.Text = dt.Rows(0)("FTPusername").ToString
        txtFTPPassword.Text = dt.Rows(0)("FTPPassword").ToString
        txtFTPfingerPrint.Text = dt.Rows(0)("FTPFingerprint").ToString
        txtFTPPort.Text = dt.Rows(0)("FTPPort").ToString
        ddlOperatorParent.SelectedValue = dt.Rows(0)("parentOperatorID").ToString
        btnSave.Text = "Update"
    End Sub

    
End Class