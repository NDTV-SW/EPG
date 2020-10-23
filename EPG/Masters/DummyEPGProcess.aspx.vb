Imports System
Imports System.Data.SqlClient
Public Class DummyEPGProcess
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strTillDate As String = Request.QueryString("tilldate")
        Dim strCopyLastWeek As String = Request.QueryString("CopyLastWeek")
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from fn_insertdummyepg('" & strTillDate & "','" & strCopyLastWeek & "') where active=1 order by channelid", False)

        If dt.Rows.Count > 0 Then

            Dim vChannelid As String, vProgName As String, vSynopsis As String, vGenreId As String, vGenrename As String, vStartDate As DateTime, vDays As Integer, vDuration As Integer
            Dim vTempStartDate As DateTime

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim sql As String = ""
                vChannelid = dt.Rows(i)("channelid").ToString
                vProgName = dt.Rows(i)("progname").ToString
                vSynopsis = dt.Rows(i)("progsynopsys").ToString
                vGenreId = dt.Rows(i)("genreid").ToString
                vGenrename = dt.Rows(i)("genrename").ToString
                vStartDate = dt.Rows(i)("date").ToString
                vDuration = dt.Rows(i)("progduration").ToString
                vDays = dt.Rows(i)("noofdays").ToString

                If strCopyLastWeek = "false" Then


                    'For j As Integer = 1 To vDays
                    vTempStartDate = vStartDate.AddDays(1)
                    Dim vdumDate As Date = DateTime.Now.Date

                    While vTempStartDate.Date <= vStartDate.AddDays(vDays).Date
                        sql = sql & "insert into map_epgexcel (channelID, [Program Name],Genre,Date,Time,Duration, Description, [Episode No],[Show-wise Description]) values('" & vChannelid & "','" & vProgName.ToString.Replace("'", "''") & "','" & vGenrename & "','" & vTempStartDate.Date & "','" & vTempStartDate.ToShortTimeString & "','" & vDuration & "','" & vSynopsis.Replace("'", "''") & "','0','');"
                        If vdumDate <> vTempStartDate.Date Then
                            obj.executeSQL("insert into mst_epg_tentative (channelid,epgdate) values('" & vChannelid & "','" & vTempStartDate.Date & "')", False)
                            vdumDate = vTempStartDate.Date
                        End If
                        vTempStartDate = vTempStartDate.AddMinutes(vDuration)
                    End While


                    Dim obj1 As New clsUploadModules
                    obj1.DeleteEPGData(vChannelid)

                    obj.executeSQL(sql, False)
                    process(vChannelid, vChannelid, vGenreId)


                    'UpdateProgress(vChannelid & ", (" & i + 1 & " of " & dt.Rows.Count & ")", "lbStatus", 1)
                    UpdateProgress("(" & i + 1 & " of " & dt.Rows.Count & "), " & vChannelid, "lbStatus", 1)
                Else
                    Dim dtNew As DataTable
                    dtNew = obj.executeSQL("sp_mst_epg_copy_epg", "channelid~startdate~checkdays~ignore", "VarChar~DateTime~Int~bit", vChannelid & "~" & vStartDate.AddDays(1).Date & "~7~false", True, True)
                    If dtNew.Rows(0)(0).ToString.ToUpper = "ERROR" Then
                        UpdateProgress("ERROR : " & vChannelid, "lbStatus", 1)
                        System.Threading.Thread.Sleep(2000)
                    Else
                        UpdateProgress("(" & i + 1 & " of " & dt.Rows.Count & "), " & vChannelid, "lbStatus", 1)
                    End If
                End If

            Next
        End If


    End Sub
    Protected Function process(ByVal vChannelID As String, ByVal vServiceID As String, ByVal vGenreID As String) As Boolean


        Try
            Dim obj1 As New clsExecute()
            Dim dt1 As DataTable = obj1.executeSQL("sc_sp_epg_validate_progname", "channelid", "varchar", vChannelID, True, False)
            Dim obj As New clsUploadModules
            If dt1.Rows.Count > 0 Then
                Dim boolInserted As Boolean = obj.Insert_mstProg(vChannelID, dt1.Rows(0)("ExcelProgname"), vGenreID, "UA", 0, 1, 0, "", 0, "Dummy Process Task", User.Identity.Name, False)
                If boolInserted Then
                    Dim boolBuildEPGSuccess As Boolean = obj.BuildEPG(vChannelID, True, False, User.Identity.Name)
                    If boolBuildEPGSuccess Then
                        Return True
                    End If

                Else
                    Return False
                End If
            Else
                Dim boolBuildEPGSuccess As Boolean = obj.BuildEPG(vChannelID, True, True, User.Identity.Name)
                If boolBuildEPGSuccess Then
                    Return True
                End If
            End If

        Catch ex As Exception
            Logger.LogError("Dummy_EPG_process", "process" & vChannelID, ex.Message.ToString, User.Identity.Name)
            Return False
        End Try
    End Function
    Protected Sub UpdateProgress(ByVal Message As String, ByVal controlid As String, ByVal status As String)
        Response.Write(String.Format("<script type=""text/javascript"">parent.UpdateProgress('{0}', '{1}', {2});</script>", Message, controlid, status))
        Response.Flush()
    End Sub
End Class
