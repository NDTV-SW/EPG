<%@ Page Title="Images Uploaded Today" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="ImagesUploadedToday.aspx.vb" Inherits="EPG.ImagesUploadedToday" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="http://epgops.ndtv.com/lightbox.css" rel="stylesheet" type="text/css" />
    <script src="http://epgops.ndtv.com/lightbox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <br />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <strong>Images Uploaded Today</strong>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3 col-md-offset-3">
                        <div class="form-group">
                            <label>
                                Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtDate" placeholder="Date" runat="server" CssClass="form-control"
                                    ValidationGroup="RFGViewEPG" />
                                <span class="input-group-addon">
                                    <asp:Image ID="imgDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CE_txtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgDateCalendar" />
                                    <asp:RequiredFieldValidator ID="RFVtxtDate" ControlToValidate="txtDate" runat="server"
                                        ForeColor="Red" Text="*" ValidationGroup="RFGViewEPG" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn alert-info" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <strong>Landscape</strong>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="grd" runat="server" CssClass="table text-center" DataSourceID="sqlDS"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="White" />
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
                            <div class="form-group">
                                <label>
                                    Users</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlusers" runat="server" CssClass="form-control" DataSourceID="sqlDSImageUsers"
                                        DataTextField="lastupdatedby" DataValueField="lastupdatedby" AutoPostBack="true" />
                                </div>
                                <asp:GridView ID="grdUsers" runat="server" CssClass="table" DataSourceID="sqlDSImage"
                                    EmptyDataText="--no record found--" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="Channelid" HeaderText="Channel" />
                                        <asp:BoundField DataField="progname" HeaderText="Program" />
                                        <asp:TemplateField HeaderText="Logo" SortExpression="programlogo">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbProgLogo" Text='<%# Bind("programlogo") %>' Visible="false" />
                                                <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                                    <asp:Image runat="server" ID="imglogo" AlternateText="NDTV" Width="150px" Height="100px" />
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <strong>Portrait</strong>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="grdP" runat="server" CssClass="table text-center" DataSourceID="sqlDSP"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="White" />
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
                            <div class="form-group">
                                <label>
                                    Users</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlusersP" runat="server" CssClass="form-control" DataSourceID="sqlDSImageUsersP"
                                        DataTextField="lastupdatedby" DataValueField="lastupdatedby" AutoPostBack="true" />
                                </div>
                            </div>
                            <asp:GridView ID="grdUsersP" runat="server" CssClass="table" DataSourceID="sqlDSImageP"
                                EmptyDataText="--no record found--" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Channelid" HeaderText="Channel" />
                                    <asp:BoundField DataField="progname" HeaderText="Program" />
                                    <asp:TemplateField HeaderText="Logo">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbProgLogo" Text='<%# Bind("programlogoportrait") %>' Visible="false" />
                                            <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                                <asp:Image runat="server" ID="imglogo" AlternateText="NDTV" Width="100px" Height="150px" />
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select count(*) Cnt,a.lastupdatedby
    [By] from ( select distinct progid,lastupdatedby from aud_mst_program_proglogo where
    cast(lastupdatedat as date)=cast(@date as date) )a group by lastupdatedby order
    by 2,1" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtDate" Name="date" PropertyName="Text" Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSP" runat="server" SelectCommand="select count(*) Cnt,a.lastupdatedby [By] from ( select distinct progid,lastupdatedby
    from aud_mst_program_proglogo_portrait where cast(lastupdatedat as date)=cast(@date
    as date) )a group by lastupdatedby order by 2,1" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtDate" Name="date" PropertyName="Text" Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSImageUsers" runat="server" SelectCommand="select '--select--' lastupdatedby
    union SELECT DISTINCT lastupdatedby FROM aud_mst_program_proglogo WHERE cast(lastupdatedat
    AS DATE) = cast(dbo.getlocaldate() AS DATE) order by 1" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" />
    <asp:SqlDataSource ID="sqlDSImage" runat="server" SelectCommand="select progid,channelid,progname,programlogo
    from mst_program where progid in (SELECT DISTINCT progid FROM aud_mst_program_proglogo
    WHERE cast(lastupdatedat AS DATE) = cast(@date AS DATE) and lastupdatedby=@user)
    order by 2,3" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtDate" Name="date" PropertyName="Text" Direction="Input" />
            <asp:ControlParameter ControlID="ddlusers" Name="user" PropertyName="SelectedValue"
                Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSImageUsersP" runat="server" SelectCommand="select '--select--' lastupdatedby
    union SELECT DISTINCT lastupdatedby FROM aud_mst_program_proglogo_portrait WHERE
    cast(lastupdatedat AS DATE) = cast(dbo.getlocaldate() AS DATE) order by 1" ConnectionString="<%$
    ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSImageP" runat="server" SelectCommand="select progid,channelid,progname,programlogoportrait from
    mst_program where progid in (SELECT DISTINCT progid FROM aud_mst_program_proglogo_portrait
    WHERE cast(lastupdatedat AS DATE) = cast(@date AS DATE) and lastupdatedby=@user)
    order by 2,3" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtDate" Name="date" PropertyName="Text" Direction="Input" />
            <asp:ControlParameter ControlID="ddlusersP" Name="user" PropertyName="SelectedValue"
                Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
