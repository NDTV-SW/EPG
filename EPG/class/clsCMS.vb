Imports System.Data
Imports System.Data.SqlClient

Public Class clsCMS
    Public Sub clsCMS()
    End Sub

    Public Function getMoviesReport(ByVal useFilter As Boolean, ByVal image As Boolean, ByVal synopsis As Boolean, ByVal release As Boolean, _
                              ByVal cast As Boolean, ByVal verified As Boolean, ByVal exact As Boolean, ByVal awards As Boolean, ByVal updatedby As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim obj As New clsExecute
        Try
            Dim strSql As String = ""
            strSql = "SELECT x.rowid  ,x.moviename  ,convert(VARCHAR, x.rowid) + '_' + replace(replace(replace(convert(VARCHAR(50), x.moviename), ' ', '_'), ':', ''), '''''', '') + '.jpg' filename  ,x.starcast  ,x.synopsis  ,x.longsynopsis  " & _
                    ",x.tmdbimageurl ,x.releaseyear  ,x.country  ,x.movielangid  ,a.Fullname movieLanguage  ,x.verified  ,x.trivia ,x.awards,x.writer  " & _
                    " ,x.director  ,x.premierdate  ,x.premierchannelid  ,isnull(x.active, 0) active  ,case when isnull(x.verified, 0)=1 then '1' else '0' end as verified  " & _
                    ",x.genre  ,x.genre2  ,x.languageid  ,CASE   WHEN (SELECT count(*) FROM mst_program WHERE ProgName=x.moviename and progid IN ( Select progid FROM mst_epg  " & _
                    "WHERE convert(VARCHAR, progdate, 112) >= convert(VARCHAR, [dbo].[GetLocalDate](), 112)  )) > 0  THEN 1  ELSE 0  END AS epg  FROM mst_moviesdb x  " & _
                    "JOIN mst_language a ON x.movielangid = a.languageid"

            obj.executeSQL(strSql, False)
            Return dt
        Catch ex As Exception
            Logger.LogError("clsCMS", "getMoviesReport", ex.Message.ToString, updatedby)
            Return dt
        End Try
    End Function

    Public Function getMovies(ByVal useFilter As Boolean, ByVal searchText As String, ByVal languageid As Integer, ByVal image As Boolean, ByVal synopsis As Boolean, ByVal release As Boolean,
                                  ByVal cast As Boolean, ByVal verified As Boolean, ByVal exact As Boolean, ByVal awards As Boolean, ByVal updatedby As String, ByVal ImageSynopsisCastMissing As Boolean, ByVal publicChannel As Boolean) As DataTable
        Dim dt As DataTable = Nothing
        Dim obj As New clsExecute
        Try
            Dim strSql As String = ""
            strSql = " select distinct a.* from (SELECT x.rowid ,x.ureqarating,x.tmdbrating,x.moviename ,convert(VARCHAR, x.rowid) + '_' + replace(replace(replace(convert(VARCHAR(50), x.moviename), ' ', '_'), ':', ''), '''''', '') + '.jpg' filename ,x.starcast ,x.synopsis ,x.longsynopsis" &
                     " ,x.tmdbimageurl ,x.releaseyear ,x.country ,x.movielangid ,a.Fullname movieLanguage ,x.trivia ,x.awards,x.writer" &
                     " ,x.director ,x.premierdate ,x.premierchannelid ,isnull(x.active, 0) active ,case when isnull(x.verified, 0)=1 then '1' else '0' end as verified" &
                     " ,x.genre ,x.genre2 ,x.languageid ,CASE WHEN (SELECT count(*) FROM mst_program WHERE ProgName=x.moviename and progid IN ( Select progid FROM mst_epg" &
                     " WHERE convert(VARCHAR, progdate, 112) >= convert(VARCHAR, [dbo].[GetLocalDate](), 112) )) > 0 THEN 1 ELSE 0 END AS epg FROM mst_moviesdb x" &
                     " JOIN mst_language a ON x.movielangid = a.languageid) a" &
                     " join mst_program b on a.moviename=b.ProgName" &
                     " join mst_epg e on e.ProgID=b.ProgID where e.ProgTime between '19:00:00' and '22:00:00' and e.duration>70" &
                     " and CONVERT(varchar, e.ProgDate,112)>=CONVERT(varchar, dbo.GetLocalDate(),112)  and movielangid in (1,2)"
            'If publicChannel Then

            strSql = strSql & " and b.channelid in (select channelid from mst_channel where onair=1 and trp>80 and publicchannel=1 "
            If useFilter Then
                If languageid <> 0 Then
                    strSql = strSql & " and channellanguageid='" & languageid & "' "
                End If
            End If
            strSql = strSql & " )"
            'End If
            If ImageSynopsisCastMissing Then


                strSql = strSql & " and (tmdbimageurl is null or tmdbimageurl =''"
                strSql = strSql & " or synopsis is  null or synopsis=''"
                strSql = strSql & " or starcast is null or starcast='')"

            Else
                If useFilter Then
                    If languageid <> 0 Then
                        strSql = strSql & " and movielangid='" & languageid & "'"
                    End If



                    If exact Then
                        strSql = strSql & " and moviename='" & searchText.Replace("'", "''") & "'"
                    Else
                        strSql = strSql & " and moviename like '%" & searchText.Replace("'", "''") & "%'"
                    End If

                    If image Then
                        strSql = strSql & " and tmdbimageurl is not null and tmdbimageurl <>''"
                    Else
                        strSql = strSql & " and (tmdbimageurl is null or tmdbimageurl ='')"
                    End If

                    If synopsis Then
                        strSql = strSql & " and synopsis is not null and synopsis <>''"
                    Else
                        strSql = strSql & " and (synopsis is  null or synopsis='')"
                    End If

                    If release Then
                        strSql = strSql & " and releaseyear is not null and releaseyear <>''"
                    Else
                        'strSql = strSql & " and (releaseyear is  null or releaseyear ='' or releaseyear<1850)"
                    End If

                    If cast Then
                        strSql = strSql & " and starcast is not null and starcast <>''"
                    Else
                        'strSql = strSql & " and (starcast is null or starcast='')"
                    End If

                    If verified Then
                        strSql = strSql & " and verified=1"
                    Else
                        strSql = strSql & " and verified=0"
                    End If

                    If awards Then
                        strSql = strSql & " and awards is not null and  awards <> ''"
                    End If

                Else
                    If exact Then
                        strSql = strSql & " and moviename='" & searchText.Replace("'", "''") & "'"
                    Else
                        strSql = strSql & " and moviename like '%" & searchText.Replace("'", "''") & "%'"
                    End If

                End If
            End If
            strSql = strSql & " order by 2"

            dt = obj.executeSQL(strSql, False)
            Return dt
        Catch ex As Exception
            Logger.LogError("clsCMS", "getMoviesReport", ex.Message.ToString, updatedby)
            Return dt
        End Try
    End Function

    Public Function BuildEPG(ByVal ChannelId As String, ByVal boolupdateSynopsis As Boolean, ByVal boolTentative As Boolean, ByVal uploadedBy As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("sp_mst_epg_getdata_v1", "ChannelId~updateSynopsis~Tentative~uploadedby", "VarChar~Bit~Bit~VarChar", ChannelId & "~" & True & "~" & boolTentative & "~" & uploadedBy, True, False)

            Dim dt As DataTable = obj.executeSQL("select isnull(datediff(DD,dbo.GetLocalDate(), max([date])),0) days from map_EPGExcel where channelId='" & ChannelId & "'", False)

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
            '

            'Dim dtFixError As DataTable = obj.executeSQL("select id,channelid,convert(varchar,epgdate,106) epgdate from mst_overlapping_issues_new where comments not like '%entry good%' and comments <> '' and mailsent=0", False)
            'If dtFixError.Rows.Count > 0 Then
            '    For i As Integer = 0 To dtFixError.Rows.Count - 1
            '        Dim strFixIid As String = dtFixError.Rows(i)("id").ToString
            '        Dim strFixChannelid As String = dtFixError.Rows(i)("channelid").ToString
            '        Dim strFixEPGDate As String = dtFixError.Rows(i)("epgdate").ToString
            '        Logger.mailMessage("epg@ndtv.com", strFixChannelid & " - Please check Duration Overlap/Gap", "Gap or Overlapping found in channel <b>" & strFixChannelid & "</b> for date <b>" & strFixEPGDate & ".</b><br/><br/>" & _
            '                           "Please manually Fix and upload again.", "", "")
            '        obj.executeSQL("update mst_overlapping_issues_new set mailsent=1 where id='" & strFixIid & "'", False)
            '    Next

            'End If
            obj.executeSQL("sp_mst_epg_consolidate", "ChannelId~FromDate~ToDate", "VarChar~DateTime~DateTime", ChannelId & "~" & dtFrom & "~" & dtTo, True, False)
            Return True
        Catch ex As Exception
            Logger.LogError("clsCMS", "btnBuildEPG_Click - " & ChannelId, ex.Message.ToString, "clsCMS")
            Return False
        End Try
    End Function

    Public Function DeleteEPGData(ByVal vChannelId As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("Delete from map_EPGExcel where channelId='" & vChannelId.ToString.Trim & "'", False)
            Return True
        Catch ex As Exception
            Logger.LogError("clsCMS", "DeleteEPGData :  " & vChannelId, ex.Message.ToString, "clsCMS")
            Return False
        End Try
    End Function

    Public Function updateExcelProg(ByVal channelid As String, ByVal replaceFrom As String, ByVal replaceTo As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("update map_EPGExcel set [Program name] = '" & replaceTo.ToString.Trim.Replace("'", "''") & "' where channelid='" & channelid & "' and [Program name] = '" & replaceFrom.ToString.Trim.Replace("'", "''") & "'", False)
            Return True
        Catch ex As Exception
            Logger.LogError("updateExcelProg", "updateExcelProg", ex.Message.ToString, "clsCMS")
            Return False
        End Try
    End Function

    Public Function updateExcelGenre(ByVal replaceFrom As String, ByVal replaceTo As String) As Boolean
        Try
            Dim obj As New clsExecute
            obj.executeSQL("update map_EPGExcel set Genre = '" & replaceTo & "' where Genre = '" & replaceFrom & "'", False)
            Return True
        Catch ex As Exception
            Logger.LogError("Upload Schedule", "updateExcelGenre", ex.Message.ToString, "clsCMS")
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
            Logger.LogError("Upload Schedule", "updateExcelGenre", ex.Message.ToString, "clsCMS")
            Return dt
        End Try
    End Function

    Public Function Insert_mstProg(ByVal vChannelId As String, ByVal vProgName As String, ByVal vGenreId As String, ByVal vRatingId As String, ByVal vSeries As String, ByVal vActive As String, ByVal vEpisodeNo As Integer, ByVal vStatus As String, ByVal boolisMovie As Boolean, ByVal Page As String, ByVal user As String) As Boolean
        Try
            Dim obj As New clsExecute
            vProgName = Logger.RemSplCharsEng(vProgName.ToString.Trim)
            If vStatus.ToUpper <> "EPISODIC" Then

                obj.executeSQL("sp_mst_program", "ProgId~ChannelId~ProgName~GenreId~SubGenreId~RatingId~SeriesEnabled~ismovie~Active~Action~Actionuser", _
                                                               "Int~nVarChar~nVarChar~Int~Int~Varchar~Bit~Bit~Bit~Char~Varchar", _
                                                               "0~" & vChannelId & "~" & vProgName & "~" & vGenreId & "~" & 0 & "~" & vRatingId & "~" & IIf(vSeries.ToString.Trim = "1", True, False) & "~" _
                                                               & boolisMovie & "~" & IIf(vActive.ToString.Trim = "1", True, False) & "~A~" & user, True, False)
            End If
            Dim progid As String, strDesc As String
            progid = "0"
            strDesc = ""
            Dim sql As String = "select top 1 a.ProgId,b.Description  from mst_program a join map_EPGExcel b on dbo.FN_Sanitise(a.ProgName) =dbo.FN_Sanitise(b.[Program Name]) where dbo.FN_Sanitise(a.progname)='" & vProgName.Replace("'", "''") & "' and a.channelid='" & vChannelId.ToString.Trim & "'"

            Dim dtSql As DataTable = obj.executeSQL(sql, False)

            If dtSql.Rows.Count > 0 Then
                progid = dtSql.Rows(0)("ProgId").ToString.Trim
                strDesc = Logger.RemSplCharsEng(dtSql.Rows(0)("Description").ToString.Trim)
            End If

            obj.executeSQL("sp_mst_ProgramRegional", "ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                    "Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                    progid & "~" & vProgName & "~" & strDesc & "~" & vEpisodeNo & "~1~A~" & user, True, False)

            If vStatus.ToUpper = "EPISODIC" Then
                Dim hinProgName As String, tamProgName As String

                Dim strLangsql = "select (select progname from mst_programregional where progid='" & progid & "' and languageid=2 and episodeno=0) as 'hinProgName', (select progname from mst_programregional where progid='" & progid & "' and languageid=7 and episodeno=0) as 'tamProgName'"
                Dim dtRegNames As DataTable = obj.executeSQL(strLangsql, False)

                If dtRegNames.Rows.Count > 0 Then
                    hinProgName = Logger.RemSplCharsAllLangs(dtRegNames.Rows(0)("hinProgName").ToString.Trim, 2)
                    tamProgName = Logger.RemSplCharsAllLangs(dtRegNames.Rows(0)("tamProgName").ToString.Trim, 7)
                    If hinProgName.Trim <> "" Then
                        obj.executeSQL("sp_mst_ProgramRegional", "ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                    "Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                    progid & "~" & hinProgName & "~~" & vEpisodeNo & "~2~A~" & user, True, False)
                    End If

                    If tamProgName.Trim <> "" Then
                        obj.executeSQL("sp_mst_ProgramRegional", "ProgId~ProgName~Synopsis~EpisodeNo~LanguageId~Action~Actionuser", _
                                    "Int~nVarChar~nVarChar~Int~Int~Char~Varchar", _
                                    progid & "~" & tamProgName & "~~" & vEpisodeNo & "~7~A~" & user, True, False)
                    End If
                End If
            End If
        Catch ex As Exception
            Logger.LogError(Page, "A Insert_mstProg", ex.Message.ToString, user)
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
        dt = obj.executeSQL("select * from fn_movierocketsearch('" & moviename & "','" & searchtext & "') order by 2", False)
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

    Public Function getChannelsForXML(prefixText As String, count As Integer) As List(Of String)

        Dim obj As New clsExecute
        Dim dt As DataTable
        If IsNothing(HttpContext.Current.Session("XMLChannelList")) Then
            dt = obj.executeSQL("SELECT ChannelId FROM mst_Channel where ChannelID in (select ChannelID from mst_ChannelRegionalName) and channelid not in (select channelID from mst_channel_ppv) and Active='1' and SendEPG='1' and Onair='1' and (AirtelFTP='1' or AirtelMail='1') ORDER BY ChannelId", False)
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

    'Public Function sanitizeImageFile(ByVal strToSanitize As String) As String
    '    strToSanitize = strToSanitize.Replace("<", "")

    '    strToSanitize = strToSanitize.Replace(">", "")
    '    strToSanitize = strToSanitize.Replace(":", "")
    '    strToSanitize = strToSanitize.Replace("'", "")
    '    strToSanitize = strToSanitize.Replace("""", "")
    '    strToSanitize = strToSanitize.Replace("/", "")
    '    strToSanitize = strToSanitize.Replace("\", "")
    '    strToSanitize = strToSanitize.Replace("?", "")
    '    strToSanitize = strToSanitize.Replace("*", "")
    '    strToSanitize = strToSanitize.Replace(" ", "_")
    '    strToSanitize = strToSanitize.Replace("+", "_")
    '    Return strToSanitize
    'End Function

End Class
