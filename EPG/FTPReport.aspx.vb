Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class FTPReport
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
            If Page.IsPostBack = False Then
                txtFTPDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
            End If

        Catch ex As Exception
            Logger.LogError("Error report", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnView.Click
        gridBind()
    End Sub
    Private Sub gridBind()
        Dim sql As String
        If txtChannel.Text = "" Then
            sql = "select distinct top 500 Channelid,Filename,loggedinuser,convert (varchar,ftpdate,106) + ' ' + left(convert (varchar,ftpdate,108),5) as ftpdate1 from ftp_records where convert(varchar,ftpdate,112)=  '" & Convert.ToDateTime(txtFTPDate.Text).ToString("yyyyMMdd") & "' order by ftpdate1 desc"
        Else
            sql = "select Channelid,Filename,loggedinuser,convert (varchar,ftpdate,106) + ' ' + left(convert (varchar,ftpdate,108),5) as ftpdate1 from ftp_records where channelid='" & txtChannel.Text & "' and convert(varchar,ftpdate,112)= '" & Convert.ToDateTime(txtFTPDate.Text).ToString("yyyyMMdd") & "' order by ftpdate desc"
        End If
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL(sql, False)
        grdFTPReport.DataSource = dt
        grdFTPReport.DataBind()
    End Sub
    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

End Class