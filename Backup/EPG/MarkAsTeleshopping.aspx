<%@ Page Title="Mark as Telshopping" Language="vb" MasterPageFile="~/SiteEPGBootstrap.Master"
    AutoEventWireup="false" CodeBehind="MarkAsTeleshopping.aspx.vb" Inherits="EPG.MarkAsTeleshopping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <%--    <asp:UpdatePanel ID="updProgramMaster" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Mark as Teleshopping</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Select Channel</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" DataSourceID="sqlDSChannel"
                                    DataTextField="Channelid" DataValueField="ChannelId" AutoPostBack="true" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="sqlDSChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT channelid from mst_channel where onair=1 order by 1"></asp:SqlDataSource>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Select Programme</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlProgram" runat="server" DataSourceID="sqlDSProgramme" DataTextField="ProgName"
                                    DataValueField="ProgId" CssClass="form-control" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="sqlDSProgramme" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT progid,progname from mst_program where progid in (select progid from mst_epg where channelid=@channelid) order by 2">
                                        <SelectParameters>
                                            <asp:ControlParameter Name="channelid" Type="String" Direction="Input" ControlID="ddlChannel" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <label>
                        </label>
                        <div class="input-group">
                            <asp:Button ID="btnMark" runat="server" Text="Mark as Teleshopping" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12 ">
                        <asp:GridView ID="grd" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" DataSourceID="sqlDS"
                            CssClass="table" AutoGenerateColumns="false">
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
                                <asp:CommandField ShowSelectButton="true" ButtonType="Image" Visible="false" SelectImageUrl="~/Images/delete.png" />
                                <asp:TemplateField HeaderText="Id" SortExpression="RowId">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRowId" runat="server" Text='<%# eval("RowId") %>' Visible="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Programme" SortExpression="ProgName">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProgName" runat="server" Text='<%# Bind("ProgName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="SELECT * from mst_exclude_programs order by 2"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
