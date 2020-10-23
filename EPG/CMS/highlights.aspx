<%@ Page Title="Highlights" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="highlights.aspx.vb" Inherits="EPG.highlights" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../lightbox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            initLightbox();
        }
    </script>
    <%--<asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                    <h3>
                        Highlights</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select Operator</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlDTHCableOperators" runat="server" DataTextField="name" DataValueField="operatorid"
                                        DataSourceID="sqlDSDTHCableOperators" CssClass="form-control" />
                                    <asp:SqlDataSource ID="sqlDSDTHCableOperators" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select operatorid,name from mst_dthcableoperators where active =1 and operatorid=232 order by 2">
                                    </asp:SqlDataSource>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnGet" runat="server" Text="Fetch" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <a href="viewHighlights.aspx" class="btn btn-info" target="_blank">View Highlights</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnHighLight" runat="server" Text="Save Highlights" CssClass="btn btn-success" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                    <asp:GridView ID="grdHighLights" runat="server" AutoGenerateColumns="False" CssClass="table"
                        AllowPaging="True" AllowSorting="False" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        PageSize="500" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("ndtvchannelid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme" SortExpression="progname">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbProgName" Text='<%# Bind("progname") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme Type" SortExpression="progtype">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbprogtype" Text='<%# Bind("showtype") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fresh/Repeat" SortExpression="FreshRepeat">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbFreshRepeat" Text='<%# Bind("is_live") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Synopsis" SortExpression="synopsis">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbsynopsis" Text='<%# Bind("synopsis") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Starcast" SortExpression="StarCast">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbStarCast" Text='<%# Bind("StarCast") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Time" SortExpression="istDate">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbISTDate" Text='<%# Bind("progdate") %>' />
                                    <asp:Label runat="server" ID="lbISTTime" Text='<%# Bind("progstarttime") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Genre" SortExpression="Genre">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbGenre" Text='<%# Bind("Genre") %>' />
                                    <asp:Label runat="server" ID="lbPoster" Text='<%# Bind("programlogo") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="subgenre" HeaderText="Sub-Genre">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbSubGenre" Text='<%# Bind("subgenre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="episodeno" HeaderText="Episode No">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbEpisodeNo" Text='<%# Bind("episodeno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="duration" HeaderText="Duration">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbDuration" Text='<%# Bind("duration") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="release_year" HeaderText="Release Year">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbReleaseYear" Text='<%# Bind("release_year") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:HyperLink rel="lightbox" ID="hyLogo" NavigateUrl='<%# Bind("programlogo") %>'
                                        runat="server">
                                        <asp:Image runat="server" ID="imglogo" AlternateText="NDTV" Width="50px" Height="50px"
                                            ImageUrl='<%# Bind("programlogo") %>' />
                                    </asp:HyperLink></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Category" DataField="Category" />
                            <asp:TemplateField HeaderText="Highlight">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkHighLight" runat="server" />
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
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
