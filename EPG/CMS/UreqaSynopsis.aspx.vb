Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class UreqaSynopsis
    Inherits System.Web.UI.Page
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & messagestr & "');", True)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If ddlLanguage.SelectedValue = 0 Then
            myMessageBox("Please select Language!")

        Else
            Try
                Dim obj As New clsExecute
                Dim strSql As String
                If btnAdd.Text = "ADD" Then
                    strSql = "insert into mst_ureqasynopsis(progid,synopsis,languageid,lastupdate) values('" & ddlProgramme.SelectedValue & "',N'" & txtSynopsis.Text.Replace("'", "''") & "','" & ddlLanguage.SelectedValue & "',dbo.getlocaldate())"
                Else
                    strSql = "update mst_ureqasynopsis set synopsis=N'" & txtSynopsis.Text.Replace("'", "''") & "',lastupdate=dbo.getlocaldate() where progid='" & ddlProgramme.SelectedValue & "' and languageid='" & ddlLanguage.SelectedValue & "'"
                End If
                obj.executeSQL(strSql, False)
                clearAll()
            Catch ex As Exception
                myMessageBox("Record already exists!")
            End Try
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub
    Private Sub clearAll()
        txtSynopsis.Text = ""
        lbID.Text = ""
        grd.SelectedIndex = -1
        grd.DataBind()
        btnAdd.Text = "ADD"

    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        ddlChannel.SelectedValue = TryCast(grd.Rows(grd.SelectedIndex).FindControl("lbChannelId"), Label).Text
        ddlProgramme.DataBind()
        ddlProgramme.SelectedValue = TryCast(grd.Rows(grd.SelectedIndex).FindControl("lbprogid"), Label).Text
        ddlLanguage.SelectedValue = TryCast(grd.Rows(grd.SelectedIndex).FindControl("lbLanguageId"), Label).Text
        txtSynopsis.Text = TryCast(grd.Rows(grd.SelectedIndex).FindControl("lbSynopsis"), Label).Text
        btnAdd.Text = "UPDATE"
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Dim intProgid As Integer = TryCast(grd.Rows(e.RowIndex).FindControl("lbprogid"), Label).Text
        Dim intLanguageid As Integer = TryCast(grd.Rows(e.RowIndex).FindControl("lbLanguageId"), Label).Text

        Dim obj As New clsExecute

        obj.executeSQL("delete from mst_ureqasynopsis where progid='" & intProgid & "' and languageid='" & intLanguageid & "'", False)

    End Sub

    Protected Sub grd_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If Not e.ExceptionHandled Then
            e.ExceptionHandled = True
        End If
    End Sub
End Class