<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteStar.Master"
    CodeBehind="FpcLogsReport.aspx.vb" Inherits="EPG2.FpcLogsReport" %>

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
    <div class="col-lg-12">
        <table class="table table-bordered table-hover">
            <tr>
                <th class="alert-success">
                    <center>
                        Seach By Client</center>
                    <%--<asp:TextBox CssClass="form-control" ID="txtclient" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlclient" DataSourceID="Sdsdd" DataTextField="client" DataValueField="client"
                        AppendDataBoundItems="true" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select Client--" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </th>
                <th>
                </th>
                <th class="alert-success">
                    <center>
                        Seach By Type</center>
                    <%--<asp:TextBox CssClass="form-control" ID="txttype" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddltype" DataSourceID="Sdsdt" DataTextField="type" DataValueField="type"
                        AppendDataBoundItems="true" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select Type--" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </th>
                <th>
                </th>
                <th class="alert-success">
                    <center>
                        Seach By UploadType</center>
                    <%--<asp:TextBox CssClass="form-control" ID="txtuploadtype" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddluploadtype" DataSourceID="Sdsdut" DataTextField="uploadtype"
                        DataValueField="uploadtype" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select Upload Type--" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </th>
                <th>
                </th>
                <th class="alert-success">
                    <center>
                        Last Time</center>
                    <asp:DropDownList ID="ddldatetime" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select Time Slot--" Value="-1441"></asp:ListItem>
                        <asp:ListItem Text="30Min" Value="-30"></asp:ListItem>
                        <asp:ListItem Text="Last Hr" Value="-60"></asp:ListItem>
                        <asp:ListItem Text="Last Day" Value="-1440"></asp:ListItem>
                        <asp:ListItem Text="Last Week" Value="-10080"></asp:ListItem>
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
            GridLines="Vertical" AllowPaging="True" AllowSorting="True" PageSize="10" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
<%--            <PagerStyle CssClass="csspager" BackColor="#FFCC99" ForeColor="Black" BorderColor="#990000"
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
                <asp:BoundField DataField="Channel" HeaderText="Channel">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="Message" HeaderText="Message">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="Type" HeaderText="Type">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="hy" runat="server" Text='<%# Eval("tracking") %>' NavigateUrl='<%# Eval("trackurl") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Uploadtype" HeaderText="Upload Type">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="Lastupdate" HeaderText="Last Update">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <div align="center" class="alert text-center alert-danger">
                    No records found</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>"
        SelectCommand="SELECT b.Client, a.Channelid as Channel, a.Message, a.Type,a.Tracking,'trackingreport.aspx?trackingid=' + a.Tracking trackurl, a.UploadType, a.Lastupdate FROM fpc_logs a JOIN fpc_distribution b ON b.id=a.clientid where client LIKE '%' + @txtclientname + '%' AND type LIKE '%' + @txttypes + '%' AND uploadtype LIKE '%' + @txtuploadtypes + '%' AND a.lastupdate between dateadd(mi,@ddltimeslot,dbo.getlocaldate()) and dbo.getlocaldate() order by lastupdate desc">
        <SelectParameters>
            <asp:ControlParameter Name="txtclientname" ControlID="ddlclient" PropertyName="Text"
                DbType="String" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter Name="txttypes" ControlID="ddltype" PropertyName="Text" DbType="String"
                ConvertEmptyStringToNull="false" />
            <asp:ControlParameter Name="txtuploadtypes" ControlID="ddluploadtype" PropertyName="Text"
                DbType="String" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter Name="ddltimeslot" ControlID="ddldatetime" PropertyName="SelectedValue"
                Direction="Input" Type="Int16" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="Sdsdd" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT  distinct Client FROM fpc_distribution" />
    <asp:SqlDataSource ID="Sdsdt" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT distinct type FROM fpc_logs" />
    <asp:SqlDataSource ID="Sdsdut" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT distinct uploadtype  FROM fpc_logs" />
</asp:Content>
