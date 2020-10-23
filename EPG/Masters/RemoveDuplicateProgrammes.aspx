<%@ Page Title="Remove Duplicate Programmes" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="RemoveDuplicateProgrammes.aspx.vb" Inherits="EPG.RemoveDuplicateProgrammes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <%--    <asp:UpdatePanel ID="updProgramMaster" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Remove Duplicate Programmes</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                        <div class="form-group">
                            <label>
                                Select Channel</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" DataSourceID="SqlDsChannel"
                                    DataTextField="ChannelId" DataValueField="ChannelId" AutoPostBack="True" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                        <div class="form-group">
                            <label>
                                Select Programme to Remove</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlSourceProgramme" runat="server" CssClass="form-control"
                                    DataSourceID="sqlDSProgrammes" DataTextField="progname" DataValueField="progid" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 ">
                        <div class="form-group">
                            <label>
                                Select Programme to replace with</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlDestination" runat="server" CssClass="form-control" DataSourceID="sqlDSProgrammes"
                                    DataTextField="progname" DataValueField="progid" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-right ">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-right ">
                        <asp:Button ID="btnUpdate" runat="server" Text="Replace and Make In-Active" CssClass="btn btn-info" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-left ">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-right ">
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="grdInactive" runat="server" DataSourceID="sqlDSInactive" CssClass="table"
                        AutoGenerateColumns="false" EmptyDataText="No In-Active program found" Caption="Inactive programs for this Channel"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="true" SelectText="Make Active" ControlStyle-CssClass="btn btn-success" />
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lbprogid" runat="server" Text='<%# Eval("progid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel">
                                <ItemTemplate>
                                    <asp:Label ID="lbchannelid" runat="server" Text='<%# Eval("channelid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme">
                                <ItemTemplate>
                                    <asp:Label ID="lbprogname" runat="server" Text='<%# Eval("progname") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
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
    <asp:SqlDataSource ID="SqlDsChannel" runat="server" ConnectionString="<%$
    ConnectionStrings:EPGConnectionString1 %>" SelectCommand="SELECT channelid from mst_channel where onair=1 order by 1">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSProgrammes" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" SelectCommand="Select progid, progname from mst_program where channelid=@channelid and active=1 order by 2">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlChannel" PropertyName="SelectedValue" Name="channelid"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSInactive" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" SelectCommand="Select progid,channelid, progname from mst_program where channelid=@channelid and active=0 order by 2">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlChannel" PropertyName="SelectedValue" Name="channelid"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <%--</ContentTemplate>
    <Triggers> <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
    </Triggers> </asp:UpdatePanel>--%>
</asp:Content>
