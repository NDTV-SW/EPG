Imports System.IO

Public Class rptImagesMissingDateWise
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        If Page.IsPostBack = False Then
            txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
            ddl.DataBind()
            bindGrid()
        End If

        'Catch ex As Exception
        '    Logger.LogError("rptImagesMissingDateWise", "Page Load", ex.Message.ToString, User.Identity.Name)
        'End Try
    End Sub

    Protected Sub grdImagesReport_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdImagesReport.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                Dim lbProgId As Label = TryCast(e.Row.FindControl("lbProgId"), Label)
                Dim lbChannelid As Label = TryCast(e.Row.FindControl("lbChannelid"), Label)
                Dim lbProgname As Label = TryCast(e.Row.FindControl("lbProgname"), Label)

                Dim hyLogo As HyperLink = TryCast(e.Row.FindControl("hyLogo"), HyperLink)
                Dim hyLogoPortrait As HyperLink = TryCast(e.Row.FindControl("hyLogoPortrait"), HyperLink)
                Dim img As Image = TryCast(e.Row.FindControl("img"), Image)
                Dim imgPortrait As Image = TryCast(e.Row.FindControl("imgPortrait"), Image)
                Dim lbProgLogo As Label = TryCast(e.Row.FindControl("lbProgLogo"), Label)
                Dim lbProgLogoPortrait As Label = TryCast(e.Row.FindControl("lbProgLogoPortrait"), Label)
                Dim hyEditLandscape As HyperLink = DirectCast(e.Row.FindControl("hyEditLandscape"), HyperLink)
                Dim hyEditPortrait As HyperLink = DirectCast(e.Row.FindControl("hyEditPortrait"), HyperLink)
                Dim lbfileName As Label = DirectCast(e.Row.FindControl("lbfileName"), Label)

                lbfileName.Text = lbChannelid.Text & "_" & lbProgname.Text & "_" & lbProgId.Text
                lbfileName.Text = lbfileName.Text.Replace("%", "").Replace("..", "")
                Dim objImage As New clsUploadModules
                lbfileName.Text = objImage.sanitizeImageFile(lbfileName.Text.Replace(".jpg", "")) & ".jpg"

                hyLogo.NavigateUrl = lbProgLogo.Text
                hyLogoPortrait.NavigateUrl = lbProgLogoPortrait.Text

                hyEditLandscape.NavigateUrl = "javascript:showDiv('MainContent_grdImagesReport_img_" & e.Row.RowIndex & "','" & lbProgId.Text & "','" & lbfileName.Text & "','0')"
                hyEditPortrait.NavigateUrl = "javascript:showDiv('MainContent_grdImagesReport_imgPortrait_" & e.Row.RowIndex & "','" & lbProgId.Text & "','" & lbfileName.Text & "','1')"


                img.ImageUrl = lbProgLogo.Text
                imgPortrait.ImageUrl = lbProgLogoPortrait.Text
        End Select
    End Sub

    Private Sub bindGrid()
        Dim obj As New clsExecute
        Dim sql As String = ""
        sql = sql & "SELECT progid,a.channelid,progname,'http://epgops.ndtv.com/uploads/' + programlogo proglogo,'http://epgops.ndtv.com/uploads/portrait/' + programlogoportrait proglogoportrait"
        sql = sql & " ,isnull(imagedim,'') imagedim,isnull(imagedimportrait,'') imagedimportrait,cast(isnull(imagesize,'0') as numeric(10,0)) imagesize,cast(isnull(imagesizeportrait,'0') as numeric(10,0)) imagesizeportrait FROM mst_program a join mst_channel b on a.channelid=b.channelid WHERE a.progid IN ( SELECT distinct progid FROM mst_epg WHERE progdate='" & txtStartDate.Text & "'"
        sql = sql & " and a.channelid IN ( SELECT DISTINCT channelid FROM dthcable_channelmapping WHERE operatorid IN (214) AND onair = 1"
        If chkPriority.Checked Then
            sql = sql & " and imagepriority = 1"
        End If
        sql = sql & ") )"
        If chkLandscapeMissing.Checked And chkPortraitMissing.Checked Then
            sql = sql & " and (len(isnull(programlogo,''))<5"
            sql = sql & " or len(isnull(programlogoportrait,''))<5)"
        Else
            If chkLandscapeMissing.Checked Then
                sql = sql & " and len(isnull(programlogo,''))<5"
            End If
            If chkPortraitMissing.Checked Then
                sql = sql & " and len(isnull(programlogoportrait,''))<5"
            End If
        End If
        If chkResMismatch.Checked And chkSizeMismatch.Checked Then
            sql = sql & " and (imagedim<>'1440x810' "
            sql = sql & " or cast(imagesize as numeric(10,0))>500)"
        Else
            If chkSizeMismatch.Checked Then
                sql = sql & " and cast(imagesize as numeric(10,0))>500"
            End If

            If chkResMismatch.Checked Then
                sql = sql & " and imagedim<>'1440x810'"
            End If
        End If

        If chkHighTRP.Checked Then
            sql = sql & " and isnull(b.trp,0)>=70"
        End If
        If ddl.SelectedValue <> 0 Then
            sql = sql & " and b.genreid='" & ddl.SelectedValue & "'"
        End If

        sql = sql & " order by channelid,progname"
        Dim dt As DataTable = obj.executeSQL(sql, False)
        grdImagesReport.DataSource = dt
        grdImagesReport.DataBind()
    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        bindGrid()
    End Sub


    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)

        Dim fileSize As Integer = Int32.Parse(Path.GetFileName(e.FileSize)) / 1024

        If (fileSize > 5000) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('File size greater than 5000KB. Please optimize.');", True)
            Exit Sub
        End If

        Dim fileName As String = jsFileName.Value
        Dim progid As String = jsProgId.Value
        Dim currentMenu As HtmlGenericControl = DirectCast(Page.FindControl("jsFileName"), HtmlGenericControl)
        Dim myControl As Label = TryCast(Page.FindControl("jsFileName"), Label)
        Dim strFileName As String = Me.Title 'jsFileName.Text

        If jsIsPortrait.Value = 0 Then
            If (AsyncFileUpload1.HasFile) Then
                Dim strpath As String = MapPath("~/uploads/") & fileName
                AsyncFileUpload1.SaveAs(strpath)

                Dim abc As New clsFTP
                abc.doS3Task(strpath, "/uploads")
                System.Threading.Thread.Sleep(200)
            End If
            If fileName.ToLower.EndsWith(".jpg") Or fileName.ToLower.EndsWith(".jpeg") Then
                Dim obj As New clsExecute
                obj.executeSQL("update mst_program set programlogo='" & fileName & "',imagesize=null,imagesizeportrait=null,imagedim=null,imagedimportrait=null where progid='" & progid & "'", False)
                obj.executeSQL("insert into aud_mst_program_proglogo(progid,action,lastupdatedat,lastupdatedby) values('" & progid & "','I',dbo.GetLocalDate(),'" & User.Identity.Name & "')", False)
            End If
        Else
            If (AsyncFileUpload1.HasFile) Then
                Dim strpath As String = MapPath("~/uploads/portrait/") & fileName
                AsyncFileUpload1.SaveAs(strpath)

                Dim abc As New clsFTP
                abc.doS3Task(strpath, "/uploads/portrait")
                System.Threading.Thread.Sleep(200)
            End If
            If fileName.ToLower.EndsWith(".jpg") Or fileName.ToLower.EndsWith(".jpeg") Then
                Dim obj As New clsExecute
                obj.executeSQL("update mst_program set programlogoportrait='" & fileName & "',imagesize=null,imagesizeportrait=null,imagedim=null,imagedimportrait=null where progid='" & progid & "'", False)
                obj.executeSQL("insert into aud_mst_program_proglogo_portrait(progid,action,lastupdatedat,lastupdatedby) values('" & progid & "','I',dbo.GetLocalDate(),'" & User.Identity.Name & "')", False)
            End If

        End If


        'bindGrid()
    End Sub


End Class