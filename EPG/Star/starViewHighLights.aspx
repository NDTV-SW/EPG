<%@ Page Title="View Highlights" Language="vb" AutoEventWireup="false" MasterPageFile="~/Star/SiteStar.Master"
    CodeBehind="starViewHighLights.aspx.vb" Inherits="EPG.starViewHighLights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .alert1
        {
            padding: 15px;
            margin-bottom: 8px;
            border: 1px solid transparent;
            border-radius: 8px;
            font-size: medium;
            font-weight: bold;
        }
        .col-md-3
        {
            padding-right: 10px !important;
            padding-left: 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">Highlights
                <asp:Label ID="lbLabel" runat="server" /></li>
        </ol>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AutoPostBack="true">
                        <asp:ListItem Text="International" Value="International" />
                        <%--<asp:ListItem Text="--select--" Value="0" />--%>
                        <asp:ListItem Text="Domestic" Value="Domestic" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <asp:GridView ID="grdNew" runat="server" CssClass="table" HeaderStyle-CssClass="text-center"
                        DataSourceID="sqlDSNew" CellPadding="3" GridLines="Vertical" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="false"
                        EmptyDataText="no new programs found" EmptyDataRowStyle-CssClass="alert alert-danger">
                        <HeaderStyle BackColor="#D0021B" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#ffe1e9" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Channel" DataField="Channelid" />
                            <asp:BoundField HeaderText="Programme" DataField="Progname" />
                            <asp:BoundField HeaderText="Category" DataField="Category" />
                            <asp:BoundField HeaderText="AirTime" DataField="AirTime" />
                        </Columns>
                    </asp:GridView>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-info" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSNew" runat="server" SelectCommand="select a.*,b.progname,convert(varchar,a.Airdate,106) + ' ' + convert(varchar,a.Airdate,108) AirTime from fpc_highlights a join mst_program b on a.progid=b.progid where convert (varchar,airdate,112)>=convert (varchar,dbo.getlocaldate(),112) and feed=@feed order by Category, airdate"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlType" PropertyName="SelectedValue" Name="feed" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
