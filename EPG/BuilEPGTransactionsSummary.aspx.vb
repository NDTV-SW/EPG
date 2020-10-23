Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class BuilEPGTransactionsSummary
    Inherits System.Web.UI.Page
    '    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                gridbind()
            End If
        Catch ex As Exception
            Logger.LogError("BuildEPGTransactions", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub gridbind()
        Dim sql As String
        If txtChannel.Text = "" Then
            If chkDTH.Checked Then
                sql = "select rowid,channelid,CONVERT(varchar,epgdate,106) epgdate1,epgdate,CONVERT(varchar,lastupdate,109) lastupdate1,lastupdate,isnull(synopsisChecked,0) synopsisChecked,updatedBy,CONVERT(varchar,updatedat,109) updatedat from mst_build_epg_transactions where(Convert(varchar, lastupdate, 112) = Convert(varchar, dbo.GetLocalDate(), 112)) and channelid in (select channelid from mst_channel where airtelftp=1) order by channelid asc, epgdate desc"
            Else
                sql = "select rowid,channelid,CONVERT(varchar,epgdate,106) epgdate1,epgdate,CONVERT(varchar,lastupdate,109) lastupdate1,lastupdate,isnull(synopsisChecked,0) synopsisChecked,updatedBy,CONVERT(varchar,updatedat,109) updatedat from mst_build_epg_transactions where(Convert(varchar, lastupdate, 112) = Convert(varchar, dbo.GetLocalDate(), 112)) order by channelid asc, epgdate desc"
            End If
        Else
            sql = "select top 50 rowid,channelid,CONVERT(varchar,epgdate,106) epgdate1,epgdate,CONVERT(varchar,lastupdate,109) lastupdate1,lastupdate,isnull(synopsisChecked,0) synopsisChecked,updatedBy,CONVERT(varchar,updatedat,109) updatedat from mst_build_epg_transactions where channelid='" & txtChannel.Text & "' order by lastupdate desc ,epgdate desc"
        End If
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL(sql, False)
        grdEPGTransactions.DataSource = dt
        grdEPGTransactions.DataBind()
    End Sub
    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        gridbind()
    End Sub
    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    Protected Sub chkDTH_CheckedChanged(sender As Object, e As EventArgs) Handles chkDTH.CheckedChanged
        txtChannel.Text = ""
        gridbind()
    End Sub

    Protected Sub grdEPGTransactions_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdEPGTransactions.RowCommand
        Try
            Dim lbRowid As Label = TryCast(grdEPGTransactions.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowid"), Label)
            Dim chkSynopsisChecked As CheckBox = TryCast(grdEPGTransactions.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("chkSynopsisChecked"), CheckBox)
            If e.CommandName.ToLower = "updatesynopsis" Then
                Dim obj As New clsExecute
                If chkSynopsisChecked.Checked Then
                    obj.executeSQL("update mst_build_epg_transactions set synopsischecked='" & chkSynopsisChecked.Checked & "', updatedby='" & User.Identity.Name & "',updatedat=dbo.getlocaldate() where rowid='" & lbRowid.Text & "'", False)
                Else
                    obj.executeSQL("update mst_build_epg_transactions set synopsischecked='" & chkSynopsisChecked.Checked & "', updatedby='',updatedat=null where rowid='" & lbRowid.Text & "'", False)
                End If

                gridbind()
            End If

        Catch ex As Exception
            Logger.LogError("BuildEPGTransactions", "grdEPGTransactions_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class