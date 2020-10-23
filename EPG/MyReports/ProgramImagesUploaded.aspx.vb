Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ProgramImagesUploaded
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                txtFromDate.Text = DateTime.Now.AddDays(0).Date.ToString("MM/dd/yyyy")
                txtToDate.Text = DateTime.Now.AddDays(0).Date.ToString("MM/dd/yyyy")
            End If
        Catch ex As Exception
            Logger.LogError("ProgramImagesUploaded", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim strPortrait As String = ""
        If chkPortrait.Checked Then
            strPortrait = "_portrait"
        End If


        Dim obj As New clsExecute
        Dim dt As DataTable
        If chkUserWiseCount.Checked = False And chkUserChannelWiseCount.Checked = False Then
            If chkYupp.Checked Then
                dt = obj.executeSQL("SELECT ChannelId,ProgName 'Programme',UploadDate,UploadTime,LastUpdatedBy FROM fn_rep_programimagesuploaded" & strPortrait & "('" & txtFromDate.Text & "','" & txtToDate.Text & "') where isyupp=1", False)
            Else
                dt = obj.executeSQL("SELECT ChannelId,ProgName 'Programme',UploadDate,UploadTime,LastUpdatedBy FROM fn_rep_programimagesuploaded" & strPortrait & "('" & txtFromDate.Text & "','" & txtToDate.Text & "')", False)
            End If
        ElseIf chkUserChannelWiseCount.Checked = True Then
            If chkYupp.Checked Then
                dt = obj.executeSQL("select  count(*) [Count],ChannelId,[Date],[Uploaded By] from ( SELECT distinct ChannelId,uploaddate 'Date',lastupdatedby 'Uploaded by',progname  FROM fn_rep_programimagesuploaded" & strPortrait & "('" & txtFromDate.Text & "','" & txtToDate.Text & "')  where isyupp=1) a group by ChannelId,[Date],[Uploaded By] order by 4,2,3", False)
            Else
                dt = obj.executeSQL("select  count(*) [Count],ChannelId,[Date],[Uploaded By] from ( SELECT distinct ChannelId,uploaddate 'Date',lastupdatedby 'Uploaded by',progname  FROM fn_rep_programimagesuploaded" & strPortrait & "('" & txtFromDate.Text & "','" & txtToDate.Text & "') ) a group by ChannelId,[Date],[Uploaded By] order by 4,2,3", False)
            End If

        Else
            If chkYupp.Checked Then
                dt = obj.executeSQL("SELECT count(*) Count,[Date],[Uploaded by] FROM  ( SELECT distinct ChannelId,uploaddate 'Date',lastupdatedby 'Uploaded by',progname  FROM fn_rep_programimagesuploaded" & strPortrait & "('" & txtFromDate.Text & "','" & txtToDate.Text & "')  where isyupp=1) a GROUP BY [Date],[Uploaded by] ORDER BY 3,2", False)
            Else
                dt = obj.executeSQL("SELECT count(*) Count,[Date],[Uploaded by] FROM  ( SELECT distinct ChannelId,uploaddate 'Date',lastupdatedby 'Uploaded by',progname  FROM fn_rep_programimagesuploaded" & strPortrait & "('" & txtFromDate.Text & "','" & txtToDate.Text & "')) a GROUP BY [Date],[Uploaded by] ORDER BY 3,2", False)
            End If
        End If
        grdImagesReport.DataSource = dt
        grdImagesReport.DataBind()
    End Sub
End Class