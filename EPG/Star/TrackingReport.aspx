<%@ Page Title="Tracking" Language="vb" MasterPageFile="SiteStar.Master"
    AutoEventWireup="false" CodeBehind="TrackingReport.aspx.vb" Inherits="EPG.TrackingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .csspager a
        {
            padding-left: 10px;
            padding-right: 10px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading">
            <h2>
                TRACKING REPORT
            </h2>
        </div>
        <div class="panel-body">
        <table class="table table-bordered table-hover">
            <tr>
                <th class="alert-success">
                    <center>
                        Seach By Type</center>
                    <asp:DropDownList ID="ddltype" DataSourceID="Sdsdt" DataTextField="type" DataValueField="type"
                        AppendDataBoundItems="true" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select Type--" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </th>
                <th>
                </th>
                <th class="alert-success">
                    <center>
                        Seach Box</center>
                    <asp:TextBox CssClass="form-control" ID="txtSearch" runat="server"></asp:TextBox>
                </th>
                <th>
                </th>
                <th>
                    <center>
                        <asp:Button ID="abc" runat="server" Text="Search" Height="40px" Width="100px" /></center>
                </th>
            </tr>
        </table>
    </div>
    <div class="panel-body">
        <asp:GridView ID="grd" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataSourceID="sqlDS"
            GridLines="Vertical" AllowPaging="False" AllowSorting="True" PageSize="10">
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle CssClass="csspager" BackColor="#FFCC99" ForeColor="Black" BorderColor="#990000"
                BorderStyle="None" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
            <EmptyDataTemplate>
                <div align="center" class="alert text-center alert-danger">
                    No records found</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT b.Client, a.Channelid as Channel, a.Message, a.Type,a.Tracking, a.UploadType, a.Lastupdate FROM fpc_logs a JOIN fpc_distribution b ON b.id=a.clientid where (Tracking=case when @txtsearch='' then @trackingid else @txtsearch end)  AND type LIKE '%' + @txttypes + '%' order by lastupdate desc">
        <SelectParameters>
            <asp:ControlParameter Name="txttypes" ControlID="ddltype" PropertyName="Text" DbType="String"
                ConvertEmptyStringToNull="false" />
            <asp:ControlParameter Name="txtsearch" ControlID="txtSearch" PropertyName="Text"
                DbType="String" ConvertEmptyStringToNull="false" />
            <asp:QueryStringParameter Name="trackingid" QueryStringField="trackingid" DefaultValue=""
                ConvertEmptyStringToNull="False" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="Sdsdt" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT distinct type FROM fpc_logs" />
    <script type="text/javascript">
$(document).ready(function () {
function getUrlVars() {
var url = "http://localhost:52985/trackingreport.aspx?trackingid=";
var vars = {};
var hashes = url.split("?")[1];
var hash = hashes.split('&');

for (var i = 0; i < hash.length; i++) {
params=hash[i].split("=");
vars[params[0]] = params[1];
}
return vars;
}
    </script>
</asp:Content>
