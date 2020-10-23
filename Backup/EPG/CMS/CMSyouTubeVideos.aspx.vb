Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSyouTubeVideos
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                txtStartDate.Text = DateTime.Now.AddDays(-10).ToString("MM/dd/yyyy")
                txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                bindGrid(False, False)
            End If
        Catch ex As Exception
            Logger.LogError("YoutubeVideos", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Try

            lbRowId.Text = DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text
            ddlYouTubeChannel.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbYouTubeChannelId"), Label).Text
            ddlProgramme.DataBind()
            Try
                ddlProgramme.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbProgid"), Label).Text
            Catch ex As Exception
                ddlProgramme.SelectedIndex = 0
            End Try
            txtEpisode.Text = DirectCast(grd.SelectedRow.FindControl("lbEpisodeId"), Label).Text
            txtVideoUrl.Text = DirectCast(grd.SelectedRow.FindControl("lbVideoUrl1"), Label).Text
            txtSeason.Text = DirectCast(grd.SelectedRow.FindControl("lbSeason"), Label).Text
            txtKeywords.Text = DirectCast(grd.SelectedRow.FindControl("lbKeywords"), Label).Text
            txtSynopsis.Text = DirectCast(grd.SelectedRow.FindControl("lbSynopsis"), Label).Text
            txtLikes.Text = DirectCast(grd.SelectedRow.FindControl("lbLikes"), Label).Text
            txtVideoTitle.Text = DirectCast(grd.SelectedRow.FindControl("lbVideoTitle"), Label).Text
            lbRowId.Text = DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text
            txtPublishDate.Text = DirectCast(grd.SelectedRow.FindControl("lbPublishDate"), Label).Text

            chkVerified.Checked = DirectCast(grd.SelectedRow.FindControl("chkVerified"), CheckBox).Checked

            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("YoutubeVideos", "grd_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtSeason.Text.Trim = "" Then
            txtSeason.Text = "0"
        End If
        If txtLikes.Text.Trim = "" Then
            txtLikes.Text = "0"
        End If
       
        Dim strParams As String, strValues As String, strType As String
        If btnUpdate.Text = "ADD" Then
            strParams = "youtubeChannelId~videoURL~videoTitle~publishDate~progId~season~keywords~synopsis~likes~episodeId~action~modifiedBy~verified"
            strType = "Int~VarChar~VarChar~DateTime~Int~Int~varchar~varchar~Int~Int~char~varchar~bit"
            strValues = ddlYouTubeChannel.SelectedValue & "~" & txtVideoUrl.Text & "~" & txtVideoTitle.Text & "~" & txtPublishDate.Text & "~" & ddlProgramme.SelectedValue & "~" & txtSeason.Text & "~" & txtKeywords.Text & "~" & txtSynopsis.Text & "~" & txtLikes.Text & "~" & txtEpisode.Text & "~A~" & User.Identity.Name & "~" & chkVerified.Checked
        Else
            strParams = "rowid~youtubeChannelId~videoURL~videoTitle~publishDate~progId~season~keywords~synopsis~likes~episodeId~action~modifiedBy~verified"
            strType = "Int~Int~VarChar~VarChar~DateTime~Int~Int~varchar~varchar~Int~Int~char~varchar~bit"
            strValues = lbRowId.Text & "~" & ddlYouTubeChannel.SelectedValue & "~" & txtVideoUrl.Text & "~" & txtVideoTitle.Text & "~" & txtPublishDate.Text & "~" & ddlProgramme.SelectedValue & "~" & txtSeason.Text & "~" & txtKeywords.Text & "~" & txtSynopsis.Text & "~" & txtLikes.Text & "~" & txtEpisode.Text & "~U~" & User.Identity.Name & "~" & chkVerified.Checked

        End If
        Dim obj As New clsExecute
        obj.executeSQL("sp_youtubeVideos", strParams, strType, strValues, True, False)
        bindGrid(True, False)

    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean, boolFromSearch As Boolean)

        If boolFromSearch Then
            If ddlChannelSearch.SelectedIndex = 0 Then
                sqlDSYouTubeVideos.SelectCommand = "select b.ProgName,a.rowid,a.duration, b.channelid,a.progid,a.season,a.keywords,a.synopsis,a.likes,a.YouTubeChannelId,a.VideoTitle ," & _
                    "REPLACE(a.videourl,'https://www.youtube.com/watch?v=','') VideoURL,a.videourl VideoURL1,a.verified, a.episodeId ,convert(varchar,a.publishdate,106) publishdate,a.modifiedby,a.modifiedat" & _
                    " from youtubevideos a left outer join mst_program b on a.progid=b.progid where a.videotitle like '%" & txtSearch.Text.Trim & "%' and " & _
                    "convert(varchar,publishdate,112) between '" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & "' and '" & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & "' " & _
                    " and verified='" & chkVerifiedSearch.Checked & "'"
            Else
                sqlDSYouTubeVideos.SelectCommand = "select b.ProgName,a.rowid,a.duration,b.channelid,a.progid,a.season,a.keywords,a.synopsis,a.likes,a.YouTubeChannelId,a.VideoTitle ," & _
                    "REPLACE(a.videourl,'https://www.youtube.com/watch?v=','') VideoURL,a.videourl VideoURL1,a.verified, a.episodeId ,convert(varchar,a.publishdate,106) publishdate,a.modifiedby,a.modifiedat" & _
                    " from youtubevideos a left outer join mst_program b on a.progid=b.progid where a.videotitle like '%" & txtSearch.Text.Trim & "%' and " & _
                    " youtubechannelid='" & ddlChannelSearch.SelectedValue & "' and convert(varchar,publishdate,112) between '" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & "' and '" & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & "' " & _
                  " and verified='" & chkVerifiedSearch.Checked & "'"
            End If

        Else
            sqlDSYouTubeVideos.SelectCommand = "select b.ProgName,a.rowid,a.duration,b.channelid,a.progid,a.season,a.keywords,a.synopsis,a.likes,a.YouTubeChannelId,a.VideoTitle ,REPLACE(a.videourl,'https://www.youtube.com/watch?v=','') VideoURL,a.videourl VideoURL1,a.verified, a.episodeId ,convert(varchar,a.publishdate,106) publishdate,a.modifiedby,a.modifiedat from youtubevideos a left outer join mst_program b on a.progid=b.progid where a.videotitle like '%" & txtSearch.Text.Trim & "%'"
        End If




        sqlDSYouTubeVideos.SelectCommandType = SqlDataSourceCommandType.Text
        If paging = False Then
            grd.PageIndex = 0
        End If
        grd.SelectedIndex = -1
        grd.DataBind()

        clearAll()
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grd.Rows(e.RowIndex).FindControl("lbrowid"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("delete from youtubevideos where rowid='" & lbRowId.Text & "'", False)

            bindGrid(True, True)
        Catch ex As Exception
            Logger.LogError("YoutubeVideos", "grd_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grd_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub clearAll()
        lbRowId.Text = String.Empty
        txtPublishDate.Text = String.Empty
        txtVideoUrl.Text = String.Empty
        txtVideoTitle.Text = String.Empty
        txtPublishDate.Text = String.Empty
        txtEpisode.Text = String.Empty
        txtSeason.Text = String.Empty
        txtKeywords.Text = String.Empty
        txtSynopsis.Text = String.Empty
        txtLikes.Text = String.Empty
        grd.SelectedIndex = -1
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        bindGrid(False, True)
    End Sub
    Dim intSno As Integer
    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1
            Dim hyView As HyperLink = TryCast(e.Row.FindControl("hyView"), HyperLink)
            Dim lbVideoUrl1 As Label = TryCast(e.Row.FindControl("lbVideoUrl1"), Label)
            hyView.NavigateUrl = lbVideoUrl1.Text

        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    Protected Sub grd_PageIndexChanged(sender As Object, e As EventArgs) Handles grd.PageIndexChanged
        bindGrid(True, True)
    End Sub

End Class