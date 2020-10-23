Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class AppScreens
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
   
    Protected Sub grdAppScreens_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdAppScreens.SelectedIndexChanged
        txtScreenId.Text = DirectCast(grdAppScreens.SelectedRow.FindControl("lbScreenId"), Label).Text
        txtTitle.Text = DirectCast(grdAppScreens.SelectedRow.FindControl("lbTitle"), Label).Text
        txtScreenId.Enabled = False
        btnAdd.Text = "UPDATE"
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        If btnAdd.Text = "ADD" Then
            obj.executeSQL("sp_app_mst_screenids", "screenid~title~action~actionby", "int~varchar~varchar~varchar", _
                           txtScreenId.Text & "~" & txtTitle.Text & "~A~" & User.Identity.Name, True, False)
        Else
            obj.executeSQL("sp_app_mst_screenids", "screenid~title~action~actionby", "int~varchar~varchar~varchar", _
                           txtScreenId.Text & "~" & txtTitle.Text & "~U~" & User.Identity.Name, True, False)
        End If
        clearall()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearall()
    End Sub
    Private Sub clearall()
        txtTitle.Text = ""
        txtScreenId.Enabled = True
        txtScreenId.Text = ""
        btnAdd.Text = "ADD"
        'lbScreenId.Text = "0"
        grdAppScreens.SelectedIndex = -1
        grdAppScreens.DataBind()
    End Sub

    Protected Sub grdAppScreens_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdAppScreens.RowDeleting
        Dim obj As New clsExecute
        Dim intScreenID As Integer
        intScreenID = DirectCast(grdAppScreens.Rows(e.RowIndex).FindControl("lbScreenId"), Label).Text

        obj.executeSQL("sp_app_mst_screenids", "screenid~title~action~actionby", "int~varchar~varchar~varchar", _
                           intScreenID & "~~D~" & User.Identity.Name, True, False)
        grdAppScreens.DataBind()
    End Sub

    Protected Sub grdAppScreens_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdAppScreens.RowDeleted
        If Not e.Exception Is Nothing Then
            ' myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub
End Class