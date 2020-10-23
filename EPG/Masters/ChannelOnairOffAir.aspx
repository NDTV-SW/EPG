<%@ Page Title="Channel On-Air/Off-Air" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="ChannelOnairOffAir.aspx.vb" Inherits="EPG.ChannelOnairOffAir" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--<div class="container">--%>
    <div class="panel panel-default">
        <div class="panel-body">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="alert-danger h4">
                    Note:-<br />
                    Off-Air Channels in Green have EPG data available<br />
                    Channels in Red have EPG data available Equal or Less than Tomorrow.<br />
                    Channels in Orange have Mail to Airtel as True.
                </div>
            </div>
            <%--<asp:Table ID="tbChannelOnairOffAir" runat="server" Width="95%">
    <asp:TableRow>
        <asp:TableCell Width="30%" HorizontalAlign="Center" VerticalAlign="Top">
            <fieldset>
                <legend>Airtel FTP Channels</legend>--%>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                        Airtel-FTP Channels</h3>
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="grdAirtelOnair" runat="server" CellPadding="4" DataSourceID="sqlDSgrdAirtelOnAir"
                            ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" AllowSorting="True"
                            CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" Font-Size="Small" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="True">
                                    <ItemTemplate>
                                        <asp:Button ID="btnOffAir" Text="Off-Air" CommandName="OffAir" runat="server" CssClass="btn btn-sm btn-danger"
                                            CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S.No." Visible="True" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSno" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RowId" Visible="false" SortExpression="RowId">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On-Air Channels" SortExpression="ChannelId">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Airtel Mail" SortExpression="AirtelMail">
                                    <ItemTemplate>
                                        <asp:Label ID="lbAirtelMail" runat="server" Text='<%# Bind("AirtelMail") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EPG Till" SortExpression="EPGAvailableTill">
                                    <ItemTemplate>
                                        <asp:Label ID="lbEPGAvailableTill" runat="server" Text='<%# Bind("EPGAvailableTill") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%--</fieldset>
        </asp:TableCell>
        <asp:TableCell Width="30%" HorizontalAlign="Center" VerticalAlign="Top">
            <fieldset>
        <legend>On-Air Channels</legend>--%>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
            <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                        On-Air Channels</h3>
                    </div>
                    <div class="panel-body">
                <asp:GridView ID="grdChannelOnAir" runat="server" CellPadding="4" DataSourceID="sqlDSgrdChannelOnAir"
                    ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" AllowSorting="True"
                    CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" Font-Size="Small" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                    <Columns>
                        <asp:TemplateField HeaderText="" Visible="True">
                            <ItemTemplate>
                                <asp:Button ID="btnOffAir" Text="Off-Air" CommandName="OffAir" runat="server" CssClass="btn btn-sm btn-danger"
                                    CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="True" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbSno" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RowId" Visible="false" SortExpression="RowId">
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="On-Air Channels" SortExpression="ChannelId">
                            <ItemTemplate>
                                <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Airtel Mail" SortExpression="AirtelMail">
                            <ItemTemplate>
                                <asp:Label ID="lbAirtelMail" runat="server" Text='<%# Bind("AirtelMail") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EPG Till" SortExpression="EPGAvailableTill">
                            <ItemTemplate>
                                <asp:Label ID="lbEPGAvailableTill" runat="server" Text='<%# Bind("EPGAvailableTill") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
                </div>
                
            </div>
            <%--</fieldset>
        </asp:TableCell>
        <asp:TableCell Width="20%" HorizontalAlign="Center" VerticalAlign="Top">
            <fieldset>
        <legend>AnyTime Channels</legend>--%>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
            <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                        Anytime Channels</h3>
                    </div>
                    <div class="panel-body">
                <asp:GridView ID="grdAnyTime" runat="server" CellPadding="4" DataSourceID="sqlDSgrdAnytime"
                    ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" AllowSorting="True"
                    CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" Font-Size="Small" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." Visible="True" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbSno" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RowId" Visible="false" SortExpression="RowId">
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Off-Air Channels" SortExpression="ChannelId">
                            <ItemTemplate>
                                <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Airtel FTP" SortExpression="AirtelFTP">
                            <ItemTemplate>
                                <asp:Label ID="lbAirtelFTP" runat="server" Text='<%# Bind("AirtelFTP") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EPG Till" SortExpression="EPGAvailableTill">
                            <ItemTemplate>
                                <asp:Label ID="lbEPGAvailableTill" runat="server" Text='<%# Bind("EPGAvailableTill") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
                </div>
            </div>
            <%--</fieldset>
        </asp:TableCell>

        <asp:TableCell Width="20%" HorizontalAlign="Center" VerticalAlign="Top">
            <fieldset>
        <legend>Off-Air Channels</legend>--%>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
            <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                        Off-Air Channels</h3>
                    </div>
                    <div class="panel-body">
                <asp:GridView ID="grdChannelOffAir" runat="server" CellPadding="4" DataSourceID="sqlDSgrdChannelOffAir"
                    ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" AllowSorting="True"
                    CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" Font-Size="Small" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                    <Columns>
                        <asp:TemplateField HeaderText="" Visible="True">
                            <ItemTemplate>
                                <asp:Button ID="btnOnAir" Text="On-Air" CommandName="OnAir" runat="server" CssClass="btn btn-sm btn-success"
                                    CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No." Visible="True" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbSno" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RowId" Visible="false" SortExpression="RowId">
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Off-Air Channels" SortExpression="ChannelId">
                            <ItemTemplate>
                                <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EPG Till" SortExpression="EPGAvailableTill">
                            <ItemTemplate>
                                <asp:Label ID="lbEPGAvailableTill" runat="server" Text='<%# Bind("EPGAvailableTill") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Color" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbColor" runat="server" Text='<%# Bind("Color") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
                </div>
            </div>
            <%--</fieldset>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
            <asp:SqlDataSource ID="sqlDSgrdAirtelOnAir" runat="server" SelectCommandType="Text"
                SelectCommand="select x.RowId, x.ChannelId,x.airtelFTP,x.airtelMail,(select convert(varchar,max(progdate),106) from mst_epg where channelid=x.channelid) EPGAvailableTill from mst_channel x where x.Active=1 and x.Onair=1 and x.airtelFTP=1 order by x.ChannelId"
                ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlDSgrdChannelOnAir" runat="server" SelectCommandType="Text"
                SelectCommand="select x.RowId, x.ChannelId,x.airtelFTP,x.airtelMail,(select convert(varchar,max(progdate),106) from mst_epg where channelid=x.channelid) EPGAvailableTill from mst_channel x where x.Active=1 and x.Onair=1 and x.airtelFTP=0 order by x.ChannelId"
                ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlDSgrdAnytime" runat="server" SelectCommandType="Text" SelectCommand="select  x.rowid,x.ChannelId,x.airtelftp,(select convert(varchar,max(progdate),106) from mst_epg where channelid=x.channelid) EPGAvailableTill from mst_channel x where x.Active=1 and x.catchupFlag=1 order by x.ChannelId"
                ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlDSgrdChannelOffAir" runat="server" SelectCommandType="StoredProcedure"
                SelectCommand="sp_offair_channels_details" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
            </asp:SqlDataSource>
        </div>
    </div>
    <%--</div>--%>
</asp:Content>
