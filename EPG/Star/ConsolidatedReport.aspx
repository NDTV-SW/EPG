<%@ Page Title="Consolidated Report" Language="vb" AutoEventWireup="false" MasterPageFile="SiteStar.Master"
    CodeBehind="ConsolidatedReport.aspx.vb" Inherits="EPG.ConsolidatedReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .csspager a
        {
            padding-left: 10px;
            padding-right: 10px;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    .GridPager a, .GridPager span
    {
    margin-right: 4px;
    padding: 5px;
    display: block;
    z-index: 5;
    width: 34px;
    font-weight: bold;
    font-size: 14px;
    text-align: center;
    text-decoration: none;
    }
    .GridPager a
    {
        background-color: #f5f5f5;
        color: #969696;
        border: 1px solid #969696;
    }
    .GridPager span
    {
        background-color: #A1DCF2;
        color: #000;
        border: 1px solid #3AC0F2;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading">
            <h2>
                FPC Consolidated
            </h2>
        </div>
        <div class="panel-body">
            <table class="table table-bordered table-hover">
                <tr>
                    <th class="alert-success">
                        <center>
                            Seach By Client</center>
                        <asp:DropDownList ID="ddlclient" DataSourceID="Sdsdt" DataTextField="clientname" DataValueField="client"
                            AppendDataBoundItems="true" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Select Client--" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                    <th>
                    </th>
                    <th class="alert-success">
                        <center>
                            Generated at</center>
                        <asp:DropDownList ID="ddlgenerated" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Select Time Slot--" Value="-1441"></asp:ListItem>
                            <asp:ListItem Text="30Min" Value="-30"></asp:ListItem>
                            <asp:ListItem Text="Last 1 Hr" Value="-60"></asp:ListItem>
                            <asp:ListItem Text="Last 2 Hrs" Value="-120"></asp:ListItem>
                            <asp:ListItem Text="Last 4 Hrs" Value="-240"></asp:ListItem>
                            <asp:ListItem Text="Last 8 Hrs" Value="-480"></asp:ListItem>
                            <asp:ListItem Text="Last 16 Hrs" Value="-960"></asp:ListItem>
                            <asp:ListItem Text="Last Day" Value="-1440"></asp:ListItem>
                            <asp:ListItem Text="Last Week" Value="-10080"></asp:ListItem>
                            <asp:ListItem Text="15 days" Value="-21600"></asp:ListItem>
                            <asp:ListItem Text="1 Month" Value="-43200"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                    <th>
                    </th>
                    <th class="alert-success">
                        <center>
                            Time Taken</center>
                        <asp:DropDownList ID="ddlsectaken" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Select Time Slot--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="2 Mins" Value="120"></asp:ListItem>
                            <asp:ListItem Text="3 Mins" Value="180"></asp:ListItem>
                            <asp:ListItem Text="5 Mins Or More" Value="300"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                    <th>
                    </th>
                    <th class="alert-success">
                        <center>
                            Seach By Type</center>
                        <asp:DropDownList ID="ddluploadtype" DataSourceID="Sdsdt1" DataTextField="uploadtype"
                            DataValueField="uploadtype" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Select Type--" Value=""></asp:ListItem>
                        </asp:DropDownList>
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
                GridLines="Vertical" AllowPaging="True" AllowSorting="True" PageSize="20" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <%--<PagerStyle CssClass="csspager" BackColor="#FFCC99" ForeColor="Black" BorderColor="#990000"
                    BorderStyle="None" />--%>
                    <PagerStyle HorizontalAlign = "Center" CssClass = "GridPager" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
                <Columns>
                    <asp:BoundField DataField="Client" HeaderText="Client Name">
                        <ItemStyle Width="250px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Mintime" HeaderText="Start Time">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="maxtime" HeaderText="Completed At">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Timetaken" HeaderText="Time Taken">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cntchannelid" HeaderText="Count Channel Id">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Tracking">
                        <ItemTemplate>
                            <asp:HyperLink ID="hy" runat="server" Text='<%# Eval("tracking") %>' NavigateUrl='<%# Eval("trackurl") %>' Target="_blank" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Uploadtype" HeaderText="Upload Type">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    <div align="center" class="alert text-center alert-danger">
                        No records found</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
            SelectCommand="select *,'trackingreport.aspx?trackingid=' + Tracking trackurl from fpc_v_consolidtedlogs where client LIKE '%' + @txtclients + '%' AND uploadtype  LIKE '%' + @txtuploadtypes + '%'  AND secondstaken > @txtsectakens and maxtime between dateadd(mi,@txtgeneratedat,dbo.getlocaldate()) and dbo.getlocaldate() order by maxtime desc">
            <SelectParameters>
                <asp:ControlParameter Name="txtclients" ControlID="ddlclient" PropertyName="Text"
                    DbType="String" ConvertEmptyStringToNull="false" />
                <asp:ControlParameter Name="txtuploadtypes" ControlID="ddluploadtype" PropertyName="Text"
                    DbType="String" ConvertEmptyStringToNull="false" />
                <asp:ControlParameter Name="txtsectakens" ControlID="ddlsectaken" PropertyName="SelectedValue"
                    Direction="Input" Type="Int16" />
                <asp:QueryStringParameter Name="trackingid" QueryStringField="tracking" DefaultValue=""
                    ConvertEmptyStringToNull="False" />
                <asp:ControlParameter Name="txtgeneratedat" ControlID="ddlgenerated" PropertyName="SelectedValue"
                    Direction="Input" Type="Int32" />
                    
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="Sdsdt" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
            SelectCommand="SELECT distinct client + ' (' + convert(varchar(2),id) + ')' clientname, client FROM fpc_v_consolidtedlogs order by client" />
        <asp:SqlDataSource ID="Sdsdt1" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
            SelectCommand="SELECT distinct uploadtype FROM fpc_v_consolidtedlogs order by uploadtype" />
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
}
        </script>
</asp:Content>
