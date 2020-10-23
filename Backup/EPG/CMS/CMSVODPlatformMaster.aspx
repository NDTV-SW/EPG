<%@ Page Title="VOD Platform Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="CMSVODPlatformMaster.aspx.vb" Inherits="EPG.CMSVODPlatformMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <br />
            <div class="container">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h3>
                                Platform Master</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Company</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlCompany" CssClass="form-control" runat="server" DataSourceID="sqlDSChannel"
                                                DataTextField="CompanyName" DataValueField="CompanyId" />
                                            <asp:SqlDataSource ID="sqlDSChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="Select companyId,CompanyName from mst_company where active=1 order by 2" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtName" placeholder="Platform Name" runat="server" CssClass="form-control" ValidationGroup="VODPlatform"/>
                                            <span class="input-group-addon">
                                                <asp:RequiredFieldValidator ID="RFVtxtName" runat="server" ControlToValidate="txtName" ErrorMessage="Name Required" Text="*"  ForeColor="Red" ValidationGroup="VODPlatform" />
                                                </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            URL</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtWebSiteURL" runat="server" CssClass="form-control" ValidationGroup="VODPlatform" />    
                                            <span class="input-group-addon"><asp:RequiredFieldValidator ID="RFVtxtWebSiteURL" runat="server" ControlToValidate="txtWebSiteURL" ErrorMessage="URL required" Text="*" ForeColor="Red" ValidationGroup="VODPlatform" /></span>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VODPlatform" />
                                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                        <asp:Label ID="lbPlatformId" runat="server" Visible="false" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <asp:ValidationSummary ID="VS" runat="server" ValidationGroup="VODPlatform" CssClass="alert-danger" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-3 col-md-3">
                                    <asp:TextBox ID="txtSearch" placeholder="search platform" runat="server" CssClass="form-control" ValidationGroup="VODPlatformSearch" />
                                </div>
                                <div class="col-lg-3 col-md-3">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="VODPlatformSearch" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                <asp:GridView ID="grd" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                                    CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="20"
                                    AllowPaging="true" BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true"
                                    EmptyDataText="----No Record Found----" EmptyDataRowStyle-HorizontalAlign="Center">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbCompanyId" runat="server" Text='<%#Eval("CompanyId") %>' Visible="false" />
                                                <asp:Label ID="lbPlatformID" runat="server" Text='<%#Eval("PlatformId") %>' Visible="false" />
                                                <asp:Label ID="lbSno" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company" SortExpression="CompanyName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbCompanyName" runat="server" Text='<%#Eval("CompanyName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" SortExpression="PlatformName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPlatformName" runat="server" Text='<%#Eval("PlatformName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Website URL" SortExpression="websiteURL">
                                            <ItemTemplate>
                                                <asp:Label ID="lbwebsiteURL" runat="server" Text='<%#Eval("websiteURL") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="../Images/Edit.png" />
                                        <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="../Images/delete.png" />
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
                                <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
