<%@ Page Title="FPC EPG Auto" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="FPC_EPG_Auto.aspx.vb" Inherits="EPG.FPC_EPG_Auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        FPC EPG Auto</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" AutoPostBack="true" DataSourceID="SqlDsChannelMaster" DataTextField="channelid" DataValueField="channelid" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-info" Text="Upload" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-heading text-center">
                    <div class="btn-lg alert-info">
                        Channel Data</div>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:GridView ID="grdExcelData" runat="server" AllowPaging="True" AllowSorting="True"
                            CssClass="table" CellPadding="4" ForeColor="Black" GridLines="Vertical" PagerSettings-PageButtonCount="10"
                            PageSize="100" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select '--select--' channelid union select Distinct channelid from fpc_epg ORDER BY ChannelId" />
</asp:Content>
