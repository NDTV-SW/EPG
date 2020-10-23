<%@ Page Title="TV Star Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master" CodeBehind="CMSTvStarMaster.aspx.vb" Inherits="EPG.CMSTvStarMaster" %>
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
            width: 40%;
            height: 35%;
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
        function showGallery(jsGalleryId) {
            window.open("viewPicGallery.aspx?Galleryid=" + jsGalleryId, "View Gallery", "width=800,height=500,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
        function hideDiv() {
            $("#popme").slideUp(200);
            $("#black_overlay").slideUp(200);
        }

        function clientMessage() {
            try {

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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
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
                <h3>TV Star Master</h3>
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
                            <label>Name</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ValidationGroup="VGTvStar" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Twitter Tag</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtTwitter" runat="server" CssClass="form-control" ValidationGroup="VGTvStar" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>BioGraphy</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtBiography" runat="server" CssClass="form-control" ValidationGroup="VGTvStar" TextMode="MultiLine" Rows="3" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>City</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" ValidationGroup="VGTvStar" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Date of Birth</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" ValidationGroup="VGBigTVSearch" />        
                                <span class="input-group-addon">
                                <asp:CalendarExtender ID="CE_txtDOB" runat="server" PopupButtonID="img_txtDOB" TargetControlID="txtDOB"  />
                                <asp:Image ID="img_txtDOB" runat="server" ImageUrl="~/Images/calendar.png" /></span>
                            </div>
                        </div>
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info"  />
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                            
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Gender</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Male" Value="M" />
                                    <asp:ListItem Text="Fe-Male" Value="F" />
                                    <asp:ListItem Text="Trans Gender" Value="T" />
                                </asp:DropDownList>
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label>Place of Birth</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPOB" runat="server" CssClass="form-control" ValidationGroup="VGTvStar" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>    
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                        <hr />
                        <h3>SEARCH</h3>     
                        <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12 ">
                            <div class="form-group">
                                <label></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ValidationGroup="VGBigTVSearch" />        
                                    <span class="input-group-addon"></span>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12 ">
                            <div class="form-group">
                                <label></label>
                                <div class="input-group">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="VGBigTVSearch" CssClass="btn btn-info" />
                                </div>
                            </div>
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
                        <asp:GridView ID="grdTvStar" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                            CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="500" AllowPaging="true"
                            BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true" EmptyDataText="----No Record Found----"
                            EmptyDataRowStyle-HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProfileId" runat="server" Text='<%#Eval("profileId") %>' Visible="false" />
                                        <asp:Label ID="lbSno" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbName" runat="server" Text='<%#Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City" SortExpression="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCity" runat="server" Text='<%#Eval("City") %>' />
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
                                        <asp:Label ID="lbDOB" runat="server" Text='<%#Eval("DOB") %>' />
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
                                            <asp:Image ID="imgCelebrityImage" runat="server" ImageUrl='<%# Bind("profilePath1") %>' Width="100px" Height="100px" />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modified" SortExpression="modifiedAt">
                                    <ItemTemplate>
                                        <asp:Label ID="lbModifiedAt" runat="server" Text='<%#Eval("modifiedAt") %>' />
                                        <asp:Label ID="lbModifiedBy" runat="server" Text='<%#Eval("modifiedBy") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="../Images/Edit.png"/>
                                <asp:ButtonField ButtonType="Button" CommandName="Upload" HeaderText="Upload Image" Text="Upload Image"/>
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
                        <asp:SqlDataSource ID="sqlDSTvStar" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                        <br /><br /><br />
                        <div class="clearfix">
                        </div>
                    </div>
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                &nbsp;
            </div>
        </div>
        <script>
            function showDiv(jsGalleryId) {
                try {
                    document.getElementById("MainContent_jsGalleryId").value = jsGalleryId;
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
        
        <input type="hidden" id="jsGalleryId" size="50" runat="server" />
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
        <span style="color:Red" >only .jpg and .jpeg files supported</span>
    </div>
    <div id="black_overlay">
    </div>
            
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
