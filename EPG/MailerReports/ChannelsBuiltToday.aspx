<%@ Page Title="Channels Built Today" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="ChannelsBuiltToday.aspx.vb" Inherits="EPG.ChannelsBuiltToday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <strong>Channels Built Today</strong>
            </div>
            <div class="panel-body">
				<div class="col-md-8 col-md-offset-2">
					<asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlDS" 
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
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
    <asp:SqlDataSource ID="sqlDS" runat="server"
     SelectCommand="select * from v_channels_built_today order by 2"
     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
</asp:Content>
