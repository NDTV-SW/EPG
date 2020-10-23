Imports System
Imports System.Data.SqlClient

Public Class RatingsMaster
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
                btnAddRating.Visible = True
                btnCancel.Visible = True
                grdRatingmaster.Columns(4).Visible = True
                grdRatingmaster.Columns(5).Visible = True
            ElseIf (User.IsInRole("USER")) Then
                btnAddRating.Visible = True
                btnCancel.Visible = True
                grdRatingmaster.Columns(4).Visible = True
                grdRatingmaster.Columns(5).Visible = False
            Else
                btnAddRating.Visible = False
                btnCancel.Visible = False
                grdRatingmaster.Columns(4).Visible = False
                grdRatingmaster.Columns(5).Visible = False
                If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                    grdRatingmaster.Columns(4).Visible = False
                    btnAddRating.Visible = False
                    btnCancel.Visible = False
                End If
            End If
            grdRatingmaster.Columns(5).Visible = False
            ' sqlDSRatingMaster.DeleteParameters("ActionUser").DefaultValue = User.Identity.Name
        Catch ex As Exception
            Logger.LogError("Rating Master", "Page Load", ex.Message.ToString, User.Identity.Name)
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

    Private Sub exec_Proc(ByVal RowId As String, ByVal RatingId As String, ByVal RatingValue As String, ByVal RatingDesc As String, ByVal action As String)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_ParentalRating", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowId", SqlDbType.VarChar, 10)).Value = RowId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingID", SqlDbType.VarChar, 10)).Value = RatingId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingValue", SqlDbType.Int, 8)).Value = RatingValue.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingDesc", SqlDbType.VarChar, 100)).Value = RatingDesc.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetAddRatings")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            'Logger.LogError(ex.Message.ToString)
            Logger.LogError("Rating Master", action & " - Add/Update", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            grdRatingmaster.SelectedIndex = -1
            txtHiddenId.Text = "0"
            txtRatingId.Text = ""
            txtRatingValue.Text = ""
            txtRatingDesc.Text = ""
            btnAddRating.Text = "Add"
        Catch ex As Exception
            Logger.LogError("Rating Master", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddRating_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRating.Click
        Try
            If btnAddRating.Text = "Add" Then
                Call exec_Proc(0, txtRatingId.Text.ToString.Trim, txtRatingValue.Text.ToString.Trim, txtRatingDesc.Text.ToString.Trim, "A")
            ElseIf btnAddRating.Text = "Update" Then
                Call exec_Proc(txtHiddenId.Text.ToString.Trim, txtRatingId.Text.ToString.Trim, txtRatingValue.Text.ToString.Trim, txtRatingDesc.Text.ToString.Trim, "U")
                grdRatingmaster.SelectedIndex = -1
            End If
            grdRatingmaster.DataBind()
            txtHiddenId.Text = "0"
            txtRatingId.Text = ""
            txtRatingValue.Text = ""
            txtRatingDesc.Text = ""
            btnAddRating.Text = "Add"
        Catch ex As Exception
            Logger.LogError("Rating Master", "btnAddRating_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdRatingmaster_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdRatingmaster.RowDeleted
        If Not e.Exception Is Nothing Then
            ' Response.Write(e.Exception.Message.ToString)
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub grdRatingmaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRatingmaster.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdRatingmaster.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbRatingID As Label = DirectCast(grdRatingmaster.SelectedRow.FindControl("lbRatingID"), Label)
            Dim lbRatingValue As Label = DirectCast(grdRatingmaster.SelectedRow.FindControl("lbRatingValue"), Label)
            Dim lbRatingDesc As Label = DirectCast(grdRatingmaster.SelectedRow.FindControl("lbRatingDesc"), Label)

            txtHiddenId.Text = lbRowId.Text.Trim
            txtRatingId.Text = lbRatingID.Text.Trim
            txtRatingValue.Text = lbRatingValue.Text.Trim
            txtRatingDesc.Text = lbRatingDesc.Text.Trim
            btnAddRating.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Rating Master", "grdRatingmaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdRatingmaster_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdRatingmaster.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grdRatingmaster.Rows(e.RowIndex).FindControl("lbRowId"), Label)
            Dim lbRatingID As Label = DirectCast(grdRatingmaster.Rows(e.RowIndex).FindControl("lbRatingID"), Label)
            Dim lbRatingValue As Label = DirectCast(grdRatingmaster.Rows(e.RowIndex).FindControl("lbRatingValue"), Label)
            Dim lbRatingDesc As Label = DirectCast(grdRatingmaster.Rows(e.RowIndex).FindControl("lbRatingDesc"), Label)

            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_ParentalRating", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingID", SqlDbType.VarChar, 10)).Value = lbRatingID.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingValue", SqlDbType.Int, 8)).Value = lbRowId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingDesc", SqlDbType.VarChar, 100)).Value = lbRatingDesc.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = lbRowId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.VarChar, 1)).Value = "D"
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetRatings")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Rating Master", "grdRatingmaster_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class