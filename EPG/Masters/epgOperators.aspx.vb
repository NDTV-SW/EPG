Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class epgOperators
    Inherits System.Web.UI.Page
    Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (User.IsInRole("ADMIN") Or User.IsInRole("SUPERUSER")) Then
            Response.Redirect("~/Default.aspx")
        End If
    End Sub
   
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim strDaysToSend As String = ""
            Dim i As Integer = 0
            While i < chkWeekList.Items.Count
                If chkWeekList.Items(i).Selected Then
                    strDaysToSend = strDaysToSend & chkWeekList.Items(i).Value & ","
                End If
                i = i + 1
            End While
            strDaysToSend = strDaysToSend.Substring(0, strDaysToSend.Length - 1)
            Dim sqlString As String
            If btnSave.Text = "Save" Then
                sqlString = "INSERT INTO mst_operators(NAME,CITY,SYSTEMNAME,EPGTYPE,FOLDERNAME,FILENAMEFORMAT,RECIPIENTLIST,CCLIST,BCCLIST,MULTIPLEDAYS,FUNCTIONNAME,OPERATORTYPE,TIMETOSEND,TIMETOSEND2,TIMETOSEND3,TIMETOSEND4,ACTIVE,MAPPINGTABLE,SELECTQUERY,FUNCTIONTOEXECUTE,[PRIORITY],HOURLYUPDATE,DAYSTOSEND)"
                sqlString = sqlString & " VALUES('" & txtName.Text.Trim & "','" & txtCity.Text.Trim & "','" & txtSystemName.Text.Trim & "','" & txtEPGType.Text.Trim & "','" & txtFolder.Text.Trim & "',"
                sqlString = sqlString & "'" & txtFileFormat.Text.Trim & "','" & txtRecepientList.Text.Trim & "','" & txtCCList.Text.Trim & "','" & txtBCCList.Text.Trim & "',"
                sqlString = sqlString & "'" & chkMultiple.Checked & "','" & txtFunctionName.Text.Trim & "','" & txtOperatorType.Text.Trim & "','" & txtTimetoSend.Text.Trim & "','" & txtTimetoSend2.Text.Trim & "','" & txtTimetoSend3.Text.Trim & "','" & txtTimetoSend4.Text.Trim & "',"
                sqlString = sqlString & "'" & chkActive.Checked & "','" & txtMappingTable.Text.Trim & "','" & txtSelectQuery.Text.Trim & "','" & txtFunctionToExecute.Text.Trim & "',"
                sqlString = sqlString & "'" & txtPriority.Text.Trim & "','" & chkHourlyUpdates.Checked & "','" & strDaysToSend.Trim & "')"
            Else
                sqlString = "UPDATE mst_operators SET NAME='" & txtName.Text.Trim & "',CITY='" & txtCity.Text.Trim & "',SYSTEMNAME='" & txtSystemName.Text.Trim & "',EPGTYPE='" & txtEPGType.Text.Trim & "',"
                sqlString = sqlString & "FOLDERNAME='" & txtFolder.Text.Trim & "',FILENAMEFORMAT='" & txtFileFormat.Text.Trim & "',RECIPIENTLIST='" & txtRecepientList.Text.Trim & "',CCLIST='" & txtCCList.Text.Trim & "',"
                sqlString = sqlString & "BCCLIST='" & txtBCCList.Text.Trim & "',MULTIPLEDAYS='" & chkMultiple.Checked & "',FUNCTIONNAME='" & txtFunctionName.Text.Trim & "',OPERATORTYPE='" & txtOperatorType.Text.Trim & "',"
                sqlString = sqlString & "TIMETOSEND='" & txtTimetoSend.Text.Trim & "',TIMETOSEND2='" & txtTimetoSend2.Text.Trim & "',TIMETOSEND3='" & txtTimetoSend3.Text.Trim & "',TIMETOSEND4='" & txtTimetoSend4.Text.Trim & "',ACTIVE='" & chkActive.Checked & "',MAPPINGTABLE='" & txtMappingTable.Text.Trim & "',SELECTQUERY='" & txtSelectQuery.Text.Trim & "',"
                sqlString = sqlString & "FUNCTIONTOEXECUTE='" & txtFunctionToExecute.Text.Trim & "',[PRIORITY]='" & txtPriority.Text.Trim & "',HOURLYUPDATE='" & chkHourlyUpdates.Checked & "',DAYSTOSEND='" & strDaysToSend.Trim & "' WHERE OPERATORID='" & lbOperatorID.Text & "'"
            End If

            Dim adp As New SqlDataAdapter(sqlString, MyConnection)
            adp.SelectCommand.CommandType = CommandType.Text
            Dim ds As New DataSet
            adp.Fill(ds, "OperatorMaster")
            MyConnection.Close()
            MyConnection.Dispose()
            clearAll()
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                Dim lbOperatorID As Label = DirectCast(e.Row.FindControl("lbOperatorID"), Label)
                'Dim lbMappingTable As Label = DirectCast(e.Row.FindControl("lbMappingTable"), Label)
                Dim hyView As HyperLink = DirectCast(e.Row.FindControl("hyView"), HyperLink)
                hyView.NavigateUrl = "Operatorchannels.aspx?operatorid=" & lbOperatorID.Text ' & "&opTable=" & lbMappingTable.Text & ""
            Catch
            End Try
        End If

    End Sub
    Private Sub clearAll()
        txtName.Text = ""
        txtCity.Text = ""
        txtSystemName.Text = ""
        txtEPGType.Text = ""
        txtFolder.Text = ""
        txtFileFormat.Text = ""
        txtRecepientList.Text = ""
        txtCCList.Text = ""
        txtBCCList.Text = ""
        chkMultiple.Checked = False
        txtFunctionName.Text = ""
        txtOperatorType.Text = ""
        txtTimetoSend.Text = ""
        txtTimetoSend2.Text = ""
        txtTimetoSend3.Text = ""
        txtTimetoSend4.Text = ""
        chkActive.Checked = False
        txtMappingTable.Text = ""
        txtSelectQuery.Text = ""
        txtFunctionToExecute.Text = ""
        txtPriority.Text = ""
        chkHourlyUpdates.Checked = False
        btnSave.Text = "Save"

        Dim i As Integer = 0
        While i < chkWeekList.Items.Count
            chkWeekList.Items(i).Selected = False
            i = i + 1
        End While

        GridView1.SelectedIndex = -1
        GridView1.DataBind()

    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim i As Integer = 0
        While i < chkWeekList.Items.Count
            chkWeekList.Items(i).Selected = False
            i = i + 1
        End While

        lbOperatorID.Text = DirectCast(GridView1.SelectedRow.FindControl("lbOperatorID"), Label).Text
        Dim adp As New SqlDataAdapter("SELECT * FROM mst_operators WHERE OPERATORID='" & lbOperatorID.Text & "'", MyConnection)
        adp.SelectCommand.CommandType = CommandType.Text
        Dim dt As New DataSet
        adp.Fill(dt)
        txtName.Text = dt.Tables(0).Rows(0)(1).ToString
        txtCity.Text = dt.Tables(0).Rows(0)(2).ToString
        txtSystemName.Text = dt.Tables(0).Rows(0)(3).ToString
        txtEPGType.Text = dt.Tables(0).Rows(0)(4).ToString
        txtFolder.Text = dt.Tables(0).Rows(0)(5).ToString
        txtFileFormat.Text = dt.Tables(0).Rows(0)(6).ToString
        txtRecepientList.Text = dt.Tables(0).Rows(0)(7).ToString
        txtCCList.Text = dt.Tables(0).Rows(0)(8).ToString
        txtBCCList.Text = dt.Tables(0).Rows(0)(9).ToString
        chkMultiple.Checked = dt.Tables(0).Rows(0)(10).ToString
        txtFunctionName.Text = dt.Tables(0).Rows(0)(11).ToString
        txtOperatorType.Text = dt.Tables(0).Rows(0)(12).ToString
        txtTimetoSend.Text = Convert.ToDateTime(dt.Tables(0).Rows(0)(13).ToString).ToString("HH:mm")
        txtTimetoSend2.Text = Convert.ToDateTime(dt.Tables(0).Rows(0)(19).ToString).ToString("HH:mm")
        txtTimetoSend3.Text = Convert.ToDateTime(dt.Tables(0).Rows(0)(23).ToString).ToString("HH:mm")
        txtTimetoSend4.Text = Convert.ToDateTime(dt.Tables(0).Rows(0)(24).ToString).ToString("HH:mm")
        chkActive.Checked = dt.Tables(0).Rows(0)(14).ToString
        txtMappingTable.Text = dt.Tables(0).Rows(0)(15).ToString
        txtSelectQuery.Text = dt.Tables(0).Rows(0)(16).ToString
        txtFunctionToExecute.Text = dt.Tables(0).Rows(0)(17).ToString
        chkHourlyUpdates.Checked = dt.Tables(0).Rows(0)(18).ToString
        txtPriority.Text = dt.Tables(0).Rows(0)(20).ToString
        Dim strDaysToSend As String = dt.Tables(0).Rows(0)(21).ToString

        Dim ArStrDaysToSend As Array = strDaysToSend.Split(",")
        For Each valDaysToSend In ArStrDaysToSend
            chkWeekList.Items(valDaysToSend - 1).Selected = True
        Next

        btnSave.Text = "Update"
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Try
            lbOperatorID = DirectCast(GridView1.Rows(e.RowIndex).FindControl("lbOperatorID"), Label)
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyDataAdapter = New SqlDataAdapter("DELETE FROM mst_operators WHERE '" & lbOperatorID.Text & "'", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.Text
            DS = New DataSet()
            'MyDataAdapter.Fill(DS, "RowDelete")
            MyDataAdapter.Dispose()
            MyConnection.Close()
            'txtChannelName.ReadOnly = False
            'btnAddChannel.Text = "Add"
        Catch ex As Exception
            'Logger.LogError("Channel Master", "grdChannelmaster_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub
End Class