<%@ Page Title="EPG Operators" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="epgOperators.aspx.vb" Inherits="EPG.epgOperators" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
      <table width="80%" cellpadding="2px" 
         style="border-color:Gray; border-width:thin; border-bottom-style:solid" 
         border="2px">
         <tr>
             <td colspan="4" align="center">
                 <h1>Operator Master</h1>
             </td>
         </tr>
         <tr>
             <td>
                 Name</td>
             <td>
                 <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 City</td>
             <td>
                 <asp:TextBox ID="txtCity" runat="server" Width="250px"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 System Name</td>
             <td>
                 <asp:TextBox ID="txtSystemName" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 EPG Type</td>
             <td>
                 <asp:TextBox ID="txtEPGType" runat="server" Width="250px"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 FolderName</td>
             <td>
                 <asp:TextBox ID="txtFolder" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 File Name Format</td>
             <td>
                 <asp:TextBox ID="txtFileFormat" runat="server" Width="250px"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 Recipient List</td>
             <td>
                 <asp:TextBox ID="txtRecepientList" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
             </td>
             <td>
                 Multiple Days</td>
             <td>
                 <asp:CheckBox ID="chkMultiple" runat="server" />
             </td>
         </tr>
         <tr>
             <td>
                 CC List</td>
             <td>
                 <asp:TextBox ID="txtCCList" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 Priority</td>
             <td>
                 <asp:TextBox ID="txtPriority" runat="server" Width="250px"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 BCC List</td>
             <td>
                 <asp:TextBox ID="txtBCCList" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 Operator Type</td>
             <td>
                 <asp:TextBox ID="txtOperatorType" runat="server" Width="250px"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 Time to Send</td>
             <td>
                 <asp:TextBox ID="txtTimetoSend" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 Time to Send2</td>
             <td>
                 <asp:TextBox ID="txtTimetoSend2" runat="server" Width="250px"></asp:TextBox>
             </td>
             
         </tr>
         <tr>
             <td>
                 Time to Send3</td>
             <td>
                 <asp:TextBox ID="txtTimetoSend3" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 Time to Send4</td>
             <td>
                 <asp:TextBox ID="txtTimetoSend4" runat="server" Width="250px"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 Hourly Updates</td>
             <td>
                 
                 <asp:CheckBox ID="chkHourlyUpdates" runat="server" />
                 
             </td>
             <td>
                 Active</td>
             <td>
                 <asp:CheckBox ID="chkActive" runat="server" />
             </td> 
        </tr>
        
         <tr>
             <td>
                 Mapping Table</td>
             <td>
                 <asp:TextBox ID="txtMappingTable" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 Select Query</td>
             <td>
                 <asp:TextBox ID="txtSelectQuery" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 Function Name</td>
             <td>
                 <asp:TextBox ID="txtFunctionName" runat="server" Width="250px"></asp:TextBox>
             </td>
             <td>
                 Function to Execute</td>
             <td>
                 <asp:TextBox ID="txtFunctionToExecute" runat="server" Width="250px"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td>
                 Days to send</td>
             <td colspan="3">
             <asp:CheckBoxList ID="chkWeekList" runat="server" 
                    RepeatDirection="Horizontal" style="font-size: small">
                    <asp:ListItem Value="1">Sun</asp:ListItem>
                    <asp:ListItem Value="2">Mon</asp:ListItem>
                    <asp:ListItem Value="3">Tue</asp:ListItem>
                    <asp:ListItem Value="4">Wed</asp:ListItem>
                    <asp:ListItem Value="5">Thurs</asp:ListItem>
                    <asp:ListItem Value="6">Fri</asp:ListItem>
                    <asp:ListItem Value="7">Sat</asp:ListItem>
                </asp:CheckBoxList>
             
             </td>
         </tr>
         <tr>
             <td colspan="4" align="center">
                 <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" Font-Bold="true" />
&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" Font-Bold="true" />
                 <asp:Label ID="lbOperatorID" runat="server" Visible="false"></asp:Label>
                 </td>
         </tr>
     </table>
     <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" AllowSorting="True">
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
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" />
                        <asp:TemplateField HeaderText="ID" SortExpression="OperatorID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lbOperatorID" runat="server" Text='<%# Bind("OperatorID") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="" Visible="true">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyView" runat="server" Text='View' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField HeaderText="ID" DataField="OperatorID" ControlStyle-Width="10px"  SortExpression="OperatorID"/>
                        <asp:BoundField HeaderText="Name" DataField="Name" ItemStyle-Width="40px"  SortExpression="Name"/>
                        <asp:BoundField HeaderText="Type" DataField="EpgType" ControlStyle-Width="40px" SortExpression="EpgType" />
                        <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="true" >
                            <ItemTemplate>
                                <asp:CheckBox ID="lbActive" runat="server" Checked='<%# Bind("Active") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Updates" SortExpression="HourlyUpdate" Visible="true">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkUpdates" runat="server" Checked='<%# Bind("HourlyUpdate") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Priority" SortExpression="Priority" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbPriority" runat="server" Text='<%# Bind("Priority") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField HeaderText="Recipients" DataField="RecipientList" ItemStyle-Width="60px"  SortExpression="RecipientList"/>
                        <asp:BoundField HeaderText="Function" DataField="FunctionName" ItemStyle-Width="60px"  SortExpression="FunctionName" />
                        <asp:BoundField HeaderText="Table" DataField="MappingTable" ItemStyle-Width="40px"  SortExpression="MappingTable" />
                        <asp:BoundField HeaderText="Days To Send" DataField="DaysToSend" ItemStyle-Width="40px"  SortExpression="DaysToSend"  />
                        <asp:BoundField HeaderText="Send Time" DataField="TimeSend" ItemStyle-Width="40px"  SortExpression="TimeToSend"  />
                        <asp:BoundField HeaderText="Send Time 2" DataField="TimeSend2" ItemStyle-Width="40px"  SortExpression="TimeToSend2"  />
                        <asp:BoundField HeaderText="Send Time 3" DataField="TimeSend3" ItemStyle-Width="40px"  SortExpression="TimetoSend3"  />
                        <asp:BoundField HeaderText="Send Time 4" DataField="TimeSend4" ItemStyle-Width="40px"  SortExpression="TimeToSend4"  />
                       
                    </Columns>
                </asp:GridView>
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                    SelectCommand="SELECT *,cast(TimeToSend as time(0)) timeSend,cast(TimeToSend2 as time(0)) timeSend2,cast(TimeToSend3 as time(0)) timeSend3,cast(TimeToSend4 as time(0)) timeSend4 FROM mst_operators order by active desc,priority,timetosend,timetosend2,name"></asp:SqlDataSource>
   <%--name,epgtype,active,recipientlist,cclist,bcclist,functionname,mappingtable--%>
    
</asp:Content>