Imports System.Data
Imports System.Data.SqlClient

Public Class clsUploadModules
    Public Sub clsUploadModules()
    End Sub

    Public Function insertFakeBuildEPG(ByVal ChannelId As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal uploadedBy As String) As Boolean
        Dim obj As New clsExecute
        Dim dtTemp As DateTime = StartDate
        Do While dtTemp <= EndDate
            obj.executeSQL("insert into mst_build_epg_transactions(channelid,epgdate,lastupdate,uploadedby) values('" & ChannelId & "','" & dtTemp & "',dbo.getlocaldate(),'" & uploadedBy & "')", False)
            dtTemp = dtTemp.AddDays(1)
        Loop
    End Function

    Public Function BuildEPG(ByVal ChannelId As String, ByVal boolupdateSynopsis As Boolean, ByVal boolTentative As Boolean, ByVal uploadedBy As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("sp_mst_epg_getdata_v1", "ChannelId~updateSynopsis~Tentative~uploadedby", "VarChar~Bit~Bit~VarChar", ChannelId & "~" & True & "~" & boolTentative & "~" & uploadedBy, True, False)

            Dim dt As DataTable = obj.executeSQL("select isnull(datediff(DD,dbo.GetLocalDate(), max([date])),0) days,min([date]) minDate,max([date]) maxDate from map_EPGExcel where channelId='" & ChannelId & "'", False)
            obj.executeSQL("insert into adp_heartbeat(channelid,fromdate,todate,rdsupdate,stepname) values('" & ChannelId & "','" & dt.Rows(0)("minDate").ToString & "','" & dt.Rows(0)("maxDate").ToString & "',dbo.getlocaldate(),'sync')", False)

            Dim dtFrom As Date, dtTo As Date
            dtFrom = DateTime.Now.AddDays(0).Date
            Dim intDays As Integer = Convert.ToInt16(dt.Rows(0)("days").ToString) + 1
            If intDays > 10 Then
                dtTo = DateTime.Now.AddDays(intDays).Date
            Else
                dtTo = DateTime.Now.AddDays(9).Date
            End If

            'obj.executeSQL("fixOverlapping_Channel", "ChannelId~FromDate~ToDate", "VarChar~DateTime~DateTime", ChannelId & "~" & dtFrom & "~" & dtTo, True, False)
            obj.executeSQL("sp_fix_duration_mismatch_channel", "vchannelid", "VarChar", ChannelId, True, False)

            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~1", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~2", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~4", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~7", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~8", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~11", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~12", True, False)

            obj.executeSQL("sp_mst_epg_consolidate_forApp", "ChannelId~FromDate~ToDate", "VarChar~DateTime~DateTime", ChannelId & "~" & dtFrom & "~" & dtTo, True, False)
            HttpContext.Current.Cache.Remove(ChannelId)
            HttpContext.Current.Cache.Remove(ChannelId & "_rich")
            Return True
        Catch ex As Exception
            Logger.LogError("clsUploadModules", "btnBuildEPG_Click - " & ChannelId, ex.Message.ToString, "clsUploadModules")
            Return False
        End Try
    End Function


    Public Function BuildEPGStar(ByVal ChannelId As String, ByVal boolupdateSynopsis As Boolean, ByVal boolTentative As Boolean, ByVal uploadedBy As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("sp_mst_epg_getdata_v1_star", "ChannelId~updateSynopsis~Tentative~uploadedby", "VarChar~Bit~Bit~VarChar", ChannelId & "~" & True & "~" & boolTentative & "~" & uploadedBy, True, False)

            Dim dt As DataTable = obj.executeSQL("select isnull(datediff(DD,dbo.GetLocalDate(), max([date])),0) days,min([date]) minDate,max([date]) maxDate from map_EPGExcel where channelId='" & ChannelId & "'", False)
            obj.executeSQL("insert into adp_heartbeat(channelid,fromdate,todate,rdsupdate,stepname) values('" & ChannelId & "','" & dt.Rows(0)("minDate").ToString & "','" & dt.Rows(0)("maxDate").ToString & "',dbo.getlocaldate(),'sync')", False)

            Dim dtFrom As Date, dtTo As Date
            dtFrom = DateTime.Now.AddDays(0).Date
            Dim intDays As Integer = Convert.ToInt16(dt.Rows(0)("days").ToString) + 1
            If intDays > 10 Then
                dtTo = DateTime.Now.AddDays(intDays).Date
            Else
                dtTo = DateTime.Now.AddDays(9).Date
            End If

            'obj.executeSQL("fixOverlapping_Channel", "ChannelId~FromDate~ToDate", "VarChar~DateTime~DateTime", ChannelId & "~" & dtFrom & "~" & dtTo, True, False)
            obj.executeSQL("sp_fix_duration_mismatch_channel", "vchannelid", "VarChar", ChannelId, True, False)

            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~1", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~2", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~4", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~7", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~8", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~11", True, False)
            obj.executeSQL("epi_reg_programregional_autoupdate", "channelid~languageid", "VarChar~int", ChannelId & "~12", True, False)

            obj.executeSQL("sp_mst_epg_consolidate_forApp", "ChannelId~FromDate~ToDate", "VarChar~DateTime~DateTime", ChannelId & "~" & dtFrom & "~" & dtTo, True, False)

            Dim dtIngest As DataTable = obj.executeSQL("select id from fpc_mailattachmentingest where channelid='" & ChannelId & "' and currstatus='ready for curation'", False)

            If dtIngest.Rows.Count > 0 Then
                obj.executeSQL("update fpc_mailattachmentingest set currstatus='epg distributed',currstatusUpdatedAt=dbo.getlocaldate() where id='" & dtIngest.Rows(0)("id").ToString & "'", False)
                obj.executeSQL("insert into fpc_aud_mailattachmentingest_status(ingestid,currstatus) values('" & dtIngest.Rows(0)("id").ToString & "','epg distributed')", False)
            End If

            Return True
        Catch ex As Exception
            Logger.LogError("clsUploadModules", "btnBuildEPG_Click - " & ChannelId, ex.Message.ToString, "clsUploadModules")
            Return False
        End Try
    End Function


    Public Function DeleteEPGData(ByVal vChannelId As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("Delete from map_EPGExcel where channelId='" & vChannelId.ToString.Trim & "'", False)
            Return True
        Catch ex As Exception
            Logger.LogError("clsUploadModules", "DeleteEPGData :  " & vChannelId, ex.Message.ToString, "clsUploadModules")
            Return False
        End Try
    End Function

    Public Function updateExcelProg(ByVal channelid As String, ByVal replaceFrom As String, ByVal replaceTo As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("update map_EPGExcel set [Program name] = '" & replaceTo.ToString.Trim.Replace("'", "''") & "' where channelid='" & channelid & "' and [Program name] = '" & replaceFrom.ToString.Trim.Replace("'", "''") & "'", False)
            Return True
        Catch ex As Exception
            Logger.LogError("updateExcelProg", "updateExcelProg", ex.Message.ToString, "clsUploadModules")
            Return False
        End Try
    End Function

    Public Function updateExcelGenre(ByVal replaceFrom As String, ByVal replaceTo As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("update map_EPGExcel set Genre = '" & replaceTo & "' where Genre = '" & replaceFrom & "'", False)
            Return True
        Catch ex As Exception
            Logger.LogError("Upload Schedule", "updateExcelGenre", ex.Message.ToString, "clsUploadModules")
            Return False
        End Try
    End Function

    Public Function EPGUploadError(ByVal ChannelId As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim obj As New clsExecute
            dt = obj.executeSQL("select *,(convert(varchar,errortimestamp,106) + ' ' + convert(varchar,errortimestamp,108)) errortimestamp1 from EPGUploadError where channelid='" & ChannelId & "'", False)
            Return dt
        Catch ex As Exception
            Logger.LogError("Upload Schedule", "updateExcelGenre", ex.Message.ToString, "clsUploadModules")
            Return dt
        End Try
    End Function

    Public Function Insert_mstProg(ByVal vChannelId As String, ByVal vProgName As String, ByVal vGenreId As String, ByVal vRatingId As String, ByVal vSeries As String, ByVal vActive As String, ByVal vEpisodeNo As Integer, ByVal vStatus As String, ByVal boolisMovie As Boolean, ByVal Page As String, ByVal user As String, ByVal vIgnoreSpecial As Boolean) As Boolean
        Try
            Dim obj As New clsExecute
            Dim progid As String = "0"
            Dim strDesc As String
            If Not vIgnoreSpecial Then
                vProgName = Logger.RemSplCharsEng(vProgName.ToString.Trim)
            End If

            If vStatus.ToUpper <> "EPISODIC" Then

                Dim dtProgram As DataTable = obj.executeSQL("sp_mst_program_new", "ProgId~ChannelId~ProgName~GenreId~SubGenreId~RatingId~SeriesEnabled~ismovie~Active~Action~Actionuser",
                                                               "Int~nVarChar~nVarChar~Int~Int~Varchar~Bit~Bit~Bit~Char~Varchar",
                                                               "0~" & vChannelId & "~" & vProgName & "~" & vGenreId & "~" & 0 & "~" & vRatingId & "~" & IIf(vSeries.ToString.Trim = "1", True, False) & "~" _
                                                               & boolisMovie & "~" & IIf(vActive.ToString.Trim = "1", True, False) & "~A~" & user, True, True)
                progid = dtProgram.Rows(0)(0).ToString
            End If

            strDesc = ""

            Dim sql As String = "select top 1 Description  from map_EPGExcel where [Program Name]='" & vProgName.Trim.Replace("'", "''") & "' and channelid='" & vChannelId & "'"
            Dim dtSql As DataTable = obj.executeSQL(sql, False)
            If dtSql.Rows.Count > 0 Then
                strDesc = Logger.RemSplCharsEng(dtSql.Rows(0)("Description").ToString.Trim)
            End If

            obj.executeSQL("sp_mst_ProgramRegional", "ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser",
                                    "Int~nVarChar~nVarChar~Int~Int~Char~Varchar",
                                    progid & "~" & Logger.RemSplCharsEng(vProgName) & "~" & Logger.RemSplCharsEng(strDesc) & "~" & vEpisodeNo & "~1~A~" & user, True, False)

            If vStatus.ToUpper = "EPISODIC" Then
                Dim hinProgName As String, tamProgName As String

                Dim strLangsql = "select (select progname from mst_programregional where progid='" & progid & "' and languageid=2 and episodeno=0) as 'hinProgName', (select progname from mst_programregional where progid='" & progid & "' and languageid=7 and episodeno=0) as 'tamProgName'"
                Dim dtRegNames As DataTable = obj.executeSQL(strLangsql, False)

                If dtRegNames.Rows.Count > 0 Then
                    hinProgName = Logger.RemSplCharsAllLangs(dtRegNames.Rows(0)("hinProgName").ToString.Trim, 2)
                    tamProgName = Logger.RemSplCharsAllLangs(dtRegNames.Rows(0)("tamProgName").ToString.Trim, 7)
                    If hinProgName.Trim <> "" Then
                        obj.executeSQL("sp_mst_ProgramRegional", "ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser",
                                    "Int~nVarChar~nVarChar~Int~Int~Char~Varchar",
                                    progid & "~" & hinProgName.Trim & "~~" & vEpisodeNo & "~2~A~" & user, True, False)
                    End If

                    If tamProgName.Trim <> "" Then
                        obj.executeSQL("sp_mst_ProgramRegional", "ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser",
                                    "Int~nVarChar~nVarChar~Int~Int~Char~Varchar",
                                    progid & "~" & tamProgName.Trim & "~~" & vEpisodeNo & "~7~A~" & user, True, False)
                    End If
                End If
            End If
        Catch ex As Exception
            Logger.LogError(Page, "A Insert_mstProg", ex.Message.ToString, user)
            Return False
        End Try
        Return True
    End Function

    Public Function upDateEpisodeinMapEPGExcel(ByVal rowid As String, ByVal episodeNo As Integer, ByVal Page As String, ByVal user As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("update map_EPGExcel set [episode no]='" & episodeNo & "' where rowid='" & rowid & "'", False)
        Catch ex As Exception
            Logger.LogError(Page, "upDateEpisodeinMapEPGExcel", ex.Message.ToString, user)
        End Try
        Return True
    End Function

    Public Function ProgramSeriesEpiMissing(ByVal channelId) As DataTable

        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("sp_check_series_program_epi_missing", "ChannelId", "VarChar", channelId, True, False)
        Return dt
    End Function

    Public Function getChannels(prefixText As String, count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        If IsNothing(HttpContext.Current.Session("ChannelList")) Then
            dt = obj.executeSQL("SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId", False)
            HttpContext.Current.Session("ChannelList") = dt

        Else
            dt = HttpContext.Current.Session("ChannelList")
        End If

        Dim channels As List(Of String) = New List(Of String)
        Dim j As Integer = 0

        For i As Integer = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("ChannelId").ToString.ToLower = prefixText.ToLower Then
                j = j + 1
                channels.Add(dt.Rows(i)("ChannelId").ToString)
            End If
        Next i


        For i = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("ChannelId").ToString.ToLower.Contains(prefixText.ToLower) Then
                j = j + 1
                channels.Add(dt.Rows(i)("ChannelId").ToString)
            End If
        Next i
        Return channels
        'where  Channelid like '" & prefixText & "' + '%'
    End Function
    Public Function getChannelsEpi(prefixText As String, count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        If IsNothing(HttpContext.Current.Session("ChannelListEpi")) Then
            dt = obj.executeSQL("SELECT ChannelId FROM mst_Channel  where channelid in (select distinct channelid from mst_epg where convert(varchar, progdate,112)>=convert(varchar, dbo.getlocaldate(),112) and progid in (select progid from mst_program where episodicsynopsis=1) ) and active=1 ORDER BY ChannelId", False)
            HttpContext.Current.Session("ChannelListEpi") = dt

        Else
            dt = HttpContext.Current.Session("ChannelListEpi")
        End If

        Dim channels As List(Of String) = New List(Of String)
        Dim j As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("ChannelId").ToString.ToLower.Contains(prefixText.ToLower) Then
                j = j + 1
                channels.Add(dt.Rows(i)("ChannelId").ToString)
            End If
        Next i
        Return channels
        'where  Channelid like '" & prefixText & "' + '%'
    End Function

    Public Function getMovie(ByVal moviename As String, ByVal searchtext As String, ByVal count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        'If IsNothing(HttpContext.Current.Session(moviename)) Then
        'dt = obj.executeSQL("select * from fn_movierocketsearch('" & moviename & "','" & searchtext & "') order by 2", False)
        dt = obj.executeSQL("select * from fn_movierocketsearch_rich('" & moviename & "','" & searchtext & "') order by 2", False)
        HttpContext.Current.Session(moviename) = dt

        'Else
        'dt = HttpContext.Current.Session(moviename)
        'End If

        Dim movie As List(Of String) = New List(Of String)
        Dim j As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            'If dt.Rows(i)("moviename").ToString) Then
            j = j + 1
            movie.Add(dt.Rows(i)("moviename").ToString)
            'End If
        Next i
        Return movie
        'where  Channelid like '" & prefixText & "' + '%'
    End Function

    Public Function getRichMeta(ByVal name As String, ByVal searchtext As String, ByVal count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        If IsNothing(HttpContext.Current.Session("richmeta")) Then
            'dt = obj.executeSQL("select type + '~' + name + '~' + convert(varchar(10),id) richmetaname from richmeta where name like '%" & name & "%' order by 1", False)
            dt = obj.executeSQL("select * from v_richmeta", False)
            HttpContext.Current.Session("richmeta") = dt

        Else
            dt = HttpContext.Current.Session("richmeta")
        End If

        Dim movie As List(Of String) = New List(Of String)
        Dim j As Integer = 0

        For i As Integer = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("richmetaname").ToString.Split("~")(1).ToLower = searchtext.ToLower Then
                j = j + 1
                movie.Add(dt.Rows(i)("richmetaname").ToString)
            End If
        Next i

        For i = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("richmetaname").ToString.Split("~")(1).ToLower.Contains(searchtext.ToLower) Then
                j = j + 1
                movie.Add(dt.Rows(i)("richmetaname").ToString)
            End If
        Next i

        
        Return movie
        'where  Channelid like '" & prefixText & "' + '%'
    End Function

    Public Function getNonMovieChannels(prefixText As String, count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        If IsNothing(HttpContext.Current.Session("NonMovieChannelList")) Then
            dt = obj.executeSQL("SELECT ChannelId FROM mst_Channel where active='1' and movie_channel<>1 ORDER BY ChannelId", False)
            HttpContext.Current.Session("NonMovieChannelList") = dt

        Else
            dt = HttpContext.Current.Session("NonMovieChannelList")
        End If

        Dim channels As List(Of String) = New List(Of String)
        Dim j As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("ChannelId").ToString.ToLower.Contains(prefixText.ToLower) Then
                j = j + 1
                channels.Add(dt.Rows(i)("ChannelId").ToString)
            End If
        Next i
        Return channels
        'where  Channelid like '" & prefixText & "' + '%'
    End Function

    Public Function getChannelsForXML(prefixText As String, count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        If IsNothing(HttpContext.Current.Session("XMLChannelList")) Then
            dt = obj.executeSQL("SELECT ChannelId FROM mst_Channel where Active='1' and SendEPG='1' and Onair='1' ORDER BY ChannelId", False)
            HttpContext.Current.Session("XMLChannelList") = dt
        Else
            dt = HttpContext.Current.Session("XMLChannelList")
        End If

        Dim channels As List(Of String) = New List(Of String)
        Dim j As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("ChannelId").ToString.ToLower.Contains(prefixText.ToLower) Then
                j = j + 1
                channels.Add(dt.Rows(i)("ChannelId").ToString)
            End If
        Next i
        Return channels
        'where  Channelid like '" & prefixText & "' + '%'
    End Function

    Public Function getChannelsForXMLAirtel(prefixText As String, count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        If IsNothing(HttpContext.Current.Session("XMLChannelListAirtel")) Then
            dt = obj.executeSQL("SELECT ChannelId FROM mst_Channel where (airtelftp=1 or airtelmail=1) ORDER BY ChannelId", False)
            HttpContext.Current.Session("XMLChannelListAirtel") = dt
        Else
            dt = HttpContext.Current.Session("XMLChannelListAirtel")
        End If

        Dim channels As List(Of String) = New List(Of String)
        Dim j As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("ChannelId").ToString.ToLower.Contains(prefixText.ToLower) Then
                j = j + 1
                channels.Add(dt.Rows(i)("ChannelId").ToString)
            End If
        Next i
        Return channels
        'where  Channelid like '" & prefixText & "' + '%'
    End Function

    Public Function getProgrammes(ByVal channelId As String, ByVal prefixText As String, ByVal count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        If IsNothing(HttpContext.Current.Session("progList_" & channelId)) Then
            dt = obj.executeSQL("SELECT progname from mst_program where channelid='" & channelId & "' and active=1 ORDER BY progname", False)
            HttpContext.Current.Session("progList_" & channelId) = dt
        Else
            dt = HttpContext.Current.Session("progList_" & channelId)
        End If

        Dim programmes As List(Of String) = New List(Of String)
        Dim j As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If j > count Then
                Exit For
            End If
            If dt.Rows(i)("progname").ToString.ToLower.Contains(prefixText.ToLower) Then
                j = j + 1
                programmes.Add(dt.Rows(i)("progname").ToString)
            End If
        Next i
        Return programmes
        'where  Channelid like '"    & prefixText & "' + '%'
    End Function
    Shared Function RemoveDiacritics(accentedStr As String) As String
        Dim tempBytes As Byte()
        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr)

        Dim asciiStr As String = System.Text.Encoding.UTF8.GetString(tempBytes)
        Dim rgx As Regex = New Regex("[^A-Za-z0-9_-]")
        asciiStr = rgx.Replace(asciiStr, "")
        Return asciiStr
    End Function
    Public Function sanitizeImageFile(ByVal strToSanitize As String) As String
        strToSanitize = RemoveDiacritics(strToSanitize)
        Return strToSanitize
    End Function

End Class
