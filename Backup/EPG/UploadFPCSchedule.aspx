<%@ Page Title="Upload Schedule #2" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="UploadFPCSchedule.aspx.vb" Inherits="EPG.UploadFPCSchedule" %>
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
      Upload Schedule
   </h2>
   <asp:Table ID="Table1" runat="server" Width="95%" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0">
      <asp:TableRow>
         <asp:TableCell ColumnSpan="6" HorizontalAlign="Center" >
            <asp:Image ID="imgSampleScedUpload" ImageUrl="~/Images/SampleScedUpload.JPG" runat="server" />
            <br />
            Sample Format of Excel to Upload
         </asp:TableCell>
      </asp:TableRow>
      <asp:TableRow>
         <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            Select Channel
         </asp:TableHeaderCell>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:ComboBox ID="ddlChannelName" runat="server"   AutoCompleteMode="SuggestAppend"
               DropDownStyle="DropDownList" ItemInsertLocation="Append" DataSourceID="SqlDsChannelMaster"
               DataTextField="ChannelId" DataValueField="ChannelId" AutoPostBack="true">
            </asp:ComboBox>
            <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId">
            </asp:SqlDataSource>
         </asp:TableCell>
         <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            Start Date
         </asp:TableHeaderCell><asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:TextBox ID="txtStartDate" runat="server" Width="150px" Enabled="true" ValidationGroup="VGUploadFPCSched"></asp:TextBox>
            <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgStartDateCalendar" />
            <asp:TextBoxWatermarkExtender TargetControlID="txtStartDate" ID="ExttxtStartDate" WatermarkText="Select Start Date" runat="server"></asp:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate" runat="server" ForeColor="Red" Text="* (Start date can not be left blank.)" ValidationGroup="VGUploadFPCSched"></asp:RequiredFieldValidator>
         </asp:TableCell>
         <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            End Date
         </asp:TableHeaderCell><asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:TextBox ID="txtEndDate" runat="server" Width="150px" Enabled="true" ValidationGroup="VGUploadFPCSched"></asp:TextBox>
            <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgEndDateCalendar" />
            <asp:TextBoxWatermarkExtender TargetControlID="txtEndDate" ID="ExttxtEndDate" WatermarkText="Select End date" runat="server"></asp:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server" ForeColor="Red" Text="* (End date can not be left blank.)" ValidationGroup="VGUploadFPCSched"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date" ErrorMessage="* (End date cannot be less than Start date)"
               ValidationGroup="VGUploadFPCSched"></asp:CompareValidator>
         </asp:TableCell>
         </asp:TableRow>
         <asp:TableRow>
         <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top" ColumnSpan="3">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1" runat="server" ErrorMessage="Only .xls file Supported !"
               ValidationExpression="^.*\.(xls|XLS)$" ForeColor="Red" ValidationGroup="VGUploadFPCSched" />
            <br />
            <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server" ErrorMessage="Please select file first !"
               ForeColor="Red" ValidationGroup="VGUploadFPCSched" />
         </asp:TableCell><asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" ColumnSpan="3">
            <asp:Button ID="btnUpload" runat="server" Text="UPLOAD" UseSubmitBehavior="false" ValidationGroup="VGUploadFPCSched" />
            <asp:Label ID="lbUploadError" runat="server" ForeColor="Red" Text="Please select file first!" Visible="false" />
         </asp:TableCell></asp:TableRow><asp:TableRow>
         </asp:TableRow></asp:Table></asp:Content>