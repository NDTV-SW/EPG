Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Data.OleDb

Public Class RelianceErrorLogging
    Inherits System.Web.UI.Page

    Dim myConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy")
            txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
            txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
            CVtxtDate.ValueToCompare = DateTime.Today.ToShortDateString()
            If HttpContext.Current.User.IsInRole("LOGGER") Then
                grdRelianceError.Columns(9).Visible = False
                grdRelianceError.Columns(10).Visible = False
                grdRelianceError.Columns(8).Visible = False
            End If
        End If
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            Dim sql As String, strParameters As String, strType As String, strvalues As String
            sql = "sp_error_details_BIGTV"
            strParameters = "channelid~progid~actualRunningProgram~errorTypeId~addedby~errordatetime~correctOnAirtel~correctOnTatasky~remarks~action"
            strType = "varchar~int~varchar~int~varchar~datetime~bit~bit~varchar~char"
            strvalues = ddlChannelName.SelectedValue & "~" & ddlProgramme.SelectedValue & "~" & txtActual.Text.Trim & "~" & ddlErrorType.SelectedValue & "~" & User.Identity.Name.Trim & "~" & txtDate.Text & " " & txtTime.Text & "~" & chkCorrectOnAirtelDTH.Checked & "~" & chkCorrectOnTataSkyDTH.Checked & "~" & txtRemarks.Text & "~A"

            Dim obj As New clsExecute
            obj.executeSQL(sql, strParameters, strType, strvalues, True, False)
            clearAll()
            myMessageBox("Error logged successfully!")
        Catch ex As Exception
            myErrorBox("Something bad happened. Record was not inserted.")
            Logger.LogError("RelianceErrorLogging", " btnAdd_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Private Sub clearAll()
       
        'txtDate.Text = ""
        txtTime.Text = ""
        txtRemarks.Text = ""
        txtActual.Text = ""
        chkCorrectOnAirtelDTH.Checked = False
        chkCorrectOnTataSkyDTH.Checked = False
        grdRelianceError.SelectedIndex = -1
        grdRelianceError.DataBind()
    End Sub

    Protected Sub grdRelianceError_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdRelianceError.RowDeleting
        Try
            Dim strId As String = TryCast(grdRelianceError.Rows(e.RowIndex).FindControl("lbId"), Label).Text

            Dim sql As String, strParameters As String, strType As String, strvalues As String
            sql = "sp_error_details_BIGTV"
            strParameters = "rowid~action"
            strType = "int~char"
            strvalues = strId & "~D"

            Dim obj As New clsExecute
            obj.executeSQL(sql, strParameters, strType, strvalues, True, False)

          
            myMessageBox("Record Deleted!")
            clearAll()
        Catch ex As Exception
            Logger.LogError("RelianceErrorLogging", "grdRelianceError_RowDeleting", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error while deleting record!")
        End Try
    End Sub

    Protected Sub grdRelianceError_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdRelianceError.RowDeleted
        If e.ExceptionHandled = False Then
            e.ExceptionHandled = True
        End If

    End Sub

    Dim intSno As Integer

    Protected Sub grdRelianceError_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRelianceError.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1

            Dim chkAccepted As CheckBox = DirectCast(e.Row.FindControl("chkAccepted"), CheckBox)
            Dim lbErrorCauseId As Label = DirectCast(e.Row.FindControl("lbErrorCauseId"), Label)
            Dim ddlCauseId As DropDownList = DirectCast(e.Row.FindControl("ddlCauseId"), DropDownList)
            'If lbErrorCauseId.Text <> "" Then

            'End If
            If chkAccepted.Checked Then
                ddlCauseId.SelectedValue = lbErrorCauseId.Text
                chkAccepted.Text = ddlCauseId.SelectedItem.Text
            End If
            If HttpContext.Current.User.IsInRole("LOGGER") Then
                chkAccepted.Enabled = False
                If Not chkAccepted.Checked Then
                    chkAccepted.Text = "Pending"
                End If
            End If

        End If
    End Sub
    Private Sub grdRelianceError_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdRelianceError.RowCommand
        Try
            If e.CommandName.ToLower = "update" Then


                Dim lbRowId As Label = DirectCast(grdRelianceError.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbId"), Label)
                Dim chkAccepted As CheckBox = DirectCast(grdRelianceError.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("chkAccepted"), CheckBox)
                Dim ddlCauseId As DropDownList = DirectCast(grdRelianceError.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("ddlCauseId"), DropDownList)


                Dim sql As String, strParameters As String, strType As String, strvalues As String
                sql = "sp_error_details_BIGTV"
                strParameters = "rowid~errorcauseid~acceptedbyNDTV~action"
                strType = "int~int~bit~char"
                strvalues = lbRowId.Text & "~" & ddlCauseId.SelectedValue & "~" & chkAccepted.Checked & "~U"

                Dim obj As New clsExecute
                obj.executeSQL(sql, strParameters, strType, strvalues, True, False)

                'Dim obj As New clsExecute
                'obj.executeSQL("update error_details_BIGTV set acceptedbyNDTV='" & chkAccepted.Checked & "', errorcauseid='" & ddlCauseId.SelectedValue & "' where rowid='" & lbRowId.Text & "'", False)


                bindGrid()
                myMessageBox("Record Updated!")
            End If
        Catch ex As Exception
            Logger.LogError("RelianceErrorLogging", e.CommandName.ToLower & " grdRelianceError_RowCommand", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Error while updating record!")
        End Try
    End Sub
    Private Sub bindGrid()
        grdRelianceError.DataBind()
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid()
    End Sub


    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchProgramme(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim obj As New clsUploadModules
        Dim programme As List(Of String) = obj.getProgrammes(contextKey, prefixText, count)
        Return programme
    End Function
  
End Class