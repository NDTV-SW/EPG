<%@ Page Title="Error Report" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="ErrorReport.aspx.vb"
    Inherits="EPG.ErrorReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ OutputCache Duration="60" Location="Server" VaryByParam="None" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <div class="col-lg-3 col-md-3 col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtErrorDate" runat="server" CssClass="form-control" />
                            <span class="input-group-addon">
                                <asp:Image ID="imgtxtErrorDate" runat="server" ImageUrl="~/Images/calendar.png" />&nbsp;&nbsp;&nbsp;
                                <asp:CalendarExtender ID="CE_ErrorDate" DefaultView="Days" Format="MM/dd/yyyy" Enabled="True"
                                    PopupButtonID="imgtxtErrorDate" PopupPosition="BottomLeft" runat="server" TargetControlID="txtErrorDate" />
                            </span>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <div class="input-group">
                            <asp:Button ID="btnView" runat="server" Text="VIEW" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <asp:GridView ID="grdErrorReport" runat="server" AutoGenerateColumns="False" CssClass="table"
                        EmptyDataText="No Record Found" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        Width="100%" SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ErrorDateTime" HeaderText="TimeStamp" SortExpression="ErrorDateTime1" />
                            <asp:BoundField DataField="ErrorPage" HeaderText="Error Page" SortExpression="ErrorPage" />
                            <asp:BoundField DataField="ErrorSource" HeaderText="Source" SortExpression="ErrorSource" />
                            <asp:BoundField DataField="ErrorType" HeaderText="Type" SortExpression="ErrorType" />
                            <asp:BoundField DataField="ErrorMessage" HeaderText="Message" SortExpression="ErrorMessage" />
                            <asp:BoundField DataField="LoggedInUser" HeaderText="User" SortExpression="LoggedInUser" />
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
    </div> </div>
</asp:Content>
