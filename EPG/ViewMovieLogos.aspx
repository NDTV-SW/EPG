<%@ Page Title="View Movie Logos" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="ViewMovieLogos.aspx.vb" Inherits="EPG.ViewMovieLogos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
        <link href="lightbox.css" rel="stylesheet" type="text/css" />
        <script src="lightbox.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <fieldset>
        <legend>View Movie Logos</legend>
            <div style="text-align:center">
                <table width="50%">
                    <tr>
                        <td>
                            Search Text
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearch" runat="server" Width="250px" /></td>
                        <td>
                            <asp:CheckBox ID="chkExact" Text="Exact Match" runat="server" Checked="false" AutoPostBack="True" />
                        </td>
                        <td><asp:Button ID="btnSearch" runat="server" Text="Search" /></td>
                    </tr>
                </table>
                <table>
                <tr>
                <td colspan="5" align="center">
                    <asp:GridView ID="grdProgImage" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sqlDSProgImage" AllowPaging="True" AllowSorting="True" 
                        CellPadding="4" ForeColor="Black" GridLines="Vertical" PageSize="100" 
                          BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"><AlternatingRowStyle 
                              BackColor="White" /><Columns>
                            <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbProgId" Text='<%# Bind("RowId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MovieName" SortExpression="MovieName">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbMovieName" Text='<%# Bind("MovieName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Synopsis" SortExpression="Synopsis">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbSynopsis" Text='<%# Bind("Synopsis") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="StarCast" SortExpression="StarCast" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbStarCast" Text='<%# Bind("StarCast") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="LanguageId" SortExpression="LanguageId" Visible="false"  >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbLanguageId" Text='<%# Bind("LanguageId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ReleaseYear" SortExpression="ReleaseYear" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbReleaseYear" Text='<%# Bind("ReleaseYear") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="TMDBImageURL" SortExpression="TMDBImageURL" Visible="false" >
                                <ItemTemplate>  
                                    <asp:Label runat="server" ID="lbTMDBImageURL" Text='<%# Bind("TMDBImageURL") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Director" SortExpression="Director" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbDirector" Text='<%# Bind("Director") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Country" SortExpression="Country" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbCountry" Text='<%# Bind("Country") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MovieLangId" SortExpression="color"  Visible="false" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbMovieLangId" Text='<%# Bind("MovieLangId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PremierDate" SortExpression="PremierDate"  Visible="false" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbPremierDate" Text='<%# Bind("PremierDate") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="PremierChannelId" SortExpression="PremierChannelId"  Visible="false" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbPremierChannelId" Text='<%# Bind("PremierChannelId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Genre" SortExpression="Genre" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbGenre" Text='<%# Bind("Genre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Language" SortExpression="FullName" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbFullName" Text='<%# Bind("FullName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Active" SortExpression="Active"  Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkActive" Text='<%# Bind("Active") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                        <asp:Image runat="server" ID="imglogo"  AlternateText="NDTV" Width="50px" Height="50px" />
                                    </asp:HyperLink></ItemTemplate></asp:TemplateField>

                        </Columns>
                        <FooterStyle BackColor="#CCCC99" /><HeaderStyle BackColor="#6B696B" 
                              Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#F7F7DE" 
                              ForeColor="Black" HorizontalAlign="Right" /><RowStyle BackColor="#F7F7DE" /><SelectedRowStyle 
                              BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle 
                              BackColor="#FBFBF2" /><SortedAscendingHeaderStyle BackColor="#848384" /><SortedDescendingCellStyle 
                              BackColor="#EAEAD3" /><SortedDescendingHeaderStyle BackColor="#575357" /></asp:GridView>
                    <asp:SqlDataSource ID="sqlDSProgImage" runat="server" SelectCommandType="Text"
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" >
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
            </div>
    </fieldset>
     </asp:Content>