Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class MarkAsTeleshopping
    Inherits System.Web.UI.Page
  

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

         
        Catch ex As Exception
            Logger.LogError("MarkAsTeleshopping", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnMark_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMark.Click
        Try
            Dim obj As New clsExecute
            obj.executeSQL("insert into mst_exclude_programs(progname) values('" & ddlProgram.SelectedItem.Text & "')", False)
            grd.SelectedIndex = -1
            grd.DataBind()
        Catch ex As Exception
            Logger.LogError("MarkAsTeleshopping", "btnMark_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Dim rowid As Integer = Convert.ToInt32(DirectCast(grd.SelectedRow.FindControl("lbRowId"), Label).Text)
        Dim obj As New clsExecute
        obj.executeSQL("delete from mst_exclude_programs where rowid='" & rowid & "'", False)
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub
End Class
