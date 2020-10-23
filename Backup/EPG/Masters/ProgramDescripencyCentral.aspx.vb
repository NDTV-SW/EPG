Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports Excel
Imports AjaxControlToolkit

Public Class ProgramDescripencyCentral
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Dim flag As Integer = 0

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
                ddlLanguageBind()
            End If
            'checkUserType()
            If Page.IsPostBack = False Then
                Try
                    If IsNothing(Request.QueryString("Mode")) Then
                        Exit Sub
                    End If
                    Dim qs As String
                    qs = Request.QueryString("Mode").ToString
                    If (qs = "Insert") Then
                        ddlType.SelectedValue = "ProgName"
                        grdProgrammaster.Columns(5).Visible = False
                    Else
                        ddlType.SelectedValue = "Synopsis"
                        grdProgrammaster.Columns(5).Visible = True
                    End If
                    ddlLanguage.SelectedValue = Request.QueryString("LangId")
                Catch ex As Exception
                End Try
            End If

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "Page Load", ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

    Private Sub checkUserType()
        Try
            For i = ddlLanguage.Items.Count - 1 To 0 Step -1
                If (ddlLanguage.Items(i).Text.ToUpper = "ENGLISH") Then
                    ddlLanguage.Items.RemoveAt(i)
                End If
            Next
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "checkUserType", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    
    Private Sub grdProgrammaster_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgrammaster.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(4).Text = ddlLanguage.SelectedItem.Text.ToString & " " & "Programme"
                e.Row.Cells(5).Text = ddlLanguage.SelectedItem.Text.ToString & " " & "Synopsis"
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lbProgid As Label = DirectCast(e.Row.FindControl("lbProgid"), Label)
                Dim lbChannel As Label = DirectCast(e.Row.FindControl("lbChannel"), Label)
                Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                Dim lbSynopsis As Label = DirectCast(e.Row.FindControl("lbSynopsis"), Label)
                Dim lbRegProgName As Label = DirectCast(e.Row.FindControl("lbRegProgName"), Label)
                Dim lbRegSynopsis As Label = DirectCast(e.Row.FindControl("lbRegSynopsis"), Label)
                Dim hyEdit As HyperLink = DirectCast(e.Row.FindControl("hyEdit"), HyperLink)
                If ddlType.SelectedValue = "ProgName" Then
                    hyEdit.NavigateUrl = "javascript:openWin('" & lbProgid.Text.Trim & "','" & ddlLanguage.SelectedValue.Trim & "','Insert','" & lbProgName.Text.Replace("'", "^") & "','" & lbSynopsis.Text.Replace("'", "^") & "','" & lbRegProgName.Text.Replace("'", "^") & "','" & lbRegSynopsis.Text.Replace("'", "^") & "','" & ddlLanguage.SelectedItem.Text.Trim & "')"
                ElseIf lbRegProgName.Text = "" Then
                    hyEdit.NavigateUrl = "javascript:openWin('" & lbProgid.Text.Trim & "','" & ddlLanguage.SelectedValue.Trim & "','Insert1','" & lbProgName.Text.Replace("'", "^") & "','" & lbSynopsis.Text.Replace("'", "^") & "','" & lbRegProgName.Text.Replace("'", "^") & "','" & lbRegSynopsis.Text.Replace("'", "^") & "','" & ddlLanguage.SelectedItem.Text.Trim & "')"
                Else
                    hyEdit.NavigateUrl = "javascript:openWin('" & lbProgid.Text.Trim & "','" & ddlLanguage.SelectedValue.Trim & "','Update','" & lbRegProgName.Text.Replace("'", "^") & "','" & lbSynopsis.Text.Replace("'", "^") & "','" & lbRegProgName.Text.Replace("'", "^") & "','" & lbRegSynopsis.Text.Replace("'", "^") & "','" & ddlLanguage.SelectedItem.Text.Trim & "')"
                End If
            End If
        Catch ex As Exception
            Logger.LogError("Program Discrepancy Central", "grdProgrammaster_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub exec_ProcRegional(ByVal ProgId As Integer, ByVal ProgName As String, ByVal synopsis As String, ByVal LanguageID As Integer, ByVal ID As Integer, ByVal action As Char)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_programregional", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = ProgId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = ProgName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = synopsis.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = LanguageID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = ID.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetRegionalProgName")
            MyDataAdapter.Dispose()
            MyConnection.Close()

        Catch ex As Exception
            Logger.LogError("ProgramDiscrepancyCentral", action & " exec_ProcRegional", "Message:  Action='" & action & "', Progid='" & ProgId & "',ProgName='" & ProgName & "',Synopsis'""',LanguageId='" & LanguageID & "', RowId='" & ID.ToString & "', ActionUser='" & User.Identity.Name & "' ", User.Identity.Name)
            Logger.LogError("ProgramDiscrepancyCentral", action & " exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Private Shared Function WriteFile(ByVal FileName As String, ByVal FileData As String) As Integer
        WriteFile = 0
        Dim outStream As StreamWriter
        outStream = New StreamWriter(FileName, False)
        outStream.Write(FileData)
        outStream.Close()
    End Function

    Private Sub ddlLanguageBind()
        Try
            Dim i As Integer
            i = ddlLanguage.Items.Count - 1
            While i > 0
                ddlLanguage.Items.RemoveAt(i)
                i = i - 1
            End While
            Dim adp As New SqlDataAdapter("select * from mst_language where active=1", ConString)
            Dim ds As New DataSet
            adp.Fill(ds)
            ddlLanguage.DataSource = ds
            ddlLanguage.DataTextField = "FullName"
            ddlLanguage.DataValueField = "LanguageId"
            ddlLanguage.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "ddlLanguageBind", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Protected Sub Excel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Excel.Click
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=AllChannel" & ddlType.SelectedValue & "_" & ddlLanguage.SelectedItem.Text.Trim & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"

        ' Add the HTML from the GridView to a StringWriter so we can write it out later
        Dim sw As System.IO.StringWriter = New System.IO.StringWriter
        Dim hw As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(sw)
        grdProgrammaster.RenderControl(hw)

        ' Write out the data
        Response.Write(sw.ToString)
        Response.End()

        'Dim form As New HtmlForm()
        'Response.Clear()
        'Response.Buffer = True
        'Dim filename As String = "AllChannelRegionalNames_" & ddlLanguage.SelectedItem.Text.Trim & ".xls"

        'Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        'Response.Charset = ""
        'Response.ContentType = "application/vnd.ms-excel"
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)

        'grdProgrammaster.AllowPaging = False
        'grdProgrammaster.DataBind()
        'form.Controls.Add(grdProgrammaster)
        'Me.Controls.Add(form)
        'form.RenderControl(hw)

        'Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
        'Response.Write(style)
        'Response.Output.Write(sw.ToString())
        'Response.[End]()
        'Response.Flush()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        If ddlType.SelectedValue = "ProgName" Then
            grdProgrammaster.Columns(4).Visible = True
            grdProgrammaster.Columns(5).Visible = False
        Else
            grdProgrammaster.Columns(4).Visible = True
            grdProgrammaster.Columns(5).Visible = True
        End If
    End Sub
End Class