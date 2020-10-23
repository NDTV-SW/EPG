<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="OperatorChannels.aspx.vb" Inherits="EPG.OperatorChannels" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            font-family: "Times New Roman", Times, serif;
            font-size: medium;
        }
    </style>
    </asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HyperLink ID="hyHome" runat="server" NavigateUrl="epgoperators.aspx" Text="Back" /><br />
    <span class="style1"><strong>Channel Details for </strong></span>
    <asp:Label ID="lbOperatorName" Text="Operator" runat="server" 
        style="font-weight: 700; font-size: large; font-family: 'Times New Roman', Times, serif; color: #000099" />
    <asp:Table ID="tbChannelUpdate" runat="server" >
        <asp:TableRow>
            <asp:TableHeaderCell>
                Channel ID
            </asp:TableHeaderCell>
            
            <asp:TableCell>
                <asp:TextBox ID="txtChannelId" runat="server" />
                <asp:Label ID="lbRowId" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            
            <asp:TableHeaderCell>
                Service ID
            </asp:TableHeaderCell><asp:TableCell>
                <asp:TextBox ID="txtServiceId" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableHeaderCell ColumnSpan="2" HorizontalAlign="Center">
                <asp:Button ID="btnAdd" runat="server" Text="Add" />
            </asp:TableHeaderCell></asp:TableRow></asp:Table><asp:Button 
        ID="Button1" runat="server" Text="Button" /><asp:Table ID="tbOnAirOffAir" runat="server" width="90%" border="1px" CellPadding="4" style="border:1,solid,grey">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>
             On-Air Channels
            </asp:TableHeaderCell><asp:TableHeaderCell>
             Off-Air and Service ID NULL Channels
            </asp:TableHeaderCell></asp:TableHeaderRow><asp:TableRow>
            <asp:TableCell VerticalAlign="Top">
                <asp:GridView ID="grdOnair" runat="server" CellPadding="4" ForeColor="#333333" 
                GridLines="Vertical" AutoGenerateColumns="False" >
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
                        <asp:BoundField HeaderText="S.No."  />
                        <asp:TemplateField HeaderText="Rowid" Visible="False" >
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" Text='<%# Eval("RowId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lbChannelid" runat="server" Text='<%# Eval("channelid") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtChannelID" runat="server" Text='<%# Eval("channelid") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lbServiceID" runat="server" Text='<%# Eval("ServiceId") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtServiceID" runat="server"  />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true"  />
                    </Columns>
                </asp:GridView>
            </asp:TableCell><asp:TableCell VerticalAlign="Top">
               <asp:GridView ID="grdOffAir" runat="server" CellPadding="4" ForeColor="#333333" 
                GridLines="Vertical" AutoGenerateColumns="false">
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
                        <asp:BoundField HeaderText="S.No." />
                        <asp:TemplateField HeaderText="Rowid" Visible="False" >
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" Text='<%# Eval("RowId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Channel ID">
                            <ItemTemplate>
                                <asp:Label ID="lbChannelid" runat="server" Text='<%# Eval("channelid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service ID">
                            <ItemTemplate>
                                <asp:Label ID="lbServiceID" runat="server" Text='<%# Eval("ServiceId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" />
                    </Columns>
                    
                </asp:GridView>
            </asp:TableCell></asp:TableRow></asp:Table><asp:GridView ID=abc runat=server /></asp:Content>