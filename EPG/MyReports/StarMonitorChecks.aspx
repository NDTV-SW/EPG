<%@ Page Title="Star Monitor Checks" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="StarMonitorChecks.aspx.vb" Inherits="EPG.StarMonitorChecks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--div class="container"-->
    <div class="panel panel-info">
        <div class="panel-heading text-center">
            <h2>
                Star Monitor Checks
            </h2>
        </div>
        <div class="panel-body">
            <div class="col-md-3">
                <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" datasourceid="sqldsChannel"
                 DataTextField="channelid" DataValueField="channelid" AutoPostBack="true"
                 />
            </div>
            <asp:GridView ID="grd1" runat="server" CssClass="table table-striped" DataSourceID="sqlDS1"
                EmptyDataText="no record found" EmptyDataRowStyle-CssClass="alert alert-danger text-center" />
            <asp:SqlDataSource ID="sqlDS1" runat="server" SelectCommand="select * from fn_star_monitor_checks(@channelid) where errortext <>''"
                ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" >
                    <SelectParameters>
                        <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqldsChannel" runat="server" SelectCommand="select channelid from mst_channel where active=1 and companyid=5 order by channelid"
                ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" />
        </div>
    </div>
</asp:Content>
