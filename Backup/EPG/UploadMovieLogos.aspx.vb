Imports System
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadMovieLogos
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ddlMovieName.DataBind()
            setImage()
        End If

    End Sub

    Private Sub uploadFile()
        Try
            'removeImage()
            Dim filename As String = AjaxFileUpload11.FileName
            lbStatus.Visible = False
            If filename.ToLower.EndsWith(".jpg") Or filename.ToLower.EndsWith(".jpeg") Then
                Dim strUploadPath As String = "~/uploads/MovieImages/"
                strUploadPath = getFilePath(True)
                AjaxFileUpload11.SaveAs(strUploadPath)
                AjaxFileUpload11.Dispose()
                System.Threading.Thread.Sleep(1000)

                Dim abc As New clsFTP
                abc.doS3Task(strUploadPath, "/uploads/MovieImages")
                System.Threading.Thread.Sleep(1000)

                Dim MyConnection As New SqlConnection
                MyConnection = New SqlConnection(ConString)
                Dim adp As New SqlDataAdapter("update mst_moviesdb set TMDBImageURL='" & getWebPath() & "' where RowId='" & ddlMovieName.SelectedValue & "'", MyConnection)
                Dim dt As New DataTable
                adp.Fill(dt)
                imgProgramlogo.ImageUrl = getFilePath(False)
                hyProgramLogo.NavigateUrl = getFilePath(False)
                getImageSize(getFilePath(True))
                grdProgImage.DataBind()
            Else
                lbStatus.Visible = True
                lbStatus.Text = "File Uploaded but not saved. Please check Extension of file"
            End If
        Catch ex As Exception
            Logger.LogError("UploadMovieLogos", "uploadFile", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub removeImage()
        Try

            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            Dim adp As New SqlDataAdapter("update mst_moviesdb set TMDBImageURL=NULL where rowid='" & ddlMovieName.SelectedValue & "'", MyConnection)
            Dim dt As New DataTable
            adp.Fill(dt)
            grdProgImage.DataBind()

        Catch ex As Exception
            Logger.LogError("UploadMovieLogos", "removeImage", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdProgImage_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgImage.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    Dim imglogo As Image = DirectCast(e.Row.FindControl("imglogo"), Image)
                    Dim hylogo As HyperLink = DirectCast(e.Row.FindControl("hylogo"), HyperLink)
                    Dim lbRowId As Label = DirectCast(e.Row.FindControl("lbRowId"), Label)
                    Dim lbMovieName As Label = DirectCast(e.Row.FindControl("lbMovieName"), Label)
                    Dim lbTMDBImageUrl As Label = DirectCast(e.Row.FindControl("lbTMDBImageUrl"), Label)

                    imglogo.ImageUrl = lbTMDBImageUrl.Text
                    hylogo.NavigateUrl = lbTMDBImageUrl.Text
            End Select
        Catch ex As Exception
            Logger.LogError("UploadMovieLogos", "grdProgImage_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Protected Sub grdProgImage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdProgImage.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdProgImage.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbTMDBImageURL As Label = DirectCast(grdProgImage.SelectedRow.FindControl("lbTMDBImageURL"), Label)
            imgProgramlogo.ImageUrl = getFilePath(False)
            hyProgramLogo.NavigateUrl = getFilePath(False)
            getImageSize(getFilePath(True))
            ddlMovieName.SelectedValue = lbRowId.Text
            setImage()
        Catch ex As Exception
            Logger.LogError("UploadMovieLogos", "grdProgImage_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Protected Sub doUpload(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        uploadFile()
    End Sub

    Private Sub setImage()
        'imgProgramlogo.ImageUrl = getFilePath(False)
        'getImageSize(getFilePath(True))
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        Dim adp As New SqlDataAdapter("select starcast,synopsis,director,country,genre,TMDBImageURL from mst_moviesdb where RowId='" & ddlMovieName.SelectedValue & "'", MyConnection)
        Dim dt As New DataTable
        adp.Fill(dt)

        imgProgramlogo.ImageUrl = dt.Rows(0)("TMDBImageURL").ToString
        getImageSize(dt.Rows(0)("TMDBImageURL").ToString)
        hyProgramLogo.NavigateUrl = dt.Rows(0)("TMDBImageURL").ToString


        lbStarCast.Text = dt.Rows(0)("starcast").ToString()
        'lbSynopsis.Text = dt.Rows(0)("synopsis").ToString()
        lbDirector.Text = dt.Rows(0)("director").ToString()
        lbcountry.Text = dt.Rows(0)("country").ToString()
        lbGenre.Text = dt.Rows(0)("genre").ToString()

    End Sub

    Private Function getFileName() As String
        Return sanitizeFile(ddlMovieName.SelectedValue & "_" & ddlMovieName.SelectedItem.Text) & ".jpg"
    End Function

    Private Function getFilePath(ByVal exactPath As Boolean) As String
        Dim strSanitizedFilename As String = sanitizeFile(ddlMovieName.SelectedItem.Text & "_" & ddlMovieName.SelectedValue)
        If exactPath Then
            Return Server.MapPath("~/uploads/MovieImages/" & strSanitizedFilename & ".jpg")
        Else
            Return "~/uploads/MovieImages/" & strSanitizedFilename & ".jpg"
        End If
    End Function

    Private Function getWebPath() As String
        Dim strSanitizedFilename As String = sanitizeFile(ddlMovieName.SelectedItem.Text & "_" & ddlMovieName.SelectedValue)
        Return "http://epgops.ndtv.com/uploads/MovieImages/" & strSanitizedFilename & ".jpg"

    End Function

    Private Function sanitizeFile(ByVal strToSanitize As String) As String
        strToSanitize = strToSanitize.Replace("<", "")
        strToSanitize = strToSanitize.Replace(">", "")
        strToSanitize = strToSanitize.Replace(":", "")
        strToSanitize = strToSanitize.Replace("'", "")
        strToSanitize = strToSanitize.Replace("""", "")
        strToSanitize = strToSanitize.Replace("/", "")
        strToSanitize = strToSanitize.Replace("\", "")
        strToSanitize = strToSanitize.Replace("?", "")
        strToSanitize = strToSanitize.Replace("*", "")
        Return strToSanitize
    End Function

    Private Sub clearall()
        lbImageSize.Text = ""
        hyProgramLogo.NavigateUrl = ""
        imgProgramlogo.ImageUrl = ""
    End Sub

    
    Private Sub getImageSize(ByVal strImagePath As String)
        lbImageSize.Text = getImage.getImageSize(strImagePath)
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        uploadFile()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        removeImage()
        clearall()
    End Sub

    Protected Sub ddlMovieName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMovieName.SelectedIndexChanged
        setImage()
    End Sub
End Class
