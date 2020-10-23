<%@ Page Title="FTP Report" Language="vb" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="false" CodeBehind="FTPReport.aspx.vb" Inherits="EPG.FTPReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:ScriptManager ID="ScriptManager1" runat="server" />
   <asp:Table ID="Table1" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
      <asp:TableRow>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
            <h2>
               FTP Logs
            </h2>
                <b>Select Date</b>
            <asp:TextBox ID="txtFTPDate" runat="server"></asp:TextBox>
            <asp:TextBoxWatermarkExtender TargetControlID="txtFTPDate" ID="ExttxtFTPDate" WatermarkText="Select FTP Date" runat="server"></asp:TextBoxWatermarkExtender>
            <asp:CalendarExtender ID="CE_FTPDate" DefaultView="Days" Format="MM/dd/yyyy"  Enabled="True" PopupButtonID="imgtxtFTPDate" PopupPosition="BottomLeft" runat="server" TargetControlID="txtFTPDate" />
            <asp:Image ID="imgtxtFTPDate" runat="server" ImageUrl="~/Images/calendar.png" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnView" runat="server" Text="VIEW" Font-Bold="true"/>
            <br /><br />
            <asp:Table ID="tblProgramGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
               <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                     <asp:GridView ID="grdFTPReport" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                        CellPadding="4" ForeColor="#333333" GridLines="Vertical" Width="100%" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                           <asp:BoundField DataField="Channelid" HeaderText="Channel" SortExpression="Channelid" />
                           <asp:BoundField DataField="Filename" HeaderText="File" SortExpression="Filename"/>
                           <asp:BoundField DataField="ftpdate1" HeaderText="FTP Time" SortExpression="ftpdate"/>
                           <asp:BoundField DataField="LoggedInUser" HeaderText="User" SortExpression="LoggedInUser" />
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
                  </asp:TableCell>
               </asp:TableRow>
            </asp:Table>
         </asp:TableCell>
      </asp:TableRow>
   </asp:Table>
</asp:Content>
