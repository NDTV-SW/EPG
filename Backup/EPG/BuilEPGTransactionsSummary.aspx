<%@ Page Title="Buil EPG Transactions Summary" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="BuilEPGTransactionsSummary.aspx.vb"
    Inherits="EPG.BuilEPGTransactionsSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Summary Build EPG Transactions</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
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
                                    Airtel DTH Channels</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkDTH" runat="server" CssClass="form-control" AutoPostBack="true" />
                                    <span class="input-group-addon">
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="SearchChannel"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtChannel" FirstRowSelected="true" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:GridView ID="grdEPGTransactions" runat="server" CssClass="table" AutoGenerateColumns="False"
                            EmptyDataText="No Record Found" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red"
                            CellPadding="4" HorizontalAlign="Center" ForeColor="#333333" GridLines="Vertical"
                            Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                            SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ChannelID" HeaderText="ChannelID" />
                                <asp:BoundField DataField="EPGDate1" HeaderText="EPGDate" />
                                <asp:BoundField DataField="LastUpdate1" HeaderText="LastUpdate" />
                                <asp:TemplateField HeaderText="SynopsisChecked">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSynopsisChecked" runat="server" Checked='<%# bind("SynopsisChecked") %>' />
                                        <asp:Label ID="lbRowid" runat="server" Text='<%# bind("Rowid") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="updatedBy" HeaderText="updatedBy" />
                                <asp:BoundField DataField="updatedAt" HeaderText="updatedAt" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info btn-xs"
                                            CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' CommandName="updateSynopsis" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#D7E2F7" />
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
