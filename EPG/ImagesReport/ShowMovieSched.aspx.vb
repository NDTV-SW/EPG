Imports System
Imports System.Data.SqlClient

Public Class ShowMovieSched
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
                Dim strProgid As String = Request.QueryString("progid").ToString
                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL("select moviename from mst_moviesdb where rowid='" & strProgid & "'", False)
                Dim strProgName As String = dt.Rows(0)(0).ToString
                Dim dt1 As DataTable = obj.executeSQL("rpt_GetMovieSchedule", "search", "varchar", strProgName, True, False)

                grdProgSched.DataSource = dt1
                grdProgSched.DataBind()

            Catch ex As Exception
                Logger.LogError("Page Load", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    'Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
    '    hfSend.Value = "2"
    'End Sub
End Class