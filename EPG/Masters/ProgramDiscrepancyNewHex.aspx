<%@ Page Title="Program Discrepancies Hex" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="ProgramDiscrepancyNewHex.aspx.vb"
    Inherits="EPG.ProgramDiscrepancyNewHex" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function copyProgramToSynopsis(progname, rownumber) {
            //alert(rownumber);
            document.getElementById('MainContent_grdProgrammaster_txtSynopsis_' + rownumber).value = progname;
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Programme Discrepancies HEX</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <div class="form-group">
                            <label>
                                Channel</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                                <div class="input-group-addon">
                                    <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtChannel" FirstRowSelected="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
               
            </div>
        </div>
        <div id="tblXML" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Generate XML</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    Start Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:RequiredFieldValidator ID="RFVStartDate" ControlToValidate="txtStartDate" ForeColor="Red"
                                            runat="server" ValidationGroup="Descripency" Text="*" ErrorMessage="* (Start date cannot be left blank)"></asp:RequiredFieldValidator>
                                        <asp:CalendarExtender ID="txtStartDate_CalendarExtender" TargetControlID="txtStartDate"
                                            PopupButtonID="imgStartDate" runat="server" Enabled="True" Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <asp:Label ID="lbEPGExists" runat="server" Text="EPG Exists" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    No. Of Days</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlDays" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="1" Value="0" />
                                        <asp:ListItem Text="2" Value="1" />
                                        <asp:ListItem Text="3" Value="2" />
                                        <asp:ListItem Text="4" Value="3" />
                                        <asp:ListItem Text="5" Value="4" />
                                        <asp:ListItem Text="6" Value="5" />
                                        <asp:ListItem Text="7" Value="6" />
                                    </asp:DropDownList>
                                    <div class="input-group-addon">
                                    </div>
                                    <asp:Label ID="lbGenerateXML" Text="XML Generated Successfully" CssClass="alert alert-danger"
                                        runat="server" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnGenerateXML" runat="server" Text="Generate XML" UseSubmitBehavior="false"
                                        CssClass="btn btn-success" ValidationGroup="Descripency" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnGenerateXMLAgain" runat="server" Font-Bold="true" Text="Generate XML Again"
                                        CssClass="btn btn-info" UseSubmitBehavior="false" ValidationGroup="Descripency1" /><br />
                                    <asp:HyperLink ID="hyViewXml" runat="server" Visible="false" Target="_blank">View XML</asp:HyperLink>
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Channel has fresh EPG data for:
                                </label>
                                <div class="input-group">
                                    <asp:GridView ID="grdXMLGenerated" runat="server" ShowHeader="False" align="center"
                                        Width="100%" CellPadding="4" EmptyDataRowStyle-BackColor="LightGreen" EmptyDataText="No Records to display"
                                        ForeColor="Black" GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="7"
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                        BorderWidth="1px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EmptyDataRowStyle BackColor="LightGreen"></EmptyDataRowStyle>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                        <Columns>
                                            <asp:BoundField DataField="ProgDate1" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    You can generate XML again for:
                                </label>
                                <div class="input-group">
                                    <asp:GridView ID="grdGenerateAgain" runat="server" ShowHeader="False" align="center"
                                        Width="100%" CellPadding="4" EmptyDataRowStyle-BackColor="LightGreen" EmptyDataText="No Records to display"
                                        ForeColor="Black" GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="5"
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                        BorderWidth="1px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EmptyDataRowStyle BackColor="LightGreen"></EmptyDataRowStyle>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                        <Columns>
                                            <asp:BoundField DataField="ProgDate1" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
