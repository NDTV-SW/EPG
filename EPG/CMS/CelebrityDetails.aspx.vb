Imports System
Imports System.Data.SqlClient
Public Class CelebrityDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Try

                lbCelebId.Text = Request.QueryString("CelebId").ToString
                lbMode.Text = Request.QueryString("Mode").ToString
                lbCelebrityName.Text = Request.QueryString("CelebName").ToString & " (" & lbMode.Text & " details)"
                Dim sql As String
                If lbMode.Text = "Cast" Then
                    sql = "select originaltitle Title,CharacterPlayed,ReleaseDate,'http://image.tmdb.org/t/p/w342' + posterpath posterPath from tmdb_Celebrity_cast where tmdbcelebrityid='" & lbCelebId.Text & "'"
                ElseIf lbMode.Text = "Crew" Then
                    sql = "select  originaltitle Title,Department + ' (' +  Job + ')' Role,ReleaseDate,'http://image.tmdb.org/t/p/w342' + posterpath posterPath from tmdb_celebrity_crew where  tmdbcelebrityid='" & lbCelebId.Text & "'"
                Else
                    sql = ""
                End If
                Dim obj As New clsExecute
                Dim dt As DataTable = obj.executeSQL(sql, False)
                grdCelebDetails.DataSource = dt
                grdCelebDetails.DataBind()

            Catch ex As Exception
                Logger.LogError("CelebrityDetails", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub
End Class