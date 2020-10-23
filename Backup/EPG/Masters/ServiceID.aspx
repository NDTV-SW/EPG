<%@ Page Title="Update Service ID" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
   CodeBehind="ServiceID.aspx.vb" Inherits="EPG.ServiceID" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
   <asp:UpdatePanel ID="updPanelChannelMaster" runat="server">
      <ContentTemplate>
                  <h2>
                     Update Service ID
                  </h2>
                  <asp:Table ID="tblChannelMaster" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="60%">
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Select Channel
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:ComboBox ID="ddlChannel" runat="server"  AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append"
                              DataSourceID="SqlDsCompanyMaster" DataTextField="ChannelID" 
                              DataValueField="ChannelID" AutoPostBack="True">
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlDsCompanyMaster" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT ChannelID FROM mst_Channel where onair='1' order by ChannelID">
                           </asp:SqlDataSource>
                           <br /><br />
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Service ID
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:TextBox ID="txtServiceID" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                           <asp:TextBoxWatermarkExtender TargetControlID="txtServiceID" ID="ExttxtServiceID" WatermarkText="Enter Service ID" runat="server"></asp:TextBoxWatermarkExtender>
                           <br />
                           <asp:RequiredFieldValidator ID="RFVtxtServiceID" ControlToValidate="txtServiceID" runat="server" ForeColor="Red" Text="* (Service ID can not be left blank.)" ValidationGroup="RFGServiceID"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                     </asp:TableRow>
                     
                     <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                           <asp:TextBox ID="txtRowId" runat="server" Visible="False"></asp:TextBox>
                           <asp:TextBox ID="txtChannelID" runat="server" Visible="False"></asp:TextBox>
                           <asp:Button ID="btnUpdateServiceId" runat="server" Text="Update" ValidationGroup="RFGServiceID" UseSubmitBehavior="false" Visible="false"/>
                           &nbsp;&nbsp;
                           <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false"/>
                        </asp:TableCell>
                     </asp:TableRow>
                  </asp:Table>
                  <br />
                  <asp:Table ID="tblServiceID" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="60%">
                     <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:GridView ID="grdServiceID" runat="server" AutoGenerateColumns="False" 
                              CellPadding="4" DataKeyNames="ChannelId" DataSourceID="sqlDSServiceID" 
                              ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                              SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                              <AlternatingRowStyle BackColor="White" />
                              <Columns>
                                 <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="False">
                                    <ItemTemplate>
                                       <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ServiceId" SortExpression="ServiceId" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbServiceId" runat="server" Text='<%# Bind("ServiceId") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png"/>
                                 <asp:CommandField ShowDeleteButton="False" ButtonType="Image"  DeleteImageUrl="~/images/delete.png"/>
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
                           <%--SelectCommand="SELECT a.ChannelId ChannelId,c.Fullname, a.catchupflag,a.active, a.RowId Rowid, a.CompanyId CompanyId, b.CompanyName,a.sendEPG sendEPG FROM mst_Channel a join mst_Company b on a.CompanyId = b.CompanyId join mst_language c on a.ChannelLanguage=c.languageid where a.COMPANYID=@COMPANYID and a.active=1"--%>
                           <asp:SqlDataSource ID="sqlDSServiceID" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT rowid,ChannelID, ServiceID FROM mst_Channel where onair='1' order by ChannelID">
                           </asp:SqlDataSource>
                        </asp:TableCell>
                     </asp:TableRow>
                  </asp:Table>
      </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

