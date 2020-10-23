Imports System.Web
Imports System.Web.Services
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class update
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "text/plain"
        context.Response.Write("Hello World!")


        Dim sID As String = context.Request.QueryString("id")
        Dim sTitle As String = context.Request.QueryString("title")
        Dim sPublished As String = context.Request.QueryString("published")
        Dim sCreatedBy As String = context.Request.QueryString("createdby")
        Dim sCategory As String = context.Request.QueryString("category")

        Dim sConnectString As String = ConfigurationManager.ConnectionStrings("JqueryConnectionString").ConnectionString

        Try
            Dim oConn As New SqlConnection(sConnectString)
            oConn.Open()
            Dim oCommand As New SqlCommand(String.Format("UPDATE POSTS SET title='{0}', published='{1}', [created by]='{2}', category='{3}' WHERE ID = '{4}'", sTitle, sPublished, sCreatedBy, sCategory, sID), oConn)
            'oCommand.ExecuteNonQuery()
            oCommand.Dispose()
            oConn.Close()
        Catch ex As Exception
            context.Response.Write("!!Error " + ex.Message)
        End Try
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class