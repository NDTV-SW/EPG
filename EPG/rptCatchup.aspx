<%@ Page Title="CatchUp Report" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="rptCatchup.aspx.vb" Inherits="EPG.rptCatchup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    <h2>
        Catchup Report
   </h2>
   <asp:Table ID="tblCatchupReport" runat="server" GridLines="both" 
      BorderWidth="2" CellPadding="5" CellSpacing="0" Width="95%">
      <asp:TableRow>
         <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top" Width="10%">
            Select Channel
         </asp:TableHeaderCell>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="20%">
            <asp:ComboBox ID="ddlChannelName" runat="server"  Width="150px"  AutoCompleteMode="SuggestAppend"
               DropDownStyle="DropDownList" ItemInsertLocation="Append"  AutoPostBack="False"
               DataSourceID="SqlDsChannelMaster" DataTextField="ChannelID" 
               DataValueField="ChannelID">
            </asp:ComboBox>
            <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="SELECT ChannelID FROM mst_Channel where onair='1' and catchupflag=1 ORDER BY ChannelID">
            </asp:SqlDataSource>
            
         </asp:TableCell><asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            Start Date
         </asp:TableHeaderCell><asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:TextBox ID="txtStartDate" runat="server" Width="150px" Enabled="true" ValidationGroup="RFGCatchupReport"></asp:TextBox>
            <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgStartDateCalendar" />
            <asp:TextBoxWatermarkExtender TargetControlID="txtStartDate" ID="ExttxtStartDate" WatermarkText="Select Start Date" runat="server"></asp:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate" runat="server" ForeColor="Red" Text="* (Start date can not be left blank.)" ValidationGroup="RFGCatchupReport"></asp:RequiredFieldValidator>
         </asp:TableCell><asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            End Date
         </asp:TableHeaderCell><asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:TextBox ID="txtEndDate" runat="server" Width="150px" Enabled="true" ValidationGroup="RFGCatchupReport"></asp:TextBox>
            <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgEndDateCalendar" />
            <asp:TextBoxWatermarkExtender TargetControlID="txtEndDate" ID="ExttxtEndDate" WatermarkText="Select End date" runat="server"></asp:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server" ForeColor="Red" Text="* (End date can not be left blank.)" ValidationGroup="RFGCatchupReport"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date" ErrorMessage="* (End date cannot be less than Start date)"
               ValidationGroup="RFGCatchupReport"></asp:CompareValidator>
         </asp:TableCell><asp:TableCell HorizontalAlign="Center" VerticalAlign="Top" ColumnSpan="4">
            <asp:Button ID="btnCatchupReport" runat="server" Text="View" Width="80px" ValidationGroup="RFGCatchupReport" />&nbsp;&nbsp;&nbsp;
         </asp:TableCell></asp:TableRow></asp:Table><asp:Table ID="tblgrdCatchupReport" runat="server" GridLines="both" 
                BorderWidth="2" CellPadding="5" CellSpacing="0" Width="80%" HorizontalAlign="Center">
      <asp:TableRow>
         <asp:TableHeaderCell VerticalAlign="Top" HorizontalAlign="Center">
            <asp:GridView ID="grdCatchupReport"  runat="server" AutoGenerateColumns="True" ShowFooter="True"  DataSourceID="sqlDSCatchupReport"
               CellPadding="4"  EmptyDataText="No record found !"
               ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
               SortedDescendingHeaderStyle-CssClass="sortdesc-header" ShowHeader="True" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
               PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana" >
               <AlternatingRowStyle BackColor="White" />
               <EditRowStyle BackColor="#2461BF" />
               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
               <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" />
               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
               <SortedAscendingHeaderStyle BackColor="#6D95E1" />
               <SortedDescendingCellStyle BackColor="#E9EBEF" />
               <SortedDescendingHeaderStyle BackColor="#4870BE" />
               <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lbDuration" runat="server" Text='<%#bind("Duration") %>' Visible="false"/>
                    </ItemTemplate>
                </asp:TemplateField>
               </Columns>
            </asp:GridView>
         </asp:TableHeaderCell></asp:TableRow></asp:Table><asp:SqlDataSource ID="sqlDSCatchupReport" runat="server" 
      ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" SelectCommand="rpt_catchup" SelectCommandType="StoredProcedure">
      <SelectParameters>
         <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelID" PropertyName="SelectedValue" Type="String" />
         <asp:ControlParameter ControlID="txtStartDate" Name="Startdate" PropertyName="Text" Type="String" />
         <asp:ControlParameter ControlID="txtEndDate" Name="EndDate" PropertyName="Text" Type="String" />
      </SelectParameters>
   </asp:SqlDataSource>
   </asp:Content>

