
Public Class LabelPrint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadLabelFormats()
        End If
    End Sub

    ''' <summary>
    ''' Populate the label formats into the drop down list
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLabelFormats()

        With ddlLabelFormats
            .DataSource = clsLabelFormatBLL.GetLabelFormats
            .DataValueField = "Id"
            .DataTextField = "Name"
            .DataBind()
        End With
        hyPrint.NavigateUrl = "LabelHandler.ashx?Id=1"
    End Sub

    ''' <summary>
    ''' Print the label
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
   

    Protected Sub ddlLabelFormats_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLabelFormats.SelectedIndexChanged
        Dim li As ListItem = CType(ddlLabelFormats.SelectedItem, ListItem)

        If Not IsNothing(li) Then
            hyPrint.NavigateUrl = "LabelHandler.ashx?Id=" & li.Value.ToString
        End If
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged

        Dim intID As Integer = grd.SelectedDataKey.Values(0)
        lbID.Text = intID
        Dim obj As New clsExecute
        Dim dt As DataTable = obj.executeSQL("select * from mst_operatorAddress where id='" & intID & "'", False)
        'id,name,company,addressline1,city,statename,district,pincode,contact,active
        If dt.Rows.Count > 0 Then
            txtName.Text = dt.Rows(0)("name").ToString
            txtCompany.Text = dt.Rows(0)("company").ToString
            txtAddressLine1.Text = dt.Rows(0)("addressline1").ToString
            txtAddressLine2.Text = dt.Rows(0)("addressline2").ToString
            txtCity.Text = dt.Rows(0)("city").ToString
            txtState.Text = dt.Rows(0)("statename").ToString
            txtDistrict.Text = dt.Rows(0)("district").ToString
            txtPinCode.Text = dt.Rows(0)("pincode").ToString
            txtContact.Text = dt.Rows(0)("contact").ToString
            chkActive.Checked = dt.Rows(0)("active").ToString
            

            btnAdd.Text = "Update"
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim obj As New clsExecute

        Dim strsql As String = ""
        If btnAdd.Text.ToUpper = "ADD" Then
            strsql = "insert into mst_operatorAddress(name,company,addressline1,addressline2,city,statename,district,pincode,contact,active) values" & _
                "('" & txtName.Text & "','" & txtCompany.Text & "','" & txtAddressLine1.Text & "','" & txtAddressLine2.Text & "','" & txtCity.Text & "','" & txtState.Text & "','" & _
                txtDistrict.Text & "','" & txtPinCode.Text & "','" & txtContact.Text & "','" & chkActive.Checked & "')"
        Else
            strsql = "update mst_operatorAddress set name='" & txtName.Text & "',company='" & txtCompany.Text & "',addressline1='" & txtAddressLine1.Text & "',addressline2='" & txtAddressLine2.Text & "',city='" & _
                txtCity.Text & "',statename='" & txtState.Text & "',district='" & txtDistrict.Text & "',pincode='" & txtPinCode.Text & "',contact='" & txtContact.Text & _
                "',active='" & chkActive.Checked & "' where id='" & lbID.Text & "'"
        End If
        obj.executeSQL(strSQL, False)
        ClearAll()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearAll()
    End Sub
    Private Sub ClearAll()
        txtName.Text = ""
        txtCompany.Text = ""
        txtAddressLine1.Text = ""
        txtAddressLine2.Text = ""
        txtCity.Text = ""
        txtState.Text = ""
        txtDistrict.Text = ""
        txtPinCode.Text = ""
        txtContact.Text = ""
        chkActive.Checked = True
        lbID.Text = ""
        grd.SelectedIndex = -1
        grd.DataBind()
        btnAdd.Text = "Add"
    End Sub
End Class