<%@ Page Title="View Dialog Highlights" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="ViewHighlightsDialog.aspx.vb" Inherits="EPG.ViewHighlightsDialog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../lightbox.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            initLightbox();
        }
    </script>
<%--    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>--%>
            <div class="container">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                            <h3>
                                View Dialog Highlights</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <asp:GridView ID="grdHighLights" runat="server" AutoGenerateColumns="False" CssClass="table"
                                AllowPaging="True" AllowSorting="False" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                PageSize="50" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" >
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbId" Text='<%# Bind("rowID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("ChannelId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Programme" SortExpression="progname">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbProgName" Text='<%# Bind("progname") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Synopsis" SortExpression="synopsis">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbsynopsis" Text='<%# Bind("synopsis") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Starcast" SortExpression="StarCast">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbStarCast" Text='<%# Bind("StarCast") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Time" SortExpression="istDate">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbISTDate" Text='<%# Bind("istDate") %>' />
                                            <asp:Label runat="server" ID="lbISTTime" Text='<%# Bind("istTime") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Genre" SortExpression="Genre">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbGenre" Text='<%# Bind("Genre") %>' />
                                            <asp:Label runat="server" ID="lbPoster" Text='<%# Bind("poster") %>' Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:HyperLink rel="lightbox" ID="hyLogo" NavigateUrl='<%# Bind("poster") %>' runat="server">
                                                <asp:Image runat="server" ID="imglogo" AlternateText="NDTV" Width="50px" Height="50px"
                                                    ImageUrl='<%# Bind("poster") %>' />
                                            </asp:HyperLink></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Images/remove.png" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
