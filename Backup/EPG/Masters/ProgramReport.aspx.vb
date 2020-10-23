Imports System
Imports System.Data.SqlClient
Public Class ProgramReport
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & errorstr & "');", True)
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
                txtAddedAfterDate.Text = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy")
                ddlLanguage.SelectedValue = "2"
                BindData(False)
                
                'If User.Identity.Name = "mverx" Then

                grdProgrammaster1.Columns(19).Visible = False
            End If
        Catch ex As Exception
            Logger.LogError("Program Report", "Page Load", ex.Message.ToString, User.Identity.Name)
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

    Private Sub btnAddProgram_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProgram.Click
        Try
            lbError.Visible = False

            Dim strShowAirTime As String = ddlHour.SelectedValue.ToString & ":" & ddlMinutes.SelectedValue.ToString
            Dim subgenre As Integer
            If ddlSubGenre.SelectedValue.ToString = "" Then
                subgenre = 0
            Else
                subgenre = ddlSubGenre.SelectedValue
            End If
            Dim duration As Integer, programtrp As Integer
            If txtDuration.Text = "" Then
                duration = 0
            Else
                duration = Convert.ToInt32(txtDuration.Text)
            End If
            If txtprogramtrp.Text = "" Then
                programtrp = 0
            Else
                programtrp = Convert.ToInt32(txtprogramtrp.Text)
            End If

            If btnAddProgram.Text = "Add" Then
                Call exec_Proc(0, ddlChannelName.Text, txtProgramName.Text, ddlGenre.SelectedValue, subgenre, ddlParentalRatingMaster.Text.ToString.Trim, "A", chkSeries.Checked, chkCatchUp.Checked, strShowAirTime, duration, chkActive.Checked, chkEpisodicSynopsis.Checked, chkIsMovie.Checked, programtrp)
            ElseIf btnAddProgram.Text = "Update" Then
                Call exec_Proc(txtHiddenId.Text, ddlChannelName.Text, txtProgramName.Text, ddlGenre.SelectedValue, subgenre, ddlParentalRatingMaster.Text, "U", chkSeries.Checked, chkCatchUp.Checked, strShowAirTime, duration, chkActive.Checked, chkEpisodicSynopsis.Checked, chkIsMovie.Checked, programtrp)
            End If

            ddlHour.SelectedIndex = 0
            ddlMinutes.SelectedIndex = 0
            BindData(True)
            clearData()
        Catch ex As Exception
            Logger.LogError("Program Report", "btnAddProgram_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub exec_Proc(ByVal ProgId As Integer, ByVal ChannelId As String, ByVal ProgName As String, ByVal GenreID As Integer, ByVal SubGenreID As Integer, ByVal RatingID As String, ByVal action As String, ByVal seriesenabled As Boolean, ByVal catchUpFlag As Boolean, ByVal ShowAirTime As String, ByVal Duration As Integer, ByVal active As Boolean, ByVal episodicSynopsis As Boolean, ByVal boolIsMovie As Boolean, ByVal programtrp As Integer)
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
            If Not ShowAirTime = "HH:MM" Then
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Showairtime", SqlDbType.VarChar)).Value = ShowAirTime.ToString.Trim
            End If
            If Not Duration = 0 Then
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Duration", SqlDbType.Int, 8)).Value = Duration
            End If
            If Not programtrp = 0 Then
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@programtrp", SqlDbType.Int, 8)).Value = programtrp
            End If
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@CatchupFlag", SqlDbType.Bit, 8)).Value = catchUpFlag.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@isMovie", SqlDbType.Bit, 8)).Value = boolIsMovie
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@EpisodicSynopsis", SqlDbType.Bit, 8)).Value = episodicSynopsis
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@active", SqlDbType.Bit)).Value = active.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetCompany")
            MyDataAdapter.Dispose()
            MyConnection.Close()

            If MyConnection.State = ConnectionState.Closed Then
                MyConnection.Open()
            End If
            Dim PID As String
            Dim cmd As New SqlCommand("SELECT PROGID FROM MST_PROGRAM WHERE PROGNAME='" & ProgName.ToString.Trim & "'  ORDER BY PROGID DESC", MyConnection)
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            dr.Read()
            PID = dr("PROGID").ToString
            dr.Close()
            dr.Dispose()
            MyConnection.Close()

            If action = "A" Then
                Dim DS1 As DataSet
                Dim MyDataAdapter1 As SqlDataAdapter
                Dim MyConnection1 As New SqlConnection
                MyConnection = New SqlConnection(ConString)
                MyDataAdapter1 = New SqlDataAdapter("sp_mst_ProgramRegional", MyConnection)
                MyDataAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure
                MyDataAdapter1.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = PID.ToString.Trim
                MyDataAdapter1.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = ProgName.ToString.Trim
                MyDataAdapter1.SelectCommand.Parameters.Add(New SqlParameter("@Synopsis", SqlDbType.NVarChar, 400)).Value = ""
                MyDataAdapter1.SelectCommand.Parameters.Add(New SqlParameter("@LanguageId", SqlDbType.Int, 8)).Value = 1
                MyDataAdapter1.SelectCommand.Parameters.Add(New SqlParameter("@RowID", SqlDbType.Int, 8)).Value = 0
                MyDataAdapter1.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
                MyDataAdapter1.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name
                DS1 = New DataSet()
                MyDataAdapter1.Fill(DS1, "GetRegionalProgName")
                MyDataAdapter1.Dispose()
                MyConnection.Close()
            End If
        Catch ex As Exception
            Logger.LogError("Program Report", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdProgrammaster1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdProgrammaster1.SelectedIndexChanged
        Try
            lbError.Visible = False
            Dim lbProgId As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbProgId"), Label)
            Dim lbChannelid As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbChannelid"), Label)
            Dim lbProgname As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbProgname"), Label)
            Dim lbGenreID As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbGenreID"), Label)
            Dim lbSubGenreID As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbSubGenreID"), Label)
            Dim lbRatingID As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbRatingID"), Label)
            Dim lbSeriesenabled As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbSeriesenabled"), Label)
            Dim lbCatchUpFlag As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbCatchUpFlag"), Label)
            Dim lbActive As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbActive"), Label)
            Dim lbshowairtime As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbshowairtime"), Label)
            Dim lbDuration As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbDuration"), Label)
            Dim lbprogramtrp As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbprogramtrp"), Label)
            Dim lbepisodicSynopsis As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbepisodicSynopsis"), Label)
            chkIsMovie.Checked = DirectCast(grdProgrammaster1.SelectedRow.FindControl("chkMovie"), CheckBox).Checked


            txtHiddenId.Text = Server.HtmlDecode(lbProgId.Text)
            ddlChannelName.SelectedValue = lbChannelid.Text
            txtProgramName.Text = Server.HtmlDecode(lbProgname.Text.Trim)
            txtDuration.Text = lbDuration.Text
            txtprogramtrp.Text = lbprogramtrp.Text

            Try
                Dim strShowairtime As String = Convert.ToDateTime(lbshowairtime.Text).ToString("HH:mm")
                ddlHour.SelectedValue = lbshowairtime.Text.Substring(0, 2)
                ddlMinutes.SelectedValue = lbshowairtime.Text.Substring(3, 2)
            Catch

            End Try
            Dim item As ListItem = ddlGenre.Items.FindByValue(Server.HtmlDecode(lbGenreID.Text.Trim))
            If item IsNot Nothing Then
                ddlGenre.SelectedValue = Server.HtmlDecode(lbGenreID.Text.Trim)
            End If
            ddlSubGenre.DataBind()
            If Not Server.HtmlDecode(lbSubGenreID.Text.Trim) = "0" Then
                ddlSubGenre.SelectedValue = Server.HtmlDecode(lbSubGenreID.Text.Trim)
            End If
            If Not Server.HtmlDecode(lbRatingID.Text.Trim).Trim = "0" Then
                ddlParentalRatingMaster.SelectedValue = Server.HtmlDecode(lbRatingID.Text.Trim)
            End If
            If lbActive.Text.Trim = "True" Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If

            If lbSeriesenabled.Text.Trim = "True" Then
                chkSeries.Checked = True
            Else
                chkSeries.Checked = False
            End If

            If lbCatchUpFlag.Text.Trim = "True" Then
                chkCatchUp.Checked = True
            Else
                chkCatchUp.Checked = False
            End If

            If lbepisodicSynopsis.Text.Trim = "True" Then
                chkEpisodicSynopsis.Checked = True
            Else
                chkEpisodicSynopsis.Checked = False
            End If

            btnAddProgram.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Program Report", "grdProgrammaster1_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    'Private Sub grdProgrammaster1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdProgrammaster1.RowDeleting
    '    Try
    '        Dim DS As DataSet
    '        Dim MyDataAdapter As SqlDataAdapter
    '        Dim MyConnection As New SqlConnection
    '        MyConnection = New SqlConnection(ConString)
    '        MyDataAdapter = New SqlDataAdapter("sp_mst_program", MyConnection)
    '        Dim lbProgId As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbProgId"), Label)
    '        Dim lbProgname As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbProgname"), Label)
    '        Dim lbGenreID As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbGenreID"), Label)
    '        Dim lbSubGenreID As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbSubGenreID"), Label)
    '        Dim lbRatingID As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbRatingID"), Label)
    '        Dim lbSeriesenabled As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbSeriesenabled"), Label)
    '        Dim lbCatchupFlag As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbCatchupFlag"), Label)
    '        Dim lbActive As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbActive"), Label)
    '        Dim lbepisodicSynopsis As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbepisodicSynopsis"), Label)

    '        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int, 8)).Value = lbProgId.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ChannelId", SqlDbType.NVarChar, 50)).Value = ddlChannelName.Text.ToString.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ProgName", SqlDbType.NVarChar, 400)).Value = lbProgname.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreID", SqlDbType.Int, 8)).Value = lbGenreID.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SubGenreID", SqlDbType.Int, 8)).Value = lbSubGenreID.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RatingID", SqlDbType.VarChar, 10)).Value = lbRatingID.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Seriesenabled", SqlDbType.Bit)).Value = lbSeriesenabled.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@CatchupFlag", SqlDbType.Bit)).Value = lbCatchupFlag.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@active", SqlDbType.Bit)).Value = lbActive.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@episodicSynopsis", SqlDbType.Bit)).Value = lbepisodicSynopsis.Text.Trim
    '        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "D"
    '        DS = New DataSet()
    '        MyDataAdapter.Fill(DS, "GetCompany")
    '        MyDataAdapter.Dispose()
    '        MyConnection.Close()
    '    Catch ex As Exception
    '        Logger.LogError("Program Report", "grdProgrammaster1_RowDeleting", ex.Message.ToString, User.Identity.Name)
    '    End Try
    'End Sub



    Sub clearData()
        Try
            txtHiddenId.Text = "0"
            txtProgramName.Text = ""
            txtDuration.Text = ""
            txtprogramtrp.Text = ""
            ddlGenre.SelectedIndex = -1
            ddlSubGenre.SelectedIndex = -1
            ddlParentalRatingMaster.SelectedIndex = -1
            chkSeries.Checked = False
            chkIsMovie.Checked = False
            chkEpisodicSynopsis.Checked = False
            chkCatchUp.Checked = False
            chkActive.Checked = True
            chkCatchUp.Checked = False
            btnAddProgram.Text = "Add"
            grdProgrammaster1.SelectedIndex = -1
        Catch ex As Exception
            Logger.LogError("Program Report", "clearData", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Private Sub ddlGenre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGenre.SelectedIndexChanged
        ddlSubGenre.DataBind()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            ddlHour.SelectedIndex = 0
            ddlMinutes.SelectedIndex = 0
            grdProgrammaster1.SelectedIndex = -1
            clearData()
        Catch ex As Exception
            Logger.LogError("Program Report", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    'Private Sub grdProgrammaster1_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdProgrammaster1.RowDeleted
    '    If Not e.Exception Is Nothing Then
    '        'myErrorBox("You can not delete this record.")
    '        e.ExceptionHandled = True
    '    End If
    'End Sub


    'Private Sub ddlChannelName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlChannelName.SelectedIndexChanged
    '    Try
    '        clearData()
    '        BindData()
    '        ddlGenre.DataBind()
    '        ddlSubGenre.DataBind()
    '    Catch ex As Exception
    '        Logger.LogError("Program Report", "ddlChannelName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
    '    End Try
    'End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            'grdProgrammaster1.PageIndex = 1
            BindData(False)
        Catch ex As Exception
            Logger.LogError("Program Report", "btnSearch_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub BindData(ByVal paging As Boolean)

        grdProgrammaster1.DataSource = Me.grdProgrdProgrammaster1Bind()
        If Not paging Then
            grdProgrammaster1.PageIndex = 0
        End If
        grdProgrammaster1.DataBind()

        If (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
            btnAddProgram.Visible = True
            btnCancel.Visible = True
            grdProgrammaster1.Columns(18).Visible = True
        ElseIf (User.IsInRole("USER")) Then
            btnAddProgram.Visible = True
            btnCancel.Visible = True
            grdProgrammaster1.Columns(18).Visible = True
        Else
            btnAddProgram.Visible = False
            btnCancel.Visible = False
            grdProgrammaster1.Columns(18).Visible = False
            If (User.IsInRole("HINDI") Or User.IsInRole("ENGLISH") Or User.IsInRole("MARATHI") Or User.IsInRole("TELUGU") Or User.IsInRole("TAMIL")) Then
                grdProgrammaster1.Columns(18).Visible = False
                btnAddProgram.Visible = False
                btnCancel.Visible = False
            End If
        End If
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortExpression As String = e.SortExpression
        Dim direction As String = String.Empty
        If SortDirection = SortDirection.Ascending Then
            SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If
        Dim table As DataTable = Me.grdProgrdProgrammaster1Bind()
        table.DefaultView.Sort = sortExpression & direction
        grdProgrammaster1.DataSource = table
        grdProgrammaster1.DataBind()
    End Sub

    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property


    Private Function grdProgrdProgrammaster1Bind() As DataTable
        Dim dt As DataTable = Nothing
        Try

            Dim sql As String

            If ddlSearchBy.SelectedValue = "progname" Then
                If txtProgName.Text <> "" Then
                    sql = "SELECT distinct a.ProgId,a.channelid,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime, a.duration,a.programtrp,cast(f.TRP as int) trp, a.episodicSynopsis, a.program_added_timestamp "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid left outer join mst_channel f on f.channelid=a.channelid where f.channellanguageid='" & ddlLanguage.SelectedValue & "' and convert(varchar, program_added_timestamp,112) >'" & Convert.ToDateTime(txtAddedAfterDate.Text).ToString("yyyyMMdd") & "' and a.active='1' "
                    sql = sql & " and e.LanguageId='1' and e.episodeno=0 and a.progname like '%" & txtProgName.Text.Replace("'", "''") & "%'"
                    If chkInEPG.Checked = True Then
                        sql = sql & " and a.progid in (select distinct progid from mst_epg where progdate > dateadd(day,-1, dbo.GetLocalDate()))"
                    End If
                    If ddlMovieChannel.SelectedValue = "1" Then
                        sql = sql & " and f.movie_channel=1 "
                    Else
                        sql = sql & " and f.movie_channel=0 "
                    End If
                    sql = sql & " order by trp desc"
                Else
                    sql = "SELECT  distinct a.ProgId,a.channelid,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime, a.duration,a.programtrp,cast(f.TRP as int) trp, a.episodicSynopsis, a.program_added_timestamp "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid left outer join mst_channel f on f.channelid=a.channelid where f.channellanguageid='" & ddlLanguage.SelectedValue & "' and convert(varchar, program_added_timestamp,112) >'" & Convert.ToDateTime(txtAddedAfterDate.Text).ToString("yyyyMMdd") & "' and a.active='1' "
                    sql = sql & " and e.episodeno=0 and e.LanguageId='1'"
                    If chkInEPG.Checked = True Then
                        sql = sql & " and a.progid in (select distinct progid from mst_epg where progdate > dateadd(day,-1, dbo.GetLocalDate()))"
                    End If
                    If ddlMovieChannel.SelectedValue = "1" Then
                        sql = sql & " and f.movie_channel=1 "
                    Else
                        sql = sql & " and f.movie_channel=0 "
                    End If
                    sql = sql & " order by trp desc"
                End If
            Else
                If txtProgName.Text <> "" Then
                    sql = "SELECT distinct a.ProgId,a.channelid,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime, a.duration,a.programtrp,cast(f.TRP as int) trp, a.episodicSynopsis, a.program_added_timestamp  "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid left outer join mst_channel f on f.channelid=a.channelid where f.channellanguageid='" & ddlLanguage.SelectedValue & "' and convert(varchar, program_added_timestamp,112) >'" & Convert.ToDateTime(txtAddedAfterDate.Text).ToString("yyyyMMdd") & "' and a.active='1' "
                    sql = sql & " and e.LanguageId='1' and e.episodeno=0 and synopsis like '%" & txtProgName.Text.Replace("'", "''") & "%' "
                    If chkInEPG.Checked = True Then
                        sql = sql & " and a.progid in (select distinct progid from mst_epg where progdate > dateadd(day,-1, dbo.GetLocalDate()))"
                    End If
                    If ddlMovieChannel.SelectedValue = "1" Then
                        sql = sql & " and f.movie_channel=1 "
                    Else
                        sql = sql & " and f.movie_channel=0 "
                    End If
                    sql = sql & " order by trp desc"
                Else
                    sql = "SELECT distinct a.ProgId,a.channelid,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime, a.duration,a.programtrp,cast(f.TRP as int) trp, a.episodicSynopsis, a.program_added_timestamp  "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid left outer join mst_channel f on f.channelid=a.channelid where f.channellanguageid='" & ddlLanguage.SelectedValue & "' and convert(varchar, program_added_timestamp,112) >'" & Convert.ToDateTime(txtAddedAfterDate.Text).ToString("yyyyMMdd") & "' and a.active='1' "
                    sql = sql & " and e.episodeno=0 and e.LanguageId='1' "
                    If chkInEPG.Checked = True Then
                        sql = sql & " and a.progid in (select distinct progid from mst_epg where progdate > dateadd(day,-1, dbo.GetLocalDate()))"
                    End If
                    If ddlMovieChannel.SelectedValue = "1" Then
                        sql = sql & " and f.movie_channel=1 "
                    Else
                        sql = sql & " and f.movie_channel=0 "
                    End If
                    sql = sql & " order by trp desc"
                End If
            End If

            Dim obj As New clsExecute
            dt = obj.executeSQL(sql, False)
            Return dt
            'grdProgrammaster1.DataSource = dt
            'grdProgrammaster1.DataBind()

        Catch ex As Exception
            Logger.LogError("Program Report", "grdProgrdProgrammaster1Bind", ex.Message.ToString, User.Identity.Name)
            Return dt
        End Try
    End Function

    Protected Sub grdProgrdProgrammaster_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProgrammaster1.PageIndexChanging
        Try
            grdProgrammaster1.PageIndex = e.NewPageIndex
            BindData(True)
        Catch ex As Exception
            Logger.LogError("Program Report", "grdProgrdProgrammaster_PageIndexChanging", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class