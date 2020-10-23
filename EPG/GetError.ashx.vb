Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class GetError
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        Dim sQuery As String = "select top 500 ErrorPage,ErrorSource,ErrorType,ErrorMessage,LoggedinUser,ErrorDateTime,convert (varchar,ErrorDateTime,100) ErrorDateTime1 from app_error_logs order by ErrorDateTime desc"
        Dim oCommand As New SqlCommand(sQuery)
        Dim sData As String = getDiscrepancyData(oCommand)
        context.Response.Write(sData)
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Private Function getDiscrepancyData(oCommand As SqlCommand) As String
        Dim sConnectString As String = ConfigurationManager.ConnectionStrings("JqueryConnectionString").ConnectionString
        Try
            Dim oConn As New SqlConnection(sConnectString)
            oConn.Open()
            oCommand.Connection = oConn
            Dim oDR As SqlDataReader = oCommand.ExecuteReader
            Dim oArr As New ArrayList
            Do While oDR.Read
                Dim oItem As New clsError
                oItem.ErrorPage = oDR("ErrorPage")
                oItem.ErrorSource = oDR("ErrorSource")
                oItem.LoggedinUser = oDR("LoggedinUser").ToString
                oItem.ErrorDateTime1 = oDR("ErrorDateTime1").ToString
                oItem.ErrorMessage = oDR("ErrorMessage").ToString.Trim
                oArr.Add(oItem)
            Loop
            oDR.Close()
            oCommand.Dispose()
            oConn.Close()
            Dim oJSON As New JavaScriptSerializer
            Dim sOutput As String = oJSON.Serialize(oArr)
            Return sOutput
        Catch ex As Exception
            Return "!!Error " + ex.Message
        End Try
    End Function
End Class

Public Class clsError
    Private _ErrorPage As String
    Public Property ErrorPage() As String
        Get
            Return _ErrorPage
        End Get
        Set(ByVal value As String)
            _ErrorPage = value
        End Set
    End Property
    Private _ErrorSource As String
    Public Property ErrorSource() As String
        Get
            Return _ErrorSource
        End Get
        Set(ByVal value As String)
            _ErrorSource = value
        End Set
    End Property
    Private _LoggedinUser As String
    Public Property LoggedinUser() As String
        Get
            Return _LoggedinUser
        End Get
        Set(ByVal value As String)
            _LoggedinUser = value
        End Set
    End Property
    Private _ErrorDateTime1 As String
    Public Property ErrorDateTime1() As String
        Get
            Return _ErrorDateTime1
        End Get
        Set(ByVal value As String)
            _ErrorDateTime1 = value
        End Set
    End Property
    Private _ErrorMessage As String
    Public Property ErrorMessage() As String
        Get
            Return _ErrorMessage
        End Get
        Set(ByVal value As String)
            _ErrorMessage = value
        End Set
    End Property

End Class