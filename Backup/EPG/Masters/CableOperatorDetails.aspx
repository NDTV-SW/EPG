<%@ Page Title="CableOperator Details" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="CableOperatorDetails.aspx.vb" Inherits="EPG.CableOperatorDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <script type="text/javascript">
       $(document).ready(function () {
           $('#ddlCableOperatorName').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   $('#txtPointPersonName').focus();
               }

           });
       });
       
   </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:ScriptManager ID="ScriptManager1" runat="server"/>
   <asp:UpdatePanel ID="updPanelCableOperatorDetails" runat="server">
      <ContentTemplate>
         <h2>
            CableOperator Details
         </h2>
         <asp:Table ID="tblCableOperatorMaster" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="80%">
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right" Width="30%">
                  CableOperator Name
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:ComboBox ID="ddlCableOperatorName" runat="server" CausesValidation="false" Width="350px"
                     AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ItemInsertLocation="Append"
                     DataSourceID="SqlDsCableOperatorMaster" DataTextField="Name" 
                     DataValueField="OperatorId" AutoPostBack="true">
                  </asp:ComboBox>
                  <asp:SqlDataSource ID="SqlDsCableOperatorMaster" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                     SelectCommand="SELECT OperatorId, Name FROM mst_Operators where active='1' ORDER BY name">
                  </asp:SqlDataSource>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Point Person Name
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtPointPersonName" runat="server" Width="350px" MaxLength="100"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtPointPersonName" ID="ExttxtPointPersonName" WatermarkText="Enter Point Person Name" runat="server"></asp:TextBoxWatermarkExtender>
                  <asp:RequiredFieldValidator ID="RFVtxtPointPersonName" ControlToValidate="txtPointPersonName" runat="server" ForeColor="Red" Text="* (Point Person's name can not be left blank.)" ValidationGroup="RFGCableOperatorDetails"></asp:RequiredFieldValidator>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Designation
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtDesignation" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtDesignation" ID="ExttxtDesignation" WatermarkText="Enter Designation" runat="server" Enabled="True"></asp:TextBoxWatermarkExtender>
                  <%--<asp:RequiredFieldValidator ID="RFVtxtDesignation" ControlToValidate="txtDesignation" runat="server" ForeColor="Red" Text="* (Designation can not be left blank.)" ValidationGroup="RFGCableOperatorDetails"></asp:RequiredFieldValidator>--%>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  DOB
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtDOB" runat="server" Width="350px"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtDOB" ID="ExttxtDOB" WatermarkText="Enter Date Of Birth" runat="server"></asp:TextBoxWatermarkExtender>
                  <asp:CalendarExtender ID="CalendarExtender1" DefaultView="Days" Format="MM/dd/yyyy"  Enabled="True" PopupPosition="BottomLeft" runat="server" TargetControlID="txtdob">
                  </asp:CalendarExtender>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Email
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtEmail" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtEmail" ID="ExttxtEmail" WatermarkText="Enter Email ID" runat="server"></asp:TextBoxWatermarkExtender>
                  <asp:RequiredFieldValidator ID="RFVtxtEmail" ControlToValidate="txtEmail" runat="server" ForeColor="Red" Text="* (Email ID can not be left blank.)" ValidationGroup="RFGCableOperatorDetails"></asp:RequiredFieldValidator>
                  <br />
                  <asp:RegularExpressionValidator ID="REVEmail" ValidationExpression="^[_A-Za-z0-9-]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$" ControlToValidate="txtEmail" runat="server" ForeColor="Red" Text="* (Proper Email needs to be entered.)" ValidationGroup="RFGCableOperatorDetails"></asp:RegularExpressionValidator>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Mobile
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtMobile" runat="server" Width="350px" MaxLength="20"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtMobile" ID="ExttxtMobile" WatermarkText="Enter Mobile Number" runat="server"></asp:TextBoxWatermarkExtender>
                  <%--<asp:RequiredFieldValidator ID="RFVtxtMobile" ControlToValidate="txtMobile" runat="server" ForeColor="Red" Text="* (Mobile number can not be left blank.)" ValidationGroup="RFGCableOperatorDetails"></asp:RequiredFieldValidator>--%>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Point Person Name 2
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtPointPerson2Name" runat="server" Width="350px" MaxLength="100"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtPointPerson2Name" ID="TextBoxWatermarkExtender1" WatermarkText="Enter Point Person Name" runat="server"></asp:TextBoxWatermarkExtender>
                  
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Email 2
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtEmail2" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtEmail2" ID="TextBoxWatermarkExtender2" WatermarkText="Enter Email ID" runat="server"></asp:TextBoxWatermarkExtender>
                  <asp:RegularExpressionValidator ID="REVtxtEmail2" ValidationExpression="^[_A-Za-z0-9-]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$" ControlToValidate="txtEmail2" runat="server" ForeColor="Red" Text="* (Proper Email needs to be entered.)" ValidationGroup="RFGCableOperatorDetails"></asp:RegularExpressionValidator>
               </asp:TableCell>
            </asp:TableRow>
            
            
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Mobile 2
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtMobile1" runat="server" Width="350px" MaxLength="20"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtMobile1" ID="ExttxtMobile1" WatermarkText="Enter Mobile Number" runat="server"></asp:TextBoxWatermarkExtender>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Landline
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtLandline" runat="server" Width="350px" MaxLength="20"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtLandline" ID="ExttxtLandline" WatermarkText="Enter Landline Number" runat="server"></asp:TextBoxWatermarkExtender>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                  Extension
               </asp:TableHeaderCell>
               <asp:TableCell HorizontalAlign="Left">
                  <asp:TextBox ID="txtExtension" runat="server" Width="350px" MaxLength="20"></asp:TextBox>
                  <asp:TextBoxWatermarkExtender TargetControlID="txtExtension" ID="ExttxtExtension" WatermarkText="Enter Extension Number" runat="server"></asp:TextBoxWatermarkExtender>
               </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                  <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
                  <asp:Button ID="btnAddCableOperatorDetails" runat="server" Text="Add" ValidationGroup="RFGCableOperatorDetails" UseSubmitBehavior="false"/>
                  &nbsp;&nbsp;
                  <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
               </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <br />
         <asp:Table ID="tblCableOperatorGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="90%">
            <asp:TableRow>
                <asp:TableCell>
                    <h2>Operator Summary</h2>
                  <asp:GridView ID="grdCableOperatormaster" runat="server" AutoGenerateColumns="False"   EmptyDataText="No record found" EmptyDataRowStyle-BackColor="Aqua"
                     CellPadding="4" DataKeyNames="OperatorId" DataSourceID="sqlDSCableOperatorDetails" 
                     ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                     SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                        <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="false">
                           <ItemTemplate>
                              <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Id" SortExpression="OperatorId" Visible="false">
                           <ItemTemplate>
                              <asp:Label ID="lbOperatorId" runat="server" Text='<%# Bind("OperatorId") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="Name" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbName" runat="server" Text='<%# Bind("Name") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Person" SortExpression="PointPersonName" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbPointPersonName" runat="server" Text='<%# Bind("PointPersonName") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation" SortExpression="Designation" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbDesignation" runat="server" Text='<%# Bind("Designation") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DOB" SortExpression="DOB" Visible="false">
                           <ItemTemplate>
                              <asp:Label ID="lbDOB" runat="server" Text='<%# Bind("DOB") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="Email" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbEmail" runat="server" Text='<%# Bind("Email") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile" SortExpression="Mobile" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbMobile" runat="server" Text='<%# Bind("Mobile") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Person 2" SortExpression="PointPerson2Name" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbPointPerson2Name" runat="server" Text='<%# Bind("PointPerson2Name") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email2" SortExpression="Email2" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbEmail2" runat="server" Text='<%# Bind("Email2") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mobile 2" SortExpression="Mobile1" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbMobile1" runat="server" Text='<%# Bind("Mobile1") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Landline" SortExpression="Landline" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbLandline" runat="server" Text='<%# Bind("Landline") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Extension" SortExpression="Extension" Visible="true">
                           <ItemTemplate>
                              <asp:Label ID="lbExtension" runat="server" Text='<%# Bind("Extension") %>' Visible="true" />
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png"/>
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/images/delete.png"/>
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
                  <asp:SqlDataSource ID="sqlDSCableOperatorDetails" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                     SelectCommand="SELECT a.OperatorId,b.RowID, a.name, b.PointPersonName,b.PointPerson2Name, b.Designation, CONVERT(VARCHAR(8), b.DOB, 1) dob, b.Email,b.Email2, b.Mobile, b.Mobile1, b.Landline, b.Extension FROM mst_Operators a join dt_operators b on a.OperatorId = b.OperatorId where b.OperatorId=@OperatorId"
                     >
                     <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCableOperatorName" Name="OperatorId" PropertyName="SelectedValue" Type="Int32" />
                     </SelectParameters>
                     
                  </asp:SqlDataSource>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
               <asp:TableCell HorizontalAlign="Left">
               <h2>CableOperator Details</h2>
                  <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false"  EmptyDataText="No record found" EmptyDataRowStyle-BackColor="Red"
                     CellPadding="4" DataSourceID="sqlDSgrdCableOperatorDetail" 
                     ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                     SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                        <asp:BoundField DataField="Name" HeaderText="CableOperator" />
                        <asp:BoundField DataField="pointpersonName" HeaderText="Contact Person" />
                        <asp:BoundField DataField="Designation" HeaderText="" />
                        <asp:BoundField DataField="Email" HeaderText="E-Mail" />
                        <asp:BoundField DataField="mobile" HeaderText="" />
                        <asp:BoundField DataField="extension" HeaderText="Extension" />
                        
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
                  <asp:SqlDataSource ID="sqlDSgrdCableOperatorDetail" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                     SelectCommand="sp_CableOperatorDetails_view" SelectCommandType="StoredProcedure">
                  </asp:SqlDataSource>
               </asp:TableCell>
            </asp:TableRow>
            

         </asp:Table>
      </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
