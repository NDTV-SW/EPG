Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class CopyEPGtoChannel
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Dim flag As Integer = 0
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                'ddlChannelName.DataSource = SqlDsChannelMaster
                'ddlChannelName.DataTextField = "sourceChannel"
                'ddlChannelName.DataValueField = "sourceChannel"
                'ddlChannelName.DataBind()
                'ddlChannelName.Items.Insert(0, New ListItem("Select", "0"))
                lbEPGDates.Visible = False
                lbDestinationEPGDates.Visible = False
            Else
                If ddlChannelName.SelectedIndex = 0 Then
                    lbEPGDates.Visible = False
                Else
                    lbEPGDates.Visible = True
                    Dim obj As New Logger
                    lbEPGDates.Text = obj.GetEpgDates(ddlChannelName.SelectedItem.ToString.Trim)
                End If

            End If
        Catch ex As Exception
            Logger.LogError("CopyEPGToChannel", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            If ddlChannelName.SelectedIndex = 0 Then
                myErrorBox("Please select Channel First")
                Exit Sub
            End If
            
            Dim dt As New DataTable
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_duplicate_channel_epg", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@sourceChannel", SqlDbType.VarChar, 100)).Value = ddlChannelName.Text.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@targetChannel", SqlDbType.VarChar, 100)).Value = ddlDestinationChannel.Text.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@fromDate", SqlDbType.Date)).Value = txtStartDate.Text
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@toDate", SqlDbType.Date)).Value = txtEndDate.Text
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@uploadedby", SqlDbType.VarChar, 50)).Value = User.Identity.Name


            MyDataAdapter.Fill(dt)
            MyDataAdapter.Dispose()
            MyConnection.Close()

            myMessageBox("EPG Copied Successfully!")
        Catch ex As Exception
            Logger.LogError("CopyEPGToChannel", "btnCopy_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error while copying EPG!")

        End Try
    End Sub

    

    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged

        'ddlDestinationChannel.DataSource = sqlDSDestinationChannel
        'sqlDSDestinationChannel.SelectParameters.Add("sourceChannel", DbType.String, ddlChannelName.SelectedValue)
        'ddlDestinationChannel.DataTextField = "destinationChannel"
        'ddlDestinationChannel.DataValueField = "destinationChannel"
        'ddlDestinationChannel.DataBind()
        'ddlDestinationChannel.Items.Insert(0, New ListItem("Select", "0"))
        ddlDestinationChannel.DataBind()
    End Sub

    Protected Sub ddlDestinationChannel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDestinationChannel.SelectedIndexChanged
        If ddlDestinationChannel.SelectedIndex = 0 Then
            lbDestinationEPGDates.Visible = False
        Else
            lbDestinationEPGDates.Visible = True
            Dim obj As New Logger
            lbDestinationEPGDates.Text = obj.GetEpgDates(ddlDestinationChannel.SelectedItem.ToString.Trim)
        End If
    End Sub
End Class
