Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class ProgramDiscrepancyNewHex
    Inherits System.Web.UI.Page
    Private Sub myErrorBox1(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
                ddlDays.SelectedValue = 6
                bindGrdXMLGenerated(False)
                bindGrdGenerateAgain(False)

            End If

            btnGenerateXML.Visible = True


        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "Page Load", ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

#Region "Bind Controls"
    
  
    Private Sub bindGrdXMLGenerated(ByVal paging As Boolean)
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select distinct convert(varchar,progdate,106) progdate1,progdate from mst_epg where ChannelId='" & txtChannel.Text & "' and xml_generated=0 and convert(varchar,Progdate,112)>=convert(varchar,dbo.GetLocalDate(),112)order by progdate", False)
            grdXMLGenerated.DataSource = dt
            If paging = False Then
                grdXMLGenerated.PageIndex = 0
            End If
            grdXMLGenerated.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "bindGrdXMLGenerated", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub bindGrdGenerateAgain(ByVal paging As Boolean)
        Try
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("select distinct convert(varchar,progdate,106) progdate1,progdate from mst_epg where ChannelId='" & txtChannel.Text & "' and xml_generated=1 and convert(varchar,Progdate,112)>=convert(varchar,dbo.GetLocalDate(),112) and convert(varchar,Progdate,112)<=convert(varchar,dbo.GetLocalDate()+3,112) order by progdate", False)
            grdGenerateAgain.DataSource = dt
            If paging = False Then
                grdGenerateAgain.PageIndex = 0
            End If
            grdGenerateAgain.DataBind()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "bindGrdGenerateAgain", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

#Region "Page Index Changing"

  

    Protected Sub grdXMLGenerated_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdXMLGenerated.PageIndexChanging
        grdXMLGenerated.PageIndex = e.NewPageIndex
        bindGrdXMLGenerated(True)
        'grdXMLGenerated.DataBind()
    End Sub

    Protected Sub grdGenerateAgain_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdGenerateAgain.PageIndexChanging
        grdGenerateAgain.PageIndex = e.NewPageIndex
        bindGrdGenerateAgain(True)
        'grdGenerateAgain.DataBind()
    End Sub

   

#End Region


    Dim fileError As Integer

#Region "XML Generation"
    Private Sub btnGenerateXML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateXML.Click
        Try
            If Convert.ToDateTime(txtStartDate.Text).AddDays(ddlDays.SelectedValue).Date > DateTime.Now.AddDays(6).Date Then
                myErrorBox("You cannot generate XML for Date > " & DateTime.Now.AddDays(6).Date.ToString("dd MMM yyyy"))
                Exit Sub
            End If

            Session.Add("StartDateValue", txtStartDate.Text)
            Session.Add("EndDateValue", Convert.ToDateTime(txtStartDate.Text).AddDays(ddlDays.SelectedValue).ToString)
            CreateXML(txtChannel.Text, txtChannel.Text.ToString.Trim, Session("StartDateValue").ToString.Trim, Session("EndDateValue").ToString.Trim)
            If fileError = 1 Then
                myErrorBox("XML File not generated. !")
                Exit Sub
            End If
            'CreateXMLRecordDetails(txtChannel.Text, txtChannel.Text, Session("StartDateValue").ToString.Trim, Session("EndDateValue").ToString.Trim)
            myMessageBox("XML Generated!")
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "btnGenerateXML_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnGenerateXMLAgain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateXMLAgain.Click
        Try
            If Convert.ToDateTime(txtStartDate.Text).AddDays(ddlDays.SelectedValue).Date > DateTime.Now.AddDays(6).Date Then
                myErrorBox("You cannot generate XML for Date > " & DateTime.Now.AddDays(6).Date.ToString("dd MMM yyyy"))
                Exit Sub
            End If
            Session.Add("StartDateValue", DateTime.Now.ToString("MM/dd/yyyy"))
            'If (ddlDays.SelectedValue > 3) Then
            '    ddlDays.SelectedValue = 3
            'End If
            Session.Add("EndDateValue", Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")).AddDays(ddlDays.SelectedValue).ToString)
            CreateXMLAgain(txtChannel.Text, txtChannel.Text, Session("StartDateValue").ToString.Trim, Session("EndDateValue").ToString.Trim)
            If fileError = 1 Then
                myErrorBox("XML File not generated. !")
                Exit Sub
            End If
            'CreateXMLRecordDetails(txtChannel.Text, txtChannel.Text, Session("StartDateValue").ToString.Trim, Session("EndDateValue").ToString.Trim)
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "btnGenerateXML_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub CreateXML(ByVal ChannelId As String, ByVal ChannelName As String, ByVal StartDate As Date, ByVal EndDate As Date)
        Try
            Dim path As String = ""
            Dim formattedString As String
            Dim XMLFile As XDocument

            Dim obj As New clsExecute
            'obj.executeSQL("sp_epg_recording", "ChannelId~EPGFromDate~EpgToDate", "VarChar~DateTime~DateTime", txtChannel.Text & "~" & StartDate & "~" & EndDate, True, False)

            Dim dt As DataTable = obj.executeSQL("Select [dbo].[FN_XML_EPG_NEW_hexdesc] ('" & ChannelId.ToString.Trim & "','" & StartDate & "','" & EndDate & "')", False)

            If dt.Rows(0)(0).ToString.ToUpper = "ERROR" Then
                lbGenerateXML.Visible = True
                lbGenerateXML.Text = "The XML for specified dates cannot be generated as some or all dates do not have new EPG data. You must upload FPC and 'BUILD EPG' for these dates!"
                Exit Sub
            End If
            formattedString = FormatXml(dt.Rows(0)(0).ToString)

            XMLFile = XDocument.Parse(formattedString)
            path = Server.MapPath("../XML/")
            WriteFile(path & Regex.Replace(ChannelName.ToString, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml", formattedString)

            Dim _FileInfo As New System.IO.FileInfo(path & Regex.Replace(ChannelName.ToString, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml")
            If Not _FileInfo.Exists() Then
                fileError = 1
                'Exit Sub
            Else
                fileError = 0
            End If

            obj.executeSQL("sp_epg_check_unique_serviceid", "ChannelId~EPGFromDate~EPGToDate", "VarChar~DateTime~DateTime", ChannelId & "~" & StartDate & "~" & EndDate, True, False)

            btnGenerateXML.Enabled = False
            lbGenerateXML.Visible = True
            hyViewXml.Visible = True

            hyViewXml.NavigateUrl = "~/XML/" & Regex.Replace(ChannelName.ToString.Trim, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & ".xml"
            Dim strStartDate, strEndDate As String
            strStartDate = (Convert.ToDateTime(Session("StartDateValue").ToString.Trim)).ToString("dd-MMM-yyyy")
            strEndDate = (Convert.ToDateTime(Session("EndDateValue").ToString.Trim)).ToString("dd-MMM-yyyy")

            myMessageBox("XML generated Successfully!")

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "CreateXML", ex.Message.ToString, User.Identity.Name)
            myErrorBox("XML not generated. Please check error logs.")
        End Try
    End Sub

    Private Sub CreateXMLAgain(ByVal ChannelId As String, ByVal ChannelName As String, ByVal StartDate As Date, ByVal EndDate As Date)
        Try
            Dim path As String = ""
            Dim formattedString As String
            Dim XMLFile As XDocument

            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL("Select [dbo].[FN_XML_EPG_NEW_AGAIN_hexdesc] ('" & ChannelId.ToString.Trim & "','" & StartDate & "','" & EndDate & "')", False)

            If dt.Rows(0)(0).ToString.ToUpper = "ERROR" Then
                lbGenerateXML.Visible = True
                lbGenerateXML.Text = "The XML for specified dates cannot be generated AGAIN as some or all dates do not have XML generated ALREADY!"
                Exit Sub
            End If

            formattedString = FormatXml(dt(0)(0).ToString)
            XMLFile = XDocument.Parse(formattedString)
            path = Server.MapPath("../XML/")
            WriteFile(path & Regex.Replace(ChannelName.ToString, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & "_Hex.xml", formattedString)
            'CatchupCount(ChannelId, StartDate, EndDate, formattedString)

            Dim _FileInfo As New System.IO.FileInfo(path & Regex.Replace(ChannelName.ToString, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & "_Hex.xml")
            If Not _FileInfo.Exists() Then
                fileError = 1
                'Exit Sub
            Else
                fileError = 0
            End If

            btnGenerateXMLAgain.Enabled = True
            hyViewXml.Visible = True

            hyViewXml.NavigateUrl = "~/XML/" & Regex.Replace(ChannelName.ToString.Trim, "[^0-9a-zA-Z]+", "") & StartDate.ToString("yyMMdd") & EndDate.ToString("yyMMdd") & "_Hex.xml"

            Dim strStartDate, strEndDate As String
            strStartDate = (Convert.ToDateTime(Session("StartDateValue").ToString.Trim)).ToString("dd-MMM-yyyy")
            strEndDate = (Convert.ToDateTime(Session("EndDateValue").ToString.Trim)).ToString("dd-MMM-yyyy")


            myMessageBox("XML generated Successfully!")

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "CreateXMLAgain", ex.Message.ToString, User.Identity.Name)
            myErrorBox("XML not generated. Please check error logs.")
        End Try
    End Sub

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
                Logger.LogError("Program Discrepancy", "FormatXml", ex.Message.ToString, User.Identity.Name)
            Finally
                'clean up even if error
                If xtw IsNot Nothing Then
                    xtw.Close()
                End If
            End Try

            'return the formatted xml
            Return sb.ToString()
        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "FormatXml", ex.Message.ToString, User.Identity.Name)
            Return (0)
        End Try
    End Function

    Private Shared Function WriteFile(ByVal FileName As String, ByVal FileData As String) As Integer
        WriteFile = 0
        Dim outStream As StreamWriter
        outStream = New StreamWriter(FileName, False)
        outStream.Write(FileData)
        outStream.Close()
    End Function

#End Region

    Protected Sub txtChannel_TextChanged(sender As Object, e As EventArgs) Handles txtChannel.TextChanged
        Try
            btnGenerateXML.Enabled = True
            hyViewXml.Visible = False
            lbGenerateXML.Visible = False
            bindGrdXMLGenerated(False)
            bindGrdGenerateAgain(False)


            Dim obj As New Logger
            lbEPGExists.Visible = True
            lbEPGExists.Text = obj.GetEpgDates(txtChannel.Text)

            Dim obj1 As New Logger
            lbEPGExists.Text = obj1.GetEpgDates(txtChannel.Text)

        Catch ex As Exception
            Logger.LogError("Program Discrepancy", "txtChannel_TextChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannelsForXML(prefixText, count)
        Return channels
    End Function


End Class