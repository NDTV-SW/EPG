Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class Mail
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString


    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
        Catch ex As Exception
            Logger.LogError("Mail", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub ddlContactPerson_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlContactPerson.SelectedIndexChanged
        Try
            ' lbContactPerson.Text = ddlContactPerson.SelectedItem.Text
        Catch
        End Try
    End Sub

    Protected Sub ddlCompanyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompanyName.SelectedIndexChanged
        Try
            'lbContactPerson.Text = ddlContactPerson.SelectedItem.Text
        Catch ex As Exception
            Logger.LogError("Mail.aspx", "ddlCompanyName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnMail_Click(sender As Object, e As EventArgs) Handles btnMail.Click
        Try
            'exec dbmail_v1 'epg@ndtv.com','Test mail from cloud',null,'Hi, this mail is with attachment. Please download the attachment and confirm. ','C:\inetpub\wwwroot\attachment.txt'
            exec_Proc("sankalp@ndtv.com", "", "", "Hi this is Mail from DB Cloud local server", "")
        Catch ex As Exception
            Logger.LogError("Mail", "btnMail_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub exec_Proc(ByVal vRecipientName As String, ByVal vSubject As String, ByVal vBcc As String, ByVal vBody As String, ByVal vAttachment As String)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("dbmail_v1", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RecipientName", SqlDbType.NVarChar, 500)).Value = vRecipientName.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Subject", SqlDbType.NVarChar, 400)).Value = vSubject.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@bcc", SqlDbType.NVarChar, 500)).Value = vBcc.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Body", SqlDbType.NVarChar, 4000)).Value = vBody.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@attachment", SqlDbType.NVarChar, 4000)).Value = vAttachment.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "Getmail")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Mail", "exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class
