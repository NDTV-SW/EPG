Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class SonyLiv
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                txtStartDate.Text = DateTime.Now.AddDays(-10).ToString("MM/dd/yyyy")
                txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                bindGrid(False, False)
            End If
        Catch ex As Exception
            Logger.LogError("SonyLiv", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Try

            lbRowId.Text = DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text
            Try
                ddlChannel.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbChannelId"), Label).Text
                ddlProgramme.DataBind()
                ddlProgramme.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbProgid"), Label).Text
            Catch ex As Exception
                ddlChannel.SelectedIndex = 0
                ddlProgramme.SelectedIndex = 0
            End Try
            lbRowId.Text = DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text
            txtVideoTitle.Text = DirectCast(grd.SelectedRow.FindControl("lbVideoTitle"), Label).Text
            txtEpisode.Text = DirectCast(grd.SelectedRow.FindControl("lbEpisodeId"), Label).Text
            txtVideoUrl.Text = DirectCast(grd.SelectedRow.FindControl("lbVideoUrl"), Label).Text
            txtSynopsis.Text = DirectCast(grd.SelectedRow.FindControl("lbSynopsis"), Label).Text
            txtShowDate.Text = DirectCast(grd.SelectedRow.FindControl("lbShowAirdate"), Label).Text

            txtPublishDate.Text = DirectCast(grd.SelectedRow.FindControl("lbPublishDate"), Label).Text
            chkVerified.Checked = DirectCast(grd.SelectedRow.FindControl("chkVerified"), CheckBox).Checked
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("SonyLiv", "grd_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strParams As String, strValues As String, strType As String
        If btnUpdate.Text = "ADD" Then
            strParams = "progId~videoURL~videoTitle~synopsis~episodeId~publishDate~showonairdate~verified~action~modifiedBy"
            strType = "Int~VarChar~VarChar~VarChar~Int~DateTime~DateTime~Bit~char~varchar"
            strValues = ddlProgramme.SelectedValue & "~" & txtVideoUrl.Text & "~" & txtVideoTitle.Text & "~" & txtSynopsis.Text & "~" & txtEpisode.Text & "~" & txtPublishDate.Text & "~" & txtShowDate.Text & "~" & chkVerified.Checked & "~A~" & User.Identity.Name
        Else
            strParams = "rowid~progId~videoURL~videoTitle~synopsis~episodeId~publishDate~showonairdate~verified~action~modifiedBy"
            strType = "Int~Int~VarChar~VarChar~VarChar~Int~DateTime~DateTime~Bit~char~varchar"
            strValues = lbRowId.Text & "~" & ddlProgramme.SelectedValue & "~" & txtVideoUrl.Text & "~" & txtVideoTitle.Text & "~" & txtSynopsis.Text & "~" & txtEpisode.Text & "~" & txtPublishDate.Text & "~" & txtShowDate.Text & "~" & chkVerified.Checked & "~U~" & User.Identity.Name

        End If
        Dim obj As New clsExecute
        obj.executeSQL("sp_SonyLiv", strParams, strType, strValues, True, False)
        bindGrid(True, True)

    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean, boolFromSearch As Boolean)
        grd.DataSource = bindGridView()
        If Not paging Then
            grd.SelectedIndex = -1
        End If

        grd.DataBind()
        clearAll()
    End Sub
    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortExpression As String = e.SortExpression
        Dim direction As String = String.Empty
        If SortDirection = SortDirection.Ascending Then
            SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If
        Dim table As DataTable = Me.bindGridView()
        table.DefaultView.Sort = sortExpression & direction
        grd.DataSource = table
        grd.DataBind()
    End Sub

    Private Function bindGridView() As DataTable
        Dim strSql As String = "select b.channelid,b.progname,a.* from mst_sonyliv a left outer join mst_program b on a.progid=b.progid "
        strSql = strSql & " where convert(varchar,publishdate,112) between '" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & "' and '" & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & "' "
        strSql = strSql & " and videotitle like '%" & txtSearch.Text.Trim & "%' "
        If ddlChannelSearch.SelectedIndex > 1 Then
            strSql = strSql & " and channelid='" & ddlChannelSearch.SelectedValue & "' "
        End If
        If chkVerifiedSearch.Checked Then
            strSql = strSql & " and verified=1 "
        Else
            strSql = strSql & " and verified=0 "
        End If

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL(strSql, False)
        Return dt
    End Function

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grd.Rows(e.RowIndex).FindControl("lbrowid"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("delete from mst_sonyliv where rowid='" & lbRowId.Text & "'", False)

            bindGrid(True, True)
        Catch ex As Exception
            Logger.LogError("SonyLiv", "grd_RowDeleting", ex.Message.ToString, User.Identity.Name)
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
        txtShowDate.Text = String.Empty
        txtEpisode.Text = String.Empty
        txtSynopsis.Text = String.Empty
        grd.SelectedIndex = -1
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        bindGrid(False, True)
    End Sub
   

    
    Protected Sub grd_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grd.PageIndexChanging
        grd.PageIndex = e.NewPageIndex
        bindGrid(True, True)
    End Sub
End Class