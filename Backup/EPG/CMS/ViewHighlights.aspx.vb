Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class ViewHighlights
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("Highlights", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Private Sub bindGrid(ByVal paging As Boolean)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select x.rowid,  x.channelid, progname, synopsis,starcast,istdate,isttime, genre, poster, vadate, vatime from mst_highlights x join dthcable_channelmapping y on x.channelid  = y.channelid  where cast(vadate + ' ' + vatime as datetime) > dbo.ConvertToUTC_Skandha (convert(varchar,dbo.GetLocalDate(),106) + ' ' + convert(varchar,dbo.GetLocalDate(),108),125, operatorchannelid)  And operatorid = 125", False)
        grdHighLights.DataSource = dt
        grdHighLights.DataBind()

        If paging = False Then
            grdHighLights.PageIndex = 0
        End If
        grdHighLights.SelectedIndex = -1
        grdHighLights.DataBind()
    End Sub

    Protected Sub grdHighLights_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdHighLights.PageIndexChanging
        grdHighLights.PageIndex = e.NewPageIndex
        bindGrid(True)
    End Sub

    Protected Sub grdHighLights_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdHighLights.Sorting
        bindGrid(True)
    End Sub
    Protected Sub grdHighLights_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdHighLights.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grdHighLights.Rows(e.RowIndex).FindControl("lbId"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("delete from mst_highlights where rowid='" & lbRowId.Text & "'", False)
            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("ViewHighLights", "grdMovieLogos_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdHighLights_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdHighLights.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub
End Class