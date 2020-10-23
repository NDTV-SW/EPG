<%@ Page Title="Star Manual Mail Alerts" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="StarManualMailAlerts.aspx.vb" Inherits="EPG.StarManualMailAlerts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--div class="container"-->
    <div class="panel panel-info">
        <div class="panel-heading text-center">
            <h2>
                Star Manual Mail Alerts
            </h2>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlChannels" runat="server" CssClass="form-control" DataSourceID="sqlDSChannels" DataTextField="channelid1"
                        DataValueField="Channelid" />
                    <asp:SqlDataSource ID="sqlDSChannels" runat="server" SelectCommand="select '' channelid,'-- select --' channelid1 union select channelid,channelid from mst_channel where companyid=5 order by channelid"
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-info">
                    <div class="panel-heading text-center">
                        <a class="btn btn-info" type="submit" onclick="javascript:sendMail(1);">Send Image Missing
                            Mail </a>
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="grd1" runat="server" CssClass="table table-striped" DataSourceID="sqlDS1"
                            EmptyDataText="no record found" EmptyDataRowStyle-CssClass="alert alert-danger text-center" />
                        <asp:SqlDataSource ID="sqlDS1" runat="server" SelectCommand="select ChannelId,Progname,NextAiring from fpc_v_mailer_ImageMissing order by Channelid,progname"
                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" />
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-info">
                    <div class="panel-heading text-center">
                        <a class="btn btn-info" type="submit" onclick="javascript:sendMail(2);">Send EPG Missing
                            Mail </a>
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="grd2" runat="server" CssClass="table table-striped" DataSourceID="sqlDS2"
                            EmptyDataText="no record found" EmptyDataRowStyle-CssClass="alert alert-danger text-center" />
                        <asp:SqlDataSource ID="sqlDS2" runat="server" SelectCommand="select Channelid,GenreID,EPGAvailableTill from fpc_v_mailer_EPGMissing order by channelid"
                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" />
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-info">
                    <div class="panel-heading text-center">
                        <a class="btn btn-info" type="submit" onclick="javascript:sendMail(3);">Send Episodic
                            Missing Mail </a>
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="grd3" runat="server" CssClass="table table-striped" DataSourceID="sqlDS3"
                            EmptyDataText="no record found" EmptyDataRowStyle-CssClass="alert alert-danger text-center" />
                        <asp:SqlDataSource ID="sqlDS3" runat="server" SelectCommand="select Channelid,Progname,EpisodeNo [Epi],NextAiring from fpc_v_mailer_EpisodicMissing order by channelid"
                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" />
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-info">
                    <div class="panel-heading text-center">
                        <a class="btn btn-info" type="submit" onclick="javascript:sendMail(4);">Send Slot Missing
                            Mail </a>
                    </div>
                    <div class="panel-body">
                        <asp:GridView ID="grd4" runat="server" CssClass="table table-striped" DataSourceID="sqlDS4"
                            EmptyDataText="no record found" EmptyDataRowStyle-CssClass="alert alert-danger text-center" />
                        <asp:SqlDataSource ID="sqlDS4" runat="server" SelectCommand="select Channelid,Progname,convert(varchar,progdate,106) + ' ' + convert(varchar,progtime,108) NextAiring from fpc_v_mailer_SlotMissing order by progdate, progtime"
                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/div-->
    <script type="text/javascript">
        function sendMail(type) {
            var result = confirm("Are you sure to send mail to STAR ?");
            var channel = document.getElementById("MainContent_ddlChannels").value;
            
            if (result) {
                var url = '';
                if (type == 1) {
                    //url = 'http://ndtv.com' + type;
                    url = "http://172.16.14.45/scheduler/public/star/image/missing?channel=" + channel;
                }
                if (type == 2) {
                    //url = 'http://ndtv.com' + type;
                    url = "http://172.16.14.45/scheduler/public/star/epg/missing?channel=" + channel;

                }
                if (type == 3) {
                    //url = 'http://ndtv.com' + type;
                    url = "http://172.16.14.45/scheduler/public/star/episode/missing?channel=" + channel;

                }
                if (type == 4) {
                    //url = 'http://ndtv.com' + type;
                    url = "http://172.16.14.45/scheduler/public/star/slot/missing?channel=" + channel;
                }
                //alert(url);
                console.log(url);
                window.open(url, "Manual Generate", "width=300,height=200,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");


            }

        }
    </script>
</asp:Content>
