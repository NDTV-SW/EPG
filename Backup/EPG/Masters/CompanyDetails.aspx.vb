Imports System
Imports System.Data.SqlClient

Public Class CompanyDetails
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
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
            bindGrid()
            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER") Or User.IsInRole("USER")) Then
                btnAddCompanyDetails.Visible = True
                btnCancel.Visible = True
                grdCompanymaster.Columns(13).Visible = True
                grdCompanymaster.Columns(14).Visible = True
                'ElseIf (User.IsInRole("USER")) Then
                '    btnAddCompanyDetails.Visible = True
                '    btnCancel.Visible = True
                '    grdCompanymaster.Columns(13).Visible = False
                '    grdCompanymaster.Columns(14).Visible = False
            Else
                btnAddCompanyDetails.Visible = False
                btnCancel.Visible = False
                grdCompanymaster.Columns(13).Visible = False
                grdCompanymaster.Columns(14).Visible = False
            End If
            grdCompanymaster.Columns(14).Visible = True

        Catch ex As Exception
            Logger.LogError("Company Details", "Page Load", ex.Message.ToString, User.Identity.Name)
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

    Private Sub exec_Proc(ByVal CompanyId As String, ByVal Channelid As String, ByVal PointPersonName As String, ByVal Designation As String, ByVal DOB As Date, ByVal Email As String, ByVal Mobile As String, ByVal Mobile1 As String, ByVal Landline As String, ByVal Extension As String, ByVal RowID As String, ByVal action As Char, ByVal PointPerson2Name As String, ByVal Email2 As String)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_dt_company", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.Int, 8)).Value = CompanyId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelId", SqlDbType.NVarChar)).Value = Channelid.ToString.Trim
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
            MyDataAdapter.Fill(DS, "GetCompanyDetails")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Company Details", action & " - exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            grdCompanymaster.SelectedIndex = -1
            txtHiddenId.Text = "0"
            txtPointPersonName.Text = ""
            txtDesignation.Text = ""
            txtDOB.Text = ""
            txtEmail.Text = ""
            txtMobile.Text = ""
            txtMobile1.Text = ""
            txtLandline.Text = ""
            txtExtension.Text = ""
            btnAddCompanyDetails.Text = "Add"
        Catch ex As Exception
            Logger.LogError("Company Details", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddCompanyDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCompanyDetails.Click
        Try
            'Dim dob As Date
            If txtDOB.Text = "" Then
                txtDOB.Text = "1/1/1990"
            End If
            If btnAddCompanyDetails.Text = "Add" Then
                Call exec_Proc(ddlCompanyName.SelectedValue.ToString.Trim, ddlChannel.SelectedValue, txtPointPersonName.Text.ToString.Trim, txtDesignation.Text.ToString.Trim, txtDOB.Text.ToString.Trim, txtEmail.Text.ToString.Trim.ToLower, txtMobile.Text.ToString.Trim, txtMobile1.Text.ToString.Trim, txtLandline.Text.ToString.Trim, txtExtension.Text.ToString.Trim, 0, "A", txtPointPerson2Name.Text.Trim, txtEmail2.Text.Trim.ToLower)
            ElseIf btnAddCompanyDetails.Text = "Update" Then
                Call exec_Proc(ddlCompanyName.SelectedValue.ToString.Trim, ddlChannel.SelectedValue, txtPointPersonName.Text.ToString.Trim, txtDesignation.Text.ToString.Trim, txtDOB.Text.ToString.Trim, txtEmail.Text.ToString.Trim.ToLower, txtMobile.Text.ToString.Trim, txtMobile1.Text.ToString.Trim, txtLandline.Text.ToString.Trim, txtExtension.Text.ToString.Trim, txtHiddenId.Text.ToString.Trim, "U", txtPointPerson2Name.Text.Trim, txtEmail2.Text.Trim.ToLower)
                grdCompanymaster.SelectedIndex = -1
            End If
            grdCompanymaster.DataBind()
            txtHiddenId.Text = "0"
            txtPointPersonName.Text = ""
            txtDesignation.Text = ""
            txtDOB.Text = ""
            txtEmail.Text = ""
            txtMobile.Text = ""
            txtMobile1.Text = ""
            txtLandline.Text = ""
            txtExtension.Text = ""
            btnAddCompanyDetails.Text = "Add"
            'GridView1.DataBind()
        Catch ex As Exception
            Logger.LogError("Company Details", "btnAddCompanyDetails_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdCompanymaster_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdCompanymaster.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub grdCompanymaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCompanymaster.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbCompanyId As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbCompanyId"), Label)
            Dim lbChannelId As Label
            Try
                lbChannelId = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbChannelId"), Label)
            Catch
                lbChannelId = Nothing
            End Try

            'Dim lbCompanyName As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbCompanyName"), Label)
            Dim lbPointPersonName As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbPointPersonName"), Label)
            Dim lbPointPerson2Name As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbPointPerson2Name"), Label)
            Dim lbDesignation As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbDesignation"), Label)
            Dim lbDOB As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbDOB"), Label)
            Dim lbEmail As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbEmail"), Label)
            Dim lbEmail2 As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbEmail2"), Label)
            Dim lbMobile As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbMobile"), Label)
            Dim lbMobile1 As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbMobile1"), Label)
            Dim lbLandline As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbLandline"), Label)
            Dim lbExtension As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbExtension"), Label)

            txtHiddenId.Text = Server.HtmlDecode(lbRowId.Text.Trim).Trim
            Try
                ddlChannel.SelectedValue = Server.HtmlDecode(lbChannelId.Text.Trim).Trim
            Catch
            End Try
            ddlCompanyName.DataBind()
            'ddlCompanyName.SelectedValue = Server.HtmlDecode(lbCompanyId.Text.Trim).Trim
            
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
            btnAddCompanyDetails.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Company Details", "grdCompanymaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdCompanymaster_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCompanymaster.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grdCompanymaster.Rows(e.RowIndex).FindControl("lbRowId"), Label)
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_dt_company", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = lbRowId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "D"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetCompanyDetails")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Company Details", "grdCompanymaster_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub bindGrid()
        sqlDSCompanyDetails.SelectCommand = "SELECT c.CompanyId,c.channelid,b.rowid,a.companyname, b.PointPersonName,b.PointPerson2Name, b.Designation, CONVERT(VARCHAR(8), b.DOB, 1) dob, b.Email,b.Email2, b.Mobile, b.Mobile1, b.Landline, b.Extension FROM mst_channel c join mst_Company a on c.CompanyId = a.CompanyId and c.channelid like '%" & txtSearch.Text & "%' join dt_Company b on c.channelid = b.ChannelId  order by 2"
        grdCompanymaster.DataBind()
    End Sub
End Class