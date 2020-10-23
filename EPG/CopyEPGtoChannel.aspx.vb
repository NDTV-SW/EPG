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

            Dim obj As New clsExecute
            obj.executeSQL("sp_duplicate_channel_epg", "sourceChannel~targetChannel~fromDate~toDate~uploadedby", "varchar~varchar~datetime~datetime~varchar",
                         ddlChannelName.Text & "~" & ddlDestinationChannel.Text & "~" & txtStartDate.Text & "~" & txtEndDate.Text & "~" & User.Identity.Name, True, False)
            Dim objUpload As New clsUploadModules
            objUpload.insertFakeBuildEPG(ddlDestinationChannel.Text, txtStartDate.Text, txtEndDate.Text, User.Identity.Name)

            myMessageBox("EPG Copied Successfully!")
        Catch ex As Exception
            Logger.LogError("CopyEPGToChannel", "btnCopy_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error while copying EPG!")

        End Try
    End Sub

    

    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged

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
