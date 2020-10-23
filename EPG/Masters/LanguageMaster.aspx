<%@ Page Title="Language Master" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
   CodeBehind="LanguageMaster.aspx.vb" Inherits="EPG.LanguageMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <script type="text/javascript">
       function AlertUser(obj) {
           if (obj.checked) {
           }
           else {
               if (obj.value != '') {
                   var doit;
                   doit = confirm('Do you want to set this language as In-Active?');
                   if (doit == true)
                   { obj.checked = false; }
                   else
                   { obj.checked = true; }
               }
           }  //function Link
       }
   </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager ID="ScriptManager1" runat="server"/>
    <asp:UpdatePanel ID="upd1" runat="server" >
      <ContentTemplate>
         <h2>
            Language Master
         </h2>
         <asp:Table ID="tblLanguageMaster"  CssClass="table table-striped"  runat="server" GridLines="both" Width="60%">
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                  Language FullName
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                  <asp:TextBox ID="txtLanguageFullName" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtLanguageFullName" ID="ExttxtLanguageFullName" WatermarkText="Enter Full Name (e.g: ENGLISH)" runat="server"></asp:TextBoxWatermarkExtender>
                  <br />
                  <asp:RequiredFieldValidator ID="RFVtxtLanguageFullName" ControlToValidate="txtLanguageFullName" runat="server" ForeColor="Red" Text="* (Language full name can not be left blank.)" ValidationGroup="RFGLangMaster"></asp:RequiredFieldValidator>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                  ShortName
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                  <asp:TextBox ID="txtLanguageShortName" runat="server" Width="250px" MaxLength="3"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtLanguageShortName" ID="ExttxtLanguageShortName" WatermarkText="Enter Short Name (e.g: ENG)" runat="server"></asp:TextBoxWatermarkExtender>
                  <br />
                  <asp:RequiredFieldValidator ID="RFVtxtLanguageShortName" ControlToValidate="txtLanguageShortName" runat="server" ForeColor="Red" Text="* (Language short name can not be left blank.)" ValidationGroup="RFGLangMaster"></asp:RequiredFieldValidator>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                  <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
                  <h1><asp:Button ID="btnAddLanguage" CssClass="button label-default" runat="server" Text="Add" ValidationGroup="RFGLangMaster"/>
                  &nbsp;&nbsp;
                  <asp:Button ID="btnCancel"  CssClass="button label-danger" runat="server" Text="Cancel"/></h1>
               </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <asp:CheckBox ID="chkActive" runat="server" />
         <br />
         <asp:Table ID="tblLanguageGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="60%">
            <asp:TableRow>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:GridView ID="grdLanguagemaster" CssClass="table table-striped"  runat="server" AutoGenerateColumns="False" 
                     CellPadding="4" DataKeyNames="LanguageId" DataSourceID="sqlDSLanguageMaster" AllowPaging="true" PageSize="20">
                     
                     <Columns>
                        <asp:TemplateField HeaderText="Language Id" SortExpression="LanguageId" Visible="false">
                           <ItemTemplate>
                              <asp:Label ID="lbLanguageId" runat="server" Text='<%# Bind("LanguageId") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Language Full Name" SortExpression="FullName">
                           <ItemTemplate>
                              <asp:Label ID="lbFullName" runat="server" Text='<%# Bind("FullName") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Language Short Name" SortExpression="ShortName" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbShortName" runat="server" Text='<%# Bind("ShortName") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbActive" runat="server" Text='<%# Bind("Active") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Images/Edit.png"/>
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image"  DeleteImageUrl="~/Images/delete.png"/>
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
                  <asp:SqlDataSource ID="sqlDSLanguageMaster" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                     SelectCommand="SELECT * FROM mst_Language where active='1' order by LanguageId"
                     DeleteCommand="sp_mst_Language" DeleteCommandType="StoredProcedure">
                  </asp:SqlDataSource>
               </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
      </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
