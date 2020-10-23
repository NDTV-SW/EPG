Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class ProgrammeLogos
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                ddlChannel.DataBind()
                bindGrid()
            End If
        Catch ex As Exception
            Logger.LogError("UploadProgrammeLogos", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid()
    End Sub

    Protected Sub grdProgImage_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgImage.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    Dim imglogo As Image = DirectCast(e.Row.FindControl("imglogo"), Image)
                    Dim hylogo As HyperLink = DirectCast(e.Row.FindControl("hylogo"), HyperLink)
                    Dim hyEdit As HyperLink = DirectCast(e.Row.FindControl("hyEdit"), HyperLink)
                    Dim hvVideo As HyperLink = DirectCast(e.Row.FindControl("hyVideo"), HyperLink)
                    Dim lbVideoURL As Label = DirectCast(e.Row.FindControl("lbVideoURL"), Label)
                    Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
                    Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)
                    Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                    Dim lbProgLogo As Label = DirectCast(e.Row.FindControl("lbProgLogo"), Label)
                    Dim lbColor As Label = DirectCast(e.Row.FindControl("lbColor"), Label)
                    Dim lbfileName As Label = DirectCast(e.Row.FindControl("lbfileName"), Label)

                    lbfileName.Text = lbChannelId.Text & "_" & lbProgName.Text & "_" & lbProgId.Text & ".jpg"
                    lbfileName.Text = lbfileName.Text.Replace("%", "").Replace("..", "")
                    Dim objImage As New clsUploadModules
                    lbfileName.Text = objImage.sanitizeImageFile(lbfileName.Text)

                    hyEdit.NavigateUrl = "javascript:showDiv('MainContent_grdProgImage_imglogo_" & e.Row.RowIndex & "','" & lbProgId.Text & "','" & lbfileName.Text & "','MainContent_grdProgImage_lbVideoURL_" & e.Row.RowIndex & "')"

                    hvVideo.NavigateUrl = "javascript:showVideo('" & lbVideoURL.Text & "','" & lbProgId.Text & "','" & lbfileName.Text & "','MainContent_grdProgImage_lbVideoURL_" & e.Row.RowIndex & "')"

                    If lbColor.Text = "0" Then
                        e.Row.BackColor = Drawing.Color.LightGreen
                    ElseIf lbColor.Text = "1" Then
                        e.Row.BackColor = Drawing.Color.LightBlue
                    End If

                    imglogo.ImageUrl = "~/uploads/" & lbProgLogo.Text
                    hylogo.NavigateUrl = "~/uploads/" & lbProgLogo.Text

            End Select
        Catch ex As Exception
            Logger.LogError("UploadProgrammeLogos", "grdProgImage_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        Dim fileName As String = jsFileName.Value
        Dim progid As String = jsProgId.Value
        Dim currentMenu As HtmlGenericControl = DirectCast(Page.FindControl("jsFileName"), HtmlGenericControl)

        Dim myControl As Label = TryCast(Page.FindControl("jsFileName"), Label)
        Dim strFileName As String = Me.Title 'jsFileName.Text

        If (AsyncFileUpload1.HasFile) Then
            Dim strpath As String = MapPath("~/uploads/") & fileName
            AsyncFileUpload1.SaveAs(strpath)
            System.Threading.Thread.Sleep(2000)
            Dim abc As New clsFTP
            abc.doS3Task(strpath, "/uploads")
            System.Threading.Thread.Sleep(2000)
        End If
        If fileName.ToLower.EndsWith(".jpg") Or fileName.ToLower.EndsWith(".jpeg") Then
            Dim obj As New clsExecute
            obj.executeSQL("update mst_program set programlogo='" & fileName & "' where progid='" & progid & "'", False)
            obj.executeSQL("insert into aud_mst_program_proglogo(progid,action,lastupdatedat,lastupdatedby) values('" & progid & "','I',dbo.GetLocalDate(),'" & User.Identity.Name & "')", False)
        End If
        bindGrid()
    End Sub

    Private Sub bindGrid()
        Dim strSearch As String = txtSearch1.Text
        Dim strMissing As String
        If chkMissing.Checked = True Then
            strMissing = "1"
        Else
            strMissing = "0"
        End If
        If strSearch = "" Then
            strSearch = "0"
        End If
        Dim paging As Boolean = True

        If Not (strSearch = "0" And ddlChannel.SelectedValue = "0") Then
            Dim adp As New SqlDataAdapter("rpt_getProgramLogosSearch_New", ConString)
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.Add("@search", SqlDbType.VarChar, 100).Value = strSearch
            adp.SelectCommand.Parameters.Add("@channelid", SqlDbType.VarChar, 100).Value = ddlChannel.SelectedValue
            adp.SelectCommand.Parameters.Add("@missing", SqlDbType.Bit).Value = chkMissing.Checked
            adp.SelectCommand.Parameters.Add("@script", SqlDbType.Bit).Value = chkScript.Checked
            Dim dt As New DataTable
            adp.Fill(dt)
            grdProgImage.DataSource = dt
            grdProgImage.DataBind()
        End If
        
        If paging = False Then
            grdProgImage.PageIndex = 0
        End If
        grdProgImage.SelectedIndex = -1
        grdProgImage.DataBind()
    End Sub

    Protected Sub grdProgImage_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdProgImage.RowDeleting
        Try
            Dim lbProgId As Label = DirectCast(grdProgImage.Rows(e.RowIndex).FindControl("lbProgId"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("update mst_program set programlogo='' where progid='" & lbProgId.Text & "'", False)
            bindGrid()
        Catch ex As Exception
            Logger.LogError("UploadProgrammeLogos", "grdProgImage_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdProgImage_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdProgImage.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub grdProgImage_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProgImage.PageIndexChanging
        grdProgImage.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub
End Class