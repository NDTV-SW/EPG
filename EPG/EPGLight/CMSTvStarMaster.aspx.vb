Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSTvStarMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                'ddlLanguageName.DataBind()
                'ddlLanguageName.SelectedValue = 2
                bindGrid(False)
                'AsyncFileUpload1.Visible = False
            End If
        Catch ex As Exception
            Logger.LogError("TV Star Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Protected Sub grdTvStar_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles grdTvStar.RowCommand
        If e.CommandName = "Upload" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            Dim selectedRow As GridViewRow = grdTvStar.Rows(index)
            Dim contactCell As TableCell = selectedRow.Cells(1)
            Dim contact As String = contactCell.Text
            Dim strStarName As String = TryCast(selectedRow.FindControl("lbName"), Label).Text
            Session("Profile") = TryCast(selectedRow.FindControl("lbProfileId"), Label).Text
            Session("FileName") = strStarName.Replace(" ", "_") & "_" & Session("Profile").ToString & ".jpg"

            'AsyncFileUpload1.Visible = True
        End If

    End Sub
    Protected Sub grdTvStar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdTvStar.SelectedIndexChanged
        Try
            txtName.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbName"), Label).Text
            txtCity.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbCity"), Label).Text
            txtBiography.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbBiography"), Label).Text
            txtDOB.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbDOB"), Label).Text
            txtPOB.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbPOB"), Label).Text
            txtTwitter.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbTwitterTag"), Label).Text
            ddlGender.SelectedValue = DirectCast(grdTvStar.SelectedRow.FindControl("lbGender"), Label).Text
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("TV Star Master", "grdTvStar_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub


    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim obj As New clsExecute
        Dim dt As DataTable

        If btnUpdate.Text = "ADD" Then
            dt = obj.executeSQL("sp_mst_tvstars", _
                                    "name~city~gender~twittertag~dob~placeofBirth~biography~insertedby~modifiedby~action", _
                                    "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                    txtName.Text & "~" & txtCity.Text & "~" & ddlGender.SelectedValue & "~" & txtTwitter.Text & "~" & txtDOB.Text & "~" & _
                                    txtPOB.Text & "~" & txtBiography.Text & "~" & User.Identity.Name & "~" & User.Identity.Name & "~A", True, True)
        Else
            dt = obj.executeSQL("sp_mst_tvstars", _
                                    "name~city~gender~twittertag~dob~placeofBirth~biography~modifiedby~action~profileid", _
                                    "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                    txtName.Text & "~" & txtCity.Text & "~" & ddlGender.SelectedValue & "~" & txtTwitter.Text & "~" & txtDOB.Text & "~" & _
                                    txtPOB.Text & "~" & txtBiography.Text & "~" & User.Identity.Name & "~U~" & Session("Profile").ToString, True, True)
        End If
        bindGrid(True)

    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean)
        sqlDSTvStar.SelectCommand = "select  'http://localhost:7799/' + starpic as profilePath1,* from mst_tvStars where name like '%" & txtSearch.Text.Trim & "%'"
        sqlDSTvStar.SelectCommandType = SqlDataSourceCommandType.Text
        grdTvStar.DataSourceID = "sqlDSTvStar"
        If paging = False Then
            grdTvStar.PageIndex = 0
        End If
        grdTvStar.SelectedIndex = -1
        grdTvStar.DataBind()

        clearAll()
    End Sub

    Private Sub clearAll()
        txtName.Text = String.Empty
        txtCity.Text = String.Empty
        txtBiography.Text = String.Empty
        txtDOB.Text = String.Empty
        txtPOB.Text = String.Empty
        txtTwitter.Text = String.Empty
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(False)
    End Sub

    Dim intSno As Integer
    Protected Sub grdTvStar_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTvStar.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    Protected Sub grdTvStar_PageIndexChanged(sender As Object, e As EventArgs) Handles grdTvStar.PageIndexChanged
        bindGrid(True)
    End Sub

    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)

        Try
            'If Session("FileName") <> "0" And Session("Profile") <> "0" Then
            Dim adpGetMaxFile As New SqlDataAdapter("select count(*) from picgallery_pictures where galleryId='" & jsGalleryId.Value & "'", ConString)
            Dim dtGetMaxFile As New DataTable
            adpGetMaxFile.Fill(dtGetMaxFile)

            Dim fileName As String = jsGalleryId.Value & "_" & Convert.ToInt32(dtGetMaxFile.Rows(0)(0).ToString) + 1 & ".jpg"
            AsyncFileUpload2.SaveAs(Server.MapPath("~/Uploads/GalleryPics/" & fileName))

            Dim StrFileSavePath As String = "Uploads/GalleryPics/" & fileName
            Dim adp As New SqlDataAdapter("insert into picgallery_pictures(galleryId,url,dateCreated,DateModified,createdBy,ModifiedBy) " _
                                          & "values('" & jsGalleryId.Value & "','" & StrFileSavePath & "',getdate(),getdate(),'" & User.Identity.Name & "','" & User.Identity.Name & "')", ConString)
            Dim dt As New DataTable
            adp.Fill(dt)
            'grdPicGallery.DataBind()

            'End If

        Catch ex As Exception

        End Try

    End Sub

End Class