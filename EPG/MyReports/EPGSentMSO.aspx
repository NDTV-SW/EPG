<%@ Page Title="EPG Sent MSO" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="EPGSentMSO.aspx.vb" Inherits="EPG.EPGSentMSO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    EPG Sent To Operators</h3>
            </div>
            <div class="panel-body">
                <asp:GridView ID="grd" runat="server" BackColor="White" BorderColor="#DEDFDE" AllowSorting="true" DataSourceID="sqlDS"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" CssClass="table"
                    GridLines="Vertical" >
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

    <asp:SqlDataSource id="sqlDS" runat="server" ConnectionString="<%$ConnectionStrings:EpgConnectionString1 %>"
    SelectCommand="select operatorid [Id],Name,convert(varchar,lastupdated,106) +' ' + convert(varchar,lastupdated,108) [Last Sent],EpgType [Type], left(convert(varchar,TimeToSend1,108),5) + ', ' + left(convert(varchar,TimeToSend2,108),5) + ', ' + left(convert(varchar,TimeToSend3,108),5) + ', ' + left(convert(varchar,TimeToSend4,108),5) TimeToSend, daysTosend,FTP,AppNo,[TimeTaken (Mins)]=(select top 1 timetakenmins from mst_dthcableoperators_timeTaken where operatorid=x.operatorid order by rowid desc) from mst_dthcableoperators x where active=1 order by lastupdated desc" />
</asp:Content>
