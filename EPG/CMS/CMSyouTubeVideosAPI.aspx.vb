Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSyouTubeVideosAPI
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindGrid(False, False)
                txtStartDate.Text = DateTime.Now.AddDays(-3).Date
                txtEndDate.Text = DateTime.Now.Date
            End If
        Catch ex As Exception
            Logger.LogError("CMSyouTubeVideosAPI", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean, useSearch As Boolean)
        If useSearch Then
            If ddlChannelSearch.SelectedIndex = 0 Then
                sqlDSYouTubeVideos.SelectCommand = "select b.ChannelId,b.ProgName,b.ProgID,a.*, 'https://www.youtube.com/watch?v=' + ytvideoid as ytVideourl from youtubevideos_api a left outer join mst_program b on a.progid=b.ProgID where publishedat between '" & txtStartDate.Text & "' and '" & txtEndDate.Text & "' order by publishedat desc"
            Else
                sqlDSYouTubeVideos.SelectCommand = "select b.ChannelId,b.ProgName,b.ProgID,a.*, 'https://www.youtube.com/watch?v=' + ytvideoid as ytVideourl from youtubevideos_api a left outer join mst_program b on a.progid=b.ProgID where a.channelid='" & ddlChannelSearch.SelectedValue & "' and publishedat between '" & txtStartDate.Text & "' and '" & txtEndDate.Text & "' order by publishedat desc"
            End If
        Else
            sqlDSYouTubeVideos.SelectCommand = "select b.ChannelId,b.ProgName,b.ProgID,a.*, 'https://www.youtube.com/watch?v=' + ytvideoid as ytVideourl from youtubevideos_api a left outer join mst_program b on a.progid=b.ProgID order by publishedat desc"

        End If
        sqlDSYouTubeVideos.SelectCommandType = SqlDataSourceCommandType.Text
        grd.DataSourceID = "sqlDSYouTubeVideos"
        If paging = False Then
            grd.PageIndex = 0
        End If
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grd.Rows(e.RowIndex).FindControl("lbrowid"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("delete from youtubevideos_api where rowid='" & lbRowId.Text & "'", False)

            bindGrid(True, False)
        Catch ex As Exception
            Logger.LogError("YoutubeVideosAPI", "grd_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grd_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub

    

    Protected Sub grd_PageIndexChanged(sender As Object, e As EventArgs) Handles grd.PageIndexChanged
        bindGrid(True, False)
    End Sub

    Protected Sub grd_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd.RowCommand
        Dim lbRowId As Label = TryCast(grd.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowid"), Label)
        Dim chkVerify As CheckBox = TryCast(grd.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("chkVerify"), CheckBox)

        If e.CommandName.ToLower = "verify" Then
            If chkVerify.Checked = True Then
                Dim obj As New clsExecute
                obj.executeSQL("update youtubevideos_api set verified='" & chkVerify.Checked & "' where rowid='" & lbRowId.Text & "'", False)
                bindGrid(True, False)
            End If
        End If
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        lbRowId.Text = DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text
        txtVideoTitle.Text = DirectCast(grd.SelectedRow.FindControl("lbVideoName"), Label).Text
        Try
            ddlChannelId.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbChannelId"), Label).Text
            ddlProgram.DataBind()
            ddlProgram.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbProgId"), Label).Text
        Catch
            ddlChannelId.SelectedIndex = 0
            ddlProgram.DataBind()
        End Try

        txtDescription.Text = DirectCast(grd.SelectedRow.FindControl("lbDescription"), Label).Text
        chkVerified.Checked = DirectCast(grd.SelectedRow.FindControl("chkVerify"), CheckBox).Checked
        btnUpdate.Visible = True
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim obj As New clsExecute
        If ddlChannelId.SelectedIndex = 0 Then


            obj.executeSQL("update youtubevideos_api set " & _
                           " channelid=null, progid=null," & _
                           " ytvideoname='" & txtVideoTitle.Text.Replace("'", "''") & "', description='" & txtDescription.Text.Replace("'", "''") & "', verified='" & chkVerified.Checked & "' where rowid='" & lbRowId.Text & "'", False)
        Else
            obj.executeSQL("update youtubevideos_api set " & _
                           " channelid='" & ddlChannelId.SelectedValue & "', progid='" & ddlProgram.SelectedValue & "'," & _
                           " ytvideoname='" & txtVideoTitle.Text.Replace("'", "''") & "', description='" & txtDescription.Text.Replace("'", "''") & "', verified='" & chkVerified.Checked & "' where rowid='" & lbRowId.Text & "'", False)

        End If
        clearAll()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Private Sub clearAll()
        txtVideoTitle.Text = ""
        txtDescription.Text = ""
        chkVerified.Checked = False
        lbRowId.Text = ""
        bindGrid(True, False)
        btnUpdate.Visible = False
    End Sub

   
    Protected Sub ddlChannelId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelId.SelectedIndexChanged
        ddlProgram.DataBind()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        bindGrid(False, True)
    End Sub
End Class