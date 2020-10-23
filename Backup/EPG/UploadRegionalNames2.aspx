<%@ Page Title="Upload Regional Names" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="UploadRegionalNames2.aspx.vb" Inherits="EPG.UploadRegionalNames2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
       $(document).ready(function () {
           $('#ddlChannelName').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   javascript: __doPostBack('ddlChannelName', '')
               }

           });
       });
   </script>
   <style type="text/css">
      .StyleRed
      {
      color:red;
      }
   </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
   <h2>
      Upload Regional Names
   </h2>
   
   <asp:Table ID="tblUpload" runat="server" Width="90%" BorderWidth="2">
      <asp:TableRow>
         <asp:TableCell ColumnSpan="6" HorizontalAlign="Center" >
            <asp:Image ID="imgSampleRegUpload" ImageUrl="~/Images/SampleRegUpload.JPG" runat="server" />
            <br />
            Sample Format of Excel to Upload<br /><br />
         </asp:TableCell>
      </asp:TableRow>
      <asp:TableRow>
         <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Top">
            Select Channel
         </asp:TableHeaderCell>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:ComboBox CssClass="ddlPostBack" ID="ddlChannelName" runat="server"   AutoCompleteMode="SuggestAppend"
               DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="true">
            </asp:ComboBox>
            <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId">
            </asp:SqlDataSource>
         </asp:TableCell>
         <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
            Language
         </asp:TableHeaderCell>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
            <asp:ComboBox ID="ddlLanguage" CssClass="ddlPostBack" runat="server"   AutoCompleteMode="SuggestAppend"
               DropDownStyle="DropDownList" ItemInsertLocation="Append"  AppendDataBoundItems="false" AutoPostBack="true">
            </asp:ComboBox>
         </asp:TableCell>
         <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            
            <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1" runat="server" ErrorMessage="Only .xls file Supported !"
               ValidationExpression="^.*\.(xls|XLS)$" ForeColor="Red" ValidationGroup="VGUploadRegSched" />
            <br />
            <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server" ErrorMessage="Please select file first !"
               ForeColor="Red" ValidationGroup="VGUploadRegSched" />
         </asp:TableCell><asp:TableCell HorizontalAlign="Left" Width="25%" VerticalAlign="Top">
            
            <asp:Button ID="btnUpload" runat="server" Text="UPLOAD" Enabled="true" ValidationGroup="VGUploadRegSched" UseSubmitBehavior="false" />
            <br />
            <asp:Label ID="lbUploadError" runat="server" ForeColor="Red" Text="File Not Uploaded. Duplicate Column Name Exists in Excel!" Visible="false" />
            </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Left" VerticalAlign="Top">
         
         <asp:TableHeaderCell HorizontalAlign="Right" ColumnSpan="4">
         Select Genre
         </asp:TableHeaderCell><asp:TableCell HorizontalAlign="Left">
         <asp:ComboBox ID="ddlGenre1" runat="server" Width="100px" AutoCompleteMode="SuggestAppend"
                           DropDownStyle="DropDownList" ItemInsertLocation="Append"
                           DataSourceID="SqlDsGenre" DataTextField="genrename" 
                           DataValueField="genreid" AutoPostBack="true">
                        </asp:ComboBox>
                        <br /><br />
                        
          </asp:TableCell></asp:TableRow></asp:Table><br />
   <asp:Table runat="server" ID='tbInstructions'  Width="90%" HorizontalAlign="Center" >

      <asp:TableRow>
         <asp:TableCell HorizontalAlign="Center">
            Press "<span class="StyleRed">Update</span>" to update with Available Program name.<br />
            Press "<span class="StyleRed">Add New</span>" by selecting all fields to Insert new Program.     
         </asp:TableCell></asp:TableRow></asp:Table><asp:GridView ID="grdData" runat="server" AutoGenerateColumns="false" EmptyDataRowStyle-BackColor="LightGreen" EmptyDataText="No Discrepancies Found"
               DataSourceID="SqlDSgrdData" CellPadding="4" ForeColor="#333333" GridLines="Vertical" Width="80%" AllowPaging="true" PageSize="20">
               <AlternatingRowStyle BackColor="White" />
               <Columns>
                  <asp:BoundField DataField="Rowid" HeaderText="RowId" SortExpression="ExcelProgName" />
                  <asp:BoundField DataField="ProgName" HeaderText="Program Name" SortExpression="MstProgName" />
                  <asp:BoundField DataField="LanguageId" HeaderText="Lang Id" SortExpression="ProgId"  />
                  <asp:TemplateField HeaderText="Available Programs">
                     <ItemTemplate>
                        <asp:ComboBox ID="ddlPrograms" runat="server" AutoCompleteMode="SuggestAppend"
                           DropDownStyle="DropDownList" ItemInsertLocation="Append" />
                     </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Genre">
                     <ItemTemplate>
                        <asp:ComboBox ID="ddlGenre" runat="server" Width="100px" AutoCompleteMode="SuggestAppend"
                           DropDownStyle="DropDownList" ItemInsertLocation="Append"
                           DataSourceID="SqlDsGenre" DataTextField="genrename" 
                           DataValueField="genreid">
                        </asp:ComboBox>
                     </ItemTemplate>
                     <ControlStyle Width="100px" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Rating">
                     <ItemTemplate>
                        <asp:ComboBox ID="ddlRating" runat="server" Width="40px" AutoCompleteMode="SuggestAppend"
                           DropDownStyle="DropDownList" ItemInsertLocation="Append"
                           DataSourceID="SqlDsRating" DataTextField="ratingid" 
                           DataValueField="ratingid">
                        </asp:ComboBox>
                     </ItemTemplate>
                     <ControlStyle Width="40px" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Series">
                     <ItemTemplate>
                        <asp:ComboBox ID="ddlSeries" runat="server" Width="50px"  AutoCompleteMode="SuggestAppend"
                           DropDownStyle="DropDownList" ItemInsertLocation="Append">
                           <asp:ListItem Text="Disabled" Value="0"/>
                           <asp:ListItem Text="Enabled" Value="1"/>
                        </asp:ComboBox>
                     </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Active">
                     <ItemTemplate>
                        <asp:ComboBox ID="ddlActive" runat="server" Width="40px" AutoCompleteMode="SuggestAppend"
                           DropDownStyle="DropDownList" ItemInsertLocation="Append">
                           <asp:ListItem Text="Active" Value="1"/>
                           <asp:ListItem Text="In-Active" Value="0"/>
                        </asp:ComboBox>
                     </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField>
                     <ItemTemplate>
                        <asp:Button ID="Btn_AddNew" runat="server" CausesValidation="False" CommandName="AddNew"
                           Text="Add New" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'  />
                     </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField>
                     <ItemTemplate>
                        <asp:Button ID="Btn_Updt" runat="server" CausesValidation="False" CommandName="Updt"
                           Text="Update" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' Visible="true" />
                     </ItemTemplate>
                  </asp:TemplateField>
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
            <asp:TextBox ID="txtProgName" runat="server" Text="" Visible="false" />
            <asp:SqlDataSource ID="sqlDSPrograms" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" >
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDSgrdData" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="sp_epg_upload_regional_data" SelectCommandType="StoredProcedure">
               <SelectParameters>
                  <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                  <asp:ControlParameter ControlID="ddlLanguage" Name="Language" PropertyName="SelectedValue" Type="String" />
               </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDsGenre" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="select * from dbo.fn_ProgGenre(@channelid) order by 3,2"> 
               <SelectParameters>
                <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
               </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDsRating" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="select ratingid from mst_parentalrating where ratingid='U' order by ratingid">
            </asp:SqlDataSource>
         
         <asp:Table runat="server" ID='tbGrdData' Width="90%" HorizontalAlign="Center" >
         <asp:TableRow>
         <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">
            <br /><br />
         </asp:TableCell></asp:TableRow><asp:TableRow>
         <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">
            <asp:SqlDataSource ID="SqlDSgrdGenre" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="sp_epg_validate_genre" SelectCommandType="StoredProcedure">
               <SelectParameters>
                  <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
               </SelectParameters>
            </asp:SqlDataSource>
         </asp:TableCell></asp:TableRow><asp:TableRow>
         <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">
            <br /><br />
         </asp:TableCell></asp:TableRow></asp:Table><asp:GridView ID="grdExcelData" runat="server" EmptyDataText="No record found"
               DataSourceID="SqlDSgrdExcelData" CellPadding="4" ForeColor="#333333" GridLines="Vertical" Width="95%" AutoGenerateColumns="true">
               <AlternatingRowStyle BackColor="White" />
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
         <asp:SqlDataSource ID="SqlDSgrdExcelData" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="Select a.progname [Program Name], [Synopsis]=(select synopsis from mst_programregional where languageId=1 and progid=a.progid),  
                c.progname [Regional Name], c.synopsis [Regional Synopsis] from mst_program a 
                left outer join mst_programregional c on (a.progid=c.progid)
                and C.languageid=@languageid
                Where a.channelId=@channelid">
               <SelectParameters>
                  <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                  <asp:ControlParameter ControlID="ddllanguage" Name="languageid" PropertyName="SelectedValue" Type="String" />
               </SelectParameters>
            </asp:SqlDataSource>
        <asp:Button ID="btnExcel" runat="server" Text="Export Excel"/>
         
         </asp:Content>