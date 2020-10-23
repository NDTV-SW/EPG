<%@ Page Title="Mail" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Mail.aspx.vb" Inherits="EPG.Mail" %>
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
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
      Mail
   </h2>
   <asp:ScriptManager ID="ScriptManager1" runat="server" />
   <asp:Table ID="Table1" runat="server" Width="95%" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0">
      <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Company
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:ComboBox ID="ddlCompanyName" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append"
                              DataSourceID="SqlDsCompanyMaster" DataTextField="CompanyName" 
                              DataValueField="CompanyId" AutoPostBack="True">
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlDsCompanyMaster" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT CompanyId, CompanyName FROM mst_Company where active='1' ORDER BY CompanyName">
                           </asp:SqlDataSource>
                           <br /><br />
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Channel
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:ComboBox ID="ddlChannel" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append"
                              DataSourceID="SqlmstChannel" DataTextField="ChannelId" 
                              DataValueField="ChannelId" AutoPostBack="True">
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlmstChannel" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT ChannelId FROM mst_Channel where COMPANYID=@COMPANYID and active='1' ORDER BY ChannelId">
                              <SelectParameters>
                                 <asp:ControlParameter ControlID="ddlCompanyName" Name="COMPANYID" PropertyName="SelectedValue" Type="Int32" />
                              </SelectParameters>
                           </asp:SqlDataSource>
                           <br /><br />
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Contact Person
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                           <asp:ComboBox ID="ddlContactPerson" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append"
                              DataSourceID="SqlContactPerson" DataTextField="POINTPERSONNAME" 
                              DataValueField="EMAIL" AutoPostBack="True">
                           </asp:ComboBox>
                           
                           <asp:SqlDataSource ID="SqlContactPerson" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT POINTPERSONNAME,EMAIL FROM DT_COMPANY WHERE COMPANYID=@COMPANYID">
                              <SelectParameters>
                                 <asp:ControlParameter ControlID="ddlCompanyName" Name="COMPANYID" PropertyName="SelectedValue" Type="Int32" />
                              </SelectParameters> 
                           </asp:SqlDataSource>
                           <br /><br />
                        </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                           Select Template
                        </asp:TableHeaderCell><asp:TableCell HorizontalAlign="Left">
                           <asp:ComboBox ID="ddlTemplate" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append"
                              DataSourceID="SqlTemplate" DataTextField="TEMPLATENAME" 
                              DataValueField="ID" AutoPostBack="True">
                           </asp:ComboBox>
                           <br /><br />
                           <asp:SqlDataSource ID="SqlTemplate" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT ID,TEMPLATENAME FROM MAIL_TEMPLATE ORDER BY TEMPLATENAME ASC">
                              
                           </asp:SqlDataSource>
                           
                           
                        </asp:TableCell></asp:TableRow><asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                            SignOff
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="lbSignOff" runat="server" Visible="True" Text="Thanks & Regards<br/>Team EPG, NDTV." />  
                            <br /><br />
                        </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Button ID="btnMail" runat="server" Text="Mail" Width="80px" />
                            </asp:TableCell></asp:TableRow>
                            <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            Dear Sir/Mam,
                            <asp:Label ID="lbContactPerson" runat="server" Text=" Contact Person " Visible="false" />
                        </asp:TableCell></asp:TableRow>
                            
                            </asp:Table></asp:Content>