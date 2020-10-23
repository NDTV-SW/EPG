Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class FreshXMLsPending
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("FreshXMLsPending", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Protected Sub btnGenerateRequest_Click(sender As Object, e As EventArgs) Handles btnGenerateRequest.Click
        Dim row As GridViewRow
        For Each row In grdReport.Rows
            Dim chkChecked As CheckBox = TryCast(row.FindControl("chkChecked"), CheckBox)
            If chkChecked.Checked Then
                Dim dtXMLDateTime As DateTime = Convert.ToDateTime(row.Cells(4).Text).Date
                Dim dtEPGAvailableTill As DateTime = Convert.ToDateTime(row.Cells(6).Text).Date
                Dim obj As New clsExecute
                obj.executeSQL("insert into aud_autogeneratexml(channelid,requestedby,requestedat,xmlgenerated,fresh,startdate) values('" & row.Cells(2).Text & "','" & User.Identity.Name & "',dbo.getlocaldate(),'0','1','" & dtXMLDateTime.AddDays(1).Date & "')", False)
                chkChecked.Checked = False
            End If
        Next

    End Sub

    Protected Sub grdReport_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdReport.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbChannelId As Label = TryCast(e.Row.FindControl("lbChannelId"), Label)
            Dim chkChecked As CheckBox = TryCast(e.Row.FindControl("chkChecked"), CheckBox)
            Dim lbTotalCount As Label = TryCast(e.Row.FindControl("lbTotalCount"), Label)
            If Convert.ToInt16(lbTotalCount.Text) < 10 Then
                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL("sp_epg_validate_synopsis_v1", "channelid~language", "varchar~int", lbChannelId.Text & "~0", True, False)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("Progname").ToString.Length > 1 Then
                        chkChecked.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub
End Class