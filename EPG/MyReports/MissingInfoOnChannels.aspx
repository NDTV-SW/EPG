<%@ Page Title="Missing Info on Channels" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="MissingInfoOnChannels.aspx.vb" Inherits="EPG.MissingInfoOnChannels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Missing Info on Channels</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" DataSourceID="sqlDSChannels" DataTextField="channelid" DataValueField="channelid" AutoPostBack="true" />
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="sqlDSChannels" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>"
                                            SelectCommand="select channelid from mst_channel where onair=1 order by 1" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Search Programme</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" DataSourceID="sqlDSProgramme" DataTextField="progname" DataValueField="progid" />
                                    <%--<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />--%>
                                    <span class="input-group-addon">
                                         <asp:SqlDataSource ID="sqlDSProgramme" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>"
                                            SelectCommand="SELECT distinct a.progid,a.progname FROM mst_program a JOIN mst_epg b ON a.ProgID=b.ProgID WHERE CONVERT(VARCHAR,b.ProgDate,112)>=CONVERT(VARCHAR,dbo.GetLocalDate(),112) AND a.ChannelId=@channelid order by 2" >
                                                <SelectParameters>
                                                    <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue" Direction="Input" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <br />
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-info" runat="server" Text="Search" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="grdData" CssClass="table" runat="server" ShowFooter="True"
                        CellPadding="4" EmptyDataText="No record found !" ForeColor="Black" GridLines="Vertical"
                        Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                        PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                        <PagerSettings NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/Prev.gif">
                        </PagerSettings>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
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
</asp:Content>
