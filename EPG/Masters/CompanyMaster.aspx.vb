Imports System
Imports System.Data.SqlClient

Public Class CompanyMaster
    Inherits System.Web.UI.Page
    Dim active As Boolean
    Dim companyid As String
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString


    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
                btnAddCompany.Visible = True
                'btnCancel.Visible = True
                grdCompanymaster.Columns(5).Visible = True
                grdCompanymaster.Columns(6).Visible = True
            ElseIf (User.IsInRole("USER")) Then
                btnAddCompany.Visible = True
                'btnCancel.Visible = True
                grdCompanymaster.Columns(5).Visible = True
                grdCompanymaster.Columns(6).Visible = False
            Else
                btnAddCompany.Visible = False
                'btnCancel.Visible = False
                grdCompanymaster.Columns(5).Visible = False
                grdCompanymaster.Columns(6).Visible = False
                If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                    grdCompanymaster.Columns(5).Visible = False
                    grdCompanymaster.Columns(5).Visible = False
                    grdCompanymaster.Columns(6).Visible = False
                End If
            End If
            grdCompanymaster.Columns(6).Visible = False
            'grdCompanymaster.Columns(0).Visible = False
            'grdCompanymaster.Columns(1).Visible = False
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            If btnAddCompany.Text = "Add" Then
                GenerateCompId()
            End If
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

    Private Sub exec_Proc(ByVal RowId As String, ByVal compid As String, ByVal CompanyName As String, ByVal Address As String, ByVal PAN As String, ByVal active As Boolean, ByVal action As Char)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_company", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = RowId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@CompanyId", SqlDbType.Int, 8)).Value = compid
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@CompanyName", SqlDbType.VarChar, 200)).Value = CompanyName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Address", SqlDbType.VarChar, 200)).Value = Address.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@PAN", SqlDbType.VarChar, 200)).Value = PAN.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@active", SqlDbType.Bit)).Value = active
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetCompany")
            MyDataAdapter.Dispose()
            If btnAddCompany.Text = "Add" Then
                GenerateCompId()
            End If

            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Company Details", action & " - exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles btnCancel.Click
        Try
            grdCompanymaster.SelectedIndex = -1
            txtHiddenId.Text = "0"
            txtCompId.Text = companyid
            txtCompanyName.Text = ""
            txtCompanyAddress.Text = ""
            txtPanNumber.Text = ""
            btnAddCompany.Text = "Add"
            If btnAddCompany.Text = "Add" Then
                GenerateCompId()
            End If
        Catch ex As Exception
            Logger.LogError("Company Details", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddCompany_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCompany.Click
        Try
            If chkActive.Checked = True Then
                active = True
            Else
                active = False
            End If
            If btnAddCompany.Text = "Add" Then
                GenerateCompId()
            End If
            If btnAddCompany.Text = "Add" Then
                Call exec_Proc(0, companyid, txtCompanyName.Text.ToString.Trim, txtCompanyAddress.Text.ToString.Trim, txtPanNumber.Text.ToString.Trim, active, "A")
            ElseIf btnAddCompany.Text = "Update" Then
                Call exec_Proc(txtHiddenId.Text.ToString.Trim, txtCompId.Text, txtCompanyName.Text.ToString.Trim, txtCompanyAddress.Text.ToString.Trim, txtPanNumber.Text.ToString.Trim, active, "U")
                grdCompanymaster.SelectedIndex = -1
            End If

            grdCompanymaster.DataBind()
            txtHiddenId.Text = "0"
            txtCompanyName.Text = ""
            txtCompanyAddress.Text = ""
            txtPanNumber.Text = ""
            btnAddCompany.Text = "Add"
            txtCompId.ReadOnly = True
            If btnAddCompany.Text = "Add" Then
                GenerateCompId()
            End If
        Catch ex As Exception
            Logger.LogError("Company Details", "btnAddCompany_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdCompanymaster_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCompanymaster.RowDataBound
        'Select Case e.Row.RowType
        '    Case DataControlRowType.DataRow
        '        ' if it's an autogenerated delete-LinkButton: '             
        '        Dim ImgBtnDelete As ImageButton = DirectCast(e.Row.Cells(5).Controls(0), ImageButton)
        '        ImgBtnDelete.OnClientClick = "return confirm('Are you sure you want to delete this record?');"
        'End Select
    End Sub

    Private Sub grdCompanymaster_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdCompanymaster.RowDeleted
        If Not e.Exception Is Nothing Then
            ' myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
        GenerateCompId()
    End Sub

    Private Sub grdCompanymaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCompanymaster.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbCompanyId As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbCompanyId"), Label)
            Dim lbCompanyName As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbCompanyName"), Label)
            Dim lbAddress As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbAddress"), Label)
            Dim lbPAN As Label = DirectCast(grdCompanymaster.SelectedRow.FindControl("lbPAN"), Label)

            txtHiddenId.Text = lbRowId.Text.Trim
            txtCompId.Text = lbCompanyId.Text.Trim
            txtCompanyName.Text = lbCompanyName.Text.Trim
            txtCompanyAddress.Text = lbAddress.Text.Trim
            txtPanNumber.Text = Server.HtmlDecode(lbPAN.Text.Trim)
            btnAddCompany.Text = "Update"
            txtCompId.ReadOnly = True
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
            MyDataAdapter = New SqlDataAdapter("sp_mst_company", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@rowId", SqlDbType.Int)).Value = lbRowId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.VarChar, 1)).Value = "D"
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetComp")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Company Details", "grdCompanymaster_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub GenerateCompId()
        Try
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            If MyConnection.State = ConnectionState.Closed Then
                MyConnection.Open()
            End If
            Dim cmd As New SqlCommand("select max(IsNull(CompanyID,0)) + 1 from mst_company", MyConnection)
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            dr.Read()
            companyid = dr(0).ToString
            txtCompId.Text = companyid
            dr.Close()
        Catch ex As Exception
            Logger.LogError("Company Details", "GenerateCompId", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class