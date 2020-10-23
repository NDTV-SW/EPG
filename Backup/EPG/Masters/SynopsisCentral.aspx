<%@ Page Title="Synopsis Central" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="SynopsisCentral.aspx.vb" Inherits="EPG.SynopsisCentral" MaintainScrollPositionOnPostback ="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
       $(document).ready(function () {
           $('#ddlChannelName').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   $('#txtSynopsis').focus();
               }
           });
       });

       function openWin(channelindex,channelid, search, progid, langid, mode, language, progname, controlid, episodeNo) {
           window.open("EditSynopsisCentral.aspx?channelindex=" + channelindex + "&channelid=" + channelid + "&search=" + search + "&progid=" + progid + "&langid=" + langid + "&mode=" + mode + "&language=" + language + "&progname=" + progname + "&controlid=" + controlid + "&episodeNo=" + episodeNo, "Edit Programme", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
       }

    </script>
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
   <%--<asp:UpdatePanel ID="updProgramMaster" runat="server">
      <ContentTemplate>--%>
   <asp:Table ID="Table1" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0"  Width="95%">
      <asp:TableRow>
         <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top" Width="50%">
            <h2>Synopsis Central Search</h2>
            <asp:Table ID="tblProgramMaster" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="60%">
               <asp:TableRow>
               <asp:TableHeaderCell HorizontalAlign="Right">
                     Select Channel
                  </asp:TableHeaderCell>
                  <asp:TableCell>
                     <asp:ComboBox ID="ddlChannelName" runat="server"  Width="150px"  AutoCompleteMode="SuggestAppend"
                       DropDownStyle="DropDownList" ItemInsertLocation="Append"  AutoPostBack="False"
                       DataSourceID="SqlDsChannelMaster" DataTextField="ChannelID" 
                       DataValueField="ChannelID">
                    </asp:ComboBox>
                    <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
                       ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                       SelectCommand="SELECT ChannelID FROM mst_Channel where active='1' ORDER BY ChannelID">
                    </asp:SqlDataSource>
                  </asp:TableCell>
                  <asp:TableHeaderCell HorizontalAlign="Right">
                     Select Language
                  </asp:TableHeaderCell>
                  <asp:TableCell>
                     <asp:DropDownList ID="ddlLanguage" runat="server"   AutoCompleteMode="SuggestAppend"
                        DropDownStyle="DropDownList" ItemInsertLocation="Append"   AppendDataBoundItems="true" AutoPostBack="False">
                        <%--<asp:ListItem Value="10">Select</asp:ListItem>--%>
                     </asp:DropDownList>
                  </asp:TableCell>

                  <asp:TableHeaderCell HorizontalAlign="Right">
                     Search Synopsis
                  </asp:TableHeaderCell>
                  <asp:TableCell>
                     <asp:TextBox ID="txtSynopsis" runat="server" Width="150px" />
                  </asp:TableCell>

                  <asp:TableCell>
                     <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" />
                  </asp:TableCell>

               </asp:TableRow>
            </asp:Table>

            <asp:Table ID="tblProgramGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0">
               <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                     <asp:GridView ID="grdProgramCentral" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-ForeColor="Red" EmptyDataText="No record Found."
                        CellPadding="4" 
                        ForeColor="#333333" GridLines="Both" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Channel" SortExpression="ChannelId" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Programme" SortExpression="ProgName" Visible="true">
                                    <ItemTemplate>
                                       <asp:Label ID="lbProgName" runat="server" Text='<%# Bind("ProgName") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="English Programme" SortExpression="EngProg">
                                    <ItemTemplate>
                                       <asp:Label ID="lbEngProg" runat="server" Text='<%# Bind("EngProg") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField HeaderText="English Synopsis" SortExpression="Engsyn">
                                    <ItemTemplate>
                                       <asp:Label ID="lbEngsyn" runat="server" Text='<%# Bind("Engsyn") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyEngEdit" runat="server" Text="Edit" />            
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Hindi Programme" SortExpression="HinProg">
                                    <ItemTemplate>
                                       <asp:Label ID="lbHinProg" runat="server" Text='<%# Bind("HinProg") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Hindi Synopsis" SortExpression="hinsyn">
                                    <ItemTemplate>
                                       <asp:Label ID="lbHinSyn" runat="server" Text='<%# Bind("hinsyn") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyHinEdit" runat="server" Text="Edit" />            
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Tamil Programme" SortExpression="TamProg">
                                    <ItemTemplate>
                                       <asp:Label ID="lbTamProg" runat="server" Text='<%# Bind("TamProg") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Tamil Synopsis" SortExpression="tamsyn" >
                                    <ItemTemplate>
                                       <asp:Label ID="lbTamSyn" runat="server" Text='<%# Bind("tamsyn") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyTamEdit" runat="server" Text="Edit" />            
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Marathi Programme" SortExpression="MarProg" >
                                    <ItemTemplate>
                                       <asp:Label ID="lbMarProg" runat="server" Text='<%# Bind("MarProg") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Marathi Synopsis" SortExpression="marsyn">
                                    <ItemTemplate>
                                       <asp:Label ID="lbMarSyn" runat="server" Text='<%# Bind("marsyn") %>' />
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyMarEdit" runat="server" Text="Edit" />            
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Telugu Programme" SortExpression="telProg">
                                    <ItemTemplate>
                                       <asp:Label ID="lbTelProg" runat="server" Text='<%# Bind("telProg") %>' Visible="true" />
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Telugu Synopsis" SortExpression="telsyn">
                                    <ItemTemplate>
                                       <asp:Label ID="lbTelSyn" runat="server" Text='<%# Bind("telsyn") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyTelEdit" runat="server" Text="Edit" />            
                                    </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="ProgId" SortExpression="ProgId" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbProgId" runat="server" Text='<%# Bind("ProgId") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField HeaderText="EngLang" SortExpression="EngLang" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbEngLang" runat="server" Text='<%# Bind("EngLang") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField HeaderText="HinLang" SortExpression="HinLang" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbHinLang" runat="server" Text='<%# Bind("HinLang") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField HeaderText="TamLang" SortExpression="TamLang" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbTamLang" runat="server" Text='<%# Bind("TamLang") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField HeaderText="MarLang" SortExpression="MarLang" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbMarLang" runat="server" Text='<%# Bind("MarLang") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField> 

                           <asp:TemplateField HeaderText="TelLang" SortExpression="TelLang" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbTelLang" runat="server" Text='<%# Bind("TelLang") %>'/>
                                    </ItemTemplate>
                           </asp:TemplateField>   
                           <asp:TemplateField HeaderText="Epi.No." SortExpression="EpisodeNo" Visible="True">
                                    <ItemTemplate>
                                       <asp:Label ID="lbEpisodeNo" runat="server" Text='<%# Bind("EpisodeNo") %>'/>
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
                     <asp:SqlDataSource ID="sqlDSProgramCentral" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                        SelectCommand="sp_synopsis_search" SelectCommandType="StoredProcedure" >
                        <SelectParameters>
                              <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelID" PropertyName="SelectedValue" Type="String" />
                              <asp:controlParameter ControlID="ddlLanguage" Direction="Input" Name="Languageid" Type="Int64" />
                              <asp:ControlParameter ControlID="txtSynopsis" Name="synopsis" PropertyName="Text" Type="String" />
                        </SelectParameters>
                     </asp:SqlDataSource>
                  </asp:TableCell>
               </asp:TableRow>
            </asp:Table>
         </asp:TableCell>
      </asp:TableRow>
   </asp:Table>
   <%--<asp:Button ID="btnSetFocus" Text="Focus" runat="server" />--%>
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
