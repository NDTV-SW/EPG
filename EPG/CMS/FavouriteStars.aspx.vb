Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class FavouriteStars
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then


            End If
        Catch ex As Exception
            Logger.LogError("AppScreens", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdFavouriteStars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdFavouriteStars.SelectedIndexChanged
        ddlLanguage.SelectedValue = DirectCast(grdFavouriteStars.SelectedRow.FindControl("lbLanguageId"), Label).Text
        ddlCelebrity.DataBind()
        ddlCelebrity.SelectedValue = DirectCast(grdFavouriteStars.SelectedRow.FindControl("lbCelebrityId"), Label).Text
        lbRowId.Text = DirectCast(grdFavouriteStars.SelectedRow.FindControl("lbRowId"), Label).Text
        btnAdd.Text = "UPDATE"
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        Dim dt As DataTable
        dt = obj.executeSQL("select * from mst_celebrity_favourites where languageid='" & ddlLanguage.SelectedValue & "' and celebrityid='" & ddlCelebrity.SelectedValue & "'", False)
        If dt.Rows.Count > 0 Then
            Response.Write("<script type=""text/javascript"">alert('Same Record already exists');</script>")
        Else

            If btnAdd.Text = "ADD" Then
                obj.executeSQL("sp_mst_celebrity_favourites", "rowid~languageid~celebrityid~action~actionby", "int~int~int~varchar~varchar", _
                              "0~" & ddlLanguage.SelectedValue & "~" & ddlCelebrity.SelectedValue & "~A~" & User.Identity.Name, True, False)
            Else
                obj.executeSQL("sp_mst_celebrity_favourites", "rowid~languageid~celebrityid~action~actionby", "int~int~int~varchar~varchar", _
                               lbRowId.Text & "~" & ddlLanguage.SelectedValue & "~" & ddlCelebrity.SelectedValue & "~U~" & User.Identity.Name, True, False)
            End If
            clearall()
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearall()
    End Sub
    Private Sub clearall()
        
        btnAdd.Text = "ADD"
        lbRowId.Text = "0"
        grdFavouriteStars.SelectedIndex = -1
        grdFavouriteStars.DataBind()
    End Sub

    Protected Sub grdAppScreens_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdFavouriteStars.RowDeleting
        Dim obj As New clsExecute
        Dim intRowid As Integer
        intRowid = DirectCast(grdFavouriteStars.Rows(e.RowIndex).FindControl("lbRowID"), Label).Text

        obj.executeSQL("sp_mst_celebrity_favourites", "rowid~languageid~celebrityid~action~actionby", "int~int~int~varchar~varchar", _
                           intRowid & "~0~0~D~" & User.Identity.Name, True, False)
        grdFavouriteStars.DataBind()
    End Sub

    Protected Sub grdAppScreens_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdFavouriteStars.RowDeleted
        If Not e.Exception Is Nothing Then
            ' myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub
End Class