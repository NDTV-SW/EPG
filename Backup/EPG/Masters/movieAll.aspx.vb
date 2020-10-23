Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports WatTmdb.V3
Imports System.Data.SqlClient
Public Class movieAll
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                lbID.Text = Request.QueryString("ID")
                txtSearch.Text = Request.QueryString("search")
                getSearchResults(txtSearch.Text)
                If Not (lbID.Text.Trim = "" Or IsNothing(lbID.Text.Trim = "")) Then
                    tbMovieDetails.Visible = True
                    Dim api As New Tmdb("2659ae67d63ca78159117900a26f4609", "en")

                    Dim abc As TmdbMovieCast
                    abc = api.GetMovieCast(lbID.Text)
                    Dim int1 As Integer = abc.cast.Count

                    For i As Integer = 0 To int1 - 1
                        lbOrigTitle.Text = lbOrigTitle.Text & " " & abc.cast(i).name & ", "
                    Next
                    Dim starcast As String = lbOrigTitle.Text
                    starcast = starcast.Trim
                    starcast = starcast.Substring(0, starcast.Length - 1)
                    lbOrigTitle.Text = starcast
                    'TmdbMovieCast GetMovieCast(int MovieID)

                    Dim movieDetailResult = api.GetMovieInfo(lbID.Text)
                    If Not IsNothing(movieDetailResult) Then
                        lbTitle.Text = movieDetailResult.title
                        'lbOrigTitle.Text = movieDetailResult.original_title
                        lbReleaseDate.Text = movieDetailResult.release_date
                        lbPopularity.Text = movieDetailResult.popularity
                        lbTagline.Text = movieDetailResult.tagline
                        lbOverview.Text = movieDetailResult.overview
                    End If
                    grdMovieResults.DataBind()
                Else
                    tbMovieDetails.Visible = False
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        tbMovieDetails.Visible = False
        getSearchResults(txtSearch.Text)
    End Sub

    Private Sub getSearchResults(ByVal queryString As String)
        Dim page As Integer = 1
        Dim api As New Tmdb("2659ae67d63ca78159117900a26f4609", "en")
        Dim searchResult = api.SearchMovie(queryString, page)
        If Not IsNothing(searchResult) Then
            'If searchResult.results.Count = 0 Then

            '    Exit Sub
            'End If

            Dim dt As New DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("Title")
            dt.Columns.Add("Orig. Title")
            dt.Columns.Add("Release Date")
            dt.Columns.Add("Popularity")
            'dt.Columns.Add("Poster Path")
            dt.Columns.Add("Vote Avg.")
            'dt.Columns.Add("Vote Count")

            For Each Movie In searchResult.results
                Dim dr As DataRow = dt.NewRow
                dr("ID") = Movie.id
                dr("Title") = Movie.title
                dr("Orig. Title") = Movie.original_title
                dr("Release Date") = Movie.release_date
                dr("Popularity") = Movie.popularity
                'dr("Poster Path") = Movie.poster_path
                dr("Vote Avg.") = Movie.vote_average
                'dr("Vote Count") = Movie.vote_count
                dt.Rows.Add(dr)
            Next
            dt.AcceptChanges()
            grdMovieResults.DataSource = dt
            grdMovieResults.DataBind()
        End If

    End Sub

    Protected Sub grdMovieResults_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMovieResults.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btnDetails As New Button
            Dim hyDetails As New HyperLink

            btnDetails.Text = "Details"
            hyDetails.Text = "Details"
            Try
                e.Row.Cells(3).Text = Convert.ToDateTime(e.Row.Cells(3).Text).ToString("dd MMM yyyy")
            Catch ex As Exception
            End Try
            'hyDetails.NavigateUrl = "javascript:openWin('" & e.Row.Cells(0).Text & "')"
            'lbID.Text = e.Row.Cells(0).Text
            'hyDetails.NavigateUrl = "#"
            hyDetails.NavigateUrl = HttpContext.Current.Request.Url.AbsolutePath.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") & "?id=" & e.Row.Cells(0).Text & "&search=" & txtSearch.Text
            'e.Row.Cells(0).Text = ""
            e.Row.Cells(0).Controls.Add(hyDetails)

        End If


    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim adp As New SqlDataAdapter("select moviename,starcast from  mst_moviesdb where moviename is null or starcast is null", MyConnection)
        Dim dt As New DataTable
        adp.Fill(dt)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim adpUpdateQuery As String = ""
                Dim dtResult As DataTable = doProcessing(dt.Rows(i).Item(0).ToString)
                If Not dtResult.Rows.Count > 0 Then
                    Continue For
                End If
                adpUpdateQuery = dtResult.Rows(0).Item(0).ToString

                lbID.Text = adpUpdateQuery
                If Not (lbID.Text.Trim = "" Or IsNothing(lbID.Text.Trim = "")) Then
                    tbMovieDetails.Visible = True
                    Dim api As New Tmdb("2659ae67d63ca78159117900a26f4609", "en")

                    Dim abc As TmdbMovieCast
                    abc = api.GetMovieCast(lbID.Text)
                    Dim int1 As Integer = abc.cast.Count
                    IIf(int1 > 4, int1 = 4, int1)
                    If int1 > 4 Then
                        int1 = 4
                    End If
                    Dim strStarcast As String = ""
                    For j As Integer = 0 To int1 - 1
                        strStarcast = strStarcast & abc.cast(j).name & ", "
                    Next
                    strStarcast = strStarcast.Trim
                    If Not (strStarcast = "") Then
                        strStarcast = strStarcast.Substring(0, strStarcast.Length - 1)
                    End If
                    'TmdbMovieCast GetMovieCast(int MovieID)

                    Dim movieDetailResult = api.GetMovieInfo(lbID.Text)
                    If Not IsNothing(movieDetailResult) Then
                        lbTitle.Text = movieDetailResult.title
                        lbOrigTitle.Text = movieDetailResult.original_title
                        lbReleaseDate.Text = movieDetailResult.release_date
                        lbPopularity.Text = movieDetailResult.popularity
                        lbTagline.Text = movieDetailResult.tagline
                        lbOverview.Text = movieDetailResult.overview
                    End If
                    Dim adpCheck As New SqlDataAdapter("select synopsis,starcast from mst_moviesdb where moviename='" & dt.Rows(i).Item(0).ToString.Replace("'", "''") & "'", MyConnection)
                    Dim dtCheck As New DataTable
                    adpCheck.Fill(dtCheck)
                    Dim ds As New DataSet
                    Dim adpUpdateDB As SqlDataAdapter
                    If dtCheck.Rows.Count > 0 And dtCheck.Rows(0).Item(0).ToString.Trim = "" Then
                        adpUpdateDB = New SqlDataAdapter("update mst_moviesdb set starcast='" & strStarcast.Replace("'", "''") & "',synopsis='" & lbOverview.Text.Replace("'", "''") & "' where moviename='" & dt.Rows(i).Item(0).ToString.Replace("'", "''") & "'", MyConnection)
                        adpUpdateDB.Fill(ds)
                        adpUpdateDB.Dispose()
                    ElseIf dtCheck.Rows.Count > 0 And dtCheck.Rows(0).Item(1).ToString.Trim = "" Then
                        adpUpdateDB = New SqlDataAdapter("update mst_moviesdb set starcast='" & strStarcast.Replace("'", "''") & "' where moviename='" & dt.Rows(i).Item(0).ToString.Replace("'", "''") & "'", MyConnection)
                        adpUpdateDB.Fill(ds)
                        adpUpdateDB.Dispose()
                    End If

                    'Dim adpUpdateDB As New SqlDataAdapter("update mst_moviesdb set starcast='" & strStarcast.Replace("'", "''") & "',synopsis='" & lbOverview.Text.Replace("'", "''") & "' where moviename='" & dt.Rows(i).Item(0).ToString.Replace("'", "''") & "'", MyConnection)

                    ds.Dispose()

                Else
                    tbMovieDetails.Visible = False
                End If
            Next
            grdMovieMaster.DataBind()
        End If
    End Sub

    Private Function doProcessing(ByVal queryString As String) As DataTable
        Dim page As Integer = 1
        Dim api As New Tmdb("2659ae67d63ca78159117900a26f4609", "en")
        Dim searchResult = api.SearchMovie(queryString, page)
        Dim dt As New DataTable
        If Not IsNothing(searchResult) Then
            'If searchResult.results.Count = 0 Then

            '    Exit Sub
            'End If


            dt.Columns.Add("ID")
            dt.Columns.Add("Title")
            dt.Columns.Add("Orig. Title")
            dt.Columns.Add("Release Date")
            dt.Columns.Add("Popularity")
            'dt.Columns.Add("Poster Path")
            dt.Columns.Add("Vote Avg.")
            'dt.Columns.Add("Vote Count")

            For Each Movie In searchResult.results
                Dim dr As DataRow = dt.NewRow
                dr("ID") = Movie.id
                dr("Title") = Movie.title
                dr("Orig. Title") = Movie.original_title
                dr("Release Date") = Movie.release_date
                dr("Popularity") = Movie.popularity
                'dr("Poster Path") = Movie.poster_path
                dr("Vote Avg.") = Movie.vote_average
                'dr("Vote Count") = Movie.vote_count
                dt.Rows.Add(dr)
            Next
            dt.AcceptChanges()
            grdMovieResults.DataSource = dt

            Return (dt)
            'grdMovieResults.DataBind()
        Else
            Return (dt)
        End If

    End Function

End Class