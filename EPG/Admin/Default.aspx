<%@ Page Title="Admin" Language="vb" MasterPageFile="SiteAdmin.Master"
    AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="EPG.AdminDefault" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div class="container">

        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>Delete EPG dates</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />

                                    <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtChannel" FirstRowSelected="true" />
                                </div>
                            </div>
                            <br />
                            <asp:Label ID="lbEPGdates" runat="server" Text="" CssClass="btn alert-danger" />
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Start Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtStartDate" placeholder="Start Date" runat="server" CssClass="form-control"
                                        ValidationGroup="RFGDelEPG" />
                                    <span class="input-group-addon">
                                        <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate"
                                            PopupButtonID="txtStartDate" />
                                        <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate"
                                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGDelEPG" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    End Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEndDate" placeholder="End Date" runat="server" CssClass="form-control"
                                        ValidationGroup="RFGDelEPG" />
                                    <span class="input-group-addon">
                                        <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate"
                                            PopupButtonID="txtEndDate" />
                                        <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server"
                                            ForeColor="Red" Text="*" ValidationGroup="RFGDelEPG" />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate"
                                            ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date"
                                            ErrorMessage="*" ValidationGroup="RFGDelEPG" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12 ">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-xs btn-danger" ValidationGroup="RFGDelEPG" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>Delete Old Rowid (mst_epg_existing)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        Old Row Id</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtRowid" runat="server" CssClass="form-control" AutoPostBack="true" />
                                        <span class="input-group-addon">
                                            <asp:RequiredFieldValidator ID="RFVtxtRowid" ControlToValidate="txtRowid" runat="server"
                                                ForeColor="Red" Text="*" ValidationGroup="RFGDelRowid" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 ">
                                <div class="form-group">
                                    <label>
                                        &nbsp;</label>
                                    <div class="input-group">
                                        <asp:Button ID="btnDelOldRowid" runat="server" Text="Delete" CssClass="btn btn-xs btn-danger" ValidationGroup="RFGDelRowid" />
                                        <%--&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnNull" runat="server" Text="Set Null" CssClass="btn btn-xs btn-danger" ValidationGroup="RFGDelEPG" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <asp:GridView ID="grdOldRowid" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
        </div>

    </div>

</asp:Content>
