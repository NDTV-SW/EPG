<%@ Page Title="FTP Report" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master" EnableViewState="true" AutoEventWireup="false" CodeBehind="FTPReport.aspx.vb" Inherits="EPG.FTPReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>FTP Logs</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select Channel</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" placeholder="Leave empty for all" />
                                    <span class="input-group-addon">
                                        <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtChannel" FirstRowSelected="true" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtFTPDate" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:CalendarExtender ID="CE_FTPDate" DefaultView="Days" Format="MM/dd/yyyy" Enabled="True" PopupButtonID="imgtxtFTPDate" PopupPosition="BottomLeft" runat="server" TargetControlID="txtFTPDate" />
                                        <asp:RequiredFieldValidator ID="RFVtxtFTPDate" runat="server" ControlToValidate="txtFTPDate" ForeColor="Red" Text="*" />
                                        <asp:Image ID="imgtxtFTPDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                        <asp:GridView ID="grdFTPReport" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found" CssClass="table"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                            SortedDescendingHeaderStyle-CssClass="sortdesc-header" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                         <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Channelid" HeaderText="Channel" SortExpression="Channelid" />
                                <asp:BoundField DataField="Filename" HeaderText="File" SortExpression="Filename" />
                                <asp:BoundField DataField="ftpdate1" HeaderText="FTP Time" SortExpression="ftpdate" />
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
        </div>
    </div>
</asp:Content>
