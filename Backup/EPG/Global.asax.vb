Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        'Session.Remove(User.Identity.Name)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        'Session.RemoveAll()
        ' Fires when the application ends
    End Sub

    Private Sub Global_asax_PreRequestHandlerExecute(sender As Object, e As System.EventArgs) Handles Me.PreRequestHandlerExecute
        '        Dim sessTimeOut As New TimeSpan(0, 0, 60)
        '        If TypeOf (Context.Handler) Is IRequiresSessionState _
        'Or TypeOf (Context.Handler) Is IReadOnlySessionState Then
        '            If Not Session(User.Identity.Name) = Nothing Then
        '                If Not Session(User.Identity.Name) = "" Then
        '                    Dim strCacheKey As String = Session(User.Identity.Name).ToString()
        '                    If HttpContext.Current.Cache.Count > 0 Then
        '                        If Not HttpContext.Current.Cache(strCacheKey) Is Nothing Then
        '                            Dim strUser As String = HttpContext.Current.Cache(strCacheKey).ToString()
        '                        Else
        '                            HttpContext.Current.Cache.Insert(strCacheKey, _
        '                            strCacheKey, _
        '                            Nothing, _
        '                            DateTime.MaxValue, _
        '                            sessTimeOut, _
        '                            CacheItemPriority.NotRemovable, _
        '                            Nothing)
        '                        End If
        '                    Else
        '                        HttpContext.Current.Cache.Insert(strCacheKey, _
        '                        strCacheKey, _
        '                        Nothing, _
        '                        DateTime.MaxValue, _
        '                        sessTimeOut, _
        '                        CacheItemPriority.NotRemovable, _
        '                        Nothing)
        '                    End If
        '                End If
        '            End If
        '        End If
    End Sub
    

    Function GetSessionCacheItem(msUserID As String) As String
        Throw New NotImplementedException
    End Function

End Class