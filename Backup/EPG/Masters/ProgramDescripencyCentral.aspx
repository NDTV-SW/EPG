<%@ Page Title="Program Discrepancies Central" Language="vb" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="false" CodeBehind="ProgramDescripencyCentral.aspx.vb" Inherits="EPG.ProgramDescripencyCentral" EnableEventValidation="false" MaintainScrollPositionOnPostback ="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
           $('#ddlLanguage').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   javascript: __doPostBack('ddlLanguage', '')
               }
           });
           
           function openWin(progid, langid, mode, progname,synopsis,regprogname,regsynopsis,language) {
               window.open("EditDiscrepancyCentral.aspx?&ProgId=" + progid + "&LangId=" + langid + "&Mode=" + mode + "&ProgName=" + progname + "&Synopsis=" + synopsis + "&RegProgName=" + regprogname + "&RegSynopsis=" + regsynopsis + "&Language=" + language, "Discrepancy Central", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
           }
        
   </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
   <asp:Table ID="Table1" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="80%">
      <asp:TableRow>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="50%">
            <h2>
               Programme Discrepancies Central
            </h2>

            <asp:Table ID="tblProgramGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="95%">
               <asp:TableRow>
                  
                  <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top" Width="25%">
                     Select
                  </asp:TableHeaderCell>
                  <asp:TableCell HorizontalAlign="Left" Width="25%">
                     <asp:ComboBox ID="ddlType" runat="server"   AutoCompleteMode="SuggestAppend" Width="250px"
                        DropDownStyle="DropDownList" ItemInsertLocation="Append"   AppendDataBoundItems="true" AutoPostBack="true">
                        <asp:ListItem Text="Programme" Value="ProgName">Only Programme Name Required</asp:ListItem>
                        <asp:ListItem Text="Synopsis" Value="Synopsis">Programme Name & Synopsis Required</asp:ListItem>
                     </asp:ComboBox>
                     
                  </asp:TableCell>
                  
                  <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top" Width="25%">
                     Language
                  </asp:TableHeaderCell>
                  <asp:TableCell HorizontalAlign="Left" Width="25%">
                     <asp:ComboBox ID="ddlLanguage" runat="server"   AutoCompleteMode="SuggestAppend"
                        DropDownStyle="DropDownList" ItemInsertLocation="Append"   AppendDataBoundItems="true" AutoPostBack="true">
                     </asp:ComboBox>
                  </asp:TableCell>
               </asp:TableRow>
            </asp:Table>
         </asp:TableCell>
      </asp:TableRow>
   </asp:Table>               
                     <h3>
                        Regional Synopsis/Names Missing if any:
                     </h3>
                     <asp:GridView ID="grdProgrammaster" runat="server" AutoGenerateColumns="false"  EmptyDataRowStyle-BackColor="LightGreen" EmptyDataText="No discrepancy for this language."
                        CellPadding="4" DataSourceID="sqlDSProgramDetails" AllowPaging="true" PageSize="500"
                        ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header">
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
                        <Columns>
                            
                            <asp:TemplateField HeaderText="ProgId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProgId" runat="server" Text='<%# Eval("ProgID") %>' />
                                    </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Channel">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannel" runat="server" Text='<%# Eval("channelid") %>' />
                                    </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField  HeaderText="English Programme Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProgName" runat="server" Text='<%# Eval("program name") %>' />
                                    </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField  HeaderText="English Synopsis">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%# Eval("Synopsis") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField  HeaderText="Regional ProgName">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRegProgName" runat="server" Text='<%# Eval("Regional ProgName") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField  HeaderText="Regional Synopsis">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRegSynopsis" runat="server" Text='<%# Eval("Regional Synopsis") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyEdit" runat="server" Text="Edit" />            
                                    </ItemTemplate>
                           </asp:TemplateField>
                        </Columns>
                     </asp:GridView>
                    
                     <asp:SqlDataSource ID="sqlDSProgramDetails" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                        SelectCommand="rpt_regional_name_synopsis_missing" SelectCommandType="StoredProcedure" >
                        <SelectParameters>
                           <asp:controlParameter ControlID="ddlLanguage" Direction="Input" Name="Languageid" Type="Int64" />
                           <asp:controlParameter ControlID="ddlType" Direction="Input" Name="what" Type="String" />
                        </SelectParameters>
    </asp:SqlDataSource>
    <br />
         <asp:Button ID="Excel" runat="server" Text="Export Excel"/>
   </asp:Content>