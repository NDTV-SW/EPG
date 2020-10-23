Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
        End If
    End Sub

    Protected Sub grdEPGMissing8Days_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEPGMissing8Days.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbnoOfDays As Label = TryCast(e.Row.FindControl("lbnoOfDays"), Label)
            If Convert.ToInt16(lbnoOfDays.Text) < 3 Then
                e.Row.BackColor = Drawing.Color.IndianRed
            End If
        End If
    End Sub
    Protected Sub btnRefreshChannelList_Click(sender As Object, e As EventArgs) Handles btnRefreshChannelList.Click
        Session("ChannelList") = Nothing
        Session("XMLChannelList") = Nothing

    End Sub

    Protected Sub btnRefreshAPI_Click(sender As Object, e As EventArgs) Handles btnRefreshAPI.Click
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select channelid from mst_channel where onair=1", False)
        For i As Integer = 0 To dt.Rows.Count - 1
            HttpContext.Current.Cache.Remove(dt.Rows(i)("channelid").ToString)
            HttpContext.Current.Cache.Remove(dt.Rows(i)("channelid").ToString & "_rich")
            HttpContext.Current.Cache.Remove(dt.Rows(i)("channelid").ToString & "_regional")
        Next
    End Sub

    Protected Sub btn8Day_Click(sender As Object, e As EventArgs) Handles btn8Day.Click
        sqlDS_EPGMissing8Days.SelectCommand = "rpt_missingepg_8days"
        sqlDS_EPGMissing8Days.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        grdEPGMissing8Days.DataSourceID = "sqlDS_EPGMissing8Days"
        grdEPGMissing8Days.DataBind()

    End Sub

    Protected Sub btn8and10_Click(sender As Object, e As EventArgs) Handles btn8and10.Click
        sqlDS_EPGMissing10Days.SelectCommand = "select ChannelId,OperatorName,DateMissing from vw_php_job_mail_8thday_missingdata"
        sqlDS_EPGMissing8Days.SelectCommandType = SqlDataSourceCommandType.Text
        grd10Days.DataSourceID = "sqlDS_EPGMissing10Days"
        grd10Days.DataBind()
    End Sub

    Protected Sub btnImagesRequired_Click(sender As Object, e As EventArgs) Handles btnImagesRequired.Click
        sqlDS_ImagesRequired.SelectCommand = "select * from v_UrgentImagesRequired order by channelid,ProgName"
        sqlDS_ImagesRequired.SelectCommandType = SqlDataSourceCommandType.Text
        grdImagesRequired.DataSourceID = "sqlDS_ImagesRequired"
        grdImagesRequired.DataBind()
    End Sub
End Class