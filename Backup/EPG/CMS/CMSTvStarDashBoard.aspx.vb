Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSTvStarDashBoard
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then

                
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("TV Star Mapping", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    
    Protected Sub bindGrid(ByVal paging As Boolean)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("SELECT * FROM v_tvStars order by 6,5", False)
        grdTvStarDashboard.DataSource = dt
        grdTvStarDashboard.DataBind()

    End Sub

    Private Sub clearAll()
        
    End Sub

    Dim intSno As Integer
    Protected Sub grdTvStarDashboard_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTvStarDashboard.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
          
            Dim lbProfileId As Label = TryCast(e.Row.FindControl("lbProfileId"), Label)
            Dim lbProgId As Label = TryCast(e.Row.FindControl("lbProgId"), Label)

            Dim lbEngProfileName As Label = TryCast(e.Row.FindControl("lbEngProfileName"), Label)
            Dim lbHinProfileName As Label = TryCast(e.Row.FindControl("lbHinProfileName"), Label)
            Dim lbTamProfileName As Label = TryCast(e.Row.FindControl("lbTamProfileName"), Label)
            Dim lbTelProfileName As Label = TryCast(e.Row.FindControl("lbTelProfileName"), Label)
            Dim lbMarProfileName As Label = TryCast(e.Row.FindControl("lbMarProfileName"), Label)

            Dim hyEngEdit As HyperLink = TryCast(e.Row.FindControl("hyEngEdit"), HyperLink)
            hyEngEdit.NavigateUrl = "javascript:openWin('" & lbProfileId.Text & "','" & lbProgId.Text & "','1','" & IIf(lbEngProfileName.Text = "-", "A", "U") & "','MainContent_grdTvStarDashboard_hyEngEdit_" & e.Row.RowIndex & "')"
            Dim hyHinEdit As HyperLink = TryCast(e.Row.FindControl("hyHinEdit"), HyperLink)
            hyHinEdit.NavigateUrl = "javascript:openWin('" & lbProfileId.Text & "','" & lbProgId.Text & "','2','" & IIf(lbHinProfileName.Text = "-", "A", "U") & "','MainContent_grdTvStarDashboard_hyHinEdit_" & e.Row.RowIndex & "')"
            Dim hyTamEdit As HyperLink = TryCast(e.Row.FindControl("hyTamEdit"), HyperLink)
            hyTamEdit.NavigateUrl = "javascript:openWin('" & lbProfileId.Text & "','" & lbProgId.Text & "','7','" & IIf(lbTamProfileName.Text = "-", "A", "U") & "','MainContent_grdTvStarDashboard_hyTamEdit_" & e.Row.RowIndex & "')"
            Dim hyTelEdit As HyperLink = TryCast(e.Row.FindControl("hyTelEdit"), HyperLink)
            hyTelEdit.NavigateUrl = "javascript:openWin('" & lbProfileId.Text & "','" & lbProgId.Text & "','8','" & IIf(lbTelProfileName.Text = "-", "A", "U") & "','MainContent_grdTvStarDashboard_hyTelEdit_" & e.Row.RowIndex & "')"
            Dim hyMarEdit As HyperLink = TryCast(e.Row.FindControl("hyMarEdit"), HyperLink)
            hyMarEdit.NavigateUrl = "javascript:openWin('" & lbProfileId.Text & "','" & lbProgId.Text & "','4','" & IIf(lbMarProfileName.Text = "-", "A", "U") & "','MainContent_grdTvStarDashboard_hyMarEdit_" & e.Row.RowIndex & "')"

            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub



    Protected Sub grdTvStarDashboard_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdTvStarDashboard.Sorting
        bindGrid(True)
    End Sub

End Class