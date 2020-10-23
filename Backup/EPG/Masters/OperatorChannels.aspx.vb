Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class OperatorChannels
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Dim MyConnection1 As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Dim offAirSno As Integer = 1
    Dim onAirSno As Integer = 1
    Dim operatorTable As String ' = Request.QueryString("opTable")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
            Response.Redirect("~/Default.aspx")
        End If
        Dim operatorid As String = Request.QueryString("operatorid")

        Dim adp As New SqlDataAdapter("select name, mappingtable from mst_operators where operatorid='" & operatorid & "'", MyConnection)
        Dim dt As New DataTable
        adp.Fill(dt)
        If dt.Rows.Count > 0 Then
            operatorTable = dt.Rows(0).Item("mappingtable").ToString
            If Not (operatorid = "") Then
                lbOperatorName.Text = dt.Rows(0).Item("name").ToString
                Dim sqlString As String
                sqlString = "Select rowid,channelID,ServiceID,Onair from " & operatorTable & " where onair=1 and channelid is not null order by ChannelID"
                Dim adpOnAir As New SqlDataAdapter(sqlString, MyConnection)
                adpOnAir.SelectCommand.CommandType = CommandType.Text
                Dim dsOnAir As New DataSet
                adpOnAir.Fill(dsOnAir, "OperatorOnAirChannels")
                grdOnair.DataSource = dsOnAir
                grdOnair.DataBind()
                MyConnection.Close()
                MyConnection.Dispose()

                sqlString = "Select rowid,channelID,ServiceID,Onair from " & operatorTable & " where onair<>1 or channelid is null order by ChannelID"
                Dim adpOffAir As New SqlDataAdapter(sqlString, MyConnection1)
                adpOffAir.SelectCommand.CommandType = CommandType.Text
                Dim dsOffAir As New DataSet
                adpOffAir.Fill(dsOffAir, "OperatorOffAirChannels")
                grdOffAir.DataSource = dsOffAir
                grdOffAir.DataBind()
                MyConnection1.Close()
                MyConnection1.Dispose()
            Else
                Response.Redirect("epgOperators.aspx")
            End If
        End If
    End Sub

    Private Sub grdOnAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdOnair.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                e.Row.Cells(0).Text = onAirSno.ToString
                onAirSno = onAirSno + 1
            Catch
            End Try

        End If
    End Sub

    Private Sub grdOffAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdOffAir.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                e.Row.Cells(0).Text = offAirSno.ToString
                offAirSno = offAirSno + 1
            Catch
            End Try

        End If
    End Sub

    Protected Sub grdOffAir_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdOffAir.RowUpdating

    End Sub


    Protected Sub grdOnAir_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdOnair.RowUpdating
        Dim sql As String = "update " & operatorTable & " set ChannelId='" & txtChannelId.Text.Trim & "', ServiceId='" & txtServiceId.Text & "' where rowid='" & lbRowId.Text & "'"
        Dim adpOnAir As New SqlDataAdapter(sql, MyConnection)
        Dim dtOnAir As New DataTable
        adpOnAir.Fill(dtOnAir)
        grdOnair.DataBind()
    End Sub

    Protected Sub grdOffAir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdOffAir.SelectedIndexChanged
        txtChannelId.Text = TryCast(grdOffAir.SelectedRow.FindControl("lbChannelId"), Label).Text
        txtServiceId.Text = TryCast(grdOffAir.SelectedRow.FindControl("lbServiceId"), Label).Text
        lbRowId.Text = TryCast(grdOffAir.SelectedRow.FindControl("lbRowId"), Label).Text
    End Sub

    Protected Sub grdOnAir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdOnair.SelectedIndexChanged
        txtChannelId.Text = TryCast(grdOnair.SelectedRow.FindControl("lbChannelId"), Label).Text
        txtServiceId.Text = TryCast(grdOnair.SelectedRow.FindControl("lbServiceId"), Label).Text
        lbRowId.Text = TryCast(grdOnair.SelectedRow.FindControl("lbRowId"), Label).Text
    End Sub

    Private Sub clearAll()
        txtChannelId.Text = String.Empty
        txtServiceId.Text = String.Empty
        lbRowId.Text = String.Empty
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim MyConnection2 As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
        Dim sql As String = "update " & operatorTable & " set ChannelId='" & txtChannelId.Text.Trim & "', ServiceId='" & txtServiceId.Text & "' where rowid='" & lbRowId.Text & "'"
        Dim adp As New SqlDataAdapter(sql, MyConnection2)
        Dim dt As New DataTable
        adp.Fill(dt)
        grdOffAir.DataBind()
        grdOnair.DataBind()
        clearAll()
    End Sub
End Class