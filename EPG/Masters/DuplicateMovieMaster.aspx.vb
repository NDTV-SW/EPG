Public Class DuplicateMovieMaster
    Inherits System.Web.UI.Page

    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Dim flag As Integer = 0
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then

            End If
        Catch ex As Exception
            Logger.LogError("Duplicate Movie Master", "Page Load", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Please see error report.")
        End Try
    End Sub

#Region "Channel Master"


    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub


    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim obj As New clsExecute

            If btnAdd.Text = "Add" Then
                obj.executeSQL("insert into mst_duplicate_movies(movie_name,languageid,addedby,addedon) values('" & txtMovieName.Text & "','" & ddlLanguageid.SelectedValue & "','" & User.Identity.Name & "',dbo.getlocaldate())", False)
            ElseIf btnAdd.Text = "Update" Then
                obj.executeSQL("update mst_duplicate_movies set movie_name='" & txtMovieName.Text & "' where id='" & txtHiddenId.Text & "'", False)
            End If
            clearAll()

        Catch ex As Exception
            Logger.LogError("Duplicate Movie Master", "btnAdd_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.SelectedIndexChanged
        Try
            txtHiddenId.Text = DirectCast(grd.SelectedRow.FindControl("lbId"), Label).Text
            txtMovieName.Text = DirectCast(grd.SelectedRow.FindControl("lbMovieName"), Label).Text
            btnAdd.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Duplicate Movie Master", "grd_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region


    Private Sub clearAll()
        btnAdd.Text = "Add"
        txtHiddenId.Text = "0"
        txtMovieName.Text = ""
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grd.RowDeleting
        Dim lbRowId As Label = DirectCast(grd.Rows(e.RowIndex).FindControl("lbId"), Label)
        Dim obj As New clsExecute
        obj.executeSQL("delete from mst_duplicate_movies where id='" & lbRowId.Text & "'", False)
        grd.DataBind()
    End Sub
End Class