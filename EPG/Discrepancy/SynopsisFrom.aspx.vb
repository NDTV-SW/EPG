Imports System
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml

Public Class SynopsisFrom
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
            Try
                
                'Dim qryChannelId As String = Request.QueryString("channelid")
                Dim qryrepfromdate As String = Request.QueryString("repfromdate")
                Dim qryreptodate As String = Request.QueryString("reptodate")

                'txtChannel.Text = qryChannelId
                txtFromDate.Text = qryrepfromdate
                txtToDate.Text = qryreptodate


                If txtFromDate.Text = "" And txtToDate.Text = "" Then
                    txtFromDate.Text = DateTime.Now.AddDays(-1).Date
                    txtToDate.Text = DateTime.Now.Date
                End If
                grdSynopsisFrom.DataBind()
            Catch ex As Exception
                Logger.LogError("repModSynopsis", "Page_Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Dim intCurrentRow As Integer

    Protected Sub grdSynopsisFrom_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSynopsisFrom.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hyEdit As HyperLink = DirectCast(e.Row.FindControl("hyEdit"), HyperLink)
                Dim lbProgID As Label = DirectCast(e.Row.FindControl("lbProgID"), Label)
                Dim lbEngProg As Label = DirectCast(e.Row.FindControl("lbEngProg"), Label)
                Dim lbEngSyn As Label = DirectCast(e.Row.FindControl("lbEngSyn"), Label)
                Dim lbEpisodeNo As Label = DirectCast(e.Row.FindControl("lbEpisodeNo"), Label)

                hyEdit.NavigateUrl = "javascript:openWin('" & txtFromDate.Text & "','" & txtToDate.Text & "','" & lbProgID.Text & "','MainContent_grdRepModSynopsis_hyEdit_" & e.Row.RowIndex & "','" & lbEpisodeNo.Text & "')"

            End If

        Catch ex As Exception
            Logger.LogError("repModSynopsis", "grdRepModSynopsis_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    

End Class