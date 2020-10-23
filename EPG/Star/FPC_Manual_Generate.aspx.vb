Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class FPC_Manual_Generate
    Inherits System.Web.UI.Page


    Protected Sub ddlClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClient.SelectedIndexChanged
        Dim obj As New clsExecute
        Try
            Dim dt As DataTable = obj.executeSQL("select frequency from fpc_distribution where id=" & ddlClient.SelectedValue, False)
            ddlfrequency.SelectedValue = dt.Rows(0)("frequency").ToString
            setDates()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlfrequency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlfrequency.SelectedIndexChanged
        setDates()
    End Sub

    Private Sub setDates()
        If ddlfrequency.SelectedValue = "" Then
            divStartDate.Visible = True
            divEndDate.Visible = True
        Else
            divStartDate.Visible = False
            divEndDate.Visible = False
        End If
    End Sub
End Class