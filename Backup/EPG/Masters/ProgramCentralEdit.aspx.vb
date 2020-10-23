Imports System
Imports System.Data.SqlClient
Imports ExcelLibrary
Public Class ProgramCentralEdit
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim mu As MembershipUser = Membership.GetUser(User.Identity.Name)
            Membership.UpdateUser(mu)
            If mu.Comment = "Need Change Password1" Then
                'Response.Redirect("~/Account/ChangePassword.aspx")
                mu.Comment = "0"
            End If
            Dim pwDateExpire As Integer
            pwDateExpire = DateDiff(DateInterval.Day, mu.LastPasswordChangedDate, Date.Now)
            If pwDateExpire > 30 Then
                'Response.Redirect("~/Account/ChangePassword.aspx")
            End If

            If Page.IsPostBack = False Then
                ddlChannelName.DataBind()
                lbChannel.Text = ddlChannelName.SelectedItem.Text
                getSynopsisList()
                Try
                    Dim channelOnair As String = Request.QueryString("onair").ToString
                    Dim index As String = Request.QueryString("index").ToString
                    ddlChannelOnair.SelectedValue = channelOnair
                    ddlChannelName.DataBind()

                    'Dim channelid As String = Request.QueryString("channelid").ToString
                    'channelid = channelid.Replace("'", "")
                    'ddlChannelName.SelectedValue = channelid
                    ddlChannelName.SelectedIndex = Convert.ToInt32(index)
                    grdProgramCentral.DataBind()
                Catch ex As Exception
                    ' myErrorBox("Error:" & ex.Message.ToString)
                    Logger.LogError("ProgramCentralEdit", "Page_Load", ex.Message.ToString, User.Identity.Name)

                End Try
            End If
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub ddlChannelName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannelName.SelectedIndexChanged
        Try
            grdProgramCentral.PageIndex = 0
            grdProgramCentral.DataBind()
            lbChannel.Text = ddlChannelName.SelectedItem.Text
            getSynopsisList()
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "ddlChannelName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub getSynopsisList()
        lbSynopsisNeeded.Text = ""
        If MyConnection.State = ConnectionState.Closed Then
            MyConnection.Open()
        End If
        Dim cmd As New SqlCommand("select b.FullName from mst_ChannelRegionalName a join mst_language b on a.LanguageId =b.LanguageID where  SynopsisNeeded =1 and ChannelId='" & lbChannel.Text & "'", MyConnection)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader

        If dr.HasRows Then

            While dr.Read()
                lbSynopsisNeeded.Text = lbSynopsisNeeded.Text & dr("FullName").ToString & ", "
            End While
            lbSynopsisNeeded.Text = lbSynopsisNeeded.Text.Substring(0, lbSynopsisNeeded.Text.Length - 2)
        End If
        dr.Close()
        MyConnection.Dispose()
    End Sub

    Private Sub grdProgramCentral_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProgramCentral.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header
                    e.Row.CssClass = "locked"
                Case DataControlRowType.DataRow
                    Dim lbProgName As Label = DirectCast(e.Row.FindControl("lbProgName"), Label)
                    Dim lbEngProg As Label = DirectCast(e.Row.FindControl("lbEngProg"), Label)
                    Dim lbEngSyn As Label = DirectCast(e.Row.FindControl("lbEngSyn"), Label)
                    Dim lbHinProg As Label = DirectCast(e.Row.FindControl("lbHinProg"), Label)
                    Dim lbHinSyn As Label = DirectCast(e.Row.FindControl("lbHinSyn"), Label)
                    Dim lbTamProg As Label = DirectCast(e.Row.FindControl("lbTamProg"), Label)
                    Dim lbTamSyn As Label = DirectCast(e.Row.FindControl("lbTamSyn"), Label)
                    Dim lbMarProg As Label = DirectCast(e.Row.FindControl("lbMarProg"), Label)
                    Dim lbMarSyn As Label = DirectCast(e.Row.FindControl("lbMarSyn"), Label)
                    Dim lbTelProg As Label = DirectCast(e.Row.FindControl("lbTelProg"), Label)
                    Dim lbTelSyn As Label = DirectCast(e.Row.FindControl("lbTelSyn"), Label)
                    Dim lbColorCode As Label = DirectCast(e.Row.FindControl("lbColorCode"), Label)
                    Dim lbEpisodeNo As Label = DirectCast(e.Row.FindControl("lbEpisodeNo"), Label)

                    Dim lbProgId As Label = DirectCast(e.Row.FindControl("lbProgId"), Label)
                    Dim lbEngLangId As Label = DirectCast(e.Row.FindControl("lbEngLang"), Label)
                    Dim lbHinLangId As Label = DirectCast(e.Row.FindControl("lbHinLang"), Label)
                    Dim lbTamLangId As Label = DirectCast(e.Row.FindControl("lbTamLang"), Label)
                    Dim lbMarLangId As Label = DirectCast(e.Row.FindControl("lbMarLang"), Label)
                    Dim lbTelLangId As Label = DirectCast(e.Row.FindControl("lbTelLang"), Label)

                    Dim hyEngEdit As HyperLink = DirectCast(e.Row.FindControl("hyEngEdit"), HyperLink)
                    Dim hyHinEdit As HyperLink = DirectCast(e.Row.FindControl("hyHinEdit"), HyperLink)
                    Dim hyTamEdit As HyperLink = DirectCast(e.Row.FindControl("hyTamEdit"), HyperLink)
                    Dim hyMarEdit As HyperLink = DirectCast(e.Row.FindControl("hyMarEdit"), HyperLink)
                    Dim hyTelEdit As HyperLink = DirectCast(e.Row.FindControl("hyTelEdit"), HyperLink)

                    'hyEngEdit.NavigateUrl = "javascript:openWin('" & ddlChannelName.SelectedValue.Trim & "','1','1')"
                    hyEngEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbEngLangId.Text & "','MainContent_grdProgramCentral_hyEngEdit_" & e.Row.RowIndex & "','" & ddlChannelName.SelectedIndex & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyHinEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbHinLangId.Text & "','MainContent_grdProgramCentral_hyHinEdit_" & e.Row.RowIndex & "','" & ddlChannelName.SelectedIndex & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyTamEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbTamLangId.Text & "','MainContent_grdProgramCentral_hyTamEdit_" & e.Row.RowIndex & "','" & ddlChannelName.SelectedIndex & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyMarEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbMarLangId.Text & "','MainContent_grdProgramCentral_hyMarEdit_" & e.Row.RowIndex & "','" & ddlChannelName.SelectedIndex & "','" & lbEpisodeNo.Text.Trim & "')"
                    hyTelEdit.NavigateUrl = "javascript:openWin('" & ddlChannelOnair.SelectedValue & "','" & lbProgId.Text.Trim & "','" & lbTelLangId.Text & "','MainContent_grdProgramCentral_hyTelEdit_" & e.Row.RowIndex & "','" & ddlChannelName.SelectedIndex & "','" & lbEpisodeNo.Text.Trim & "')"

                    If (User.IsInRole("ADMIN") Or User.IsInRole("USER") Or User.IsInRole("SUPERUSER")) Then
                        hyEngEdit.Enabled = True
                        hyHinEdit.Enabled = True
                        hyTamEdit.Enabled = True
                        hyMarEdit.Enabled = True
                        hyTelEdit.Enabled = True
                    ElseIf (User.IsInRole("HINDI")) Then
                        hyEngEdit.Enabled = False
                        hyHinEdit.Enabled = True
                        hyTamEdit.Enabled = False
                        hyMarEdit.Enabled = False
                        hyTelEdit.Enabled = False
                    ElseIf (User.IsInRole("ENGLISH")) Then
                        hyEngEdit.Enabled = True
                        hyHinEdit.Enabled = False
                        hyTamEdit.Enabled = False
                        hyMarEdit.Enabled = False
                        hyTelEdit.Enabled = False
                    ElseIf (User.IsInRole("MARATHI")) Then
                        hyEngEdit.Enabled = False
                        hyHinEdit.Enabled = False
                        hyTamEdit.Enabled = False
                        hyMarEdit.Enabled = True
                        hyTelEdit.Enabled = False
                    ElseIf (User.IsInRole("TELUGU")) Then
                        hyEngEdit.Enabled = False
                        hyHinEdit.Enabled = True
                        hyTamEdit.Enabled = False
                        hyMarEdit.Enabled = False
                        hyTelEdit.Enabled = True
                    ElseIf (User.IsInRole("TAMIL")) Then
                        hyEngEdit.Enabled = False
                        hyHinEdit.Enabled = False
                        hyTamEdit.Enabled = True
                        hyMarEdit.Enabled = False
                        hyTelEdit.Enabled = False
                    End If

                    If lbEngSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(2).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(2).Font.Bold = True
                    End If
                    If lbHinSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(5).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(5).Font.Bold = True
                    End If

                    If lbTamSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(8).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(8).Font.Bold = True
                    End If

                    If lbMarSyn.Text.ToString.Length <= 10 Then
                        e.Row.Cells(11).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(11).Font.Bold = True
                    End If

                    If lbTelProg.Text.ToString.Length <= 10 Then
                        e.Row.Cells(14).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(14).Font.Bold = True
                    End If

                    If lbProgName.Text.ToString.Length <= 5 Then
                        e.Row.Cells(0).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(0).Font.Bold = True
                    End If
                    If lbEngProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(1).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(1).Font.Bold = True
                    End If
                    If lbHinProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(4).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(4).Font.Bold = True
                    End If
                    If lbTamProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(7).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(7).Font.Bold = True
                    End If
                    If lbMarProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(10).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(10).Font.Bold = True
                    End If
                    If lbTelProg.Text.ToString.Length <= 5 Then
                        e.Row.Cells(13).BackColor = Drawing.Color.MistyRose
                        e.Row.Cells(13).Font.Bold = True
                    End If

                    lbEngSyn.Text = lbEngSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbHinSyn.Text = lbHinSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbTamSyn.Text = lbTamSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbMarSyn.Text = lbMarSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    lbTelSyn.Text = lbTelSyn.Text.ToString().Replace("(", "<span style='background-color:Yellow'>(").Replace(")", ")</span>")
                    ' 0 for programmes not in mst_epg, 1 for programmes in mst_epg.
                    ' 2 for programmes airing in next 7 days.
                    If lbColorCode.Text.Trim = 1 Then
                        e.Row.BackColor = Drawing.Color.LightBlue
                    ElseIf lbColorCode.Text.Trim = 2 Then
                        e.Row.BackColor = Drawing.Color.LightGreen
                    End If
                    '    Case DataControlRowType.Footer

            End Select
        Catch ex As Exception
            Logger.LogError("Program Central Edit", "grdProgramCentral_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Private Sub exportExcel(ByVal channelid As String, ByVal languagename As String, ByVal languageid As String)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from fn_regional_prog_exportformat('" & channelid & "','" & languageid & "')", False)

        dt.Columns("Channelid").ColumnName = "Channel"
        dt.Columns("Program Name").ColumnName = "English Programme Name"
        dt.Columns("Synopsis").ColumnName = "English Synopsis"
        dt.Columns("Regional ProgName").ColumnName = languagename & " Programme"
        dt.Columns("Regional Synopsis").ColumnName = languagename & "_Synopsis"

        'dt.Columns.Remove("")

        Dim workbook As SpreadSheet.Workbook = New SpreadSheet.Workbook()
        Dim worksheet As SpreadSheet.Worksheet
        Dim iRow As Integer = 0
        Dim iCol As Integer = 0
        Dim sTemp As String = String.Empty
        Dim dTemp As Double = 0
        Dim iTemp As Integer = 0

        Dim count As Integer = 0
        Dim iTotalRows As Integer = 0
        Dim iSheetCount As Integer = 0

        worksheet = New SpreadSheet.Worksheet("Sheet " & iSheetCount.ToString())
        iSheetCount = iSheetCount + 1

        'Write Table Header
        For Each dc As DataColumn In dt.Columns
            worksheet.Cells(iRow, iCol) = New SpreadSheet.Cell(dc.ColumnName)
            iCol = iCol + 1
        Next

        'Write Table Data
        iRow = 2
        For Each dr As DataRow In dt.Rows
            iCol = 0
            For Each dc As DataColumn In dt.Columns
                sTemp = dr(dc.ColumnName).ToString()
                worksheet.Cells(iRow - 1, iCol) = New SpreadSheet.Cell(sTemp)

                iCol = iCol + 1
            Next
            iRow = iRow + 1
        Next


        worksheet.Cells.ColumnWidth(0) = 5000
        worksheet.Cells.ColumnWidth(1) = 7000
        worksheet.Cells.ColumnWidth(2) = 10000
        worksheet.Cells.ColumnWidth(3) = 7000
        worksheet.Cells.ColumnWidth(4) = 10000

        worksheet.SheetType = SpreadSheet.SheetType.Worksheet
        workbook.Worksheets.Add(worksheet)
        iTotalRows = iTotalRows + iRow

        'Bug on Excel Library, min file size must be 7 Kb
        'thus we need to add empty row for safety
        If iTotalRows < 100 Then
            worksheet = New SpreadSheet.Worksheet("Sheet X")
            count = 1
            Do While count < 100
                worksheet.Cells(count, 0) = New SpreadSheet.Cell(" ")
                count = count + 1
            Loop
            workbook.Worksheets.Add(worksheet)
        End If

        Dim strPath As String = Server.MapPath("~/Excel/")
        Dim strFileName As String = channelid & "_ProgrammeExport_" & languagename & "_" & DateTime.Now.ToString("ddMMMyyyy") & ".xls"
        'Return strPath & strFileName

        Dim newFileName As String = strPath & strFileName
        If System.IO.File.Exists(newFileName) Then
            System.IO.File.Delete(newFileName)
        End If
        workbook.Save(newFileName)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & strFileName)
        Response.WriteFile(newFileName)
        Response.End()
    End Sub

    
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        exportExcel(ddlChannelName.SelectedValue, ddlLanguage.SelectedItem.Text, ddlLanguage.SelectedValue)
    End Sub
End Class