Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CelebrityMaster
    Inherits System.Web.UI.Page

    Private Sub myMessageBox(ByVal messagestr As String)
        Logger.myLogMessage(Me.Page, messagestr)
    End Sub
    Private Sub myErrorBox(ByVal messagestr As String)
        Logger.myLogError(Me.Page, messagestr)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                ddlLanguage.DataBind()
                ddlLanguage.SelectedValue = 2
                bindGrid(False, False)
            End If
        Catch ex As Exception
            Logger.LogError("Season Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdCelebrity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdCelebrity.SelectedIndexChanged
        Try
            lbID.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbRowId"), Label).Text
            txtName.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbName"), Label).Text
            txtBirthPlace.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbPlaceofBirth"), Label).Text
            txtBirthDay.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbBirthDay"), Label).Text
            txtDeathDay.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbDeathDay"), Label).Text
            txtBioGraphy.Text = DirectCast(grdCelebrity.SelectedRow.FindControl("lbBiography"), Label).Text
            chkVerified.Checked = DirectCast(grdCelebrity.SelectedRow.FindControl("chkOK"), CheckBox).Checked
            ddlCelebLanguage.SelectedValue = DirectCast(grdCelebrity.SelectedRow.FindControl("lbLanguageId"), Label).Text
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("Celebrity Master", "grdCelebrity_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim strRowId As String, strCelebrityName As String, strPlaceOfBirth As String, strBirthDay As String, strDeathDay As String, strHomePage As String, strProfilePath As String, strAdult As String, strBiography As String
        strCelebrityName = txtName.Text.Trim
        Try
            If lbID.Text = "" Then
                strRowId = 0
            Else
                strRowId = lbID.Text
            End If

            strCelebrityName = txtName.Text.Trim
            strPlaceOfBirth = txtBirthPlace.Text.Trim
            strBirthDay = txtBirthDay.Text.Trim
            strDeathDay = txtDeathDay.Text.Trim
            strHomePage = ""
            strProfilePath = ""
            strAdult = "False"
            strBiography = txtBioGraphy.Text.Trim

            'sp_tmdb_celebrity
            Dim obj As New clsExecute
            If btnUpdate.Text = "ADD" Then
                obj.executeSQL("sp_tmdb_celebrity",
                               "tmdbCelebrityId~Name~placeofBirth~BirthDay~DeathDay~homepage~profilePath~adult~biography~verified~languageId~ActionUser~ActionType",
                               "Int~NVarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~NVarChar~Bit~Int~VarChar~Char",
                               strRowId & "~" & Logger.RemSplCharsEng(strCelebrityName) & "~" & Logger.RemSplCharsEng(strPlaceOfBirth) & "~" & strBirthDay & "~" & strDeathDay & "~~~" & strAdult & "~" & Logger.RemSplCharsEng(strBiography) & "~" & chkVerified.Checked & "~" & ddlCelebLanguage.SelectedValue & "~" & User.Identity.Name & "~A",
                                True, False)
            Else
                obj.executeSQL("sp_tmdb_celebrity",
                           "rowid~Name~placeofBirth~BirthDay~DeathDay~homepage~profilePath~adult~biography~verified~languageId~ActionUser~ActionType",
                           "Int~NVarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~NVarChar~Bit~Int~VarChar~Char",
                           strRowId & "~" & Logger.RemSplCharsEng(strCelebrityName) & "~" & Logger.RemSplCharsEng(strPlaceOfBirth) & "~" & strBirthDay & "~" & strDeathDay & "~" & strHomePage & "~" & strProfilePath & "~" & strAdult & "~" & Logger.RemSplCharsEng(strBiography) & "~" & chkVerified.Checked & "~" & ddlCelebLanguage.SelectedValue & "~" & User.Identity.Name & "~U",
                            True, False)
            End If
            bindGrid(True, False)
        Catch ex As Exception
            myErrorBox("Record Already exists of " & strCelebrityName & " for language " & ddlCelebLanguage.SelectedItem.Text)
        End Try
    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean, ByVal useSearch As Boolean)
        Dim sql As String
        sql = "select * ,profilepath as profilepath1, REPLACE(replace(name,'''',''),' ','') + '_' + convert(varchar,rowid) + '.jpg' as filename "
        sql = sql & " from tmdb_celebrity "
        sql = sql & " where name like '%" & txtSearch1.Text.Replace("'", "''") & "%' "
        If Not chkIgnore.Checked Then

            If chkOnlyVerified.Checked Then
                sql = sql & " and verified= 1 "
            Else
                sql = sql & " and verified= 0 "
            End If

            If chkImages.Checked Then
                sql = sql & " and profilepath <> '' and profilepath is not null "
            End If
            If chkBiography.Checked Then
                sql = sql & " and biography <> '' and biography is not null "
            End If
            If useSearch Then
                sql = sql & " and languageid ='" & ddlLanguage.SelectedValue & "' "
            End If
        End If

        sql = sql & " order by name "
        sqlDSCelebrityMaster.SelectCommand = sql
        sqlDSCelebrityMaster.SelectCommandType = SqlDataSourceCommandType.Text
        If paging = False Then
            grdCelebrity.PageIndex = 0
        End If
        grdCelebrity.SelectedIndex = -1
        grdCelebrity.DataBind()
        getCelebrityImageReport()
        clearAll()
    End Sub

    Private Sub clearAll()
        lbID.Text = ""
        txtName.Text = ""
        txtBirthPlace.Text = ""
        txtBirthDay.Text = ""
        txtDeathDay.Text = ""
        txtBioGraphy.Text = ""
        chkVerified.Checked = False
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(False, True)
    End Sub

    Protected Sub grdCelebrity_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCelebrity.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim lbRowId As Label = TryCast(e.Row.FindControl("lbRowId"), Label)
            Dim lbID As Label = TryCast(e.Row.FindControl("lbRowId"), Label)
            Dim lbCelebrityID As Label = TryCast(e.Row.FindControl("lbCelebrityID"), Label)
            Dim lbName As Label = TryCast(e.Row.FindControl("lbName"), Label)
            Dim lbFileName As Label = TryCast(e.Row.FindControl("lbFileName"), Label)
            Dim hyCast As HyperLink = TryCast(e.Row.FindControl("hyCast"), HyperLink)
            Dim hyCrew As HyperLink = TryCast(e.Row.FindControl("hyCrew"), HyperLink)
            Dim hyUpload As HyperLink = TryCast(e.Row.FindControl("hyUpload"), HyperLink)

            Dim objImage As New clsUploadModules
            lbFileName.Text = objImage.sanitizeImageFile(lbFileName.Text.Replace(".jpg", "")) & ".jpg"
            hyCast.NavigateUrl = "javascript:openCelebDetails('" & lbName.Text & "','" & lbCelebrityID.Text & "','Cast')"
            hyCrew.NavigateUrl = "javascript:openCelebDetails('" & lbName.Text & "','" & lbCelebrityID.Text & "','Crew')"
            hyUpload.NavigateUrl = "javascript:showDiv('MainContent_grdCelebrity_lbProgLogo_" & e.Row.RowIndex & "','MainContent_grdCelebrity_imgCelebrityImage_" & e.Row.RowIndex & "','" & lbID.Text & "','" & lbFileName.Text & "')"
        End If
    End Sub

    Protected Sub grdCelebrity_PageIndexChanged(sender As Object, e As EventArgs) Handles grdCelebrity.PageIndexChanged
        bindGrid(True, False)
    End Sub
    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        Dim fileSize As Integer = Int32.Parse(Path.GetFileName(e.FileSize)) / 1024

        If (fileSize > 5000) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('File size greater than 5000KB. Please optimize.');", True)
            Exit Sub
        End If
        Dim fileName As String = jsFileName.Value
        Dim RowId As String = jsTmdbCelebrityId.Value
        Dim currentMenu As HtmlGenericControl = DirectCast(Page.FindControl("jsFileName"), HtmlGenericControl)

        Dim myControl As Label = TryCast(Page.FindControl("jsFileName"), Label)

        If AsyncFileUpload1.FileName.ToLower.EndsWith(".jpg") Or AsyncFileUpload1.FileName.ToLower.EndsWith(".jpeg") Then
            System.Threading.Thread.Sleep(2000)
            If (AsyncFileUpload1.HasFile) Then
                Dim strpath As String = MapPath("~/uploads/CelebImages/") & fileName
                AsyncFileUpload1.SaveAs(strpath)
                Dim abc As New clsFTP

                abc.doS3Task(strpath, "/uploads/CelebImages")
                System.Threading.Thread.Sleep(200)
            End If
          
            Dim strUrl As String = "http://epgops.ndtv.com/uploads/CelebImages/" & fileName

            Dim obj As New clsExecute
            obj.executeSQL("sp_tmdb_celebrity", _
                           "rowid~profilePath~ActionUser~ActionType", _
                           "Int~VarChar~VarChar~Char", _
                           RowId & "~" & strUrl & "~" & User.Identity.Name & "~P", _
                            True, False)
        Else
            Throw New Exception("only .jpg and .jpeg files supported")
        End If

    End Sub

    Private Sub getCelebrityImageReport()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("rpt_CelebrityImages", "Languageid", "int", ddlLanguage.SelectedValue, True, False)

        
        If dt.Rows.Count > 0 Then
            Dim str As String = "Total Verified Celebrities : " & dt.Rows(0)("TotVerCelebs").ToString() & " (" & dt.Rows(0)("TotCelebs").ToString() & ")" & "<br/>" & _
                "Total " & ddlLanguage.SelectedItem.Text & " Celebrities : " & dt.Rows(0)("TotalCelebrities").ToString() & "<br/>" & _
                "Percentage with Pictures : " & dt.Rows(0)("perImage").ToString() & "<br/> " & _
                "Percentage with Biography : " & dt.Rows(0)("perBio").ToString() & "<br/>" & _
                "Percentage of Verified : " & dt.Rows(0)("perVer").ToString()
            lbReport.Text = str
        Else
            lbReport.Text = ""
        End If

    End Sub

    Protected Sub btnVerifyMultiple_Click(sender As Object, e As EventArgs) Handles btnVerifyMultiple.Click
        For i As Integer = 0 To grdCelebrity.Rows.Count - 1
            Dim chkVerified As CheckBox = TryCast(grdCelebrity.Rows(i).FindControl("chkOK"), CheckBox)
            Dim lbCelebrityID As Label = TryCast(grdCelebrity.Rows(i).FindControl("lbCelebrityID"), Label)
            Dim intVerified As Integer = 0
            If chkVerified.Checked = True Then
                intVerified = 1
                Dim obj As New clsExecute
                obj.executeSQL("update tmdb_celebrity set verified=" & intVerified & " where tmdbCelebrityId='" & lbCelebrityID.Text & "'", False)
                'Dim dt As New DataTable
                'adp.Fill(dt)
            End If
            
        Next i
        bindGrid(True, False)
    End Sub

    Protected Sub grdCelebrity_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCelebrity.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grdCelebrity.Rows(e.RowIndex).FindControl("lbRowId"), Label)
            Dim obj As New clsExecute
            obj.executeSQL("sp_tmdb_celebrity", _
                           "rowid~ActionUser~ActionType", _
                           "Int~VarChar~Char", _
                           lbRowId.Text & "~" & User.Identity.Name & "~D", _
                            True, False)
            bindGrid(True, False)
        Catch ex As Exception
            Logger.LogError("Celebrity Master", "grdCelebrity_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdCelebrity_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdCelebrity.RowDeleted
        If Not e.Exception Is Nothing Then
            'myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub
End Class