<%@ Page Title="Client Wise Errors" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="ErrorReportChannelWise.aspx.vb" Inherits="EPG.ErrorReportChannelWise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <table width="95%" cellpadding="8px" style="border-color:Gray; border-width:thin; border-bottom-style:solid" border="2px">
         <tr>
             <td colspan="6" align="center">
                 <h1>Client Wise Error Report</h1>
             </td>
         </tr>
         <tr>
             <td>
                 Channel</td>
             <td>
                <asp:ComboBox CssClass="ddlPostBack" ID="ddlChannelName" runat="server"   AutoCompleteMode="SuggestAppend"
                    DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="true" DataSourceID="SqlDsChannelMaster"
                    DataTextField="ChannelId" DataValueField="ChannelId" Width="200px" />
                <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                    SelectCommand="SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId"/>
            </td>
             <td>
                 Programme</td>
             <td>
                  
                  <asp:ComboBox CssClass="ddlPostBack" ID="ddlProgrammes" runat="server"   AutoCompleteMode="SuggestAppend"
                    DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="false" DataSourceID="SqlDsProgrammes"
                    DataTextField="ProgName" DataValueField="progid" Width="200px"  />
                  <asp:SqlDataSource ID="SqlDsProgrammes" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                    SelectCommand="SELECT progid,progname from mst_program where channelid=@ChannelId order by progname asc">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                    </asp:SqlDataSource>
                    
             </td>
         
             <td>
                 Error 
                 type</td>
             <td>
                <asp:ComboBox CssClass="ddlPostBack" ID="ddlErrorType" runat="server"   AutoCompleteMode="SuggestAppend"
                    DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="false" DataSourceID="SqlDsErrorType"
                    DataTextField="ErrorType" DataValueField="ErrorTypeId" Width="200px" />
                <asp:SqlDataSource ID="SqlDsErrorType" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                    SelectCommand="SELECT * from error_type order by errortype"/>
             </td>
         </tr>
         <tr>
             <td>
                 Cause of Error</td>
             <td>
                  <asp:ComboBox CssClass="ddlPostBack" ID="ddlErrorCause" runat="server"   AutoCompleteMode="SuggestAppend"
                    DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="false" DataSourceID="SqlDsErrorCause"
                    DataTextField="Cause" DataValueField="CauseId" Width="200px" />
                <asp:SqlDataSource ID="SqlDsErrorCause" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                    SelectCommand="SELECT * from error_cause"/>
             </td>
             <td>
                Error Date
             </td>
             <td>
                <asp:TextBox ID="txtDate" runat="server" />
                <asp:CalendarExtender ID="CE_txtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgStartDateCalendar" />
                <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                <asp:RequiredFieldValidator ID="RFVtxtDate" runat="server" ControlToValidate="txtDate"
                    Font-Bold="true" ForeColor="Red" Text="*"/>
                
             </td>
             <td>
                Error Time
             </td>
             <td>
                <asp:TextBox ID="txtTime" runat="server" />24 hr format
                <asp:maskededitextender ID="maskTime" runat="server" AcceptAMPM="false"
                    AutoComplete="true" InputDirection="LeftToRight" Mask="99:99:99" MaskType="Time"
                    MessageValidatorTip="true" TargetControlID="txtTime" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTime"
                    Font-Bold="true" ForeColor="Red" Text="*" />
                <asp:maskededitvalidator ID="MaskedEditValidator2" runat="server" ControlExtender="maskTime"
                    ControlToValidate="txtTime" Display="Dynamic" EmptyValueMessage="Enter Time"
                    InvalidValueMessage="Invalid" IsValidEmpty="true" MaximumValue="23:59:59" />
             </td>
         </tr>
         <tr>
         <td>
                Error Source
             </td>
             <td>
               <asp:DropDownList ID="ddlOperator" runat="server">
                    <asp:ListItem Text="Airtel DTH" Value="Airtel DTH" />
                    <asp:ListItem Text="Airtel IPTV" Value="Airtel IPTV" />
                    <asp:ListItem Text="BIG TV" Value="BIG TV" />
                    <asp:ListItem Text="Internal Monitoring" Value="Internal Monitoring" />
                    <asp:ListItem Text="Others" Value="Others" />
               </asp:DropDownList>
             </td>
             <td>
                Programme Fixed when On-Air
             </td>
             <td>
                <asp:CheckBox ID="chkFixedWhenOnAir" runat="server" Checked="false" />
             </td>
             <td>
                Remarks
             </td>
             <td>
                <asp:TextBox ID="txtRemarks" runat="server" Width="250px" TextMode="MultiLine" Height="50px" MaxLength="190" />
             </td>
         </tr>
          <tr>
             <td colspan="6" align="center">
                 <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" Font-Bold="true" />
                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" Font-Bold="true" />
                 </td>
         </tr>
        </table>
        <table width="95%" cellpadding="8px" style="border-color:Gray; border-width:thin; border-bottom-style:solid" border="2px">
         <tr>
            <td>
                 Channel</td>
             <td>
                <asp:ComboBox CssClass="ddlPostBack" ID="ddlChannel" runat="server"   AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                    ItemInsertLocation="Append" DataSourceID="sqlDSChannel" DataTextField="ChannelId" DataValueField="ChannelId" Width="200px" />
                <asp:SqlDataSource ID="sqlDSChannel" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                    SelectCommand="SELECT '--All Channels--' channelid union Select ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId"/>
            </td>
             <td>
                Start Date
             </td>
             <td>
                <asp:TextBox ID="txtStartDate" runat="server" />
                <asp:Image ID="img_txtStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                <asp:CalendarExtender ID="CE_txtStartDate" runat="server" PopupButtonID="img_txtStartDate" TargetControlID="txtStartDate" />
             </td>
             <td>
                End Date
             </td>
             <td>
                <asp:TextBox ID="txtEndDate" runat="server" />
                <asp:Image ID="img_EndDate" runat="server" ImageUrl="~/Images/calendar.png" />
                <asp:CalendarExtender ID="CE_txtEndDate" runat="server" PopupButtonID="img_EndDate" TargetControlID="txtEndDate" />
             </td>
             <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="VGSearch" />
             </td>
             
         </tr>
         
         <tr>
            <td colspan="7" align="center">
                <asp:GridView ID="grdErrorReportChannelWise" runat="server" CellPadding="4" ForeColor="#333333" EmptyDataText="No Record Found..." 
                    GridLines="Vertical" AutoGenerateColumns="True" AllowPaging="true" PageSize="100">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" BorderStyle="Inset" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    
                </asp:GridView>
            </td>
         </tr>
     </table>
                     <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
</asp:Content>