Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class ReportImages
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                ddlOperator.DataBind()
                grd.DataBind()
                grdSummary.DataBind()
            End If
        Catch ex As Exception
            Logger.LogError("Report Images", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        grd.DataBind()
        grdSummary.DataBind()
    End Sub
End Class