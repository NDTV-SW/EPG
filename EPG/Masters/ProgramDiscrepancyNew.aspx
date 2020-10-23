<%@ Page Title="Program Discrepancies" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="ProgramDiscrepancyNew.aspx.vb"
    Inherits="EPG.ProgramDiscrepancyNew" MaintainScrollPositionOnPostback="true" %>

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
                    Programme Discrepancies</h3>
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
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <div class="form-group">
                            <label>
                                Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" AutoPostBack="true" />
                                <div class="input-group-addon" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="btn-lg alert-danger">
                        <b>Note :</b> New synopsis received in FPC excel(You can Copy, Paste in Program
                        Central if needed).
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grdSynopsis" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        CssClass="table" DataKeyNames="RowId" EmptyDataRowStyle-BackColor="LightGreen"
                        EmptyDataText="All English synopses are matching with the EPG excel." ForeColor="Black"
                        GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="20"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="RowId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbRowId" runat="server" Text='<%#Bind("RowId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgName" runat="server" Text='<%# Bind("ProgName")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Old Synopsis">
                                <ItemTemplate>
                                    <asp:Label ID="lbOldSynop" runat="server" Text='<%# Bind("OldSynop")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New Synopsis">
                                <ItemTemplate>
                                    <asp:Label ID="lbNewSynop" runat="server" Text='<%# Bind("NewSynop")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkFixSynopsis" runat="server" Checked="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="Btn_Fix" runat="server" CausesValidation="False" CommandName="Fix"
                                        CssClass="btn btn-info" Text="Fixed all Languages." CommandArgument='<%# CType(Container,GridViewRow).RowIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
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
                    </asp:GridView>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                    <asp:Button ID="btnFixSynopsis" runat="server" Text="Update Synopsis in Master" Visible="false"
                        CssClass="btn btn-info" />
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                    <div class="btn-lg alert-danger">
                        Regional Synopsis/Names Missing if any:
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-right">
                    <asp:CheckBox ID="chkCheckAll" runat="server" Text="Check all regional" AutoPostBack="true" />
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">
                    <asp:Button ID="btnUpdate" runat="server" Text="Copy Generic" CssClass="btn btn-success" />
                    <asp:Button ID="btnCopyEnglish" runat="server" Text="Copy English" CssClass="btn btn-info" />
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grdProgrammaster" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="LightGreen"
                        EmptyDataText="All regional Name or synopses are missing." CellPadding="4" DataKeyNames="ProgId"
                        CssClass="table" ForeColor="Black" GridLines="Vertical" Width="100%" AllowSorting="True"
                        SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header"
                        AllowPaging="True" PageSize="20" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                        BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbRowId" Text='<%#Eval("RowId") %>' runat="server" />
                                    <asp:Label ID="lbProgId" Text='<%#Eval("ProgId") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Program Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgName" Text='<%#Eval("ProgName") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Fullname" HeaderText="Language" ReadOnly="true" />
                            <asp:BoundField DataField="EngSynopsis" HeaderText="English Synopsis" ReadOnly="true" />
                            <asp:TemplateField HeaderText="OrigSynopsis" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbOrigProgName" Text='<%#Eval("OrigProgName") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Orig Synopsis" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbOrigSynopsis" Text='<%# Eval("OrigSynopsis") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Episode">
                                <ItemTemplate>
                                    <asp:Label ID="lbEpisodeNo" Text='<%# Eval("EpisodeNo") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Program Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtProgName" Text='<%# Eval("Program") %>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Synopsis">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSynopsis" Text='<%# Eval("Synopsis") %>' runat="server" MaxLength="200"
                                        Height="80px" Width="250px" Wrap="true" TextMode="MultiLine" ValidationGroup="PDSynopsis"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Default Synopsis">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDefaultSynopsis" runat="server" Checked="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkUpdateSynopsis" runat="server" Checked="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Copy Generic">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCopyEnglish" runat="server" Checked="false" />
                                    <asp:Label ID="lbLanguageId" Text='<%#Eval("LanguageID") %>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="Btn_AddNew" runat="server" CausesValidation="False" CommandName="Insert"
                                        Text="Insert" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'
                                        ValidationGroup="PDSynopsis" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Char Limit" SortExpression="CharLimit" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lbCharLimit" Text='<%# Eval("CharLimit") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
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
                    </asp:GridView>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                    <asp:Button ID="btnUpdateSynopsis" runat="server" Text="Update Synopsis" CssClass="btn btn-info" />
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
