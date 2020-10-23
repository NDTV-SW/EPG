<%@ Page Title="Copy EPG to Other Channel" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="CopyEPGtoChannel.aspx.vb" Inherits="EPG.CopyEPGtoChannel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Copy Schedule to Other Channel</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Source Channel
                                </label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannelName" runat="server" DataTextField="sourceChannel" DataValueField="sourceChannel" DataSourceID="SqlDsChannelMaster"  AutoPostBack="true" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="SELECT '---Select Channel---' sourceChannel union SELECT sourceChannel from mst_duplicate_channel_epg order by 1">

                                        </asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Destination Channel</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlDestinationChannel" DataTextField="destinationChannel" DataValueField="destinationChannel" DataSourceID="sqlDSDestinationChannel" runat="server" AutoPostBack="true" CssClass="form-control" />
                                        <span class="input-group-addon">
                                            <asp:SqlDataSource ID="sqlDSDestinationChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="SELECT destinationChannel from mst_duplicate_channel_epg where sourceChannel=@sourceChannel order by 1">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="ddlChannelName" Name="sourceChannel" PropertyName="SelectedValue" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </span>
                                    </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Start Date
                                    <br />
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Enabled="true"
                                        placeholder="Select Start Date" ValidationGroup="RFGCopyEPGToChannel"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate"
                                            PopupButtonID="imgStartDateCalendar" />
                                        <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate"
                                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGCopyEPGToChannel"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    End Date
                                    <br />
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEndDate" runat="server" Enabled="true" CssClass="form-control"
                                        Placeholder="End Date" ValidationGroup="RFGCopyEPGToChannel"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate"
                                            PopupButtonID="imgEndDateCalendar" />
                                        <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtStartDate" runat="server"
                                            ForeColor="Red" Text="*" ValidationGroup="RFGCopyEPGToChannel" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;
                                    <br />
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnCopy" runat="server" Text="Copy EPG to Other Channel" Visible="True"
                                        ValidationGroup="RFGCopyEPGToChannel" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <asp:Label ID="lbError" runat="server" ForeColor="Red" Font-Size="Larger" Visible="false" />
                            <asp:Label ID="lbStatus" runat="server" ForeColor="Green" Font-Size="Larger" Visible="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <asp:Label ID="lbEPGDates" runat="server" ForeColor="Red" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <asp:Label ID="lbDestinationEPGDates" runat="server" ForeColor="Red" /></label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
