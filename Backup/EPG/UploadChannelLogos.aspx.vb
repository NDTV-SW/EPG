Imports System
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadChannelLogos
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ddlChannelName.DataBind()
            setImage()
        End If

    End Sub

    Private Sub uploadFile()
        Try

            Dim filename As String = AjaxFileUpload11.FileName
            lbStatus.Visible = False
            If filename.ToLower.EndsWith(".jpg") Or filename.ToLower.EndsWith(".jpeg") Then

                Dim strUploadPath As String = "~/uploads/ChannelLogos/"
                If Not System.IO.Directory.Exists(Server.MapPath(strUploadPath)) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath(strUploadPath))
                End If
                strUploadPath = getFilePath(True)

                AjaxFileUpload11.SaveAs(strUploadPath)
                AjaxFileUpload11.Dispose()

                Dim abc As New clsFTP
                abc.doS3Task(strUploadPath, "/uploads/ChannelLogos")
                System.Threading.Thread.Sleep(1000)

                Dim MyConnection As New SqlConnection
                MyConnection = New SqlConnection(ConString)
                'Dim adp As New SqlDataAdapter("update mst_Channel set Channellogo='" & getFileName() & "'", MyConnection)
                'Dim dt As New DataTable
                'adp.Fill(dt)
                imgChannellogo.ImageUrl = getFilePath(False)
                hyChannelLogo.NavigateUrl = getFilePath(False)
                getImageSize(getFilePath(True))
                grdChannelLogo.DataBind()
            Else
                lbStatus.Visible = True
                lbStatus.Text = "File Uploaded but not saved. Please check Extension of file"
            End If
        Catch ex As Exception
            Logger.LogError("UploadChannelmeLogos", "uploadFile", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    'Protected Sub grdProgImage_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgImage.RowDataBound
    '    Try
    '        Select Case e.Row.RowType
    '            Case DataControlRowType.DataRow
    '                Dim imglogo As Image = DirectCast(e.Row.FindControl("imglogo"), Image)
    '                Dim hylogo As HyperLink = DirectCast(e.Row.FindControl("hylogo"), HyperLink)
    '                Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)
    '                imglogo.ImageUrl = "~/uploads/ChannelLogos/" & lbChannelId.Text.Replace(" ", "_") & ".jpg"
    '                hylogo.NavigateUrl = "~/uploads/ChannelLogos/" & lbChannelId.Text.Replace(" ", "_") & ".jpg"

    '        End Select
    '    Catch ex As Exception
    '        Logger.LogError("UploadChannelmeLogos", "grdProgImage_RowDataBound", ex.Message.ToString, User.Identity.Name)
    '    End Try

    'End Sub

    'Protected Sub grdProgImage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grdProgImage.SelectedIndexChanged
    '    Try
    '        Dim lbProgId As Label = DirectCast(grdProgImage.SelectedRow.FindControl("lbProgId"), Label)
    '        Dim lbProgLogo As Label = DirectCast(grdProgImage.SelectedRow.FindControl("lbProgLogo"), Label)
    '        ddlChannelName.SelectedValue = lbProgId.Text
    '        imgChannellogo.ImageUrl = getFilePath(False)
    '        hyChannelLogo.NavigateUrl = getFilePath(False)
    '        getImageSize(getFilePath(True))
    '    Catch ex As Exception
    '        Logger.LogError("UploadChannelmeLogos", "grdProgImage_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
    '    End Try

    'End Sub

    Protected Sub doUpload(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        uploadFile()
    End Sub

    Private Sub setImage()
        imgChannellogo.ImageUrl = getFilePath(False)
        getImageSize(getFilePath(True))
    End Sub

    Private Function getFileName() As String
        Return sanitizeFile(ddlChannelName.SelectedValue.Replace(" ", "_")) & ".jpg"
    End Function
    Private Function getFilePath(ByVal exactPath As Boolean) As String
        Dim strSanitizedFilename As String = sanitizeFile(ddlChannelName.SelectedValue.Replace(" ", "_"))
        If exactPath Then
            Return Server.MapPath("~/uploads/ChannelLogos/" & strSanitizedFilename & ".jpg")
        Else
            Return "~/uploads/ChannelLogos/" & strSanitizedFilename & ".jpg"
        End If
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
        ddlChannelName.DataBind()
        lbImageSize.Text = ""
        hyChannelLogo.NavigateUrl = ""
        imgChannellogo.ImageUrl = ""
    End Sub

    Protected Sub ddlChannelName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlChannelName.SelectedIndexChanged
        setImage()
    End Sub

    Private Sub getImageSize(ByVal strImagePath As String)
        lbImageSize.Text = getImage.getImageSize(strImagePath)
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        uploadFile()
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove.Click
        clearall()
    End Sub

    Protected Sub grdChannelLogo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles grdChannelLogo.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imglogo As Image = DirectCast(e.Item.FindControl("imglogo"), Image)
            Dim hylogo As HyperLink = DirectCast(e.Item.FindControl("hylogo"), HyperLink)
            Dim lbChannelId As Label = DirectCast(e.Item.FindControl("lbChannelId"), Label)
            imglogo.ImageUrl = "~/uploads/ChannelLogos/" & lbChannelId.Text.Replace(" ", "_") & ".jpg"
            hylogo.NavigateUrl = "~/uploads/ChannelLogos/" & lbChannelId.Text.Replace(" ", "_") & ".jpg"
        End If

    End Sub
End Class
