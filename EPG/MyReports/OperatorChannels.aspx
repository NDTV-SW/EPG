<%@ Page Title="Operator Wise Channels" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="OperatorChannels.aspx.vb" Inherits="EPG.OperatorChannels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>Operator Wise Channels</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <div class="col-lg-6 col-md-6 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select Operator</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control" DataSourceID="sqlDSOperators"
                                        DataTextField="name" DataValueField="operatorid" AutoPostBack="true" />
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="sqlDSOperators" runat="server" SelectCommand="select operatorid,name from mst_dthcableoperators where active=1 and isnull(parentoperatorid,0)=0 order by 2"
                                            ConnectionString="<%$ConnectionStrings:EpgConnectionString1 %>"></asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                        <asp:GridView ID="grd" runat="server" ShowFooter="True" AutoGenerateColumns="true" CssClass="table"
                            CellPadding="4" EmptyDataText="No record found !" ForeColor="Black"
                            GridLines="Vertical" PageSize="50" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                            DataSourceID="sqlDS" SortedDescendingHeaderStyle-CssClass="sortdesc-header" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                            PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#CCCC99" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                            <PagerSettings NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/Prev.gif"></PagerSettings>
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
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
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select ChannelId,Serviceid OperatorServiceId,OperatorChannelId,Onair, maxEPG=(select case when isnull(max(progdate),0)=0 then 'No EPG' else convert(varchar,max(progdate),106) end  from mst_epg where channelid=a.ChannelId) from dthcable_channelmapping a  where operatorid=@operatorid order by onair desc, channelid"
        ConnectionString="<%$ConnectionStrings:EpgConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter Name="operatorid" ControlID="ddlOperator" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
