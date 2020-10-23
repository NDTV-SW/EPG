<%@ Page Title="VA View EPG" Language="vb" MasterPageFile="~/SiteVisionAsia.Master"
    AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="EPG._DefaultVisionAsia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .gridViewPager td
            {
                padding: 5px;
                font-weight:bold;
                
            }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <%--<asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                <h3>
                    View EPG</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannelName" runat="server" AutoPostBack="True" CssClass="form-control"
                                        DataSourceID="SqlDsChannelMaster" DataTextField="ChannelID" DataValueField="ChannelID" />
                                    <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT ChannelID FROM mst_Channel where active='1' and channelid in (select channelid from dthcable_channelmapping where operatorid=125 and onair=1) ORDER BY ChannelID">
                                    </asp:SqlDataSource>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    Language</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" DataTextField="FullName"
                                        DataValueField="LanguageID" DataSourceID="SqlmstLanguage" />
                                    <asp:SqlDataSource ID="SqlmstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select LanguageID,FullName from mst_language where languageid=1 and active=1">
                                    </asp:SqlDataSource>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    Start Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Enabled="true"
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
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    End Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Enabled="true"
                                        ValidationGroup="RFGViewEPG"></asp:TextBox>
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
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkViewGMT" Text="View GMT" runat="server" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-sm btn-info"
                                        ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnViewAll" runat="server" Text="View All" Width="80px" ValidationGroup="RFGViewEPG" />
                                    
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tbHead" runat="server" visible="false">
                    <div class="row alert-info h3">
                        <div class="col-lg-2 col-md-2 col-sm-6 table-bordered">
                            Air Date
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 table-bordered">
                            WeekDay
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 table-bordered">
                            Week no.
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 table-bordered">
                            Duration (Mins)
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 table-bordered">
                        <asp:Button ID="Button1" runat="server" Text="EXPORT" CssClass="btn btn-sm btn-info"
                                        ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button2" runat="server" Text="EXPORT IN GMT" CssClass="btn btn-sm btn-info"
                                        ValidationGroup="RFGViewEPG" />&nbsp;&nbsp;&nbsp;
                                        </div>
                    </div>
                    <div class="row alert-success h4 table-bordered">
                        <div class="col-lg-2 col-md-2 col-sm-6 table-bordered">
                            <asp:Label ID="lbDateprog" runat="server" Text="Date" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 table-bordered">
                            <asp:Label ID="lbWeekDayprog" runat="server" Text="WeekDay" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 table-bordered">
                            <asp:Label ID="lbWeekNumberprog" runat="server" Text="Week No." />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 table-bordered">
                            <asp:Label ID="lbTotalDuration" runat="server" Text="Total Duration (Mins.)" />
                        </div>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-6">
                        <asp:GridView ID="grdViewEPG" runat="server" AutoGenerateColumns="false" ShowFooter="false"
                            CssClass="table table-bordered" DataKeyNames="ChannelId" AllowPaging="true"
                            PageSize="1" EmptyDataText="No EPG exists for the following Dates !" ForeColor="#333333"
                            GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                            SortedDescendingHeaderStyle-CssClass="sortdesc-header" ShowHeader="false"
                             Font-Names="Verdana" PagerStyle-CssClass="gridViewPager">
                            <AlternatingRowStyle BackColor="White" />
                            <pagersettings mode="NextPreviousFirstLast"
                                firstpagetext="First"
                                lastpagetext="Last"
                                nextpagetext="Next"
                                previouspagetext="Prev"   
                                position="Top"
                                 
                                /> 
                            <Columns>
                                <asp:TemplateField HeaderText="Air Date" ItemStyle-VerticalAlign="Top" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbprogdate" runat="server" Text='<%# Eval("progdate1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WeekDay" ItemStyle-VerticalAlign="Top" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbWeekDay" runat="server" Text='<%# Eval("WeekDay") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Week" ItemStyle-VerticalAlign="Top" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbWeekNumber" runat="server" Text='<%# Eval("WeekNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:GridView ID="grdChildViewEpg" runat="server" AutoGenerateColumns="false" ShowHeader="true" CssClass="table table-bordered"
                                            OnRowDataBound="grdChildViewEpg_Databound" CellPadding="4" CellSpacing="4" ForeColor="#333333"
                                            ShowFooter="true" EmptyDataText="No Record Found"  >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Program Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogname" runat="server" Text='<%# Eval("progname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Synopsis" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%# Eval("Synopsis") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Genre" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbGenre" runat="server" Text='<%# Eval("GenreName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AirTime" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogtime" runat="server" Text='<%# Eval("progtime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GMT AirDate" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogDateGMT" runat="server" Text='<%# Eval("progtimeGMT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GMT AirTime" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbprogtimeGMT" runat="server" Text='<%# Eval("progtimeGMT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Duration" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbduration" runat="server" Text='<%# Eval("duration") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Epi. No." ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEpisodeNo" runat="server" Text='<%# Eval("EpisodeNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>
                    </div>
                    <asp:SqlDataSource ID="sqlDSViewEPG" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                        SelectCommand="select a.ChannelID as ChannelID,Convert(varchar(11),a.progdate,113) as progdate, RIGHT(convert(varchar(25),a.progtime,100),7) as progtime,
      a.duration as duration from mst_epg a join mst_Channel b on a.ChannelID = b.ChannelID where a.ChannelID = @ChannelID and a.progdate
      between @Startdate and @EndDate order by a.progdate,a.progtime asc">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelID" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:ControlParameter ControlID="txtStartDate" Name="Startdate" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="txtEndDate" Name="EndDate" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <div class="col-lg-12 col-md-12 col-sm-6">
                        <asp:GridView ID="grdViewEPGExport" CssClass="form-control" runat="server" Visible="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
