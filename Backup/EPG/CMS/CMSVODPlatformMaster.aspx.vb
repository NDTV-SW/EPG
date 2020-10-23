Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSVODPlatformMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("VOD Platform Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Try

            lbPlatformId.Text = DirectCast(grd.SelectedRow.FindControl("lbPlatformId"), Label).Text
            ddlCompany.SelectedValue = DirectCast(grd.SelectedRow.FindControl("lbCompanyId"), Label).Text
            txtName.Text = DirectCast(grd.SelectedRow.FindControl("lbPlatformName"), Label).Text
            txtWebSiteURL.Text = DirectCast(grd.SelectedRow.FindControl("lbwebsiteURL"), Label).Text

            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("VOD Platform Master", "grd_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        grd.SelectedIndex = -1
        bindGrid(False)
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim strParams As String, strValues As String, strType As String
        If btnUpdate.Text = "ADD" Then
            strParams = "PlatformName~websiteURL~Companyid~action~actionuser"
            strType = "VarChar~VarChar~Int~char~varchar"
            strValues = txtName.Text & "~" & txtWebSiteURL.Text & "~" & ddlCompany.SelectedValue & "~A~" & User.Identity.Name
        Else
            strParams = "PlatformID~PlatformName~websiteURL~Companyid~action~actionuser"
            strType = "Int~VarChar~VarChar~Int~char~varchar"
            strValues = lbPlatformId.Text & "~" & txtName.Text & "~" & txtWebSiteURL.Text & "~" & ddlCompany.SelectedValue & "~U~" & User.Identity.Name
        End If
        Dim obj As New clsExecute
        obj.executeSQL("sp_mst_Vodplatforms", strParams, strType, strValues, True, False)
        bindGrid(True)
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Try
            Dim lbPlatformID As Label = DirectCast(grd.Rows(e.RowIndex).FindControl("lbPlatformID"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("delete from mst_vodplatforms where platformId='" & lbPlatformID.Text & "'", False)

            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("PlatformMaster", "grd_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grd_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub


    Protected Sub bindGrid(ByVal paging As Boolean)
        sqlDS.SelectCommand = "select b.CompanyName,a.* from mst_VODPlatforms a  join mst_company b on a.companyid=b.CompanyId and PlatformName like '%" & txtSearch.Text.Trim & "%'"
        sqlDS.SelectCommandType = SqlDataSourceCommandType.Text
        grd.DataSourceID = "sqlDS"
        If paging = False Then
            grd.PageIndex = 0
        End If
        grd.SelectedIndex = -1
        grd.DataBind()
        clearAll()
    End Sub

    Private Sub clearAll()
        lbPlatformId.Text = String.Empty
        txtName.Text = String.Empty
        txtWebSiteURL.Text = String.Empty
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(False)
    End Sub

    Dim intSno As Integer
    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound
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

    Protected Sub grd_PageIndexChanged(sender As Object, e As EventArgs) Handles grd.PageIndexChanged
        bindGrid(True)
    End Sub

End Class