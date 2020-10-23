Public Class DVBSubGenreMapping
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("DVBSubGenreMapping", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Protected Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("insert into dvb_subgenre_mapping(dvbgenreid,dvbsubgenreid,ndtvsubgenreid) values('" & ddlDVBGenre.SelectedValue & "','" & ddlDVBSubGenre.SelectedValue & "','" & ddlNDTVSubGenre.SelectedValue & "')", False)

        clearall()

    End Sub


    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        Dim lbID As Label = TryCast(grd.Rows(grd.SelectedIndex).FindControl("lbID"), Label)
        Dim obj As New clsExecute
        obj.executeSQL("delete from dvb_subgenre_mapping where rowid='" & lbID.Text & "'", False)
        clearall()


    End Sub

    Private Sub clearall()
        ddlNDTVGenre.DataBind()
        ddlNDTVSubGenre.DataBind()

        grd.SelectedIndex = -1
        grd.DataBind()

    End Sub
End Class