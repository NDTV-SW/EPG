Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class ChannelSummary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then


        End If
    End Sub


    Private Function ReplaceDoubleDoubleQuotes(ByVal text As String) As String
        Return text.Trim.Replace("'", "''")
    End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveMe.Click

        Dim obj As New clsExecute
        If btnSaveMe.Text = "SAVE" Then
            obj.executeSQL("insert into fpc_channelsummary(Channelid,SDHD,ChannelType,Category,SingleFPCForAllRegions,Genre,GenericSynopsis,EpisodicSynopsisProgLevel,EpisodicSynopsisInBulkForMultipleEpisodes," & _
                           "EpisodicTitle,EpisodeNo,StarCast,GuestName,GenericImage,EpisodicImage,PressRelease,SpecialPrograms,ImagesofSpecialPrograms,ProviderName1,ProviderMail1,ProviderContact1,ProviderName2,ProviderMail2,ProviderContact2,ProviderName3,ProviderMail3,ProviderContact3," & _
                           "escalationpoc1,escalationpoc2,escalationpoc3,escalationpoccontact1,escalationpoccontact2,escalationpoccontact3,active,feed) " & _
            " values('" & txtChannel.Text & "','" & ddlSDHD.SelectedValue & "','" & ddlChannelType.SelectedValue & "','" & ddlCategory.SelectedValue & "'," & _
            " '" & chkSingleFPCforAllRegions.Checked & "','" & ddlGenre.SelectedValue & "','" & chkGenericSynopsis.Checked & "','" & chkEpiodicSynopsisProgLevel.Checked & "','" & chkEPisodicSynopsisInBulkfForMultipleEpisodes.Checked & "'," & _
            " '" & chkEPisodicTitle.Checked & "','" & chkEpisodeNo.Checked & "','" & chkStarCast.Checked & "','" & chkGuestName.Checked & "'," & _
            " '" & chkGenericImage.Checked & "','" & chkEpisodicImage.Checked & "','" & chkPressRelease.Checked & "','" & chkSpecialPrograms.Checked & "','" & chkImagesofSpecialPrograms.Checked & "'," & _
            " '" & txtProviderName1.Text & "','" & txtProviderMail1.Text & "','" & txtProviderContact1.Text & "','" & txtProviderName2.Text & "','" & txtProviderMail2.Text & "','" & txtProviderContact2.Text & "'," & _
            " '" & txtProviderName3.Text & "','" & txtProviderMail3.Text & "','" & txtProviderContact3.Text & "','" & txtEscalationPoc1.Text & "','" & txtEscalationPoc2.Text & "','" & txtEscalationPoc3.Text & "'," & _
            " '" & txtEscalationPOCContact1.Text & "','" & txtEscalationPOCContact2.Text & "','" & txtEscalationPOCContact3.Text & "','" & chkActive.Checked & "','" & txtFeed.Text & "')", False)
        Else
            Dim sql As String = ""
            sql = sql & "update fpc_channelsummary set "

            sql = sql & "Channelid='" & txtChannel.Text & "',"
            sql = sql & "SDHD='" & ddlSDHD.SelectedValue & "',"
            sql = sql & "ChannelType='" & ddlChannelType.SelectedValue & "',"
            sql = sql & "Category='" & ddlCategory.SelectedValue & "',"
            sql = sql & "SingleFPCForAllRegions='" & chkSingleFPCforAllRegions.Checked & "',"
            sql = sql & "Genre='" & ddlGenre.SelectedValue & "',"
            sql = sql & "GenericSynopsis='" & chkGenericSynopsis.Checked & "',"
            sql = sql & "EpisodicSynopsisProgLevel='" & chkEpiodicSynopsisProgLevel.Checked & "',"
            sql = sql & "EpisodicSynopsisInBulkForMultipleEpisodes='" & chkEPisodicSynopsisInBulkfForMultipleEpisodes.Checked & "',"
            sql = sql & "EpisodicTitle='" & chkEPisodicTitle.Checked & "',"
            sql = sql & "EpisodeNo='" & chkEpisodeNo.Checked & "',"
            sql = sql & "StarCast='" & chkStarCast.Checked & "',"
            sql = sql & "GuestName='" & chkGuestName.Checked & "',"
            sql = sql & "GenericImage='" & chkGenericImage.Checked & "',"
            sql = sql & "EpisodicImage='" & chkEpisodicImage.Checked & "',"
            sql = sql & "PressRelease='" & chkPressRelease.Checked & "',"
            sql = sql & "SpecialPrograms='" & chkSpecialPrograms.Checked & "',"
            sql = sql & "ImagesofSpecialPrograms='" & chkImagesofSpecialPrograms.Checked & "',"
            sql = sql & "ProviderName1='" & txtProviderName1.Text & "',"
            sql = sql & "ProviderMail1='" & txtProviderMail1.Text & "',"
            sql = sql & "ProviderContact1='" & txtProviderContact1.Text & "',"
            sql = sql & "ProviderName2='" & txtProviderName2.Text & "',"
            sql = sql & "ProviderMail2='" & txtProviderMail2.Text & "',"
            sql = sql & "ProviderContact2='" & txtProviderContact2.Text & "',"
            sql = sql & "ProviderName3='" & txtProviderName3.Text & "',"
            sql = sql & "ProviderMail3='" & txtProviderMail3.Text & "',"
            sql = sql & "ProviderContact3='" & txtProviderContact3.Text & "',"

            sql = sql & "escalationpoc1='" & txtEscalationPoc1.Text & "',"
            sql = sql & "escalationpoc2='" & txtEscalationPoc2.Text & "',"
            sql = sql & "escalationpoc3='" & txtEscalationPoc3.Text & "',"
            sql = sql & "escalationpoccontact1='" & txtEscalationPOCContact1.Text & "',"
            sql = sql & "escalationpoccontact2='" & txtEscalationPOCContact2.Text & "',"
            sql = sql & "escalationpoccontact3='" & txtEscalationPOCContact3.Text & "',"
            

            sql = sql & "active='" & chkActive.Checked & "',"
            sql = sql & "feed='" & txtFeed.Text & "' "
            sql = sql & " where id='" & lbID.Text & "'"
            obj.executeSQL(sql, False)
        End If


        clearAll()
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged


        Dim obj As New clsExecute
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        Dim dt As DataTable = obj.executeSQL("SELECT * FROM fpc_channelsummary WHERE id='" & lbID.Text & "'", False)

        txtChannel.Text = dt.Rows(0)("channelid").ToString
        txtFeed.Text = dt.Rows(0)("feed").ToString

        ddlSDHD.SelectedValue = dt.Rows(0)("sdhd").ToString
        ddlChannelType.SelectedValue = dt.Rows(0)("channeltype").ToString
        Try
            ddlCategory.SelectedValue = dt.Rows(0)("category").ToString
        Catch ex As Exception

        End Try
        Try
            ddlGenre.SelectedValue = dt.Rows(0)("Genre").ToString
        Catch ex As Exception

        End Try

        chkSingleFPCforAllRegions.Checked = dt.Rows(0)("SingleFPCForAllRegions").ToString
        chkGenericSynopsis.Checked = dt.Rows(0)("GenericSynopsis").ToString
        chkEpiodicSynopsisProgLevel.Checked = dt.Rows(0)("EpisodicSynopsisProgLevel").ToString
        chkEPisodicSynopsisInBulkfForMultipleEpisodes.Checked = dt.Rows(0)("EpisodicSynopsisInBulkForMultipleEpisodes").ToString
        chkEPisodicTitle.Checked = dt.Rows(0)("EpisodicTitle").ToString
        chkEpisodeNo.Checked = dt.Rows(0)("Episodeno").ToString
        chkStarCast.Checked = dt.Rows(0)("starcast").ToString
        chkGuestName.Checked = dt.Rows(0)("guestname").ToString
        chkGenericImage.Checked = dt.Rows(0)("genericimage").ToString
        chkEpisodicImage.Checked = dt.Rows(0)("episodicImage").ToString
        txtProviderName1.Text = dt.Rows(0)("ProviderName1").ToString
        txtProviderMail1.Text = dt.Rows(0)("ProviderMail1").ToString
        txtProviderContact1.Text = dt.Rows(0)("ProviderContact1").ToString
        txtProviderName2.Text = dt.Rows(0)("ProviderName2").ToString
        txtProviderMail2.Text = dt.Rows(0)("ProviderMail2").ToString
        txtProviderContact2.Text = dt.Rows(0)("ProviderContact2").ToString
        txtProviderName3.Text = dt.Rows(0)("ProviderName3").ToString
        txtProviderMail3.Text = dt.Rows(0)("ProviderMail3").ToString
        txtProviderContact3.Text = dt.Rows(0)("ProviderContact3").ToString

        txtEscalationPoc1.Text = dt.Rows(0)("escalationpoc1").ToString
        txtEscalationPoc2.Text = dt.Rows(0)("escalationpoc2").ToString
        txtEscalationPoc3.Text = dt.Rows(0)("escalationpoc3").ToString
        txtEscalationPOCContact1.Text = dt.Rows(0)("escalationpoccontact1").ToString
        txtEscalationPOCContact2.Text = dt.Rows(0)("escalationpoccontact2").ToString
        txtEscalationPOCContact3.Text = dt.Rows(0)("escalationpoccontact3").ToString
        
        chkPressRelease.Checked = dt.Rows(0)("PressRelease").ToString

        chkSpecialPrograms.Checked = dt.Rows(0)("SpecialPrograms").ToString
        chkImagesofSpecialPrograms.Checked = dt.Rows(0)("ImagesofSpecialPrograms").ToString
        chkActive.Checked = dt.Rows(0)("active").ToString


        lbID.Text = dt.Rows(0)("id").ToString
        btnSaveMe.Text = "UPDATE"
    End Sub

    Private Sub clearAll()

        txtChannel.Text = ""
        txtFeed.Text = "'"

        ddlSDHD.SelectedIndex = 0
        ddlChannelType.SelectedIndex = 0
        ddlCategory.SelectedIndex = 0

        ddlGenre.SelectedIndex = 0

        chkSingleFPCforAllRegions.Checked = False
        chkGenericSynopsis.Checked = False
        chkEpiodicSynopsisProgLevel.Checked = False
        chkEPisodicSynopsisInBulkfForMultipleEpisodes.Checked = False
        chkEPisodicTitle.Checked = False
        chkEpisodeNo.Checked = False
        chkStarCast.Checked = False
        chkGuestName.Checked = False
        chkGenericImage.Checked = False
        chkEpisodicImage.Checked = False
        txtProviderName1.Text = ""
        txtProviderMail1.Text = ""
        txtProviderContact1.Text = ""
        txtProviderName2.Text = ""
        txtProviderMail2.Text = ""
        txtProviderContact2.Text = ""
        txtProviderName3.Text = ""
        txtProviderMail3.Text = ""
        txtProviderContact3.Text = ""

        txtEscalationPoc1.Text = ""
        txtEscalationPoc2.Text = ""
        txtEscalationPoc3.Text = ""
        txtEscalationPOCContact1.Text = ""
        txtEscalationPOCContact2.Text = ""
        txtEscalationPOCContact3.Text = ""

        chkPressRelease.Checked = False

        chkSpecialPrograms.Checked = False
        chkImagesofSpecialPrograms.Checked = False

        lbID.Text = ""
        chkActive.Checked = True
        grd.SelectedIndex = -1
        grd.DataBind()
        btnSaveMe.Text = "SAVE"

        
    End Sub


    Dim intColCount

    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim gridView As GridView = TryCast(sender, GridView)
            intColCount = gridView.Columns.Count
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            For i As Integer = 0 To intColCount - 1
                If (e.Row.Cells(i).Text = "False") Then
                    e.Row.Cells(i).CssClass = "alert-danger"
                ElseIf (e.Row.Cells(i).Text = "True") Then
                    e.Row.Cells(i).CssClass = "alert-success"
                End If
            Next
        End If
    End Sub
End Class