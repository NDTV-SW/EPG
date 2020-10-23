Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class PicGalleryMaster
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

    Protected Sub grdPicGallery_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdPicGallery.SelectedIndexChanged
        Try

            lbGalleryId.Text = DirectCast(grdPicGallery.SelectedRow.FindControl("lbGalleryId"), Label).Text
            ddlCategory.SelectedValue = DirectCast(grdPicGallery.SelectedRow.FindControl("lbCategoryid"), Label).Text
            ddlTvStar.SelectedValue = DirectCast(grdPicGallery.SelectedRow.FindControl("lbProfileid"), Label).Text
            'txtCategoryName.Text = DirectCast(grdPicGallery.SelectedRow.FindControl("lbcategoryname"), Label).Text
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("TV Star Master", "grdPicGallery_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strSql As String
        If btnUpdate.Text = "ADD" Then
            strSql = "insert into picgallery_master(categoryId,ProfileId,dateCreated,DateModified,createdBy,ModifiedBy) " _
                & "values ('" & ddlCategory.SelectedValue & "','" & ddlTvStar.SelectedValue & "',dbo.GetLocalDate(),dbo.GetLocalDate(),'" & User.Identity.Name & "','" & User.Identity.Name & "')"
        Else
            strSql = "update picgallery_master set categoryId='" & ddlCategory.SelectedValue & "', ProfileId='" & ddlTvStar.SelectedValue & "',dateModified=dbo.GetLocalDate(),ModifiedBy='" & User.Identity.Name & "' where categoryid ='" & lbGalleryId.Text & "'"
        End If
        Dim adp As New SqlDataAdapter(strSql, ConString)
        Dim dt As New DataTable
        adp.Fill(dt)
        grdPicGallery.SelectedIndex = -1
        grdPicGallery.DataBind()
        clearAll()
    End Sub

    Private Sub clearAll()
        'txtCategoryName.Text = String.Empty

        btnUpdate.Text = "ADD"
    End Sub

    Dim intSno As Integer
    Protected Sub grdPicGallery_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPicGallery.RowDataBound
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