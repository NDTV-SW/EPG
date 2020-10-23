Public Class viewrich
    Inherits System.Web.UI.Page
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & messagestr & "');", True)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim intRichMetaId As Integer = Request.QueryString("id")
            ddlGenre.DataBind()
            ddlSubGenre.DataBind()
            bindValues(intRichMetaId)
            bindGrid(intRichMetaId)

        End If
    End Sub

  
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim obj As New clsExecute
            Dim sql As String
            sql = "update richmeta set releaseyear='" & txtYear.Text & "',originallanguageid='" & ddlOrigLangauge.SelectedValue & "',dubbedlanguge='" & ddlDubbedLanguage.SelectedValue & "'"
            sql = sql & ",seasonno='" & txtSeasonNo.Text & "',seasonname='" & txtSeasonName.Text & "',genre='" & ddlGenre.SelectedValue & "',subgenre='" & ddlSubGenre.SelectedValue & "'"
            sql = sql & ",synopsis='" & txtSynopsis.Text.Replace("'", "''") & "',starcast='" & txtStarCast.Text.Replace("'", "''") & "',director='" & txtDirector.Text.Replace("'", "''") & "'"
            sql = sql & ",producer='" & txtProducer.Text.Replace("'", "''") & "',writer='" & txtWriter.Text.Replace("'", "''") & "',country='" & txtCountry.Text & "'"
            sql = sql & ",verified='" & chkVerified.Checked & "',active='" & chkVerified.Checked & "',updatedby='" & User.Identity.Name & "',updatedat=dbo.getlocaldate()"
            sql = sql & " where id='" & lbID.Text & "'"
            obj.executeSQL(sql, False)
            bindValues(lbID.Text)
            myMessageBox("Data updated successfully")
        Catch ex As Exception
            Logger.LogError(Logger.whichPageCalledMe, Logger.whichMethodCalledMe, ex.Message.ToString, User.Identity.Name)
            myMessageBox("ERROR. Please check Error Report")

        End Try
    End Sub

    Private Sub bindValues(ByVal intRichMetaId As Integer)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from richmeta where id='" & intRichMetaId & "'", False)
        If dt.Rows.Count = 1 Then


            lbID.Text = intRichMetaId
            lbType.Text = dt.Rows(0)("type").ToString
            lbName.Text = dt.Rows(0)("name").ToString
            txtSeasonNo.Text = dt.Rows(0)("seasonno").ToString
            txtSeasonName.Text = dt.Rows(0)("seasonname").ToString
            'txtPG.Text = dt.Rows(0)("parentalguide").ToString
            txtSynopsis.Text = dt.Rows(0)("synopsis").ToString
            txtStarCast.Text = dt.Rows(0)("starcast").ToString
            txtDirector.Text = dt.Rows(0)("director").ToString
            txtProducer.Text = dt.Rows(0)("producer").ToString
            txtWriter.Text = dt.Rows(0)("writer").ToString
            'txtTrivia.Text = dt.Rows(0)("trivia").ToString
            'txtAwards.Text = dt.Rows(0)("awards").ToString
            txtYear.Text = dt.Rows(0)("releaseyear").ToString
            txtCountry.Text = dt.Rows(0)("country").ToString
            ddlOrigLangauge.SelectedValue = dt.Rows(0)("originallanguageid").ToString
            ddlDubbedLanguage.SelectedValue = dt.Rows(0)("dubbedlanguge").ToString
            'Dim dt1 As DataTable
            'dt1 = obj.executeSQL("select fullname from mst_language where languageid='" & dt.Rows(0)("originallanguageid").ToString & "'", False)
            'If dt1.Rows.Count > 0 Then
            '    txtDubbedLang.Text = dt1.Rows(0)(0).ToString
            'End If
            'dt1 = obj.executeSQL("select fullname from mst_language where languageid='" & dt.Rows(0)("dubbedlanguge").ToString & "'", False)
            'If dt1.Rows.Count > 0 Then
            '    txtDubbedLang.Text = dt1.Rows(0)(0).ToString
            'End If
            Try
                ddlGenre.SelectedValue = dt.Rows(0)("genre").ToString
                ddlSubGenre.DataBind()
                ddlSubGenre.SelectedValue = dt.Rows(0)("subgenre").ToString
            Catch ex As Exception

            End Try
            chkVerified.Checked = IIf(dt.Rows(0)("verified").ToString = "", False, dt.Rows(0)("verified").ToString)
            'txtGenre1.Text = dt.Rows(0)("genrename1").ToString
            'txtGenre2.Text = dt.Rows(0)("genrename2").ToString

            'dt1 = obj.executeSQL("select genrename from mst_genre where genreid='" & dt.Rows(0)("genre").ToString & "'", False)
            'If dt1.Rows.Count > 0 Then
            '    txtGenre.Text = dt1.Rows(0)(0).ToString
            'End If
            'dt1 = obj.executeSQL("select subgenrename from mst_subgenre where subgenreid='" & dt.Rows(0)("subgenre").ToString & "'", False)
            'If dt1.Rows.Count > 0 Then
            '    txtSubgenre.Text = dt1.Rows(0)(0).ToString
            'End If
            lbError.Visible = False
            divRich.Visible = True
        Else
            lbError.Visible = True
            divRich.Visible = False
        End If
    End Sub

    Private Sub bindGrid(ByVal intRichMetaId As Integer)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select progid,channelid,progname,richmetaid from mst_program where richmetaid='" & intRichMetaId & "'", False)
        grd.DataSource = dt
        grd.DataBind()
    End Sub

    Protected Sub grd_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grd.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim item As String = e.Row.Cells(2).Text
            For Each button As Button In e.Row.Cells(0).Controls.OfType(Of Button)()
                If button.CommandName = "Delete" Then
                    button.Attributes("onclick") = "if(!confirm('Do you want to delete """ + item + """ ?')){ return false; };"
                End If
            Next

        End If
    End Sub

    Protected Sub grd_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd.RowDeleting
        Dim obj As New clsExecute
        Dim lbLabel As Label = TryCast(grd.Rows(e.RowIndex).FindControl("lbId"), Label)
        Dim lbRichMetaId As Label = TryCast(grd.Rows(e.RowIndex).FindControl("lbRichMetaId"), Label)

        obj.executeSQL("update mst_program set richmetaid='' where progid='" & lbLabel.Text & "'", False)
        bindGrid(lbRichMetaId.Text)
    End Sub

    Protected Sub grd_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grd.RowDeleted
        If e.ExceptionHandled = False Then
            e.ExceptionHandled = True
        End If
    End Sub

End Class