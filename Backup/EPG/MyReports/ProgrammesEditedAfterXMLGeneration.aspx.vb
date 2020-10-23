Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ProgrammesEditedAfterXMLGeneration
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("ProgrammesEditedAfterXMLGeneration", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Protected Sub btnGenerateRequest_Click(sender As Object, e As EventArgs) Handles btnGenerateRequest.Click
        Dim row As GridViewRow
        For Each row In grdReport.Rows
            Dim chkChecked As CheckBox = TryCast(row.FindControl("chkChecked"), CheckBox)
            If chkChecked.Checked Then
                Dim obj As New clsExecute
                obj.executeSQL("insert into aud_autogeneratexml(channelid,requestedby,requestedat,xmlgenerated) values('" & row.Cells(2).Text & "','" & User.Identity.Name & "',dbo.getlocaldate(),'0')", False)
                chkChecked.Checked = False
            End If
        Next
    End Sub

    Protected Sub grdReport_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdReport.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbChannelId As Label = TryCast(e.Row.FindControl("lbChannelId"), Label)
            Dim chkChecked As CheckBox = TryCast(e.Row.FindControl("chkChecked"), CheckBox)
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("sp_epg_validate_synopsis_v1", "channelid~language", "varchar~int", lbChannelId.Text & "~0", True, False)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Progname").ToString.Length > 1 Then


                    chkChecked.Enabled = False
                End If
            End If
        End If
    End Sub
End Class