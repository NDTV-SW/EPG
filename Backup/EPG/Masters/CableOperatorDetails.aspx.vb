Imports System
Imports System.Data.SqlClient

Public Class CableOperatorDetails
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
            'CalendarExtender1.SelectedDate = txtDOB.Text.ToString
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
            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER") Or User.IsInRole("USER")) Then
                btnAddCableOperatorDetails.Visible = True
                btnCancel.Visible = True
                grdCableOperatorMaster.Columns(13).Visible = True
                grdCableOperatorMaster.Columns(14).Visible = True
            ElseIf (User.IsInRole("USER")) Then
                btnAddCableOperatorDetails.Visible = True
                btnCancel.Visible = True
                grdCableOperatormaster.Columns(13).Visible = False
                grdCableOperatormaster.Columns(14).Visible = False
            Else
                btnAddCableOperatorDetails.Visible = False
                btnCancel.Visible = False
                grdCableOperatormaster.Columns(13).Visible = False
                grdCableOperatormaster.Columns(14).Visible = False
            End If
            grdCableOperatormaster.Columns(14).Visible = True
        Catch ex As Exception
            Logger.LogError("CableOperator Details", "Page Load", ex.Message.ToString, User.Identity.Name)
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

    Private Sub exec_Proc(ByVal CableOperatorId As String, ByVal PointPersonName As String, ByVal Designation As String, ByVal DOB As Date, ByVal Email As String, ByVal Mobile As String, ByVal Mobile1 As String, ByVal Landline As String, ByVal Extension As String, ByVal RowID As String, ByVal action As String, ByVal PointPerson2Name As String, ByVal Email2 As String)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_dt_Operators", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OperatorID", SqlDbType.Int, 8)).Value = CableOperatorId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@PointPersonName", SqlDbType.NVarChar, 200)).Value = PointPersonName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@PointPerson2Name", SqlDbType.NVarChar, 200)).Value = PointPerson2Name.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Designation", SqlDbType.NVarChar, 200)).Value = Designation.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@DOB", SqlDbType.DateTime)).Value = DOB.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 200)).Value = Email.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Email2", SqlDbType.NVarChar, 200)).Value = Email2.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Mobile", SqlDbType.NVarChar, 200)).Value = Mobile.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Mobile1", SqlDbType.NVarChar, 200)).Value = Mobile1.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Landline", SqlDbType.NVarChar, 200)).Value = Landline.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Extension", SqlDbType.NVarChar, 200)).Value = Extension.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = RowID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.Char, 1)).Value = User.Identity.Name
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetCableOperatorDetails")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("CableOperator Details", action & " - exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            grdCableOperatorMaster.SelectedIndex = -1
            txtHiddenId.Text = "0"
            txtPointPersonName.Text = ""
            txtDesignation.Text = ""
            txtDOB.Text = ""
            txtEmail.Text = ""
            txtMobile.Text = ""
            txtMobile1.Text = ""
            txtLandline.Text = ""
            txtExtension.Text = ""
            btnAddCableOperatorDetails.Text = "Add"
        Catch ex As Exception
            Logger.LogError("CableOperator Details", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddCableOperatorDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCableOperatorDetails.Click
        Try
            'Dim dob As Date
            If txtDOB.Text = "" Then
                txtDOB.Text = "1/1/1990"
            End If

            If btnAddCableOperatorDetails.Text = "Add" Then
                Call exec_Proc(ddlCableOperatorName.SelectedValue.ToString.Trim, txtPointPersonName.Text.ToString.Trim, txtDesignation.Text.ToString.Trim, txtDOB.Text.ToString.Trim, txtEmail.Text.ToString.Trim.ToLower, txtMobile.Text.ToString.Trim, txtMobile1.Text.ToString.Trim, txtLandline.Text.ToString.Trim, txtExtension.Text.ToString.Trim, "0", "A", txtPointPerson2Name.Text.Trim, txtEmail2.Text.Trim.ToLower)
            ElseIf btnAddCableOperatorDetails.Text = "Update" Then
                Call exec_Proc(ddlCableOperatorName.SelectedValue.ToString.Trim, txtPointPersonName.Text.ToString.Trim, txtDesignation.Text.ToString.Trim, txtDOB.Text.ToString.Trim, txtEmail.Text.ToString.Trim.ToLower, txtMobile.Text.ToString.Trim, txtMobile1.Text.ToString.Trim, txtLandline.Text.ToString.Trim, txtExtension.Text.ToString.Trim, txtHiddenId.Text.ToString.Trim, "U", txtPointPerson2Name.Text.Trim, txtEmail2.Text.Trim.ToLower)
                grdCableOperatormaster.SelectedIndex = -1
            End If

            grdCableOperatormaster.DataBind()
            Gridview1.DataBind()
            txtHiddenId.Text = "0"
            txtPointPersonName.Text = ""
            txtPointPerson2Name.Text = ""
            txtDesignation.Text = ""
            txtDOB.Text = ""
            txtEmail.Text = ""
            txtEmail2.Text = ""
            txtMobile.Text = ""
            txtMobile1.Text = ""
            txtLandline.Text = ""
            txtExtension.Text = ""
            btnAddCableOperatorDetails.Text = "Add"
            grdCableOperatorMaster.DataBind()
        Catch ex As Exception
            Logger.LogError("CableOperator Details", "btnAddCableOperatorDetails_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdCableOperatormaster_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdCableOperatormaster.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub grdCableOperatormaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCableOperatormaster.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbCableOperatorId As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbOperatorId"), Label)
            Dim lbCableOperatorName As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbName"), Label)
            Dim lbPointPersonName As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbPointPersonName"), Label)
            Dim lbPointPerson2Name As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbPointPerson2Name"), Label)
            Dim lbDesignation As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbDesignation"), Label)
            Dim lbDOB As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbDOB"), Label)
            Dim lbEmail As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbEmail"), Label)
            Dim lbEmail2 As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbEmail2"), Label)
            Dim lbMobile As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbMobile"), Label)
            Dim lbMobile1 As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbMobile1"), Label)
            Dim lbLandline As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbLandline"), Label)
            Dim lbExtension As Label = DirectCast(grdCableOperatormaster.SelectedRow.FindControl("lbExtension"), Label)

            txtHiddenId.Text = Server.HtmlDecode(lbRowId.Text.Trim).Trim
            ddlCableOperatorName.SelectedValue = Server.HtmlDecode(lbCableOperatorId.Text.Trim).Trim
            txtPointPersonName.Text = Server.HtmlDecode(lbPointPersonName.Text.Trim).Trim
            txtPointPerson2Name.Text = Server.HtmlDecode(lbPointPerson2Name.Text.Trim).Trim
            txtDesignation.Text = Server.HtmlDecode(lbDesignation.Text.Trim).Trim
            txtDOB.Text = Server.HtmlDecode(lbDOB.Text.Trim).Trim
            txtEmail.Text = Server.HtmlDecode(lbEmail.Text.Trim).Trim
            txtEmail2.Text = Server.HtmlDecode(lbEmail2.Text.Trim).Trim
            txtMobile.Text = Server.HtmlDecode(lbMobile.Text.Trim).Trim
            txtMobile1.Text = Server.HtmlDecode(lbMobile1.Text.Trim).Trim
            txtLandline.Text = Server.HtmlDecode(lbLandline.Text.Trim).Trim
            txtExtension.Text = Server.HtmlDecode(lbExtension.Text.Trim).Trim
            btnAddCableOperatorDetails.Text = "Update"
        Catch ex As Exception
            Logger.LogError("CableOperator Details", "grdCableOperatormaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdCableOperatormaster_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCableOperatormaster.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grdCableOperatormaster.Rows(e.RowIndex).FindControl("lbRowId"), Label)
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_dt_Operators", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = lbRowId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "D"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetCableOperatorDetails")
            MyDataAdapter.Dispose()
            MyConnection.Close()

            Gridview1.DataBind()
        Catch ex As Exception
            Logger.LogError("CableOperator Details", "grdCableOperatormaster_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class