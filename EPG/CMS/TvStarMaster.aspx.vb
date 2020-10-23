Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class TvStarMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                bindMultiSelectDropDown()
                ddlLanguage.DataBind()
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("TV Star Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    
    Protected Sub grdTvStar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdTvStar.SelectedIndexChanged
        Try
            lbProfileId.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbProfileId"), Label).Text
            txtName.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbName"), Label).Text
            'txtCity.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbCity"), Label).Text
            txtBiography.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbBiography"), Label).Text
            txtDOB.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbDOB2"), Label).Text
            txtPOB.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbPOB"), Label).Text
            txtTwitter.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbTwitterTag"), Label).Text
            ddlGender.SelectedValue = DirectCast(grdTvStar.SelectedRow.FindControl("lbGender"), Label).Text
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select languageid from tvstars_lang_mapping where profileid='" & lbProfileId.Text & "'", False)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    For Each item As ListItem In lstLanguage.Items
                        If item.Value = dt.Rows(i)(0).ToString Then
                            item.Selected = True
                        Else
                            item.Selected = False
                        End If
                    Next
                Next i
            End If

            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("TV Star Master", "grdTvStar_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable
            Dim strProfileId As String

            If btnUpdate.Text = "ADD" Then
                If txtDOB.Text.Trim = "" Then
                    dt = obj.executeSQL("sp_mst_tvstars", _
                                            "name~gender~twittertag~placeofBirth~biography~insertedby~modifiedby~action", _
                                            "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                            txtName.Text & "~" & ddlGender.SelectedValue & "~" & txtTwitter.Text & "~" & _
                                            txtPOB.Text & "~" & txtBiography.Text & "~" & User.Identity.Name & "~" & User.Identity.Name & "~A", True, True)
                Else
                    dt = obj.executeSQL("sp_mst_tvstars", _
                                            "name~gender~twittertag~dob~placeofBirth~biography~insertedby~modifiedby~action", _
                                            "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                            txtName.Text & "~" & ddlGender.SelectedValue & "~" & txtTwitter.Text & "~" & txtDOB.Text & "~" & _
                                            txtPOB.Text & "~" & txtBiography.Text & "~" & User.Identity.Name & "~" & User.Identity.Name & "~A", True, True)
                End If
                strProfileId = dt.Rows(0)(0).ToString
            Else
                If txtDOB.Text.Trim = "" Then
                    dt = obj.executeSQL("sp_mst_tvstars", _
                                            "name~gender~twittertag~placeofBirth~biography~modifiedby~action~profileid", _
                                            "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                            txtName.Text & "~" & ddlGender.SelectedValue & "~" & txtTwitter.Text & "~" & _
                                            txtPOB.Text & "~" & txtBiography.Text & "~" & User.Identity.Name & "~U~" & lbProfileId.Text, True, True)

                Else
                    dt = obj.executeSQL("sp_mst_tvstars", _
                                            "name~gender~twittertag~dob~placeofBirth~biography~modifiedby~action~profileid", _
                                            "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                            txtName.Text & "~" & ddlGender.SelectedValue & "~" & txtTwitter.Text & "~" & txtDOB.Text & "~" & _
                                            txtPOB.Text & "~" & txtBiography.Text & "~" & User.Identity.Name & "~U~" & lbProfileId.Text, True, True)
                End If
                strProfileId = lbProfileId.Text
                obj.executeSQL("delete from tvstars_lang_mapping where profileid='" & strProfileId & "'", False)
                End If

                For Each item As ListItem In lstLanguage.Items
                    If item.Selected Then
                        Try
                            Dim sql As String
                            sql = "insert into tvstars_lang_mapping(profileId,LanguageId) values('" & strProfileId & "','" & item.Value & "')"
                            Dim adp1 As New SqlDataAdapter(sql, ConString)
                            Dim dt1 As New DataTable
                            adp1.Fill(dt1)
                        Catch
                        End Try
                    End If
                Next

                bindGrid(True)
        Catch ex As Exception
            Logger.LogError("TV Star Master", "btnUpdate_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub bindMultiSelectDropDown()
        lstLanguage.Items.Clear()

        Dim adp As New SqlDataAdapter("select languageId,FullName from mst_language where active=1 order by 2", ConString)
        Dim dt As New DataTable
        adp.Fill(dt)

        If dt.Rows.Count > 0 Then
            For j As Integer = 0 To (dt.Rows.Count - 1)
                lstLanguage.Items.Add(New ListItem(dt.Rows(j)(1).ToString, Convert.ToInt32(dt.Rows(j)(0).ToString)))
            Next j
        End If
    End Sub
    Protected Sub bindGrid(ByVal paging As Boolean)
        Dim strSearchString As String
        If txtSearch1.Text.Trim = "" Then
            strSearchString = "0"
        Else
            strSearchString = txtSearch1.Text
        End If
        'If sqlDSCelebrityMaster.SelectParameters.Count = 4 Then
        sqlDSCelebrityMaster.SelectParameters.Remove(sqlDSCelebrityMaster.SelectParameters("search_string"))
        sqlDSCelebrityMaster.SelectParameters.Remove(sqlDSCelebrityMaster.SelectParameters("withImage"))
        sqlDSCelebrityMaster.SelectParameters.Remove(sqlDSCelebrityMaster.SelectParameters("withBiography"))
        sqlDSCelebrityMaster.SelectParameters.Remove(sqlDSCelebrityMaster.SelectParameters("languageid"))
        'sqlDSCelebrityMaster.SelectParameters.RemoveAt(1)
        'sqlDSCelebrityMaster.SelectParameters.RemoveAt(2)
        'sqlDSCelebrityMaster.SelectParameters.RemoveAt(3)
        'End If

        Dim sql As String = "sp_search_mst_tvstars"
        sqlDSCelebrityMaster.SelectCommand = sql
        sqlDSCelebrityMaster.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        sqlDSCelebrityMaster.SelectParameters.Add("search_string", Data.DbType.String, strSearchString)
        If chkImages.Checked Then
            sqlDSCelebrityMaster.SelectParameters.Add("withImage", Data.DbType.String, "1")
        Else
            sqlDSCelebrityMaster.SelectParameters.Add("withImage", Data.DbType.String, "0")
        End If
        If chkBiography.Checked = True Then
            sqlDSCelebrityMaster.SelectParameters.Add("withBiography", Data.DbType.String, "1")
        Else
            sqlDSCelebrityMaster.SelectParameters.Add("withBiography", Data.DbType.String, "0")
        End If

        If ddlLanguage.SelectedValue = 0 Then
            sqlDSCelebrityMaster.SelectParameters.Add("languageid", Data.DbType.String, "0")
        Else
            sqlDSCelebrityMaster.SelectParameters.Add("languageid", Data.DbType.String, ddlLanguage.SelectedValue.ToString)
        End If

        If paging = False Then
            grdTvStar.PageIndex = 0
        End If
        grdTvStar.SelectedIndex = -1
        grdTvStar.DataBind()
        clearAll()
        getCelebrityImageReport()
    End Sub
    Private Sub getCelebrityImageReport()
        Dim adp As New SqlDataAdapter("rpt_tvStarReport", ConString)
        adp.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        adp.Fill(dt)
        If dt.Rows.Count > 0 Then
            Dim str As String = "Total Celebrities : " & dt.Rows(0)("TotalCelebrities").ToString() & "<br/>" & _
                "Percentage with Pictures : " & dt.Rows(0)("perImage").ToString() & "<br/> " & _
                "Percentage with Biography : " & dt.Rows(0)("perBio").ToString()
            lbReport.Text = str
        Else
            lbReport.Text = ""
        End If

    End Sub

    Private Sub clearAll()
        lbProfileId.Text = String.Empty
        txtName.Text = String.Empty
        txtBiography.Text = String.Empty
        txtDOB.Text = String.Empty
        txtPOB.Text = String.Empty
        txtTwitter.Text = String.Empty
        lstLanguage.SelectedIndex = -1
        btnUpdate.Text = "ADD"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(False)
    End Sub

    Dim intSno As Integer
    Protected Sub grdTvStar_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTvStar.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1
            Dim lbProfileId As Label = TryCast(e.Row.FindControl("lbProfileId"), Label)
            Dim lbName As Label = TryCast(e.Row.FindControl("lbName"), Label)
            Dim hyName As HyperLink = TryCast(e.Row.FindControl("hyName"), HyperLink)
            Dim lbFileName As Label = TryCast(e.Row.FindControl("lbFileName"), Label)

            hyName.NavigateUrl = "javascript:openWin('" & lbProfileId.Text & "')"

            Dim hyUpload As HyperLink = TryCast(e.Row.FindControl("hyUpload"), HyperLink)
            hyUpload.NavigateUrl = "javascript:showDiv('MainContent_grdTvStar_lbProgLogo_" & e.Row.RowIndex & "','MainContent_grdTvStar_imgCelebrityImage_" & e.Row.RowIndex & "','" & lbProfileId.Text & "','" & lbFileName.Text & "')"
        End If
    End Sub

    Protected Sub grdTvStar_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTvStar.PageIndexChanging
        grdTvStar.PageIndex = e.NewPageIndex
        bindGrid(True)
    End Sub


    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        Dim fileSize As Integer = Int32.Parse(Path.GetFileName(e.FileSize)) / 1024

        If (fileSize > 5000) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('File size greater than 5000KB. Please optimize.');", True)
            Exit Sub
        End If
        Dim fileName As String = jsFileName.Value
        Dim profileId As String = jsProfileId.Value

        Dim myControl As Label = TryCast(Page.FindControl("jsFileName"), Label)

        If AsyncFileUpload1.FileName.ToLower.EndsWith(".jpg") Or AsyncFileUpload1.FileName.ToLower.EndsWith(".jpeg") Then
            If (AsyncFileUpload1.HasFile) Then
                Dim strpath As String = MapPath("~/Uploads/tvStarImages/") & fileName
                AsyncFileUpload1.SaveAs(strpath)
                'System.Threading.Thread.Sleep(1000)
                Dim abc As New clsFTP
                abc.doS3Task(strpath, "/uploads/tvStarImages")
                System.Threading.Thread.Sleep(200)
            End If
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            Dim strUrl As String = "uploads/tvStarImages/" & fileName
            Dim adp As New SqlDataAdapter("update mst_tvstars set starpic='" & strUrl & "' where profileId='" & profileId & "'", MyConnection)
            Dim dt As New DataTable
            adp.Fill(dt)
        Else
            Throw New Exception("only .jpg and .jpeg files supported")
        End If

    End Sub

    Protected Sub grdTvStar_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdTvStar.RowDeleting
        Try
            Dim lbProfileId As Label = DirectCast(grdTvStar.Rows(e.RowIndex).FindControl("lbProfileId"), Label)
            Dim strSql As String
            Dim obj As New clsExecute

            strSql = "delete from tvstars_lang_mapping where profileId='" & lbProfileId.Text & "'"
            obj.executeSQL(strSql, False)

            strSql = "delete from tvstars_prog_mapping where profileId='" & lbProfileId.Text & "'"
            obj.executeSQL(strSql, False)

            strSql = "delete from mst_tvstars where profileId='" & lbProfileId.Text & "'"
            obj.executeSQL(strSql, False)

            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("TV Star Master", "grdTvStar_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdTvStar_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdTvStar.RowDeleted
        If Not e.Exception Is Nothing Then
            e.ExceptionHandled = True
        End If
    End Sub

    Protected Sub grdTvStar_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdTvStar.Sorting
        bindGrid(True)
    End Sub
End Class