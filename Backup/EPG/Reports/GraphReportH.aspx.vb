Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Data.OleDb
Imports System.Web.UI.DataVisualization.Charting
Imports DotNet.Highcharts
Imports System.Linq
Imports DotNet.Highcharts.Options
Imports DotNet.Highcharts.Attributes
Imports DotNet.Highcharts.Helpers
Imports DotNet.Highcharts.Enums

Public Class GraphReportH
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

        Dim a As String() = New String(dt.Rows.Count - 1) {}
        Dim seriesY As Options.Series() = New Options.Series(dt.Rows.Count - 1) {}

        For i As Integer = 0 To dt.Rows.Count - 1
            If ddlChannel.SelectedValue = "---All Channels---" Then
                seriesY(i) = New Options.Series() With { _
                    .Name = dt.Rows(i)("channelid").ToString(), _
                    .Data = New Data(New Object() {dt.Rows(i)("errors")}), _
                    .Type = Enums.ChartTypes.Column
                }
            Else
                seriesY(i) = New Options.Series() With { _
                    .Name = dt.Rows(i)("ErrorDate").ToString(), _
                    .Data = New Data(New Object() {dt.Rows(i)("errors")}), _
                    .Type = Enums.ChartTypes.Column
                }
            End If
        Next

        Dim cTitle As Options.Title = New Options.Title()
        Dim cSubTitle As Options.Subtitle = New Options.Subtitle()
        cTitle.Text = "Top 10 Erroneous channels"
        cSubTitle.Text = Convert.ToDateTime(txtStartDate.Text).ToString("dd-MMM-yyyy") & " to " & Convert.ToDateTime(txtEndDate.Text).ToString("dd-MMM-yyyy")
        Dim chart1 As Highcharts = New Highcharts("chart1")
        chart1.SetTitle(cTitle)
        chart1.InitChart(New Options.Chart With {.DefaultSeriesType = Enums.ChartTypes.Line})
        
        chart1.SetSubtitle(cSubTitle)
        chart1.SetSeries(seriesY)

        ltrChart.Text = chart1.ToHtmlString
    End Sub
End Class