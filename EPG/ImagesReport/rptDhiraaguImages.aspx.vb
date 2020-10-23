
Public Class rptDhiraaguImages
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("Images Report", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Dim rowId As Integer
    Dim intProgs As Integer
    Dim intEPGImages As Integer, intEPGImagesPers As Integer, intOverAllImages As Integer, intOverAllImagesPers As Integer
    Dim intPEPGImages As Integer, intPEPGImagesPers As Integer, intPOverAllImages As Integer, intPOverAllImagesPers As Integer

    Protected Sub grdImagesReport_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdImagesReport.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            rowId = 0
            intProgs = 0
            intEPGImages = 0
            intEPGImagesPers = 0
            intOverAllImages = 0
            intOverAllImagesPers = 0
            intPEPGImages = 0
            intPEPGImagesPers = 0
            intPOverAllImages = 0
            intPOverAllImagesPers = 0

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            rowId = rowId + 1
            Dim lbRowId As Label = TryCast(e.Row.FindControl("lbRowId"), Label)
            lbRowId.Text = rowId

            intProgs = intProgs + e.Row.Cells(3).Text.ToString

            intEPGImages = intEPGImages + e.Row.Cells(4).Text.ToString
            intEPGImagesPers = intEPGImagesPers + e.Row.Cells(5).Text.ToString.Replace("%", "")
            intOverAllImages = intOverAllImages + e.Row.Cells(6).Text.ToString
            intOverAllImagesPers = intOverAllImagesPers + e.Row.Cells(7).Text.ToString.Replace("%", "")

            intPEPGImages = intPEPGImages + e.Row.Cells(8).Text.ToString
            intPEPGImagesPers = intPEPGImagesPers + e.Row.Cells(9).Text.ToString.Replace("%", "")
            intPOverAllImages = intPOverAllImages + e.Row.Cells(10).Text.ToString
            intPOverAllImagesPers = intPOverAllImagesPers + e.Row.Cells(11).Text.ToString.Replace("%", "")

        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTAL :"
            e.Row.Cells(3).Text = intProgs
            e.Row.Cells(4).Text = intEPGImages
            e.Row.Cells(5).Text = "Avg : " & Math.Round((intEPGImagesPers / rowId), 1) & "%"
            e.Row.Cells(6).Text = intOverAllImages
            e.Row.Cells(7).Text = "Avg : " & Math.Round((intOverAllImagesPers / rowId), 1) & "%"
            e.Row.Cells(8).Text = intPEPGImages
            e.Row.Cells(9).Text = "Avg : " & Math.Round((intPEPGImagesPers / rowId), 1) & "%"
            e.Row.Cells(10).Text = intPOverAllImages
            e.Row.Cells(11).Text = "Avg : " & Math.Round((intPOverAllImagesPers / rowId), 1) & "%"
        End If
    End Sub
End Class