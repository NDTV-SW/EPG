<%@ Page Title="TV Star Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="TvStarMaster.aspx.vb" Inherits="EPG.TvStarMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #black_overlay
        {
            display: none;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
        }
        #popme
        {
            display: none;
            position: fixed;
            top: 35%;
            left: 35%;
            width: 25%;
            height: 20%;
            padding: 16px;
            border: 5px solid orange;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        #popme1
        {
            position: fixed;
            top: 20%;
            left: 22%;
            width: 200;
            display: none;
            z-index: 10;
            background: rgba(0, 0, 0, 0.5);
            padding-left: 0.5%;
            padding-right: 0.5%;
        }
        #demo3
        {
            position: fixed;
            top: 7%;
            left: 40%;
            right: 1%;
            display: none;
        }
    </style>
    <script type="text/javascript">

        function openWin(profileId) {
            window.open("TvStarDetails.aspx?profileId=" + profileId, "Tv Star Details", "width=400,height=400,toolbar=0,left=300,top=150,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }

        function hideDiv() {
            $("#popme").slideUp(200);
            $("#black_overlay").slideUp(200);
        }

        function clientMessage() {
            try {
                //alert("file Uploaded");
                //$("#jsStatus").text("File Uploaded");
                var vControlId = $("#jsControlId").val();
                var vimgControlid = $("#jsImageId").val();
                var fileName = $("#MainContent_jsFileName").val();
                //alert(vControlId);
                $("#" + vControlId).html(fileName);
                $("#" + vimgControlid).attr("src", "../uploads/tvstarimages/" + $("#MainContent_jsFileName").val());
                $("#MainContent_grdProgImage_imglogo_1").attr("src", $("#txtEmail").val());
                hideDiv();
            }
            catch (ex) {
                //alert("Error : " + ex.Message);
            }


        }

    </script>
    <link href="../lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../lightbox.js" type="text/javascript"></script>
    <%--For Multi-select DropDown--%>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstLanguage]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            initLightbox();
        }
    </script>
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                            TV Star Master</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 input-group-sm ">
                                    <div class="form-group">
                                        <label>
                                            Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ValidationGroup="VGTvStar" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            Gender</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Male" Value="M" />
                                                <asp:ListItem Text="Female" Value="F" />
                                                <asp:ListItem Text="Trans Gender" Value="T" />
                                            </asp:DropDownList>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            Date of Birth</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" Enabled="false" ValidationGroup="VGTvStar" />
                                            <span class="input-group-addon">
                                                <asp:CalendarExtender ID="CE_txtDOB" runat="server" PopupButtonID="img_txtDOB" TargetControlID="txtDOB" />
                                                <asp:Image ID="img_txtDOB" runat="server" ImageUrl="~/Images/calendar.png" /></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            Place of Birth</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtPOB" runat="server" CssClass="form-control" ValidationGroup="VGTvStar" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            Twitter Tag</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtTwitter" runat="server" CssClass="form-control" ValidationGroup="VGTvStar" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            BioGraphy</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtBiography" runat="server" CssClass="form-control" ValidationGroup="VGTvStar"
                                                TextMode="MultiLine" Rows="2" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            Select Language (Multiple)</label>
                                        <div class="input-group">
                                            <asp:ListBox ID="lstLanguage" runat="server" CssClass="form-control" Height="60px"
                                                SelectionMode="Multiple" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <br />
                                    <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                    <asp:Label ID="lbProfileId" runat="server" Visible="false" />
                                    <br />
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 alert-danger">
                                        <asp:Label ID="lbReport" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <asp:TextBox ID="txtSearch1" runat="server" placeholder="search text" CssClass="input-group-sm btn btn-default" />
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="FullName" DataValueField="LanguageID"
                                    DataSourceID="SqlmstLanguage" CssClass="form-control" />
                                <asp:SqlDataSource ID="SqlmstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                    SelectCommand="select 0 LanguageID,'Select All' FullName, 0 p union select LanguageID,FullName, 1 p from mst_language where active=1 order by p">
                                </asp:SqlDataSource>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                With
                                <asp:CheckBox ID="chkImages" runat="server" Text="Images" Checked="False" />
                                <asp:CheckBox ID="chkBiography" runat="server" Text="Biography" Checked="False" />
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" style="max-height: 400px; overflow-y: scroll;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:GridView ID="grdTvStar" runat="server" BackColor="White" Width="100%" DataSourceID="sqlDSCelebrityMaster"
                                CssClass="table" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                AutoGenerateColumns="false" AllowPaging="true" PageSize="20" ForeColor="Black"
                                GridLines="Vertical" EmptyDataText="No record Found..." AllowSorting="true">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbProfileId" runat="server" Text='<%#Eval("profileId") %>' Visible="false" />
                                            <asp:Label ID="lbSno" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbName" runat="server" Text='<%#Eval("Name") %>' Visible="false" />
                                            <asp:HyperLink ID="hyName" runat="server" Text='<%#Eval("Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                        <ItemTemplate>
                                            <asp:Label ID="lbGender" runat="server" Text='<%#Eval("Gender") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Twitter Tag" SortExpression="TwitterTag">
                                        <ItemTemplate>
                                            <asp:Label ID="lbTwitterTag" runat="server" Text='<%#Eval("TwitterTag") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOB" SortExpression="DOB">
                                        <ItemTemplate>
                                            <asp:Label ID="lbDOB1" runat="server" Text='<%#Eval("DOB1") %>' />
                                            <asp:Label ID="lbDOB2" runat="server" Text='<%#Eval("DOB2") %>' Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="POB" SortExpression="PlaceOfBirth">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPOB" runat="server" Text='<%#Eval("PlaceOfBirth") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BioGraphy" SortExpression="BioGraphy">
                                        <ItemTemplate>
                                            <asp:Label ID="lbBioGraphy" runat="server" Text='<%#Eval("BioGraphy") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image" SortExpression="profilePath">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyLogo" rel="lightbox" runat="server" NavigateUrl='<%# Bind("profilePath1") %>'>
                                                <asp:Image ID="imgCelebrityImage" runat="server" ImageUrl='<%# Bind("profilePath1") %>'
                                                    Width="100px" Height="100px" />
                                            </asp:HyperLink>
                                            <asp:Label ID="lbFileName" runat="server" Visible="false" Text='<%# Bind("filename") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Upload">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyUpload" runat="server" Text="Upload Image" ImageUrl="../images/upload.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png" />
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/images/delete.png" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sqlDSCelebrityMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
                            </asp:SqlDataSource>
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <div id="popme">
                <button type="button" style="float: right" onclick="hideDiv();">
                    X</button><br />
                <input type="hidden" id="jsProfileId" size="50" runat="server" />
                <input type="hidden" id="jsImageId" size="50" />
                <input type="hidden" id="jsControlId" size="50" />
                <input type="hidden" id="jsFileName" size="50" runat="server" />
                <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="#82CAFA"
                    CompleteBackColor="#FFFFFF" Filter="*.jpg;*.jpeg|Supported Images Types (*.jpg;*.jpeg)"
                    OnClientUploadComplete="clientMessage" OnUploadedComplete="AsyncFileUpload1_UploadedComplete"
                    ThrobberID="Image1" ProgressInterval="50" SaveBufferSize="128" Width="200px"
                    EnableProgress="OnSubmit, OnPreview" />
                <br />
                <span style="color: Red">only .jpg and .jpeg files supported</span>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loader.gif" />
                <br />
            </div>
            <div id="black_overlay">
            </div>
            <script>
                function showDiv(jsControlId, jsImageId, jsProgId, jsFileName) {
                    try {
                        $("#jsImageId").val(jsImageId);
                        $("#jsControlId").val(jsControlId);

                        document.getElementById("MainContent_jsFileName").value = jsFileName;
                        document.getElementById("MainContent_jsProfileId").value = jsProgId;

                        $("#popme").delay(200).slideDown(300);
                        $("#black_overlay").delay(200).slideDown(200);
                    }
                    catch (ex) {
                        alert(ex.ToString());
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
