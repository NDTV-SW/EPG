<%@ Page Title="Copy Last Week EPG" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="CopyLastWeekEPG.aspx.vb" Inherits="EPG.CopyLastWeekEPG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Copy Last week EPG</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Select Channel
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                                    <span class="input-group-addon">
                                            <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtChannel" FirstRowSelected="true" />
                                        <asp:RequiredFieldValidator ID="RFV_txtChannel" runat="server" Text="*" ForeColor="red" ValidationGroup="RFGCopyEPGToChannel" ControlToValidate="txtChannel" />
                                    </span>
                                </div>
                            </div>
                            <br />
                            <asp:Label ID="lbEPGDates" runat="server" ForeColor="Gray" Font-Bold="true" />
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Start Date
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Enabled="false" ValidationGroup="RFGCopyLastWeekEPG" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate"
                                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGCopyLastWeekEPG"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Select Days
                                </label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlDays" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="5" Value="5" />
                                        <asp:ListItem Text="6" Value="6" />
                                        <asp:ListItem Text="7" Value="7" />
                                    </asp:DropDownList>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnCopy" runat="server" Text="Copy EPG from last week" CssClass="btn btn-info"
                                        Visible="True" ValidationGroup="RFGCopyLastWeekEPG" />
                                    <br />
                                    <asp:Label ID="lbError" runat="server" ForeColor="Red" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:GridView ID="grdCopyLastWeekdata" runat="server" AutoGenerateColumns="false"
                                CssClass="table" EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333"
                                GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Program ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprogid" runat="server" Text='<%# Eval("progid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Program Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprogname" runat="server" Text='<%# Eval("progname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Synopsis">
                                        <ItemTemplate>
                                            <asp:Label ID="lbSynopsis" runat="server" Text='<%# Eval("Synopsis") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="350px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AirDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprogdate" runat="server" Text='<%# Eval("progdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AirTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprogtime" runat="server" Text='<%# Eval("progtime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Duration" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lbduration" runat="server" Text='<%# Eval("duration") %>'></asp:Label>
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
    </div>
</asp:Content>
