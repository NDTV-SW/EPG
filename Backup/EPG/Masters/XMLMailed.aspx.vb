
Public Class XMLMailed
    Inherits System.Web.UI.Page


    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER") Or User.IsInRole("USER")) Then
                Response.Redirect("default.aspx")
            End If
            If Page.IsPostBack = False Then

            End If
        Catch ex As Exception
            Logger.LogError("XML Mailed", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Dim Sno As Integer = 1
    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSno"), Label)
                Dim lbName As Label = DirectCast(e.Row.FindControl("lbXMLFileName"), Label)
                Dim lbFTPDateTime As Label = DirectCast(e.Row.FindControl("lbFTPDateTime"), Label)
                Dim lbmaxdateSend As Label = DirectCast(e.Row.FindControl("lbmaxdateSend"), Label)
                Dim lbRowId As Label = DirectCast(e.Row.FindControl("lbRowId"), Label)
                Dim lbXMLDateTime As Label = DirectCast(e.Row.FindControl("lbXMLDateTime"), Label)
                Dim lbXMLFileName As Label = DirectCast(e.Row.FindControl("lbXMLFileName"), Label)
                Dim lbStartDate As Label = DirectCast(e.Row.FindControl("lbStartDate"), Label)
                Dim lbEndDate As Label = DirectCast(e.Row.FindControl("lbEndDate"), Label)
                Dim lbChannelId As Label = DirectCast(e.Row.FindControl("lbChannelId"), Label)

                Dim hyXMLMailedOn As HyperLink = DirectCast(e.Row.FindControl("hyXMLMailedOn"), HyperLink)
                Dim hyXMLFileName As HyperLink = DirectCast(e.Row.FindControl("hyXMLFileName"), HyperLink)
                Dim hyChannelID As HyperLink = DirectCast(e.Row.FindControl("hyChannelID"), HyperLink)

                hyChannelID.NavigateUrl = "Javascript:openXmlMailed('" & lbChannelId.Text & "')"
                hyXMLMailedOn.NavigateUrl = "Javascript:openWin('" & lbRowId.Text & "')"
                hyXMLFileName.NavigateUrl = "~/xml/" & lbXMLFileName.Text

                Dim strName As String, strLength As String, strStartDate As String, strEndDate As String
                Dim strSMonth As String, strSDate As String, strSYear As String
                Dim strEMonth As String, strEDate As String, strEYear As String
                lbXMLDateTime.Text = Convert.ToDateTime(lbXMLDateTime.Text).ToString("dd-MMM-yyyy hh:mm:ss tt")
                lbmaxdateSend.Text = Convert.ToDateTime(lbmaxdateSend.Text).ToString("dd-MMM-yyyy")

                'If Convert.ToDateTime(lbmaxdateSend.Text).ToString("dd-MMM-yyyy") < DateAdd(DateInterval.Day, 4, Now()).ToString("dd-MMM-yyyy") Then
                'lbmaxdateSend.ForeColor = Drawing.Color.Red
                'End If

                If Convert.ToDateTime(lbmaxdateSend.Text).ToString("yyyyMMdd") < DateAdd(DateInterval.Day, 4, Now()).ToString("yyyyMMdd") Then
                    lbmaxdateSend.ForeColor = Drawing.Color.Red
                End If

                Try
                    lbFTPDateTime.Text = Convert.ToDateTime(lbFTPDateTime.Text).ToString("dd-MMM-yyyy hh:mm:ss tt")
                Catch
                End Try

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

                lbSno.Text = Sno.ToString
                Sno = Sno + 1

            End If
        Catch ex As Exception
            Logger.LogError("XML Mailed on", "GridView1_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


End Class