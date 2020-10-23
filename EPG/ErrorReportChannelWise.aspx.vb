Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class ErrorReportChannelWise
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
        '    'Response.Redirect("~/Default.aspx")
        'End If
        If Page.IsPostBack = False Then
            txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
            txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            MyDataAdapter = New SqlDataAdapter("sp_error_insert", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelID", SqlDbType.NVarChar, 100)).Value = ddlChannelName.SelectedValue.ToString
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int, 4)).Value = IIf(ddlErrorType.SelectedValue = 6, 0, Convert.ToInt32(ddlProgrammes.SelectedValue.ToString))
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@FixedWhenOnAir", SqlDbType.VarChar, 50)).Value = IIf(chkFixedWhenOnAir.Checked, "Yes", "No")
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ErrorId", SqlDbType.Int, 4)).Value = Convert.ToInt32(ddlErrorType.SelectedValue.ToString)
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@CauseId", SqlDbType.Int, 4)).Value = Convert.ToInt32(ddlErrorCause.SelectedValue.ToString)
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@errorDateTime", SqlDbType.DateTime, 4)).Value = txtDate.text & " " & txtTime.text
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@sourceOperator", SqlDbType.NVarChar, 200)).Value = ddlOperator.SelectedValue
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@remarks", SqlDbType.NVarChar, 200)).Value = txtRemarks.Text
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Addedby", SqlDbType.NVarChar, 50)).Value = User.Identity.Name.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "sp_error_insert")
            MyDataAdapter.Dispose()
            MyConnection.Close()
            grdErrorReportChannelWise.DataBind()
            clearAll()
        Catch ex As Exception
            Logger.LogError("ErrorReportChannelWise", "btnSave_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub clearAll()
        txtDate.Text = ""
        txtTime.Text = ""
        txtRemarks.Text = ""
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim MyDataAdapter As SqlDataAdapter
        MyDataAdapter = New SqlDataAdapter("sp_error_display1", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelID", SqlDbType.NVarChar, 100)).Value = ddlChannel.SelectedValue.ToString
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@StartDate", SqlDbType.DateTime)).Value = txtStartDate.Text
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@EndDate", SqlDbType.DateTime)).Value = txtEndDate.Text
        Dim dt As New DataTable
        MyDataAdapter.Fill(dt)
        grdErrorReportChannelWise.DataSource = dt
        grdErrorReportChannelWise.DataBind()
        MyDataAdapter.Dispose()
        MyConnection.Close()
    End Sub
End Class