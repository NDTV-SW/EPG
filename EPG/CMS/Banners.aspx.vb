Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class Banners
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                ddlChannel.DataBind()
                ddlProgramme.DataBind()
                Dim strpath As String = Request.QueryString("strimage")
                If Not (IsNothing(strpath)) Then
                    If strpath.Length > 0 Then
                        imgPreview.ImageUrl = "file://" & strpath
                    End If
                End If
                txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
                txtEndDate.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy")
            End If
        Catch ex As Exception
            Logger.LogError("Banners", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Dim intSno As Integer
    Protected Sub grdBanners_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBanners.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header
                    intSno = 1
                Case DataControlRowType.DataRow
                    Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
                    lbSno.Text = intSno
                    intSno = intSno + 1


                    Dim lbNotTitle As Label = TryCast(e.Row.FindControl("lbNotTitle"), Label)
                    Dim lbNotBody As Label = TryCast(e.Row.FindControl("lbNotBody"), Label)
                    Dim lbNotTime As Label = TryCast(e.Row.FindControl("lbNotTime"), Label)
                    Dim hyLogo As HyperLink = TryCast(e.Row.FindControl("hyLogo"), HyperLink)
                    hyLogo.Attributes.Add("data-toggle", "tooltip")
                    'hyLogo.Attributes.Add("data-html", "true")
                    hyLogo.Attributes.Add("title", IIf(lbNotTitle.Text = "", "No Title", lbNotTitle.Text) & " | " & IIf(lbNotBody.Text = "", "No Body", lbNotBody.Text) & " | " & IIf(lbNotTime.Text = "", "No Time", lbNotTime.Text))

            End Select
        Catch ex As Exception
            Logger.LogError("Banners", "grdBanners_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Private Sub grdBanners_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdBanners.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        Dim straction As String
        Try
            txtPriority.Text = Convert.ToInt16(txtPriority.Text)
        Catch
            txtPriority.Text = "0"
        End Try
        If btnAdd.Text.ToUpper = "ADD" Then
            straction = "A"
            If lstlanguage.Items.Count > 0 Then
                For i As Integer = 0 To lstlanguage.Items.Count - 1
                    If lstlanguage.Items(i).Selected = True Then
                        obj.executeSQL("sp_mst_banners", "channelid~progid~progtype~languageid~bannerurl~startdate~enddate~starttime~endtime~priority~notificationtitle~notificationbody~notificationtime~notificationsent~action~lastupdatedby",
                                   "varchar~int~varchar~int~varchar~datetime~datetime~datetime~datetime~int~nvarchar~nvarchar~datetime~bit~char~varchar",
                                  ddlChannel.SelectedValue & "~" & ddlProgramme.SelectedValue & "~" & ddlProgType.SelectedValue & "~" & lstlanguage.Items(i).Value & "~" &
                                  imgPreview.ImageUrl & "~" & txtStartDate.Text & "~" & txtEndDate.Text & "~" & txtStartTime.Text & "~" & txtEndTime.Text & "~" & txtPriority.Text & "~" &
                                 txtNotTitle.Text & "~" & txtNotBody.Text & "~" & txtNotTime.Text & "~False~" & straction & "~" & User.Identity.Name, True, False)
                        lstlanguage.Items(i).Selected = False
                    End If
                Next
            End If
            clearAll()

        Else
            straction = "U"
            obj.executeSQL("sp_mst_banners", "rowid~channelid~progid~progtype~languageid~bannerurl~startdate~enddate~starttime~endtime~priority~notificationtitle~notificationbody~notificationtime~notificationsent~action~lastupdatedby",
                       "int~varchar~int~varchar~int~varchar~datetime~datetime~datetime~datetime~int~nvarchar~nvarchar~datetime~bit~char~varchar",
                       lbRowid.Text & "~" & ddlChannel.SelectedValue & "~" & ddlProgramme.SelectedValue & "~" & ddlProgType.SelectedValue & "~" & lstlanguage.SelectedItem.Value & "~" &
                      imgPreview.ImageUrl & "~" & txtStartDate.Text & "~" & txtEndDate.Text & "~" & txtStartTime.Text & "~" & txtEndTime.Text & "~" & txtPriority.Text & "~" &
                      txtNotTitle.Text & "~" & txtNotBody.Text & "~" & txtNotTime.Text & "~False~" & straction & "~" & User.Identity.Name, True, False)
            clearAll()
        End If

    End Sub
    Private Sub clearAll()
        txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
        txtEndDate.Text = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy")
        txtStartTime.Text = "00:00:01"
        txtEndTime.Text = "23:59:59"
        txtPriority.Text = "0"
        btnAdd.Text = "ADD"
        imgPreview.ImageUrl = ""
        lbRowid.Text = "0"

        txtNotTitle.Text = ""
        txtNotBody.Text = ""
        txtNotTime.Text = ""
        ddlChannel.DataBind()
        ddlProgramme.DataBind()

        grdBanners.SelectedIndex = -1
        grdBanners.DataBind()
    End Sub

    Private Function getFileName() As String
        Dim strFileName As String
        Dim objImage As New clsUploadModules
        strFileName = ddlChannel.SelectedValue & "_" & ddlProgramme.SelectedValue & "_1_" & DateTime.Now.ToString("ddMMyy_HH") & ".jpg"
        strFileName = objImage.sanitizeImageFile(strFileName.Replace(".jpg", "")) & ".jpg"
        Return strFileName
    End Function

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If FileUpload1.HasFile And FileUpload1.PostedFile.FileName.EndsWith(".jpg") Then
            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream)
            Dim height As Integer = img.Height
            Dim width As Integer = img.Width
            Dim strBannerHeight As String = ConfigurationManager.AppSettings("ureqabannerheight").ToString
            Dim strBannerWidth As String = ConfigurationManager.AppSettings("ureqabannerwidth").ToString

            If height = strBannerHeight And width = strBannerWidth Then

                Dim size As Decimal = Math.Round((CDec(FileUpload1.PostedFile.ContentLength) / CDec(1024)), 2)
                'ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "alert('Size: " & size.ToString() & "KB\nHeight: " & height.ToString() + "\nWidth: " & width.ToString() & "');", True)

                If Not System.IO.Directory.Exists(MapPath("~/Uploads/Banners/" & width & "X" & height & "/")) Then
                    System.IO.Directory.CreateDirectory(MapPath("~/Uploads/Banners/" & width & "X" & height & "/"))
                End If
                Dim strpath As String = MapPath("~/Uploads/Banners/" & width & "X" & height & "/") & getFileName()
                img.Save(strpath)
                Dim abc As New clsFTP
                'System.Threading.Thread.Sleep(1000)
                abc.doS3Task(strpath, "/uploads/Banners/" & width & "X" & height)
                System.Threading.Thread.Sleep(200)

                Dim strUrl As String = "http://epgops.ndtv.com/uploads/Banners/" & width & "X" & height & "/" & getFileName()

                imgPreview.ImageUrl = strUrl
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "alert('File size should be " & strBannerWidth & "x" & strBannerHeight & "');", True)
            End If
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "alert('File format not correct');", True)
        End If
    End Sub

    Protected Sub grdBanners_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdBanners.RowDeleting
        Dim lbRowId As Label = DirectCast(grdBanners.Rows(e.RowIndex).FindControl("lbRowId"), Label)

        Dim obj As New clsExecute
        Dim straction As String
        straction = "D"
        obj.executeSQL("sp_mst_banners", "rowid~action~lastupdatedby", _
                    "int~char~varchar", lbRowId.Text & "~" & straction & "~" & User.Identity.Name, True, False)
        clearAll()

    End Sub

    Protected Sub grdBanners_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdBanners.SelectedIndexChanged
        lbRowid.Text = DirectCast(grdBanners.SelectedRow.FindControl("lbRowId"), Label).Text
        ddlChannel.SelectedValue = DirectCast(grdBanners.SelectedRow.FindControl("lbchannelid"), Label).Text
        ddlProgramme.DataBind()
        Try
            ddlProgramme.SelectedValue = DirectCast(grdBanners.SelectedRow.FindControl("lbprogid"), Label).Text
        Catch
        End Try
        lstlanguage.SelectedValue = DirectCast(grdBanners.SelectedRow.FindControl("lblanguageid"), Label).Text
        ddlProgType.SelectedValue = DirectCast(grdBanners.SelectedRow.FindControl("lbprogtype"), Label).Text
        txtPriority.Text = DirectCast(grdBanners.SelectedRow.FindControl("lbPriority"), Label).Text

        txtStartDate.Text = Convert.ToDateTime(DirectCast(grdBanners.SelectedRow.FindControl("lbstartdate"), Label).Text).ToString("MM/dd/yyyy")
        txtEndDate.Text = Convert.ToDateTime(DirectCast(grdBanners.SelectedRow.FindControl("lbenddate"), Label).Text).ToString("MM/dd/yyyy")
        txtStartTime.Text = Convert.ToDateTime(DirectCast(grdBanners.SelectedRow.FindControl("lbstarttime"), Label).Text).ToString("HH:mm:ss")
        txtEndTime.Text = Convert.ToDateTime(DirectCast(grdBanners.SelectedRow.FindControl("lbendtime"), Label).Text).ToString("HH:mm:ss")
        imgPreview.ImageUrl = DirectCast(grdBanners.SelectedRow.FindControl("imglogo"), Image).ImageUrl

        txtNotTitle.Text = DirectCast(grdBanners.SelectedRow.FindControl("lbNotTitle"), Label).Text
        txtNotBody.Text = DirectCast(grdBanners.SelectedRow.FindControl("lbNotBody"), Label).Text
        txtNotTime.Text = DirectCast(grdBanners.SelectedRow.FindControl("lbNotTime"), Label).Text

        btnAdd.Text = "Update"
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    'Protected Sub moveUp(ByRef priority As Integer, ByVal tableName As String)
    '    Dim obj As New clsExecute
    '    Dim dt As DataTable = obj.executeSQL("SELECT priority FROM " & tableName & " where priority<" & priority & " ORDER BY priority desc", False)

    '    If dt.Rows.Count > 0 Then
    '        Dim tmpID As Integer = dt.Rows.Item(0).Item(0)
    '        obj.executeSQL("UPDATE " & tableName & " SET priority=-1  WHERE priority=" & priority, False)
    '        obj.executeSQL("UPDATE " & tableName & " SET priority=" & priority & "  WHERE priority=" & tmpID, False)
    '        obj.executeSQL("UPDATE " & tableName & " SET priority=" & tmpID & " WHERE priority=-1", False)
    '        grdBanners.DataBind()
    '    End If
    'End Sub

    'Protected Sub moveDown(ByRef priority As Integer, ByVal tableName As String)
    '    Dim obj As New clsExecute
    '    Dim dt As DataTable = obj.executeSQL("SELECT priority FROM " & tableName & " where priority>" & priority & " ORDER BY priority", False)

    '    If dt.Rows.Count > 0 Then
    '        Dim tmpID As Integer = dt.Rows.Item(0).Item(0)
    '        obj.executeSQL("UPDATE " & tableName & " SET priority=-1  WHERE priority=" & priority, False)
    '        obj.executeSQL("UPDATE " & tableName & " SET priority=" & priority & "  WHERE priority=" & tmpID, False)
    '        obj.executeSQL("UPDATE " & tableName & " SET priority=" & tmpID & " WHERE priority=-1", False)
    '        grdBanners.DataBind()
    '    End If


    'End Sub

    'Protected Sub grdBanners_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdBanners.RowCommand
    '    Try
    '        Dim lbpriority As Label = TryCast(grdBanners.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbpriority"), Label)
    '        If e.CommandName.ToLower = "up" Then
    '            moveUp(lbpriority.Text, "mst_banners")
    '        End If
    '        If e.CommandName.ToLower = "down" Then
    '            moveDown(lbpriority.Text, "mst_banners")
    '        End If
    '    Catch ex As Exception
    '        Logger.LogError("Banners Master", "grdBanners_RowCommand", ex.Message.ToString, User.Identity.Name)
    '    End Try
    'End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        grdBanners.DataBind()
    End Sub

    Protected Sub ddlProgramme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgramme.SelectedIndexChanged
        txtNotTitle.Text = ddlProgramme.SelectedItem.Text
        txtNotBody.Text = "Tap to know more and set a reminder."
    End Sub

    Protected Sub txtEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtEndDate.TextChanged
        Try
            txtNotTime.Text = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy/MM/dd 10:10:00")
        Catch
        End Try
    End Sub
End Class