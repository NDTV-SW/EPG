Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CelebrityRegional
    Inherits System.Web.UI.Page

    Private Sub myMessageBox(ByVal messagestr As String)
        Logger.myLogMessage(Me.Page, messagestr)
    End Sub
    Private Sub myErrorBox(ByVal messagestr As String)
        Logger.myLogError(Me.Page, messagestr)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                ddlSearchLanguage.DataBind()
                ddlSearchLanguage.SelectedValue = 2
                bindGrid(False, False)
            End If
        Catch ex As Exception
            Logger.LogError("CelebrityRegional", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdCelebrity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdCelebrity.SelectedIndexChanged
        Try
            txtRegName.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbRegCelebName"), Label).Text
            txtPlaceOfBirth.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbRegPlaceofBirth"), Label).Text
            txtBioGraphy.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbRegBiography"), Label).Text
            ddlLanguage.SelectedValue = DirectCast(grdCelebrity.SelectedRow.FindControl("lbLanguageId"), Label).Text
            ddlLanguage.Enabled = False
            ddlCelebrity.SelectedValue = DirectCast(grdCelebrity.SelectedRow.FindControl("lbcelebrityid"), Label).Text
            ddlCelebrity.Enabled = False

            lbRowid.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbRowid"), Label).Text
            If txtRegName.Text = "" Then
                btnUpdate.Text = "ADD"
            Else
                btnUpdate.Text = "UPDATE"
            End If

        Catch ex As Exception
            Logger.LogError("CelebrityRegional", "grdCelebrity_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Try
            'sp_tmdb_celebrity
            Dim obj As New clsExecute
            If btnUpdate.Text = "ADD" Then
                obj.executeSQL("sp_mst_celebrity_regional", _
                               "rowid~celebrityid~celebname~placeofbirth~biography~languageid~updatedby~action", _
                               "int~int~nvarchar~nvarchar~nvarchar~int~varchar~char", _
                               "0~" & ddlCelebrity.SelectedValue & "~" & txtRegName.Text & "~" & txtPlaceOfBirth.Text & "~" & txtBioGraphy.Text.Trim & "~" & ddlLanguage.SelectedValue & "~" & User.Identity.Name & "~A", _
                                True, False)
            Else
                obj.executeSQL("sp_mst_celebrity_regional", _
                               "rowid~celebrityid~celebname~placeofbirth~biography~languageid~updatedby~action", _
                               "int~int~nvarchar~nvarchar~nvarchar~int~varchar~char", _
                               lbRowid.Text & "~" & ddlCelebrity.SelectedValue & "~" & txtRegName.Text & "~" & txtPlaceOfBirth.Text & "~" & txtBioGraphy.Text.Trim & "~" & ddlLanguage.SelectedValue & "~" & User.Identity.Name & "~U", _
                                True, False)
            End If
            bindGrid(True, False)
        Catch ex As Exception
            myErrorBox("Record Already exists of " & txtRegName.Text & " for language " & ddlLanguage.SelectedItem.Text)
        End Try
    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean, ByVal useSearch As Boolean)
        Dim sql As String
        sql = "select top 100 * from fn_celebrity_regional('" & ddlSearchLanguage.SelectedValue & "') where name like '%" & txtSearch1.Text & "%'"
        sqlDSCelebrityMaster.SelectCommand = sql
        sqlDSCelebrityMaster.SelectCommandType = SqlDataSourceCommandType.Text
        If paging = False Then
            grdCelebrity.PageIndex = 0
        End If
        grdCelebrity.SelectedIndex = -1
        grdCelebrity.DataBind()
        clearAll()
    End Sub

    Private Sub clearAll()
        lbRowid.Text = ""
        txtRegName.Text = ""
        txtPlaceOfBirth.Text = ""
        txtBioGraphy.Text = ""
        ddlCelebrity.Enabled = True
        ddlLanguage.Enabled = True
        grdCelebrity.SelectedIndex = -1
        btnUpdate.Text = "ADD"

    End Sub

    'Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btn.Click
    '    bindGrid(False, True)
    'End Sub

    Protected Sub grdCelebrity_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCelebrity.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbID As Label = TryCast(e.Row.FindControl("lbRowId"), Label)

        End If
    End Sub

    Protected Sub grdCelebrity_PageIndexChanged(sender As Object, e As EventArgs) Handles grdCelebrity.PageIndexChanged
        bindGrid(True, False)
    End Sub
    

    Protected Sub grdCelebrity_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCelebrity.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grdCelebrity.Rows(e.RowIndex).FindControl("lbRowId"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("sp_tmdb_celebrity", _
                           "rowid~ActionUser~ActionType", _
                           "Int~VarChar~Char", _
                           lbRowId.Text & "~" & User.Identity.Name & "~D", _
                            True, False)
            bindGrid(True, False)
        Catch ex As Exception
            Logger.LogError("CelebrityRegional", "grdCelebrity_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdCelebrity_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdCelebrity.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(True, False)
    End Sub
End Class