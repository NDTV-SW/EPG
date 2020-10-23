<%@ Page Title="Graphical Reports" Language="vb" MasterPageFile="~/SiteBootStrap.Master" AutoEventWireup="false"
    CodeBehind="GraphReport.aspx.vb" Inherits="EPG.GraphReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
                                Reports</h3>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-2 col-md-3">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlChannel" CssClass="form-control" runat="server" DataTextField="OperatorChannelid"
                                            DataValueField="ChannelId" DataSourceID="sqlDSChannel" />
                                        <span class="input-group-addon">
                                            <asp:SqlDataSource ID="sqlDSChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>"
                                                SelectCommand="Select '---All Channels---' channelid,'---All Channels---' operatorchannelid,0
                                            union select channelid,replace(replace(operatorchannelid,'&amp','&'),';','') operatorchannelid,1
                                            from dthcable_channelmapping where operatorid=124 and onair=1 order by 3" />
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ValidationGroup="VGBigTVSearch" />
                                        <span class="input-group-addon">
                                            <asp:CalendarExtender ID="CE_txtStartDate" runat="server" PopupButtonID="img_txtStartDate"
                                                TargetControlID="txtStartDate" />
                                            <asp:RequiredFieldValidator ID="RFVtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Text="*" ForeColor="Red" ValidationGroup="VGBigTVSearch" />
                                            <asp:Image ID="img_txtStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ValidationGroup="VGBigTVSearch" />
                                        <span class="input-group-addon">
                                            <asp:CalendarExtender ID="CE_txtEndDate" runat="server" PopupButtonID="img_EndDate"
                                                TargetControlID="txtEndDate" />
                                            <asp:RequiredFieldValidator ID="RFVtxtEndDate" runat="server" ControlToValidate="txtEndDate"
                                                Text="*" ForeColor="Red" ValidationGroup="VGBigTVSearch" />
                                            <asp:CompareValidator ID="CVtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                                                ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date"
                                                ErrorMessage="End date cannot be less than Startdate" Text="*" ValidationGroup="VGBigTVSearch"></asp:CompareValidator>
                                            <asp:Image ID="img_EndDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        </span>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-3">
                                    <div class="input-group">
                                        <asp:Button ID="btnSearch" runat="server" Text="View" ValidationGroup="VGBigTVSearch"
                                            CssClass="btn btn-info" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:Label ID="lbError" runat="server" ForeColor="Red" Text="--- no record found ---" Visible="false" />
                                <asp:Chart ID="Chart1" runat="server" Height="400px" Width="1200px">
                                    <Series>
                                        <asp:Series Name="Series1">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
