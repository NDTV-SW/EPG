<%@ Page Title="Airtel Missing Info" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="AirtelMissingInfo.aspx.vb" Inherits="EPG.AirtelMissingInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <strong>Airtel Missing Info</strong>
            </div>
            <div class="panel-body">
                <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlDS">
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server"
     SelectCommand="SELECT * FROM vw_php_job_mail_epg_missing_info ORDER BY Progdate , channelId ,Progtime"
     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
</asp:Content>
