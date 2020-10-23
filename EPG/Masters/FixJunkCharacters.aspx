<%@ Page Title="Fix Junk Characters" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="FixJunkCharacters.aspx.vb" Inherits="EPG.FixJunkCharacters" MaintainScrollPositionOnPostback ="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
       function openWin(channelid, progid, langid, mode, language, progname, controlid) {
           window.open("EditJunkData.aspx?channelid=" + channelid + "&progid=" + progid + "&langid=" + langid + "&mode=" + mode + "&language=" + language + "&progname=" + progname + "&controlid=" + controlid, "Edit Programme", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
       }

    </script>
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

            

    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
   <%--<asp:UpdatePanel ID="updProgramMaster" runat="server">
      <ContentTemplate>--%>
   
           <span style="color:White">'%[^a-zA-Z0-9!?.'''',+&:/\_ ]%' &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sp_get_junk_data</span>
            <asp:Table ID="tblJunkData" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0">
               <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                     <fieldset><legend>Fix Junk Characters</legend>
                     <asp:GridView ID="grdJunkData" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="Red" EmptyDataText="No record Found for this Channel"
                        CellPadding="4" DataKeyNames="ProgName" DataSourceID="sqlDSProgramCentral" 
                        ForeColor="#333333" GridLines="Both" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Master Programme" SortExpression="mstProg" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbmstProg" runat="server" Text='<%# Bind("mstProg") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Programme" SortExpression="ProgName" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbProgName" runat="server" Text='<%# Bind("ProgName") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Synopsis" SortExpression="Synopsis">
                                    <ItemTemplate>
                                       <asp:Label ID="lbSynopsis" runat="server" Text='<%# Bind("Synopsis") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyEdit" runat="server" Text="Edit" />            
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="ProgId" SortExpression="ProgId" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbProgId" runat="server" Text='<%# Bind("ProgId") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField HeaderText="LanguageId" SortExpression="LanguageId" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbLanguageId" runat="server" Text='<%# Bind("LanguageId") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField HeaderText="ColorCode" SortExpression="ColorCode" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbColorCode" runat="server" Text='<%# Bind("ColorCode") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                     </asp:GridView>
                     </fieldset>
                     <asp:SqlDataSource ID="sqlDSProgramCentral" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                        SelectCommand="sp_get_junk_data" SelectCommandType="StoredProcedure" >
                     </asp:SqlDataSource>
                  </asp:TableCell>
               </asp:TableRow>
            </asp:Table>
   
   <%--    </ContentTemplate></asp:UpdatePanel>--%>
   <script type="text/javascript">
       function getQueryStrings() {
           var assoc = {};
           var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
           var queryString = location.search.substring(1);
           var keyValues = queryString.split('&');

           for (var i in keyValues) {
               var key = keyValues[i].split('=');
               if (key.length > 1) {
                   assoc[decode(key[0])] = decode(key[1]);
               }
           }
          // alert(assoc + "111");
            return assoc;
       }
       
       var qs = getQueryStrings();
       var controlid = qs["controlid"];
       // alert(controlid)
       document.getElementById(controlid).focus();
       document.getElementById(controlid).select();
       //MainContent_grdProgramCentral_hyEngEdit_22.focus();
       //MainContent_grdProgramCentral_hyEngEdit_22.select();
  </script>
</asp:Content>
