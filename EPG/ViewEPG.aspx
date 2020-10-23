<%@ Page Title="View EPG" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="ViewEPG.aspx.vb" Inherits="EPG.ViewEPG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .csspager a
        {
            padding-left: 10px;
            padding-right: 10px;
            font-weight: bold;
        }
    </style>

     <script type="text/javascript">

         function openWin(richmetaid) {
             window.open("richmeta/viewrich.aspx?id=" + richmetaid, "View Rich", "width=1000,height=480,toolbar=0,left=100,top=100,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    View EPG</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Channel</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                                <span class="input-group-addon">
                                    <asp:Label ID="lbIsMovieChannel" runat="server" />
                                    <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtChannel" FirstRowSelected="true" />
                                    <asp:RequiredFieldValidator ID="RFV_txtChannel" runat="server" ControlToValidate="txtChannel"
                                        Text="*" ForeColor="Red" ValidationGroup="RFGViewEPG" />
                                </span>
                            </div>
                            <br />
                            <asp:Label ID="lbEPGExists" runat="server" Text="EPG Exists" Visible="false" />
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Select Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlLanguage1" runat="server" DataTextField="FullName" CssClass="form-control"
                                    DataValueField="LanguageID" DataSourceID="SqlmstLanguage" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="SqlmstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select LanguageID,FullName from mst_language where active=1">
                                    </asp:SqlDataSource>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Start Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtStartDate" placeholder="Start Date" runat="server" CssClass="form-control"
                                    ValidationGroup="RFGViewEPG" />
                                <span class="input-group-addon">
                                    <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate"
                                        PopupButtonID="imgStartDateCalendar" />
                                    <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate"
                                        runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGViewEPG" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                End Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtEndDate" placeholder="End Date" runat="server" CssClass="form-control"
                                    ValidationGroup="RFGViewEPG" />
                                <span class="input-group-addon">
                                    <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate"
                                        PopupButtonID="imgEndDateCalendar" />
                                    <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server"
                                        ForeColor="Red" Text="*" ValidationGroup="RFGViewEPG" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate"
                                        ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date"
                                        ErrorMessage="*" ValidationGroup="RFGViewEPG" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                View GMT ?</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkViewGMT" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-sm btn-info"
                                    ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12 col-lg-offset-2 col-md-offset-2">
                        <asp:Button ID="Button1" runat="server" Text="EXPORT EPG to EXCEL" CssClass="btn btn-sm btn-success"
                            ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Text="EXPORT EPG to EXCEL IN GMT" CssClass="btn btn-sm btn-success"
                            ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button3" runat="server" Text="EXPORT EPG Episodic" CssClass="btn btn-sm btn-success"
                            ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnEntertainment" runat="server" Text="EXPORT Entertainment" CssClass="btn btn-sm alert-info"
                            ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnMovie" runat="server" Text="EXPORT Movie" CssClass="btn btn-sm alert-info"
                            ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSports" runat="server" Text="EXPORT Sports" CssClass="btn btn-sm alert-info"
                            ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hyExcelView" runat="server" Text="View Excel" Visible="false" />
                        <asp:Button ID="btnViewAll" runat="server" Text="View All" ValidationGroup="RFGViewEPG" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:Table ID="tbHead" runat="Server" GridLines="both" CssClass="tbViewEPG table"
                            BorderWidth="2" CellPadding="5" CellSpacing="0">
                            <asp:TableHeaderRow Font-Size="Large" Font-Bold="true" Font-Names="Verdana" HorizontalAlign="Center">
                                <asp:TableCell>
                                
            Air Date 
                                </asp:TableCell><asp:TableCell>
            WeekDay
                                </asp:TableCell><asp:TableCell>
            Week no.
                                </asp:TableCell><asp:TableCell>
            Duration (Mins)
                                </asp:TableCell></asp:TableHeaderRow>
                            <asp:TableRow BackColor="#6B696B" ForeColor="White" Font-Size="Medium" Font-Bold="true"
                                Font-Names="Verdana" HorizontalAlign="Center">
                                <asp:TableCell>
                                    <asp:Label ID="lbDateprog" runat="server" Text="Date" />
                                </asp:TableCell><asp:TableCell>
                                    <asp:Label ID="lbWeekDayprog" runat="server" Text="WeekDay" />
                                </asp:TableCell><asp:TableCell>
                                    <asp:Label ID="lbWeekNumberprog" runat="server" Text="Week No." />
                                </asp:TableCell><asp:TableCell>
                                    <asp:Label ID="lbTotalDuration" runat="server" Text="Total Duration (Mins.)" />
                                </asp:TableCell></asp:TableRow>
                        </asp:Table>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:GridView ID="grdViewEPG" runat="server" AutoGenerateColumns="False" CssClass="table"
                            CellPadding="100" DataKeyNames="ChannelId" AllowPaging="True" PageSize="1" EmptyDataText="No EPG exists for the following Dates !"
                            ForeColor="Black" GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                            SortedDescendingHeaderStyle-CssClass="sortdesc-header" ShowHeader="False" Font-Names="Verdana"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Air Date" ItemStyle-VerticalAlign="Top" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbprogdate" runat="server" Text='<%# Eval("progdate1") %>'></asp:Label></ItemTemplate>
                                    <ControlStyle Width="100" />
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WeekDay" ItemStyle-VerticalAlign="Top" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbWeekDay" runat="server" Text='<%# Eval("WeekDay") %>'></asp:Label></ItemTemplate>
                                    <ControlStyle Width="60" />
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Week" ItemStyle-VerticalAlign="Top" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbWeekNumber" runat="server" Text='<%# Eval("WeekNumber") %>'></asp:Label></ItemTemplate>
                                    <ControlStyle Width="10" />
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:GridView ID="grdChildViewEpg" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                            OnRowDataBound="grdChildViewEpg_Databound" CellPadding="4" CellSpacing="4" ForeColor="#333333"
                                            GridLines="Vertical" CssClass="table" ShowFooter="true" EmptyDataText="No Record Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Prog ID" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogid" runat="server" Text='<%# Eval("progid") %>'></asp:Label>
                                                        <asp:HyperLink ID="hyRichMetaId" runat="server" Text='<%# Eval("richmetaid") %>' />
                                                        <asp:CheckBox ID="chkVerified" runat="server" Checked='<%# Bind("verified") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    <ControlStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Program Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogname" runat="server" Text='<%# Eval("progname") %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="200" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Synopsis" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%# Eval("Synopsis") %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="300" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Genre" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbGenre" runat="server" Text='<%# Eval("GenreName") %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="100" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AirTime" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogtime" runat="server" Text='<%# Eval("progtime") %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="100" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GMT AirDate" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogDateGMT" runat="server" Text='<%# Eval("progtimeGMT") %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="100" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GMT AirTime" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogtimeGMT" runat="server" Text='<%# Eval("progtimeGMT") %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="100" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Duration" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbduration" runat="server" Text='<%# Eval("duration") %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="80" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Epi. No." ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEpisodeNo" runat="server" Text='<%# Eval("EpisodeNo") %>'></asp:Label></ItemTemplate>
                                                    <ControlStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Episodic" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEpisodicSynopsis" runat="server" Text='<%# Eval("EpisodicSynopsis") %>' />
                                                        <asp:Label ID="lbisduplicate" runat="server" Text='<%# Eval("isduplicate") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Original-Repeat" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbLiveRepeat" runat="server" Text='<%# Eval("liverepeat") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Live-Recorded" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbOriginalRepeat" runat="server" Text='<%# Eval("originalrepeat") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PG" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbPG" runat="server" Text='<%# Eval("ratingid") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Season" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbSeason" runat="server" Text='<%# Eval("eseason") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" HorizontalAlign="Right" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" />
                                            <RowStyle BackColor="#F7F7DE" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                            <SortedAscendingHeaderStyle BackColor="#848384" />
                                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                            <SortedDescendingHeaderStyle BackColor="#575357" />
                                        </asp:GridView>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" CssClass="csspager" />
                            <PagerSettings Position="TopAndBottom" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSViewEPG" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select a.ChannelID as ChannelID,Convert(varchar(11),a.progdate,113) as progdate, RIGHT(convert(varchar(25),a.progtime,100),7) as progtime,
      a.duration as duration from mst_epg a join mst_Channel b on a.ChannelID = b.ChannelID where a.ChannelID = @ChannelID and a.progdate
      between @Startdate and @EndDate order by a.progdate,a.progtime asc">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtChannel" Name="ChannelID" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="txtStartDate" Name="Startdate" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="txtEndDate" Name="EndDate" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="grdViewEPGExport" runat="server" Visible="False" CellPadding="4"
        ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
        CssClass="table" BorderStyle="None" BorderWidth="1px">
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
</asp:Content>
