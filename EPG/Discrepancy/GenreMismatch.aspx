<%@ Page Title="Genre Mismatch" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master" AutoEventWireup="false" CodeBehind="GenreMismatch.aspx.vb" Inherits="EPG.GenreMismatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>Genre Mismatch</h3>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grd" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" DataSourceID="sqlDS"
                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table" AutoGenerateColumns="false">
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
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbSProgid" runat="server" Text='<%#Bind("SprogId") %>' Visible="false" />
                                    <asp:Label ID="lbTProgid" runat="server" Text='<%#Bind("TProgid") %>' Visible="false" />
                                    <asp:Label ID="lbSourceG" runat="server" Text='<%#Bind("SourceG") %>' Visible="false" />
                                    <asp:Label ID="lbTargetG" runat="server" Text='<%#Bind("TargetG") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source Channel">
                                <ItemTemplate>
                                    <asp:Label ID="lbSourceChannel" runat="server" Text='<%#Bind("Source") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source Programme">
                                <ItemTemplate>
                                    <asp:Label ID="lbSProgName" runat="server" Text='<%#Bind("SProgName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source Genre">
                                <ItemTemplate>
                                    <asp:Label ID="lbSourceGName" runat="server" Text='<%#Bind("SourceGName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source Duration">
                                <ItemTemplate>
                                    <asp:Label ID="lbSourceDuration" runat="server" Text='<%#Bind("SourceDuration") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Copy Genre -->">
                                <ItemTemplate>
                                    <asp:Button ID="btnUpdate1" runat="server" Text="Source to Target -->" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' CommandName="sourcetotarget" CssClass="btn btn-info" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Channel">
                                <ItemTemplate>
                                    <asp:Label ID="lbTargetChannel" runat="server" Text='<%#Bind("Target") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Programme">
                                <ItemTemplate>
                                    <asp:Label ID="lbTProgName" runat="server" Text='<%#Bind("TProgName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Genre">
                                <ItemTemplate>
                                    <asp:Label ID="lbTargetGName" runat="server" Text='<%#Bind("TargetGName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Duration">
                                <ItemTemplate>
                                    <asp:Label ID="lbTargetDuration" runat="server" Text='<%#Bind("TargetDuration") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<-- Copy Genre">
                                <ItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="<-- Target to Source" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' CommandName="targettosource" CssClass="btn btn-success" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                        SelectCommand="select * from v_GenreMismatch where sourceG<>TargetG order by 2,5"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
