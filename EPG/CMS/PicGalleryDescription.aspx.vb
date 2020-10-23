Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class PicGalleryDescription
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

            lbRowId.Text = DirectCast(grdPicGallery.SelectedRow.FindControl("lbRowId"), Label).Text
            ddlGallery.SelectedValue = DirectCast(grdPicGallery.SelectedRow.FindControl("lbGalleryId"), Label).Text
            ddlLanguage.SelectedValue = DirectCast(grdPicGallery.SelectedRow.FindControl("lbLanguageId"), Label).Text
            txtTitle.Text = DirectCast(grdPicGallery.SelectedRow.FindControl("lbTitle"), Label).Text
            txtSynopsis.Text = DirectCast(grdPicGallery.SelectedRow.FindControl("lbSynopsis"), Label).Text
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
            strSql = "insert into picgallery_desc(galleryId,Title,Synopsis,LanguageId,dateCreated,DateModified,createdBy,ModifiedBy) " _
                & "values ('" & ddlGallery.SelectedValue & "','" & txtTitle.Text & "','" & txtSynopsis.Text & "','" & ddlLanguage.SelectedValue & "',dbo.GetLocalDate(),dbo.GetLocalDate(),'" & User.Identity.Name & "','" & User.Identity.Name & "')"
        Else
            strSql = "update picgallery_desc set Title='" & txtTitle.Text & "', Synopsis='" & txtSynopsis.Text & "',dateModified=dbo.GetLocalDate(),ModifiedBy='" & User.Identity.Name & "' where rowId ='" & lbRowId.Text & "'"
        End If
        Dim adp As New SqlDataAdapter(strSql, ConString)
        Dim dt As New DataTable
        adp.Fill(dt)
        grdPicGallery.SelectedIndex = -1
        grdPicGallery.DataBind()
        clearAll()
    End Sub

    Private Sub clearAll()
        txtTitle.Text = String.Empty
        txtSynopsis.Text = String.Empty
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