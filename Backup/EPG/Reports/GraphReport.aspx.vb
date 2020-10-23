Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Data.OleDb
Imports System.Web.UI.DataVisualization.Charting

Public Class GraphReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy")
            txtEndDate.Text = DateTime.Now.AddDays(0).ToString("MM/dd/yyyy")
            'bindChart()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindChart()
    End Sub
    Private Sub bindChart()
        Dim sql As String
        If ddlChannel.SelectedValue = "---All Channels---" Then
            sql = "select top 10 channelid,COUNT(*) errors  from error_details_BIGTV where convert(varchar,errordatetime,112) between '" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & "' and '" & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & "' group by channelid order by 2 desc,1"
        Else
            sql = "select convert(varchar,errordatetime,106) ErrorDate ,COUNT(*) errors  from error_details_BIGTV where channelid='" & ddlChannel.SelectedValue & "' and  convert(varchar,errordatetime,112) between '" & Convert.ToDateTime(txtStartDate.Text).ToString("yyyyMMdd") & "' and '" & Convert.ToDateTime(txtEndDate.Text).ToString("yyyyMMdd") & "' group by convert(varchar,errordatetime,106),convert(varchar,errordatetime,112) order by  convert(varchar,errordatetime,112)"

        End If
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL(sql, False)
        If dt.Rows.Count > 0 Then
            lbError.Visible = False

            Dim x As String() = New String(dt.Rows.Count - 1) {}
            Dim y As Integer() = New Integer(dt.Rows.Count - 1) {}
            For i As Integer = 0 To dt.Rows.Count - 1
                x(i) = dt.Rows(i)(0).ToString()
                y(i) = Convert.ToInt32(dt.Rows(i)(1))
            Next
            Chart1.Series(0).Points.DataBindXY(x, y)
            Chart1.Series(0).ChartType = SeriesChartType.Column
            Chart1.Series(0).IsValueShownAsLabel = True
            Chart1.Series(0).LabelForeColor = Drawing.Color.Red
            Chart1.Series(0).LabelBackColor = Drawing.Color.White
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Interval = 1
            
            Chart1.ChartAreas("ChartArea1").AxisX.MinorGrid.Enabled = True
            Chart1.ChartAreas("ChartArea1").AxisY.MinorGrid.Enabled = False

            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Angle = -45

            Chart1.DataBind()
        Else
            lbError.Visible = True

        End If

    End Sub
End Class