Imports System
Imports System.Data.SqlClient
Public Class DeleteEPGData
    Inherits System.Web.UI.Page


    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & errorstr & "');", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("Admin", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    <System.Web.Script.Services.ScriptMethod(),
System.Web.Services.WebMethod()>
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        getEPGDates()
    End Sub
    Private Sub getEPGDates()
        Try
            Dim obj As New Logger
            lbEPGdates.Visible = True
            lbEPGdates.Text = obj.GetEpgDates(txtChannel.Text)
        Catch ex As Exception
            Logger.LogError("Admin", "getEPGDates", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim sql As String
        sql = "delete from mst_epg where channelid='" & txtChannel.Text & "' and  convert(varchar,progdate,112)>='" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & "' and convert(varchar,progdate,112)<='" & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & "'"
        Dim obj As New clsExecute
        obj.executeSQL(sql, False)
        getEPGDates()
        myMessageBox("EPG deleted for '" & txtChannel.Text & "' from " & Convert.ToDateTime(txtStartDate.Text).ToString("dd-MMM-yyyy") & " to " & Convert.ToDateTime(txtEndDate.Text).ToString("dd-MMM-yyyy") & ".")
    End Sub

    Protected Sub btnDelOldRowid_Click(sender As Object, e As EventArgs) Handles btnDelOldRowid.Click
        Dim obj As New clsExecute
        obj.executeSQL("update mst_epg set oldrowid=null where oldrowid='" & txtRowid.Text & "'", False)
        obj.executeSQL("delete from mst_epg_existing where (oldrowid='" & txtRowid.Text & "' or rowid='" & txtRowid.Text & "')", False)
        bindOldRowId()
        myMessageBox("Old rowid Set to null in MST_EPG. ---- Rowid and Old Rowid deleted from MST_EPG_EXISTING")
    End Sub

    Protected Sub txtRowid_TextChanged(sender As Object, e As EventArgs) Handles txtRowid.TextChanged
        bindOldRowId()
    End Sub
    Private Sub bindOldRowId()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select a.Rowid,a.ChannelId,b.Progname, convert(varchar,a.progdate,106) [Date],convert(varchar,a.progtime,108) [Time],a.Duration from mst_epg_existing a join mst_program b on a.progid=b.progid where oldrowid='" & txtRowid.Text & "' order by rowid desc", False)
        grdOldRowid.DataSource = dt
        grdOldRowid.DataBind()
    End Sub
End Class