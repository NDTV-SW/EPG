Imports System.Data
Imports System.Web.HttpPostedFile
Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Public Class FPC_EPG_Auto
    Inherits System.Web.UI.Page

    Private Sub myErrorBox(ByVal errorstr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & errorstr & "');", True)
    End Sub

    Private Sub myMessageBox(ByVal messagestr As String)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alertBox('" & messagestr & "');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then

            End If
        Catch ex As Exception
            Logger.LogError("FPC_EPG_Auto", "Page Load", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

    Private Sub uploadChannelSchedule()
        Try

            Dim sql As String = "insert into map_epgexcel([Program Name],Genre,Date,Time,Description,[Episode No],channelID,season,pg,seriesandteams,proglang,releaseyear,actor,director,producer,originalrepeat) select progname,genre,progdate,starttime,synopsis,episodeno,channelid,seriesno,pg,seriesname,proglang,releaseyear,starcast,director,producer,orignalrepeate from fpc_epg where channelid='" & ddlChannel.SelectedValue & "'"
            Dim obj As New clsExecute
            obj.executeSQL("delete from map_epgexcel where channelid='" & ddlChannel.SelectedValue & "'", False)
            obj.executeSQL(sql, False)
            myMessageBox("EPG updated. Please use Upload Module to Build channel")
        Catch ex As Exception
            Logger.LogError("FPC_EPG_Auto", "uploadChannelSchedule", ex.Message.ToString, User.Identity.Name)
            myErrorBox("ERROR:" & ex.Message.ToString)
        End Try
    End Sub

    Private Sub bindGrid()
        Try
            Dim sql As String = "select id,progname,genre,progdate,starttime,synopsis,episodeno,channelid,seriesno,pg,seriesname,proglang,releaseyear,starcast,director,producer,orignalrepeate from fpc_epg where channelid='" & ddlChannel.SelectedValue & "'"
            Dim obj As New clsExecute
            Dim dt As DataTable = obj.executeSQL(sql, False)
            grdExcelData.DataSource = dt
            grdExcelData.DataBind()
        Catch ex As Exception
            Logger.LogError("FPC_EPG_Auto", "bindGrid", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub
   
    Protected Sub ddlChannel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChannel.SelectedIndexChanged
        bindGrid()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        uploadChannelSchedule()
    End Sub
End Class
