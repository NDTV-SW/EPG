<%@ Page Title="Duplicate Movies Details" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="DuplicateMoviesDetails.aspx.vb" Inherits="EPG.DuplicateMoviesDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <strong>EPG Due for Airtel</strong>
            </div>
            <div class="panel-body">
                <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlDS">
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server"
     SelectCommand="select distinct progname, progdate, progtime, channelid, duration, engsynopsis from vw_duplicate_movie_airtel order by progdate,progtime"
     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
</asp:Content>
