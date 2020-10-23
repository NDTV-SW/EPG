Imports System
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class ViewMovieLogos
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

        End If
        bindGrid()
    End Sub


    Protected Sub grdProgImage_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgImage.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    Dim imglogo As Image = DirectCast(e.Row.FindControl("imglogo"), Image)
                    Dim hylogo As HyperLink = DirectCast(e.Row.FindControl("hylogo"), HyperLink)
                    Dim lbRowId As Label = DirectCast(e.Row.FindControl("lbRowId"), Label)
                    Dim lbTMDBImageURL As Label = DirectCast(e.Row.FindControl("lbTMDBImageURL"), Label)

                    imglogo.ImageUrl = lbTMDBImageURL.Text
                    hylogo.NavigateUrl = lbTMDBImageURL.Text

            End Select
        Catch ex As Exception
            Logger.LogError("UploadProgrammeLogos", "grdProgImage_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Protected Sub grdProgImage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdProgImage.SelectedIndexChanged
        Try
            Dim lbProgId As Label = DirectCast(grdProgImage.SelectedRow.FindControl("lbProgId"), Label)
            Dim lbProgLogo As Label = DirectCast(grdProgImage.SelectedRow.FindControl("lbProgLogo"), Label)
            'getImageSize(getFilePath(True))
        Catch ex As Exception
            Logger.LogError("UploadProgrammeLogos", "grdProgImage_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Protected Sub doUpload(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)

    End Sub

    Private Sub setImage()
    End Sub

    Private Function getFileName() As String
        'Return sanitizeFile(ddlChannelName.Text & "_" & ddlProgram.SelectedItem.Text & "_" & ddlProgram.SelectedValue) & ".png"
        Return ""
    End Function
    Private Function getFilePath(ByVal exactPath As Boolean) As String
        'Dim strSanitizedFilename As String = sanitizeFile(ddlChannelName.Text.ToString & "_" & ddlProgram.SelectedItem.Text.Replace("'", "").ToString & "_" & ddlProgram.SelectedValue.ToString)
        'If exactPath Then
        '    Return Server.MapPath("~/uploads/MovieImages/" & strSanitizedFilename & ".png")
        'Else
        '    Return "~/uploads/MovieImages/" & strSanitizedFilename & ".png"
        'End If
        Return ""
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

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid()

    End Sub
    Private Sub bindGrid()
        'chkExact
        If chkExact.Checked Then
            sqlDSProgImage.SelectCommand = "select top 500 a.*,b.FullName from mst_moviesdb a join mst_language b on a.movielangid=b.LanguageID where moviename='" & txtSearch.Text & "'"
        Else
            sqlDSProgImage.SelectCommand = "select top 500 a.*,b.FullName from mst_moviesdb a join mst_language b on a.movielangid=b.LanguageID where moviename like '%" & txtSearch.Text & "%'"
        End If

        grdProgImage.DataBind()
    End Sub
End Class
