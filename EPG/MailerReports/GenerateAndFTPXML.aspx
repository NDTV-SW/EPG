<%@ Page Title="Need to generate and FTP XML" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="GenerateAndFTPXML.aspx.vb" Inherits="EPG.GenerateAndFTPXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <strong>Need to generate and FTP XML</strong>
            </div>
            <div class="panel-body">
                <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlDS">
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server"
     SelectCommand="SELECT * FROM vw_php_job_mail_xml_alert ORDER BY FTPdatetime ,ChannelId"
     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
</asp:Content>
