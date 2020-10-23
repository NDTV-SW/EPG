Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Imports AjaxControlToolkit

Public Class UpdatePPV1
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
            If Page.IsPostBack = False Then
                btnUpdate.Enabled = True
                ddlChannelID.DataBind()
                bindGrid()
                Dim obj As New Logger
                lbEPGExists.Text = obj.GetEpgDates(ddlChannelID.SelectedValue)
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
            End If
        Catch ex As Exception
            Logger.LogError("UpdatePPV", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub txtStartDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtStartDate.TextChanged
        Try
            If Not txtStartDate.Text.Trim = "" Then
                Dim strStartDate As String, strEndDate As String
                strStartDate = Convert.ToDateTime(txtStartDate.Text).ToString("dd-MMM-yyyy")
                strEndDate = Convert.ToDateTime(txtStartDate.Text).AddDays(Convert.ToInt16(txtDays.Text) - 1).ToString("dd-MMM-yyyy")
                lbEndDate.Visible = True
                lbEndDate.Text = "XML will be generated from " & strStartDate & " to " & strEndDate & "."
            Else
                lbEndDate.Visible = False
            End If
        Catch ex As Exception
            Logger.LogError("Update PPV", "txtStartDate_TextChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Protected Sub txtDays_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtDays.TextChanged
        Try

            If Not txtStartDate.Text.Trim = "" Then
                Dim strStartDate As String, strEndDate As String
                strStartDate = Convert.ToDateTime(txtStartDate.Text).ToString("dd-MMM-yyyy")
                strEndDate = Convert.ToDateTime(txtStartDate.Text).AddDays(Convert.ToInt16(txtDays.Text) - 1).ToString("dd-MMM-yyyy")
                lbEndDate.Visible = True
                lbEndDate.Text = "XML will be generated from " & strStartDate & " to " & strEndDate & "."
            Else
                lbEndDate.Visible = False
            End If

        Catch ex As Exception
            Logger.LogError("Update PPV", "txtDays_TextChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    'Protected Sub ddlDays_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlDays.SelectedIndexChanged
    '    Try
    '        If Not txtStartDate.Text.Trim = "" Then
    '            Dim strStartDate As String, strEndDate As String
    '            strStartDate = Convert.ToDateTime(txtStartDate.Text).ToString("dd-MMM-yyyy")
    '            strEndDate = Convert.ToDateTime(txtStartDate.Text).AddDays(ddlDays.SelectedValue).ToString("dd-MMM-yyyy")
    '            lbEndDate.Visible = True
    '            lbEndDate.Text = "XML will be generated from " & strStartDate & " to " & strEndDate & "."
    '        Else
    '            lbEndDate.Visible = False
    '        End If
    '    Catch ex As Exception
    '        Logger.LogError("Update PPV", "ddlDays_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
    '    End Try
    'End Sub

    Private Sub bindGrid()
        'Dim adpBindPPV As New SqlDataAdapter("select * ,convert(varchar,fillerduration,108) fillerduration1 ,convert(varchar,f2duration,108) f2duration1 ,convert(varchar,movieduration,108) movieduration1,convert(varchar,totalduration,108) totalduration1 from mst_channel_ppv order by channelID asc", MyConnection)
        Dim adpBindPPV As New SqlDataAdapter("select * ,convert(varchar,fillerduration,108) fillerduration1 ,convert(varchar,f2duration,108) f2duration1 ,convert(varchar,movieduration,108) movieduration1,convert(varchar,totalduration,108) totalduration1 from mst_channel_ppv where channelid='" & ddlChannelID.SelectedValue & "' order by channelID asc", MyConnection)
        adpBindPPV.SelectCommand.CommandType = CommandType.Text
        Dim dsBindPPV As New DataSet
        adpBindPPV.Fill(dsBindPPV, "ppvBind")
        grdUpdatePPV.DataSource = dsBindPPV
        grdUpdatePPV.DataBind()
        'MyConnection.Dispose()
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click


        Try
            Dim strFillerDuration As String, strF2Duration As String, strMovieDuration As String, strDisplayDuration As String
            strFillerDuration = txtFillerDuration.Text
            strF2Duration = txtF2Duration.Text
            strMovieDuration = txtMovieDuration.Text
            strDisplayDuration = txtDisplayDuration.Text
            ' '' ''  Dim adpUpdatePPV As New SqlDataAdapter("sp_mst_channel_ppv", MyConnection)
            Dim adpUpdatePPV As New SqlDataAdapter("sp_mst_channel_ppv", MyConnection)
            adpUpdatePPV.SelectCommand.CommandType = CommandType.StoredProcedure
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@ChannelId", SqlDbType.VarChar)).Value = ddlChannelID.Text
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@ShowTicket", SqlDbType.Int)).Value = txtShowTicket.Text
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@ShowPass", SqlDbType.Int)).Value = txtShowPass.Text
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@ChannelPass", SqlDbType.Int)).Value = txtChannelPass.Text
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@WeekPass", SqlDbType.Int)).Value = txtWeekPass.Text
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@MonthPass", SqlDbType.Int)).Value = txtMonthPass.Text
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@FillerDuration", SqlDbType.VarChar)).Value = strFillerDuration
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@Filler2Duration", SqlDbType.VarChar)).Value = strF2Duration
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@MovieDuration", SqlDbType.VarChar)).Value = strMovieDuration
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@totalduration", SqlDbType.VarChar)).Value = strDisplayDuration
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@MovieLang", SqlDbType.VarChar)).Value = ddlLanguage.SelectedItem.Text
            adpUpdatePPV.SelectCommand.Parameters.Add(New SqlParameter("@MovieName", SqlDbType.VarChar)).Value = ddlMovieName.SelectedItem.Text

            Dim dsUpdatePPV As New DataSet
            adpUpdatePPV.Fill(dsUpdatePPV, "UpdatePPV")
            'MyConnection.Dispose()
            'grdUpdatePPV.DataBind()
            clearData()
            btnUpdate.Enabled = True
            grdUpdatePPV.SelectedIndex = -1
            bindGrid()
            grdMissingChannels.DataBind()

        Catch ex As Exception
            Logger.LogError("UpdatePPV", "btnUpdate_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            clearData()
        Catch ex As Exception
            Logger.LogError("UpdatePPV", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub



    Private Sub clearData()
        txtShowTicket.Text = ""
        txtShowPass.Text = ""
        txtChannelPass.Text = ""
        txtWeekPass.Text = ""
        txtMonthPass.Text = ""

        txtMovieDuration.Text = ""
        txtFillerDuration.Text = ""
        txtF2Duration.Text = ""

    End Sub
    Private Sub grdAddPPVData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbRowId As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbRowid"), Label)
            Dim lbChannelId As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbChannelID"), Label)

        Catch ex As Exception
            'myErrorBox("You cannot update this record!")
            Logger.LogError("UpdatePPV", "grdUpdatePPV_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdUpdatePPV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUpdatePPV.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbRowid"), Label)
            Dim lbChannelId As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbChannelID"), Label)
            Dim lbShowPass As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbShowPass"), Label)
            Dim lbShowTicket As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbShowTicket"), Label)
            Dim lbChannelPass As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbChannelPass"), Label)
            Dim lbWeekPass As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbWeekPass"), Label)
            Dim lbMonthPass As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbMonthPass"), Label)
            Dim lbFillerDuration As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbFillerDuration"), Label)
            Dim lbF2Duration As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbF2Duration"), Label)

            Dim lbMovieDuration As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbMovieDuration"), Label)
            Dim lbDisplayDuration As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbDisplayDuration"), Label)
            Dim lbMovieLang As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbMovieLang"), Label)
            Dim lbMovieName As Label = DirectCast(grdUpdatePPV.SelectedRow.FindControl("lbMovieName"), Label)

            txtHiddenId.Text = lbChannelId.Text
            ddlChannelID.Text = lbChannelId.Text.Trim
            ddlMovieName.DataBind()
            'txtChannelNo.Text = lbChannelId.Text
            txtShowTicket.Text = lbShowTicket.Text
            txtShowPass.Text = lbShowPass.Text
            txtChannelPass.Text = lbChannelPass.Text
            txtWeekPass.Text = lbWeekPass.Text
            txtMonthPass.Text = lbMonthPass.Text
            ddlMovieName.Text = lbMovieName.Text


            Try
                Dim strFillerDuration As String, strF2Duration As String
                Try
                    strFillerDuration = Convert.ToDateTime(lbFillerDuration.Text).ToString("HH:mm:ss")
                Catch ex As Exception

                End Try
                Try
                    strF2Duration = Convert.ToDateTime(lbF2Duration.Text).ToString("HH:mm:ss")
                Catch ex As Exception

                End Try

                Dim strMovieDuration As String = Convert.ToDateTime(lbMovieDuration.Text).ToString("HH:mm:ss")
                Dim strDisplayDuration As String = Convert.ToDateTime(lbDisplayDuration.Text).ToString("HH:mm:ss")
                txtFillerDuration.Text = strFillerDuration
                txtF2Duration.Text = strF2Duration
                txtMovieDuration.Text = strMovieDuration
                txtDisplayDuration.Text = strDisplayDuration
            Catch

            End Try

            ddlLanguage.SelectedValue = lbMovieLang.Text
            btnUpdate.Enabled = True
        Catch ex As Exception
            'myErrorBox("You cannot update this record!")
            Logger.LogError("UpdatePPV", "grdUpdatePPV_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Private Sub grdUpdatePPV_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdUpdatePPV.RowDeleting
        Try
            Dim lbRowID As Label = DirectCast(grdUpdatePPV.Rows(e.RowIndex).FindControl("lbRowid"), Label)

            Dim obj As New clsExecute
            obj.executeSQL("DELETE FROM MST_CHANNEL_PPV WHERE ROWID='" & lbRowID.Text & "'", False)

            bindGrid()
        Catch ex As Exception
            Logger.LogError("Channel Master", "grdChannelmaster_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdUpdatePPV_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUpdatePPV.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim lbFillerD As Label = DirectCast(e.Row.FindControl("lbFillerD"), Label)
                'Dim lbMovieD As Label = DirectCast(e.Row.FindControl("lbMovieD"), Label)
                'lbFillerD.Text = Convert.ToDateTime(lbFillerD.Text).ToString("HH:mm:ss")
                'lbMovieD.Text = Convert.ToDateTime(lbMovieD.Text).ToString("HH:mm:ss")
            End If
        Catch ex As Exception
            Logger.LogError("UploadRegionalNames2", "grdExcelData_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    Protected Sub ddlChannelName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlChannelID.SelectedIndexChanged
        Try
            btnGenerateXML.Enabled = True
            hyViewXml.Visible = False
            Dim obj As New Logger
            lbEPGExists.Visible = True
            lbEPGExists.Text = obj.GetEpgDates(ddlChannelID.SelectedValue)
            bindGrid()
        Catch ex As Exception
            Logger.LogError("Update PPV", "ddlChannelName_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Dim fileError As Integer
    Dim flag As Integer
    Private Sub CreateXml()
        Dim ChannelId As String = ddlChannelID.SelectedValue.ToString
        Dim StartDate As Date = txtStartDate.Text.Trim
        Dim EndDate As Date = Convert.ToDateTime(txtStartDate.Text.Trim).AddDays(Convert.ToInt16(txtDays.Text) - 1)
        Dim path As String = ""
        Dim formattedString As String
        Dim XMLFile As XDocument
        Dim strSql As New StringBuilder

        If MyConnection.State = ConnectionState.Closed Then
            MyConnection.Open()
        End If

        strSql.Append("select dbo.fn_xml_main_ppv('" & ChannelId & "','" & StartDate.ToString("MM/dd/yyyy") & "','" & EndDate.ToString("MM/dd/yyyy") & "')")
        Dim cmd As New SqlCommand(strSql.ToString, MyConnection)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader()
        dr.Read()
        If (dr(0).ToString = "ERROR") Then
            lbGenerateXML.Visible = True
            lbGenerateXML.Text = "The XML for specified dates cannot be generated as some or all dates do not have new EPG data. You must upload FPC and 'BUILD EPG' for these dates!"
            Exit Sub
        End If

        formattedString = FormatXml(dr(0).ToString)
        XMLFile = XDocument.Parse(formattedString)

        path = Server.MapPath("../XML/")
        WriteFile(path & Regex.Replace(ChannelId, "[^0-9a-zA-Z]+", "") & "_PPVFile_" & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml", formattedString)
        dr.Close()
        cmd.Dispose()
        MyConnection.Close()
        MyConnection.Dispose()

        Dim _FileInfo As New System.IO.FileInfo(path & Regex.Replace(ChannelId, "[^0-9a-zA-Z]+", "") & "_PPVFile_" & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml")
        If Not _FileInfo.Exists() Then
            fileError = 1
            'Exit Sub
        Else
            fileError = 0
        End If

        btnGenerateXML.Enabled = False
        lbGenerateXML.Visible = True
        hyViewXml.Visible = True

        hyViewXml.NavigateUrl = "~/XML/" & Regex.Replace(ChannelId.ToString.Trim, "[^0-9a-zA-Z]+", "") & "_PPVFile_" & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml"
        Dim strStartDate, strEndDate As String
        strStartDate = StartDate.ToString("dd-MMM-yyyy")
        strEndDate = EndDate.ToString("dd-MMM-yyyy")

        Dim strRecepient As String, strSubject As String, strBody As String, strAttachment As String, strBcc As String, strRegards As String
        strRecepient = "rohitm@ndtv.com;sankalp@ndtv.com"
        strSubject = "XML generated for " & ddlChannelID.Text.ToString.Trim & " from " & strStartDate & " to " & strEndDate & ""
        strBody = "Hi Team,<br/><br/>XML for <b>" & ddlChannelID.Text.ToString.Trim & "</b> has been generated from <b>" & strStartDate & "</b> to <b>" & strEndDate & "</b>.<br/><br/>"
        strRegards = "Regards<br/>Team EPG, NDTV"
        strBcc = ""
        strAttachment = path & Regex.Replace(ChannelId, "[^0-9a-zA-Z]+", "") & "_PPVFile_" & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml"
        'Logger.mailMessage(strRecepient.Trim, strSubject.Trim, strBody.Trim & strRegards.Trim, strBcc.Trim, strAttachment.Trim)
        myMessageBox("XML generated Successfully!")
    End Sub
    Private Shared Function WriteFile(ByVal FileName As String, ByVal FileData As String) As Integer
        WriteFile = 0
        Dim outStream As StreamWriter
        outStream = New StreamWriter(FileName, False)
        outStream.Write(FileData)
        outStream.Close()
    End Function
    Private Function FormatXml(ByVal sUnformattedXml As String) As String
        Try
            'load unformatted xml into a dom
            Dim xd As New XmlDocument()
            xd.LoadXml(sUnformattedXml)

            'will hold formatted xml
            Dim sb As New StringBuilder()

            'pumps the formatted xml into the StringBuilder above
            Dim sw As New StringWriter(sb)

            'does the formatting
            Dim xtw As XmlTextWriter = Nothing

            Try
                'point the xtw at the StringWriter
                xtw = New XmlTextWriter(sw)

                'we want the output formatted
                xtw.Formatting = Formatting.Indented

                'get the dom to dump its contents into the xtw 
                xd.WriteTo(xtw)
            Catch ex As Exception
                Logger.LogError("Update PPV", "FormatXml", ex.Message.ToString, User.Identity.Name)
            Finally
                'clean up even if error
                If xtw IsNot Nothing Then
                    xtw.Close()
                End If
            End Try

            'return the formatted xml
            Return sb.ToString()
        Catch ex As Exception
            Logger.LogError("Update PPV", "FormatXml", ex.Message.ToString, User.Identity.Name)
            Return (0)
        End Try
    End Function
    Private Sub btnGenerateXML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateXML.Click
        Try
            CreateXml()
            If fileError = 1 Then
                myErrorBox("XML File not generated. !")
                Exit Sub
            End If
        Catch ex As Exception
            Logger.LogError("Update PPV", "btnGenerateXML_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdXMLGenerated_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdXMLGenerated.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Text = (Convert.ToDateTime(e.Row.Cells(0).Text)).ToString("dd-MMM-yyyy")
            End If
        Catch ex As Exception
            Logger.LogError("Update PPV", "grdXMLGenerated_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class