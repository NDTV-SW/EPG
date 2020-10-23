Imports System
Imports System.Data.SqlClient
Public Class TvStarDetails
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try

                lbProfileId.Text = Request.QueryString("profileId").ToString
                
                Dim sql As String
                sql = "select c.Name,b.ChannelId as Channel,b.ProgName as Programme from tvstars_prog_mapping a join mst_program b on a.Progid=b.ProgID join mst_tvstars c on a.ProfileID=c.ProfileID where a.ProfileID='" & lbProfileId.Text & "'"

                Dim adpCelDetails As New SqlDataAdapter(sql, ConString)
                Dim dt As New DataTable
                adpCelDetails.Fill(dt)
                grdCelebDetails.DataSource = dt
                grdCelebDetails.DataBind()

            Catch ex As Exception
                Logger.LogError("TvStarDetails", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub
End Class