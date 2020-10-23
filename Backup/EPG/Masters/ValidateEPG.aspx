<%@ Page Language="vb" Title="Validate EPG"  AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ValidateEPG.aspx.vb" Inherits="EPG.ValidateEPG" %>
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
       });
   </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <h2>
      EPG Validation
   </h2>
   <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
   <asp:Table ID="tblSubGenreMaster" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="60%">
      <asp:TableRow>
         <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            Select Channel
         </asp:TableHeaderCell>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:ComboBox ID="ddlChannelName" runat="server"  Width="150px"  AutoCompleteMode="SuggestAppend"
               DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="true" 
               DataSourceID="SqlDsChannelMaster" DataTextField="ChannelID" 
               DataValueField="ChannelID">
            </asp:ComboBox>
            <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="SELECT ChannelID FROM mst_Channel where active='1' ORDER BY ChannelID">
            </asp:SqlDataSource>
            <br />
            <asp:label id="lbEPGExists" runat="server" Text="EPG Exists" />
         </asp:TableCell></asp:TableRow><asp:TableRow>
         <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            Start Date
         </asp:TableHeaderCell><asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:TextBox ID="txtStartDate" runat="server" Width="150px" Enabled="true" ValidationGroup="RFGValidateEPG"></asp:TextBox>
            <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgStartDateCalendar" />
            <asp:TextBoxWatermarkExtender TargetControlID="txtStartDate" ID="ExttxtStartDate" WatermarkText="Select Start Date" runat="server"></asp:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate" runat="server" ForeColor="Red" Text="* (Start date can not be left blank.)" ValidationGroup="RFGValidateEPG"></asp:RequiredFieldValidator>
         </asp:TableCell></asp:TableRow><asp:TableRow>
         <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            End Date
         </asp:TableHeaderCell><asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:TextBox ID="txtEndDate" runat="server" Width="150px" Enabled="true" ValidationGroup="RFGValidateEPG"></asp:TextBox>
            <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgEndDateCalendar" />
            <asp:TextBoxWatermarkExtender TargetControlID="txtEndDate" ID="ExttxtEndDate" WatermarkText="Select End date" runat="server"></asp:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server" ForeColor="Red" Text="* (End date can not be left blank.)" ValidationGroup="RFGValidateEPG"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date" ErrorMessage="* (End date cannot be less than Start date)"
               ValidationGroup="RFGValidateEPG"></asp:CompareValidator>
         </asp:TableCell></asp:TableRow><asp:TableRow>
         <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
            <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
            <asp:Button ID="btnAddSubGenre" runat="server" Text="View" ValidationGroup="RFGValidateEPG"/>
            &nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
         </asp:TableCell></asp:TableRow></asp:Table><br />
   <asp:Table ID="tblSubGenreGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
      <asp:TableRow>
         <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
            <asp:GridView ID="grdValidateEPG" runat="server" AutoGenerateColumns="False"
               CellPadding="4" DataKeyNames="RowId" DataSourceID="sqlDSValidateEPG"
               ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
               SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                  <asp:BoundField DataField="RowId" HeaderText="RowId" SortExpression="RowId" />
                  <asp:BoundField DataField="errcode" HeaderText="Error Code" SortExpression="errcode" />
                  <asp:BoundField DataField="errsub" HeaderText="Subject" SortExpression="errsub"/>
                  <asp:BoundField DataField="errmess" HeaderText="Message" SortExpression="errmess" />
                  <asp:BoundField DataField="errsource" HeaderText="Source" SortExpression="errsource"/>
                  <asp:BoundField DataField="errdate" HeaderText="Error Date" SortExpression="errdate"/>
                  <asp:BoundField DataField="mailto" HeaderText="Mail To" SortExpression="mailto"/>
                  <asp:BoundField DataField="maicc" HeaderText="Mail CC" SortExpression="maicc"/>
               </Columns>
               <EditRowStyle BackColor="#2461BF" />
               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#EFF3FB" />
               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#F5F7FB" />
               <SortedAscendingHeaderStyle BackColor="#6D95E1" />
               <SortedDescendingCellStyle BackColor="#E9EBEF" />
               <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:SqlDataSource ID="sqlDSValidateEPG" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="sp_epg_validate_epgDates" SelectCommandType="StoredProcedure"                                
               >
               <SelectParameters>
                  <asp:controlParameter ControlID="ddlChannelName" PropertyName="Text" Direction="Input" Name="ChannelId" Type="String" />
                  <asp:controlParameter ControlID="txtStartDate" PropertyName="Text" Direction="Input" Name="StartDate" Type="DateTime" />
                  <asp:controlParameter ControlID="txtEndDate" PropertyName="Text" Direction="Input" Name="EndDate" Type="DateTime" />
               </SelectParameters>
            </asp:SqlDataSource>
         </asp:TableCell></asp:TableRow></asp:Table></asp:Content>