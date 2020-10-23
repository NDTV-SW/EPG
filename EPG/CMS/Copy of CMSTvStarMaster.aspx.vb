Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSTvStarMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                'ddlLanguageName.DataBind()
                'ddlLanguageName.SelectedValue = 2
                bindGrid(False)
                pnlUpload.Visible = False
            End If
        Catch ex As Exception
            Logger.LogError("TV Star Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Protected Sub grdTvStar_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles grdTvStar.RowCommand
        If e.CommandName = "Upload" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            Dim selectedRow As GridViewRow = grdTvStar.Rows(index)
            Dim contactCell As TableCell = selectedRow.Cells(1)
            Dim contact As String = contactCell.Text
            lbProfileId.Text = TryCast(selectedRow.FindControl("lbProfileId"), Label).Text
            Dim strStarName As String = TryCast(selectedRow.FindControl("lbName"), Label).Text
            lbFileName.Text = strStarName.Replace(" ", "_") & "_" & lbProfileId.Text & ".jpg"
            pnlUpload.Visible = True
        End If

    End Sub
    Protected Sub grdTvStar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdTvStar.SelectedIndexChanged
        Try

            lbProfileId.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbProfileId"), Label).Text
            txtName.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbName"), Label).Text
            txtCity.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbCity"), Label).Text
            txtBiography.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbBiography"), Label).Text
            txtDOB.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbDOB"), Label).Text
            txtPOB.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbPOB"), Label).Text
            txtTwitter.Text = DirectCast(grdTvStar.SelectedRow.FindControl("lbTwitterTag"), Label).Text
            ddlGender.SelectedValue = DirectCast(grdTvStar.SelectedRow.FindControl("lbGender"), Label).Text
            btnUpdate.Text = "UPDATE"
        Catch ex As Exception
            Logger.LogError("TV Star Master", "grdTvStar_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Dim outputId As String

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim obj As New clsExecute
        Dim dt As DataTable

        If btnUpdate.Text = "ADD" Then
            dt = obj.executeSQL("sp_mst_tvstars", _
                                    "name~city~gender~twittertag~dob~placeofBirth~biography~insertedby~modifiedby~action", _
                                    "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                    txtName.Text & "~" & txtCity.Text & "~" & ddlGender.SelectedValue & "~" & txtTwitter.Text & "~" & txtDOB.Text & "~" & _
                                    txtPOB.Text & "~" & txtBiography.Text & "~" & User.Identity.Name & "~" & User.Identity.Name & "~A", True, True)
        Else
            dt = obj.executeSQL("sp_mst_tvstars", _
                                    "name~city~gender~twittertag~dob~placeofBirth~biography~modifiedby~action~profileid", _
                                    "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                    txtName.Text & "~" & txtCity.Text & "~" & ddlGender.SelectedValue & "~" & txtTwitter.Text & "~" & txtDOB.Text & "~" & _
                                    txtPOB.Text & "~" & txtBiography.Text & "~" & User.Identity.Name & "~U~" & lbProfileId.Text, True, True)
        End If
        'lbFileName2.Value = txtName.Text.Replace(" ", "_") & "_" & outputId & ".jpg"
        bindGrid(True)

        'Dim adp As New SqlDataAdapter("sp_mst_tvstars", ConString)
        'adp.SelectCommand.CommandType = CommandType.StoredProcedure
        'adp.SelectCommand.Parameters.Add("name", SqlDbType.VarChar).Value = txtName.Text.Trim
        'adp.SelectCommand.Parameters.Add("city", SqlDbType.VarChar).Value = txtCity.Text.Trim
        'adp.SelectCommand.Parameters.Add("gender", SqlDbType.VarChar).Value = ddlGender.SelectedValue
        'adp.SelectCommand.Parameters.Add("twitterTag", SqlDbType.VarChar).Value = txtTwitter.Text.Trim
        'adp.SelectCommand.Parameters.Add("dob", SqlDbType.VarChar).Value = txtDOB.Text.Trim
        'adp.SelectCommand.Parameters.Add("placeOfBirth", SqlDbType.VarChar).Value = txtPOB.Text.Trim
        'adp.SelectCommand.Parameters.Add("biography", SqlDbType.VarChar).Value = txtBiography.Text.Trim
        'adp.SelectCommand.Parameters.Add("insertedBy", SqlDbType.VarChar).Value = User.Identity.Name
        'adp.SelectCommand.Parameters.Add("modifiedBy", SqlDbType.VarChar).Value = User.Identity.Name
        'adp.SelectCommand.Parameters.Add("id", SqlDbType.Int).Direction = ParameterDirection.Output

        'If btnUpdate.Text = "ADD" Then
        '    adp.SelectCommand.Parameters.Add("action", SqlDbType.VarChar).Value = "A"
        'Else
        '    adp.SelectCommand.Parameters.Add("action", SqlDbType.VarChar).Value = "U"
        '    adp.SelectCommand.Parameters.Add("profileid", SqlDbType.Int).Value = lbProfileId.Text.Trim
        'End If

        'Dim dt As New DataTable
        'adp.Fill(dt)
        'outputId = adp.SelectCommand.Parameters("id").Value.ToString()


    End Sub
    <System.Web.Services.WebMethod()> _
    Public Shared Function uploadFile(ByVal a As Integer)
        Return a
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function AddUpdate(ByVal vName As String, ByVal vCity As String, ByVal vGender As String, ByVal vTwitter As String, ByVal vDOB As String, _
                                     ByVal vPOB As String, ByVal vBioGraphy As String, ByVal vInsertedy As String, ByVal vModifiedBy As String, ByVal vAction As String, ByVal vProfileId As String)
        Dim obj As New clsExecute
        Dim dt As DataTable
        If vAction = "A" Then
            dt = obj.executeSQL("sp_mst_tvstars", _
                                    "name~city~gender~twittertag~dob~placeofBirth~biography~insertedby~modifiedby~action", _
                                    "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                    vName & "~" & vCity & "~" & vGender & "~" & vTwitter & "~" & vDOB & "~" & vPOB & "~" & _
                                    vBioGraphy & "~" & vInsertedy & "~" & vModifiedBy & "~A", True, True)
        Else
            dt = obj.executeSQL("sp_mst_tvstars", _
                                    "name~city~gender~twittertag~dob~placeofBirth~biography~modifiedby~action~profileid", _
                                    "varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar~varChar", _
                                    vName & "~" & vCity & "~" & vGender & "~" & vTwitter & "~" & vDOB & "~" & vPOB & "~" & _
                                    vBioGraphy & "~" & vModifiedBy & "~U~" & vProfileId, True, True)
        End If
        Return vName.Replace(" ", "_") & "_" & dt.Rows(0)(0).ToString()
    End Function
    
    Protected Sub bindGrid(ByVal paging As Boolean)
        sqlDSTvStar.SelectCommand = "select * from mst_tvStars where name like '%" & txtSearch.Text.Trim & "%'"
        sqlDSTvStar.SelectCommandType = SqlDataSourceCommandType.Text
        grdTvStar.DataSourceID = "sqlDSTvStar"
        If paging = False Then
            grdTvStar.PageIndex = 0
        End If
        grdTvStar.SelectedIndex = -1
        grdTvStar.DataBind()

        clearAll()
    End Sub

    Private Sub clearAll()
        txtName.Text = String.Empty
        txtCity.Text = String.Empty
        txtBiography.Text = String.Empty
        txtDOB.Text = String.Empty
        txtPOB.Text = String.Empty
        txtTwitter.Text = String.Empty
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
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    Protected Sub grdTvStar_PageIndexChanged(sender As Object, e As EventArgs) Handles grdTvStar.PageIndexChanged
        bindGrid(True)
    End Sub

    Protected Sub OnUploadComplete(sender As Object, e As AjaxFileUploadEventArgs)
        Try
            'If outputId <> 0 Then

            Dim fileName As String = lbFileName.Text

            'Dim profileId As String = lbProfileId.Text
            'AsyncFileUpload1.SaveAs("~/Uploads/tvstarimages/" & e.FileName)
            AsyncFileUpload1.SaveAs(Server.MapPath("~/Uploads/tvstarimages/" & fileName))
            'Dim strUrl As String = "http://epgops.ndtv.com/uploads/tvstarimages/" & fileName
            Dim adp As New SqlDataAdapter("update mst_tvstars set starPic='" & fileName & "' where profileId='" & lbProfileId.Text & "'", ConString)
            Dim dt As New DataTable
            adp.Fill(dt)

            outputId = 0
            bindGrid(True)
            'End If
            pnlUpload.Visible = False
        Catch ex As Exception

        End Try
    End Sub

End Class