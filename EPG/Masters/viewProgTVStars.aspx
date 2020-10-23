<%@ Page Title="View Programme Tv Stars" Language="vb" MasterPageFile="~/SiteLess.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="viewProgTVStars.aspx.vb"
    Inherits="EPG.viewProgTVStars" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../lightbox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    View Programme TV Stars</h3>
            </div>
            <div class="panel-body container">
                
                    <asp:GridView ID="grdData" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        PageSize="50" AllowPaging="true" AllowSorting="true" EmptyDataText="--- no record found ---"
                        AutoGenerateColumns="false">
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
                            <asp:TemplateField HeaderText="S.no.">
                                <ItemTemplate>
                                    <asp:Label ID="lbSNO" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="ChannelId">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgId" runat="server" Text='<%#Eval("ProgId") %>' Visible="false" />
                                    <asp:Label ID="lbProfileId" runat="server" Text='<%#Eval("ProfileId") %>' Visible="false" />
                                    <asp:Label ID="lbName" runat="server" Text='<%#Eval("Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role" SortExpression="RoleName">
                                <ItemTemplate>
                                    <asp:Label ID="lbRoleName" runat="server" Text='<%#Eval("RoleName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role Desc" SortExpression="RoleDesc">
                                <ItemTemplate>
                                    <asp:Label ID="lbRoleDesc" runat="server" Text='<%#Eval("RoleDesc") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Profile Pic">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hyProfileStarPic" runat="server" rel="lightbox" NavigateUrl='<%#EVAL("ProfileStarPic") %>'>
                                        <asp:Image ID="imgProfileStarPic" runat="server" ImageUrl='<%#EVAL("ProfileStarPic") %>'
                                            Width="50px" Height="50px" />
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme Pic">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hyProgStarPic" runat="server" rel="lightbox" NavigateUrl='<%#EVAL("ProgStarPic") %>'>
                                        <asp:Image ID="imgProgStarPic" runat="server" ImageUrl='<%#EVAL("ProgStarPic") %>'
                                            Width="50px" Height="50px" />
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
               
            </div>
        </div>
</asp:Content>
