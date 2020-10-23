<%@ Page Title="Update PPV" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="UpdatePPV1_new.aspx.vb"
    Inherits="EPG.UpdatePPV1_new" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style>
        .ajax__DropDownList_itemlist
        {
            position: absolute !important;
            height: 100px !important;
            overflow: auto !important;
            top: auto !important;
            left: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Generate PPV</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Channel Name</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannelID" runat="server" DataSourceID="SqlDsChannelMaster"
                                        DataTextField="ChannelId" DataValueField="ChannelId" AutoPostBack="true" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Movie Name</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlMovieName" runat="server" CssClass="form-control" DataSourceID="SqlDsMovies"
                                        DataTextField="progname" DataValueField="progname" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Movie Language</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" AutoCompleteMode="SuggestAppend"
                                        DropDownStyle="DropDownList" ItemInsertLocation="Append" DataTextField="FullName"
                                        DataValueField="FullName" AppendDataBoundItems="true" DataSourceID="SqlmstLanguage">
                                    </asp:DropDownList>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Show Pass</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtShowPass" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtShowPass" ControlToValidate="txtShowPass" runat="server"
                                            ForeColor="Red" Text="*" ValidationGroup="RFGUpdatePPV"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Show Ticket</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtShowTicket" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtShowTicket" ControlToValidate="txtShowTicket"
                                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGUpdatePPV"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Channel Pass</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannelPass" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtChannelPass" ControlToValidate="txtChannelPass"
                                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGUpdatePPV"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Week Pass</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtWeekPass" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtWeekPass" ControlToValidate="txtWeekPass" runat="server"
                                            ForeColor="Red" Text="*" ValidationGroup="RFGUpdatePPV"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Month Pass</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtMonthPass" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtMonthPass" ControlToValidate="txtMonthPass"
                                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGUpdatePPV"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    F1 Duration</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtFillerDuration" runat="server" CssClass="form-control" ValidationGroup="VGBigTV" />
                                    <span class="input-group-addon alert-danger">24 hr format only
                                        <asp:MaskedEditExtender ID="maskTime" runat="server" AcceptAMPM="false" AutoComplete="true"
                                            InputDirection="LeftToRight" Mask="99:99:99" MaskType="Time" MessageValidatorTip="true"
                                            TargetControlID="txtFillerDuration" />
                                        <asp:RequiredFieldValidator ID="RFVtxtTime" runat="server" ControlToValidate="txtFillerDuration"
                                            Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="Error Time Required"
                                            ValidationGroup="VGBigTV" />
                                        <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="maskTime"
                                            ValidationGroup="VGBigTV" ControlToValidate="txtFillerDuration" Display="Dynamic"
                                            EmptyValueMessage="Enter Time" ErrorMessage="Error time" InvalidValueMessage="Invalid"
                                            IsValidEmpty="true" MaximumValue="23:59:59" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    F2 Duration</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtF2Duration" runat="server" CssClass="form-control" ValidationGroup="VGBigTV" />
                                    <span class="input-group-addon alert-danger">24 hr format only
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptAMPM="false" AutoComplete="true"
                                            InputDirection="LeftToRight" Mask="99:99:99" MaskType="Time" MessageValidatorTip="true"
                                            TargetControlID="txtF2Duration" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtF2Duration"
                                            Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="Error Time Required"
                                            ValidationGroup="VGBigTV" />
                                        <asp:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="maskTime"
                                            ValidationGroup="VGBigTV" ControlToValidate="txtF2Duration" Display="Dynamic"
                                            EmptyValueMessage="Enter Time" ErrorMessage="Error time" InvalidValueMessage="Invalid"
                                            IsValidEmpty="true" MaximumValue="23:59:59" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Movie Duration</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtMovieDuration" runat="server" CssClass="form-control" ValidationGroup="VGBigTV" />
                                    <span class="input-group-addon alert-danger">24 hr format only
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="false"
                                            AutoComplete="true" InputDirection="LeftToRight" Mask="99:99:99" MaskType="Time"
                                            MessageValidatorTip="true" TargetControlID="txtMovieDuration" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMovieDuration"
                                            Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="Error Time Required"
                                            ValidationGroup="VGBigTV" />
                                        <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                            ValidationGroup="VGBigTV" ControlToValidate="txtMovieDuration" Display="Dynamic"
                                            EmptyValueMessage="Enter Time" ErrorMessage="Error time" InvalidValueMessage="Invalid"
                                            IsValidEmpty="true" MaximumValue="23:59:59" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>
                                    Display Duration</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDisplayDuration" runat="server" CssClass="form-control" ValidationGroup="VGBigTV" />
                                    <span class="input-group-addon alert-danger">24 hr format only
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="false"
                                            AutoComplete="true" InputDirection="LeftToRight" Mask="99:99:99" MaskType="Time"
                                            MessageValidatorTip="true" TargetControlID="txtDisplayDuration" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDisplayDuration"
                                            Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="Error Time Required"
                                            ValidationGroup="VGBigTV" />
                                        <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                            ValidationGroup="VGBigTV" ControlToValidate="txtDisplayDuration" Display="Dynamic"
                                            EmptyValueMessage="Enter Time" ErrorMessage="Error time" InvalidValueMessage="Invalid"
                                            IsValidEmpty="true" MaximumValue="23:59:59" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:GridView ID="grdMissingChannels" runat="server" DataSourceID="sqlDSMissingChannles"
                                HeaderStyle-BackColor="LightBlue" AutoGenerateColumns="true" EmptyDataText="No New Programmes to be Added"
                                EmptyDataRowStyle-BackColor="LightGreen">
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="RFGUpdatePPV"
                                        CssClass="btn btn-info" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="grdUpdatePPV" runat="server" AutoGenerateColumns="False" CssClass="table"
                            EmptyDataText="No Record Found" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                            Width="95%" SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="~/Images/delete.png" ShowSelectButton="true"
                                    ButtonType="Image" SelectImageUrl="~/Images/edit32.png" />
                                <asp:TemplateField HeaderText="Row Id" SortExpression="Rowid" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRowid" runat="server" Text='<%# Bind("Rowid") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Channel" SortExpression="ChannelNo" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelNo" runat="server" Text='<%# Bind("ChannelNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Channel" SortExpression="ChannelId" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Show Pass" SortExpression="ShowPass" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbShowPass" runat="server" Text='<%# Bind("ShowPass") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Show Ticket" SortExpression="ShowTicket" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbShowTicket" runat="server" Text='<%# Bind("ShowTicket") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Channel Pass" SortExpression="ChannelPass" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelPass" runat="server" Text='<%# Bind("ChannelPass") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Week Pass" SortExpression="WeekPass" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbWeekPass" runat="server" Text='<%# Bind("WeekPass") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month Pass" SortExpression="MonthPass" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMonthPass" runat="server" Text='<%# Bind("MonthPass") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Movie Name" SortExpression="MovieName" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMovieName" runat="server" Text='<%# Bind("MovieName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F1 Duration" SortExpression="FillerDuration" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbFillerDuration" runat="server" Text='<%# Bind("FillerDuration") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F2 Duration" SortExpression="F2Duration" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbF2Duration" runat="server" Text='<%# Bind("F2Duration") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Movie Duration" SortExpression="MovieDuration" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMovieDuration" runat="server" Text='<%# Bind("MovieDuration") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Duration" SortExpression="totalduration" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDisplayDuration" runat="server" Text='<%# Bind("totalduration") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F1 Duration" SortExpression="FillerDuration" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbFillerD" runat="server" Text='<%# Bind("FillerDuration1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F2 Duration" SortExpression="F2Duration" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbF2D" runat="server" Text='<%# Bind("F2Duration1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Movie Duration" SortExpression="MovieDuration" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMovieD" runat="server" Text='<%# Bind("MovieDuration1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Display Duration" SortExpression="totalduration" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDisplayD" runat="server" Text='<%# Bind("totalduration1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Language" SortExpression="MovieLang" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMovieLang" runat="server" Text='<%# Bind("MovieLang") %>' />
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
                <div class="panel-heading text-center">
                    <h3>
                        Generate XML
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Start date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:CalendarExtender ID="txtStartDate_CalendarExtender" TargetControlID="txtStartDate"
                                            PopupButtonID="imgStartDate" runat="server" Enabled="True" Format="yyyy-MM-dd" />
                                        <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:RequiredFieldValidator ID="RFVStartDate" ControlToValidate="txtStartDate" ForeColor="Red"
                                            runat="server" ValidationGroup="Descripency" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-6 col-sm-6">
                            <div class="form-group">
                                <label>
                                    No. Of Days</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDays" runat="server" CssClass="form-control" ValidationGroup="days"
                                        CausesValidation="true" AutoPostBack="true" />
                                    <%--<asp:DropDownList ID="ddlDays" runat="server" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Text="1" Value="0" />
                                            <asp:ListItem Text="2" Value="1" />
                                            <asp:ListItem Text="3" Value="2" />
                                            <asp:ListItem Text="4" Value="3" />
                                            <asp:ListItem Text="5" Value="4" />
                                            <asp:ListItem Text="6" Value="5" />
                                            <asp:ListItem Text="7" Value="6" />
                                            <asp:ListItem Text="8" Value="7" />
                                            <asp:ListItem Text="9" Value="8" />
                                            <asp:ListItem Text="10" Value="9" />
                                            <asp:ListItem Text="11" Value="10" />
                                            <asp:ListItem Text="12" Value="11" />
                                            <asp:ListItem Text="13" Value="12" />
                                            <asp:ListItem Text="14" Value="13" />
                                            <asp:ListItem Text="15" Value="14" />
                                            <asp:ListItem Text="16" Value="15" />
                                            <asp:ListItem Text="17" Value="16" />
                                            <asp:ListItem Text="18" Value="17" />
                                            <asp:ListItem Text="19" Value="18" />
                                            <asp:ListItem Text="20" Value="19" />
                                            <asp:ListItem Text="21" Value="20" />
                                        </asp:DropDownList>--%>
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REVtxtDays" runat="server" ControlToValidate="txtDays"
                                            ValidationGroup="days" ForeColor="Red" Text="*Number" ValidationExpression="^\d+" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-6 col-sm-6">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnGenerateXML" runat="server" Text="Generate XML" CssClass="btn btn-info"
                                        ValidationGroup="Descripency" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-6 col-sm-6">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:HyperLink ID="hyViewXml" runat="server" Visible="false" Target="_blank">View XML</asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 alert-danger">
                            <asp:Label ID="lbEPGExists" Font-Bold="true" runat="server" Text="EPG Exists" />
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6 alert-success">
                            <div class="form-group">
                                <label>
                                    Channel has fresh EPG data for :
                                </label>
                                <div class="input-group">
                                    <asp:GridView ID="grdXMLGenerated" runat="server" ShowHeader="False" CssClass="table"
                                        HorizontalAlign="Center" Width="100%" CellPadding="4" DataSourceID="SqlDSXMLGenerated"
                                        EmptyDataText="No Records to display" GridLines="Horizontal" AllowSorting="True"
                                        SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header"
                                        AllowPaging="True" PageSize="5" BackColor="White" BorderColor="#336666" BorderStyle="Double"
                                        BorderWidth="3px">
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 alert-danger">
                            <asp:Label ID="lbEndDate" runat="server" Font-Bold="true" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lbGenerateXML" Font-Bold="true" runat="server" Text="EPG Exists" />
    <asp:SqlDataSource ID="sqlDSMissingChannles" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="sp_ppv_addprograms" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlChannelID" Name="ChannelId" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlmstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select FullName from mst_language where active=1"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSUpdatePPV" runat="server" SelectCommand="select * from mst_channel_ppv order by channelID, moviename asc"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" DeleteCommand="" />
    <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT ChannelId FROM mst_ppv order by channelid"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSMovies" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT progid,progname from mst_program where ChannelId=@ChannelId">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlChannelID" Name="ChannelId" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSXMLGenerated" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select distinct progdate from mst_epg where ChannelId=@ChannelId and xml_generated=0 and convert(varchar,Progdate,112)>=convert(varchar,dbo.GetLocalDate(),112)order by progdate">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlChannelID" Name="ChannelId" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </div>
</asp:Content>
