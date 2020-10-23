Public Class verifyrichchannels
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim li As HtmlGenericControl = TryCast(Master.FindControl("liverifyrichchannels"), HtmlGenericControl)
            li.Attributes.Add("class", "active")
        End If
    End Sub

   
 
    Private Sub clearall()
        
        grd1.SelectedIndex = -1
        grd1.DataBind()


    End Sub

    
    Private Sub grd1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd1.SelectedIndexChanged
        Try
            Dim intId As Integer = grd1.DataKeys(grd1.SelectedIndex).Value
         
            'btnAdd.Text = "Update"

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grd1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grd1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim item As String = e.Row.Cells(5).Text
            For Each button As Button In e.Row.Cells(0).Controls.OfType(Of Button)()
                If button.CommandName = "Delete" Then
                    button.Attributes("onclick") = "if(!confirm('Do you want to delete """ + item + """ ?')){ return false; };"
                End If
            Next
            'Dim img As Image = TryCast(e.Row.FindControl("img"), Image)
            'img.ImageUrl = ""
            Dim hyRichMetaId As HyperLink = DirectCast(e.Row.FindControl("hyRichMetaId"), HyperLink)
            Dim chkVerified As CheckBox = DirectCast(e.Row.FindControl("chkVerified"), CheckBox)
            If chkVerified.Checked Then
                hyRichMetaId.BackColor = Drawing.Color.LawnGreen
            End If

            If hyRichMetaId.Text <> "0" Then
                hyRichMetaId.NavigateUrl = "javascript:openWin('" & hyRichMetaId.Text & "')"
                hyRichMetaId.Text = "View Rich"
            End If
        End If
    End Sub

End Class