Imports System
Imports System.Data.SqlClient
Public Class DummyEPGChannels
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


        Catch ex As Exception
            Logger.LogError("DummyEPGCopyChannels", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select b.genrename from  mst_channel a join mst_genre b on a.genreid=b.genreid where a.channelid='" & txtChannel.Text & "'", False)
        If dt.Rows.Count = 1 Then
            lbChannelGenre.Text = dt.Rows(0)(0).ToString
        Else
            lbChannelGenre.Text = "no record found"
        End If

    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Try
            Dim intID As Integer = grd.SelectedDataKey.Values(0)
            lbID.Text = intID
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select a.id,a.channelid,b.genrename,a.progname,a.progsynopsys,a.progduration,a.copylastweek,a.active from mst_insertdummyepg a join mst_channel c on a.channelid=c.channelid join mst_genre b  on c.genreid=b.genreid where a.id='" & intID & "'", False)
            If dt.Rows.Count > 0 Then
                txtChannel.Text = dt.Rows(0)("channelid").ToString
                lbChannelGenre.Text = dt.Rows(0)("genrename").ToString
                txtDummyProgramme.Text = dt.Rows(0)("progname").ToString
                txtDummySynopsis.Text = dt.Rows(0)("progsynopsys").ToString
                txtDummyDuration.Text = dt.Rows(0)("progduration").ToString
                chkCopyLastWeek.Checked = dt.Rows(0)("copylastweek").ToString
                chkActive.Checked = dt.Rows(0)("active").ToString
                btnAdd.Text = "Update"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub clearAll()
        txtChannel.Text = ""
        txtDummyProgramme.Text = ""
        txtDummySynopsis.Text = ""
        txtDummyDuration.Text = ""
        chkActive.Checked = False
        chkActive.Checked = True
        lbChannelGenre.Text = "."
        lbID.Text = ""
        btnAdd.Text = "Add"
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute
        Dim strSql As String
        If btnAdd.Text.ToUpper = "ADD" Then
            strSql = "insert into mst_insertdummyepg(channelid,progname,progsynopsys,progduration,copylastweek,active) values" & _
                "('" & txtChannel.Text & "','" & txtDummyProgramme.Text & "','" & txtDummySynopsis.Text & "','" & txtDummyDuration.Text & "','" & chkCopyLastWeek.Checked & "','" & chkActive.Checked & "')"
        Else
            strSql = "update mst_insertdummyepg set channelid='" & txtChannel.Text & "',progname='" & txtDummyProgramme.Text & "',progsynopsys='" & txtDummySynopsis.Text & _
                "',progduration='" & txtDummyDuration.Text & "',copylastweek='" & chkCopyLastWeek.Checked & "',active='" & chkActive.Checked & "' where id='" & lbID.Text & "'"
        End If
        obj.executeSQL(strSql, False)
        clearAll()

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clearAll()
    End Sub
End Class