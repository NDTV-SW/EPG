Imports System
Imports System.Data.SqlClient
Public Class ProgramMaster
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

            If Page.IsPostBack = False Then
                grdProgrdProgrammaster1Bind()
                btnAddProgram.Visible = True
                btnCancel.Visible = True

                grdProgrammaster1.Columns(21).Visible = True

                grdProgrammaster1.Columns(22).Visible = False
            End If
        Catch ex As Exception
            Logger.LogError("Program Master", "Page Load", ex.Message.ToString, User.Identity.Name)
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
            lbError1.Visible = False
            lbError2.Visible = False

            Dim strShowAirTime As String = ddlHour.SelectedValue & ":" & ddlMinutes.SelectedValue
            Dim strShowAirTime1 As String = ddlHour1.SelectedValue & ":" & ddlMinutes1.SelectedValue
            Dim strShowAirTime2 As String = ddlHour2.SelectedValue & ":" & ddlMinutes2.SelectedValue

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
                Call exec_Proc(0, ddlChannelName.Text, txtProgramName.Text, ddlGenre.SelectedValue, subgenre, ddlParentalRatingMaster.Text.ToString.Trim, "A", chkSeries.Checked, chkCatchUp.Checked, strShowAirTime, strShowAirTime1, strShowAirTime2, duration, chkActive.Checked, chkEpisodicSynopsis.Checked, chkIsMovie.Checked, programtrp)
            ElseIf btnAddProgram.Text = "Update" Then
                Call exec_Proc(txtHiddenId.Text, ddlChannelName.Text, txtProgramName.Text, ddlGenre.SelectedValue, subgenre, ddlParentalRatingMaster.Text, "U", chkSeries.Checked, chkCatchUp.Checked, strShowAirTime, strShowAirTime1, strShowAirTime2, duration, chkActive.Checked, chkEpisodicSynopsis.Checked, chkIsMovie.Checked, programtrp)
                Dim obj As New clsExecute
                Dim sql As String
                sql = "update mst_program set "
                If chkEpisodicException.Checked Then
                    sql = sql & "exception=1"
                Else
                    sql = sql & "exception=0"
                End If
                If chkIsMovie.Checked Then
                    sql = sql & ",seasonno=0"
                Else
                    sql = sql & ",seasonno='" & txtSeason.Text & "'"
                End If
                sql = sql & ",preleaseyear='" & txtReleaseYear.Text & "'"
                sql = sql & ",subtitles='" & chkSubtitles.Checked & "'"
                If txtChainID.Text.Length > 2 Then
                    sql = sql & ",abs_serieslink='M'"
                Else
                    sql = sql & ",abs_serieslink=''"
                End If
                sql = sql & ",abs_chainid='" & txtChainID.Text & "'"
                sql = sql & " where progid='" & txtHiddenId.Text & "'"



                obj.executeSQL(sql, False)
            End If



            ddlHour.SelectedIndex = 0
            ddlMinutes.SelectedIndex = 0
            ddlHour1.SelectedIndex = 0
            ddlMinutes1.SelectedIndex = 0
            ddlHour2.SelectedIndex = 0
            ddlMinutes2.SelectedIndex = 0
            grdProgrdProgrammaster1Bind()
            clearData()
        Catch ex As Exception
            Logger.LogError("Program Master", "btnAddProgram_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub exec_Proc(ByVal ProgId As Integer, ByVal ChannelId As String, ByVal ProgName As String, ByVal GenreID As Integer, ByVal SubGenreID As Integer, ByVal RatingID As String, ByVal action As String, ByVal seriesenabled As Boolean, ByVal catchUpFlag As Boolean, ByVal ShowAirTime As String, ByVal ShowAirTime1 As String, ByVal ShowAirTime2 As String, ByVal Duration As Integer, ByVal active As Boolean, ByVal episodicSynopsis As Boolean, ByVal boolIsMovie As Boolean, ByVal programtrp As Integer)
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
            If Not ShowAirTime1 = "HH:MM" Then
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Showairtime1", SqlDbType.VarChar)).Value = ShowAirTime1.ToString.Trim
            End If
            If Not ShowAirTime2 = "HH:MM" Then
                MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Showairtime2", SqlDbType.VarChar)).Value = ShowAirTime2.ToString.Trim
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

            If action = "A" Then
                Dim obj As New clsExecute
                Dim PID As String
                Dim dt As DataTable = obj.executeSQL("SELECT PROGID FROM MST_PROGRAM WHERE PROGNAME='" & ProgName.Replace("'", "''") & "' and channelid='" & ChannelId & "'  ORDER BY PROGID DESC", False)

                PID = dt.Rows(0)("PROGID").ToString

                obj.executeSQL("sp_mst_ProgramRegional", "ProgID~ProgName~Synopsis~LanguageId~RowID~Action~ActionUser",
                               "int~nvarchar~nvarchar~int~int~char,varchar", PID & "~" & ProgName & "~~1~0~" & action & "~" & User.Identity.Name, True, False)

            End If
        Catch ex As Exception
            Logger.LogError("Program Master", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdProgrammaster1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdProgrammaster1.SelectedIndexChanged
        Try
            lbError.Visible = False
            lbError1.Visible = False
            lbError2.Visible = False
            Dim lbProgId As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbProgId"), Label)
            Dim lbProgname As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbProgname"), Label)
            Dim lbGenreID As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbGenreID"), Label)
            Dim lbSubGenreID As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbSubGenreID"), Label)
            Dim lbRatingID As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbRatingID"), Label)
            Dim lbSeriesenabled As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbSeriesenabled"), Label)
            Dim lbCatchUpFlag As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbCatchUpFlag"), Label)
            Dim lbActive As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbActive"), Label)
            Dim lbshowairtime As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbshowairtime"), Label)
            Dim lbshowairtime1 As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbshowairtime1"), Label)
            Dim lbshowairtime2 As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbshowairtime2"), Label)
            Dim lbDuration As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbDuration"), Label)
            Dim lbprogramtrp As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbprogramtrp"), Label)
            Dim lbepisodicSynopsis As Label = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbepisodicSynopsis"), Label)

            chkIsMovie.Checked = DirectCast(grdProgrammaster1.SelectedRow.FindControl("chkMovie"), CheckBox).Checked
            chkSubtitles.Checked = DirectCast(grdProgrammaster1.SelectedRow.FindControl("chkSubtitles"), CheckBox).Checked
            chkEpisodicException.Checked = DirectCast(grdProgrammaster1.SelectedRow.FindControl("chkException"), CheckBox).Checked
            txtSeason.Text = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbSeason"), Label).Text
            txtReleaseYear.Text = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbReleaseYear"), Label).Text

            txtChainID.Text = DirectCast(grdProgrammaster1.SelectedRow.FindControl("lbChainID"), Label).Text


            txtHiddenId.Text = Server.HtmlDecode(lbProgId.Text)
            txtProgramName.Text = Server.HtmlDecode(lbProgname.Text.Trim)
            txtDuration.Text = lbDuration.Text
            txtprogramtrp.Text = lbprogramtrp.Text

            Try
                Dim strShowairtime As String = Convert.ToDateTime(lbshowairtime.Text).ToString("HH:mm")
                ddlHour.SelectedValue = lbshowairtime.Text.Substring(0, 2)
                ddlMinutes.SelectedValue = lbshowairtime.Text.Substring(3, 2)
            Catch
                ddlHour.SelectedIndex = 0
                ddlMinutes.SelectedIndex = 0
            End Try

            Try
                Dim strShowairtime1 As String = Convert.ToDateTime(lbshowairtime1.Text).ToString("HH:mm")
                ddlHour1.SelectedValue = lbshowairtime1.Text.Substring(0, 2)
                ddlMinutes1.SelectedValue = lbshowairtime1.Text.Substring(3, 2)
            Catch
                ddlHour1.SelectedIndex = 0
                ddlMinutes1.SelectedIndex = 0
            End Try

            Try
                Dim strShowairtime2 As String = Convert.ToDateTime(lbshowairtime2.Text).ToString("HH:mm")
                ddlHour2.SelectedValue = lbshowairtime2.Text.Substring(0, 2)
                ddlMinutes2.SelectedValue = lbshowairtime2.Text.Substring(3, 2)
            Catch
                ddlHour2.SelectedIndex = 0
                ddlMinutes2.SelectedIndex = 0
            End Try


            Dim item As ListItem = ddlGenre.Items.FindByValue(Server.HtmlDecode(lbGenreID.Text.Trim))
            If item IsNot Nothing Then
                ddlGenre.SelectedValue = Server.HtmlDecode(lbGenreID.Text.Trim)
            End If
            ddlSubGenre.DataBind()
            If Not Server.HtmlDecode(lbSubGenreID.Text.Trim) = "0" Then
                Try
                    ddlSubGenre.SelectedValue = Server.HtmlDecode(lbSubGenreID.Text.Trim)
                Catch ex As Exception
                End Try

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
            Logger.LogError("Program Master", "grdProgrammaster1_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Private Sub grdProgrammaster1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdProgrammaster1.RowDeleting
        Try
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_program", MyConnection)
            Dim lbProgId As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbProgId"), Label)
            Dim lbProgname As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbProgname"), Label)
            Dim lbGenreID As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbGenreID"), Label)
            Dim lbSubGenreID As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbSubGenreID"), Label)
            Dim lbRatingID As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbRatingID"), Label)
            Dim lbSeriesenabled As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbSeriesenabled"), Label)
            Dim lbCatchupFlag As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbCatchupFlag"), Label)
            Dim lbActive As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbActive"), Label)
            Dim lbepisodicSynopsis As Label = DirectCast(grdProgrammaster1.Rows(e.RowIndex).FindControl("lbepisodicSynopsis"), Label)

            Dim obj As New clsExecute
            obj.executeSQL("sp_mst_program", "ProgID~ChannelId~ProgName~GenreID~SubGenreID~RatingID~Seriesenabled~CatchupFlag~ActionUser~active~episodicSynopsis~Action",
                           "int~nvarchar~nvarchar~int~int~varchar~bit~bit~varchar~bit~bit~char",
                           lbProgId.Text & "~" & ddlChannelName.Text & "~" & lbProgname.Text & "~" & lbGenreID.Text & "~" & lbSubGenreID.Text & "~" & lbRatingID.Text & "~" &
                           lbSeriesenabled.Text & "~" & lbCatchupFlag.Text & "~" & User.Identity.Name.ToString & "~" & lbActive.Text & "~" & lbepisodicSynopsis.Text & "~D", True, False)


        Catch ex As Exception
            Logger.LogError("Program Master", "grdProgrammaster1_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdProgrdProgrammaster1Bind()
        Try
            Dim sql As String = ""
            If ddlSearchBy.SelectedValue = "progname" Then
                If txtProgName.Text <> "" Then
                    sql = "SELECT distinct a.ProgId,isnull(a.exception,0) exception,a.seasonno,a.preleaseyear,a.abs_chainid,isnull(a.subtitles,0) subtitles,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime, isnull( convert(varchar,a.showairtime2,108),'') showairtime2, isnull( convert(varchar,a.showairtime3,108),'') showairtime3, a.duration,a.programtrp, a.episodicSynopsis "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid where a.ChannelID='" & ddlChannelName.SelectedValue.ToString & "' and a.active='1' "
                    sql = sql & " and e.LanguageId='1' and e.episodeno=0 and a.progname like '%" & txtProgName.Text.Replace("'", "''") & "%'"
                    If chkInEPG.Checked = True Then
                        sql = sql & " and a.progid in (select distinct progid from mst_epg where progdate > dateadd(day,-1, dbo.GetLocalDate()))"
                    End If
                    sql = sql & " order by progname"
                Else
                    sql = "SELECT  distinct a.ProgId,isnull(a.exception,0) exception,a.seasonno,a.preleaseyear,a.abs_chainid,isnull(a.subtitles,0) subtitles,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime,isnull( convert(varchar,a.showairtime2,108),'') showairtime2,isnull( convert(varchar,a.showairtime3,108),'') showairtime3, a.duration,a.programtrp, a.episodicSynopsis "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid where a.ChannelID='" & ddlChannelName.SelectedValue.ToString & "' and a.active='1' "
                    sql = sql & " and e.episodeno=0 and e.LanguageId='1'"
                    If chkInEPG.Checked = True Then
                        sql = sql & " and a.progid in (select distinct progid from mst_epg where progdate > dateadd(day,-1, dbo.GetLocalDate()))"
                    End If
                    sql = sql & " order by progname"
                End If
            ElseIf ddlSearchBy.SelectedValue = "progid" Then

                If txtProgName.Text <> "" Then
                    sql = "SELECT distinct a.ProgId,isnull(a.exception,0) exception,a.seasonno,a.preleaseyear,a.abs_chainid,isnull(a.subtitles,0) subtitles,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime, isnull( convert(varchar,a.showairtime2,108),'') showairtime2, isnull( convert(varchar,a.showairtime3,108),'') showairtime3, a.duration,a.programtrp, a.episodicSynopsis "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid where a.active='1' "
                    sql = sql & " and e.LanguageId='1' and e.episodeno=0 and a.progid='" & txtProgName.Text.Replace("'", "''") & "'"
                    sql = sql & " order by progname"
                End If


            Else
                If txtProgName.Text <> "" Then
                    sql = "SELECT distinct a.ProgId,isnull(a.exception,0) exception,a.seasonno,a.preleaseyear,a.abs_chainid,isnull(a.subtitles,0) subtitles,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime, isnull( convert(varchar,a.showairtime2,108),'') showairtime2, isnull( convert(varchar,a.showairtime3,108),'') showairtime3, a.duration,a.programtrp, a.episodicSynopsis  "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid where a.ChannelID='" & ddlChannelName.SelectedValue.ToString & "' and a.active='1' "
                    sql = sql & " and e.LanguageId='1' and e.episodeno=0 and synopsis like '%" & txtProgName.Text.Replace("'", "''") & "%' "
                    If chkInEPG.Checked = True Then
                        sql = sql & " and a.progid in (select distinct progid from mst_epg where progdate > dateadd(day,-1, dbo.GetLocalDate()))"
                    End If
                    sql = sql & " order by progname"
                Else
                    sql = "SELECT distinct a.ProgId,isnull(a.exception,0) exception,a.seasonno,a.preleaseyear,a.abs_chainid,isnull(a.subtitles,0) subtitles,a.seriesenabled,a.catchupflag,a.active,isnull(a.ismovie,0) ismovie, a.Progname,case when episodicsynopsis = 0 then e.synopsis else (select synopsis from mst_programregional x where x.progid = e.progid and episodeNo = 0 and x.LanguageId = e.LanguageId) end synopsis, isnull(a.genreid,0) GenreId, isnull(a.subgenreid,0) SubGenreId, isnull(a.ratingid,0) RatingId, isnull(b.genrename,'') GenreName, isnull(c.subgenrename,'') SubGenreName, isnull( convert(varchar,a.showairtime,108),'') showairtime, isnull( convert(varchar,a.showairtime2,108),'') showairtime2, isnull( convert(varchar,a.showairtime3,108),'') showairtime3, a.duration,a.programtrp, a.episodicSynopsis  "
                    sql = sql & "FROM mst_Program a left outer join mst_genre b on a.GenreId = b.GenreId left outer join mst_subGenre c on a.SubGenreID=c.SubGenreID left outer join mst_ParentalRating d on a.RatingID = d.RatingID "
                    sql = sql & "left outer join mst_ProgramRegional e on a.ProgID = e.progid where a.ChannelID='" & ddlChannelName.SelectedValue.ToString & "' and a.active='1' "
                    sql = sql & " and e.episodeno=0 and e.LanguageId='1' "
                    If chkInEPG.Checked = True Then
                        sql = sql & " and a.progid in (select distinct progid from mst_epg where progdate > dateadd(day,-1, dbo.GetLocalDate()))"
                    End If
                    sql = sql & " order by progname"
                End If
            End If

            Dim obj As New clsExecute
            Dim dt As DataTable
            If ddlSearchBy.SelectedValue <> "progid" Then
                dt = obj.executeSQL(sql, False)
                grdProgrammaster1.DataSource = dt
                grdProgrammaster1.DataBind()
            End If
            Try
                If ddlSearchBy.SelectedValue = "progid" Then

                    dt = obj.executeSQL(sql, False)
                    grdProgrammaster1.DataSource = dt
                    grdProgrammaster1.DataBind()
                End If
            Catch
                myErrorBox("Please enter a number for Progid")
            End Try
        Catch ex As Exception
            Logger.LogError("Program Master", "grdProgrdProgrammaster1Bind", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

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
            txtSeason.Text = ""
            chkSubtitles.Checked = False
            chkCatchUp.Checked = False
            chkActive.Checked = True
            chkCatchUp.Checked = False
            chkEpisodicException.Checked = False
            txtReleaseYear.Text = ""

            txtChainID.Text = ""


            btnAddProgram.Text = "Add"
            grdProgrammaster1.SelectedIndex = -1
        Catch ex As Exception
            Logger.LogError("Program Master", "clearData", ex.Message.ToString, User.Identity.Name)
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
            Logger.LogError("Program Master", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdProgrammaster1_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdProgrammaster1.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub
    Private Sub grdProgrammaster1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgrammaster1.RowDataBound
        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                'Dim LnkBtnDeleteProgramMaster As ImageButton = DirectCast(e.Row.Cells(10).Controls(0), ImageButton)
                'LnkBtnDeleteProgramMaster.OnClientClick = "return confirm('All entries of this program in EPG master will be deleted. Do you still want to continue ?');"
                'dr.Close()
        End Select
    End Sub

    Private Sub ddlChannelName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlChannelName.SelectedIndexChanged
        Try
            clearData()
            grdProgrdProgrammaster1Bind()
            ddlGenre.DataBind()
            ddlSubGenre.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Master", "ddlChannelName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            grdProgrammaster1.PageIndex = 1
            grdProgrdProgrammaster1Bind()
        Catch ex As Exception
            Logger.LogError("Program Master", "btnSearch_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdProgrdProgrammaster_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProgrammaster1.PageIndexChanging
        Try
            grdProgrammaster1.PageIndex = e.NewPageIndex
            grdProgrdProgrammaster1Bind()
        Catch ex As Exception
            Logger.LogError("Program Master", "grdProgrdProgrammaster_PageIndexChanging", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class