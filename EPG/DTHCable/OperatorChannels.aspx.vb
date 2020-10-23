Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class DTHOperatorChannels
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ddlOperator.DataSource = sqlDSOperatorList
            ddlOperator.DataTextField = "Name"
            ddlOperator.DataValueField = "operatorid"
            ddlOperator.DataBind()
            ddlOperator.Items.Insert(0, New ListItem("Select Operator", "0"))

            ddlChannel.DataSource = sqlDSChannel
            ddlChannel.DataTextField = "channelid"
            ddlChannel.DataValueField = "channelid"
            ddlChannel.DataBind()
            ddlChannel.Items.Insert(0, New ListItem("Select Channel", "0"))
            bindGrid()
        End If
    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            UpdateAppPriority(DirectCast(GridView1.Rows(index).FindControl("lbOperatorID"), Label).Text, e.CommandName.ToLower)
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub UpdateAppPriority(ByVal vOperatorid As Integer, ByVal vCommandName As String)
        Dim sql As String

        If vCommandName = "priorityup" Then
            sql = "update mst_dthcableoperators set priority=priority+1 where operatorid='" & vOperatorid & "'"
        ElseIf vCommandName = "prioritydown" Then
            sql = "update mst_dthcableoperators set priority=priority-1 where operatorid='" & vOperatorid & "'"
        ElseIf vCommandName = "appup" Then
            sql = "update mst_dthcableoperators set appno=appno+1 where operatorid='" & vOperatorid & "'"
        ElseIf vCommandName = "appdown" Then
            sql = "update mst_dthcableoperators set appno=appno-1 where operatorid='" & vOperatorid & "'"
        Else
            sql = "select '1"
        End If

        Dim obj As New clsExecute
        obj.executeSQL(sql, False)
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbRowid As Label = TryCast(e.Row.FindControl("lbRowid"), Label)
            Dim hySubmit As HyperLink = TryCast(e.Row.FindControl("hySubmit"), HyperLink)
            Dim hySubmitChannel As HyperLink = TryCast(e.Row.FindControl("hySubmitChannel"), HyperLink)
            hySubmit.NavigateUrl = "javascript:BeginProcess('" & lbRowid.Text & "','" & e.Row.RowIndex & "')"
            hySubmitChannel.NavigateUrl = "javascript:BeginProcessChannel('" & lbRowid.Text & "','" & e.Row.RowIndex & "')"

        End If
    End Sub

    Protected Sub ddlOperatorParent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOperator.SelectedIndexChanged
        bindGrid()
    End Sub
    Private Sub clearAll()
        ddlChannel.SelectedValue = 0
        txtServiceId.Text = String.Empty
        txtOperatorChannel.Text = String.Empty
        txtFrequency.Text = String.Empty
        txtTSID.Text = String.Empty
        txtChannelno.Text = String.Empty
        txtTimeDiff.Text = String.Empty
        chkOnAir.Checked = True
        btnSave.Text = "Save"
        GridView1.SelectedIndex = -1
        bindGrid()
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged

        lbID.Text = DirectCast(GridView1.SelectedRow.FindControl("lbRowid"), Label).Text

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select RowID,OperatorID,ChannelID,ServiceID,OperatorChannelID,ChannelNo,TSID,OnAir,Frequency,epgtimediff from dthcable_channelmapping where rowid='" & lbID.Text & "'", False)

        Try
            ddlChannel.SelectedValue = dt.Rows(0)("channelid").ToString()
        Catch
        End Try

        txtServiceId.Text = dt.Rows(0)("serviceid").ToString()
        txtOperatorChannel.Text = dt.Rows(0)("OperatorChannelId").ToString()
        txtFrequency.Text = dt.Rows(0)("Frequency").ToString()
        txtTSID.Text = dt.Rows(0)("TSID").ToString()
        txtChannelno.Text = dt.Rows(0)("ChannelNO").ToString()
        txtTimeDiff.Text = dt.Rows(0)("epgtimediff").ToString()
        If dt.Rows(0)("onair").ToString() = "" Then
            chkOnAir.Checked = False
        Else
            chkOnAir.Checked = Convert.ToBoolean(dt.Rows(0)("onair").ToString())
        End If

        btnSave.Text = "Update"
    End Sub
    Private Sub bindGrid()
        GridView1.DataBind()
        'Dim sql As String = "select RowID,OperatorID,ChannelID,ServiceID,OperatorChannelID,ChannelNo,TSID,OnAir,Frequency from dthcable_channelmapping where operatorid=" & ddlOperator.SelectedValue & " order by onair desc, channelid"
        'Dim obj As New clsExecute
        'Dim dt As DataTable = obj.executeSQL(sql, False)
        'GridView1.DataSource = dt
        'GridView1.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim sql As String
        If btnSave.Text = "Update" Then
            sql = "update dthcable_channelmapping set channelid='" & ddlChannel.SelectedValue & "', serviceid='" & txtServiceId.Text & "', Operatorchannelid='" & txtOperatorChannel.Text & "'," _
                 & "channelNo='" & txtChannelno.Text & "', epgtimediff='" & txtTimeDiff.Text & "', TSID='" & txtTSID.Text & "', Frequency='" & txtFrequency.Text & "', OnAir='" & chkOnAir.Checked & "' where rowid=" & lbID.Text
        Else
            sql = "insert into dthcable_channelmapping(operatorid,channelid,serviceid,operatorchannelid,channelno,TSID,frequency,onair,epgtimediff) values" _
                & "('" & ddlOperator.SelectedValue & "','" & ddlChannel.SelectedValue & "','" & txtServiceId.Text & "','" & txtOperatorChannel.Text & "','" & txtChannelno.Text & "','" & txtTSID.Text & "','" & txtFrequency.Text & "','" & chkOnAir.Checked & "','" & IIf(txtTimeDiff.Text = "", "0", txtTimeDiff.Text) & "')"
        End If
        'update dthcable_channelmapping set channelno=null where operatorid='" & ddlOperator.SelectedValue & "' and channelno=0 
        Dim obj As New clsExecute
        obj.executeSQL(sql, False)
        obj.executeSQL("update dthcable_channelmapping set channelno=null where operatorid='" & ddlOperator.SelectedValue & "' and channelno=0 ", False)
        clearAll()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function



End Class