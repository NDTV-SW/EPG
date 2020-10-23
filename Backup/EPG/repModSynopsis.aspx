<%@ Page Title="Synopsis Modified Report" Language="vb" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="false" CodeBehind="repModSynopsis.aspx.vb" Inherits="EPG.repModSynopsis" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openWin(channelid, langid,language, onair, repdate, hour, progid, controlid, episodeno) {
            window.open("EditProgSynopModified.aspx?channelid=" + channelid + "&langid=" + langid + "&language=" + language + "&onair=" + onair + "&repdate=" + repdate + "&hour=" + hour + "&progid=" + progid + "&controlid=" + controlid + "&episodeno=" + episodeno, "Edit Programme", "width=750,height=350,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
   <asp:Table ID="Table1" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
      <asp:TableRow>
         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
            <h2>
               Synopsis Modified Report
            </h2>
            <asp:Table ID="tblProgramGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                <asp:TableRow>
                <asp:TableHeaderCell>
                        Select Channel
                    </asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:ComboBox ID="ddlChannelId" runat="server"   AutoCompleteMode="SuggestAppend" DataSourceID="SqlDsChannelMaster"
                           DataValueField="ChannelId" DataTextField="ChannelId" DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="true" Width="180px">
                        </asp:ComboBox>
                        <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                           SelectCommand="select * from mst_Channel where active=1 and SendEPG=1 ORDER BY ChannelId">
                        </asp:SqlDataSource>
                    </asp:TableCell>
                    <asp:TableHeaderCell>
                        Select Language
                    </asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlLanguage" runat="server" DataSourceID="SqlDSLanguage"
                             DataValueField="LanguageId" DataTextField="FullName" AutoPostBack="true" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDSLanguage" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                            SelectCommand="SELECT LanguageId, FullName FROM mst_Language where active='1' ORDER BY FullName">                                
                        </asp:SqlDataSource>
                    </asp:TableCell>
                    <asp:TableHeaderCell>
                        OnAir / OffAir
                    </asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:CheckBox ID="chkOnAir" runat="server" Checked="true" AutoPostBack="true" />
                    </asp:TableCell>
                    
                </asp:TableRow>
                <asp:TableRow>
                    
                    <asp:TableHeaderCell>
                        Select Date
                    </asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtDate" runat="server" Width="150px" Enabled="true" ValidationGroup="RFGViewEPG"></asp:TextBox>
                        <asp:Image ID="imgDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                        <asp:CalendarExtender ID="CE_txtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgDateCalendar" />
                        <asp:TextBoxWatermarkExtender TargetControlID="txtDate" ID="ExttxtDate" WatermarkText="Select Start Date" runat="server"></asp:TextBoxWatermarkExtender>
                    </asp:TableCell>
                    <asp:TableHeaderCell>
                        Select Hour
                    </asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlHour" runat="server" AutoPostBack="true">
                            <asp:ListItem Value="0">0</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableHeaderCell>
                        Episodic Shows
                    </asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:CheckBox ID="chkEpisodic" runat="server" Checked="false" AutoPostBack="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">
                        <asp:Button ID="btnView" runat="server" Text="View" Font-Bold="True" />
                    </asp:TableCell>
                </asp:TableRow>
               <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="6">
                  
                     <asp:GridView ID="grdRepModSynopsis" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                        CellPadding="4" ForeColor="#333333" DataSourceID="sqlDSModSynopsis" GridLines="Vertical" Width="100%" AllowSorting="True" 
                        SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                           <asp:TemplateField HeaderText="Channel">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannelID" runat="server" Text='<%#Eval("ChannelId") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Program Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgName" runat="server" Text='<%#Eval("Program Name") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Synopsis">
                                <ItemTemplate>
                                    <asp:Label ID="lbSynopsis" runat="server" Text='<%#Eval("Synopsis") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Regional ProgName">
                                <ItemTemplate>
                                    <asp:Label ID="lbRegProgName" runat="server" Text='<%#Eval("Regional ProgName") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Regional Synopsis">
                                <ItemTemplate>
                                    <asp:Label ID="lbRegSynopsis" runat="server" Text='<%#Eval("Regional Synopsis") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="Epi. no">
                                <ItemTemplate>
                                    <asp:Label ID="lbEpisodeNo" runat="server" Text='<%#Eval("Episodeno") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="ProgId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgId" runat="server" Text='<%#Eval("ProgId") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Action User">
                                <ItemTemplate>
                                    <asp:Label ID="lbActionUser" runat="server" Text='<%#Eval("ActionUser") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Action Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbActionDate" runat="server" Text='<%#Eval("ActionDate") %>' />
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Hyperlink ID="hyEdit" runat="server" Text="Edit" />
                                </ItemTemplate>
                           </asp:TemplateField>
                           
                           <%--<asp:CommandField ShowEditButton="true" />--%> 
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#D7E2F7" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                     </asp:GridView>
                     <asp:SqlDataSource ID="sqlDSModSynopsis" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                        SelectCommand="rpt_modified_synopsis" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                           <asp:controlParameter ControlID="ddlChannelId" Direction="Input" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                           <asp:controlParameter ControlID="ddlLanguage" Direction="Input" Name="languageId" PropertyName="SelectedValue" Type="String" />
                           <asp:controlParameter ControlID="chkOnair" Direction="Input" Name="onair" PropertyName="Checked" Type="Boolean" />
                           <asp:controlParameter ControlID="chkEpisodic" Direction="Input" Name="episodic" PropertyName="Checked" Type="Boolean" />
                           <asp:controlParameter ControlID="txtDate" Direction="Input" Name="date" PropertyName="Text" Type="String" />
                           <asp:controlParameter ControlID="ddlHour" Direction="Input" Name="hour" PropertyName="SelectedValue" Type="Int16" />
                        </SelectParameters>
                        
                     </asp:SqlDataSource>
                  </asp:TableCell>
               </asp:TableRow>
            </asp:Table>
         </asp:TableCell>
      </asp:TableRow>
   </asp:Table>
        
   
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
 