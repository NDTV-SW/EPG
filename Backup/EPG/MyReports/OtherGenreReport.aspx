<%@ Page Title="Other Genre Yupp" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="OtherGenreReport.aspx.vb" Inherits="EPG.OtherGenreReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                   Yupp Channel Programmes with <b>Others</b> as Genre</h3>
            </div>
            <div class="panel-body">
              
                    <asp:GridView ID="grdReport" CssClass="table" runat="server" ShowFooter="True" AutoGenerateColumns="false"
                        CellPadding="4" EmptyDataText="No record found !" ForeColor="Black" GridLines="Vertical"
                        Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                        PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" 
                        Font-Names="Verdana" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
                        BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />   
                        <FooterStyle BackColor="#CCCC99" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                        <PagerSettings NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/Prev.gif"></PagerSettings>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbProgid" runat="server" Text='<%#Bind("progid") %>' Visible="false" />
                                    <asp:Label ID="lbProgNewGenre" runat="server" Text='<%#Bind("ProgNewGenre") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Channel" DataField="channelid" />
                            <asp:BoundField HeaderText="Programme" DataField="progname" />
                            <asp:BoundField HeaderText="Synopsis" DataField="Synopsis" />
                            <asp:BoundField HeaderText="Current Genre" DataField="currentGenre" />
                            <asp:BoundField HeaderText="New Genre" DataField="prognewgenrename" />
                            <asp:TemplateField HeaderText="Select Genre">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlGenre" runat="server" DataTextField="GenreName" DataValueField="GenreId" DataSourceID="sqlDSGenre" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info btn-xs"
                                        CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' CommandName="updategenre" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDSGenre" runat="server" SelectCommand="select genreid,genrename from mst_genre where genreCategory='P' order by 2"
                      ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
            </div>
        </div>
    </div>
</asp:Content>
