<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="EPG._Default" %>

<%@ OutputCache Duration="60" Location="Server" VaryByParam="None" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="lblAccess" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"
        Visible="False"></asp:Label>
    <asp:Label ID="lbPasswordExpire" runat="server" Font-Bold="True" Font-Size="Medium"
        ForeColor="Red"></asp:Label>
    <br />
    <asp:Button ID="btnRefreshChannelList" runat="server" Text="Refresh Channels" CssClass="btn btn-info" />
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="panel panel-info">
                <div class="panel-heading text-center alert alert-info">
                    <h4>
                        URGENT 8th/10th Day EPG Missing</h4>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GridView3" CssClass="table table-striped" runat="server" DataSourceID="sqlDS_EPGMissing10Days"
                        AutoGenerateColumns="true" AllowSorting="true" EmptyDataText="...No Record found .....">
                    </asp:GridView>
                </div>
                <div class="panel-heading text-center">
                    <h4>
                        Minimum 8 days EPG Data required</h4>
                    <%--<div class="alert alert-danger">
                        <h4>
                            Note: Rows in red have less than 3 days of EPG available.</h4>
                    </div>--%>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grdEPGMissing8Days" CssClass="table table-striped" runat="server"
                        DataSourceID="sqlDS_EPGMissing8Days" AutoGenerateColumns="false" AllowSorting="true" EmptyDataText="...No Record found .....">
                        <Columns>
                            <asp:TemplateField HeaderText="Channel" SortExpression="ChannelId">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannel" runat="server" Text='<%#Eval("ChannelId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EPG available for(Days)" SortExpression="noOfDays">
                                <ItemTemplate>
                                    <asp:Label ID="lbnoOfDays" runat="server" Text='<%#Eval("noOfDays") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EPG From" SortExpression="EPGFrom">
                                <ItemTemplate>
                                    <asp:Label ID="lbEPGFrom" runat="server" Text='<%#Eval("EPGFrom") %>' />
                                </ItemTemplate>
                                <ControlStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EPG To" SortExpression="EPGTo">
                                <ItemTemplate>
                                    <asp:Label ID="lbEPGTo" runat="server" Text='<%#Eval("EPGTo") %>' />
                                </ItemTemplate>
                                <ControlStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operators" SortExpression="Operators">
                                <ItemTemplate>
                                    <asp:Label ID="lbOperators" runat="server" Text='<%#Eval("Operators") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="panel panel-info">
                <div class="panel-heading text-center">
                    <h4>
                        Channels that are ONAIR in DB but set to OFF-AIR for operators
                    </h4>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="GridView2" CssClass="table table-striped" runat="server" DataSourceID="sqlDS_CableOpChannels"
                        AutoGenerateColumns="true" AllowSorting="true" EmptyDataText="...No Record found ....." />
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS_EPGMissing8Days" runat="server" SelectCommand="rpt_missingepg_8days"
        SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDS_EPGMissing10Days" runat="server" SelectCommand="select ChannelId,OperatorName,DateMissing from vw_php_job_mail_8thday_missingdata"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDS_CableOpChannels" runat="server" SelectCommand="select x.Operatorid 'id',Name = (select name from mst_dthcableoperators where operatorid = x.operatorid ),x.ChannelId, x.ServiceId,x.operatorchannelid 'Operator Channel',x.Onair
                    from dthcable_channelmapping x where (onair = 0 or onair is null) and( channelid in (select channelid from mst_channel where onair = 1 ) or operatorchannelid in (select channelid from mst_channel where onair = 1 ) or serviceId in (select channelid from mst_channel where onair = 1 ))
                    and operatorid in (select operatorid from mst_dthcableoperators where active = 1) and channelid not like 'dummy%'
                    and operatorid not in (23,125,6,9,124) order by 2" SelectCommandType="Text"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
</asp:Content>
