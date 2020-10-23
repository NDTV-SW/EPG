<%@ Page Title="DashBoard On-Air Channels" Language="vb" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="false" CodeBehind="Dashboard_Channel_Onair.aspx.vb" Inherits="EPG.Dashboard_Channel_Onair" MaintainScrollPositionOnPostback ="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openWin(channelid, xmlmaildate) {
            window.open("Editxmlmaildate.aspx?channelid=" + channelid + "&xmlmaildate=" + xmlmaildate, "Edit XML Mail Date", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:ScriptManager ID="ScriptManager1" runat="server" />
   <asp:Table ID="Table1" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
      <asp:TableRow>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
            <h2>
               DashBoard On-Air Channels
            </h2>
            <asp:Table ID="tblProgramGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
               <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                     <asp:GridView ID="grdDashBoard" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                        CellPadding="4" DataKeyNames="ChannelId" DataSourceID="sqlDSProgramDetails" 
                        ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="100" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                           <asp:BoundField DataField="SendEPG" HeaderText="Send EPG" SortExpression="ChannelID"/>
                           <asp:BoundField DataField="ChannelID" HeaderText="Channel" SortExpression="ChannelID"/>
                           <asp:TemplateField>
                              <ItemTemplate>
                                 <asp:Label ID="lbChannelID" runat="server" Text='<%#bind("ChannelID") %>' />
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="ProgNeeded" HeaderText="ShowName Needed" SortExpression="ChannelID" />
                           <asp:BoundField DataField="ProgHave" HeaderText="ShowName Existing" SortExpression="ChannelID" />
                           <asp:BoundField DataField="SynNeeded" HeaderText="Synopsis Needed" SortExpression="ChannelID" />
                           <asp:BoundField DataField="SynHave" HeaderText="Synopsis Existing" SortExpression="ChannelID" />
                           <asp:BoundField DataField="MinEPGDate" HeaderText="EPG Available From" SortExpression="ChannelID" />
                           <asp:BoundField DataField="MaxEPGDate" HeaderText="EPG Available To" SortExpression="ChannelID" />
                           <asp:BoundField DataField="XMLGenTime" HeaderText="Last XML Generated at" SortExpression="ChannelID" />
                           <asp:BoundField DataField="FTPSentTime" HeaderText="Last FTP At" SortExpression="ChannelID" />
                           
                           <asp:TemplateField>
                              <ItemTemplate>
                                 <asp:HyperLink ID="hyXML" runat="server" Target="_blank" ToolTip="To save XML right click and select (Save Link as)" NavigateUrl='<%#bind("xmlfilename") %>' Text="View XML">View XML</asp:HyperLink>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <%--<asp:TemplateField>
                              <ItemTemplate>
                                 <asp:HyperLink ID="hyXMLMailTill" runat="server" Target="_blank" Text="View XML">View XML</asp:HyperLink>
                              </ItemTemplate>
                           </asp:TemplateField>--%>
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
                     <asp:SqlDataSource ID="sqlDSProgramDetails" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                        SelectCommand="sp_epg_dashboard_Onair" SelectCommandType="StoredProcedure" >
                     </asp:SqlDataSource>
                  </asp:TableCell>
               </asp:TableRow>
            </asp:Table>
         </asp:TableCell>
      </asp:TableRow>
   </asp:Table>
</asp:Content>
