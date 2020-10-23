Imports System
Imports System.Data.SqlClient
Imports System.Net
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Data.OleDb
Imports System.Runtime.Serialization.Json
Imports Newtonsoft.Json



Public Class GoogleTranslate
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
                ddlLanguageBind()
            End If
        Catch ex As Exception
            Logger.LogError("Dictionary Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Private Sub ddlLanguageBind()
        Try
            Dim i As Integer
            i = ddlLanguage.Items.Count - 1
            While i > 0
                ddlLanguage.Items.RemoveAt(i)
                i = i - 1
            End While
            'ddlLanguage.Items.Add("Select")
            Dim adp As New SqlDataAdapter("select * from mst_language where active=1", ConString)
            Dim ds As New DataSet
            adp.Fill(ds)
            ddlLanguage.DataSource = ds
            ddlLanguage.DataTextField = "FullName"
            ddlLanguage.DataValueField = "LanguageId"
            ddlLanguage.DataBind()

        Catch ex As Exception
            Logger.LogError("Dictionary Master", "ddlLanguageBind", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnTranslate_Click(sender As Object, e As EventArgs) Handles btnTranslate.Click
        Try
            txtRegName.Text = TranslateFromGoogle.Translate(txtEngName.Text, ddlLanguage.SelectedItem.Text)
        Catch ex As Exception
            Logger.LogError("Google translate", "btnSearch_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class