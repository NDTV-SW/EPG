Public Class richlinkmissing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim li As HtmlGenericControl = TryCast(Master.FindControl("lirichlinkmissing"), HtmlGenericControl)
            li.Attributes.Add("class", "active")
        End If
    End Sub

    Protected Sub btnUpdateRich_Click(sender As Object, e As EventArgs) Handles btnUpdateRich.Click
        Dim obj As New clsExecute

        For Each row In grd1.Rows
            Dim chkAddRich As CheckBox = DirectCast(row.FindControl("chkAddRich"), CheckBox)
            Dim txtSearchRichMeta As TextBox = DirectCast(row.FindControl("txtSearchRichMeta"), TextBox)

            Dim ddlType As DropDownList = DirectCast(row.FindControl("ddlType"), DropDownList)
            'Dim intRowId As Integer = DirectCast(row.FindControl("lbRowid"), Label).Text
            Dim intProgid As Integer = DirectCast(row.FindControl("lbProgid"), Label).Text
            Dim lbChannelid As Label = DirectCast(row.FindControl("lbChannelid"), Label)
            Dim intRichMetaId As Integer = 0

            If chkAddRich.Checked Then
                Dim sql As String
                'sql = "insert into richmeta(type,broadcasterid,name,synopsis,genrename1,genrename2,parentalguide,releaseyear,starcast,director,producer,originallanguageid,dubbedlanguge,country,imageurl,imageurlportrait) "
                'sql = sql & "select '" & ddlType.SelectedValue & "' type,(select companyid from mst_channel where channelid=b.channelid) broadcasterid,[program name] progname,description,genre,subgenre,pg,releaseyear,actor,director,producer"
                'sql = sql & ",isnull((select languageid from mst_language where fullname=a.origlang),isnull((select channellanguageid from mst_channel where channelid='" & ddlChannel.SelectedValue & "'),2)),isnull((select languageid from mst_language where fullname=a.dubbedlang),isnull((select channellanguageid from mst_channel where channelid='" & ddlChannel.SelectedValue & "'),2))"
                'sql = sql & ",origincountry,b.programlogo,b.programlogoportrait from map_epgexcel a "
                'sql = sql & " join mst_program b on a.[program name]=b.progname and a.channelid=b.channelid where a.rowid='" & intRowId & "'"

                sql = "insert into richmeta(type,broadcasterid,name,synopsis,genrename1,genrename2,parentalguide,releaseyear,starcast,director,producer,originallanguageid,dubbedlanguge,country,imageurl,imageurlportrait) "
                sql = sql & "select top 1 '" & ddlType.SelectedValue & "' type,(select companyid from mst_channel where channelid=b.channelid) broadcasterid,b.progname,a.episodicsynopsis,egenre,esubgenre,pg,ereleaseyear,eactor,edirector,eproducer"
                sql = sql & ",isnull((select languageid from mst_language where fullname=a.origlang),isnull((select channellanguageid from mst_channel where channelid='" & lbChannelid.Text & "'),2)),isnull((select languageid from mst_language where fullname=a.dubbedlang),isnull((select channellanguageid from mst_channel where channelid='" & lbChannelid.Text & "'),2))"
                sql = sql & ",origincountry,b.programlogo,b.programlogoportrait from mst_epg a "
                sql = sql & " join mst_program b on a.progid=b.progid and a.channelid=b.channelid where a.progid='" & intProgid & "'"
                obj.executeSQL(sql, False)
                HttpContext.Current.Session("richmeta") = Nothing
            ElseIf txtSearchRichMeta.Text.Trim.Length > 0 Then
                intRichMetaId = txtSearchRichMeta.Text.Split("~")(2)
                obj.executeSQL("update mst_program set richmetaid='" & intRichMetaId & "' where progid='" & intProgid & "'", False)

                Dim dtProg As DataTable = obj.executeSQL("select programlogo,programlogoportrait from mst_program where progid='" & intProgid & "'", False)
                Dim dtRich As DataTable = obj.executeSQL("select imageurl,imageurlportrait from richmeta where id='" & intRichMetaId & "'", False)

                Dim progProgramLogo As String = dtProg.Rows(0)("programlogo").ToString
                Dim progProgramLogoPortrait As String = dtProg.Rows(0)("programlogoportrait").ToString
                Dim richImageUrl As String = dtRich.Rows(0)("imageurl").ToString
                Dim richImageUrlPortrait As String = dtRich.Rows(0)("imageurlportrait").ToString

                If progProgramLogo.Trim = "" And richImageUrl.Trim <> "" Then
                    obj.executeSQL("update mst_program set programlogo='" & richImageUrl & "' where progid='" & intProgid & "'", False)
                End If
                If progProgramLogoPortrait.Trim = "" And richImageUrlPortrait.Trim <> "" Then
                    obj.executeSQL("update mst_program set programlogoportrait='" & richImageUrlPortrait & "' where progid='" & intProgid & "'", False)
                End If

                If progProgramLogo.Trim <> "" And richImageUrl.Trim = "" Then
                    obj.executeSQL("update richmeta set imageurl='" & progProgramLogo & "' where id='" & intRichMetaId & "'", False)
                End If
                If progProgramLogoPortrait.Trim <> "" And richImageUrlPortrait.Trim = "" Then
                    obj.executeSQL("update richmeta set imageurlportrait='" & progProgramLogoPortrait & "' where id='" & intRichMetaId & "'", False)
                End If

            End If

            'select top 10 programlogo,programlogoportrait,* from mst_program
            'select top 10 imageurl,imageurlportrait,* from richmeta

        Next row
        grd1.DataBind()
    End Sub

    <System.Web.Script.Services.ScriptMethod(),
 System.Web.Services.WebMethod()>
    Public Shared Function SearhRichMeta(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim obj As New clsUploadModules
        Dim channels As List(Of String) = obj.getRichMeta(contextKey, prefixText, count)
        'select type + '~' + name + '~' + convert(varchar(10),id) richmetaname from richmeta where name like '%love%'
        Return channels
    End Function
End Class