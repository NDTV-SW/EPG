Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class ViewPicGallery
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim strGalleryId As String = Request.QueryString("GalleryId")
                Dim adp As New SqlDataAdapter("select 'http://epgops.ndtv.com/' + url profilePath1,* from picgallery_pictures where galleryId='" & strGalleryId & "' order by dateCreated", ConString)
                Dim dt As New DataTable
                adp.Fill(dt)
                grdPicGallery.DataSource = dt
                grdPicGallery.DataBind()
            End If
        Catch ex As Exception
            Logger.LogError("View Pic Gallery", "Page Load", ex.Message.ToString, User.Identity.Name)
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

        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub
    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        Dim fileSize As Integer = Int32.Parse(Path.GetFileName(e.FileSize)) / 1024

        If (fileSize > 5000) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('File size greater than 5000KB. Please optimize.');", True)
            Exit Sub
        End If
        Try
            'If Session("FileName") <> "0" And Session("Profile") <> "0" Then
            Dim adpGetMaxFile As New SqlDataAdapter("select count(*) from picgallery_pictures where galleryId='" & jsGalleryId.Value & "'", ConString)
            Dim dtGetMaxFile As New DataTable
            adpGetMaxFile.Fill(dtGetMaxFile)

            Dim fileName As String = jsGalleryId.Value & "_" & Convert.ToInt32(dtGetMaxFile.Rows(0)(0).ToString) + 1 & ".jpg"
            Dim strfilepath As String = Server.MapPath("~/Uploads/galleryPics/" & fileName)
            AsyncFileUpload2.SaveAs(strfilepath)

            Dim abc As New clsFTP
            abc.doS3Task(strfilepath, "/uploads/GalleryPics")
            System.Threading.Thread.Sleep(200)

            Dim StrFileSavePath As String = "uploads/galleryPics/" & fileName
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