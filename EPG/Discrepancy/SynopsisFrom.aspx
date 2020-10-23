<%@ Page Title="Synopsis From" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="SynopsisFrom.aspx.vb"
    Inherits="EPG.SynopsisFrom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openWin( repfromdate, reptodate, progid, controlid, episodeno) {
            //alert(episodeno);
            window.open("EditSynopsisFrom.aspx?repfromdate=" + repfromdate + "&reptodate=" + reptodate + "&progid=" + progid + "&controlid=" + controlid + "&episodeno=" + episodeno, "Edit Programme", "width=750,height=350,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        SYNOPSES ORIGINALLY FROM WOI/CHANNELS</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-3 col-sm-6">
                            <div class="form-group">
                                <label>
                                    From Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgtxtFromDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtFromDate" runat="server" TargetControlID="txtFromDate"
                                            PopupButtonID="imgtxtFromDateCalendar" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-6">
                            <div class="form-group">
                                <label>
                                    To Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgtxtToDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtToDate" runat="server" TargetControlID="txtToDate"
                                            PopupButtonID="imgtxtToDate" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-6">
                            <div class="form-group">
                                <label>
                                    From</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlFrom" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="From WOI" Value="woi" />
                                        <asp:ListItem Text="From other sources" Value="other" />
                                        <asp:ListItem Text="All sources" Value="all" />
                                    </asp:DropDownList>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:Button ID="btnFetch" runat="server" CssClass="btn btn-info" Text="Fetch" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="grdSynopsisFrom" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                            CellPadding="4" ForeColor="Black" DataSourceID="sqlDSModSynopsisFrom" GridLines="Vertical"
                            CssClass="table" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                            SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="20"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Channel">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelID" runat="server" Text='<%#Eval("ChannelId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Program Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProgName" runat="server" Text='<%#Eval("ProgName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Synopsis">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%#Eval("Synopsis") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Epi. no">
                                    <ItemTemplate>
                                        <asp:Label ID="lbEpisodeNo" runat="server" Text='<%#Eval("Episodeno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ProgId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProgId" runat="server" Text='<%#Eval("ProgId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyEdit" runat="server" Text="Edit" />
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
    </div>
    <asp:SqlDataSource ID="sqlDSModSynopsisFrom" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="rpt_modified_synopsisfrom" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtFromDate" Direction="Input" Name="fromdate" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="txtToDate" Direction="Input" Name="todate" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="ddlfrom" Direction="Input" Name="from" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <script type="text/javascript">
        function getQueryStrings() {
            var assoc = {};
            var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
            var queryString = location.search.substring(1);
            var keyValues = queryString.split('&');

            for (var i in keyValues) {
                var key = keyValues[i].split('=');
                if (key.length > 1) {
                    assoc[decode(key[0])] = decode(key[1]);
                }
            }
            return assoc;
        }
        var qs = getQueryStrings();
        var controlid = qs["controlid"];
        document.getElementById(controlid).focus();
        document.getElementById(controlid).select();      
    </script>
</asp:Content>
