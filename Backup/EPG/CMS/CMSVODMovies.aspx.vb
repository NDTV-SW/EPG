Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSVODMovies
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("VOD Movies", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Try

            lbRowId.Text = DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text
            ddlMovie.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbMovieId"), Label).Text
            txtVideoUrl.Text = DirectCast(grd.SelectedRow.FindControl("lbVideoUrl"), Label).Text
            txtKeywords.Text = DirectCast(grd.SelectedRow.FindControl("lbKeywords"), Label).Text
            txtPrice.Text = DirectCast(grd.SelectedRow.FindControl("lbPrice"), Label).Text
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("VOD Movies", "grd_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim strParams As String, strValues As String, strType As String
        If btnUpdate.Text = "ADD" Then
            If txtPrice.Text = "" Then
                strParams = "platformId~movieid~VODURL~keywords~action~actionuser"
                strType = "Int~int~varchar~varchar~char~varchar"
                strValues = ddlPlatform.SelectedValue & "~" & ddlMovie.SelectedValue & "~" & txtVideoUrl.Text & "~" & txtKeywords.Text & "~A~" & User.Identity.Name
            Else
                strParams = "platformId~movieid~VODURL~price~keywords~action~actionuser"
                strType = "Int~int~varchar~int~varchar~char~varchar"
                strValues = ddlPlatform.SelectedValue & "~" & ddlMovie.SelectedValue & "~" & txtVideoUrl.Text & "~" & txtPrice.Text & "~" & txtKeywords.Text & "~A~" & User.Identity.Name
            End If
        Else
            If txtPrice.Text = "" Then
                strParams = "rowid~platformId~movieid~VODURL~keywords~action~actionuser"
                strType = "Int~Int~int~varchar~varchar~char~varchar"
                strValues = lbRowId.Text & "~" & ddlPlatform.SelectedValue & "~" & ddlMovie.SelectedValue & "~" & txtVideoUrl.Text & "~" & txtKeywords.Text & "~U~" & User.Identity.Name
            Else
                strParams = "rowid~platformId~movieid~VODURL~price~keywords~action~actionuser"
                strType = "Int~Int~int~varchar~int~varchar~char~varchar"
                strValues = lbRowId.Text & "~" & ddlPlatform.SelectedValue & "~" & ddlMovie.SelectedValue & "~" & txtVideoUrl.Text & "~" & txtPrice.Text & "~" & txtKeywords.Text & "~U~" & User.Identity.Name

            End If
        End If
        Dim obj As New clsExecute
        obj.executeSQL("sp_mst_VOD_Movies", strParams, strType, strValues, True, False)
        bindGrid(True)

    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grd.Rows(e.RowIndex).FindControl("lbRowId"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("delete from mst_vod_movies where rowid='" & lbRowId.Text & "'", False)

            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("PlatformMaster", "grd_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grd_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean)
        sqlDS.SelectCommand = "select c.platformname,b.moviename,a.rowid,a.price,a.movieid,a.keywords,a.PlatformId,a.VODURL VideoURL,a.modifiedby,a.modifiedat from mst_vod_movies a join mst_moviesdb b on a.movieid=b.rowid join mst_VODPlatforms c on a.platformid=c.platformid  and b.moviename like '%" & txtSearch.Text.Trim & "%' order by 1,2"
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text
        grd.DataSourceID = "sqlDS"
        If paging = False Then
            grd.PageIndex = 0
        End If
        grd.SelectedIndex = -1
        grd.DataBind()

        clearAll()
    End Sub

    Private Sub clearAll()
        lbRowId.Text = String.Empty
        txtVideoUrl.Text = String.Empty
        txtKeywords.Text = String.Empty
        txtPrice.Text = String.Empty
        grd.SelectedIndex = -1
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
            lbSno.Text = intSno
            intSno = intSno + 1
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    Protected Sub grd_PageIndexChanged(sender As Object, e As EventArgs) Handles grd.PageIndexChanged
        bindGrid(True)
    End Sub

End Class