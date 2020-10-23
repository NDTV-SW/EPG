﻿Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class MovieCMS
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                ddlLanguage.DataBind()
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("MovieLogos", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdMovieLogos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdMovieLogos.SelectedIndexChanged
        Try
            lbRowId.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbRowId"), Label).Text
            txtName.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbName"), Label).Text
            txtSynopsis.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbSynopsis"), Label).Text
            txtLongSynopsis.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbLongSynopsis"), Label).Text
            txtStarCast1.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbStarCast"), Label).Text
            txtReleaseYear.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbReleaseYear"), Label).Text
            txtCountry.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbCountry"), Label).Text
            Try
                ddlGenre1.SelectedValue = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbGenre"), Label).Text
                ddlGenre2.SelectedValue = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbGenre2"), Label).Text
            Catch
            End Try

            txtTrivia.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbTrivia"), Label).Text
            txtAwards.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbAwards"), Label).Text
            txtDirector.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbDirector"), Label).Text
            txtWriter.Text = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbWriter"), Label).Text
            ddlMovieLanguage.SelectedValue = DirectCast(grdMovieLogos.SelectedRow.FindControl("lbMovieLangId"), Label).Text
            chkIsVerified.Checked = DirectCast(grdMovieLogos.SelectedRow.FindControl("chkVerified"), CheckBox).Checked
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("MovieLogos", "grdMovieLogos_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable
            Dim strParameters As String
            Dim strTypes As String
            Dim strValues As String

            If btnUpdate.Text = "ADD" Then
                If txtReleaseYear.Text = "" Then

                    strParameters = "moviename~synopsis~longsynopsis~starcast~languageId~Director~Writer~country~movieLangId~Verified~Genre~Genre2~Trivia~Awards~ActionUser~ActionType"
                    strTypes = "VarChar~VarChar~VarChar~VarChar~Int~VarChar~VarChar~VarChar~Int~Bit~VarChar~VarChar~VarChar~VarChar~VarChar~Char"
                    strValues = txtName.Text & "~" & txtSynopsis.Text & "~" & txtLongSynopsis.Text & "~" & txtStarCast1.Text & "~" & ddlMovieLanguage.SelectedValue & "~" & txtDirector.Text & "~" & _
                                            txtWriter.Text & "~" & txtCountry.Text & "~" & ddlMovieLanguage.SelectedValue & "~" & chkIsVerified.Checked & "~" & _
                                            ddlGenre1.SelectedValue & "~" & ddlGenre2.SelectedValue & "~" & txtTrivia.Text & "~" & txtAwards.Text & "~" & User.Identity.Name & "~A"

                Else
                    strParameters = "moviename~synopsis~longsynopsis~starcast~languageId~releaseyear~Director~Writer~country~movieLangId~Verified~Genre~Genre2~Trivia~Awards~ActionUser~ActionType"
                    strTypes = "VarChar~VarChar~VarChar~VarChar~Int~VarChar~VarChar~VarChar~VarChar~Int~Bit~VarChar~VarChar~VarChar~VarChar~VarChar~Char"
                    strValues = txtName.Text & "~" & txtSynopsis.Text & "~" & txtLongSynopsis.Text & "~" & txtStarCast1.Text & "~" & ddlMovieLanguage.SelectedValue & "~" & txtReleaseYear.Text & "~" & txtDirector.Text & "~" & _
                                            txtWriter.Text & "~" & txtCountry.Text & "~" & ddlMovieLanguage.SelectedValue & "~" & chkIsVerified.Checked & "~" & _
                                            ddlGenre1.SelectedValue & "~" & ddlGenre2.SelectedValue & "~" & txtTrivia.Text & "~" & txtAwards.Text & "~" & User.Identity.Name & "~A"
                End If
                dt = obj.executeSQL("sp_mst_moviesdb", strParameters, strTypes, strValues, True, False)
            Else
                If txtReleaseYear.Text = "" Then
                    strParameters = "rowid~moviename~synopsis~longsynopsis~starcast~languageId~Director~Writer~country~movieLangId~Verified~Genre~Genre2~Trivia~Awards~ActionUser~ActionType"
                    strTypes = "Int~VarChar~VarChar~VarChar~VarChar~Int~VarChar~VarChar~VarChar~Int~Bit~VarChar~VarChar~VarChar~VarChar~VarChar~Char"
                    strValues = lbRowId.Text & "~" & txtName.Text & "~" & txtSynopsis.Text & "~" & txtLongSynopsis.Text & "~" & txtStarCast1.Text & "~" & ddlMovieLanguage.SelectedValue & "~" & txtDirector.Text & "~" & _
                                            txtWriter.Text & "~" & txtCountry.Text & "~" & ddlMovieLanguage.SelectedValue & "~" & chkIsVerified.Checked & "~" & _
                                            ddlGenre1.SelectedValue & "~" & ddlGenre2.SelectedValue & "~" & txtTrivia.Text & "~" & txtAwards.Text & "~" & User.Identity.Name & "~U"
                Else
                    strParameters = "rowid~moviename~synopsis~longsynopsis~starcast~languageId~releaseyear~Director~Writer~country~movieLangId~Verified~Genre~Genre2~Trivia~Awards~ActionUser~ActionType"
                    strTypes = "Int~VarChar~VarChar~VarChar~VarChar~Int~VarChar~VarChar~VarChar~VarChar~Int~Bit~VarChar~VarChar~VarChar~VarChar~VarChar~Char"
                    strValues = lbRowId.Text & "~" & txtName.Text & "~" & txtSynopsis.Text & "~" & txtLongSynopsis.Text & "~" & txtStarCast1.Text & "~" & ddlMovieLanguage.SelectedValue & "~" & txtReleaseYear.Text & "~" & txtDirector.Text & "~" & _
                                            txtWriter.Text & "~" & txtCountry.Text & "~" & ddlMovieLanguage.SelectedValue & "~" & chkIsVerified.Checked & "~" & _
                                            ddlGenre1.SelectedValue & "~" & ddlGenre2.SelectedValue & "~" & txtTrivia.Text & "~" & txtAwards.Text & "~" & User.Identity.Name & "~U"
                End If

                dt = obj.executeSQL("sp_mst_moviesdb", strParameters, strTypes, strValues, True, False)
            End If

            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("MovieLogos", "btnUpdate_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("rpt_getMovieLogoCMS", True)
        Dim strWhere As String = ""
        If chkWithImage.Checked Then
            strWhere = strWhere & "tmdbimageurl IS NOT NULL AND tmdbimageurl <> ''" & " AND "
        Else
            strWhere = strWhere & "(tmdbimageurl IS NULL OR tmdbimageurl = '')" & " AND "
        End If

        If chkWithSynopsis.Checked Then
            strWhere = strWhere & "Synopsis IS NOT NULL AND Synopsis <> ''" & " AND "
        Else
            strWhere = strWhere & "(Synopsis IS NULL OR Synopsis = '')" & " AND "
        End If
        If chkWithReleaseYear.Checked Then
            strWhere = strWhere & "ReleaseYear IS NOT NULL AND ReleaseYear <> ''" & " AND "
        Else
            strWhere = strWhere & "(ReleaseYear IS NULL OR ReleaseYear = '')" & " AND "
        End If
        If chkWithStarCast.Checked Then
            strWhere = strWhere & "Starcast IS NOT NULL AND Starcast <> ''" & " AND "
        Else
            strWhere = strWhere & "(Starcast IS NULL OR Starcast = '')" & " AND "
        End If

        If chkInCurrentEPG.Checked Then
            strWhere = strWhere & "epg=1" & " AND "
        Else
            strWhere = strWhere & "epg=0" & " AND "
        End If

        If chkVerified.Checked Then
            strWhere = strWhere & "verified=1" & " AND "
        Else
            strWhere = strWhere & "verified=1" & " AND "
        End If

        If chkExact.Checked Then
            'strWhere = strWhere & "Image=1" & " AND "
        Else
            'strWhere = strWhere & "Image=1" & " AND "
        End If

        If chkAwards.Checked Then
            strWhere = strWhere & "awards IS NOT NULL AND awards <> ''" & " AND "
        Else
            'strWhere = strWhere & "Image=1" & " AND "
        End If

        If chkusefilters.Checked Then
            'strWhere = strWhere & "Image=1" & " AND "
        Else
            'strWhere = strWhere & "Image=1" & " AND "
        End If

        If Not (ddlLanguage.SelectedValue = 0) Then
            strWhere = strWhere & "movielangid=" & ddlLanguage.SelectedValue & " AND "
        End If

        strWhere = strWhere.Substring(0, strWhere.Length - 5)

        Dim result() As DataRow = dt.Select(strWhere)

        Dim dtRes As New DataTable

        For Each row As DataRow In result
            dtRes.Rows.Add(row)
            'Console.WriteLine("{0}, {1}", row(0), row(1))
        Next

        grdMovieLogos.DataSource = dtRes
        grdMovieLogos.DataBind()
    End Sub

    Private Sub getMovieLogoReport()
        Dim strSearchString As String
        If txtSearch1.Text.Trim = "" Then
            strSearchString = "0"
        Else
            strSearchString = txtSearch1.Text
        End If

        Dim adp As New SqlDataAdapter("rpt_MovieReportNew", ConString)
        adp.SelectCommand.CommandType = CommandType.StoredProcedure
        adp.SelectCommand.Parameters.Add("search", SqlDbType.VarChar).Value = strSearchString
        If chkWithImage.Checked Then
            adp.SelectCommand.Parameters.Add("withImage", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("withImage", SqlDbType.VarChar).Value = "0"
        End If
        If chkWithSynopsis.Checked Then
            adp.SelectCommand.Parameters.Add("withSynopsis", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("withSynopsis", SqlDbType.VarChar).Value = "0"
        End If
        If chkWithReleaseYear.Checked Then
            adp.SelectCommand.Parameters.Add("withYear", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("withYear", SqlDbType.VarChar).Value = "0"
        End If
        If chkWithStarCast.Checked Then
            adp.SelectCommand.Parameters.Add("withStarcast", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("withStarcast", SqlDbType.VarChar).Value = "0"
        End If

        If chkInCurrentEPG.Checked Then
            adp.SelectCommand.Parameters.Add("inCurrentEPG", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("inCurrentEPG", SqlDbType.VarChar).Value = "0"
        End If

        If chkVerified.Checked Then
            adp.SelectCommand.Parameters.Add("verified", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("verified", SqlDbType.VarChar).Value = "0"
        End If

        If chkExact.Checked Then
            adp.SelectCommand.Parameters.Add("exactsearch", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("exactsearch", SqlDbType.VarChar).Value = "0"
        End If

        If chkAwards.Checked Then
            adp.SelectCommand.Parameters.Add("awards", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("awards", SqlDbType.VarChar).Value = "0"
        End If

        If chkusefilters.Checked Then
            adp.SelectCommand.Parameters.Add("usefilters", SqlDbType.VarChar).Value = "1"
        Else
            adp.SelectCommand.Parameters.Add("usefilters", SqlDbType.VarChar).Value = "0"
        End If

        adp.SelectCommand.Parameters.Add("languageid", SqlDbType.VarChar).Value = ddlLanguage.SelectedValue.ToString

        Dim dt As New DataTable
        adp.Fill(dt)
        If dt.Rows.Count > 0 Then
            Dim str As String = "Total Movies : " & dt.Rows(0)("TotalMovies").ToString() & "<br/>" & _
                "Percentage with Image : " & dt.Rows(0)("perImage").ToString() & "<br/> " & _
                "Percentage with Synopsis : " & dt.Rows(0)("perBio").ToString() & "<br/> " & _
                "Percentage with Starcast : " & dt.Rows(0)("perStarCast").ToString()
            lbReport.Text = str
        Else
            lbReport.Text = ""
        End If

    End Sub

    Private Sub clearAll()
        txtName.Text = String.Empty
        txtSynopsis.Text = String.Empty
        txtLongSynopsis.Text = String.Empty
        txtStarCast1.Text = String.Empty
        txtWriter.Text = String.Empty
        txtDirector.Text = String.Empty
        txtReleaseYear.Text = String.Empty
        txtCountry.Text = String.Empty
        txtTrivia.Text = String.Empty
        txtAwards.Text = String.Empty
        chkIsVerified.Checked = False
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(False)
    End Sub

    'Dim intSno As Integer
    Protected Sub grdMovieLogos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMovieLogos.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            'intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            'lbSno.Text = intSno
            'intSno = intSno + 1
            Dim lbRowId As Label = TryCast(e.Row.FindControl("lbRowId"), Label)
            Dim lbName As Label = TryCast(e.Row.FindControl("lbName"), Label)
            Dim lbFileName As Label = TryCast(e.Row.FindControl("lbFileName"), Label)
            Dim objImage As New clsUploadModules
            lbFileName.Text = objImage.sanitizeImageFile(lbFileName.Text)


            Dim hyUpload As HyperLink = TryCast(e.Row.FindControl("hyUpload"), HyperLink)
            hyUpload.NavigateUrl = "javascript:showDiv('MainContent_grdMovieLogos_lbProgLogo_" & e.Row.RowIndex & "','MainContent_grdMovieLogos_hyLogo_" & e.Row.RowIndex & "','MainContent_grdMovieLogos_imgCelebrityImage_" & e.Row.RowIndex & "','" & lbRowId.Text & "','" & lbFileName.Text & "')"
        End If
    End Sub

    Protected Sub grdMovieLogos_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMovieLogos.PageIndexChanging
        grdMovieLogos.PageIndex = e.NewPageIndex
        bindGrid(True)
    End Sub


    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)

        Dim fileName As String = jsFileName.Value
        Dim rowid As String = jsProfileId.Value

        Dim myControl As Label = TryCast(Page.FindControl("jsFileName"), Label)

        If AsyncFileUpload1.FileName.ToLower.EndsWith(".jpg") Or AsyncFileUpload1.FileName.ToLower.EndsWith(".jpeg") Then
            If (AsyncFileUpload1.HasFile) Then
                Dim strpath As String = MapPath("~/uploads/MovieImages/") & fileName
                AsyncFileUpload1.SaveAs(strpath)
                System.Threading.Thread.Sleep(2000)
                Dim abc As New clsFTP
                abc.doS3Task(strpath, "/uploads/MovieImages")
                System.Threading.Thread.Sleep(2000)
            End If
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            Dim strUrl As String = "http://epgops.ndtv.com/uploads/MovieImages/" & fileName
            Dim adp As New SqlDataAdapter("update mst_moviesdb set tmdbimageurl='" & strUrl & "' where rowid='" & rowid & "'", MyConnection)
            Dim dt As New DataTable
            adp.Fill(dt)
        Else
            Throw New Exception("only .jpg and .jpeg files supported")
        End If

    End Sub

    Protected Sub grdMovieLogos_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMovieLogos.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grdMovieLogos.Rows(e.RowIndex).FindControl("lbRowId"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("update mst_moviesdb set tmdbImageurl='' where rowid='" & lbRowId.Text & "'", False)

            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("MovieLogos", "grdMovieLogos_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdMovieLogos_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdMovieLogos.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub grdMovieLogos_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMovieLogos.Sorting
        bindGrid(True)
    End Sub

    Protected Sub btnVerifyMultiple_Click(sender As Object, e As EventArgs) Handles btnVerifyMultiple.Click
        For i As Integer = 0 To grdMovieLogos.Rows.Count - 1
            Dim chkVerified As CheckBox = TryCast(grdMovieLogos.Rows(i).FindControl("chkVerified"), CheckBox)
            Dim lbRowId As Label = TryCast(grdMovieLogos.Rows(i).FindControl("lbRowId"), Label)
            Dim intVerified As Integer = 0
            If chkVerified.Checked = True Then
                intVerified = 1
                Dim obj As New clsExecute
                obj.executeSQL("update mst_moviesdb set verified='1' where rowid='" & lbRowId.Text & "'", False)
                chkVerified.Checked = False
            End If

        Next i
        bindGrid(True)
    End Sub
End Class