Imports System
Imports System.Data.SqlClient

Public Class ShowProgSched
    Inherits System.Web.UI.Page

    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ConnectionString

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then
            Try
                Dim strProgId = Request.QueryString("ProgId").ToString
                Dim adpProgSyn As New SqlDataAdapter("select a.Channelid as Channel, b.progname as 'Programme',convert(varchar,a.progdate,106) as 'Date',CONVERT(VARCHAR(9),RIGHT(a.ProgTime,7),108) as 'Time',a.Duration,a.EpisodeNo as 'Episode' from mst_epg a join mst_program b on a.progid=b.progid where progdate > dbo.GetLocalDate()-1 and a.progid='" & strProgId & "' order by a.progdate, a.progtime", ConString)
                Dim dt As New DataTable
                adpProgSyn.Fill(dt)
                grdProgSched.DataSource = dt
                grdProgSched.DataBind()

            Catch ex As Exception
                Logger.LogError("Page Load", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        hfSend.Value = "2"
    End Sub
End Class