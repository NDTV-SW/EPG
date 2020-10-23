<%@ Page Title="Programme Discrepancy Central" Language="vb" MasterPageFile="~/Site.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="ProgramDiscrepancyCentralImport.aspx.vb"
    Inherits="EPG.ProgramDiscrepancyCentralImport" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        $('#ddlLanguage').bind("keyup", function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                javascript: __doPostBack('ddlLanguage', '')
            }
        });

        function openWin(progid, langid, mode, progname, synopsis, regprogname, regsynopsis, language, option, onair, controlid, today, airtelDTH, episodeNo) {
            window.open("EditDiscrepancyCentral.aspx?&ProgId=" + progid + "&airtelDTH=" + airtelDTH + "&LangId=" + langid + "&Mode=" + mode + "&ProgName=" + progname + "&Synopsis=" + synopsis + "&RegProgName=" + regprogname + "&RegSynopsis=" + regsynopsis + "&Language=" + language + "&option=" + option + "&onair=" + onair + "&controlid=" + controlid + "&today=" + today + "&episodeNo=" + episodeNo, "Discrepancy Central", "width=750,height=330,toolbar=0,left=200,top=200,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
        
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Table ID="Table1" runat="server" GridLines="both" BorderWidth="2" CellPadding="5"
        CellSpacing="0" Width="90%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="50%">
                <h2>
                    <asp:Label ID="lbPageTitle" runat="server" Text="Programme Discrepancies Central" />
                </h2>
                <asp:ScriptManager ID="ScriptManager2" runat="server">
                </asp:ScriptManager>
                <asp:Table ID="tblProgramGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5"
                    CellSpacing="0" Width="95%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="10" HorizontalAlign="Center">
                            <br />
                            <asp:Image ID="discrepancycentralimport" ImageUrl="~/Images/discrepancycentralimport.JPG"
                                runat="server" />
                            <br />
                            <span style="color: Red">Sample Format of Excel to Import</span><br />
                            <br />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top" Width="15%">
                     Select
                     <br />
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left" Width="20%">
                            <asp:DropDownList ID="ddlType" runat="server" AutoCompleteMode="SuggestAppend" Width="250px"
                                DropDownStyle="DropDownList" ItemInsertLocation="Append" AppendDataBoundItems="true"
                                AutoPostBack="true">
                                <asp:ListItem Text="ProgName" Value="ProgName">Only Programme Name Required</asp:ListItem>
                                <asp:ListItem Text="Synopsis_HTRP" Value="Synopsis_HTRP">High TRP-Programme Name & Synopsis Required</asp:ListItem>
                                <asp:ListItem Text="Synopsis_LTRP" Value="Synopsis_LTRP">Low TRP-Programme Name & Synopsis Required</asp:ListItem>
                                <asp:ListItem Text="ProgName" Value="ProgName">Only Programme Name Required</asp:ListItem>
                                <asp:ListItem Text="GT150" Value="GT150">Synopsis length>150</asp:ListItem>
                                <asp:ListItem Text="GT200" Value="GT200">Synopsis length>200</asp:ListItem>
                                <asp:ListItem Text="Regional" Value="Regional">English letters in Regional</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </asp:TableCell>
                        <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top" Width="15%">
                     Language
                     <br />
                        </asp:TableHeaderCell>
                        <asp:TableCell HorizontalAlign="Left" Width="20%">
                            <asp:DropDownList ID="ddlLanguage" runat="server" AutoCompleteMode="SuggestAppend"
                                DropDownStyle="DropDownList" ItemInsertLocation="Append" AppendDataBoundItems="true"
                                AutoPostBack="true">
                                <asp:ListItem Value="10">Select</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </asp:TableCell>
                        <asp:TableHeaderCell Width="10%">
                    On-Air
                        </asp:TableHeaderCell>
                        <asp:TableCell Width="10%">
                            <asp:CheckBox ID="chkOnAir" runat="server" Checked="true" AutoPostBack="true" />
                        </asp:TableCell>
                        <asp:TableHeaderCell Width="10%">
                    Airtel DTH
                        </asp:TableHeaderCell>
                        <asp:TableCell Width="10%">
                            <asp:CheckBox ID="chkAirtelDTH" runat="server" Checked="true" AutoPostBack="true" />
                        </asp:TableCell>
                        <asp:TableHeaderCell Width="10%">
                    Today/Tomorrow
                        </asp:TableHeaderCell>
                        <asp:TableCell Width="10%">
                            <asp:CheckBox ID="chkTodayTomorrow" runat="server" Checked="False" AutoPostBack="true" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top" ColumnSpan="4">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <br />
                            <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1"
                                runat="server" ErrorMessage="Only .xls file Supported !" ValidationExpression="^.*\.(xls|XLS)$"
                                ForeColor="Red" ValidationGroup="VGImportPC" />
                            <br />
                            <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server"
                                ErrorMessage="Please select file first !" ForeColor="Red" ValidationGroup="VGImportPC" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Width="25%" VerticalAlign="Top" ColumnSpan="6">
                            <asp:Button ID="btnUpload" runat="server" Text="IMPORT" Enabled="true" ValidationGroup="VGImportPC"
                                UseSubmitBehavior="false" />
                            <br />
                            <span style="color: Red">Choose Language first to Import</span>
                            <asp:Label ID="lbUploadError" runat="server" ForeColor="Red" Text="File Not Uploaded. Duplicate Column Name Exists in Excel!"
                                Visible="false" />
                            <br />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell ColumnSpan="7">
                <span style="color: Red">Following programmes were not updated, rest all have been updated.</span>
                <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="false" EmptyDataRowStyle-BackColor="LightGreen"
                    EmptyDataText="No Discrepancies Found in file import." CellPadding="4" ForeColor="#333333"
                    GridLines="Vertical" Width="80%" AllowPaging="true" PageSize="20">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <%--<asp:BoundField DataField="Rowid" HeaderText="RowId" SortExpression="ExcelProgName" />--%>
                        <asp:BoundField DataField="ChannelId" HeaderText="ChannelId" SortExpression="ChannelId" />
                        <asp:BoundField DataField="ProgName" HeaderText="Program Name" SortExpression="MstProgName" />
                        <asp:BoundField DataField="LanguageId" HeaderText="Lang Id" SortExpression="ProgId" />
                        <asp:BoundField DataField="ErrorType" HeaderText="Error Type" SortExpression="ErrorType" />
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
                <asp:SqlDataSource ID="sqlDSPrograms" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
                </asp:SqlDataSource>
                
            </asp:TableCell></asp:TableRow></asp:Table><br class="style2" />
    <span class="style1">Note:-Channels in Pale Orange rows have Airtel FTP = False.</span> <h3>
        Regional Synopsis/Names Missing if any: </h3>
        <h4><font color="red"><asp:Label ID="lbCount" runat="server" /></font></h4>
        
        <asp:GridView ID="grdProgrammaster" runat="server" AutoGenerateColumns="false" EmptyDataRowStyle-BackColor="LightGreen"
        EmptyDataText="No discrepancy for this language." CellPadding="4" ForeColor="#333333"
        EnableViewState="false" GridLines="Vertical" Width="100%" AllowSorting="True"
        SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header">
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
                    <asp:Label ID="lbRegProgName" runat="server" Text='<%# Eval("Regional ProgName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Regional Synopsis">
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
    <br />
    <asp:Button ID="Excel" runat="server" Text="Export Excel" />
    <asp:Button ID="btnExportWithTranslation" runat="server" Text="Export With Translation" />
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
