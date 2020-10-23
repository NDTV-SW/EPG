Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports Excel
Imports AjaxControlToolkit
Imports ExcelLibrary

Public Class ImportTransliteration
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                ddlLanguageBind()

            End If
            'grdProgrammaster.DataBind()
        Catch ex As Exception
            Logger.LogError("Import Transliteration", "Page Load", ex.Message.ToString, User.Identity.Name)

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

            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select * from mst_language where active=1 and languageid in (2,4,7,8,11,12,19)", False)
            ddlLanguage.DataSource = dt
            ddlLanguage.DataTextField = "FullName"
            ddlLanguage.DataValueField = "LanguageId"
            ddlLanguage.DataBind()

        Catch ex As Exception
            Logger.LogError("ProgramDiscrepancyImport", "ddlLanguageBind", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim excelReader As IExcelDataReader
        If (FileUpload1.HasFile AndAlso (IO.Path.GetExtension(FileUpload1.FileName) = ".xlsx" Or IO.Path.GetExtension(FileUpload1.FileName) = ".xls")) Then
            Try
                excelReader = ExcelReaderFactory.CreateBinaryReader(FileUpload1.FileContent)
                'excelReader.IsFirstRowAsColumnNames = True

                If excelReader.AsDataSet.Tables(0).Rows.Count > 0 Then
                    Dim ImEngProgName As String, ImRegProgName As String
                    For i = 1 To excelReader.AsDataSet.Tables(0).Rows.Count - 1
                        If Not excelReader.AsDataSet.Tables(0).Rows(i).Item(2).ToString = String.Empty Then
                            ImEngProgName = excelReader.AsDataSet.Tables(0).Rows(i).Item(1).ToString
                            ImRegProgName = excelReader.AsDataSet.Tables(0).Rows(i).Item(2).ToString
                            'If ImRegProgName.Trim.Length > 0 Then
                            ImportData(ImEngProgName.Trim, ImRegProgName.Trim, ddlLanguage.SelectedValue.Trim)
                            'End If

                        End If
                    Next
                End If
                grd.DataBind()
            Catch ex As Exception
                Logger.LogError("Import Transliteration", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)

            End Try
        End If
    End Sub

    Private Sub ImportData(ByVal vProgName As String, ByVal vProgramRegionalname As String, ByVal vLanguageId As Integer)
        Dim obj As New clsExecute
        vProgName = vProgName.Replace("'", "''")
        vProgramRegionalname = vProgramRegionalname.Replace("'", "''")
        Dim sql As String = "insert into dictionary_dump_import(progname,regprogname,languageid) values('" & vProgName & "',N'" & vProgramRegionalname & "','" & ddlLanguage.SelectedValue & "')"
        Try
            obj.executeSQL(sql, False)
        Catch ex As Exception

        End Try

    End Sub

End Class