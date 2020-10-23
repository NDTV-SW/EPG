Public Class verifyrichmeta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim li As HtmlGenericControl = TryCast(Master.FindControl("liverifyrichmeta"), HtmlGenericControl)
            li.Attributes.Add("class", "active")
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim obj As New clsExecute
            Dim strParams As String = "mode~id~type~name~displayname~languageid~originallanguageid~dubbedlanguge~parentalguide~rating~seasonno~seasonname~synopsis~longsynopsis~starcast~director~producer~writer~trivia~awards~releaseyear~country~genre~subgenre~verified~broadcasterid~team1~team2~series~sporttype~venue~showhost~active~updatedby~tags~genrename1~genrename2"
            Dim strType As String = "varchar~int~varchar~varchar~varchar~int~int~int~varchar~float~int~varchar~varchar~varchar~varchar~varchar~varchar~varchar~varchar~varchar~int~varchar~int~int~bit~int~varchar~varchar~varchar~varchar~varchar~varchar~bit~varchar~varchar~varchar~varchar"
            Dim strVals As String
            If btnAdd.Text = "Add" Then
                strVals = "A~0~"
            Else
                strVals = "U~" & lbId.Text & "~"
            End If
            strVals = strVals & ddlType.SelectedValue & "~" & txtName.Text & "~" & txtDisplayName.Text & "~" & ddlOriginalLanguage.SelectedValue & "~" & ddlOriginalLanguage.SelectedValue & "~" & ddlDubbedLanguage.SelectedValue & "~"
            strVals = strVals & ddlParentalGuide.SelectedValue & "~" & txtRating.Text & "~" & txtSeasonNo.Text & "~" & txtSeasonName.Text & "~" & txtSynopsis.Text & "~" & txtLongSynopsis.Text & "~" & txtStarCast.Text & "~"
            strVals = strVals & txtDirector.Text & "~" & txtProducer.Text & "~" & txtWriter.Text & "~" & txtTrivia.Text & "~" & txtAwards.Text & "~" & txtReleaseYear.Text & "~" & txtCountry.Text & "~"
            strVals = strVals & ddlGenre.SelectedValue & "~" & ddlSubGenre.SelectedValue & "~" & chkVerified.Checked & "~" & ddlBroadcaster.SelectedValue & "~" & txtTeam1.Text & "~" & txtTeam2.Text & "~" & txtSeries.Text & "~"
            strVals = strVals & ddlSportsType.SelectedValue & "~" & txtVenue.Text & "~" & txtShowHost.Text & "~" & chkActive.Checked & "~" & User.Identity.Name & "~" & txtTags.Text & "~" & ddlGenre.SelectedItem.Text & "~" & ddlSubGenre.SelectedItem.Text

            obj.executeSQL("sp_richmeta", strParams, strType, strVals, True, False)
            clearall()
        Catch ex As Exception
            Logger.LogError(Logger.whichPageCalledMe, Logger.whichMethodCalledMe, ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearall()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        clearall()
    End Sub

    Private Sub clearall()
        txtName.Text = ""
        txtDisplayName.Text = ""
        txtRating.Text = ""
        txtSeasonNo.Text = ""
        txtSeasonName.Text = ""
        txtStarCast.Text = ""
        txtDirector.Text = ""
        txtProducer.Text = ""
        txtWriter.Text = ""
        txtSynopsis.Text = ""
        txtLongSynopsis.Text = ""
        txtTrivia.Text = ""
        txtAwards.Text = ""
        txtReleaseYear.Text = ""
        txtCountry.Text = ""
        txtTeam1.Text = ""
        txtTeam2.Text = ""
        txtSeries.Text = ""
        txtTags.Text = ""
        txtShowHost.Text = ""
        chkVerified.Checked = False
        grd1.SelectedIndex = -1
        grd1.DataBind()

        btnAdd.Text = "Add"
    End Sub

    Private Sub bindGenre()
        ddlSubGenre.DataBind()
    End Sub

    Private Sub grdChannelmaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd1.SelectedIndexChanged
        Try
            Dim intId As Integer = grd1.DataKeys(grd1.SelectedIndex).Value
            lbId.Text = intId
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select * from richmeta where id='" & intId & "'", False)
            If dt.Rows.Count = 1 Then
                ddlType.SelectedValue = dt.Rows(0)("type").ToString.Trim
                If ddlType.SelectedValue = "Show" Then
                    Try
                        ddlBroadcaster.SelectedValue = dt.Rows(0)("broadcasterid").ToString.Trim
                    Catch ex As Exception

                    End Try
                Else
                    ddlBroadcaster.SelectedIndex = 0
                End If

                Try
                    ddlOriginalLanguage.SelectedValue = dt.Rows(0)("originallanguageid").ToString.Trim
                Catch ex As Exception
                End Try
                Try
                    ddlDubbedLanguage.SelectedValue = dt.Rows(0)("dubbedlanguage").ToString.Trim
                Catch ex As Exception
                End Try
                Try
                    ddlParentalGuide.SelectedValue = 0
                Catch ex As Exception
                End Try
                Try
                    ddlGenre.SelectedValue = dt.Rows(0)("genre").ToString.Trim
                    bindGenre()
                    ddlSubGenre.SelectedValue = dt.Rows(0)("subgenre").ToString.Trim
                Catch ex As Exception
                End Try

                Try
                    ddlSportsType.SelectedValue = dt.Rows(0)("sporttype").ToString.Trim
                Catch ex As Exception
                End Try


                txtName.Text = dt.Rows(0)("name").ToString.Trim
                txtDisplayName.Text = dt.Rows(0)("displayname").ToString.Trim
                txtRating.Text = dt.Rows(0)("rating").ToString.Trim
                txtSeasonNo.Text = dt.Rows(0)("seasonno").ToString.Trim
                txtSeasonName.Text = dt.Rows(0)("seasonname").ToString.Trim
                txtStarCast.Text = dt.Rows(0)("starcast").ToString.Trim
                txtDirector.Text = dt.Rows(0)("director").ToString.Trim
                txtProducer.Text = dt.Rows(0)("producer").ToString.Trim
                txtWriter.Text = dt.Rows(0)("writer").ToString.Trim
                txtSynopsis.Text = dt.Rows(0)("synopsis").ToString.Trim
                txtLongSynopsis.Text = dt.Rows(0)("longsynopsis").ToString.Trim
                txtTrivia.Text = dt.Rows(0)("trivia").ToString.Trim
                txtAwards.Text = dt.Rows(0)("awards").ToString.Trim
                txtReleaseYear.Text = dt.Rows(0)("releaseyear").ToString.Trim
                txtCountry.Text = dt.Rows(0)("country").ToString.Trim
                txtTeam1.Text = dt.Rows(0)("team1").ToString.Trim
                txtTeam2.Text = dt.Rows(0)("team2").ToString.Trim
                txtSeries.Text = dt.Rows(0)("series").ToString.Trim
                txtTags.Text = dt.Rows(0)("tags").ToString.Trim
                txtShowHost.Text = dt.Rows(0)("showhost").ToString.Trim
                Try
                    chkVerified.Checked = dt.Rows(0)("verified").ToString
                Catch ex As Exception
                End Try
                Try
                    chkActive.Checked = dt.Rows(0)("active").ToString
                Catch ex As Exception
                End Try
                btnAdd.Text = "Update"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grd1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grd1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim item As String = e.Row.Cells(5).Text
            For Each button As Button In e.Row.Cells(0).Controls.OfType(Of Button)()
                If button.CommandName = "Delete" Then
                    button.Attributes("onclick") = "if(!confirm('Do you want to delete """ + item + """ ?')){ return false; };"
                End If
            Next
        End If
    End Sub

End Class