<%@ Page Title="DVB Genre Mapping" Language="vb" MasterPageFile="SiteAdmin.Master"
    AutoEventWireup="false" CodeBehind="DVBGenreMapping.aspx.vb" Inherits="EPG.DVBGenreMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    DVB Genre Mapping</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                NDTV Genre</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlNDTVGenre" DataSourceID="sqlDSNDTVGenre" runat="server"
                                    CssClass="form-control" DataTextField="genrename" DataValueField="genreid" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="sqlDSNDTVGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>"
                                        SelectCommand="Select genreid,genrename from mst_genre where genreCategory='p' and genreid not in(select ndtvgenreid from dvb_genre_mapping) order by 2" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                DVB Genre</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlDVBGenre" DataSourceID="sqlDSDVBGenre" runat="server" CssClass="form-control"
                                    DataTextField="genrename" DataValueField="genreid" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="sqlDSDVBGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>"
                                        SelectCommand="Select genreid,genrename from dvb_genre order by 2" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-info" Text="Add" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grd" runat="server" BackColor="White" BorderColor="#DEDFDE" CssClass="table"
                        DataSourceID="sqlDS" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" AutoGenerateColumns="false"
                        GridLines="Vertical" EmptyDataText="No record Found" EmptyDataRowStyle-CssClass="alert alert-danger">
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
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/delete.png" ShowSelectButton="true" />
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbID" runat="server" Text='<%#Bind("rowid") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="dvbgenreid" HeaderText="DVB ID" />
                            <asp:BoundField DataField="genrename" HeaderText="DVB Genre" />
                            <asp:BoundField DataField="ndtvgenreid" HeaderText=" NDTV ID" />
                            <asp:BoundField DataField="NDTVGenre" HeaderText="NDTV Genre" />
                            			
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>"
                        SelectCommand="select a.rowid,a.dvbgenreid,b.genrename,a.ndtvgenreid,c.GenreName [NDTVGenre] from dvb_genre_mapping a join dvb_genre b on a.dvbgenreid=b.genreid join mst_genre c on a.ndtvgenreid=c.genreid order by 3,4" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
