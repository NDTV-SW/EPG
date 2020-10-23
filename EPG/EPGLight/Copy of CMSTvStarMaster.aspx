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
    </script>
    <script type="text/javascript">
//        function emptyMyAjaxFileUpload() {
//            $(".ajax__fileupload_queueContainer").empty();
//            $(".ajax__fileupload_topFileStatus").empty();
//        }
//        function uploadStart() {
//            var vName =  $("#MainContent_txtName").val();
//            var vCity = $("#MainContent_txtCity").val(); 
//            var vGender = $("#MainContent_ddlGender").val();
//            var vTwittertag = $("#MainContent_txtTwitter").val();
//            var vDob = $("#MainContent_txtDOB").val();
//            var vPob = $("#MainContent_txtPOB").val();
//            var vBiography = $("#MainContent_txtBiography").val();
////            var vName = document.getElementById('MainContent_txtName').value;
////            var vCity = document.getElementById('MainContent_txtCity').value;
////            var vGender = document.getElementById('MainContent_ddlGender').value;
////            var vTwittertag = document.getElementById('MainContent_txtTwitter').value;
////            var vDob = document.getElementById('MainContent_txtDOB').value;
////            var vPob = document.getElementById('MainContent_txtPOB').value;
////            var vBiography = document.getElementById('MainContent_txtBiography').value;
//            //var vInsertedby = document.getElementById('MainContent_txtName').value
//            //var vModifiedby = document.getElementById('MainContent_txtName').value
//            var vInsertedby = "kautilyar";
//            var vModifiedby = "kautilyar";
//            var vAction = "A";
//            var vProfileId = "0";
//            PageMethods.AddUpdate(vName, vCity, vGender, vTwittertag, vDob, vPob, vBiography, vInsertedby, vModifiedby, vAction,vProfileId, OnSuccess);
//        }
//        
//        function OnSuccess(response, userContext, methodName) {
//            alert(response);
//            $("#MainContent_lbFileName2").val(response.toString());
//            clearAll();
//            //document.getElementById('MainContent_lbProfileId').value = response;
//        }
//        function clearAll() {
//            $("#MainContent_txtName").val("");
//            $("#MainContent_txtCity").val("");
//            $("#MainContent_ddlGender").val("");
//            $("#MainContent_txtTwitter").val("");
//            $("#MainContent_txtDOB").val("");
//            $("#MainContent_txtPOB").val("");
//            $("#MainContent_txtBiography").val("");
//        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
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
                            <asp:Label ID="lbProfileId" runat="server" Visible="true" />
                            <asp:Label ID="lbFileName" runat="server"  Visible="true" />
                            
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
                        <div class="form-group">
                            <asp:Panel ID="pnlUpload" runat="server" Visible="True">
                                <div class="input-group form-control">
                                    <asp:AjaxFileUpload ID="AsyncFileUpload1" runat="server" MaximumNumberOfFiles="1"
                                            OnUploadComplete="OnUploadComplete" AllowedFileTypes="jpg,jpeg" OnClientUploadCompleteAll="emptyMyAjaxFileUpload" />
                                </div>
                        
                            </asp:Panel>
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

    
    

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
