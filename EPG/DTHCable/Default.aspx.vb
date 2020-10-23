Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class DTHDefault
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim strDaysToSend As String = ""
            Dim i As Integer = 0
            While i < chkWeekList.Items.Count
                If chkWeekList.Items(i).Selected Then
                    strDaysToSend = strDaysToSend & chkWeekList.Items(i).Value & ","
                End If
                i = i + 1
            End While
            strDaysToSend = strDaysToSend.Substring(0, strDaysToSend.Length - 1)
            'Dim sqlString As String

            Dim obj As New clsExecute
            Dim strParameterList As String
            Dim strParameterType As String
            Dim strParameterValues As String

            If btnSave.Text = "Save" Then
                Dim strMailSubject As String = txtName.Text.Trim & " : Master EPG Updates"
                Dim strMailUpdateSubject As String = txtName.Text.Trim & " : Regular EPG Updates"
                Dim strMailBody As String = "Please find attached udpated EPG schedule."

                strParameterList = "NAME~CITY~SYSTEMNAME~EPGTYPE~FOLDERNAME~FileDateformatSuffix~RECIPIENTLIST~CCLIST~BCCLIST~MULTIPLEDAYS~FUNCTIONNAME~OPERATORTYPE~TIMETOSEND1~TIMETOSEND2~TIMETOSEND3~TIMETOSEND4~ACTIVE~MAPPINGTABLE~SELECTQUERY~PRIORITY~HOURLYUPDATE~DAYSTOSEND~STARTDAY~NOOFDAYS~ISPROPERXML~DEFAULTXMLSTRING~XMLSTRING~FILLERSTRING~CHANNELREPLACESTRING~FILEPREFIXFIELD~SIDFIELD~MULTIPLECHANNELS~MailSubject~MailUpdateSubject~MailBody~ACTION~RETURNOUTPUT"
                strParameterType = "VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar"

                strParameterValues = txtName.Text.Trim & "~" & txtCity.Text.Trim & "~" & txtSystemName.Text.Trim & "~" & txtEPGType.Text.Trim & "~" & txtFolder.Text.Trim & "~"
                strParameterValues = strParameterValues & txtFileFormat.Text.Trim & "~" & txtRecepientList.Text.Trim & "~" & txtCCList.Text.Trim & "~" & txtCCList.Text.Trim & "~" & chkMultipleDays.Checked & "~"
                strParameterValues = strParameterValues & txtFunctionName.Text.Trim & "~" & txtOperatorType.Text.Trim & "~" & txttimetosend1.Text.Trim & "~" & txttimetosend2.Text.Trim & "~" & txttimetosend3.Text.Trim & "~" & txttimetosend4.Text.Trim & "~" & chkActive.Checked & "~"
                strParameterValues = strParameterValues & "dthcable_channelmapping~" & txtSelectQuery.Text & "~" & txtPriority.Text.Trim & "~" & chkHourlyUpdates.Checked & "~" & strDaysToSend.Trim & "~"
                strParameterValues = strParameterValues & txtStartday.Text.Trim & "~" & txtNoOfDays.Text.Trim & "~" & chkIsProperXML.Checked & "~" & txtDefaultXMLString.Text & "~" & txtXMLString.Text & "~"
                strParameterValues = strParameterValues & txtFillerString.Text.Trim & "~" & txtChannelReplaceString.Text & "~" & txtFilePrefixField.Text.Trim & "~" & txtSIDField.Text.Trim & "~" & chkMultipleChannels.Checked & "~" & strMailSubject & "~" & strMailUpdateSubject & "~" & strMailBody & "~A~" & chkReturnsOutput.Checked

            Else
                strParameterList = "OPERATORID~NAME~CITY~SYSTEMNAME~EPGTYPE~FOLDERNAME~FileDateformatSuffix~RECIPIENTLIST~CCLIST~BCCLIST~MULTIPLEDAYS~FUNCTIONNAME~OPERATORTYPE~TIMETOSEND1~TIMETOSEND2~TIMETOSEND3~TIMETOSEND4~ACTIVE~MAPPINGTABLE~SELECTQUERY~PRIORITY~HOURLYUPDATE~DAYSTOSEND~STARTDAY~NOOFDAYS~ISPROPERXML~DEFAULTXMLSTRING~XMLSTRING~FILLERSTRING~CHANNELREPLACESTRING~FILEPREFIXFIELD~SIDFIELD~MULTIPLECHANNELS~ACTION~RETURNOUTPUT"
                strParameterType = "Int~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar~VarChar"
                strParameterValues = lbOperatorID.Text.Trim & "~" & txtName.Text.Trim & "~" & txtCity.Text.Trim & "~" & txtSystemName.Text.Trim & "~" & txtEPGType.Text.Trim & "~" & txtFolder.Text.Trim & "~"
                strParameterValues = strParameterValues & txtFileFormat.Text.Trim & "~" & txtRecepientList.Text.Trim & "~" & txtCCList.Text.Trim & "~" & txtCCList.Text.Trim & "~" & chkMultipleDays.Checked & "~"
                strParameterValues = strParameterValues & txtFunctionName.Text.Trim & "~" & txtOperatorType.Text.Trim & "~" & txttimetosend1.Text.Trim & "~" & txttimetosend2.Text.Trim & "~" & txttimetosend3.Text.Trim & "~" & txttimetosend4.Text.Trim & "~" & chkActive.Checked & "~"
                strParameterValues = strParameterValues & "dthcable_channelmapping~" & txtSelectQuery.Text & "~" & txtPriority.Text.Trim & "~" & chkHourlyUpdates.Checked & "~" & strDaysToSend.Trim & "~"
                strParameterValues = strParameterValues & txtStartday.Text.Trim & "~" & txtNoOfDays.Text.Trim & "~" & chkIsProperXML.Checked & "~" & txtDefaultXMLString.Text & "~" & txtXMLString.Text & "~"
                strParameterValues = strParameterValues & txtFillerString.Text.Trim & "~" & txtChannelReplaceString.Text & "~" & txtFilePrefixField.Text.Trim & "~" & txtSIDField.Text.Trim & "~" & chkMultipleChannels.Checked & "~U~" & chkReturnsOutput.Checked
            End If
            obj.executeSQL("SP_mst_dthcableoperators", strParameterList, strParameterType, strParameterValues, True, False)
            clearAll()
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                Dim lbName As Label = DirectCast(e.Row.FindControl("lbName"), Label)
                Dim lbMappingTable As Label = DirectCast(e.Row.FindControl("lbMappingTable"), Label)
                Dim hyView As HyperLink = DirectCast(e.Row.FindControl("hyView"), HyperLink)
                hyView.NavigateUrl = "Operatorchannels.aspx?operator=" & lbName.Text & "&opTable=" & lbMappingTable.Text & ""
            Catch
            End Try
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim abc As String
            abc = "s"
        End If
    End Sub

    Private Sub clearAll()
        txtName.Text = ""
        txtCity.Text = ""
        txtSystemName.Text = ""
        txtEPGType.Text = ""
        txtFolder.Text = ""
        txtFileFormat.Text = ""
        txtRecepientList.Text = ""
        txtCCList.Text = ""

        txtFunctionName.Text = ""
        txtOperatorType.Text = ""

        txtTimetoSend1.Text = ""
        txtTimetoSend2.Text = ""
        txtTimetoSend3.Text = ""
        txtTimetoSend4.Text = ""

        txtSelectQuery.Text = ""

        txtPriority.Text = ""
        txtDefaultXMLString.Text = ""
        txtXMLString.Text = ""
        txtNoOfDays.Text = ""
        txtStartday.Text = ""
        txtFilePrefixField.Text = ""
        txtSIDField.Text = ""
        txtFillerString.Text = ""
        txtChannelReplaceString.Text = ""

        chkMultipleDays.Checked = False
        chkMultipleChannels.Checked = False
        chkActive.Checked = False
        chkHourlyUpdates.Checked = False
        chkIsProperXML.Checked = False
        chkReturnsOutput.Checked = False
        btnSave.Text = "Save"

        Dim i As Integer = 0
        While i < chkWeekList.Items.Count
            chkWeekList.Items(i).Selected = True
            i = i + 1
        End While

        GridView1.SelectedIndex = -1
        GridView1.DataBind()

    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim i As Integer = 0
        While i < chkWeekList.Items.Count
            chkWeekList.Items(i).Selected = False
            i = i + 1
        End While

        lbOperatorID.Text = DirectCast(GridView1.SelectedRow.FindControl("lbOperatorID"), Label).Text
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("SELECT * FROM mst_dthcableoperators WHERE OPERATORID='" & lbOperatorID.Text & "'", False)

        txtName.Text = dt.Rows(0)("name").ToString
        txtCity.Text = dt.Rows(0)("city").ToString
        txtSystemName.Text = dt.Rows(0)("SystemName").ToString
        txtEPGType.Text = dt.Rows(0)("EPGType").ToString
        txtFolder.Text = dt.Rows(0)("FolderName").ToString
        txtFileFormat.Text = dt.Rows(0)("fileDateFormatSuffix").ToString
        txtRecepientList.Text = dt.Rows(0)("recipientlist").ToString
        txtCCList.Text = dt.Rows(0)("ccList").ToString
        txtCCList.Text = dt.Rows(0)("bccList").ToString
        chkMultipleDays.Checked = dt.Rows(0)("multipleDays").ToString
        chkMultipleChannels.Checked = dt.Rows(0)("multipleChannels").ToString
        txtFunctionName.Text = dt.Rows(0)("functionName").ToString
        txtOperatorType.Text = dt.Rows(0)("OperatorType").ToString
        txttimetosend1.Text = Convert.ToDateTime(dt.Rows(0)("TimeToSend1").ToString).ToString("HH:mm")
        txttimetosend2.Text = Convert.ToDateTime(dt.Rows(0)("TimeToSend2").ToString).ToString("HH:mm")
        txttimetosend3.Text = Convert.ToDateTime(dt.Rows(0)("TimeToSend3").ToString).ToString("HH:mm")
        txttimetosend4.Text = Convert.ToDateTime(dt.Rows(0)("TimeToSend4").ToString).ToString("HH:mm")
        chkActive.Checked = dt.Rows(0)("Active").ToString
        txtSelectQuery.Text = dt.Rows(0)("SelectQuery").ToString
        chkHourlyUpdates.Checked = dt.Rows(0)("HourlyUpdate").ToString
        txtPriority.Text = dt.Rows(0)("Priority").ToString

        Dim strDaysToSend As String = dt.Rows(0)("DaysToSend").ToString
        Dim ArStrDaysToSend As Array = strDaysToSend.Split(",")
        For Each valDaysToSend In ArStrDaysToSend
            chkWeekList.Items(valDaysToSend - 1).Selected = True
        Next

        txtNoOfDays.Text = dt.Rows(0)("NoOfDays").ToString
        txtStartday.Text = dt.Rows(0)("StartDay").ToString
        chkIsProperXML.Checked = dt.Rows(0)("IsProperXML").ToString
        txtDefaultXMLString.Text = dt.Rows(0)("DefaultXMLString").ToString
        txtXMLString.Text = dt.Rows(0)("XMLString").ToString
        txtFillerString.Text = dt.Rows(0)("FillerString").ToString
        txtChannelReplaceString.Text = dt.Rows(0)("ChannelReplaceString").ToString
        txtFilePrefixField.Text = dt.Rows(0)("FilePrefixField").ToString
        txtSIDField.Text = dt.Rows(0)("SiDField").ToString
        chkReturnsOutput.Checked = dt.Rows(0)("returnoutput").ToString

        btnSave.Text = "Update"
    End Sub

End Class