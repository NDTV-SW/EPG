
Public Class ProcessTask
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim gridIndex As String = Request.QueryString("gridIndex")
        Dim progname As String = HttpUtility.UrlDecode(Request.QueryString("progname")).Replace("&amp;", "&")
        Dim channelid As String = Request.QueryString("channelid")
        Dim episode As String = Request.QueryString("episode")
        Dim rating As String = Request.QueryString("rating")
        Dim genre As String = Request.QueryString("genre")
        Dim movie As String = Request.QueryString("movie")
        Dim status As String = Request.QueryString("status")
        Dim series As String = Request.QueryString("series")
        Response.Write(New String("*"c, 256))
        Response.Flush()
        Try
            Dim obj As New clsUploadModules
            Dim boolInserted As Boolean = obj.Insert_mstProg(channelid, progname, genre, rating, series, 1, IIf(episode = "", 0, episode), status, movie, "Process Task", User.Identity.Name, False)

            If boolInserted Then
                UpdateProgress("Updating", "MainContent_grdData_hyAddNew_" & gridIndex, 1)
                System.Threading.Thread.Sleep(200)

                UpdateProgress("Updated", "MainContent_grdData_hyAddNew_" & gridIndex, 1)
            Else
                UpdateProgress("Update", "MainContent_grdData_hyAddNew_" & gridIndex, 0)
            End If
        Catch ex As Exception
            UpdateProgress("Update", "MainContent_grdData_hyAddNew_" & gridIndex, 0)
        End Try

    End Sub

    Protected Sub UpdateProgress(ByVal Message As String, ByVal controlid As String, ByVal status As Integer)
        Response.Write(String.Format("<script type=""text/javascript"">parent.UpdateProgress('{0}', '{1}', {2});</script>", Message, controlid, status))
        Response.Flush()
    End Sub
End Class
