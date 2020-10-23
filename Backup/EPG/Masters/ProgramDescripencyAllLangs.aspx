<%@ Page Title="Program Discrepancies" Language="vb" MasterPageFile="~/Site.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="ProgramDescripencyAllLangs.aspx.vb"
    Inherits="EPG.ProgramDescripencyAllLangs" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlChannelName').bind("keyup", function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    javascript: __doPostBack('ddlChannelName', '')
                }

            });
            $('#ddlLanguage').bind("keyup", function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    javascript: __doPostBack('ddlLanguage', '')
                }

            });
        });

        $(window).bind('unload', function () {
            if (event.clientY < 0) {
                //alert('Gracias por emplear este servicio');
                //endSession(); // here you can do what you want ...
            }
        });

        window.onbeforeunload = function () {
            $(window).unbind('unload');
            // si se devuelve un string, automáticamente se pregunta al usuario por si desea irse o no ....
            //return ''; //'beforeunload event'; 
            if (event.clientY < 0) {
                //alert('Gracias por emplear este servicio');
                //endSession();
                var varUser;
                varUser = document.getElementById("txtUser").value;
                window.open("LogOff.aspx?user=" + varUser);
            }
        } 
    </script>
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table width="95%">
        <tr>
            <td align="left" valign="top" width="50%">
                <h2>
                    Program Discrepancies
                </h2>
                <table width="95%">
                    <tr>
                        <th align="right" width="20%">
                            Channel
                        </th>
                        <td align="left" width="20%">
                            <asp:ComboBox ID="ddlChannelName" runat="server" AutoCompleteMode="SuggestAppend"
                                DropDownStyle="DropDownList" ItemInsertLocation="Append" DataTextField="ChannelId"
                                DataValueField="ChannelId" AutoPostBack="True">
                            </asp:ComboBox>
                        </td>
                        <td width="20%" height="90px" align="center">
                            <asp:Image ID="imgChannel" runat="server" />
                        </td>
                        <th align="right" valign="top" width="20%">
                            Language
                        </th>
                        <td align="left" width="20%">
                            <asp:ComboBox ID="ddlLanguage" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                                ItemInsertLocation="Append" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                            </asp:ComboBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <h3>
                                New synopsis received in FPC excel(You can Copy, Paste in Program Central if needed.).
                            </h3>
                            <asp:GridView ID="grdSynopsis" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                DataKeyNames="RowId" EmptyDataRowStyle-BackColor="LightGreen" EmptyDataText="All English synopses are matching with the EPG excel."
                                ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Row Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Channel">
                                        <ItemTemplate>
                                            <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Programme">
                                        <ItemTemplate>
                                            <asp:Label ID="lbProgName" runat="server" Text='<%# Bind("ProgName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Old Synopsis">
                                        <ItemTemplate>
                                            <asp:Label ID="lbOldSynop" runat="server" Text='<%# Bind("OldSynop") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="New Synopsis">
                                        <ItemTemplate>
                                            <asp:Label ID="lbNewSynop" runat="server" Text='<%# Bind("NewSynop") %>' />
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
                                                Text="Fixed all Languages." CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <asp:Button ID="btnFixSynopsis" runat="server" Text="Update Synopsis in Master" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <h3>
                                Regional Synopsis/Names Missing if any:
                            </h3>
                            <asp:GridView ID="grdProgrammaster" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="LightGreen"
                                EmptyDataText="All regional Name or synopses are missing." CellPadding="4" DataKeyNames="ProgId"
                                ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="RowId" HeaderText="RowId" ReadOnly="true" />
                                    <asp:BoundField DataField="ProgId" HeaderText="ProgId" InsertVisible="False" ReadOnly="True" />
                                    <asp:BoundField DataField="LanguageId" HeaderText="LanguageId" ReadOnly="true" />
                                    <asp:BoundField DataField="ProgName" HeaderText="Program Name" ReadOnly="true" />
                                    <asp:BoundField DataField="Fullname" HeaderText="Language" ReadOnly="true" />
                                    <asp:BoundField DataField="EngSynopsis" HeaderText="English Synopsis" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Orig Synopsis" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbOrigProgName" Text='<%# Eval("OrigProgName") %>' runat="server" />
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
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <asp:Button ID="btnUpdateSynopsis" runat="server" Text="Update Synopsis" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <table id="tblXML" runat="server"  width="95%" 
       >
        <tr>
            <td align="left" valign="top" colspan="5">
                <h2>
                    Generate XML
                </h2>
            </td>
        </tr>
        <tr>
            <th align="right" width="15%">
                Start Date
            </th>
            <td align="left" width="20%">
                <asp:TextBox ID="txtStartDate" runat="server" MaxLength="15"></asp:TextBox>
                <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                <br />
                <asp:Label ID="lbEPGExists" runat="server" Text="EPG Exists" />
                <asp:RequiredFieldValidator ID="RFVStartDate" ControlToValidate="txtStartDate" ForeColor="Red"
                    runat="server" ValidationGroup="Descripency" ErrorMessage="* (Start date cannot be left blank)"></asp:RequiredFieldValidator>
                <asp:CalendarExtender ID="txtStartDate_CalendarExtender" TargetControlID="txtStartDate"
                    PopupButtonID="imgStartDate" runat="server" Enabled="True" Format="yyyy-MM-dd">
                </asp:CalendarExtender>
            </td>
            <td align="left" valign="middle" width="20%" rowspan="2">
                <asp:Button ID="btnGenerateXML" runat="server" Text="Generate XML" UseSubmitBehavior="false"
                    ValidationGroup="Descripency" />
                <br />
                <br />
                <asp:Button ID="btnGenerateXMLAgain" runat="server" Font-Bold="true" Text="Generate XML Again"
                    UseSubmitBehavior="false" ValidationGroup="Descripency1" BackColor="pink" />
                <br />
                (Select no of days to generate XML Again from Today Onwards)
                <br />
                <asp:Label ID="lbGenerateXML" Text="XML Generated Successfully" ForeColor="Red" runat="server"
                    Visible="false" />
                <br />
                <asp:HyperLink ID="hyViewXml" runat="server" Visible="false" Target="_blank">View XML</asp:HyperLink>
            </td>
            <td rowspan="2">
                <h3>
                    <b>
                        <asp:Label ID="lbChannelSelected" runat="server" Visible="true" BackColor="LightGreen"
                            Text="Channel has fresh EPG data for:" /></b></h3>
                <asp:GridView ID="grdXMLGenerated" runat="server" ShowHeader="false"
                    align="center" Width="100%" CellPadding="2" EmptyDataRowStyle-BackColor="LightGreen"
                    EmptyDataText="No Records to display" ForeColor="#333333" GridLines="Vertical"
                    AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header"
                    AllowPaging="true" PageSize="7" AutoGenerateColumns="false">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:BoundField DataField="ProgDate1"/>
                    </Columns>
                </asp:GridView>
            </td>
            <td rowspan="2">
                <h3>
                    <b>
                        <asp:Label ID="Label1" runat="server" Visible="True" BackColor="LightGreen" Text="You can generate XML again for:" /></b></h3>
                <asp:GridView ID="grdGenerateAgain" runat="server" ShowHeader="false"
                    align="center" Width="100%" CellPadding="2" EmptyDataRowStyle-BackColor="LightGreen"
                    EmptyDataText="No Records to display" ForeColor="#333333" GridLines="Vertical"
                    AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header"
                    AllowPaging="true" PageSize="7" AutoGenerateColumns="false">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:BoundField DataField="ProgDate1"/>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <th align="right" valign="middle" width="20%">
                No. Of Days
            </th>
            <td align="left" width="25%">
                <asp:ComboBox ID="ddlDays" runat="server">
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
                </asp:ComboBox>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
