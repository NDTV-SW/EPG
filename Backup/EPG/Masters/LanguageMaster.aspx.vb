Imports System
Imports System.Data.SqlClient
Public Class LanguageMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Dim active As Boolean

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
            If (User.IsInRole("ADMIN")) Then
                btnAddLanguage.Visible = True
                btnCancel.Visible = True
                grdLanguagemaster.Columns(4).Visible = True
                grdLanguagemaster.Columns(5).Visible = True
            ElseIf (User.IsInRole("USER")) Then
                btnAddLanguage.Visible = True
                btnCancel.Visible = True
                grdLanguagemaster.Columns(4).Visible = True
                grdLanguagemaster.Columns(5).Visible = False
            Else
                btnAddLanguage.Visible = False
                btnCancel.Visible = False
                grdLanguagemaster.Columns(4).Visible = False
                grdLanguagemaster.Columns(5).Visible = False
                If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                    grdLanguagemaster.Columns(4).Visible = False
                    btnAddLanguage.Visible = False
                    btnCancel.Visible = False
                End If
            End If
            grdLanguagemaster.Columns(5).Visible = False
            'grdLanguagemaster.Columns(0).Visible = False
            'chkActive.OnClientClick = "return confirm('Are you sure you want to delete this record?');"
            'sqlDSLanguageMaster.DeleteParameters("ActionUser").DefaultValue = User.Identity.Name
        Catch ex As Exception
            Logger.LogError("Language Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddLanguage.Click
        Try
            If chkActive.Checked = True Then
                active = True
            Else
                active = False
            End If
            If btnAddLanguage.Text = "Add" Then
                Call exec_Proc(0, txtLanguageFullName.Text.ToString.Trim, txtLanguageShortName.Text.ToString.Trim, active, "A")
            ElseIf btnAddLanguage.Text = "Update" Then
                Call exec_Proc(txtHiddenId.Text.ToString.Trim, txtLanguageFullName.Text.ToString.Trim, txtLanguageShortName.Text.ToString.Trim, active, "U")
                grdLanguagemaster.SelectedIndex = -1
            End If
            grdLanguagemaster.DataBind()

            txtHiddenId.Text = "0"
            txtLanguageFullName.Text = ""
            txtLanguageShortName.Text = ""
            btnAddLanguage.Text = "Add"
            chkActive.Checked = True
        Catch ex As Exception
            Logger.LogError("Language Master", btnAddLanguage.Text & " btnAddLanguage_Click", ex.Message.ToString, User.Identity.Name)
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

    Private Sub exec_Proc(ByVal langid As String, ByVal FullName As String, ByVal ShortName As String, ByVal active As Boolean, ByVal action As String)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_language", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int)).Value = langid.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@FullName", SqlDbType.VarChar, 50)).Value = FullName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ShortName", SqlDbType.VarChar, 10)).Value = ShortName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@active", SqlDbType.Bit)).Value = IIf(chkActive.Checked, True, False)
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetLang")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Language Master", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdLanguagemaster_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLanguagemaster.RowDataBound
        'Select Case e.Row.RowType
        '    Case DataControlRowType.DataRow
        '        ' if it's an autogenerated delete-LinkButton: '             
        'Dim LnkBtnDelete As LinkButton = DirectCast(e.Row.Cells(4).Controls(0), LinkButton)
        'LnkBtnDelete.OnClientClick = "return confirm('Are you sure you want to delete this record?');"
        'End Select
    End Sub

    Private Sub grdLanguagemaster_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdLanguagemaster.RowDeleted
        If Not e.Exception Is Nothing Then
            'Response.Write(e.Exception.Message.ToString)
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub grdLanguagemaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLanguagemaster.SelectedIndexChanged
        Try
            Dim lbLanguageId As Label = DirectCast(grdLanguagemaster.SelectedRow.FindControl("lbLanguageId"), Label)
            Dim lbFullName As Label = DirectCast(grdLanguagemaster.SelectedRow.FindControl("lbFullName"), Label)
            Dim lbShortName As Label = DirectCast(grdLanguagemaster.SelectedRow.FindControl("lbShortName"), Label)
            Dim lbActive As Label = DirectCast(grdLanguagemaster.SelectedRow.FindControl("lbActive"), Label)

            txtHiddenId.Text = lbLanguageId.Text.Trim
            txtLanguageFullName.Text = lbFullName.Text.Trim
            txtLanguageShortName.Text = lbShortName.Text.Trim
            If lbActive.Text.Trim = "True" Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If
            btnAddLanguage.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Language Master", "grdLanguagemaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdLanguagemaster_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdLanguagemaster.RowDeleting
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_language", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            Dim lbLanguageId As Label = DirectCast(grdLanguagemaster.Rows(e.RowIndex).FindControl("lbLanguageId"), Label)
            Dim lbFullName As Label = DirectCast(grdLanguagemaster.Rows(e.RowIndex).FindControl("lbFullName"), Label)
            Dim lbShortName As Label = DirectCast(grdLanguagemaster.Rows(e.RowIndex).FindControl("lbShortName"), Label)
            Dim lbActive As Label = DirectCast(grdLanguagemaster.Rows(e.RowIndex).FindControl("lbActive"), Label)
            If lbActive.Text.Trim = "True" Then
                active = True
            Else
                active = False
            End If
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int)).Value = lbLanguageId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@FullName", SqlDbType.VarChar, 50)).Value = lbFullName.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ShortName", SqlDbType.VarChar, 10)).Value = lbShortName.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@active", SqlDbType.Bit)).Value = active
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "D"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetLang")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Language Master", "grdLanguagemaster_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub ClearAll()
        Try
            grdLanguagemaster.SelectedIndex = -1
            txtHiddenId.Text = "0"
            txtLanguageFullName.Text = ""
            txtLanguageShortName.Text = ""
            btnAddLanguage.Text = "Add"
        Catch ex As Exception
            Logger.LogError("Language Master", "ClearAll", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            ClearAll()
        Catch ex As Exception
            Logger.LogError("Language Master", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class