Imports System
Imports System.Data.SqlClient
Public Class ChannelOnairOffAir
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Dim MyConnection As New SqlConnection(ConString)
    Dim flag As Integer = 0
    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' '' ''     If Not (User.Identity.Name.ToLower = "hemant" Or User.Identity.Name.ToLower = "kautilyar") Then
            If Not (User.Identity.Name.ToLower = "rohitm" Or User.Identity.Name.ToLower = "sankalp") Then
                grdAirtelOnair.Columns(0).Visible = False
                grdChannelOnAir.Columns(0).Visible = False
                grdChannelOffAir.Columns(0).Visible = False
                'Response.Redirect("~/default.aspx")
            End If

        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

#Region "RowDataBound"
    Dim AirtelOnAirSno As Integer = 1
    Dim offAirSno As Integer = 1
    Dim onAirSno As Integer = 1
    Dim AnytimeSno As Integer = 1
    Private Sub grdAirtelOnAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAirtelOnair.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Try
                    Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSno"), Label)
                    Dim lbAirtelFTP As Label = DirectCast(e.Row.FindControl("lbAirtelFTP"), Label)

                    lbSno.Text = AirtelOnAirSno.ToString
                    AirtelOnAirSno = AirtelOnAirSno + 1
                    Dim lbEPGAvailableTill As Label = DirectCast(e.Row.FindControl("lbEPGAvailableTill"), Label)
                    Dim dtEPGAvailableTill As Date = Convert.ToDateTime(lbEPGAvailableTill.Text).ToString
                    If dtEPGAvailableTill.ToString("yyyyMMdd") <= DateTime.Now.AddDays(1).ToString("yyyyMMdd") Then
                        e.Row.BackColor = Drawing.Color.IndianRed
                    End If
                    If Not lbAirtelFTP.Text = "True" Then
                        e.Row.BackColor = Drawing.Color.OrangeRed
                    End If
                Catch
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", "grdAirtelOnAir_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdChannelOffAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdChannelOffAir.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Try
                    Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSno"), Label)
                    lbSno.Text = offAirSno.ToString
                    offAirSno = offAirSno + 1

                    Dim lbColor As Label = DirectCast(e.Row.FindControl("lbColor"), Label)
                    Dim lbEPGAvailableTill As Label = DirectCast(e.Row.FindControl("lbEPGAvailableTill"), Label)
                    Dim dtEPGAvailableTill As Date = Convert.ToDateTime(lbEPGAvailableTill.Text).ToString


                    If lbColor.Text.ToLower = "green" Then
                        e.Row.BackColor = Drawing.Color.LightGreen
                    End If

                    If dtEPGAvailableTill.ToString("yyyyMMdd") <= DateTime.Now.AddDays(1).ToString("yyyyMMdd") Then
                        e.Row.BackColor = Drawing.Color.IndianRed
                    End If
                Catch
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", "grdChannelOffAir_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdAnyTime_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAnyTime.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Try
                    Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSno"), Label)
                    lbSno.Text = AnytimeSno.ToString
                    AnytimeSno = AnytimeSno + 1

                    Dim lbEPGAvailableTill As Label = DirectCast(e.Row.FindControl("lbEPGAvailableTill"), Label)
                    Dim dtEPGAvailableTill As Date = Convert.ToDateTime(lbEPGAvailableTill.Text).ToString


                    If dtEPGAvailableTill.ToString("yyyyMMdd") <= DateTime.Now.AddDays(1).ToString("yyyyMMdd") Then
                        e.Row.BackColor = Drawing.Color.IndianRed
                    End If
                Catch
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", "grdAnyTime_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdChannelOnAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdChannelOnAir.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Try
                    Dim lbSno As Label = DirectCast(e.Row.FindControl("lbSno"), Label)
                    Dim lbAirtelFTP As Label = DirectCast(e.Row.FindControl("lbAirtelFTP"), Label)
                    Dim lbAirtelMail As Label = DirectCast(e.Row.FindControl("lbAirtelMail"), Label)

                    lbSno.Text = onAirSno.ToString
                    onAirSno = onAirSno + 1
                    Dim lbEPGAvailableTill As Label = DirectCast(e.Row.FindControl("lbEPGAvailableTill"), Label)
                    Dim dtEPGAvailableTill As Date = Convert.ToDateTime(lbEPGAvailableTill.Text).ToString
                    If dtEPGAvailableTill.ToString("yyyyMMdd") <= DateTime.Now.AddDays(1).ToString("yyyyMMdd") Then
                        e.Row.BackColor = Drawing.Color.IndianRed
                    End If
                    If lbAirtelMail.Text = "True" Then
                        e.Row.BackColor = Drawing.Color.OrangeRed
                    End If
                Catch
                End Try
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", "grdChannelOnAir_RowDataBound", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

#Region "Update Onair-OffAir"
    Protected Sub grdAirtelOnAir_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAirtelOnair.RowCommand
        Try
            Dim lbRowID As Label = DirectCast(grdChannelOnAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowID"), Label)
            Dim lbChannelId As Label = DirectCast(grdChannelOnAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbChannelId"), Label)
            Dim obj As New clsExecute
            If e.CommandName.ToLower = "offair" Then
                Dim dt As DataTable = obj.executeSQL("update mst_Channel set OnAir=0 where RowId='" & lbRowID.Text & "'", False)
                obj.executeSQL("insert into mst_channel_offair values('" & lbChannelId.Text & "','" & User.Identity.Name & "',dbo.getlocaldate(),0)", False)
                bindGrids()
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", e.CommandName.ToLower & " grdAirtelOnAir_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub grdChannelOnAir_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdChannelOnAir.RowCommand
        Try
            Dim lbRowID As Label = DirectCast(grdChannelOnAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowID"), Label)
            Dim lbChannelId As Label = DirectCast(grdChannelOnAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbChannelId"), Label)
            Dim obj As New clsExecute
            If e.CommandName.ToLower = "offair" Then
                Dim dt As DataTable = obj.executeSQL("update mst_Channel set OnAir=0 where RowId='" & lbRowID.Text & "'", False)
                obj.executeSQL("insert into mst_channel_offair values('" & lbChannelId.Text & "','" & User.Identity.Name & "',dbo.getlocaldate(),0)", False)
                bindGrids()
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", e.CommandName.ToLower & " grdChannelOnAir_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Protected Sub grdChannelOffAir_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdChannelOffAir.RowCommand
        Try
            Dim lbRowID As Label = DirectCast(grdChannelOffAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbRowID"), Label)
            Dim lbChannelId As Label = DirectCast(grdChannelOffAir.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbChannelId"), Label)
            Dim obj As New clsExecute
            If e.CommandName.ToLower = "onair" Then
                Dim dt As DataTable = obj.executeSQL("update mst_Channel set OnAir=1 where RowId='" & lbRowID.Text & "'", False)
                bindGrids()
            End If
        Catch ex As Exception
            Logger.LogError("ChannelOnAirOffAir", e.CommandName.ToLower & " grdChannelOffAir_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
#End Region

    Private Sub bindGrids()
        grdAirtelOnair.DataBind()
        grdChannelOnAir.DataBind()
        grdChannelOffAir.DataBind()
    End Sub
End Class