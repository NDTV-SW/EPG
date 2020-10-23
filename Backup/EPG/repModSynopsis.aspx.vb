Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class repModSynopsis
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            ddlChannelId.DataBind()
            ddlChannelId.Items.Insert(0, New ListItem("All Channels", "0"))
            Try
                Dim qryChannelId As String = Request.QueryString("channelid")
                Dim qryhour As String = Request.QueryString("hour")
                Dim qryrepdate As String = Request.QueryString("repdate")
                Dim qrylangid As String = Request.QueryString("langid")
                Dim qryonair As String = Request.QueryString("onair")

                ddlChannelId.SelectedValue = qryChannelId
                ddlHour.SelectedValue = qryhour
                txtDate.Text = qryrepdate
                ddlLanguage.SelectedValue = qrylangid
                chkOnAir.Checked = IIf(qryonair = "0", False, True)

                grdRepModSynopsis.DataBind()
            Catch ex As Exception
                Logger.LogError("repModSynopsis", "Page_Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Dim intCurrentRow As Integer
    
    Protected Sub grdRepModSynopsis_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdRepModSynopsis.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hyEdit As HyperLink = DirectCast(e.Row.FindControl("hyEdit"), HyperLink)
                Dim lbProgID As Label = DirectCast(e.Row.FindControl("lbProgID"), Label)
                Dim lbEngProg As Label = DirectCast(e.Row.FindControl("lbEngProg"), Label)
                Dim lbEngSyn As Label = DirectCast(e.Row.FindControl("lbEngSyn"), Label)
                Dim lbEpisodeNo As Label = DirectCast(e.Row.FindControl("lbEpisodeNo"), Label)

                hyEdit.NavigateUrl = "javascript:openWin('" & ddlChannelId.SelectedValue & "','" & ddlLanguage.SelectedValue & "','" & ddlLanguage.SelectedItem.Text & "','" & IIf(chkOnAir.Checked, "1", "0") & "','" & txtDate.Text & "','" & ddlHour.SelectedValue & "','" & lbProgID.Text & "','MainContent_grdRepModSynopsis_hyEdit_" & e.Row.RowIndex & "','" & lbEpisodeNo.Text & "')"


                If User.IsInRole("ENGLISH") Then
                    If Not ddlLanguage.SelectedItem.Text.ToUpper = "ENGLISH" Then
                        e.Row.Cells(8).Text = ""
                    End If
                ElseIf User.IsInRole("HINDI") Then
                    If Not ddlLanguage.SelectedItem.Text.ToUpper = "HINDI" Then
                        e.Row.Cells(8).Text = ""
                    End If
                ElseIf User.IsInRole("MARATHI") Then
                    If Not ddlLanguage.SelectedItem.Text.ToUpper = "MARATHI" Then
                        e.Row.Cells(8).Text = ""
                    End If
                ElseIf User.IsInRole("TELUGU") Then
                    If Not ddlLanguage.SelectedItem.Text.ToUpper = "TELUGU" Then
                        e.Row.Cells(8).Text = ""
                    End If
                ElseIf User.IsInRole("TAMIL") Then
                    If Not ddlLanguage.SelectedItem.Text.ToUpper = "TAMIL" Then
                        e.Row.Cells(8).Text = ""
                    End If
                End If
            End If

        Catch ex As Exception
            Logger.LogError("repModSynopsis", "grdRepModSynopsis_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
End Class