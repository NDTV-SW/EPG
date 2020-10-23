Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ChannelOperators
    Inherits System.Web.UI.Page
    'Dim ConString As String = ConfigurationManager.ConnectionStrings("EPGConnectionString1").ToString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            Logger.LogError("ChannelOperators", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub


    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function SearchChannel(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getChannels(prefixText, count)
        Return channels
    End Function

End Class