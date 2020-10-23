Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class Notifications
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            txtURL.Text = "http://search-ureqa-bulk6ohxl52baeqfsacpdzidra.ap-southeast-1.es.amazonaws.com/shows/all/_search?sort=airtime&size=1&q="
        End If
    End Sub

    'Dim intSno As Integer
   
    Private Sub grdNotifications_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdNotifications.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        Dim straction As String, strNotificationType As String
        If chkNotifyType.Checked Then
            strNotificationType = "auto"
        Else
            strNotificationType = "manual"
        End If
        If btnAdd.Text.ToUpper = "ADD" Then
            straction = "A"
            obj.executeSQL("sp_mst_Notifications", "title~body~category~img~actionurl~killtime~repeat~repeatduration~keywords~genrekeyword~languagekeyword~notifyall~action~lastupdatedby~notificationtype",
                       "nvarchar~nvarchar~varchar~varchar~varchar~datetime~bit~int~varchar~varchar~varchar~bit~char~varchar~varchar",
                      txtTitle.Text.Trim & "~" & txtBody.Text.Trim & "~" & ddlCategory.SelectedValue & "~" & imgPreview.ImageUrl & "~" & txtURL.Text & "~" & txtKillTime.Text & "~" &
                      chkRepeat.Checked & "~" & ddlRepeatDuration.SelectedValue & "~" & txtKeywords.Text & "~" & txtGenreKeywords.Text & "~" & txtLangKeywords.Text & "~" & chkNotifyAll.Checked & "~" & straction & "~" & User.Identity.Name & "~" & strNotificationType, True, False)
            clearAll()
        Else
            straction = "U"
            obj.executeSQL("sp_mst_Notifications", "rowid~title~body~category~img~actionurl~killtime~repeat~repeatduration~keywords~genrekeyword~languagekeyword~notifyall~action~lastupdatedby~notificationtype",
                        "int~nvarchar~nvarchar~varchar~varchar~varchar~datetime~bit~int~varchar~varchar~varchar~bit~char~varchar~varchar",
                        lbRowid.Text.Trim & "~" & txtTitle.Text.Trim & "~" & txtBody.Text.Trim & "~" & ddlCategory.SelectedValue & "~" & imgPreview.ImageUrl & "~" & txtURL.Text & "~" & txtKillTime.Text & "~" &
                        chkRepeat.Checked & "~" & ddlRepeatDuration.SelectedValue & "~" & txtKeywords.Text & "~" & txtGenreKeywords.Text & "~" & txtLangKeywords.Text & "~" & chkNotifyAll.Checked & "~" & straction & "~" & User.Identity.Name & "~" & strNotificationType, True, False)
            clearAll()
        End If

    End Sub
    Private Sub clearAll()

        lbRowid.Text = "0"
        txtTitle.Text = ""
        txtBody.Text = ""
        ddlCategory.SelectedIndex = 0
        txtURL.Text = "http://search-ureqa-bulk6ohxl52baeqfsacpdzidra.ap-southeast-1.es.amazonaws.com/shows/all/_search?sort=airtime&size=1&q="
        txtKillTime.Text = ""
        chkRepeat.Checked = False
        ddlRepeatDuration.SelectedIndex = 0
        txtKeywords.Text = ""
        txtGenreKeywords.Text = ""
        txtLangKeywords.Text = ""
        chkNotifyAll.Checked = False

        btnAdd.Text = "ADD"
        grdNotifications.SelectedIndex = -1
        grdNotifications.DataBind()
    End Sub



    Protected Sub grdNotifications_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdNotifications.RowDeleting
        Dim lbRowId As Label = DirectCast(grdNotifications.Rows(e.RowIndex).FindControl("lbRowId"), Label)

        Dim obj As New clsExecute
        Dim straction As String
        straction = "D"
        obj.executeSQL("sp_mst_notifications", "rowid~action~lastupdatedby", _
                    "int~char~varchar", lbRowId.Text & "~" & straction & "~" & User.Identity.Name, True, False)
        clearAll()

    End Sub

    Protected Sub grdNotifications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdNotifications.SelectedIndexChanged
        lbRowid.Text = DirectCast(grdNotifications.SelectedRow.FindControl("lbRowId"), Label).Text
        txtTitle.Text = DirectCast(grdNotifications.SelectedRow.FindControl("lbTitle"), Label).Text
        txtBody.Text = DirectCast(grdNotifications.SelectedRow.FindControl("lbBody"), Label).Text
        ddlCategory.SelectedValue = DirectCast(grdNotifications.SelectedRow.FindControl("lbCategory"), Label).Text
        txtURL.Text = DirectCast(grdNotifications.SelectedRow.FindControl("lbActionURL"), Label).Text
        txtKillTime.Text = DirectCast(grdNotifications.SelectedRow.FindControl("lbKillTime"), Label).Text
        chkRepeat.Checked = DirectCast(grdNotifications.SelectedRow.FindControl("chkRepeat"), CheckBox).Checked
        ddlRepeatDuration.SelectedValue = DirectCast(grdNotifications.SelectedRow.FindControl("lbRepeatDuration"), Label).Text
        txtKeywords.Text = DirectCast(grdNotifications.SelectedRow.FindControl("lbKeywords"), Label).Text
        txtGenreKeywords.Text = DirectCast(grdNotifications.SelectedRow.FindControl("lbGenreKeyword"), Label).Text
        txtLangKeywords.Text = DirectCast(grdNotifications.SelectedRow.FindControl("lbLanguageKeyword"), Label).Text
        chkNotifyAll.Checked = DirectCast(grdNotifications.SelectedRow.FindControl("chkNotifyAll"), CheckBox).Checked
        imgPreview.ImageUrl = DirectCast(grdNotifications.SelectedRow.FindControl("imglogo"), Image).ImageUrl
        Dim strnotificationtype As String

        strnotificationtype = DirectCast(grdNotifications.SelectedRow.FindControl("lbnotificationtype"), Label).Text

        If strnotificationtype.ToLower = "auto" Then
            chkNotifyType.Checked = True
        Else
            chkNotifyType.Checked = False
        End If

        btnAdd.Text = "Update"
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

   
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If FileUpload1.HasFile And FileUpload1.PostedFile.FileName.EndsWith(".jpg") Then
            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream)
            Dim height As Integer = img.Height
            Dim width As Integer = img.Width
            Dim strNotificationHeight As String = ConfigurationManager.AppSettings("ureqaNotificationHeight").ToString
            Dim strNotificationWidth As String = ConfigurationManager.AppSettings("ureqaNotificationWidth").ToString

            If height = strNotificationHeight And width = strNotificationWidth Then

                Dim size As Decimal = Math.Round((CDec(FileUpload1.PostedFile.ContentLength) / CDec(1024)), 2)
        
                If Not System.IO.Directory.Exists(MapPath("~/Uploads/Notifications/" & width & "X" & height & "/")) Then
                    System.IO.Directory.CreateDirectory(MapPath("~/Uploads/Notifications/" & width & "X" & height & "/"))
                End If
                Dim strpath As String = MapPath("~/Uploads/Notifications/" & width & "X" & height & "/") & getFileName()
                img.Save(strpath)

                Dim strUrl As String = "http://epgops.ndtv.com/uploads/Notifications/" & width & "X" & height & "/" & getFileName()

                imgPreview.ImageUrl = strUrl
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "alert('File size should be " & strNotificationWidth & "x" & strNotificationHeight & "');", True)
            End If
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "alert('File format not correct');", True)
        End If
    End Sub

    Private Function getFileName() As String
        Dim strFileName As String
        Dim objImage As New clsUploadModules
        strFileName = txtTitle.Text & "_" & txtKillTime.Text & ".jpg"
        strFileName = objImage.sanitizeImageFile(strFileName.Replace(".jpg", "")) & ".jpg"
        Return strFileName
    End Function

    Protected Sub grdNotifications_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdNotifications.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hyPush As HyperLink = TryCast(e.Row.FindControl("hyPush"), HyperLink)
            Dim hyTestPush As HyperLink = TryCast(e.Row.FindControl("hyTestPush"), HyperLink)
            Dim lbRowId As Label = TryCast(e.Row.FindControl("lbRowId"), Label)
            hyPush.NavigateUrl = "javascript:pushNotification('" & lbRowId.Text & "')"
			hyTestPush.NavigateUrl = "javascript:pushTestNotification('" & lbRowId.Text & "')"
	
        End If
    End Sub
End Class