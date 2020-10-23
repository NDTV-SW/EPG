Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Data.SqlClient

Public Class SiteVisionAsia
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SqlConnection.ClearAllPools()
    End Sub

End Class