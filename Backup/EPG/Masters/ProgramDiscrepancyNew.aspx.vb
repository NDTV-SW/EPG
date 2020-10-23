Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class ProgramDiscrepancyNew
    Inherits System.Web.UI.Page
    Private Sub myErrorBox1(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
                ddlDays.SelectedValue = 6
                'bindDdlChannelName()
                bindDdlLanguage()
                bindGrdSynopsis(False)
                bindGrdXMLGenerated(False)
                bindGrdGenerateAgain(False)
                bindGrdProgramMaster(False)
                tblXML.Visible = False
            End If

            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER") Or User.IsInRole("USER")) Then
                btnGenerateXML.Visible = True
            Else
                btnGenerateXML.Visible = False
                checkUserType()
                If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                    txtChannel.Visible = True
                    checkUserType()
                End If
            End If

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "Page Load", ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

#Region "Bind Controls"
    Private Sub bindDdlLanguage()
        Try
            Dim i As Integer
            i = ddlLanguage.Items.Count - 1

            While i > 0
                ddlLanguage.Items.RemoveAt(i)
                i = i - 1
            End While

            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select '0' LanguageID,'All' FullName union select  b.LanguageID,b.FullName from mst_ChannelRegionalName a,mst_language b where channelid='" & txtChannel.Text & "' and a.LanguageId=b.LanguageID and b.active=1", False)

            ddlLanguage.DataSource = dt
            ddlLanguage.DataTextField = "FullName"
            ddlLanguage.DataValueField = "LanguageId"
            ddlLanguage.DataBind()

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "ddlLanguageBind", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub bindGrdProgramMaster(ByVal paging As Boolean)
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("sp_epg_validate_synopsis_v1", "ChannelId~Language", "VarChar~Int", txtChannel.Text & "~" & ddlLanguage.SelectedValue, True, False)
            grdProgrammaster.DataSource = dt
            If paging = False Then
                grdProgrammaster.PageIndex = 0
            End If
            grdProgrammaster.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "bindGrdProgramMaster", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub bindGrdSynopsis(ByVal paging As Boolean)
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("Select * from SYNOPSIS_UPDATE_PENDING where ChannelId='" & txtChannel.Text & "' and fixed=0 and CONVERT(varchar,updatedat,112)=CONVERT(varchar,dbo.GetLocalDate(),112)", False)
            grdSynopsis.DataSource = dt
            If paging = False Then
                grdSynopsis.PageIndex = 0
            End If
            grdSynopsis.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "bindGrdSynopsis", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub bindGrdXMLGenerated(ByVal paging As Boolean)
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select distinct convert(varchar,progdate,106) progdate1,progdate from mst_epg where ChannelId='" & txtChannel.Text & "' and xml_generated=0 and convert(varchar,Progdate,112)>=convert(varchar,dbo.GetLocalDate(),112)order by progdate", False)
            grdXMLGenerated.DataSource = dt
            If paging = False Then
                grdXMLGenerated.PageIndex = 0
            End If
            grdXMLGenerated.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "bindGrdXMLGenerated", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub bindGrdGenerateAgain(ByVal paging As Boolean)
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select distinct convert(varchar,progdate,106) progdate1,progdate from mst_epg where ChannelId='" & txtChannel.Text & "' and xml_generated=1 and convert(varchar,Progdate,112)>=convert(varchar,dbo.GetLocalDate(),112) and convert(varchar,Progdate,112)<=convert(varchar,dbo.GetLocalDate()+3,112) order by progdate", False)
            grdGenerateAgain.DataSource = dt
            If paging = False Then
                grdGenerateAgain.PageIndex = 0
            End If
            grdGenerateAgain.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "bindGrdGenerateAgain", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

#Region "Page Index Changing"

    Protected Sub grdProgrammaster_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProgrammaster.PageIndexChanging
        grdProgrammaster.PageIndex = e.NewPageIndex
        bindGrdProgramMaster(True)
        'grdProgrammaster.DataBind()
    End Sub

    Protected Sub grdXMLGenerated_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdXMLGenerated.PageIndexChanging
        grdXMLGenerated.PageIndex = e.NewPageIndex
        bindGrdXMLGenerated(True)
        'grdXMLGenerated.DataBind()
    End Sub

    Protected Sub grdGenerateAgain_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdGenerateAgain.PageIndexChanging
        grdGenerateAgain.PageIndex = e.NewPageIndex
        bindGrdGenerateAgain(True)
        'grdGenerateAgain.DataBind()
    End Sub

    Protected Sub grdSynopsis_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSynopsis.PageIndexChanging
        grdSynopsis.PageIndex = e.NewPageIndex
        bindGrdSynopsis(True)
        'grdSynopsis.DataBind()
    End Sub

#End Region

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
            Logger.LogError("Program Discrepancy", "checkUserType", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdProgrammaster_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdProgrammaster.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdProgrammaster.Rows(index)


            Dim txtProgram As TextBox = DirectCast(row.FindControl("txtProgName"), TextBox)
            Dim txtSynopsis As TextBox = DirectCast(row.FindControl("txtSynopsis"), TextBox)
            Dim lbEpisodeNo As Label = DirectCast(row.FindControl("lbEpisodeNo"), Label)
            Dim chkDefaultSynopsis As CheckBox = DirectCast(row.FindControl("chkDefaultSynopsis"), CheckBox)

            Dim lbCharLimit As Label = DirectCast(row.FindControl("lbCharLimit"), Label)
            Dim lbRowId As Label = DirectCast(row.FindControl("lbRowId"), Label)
            Dim lbProgid As Label = DirectCast(row.FindControl("lbProgid"), Label)
            Dim lbLanguageId As Label = DirectCast(row.FindControl("lbLanguageId"), Label)
            Dim lbProgName As Label = DirectCast(row.FindControl("lbProgName"), Label)

            If txtSynopsis.Text.ToString.Trim.Length > Convert.ToInt32(lbCharLimit.Text) Then
                myErrorBox("Length of synopsis cannot be greater than " & lbCharLimit.Text & ".")
                Exit Sub
            End If

            If e.CommandName.ToLower = "insert" Then
                Dim theProgName As String = txtProgram.Text
                Dim theSynopsis As String = IIf(txtSynopsis.Text = "N" Or txtSynopsis.Text = "Y", "", txtSynopsis.Text)

                If chkDefaultSynopsis.Checked Then

                    Dim obj As New clsExecute
                    Dim dtDefaultSynopsis As DataTable = obj.executeSQL("select synopsis from mst_programregional where progid='" & lbProgid.Text & "' and languageid='" & lbLanguageId.Text & "' and episodeno='0'", False)
                    If dtDefaultSynopsis.Rows.Count >= 1 Then
                        exec_ProcRegional(lbProgid.Text, theProgName, dtDefaultSynopsis.Rows(0)("synopsis").ToString.Trim, lbLanguageId.Text, 0, "A", lbEpisodeNo.Text)
                    Else
                        Logger.LogError("ProgramDescripency", "grdProgrammaster_RowCommand", "Synopsis does not exist for progid: " & lbProgid.Text & ", language: " & lbLanguageId.Text & ",Episode: 0", User.Identity.Name)
                    End If
                Else
                    exec_ProcRegional(lbProgid.Text, theProgName, theSynopsis, lbLanguageId.Text, 0, "A", lbEpisodeNo.Text)
                End If
                bindGrdProgramMaster(True)
            ElseIf e.CommandName.ToLower = "upd" Then
                Dim theProgName As String = txtProgram.Text
                Dim theSynopsis As String = IIf(txtSynopsis.Text = "N" Or txtSynopsis.Text = "Y", "", txtSynopsis.Text)
                If chkDefaultSynopsis.Checked Then
                    Dim obj As New clsExecute
                    Dim dtDefaultSynopsis As DataTable = obj.executeSQL("select synopsis from mst_programregional where progid='" & lbProgid.Text & "' and languageid='" & lbLanguageId.Text & "' and episodeno='0'", 0)
                    If dtDefaultSynopsis.Rows.Count >= 1 Then
                        exec_ProcRegional(lbProgid.Text, txtProgram.Text, dtDefaultSynopsis.Rows(0)("synopsis").ToString.Trim, lbLanguageId.Text, lbRowId.Text, "U", lbEpisodeNo.Text)
                    Else
                        Logger.LogError("ProgramDescripency", "grdProgrammaster_RowCommand", "Synopsis does not exist for progid: " & lbProgid.Text & ", language: " & lbLanguageId.Text & ",Episode: 0", User.Identity.Name)
                    End If
                Else
                    exec_ProcRegional(lbProgid.Text, txtProgram.Text, theSynopsis, lbLanguageId.Text, lbRowId.Text, "U", lbEpisodeNo.Text)
                End If
                bindGrdProgramMaster(True)
            End If

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", e.CommandName.ToLower & " grdProgrammaster_RowCommand", ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

    Private Sub btnUpdateSynopsis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateSynopsis.Click
        Try
            Dim row As GridViewRow
            For Each row In grdProgrammaster.Rows
                Dim txtProgram As TextBox = DirectCast(row.FindControl("txtProgName"), TextBox)
                Dim txtSynopsis As TextBox = DirectCast(row.FindControl("txtSynopsis"), TextBox)
                Dim lbCharLimit As Label = DirectCast(row.FindControl("lbCharLimit"), Label)
                Dim lbEpisodeNo As Label = DirectCast(row.FindControl("lbEpisodeNo"), Label)
                Dim lbProgId As Label = DirectCast(row.FindControl("lbProgId"), Label)
                Dim lbRowId As Label = DirectCast(row.FindControl("lbRowId"), Label)
                Dim lbLanguageId As Label = DirectCast(row.FindControl("lbLanguageId"), Label)
                Dim chkDefaultSynopsis As CheckBox = DirectCast(row.FindControl("chkDefaultSynopsis"), CheckBox)
                Dim chkUpdateSynopsis As CheckBox = DirectCast(row.FindControl("chkUpdateSynopsis"), CheckBox)

                If txtSynopsis.Text.ToString.Trim.Length > Convert.ToInt32(lbCharLimit.Text) Then
                    myErrorBox("Length of synopsis cannot be greater than " & lbCharLimit.Text & ".")
                    Exit Sub
                End If
                If chkUpdateSynopsis.Checked Then
                    Dim theProgName As String = txtProgram.Text
                    Dim theSynopsis As String = IIf(txtSynopsis.Text = "N", "", txtSynopsis.Text)



                    If chkDefaultSynopsis.Checked Then

                        Dim obj As New clsExecute
                        Dim dtDefaultSynopsis As DataTable = obj.executeSQL("select synopsis from mst_programregional where progid='" & lbProgId.Text & "' and languageid='" & lbLanguageId.Text & "' and episodeno='0'", 0)

                        If dtDefaultSynopsis.Rows.Count >= 1 Then
                            exec_ProcRegional(lbProgId.Text, txtProgram.Text, dtDefaultSynopsis.Rows(0)("Synopsis").ToString.Trim, lbLanguageId.Text, lbRowId.Text, "U", lbEpisodeNo.Text)
                        Else
                            Logger.LogError("ProgramDescripency", "btnUpdateSynopsis_Click", "Synopsis does not exist for progid: " & lbProgId.Text & ", language: " & lbLanguageId.Text & ",Episode: 0", User.Identity.Name)
                        End If
                    Else
                        exec_ProcRegional(lbProgId.Text, txtProgram.Text, theSynopsis, lbLanguageId.Text, lbRowId.Text, "U", lbEpisodeNo.Text)
                    End If
                End If
            Next row
            bindGrdProgramMaster(True)
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "btnUpdateSynopsis_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdSynopsis_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSynopsis.RowCommand
        Try
            If e.CommandName.ToLower = "fix" Then
                Dim lbRowId As Label = DirectCast(grdSynopsis.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowId"), Label)
                Dim lbFix As Label = DirectCast(grdSynopsis.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowId"), Label)

                Dim obj As New clsExecute
                obj.executeSQL("sp_SYNOPSIS_UPDATE_PENDING", "rowid~fixed~fixedby", "varchar~bit~varchar", lbRowId.Text.Trim & "~" & True & "~" & User.Identity.Name.ToString.Trim, True, False)
                bindGrdSynopsis(False)
            End If
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", e.CommandName.ToLower & " grdSynopsis_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    'Private Sub btnFixSynopsis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFixSynopsis.Click
    '    Try
    '        Dim row As GridViewRow
    '        For Each row In grdSynopsis.Rows
    '            Dim chkFixSynopsis As CheckBox = DirectCast(row.FindControl("chkFixSynopsis"), CheckBox)
    '            If chkFixSynopsis.Checked Then
    '                Dim lbRowId As Label = DirectCast(row.FindControl("lbRowId"), Label)
    '                Dim lbProgname As Label = DirectCast(row.FindControl("lbProgName"), Label)
    '                Dim lbNewSynopsis As Label = DirectCast(row.FindControl("lbNewSynop"), Label)

    '                Dim obj As New clsExecute
    '                obj.executeSQL("update mst_programregional set synopsis='" & lbNewSynopsis.Text.Replace("'", "''") & "' where languageId=1 and progid=(select top 1 progID from mst_program where channelID='" & txtChannel.Text & "' and ltrim(rtrim(progname))=LTRIM(rtrim('" & lbProgname.Text.Replace("'", "''") & "')))", False)
    '                obj.executeSQL("sp_SYNOPSIS_UPDATE_PENDING", "rowid~fixed~fixedby", "varchar~bit~varchra", lbRowId.Text.Trim & "~" & True & "~" & User.Identity.Name.ToString.Trim, True, False)
    '            End If
    '        Next row
    '        bindGrdSynopsis(False)
    '    Catch ex As Exception
    '        Logger.LogError("Program Discrepancy", "btnFixSynopsis_Click", ex.Message.ToString, User.Identity.Name)
    '    End Try
    'End Sub

    Private Sub grdProgrammaster_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgrammaster.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim txtSynopsis As TextBox = DirectCast(e.Row.FindControl("txtSynopsis"), TextBox)
                Dim txtProgramname As TextBox = DirectCast(e.Row.FindControl("txtProgName"), TextBox)
                Dim lbOrigProgName As Label = DirectCast(e.Row.FindControl("lbOrigProgName"), Label)
                Dim lbOrigSynopsis As Label = DirectCast(e.Row.FindControl("lbOrigSynopsis"), Label)
                Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
                Dim lbCharLimit As Label = DirectCast(e.Row.FindControl("lbCharLimit"), Label)
                Dim chkUpdateSynopsis As CheckBox = DirectCast(e.Row.FindControl("chkUpdateSynopsis"), CheckBox)
                Dim btn As Button = DirectCast(e.Row.FindControl("Btn_AddNew"), Button)
                txtProgramname.Visible = False
                txtSynopsis.Visible = False
                btn.Visible = False

                If lbProgId.Text = "" Then
                    btn.Visible = False
                    chkUpdateSynopsis.Visible = False
                    btnUpdateSynopsis.Visible = False
                    tblXML.Visible = True
                Else
                    btn.Visible = True
                    tblXML.Visible = False
                    chkUpdateSynopsis.Visible = True
                    btnUpdateSynopsis.Visible = True

                    If txtProgramname.Text = "Y" And txtSynopsis.Text = "Y" Then
                        txtProgramname.Visible = True
                        txtSynopsis.Visible = True
                        txtProgramname.Text = String.Empty
                        txtSynopsis.Text = String.Empty
                        btn.Text = "Insert"
                        btn.CommandName = "insert"

                    ElseIf txtProgramname.Text = "Y" And txtSynopsis.Text = "N" Then
                        txtProgramname.Visible = True
                        txtSynopsis.Visible = True
                        txtSynopsis.Enabled = False

                        txtProgramname.Text = String.Empty
                        txtSynopsis.Text = String.Empty
                        btn.Text = "Insert"
                        btn.CommandName = "insert"

                    ElseIf txtProgramname.Text = "N" And txtSynopsis.Text = "Y" Then
                        txtProgramname.Enabled = False
                        txtProgramname.Text = lbOrigProgName.Text
                        txtProgramname.Visible = True
                        txtSynopsis.Text = String.Empty
                        txtSynopsis.Visible = True
                        txtSynopsis.Text = lbOrigSynopsis.Text
                        btn.Text = "Update"
                        btn.CommandName = "upd"
                    End If

                    ' If ddlLanguage.SelectedValue = 1 Then
                    lbProgName.Attributes.Add("onclick", "javascript:copyProgramToSynopsis('" & txtProgramname.Text & "','" & e.Row.RowIndex & "')")
                    'End If

                    If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER") Or User.IsInRole("USER")) Then
                        btn.Visible = True
                    Else
                        btn.Visible = False
                        If (User.IsInRole("ENGLISH") And ddlLanguage.SelectedItem.Text.ToUpper = "ENGLISH") Then
                            btn.Visible = True
                        ElseIf (User.IsInRole("HINDI") And ddlLanguage.SelectedItem.Text.ToUpper = "HINDI") Then
                            btn.Visible = True
                        ElseIf (User.IsInRole("MARATHI") And ddlLanguage.SelectedItem.Text.ToUpper = "MARATHI") Then
                            btn.Visible = True
                        ElseIf (User.IsInRole("TELUGU") And ddlLanguage.SelectedItem.Text.ToUpper = "TELUGU") Then
                            btn.Visible = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "grdProgrammaster_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub exec_ProcRegional(ByVal ProgId As Integer, ByVal ProgName As String, ByVal synopsis As String, ByVal LanguageID As Integer, ByVal ID As Integer, ByVal action As Char, ByVal vEpisodeNo As Integer)
        Try

            Dim vProgName As String
            Dim vSynopsis As String
            If LanguageID.ToString.Trim = "1" Then
                vProgName = Logger.RemSplCharsEng(ProgName.ToString.Trim)
                vSynopsis = Logger.RemSplCharsEng(synopsis.ToString.Trim)
            Else
                vProgName = Logger.RemSplCharsAllLangs(ProgName.ToString.Trim, Convert.ToInt32(LanguageID.ToString))
                vSynopsis = Logger.RemSplCharsAllLangs(synopsis.ToString.Trim, Convert.ToInt32(LanguageID.ToString))
            End If

            If Not (action.ToString.ToUpper = "A" And vProgName.Trim = "") Then

                Dim obj As New clsExecute
                obj.executeSQL("sp_mst_ProgramRegional", "Rowid~ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                        "Int~Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                        ID & "~" & ProgId & "~" & vProgName & "~" & vSynopsis & "~" & vEpisodeNo & "~" & LanguageID & "~" & action & "~" & User.Identity.Name, True, False)
            End If
        Catch ex As Exception
            Logger.LogError("ProgramDiscrepancyNew", action & " exec_ProcRegional", "Message:  Action='" & action & "', Progid='" & ProgId & "',ProgName='" & ProgName & "',Synopsis'""',LanguageId='" & LanguageID & "', RowId='" & ID.ToString & "', ActionUser='" & User.Identity.Name & "' ", User.Identity.Name)
            Logger.LogError("ProgramDiscrepancyNew", action & " exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Dim fileError As Integer

#Region "XML Generation"
    Private Sub btnGenerateXML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateXML.Click
        Try

            Session.Add("StartDateValue", txtStartDate.Text)
            Session.Add("EndDateValue", Convert.ToDateTime(txtStartDate.Text).AddDays(ddlDays.SelectedValue).ToString)
            CreateXML(txtChannel.Text, txtChannel.Text.ToString.Trim, Session("StartDateValue").ToString.Trim, Session("EndDateValue").ToString.Trim)
            If fileError = 1 Then
                myErrorBox("XML File not generated. !")
                Exit Sub
            End If
            CreateXMLRecordDetails(txtChannel.Text, txtChannel.Text, Session("StartDateValue").ToString.Trim, Session("EndDateValue").ToString.Trim)
            myMessageBox("XML Generated!")
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "btnGenerateXML_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnGenerateXMLAgain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateXMLAgain.Click
        Try

            Session.Add("StartDateValue", DateTime.Now.ToString("MM/dd/yyyy"))
            If (ddlDays.SelectedValue > 3) Then
                ddlDays.SelectedValue = 3
            End If
            Session.Add("EndDateValue", Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")).AddDays(ddlDays.SelectedValue).ToString)
            CreateXMLAgain(txtChannel.Text, txtChannel.Text, Session("StartDateValue").ToString.Trim, Session("EndDateValue").ToString.Trim)
            If fileError = 1 Then
                myErrorBox("XML File not generated. !")
                Exit Sub
            End If
            CreateXMLRecordDetails(txtChannel.Text, txtChannel.Text, Session("StartDateValue").ToString.Trim, Session("EndDateValue").ToString.Trim)
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "btnGenerateXML_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub CreateXML(ByVal ChannelId As String, ByVal ChannelName As String, ByVal StartDate As Date, ByVal EndDate As Date)
        Try
            Dim path As String = ""
            Dim formattedString As String
            Dim XMLFile As XDocument

            Dim obj As New clsExecute
            obj.executeSQL("sp_epg_recording", "ChannelId~EPGFromDate~EpgToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & StartDate & "~" & EndDate, True, False)

            Dim dt As DataTable = obj.executeSQL("Select [dbo].[FN_XML_EPG_NEW] ('" & ChannelId.ToString.Trim & "','" & StartDate & "','" & EndDate & "')", False)

            If dt.Rows(0)(0).ToString.ToUpper = "ERROR" Then
                lbGenerateXML.Visible = True
                lbGenerateXML.Text = "The XML for specified dates cannot be generated as some or all dates do not have new EPG data. You must upload FPC and 'BUILD EPG' for these dates!"
                Exit Sub
            End If
            formattedString = FormatXml(dt.Rows(0)(0).ToString)

            XMLFile = XDocument.Parse(formattedString)
            path = Server.MapPath("../XML/")
            WriteFile(path & Regex.Replace(ChannelName.ToString, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml", formattedString)
            CatchupCount(ChannelId, StartDate, EndDate, formattedString)

            Dim _FileInfo As New System.IO.FileInfo(path & Regex.Replace(ChannelName.ToString, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml")
            If Not _FileInfo.Exists() Then
                fileError = 1
                'Exit Sub
            Else
                fileError = 0
            End If

            obj.executeSQL("sp_epg_check_unique_serviceid", "ChannelId~EPGFromDate~EPGToDate", "VarChar~DateTime~DateTime", ChannelId & "~" & StartDate & "~" & EndDate, True, False)

            btnGenerateXML.Enabled = False
            lbGenerateXML.Visible = True
            hyViewXml.Visible = True

            hyViewXml.NavigateUrl = "~/XML/" & Regex.Replace(ChannelName.ToString.Trim, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml"
            Dim strStartDate, strEndDate As String
            strStartDate = (Convert.ToDateTime(Session("StartDateValue").ToString.Trim)).ToString("dd-MMM-yyyy")
            strEndDate = (Convert.ToDateTime(Session("EndDateValue").ToString.Trim)).ToString("dd-MMM-yyyy")

            myMessageBox("XML generated Successfully!")

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "CreateXML", ex.Message.ToString, User.Identity.Name)
            myErrorBox("XML not generated. Please check error logs.")
        End Try
    End Sub

    Private Sub CreateXMLAgain(ByVal ChannelId As String, ByVal ChannelName As String, ByVal StartDate As Date, ByVal EndDate As Date)
        Try
            Dim path As String = ""
            Dim formattedString As String
            Dim XMLFile As XDocument

            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("Select [dbo].[FN_XML_EPG_NEW_AGAIN] ('" & ChannelId.ToString.Trim & "','" & StartDate & "','" & EndDate & "')", False)

            If dt.Rows(0)(0).ToString.ToUpper = "ERROR" Then
                lbGenerateXML.Visible = True
                lbGenerateXML.Text = "The XML for specified dates cannot be generated AGAIN as some or all dates do not have XML generated ALREADY!"
                Exit Sub
            End If

            formattedString = FormatXml(dt(0)(0).ToString)
            XMLFile = XDocument.Parse(formattedString)
            path = Server.MapPath("../XML/")
            WriteFile(path & Regex.Replace(ChannelName.ToString, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml", formattedString)
            CatchupCount(ChannelId, StartDate, EndDate, formattedString)

            Dim _FileInfo As New System.IO.FileInfo(path & Regex.Replace(ChannelName.ToString, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml")
            If Not _FileInfo.Exists() Then
                fileError = 1
                'Exit Sub
            Else
                fileError = 0
            End If

            btnGenerateXMLAgain.Enabled = True
            hyViewXml.Visible = True

            hyViewXml.NavigateUrl = "~/XML/" & Regex.Replace(ChannelName.ToString.Trim, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml"

            Dim strStartDate, strEndDate As String
            strStartDate = (Convert.ToDateTime(Session("StartDateValue").ToString.Trim)).ToString("dd-MMM-yyyy")
            strEndDate = (Convert.ToDateTime(Session("EndDateValue").ToString.Trim)).ToString("dd-MMM-yyyy")


            myMessageBox("XML generated Successfully!")

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "CreateXMLAgain", ex.Message.ToString, User.Identity.Name)
            myErrorBox("XML not generated. Please check error logs.")
        End Try
    End Sub

    Private Function FormatXml(ByVal sUnformattedXml As String) As String
        Try
            'load unformatted xml into a dom
            Dim xd As New XmlDocument()
            xd.LoadXml(sUnformattedXml)

            'will hold formatted xml
            Dim sb As New StringBuilder()

            'pumps the formatted xml into the StringBuilder above
            Dim sw As New StringWriter(sb)

            'does the formatting
            Dim xtw As XmlTextWriter = Nothing

            Try
                'point the xtw at the StringWriter
                xtw = New XmlTextWriter(sw)

                'we want the output formatted
                xtw.Formatting = Formatting.Indented

                'get the dom to dump its contents into the xtw 
                xd.WriteTo(xtw)
            Catch ex As Exception
                Logger.LogError("Program Discrepancy", "FormatXml", ex.Message.ToString, User.Identity.Name)
            Finally
                'clean up even if error
                If xtw IsNot Nothing Then
                    xtw.Close()
                End If
            End Try

            'return the formatted xml
            Return sb.ToString()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "FormatXml", ex.Message.ToString, User.Identity.Name)
            Return (0)
        End Try
    End Function

    Private Sub CreateXMLRecordDetails(ByVal ChannelId As String, ByVal ChannelName As String, ByVal StartDate As Date, ByVal EndDate As Date)
        Try

            Dim xmlfilename As String = Regex.Replace(ChannelName.ToString.Trim, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml"
            Dim obj As New clsExecute
            obj.executeSQL("sp_epg_xml_ftp_record_details", "ChannelId~StartDate~EndDate~xmlFileName", "VarChar~DateTime~DateTime~VarChar", ChannelId & "~" & StartDate & "~" & EndDate & "~" & xmlfilename, True, False)

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "CreateXMLRecordDetails", ex.Message.ToString, User.Identity.Name)
            myErrorBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub CatchupCount(ByVal ChannelId As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal FileData As String)
        Try

            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select catchupflag from mst_channel where channelid='" + ChannelId + "'", False)

            Dim strSearch As String = ""
            Dim Occurrences As Integer = 0
            Dim epgDate As Date, epgNextDate As Date
            Dim i As Integer, j As Integer
            Dim inputStr As String = ""

            If dt.Rows(0)(0).ToString = "1" Or dt.Rows(0)(0).ToString = "True" Then

                obj.executeSQL("SP_CatchupDuration_Channelwise", "ChannelId~EPGFromDate~EPGToDate", "VarChar~DateTime~DateTime", ChannelId & "~" & StartDate & "~" & EndDate, True, False)

                strSearch = String.Format("{0:yyyy/MM/dd}", StartDate)
                i = InStr(FileData, strSearch)
                If StartDate <> EndDate Then 'more than one day
                    epgDate = StartDate
                    Do While epgDate < EndDate
                        strSearch = String.Format("{0:yyyy/MM/dd}", epgDate)
                        i = InStr(FileData, strSearch)

                        epgNextDate = DateAdd(DateInterval.Day, 1, epgDate)
                        strSearch = String.Format("{0:yyyy/MM/dd}", epgNextDate)
                        j = InStr(i + 100, FileData, strSearch) 'first occurence in header ignore

                        inputStr = Mid(FileData, i, (j - i))
                        Occurrences = UBound(Split(inputStr, "<catchupFlag>1</catchupFlag>"))
                        obj.executeSQL("delete trn_catchupdatewise where convert(varchar(12),epgdate,112)='" & epgDate.ToString("yyyyMMdd") & "' and channelid='" + ChannelId + "'", False)
                        obj.executeSQL("insert trn_catchupdatewise(channelid,epgdate,catchupcount,lastupdate) values('" + ChannelId + "','" + epgDate + "','" + Occurrences.ToString + "',dbo.GetLocalDate())", False)
                        epgDate = DateAdd(DateInterval.Day, 1, epgDate)
                    Loop
                    If epgDate = EndDate Then 'reached last date
                        inputStr = Mid(FileData, j)
                        Occurrences = UBound(Split(inputStr, "<catchupFlag>1</catchupFlag>"))
                        obj.executeSQL("delete trn_catchupdatewise where convert(varchar(12),epgdate,112)='" & epgDate.ToString("yyyyMMdd") & "' and channelid='" + ChannelId + "'", False)
                        obj.executeSQL("insert trn_catchupdatewise(channelid,epgdate,catchupcount,lastupdate) values('" + ChannelId + "','" + epgDate + "','" + Occurrences.ToString + "',dbo.GetLocalDate())", False)
                    End If
                Else 'one day xml
                    Occurrences = UBound(Split(FileData, "<catchupFlag>1</catchupFlag>"))
                    obj.executeSQL("delete trn_catchupdatewise where convert(varchar(12),epgdate,112)='" & StartDate.ToString("yyyyMMdd") & "' and channelid='" + ChannelId + "'", False)
                    obj.executeSQL("insert trn_catchupdatewise(channelid,epgdate,catchupcount,lastupdate) values('" + ChannelId + "','" + StartDate + "','" + Occurrences.ToString + "',dbo.GetLocalDate())", False)
                End If
            End If
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "CatchupCount", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Shared Function WriteFile(ByVal FileName As String, ByVal FileData As String) As Integer
        WriteFile = 0
        Dim outStream As StreamWriter
        outStream = New StreamWriter(FileName, False)
        outStream.Write(FileData)
        outStream.Close()
    End Function

#End Region

    Protected Sub ddlLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlLanguage.SelectedIndexChanged
        Try
            bindGrdProgramMaster(False)
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "ddlLanguage_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        Try
            btnGenerateXML.Enabled = True
            hyViewXml.Visible = False
            lbGenerateXML.Visible = False
            bindDdlLanguage()

            bindGrdProgramMaster(False)
            bindGrdSynopsis(False)
            bindGrdXMLGenerated(False)
            bindGrdGenerateAgain(False)


            Dim obj As New Logger
            lbEPGExists.Visible = True
            lbEPGExists.Text = obj.GetEpgDates(txtChannel.Text)
            
            Dim obj1 As New Logger
            lbEPGExists.Text = obj1.GetEpgDates(txtChannel.Text)
            
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "txtChannel_TextChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannelsForXML(prefixText, count)
        Return channels
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim row As GridViewRow
        For Each row In grdProgrammaster.Rows
            Dim chkCopyEnglish As CheckBox = DirectCast(row.FindControl("chkCopyEnglish"), CheckBox)
            If chkCopyEnglish.Checked Then
                Dim lbCharLimit As Label = DirectCast(row.FindControl("lbCharLimit"), Label)
                Dim lbRowId As Label = DirectCast(row.FindControl("lbRowId"), Label)
                Dim lbProgID As Label = DirectCast(row.FindControl("lbProgID"), Label)
                Dim lbProgName As Label = DirectCast(row.FindControl("lbProgName"), Label)

                Dim lbLanguageid As Label = DirectCast(row.FindControl("lbLanguageid"), Label)
                exec_ProcRegional(lbProgID.Text, lbProgName.Text, lbProgName.Text, lbLanguageid.Text, 0, "A", 0)
                chkCopyEnglish.Checked = False
            End If

        Next
        bindGrdProgramMaster(False)
    End Sub

    Protected Sub chkCheckAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckAll.CheckedChanged
        Dim row As GridViewRow
        For Each row In grdProgrammaster.Rows
            Dim chkCopyEnglish As CheckBox = DirectCast(row.FindControl("chkCopyEnglish"), CheckBox)
            Dim lbLanguageid As Label = DirectCast(row.FindControl("lbLanguageid"), Label)
            If chkCheckAll.Checked Then
                If Not (lbLanguageid.Text = 1 Or lbLanguageid.Text = 2) Then
                    chkCopyEnglish.Checked = True
                End If
            Else
                chkCopyEnglish.Checked = False
            End If
            
        Next
    End Sub

End Class