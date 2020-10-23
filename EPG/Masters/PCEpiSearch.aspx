<%@ Page Title="Program Central" Language="vb" MasterPageFile="~/SiteEPGBootstrap.Master"
    AutoEventWireup="false" CodeBehind="PCEpiSearch.aspx.vb" Inherits="EPG.PCEpiSearch"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">


        function openWin(progid, langid, controlid, index, episodeNo,frompage) {
            window.open("EditProgNameEpisodic.aspx?progid=" + progid + "&langid=" + langid + "&controlid=" + controlid + "&index=" + index + "&episodeNo=" + episodeNo + "&frompage=" + frompage, "Edit Programme", "width=640,height=480,toolbar=0,left=250,top=100,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Programme Central Episodic Search</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-2">
                    <div class="form-group">
                        <label>
                            Search Program</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" AutoPostBack="true"
                                Text="Search Program" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtSearch" runat="server" ControlToValidate="txtSearch"
                                    Text="*" ForeColor="Red" />
                            </span>
                        </div>
                    </div>
                </div>
                
                <asp:GridView ID="grdProgramCentral" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="Red"
                    EmptyDataText="No record Found for the searched program" CellPadding="4" DataKeyNames="Engprog"
                    CssClass="table" DataSourceID="sqlDSProgramCentral" ForeColor="#333333" GridLines="Both"
                    Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                    SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Channel" SortExpression="channelid" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbchannelid" runat="server" Text='<%# Bind("channelid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Programme" SortExpression="ProgName" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbProgName" runat="server" Text='<%# Bind("ProgName") %>' />
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
                        <%--<asp:TemplateField HeaderText="Hindi Programme" SortExpression="HinProg">
                            <ItemTemplate>
                                <asp:Label ID="lbHinProg" runat="server" Text='<%# Bind("HinProg") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hindi Synopsis" SortExpression="hinsyn">
                            <ItemTemplate>
                                <asp:Label ID="lbHinSyn" runat="server" Text='<%# Bind("hinsyn") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyHinEdit" runat="server" Text="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tamil Programme" SortExpression="TamProg">
                            <ItemTemplate>
                                <asp:Label ID="lbTamProg" runat="server" Text='<%# Bind("TamProg") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tamil Synopsis" SortExpression="tamsyn">
                            <ItemTemplate>
                                <asp:Label ID="lbTamSyn" runat="server" Text='<%# Bind("tamsyn") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyTamEdit" runat="server" Text="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marathi Programme" SortExpression="MarProg" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbMarProg" runat="server" Text='<%# Bind("MarProg") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marathi Synopsis" SortExpression="marsyn" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbMarSyn" runat="server" Text='<%# Bind("marsyn") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="true">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyMarEdit" runat="server" Text="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telugu Programme" SortExpression="telProg" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbTelProg" runat="server" Text='<%# Bind("telProg") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telugu Synopsis" SortExpression="telsyn" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbTelSyn" runat="server" Text='<%# Bind("telsyn") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="true">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyTelEdit" runat="server" Text="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="ProgId" SortExpression="ProgId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbProgId" runat="server" Text='<%# Bind("ProgId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EngLang" SortExpression="EngLang" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbEngLang" runat="server" Text='<%# Bind("EngLang") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="HinLang" SortExpression="HinLang" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbHinLang" runat="server" Text='<%# Bind("HinLang") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TamLang" SortExpression="TamLang" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbTamLang" runat="server" Text='<%# Bind("TamLang") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MarLang" SortExpression="MarLang" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbMarLang" runat="server" Text='<%# Bind("MarLang") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TelLang" SortExpression="TelLang" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbTelLang" runat="server" Text='<%# Bind("TelLang") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="ColorCode" SortExpression="ColorCode" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbColorCode" runat="server" Text='<%# Bind("ColorCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Epi. no." SortExpression="episodeNo" Visible="True">
                            <ItemTemplate>
                                <asp:Label ID="lbEpisodeNo" runat="server" Text='<%# Bind("episodeNo") %>' />
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
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSProgramCentral" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="sp_program_descriptions_edit_v1_episode_search" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" Direction="Input" Name="progname" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Button ID="btnSetFocus" Text="Focus" runat="server" Visible="false" />
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
