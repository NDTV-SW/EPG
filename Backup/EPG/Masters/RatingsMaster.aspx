<%@ Page Title="Parental Ratings Master" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="RatingsMaster.aspx.vb" Inherits="EPG.RatingsMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:ScriptManager ID="ScriptManager2" runat="server"/>
   <asp:UpdatePanel ID="updRatingsMaster" runat="server">
      <ContentTemplate>
         <h2>
            Parental Ratings Master
         </h2>
         <asp:Table ID="tblRatingMaster" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="50%">
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Rating Id
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtRatingId" runat="server" Width="250px"  MaxLength="10"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtRatingId" ID="ExttxtRatingId" WatermarkText="Enter Rating Id" runat="server"></asp:TextBoxWatermarkExtender>
                  <br />
                  <asp:RequiredFieldValidator ID="RFVtxtRatingId" ControlToValidate="txtRatingId" runat="server" ForeColor="Red" Text="* (Rating Id can not be left blank.)" ValidationGroup="RFGRatingMaster"></asp:RequiredFieldValidator>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Rating Value
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtRatingValue" runat="server" Width="250px" MaxLength="4"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtRatingValue" ID="ExttxtRatingValue" WatermarkText="Enter Rating Value" runat="server"></asp:TextBoxWatermarkExtender>
                  <br />
                  <asp:RequiredFieldValidator ID="RFVtxtRatingValue" ControlToValidate="txtRatingValue" runat="server" ForeColor="Red" Text="* (Rating value can not be left blank.)" ValidationGroup="RFGRatingMaster"></asp:RequiredFieldValidator>
                  <br />
                  <asp:RegularExpressionValidator ID="REVtxtRatingValue" ControlToValidate="txtRatingValue" runat="server" ForeColor="Red" ValidationExpression="^[0-9]+$" Text="* (Rating Id numbers only.)" ValidationGroup="RFGRatingMaster" ></asp:RegularExpressionValidator>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Rating Description
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtRatingDesc" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtRatingDesc" ID="EXTtxtRatingDesc" WatermarkText="Enter Rating Description" runat="server"></asp:TextBoxWatermarkExtender>
                  <br />
                  <asp:RequiredFieldValidator ID="RFVtxtRatingDesc" ControlToValidate="txtRatingDesc" runat="server" ForeColor="Red" Text="* (Rating Description can not be left blank.)" ValidationGroup="RFGtxtRatingDesc"></asp:RequiredFieldValidator>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                  <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
                  <asp:Button ID="btnAddRating" runat="server" Text="Add" ValidationGroup="RFGRatingMaster"/>
                  &nbsp;&nbsp;
                  <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
               </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <br />
         <asp:Table ID="tblRatingGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="50%">
            <asp:TableRow>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:GridView ID="grdRatingmaster" runat="server" AutoGenerateColumns="False" 
                     CellPadding="4" DataKeyNames="RatingId" DataSourceID="sqlDSRatingMaster" 
                     ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                     SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                        <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="false">
                           <ItemTemplate>
                              <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rating Id" SortExpression="RatingId" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbRatingId" runat="server" Text='<%# Bind("RatingId") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RatingValue" SortExpression="RatingValue" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbRatingValue" runat="server" Text='<%# Bind("RatingValue") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="RatingDesc" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbRatingDesc" runat="server" Text='<%# Bind("RatingDesc") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Images/Edit.png"/>
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images/delete.png"/>
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
                  <asp:SqlDataSource ID="sqlDSRatingMaster" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                     SelectCommand="SELECT * FROM mst_ParentalRating order by RatingId"
                     DeleteCommand="sp_mst_ParentalRating" DeleteCommandType="StoredProcedure">
                  </asp:SqlDataSource>
               </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
      </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

