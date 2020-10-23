<%@ Page Title="Pic Gallery Description" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master" CodeBehind="PicGallery.aspx.vb" Inherits="EPG.PicGallery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        #black_overlay{
            display: none;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index:1001;
            -moz-opacity: 0.8;
            opacity:.80;
            filter: alpha(opacity=80);
        }
        #popme {
            display: none;
            position: fixed;
            top: 35%;
            left: 35%;
            width: 25%;
            height: 20%;
            padding: 16px;
            border: 5px solid orange;
            background-color: white;
            z-index:1002;
            overflow: auto;
        }
        #popme1
        {
            position: fixed;
            top: 20%;
            left: 22%;
            width:200;
            
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
            display:none;
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
                $("#" + vimgControlid).attr("src", "../uploads/gallerypics/" + $("#MainContent_jsFileName").val());
                $("#MainContent_grdPicGallery_imglogo_1").attr("src", $("#txtEmail").val());
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
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            initLightbox();
        }
    </script>
    <asp:UpdatePanel ID="upd" runat="server">
    <ContentTemplate>
        <br />
        <div class="row">
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                <h3>Picture Gallery Description</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                &nbsp;
            </div>
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                <div class="row">  
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Select Gallery</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlGallery" runat="server" DataSourceID="sqlDSGallery" DataTextField="GalleryName" DataValueField="galleryID" CssClass="form-control"/>
                                <asp:SqlDataSource id="sqlDSGallery" runat="server" SelectCommand="select a.galleryID, c.categoryname + ' (' + b.Name + ')' GalleryName from picgallery_master  a join mst_tvstars b on a.profileid=b.ProfileID join picgallery_category c on a.categoryid=c.categoryid order by 2"
                                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Upload Image To Gallery</label>
                            <div class="input-group form-control">
                                <asp:AjaxFileUpload ID="AsyncFileUpload1" runat="server" MaximumNumberOfFiles="10"
                                            OnUploadComplete="OnUploadComplete" AllowedFileTypes="jpg,jpeg" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" />
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                            <asp:Label ID="lbRowId" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                &nbsp;  
            </div>
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 ">
                    <hr />
                    <h3>REPORT</h3>
                        <asp:GridView ID="grdPicGallery" runat="server" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="sqlDSPicGallery"
                            CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="500" AllowPaging="true"
                            BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true" EmptyDataText="----No Record Found----"
                            EmptyDataRowStyle-HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSno" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gallery ID" SortExpression="categoryid">
                                    <ItemTemplate>
                                        <asp:Label ID="lbGalleryId" runat="server" Text='<%#Eval("galleryId") %>' />
                                        <asp:Label ID="lbPicId" runat="server" Text='<%#Eval("PicId") %>' Visible="false" />
                                        <asp:Label ID="lblanguageId" runat="server" Text='<%#Eval("languageId") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image" SortExpression="profilePath">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyLogo" rel="lightbox" runat="server" NavigateUrl='<%# Bind("profilePath1") %>'>
                                            <asp:Image ID="imgCelebrityImage" runat="server" ImageUrl='<%# Bind("profilePath1") %>' Width="100px" Height="100px" />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modified" SortExpression="ModifiedBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lbModifiedBy" runat="server" Text='<%#Eval("ModifiedBy") %>' />
                                        <asp:Label ID="lbdateModified" runat="server" Text='<%#Eval("dateModified") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="../Images/Edit.png"/>
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
                        <%--<asp:SqlDataSource ID="sqlDSPicGallery" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="select 'http://localhost:7799/' + url profilePath1,* from picgallery_pictures"
                            SelectCommandType="Text" />--%>
                        <asp:SqlDataSource ID="sqlDSPicGallery" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="select galleryId from picgallery_master"
                            SelectCommandType="Text" />
                            
                        <br /><br /><br />
                        <div class="clearfix">
                        </div>
                    </div>
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                &nbsp;
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
        <button type="button" style="float:right" onclick="hideDiv();">X</button><br />
        
        <input type="hidden" id="jsTmdbCelebrityId" size="50" runat="server" />
        <input type="hidden" id="jsImageId" size="50" />
        <input type="hidden" id="jsControlId" size="50" />
        <input type="hidden" id="jsFileName" size="50" runat="server" />

        <asp:AsyncFileUpload ID="AsyncFileUpload2" runat="server"
		    UploadingBackColor="#82CAFA"
		    CompleteBackColor = "#FFFFFF"
		    Filter="*.jpg;*.jpeg|Supported Images Types (*.jpg;*.jpeg)"
		    OnClientUploadComplete="clientMessage"
            onuploadedcomplete="AsyncFileUpload1_UploadedComplete" 
		    ThrobberID="Image1"
            ProgressInterval="50"
            SaveBufferSize="128"
            Width="200px"
            EnableProgress="OnSubmit, OnPreview" /><br />
            <span style="color:Red" >only .jpg and .jpeg files supported</span>
        <asp:Image ID="Image1" runat="server" ImageUrl = "~/Images/loader.gif"/>
            <br />
    </div>
    <div id="black_overlay">
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
