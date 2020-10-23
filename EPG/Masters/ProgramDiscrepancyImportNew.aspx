<%@ Page Title="Programme Discrepancy Central" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="ProgramDiscrepancyImportNew.aspx.vb"
    Inherits="EPG.ProgramDiscrepancyImportNew" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">

        function openWin(progid, langid, mode, progname, synopsis, regprogname, regsynopsis, language, option, onair, controlid, today, airtelDTH, episodeNo) {
            window.open("EditDiscrepancyCentral.aspx?&ProgId=" + progid + "&airtelDTH=" + airtelDTH + "&LangId=" + langid + "&Mode=" + mode + "&ProgName=" + progname + "&Synopsis=" + synopsis + "&RegProgName=" + regprogname + "&RegSynopsis=" + regsynopsis + "&Language=" + language + "&option=" + option + "&onair=" + onair + "&controlid=" + controlid + "&today=" + today + "&episodeNo=" + episodeNo, "Discrepancy Central", "width=750,height=330,toolbar=0,left=200,top=200,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
        
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h4>
                        <asp:Label ID="lbPageTitle" runat="server" Text="Programme Discrepancies Central" /></h4>
                </div>
                <div class="panel-body">
                    
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="btn-lg alert-success">
                                <b>Note :</b> Sample Format of Excel to Import
                            
                           <img src="../Images/discrepancycentralimport.JPG" class="btn-lg alert-success" width="100%" alt="" />
                           </div>
                        </div>
                    
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select
                                </label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="ProgName" Value="ProgName">Only Programme Name Required</asp:ListItem>
                                        <asp:ListItem Text="ProgNameGT80" Value="ProgNameGT80">Programme Length > 80</asp:ListItem>
                                        <asp:ListItem Text="Star" Value="Star">Star</asp:ListItem>
                                        <asp:ListItem Text="Synopsis_VHTRP" Value="Synopsis_VHTRP">Very High TRP-Programme Name & Synopsis Required</asp:ListItem>
                                        <asp:ListItem Text="Synopsis_HTRP" Value="Synopsis_HTRP">High TRP-Programme Name & Synopsis Required</asp:ListItem>
                                        <asp:ListItem Text="Synopsis_LTRP" Value="Synopsis_LTRP">Low TRP-Programme Name & Synopsis Required</asp:ListItem>
										<asp:ListItem Text="Regional_HTRP" Value="Regional_HTRP">Regional High TRP-Programme Name & Synopsis Required</asp:ListItem>
                                        <asp:ListItem Text="Regional_LTRP" Value="Regional_LTRP">Regional Low TRP-Programme Name & Synopsis Required</asp:ListItem>
                                        <asp:ListItem Text="ProgName" Value="ProgName">Only Programme Name Required</asp:ListItem>
										<asp:ListItem Text="Dhiraagu" Value="Dhiraagu">Synopsis missing for Dhiraagu Maldives</asp:ListItem>
                                        <asp:ListItem Text="GT150" Value="GT150">Synopsis length>150</asp:ListItem>
                                        <asp:ListItem Text="GT200" Value="GT200">Synopsis length>200</asp:ListItem>
                                        <asp:ListItem Text="Regional" Value="Regional">English letters in Regional</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    Language
                                </label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="10">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <div class="form-group">
                                <label>
                                    On-Air
                                </label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkOnAir" runat="server" Checked="true" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <div class="form-group">
                                <label>
                                    Airtel DTH
                                </label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkAirtelDTH" runat="server" Checked="true" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    Today/Tomorrow
                                </label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkTodayTomorrow" runat="server" Checked="False" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <button id="btnSearch" class="btn btn-info">
                                        Search</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1"
                                            runat="server" Text="*" ErrorMessage="Only .xls file Supported !" ValidationExpression="^.*\.(xls|XLS)$"
                                            ForeColor="Red" ValidationGroup="VGImportPC" />
                                        <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server"
                                            Text="*" ErrorMessage="Please select file first !" ForeColor="Red" ValidationGroup="VGImportPC" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnUpload" runat="server" Text="Import" Enabled="true" ValidationGroup="VGImportPC"
                                        UseSubmitBehavior="false" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Label ID="lbUploadError" runat="server" ForeColor="Red" Text="File Not Uploaded. Duplicate Column Name Exists in Excel!" Visible="false" />
                            <asp:ValidationSummary ID="VGSummary" runat="server" ValidationGroup="VGImportPC"  
                                CssClass="form-control alert-danger" />
                        </div>
                    </div>
                    <div class="alert alert-danger row">
                        Following programmes were not updated, rest all have been updated.</div>
                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="LightGreen"
                        EmptyDataText="No Discrepancies Found in file import." CellPadding="4" ForeColor="Black"
                        AllowPaging="True" PageSize="20" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ChannelId" HeaderText="ChannelId" SortExpression="ChannelId" />
                            <asp:BoundField DataField="ProgName" HeaderText="Program Name" SortExpression="MstProgName" />
                            <asp:BoundField DataField="LanguageId" HeaderText="Lang Id" SortExpression="ProgId" />
                            <asp:BoundField DataField="ErrorType" HeaderText="Error Type" SortExpression="ErrorType" />
                        </Columns>
                        <EmptyDataRowStyle BackColor="LightGreen"></EmptyDataRowStyle>
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
                    <div class="btn-lg alert-danger row">
                        <b>Note :</b> Channels in Pale Orange rows have Airtel FTP = False.<br />
                        Regional Synopsis/Names Missing if any:
                    </div>
                    <asp:GridView ID="grdProgrammaster" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="LightGreen"
                        EmptyDataText="No discrepancy for this language." CellPadding="4" ForeColor="Black"
                        EnableViewState="False" GridLines="Vertical" Width="100%" AllowSorting="True"
                        CssClass="table" SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <EmptyDataRowStyle BackColor="LightGreen"></EmptyDataRowStyle>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
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
                            <asp:TemplateField HeaderText="English Programme Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgName" runat="server" Text='<%# Eval("program name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="English Synopsis">
                                <ItemTemplate>
                                    <asp:Label ID="lbSynopsis" runat="server" Text='<%# Eval("Synopsis") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Regional ProgName">
                                <ItemTemplate>
                                    <asp:Label ID="lbRegProgName" runat="server" Text='<%# Eval("Regional ProgName")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RegionalSynopsis">
                                <ItemTemplate>
                                    <asp:Label ID="lbRegSynopsis" runat="server" Text='<%# Eval("Regional Synopsis") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Epi. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbEpisodeNo" runat="server" Text='<%# Eval("EpisodeNo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hyEdit" runat="server" Text="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Airtel FTP" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbAirtelFTP" runat="server" Text='<%# Eval("FTP") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="Excel" runat="server" Text="Export Excel" CssClass="btn btn-success" />
                    <asp:Button ID="btnExportWithTranslation" runat="server" Text="Export With Translation"
                        CssClass="btn btn-success" />
                    <asp:CheckBox ID="chkMMOnly" runat="server" Checked="false" Text="Marketing Message only"
                        Font-Bold="true" />
                    <asp:GridView ID="grdTranslated" runat="server" CellPadding="4" ForeColor="#333333">
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
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSProgramDetails" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="rpt_regional_name_synopsis_missing_v1" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlLanguage" Direction="Input" Name="Languageid"
                Type="Int64" />
            <asp:ControlParameter ControlID="ddlType" Direction="Input" Name="what" Type="String" />
            <asp:ControlParameter ControlID="chkOnAir" Direction="Input" PropertyName="Checked"
                Name="OnAir" Type="Boolean" />
            <asp:ControlParameter ControlID="chkAirtelDTH" Direction="Input" PropertyName="Checked"
                Name="SFTP" Type="Boolean" />
            <asp:ControlParameter ControlID="chkTodayTomorrow" Direction="Input" PropertyName="Checked"
                Name="Today" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:TextBox ID="txtProgName" runat="server" Text="" Visible="false" />
    <asp:SqlDataSource ID="sqlDSPrograms" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <script type="text/javascript">
        function getQueryStrings() {
            var assoc = {}; var decode = function (s) {
                return decodeURIComponent(s.replace(/\+/g, " "));
            }; var queryString = location.search.substring(1); var keyValues = queryString.split('&');
            for (var i in keyValues) {
                var key = keyValues[i].split('='); if (key.length > 1)
                { assoc[decode(key[0])] = decode(key[1]); }
            } // alert(assoc + "111"); return assoc;
        } var qs = getQueryStrings(); var controlid = qs["controlid"]; // alert(controlid)
        document.getElementById(controlid).focus(); document.getElementById(controlid).select();
        //MainContent_grdProgramCentral_hyEngEdit_22.focus(); //MainContent_grdProgramCentral_hyEngEdit_22.select();
    </script>
</asp:Content>
