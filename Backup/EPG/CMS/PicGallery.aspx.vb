Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class PicGallery
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then

            End If
        Catch ex As Exception
            Logger.LogError("Pic Gallery", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
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

            Dim lbID As Label = TryCast(e.Row.FindControl("lbGalleryID"), Label)
            Dim hyUpload As HyperLink = TryCast(e.Row.FindControl("hyUpload"), HyperLink)
            hyUpload.NavigateUrl = "javascript:showDiv('" & lbID.Text & "')"
            Dim hyView As HyperLink = TryCast(e.Row.FindControl("hyView"), HyperLink)
            hyView.NavigateUrl = "javascript:showGallery('" & lbID.Text & "')"
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    'Protected Sub OnUploadComplete(sender As Object, e As AjaxFileUploadEventArgs)
    '    Try
    '        'If Session("FileName") <> "0" And Session("Profile") <> "0" Then
    '        Dim adpGetMaxFile As New SqlDataAdapter("select count(*) from picgallery_pictures where galleryId='" & jsGalleryId.Value & "'", ConString)
    '        Dim dtGetMaxFile As New DataTable
    '        adpGetMaxFile.Fill(dtGetMaxFile)

    '        Dim fileName As String = jsGalleryId.Value & "_" & Convert.ToInt32(dtGetMaxFile.Rows(0)(0).ToString) + 1 & ".jpg"
    '        AsyncFileUpload1.SaveAs(Server.MapPath("~/Uploads/GalleryPics/" & fileName))

    '        Dim StrFileSavePath As String = "Uploads/GalleryPics/" & fileName
    '        Dim adp As New SqlDataAdapter("insert into picgallery_pictures(galleryId,url,dateCreated,DateModified,createdBy,ModifiedBy) " _
    '                                      & "values('" & jsGalleryId.Value & "','" & StrFileSavePath & "',dbo.GetLocalDate(),dbo.GetLocalDate(),'" & User.Identity.Name & "','" & User.Identity.Name & "')", ConString)
    '        Dim dt As New DataTable
    '        'adp.Fill(dt)
    '        'grdPicGallery.DataBind()

    '        'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)

        Try
            'If Session("FileName") <> "0" And Session("Profile") <> "0" Then
            Dim adpGetMaxFile As New SqlDataAdapter("select count(*) from picgallery_pictures where galleryId='" & jsGalleryId.Value & "'", ConString)
            Dim dtGetMaxFile As New DataTable
            adpGetMaxFile.Fill(dtGetMaxFile)

            Dim fileName As String = jsGalleryId.Value & "_" & Convert.ToInt32(dtGetMaxFile.Rows(0)(0).ToString) + 1 & ".jpg"
            AsyncFileUpload2.SaveAs(Server.MapPath("~/Uploads/GalleryPics/" & fileName))

            Dim StrFileSavePath As String = "Uploads/GalleryPics/" & fileName
            Dim adp As New SqlDataAdapter("insert into picgallery_pictures(galleryId,url,dateCreated,DateModified,createdBy,ModifiedBy) " _
                                          & "values('" & jsGalleryId.Value & "','" & StrFileSavePath & "',dbo.GetLocalDate(),dbo.GetLocalDate(),'" & User.Identity.Name & "','" & User.Identity.Name & "')", ConString)
            Dim dt As New DataTable
            adp.Fill(dt)
            'grdPicGallery.DataBind()

            'End If

        Catch ex As Exception

        End Try

    End Sub

End Class