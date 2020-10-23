Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ProgramImagesUploaded
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                txtFromDate.Text = DateTime.Now.AddDays(0).Date.ToString
                txtToDate.Text = DateTime.Now.AddDays(0).Date.ToString
            End If
        Catch ex As Exception
            Logger.LogError("ProgramImagesUploaded", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim obj As New clsExecute
        Dim dt As DataTable
        If chkUserWiseCount.Checked = False And chkUserChannelWiseCount.Checked = False Then
            dt = obj.executeSQL("SELECT ChannelId,ProgName 'Programme',UploadDate,UploadTime,LastUpdatedBy FROM fn_rep_programimagesuploaded('" & txtFromDate.Text & "','" & txtToDate.Text & "')", False)
        ElseIf chkUserChannelWiseCount.Checked = True Then
            dt = obj.executeSQL("SELECT count(*) Count,ChannelId,uploaddate 'Date',lastupdatedby 'Uploaded by'  FROM fn_rep_programimagesuploaded('" & txtFromDate.Text & "','" & txtToDate.Text & "') GROUP BY lastupdatedby,channelid,uploaddate ORDER BY 4,2,uploaddate", False)
        Else
            dt = obj.executeSQL("SELECT count(*) Count,uploaddate 'Date',lastupdatedby 'Uploaded by' FROM fn_rep_programimagesuploaded('" & txtFromDate.Text & "','" & txtToDate.Text & "') GROUP BY lastupdatedby,uploaddate ORDER BY 3,2", False)
        End If
        grdImagesReport.DataSource = dt
        grdImagesReport.DataBind()
    End Sub
End Class