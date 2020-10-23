Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Globalization
Public Class FPC_Distribution
    Inherits System.Web.UI.Page


    Private Function ReplaceDoubleDoubleQuotes(ByVal text As String) As String
        Return text.Trim.Replace("'", "''")
    End Function

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveMe.Click
        Dim strMasterDay As String = ""
        Dim i As Integer = 0
        While i < chkWeekList.Items.Count
            If chkWeekList.Items(i).Selected Then
                strMasterDay = strMasterDay & chkWeekList.Items(i).Value & "|"
            End If
            i = i + 1
        End While
        strMasterDay = strMasterDay.Substring(0, strMasterDay.Length - 1)

        Dim obj As New clsExecute
        If btnSaveMe.Text = "SAVE" Then
            obj.executeSQL("insert into fpc_distribution(feedtype,feedformat,maintz,fieldmapping,frequency,subfrequency,singlefile,sheettype,sendmail,sendftp,mailto,mailcc,mailbcc,ftpip,ftpuser,ftppass,ftpfingerprint,ftpport,client,active,lastupdate,masterfile,updates,zip,masterday,mastertime,zipupload,ftpdir,fpcbackup,mailfromname,mailfrom,market,fnname,disclaimer) " & _
            " values('" & ddlfeedtype.SelectedValue & "','" & ddlfeedformat.SelectedValue & "','" & ddlTZ.SelectedValue & "','" & ReplaceDoubleDoubleQuotes(txtfieldmapping.Text) & "'," & _
            " '" & ddlfrequency.SelectedValue & "','" & ddlSubFrequency.SelectedValue & "','" & chkSingleFile.Checked & "','" & ddlSheetType.SelectedValue & "','" & chkSendMail.Checked & "','" & chkSendFTP.Checked & "','" & txtmailto.Text & "','" & txtmailcc.Text & "'," & _
            " '" & ReplaceDoubleDoubleQuotes(txtmailbcc.Text) & "','" & ReplaceDoubleDoubleQuotes(txtftpip.Text) & "','" & ReplaceDoubleDoubleQuotes(txtftpuser.Text) & "','" & ReplaceDoubleDoubleQuotes(txtftppass.Text) & "'," & _
            " '" & ReplaceDoubleDoubleQuotes(txtfingerprint.Text) & "','" & ReplaceDoubleDoubleQuotes(txtftpport.Text) & "','" & ReplaceDoubleDoubleQuotes(txtclient.Text) & "','" & chkActive.Checked & "',dbo.getlocaldate()," & _
            " '" & chkMasterFile.Checked & "','" & chkUpdates.Checked & "','" & chkZip.Checked & "','" & strMasterDay & "','" & txtMasterTime.Text & "','" & chkZipUpload.Checked & "','" & txtFtpDir.Text & "','" & chkFpcBkp.Checked & "'," & _
            " '" & txtMailFromName.Text & "','" & txtMailFrom.Text & "','" & ddlMarket.SelectedValue & "','" & txtFunctionName.Text & "','" & chkDisclaimer.Checked & "')", False)
        Else
            Dim sql As String = ""
            sql = sql & "update fpc_distribution set "
            sql = sql & "feedtype='" & ddlfeedtype.SelectedValue & "',"
            sql = sql & "feedformat='" & ddlfeedformat.SelectedValue & "',"
            sql = sql & "maintz='" & ddlTZ.SelectedValue & "',"
            sql = sql & "fieldmapping='" & ReplaceDoubleDoubleQuotes(txtfieldmapping.Text) & "',"
            sql = sql & "frequency='" & ddlfrequency.SelectedValue & "',"
            sql = sql & "subfrequency='" & ddlSubFrequency.SelectedValue & "',"
            sql = sql & "singlefile='" & chkSingleFile.Checked & "',"
            sql = sql & "sheettype='" & ddlSheetType.SelectedValue & "',"
            sql = sql & "sendmail='" & chkSendMail.Checked & "',"
            sql = sql & "sendftp='" & chkSendFTP.Checked & "',"
            sql = sql & "masterfile='" & chkMasterFile.Checked & "',"
            sql = sql & "updates='" & chkUpdates.Checked & "',"
            sql = sql & "zip='" & chkZip.Checked & "',"
            sql = sql & "masterday='" & strMasterDay & "',"
            sql = sql & "mastertime='" & txtMasterTime.Text & "',"
            sql = sql & "mailfromname='" & txtMailFromName.Text & "',"
            sql = sql & "mailfrom='" & txtMailFrom.Text & "',"

            sql = sql & "mailto='" & txtmailto.Text & "',"
            sql = sql & "mailcc='" & txtmailcc.Text & "',"
            sql = sql & "mailbcc='" & txtmailbcc.Text & "',"
            sql = sql & "ftpip='" & txtftpip.Text & "',"
            sql = sql & "ftpuser='" & txtftpuser.Text & "',"
            sql = sql & "ftppass='" & txtftppass.Text & "',"
            sql = sql & "ftpfingerprint='" & txtfingerprint.Text & "',"
            sql = sql & "ftpport='" & txtftpport.Text & "',"
            sql = sql & "client='" & txtclient.Text & "',"

            sql = sql & "zipupload='" & chkZipUpload.Checked & "',"
            sql = sql & "ftpdir='" & txtFtpDir.Text & "',"
            sql = sql & "fpcbackup='" & chkFpcBkp.Checked & "',"
            sql = sql & "market='" & ddlMarket.SelectedValue & "',"
            sql = sql & "fnname='" & txtFunctionName.Text & "',"
            sql = sql & "disclaimer='" & chkDisclaimer.Checked & "',"

            sql = sql & "active='" & chkActive.Checked & "',"
            sql = sql & "lastupdate=dbo.getlocaldate()"
            sql = sql & " where id='" & lbID.Text & "'"
            obj.executeSQL(sql, False)
        End If


        clearAll()
    End Sub

    Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grd.SelectedIndexChanged
        If Not (User.Identity.Name.ToLower = "sachint" Or User.Identity.Name.ToLower = "rohitm" Or User.Identity.Name.ToLower = "kautilyar") Then
            txtmailto.Enabled = False
            txtmailcc.Enabled = False
            txtmailbcc.Enabled = False
            txtfieldmapping.Enabled = False
        End If
        Dim i As Integer = 0
        While i < chkWeekList.Items.Count
            chkWeekList.Items(i).Selected = False
            i = i + 1
        End While

        Dim obj As New clsExecute
        lbID.Text = DirectCast(grd.SelectedRow.FindControl("lbID"), Label).Text
        Dim dt As DataTable = obj.executeSQL("SELECT * FROM fpc_distribution WHERE id='" & lbID.Text & "'", False)

        Dim strMasterDay As String = dt.Rows(0)("masterday").ToString
        strMasterDay = strMasterDay.Replace(",", "|")
        Dim ArStrMasterDay As Array = strMasterDay.Split("|")
        If ArStrMasterDay.Length > 0 Then
            For Each valMasterDay In ArStrMasterDay
                Dim title As String = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valMasterDay)
                Try
                    chkWeekList.Items.FindByValue(title).Selected = True
                Catch ex As Exception

                End Try

            Next
        End If

        Try
            ddlfeedtype.SelectedValue = dt.Rows(0)("feedtype").ToString
        Catch
        End Try
        Try
            ddlfeedformat.SelectedValue = dt.Rows(0)("feedformat").ToString
        Catch
        End Try
        Try
            ddlfrequency.SelectedValue = dt.Rows(0)("frequency").ToString
        Catch
        End Try

        Try
            ddlSubFrequency.SelectedValue = dt.Rows(0)("subfrequency").ToString
        Catch
        End Try

        chkSingleFile.Checked = dt.Rows(0)("singlefile").ToString

        Try
            ddlSheetType.SelectedValue = dt.Rows(0)("sheettype").ToString
        Catch
        End Try

        Try
            ddlTZ.SelectedValue = dt.Rows(0)("maintz").ToString
        Catch
        End Try


        txtfieldmapping.Text = dt.Rows(0)("fieldmapping").ToString
        chkSendMail.Checked = dt.Rows(0)("sendmail").ToString
        chkSendFTP.Checked = dt.Rows(0)("sendftp").ToString
        txtmailto.Text = dt.Rows(0)("mailto").ToString
        txtmailcc.Text = dt.Rows(0)("mailcc").ToString
        txtmailbcc.Text = dt.Rows(0)("mailbcc").ToString
        txtftpip.Text = dt.Rows(0)("ftpip").ToString
        txtftpuser.Text = dt.Rows(0)("ftpuser").ToString
        txtftppass.Text = dt.Rows(0)("ftppass").ToString
        txtfingerprint.Text = dt.Rows(0)("ftpfingerprint").ToString
        txtftpport.Text = dt.Rows(0)("ftpport").ToString
        txtclient.Text = dt.Rows(0)("client").ToString
        chkMasterFile.Checked = dt.Rows(0)("masterfile").ToString
        chkUpdates.Checked = dt.Rows(0)("updates").ToString
        chkZip.Checked = dt.Rows(0)("zip").ToString
        txtMailFromName.Text = dt.Rows(0)("mailfromname").ToString
        txtMailFrom.Text = dt.Rows(0)("mailfrom").ToString

        txtMasterTime.Text = dt.Rows(0)("mastertime").ToString

        chkZipUpload.Checked = dt.Rows(0)("zipupload").ToString
        txtFtpDir.Text = dt.Rows(0)("ftpdir").ToString
        chkFpcBkp.Checked = dt.Rows(0)("fpcbackup").ToString

        ddlMarket.SelectedValue = dt.Rows(0)("market").ToString
        txtFunctionName.Text = dt.Rows(0)("fnname").ToString
        chkDisclaimer.Checked = dt.Rows(0)("disclaimer").ToString


        chkActive.Checked = dt.Rows(0)("active").ToString
        lbID.Text = dt.Rows(0)("id").ToString
        btnSaveMe.Text = "UPDATE"
    End Sub

    Private Sub clearAll()

        txtfieldmapping.Text = ""
        chkSendMail.Checked = False
        chkSendFTP.Checked = False
        txtmailto.Text = ""
        txtmailcc.Text = ""
        txtmailbcc.Text = ""
        txtftpip.Text = ""
        txtftpuser.Text = ""
        txtftppass.Text = ""
        txtfingerprint.Text = ""
        txtftpport.Text = ""
        txtclient.Text = ""
        txtMasterTime.Text = ""
        txtMailFromName.Text = ""
        txtMailFrom.Text = ""

        txtmailto.Enabled = True
        txtmailcc.Enabled = True
        txtmailbcc.Enabled = True
        txtfieldmapping.Enabled = True

        ddlfeedtype.SelectedIndex = 0
        ddlfeedformat.SelectedIndex = 0
        ddlfrequency.SelectedIndex = 0
        ddlSubFrequency.SelectedIndex = 0
        chkSingleFile.Checked = True
        ddlSheetType.SelectedIndex = 0
        chkZip.Checked = False
        chkMasterFile.Checked = False
        chkUpdates.Checked = False


        chkZipUpload.Checked = False
        txtFtpDir.Text = ""
        chkFpcBkp.Checked = False

        ddlMarket.SelectedIndex = 0

        txtFunctionName.Text = ""
        chkDisclaimer.Checked = True
        chkActive.Checked = True


        Dim i As Integer = 0
        While i < chkWeekList.Items.Count
            chkWeekList.Items(i).Selected = True
            i = i + 1
        End While
        grd.SelectedIndex = -1
        grd.DataBind()

        btnSaveMe.Text = "SAVE"

        ddlTZ.SelectedValue = "IST"
    End Sub

    Protected Sub chkSendFTP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSendFTP.CheckedChanged
        If chkSendFTP.Checked Then
            rfvtxtftpip.Enabled = True
            rfvftpuser.Enabled = True
            rfvftppass.Enabled = True
        Else
            rfvtxtftpip.Enabled = False
            rfvftpuser.Enabled = False
            rfvftppass.Enabled = False
        End If
    End Sub

    Protected Sub chkSendMail_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSendMail.CheckedChanged
        If chkSendMail.Checked Then
            rfvmailto.Enabled = True
        Else
            rfvmailto.Enabled = False
        End If
    End Sub

    Protected Sub grd_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chkActiveGrd As CheckBox = TryCast(e.Row.FindControl("chkActiveGrd"), CheckBox)

            If Not chkActiveGrd.Checked Then
                e.Row.BackColor = Drawing.Color.PaleVioletRed
            End If
        End If
    End Sub
End Class