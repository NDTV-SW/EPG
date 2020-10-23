Imports System
Imports System.Data.SqlClient
Public Class RegProgramMaster
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
                    btnAddRegionalName.Visible = True
                    btnCancel1.Visible = True
                    grdRegionalNames.Columns(6).Visible = True
                    grdRegionalNames.Columns(7).Visible = True

                ElseIf (User.IsInRole("USER")) Then
                    btnAddRegionalName.Visible = True
                    btnCancel1.Visible = True
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
                        checkUserType()
                    End If
                End If
                grdRegionalNames.Columns(7).Visible = False
            End If
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", "Page Load", ex.Message.ToString, User.Identity.Name)
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
            End If
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", "CheckUsertype", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub exec_Proc(ByVal ProgId As Integer, ByVal ChannelId As String, ByVal ProgName As String, ByVal GenreID As Integer, ByVal SubGenreID As Integer, ByVal RatingID As String, ByVal action As String, ByVal seriesenabled As Boolean, ByVal active As Boolean)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_program", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = ProgId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelId", SqlDbType.NVarChar, 50)).Value = ChannelId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = ProgName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreID", SqlDbType.Int, 8)).Value = GenreID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SubGenreID", SqlDbType.Int, 8)).Value = SubGenreID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingID", SqlDbType.VarChar, 10)).Value = RatingID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Seriesenabled", SqlDbType.Bit)).Value = seriesenabled.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@active", SqlDbType.Bit)).Value = active.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "ProgramMaster")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Sub clearRegionalData()
        Try
            txtHiddenId1.Text = "0"
            txtRegionalName.Text = ""
            txtRegionalDescription.Text = ""
            ddlLanguage.SelectedIndex = -1
            btnAddRegionalName.Text = "Add"
            grdRegionalNames.SelectedIndex = -1
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", "clearRegionalData", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdRegionalNames_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRegionalNames.SelectedIndexChanged
        Try
            btnAddRegionalName.Text = "Add"
            Dim lbRowid As Label, lbProgId As Label, lbLanguageId As Label, lbProgName As Label, lbSynopsis As Label, lbFullName As Label
            lbRowid = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbRowid"), Label)
            lbProgId = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbProgId"), Label)
            lbLanguageId = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbLanguageId"), Label)
            lbProgName = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbProgName"), Label)
            lbSynopsis = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbSynopsis"), Label)
            lbFullName = DirectCast(grdRegionalNames.SelectedRow.FindControl("lbFullName"), Label)
            Dim item As ListItem = ddlLanguage.Items.FindByValue(Server.HtmlDecode(lbLanguageId.Text.Trim))
            If item Is Nothing Then
                myErrorBox("You cannot Update this record")
                txtRegionalName.Text = ""
                txtRegionalDescription.Text = ""
                Exit Sub
                'ddlLanguage.SelectedValue = Server.HtmlDecode(grdRegionalNames.SelectedRow.Cells(1).Text.ToString)
            End If
            If item IsNot Nothing Then
                'myErrorBox("You cannot Update this record")
                'ddlLanguage.SelectedValue = Server.HtmlDecode(grdRegionalNames.SelectedRow.Cells(1).Text.ToString)
            End If
            txtHiddenId1.Text = Server.HtmlDecode(lbRowid.Text.Trim)
            txtRegionalName.Text = Server.HtmlDecode(lbProgName.Text.Trim)
            ddlLanguage.Text = Server.HtmlDecode(lbLanguageId.Text.Trim)
            txtRegionalDescription.Text = Server.HtmlDecode(lbSynopsis.Text.Trim)
            btnAddRegionalName.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", "grdRegionalNames_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
            myErrorBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnCancel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel1.Click
        Try
            clearRegionalData()
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", "btnCancel1_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddRegionalName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRegionalName.Click
        Try
            If btnAddRegionalName.Text = "Add" Then
                Call exec_ProcRegional(ddlRegionalProgram.Text.ToString.Trim, txtRegionalName.Text.ToString.Trim, txtRegionalDescription.Text.ToString.Trim, ddlLanguage.Text.ToString.Trim, 0, "A", IIf(txtEpisodeNo.Text.Trim = "", 0, txtEpisodeNo.Text.Trim))
            ElseIf btnAddRegionalName.Text = "Update" Then
                Call exec_ProcRegional(ddlRegionalProgram.Text.ToString.Trim, txtRegionalName.Text.ToString.Trim, txtRegionalDescription.Text.ToString.Trim, ddlLanguage.Text.ToString.Trim, txtHiddenId1.Text.ToString.Trim, "U", IIf(txtEpisodeNo.Text.Trim = "", 0, txtEpisodeNo.Text.Trim))
            End If
            grdRegionalNames.DataBind()
            clearRegionalData()
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", btnAddRegionalName.Text & " btnAddRegionalName_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub exec_ProcRegional(ByVal ProgId As Integer, ByVal ProgName As String, ByVal synopsis As String, ByVal LanguageID As Integer, ByVal ID As Integer, ByVal action As String, ByVal EpisodeNo As Integer)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_ProgramRegional", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = ProgId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = ProgName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = synopsis.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@EpisodeNo", SqlDbType.Int)).Value = EpisodeNo
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = LanguageID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = ID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetRegionalProgName")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", action & " exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdRegionalNames_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdRegionalNames.RowDeleting
        Try
            Dim lbRowid As Label = DirectCast(grdRegionalNames.Rows(e.RowIndex).FindControl("lbRowid"), Label)
            Dim lbProgId As Label = DirectCast(grdRegionalNames.Rows(e.RowIndex).FindControl("lbProgId"), Label)
            Dim lbLanguageId As Label = DirectCast(grdRegionalNames.Rows(e.RowIndex).FindControl("lbLanguageId"), Label)
            Dim lbProgName As Label = DirectCast(grdRegionalNames.Rows(e.RowIndex).FindControl("lbProgName"), Label)
            Dim lbSynopsis As Label = DirectCast(grdRegionalNames.Rows(e.RowIndex).FindControl("lbSynopsis"), Label)
            Dim lbFullName As Label = DirectCast(grdRegionalNames.Rows(e.RowIndex).FindControl("lbFullName"), Label)
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_ProgramRegional", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = lbProgId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = lbProgName.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = lbSynopsis.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = lbLanguageId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = lbRowid.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "D"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetCompany")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Regional Programmed Master", "grdRegionalNames_RowDeleting", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub grdRegionalNames_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdRegionalNames.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub
    Private Sub gr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegionalProgram.SelectedIndexChanged
        clearRegionalData()
    End Sub
End Class