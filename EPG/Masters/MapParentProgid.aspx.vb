Imports System
Imports System.Data.SqlClient
Public Class MapParentProgid
    Inherits System.Web.UI.Page
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & errorstr & "');", True)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
          
        Catch ex As Exception
            Logger.LogError("MapParentProgid", "Page_Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            Dim row As GridViewRow
            For Each row In grdMap.Rows
                Dim lbProgid As Label = TryCast(row.FindControl("lbProgId"), Label)
                Dim lbParentProgId As Label = TryCast(row.FindControl("lbParentProgId"), Label)

                Dim ddlParentProgram As DropDownList = TryCast(row.FindControl("ddlParentProgram"), DropDownList)
                Dim lbParentEpisode As Label = TryCast(row.FindControl("lbParentEpisode"), Label)
                If lbParentProgId.Text = "" And ddlParentProgram.SelectedValue <> 0 Then
                    Dim obj As New clsExecute
                    obj.executeSQL("update mst_program set parent_progid='" & ddlParentProgram.SelectedValue & "' where progid='" & lbProgid.Text & "'", False)
                    Dim dt As DataTable = obj.executeSQL("select isnull(max(parent_episode),0) + 1 from mst_program where parent_progid = '" & ddlParentProgram.SelectedValue & "'", False)
                    lbParentEpisode.Text = dt.Rows(0)(0).ToString
                    obj.executeSQL("update mst_program set parent_episode='" & lbParentEpisode.Text & "' where progid='" & lbProgid.Text & "'", False)
                End If
            Next
            grdMap.DataBind()
            
        Catch ex As Exception
            Logger.LogError("Map Parent Program", "btnSave_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Private Sub grdMap_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMap.RowDeleting
        Try
            Dim lbProgId As Label = DirectCast(grdMap.Rows(e.RowIndex).FindControl("lbProgId"), Label)

            Dim obj As New clsExecute
            obj.executeSQL("update mst_program set parent_progid=null,parent_episode=null where progid='" & lbProgId.Text & "'", False)

            grdMap.DataBind()
        Catch ex As Exception
            Logger.LogError("Map Parent Program", "grdMap_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    
    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        grdMap.DataBind()
    End Sub


    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannelsForXML(prefixText, count)
        Return channels
    End Function



    Protected Sub grdMap_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMap.RowDataBound
        Dim obj As New clsExecute


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlParentProgram As DropDownList = TryCast(e.Row.FindControl("ddlParentProgram"), DropDownList)
            Dim dt As DataTable
            If IsNothing(HttpContext.Current.Session("MapParent_" & txtChannel.Text)) Then
                dt = obj.executeSQL("select '0' progid,' ---Select---' progname union select progid,progname from mst_program where progid = parent_progid and channelid='" & txtChannel.Text & "' union  select progid,progname from mst_program where seriesenabled=1 and channelid='" & txtChannel.Text & "' and progid in (select distinct progid from mst_epg where channelid='" & txtChannel.Text & "') order by 2", False)
                HttpContext.Current.Session("MapParent_" & txtChannel.Text) = dt

            Else
                dt = HttpContext.Current.Session("MapParent_" & txtChannel.Text)
            End If
            ddlParentProgram.DataTextField = "progname"
            ddlParentProgram.DataValueField = "progid"
            ddlParentProgram.DataSource = dt
            ddlParentProgram.DataBind()
            'DataSourceID="SqlDsProgram" DataTextField="" DataValueField=""


            Dim lbParentProgId As Label = TryCast(e.Row.FindControl("lbParentProgId"), Label)
            Dim lbSeriesEnabled As Label = TryCast(e.Row.FindControl("lbSeriesEnabled"), Label)

            If lbParentProgId.Text <> "" Or lbSeriesEnabled.Text = "False" Then
                ddlParentProgram.Visible = False
            End If
            If lbParentProgId.Text = "" Then
                e.Row.Cells(9).Visible = False
            Else
                e.Row.Cells(9).Visible = True
            End If


            If lbSeriesEnabled.Text = "False" Then
                e.Row.BackColor = Drawing.Color.RosyBrown
            End If

        End If
    End Sub

    Protected Sub btnAddParent_Click(sender As Object, e As EventArgs) Handles btnAddParent.Click
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select * from dbo.fn_ProgGenre('" & txtChannel.Text & "') order by 3,2", False)

            Dim obj1 As New clsUploadModules
            obj1.Insert_mstProg(txtChannel.Text, txtParentProgram.Text, dt.Rows(0)("GenreId").ToString, "U", 1, 1, 0, "NEW", False, "", User.Identity.Name, False)
            dt = obj.executeSQL("select progid from mst_program where channelid='" & txtChannel.Text & "' and progname='" & txtParentProgram.Text.Replace("'", "''") & "'", False)
            obj.executeSQL("update mst_program set parent_progid='" & dt.Rows(0)(0).ToString & "' where progid='" & dt.Rows(0)(0).ToString & "'", False)
            txtParentProgram.Text = ""
            HttpContext.Current.Session("MapParent_" & txtChannel.Text) = Nothing
            grdMap.DataBind()

        Catch ex As Exception
            myErrorBox("Program Already exits!")
            Logger.LogError("Map Parent Program", "btnAddParent_Click", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
End Class