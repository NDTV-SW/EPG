Imports System
Imports System.Data.SqlClient

Public Class showXMLMailed
    Inherits System.Web.UI.Page
    Dim ConString As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ConnectionString)

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try
                lbChannel.Text = Request.QueryString("ChannelId").ToString

                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL("select top 20 * from fn_xml_ftp_time ('" & lbChannel.Text & "')  order by CAST(maxxmldatetime as datetime) desc", False)

                GridView1.DataSource = dt
                GridView1.DataBind()

            Catch ex As Exception
                Logger.LogError("Show XML Mailed", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lbName As Label = DirectCast(e.Row.FindControl("lbXMLFileName"), Label)
                Dim lbXMLMailedOn As Label = DirectCast(e.Row.FindControl("lbminXMLMailedOn"), Label)
                Dim lbRowId As Label = DirectCast(e.Row.FindControl("lbRowId"), Label)
                'Dim lbXMLDateTime As Label = DirectCast(e.Row.FindControl("lbminXMLDateTime"), Label)
                Dim lbXMLFileName As Label = DirectCast(e.Row.FindControl("lbXMLFileName"), Label)
                Dim lbStartDate As Label = DirectCast(e.Row.FindControl("lbStartDate"), Label)
                Dim lbEndDate As Label = DirectCast(e.Row.FindControl("lbEndDate"), Label)
                'Dim lbFTPDateTime As Label = DirectCast(e.Row.FindControl("lbminFTPDateTime"), Label)

                'Dim hyXMLMailedOn As HyperLink = DirectCast(e.Row.FindControl("hyXMLMailedOn"), HyperLink)
                Dim hyXMLFileName As HyperLink = DirectCast(e.Row.FindControl("hyXMLFileName"), HyperLink)

                hyXMLFileName.NavigateUrl = "~/xml/" & lbXMLFileName.Text

                Dim strName As String, strLength As String, strStartDate As String, strEndDate As String
                Dim strSMonth As String, strSDate As String, strSYear As String
                Dim strEMonth As String, strEDate As String, strEYear As String
                'lbXMLDateTime.Text = Convert.ToDateTime(lbXMLDateTime.Text).ToString("dd-MMM-yyyy hh:mm:ss tt")
                'Try
                '    'lbXMLMailedOn.Text = Convert.ToDateTime(lbXMLMailedOn.Text).ToString("dd-MMM-yyyy")
                '    lbFTPDateTime.Text = Convert.ToDateTime(lbFTPDateTime.Text).ToString("dd-MMM-yyyy hh:mm:ss tt")
                'Catch
                'End Try

                strName = lbName.Text
                strLength = lbName.Text.ToString.Length

                strStartDate = lbName.Text.Substring(lbName.Text.ToString.Length - 16, 6)
                strEndDate = lbName.Text.Substring(lbName.Text.ToString.Length - 10, 6)

                strSYear = strStartDate.Substring(0, 2)
                strSMonth = strStartDate.Substring(2, 2)
                strSDate = strStartDate.Substring(4, 2)

                strEYear = strEndDate.Substring(0, 2)
                strEMonth = strEndDate.Substring(2, 2)
                strEDate = strEndDate.Substring(4, 2)

                lbStartDate.Text = Convert.ToDateTime(strSMonth & "/" & strSDate & "/" & strSYear).ToString("dd-MMM-yyyy")
                lbEndDate.Text = Convert.ToDateTime(strEMonth & "/" & strEDate & "/" & strEYear).ToString("dd-MMM-yyyy")
            End If
        Catch ex As Exception
            Logger.LogError("XML Mailed on", "GridView1_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        hfSend.Value = "2"
    End Sub
    
End Class