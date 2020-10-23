Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class UploadSchedule4WOITest
    Inherits System.Web.UI.Page

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub
    Private Function bindUploaded() As DataTable
        Dim dt As DataTable = Nothing
        Dim obj As New clsExecute
        dt = obj.executeSQL("select channelid Channel,uploadedfilename FileName,CONVERT(varchar,uploadedAt,108) 'Upload At',CONVERT(varchar,startdate,106) 'Start Date',CONVERT(varchar,enddate,106) 'End Date',epgbuilt Built,xmlgenerated 'XML Generated',sameEPG from aud_mst_channel_woi_upload where CONVERT(varchar, uploadedAt,112)=CONVERT(varchar, dbo.getlocaldate(),112) and  CONVERT(varchar, uploadedAt,108)>CONVERT(varchar, dateadd(hh,-" & ddlHour.SelectedValue & ", dbo.getlocaldate()),108) order by uploadedAt desc", False)
        Return dt
    End Function

    Private Sub bindUploadedGrid()
        Dim dt As DataTable = Me.bindUploaded
        grdUploaded.DataSource = dt
        grdUploaded.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindUploadedGrid()
            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOITEST", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        bindAll()
        lbChannel.Text = txtChannel.Text
    End Sub

    Private Sub bindgrdExcelData()
        'DataSourceID="SqlDSgrdData" 
        Dim obj As New clsExecute
        'Dim dt As DataTable = obj.executeSQL("select [Program Name], Genre, convert(varchar,[Date],106), convert(varchar,[Time],108), Duration, [Description],[Episode No] from testtabletemp where channelID=@ChannelId order by Date,time", "ChannelId", "varchar", txtChannel.Text, False, False)
        Dim dt As DataTable = obj.executeSQL("select [Program Name], Genre, convert(varchar,[Date],106), convert(varchar,[Time],108), Duration, [Description],[Episode No] from map_epgexcel where channelID=@ChannelId order by Date,time", "ChannelId", "varchar", txtChannel.Text, False, False)
        grdExcelData.DataSource = dt

        grdExcelData.DataBind()
        'DataSourceID = "SqlDSgrdGenre"
    End Sub


    Private Sub bindAll()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select movie_channel,movielangid from mst_channel where channelid='" & txtChannel.Text & "'", False)
        bindgrdExcelData()
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> _
    Public Shared Function SearchMovie(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getMovie(contextKey, prefixText, count)
        Return channels
    End Function

    Protected Sub grdExcelData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdExcelData.PageIndexChanging
        grdExcelData.PageIndex = e.NewPageIndex
        bindgrdExcelData() 'grdExcelData.DataBind()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try

            Dim strExtension As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower.Replace(".", "")
            Dim strPath As String = ConfigurationManager.AppSettings("woi" & strExtension & "uploadpath").ToString()
            FileUpload1.SaveAs(strPath & FileUpload1.FileName)
            myMessageBox(FileUpload1.FileName & " uploaded")
        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOITEST", "btnUpload_Click", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Protected Sub ddlHour_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHour.SelectedIndexChanged
        bindUploadedGrid()
    End Sub

    Protected Sub grdUploaded_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUploaded.PageIndexChanging
        grdUploaded.PageIndex = e.NewPageIndex
        bindUploadedGrid()
    End Sub


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
        Dim table As DataTable = Me.bindUploaded()
        table.DefaultView.Sort = sortExpression & direction
        grdUploaded.DataSource = table
        grdUploaded.DataBind()
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

End Class
