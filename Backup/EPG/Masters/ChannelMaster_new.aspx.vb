Imports System
Imports System.Data.SqlClient
Public Class ChannelMaster_new
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
            If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
                btnAddChannel.Visible = True
                btnCancel.Visible = True
            ElseIf (User.IsInRole("USER")) Then
                btnAddChannel.Visible = True
                btnCancel.Visible = True
            Else
                btnAddChannel.Visible = False
                btnCancel.Visible = False
                If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                    btnAddChannel.Visible = False
                    btnCancel.Visible = False
                End If
            End If
            If Page.IsPostBack = False Then
                bindGrid()
            End If
        Catch ex As Exception
            Logger.LogError("Channel Master", "Page Load", ex.Message.ToString, User.Identity.Name)
            myErrorBox("Please see error report.")
        End Try
    End Sub
    
#Region "Channel Master"
    Private Sub exec_Proc(ChannelId As String, ByVal CompanyId As Integer, ByVal action As String, ByVal catchupflag As Boolean, ByVal active As Boolean, ByVal sendepg As Boolean, ByVal movieChannel As Boolean, ByVal channelGenre As Integer, ByVal ChannelLanguage As String, ByVal MovieLangId As Integer, ByVal seriesEnabled As Boolean, ByVal trp As Integer, ByVal publicChannel As Boolean, ByVal channelLanguageid As Integer)
        Try
            Dim obj As New clsExecute
            Dim strParams As String, strParamType As String, strParamVal As String
            strParams = "ChannelID~CompanyId~CatchupFlag~Active~moviechannel~ChannelLanguage~genreid~seriesEnabled~movielangid~sendEPG~trp~ActionUser~Action~publicChannel~channelLanguageid"
            strParamType = "varchar~int~bit~bit~bit~varchar~int~bit~int~bit~int~varchar~char~bit~Int"
            strParamVal = ChannelId & "~" & CompanyId & "~" & catchupflag & "~" & active & "~" & movieChannel & "~" & ChannelLanguage & "~" & channelGenre & "~" _
                        & seriesEnabled & "~" & MovieLangId & "~" & sendepg & "~" & trp & "~" & User.Identity.Name & "~" & action & "~" & publicChannel & "~" & channelLanguageid

            obj.executeSQL("sp_mst_channel", strParams, strParamType, strParamVal, True, False)

            grdChannelmaster.DataBind()
            'bindGrid()
        Catch ex As Exception
            Logger.LogError("Channel Master", action & " -Add/Update", ex.Message.ToString, User.Identity.Name)
            flag = 1
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            grdChannelmaster.SelectedIndex = -1
            txtHiddenId.Text = "0"
            txtChannelName.Text = ""
            btnAddChannel.Text = "Add"
            chkActive.Checked = True
            chkSendEPG.Checked = True
            chkMovieChannel.Checked = False
            chkCatchUpFlag.Checked = False
            txtChannelName.ReadOnly = False
        Catch ex As Exception
            Logger.LogError("Channel Master", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
            myErrorBox(ex.Message.ToString)
        End Try
    End Sub


    Private Sub btnAddChannel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddChannel.Click
        Try

            If btnAddChannel.Text = "Add" Then
                Call exec_Proc(txtChannelName.Text.ToString.Trim, ddlCompanyName.Text.ToString.Trim, "A", chkCatchUpFlag.Checked, chkActive.Checked, chkSendEPG.Checked, chkMovieChannel.Checked, cmbChannelGenre.SelectedValue, txtLanguage.Text.Trim, ddlMovieLanguage.SelectedValue, chkSeriesEnabled.Checked, IIf(txtTRP.Text = "", 0, txtTRP.Text), chkPublicChannel.Checked, ddlChannelLanguageid.SelectedValue)
            ElseIf btnAddChannel.Text = "Update" Then
                Call exec_Proc(txtHiddenId.Text.ToString.Trim, ddlCompanyName.Text.ToString.Trim, "U", chkCatchUpFlag.Checked, chkActive.Checked, chkSendEPG.Checked, chkMovieChannel.Checked, cmbChannelGenre.SelectedValue, txtLanguage.Text.Trim, ddlMovieLanguage.SelectedValue, chkSeriesEnabled.Checked, IIf(txtTRP.Text = "", 0, txtTRP.Text), chkPublicChannel.Checked, ddlChannelLanguageid.SelectedValue)
                grdChannelmaster.SelectedIndex = -1
            End If
            bindGrid()
            clearAll()
            If flag = 0 Then
                myMessageBox("Channel Add/Update successfull!")
            Else
                myErrorBox("Please check error report.")
            End If
            flag = 0
        Catch ex As Exception
            Logger.LogError("Channel Master", "btnAddChannel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdChannelmaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdChannelmaster.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbCompanyId As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbCompanyId"), Label)
            Dim lbCompanyName As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbCompanyName"), Label)
            Dim lbChannelId As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbChannelId"), Label)
            Dim lbChannelLanguage As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbChannelLanguage"), Label)
            Dim lbChannelLanguageId As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbChannelLanguageId"), Label)
            Dim lbMovieLangId As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbMovieLangId"), Label)
            Dim lbGenreId As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbGenreId"), Label)
            Dim lbTRP As Label = DirectCast(grdChannelmaster.SelectedRow.FindControl("lbTRP"), Label)

            chkActive.Checked = DirectCast(grdChannelmaster.SelectedRow.FindControl("chkActive"), CheckBox).Checked
            chkPublicChannel.Checked = DirectCast(grdChannelmaster.SelectedRow.FindControl("chkpublicChannel"), CheckBox).Checked

            chkMovieChannel.Checked = DirectCast(grdChannelmaster.SelectedRow.FindControl("chkMovieChannel"), CheckBox).Checked
            chkCatchUpFlag.Checked = DirectCast(grdChannelmaster.SelectedRow.FindControl("chkCatchupFlag"), CheckBox).Checked
            chkSendEPG.Checked = DirectCast(grdChannelmaster.SelectedRow.FindControl("chkSendEPG"), CheckBox).Checked
            chkSeriesEnabled.Checked = DirectCast(grdChannelmaster.SelectedRow.FindControl("chkSeriesEnabled"), CheckBox).Checked

            txtHiddenId.Text = lbChannelId.Text.Trim
            ddlCompanyName.Text = lbCompanyId.Text.Trim
            txtChannelName.Text = lbChannelId.Text.Trim
            Try
                ddlChannelLanguageid.SelectedValue = lbChannelLanguageId.Text
            Catch ex As Exception
                ddlChannelLanguageid.SelectedIndex = 0
            End Try
            ddlMovieLanguage.SelectedValue = lbMovieLangId.Text
            txtTRP.Text = lbTRP.Text

            txtChannelName.ReadOnly = True
            btnAddChannel.Text = "Update"
            txtLanguage.Text = lbChannelLanguage.Text.Trim
            cmbChannelGenre.SelectedValue = lbGenreId.Text.Trim
        Catch ex As Exception
            'myErrorBox("You cannot update this record!")
            Logger.LogError("Channel Master", "grdChannelmaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid()
    End Sub
    Private Sub bindGrid()

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("rptChannels", "search", "VarChar", txtSearch.Text, True, False)

        Session("dtGrid") = dt

        'If sqlDSChannelMaster.SelectParameters.Count > 0 And txtSearch.Text.Trim = "" Then
        '    sqlDSChannelMaster.SelectParameters.RemoveAt(0)
        'End If

        'If sqlDSChannelMaster.SelectParameters.Count = 0 And txtSearch.Text.Trim <> "" Then
        '    sqlDSChannelMaster.SelectParameters.Add("search", txtSearch.Text)
        'End If
        grdChannelmaster.DataSource = dt
        grdChannelmaster.PageIndex = 0
        grdChannelmaster.DataBind()
    End Sub

    Private Sub clearAll()
        txtHiddenId.Text = "0"
        txtChannelName.Text = ""
        txtChannelName.ReadOnly = False
        btnAddChannel.Text = "Add"
        txtLanguage.Text = ""
        txtTRP.Text = ""

        chkActive.Checked = True
        chkSendEPG.Checked = True
        chkCatchUpFlag.Checked = False
        chkSeriesEnabled.Checked = False
        chkMovieChannel.Checked = False

    End Sub

    Protected Sub grdChannelmaster_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdChannelmaster.Sorting
        Try
            Dim dt As DataTable = TryCast(Session("dtGrid"), DataTable)

            Dim sortdata As String
            If IsNothing(Session("sortExpression")) Then
                sortdata = ""
            Else
                sortdata = Session("sortExpression").ToString
            End If
            If sortdata = "" Then
                e.SortDirection = SortDirection.Ascending
            ElseIf sortdata = "ASC" Then
                e.SortDirection = SortDirection.Descending
            ElseIf sortdata = "DESC" Then
                e.SortDirection = SortDirection.Ascending
            End If
            If Not IsNothing(dt) Then
                Dim dv As DataView = New DataView(dt)
                If e.SortDirection = SortDirection.Ascending Then
                    dv.Sort = e.SortExpression + " ASC"
                    Session("sortExpression") = "ASC"
                Else
                    dv.Sort = e.SortExpression + " DESC"
                    Session("sortExpression") = "DESC"
                End If

                grdChannelmaster.DataSource = dv
                grdChannelmaster.DataBind()
                Dim dtnew As DataTable = dv.ToTable
                Session("dtGrid") = dtnew
            Else
                bindGrid()
            End If
        Catch
            bindGrid()
        End Try


    End Sub

    Protected Sub grdChannelmaster_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdChannelmaster.PageIndexChanging
        grdChannelmaster.PageIndex = e.NewPageIndex
        Dim dt As DataTable = TryCast(Session("dtGrid"), DataTable)


        If Not IsNothing(dt) Then
            Dim dv As DataView = New DataView(dt)
            grdChannelmaster.DataSource = dv
            grdChannelmaster.DataBind()
        End If

    End Sub
End Class