Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class StarMonitorChecks
    Inherits System.Web.UI.Page


    Protected Sub sqlDS1_Selecting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles sqlDS1.Selecting
        e.Command.CommandTimeout = 0
    End Sub
End Class