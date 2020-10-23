<%@ Page Title="TV Star Discrepancy" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="TVStarDiscrepancy.aspx.vb"
    Inherits="EPG.TVStarDiscrepancy" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openWin(progid) {
            window.open("viewProgTVStars.aspx?progid=" + progid, "Programme TV Stars", "width=850,height=550,toolbar=0,left=150,top=100,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    TV Star Discrepancy</h3>
            </div>
            <div class="panel-body container">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Channel</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlChannel" runat="server" DataTextField="ChannelId" DataValueField="channelvalue"
                                    DataSourceID="sqlDSChannels" CssClass="form-control" AutoPostBack="true" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Genre</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlGenre" runat="server" DataTextField="genrename" DataValueField="genreid"
                                    DataSourceID="sqlDSGenre" CssClass="form-control" AutoPostBack="true" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="fullname" DataValueField="languageid"
                                    DataSourceID="sqlDSLanguage" CssClass="form-control" AutoPostBack="true" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                High TRP channels</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkHighTRP" runat="server" CssClass="form-control" Checked="true"
                                    AutoPostBack="true" Text="" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Missing Programmes</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkMissing" runat="server" CssClass="form-control" Checked="true"
                                    AutoPostBack="true" Text="" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                In EPG</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkInEPG" runat="server" CssClass="form-control" Checked="true"
                                    AutoPostBack="true" Text="" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grdData" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        DataSourceID="sqlDSData" PageSize="50" AllowPaging="true" AllowSorting="true"
                        AutoGenerateColumns="false">
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
                        <Columns>
                            <asp:TemplateField HeaderText="S.no.">
                                <ItemTemplate>
                                    <asp:Label ID="lbSNO" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel" SortExpression="ChannelId">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannelId" runat="server" Text='<%#Eval("ChannelId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme" SortExpression="ProgName">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgId" runat="server" Text='<%#Eval("ProgId") %>' Visible="false" />
                                    <asp:HyperLink ID="hyProgName" runat="server" Text='<%#Eval("ProgName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSChannels" runat="server" SelectCommand="sp_getChannelList"
        SelectCommandType="StoredProcedure" ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'>
        <SelectParameters>
            <asp:ControlParameter Name="highTRP" ControlID="chkHighTRP" PropertyName="Checked" Direction="Input" />
            <asp:ControlParameter Name="genreid" ControlID="ddlGenre" PropertyName="SelectedValue" Direction="Input" />
            <asp:ControlParameter Name="languageid" ControlID="ddlLanguage" PropertyName="SelectedValue" Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSGenre" runat="server" SelectCommand="select '--All Genre--' GenreName, '0' 'GenreId'  union select GenreName,GenreId from mst_genre where genreCategory='S' order by 1"
        ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSLanguage" runat="server" SelectCommand="select '--All Languages--' FullName, '0' 'Languageid' union select FullName,LanguageID from mst_language where active=1  order by 1"
        ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSData" runat="server" SelectCommand="sp_getTVStarDiscrepancy"
        SelectCommandType="StoredProcedure" ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'>
        <SelectParameters>
            <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue"
                Direction="Input" />
            <asp:ControlParameter Name="languageid" ControlID="ddlLanguage" PropertyName="SelectedValue"
                Direction="Input" />
                <asp:ControlParameter Name="genreid" ControlID="ddlGenre" PropertyName="SelectedValue"
                Direction="Input" />
            <asp:ControlParameter Name="highTRP" ControlID="chkHighTRP" PropertyName="Checked"
                Direction="Input" />
            <asp:ControlParameter Name="missing" ControlID="chkMissing" PropertyName="Checked"
                Direction="Input" />
            <asp:ControlParameter Name="inepg" ControlID="chkInEPG" PropertyName="Checked" Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
