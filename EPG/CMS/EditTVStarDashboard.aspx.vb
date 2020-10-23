Imports System
Imports System.Data.SqlClient

Public Class EditTVStarDashboard
    Inherits System.Web.UI.Page
    Dim ConString As New SqlConnection(ConfigurationManager.ConnectionStrings("EPGConnectionString1").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClientScript.RegisterStartupScript(Me.GetType, "PageLoad", "pageLoad();", True)
        If Page.IsPostBack = False Then
            Try
                lbLanguageid.Text = Request.QueryString("langid").ToString
                lbProfileId.Text = Request.QueryString("profileid").ToString
                lbprogid.Text = Request.QueryString("progid").ToString
                lbMode.Text = Request.QueryString("mode").ToString

                Dim obj As New clsExecute

                Dim dt As DataTable = obj.executeSQL("select * from mst_program where progid='" & lbprogid.Text & "'", False)
                lbChannel.Text = dt.Rows(0)("channelid").ToString
                lbProgramme.Text = dt.Rows(0)("progname").ToString

                Dim dtLang As DataTable = obj.executeSQL("select * from mst_language where languageid='" & lbLanguageid.Text & "'", False)
                lbChannel.Text = dtLang.Rows(0)("fullname").ToString

                Dim dteng As DataTable = obj.executeSQL("SELECT * FROM tvstars_prog_mapping_regional WHERE profileID='" & lbProfileId.Text & "' AND progid='" & lbprogid.Text & "' AND languageID=1", False)
                lbName.Text = dteng.Rows(0)("profilename").ToString
                lbRole.Text = dteng.Rows(0)("RoleName").ToString
                lbDescription.Text = dteng.Rows(0)("RoleDesc").ToString


                BtnSubmit.Text = "ADD"
                If lbMode.Text = "U" Then
                    Dim dtreg As DataTable = obj.executeSQL("SELECT * FROM tvstars_prog_mapping_regional WHERE profileID='" & lbProfileId.Text & "' AND progid='" & lbprogid.Text & "' AND languageID='" & lbLanguageid.Text & "'", False)
                    txtProfileName.Text = dtreg.Rows(0)("profilename").ToString
                    txtRoleName.Text = dtreg.Rows(0)("rolename").ToString
                    txtDescription.Text = dtreg.Rows(0)("roledesc").ToString
                    BtnSubmit.Text = "UPDATE"
                End If

            Catch ex As Exception
                Logger.LogError("Edit TV Star Dashboard", "Page Load", ex.Message.ToString, User.Identity.Name)
            End Try
        End If
    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Try
            If BtnSubmit.Text = "ADD" Then
                exec_Proc(lbprogid.Text, lbProfileId.Text, lbLanguageid.Text, txtProfileName.Text.Trim, txtRoleName.Text.Trim, txtDescription.Text.Trim, "A")
            ElseIf BtnSubmit.Text = "UPDATE" Then
                exec_Proc(lbprogid.Text, lbProfileId.Text, lbLanguageid.Text, txtProfileName.Text.Trim, txtRoleName.Text.Trim, txtDescription.Text.Trim, "U")
            End If
            hfSend.Value = "2"

        Catch ex As Exception
            lberror.Text = ex.Message.ToString.Trim
            lberror.Visible = True
            Logger.LogError("EditTVStarDashboard", "BtnSubmit_Click", ex.Message.ToString, User.Identity.Name)
        Finally
            ConString.Close()
        End Try
    End Sub
  
    Private Sub exec_Proc(ByVal vProgId As Integer, ByVal vProfileId As Integer, ByVal vLanguageId As Integer, ByVal vProfileName As String, ByVal vRoleName As String, ByVal vRoleDescription As String, ByVal vAction As String)
        Try
            If vLanguageId = 1 Then
                vProfileName = Logger.RemSplCharsEng(vProfileName.ToString)
                vRoleName = Logger.RemSplCharsEng(vRoleName.ToString)
                vRoleDescription = Logger.RemSplCharsEng(vRoleDescription.ToString)
            Else
                vProfileName = Logger.RemSplCharsAllLangs(vProfileName.ToString, vLanguageId)
                vRoleName = Logger.RemSplCharsAllLangs(vRoleName.ToString, vLanguageId)
                vRoleDescription = Logger.RemSplCharsAllLangs(vRoleDescription.ToString, vLanguageId)

            End If

            Dim obj As New clsExecute

            obj.executeSQL("sp_tvstars_prog_mapping_regional", "ProgId~ProfileId~LanguageId~ProfileName~RoleName~roledescription~Actionuser~Action", _
                                    "Int~Int~Int~nVarChar~nVarChar~nVarChar~Varchar~Char", _
                                     vProgId & "~" & vProfileId & "~" & vLanguageId & "~" & vProfileName & "~" & vRoleName & "~" & vRoleDescription & "~" & User.Identity.Name & "~" & vAction, True, False)
            'Else

            'End If



        Catch ex As Exception
            'Logger.LogError("Edit Program Name", action & " exec_ProcRegional", ex.Message.ToString, User.Identity.Name)
        End Try
    End Sub

End Class