Imports System
Imports System.Data.SqlClient
Public Class DictionaryMaster
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

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim adp As New SqlDataAdapter("rpt_mst_dictionary", ConString)
        adp.SelectCommand.CommandType = CommandType.StoredProcedure
        adp.SelectCommand.Parameters.Add("@languageid", SqlDbType.Int).Value = ddlLanguage.SelectedValue
        Dim dt As New DataTable
        adp.Fill(dt)
        Dim strEngName() As String, strRegName() As String

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                strEngName = dt.Rows(i).Item(0).ToString().Split(" ")
                strRegName = dt.Rows(i).Item(1).ToString().Split(" ")
                Dim EngName As String, RegName As String
                If strEngName.Length = strRegName.Length And strEngName.Length > 0 Then
                    For j As Integer = 0 To strEngName.Length - 1
                        EngName = strEngName(j).Trim
                        RegName = strRegName(j).Trim
                        Try
                            Dim adpInsert As New SqlDataAdapter("insert into mst_dictionary(engName,regName,languageID) values ('" & EngName & "',N'" & RegName & "','" & ddlLanguage.SelectedValue & "')", ConString)
                            Dim dtInsert As New DataTable
                            adpInsert.Fill(dtInsert)
                        Catch
                        End Try
                    Next
                End If
            
            Next
        End If

    End Sub
End Class