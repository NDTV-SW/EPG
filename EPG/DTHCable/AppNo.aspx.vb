Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class AppNo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

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
            Dim Appno As Integer = Convert.ToInt16(DirectCast(e.Row.FindControl("lbAppno"), Label).Text)
            Select Case Appno
                Case 1
                    e.Row.BackColor = Drawing.Color.Wheat
                Case 2
                    e.Row.BackColor = Drawing.Color.White
                Case 3
                    e.Row.BackColor = Drawing.Color.Wheat
                Case 4
                    e.Row.BackColor = Drawing.Color.White
                Case 5
                    e.Row.BackColor = Drawing.Color.Wheat
                Case Else
                    e.Row.BackColor = Drawing.Color.White
            End Select
        End If
    End Sub

End Class