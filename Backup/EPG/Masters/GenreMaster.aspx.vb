Imports System
Imports System.Data.SqlClient
Public Class GenreMaster
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString


    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title: 'Error Occured',content: '" & errorstr.Trim & "',opacity:0.8});", True)
    End Sub
    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "$.msgBox({title:'Message',content:'" & messagestr.Trim & "',type:'info',opacity:0.8});", True)
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                getMaxgenre()
            End If
        Catch ex As Exception
            Logger.LogError("Genre Master", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub grdGenreMaster_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdGenreMaster.RowCommand
        Try
            Dim lbpriority As Label = TryCast(grdGenreMaster.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lbpriority"), Label)
            If e.CommandName.ToLower = "up" Then
                moveUp(lbpriority.Text, "mst_genre", RBCategory.SelectedValue)
            End If
            If e.CommandName.ToLower = "down" Then
                moveDown(lbpriority.Text, "mst_genre", RBCategory.SelectedValue)
            End If
        Catch ex As Exception
            Logger.LogError("Genre Master", "grdGenreMaster_RowCommand", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub btnAddGenre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddGenre.Click
        Try
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            If MyConnection.State = ConnectionState.Closed Then
                MyConnection.Open()
            End If
            Dim cmd As New SqlCommand("select max(IsNull(CompanyID,0)) + 1 from mst_company", MyConnection)
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            If dr.HasRows() Then
                dr.Read()
                If dr(0).ToString = txtGenreID.Text.Trim Then
                    ' myErrorBox("Genre Id Already Exists !")
                    Exit Sub
                End If

            End If
            dr.Close()

            If btnAddGenre.Text = "Add" Then
                Call exec_Proc(0, txtGenreID.Text, txtGenreName.Text.ToString.Trim, "A", RBCategory.SelectedValue)
            ElseIf btnAddGenre.Text = "Update" Then
                Call exec_Proc(txtHiddenId.Text.ToString.Trim, txtGenreID.Text, txtGenreName.Text.ToString.Trim, "U", RBCategory.SelectedValue)
                grdGenreMaster.SelectedIndex = -1
            End If
            grdGenreMaster.DataBind()

            txtHiddenId.Text = "0"
            txtGenreName.Text = ""
            btnAddGenre.Text = "Add"
            txtGenreID.Text = ""
        Catch ex As Exception
            Logger.LogError("Genre Master", btnAddGenre.Text & " btnAddGenre_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Function GetData(ByVal StrSql As String) As DataSet
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        Dim dbCommand As New SqlCommand
        dbCommand.CommandText = StrSql.ToString
        dbCommand.Connection = MyConnection
        Dim dataAdapter As New SqlDataAdapter
        dataAdapter.SelectCommand = dbCommand
        Dim ds As DataSet
        ds = New DataSet
        dataAdapter.Fill(ds)
        Return ds
        dataAdapter.Dispose()
        MyConnection.Dispose()
    End Function

    Private Sub exec_Proc(ByVal RowId As String, ByVal GenreId As String, ByVal GenreName As String, ByVal action As String, ByVal genrecategory As String)
        Try
            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_Genre", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowId", SqlDbType.Int, 8)).Value = RowId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreID", SqlDbType.Int, 8)).Value = GenreId.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreName", SqlDbType.NVarChar, 200)).Value = GenreName.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = action.ToString.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreCategory", SqlDbType.Char, 1)).Value = genrecategory.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetGenreMaster")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            myErrorBox("GenreId Already Exists")
            Logger.LogError("Genre Master", action & " exec_Proc", ex.Message.ToString, User.Identity.Name)
        End Try

    End Sub

    Private Sub grdGenreMaster_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdGenreMaster.RowDeleted
        If Not e.Exception Is Nothing Then
            ' myErrorBox("You can not delete this record.")
            e.ExceptionHandled = True
        End If
    End Sub

    Private Sub grdGenreMaster_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGenreMaster.SelectedIndexChanged
        Try
            Dim lbRowId As Label = DirectCast(grdGenreMaster.SelectedRow.FindControl("lbRowId"), Label)
            Dim lbGenreId As Label = DirectCast(grdGenreMaster.SelectedRow.FindControl("lbGenreId"), Label)
            Dim lbGenreName As Label = DirectCast(grdGenreMaster.SelectedRow.FindControl("lbGenreName"), Label)
            Dim lbGenreCategory As Label = DirectCast(grdGenreMaster.SelectedRow.FindControl("lbGenreCategory"), Label)

            txtHiddenId.Text = lbRowId.Text.Trim
            txtGenreID.Text = lbGenreId.Text.Trim
            txtGenreName.Text = Server.HtmlDecode(lbGenreName.Text.Trim)
            RBCategory.SelectedValue = lbGenreCategory.Text.Trim
            btnAddGenre.Text = "Update"
        Catch ex As Exception
            Logger.LogError("Genre Master", "grdGenreMaster_SelectedIndexChanged", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            grdGenreMaster.SelectedIndex = -1
            txtHiddenId.Text = "0"
            txtGenreName.Text = ""
            btnAddGenre.Text = "Add"
            txtGenreID.Text = ""
        Catch ex As Exception
            Logger.LogError("Genre Master", "btnCancel_Click", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
    Private Sub grdGenreMaster_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdGenreMaster.RowDeleting
        Try
            Dim lbRowId As Label = DirectCast(grdGenreMaster.Rows(e.RowIndex).FindControl("lbRowId"), Label)
            Dim lbGenreId As Label = DirectCast(grdGenreMaster.Rows(e.RowIndex).FindControl("lbGenreId"), Label)
            Dim lbGenreName As Label = DirectCast(grdGenreMaster.Rows(e.RowIndex).FindControl("lbGenreName"), Label)
            Dim lbGenreCategory As Label = DirectCast(grdGenreMaster.Rows(e.RowIndex).FindControl("lbGenreCategory"), Label)

            Dim DS As DataSet
            Dim MyDataAdapter As SqlDataAdapter
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            MyDataAdapter = New SqlDataAdapter("sp_mst_genre", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@RowId", SqlDbType.Int)).Value = lbRowId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreId", SqlDbType.Int)).Value = lbGenreId.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreName", SqlDbType.VarChar, 50)).Value = lbGenreName.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GenreCategory", SqlDbType.Char, 1)).Value = lbGenreCategory.Text.Trim
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Action", SqlDbType.Char, 1)).Value = "D"
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ActionUser", SqlDbType.VarChar, 50)).Value = User.Identity.Name.ToString.Trim
            DS = New DataSet()
            MyDataAdapter.Fill(DS, "GetLang")
            MyDataAdapter.Dispose()
            MyConnection.Close()
        Catch ex As Exception
            Logger.LogError("Genre Master", "grdGenreMaster_RowDeleting", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Protected Sub RBCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RBCategory.SelectedIndexChanged
        getMaxgenre()
    End Sub
    Private Sub getMaxgenre()
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select max(GenreId) maxgenreid, max(GenreId)+1 nextgenreid from mst_genre where genreCategory='" & RBCategory.SelectedValue & "'", False)
        If dt.Rows.Count > 0 Then
            txtGenreID.Text = dt.Rows(0)("nextGenreId").ToString()
        End If
    End Sub
    Dim intSno As Integer
    Protected Sub grdGenreMaster_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGenreMaster.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            intSno = 1
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbSno As Label = TryCast(e.Row.FindControl("lbSno"), Label)
            lbSno.Text = intSno
            intSno = intSno + 1
            Dim lbGenreId As Label = TryCast(e.Row.FindControl("lbGenreId"), Label)
            Dim lbName As Label = TryCast(e.Row.FindControl("lbName"), Label)
            Dim lbFileName As Label = TryCast(e.Row.FindControl("lbFileName"), Label)
            Dim objImage As New clsUploadModules
            'lbFileName.Text = "abc.jpg"
            lbFileName.Text = objImage.sanitizeImageFile(lbFileName.Text)


            Dim hyUpload As HyperLink = TryCast(e.Row.FindControl("hyUpload"), HyperLink)
            hyUpload.NavigateUrl = "javascript:showDiv('MainContent_grdGenreMaster_lbGenreLogo_" & e.Row.RowIndex & "','MainContent_grdGenreMaster_hyLogo_" & e.Row.RowIndex & "','MainContent_grdGenreMaster_imgGenrePic_" & e.Row.RowIndex & "','" & lbGenreId.Text & "','" & lbFileName.Text & "')"
        End If
    End Sub

    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)

        Dim fileName As String = jsFileName.Value
        Dim GenreId As String = jsGenreId.Value

        If AsyncFileUpload1.FileName.ToLower.EndsWith(".jpg") Or AsyncFileUpload1.FileName.ToLower.EndsWith(".jpeg") Or AsyncFileUpload1.FileName.ToLower.EndsWith(".png") Then
            If (AsyncFileUpload1.HasFile) Then
                Dim strpath As String = MapPath("~/uploads/genre/") & fileName
                AsyncFileUpload1.SaveAs(strpath)
                System.Threading.Thread.Sleep(1000)
                Dim abc As New clsFTP
                abc.doS3Task(strpath, "/uploads/genre")
                System.Threading.Thread.Sleep(1000)
            End If
            Dim MyConnection As New SqlConnection
            MyConnection = New SqlConnection(ConString)
            Dim strUrl As String = "http://epgops.ndtv.com/uploads/genre/" & fileName
            Dim obj As New clsExecute
            obj.executeSQL("update mst_genre set genrepic='" & strUrl & "' where genreid='" & GenreId & "'", False)
        Else
            Throw New Exception("only .jpg and .jpeg and .png files supported")
        End If

    End Sub

    Protected Sub moveUp(ByRef id As Integer, ByVal tableName As String, ByVal category As String)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("SELECT priority FROM " & tableName & " where priority<" & id & " and genreCategory='" & category & "' ORDER BY priority desc", False)

        If dt.Rows.Count > 0 Then
            Dim tmpID As Integer = dt.Rows.Item(0).Item(0)
            obj.executeSQL("UPDATE " & tableName & " SET priority=-1  WHERE priority=" & ID, False)
            obj.executeSQL("UPDATE " & tableName & " SET priority=" & ID & "  WHERE priority=" & tmpID, False)
            obj.executeSQL("UPDATE " & tableName & " SET priority=" & tmpID & " WHERE priority=-1", False)
            grdGenreMaster.DataBind()
        End If
    End Sub

    Protected Sub moveDown(ByRef id As Integer, ByVal tableName As String, ByVal category As String)
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("SELECT priority FROM " & tableName & " where priority>" & id & " and genreCategory='" & category & "' ORDER BY priority", False)

        If dt.Rows.Count > 0 Then
            Dim tmpID As Integer = dt.Rows.Item(0).Item(0)
            obj.executeSQL("UPDATE " & tableName & " SET priority=-1  WHERE priority=" & id, False)
            obj.executeSQL("UPDATE " & tableName & " SET priority=" & id & "  WHERE priority=" & tmpID, False)
            obj.executeSQL("UPDATE " & tableName & " SET priority=" & tmpID & " WHERE priority=-1", False)
            grdGenreMaster.DataBind()
        End If


    End Sub
End Class