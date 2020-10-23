<%@ Page Title="Highlights" Language="vb" AutoEventWireup="false" MasterPageFile="SiteStar.Master"
    CodeBehind="starHighlights.aspx.vb" Inherits="EPG.starHighlights" %>

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
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AutoPostBack="true">
                        <%--<asp:ListItem Text="--select--" Value="0" />--%>
                        <asp:ListItem Text="International" Value="International" />
                        <asp:ListItem Text="Domestic" Value="Domestic" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-9">
                    &nbsp;
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    <asp:Repeater ID="dl1" runat="server" DataSourceID="sqlDS">
                        <ItemTemplate>
                            <div class="text-center col-md-6">
                                <span class="label btn-info pull-left">
                                    <%# Container.ItemIndex + 1  %>. </span><span class="label alert-warning pull-right">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Genre") %>' />
                                    </span>
                                <div class="alert1 alert-info">
                                    <asp:LinkButton CssClass="lnkchannel" ID="lnkDeactive" runat="Server" CommandName="channel"
                                        Text='<%# Bind("channel") %>' CommandArgument='<%#Bind("channel") %>' />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-md-6">
                    <div class="col-md-12 text-center">
                        <asp:Label ID="lbChannel" runat="server" CssClass="h5 text-center alert1" />
                    </div>
                    <asp:GridView ID="grdNew" runat="server" CssClass="table" HeaderStyle-CssClass="text-center"
                        DataSourceID="sqlDSNew" CellPadding="3" GridLines="Vertical" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="false"
                        EmptyDataText="no record found" EmptyDataRowStyle-CssClass="alert text-center alert-danger">
                        <HeaderStyle BackColor="#D0021B" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#ffe1e9" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgname" runat="server" Text='<%#Bind("Progname") %>' />
                                    <asp:Label ID="lbprogid" runat="server" Text='<%#Bind("progid") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Synopsis">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%#Bind("Synopsis") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Genre">
                                    <ItemTemplate>
                                        <asp:Label ID="lbGenre" runat="server" Text='<%#Bind("absGenre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Epi">
                                    <ItemTemplate>
                                        <asp:Label ID="lbEpisode" runat="server" Text='<%#Bind("episodeno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duration">
                                <ItemTemplate>
                                    <asp:Label ID="lbDuration" runat="server" Text='<%#Bind("duration_int") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:Label ID="lbCategory" runat="server" Text='<%#Bind("Category") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AirDate">
                                <ItemTemplate>
                                    <asp:Label ID="lbAirDate" runat="server" Text='<%#Bind("progdatetime") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select Channel,Genre from fpc_v_dashboard where type=@type order by 2,1"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter Name="type" ControlID="ddlType" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSNew" runat="server" SelectCommand="select * from star_fn_highlights(@channelid,dbo.getlocaldate(),dbo.getlocaldate()+10) order by progdatetime"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter Name="channelid" ControlID="lbChannel" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
