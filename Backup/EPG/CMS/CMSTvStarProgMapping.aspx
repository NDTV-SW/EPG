<%@ Page Title="TV Star Programme Mapping" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/SiteCMS.Master" CodeBehind="CMSTvStarProgMapping.aspx.vb" Inherits="EPG.CMSTvStarProgMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .noFormatting
        {
            text-decoration: none;
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
            width: 30%;
            height: 25%;
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

                var vControlId = $("#MainContent_jsControlId").val();
                var fileName = $("#MainContent_jsFileName").val();
                $("#" + vControlId).attr("src", "../uploads/tvstarimages/" + $("#MainContent_jsFileName").val());
                hideDiv();
            }
            catch (ex) {
                alert(ex);
            }


        }
    </script>
    <link href="../lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../lightbox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            initLightbox();
        }
    </script>
    <%--<asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <div class="row">
                    <div class="col-lg-7 text-right">
                        <h3>
                            TV Star Programme Mapping</h3>
                    </div>
                    <div class="col-lg-5">
                        <h5>
                            <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6 text-right">
                                &nbsp;
                            </div>
                            <div class="col-lg-1 text-right">Genre</div>
                            
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 text-right">
                                <asp:DropDownList ID="ddlGenre" runat="server" DataTextField="genrename" DataValueField="genreid"
                                    DataSourceID="sqlDSGenre" CssClass="form-control" AutoPostBack="true" />
                            </div>
                            <div class="col-lg-2 text-right">Language</div>
                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6 text-right">
                                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="fullname" DataValueField="languageid"
                                    DataSourceID="sqlDSLanguage" CssClass="form-control" AutoPostBack="true" />
                            </div>
                        </h5>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Name</label>
                                <div class="input-group">
                                    <asp:Label ID="lbName" runat="server" Visible="false" />
                                    <asp:DropDownList ID="ddlTvStar" runat="server" DataSourceID="sqlDSTvStar" DataTextField="name"
                                        DataValueField="profileId" CssClass="form-control" />
                                    <asp:SqlDataSource ID="sqlDSTvStar" runat="server" SelectCommand="select ProfileID,name from mst_tvStars WHERE Name <>'' order by name"
                                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>
                                    Likes</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtLikes" runat="server" CssClass="form-control" ValidationGroup="VGTvStarGallery" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REV_txtLikes" ControlToValidate="txtLikes" ValidationExpression="^\d+"
                                            Display="Static" ErrorMessage="Only No.s" ForeColor="Red" runat="server" ValidationGroup="VGTvStarGallery" />
                                    </span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>
                                    Rating</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlRating" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="No Rating" Value="0" />
                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" />
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtRating" runat="server" CssClass="form-control" ValidationGroup="VGBigTVSearchGallery" />        --%>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Programme</label>
                                <div class="input-group">
                                    <%--<asp:DropDownList ID="ddlProgramme" CssClass="form-control" runat="server" />--%>
                                    <asp:ListBox ID="lstProgramme" CssClass="form-control" runat="server" SelectionMode="Multiple"
                                        Height="200px" />
                                    <asp:SqlDataSource ID="sqlDSProgramme" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VGTvStarGallery" />
                                <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                <asp:Label ID="lbProfileId" runat="server" Visible="false" />
                                <asp:Label ID="lbProgId" runat="server" Visible="false" />
                                <asp:Label ID="lbRowId" runat="server" Visible="false" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Role Name</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control" ValidationGroup="VGTvStarGallery" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>
                                    Role Desc</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtRoleDesc" runat="server" CssClass="form-control" ValidationGroup="VGTvStarGallery"
                                        TextMode="MultiLine" Rows="4" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Image</label>
                                <div class="input-group">
                                    <asp:HyperLink rel="lightbox" ID="hyPic" runat="server">
                                        <asp:Image ID="imgPic" runat="server" CssClass="form-control" Width="180px" Height="180px" /></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="input-group-sm btn btn-default" />
                <asp:Button ID="btnSearch" runat="server" Text="Search by TV Star" CssClass="btn btn-info" />
                <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="input-group-sm btn btn-default" />
                <asp:Button ID="btnSearch1" runat="server" Text="Search by Program" CssClass="btn btn-info" />
            </div>
            <div class="panel-body" style="max-height: 400px; overflow-y: scroll;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                    <asp:GridView ID="grdTvStarProgmapping" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                        CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" AllowPaging="true"
                        PageSize="20" BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true"
                        EmptyDataText="----No Record Found----" EmptyDataRowStyle-HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbProfId" runat="server" Text='<%#Eval("profileId") %>' Visible="false" />
                                    <asp:Label ID="lbPic" runat="server" Text='<%#Eval("Pic") %>' Visible="false" />
                                    <asp:Label ID="lbFileName" runat="server" Text='<%#Eval("fileName") %>' Visible="false" />
                                    <asp:Label ID="lbprogid" runat="server" Text='<%#Eval("progid") %>' Visible="false" />
                                    <asp:Label ID="lbRowId" runat="server" Text='<%#Eval("RowId") %>' Visible="false" />
                                    <asp:Label ID="lbSno" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbName" runat="server" Text='<%#Eval("Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel" SortExpression="ChannelId">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannelId" runat="server" Text='<%#Eval("ChannelId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme" SortExpression="ProgName">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgName" runat="server" Text='<%#Eval("ProgName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RoleName" SortExpression="RoleName">
                                <ItemTemplate>
                                    <asp:Label ID="lbRoleName" runat="server" Text='<%#Eval("RoleName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RoleDesc" SortExpression="RoleDesc">
                                <ItemTemplate>
                                    <asp:Label ID="lbRoleDesc" runat="server" Text='<%#Eval("RoleDesc") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rating" SortExpression="Rating">
                                <ItemTemplate>
                                    <asp:Label ID="lbRating" runat="server" Text='<%#Eval("Rating") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Likes" SortExpression="Likes">
                                <ItemTemplate>
                                    <asp:Label ID="lbLikes" runat="server" Text='<%#Eval("Likes") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Image" SortExpression="Pic">
                                <ItemTemplate>
                                    <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server" NavigateUrl='<%#Eval("Pic") %>'>
                                        <asp:Image ID="imgTvStarLogo" runat="server" ImageUrl='<%#Eval("Pic") %>' Width="100px"
                                            Height="100px" /></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modified" SortExpression="modifiedAt">
                                <ItemTemplate>
                                    <asp:Label ID="lbModifiedAt" runat="server" Text='<%#Eval("modifiedAt") %>' />
                                    <asp:Label ID="lbModifiedBy" runat="server" Text='<%#Eval("modifiedBy") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hyUpload" runat="server" Text="Upload Image" ImageUrl="../images/upload.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="../Images/Edit.png" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDSTvStarGallery" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                    <asp:SqlDataSource ID="sqlDSGenre" runat="server" SelectCommand="SELECT GenreName,GenreId, CASE GenreId WHEN '110' THEN 0 ELSE 1 END	 from mst_genre where genreCategory='S' order by 3,1"
                        ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'></asp:SqlDataSource>
                    <asp:SqlDataSource ID="sqlDSLanguage" runat="server" SelectCommand="select FullName,LanguageID, CASE LanguageID WHEN '2' THEN 0 ELSE 1 END	 from mst_language where active=1  order by 3,1"
                        ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'></asp:SqlDataSource>
                    <br />
                    <br />
                    <br />
                    <div class="clearfix">
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                function showDiv(jsControlId, jsRowId, jsFileName) {
                    try {
                        document.getElementById("MainContent_jsControlId").value = jsControlId;
                        document.getElementById("MainContent_jsRowId").value = jsRowId;
                        document.getElementById("MainContent_jsFileName").value = jsFileName;

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
                <input id="jsRowId" type="hidden" runat="server" />
                <input id="jsFileName" type="hidden" runat="server" />
                <input id="jsControlId" type="hidden" runat="server" />
                <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="#82CAFA"
                    CompleteBackColor="#FFFFFF" Filter="*.jpg;*.jpeg|Supported Images Types (*.jpg;*.jpeg)"
                    OnClientUploadComplete="clientMessage" OnUploadedComplete="AsyncFileUpload1_UploadedComplete"
                    ThrobberID="Image1" ProgressInterval="50" SaveBufferSize="128" Width="200px"
                    EnableProgress="OnSubmit, OnPreview" />
                <br />
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loader.gif" />
                <br />
            </div>
            <div id="black_overlay">
            </div>
            <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
