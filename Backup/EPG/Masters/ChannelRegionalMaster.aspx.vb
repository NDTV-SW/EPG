Imports System
Imports System.Data.SqlClient
Public Class ChannelRegionalMaster
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
            If Page.IsPostBack = False Then
                Dim adp As New SqlDataAdapter("SELECT LanguageId, FullName FROM mst_Language where active='1' ORDER BY FullName", ConString)
                Dim ds As New DataSet
                adp.Fill(ds)
                ddlLanguage.DataSource = ds
                ddlLanguage.DataTextField = "FullName"
                ddlLanguage.DataValueField = "LanguageId"
                ddlLanguage.DataBind()
                If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
                    btnAddRegionalName.Visible = True
                    btnCancel1.Visible = True
                    grdRegionalNames.Columns(6).Visible = True
                    grdRegionalNames.Columns(7).Visible = True
                ElseIf (User.IsInRole("USER")) Then
                    btnAddRegionalName.Visible = True
                    btnCancel1.Visible = True
                    grdRegionalNames.Columns(6).Visible = True
                    grdRegionalNames.Columns(7).Visible = False
                Else
                    btnAddRegionalName.Visible = False
                    btnCancel1.Visible = False
                    grdRegionalNames.Columns(6).Visible = False
                    grdRegionalNames.Columns(7).Visible = False
                    If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                        grdRegionalNames.Columns(6).Visible = True
                        btnAddRegionalName.Visible = True
                        btnCancel1.Visible = True
                    End If
                    checkUserType()
                End If
                grdRegionalNames.Columns(7).Visible = False
                GetCompanyName()
            End If
        Catch ex As Exception
            Logger.LogError("Channel Regional Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub checkUserType()
        Try
            If (User.IsInRole("ENGLISH")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "ENGLISH") Then
                        ddlLanguage.Items.RemoveAt(i)
                    End If
                Next
            ElseIf (User.IsInRole("HINDI")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "HINDI") Then
                        ddlLanguage.Items.RemoveAt(i)

                    End If
                Next
            ElseIf (User.IsInRole("MARATHI")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "MARATHI") Then
                        ddlLanguage.Items.RemoveAt(i)

                    End If
                Next
            ElseIf (User.IsInRole("TELUGU")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "TELUGU") Then
                        ddlLanguage.Items.RemoveAt(i)

                    End If
                Next
            ElseIf (User.IsInRole("TAMIL")) Then
                For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                    If Not (ddlLanguage.Items(i).Text.ToUpper = "TAMIL") Then
                        ddlLanguage.Items.RemoveAt(i)

                    End If
                Next
            End If
        Catch ex As Exception
            Logger.LogError("Channel Regional Master", "checkUserType", ex.Message.ToString, User.Identity.Name)
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

#Region "Regional Names"
    Private Sub ddlRegionalChannel_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegionalChannel.DataBound
        'Response.Write(ddlCompanyName.SelectedValue)
        grdRegionalNames.DataBind()
        GetCompanyName()
    End Sub

    Private Sub ddlRegionalChannel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegionalChannel.SelectedIndexChanged
        GetCompanyName()
    End Sub
    Private Sub GetCompanyName()
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        Dim cmd As New SqlCommand("select CompanyName from mst_company where companyid=(select CompanyId from mst_channel where channelid='" & ddlRegionalChannel.SelectedValue.Trim & "')", MyConnection)
        If MyConnection.State = ConnectionState.Closed Then
            MyConnection.Open()
        End If
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader

        If dr.HasRows Then
            dr.Read()
            lbCompanyName.Text = dr("CompanyName").ToString.Trim
        Else
            lbCompanyName.Text = "No Company Associated"
        End If
        dr.Close()
        MyConnection.Dispose()

    End Sub

    Private Sub exec_ProcRegional(ByVal ChannelId As String, ByVal RegionalName As String, ByVal LanguageId As Integer, ByVal ID As Integer, ByVal action As Char, ByVal synopsisneeded As Boolean)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_ChannelRegionalName", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelID", SqlDbType.VarChar, 100)).Value = ChannelId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RegionalName", SqlDbType.NVarChar, 400)).Value = RegionalName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = LanguageId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = ID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SynopsisNeeded", SqlDbType.Bit)).Value = synopsisneeded
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetChannelRegional")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Channel Regional Master", action & "-exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Duplicate Data !")
        End Try
    End Sub

    Private Sub btnCancel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel1.Click
        Try
            grdRegionalNames.SelectedIndex = -1
            txtHiddenId1.Text = "0"
            txtRegionalName.Text = ""
            btnAddRegionalName.Text = "Add"
        Catch ex As Exception
            Logger.LogError("Channel Regional Master", "btnCancel1_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddRegionalName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRegionalName.Click
        Try
            Dim synopsisneeded As Boolean
            If chkSynopsis.Checked Then
                synopsisneeded = True
            Else
                synopsisneeded = False
            End If
            If btnAddRegionalName.Text = "Add" Then
                Call exec_ProcRegional(ddlRegionalChannel.Text.ToString.Trim, txtRegionalName.Text.ToString.Trim, ddlLanguage.Text.ToString.Trim, 0, "A", synopsisneeded)
            ElseIf btnAddRegionalName.Text = "Update" Then
                Call exec_ProcRegional(ddlRegionalChannel.Text.ToString.Trim, txtRegionalName.Text.ToString.Trim, ddlLanguage.Text.ToString.Trim, txtHiddenId1.Text.ToString.Trim, "U", synopsisneeded)
                grdRegionalNames.SelectedIndex = -1
            End If
            grdRegionalNames.DataBind()
            txtHiddenId1.Text = "0"
            txtRegionalName.Text = ""
            btnAddRegionalName.Text = "Add"
        Catch ex As Exception
            Logger.LogError("Channel Regional Master", "btnAddRegionalName_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdRegionalNames_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdRegionalNames.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub grdRegionalNames_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRegionalNames.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbChannelId As Label = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbChannelId"), Label)
            Dim lbLanguageId As Label = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbLanguageId"), Label)
            Dim lbRegionalName As Label = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbRegionalName"), Label)
            Dim lbFullName As Label = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbFullName"), Label)
            Dim lbSynopsisNeeded As Label = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbSynopsisNeeded"), Label)

            txtHiddenId1.Text = lbRowId.Text.Trim
            'ddlRegionalChannel.Text = lbChannelId.Text.Trim
            ddlLanguage.Text = lbLanguageId.Text.Trim
            txtRegionalName.Text = lbRegionalName.Text.Trim

            If lbSynopsisNeeded.Text.Trim = "True" Then
                chkSynopsis.Checked = True
            Else
                chkSynopsis.Checked = False
            End If
            btnAddRegionalName.Text = "Update"
        Catch ex As Exception
            myErrorBox("You cannot update this record!")
            Logger.LogError("Channel Regional Master", "grdRegionalNames_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdRegionalNames_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRegionalNames.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                'Dim LnkBtnDeleteProgramMaster As ImageButton = DirectCast(e.Row.Cells(7).Controls(0), ImageButton)
                'LnkBtnDeleteProgramMaster.OnClientClick = "return confirm('Do you really want to delete this record ?');"
        End Select
    End Sub
    Private Sub grdRegionalNames_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdRegionalNames.RowDeleting
        Try
            Dim lbLanguageId As Label = DirectCast(grdRegionalNames.Rows(e.RowIndex).FindControl("lbLanguageId"), Label)
            Dim lbRowId As Label = DirectCast(grdRegionalNames.Rows(e.RowIndex).FindControl("lbRowId"), Label)

            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_ChannelRegionalName", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            'MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelID", SqlDbType.VarChar, 100)).Value = grdRegionalNames.Rows(e.RowIndex).Cells(1).Text.ToString
            'MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RegionalName", SqlDbType.NVarChar, 400)).Value = grdRegionalNames.Rows(e.RowIndex).Cells(3).Text.ToString
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = lbLanguageId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@rowID", SqlDbType.Int, 8)).Value = Convert.ToInt32(lbRowId.Text.Trim)
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "D"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            'MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SynopsisNeeded", SqlDbType.Bit)).Value = grdRegionalNames.Rows(e.RowIndex).Cells(5).Text.ToString
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetChannelRegional")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Channel Regional Master", "grdRegionalNames_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region



End Class