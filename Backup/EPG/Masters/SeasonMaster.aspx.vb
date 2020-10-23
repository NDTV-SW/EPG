Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class SeasonMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
  

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
          
            If Page.IsPostBack = False Then

      
            End If
        Catch ex As Exception
            Logger.LogError("Season Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdSeasons_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSeasons.SelectedIndexChanged
        Try
            'ddlChannelName.SelectedValue = DirectCast(grdSeasons.SelectedRow.FindControl("lbChannelId"), Label).Text
            'ddlProgramme.DataBind()
            ddlProgramme.SelectedValue = DirectCast(grdSeasons.SelectedRow.FindControl("lbProgId"), Label).Text
            txtSeasonNo.Text = DirectCast(grdSeasons.SelectedRow.FindControl("lbSeasonNo"), Label).Text
            Try
                txtStartDate.Text = Convert.ToDateTime(DirectCast(grdSeasons.SelectedRow.FindControl("lbStartdate"), Label).Text).ToString("MM/dd/yyyy")
            Catch
            End Try
            Try
                txtEndDate.Text = Convert.ToDateTime(DirectCast(grdSeasons.SelectedRow.FindControl("lbEndDate"), Label).Text).ToString("MM/dd/yyyy")
            Catch
            End Try
            btnAdd.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Season Master", "grdSeasons_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub clearAll()
        'ddlChannelName.DataBind()
        'ddlProgramme.DataBind()
        grdSeasons.SelectedIndex = -1
        grdSeasons.DataBind()
        txtSeasonNo.Text = String.Empty
        txtStartDate.Text = String.Empty
        txtEndDate.Text = String.Empty
        txtStartDate.Text = "1/1/2015"
        txtEndDate.Text = "1/1/2019"
        btnAdd.Text = "Add"
    End Sub


    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If btnAdd.Text = "Add" Then
            Dim MyConnection As New SqlConnection(ConString)
            Dim MyDataAdapter As New SqlDataAdapter("insert into mst_program_seasons(ChannelId,Progid,SeasonNo,startDate,EndDate) values('" & ddlChannelName.SelectedValue & "','" & ddlProgramme.SelectedValue & "','" & txtSeasonNo.Text & "','" & txtStartDate.Text & "','" & txtEndDate.Text & "') ", MyConnection)
            Dim ds As New DataSet
            MyDataAdapter.Fill(ds)
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Else
            Dim MyConnection As New SqlConnection(ConString)
            Dim MyDataAdapter As New SqlDataAdapter("update mst_program_seasons set seasonno='" & txtSeasonNo.Text & "', startdate='" & txtStartDate.Text & "', enddate='" & txtEndDate.Text & "' where progid='" & ddlProgramme.SelectedValue & "'", MyConnection)
            Dim ds As New DataSet
            MyDataAdapter.Fill(ds)
            MyDataAdapter.Dispose()
            MyConnection.Close()
        End If
        grdSeasons.SelectedIndex = -1
        grdSeasons.DataBind()
        clearAll()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub grdSeasons_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdSeasons.RowDeleting
        Dim lbProgId As Label = DirectCast(grdSeasons.Rows(e.RowIndex).FindControl("lbProgId"), Label)
        Dim MyConnection As New SqlConnection(ConString)
        Dim MyDataAdapter As New SqlDataAdapter("delete from mst_program_seasons where progid='" & lbProgId.Text & "'", MyConnection)
        Dim ds As New DataSet
        MyDataAdapter.Fill(ds)
        MyDataAdapter.Dispose()
        MyConnection.Close()
        clearAll()
    End Sub

  
    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged
        clearAll()
    End Sub
End Class