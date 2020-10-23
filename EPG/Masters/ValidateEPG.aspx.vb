Imports System
Imports System.Data.SqlClient

Public Class ValidateEPG
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
           
            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER") Or User.IsInRole("USER")) Then
                btnAddSubGenre.Visible = True
                btnCancel.Visible = True
                '    grdValidateEPG.Columns(4).Visible = True
                '   grdValidateEPG.Columns(5).Visible = True
            Else
                btnAddSubGenre.Visible = False
                btnCancel.Visible = False
                'grdValidateEPG.Columns(4).Visible = False
                'grdValidateEPG.Columns(5).Visible = False
                If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                    grdValidateEPG.Columns(4).Visible = True
                    btnAddSubGenre.Visible = True
                    btnCancel.Visible = True
                End If
            End If
            If Page.IsPostBack = False Then
                Dim MyConnection As New SqlConnection
                MyConnection = New SqlConnection(ConString)
                If MyConnection.State = ConnectionState.Closed Then
                    MyConnection.Open()
                End If
                Dim strChannel As String
                strChannel = ddlChannelName.SelectedValue.ToCharArray
                Dim cmd As New SqlCommand("SELECT CONVERT(varchar, Min(a.Progdate),106) as MinDate, (Select Right(CONVERT(varchar,MIN(b.Progtime),100),7) from mst_epg b where progdate=CONVERT(varchar, Min(a.Progdate),106) and  ChannelId='" & strChannel & "') as MinTime, CONVERT(varchar, MAX(a.Progdate),106) as MaxDate, (Select Right(CONVERT(varchar,MAX(b.Progtime),100),7) from mst_epg b where progdate=CONVERT(varchar, MAX(a.Progdate),106) and ChannelId='" & strChannel & "') as MaxTime from mst_epg a where ChannelId='" & strChannel & "'", MyConnection)
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    lbEPGExists.Visible = True
                    lbEPGExists.Text = "EPG Exists from " & dr("MinDate").ToString.Trim & " " & dr("MinTime").ToString.Trim & " to " & dr("MaxDate").ToString.Trim & " " & dr("MaxTime").ToString.Trim & "."
                    If dr("MinDate").ToString.Trim = "" And dr("MinTime").ToString.Trim = "" And dr("MaxDate").ToString.Trim = "" And dr("MaxTime").ToString.Trim = "" Then
                        lbEPGExists.Text = "No Data Avaialble in EPG for these Dates."
                    End If

                Else
                    lbEPGExists.Visible = True
                    lbEPGExists.Text = "No Data Avaialble in EPG for these Dates."
                End If
                dr.Close()
            End If
            CheckgrdValidateEPG()
        Catch ex As Exception
            Logger.LogError("Validate EPG", "Page Load", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Validate EPG", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try

        Catch ex As Exception
            Logger.LogError("Validate EPG", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddSubGenre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSubGenre.Click
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_epg_validate_epgDates", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelId", SqlDbType.VarChar, 50)).Value = ddlChannelName.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Startdate", SqlDbType.Date)).Value = txtStartDate.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@EndDate", SqlDbType.Date)).Value = txtEndDate.Text.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetValidateEpg")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Validate EPG", "btnAddSubGenre_Click", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Private Sub grdValidateEPG_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdValidateEPG.RowDeleted
        If Not e.Exception Is Nothing Then
            ' myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub grdValidateEPG_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdValidateEPG.SelectedIndexChanged
        
    End Sub

    Private Sub grdValidateEPG_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdValidateEPG.RowDeleting
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_subgenre", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SubGenreID", SqlDbType.Int)).Value = grdValidateEPG.Rows(e.RowIndex).Cells(1).Text.ToString
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int)).Value = grdValidateEPG.Rows(e.RowIndex).Cells(1).Text.ToString
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreID", SqlDbType.Int)).Value = grdValidateEPG.Rows(e.RowIndex).Cells(2).Text.ToString
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "D"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetSubGenre")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Validate EPG", "grdValidateEPG_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged
        Try
            Dim obj As New Logger
            lbEPGExists.Visible = True
            lbEPGExists.Text = obj.GetEpgDates(ddlChannelName.SelectedValue)
            If lbEPGExists.Text = "EPG Exists from   to  ." Then
                lbEPGExists.Visible = False
            End If
            CheckgrdValidateEPG()
        Catch ex As Exception
            Logger.LogError("Validate EPG", "ddlChannelName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub CheckgrdValidateEPG()
        Try
            If txtStartDate.Text = "" Or txtEndDate.Text = "" Or lbEPGExists.Text = "No Data Avaialble in EPG for these Dates." Then
                grdValidateEPG.EmptyDataText = "Dates not selected or no record found !"
                grdValidateEPG.EmptyDataRowStyle.BackColor = Drawing.Color.Red
            Else
                grdValidateEPG.EmptyDataText = "EPG for selected dates is validated"
                grdValidateEPG.EmptyDataRowStyle.BackColor = Drawing.Color.Green
            End If
        Catch ex As Exception
            Logger.LogError("Validate EPG", "CheckgrdValidateEPG", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class