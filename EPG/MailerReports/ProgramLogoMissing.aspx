<%@ Page Title="Program Logo Missing" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="ProgramLogoMissing.aspx.vb" Inherits="EPG.ProgramLogoMissing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <strong>Program Logo Missing</strong>
            </div>
            <div class="panel-body">
                <div class="panel panel-default">
                    <div class="col-md-3">
                        <div class="panel panel-danger">
                            <div class="panel-heading text-center">
                                <strong>Dhiraagu</strong>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="grd1" runat="server" CssClass="table" DataSourceID="sqlDS1" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="panel panel-info">
                            <div class="panel-heading text-center">
                                <strong>Yupp</strong>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="grd2" runat="server" CssClass="table" DataSourceID="sqlDS2" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="panel panel-warning">
                            <div class="panel-heading text-center">
                                <strong>SPUUL</strong>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="grd3" runat="server" CssClass="table" DataSourceID="sqlDS3" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
					<div class="panel panel-default">
                            <div class="panel-heading text-center">
                                <strong>Star</strong>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="grd5" runat="server" CssClass="table" DataSourceID="sqlDS5" />
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading text-center">
                                <strong>Tablet Stream</strong>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="grd4" runat="server" CssClass="table" DataSourceID="sqlDS4" />
                            </div>
                        </div>
						
						
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS1" runat="server" SelectCommand="select ChannelID Channel_Name,ProgName Program_Name,
    convert(varchar,Airdate,106) Airing_Date from fn_php_mail_noprogramlogo (222) ORDER
    BY Airdate,ChannelId ,ProgName" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" />
    <asp:SqlDataSource ID="sqlDS2" runat="server" SelectCommand="select ChannelID Channel_Name,ProgName Program_Name,
    convert(varchar,Airdate,106) Airing_Date from fn_php_mail_noprogramlogo (199) ORDER
    BY Airdate,ChannelId ,ProgName" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" />
    <asp:SqlDataSource ID="sqlDS3" runat="server" SelectCommand="select ChannelID Channel_Name,ProgName Program_Name,
    convert(varchar,Airdate,106) Airing_Date from fn_php_mail_noprogramlogo (232) ORDER
    BY Airdate,ChannelId ,ProgName" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" />
    <asp:SqlDataSource ID="sqlDS4" runat="server" SelectCommand="select ChannelID Channel_Name,ProgName Program_Name,
    convert(varchar,Airdate,106) Airing_Date from fn_php_mail_noprogramlogo (248) ORDER
    BY Airdate,ChannelId ,ProgName" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" />
	
	<asp:SqlDataSource ID="sqlDS5" runat="server" SelectCommand="select ChannelID Channel_Name,ProgName Program_Name,
    convert(varchar,Airdate,106) Airing_Date from star_v_imagemissing ORDER
    BY Airdate,ChannelId ,ProgName" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" />
</asp:Content>
