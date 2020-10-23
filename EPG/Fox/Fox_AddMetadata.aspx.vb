
Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports System.IO
Imports Excel
Imports System.Data.SqlClient
Imports AjaxControlToolkit
Imports Foxplus


Public Class Fox_AddMetadata
    Inherits System.Web.UI.Page
    Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Dim flag As Integer = 0
    Dim obj1 As New clsUploadModules
    Dim obj As New clsExecute
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Page.IsPostBack = False Then
                ddlShelf.DataBind()
                
            End If

        Catch ex As Exception
            Logger.LogError("TV Star Discrepancy", "Page Load", ex.Message.ToString, User.Identity.Name)

        End Try
    End Sub

    Protected Sub ddlShelf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlShelf.SelectedIndexChanged
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        MyConnection.Open()
        Try

            Dim strSql As String
            strSql = "select Shelf_Title,Shelf_Id from Fox_map_EPGExcel where Shelf_Name='" & ddlShelf.SelectedItem.ToString.Trim & "' order by 2"
            Dim dt As DataTable = obj.executeSQL(strSql, False)
            If dt.Rows.Count > 0 Then
                ddlTitle.DataSource = dt
                ddlTitle.DataBind()
                ddlTitle.DataTextField = "Shelf_Title"
                ddlTitle.DataValueField = "Shelf_Id"
            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    Private Sub BindGrid()
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        MyConnection.Open()
        Try

            Dim dtlist As DataTable = obj.executeSQL("sp_fox_FinalReport", "Shelf_Id~Shelf_Title", "int~varchar", ddlShelf.SelectedValue.ToString.Trim & "~" & ddlTitle.SelectedItem.ToString.Trim, True, False)
            If dtlist.Rows.Count > 0 Then
                grdSeriesData.DataSource = dtlist
                grdSeriesData.DataBind()

            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    Protected Sub ddlTitle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTitle.SelectedIndexChanged
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        MyConnection.Open()
        Try

            Dim strSql As String
            strSql = "select Shelf_SeasonNo,Shelf_Id from Fox_map_EPGExcel where Shelf_Name='" & ddlShelf.SelectedItem.ToString.Trim & "' and Shelf_Title='" & ddlTitle.SelectedItem.ToString.Trim & "'  order by 1"
            Dim dt As DataTable = obj.executeSQL(strSql, False)
            If dt.Rows.Count > 0 Then
                ddlSeasonNo.DataSource = dt
                ddlSeasonNo.DataBind()
                ddlSeasonNo.DataTextField = "Shelf_SeasonNo"
                ddlSeasonNo.DataValueField = "Shelf_Id"
            End If

            If ddlShelf.SelectedValue = 1 Then
                BindGrid()
            ElseIf ddlShelf.SelectedValue = 2 Then
                BindMoviesGrid()
            ElseIf ddlShelf.SelectedValue = 3 Then
                BindLIFEGrid()
            ElseIf ddlShelf.SelectedValue = 4 Then
                BindNATGEOGrid()
            ElseIf ddlShelf.SelectedValue = 5 Then
                BindBABYTVGrid()

            End If




        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    'Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
    '    grdSeriesData.EditIndex = e.NewEditIndex
    '    BindGrid()
    'End Sub

    'Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
    '    'Dim id As Label = TryCast(grdSeriesData.Rows(e.RowIndex).FindControl("lbl_ID"), Label)
    '    'Dim name As TextBox = TryCast(grdSeriesData.Rows(e.RowIndex).FindControl("txt_Name"), TextBox)
    '    'Dim city As TextBox = TryCast(grdSeriesData.Rows(e.RowIndex).FindControl("txt_City"), TextBox)
    '    'con = New SqlConnection(cs)
    '    'con.Open()
    '    'Dim cmd As SqlCommand = New SqlCommand("Update tbl_Employee set Name='" & name.Text & "',City='" + city.Text & "' where ID=" + Convert.ToInt32(id.Text), con)
    '    'cmd.ExecuteNonQuery()
    '    'con.Close()
    '    grdSeriesData.EditIndex = -1
    '    BindGrid()
    'End Sub

    'Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs)
    '    grdSeriesData.EditIndex = -1
    '    BindGrid()
    'End Sub


    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        grdSeriesData.EditIndex = e.NewEditIndex
        Me.BindGrid()
    End Sub
    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = grdSeriesData.Rows(e.RowIndex)
        Dim customerId As Integer = Convert.ToInt32(grdSeriesData.DataKeys(e.RowIndex).Values(0))
        Dim name As String = (TryCast(row.FindControl("txtName"), TextBox)).Text
        Dim country As String = (TryCast(row.FindControl("txtCountry"), TextBox)).Text
        Dim query As String = "UPDATE Customers SET Name=@Name, Country=@Country WHERE CustomerId=@CustomerId"
        Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand(query)
                cmd.Parameters.AddWithValue("@CustomerId", customerId)
                cmd.Parameters.AddWithValue("@Name", name)
                cmd.Parameters.AddWithValue("@Country", country)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        grdSeriesData.EditIndex = -1
        Me.BindGrid()
    End Sub
    Protected Sub OnRowCancelingEdit(sender As Object, e As EventArgs)
        grdSeriesData.EditIndex = -1
        Me.BindGrid()
    End Sub
    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim customerId As Integer = Convert.ToInt32(grdSeriesData.DataKeys(e.RowIndex).Values(0))
        Dim query As String = "DELETE FROM Customers WHERE CustomerId=@CustomerId"
        Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand(query)
                cmd.Parameters.AddWithValue("@CustomerId", customerId)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        Me.BindGrid()
    End Sub

    Private Sub BindMoviesGrid()
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        MyConnection.Open()
        Try

            Dim dtlist As DataTable = obj.executeSQL("sp_fox_FinalReport", "Shelf_Id~Shelf_Title", "int~varchar", ddlShelf.SelectedValue.ToString.Trim & "~" & ddlTitle.SelectedItem.ToString.Trim, True, False)
            If dtlist.Rows.Count > 0 Then
                grdMoviesData.DataSource = dtlist
                grdMoviesData.DataBind()

            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    Private Sub BindLIFEGrid()
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        MyConnection.Open()
        Try

            Dim dtlist As DataTable = obj.executeSQL("sp_fox_FinalReport", "Shelf_Id~Shelf_Title", "int~varchar", ddlShelf.SelectedValue.ToString.Trim & "~" & ddlTitle.SelectedItem.ToString.Trim, True, False)
            If dtlist.Rows.Count > 0 Then
                grdFoxlifeData.DataSource = dtlist
                grdFoxlifeData.DataBind()

            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    Private Sub BindNATGEOGrid()
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        MyConnection.Open()
        Try

            Dim dtlist As DataTable = obj.executeSQL("sp_fox_FinalReport", "Shelf_Id~Shelf_Title", "int~varchar", ddlShelf.SelectedValue.ToString.Trim & "~" & ddlTitle.SelectedItem.ToString.Trim, True, False)
            If dtlist.Rows.Count > 0 Then
                grdNetNeoData.DataSource = dtlist
                grdNetNeoData.DataBind()

            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    Private Sub BindBABYTVGrid()
        Dim MyConnection As New SqlConnection
        MyConnection = New SqlConnection(ConString)
        MyConnection.Open()
        Try

            Dim dtlist As DataTable = obj.executeSQL("sp_fox_FinalReport", "Shelf_Id~Shelf_Title", "int~varchar", ddlShelf.SelectedValue.ToString.Trim & "~" & ddlTitle.SelectedItem.ToString.Trim, True, False)
            If dtlist.Rows.Count > 0 Then
                grdBabyTvData.DataSource = dtlist
                grdBabyTvData.DataBind()

            End If

        Catch ex As Exception
            Logger.LogError("Upload Schedule4WOI", "InsertEPGData", ex.Message.ToString, User.Identity.Name)
            flag = 1
        Finally
            MyConnection.Close()
            MyConnection.Dispose()
        End Try
    End Sub

    Protected Sub OnRowMoviesEditing(sender As Object, e As GridViewEditEventArgs)
        grdSeriesData.EditIndex = e.NewEditIndex
        Me.BindGrid()
    End Sub
    Protected Sub OnRowMoviesUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = grdSeriesData.Rows(e.RowIndex)
        Dim customerId As Integer = Convert.ToInt32(grdSeriesData.DataKeys(e.RowIndex).Values(0))
        Dim name As String = (TryCast(row.FindControl("txtName"), TextBox)).Text
        Dim country As String = (TryCast(row.FindControl("txtCountry"), TextBox)).Text
        Dim query As String = "UPDATE Customers SET Name=@Name, Country=@Country WHERE CustomerId=@CustomerId"
        Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand(query)
                cmd.Parameters.AddWithValue("@CustomerId", customerId)
                cmd.Parameters.AddWithValue("@Name", name)
                cmd.Parameters.AddWithValue("@Country", country)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        grdSeriesData.EditIndex = -1
        Me.BindGrid()
    End Sub
    Protected Sub OnRowMoviesCancelingEdit(sender As Object, e As EventArgs)
        grdSeriesData.EditIndex = -1
        Me.BindGrid()
    End Sub


End Class