Imports System
Imports System.Data.SqlClient

Public Class SubGenreMaster
    Inherits System.Web.UI.Page

    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString


    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim mu As MembershipUser = Membership.GetUser(User.Identity.Name)
            Membership.UpdateUser(mu)
            If mu.Comment = "Need Change Password1" Then
                'Response.Redirect("~/Account/ChangePassword.aspx")
                mu.Comment = "0"
            End If
            Dim pwDateExpire As Integer
            pwDateExpire = DateDiff(DateInterval.Day, mu.LastPasswordChangedDate, Date.Now)
            If pwDateExpire > 30 Then
                'Response.Redirect("~/Account/ChangePassword.aspx")
            End If
            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
                btnAddSubGenre.Visible = True
                btnCancel.Visible = True
                grdSubGenreMaster.Columns(4).Visible = True
                grdSubGenreMaster.Columns(5).Visible = True
            ElseIf (User.IsInRole("USER")) Then
                btnAddSubGenre.Visible = True
                btnCancel.Visible = True
                grdSubGenreMaster.Columns(4).Visible = True
                grdSubGenreMaster.Columns(5).Visible = False
            Else
                btnAddSubGenre.Visible = False
                btnCancel.Visible = False
                grdSubGenreMaster.Columns(4).Visible = False
                grdSubGenreMaster.Columns(5).Visible = False
                If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                    grdSubGenreMaster.Columns(4).Visible = False
                    btnAddSubGenre.Visible = False
                    btnCancel.Visible = False
                End If
            End If
            grdSubGenreMaster.Columns(5).Visible = False
        Catch ex As Exception
            Logger.LogError("Sub Genre Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Function GetData(ByVal StrSql As String) As DataSet
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        Dim dbCommand As New SqlCommand
        dbCommand.CommandText = StrSql.ToString
        dbCommand.Connection = MyConnection
        Dim dataAdapter As New SqlDataAdapter
        dataAdapter.SelectCommand = dbCommand
        Dim ds As DataSet
        ds = New DataSet
        dataAdapter.Fill(ds)
        Return ds
        dataAdapter.Dispose()
        MyConnection.Dispose()
    End Function

    Private Sub exec_Proc(ByVal SubGenreId As Integer, ByVal SubGenreName As String, ByVal GenreId As Integer, ByVal action As Char)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_subGenre", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreId", SqlDbType.Int, 8)).Value = GenreId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SubGenreName", SqlDbType.NVarChar, 400)).Value = SubGenreName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SubGenreId", SqlDbType.Int, 8)).Value = SubGenreId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.NVarChar, 50)).Value = User.Identity.Name
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetSubGenre")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Sub Genre Master", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        grdSubGenreMaster.SelectedIndex = -1
        txtHiddenId.Text = "0"
        txtSubGenreName.Text = ""
        btnAddSubGenre.Text = "Add"
    End Sub

    Private Sub btnAddSubGenre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSubGenre.Click
        Try
            If btnAddSubGenre.Text = "Add" Then
                Call exec_Proc(0, txtSubGenreName.Text.ToString.Trim, ddlGenreName.Text.ToString.Trim, "A")
            ElseIf btnAddSubGenre.Text = "Update" Then
                Call exec_Proc(txtHiddenId.Text.ToString.Trim, txtSubGenreName.Text.ToString.Trim, ddlGenreName.Text.ToString.Trim, "U")
                grdSubGenreMaster.SelectedIndex = -1
            End If
            clearAll()
        Catch ex As Exception
            Logger.LogError("Sub Genre Master", btnAddSubGenre.Text & " btnAddSubGenre_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub clearAll()
        txtHiddenId.Text = "0"
        txtSubGenreName.Text = ""
        btnAddSubGenre.Text = "Add"
        grdSubGenreMaster.DataBind()
    End Sub
    Private Sub grdSubGenreMaster_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdSubGenreMaster.RowDeleted
        If Not e.Exception Is Nothing Then
            ' myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub grdSubGenreMaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSubGenreMaster.SelectedIndexChanged
        Try
            Dim lbSubGenreId As Label = DirectCast(grdSubGenreMaster.SelectedRow.FindControl("lbSubGenreId"), Label)
            Dim lbGenreId As Label = DirectCast(grdSubGenreMaster.SelectedRow.FindControl("lbGenreId"), Label)
            Dim lbSubGenreName As Label = DirectCast(grdSubGenreMaster.SelectedRow.FindControl("lbSubGenreName"), Label)
            txtHiddenId.Text = lbSubGenreId.Text
            ddlGenreName.SelectedValue = lbGenreId.Text
            txtSubGenreName.Text = lbSubGenreName.Text
            btnAddSubGenre.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Sub Genre Master", "grdSubGenreMaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

   
End Class