Imports System
Imports System.Data.SqlClient

Public Class EditXMLMailDate
    Inherits System.Web.UI.Page
    Dim ConString As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ConnectionString)

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                lbRowid.Text = Request.QueryString("rowid").ToString
                
            Catch ex As Exception
                Logger.LogError("Edit XML Mailed Date", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Try
            '            exec_XMLMailedOn(lbRowid.Text, txtXMLMailedOn.Text)

            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            MyDataAdapter = New SqlDataAdapter("update aud_epg_xml_ftp set xmlmailedon='" & txtXMLMailedOn.Text & "' where rowid='" & lbRowid.Text & "'", ConString)
            MyDataAdapter.SelectCommand.CommandType = CommandType.Text
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "XMLMailedOn")
            MyDataAdapter.Dispose()
            ConString.Close()

            hfSend.Value = "2"

        Catch ex As Exception
            Logger.LogError("Edit XML Mailed On", "BtnSubmit_Click", ex.Message.ToString, User.Identity.Name)
        Finally
            ConString.Close()
        End Try
    End Sub
    
End Class