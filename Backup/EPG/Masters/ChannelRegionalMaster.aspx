<%@ Page Title="Channel Regional Names" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
   CodeBehind="ChannelRegionalMaster.aspx.vb" Inherits="EPG.ChannelRegionalMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <script type="text/javascript">
       function AlertCatchUp(obj) {
           if (!obj.checked) {
           }
           else {
               if (obj.value != '') {
                   var doit;
                   doit = confirm('Do you want to set Catchup as True?');
                   if (doit == true)
                   { obj.checked = true; }
                   else
                   { obj.checked = false; }
               }
           }  //function Link
       }

       function AlertActive(obj) {
           if (obj.checked) {

           }
           else {
               if (obj.value != '') {
                   var doit;
                   doit = confirm('Do you want to set this Channel as In-Active?');
                   if (doit == true)
                   { obj.checked = false; }
                   else
                   { obj.checked = true; }
               }

           }  //function Link
       }

       function AlertSynopsis(obj) {
           if (obj.checked) {

           }
           else {
               if (obj.value != '') {
                   var doit;
                   doit = confirm('Do you want to set Synopsis as In-Active?');
                   if (doit == true)
                   { obj.checked = false; }
                   else
                   { obj.checked = true; }
               }

           }  //function Link
       }

       $(document).ready(function () {
           $('#ddlRegionalChannel').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   $('#txtRegionalName').focus();
               }

           });
       });
       $(document).ready(function () {
           $('#ddlCompanyName').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   $('#txtRegionalName').focus();
               }

           });
       });
       $(document).ready(function () {
           $('#ddlLanguage').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   $('#txtRegionalName').focus();
               }

           });
       });
      
   </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:ScriptManager ID="ScriptManager2" runat="server"/>
   <asp:UpdatePanel ID="updPanelChannelRegMaster" runat="server">
      <ContentTemplate>
         <asp:Table ID="Table1" runat="server" GridLines="both" 
            BorderWidth="2" CellPadding="5" CellSpacing="0" Width="70%">
            <asp:TableRow>
               <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="90%">
                  <h2>
                     Channel Regional Names
                  </h2>
                  <asp:Table ID="Table2" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="90%">
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Company
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:Label ID="lbCompanyName" runat="server" Text="Company Name" Font-Bold="true" ForeColor="Green" />
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Channel
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:ComboBox ID="ddlRegionalChannel" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append"
                              DataSourceID="SqlmstChannel" DataTextField="ChannelId" 
                              DataValueField="ChannelId" AutoPostBack="True">
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlmstChannel" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId">
                              
                           </asp:SqlDataSource>
                           <br /><br />
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Language
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:ComboBox ID="ddlLanguage" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append" 
                              >
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlmstLanguage" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT LanguageId, FullName FROM mst_Language where active='1' ORDER BY FullName">                                
                           </asp:SqlDataSource>
                           <br /><br />
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Channel Regional Name
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:TextBox ID="txtRegionalName" runat="server" Width="250px" MaxLength="200"></asp:TextBox>
                           <asp:TextBoxWatermarkExtender TargetControlID="txtRegionalName" ID="ExttxtRegionalName" WatermarkText="Enter Channel Regional Name" runat="server"></asp:TextBoxWatermarkExtender>
                           <br />
                           <asp:RequiredFieldValidator ID="RFVtxtRegionalName" ControlToValidate="txtRegionalName" runat="server" ForeColor="Red" Text="* (Channel Reginal name can not be left blank.)" ValidationGroup="RFGRegionalName"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Synopsis Needed
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:CheckBox ID="chkSynopsis" runat="server" Checked="True" onclick="javascript:AlertSynopsis(this);" />
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                           <asp:TextBox ID="txtHiddenId1" runat="server" Visible="False" Text="0"></asp:TextBox>
                           <asp:Button ID="btnAddRegionalName" runat="server" Text="Add" ValidationGroup="RFGRegionalName" UseSubmitBehavior="false"/>
                           &nbsp;&nbsp;
                           <asp:Button ID="btnCancel1" runat="server" Text="Cancel"/>
                        </asp:TableCell>
                     </asp:TableRow>
                  </asp:Table>
                  <br />
                  <asp:Table ID="Table3" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="90%">
                     <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:GridView ID="grdRegionalNames" runat="server" AutoGenerateColumns="False" 
                              CellPadding="4" DataKeyNames="RowID" DataSourceID="sqlDSRegionalMaster" 
                              ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                              SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                              <AlternatingRowStyle BackColor="White" />
                              <Columns>
                                 <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Language Id" SortExpression="LanguageId" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbLanguageId" runat="server" Text='<%# Bind("LanguageId") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Channel Regional Name" SortExpression="RegionalName" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbRegionalName" runat="server" Text='<%# Bind("RegionalName") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Language FullName" SortExpression="FullName" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbFullName" runat="server" Text='<%# Bind("FullName") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Synopsis" SortExpression="SynopsisNeeded" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbSynopsisNeeded" runat="server" Text='<%# Bind("SynopsisNeeded") %>' Visible="true" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png"/>
                                 <asp:CommandField ShowDeleteButton="True" ButtonType="Image"  DeleteImageUrl="~/images/delete.png"/>
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
                           <asp:SqlDataSource ID="sqlDSRegionalMaster" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT a.RowID,a.synopsisneeded, b.ChannelId, a.RegionalName, c.LanguageId, c.FullName FROM mst_ChannelRegionalName a join mst_Channel b on a.ChannelId = b.ChannelId join mst_Language c on a.LanguageId=c.LanguageId where a.ChannelId=@ChannelId"
                              DeleteCommand="sp_mst_ChannelRegionalName" DeleteCommandType="StoredProcedure"
                              >
                              <SelectParameters>
                                 <asp:ControlParameter ControlID="ddlRegionalChannel" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                              </SelectParameters>
                           </asp:SqlDataSource>
                        </asp:TableCell>
                     </asp:TableRow>
                  </asp:Table>
               </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
      </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
