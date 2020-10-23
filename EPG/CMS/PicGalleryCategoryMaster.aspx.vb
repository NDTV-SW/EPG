Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class PicGalleryCategoryMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then

            End If
        Catch ex As Exception
            Logger.LogError("TV Star Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdPicGalleryCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdPicGalleryCategory.SelectedIndexChanged
        Try

            lbCategoryId.Text = DirectCast(grdPicGalleryCategory.SelectedRow.FindControl("lbcategoryid"), Label).Text
            txtCategoryName.Text = DirectCast(grdPicGalleryCategory.SelectedRow.FindControl("lbcategoryname"), Label).Text
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("TV Star Master", "grdPicGalleryCategory_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strSql As String
        If btnUpdate.Text = "ADD" Then
            strSql = "insert into picgallery_category(categoryname)  values ('" & txtCategoryName.Text & "')"
        Else
            strSql = "update picgallery_category set categoryname='" & txtCategoryName.Text & "' where categoryid ='" & lbCategoryId.Text & "'"
        End If
        Dim adp As New SqlDataAdapter(strSql, ConString)
        Dim dt As New DataTable
        adp.Fill(dt)
        grdPicGalleryCategory.SelectedIndex = -1
        grdPicGalleryCategory.DataBind()
        clearAll()
    End Sub

    Private Sub clearAll()
        txtCategoryName.Text = String.Empty
        
        btnUpdate.Text = "ADD"
    End Sub

    Dim intSno As Integer
    Protected Sub grdPicGalleryCategory_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPicGalleryCategory.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

End Class