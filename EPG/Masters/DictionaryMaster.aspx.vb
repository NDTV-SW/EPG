Imports System
Imports System.Data.SqlClient
Public Class DictionaryMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                ddlLanguageBind()

            End If
        Catch ex As Exception
            Logger.LogError("Dictionary Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Dim dt As DataTable
    Private Sub bindGrid()
        Dim obj As New clsExecute
        dt = obj.executeSQL("select progname,regprogname from dictionary_dump where languageid='" & ddlLanguage.SelectedValue & "'", False)
        'dt = obj.executeSQL("select distinct progname,regprogname from (select progname, regprogname =(select progname from mst_programregional where progid=x.progid and languageid='" & ddlLanguage.SelectedValue & "' and episodeno=0) from mst_program x ) a where regprogname is not null order by 1 desc", False)
        grdDictionary.DataSource = dt
        grdDictionary.DataBind()
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
            Dim adp As New SqlDataAdapter("select * from mst_language where active=1 and languageid in (1,2,4,7,8,11,12)", ConString)
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
    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        bindGrid()
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim obj As New clsExecute
        dt = obj.executeSQL("select progname,regprogname from dictionary_dump where languageid='" & ddlLanguage.SelectedValue & "'", False)
        'dt = obj.executeSQL("select distinct progname,regprogname from (select progname, regprogname =(select progname from mst_programregional where progid=x.progid and languageid='" & ddlLanguage.SelectedValue & "' and episodeno=0) from mst_program x ) a where regprogname is not null order by 1 desc", False)
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
                            Dim dtInsert As DataTable = obj.executeSQL("insert into mst_dictionary(engName,regName,languageID) values ('" & EngName & "',N'" & RegName & "','" & ddlLanguage.SelectedValue & "')", False)

                        Catch
                        End Try
                    Next
                End If

            Next
        End If

    End Sub
End Class