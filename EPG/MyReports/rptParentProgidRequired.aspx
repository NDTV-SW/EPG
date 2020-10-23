<%@ Page Title="Parent Progid Required" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="rptParentProgidRequired.aspx.vb" Inherits="EPG.rptParentProgidRequired" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>Parent Progid Required</h3>
            </div>
            <div class="panel-body">
                <asp:GridView ID="grdParentProgIdReq" runat="server" CssClass="table" AutoGenerateColumns="True" AllowPaging="true" PageSize="100"
                    ShowFooter="True" DataSourceID="sqlDSParentProgIdReq" CellPadding="4" EmptyDataText="No record found !"
                    ForeColor="#333333" GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                    SortedDescendingHeaderStyle-CssClass="sortdesc-header" ShowHeader="True" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                    PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Font-Bold="true" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSParentProgIdReq" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select ChannelId,ProgName,convert(varchar,progdate,106) AirDate, convert(varchar,progtime,108) Airtime,Duration from v_ParentProgidRequired order by channelid,progname,progdate,progtime" SelectCommandType="Text"></asp:SqlDataSource>
</asp:Content>
