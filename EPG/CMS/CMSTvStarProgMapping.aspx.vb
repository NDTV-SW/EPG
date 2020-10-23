Imports System.Data
Imports System.Data.SqlClient
Imports ExcelLibrary
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports AjaxControlToolkit

Public Class CMSTvStarProgMapping
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                ddlLanguage.DataBind()
                ddlGenre.DataBind()
                bindProgramme()
                bindGrid(False)
            End If
        Catch ex As Exception
            Logger.LogError("TV Star Prog Mapping", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdTvStarProgmapping_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdTvStarProgmapping.SelectedIndexChanged
        Try
            lbRowId.Text = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbRowId"), Label).Text
            lbName.Text = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbName"), Label).Text
            ddlTvStar.SelectedValue = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbProfId"), Label).Text
            txtRoleName.Text = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbRoleName"), Label).Text
            txtRoleDesc.Text = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbRoleDesc"), Label).Text
            ddlRating.SelectedValue = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbRating"), Label).Text
            txtLikes.Text = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbLikes"), Label).Text
            imgPic.ImageUrl = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbPic"), Label).Text
            hyPic.NavigateUrl = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbPic"), Label).Text
            btnUpdate.Text = "UPDATE"
            lbProgId.Text = DirectCast(grdTvStarProgmapping.SelectedRow.FindControl("lbProgid"), Label).Text
            lstProgramme.SelectionMode = ListSelectionMode.Single
            lstProgramme.SelectedValue = lbProgId.Text
        Catch ex As Exception
            Logger.LogError("TV Star Master", "grdTvStarProgmapping_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim obj As New clsExecute
            Dim strParams As String = "progid~rolename~roledesc~rating~likes~insertedBy~modifiedBy~profileid~action~rowid"
            Dim strTypes As String = "int~varchar~varchar~int~int~varchar~varchar~int~varchar~int"
            For Each item As ListItem In lstProgramme.Items
                If item.Selected Then
                    Dim strValues As String = item.Value & "~" & txtRoleName.Text & "~" & txtRoleDesc.Text & "~"
                    strValues = strValues & ddlRating.SelectedValue & "~" & IIf(txtLikes.Text = "", "0", txtLikes.Text) & "~" & User.Identity.Name & "~" & User.Identity.Name & "~"
                    strValues = strValues & ddlTvStar.SelectedValue & "~" & IIf(btnUpdate.Text = "ADD", "A", "U") & "~" & IIf(btnUpdate.Text = "ADD", "0", lbRowId.Text)
                    obj.executeSQL("sp_tvstars_prog_mapping", strParams, strTypes, strValues, True, False)

                    obj.executeSQL("sp_tvstars_prog_mapping_regional", "ProgId~ProfileId~LanguageId~ProfileName~RoleName~roledescription~Actionuser~Action", _
                                  "Int~Int~Int~nVarChar~nVarChar~nVarChar~Varchar~Char", _
                                   item.Value & "~" & ddlTvStar.SelectedValue & "~1~" & ddlTvStar.Text & "~" & txtRoleName.Text & "~" & txtRoleDesc.Text & "~" & User.Identity.Name & "~A", True, False)
                    item.Selected = False
                End If
            Next
            bindGrid(True)
        Catch ex As Exception
            Logger.LogError("TV Star Prog Mapping", "btnUpdate_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub bindGrid(ByVal paging As Boolean)
        sqlDSTvStarGallery.SelectCommand = "select 'http://epgops.ndtv.com/' + pic as pic,replace(c.Name + '-' + convert(varchar,a.profileid) + '-' + convert(varchar,a.progid) + '.jpg',' ','_') fileName,c.Name,b.channelid, b.ProgName,* from tvstars_prog_mapping a join  mst_program b on a.Progid=b.ProgID join mst_tvstars c on a.ProfileID=c.ProfileID and c.Name like '%" & txtSearch.Text.Trim & "%' and a.Progid in (select distinct Progid from mst_epg) order by 3"
        sqlDSTvStarGallery.SelectCommandType = SqlDataSourceCommandType.Text
        grdTvStarProgmapping.DataSourceID = "sqlDSTvStarGallery"
        If paging = False Then
            grdTvStarProgmapping.PageIndex = 0
        End If
        grdTvStarProgmapping.SelectedIndex = -1
        grdTvStarProgmapping.DataBind()
        clearAll()
    End Sub
    Protected Sub bindGrid1(ByVal paging As Boolean)
        sqlDSTvStarGallery.SelectCommand = "select 'http://epgops.ndtv.com/' + pic as pic,replace(c.Name + '-' + convert(varchar,a.profileid) + '-' + convert(varchar,a.progid) + '.jpg',' ','_') fileName,c.Name,b.channelid, b.ProgName,* from tvstars_prog_mapping a join  mst_program b on a.Progid=b.ProgID join mst_tvstars c on a.ProfileID=c.ProfileID and a.Progid='" & ddlProgramme.SelectedValue & "' order by 3"
        sqlDSTvStarGallery.SelectCommandType = SqlDataSourceCommandType.Text
        grdTvStarProgmapping.DataSourceID = "sqlDSTvStarGallery"
        If paging = False Then
            grdTvStarProgmapping.PageIndex = 0
        End If
        grdTvStarProgmapping.SelectedIndex = -1
        grdTvStarProgmapping.DataBind()
        clearAll()
    End Sub
    Protected Sub bindProgramme()
        Dim sql As String
       
        sql = "select * from fn_getTVStarsProgrammeforMapping('" & ddlGenre.SelectedValue & "','" & ddlLanguage.SelectedValue & "') order by 1"

        sqlDSProgramme.SelectCommand = sql
        sqlDSProgramme.SelectCommandType = SqlDataSourceCommandType.Text

        ddlProgramme.DataSourceID = "sqlDSProgramme"
        ddlProgramme.DataTextField = "Programme"
        ddlProgramme.DataValueField = "ProgId"
        ddlProgramme.DataBind()

        lstProgramme.DataSourceID = "sqlDSProgramme"
        lstProgramme.DataTextField = "Programme"
        lstProgramme.DataValueField = "ProgId"
        lstProgramme.DataBind()

    End Sub

    Private Sub clearAll()
        'txtName.Text = String.Empty
        lbName.Text = String.Empty
        txtRoleName.Text = String.Empty
        txtRoleDesc.Text = String.Empty
        txtLikes.Text = String.Empty
        imgPic.ImageUrl = String.Empty
        hyPic.NavigateUrl = String.Empty
        btnUpdate.Text = "ADD"
        lstProgramme.SelectionMode = ListSelectionMode.Multiple
        'bindGrid(True)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid(False)
    End Sub

    Protected Sub btnSearch1_Click(sender As Object, e As EventArgs) Handles btnSearch1.Click
        bindGrid1(False)
    End Sub

    Dim intSno As Integer
    Protected Sub grdTvStarProgmapping_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTvStarProgmapping.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbProfileId As Label = TryCast(e.Row.FindControl("lbProfId"), Label)
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            Dim lbFileName As Label = TryCast(e.Row.FindControl("lbFileName"), Label)
            Dim lbRowId As Label = TryCast(e.Row.FindControl("lbRowId"), Label)
            Dim hyUpload As HyperLink = TryCast(e.Row.FindControl("hyUpload"), HyperLink)
            hyUpload.NavigateUrl = "javascript:showDiv('MainContent_grdTvStarProgmapping_imgTvStarLogo_" & e.Row.RowIndex & "','" & lbRowId.Text & "','" & lbFileName.Text & "')"
            intSno = intSno + 1
        End If
        If e.Row.RowType = DataControlRowType.Footer Then

        End If
    End Sub

    Protected Sub grdTvStarProgmapping_PageIndexChanging(sender As Object, e As EventArgs) Handles grdTvStarProgmapping.PageIndexChanging

        bindGrid(True)
    End Sub
    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        Dim fileSize As Integer = Int32.Parse(Path.GetFileName(e.FileSize)) / 1024

        If (fileSize > 5000) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('File size greater than 5000KB. Please optimize.');", True)
            Exit Sub
        End If

        Dim fileName As String = jsFileName.Value
        Dim rowid As String = jsRowid.Value
        If AsyncFileUpload1.FileName.ToLower.EndsWith(".jpg") Or AsyncFileUpload1.FileName.ToLower.EndsWith(".jpeg") Then
            System.Threading.Thread.Sleep(1000)
            If (AsyncFileUpload1.HasFile) Then
                Dim strpath As String = MapPath("~/Uploads/TvStarProgImages/") & fileName
                AsyncFileUpload1.SaveAs(strpath)
                System.Threading.Thread.Sleep(2000)
            End If

            Dim strUrl As String = "uploads/TvStarProgImages/" & fileName
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("update tvstars_prog_mapping set pic='" & strUrl & "' where rowid='" & rowid & "'", False)

        Else
            Throw New Exception("only .jpg and .jpeg files supported")
        End If

    End Sub

    Protected Sub grdTvStarProgmapping_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTvStarProgmapping.PageIndexChanging
        grdTvStarProgmapping.PageIndex = e.NewPageIndex
        bindGrid(True)
    End Sub

    Protected Sub ddlGenre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGenre.SelectedIndexChanged
         bindProgramme()
    End Sub

    Protected Sub ddlLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLanguage.SelectedIndexChanged
          bindProgramme()
    End Sub
End Class