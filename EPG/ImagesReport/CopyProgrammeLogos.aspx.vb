Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CopyProgrammeLogos
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                'bindSourceProgramme()
                'bindGrid()
            End If
        Catch ex As Exception
            Logger.LogError("UploadProgrammeLogos", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Dim obj As New clsExecute
        For Each row As GridViewRow In grd.Rows
            Dim chkCopy As CheckBox = TryCast(row.FindControl("chkCopy"), CheckBox)
            Dim dProgid As Integer = TryCast(row.FindControl("lbProgid"), Label).Text
            If chkCopy.Checked = True Then
                Dim sProgid As Integer
                sProgid = ddlSource.SelectedItem.Text.Split("~")(2)
                obj.executeSQL("update mst_program set programlogo='" & ddlSource.SelectedValue & "' where progid='" & dProgid & "'", False)
                obj.executeSQL("insert into aud_mst_program_proglogo(progid,action,lastupdatedat,lastupdatedby) values('" & dProgid & "','I',dbo.GetLocalDate(),'copy-" & User.Identity.Name & "')", False)
            End If
        Next
        bindMeGrid()
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindSourceProgramme()
        bindMeGrid()
    End Sub

    Protected Sub bindSourceProgramme()
        Dim obj As New clsExecute
        Dim sql As String
        sql = "select programlogo as logo, progname  + '~' + channelid + '~' + convert(varchar,progid) as channelprogram,programlogo from mst_program where "
        If chkExact.Checked = False Then
            sql = sql & " (progname like '%" & txtSearch.Text & "%' or channelid like '%" & txtSearch.Text & "%') "
        Else
            sql = sql & " (progname ='" & txtSearch.Text & "' or channelid ='" & txtSearch.Text & "') "
        End If
        sql = sql & " and (programlogo is not null and programlogo<>'' and programlogo not like '%script%') and progid in (select distinct progid from mst_epg where progdate>='" & DateTime.Now.Date & "')  order by 2"

        Dim dt As DataTable = obj.executeSQL(sql, False)
        ddlSource.DataTextField = "channelprogram"
        ddlSource.DataValueField = "logo"
        ddlSource.DataSource = dt
        ddlSource.DataBind()
    End Sub

    Protected Sub bindMeGrid()
        Dim dt As DataTable = Me.bindGrid()
        grd.DataSource = dt
        grd.DataBind()
    End Sub
    Protected Function bindGrid() As DataTable
        Dim obj As New clsExecute
        Dim sql As String
        sql = "select progid,channelid, progname,programlogo from mst_program where "
        If chkExact.Checked = False Then
            sql = sql & " (progname like '%" & txtSearch.Text & "%' or channelid like '%" & txtSearch.Text & "%') "
        Else
            sql = sql & " (progname ='" & txtSearch.Text & "' or channelid ='" & txtSearch.Text & "') "
        End If
        sql = sql & " and (programlogo is null or programlogo='' or programlogo like '%script%') and progid in (select distinct progid from mst_epg where progdate>='" & DateTime.Now.Date & "')  order by 2 "

        Dim dt As DataTable = obj.executeSQL(sql, False)
        Return dt

    End Function
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
        Dim table As DataTable = Me.bindGrid()
        table.DefaultView.Sort = sortExpression & direction
        grd.DataSource = table
        grd.DataBind()
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

    Protected Sub grd_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grd.PageIndexChanging
        grd.PageIndex = e.NewPageIndex
        bindMeGrid()
    End Sub
End Class