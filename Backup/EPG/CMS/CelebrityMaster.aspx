<%@ Page Title="Celebrity Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="CelebrityMaster.aspx.vb" Inherits="EPG.CelebrityMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function openCelebDetails(CelebName, CelebId, Mode) {
            window.open("CelebrityDetails.aspx?CelebName=" + CelebName + "&CelebId=" + CelebId + "&Mode=" + Mode, "Celebrity Details Programme", "width=800,height=500,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
    <style type="text/css">
        #fixed
        {
            position: fixed;
            top: 5%;
            left: 0%;
        }
        .clear
        {
            clear: both;
            margin: 0px;
            padding: 0px;
            line-height: 0px;
        }
        #fixedbelow
        {
            position: relative;
        }
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
                $("#" + vimgControlid).attr("src", "../uploads/celebimages/" + $("#MainContent_jsFileName").val());
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Celebrity Master</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Name</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                                    <asp:Label ID="lbID" runat="server" Visible="false" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Birth Place</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtBirthPlace" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Birth Day</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtBirthDay" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgBirthDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtBirthDay" runat="server" TargetControlID="txtBirthDay"
                                            PopupButtonID="imgBirthDateCalendar" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Death Day</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDeathDay" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgDeathDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtDeathDay" runat="server" TargetControlID="txtDeathDay"
                                            PopupButtonID="imgDeathDateCalendar" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Bio-graphy</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtBioGraphy" runat="server" TextMode="MultiLine" Rows="3" MaxLength="2000"
                                        CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Celeb Language</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlCelebLanguage" runat="server" DataSourceID="sqlDSLanguage"
                                        DataTextField="fullname" DataValueField="languageid" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                    <asp:SqlDataSource ID="sqlDSLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select LanguageID,FullName from mst_language where active=1">
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Verified</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkVerified" Text="Verified" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-heading text-left">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <div class="form-group">
                            <label>
                                Search Celebrity</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch1" runat="server" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <div class="form-group">
                            <label>
                                Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="FullName" DataValueField="LanguageID"
                                    DataSourceID="SqlmstLanguage" CssClass="form-control" />
                                <asp:SqlDataSource ID="SqlmstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                    SelectCommand="select LanguageID,FullName from mst_language where active=1">
                                </asp:SqlDataSource>
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <div class="form-group">
                            <label>
                                only / with</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkOnlyVerified" runat="server" Text="Verified" Checked="False" />
                                <asp:CheckBox ID="chkImages" runat="server" Text="Images" Checked="False" />
                                <asp:CheckBox ID="chkBiography" runat="server" Text="Biography" Checked="False" />
                                
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-left">
                        <div class="form-group">
                            <label>
                            </label>
                            <div class="input-group">
                            <asp:CheckBox ID="chkIgnore" runat="server" Text="Ignore Filters" Checked="False" BackColor="Yellow" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 alert-danger">
                        <asp:Label ID="lbReport" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading text-right">
                <asp:Button ID="btnVerifyMultiple" runat="server" Text="Verify Multiple" CssClass="btn btn-info" />
            </div>
            <div class="panel-body" style="max-height: 400px; overflow-y: scroll;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grdCelebrity" runat="server" BackColor="White" Width="100%" DataSourceID="sqlDSCelebrityMaster"
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
                            <asp:TemplateField HeaderText="ID" SortExpression="tmdbCelebrityID" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lbCelebrityID" runat="server" Text='<%# Bind("tmdbCelebrityID") %>' />
                                    <asp:Label ID="lbRowid" runat="server" Text='<%# Bind("rowid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbName" runat="server" Text='<%# Bind("Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Birth Place" SortExpression="PlaceofBirth">
                                <ItemTemplate>
                                    <asp:Label ID="lbPlaceofBirth" runat="server" Text='<%# Bind("PlaceofBirth") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BirthDay" SortExpression="BirthDay">
                                <ItemTemplate>
                                    <asp:Label ID="lbBirthDay" runat="server" Text='<%# Bind("BirthDay") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DeathDay" SortExpression="DeathDay">
                                <ItemTemplate>
                                    <asp:Label ID="lbDeathDay" runat="server" Text='<%# Bind("DeathDay") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Biography" SortExpression="Biography">
                                <ItemTemplate>
                                    <asp:Label ID="lbBiography" runat="server" Text='<%# Bind("Biography") %>' />
                                    <asp:Label ID="lbLanguageId" runat="server" Text='<%# Bind("LanguageId") %>' Visible="false" />
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
                            <asp:TemplateField HeaderText="Verified">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkOK" runat="server" Checked='<%# Bind("verified") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cast & Crew">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hyUpload" runat="server" Text="Upload Image" ImageUrl="../images/upload.png" />
                                    <asp:HyperLink ID="hyCast" runat="server" Text="Cast" />
                                    <asp:HyperLink ID="hyCrew" runat="server" Text="Crew" />
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
    <script>
        function showDiv(jsControlId, jsImageId, jsProgId, jsFileName) {
            try {
                $("#jsImageId").val(jsImageId);
                $("#jsControlId").val(jsControlId);

                document.getElementById("MainContent_jsFileName").value = jsFileName;
                document.getElementById("MainContent_jsTmdbCelebrityId").value = jsProgId;

                $("#popme").delay(200).slideDown(300);
                $("#black_overlay").delay(200).slideDown(200);
            }
            catch (ex) {
                alert(ex.ToString());
            }
        }
    </script>
    <div id="popme">
        <button type="button" style="float: right" onclick="hideDiv();">
            X</button><br />
        <input type="hidden" id="jsTmdbCelebrityId" size="50" runat="server" />
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
    
</asp:Content>
