Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSTvStarMapping
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                'ddlLanguageName.DataBind()
                'ddlLanguageName.SelectedValue = 2
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("TV Star Mapping", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdTvStarMapping_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdTvStarMapping.SelectedIndexChanged
        Try
            lbRowId.Text = DirectCast(grdTvStarMapping.SelectedRow.FindControl("lbRowId"), Label).Text
            ddlTvStar.SelectedValue = DirectCast(grdTvStarMapping.SelectedRow.FindControl("lbProfileid"), Label).Text
            ddlLanguageName.SelectedValue = DirectCast(grdTvStarMapping.SelectedRow.FindControl("lbLanguageid"), Label).Text
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("TV Star Mapping", "grdTvStarMapping_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim sql As String

        If btnUpdate.Text = "ADD" Then
            sql = "insert into tvstars_lang_mapping(profileId,LanguageId) values('" & ddlTvStar.SelectedValue & "','" & ddlLanguageName.SelectedValue & "')"
        Else
            sql = "update tvstars_lang_mapping set profileid='" & ddlTvStar.SelectedValue & "',languageid='" & ddlLanguageName.SelectedValue & "' where rowid='" & lbRowId.Text & "'"
        End If
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL(sql, False)
        bindGrid(True)

    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean)
        sqlDSTvStarMapping.SelectCommand = "select a.rowid, a.profileid,a.languageid, b.Name,c.fullname from tvstars_lang_mapping a join mst_tvstars b on a.profileid=b.ProfileID join mst_language c on a.languageid=c.LanguageID and b.name like '%" & txtSearch.Text.Trim & "%'"
        sqlDSTvStarMapping.SelectCommandType = SqlDataSourceCommandType.Text
        grdTvStarMapping.DataSourceID = "sqlDSTvStarMapping"
        If paging = False Then
            grdTvStarMapping.PageIndex = 0
        End If
        grdTvStarMapping.SelectedIndex = -1
        grdTvStarMapping.DataBind()
        clearAll()
    End Sub

    Private Sub clearAll()
        lbRowId.Text = ""
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(False)
    End Sub
    Dim intSno As Integer
    Protected Sub grdTvStarMapping_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTvStarMapping.RowDataBound
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

    Protected Sub grdTvStarMapping_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTvStarMapping.PageIndexChanging
        grdTvStarMapping.PageIndex = e.NewPageIndex
        bindGrid(True)
    End Sub

    Protected Sub grdTvStarMapping_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdTvStarMapping.Sorting
        bindGrid(True)
    End Sub

End Class