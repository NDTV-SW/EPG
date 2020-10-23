<%@ Page Language="vb" Title="PROGRAM LEVEL SUB GENRE MASTER"  AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SubGenreMaster.aspx.vb" Inherits="EPG.SubGenreMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <script type="text/javascript">
       $(document).ready(function () {
           $('#ddlGenreName').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   $('#txtSubGenreName').focus();
               }

           });
       });
   </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:ScriptManager ID="ScriptManager1" runat="server"/>
   <%--<asp:UpdatePanel ID="updPanelSubGenreMaster" runat="server">
      <ContentTemplate>--%>
         <h2>
            PROGRAM LEVEL SUB GENRE MASTER
         </h2>
         <asp:Table ID="tblSubGenreMaster" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="50%">
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                  Genre Name
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:ComboBox ID="ddlGenreName" runat="server" AutoCompleteMode="SuggestAppend"
                     DropDownStyle="DropDownList" ItemInsertLocation="Append"
                     DataSourceID="SqlDsGenreMaster" DataTextField="GenreName" 
                     DataValueField="GenreId" AutoPostBack="true">
                  </asp:ComboBox>
                  <asp:SqlDataSource ID="SqlDsGenreMaster" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                     SelectCommand="SELECT GenreId, GenreName FROM mst_Genre where genrecategory='P' ORDER BY GenreName">
                  </asp:SqlDataSource>
                  <br /><br />
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                  Sub Genre Name
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtSubGenreName" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtSubGenreName" ID="ExttxtSubGenreName" WatermarkText="Enter Sub Genre Name" runat="server"></asp:TextBoxWatermarkExtender>
                  <br />
                  <asp:RequiredFieldValidator ID="RFVtxtSubGenreName" ControlToValidate="txtSubGenreName" runat="server" ForeColor="Red" Text="* (Sub Genre name can not be left blank.)" ValidationGroup="RFGSubGenreMaster"></asp:RequiredFieldValidator>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                  <asp:TextBox ID="txtHiddenId" runat="server" Visible="false" Text="0"></asp:TextBox>
                  <asp:Button ID="btnAddSubGenre" runat="server" Text="Add" ValidationGroup="RFGSubGenreMaster" UseSubmitBehavior="false"/>
                  &nbsp;&nbsp;
                  <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
               </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <br />
         <asp:Table ID="tblSubGenreGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="50%">
            <asp:TableRow>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:GridView ID="grdSubGenreMaster" runat="server" AutoGenerateColumns="False" 
                     CellPadding="4" DataKeyNames="SubGenreId" DataSourceID="sqlDSSubGenreMaster" 
                     ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                     SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                        <asp:TemplateField HeaderText="SubGenre Id" SortExpression="SubGenreId" Visible="True">
                           <ItemTemplate>
                              <asp:Label ID="lbSubGenreId" runat="server" Text='<%# Bind("SubGenreId") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Genre Id" SortExpression="GenreId" Visible="false">
                           <ItemTemplate>
                              <asp:Label ID="lbGenreId" runat="server" Text='<%# Bind("GenreId") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Genre Name" SortExpression="GenreName" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbGenreName" runat="server" Text='<%# Bind("GenreName") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SubGenreName" SortExpression="SubGenreName" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbSubGenreName" runat="server" Text='<%# Bind("SubGenreName") %>' Visible="true" />
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
                  <asp:SqlDataSource ID="sqlDSSubGenreMaster" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                     SelectCommand="SELECT a.SubGenreId SubGenreId, a.SubGenreName , a.GenreID GenreID, b.GenreName FROM mst_SubGenre a join mst_Genre b on a.GenreID = b.GenreID where a.GENREID=@GENREID order by SubGenreName"
                     DeleteCommand="sp_mst_SubGenre" DeleteCommandType="StoredProcedure"
                     >
                     <SelectParameters>
                        <asp:ControlParameter ControlID="ddlGenreName" Name="GENREID" PropertyName="SelectedValue" Type="Int32" />
                     </SelectParameters>
                  </asp:SqlDataSource>
               </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
      <%--</ContentTemplate>
   </asp:UpdatePanel>--%>
</asp:Content>
