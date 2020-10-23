<%@ Page Title="Mails Dashboard" Language="vb" MasterPageFile="SiteStar.Master" AutoEventWireup="false"
    CodeBehind="MailsReceivedDashboard.aspx.vb" Inherits="EPG.MailsReceivedDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info text-center">
        <div class="panel-heading">
            <h2>
                Mails Received Dashboard
            </h2>
        </div>
        <div class="panel-body">
            <asp:GridView ID="grd" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                DataSourceID="sqlDS" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Vertical" AutoGenerateColumns="True">
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
                    <asp:CommandField ButtonType="Image" ShowSelectButton="true" SelectImageUrl="~/Images/edit.png" />
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select a.channelid,a.mailsubject,a.mailreceivedby,a.startdate, a.enddate,a.mailreceivedat,min(b.lastupdate) uploadedat from fpc_ChannelEPGMailsReceived a  join mst_build_epg_transactions b on a.channelid=b.channelid where b.epgdate between a.startdate and a.enddate and b.lastupdate>a.mailreceivedat group by a.channelid,a.startdate, a.enddate,a.mailreceivedat,a.mailreceivedby,a.mailsubject" />
    
</asp:Content>
