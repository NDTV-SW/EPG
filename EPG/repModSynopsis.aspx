<%@ Page Title="Synopsis Modified Report" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master" EnableViewState="true" AutoEventWireup="false" CodeBehind="repModSynopsis.aspx.vb" Inherits="EPG.repModSynopsis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openWin(channelid, langid, language, onair, repdate, hour, progid, controlid, episodeno) {
            window.open("EditProgSynopModified.aspx?channelid=" + channelid + "&langid=" + langid + "&language=" + language + "&onair=" + onair + "&repdate=" + repdate + "&hour=" + hour + "&progid=" + progid + "&controlid=" + controlid + "&episodeno=" + episodeno, "Edit Programme", "width=750,height=350,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>Synopsis Modified Report</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Select Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannelId" runat="server" DataSourceID="SqlDsChannelMaster"
                                        DataValueField="ChannelId" DataTextField="ChannelId" CssClass="form-control">
                                    </asp:DropDownList>
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="select * from mst_Channel where active=1 and SendEPG=1 ORDER BY ChannelId"></asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Select Language</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" DataSourceID="SqlDSLanguage"
                                        DataValueField="LanguageId" DataTextField="FullName" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="SqlDSLanguage" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="SELECT LanguageId, FullName FROM mst_Language where active='1' ORDER BY FullName"></asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    OnAir ?</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkOnAir" runat="server" Checked="true" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Select Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Enabled="true" ValidationGroup="RFGViewEPG" />
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgDateCalendar" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Select Hour</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlHour" runat="server" CssClass="form-control">
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
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Episodic?</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkEpisodic" runat="server" Checked="false" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                        
                       </div>
                    
                
                                <asp:GridView ID="grdRepModSynopsis" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                    CellPadding="4" ForeColor="Black" DataSourceID="sqlDSModSynopsis" GridLines="Vertical" CssClass="table" AllowSorting="True"
                                    SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                    SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="20" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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
                                                <asp:HyperLink ID="hyEdit" runat="server" Text="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:CommandField ShowEditButton="true" />--%>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#F7F7DE" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="sqlDSModSynopsis" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                    SelectCommand="rpt_modified_synopsis" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlChannelId" Direction="Input" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter ControlID="ddlLanguage" Direction="Input" Name="languageId" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter ControlID="chkOnair" Direction="Input" Name="onair" PropertyName="Checked" Type="Boolean" />
                                        <asp:ControlParameter ControlID="chkEpisodic" Direction="Input" Name="episodic" PropertyName="Checked" Type="Boolean" />
                                        <asp:ControlParameter ControlID="txtDate" Direction="Input" Name="date" PropertyName="Text" Type="String" />
                                        <asp:ControlParameter ControlID="ddlHour" Direction="Input" Name="hour" PropertyName="SelectedValue" Type="Int16" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                           </div>
                </div>
            </div>
        
   
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
