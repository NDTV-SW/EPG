Public Class DTHProcessTask
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim rowid As String = Request.QueryString("rowid")
        Dim gridIndex As String = Request.QueryString("gridIndex")
        Dim checked As String = Request.QueryString("checked")
        Dim channelid As String = Request.QueryString("channelid")
        'Padding to circumvent IE's buffer.
        Response.Write(New String("*"c, 256))
        Response.Flush()


        If IsNothing(checked) Then
            Try

                UpdateProgress("Updating", "MainContent_GridView1_hySubmitChannelid_" & gridIndex, 1)
                System.Threading.Thread.Sleep(200)

                Dim obj As New clsExecute
                obj.executeSQL("update dthcable_channelmapping set channelid='" & channelid & "' where rowid=" & rowid, False)

                UpdateProgress("Updated", "MainContent_GridView1_hySubmitChannel_" & gridIndex, 1)
                'System.Threading.Thread.Sleep(500)
                'UpdateProgress("Update", "MainContent_GridView1_hySubmitChannel_" & gridIndex, 2)
            Catch ex As Exception
                UpdateProgress("Update", "MainContent_GridView1_hySubmitChannel_" & gridIndex, 0)
            End Try
        Else
            Try

                UpdateProgress("Updating", "MainContent_GridView1_hySubmit_" & gridIndex, 1)
                System.Threading.Thread.Sleep(200)

                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL("select a.rowid,a.ChannelID,b.onair from dthcable_channelmapping a join mst_channel b on a.channelid=b.channelid where a.rowid=" & rowid, False)
                If dt.Rows.Count > 0 Then
                    If Convert.ToBoolean(dt.Rows(0)("onair")) Then
                        obj.executeSQL("update dthcable_channelmapping set onair=" & checked & " where rowid=" & rowid, False)
                        UpdateProgress("Updated", "MainContent_GridView1_hySubmit_" & gridIndex, 1)
                    Else
                        UpdateProgress("Updated", "MainContent_GridView1_hySubmit_" & gridIndex, 3)
                    End If
                Else
                    UpdateProgress("Updated", "MainContent_GridView1_hySubmit_" & gridIndex, 3)
                End If


            Catch ex As Exception
                UpdateProgress("Update", "MainContent_GridView1_hySubmit_" & gridIndex, 0)
            End Try

        End If

        ''Initialization
        'UpdateProgress(0, "Initializing task.")
        'System.Threading.Thread.Sleep(2000)

        ''Gather data.
        'UpdateProgress(25, "Gathering data.")
        'System.Threading.Thread.Sleep(1200)

        ''Process data.
        'UpdateProgress(40, "Processing data.")
        'System.Threading.Thread.Sleep(4000)

        ''Clean up.
        'UpdateProgress(90, "Cleaning up.")
        'System.Threading.Thread.Sleep(800)

        ' All finished!
        'UpdateProgress(100, "Task completed!")
    End Sub
    'Protected Sub UpdateProgress(ByVal PercentComplete As Integer, ByVal Message As String)
    '    ' Write out the parent script callback.
    '    Response.Write(String.Format("<script type=""text/javascript"">parent.UpdateProgress({0}, '{1}');</script>", PercentComplete, Message))
    '    ' To be sure the response isn't buffered on the server.    
    '    Response.Flush()
    'End Sub
    Protected Sub UpdateProgress(ByVal Message As String, ByVal controlid As String, ByVal status As Integer)
        ' Write out the parent script callback.
        Response.Write(String.Format("<script type=""text/javascript"">parent.UpdateProgress('{0}', '{1}', {2});</script>", Message, controlid, status))
        ' To be sure the response isn't buffered on the server.    
        Response.Flush()
    End Sub
End Class