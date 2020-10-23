Imports System
Imports System.Data.SqlClient
Public Class RemoveDuplicateProgrammes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("RemoveDuplicateProgrammes", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim obj As New clsExecute
        obj.executeSQL("update mst_epg set progid='" & ddlDestination.SelectedValue & "' where progid='" & ddlSourceProgramme.SelectedValue & "'", False)
        obj.executeSQL("update mst_program set active=0 where progid='" & ddlSourceProgramme.SelectedValue & "'", False)
        ddlSourceProgramme.DataBind()
        ddlDestination.DataBind()
        grdInactive.DataBind()
    End Sub

    Protected Sub grdInactive_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdInactive.SelectedIndexChanged
        Dim lbprogid As Label = TryCast(grdInactive.SelectedRow.FindControl("lbprogid"), Label)
        Dim obj As New clsExecute
        obj.executeSQL("update mst_program set active=1 where progid=" & lbprogid.Text, False)
        ddlSourceProgramme.DataBind()
        ddlDestination.DataBind()
        grdInactive.SelectedIndex = -1
        grdInactive.DataBind()
    End Sub
End Class