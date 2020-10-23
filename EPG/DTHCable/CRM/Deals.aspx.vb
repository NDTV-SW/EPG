Public Class Deals
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim dtStartDate As DateTime = txtStartDate.Text
        Dim dtEndDate As DateTime = txtEndDate.Text

        Dim intMonths As Integer = DateDiff(DateInterval.Month, dtStartDate, dtEndDate)
        intMonths = intMonths + 1



        Dim obj As New clsExecute
        Dim sql As String
        If btnAdd.Text.ToUpper = "ADD" Then
            sql = "insert into crm_deal(crmid,gst,pan,concernedperson,designation,addressline,pricing,tenure,startdate,enddate,exclusivetaxes"
            sql = sql & ",channelsubscribed,languages,otherfeatures,proglengthandsynopsis,systemname,fileformat,paying,active) values"
            sql = sql & "('" & ddlClient.SelectedValue & "','" & txtGST.Text & "','" & txtPAN.Text & "','" & txtConcerned.Text & "'"
            sql = sql & ",'" & txtDesignation.Text & "','" & txtAddress.Text & "','" & txtPricing.Text & "','" & intMonths & "'"
            sql = sql & ",'" & txtStartDate.Text & "','" & txtEndDate.Text & "','" & chkExcludingTaxes.Checked & "','" & txtChannelsSubscribed.Text & "'"
            sql = sql & ",'" & lstLanguage.SelectedValue & "','" & txtOtherFeatures.Text & "','" & txtProgLengthAndSynopsis.Text & "','" & txtSystemName.Text & "'"
            sql = sql & ",'" & txtFormat.Text & "','" & ddlContractStatus.SelectedValue & "','" & chkActive.Checked & "')"
        Else
            sql = "update crm_deal set crmid='" & ddlClient.SelectedValue & "',gst='" & txtGST.Text & "',pan='" & txtPAN.Text & "'"
            sql = sql & ",concernedperson='" & txtConcerned.Text & "',designation='" & txtDesignation.Text & "'"
            sql = sql & ",addressline='" & txtAddress.Text & "',pricing='" & txtPricing.Text & "',tenure='" & intMonths & "'"
            sql = sql & ",startdate='" & txtStartDate.Text & "',enddate='" & txtEndDate.Text & "',exclusivetaxes='" & chkExcludingTaxes.Checked & "'"
            sql = sql & ",channelsubscribed='" & txtChannelsSubscribed.Text & "',languages='" & lstLanguage.SelectedValue & "'"
            sql = sql & ",otherfeatures='" & txtOtherFeatures.Text & "',proglengthandsynopsis='" & txtProgLengthAndSynopsis.Text & "'"
            sql = sql & ",systemname='" & txtSystemName.Text & "',fileformat='" & txtFormat.Text & "',paying='" & ddlContractStatus.SelectedValue & "'"
            sql = sql & ",active='" & chkActive.Checked & "' where id='" & lbID.Text & "'"
        End If
        obj.executeSQL(sql, False)
        clearAll()

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Private Sub clearAll()
        ddlClient.SelectedIndex = 0
        txtGST.Text = ""
        txtPAN.Text = ""
        txtConcerned.Text = ""
        txtDesignation.Text = ""
        txtAddress.Text = ""
        txtPricing.Text = ""
        'txtTenure.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        chkExcludingTaxes.Checked = True
        txtChannelsSubscribed.Text = ""
        lstLanguage.SelectedIndex = 0
        txtOtherFeatures.Text = ""
        txtProgLengthAndSynopsis.Text = ""
        txtSystemName.Text = ""
        txtFormat.Text = ""
        ddlContractStatus.SelectedIndex = 0
        chkActive.Checked = True
        lbID.Text = ""
        btnAdd.Text = "Add"
        grd.SelectedIndex = -1
        grd.DataBind()
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from crm_deal where id='" & lbID.Text & "'", False)
        ddlClient.SelectedValue = dt.Rows(0)("crmid").ToString
        txtGST.Text = dt.Rows(0)("gst").ToString
        txtPAN.Text = dt.Rows(0)("pan").ToString
        txtConcerned.Text = dt.Rows(0)("concernedperson").ToString
        txtDesignation.Text = dt.Rows(0)("designation").ToString
        txtAddress.Text = dt.Rows(0)("addressline").ToString
        txtPricing.Text = dt.Rows(0)("pricing").ToString
        'txtTenure.Text = dt.Rows(0)("tenure").ToString
        txtStartDate.Text = Convert.ToDateTime(dt.Rows(0)("startdate").ToString).ToString("MM/dd/yyyy")
        txtEndDate.Text = Convert.ToDateTime(dt.Rows(0)("enddate").ToString).ToString("MM/dd/yyyy")
        chkExcludingTaxes.Checked = dt.Rows(0)("exclusivetaxes").ToString
        txtChannelsSubscribed.Text = dt.Rows(0)("channelsubscribed").ToString
        Try
            lstLanguage.SelectedValue = dt.Rows(0)("languages").ToString
        Catch
        End Try
        txtOtherFeatures.Text = dt.Rows(0)("otherfeatures").ToString
        txtProgLengthAndSynopsis.Text = dt.Rows(0)("proglengthandsynopsis").ToString
        txtSystemName.Text = dt.Rows(0)("systemname").ToString
        txtFormat.Text = dt.Rows(0)("fileformat").ToString
        ddlContractStatus.SelectedValue = dt.Rows(0)("paying").ToString
        chkActive.Checked = dt.Rows(0)("active").ToString
        btnAdd.Text = "Update"
    End Sub

    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(8).CssClass = "text-right"

            Dim strStatus As String = e.Row.Cells(9).Text
            If strStatus.ToUpper = "BARTER" Then
                e.Row.Cells(9).CssClass = "alert-warning"
            ElseIf strStatus.ToUpper = "PAYING" Then
                e.Row.Cells(9).CssClass = "alert-success"
            ElseIf strStatus.ToUpper = "NOT PAYING" Then
                e.Row.Cells(9).CssClass = "alert-danger"
            ElseIf strStatus.ToUpper = "PROVISION" Then
                e.Row.Cells(9).CssClass = "alert-info"
            End If

        End If
    End Sub
End Class