Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSyouTubeChannelMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("YoutubeChannelMaster", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Try

            lbYouTubeChannelId.Text = DirectCast(grd.SelectedRow.FindControl("lbYouTubeChannelId"), Label).Text
            ddlChannel.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbChannelId"), Label).Text.Trim
            txtChannelName.Text = DirectCast(grd.SelectedRow.FindControl("lbChannelName"), Label).Text
            txtLaunchDate.Text = DirectCast(grd.SelectedRow.FindControl("lbLaunchDate"), Label).Text
            ddlGenre.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbGenreId"), Label).Text
            chkAvailable.Checked = DirectCast(grd.SelectedRow.FindControl("chkAvailable"), CheckBox).Checked
            ddlLanguage.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbLanguageId"), Label).Text
            txtVideoUrl.Text = DirectCast(grd.SelectedRow.FindControl("lbYoutubeURL"), Label).Text


            'ddlGenre.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbGenre"), Label).Text
            'ddlLanguageName.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbLanguageid"), Label).Text
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("YoutubeChannelMaster", "grd_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        grd.SelectedIndex = -1
        bindGrid(False)
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strParams As String, strValues As String, strType As String
        If btnUpdate.Text = "ADD" Then
            strParams = "channelId~channelName~LaunchDate~GenreId~Available~languageId~action~actionuser~youtubeurl"
            strType = "VarChar~VarChar~DateTime~Int~Bit~Int~char~varchar~VarChar"
            strValues = ddlChannel.SelectedValue & "~" & txtChannelName.Text & "~" & txtLaunchDate.Text & "~" & ddlGenre.SelectedValue & "~" & chkAvailable.Checked & "~" & ddlLanguage.SelectedValue & "~A~" & User.Identity.Name & "~" & txtVideoUrl.Text
        Else
            strParams = "youtubeChannelId~channelId~channelName~LaunchDate~GenreId~Available~languageId~action~actionuser~youtubeurl"
            strType = "Int~VarChar~VarChar~DateTime~Int~Bit~Int~char~VarChar~VarChar"
            strValues = lbYouTubeChannelId.Text & "~" & ddlChannel.SelectedValue & "~" & txtChannelName.Text & "~" & txtLaunchDate.Text & "~" & ddlGenre.SelectedValue & "~" & chkAvailable.Checked & "~" & ddlLanguage.SelectedValue & "~U~" & User.Identity.Name & "~" & txtVideoUrl.Text

        End If
        Dim obj As New clsExecute
        obj.executeSQL("sp_mst_youtubechannels", strParams, strType, strValues, True, False)
        bindGrid(True)

    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean)
        sqlDSTvStar.SelectCommand = "select a.channelid,b.FullName,c.GenreName,CONVERT(varchar,launchDate,106) launchdate1,CONVERT(varchar,launchDate,110) launchdate2,* from mst_youtubechannels a join mst_language b on a.languageid=b.LanguageID join mst_genre c on a.genreid=c.GenreId and channelName like '%" & txtSearch.Text.Trim & "%'"
        sqlDSTvStar.SelectCommandType = SqlDataSourceCommandType.Text
        grd.DataSourceID = "sqlDSTvStar"
        If paging = False Then
            grd.PageIndex = 0
        End If
        grd.SelectedIndex = -1
        grd.DataBind()
        clearAll()
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Try
            Dim youtubeChannelId As Label = DirectCast(grd.Rows(e.RowIndex).FindControl("lbyoutubeChannelId"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("delete from mst_youtubechannels where youtubeChannelId='" & youtubeChannelId.Text & "'", False)

            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("YoutubeChannelMaster", "grd_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grd_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub clearAll()
        lbYouTubeChannelId.Text = String.Empty
        txtChannelName.Text = String.Empty
        txtLaunchDate.Text = String.Empty
        txtVideoUrl.Text = String.Empty
        chkAvailable.Checked = False
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(False)
    End Sub

    Dim intSno As Integer
    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno.ToString
            intSno = intSno + 1
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    Protected Sub grd_PageIndexChanged(sender As Object, e As EventArgs) Handles grd.PageIndexChanged
        bindGrid(True)
    End Sub

End Class