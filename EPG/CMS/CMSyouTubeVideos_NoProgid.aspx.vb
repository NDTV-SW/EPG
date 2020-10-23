Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSyouTubeVideos_NoProgid
    Inherits System.Web.UI.Page
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & messagestr & "');", True)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                txtStartDate.Text = DateTime.Now.AddDays(-10).ToString("MM/dd/yyyy")
                txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                bindGrid(False, True)
            End If
        Catch ex As Exception
            Logger.LogError("YoutubeVideos", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Try
            Dim obj As New clsExecute
            lbRowId.Text = DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text

            Dim dt As DataTable = obj.executeSQL("select * from youtubevideos where rowid='" & lbRowId.Text & "'", False)

            If dt.Rows.Count = 1 Then
                txtEpisode.Text = dt.Rows(0)("episodeid").ToString
                txtSeason.Text = dt.Rows(0)("season").ToString
                txtVideoUrl.Text = dt.Rows(0)("videourl").ToString

                txtKeywords.Text = dt.Rows(0)("keywords").ToString
                txtLikes.Text = dt.Rows(0)("likes").ToString
                txtVideoTitle.Text = dt.Rows(0)("videotitle").ToString
                txtSynopsis.Text = dt.Rows(0)("synopsis").ToString
                txtVideoTitleHindi.Text = dt.Rows(0)("videotitle_hin").ToString
                txtSynopsisHindi.Text = dt.Rows(0)("synopsis_hin").ToString

                Try

                    ddlGenre.SelectedValue = dt.Rows(0)("genreid").ToString
                    ddlSubGenre.SelectedValue = dt.Rows(0)("subgenreid").ToString
                Catch
                End Try
                chkVerified.Checked = dt.Rows(0)("verified")

                btnUpdate.Text = "UPDATE"
            End If


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
            strParams = "videoURL~videoTitle~synopsis~videoTitle_hin~synopsis_hin~genreid~subgenreid~season~keywords~likes~episodeId~action~modifiedBy~verified"
            strType = "VarChar~VarChar~VarChar~nVarChar~nVarChar~Int~Int~Int~varchar~Int~Int~char~varchar~bit"
            strValues = txtVideoUrl.Text & "~" & txtVideoTitle.Text & "~" & txtSynopsis.Text & "~" & txtVideoTitleHindi.Text & "~" & txtSynopsisHindi.Text & "~" & ddlGenre.SelectedValue & "~" & ddlSubGenre.SelectedValue & "~" & txtSeason.Text & "~" & txtKeywords.Text & "~" & txtLikes.Text & "~" & txtEpisode.Text & "~A~" & User.Identity.Name & "~" & chkVerified.Checked
        Else
            strParams = "rowid~videoURL~videoTitle~synopsis~videoTitle_hin~synopsis_hin~genreid~subgenreid~season~keywords~likes~episodeId~action~modifiedBy~verified"
            strType = "Int~VarChar~VarChar~VarChar~nVarChar~nVarChar~Int~Int~Int~varchar~Int~Int~char~varchar~bit"
            strValues = lbRowId.Text & "~" & txtVideoUrl.Text & "~" & txtVideoTitle.Text & "~" & txtSynopsis.Text & "~" & txtVideoTitleHindi.Text & "~" & txtSynopsisHindi.Text & "~" & ddlGenre.SelectedValue & "~" & ddlSubGenre.SelectedValue & "~" & txtSeason.Text & "~" & txtKeywords.Text & "~" & txtLikes.Text & "~" & txtEpisode.Text & "~U~" & User.Identity.Name & "~" & chkVerified.Checked

        End If
        Dim obj As New clsExecute
        obj.executeSQL("sp_youtubeVideos_hin", strParams, strType, strValues, True, False)
        bindGrid(False, True)
    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean, boolFromSearch As Boolean)

        If boolFromSearch Then
            If ddlChannelSearch.SelectedIndex = 0 Then
                sqlDSYouTubeVideos.SelectCommand = "select a.*,REPLACE(a.videourl,'https://www.youtube.com/watch?v=','') VideoURL1,convert(varchar,a.publishdate,106) publishdate" & _
                    " from youtubevideos a where a.videotitle like '%" & txtSearch.Text.Trim & "%' and convert(varchar,publishdate,112) between " & _
                    "'" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & "' and '" & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & "' and verified='" & chkVerifiedSearch.Checked & "'"
            Else
                sqlDSYouTubeVideos.SelectCommand = "select a.*,REPLACE(a.videourl,'https://www.youtube.com/watch?v=','') VideoURL1,convert(varchar,a.publishdate,106) publishdate" & _
                    " from youtubevideos a where a.videotitle like '%" & txtSearch.Text.Trim & "%' and convert(varchar,publishdate,112) between " & _
                    "'" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & "' and '" & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & "' and verified='" & chkVerifiedSearch.Checked & "' and " & _
                    " a.youtubechannelid='" & ddlChannelSearch.SelectedValue & "'"
            End If

        Else
            sqlDSYouTubeVideos.SelectCommand = "select  a.* from youtubevideos a where a.videotitle like '%" & txtSearch.Text.Trim & "%' and convert(varchar,a.publishdate,112) between convert(varchar,dbo.getlocaldate()-30,112) and convert(varchar,dbo.getlocaldate(),112)"
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
        txtVideoUrl.Text = String.Empty

        txtVideoTitle.Text = String.Empty
        txtSynopsis.Text = String.Empty
        txtVideoTitleHindi.Text = String.Empty
        txtSynopsisHindi.Text = String.Empty
        ddlGenre.SelectedIndex = 0
        ddlSubGenre.SelectedIndex = 0

        txtEpisode.Text = String.Empty
        txtSeason.Text = String.Empty
        txtKeywords.Text = String.Empty
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
            Dim lbVideoUrl1 As Label = TryCast(e.Row.FindControl("lbVideoUrl"), Label)
            hyView.NavigateUrl = lbVideoUrl1.Text

        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    Protected Sub grd_PageIndexChanged(sender As Object, e As EventArgs) Handles grd.PageIndexChanged
        bindGrid(True, True)
    End Sub

End Class