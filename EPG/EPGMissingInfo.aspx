<%@ Page Title="EPG Missing Info" Language="vb" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="false" CodeBehind="EPGMissingInfo.aspx.vb" Inherits="EPG.EPGMissingInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
   <asp:Table ID="Table1" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
      <asp:TableRow>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
            <h2>
               EPG Missing Information
            </h2>
            <asp:Table ID="tblProgramGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
               <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                     <asp:GridView ID="grdErrorReport" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                        CellPadding="4" DataSourceID="sqlDSEPGMissingInfo" 
                        ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="50">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                           <asp:BoundField DataField="ProgID" HeaderText="ProgID" SortExpression="PROGID" />
                           <asp:BoundField DataField="ChannelID" HeaderText="Channel" SortExpression="ChannelID"/>
                           <asp:BoundField DataField="Progname" HeaderText="Programme" SortExpression="Progname"/>
                           <asp:BoundField DataField="Synopsis" HeaderText="Synopsis" SortExpression="Synopsis" />
                           
                           <asp:TemplateField HeaderText="Programme Date" SortExpression="ProgDate" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgdate" runat="server" Text='<%# Bind("ProgDate") %>' Visible="true" />
                                </ItemTemplate>
                           </asp:TemplateField>
                           
                           <asp:BoundField DataField="Progtime" HeaderText="Programme Time" SortExpression="Progtime" />
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
                     <asp:SqlDataSource ID="sqlDSEPGMissingInfo" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                        SelectCommand="sp_missing_epg_info" SelectCommandType="StoredProcedure">
                     </asp:SqlDataSource>
                  </asp:TableCell>
               </asp:TableRow>
            </asp:Table>
         </asp:TableCell>
      </asp:TableRow>
   </asp:Table>
</asp:Content>
