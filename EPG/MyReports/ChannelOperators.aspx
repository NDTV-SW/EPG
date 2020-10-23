<%@ Page Title="Channel Operators" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="ChannelOperators.aspx.vb" Inherits="EPG.ChannelOperators" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Channel Operators</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                                    <span class="input-group-addon">
                                        <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtChannel" FirstRowSelected="true" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:GridView ID="grdImagesReport" runat="server" ShowFooter="True" AutoGenerateColumns="true"
                            CellPadding="4" EmptyDataText="No record found !" Width="50%" ForeColor="Black"
                            GridLines="Vertical" PageSize="50" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                            DataSourceID="sqlDS" SortedDescendingHeaderStyle-CssClass="sortdesc-header" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                            PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#CCCC99" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                            <PagerSettings NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/Prev.gif">
                            </PagerSettings>
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select b.ChannelId, a.Name [Cable Operator Name],a.City from mst_dthcableoperators a join dthcable_channelmapping b on a.operatorid=b.operatorid
                                                                                where a.Active=1 and b.Onair=1 and b.ChannelID=@channelid order by 2"
        ConnectionString="<%$ConnectionStrings:EpgConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter Name="channelid" ControlID="txtChannel" PropertyName="Text"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
