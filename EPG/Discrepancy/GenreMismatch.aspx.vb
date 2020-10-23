Imports System
Imports System.Data.SqlClient

Public Class GenreMismatch
    Inherits System.Web.UI.Page

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                If Page.IsPostBack = False Then
                    'Dim obj As New clsExecute
                    'Dim dt As DataTable = obj.executeSQL("select * from v_GenreMismatch where sourceG<>TargetG order by 2,5", False)
                    'grd.DataSource = dt
                    'grd.DataBind()
                End If
            Catch ex As Exception
                Logger.LogError("GenreMismatch", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub
    Protected Sub grd_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd.RowCommand
        Try
            Dim lbSProgid As Label = TryCast(grd.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbSProgid"), Label)
            Dim lbSourceG As Label = TryCast(grd.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbSourceG"), Label)
            Dim lbTProgid As Label = TryCast(grd.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbTProgid"), Label)
            Dim lbTargetG As Label = TryCast(grd.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbTargetG"), Label)
            Dim obj As New clsExecute
            Dim sql As String = ""
            If e.CommandName.ToLower = "targettosource" Then
                sql = "update mst_program set genreid='" & lbTargetG.Text & "' where progid='" & lbSProgid.Text & "'"
            End If
            If e.CommandName.ToLower = "sourcetotarget" Then
                sql = "update mst_program set genreid='" & lbSourceG.Text & "' where progid='" & lbTProgid.Text & "'"
            End If
            obj.executeSQL(sql, False)
            grd.DataBind()
        Catch ex As Exception
            Logger.LogError("GenreMismatch", "grdEPGTransactions_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


End Class