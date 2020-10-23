<%@ Page Title="App No. & Priority" Language="vb" MasterPageFile="SiteDTH.Master"
    AutoEventWireup="false" CodeBehind="AppNo.aspx.vb" Inherits="EPG.AppNo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                            <h3>
                                Update App No. & Priority</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                CssClass="table" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" AllowSorting="True"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" BorderStyle="Inset" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" SortExpression="OperatorID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbOperatorID" runat="server" Text='<%# Bind("OperatorID") %>' Visible="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="ID" DataField="OperatorID" SortExpression="OperatorID">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name"></asp:BoundField>
                                    <asp:BoundField HeaderText="EPGTYPE" DataField="EPGTYPE" SortExpression="EPGTYPE">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="true">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="lbActive" runat="server" Checked='<%# Bind("Active") %>' Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AppNo" SortExpression="AppNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lbAppNo" runat="server" Text='<%# Bind("AppNo") %>' CssClass="badge" />
                                            <asp:ImageButton ID="btnAppUp" runat="server" CommandName="appup" ImageUrl="../images/up.gif"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                            <asp:ImageButton ID="btnAppDown" runat="server" CommandName="appdown" ImageUrl="../images/down.gif"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPriority" runat="server" Text='<%# Bind("Priority") %>' CssClass="badge" />
                                            <asp:ImageButton ID="btnUp" runat="server" CommandName="priorityup" ImageUrl="../images/up.gif"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                            <asp:ImageButton ID="btnDown" runat="server" CommandName="prioritydown" ImageUrl="../images/down.gif"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Send Time" DataField="TimeSend1" SortExpression="timetosend1">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Send Time 2" DataField="TimeSend2" SortExpression="TimeToSend2">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Send Time 3" DataField="TimeSend3" SortExpression="TimeToSend3">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Send Time 4" DataField="TimeSend4" SortExpression="TimeToSend4">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Days To Send" DataField="DaysToSend" SortExpression="DaysToSend">
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                SelectCommand="SELECT *, left(convert(varchar,TimeToSend1,108),5) timeSend1,left(convert(varchar,TimeToSend2,108),5) timeSend2 ,left(convert(varchar,TimeToSend3,108),5) timeSend3 ,left(convert(varchar,TimeToSend4,108),5) timeSend4 FROM mst_dthcableoperators order by active desc,appno,priority,timetosend1,name,timetosend2">
                            </asp:SqlDataSource>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
