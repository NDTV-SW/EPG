Imports System.Data
Imports System.Data.SqlClient

Public Class clsExecute

    Public Sub clsExecute()

    End Sub

    Public Function executeSQL(ByVal strSQL As String, ByVal boolSP As Boolean) As DataTable
        Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
        Dim adp As New SqlDataAdapter(strSQL, MyConnection)
        If boolSP Then
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
        Else
            adp.SelectCommand.CommandType = CommandType.Text
        End If
        adp.SelectCommand.CommandTimeout = 0
        Dim dt As New DataTable
        adp.Fill(dt)
        MyConnection.Dispose()
        adp.Dispose()
        Return (dt)
    End Function


    'Stored procedure with/without any output parameter
    Public Function executeSQL(ByVal strSPName As String, ByVal strParameter As String, ByVal strParameterType As String, _
                                      ByVal strParameterValues As String, ByVal boolIsSP As Boolean, ByVal boolIsOutPut As Boolean) As DataTable
        Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString)
        Dim adp As New SqlDataAdapter(strSPName, MyConnection)
        Dim dt As New DataTable
        If boolIsSP Then
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
        Else
            adp.SelectCommand.CommandType = CommandType.Text
        End If
        adp.SelectCommand.CommandTimeout = 0
        'START: If no parameter are there then do not add parameters
        ' Split Parameters based on ~

        If Not strParameter.Trim = String.Empty Then
            Dim strParameterList As String() = strParameter.Split("~")
            Dim strParameterValuesList As String() = strParameterValues.Split("~")
            Dim strParameterTypeList As String() = strParameterType.Split("~")

            If Not (strParameterList.Count = strParameterTypeList.Count And strParameterList.Count = strParameterValuesList.Count) Then
                Return (dt)
            End If

            ' Use For Each loop over Parameter and Add them
            For i As Integer = 0 To strParameterList.Count - 1
                If strParameterTypeList(i).ToLower = "varchar" Then
                    adp.SelectCommand.Parameters.Add("@" & strParameterList(i).ToString & "", SqlDbType.VarChar).Value = strParameterValuesList(i)
                ElseIf strParameterTypeList(i).ToLower = "char" Then
                    adp.SelectCommand.Parameters.Add("@" & strParameterList(i).ToString & "", SqlDbType.Char).Value = strParameterValuesList(i)
                ElseIf strParameterTypeList(i).ToLower = "nvarchar" Then
                    adp.SelectCommand.Parameters.Add("@" & strParameterList(i).ToString & "", SqlDbType.NVarChar).Value = strParameterValuesList(i)
                ElseIf strParameterTypeList(i).ToLower = "int" Then
                    adp.SelectCommand.Parameters.Add("@" & strParameterList(i).ToString & "", SqlDbType.Int).Value = Convert.ToInt32(IIf(strParameterValuesList(i) = "", "0", strParameterValuesList(i)))
                ElseIf strParameterTypeList(i).ToLower = "float" Then
                    adp.SelectCommand.Parameters.Add("@" & strParameterList(i).ToString & "", SqlDbType.Float).Value = Convert.ToDouble(IIf(strParameterValuesList(i) = "", "0", strParameterValuesList(i)))
                ElseIf strParameterTypeList(i).ToLower = "datetime" Then
                    adp.SelectCommand.Parameters.Add("@" & strParameterList(i).ToString & "", SqlDbType.DateTime).Value = IIf(strParameterValuesList(i) = "", Nothing, Convert.ToDateTime(strParameterValuesList(i)))
                ElseIf strParameterTypeList(i).ToLower = "bit" Then
                    adp.SelectCommand.Parameters.Add("@" & strParameterList(i).ToString & "", SqlDbType.Bit).Value = Convert.ToBoolean(strParameterValuesList(i))
                End If
            Next
        End If

        Dim strResult As String
        If boolIsOutPut Then
            adp.SelectCommand.Parameters.Add("@result", SqlDbType.NVarChar, 65000).Direction = ParameterDirection.Output
        End If

        adp.Fill(dt)

        If boolIsOutPut Then
            strResult = adp.SelectCommand.Parameters("@Result").Value.ToString
            dt.Columns.Add("data")
            Dim row As DataRow = dt.NewRow()
            row("data") = strResult
            dt.Rows.Add(row)
        End If
        adp.Dispose()
        MyConnection.Dispose()
        Return (dt)
    End Function

End Class
