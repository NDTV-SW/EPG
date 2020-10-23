<%@ Page Title="Banners" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="Banners.aspx.vb" Inherits="EPG.Banners" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="http://epgops.ndtv.com/lightbox.css" rel="stylesheet" type="text/css" />
    <script src="http://epgops.ndtv.com/lightbox.js" type="text/javascript"></script>
    <script src="../js/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../js/calendar-en.min.js" type="text/javascript"></script>
    <link href="../css/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtNotTime  .ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%Y/%m/%d %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Banners</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Select Channel</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlChannel" runat="server" DataTextField="channelid" DataValueField="channelid"
                                    DataSourceID="SqlmstChannel" CssClass="form-control" AutoPostBack="true" />
                                <asp:SqlDataSource ID="SqlmstChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                    SelectCommand="SELECT channelid FROM mst_channel where onair=1 and CompanyId<>28 and publicchannel=1 Union Select Channelid from mst_ppv order by 1">
                                </asp:SqlDataSource>
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Select Programme</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlProgramme" runat="server" DataTextField="Progname" DataValueField="Progid"
                                    DataSourceID="sqlMstProgram" CssClass="form-control" AutoPostBack="true" />
                                <asp:SqlDataSource ID="sqlMstProgram" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                    SelectCommand="SELECT progid,Progname from mst_program where active=1 and channelid=@channelid and progid in (select progid from mst_epg where channelid=@channelid and convert(varchar, progdate,112) >= convert(varchar, dbo.getLocalDate(),112) ) order by progname">
                                    <SelectParameters>
                                        <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue"
                                            Direction="Input" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Select Language</label>
                            <div class="input-group">
                                <%--<asp:DropDownList ID="ddllanguage" runat="server" DataTextField="fullname" DataValueField="languageid"
                                    DataSourceID="sqlMstLanguage" CssClass="form-control" />--%>
                                <asp:ListBox ID="lstlanguage" runat="server" DataTextField="fullname" DataValueField="languageid"
                                    DataSourceID="sqlMstLanguage" CssClass="form-control" SelectionMode="Multiple" Rows="1" />

                                <asp:SqlDataSource ID="sqlMstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                    SelectCommand="SELECT top 2 languageid,fullname from mst_language where active=1 order by 1">
                                </asp:SqlDataSource>
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Program Type</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlProgType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Show" Value="shows" />
                                    <asp:ListItem Text="Movie" Value="movies" />
                                    <asp:ListItem Text="Promo" Value="promos" />
                                    <asp:ListItem Text="Offer" Value="offers" />
                                    <asp:ListItem Text="PPV" Value="ppv" />
                                </asp:DropDownList>
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Start Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" />
                                <span class="input-group-addon">
                                    <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate"
                                        PopupButtonID="imgStartDateCalendar" />
                                    <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate"
                                        runat="server" ForeColor="Red" Text="*" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                End Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" AutoPostBack="true" />
                                <span class="input-group-addon">
                                    <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate"
                                        PopupButtonID="imgEndDateCalendar" />
                                    <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server"
                                        ForeColor="Red" Text="*" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate"
                                        ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date"
                                        ErrorMessage="*" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Start Time</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtStartTime" Text="00:00:01" runat="server" CssClass="form-control" />
                                <span class="input-group-addon">24 hr format
                                    <asp:MaskedEditExtender ID="MEEtxtStartTime" runat="server" AcceptAMPM="false" AutoComplete="true"
                                        InputDirection="LeftToRight" Mask="99:99:99" MaskType="Time" MessageValidatorTip="true"
                                        TargetControlID="txtStartTime" />
                                    <asp:RequiredFieldValidator ID="RFVtxtStartTime" runat="server" ControlToValidate="txtStartTime"
                                        Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="Start Time Required" />
                                    <asp:MaskedEditValidator ID="MEVtxtStartTime" runat="server" ControlExtender="MEEtxtStartTime"
                                        ControlToValidate="txtStartTime" Display="Dynamic" EmptyValueMessage="Enter Time"
                                        ErrorMessage="Error time" InvalidValueMessage="Time invalid" IsValidEmpty="true"
                                        MaximumValue="23:59:59" ForeColor="Red" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                End Time</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtEndTime" Text="23:59:59" runat="server" CssClass="form-control" />
                                <span class="input-group-addon">24 hr format
                                    <asp:MaskedEditExtender ID="MEEtxtEndTime" runat="server" AcceptAMPM="false" AutoComplete="true"
                                        InputDirection="LeftToRight" Mask="99:99:99" MaskType="Time" MessageValidatorTip="true"
                                        TargetControlID="txtEndTime" />
                                    <asp:RequiredFieldValidator ID="RFVtxtEndTime" runat="server" ControlToValidate="txtEndTime"
                                        Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="End Time Required" />
                                    <asp:MaskedEditValidator ID="MEVtxtEndTime" runat="server" ControlExtender="MEEtxtEndTime"
                                        ControlToValidate="txtEndTime" Display="Dynamic" EmptyValueMessage="Enter Time"
                                        ErrorMessage="Error time" InvalidValueMessage="Time invalid" IsValidEmpty="true"
                                        MaximumValue="23:59:59" ForeColor="Red" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Upload Image</label>
                            <div class="input-group">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                <span class="input-group-addon">
                                    <asp:Button ID="btnUpload" Text="Upload" runat="server" CausesValidation="true" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Image</label>
                            <div class="input-group">
                                <asp:Image ID="imgPreview" runat="server" Width="144" Height="54" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Priority
                            </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPriority" runat="server" Text="0" CssClass="form-control" MaxLength="2" />
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFVtxtPriority" ControlToValidate="txtPriority" runat="server"
                                        ForeColor="Red" Text="*" limit="3" />
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Notification Title
                            </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtNotTitle" runat="server" CssClass="form-control" />
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFVtxtNotTitle" ControlToValidate="txtNotTitle"
                                        runat="server" ForeColor="Red" Text="*" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Notification Body
                            </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtNotBody" runat="server" CssClass="form-control" TextMode="MultiLine"
                                    Rows="3" />
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFVtxtNotBody" ControlToValidate="txtNotBody"
                                        runat="server" ForeColor="Red" Text="*" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Time to Send
                            </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtNotTime" runat="server" CssClass="form-control" />
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFV_txtNotTime" runat="server" ControlToValidate="txtNotTime"
                                        Text="*" ForeColor="Red" />
                                    <img src="../images/calendar.png" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                            </label>
                            <div class="input-group">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                <asp:Label ID="lbRowid" runat="server" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row alert-info">
                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                        <div class="form-group">
                            <label>
                                Search Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlSearchlanguage" runat="server" DataTextField="fullname"
                                    DataValueField="languageid" DataSourceID="sqlMstLanguage" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-4 col-xs-4">
                        <div class="form-group">
                            <label>
                            </label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkActive" runat="server" Text="Active ?" Checked="true" CssClass="btn btn-info" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                        <div class="form-group">
                            <label>
                            </label>
                            <div class="input-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false"
                                    CssClass="btn btn-success" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdBanners" runat="server" AutoGenerateColumns="False" CssClass="table"
                    DataSourceID="sqlDSBanners" AllowPaging="True" AllowSorting="True" CellPadding="4"
                    ForeColor="Black" GridLines="Vertical" PageSize="50" BackColor="White" BorderColor="#DEDFDE"
                    BorderStyle="None" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sno.">
                            <ItemTemplate>
                                <asp:Label ID="lbsno" runat="server" />
                                <asp:Label runat="server" ID="lbProgId" Text='<%# Bind("progid") %>' Visible="false" />
                                <asp:Label ID="lbRowId" runat="server" Text='<%#Eval("rowid") %>' Visible="false" />
                                <asp:Label ID="lblanguageid" runat="server" Text='<%#Eval("languageid") %>' Visible="false" />
                                <asp:Label ID="lbNotTitle" runat="server" Text='<%#Eval("notificationtitle") %>' Visible="false" />
                                <asp:Label ID="lbNotBody" runat="server" Text='<%#Eval("notificationbody") %>' Visible="false" />
                                <asp:Label ID="lbNotTime" runat="server" Text='<%#Eval("notificationtime") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("ChannelId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Programme" SortExpression="progname">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbProgName" Text='<%# Bind("progname") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" SortExpression="progtype">
                            <ItemTemplate>
                                <asp:Label ID="lbprogtype" runat="server" Text='<%#Eval("progtype") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Language" SortExpression="fullname">
                            <ItemTemplate>
                                <asp:Label ID="lbLanguage" runat="server" Text='<%#Eval("fullname") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date" SortExpression="startdate">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbstartdate" Text='<%# Bind("startdate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date" SortExpression="enddate">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbenddate" Text='<%# Bind("enddate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Time" SortExpression="starttime">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbstarttime" Text='<%# Bind("starttime") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Time" SortExpression="endtime">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbendtime" Text='<%# Bind("endtime") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server" NavigateUrl='<%# Bind("bannerurl") %>'>
                                    <asp:Image runat="server" ID="imglogo" ImageUrl='<%# Bind("bannerurl") %>' AlternateText="NDTV"
                                        Width="150px" Height="60px" />
                                </asp:HyperLink></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Priority">
                            <ItemTemplate>
                                <asp:Label ID="lbpriority" runat="server" Text='<%# Bind("priority") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" />
                        <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Images/delete.png" />
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
                <asp:SqlDataSource ID="sqlDSBanners" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="select * from v_mst_banners where languageid=@languageid and active=@active order by priority">
                    <SelectParameters>
                        <asp:ControlParameter Name="languageid" ControlID="ddlSearchLanguage" PropertyName="SelectedValue"
                            DbType="Int16" Direction="Input" />
                        <asp:ControlParameter Name="active" ControlID="chkActive" PropertyName="Checked"
                            DbType="Boolean" Direction="Input" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
