<%@ Page Title="Channel Regional Names" Language="vb" MasterPageFile="SiteAdmin.Master"
    AutoEventWireup="false" CodeBehind="ChannelRegionalMaster.aspx.vb" Inherits="EPG.ChannelRegionalMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function AlertCatchUp(obj) {
            if (!obj.checked) {
            }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to set Catchup as True?');
                    if (doit == true)
                    { obj.checked = true; }
                    else
                    { obj.checked = false; }
                }
            }  //function Link
        }

        function AlertActive(obj) {
            if (obj.checked) {

            }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to set this Channel as In-Active?');
                    if (doit == true)
                    { obj.checked = false; }
                    else
                    { obj.checked = true; }
                }

            }  //function Link
        }

        function AlertSynopsis(obj) {
            if (obj.checked) {

            }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to set Synopsis as In-Active?');
                    if (doit == true)
                    { obj.checked = false; }
                    else
                    { obj.checked = true; }
                }

            }  //function Link
        }

       
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="panel panel-default">
        <div class="panel-heading text-center">
            <h2>
                Channel Regional Names</h2>
        </div>
        <div class="panel-body">
            <div class="col-lg-6">
                <div class="form-group">
                    <div class="input-group alert alert-success">
                        <label>
                            Company :<asp:Label ID="lbCompanyName" runat="server" Text="Company Name" />
                        </label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <label>
                            Channel
                            <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="txtChannel" FirstRowSelected="true" />
                        </label>
                        <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <label>
                            Langugage
                        </label>
                        <asp:DropDownList ID="ddlLanguage1" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <label>
                            Channel Regional Name
                            <asp:RequiredFieldValidator ID="RFVtxtRegionalName" ControlToValidate="txtRegionalName"
                                runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGRegionalName" />
                        </label>
                        <asp:TextBox ID="txtRegionalName" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <label>
                            Synopsis Needed
                        </label>
                        <asp:CheckBox ID="chkSynopsis" runat="server" CssClass="form-control" Checked="True"
                            onclick="javascript:AlertSynopsis(this);" />
                    </div>
                </div>
                <asp:TextBox ID="txtHiddenId1" runat="server" Visible="False" Text="0" />
                <asp:Button ID="btnAddRegionalName" runat="server" Text="Add" ValidationGroup="RFGRegionalName"
                    UseSubmitBehavior="false" CssClass="btn alert-info" />
                <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CssClass="btn alert-danger" />
                <asp:GridView ID="grdRegionalNames" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="table"
                    DataKeyNames="RowID" DataSourceID="sqlDSRegionalMaster" ForeColor="#333333" GridLines="Vertical"
                    Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                    SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Language Id" SortExpression="LanguageId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbLanguageId" runat="server" Text='<%# Bind("LanguageId") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Channel Regional Name" SortExpression="RegionalName"
                            Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbRegionalName" runat="server" Text='<%# Bind("RegionalName") %>'
                                    Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Language FullName" SortExpression="FullName" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbFullName" runat="server" Text='<%# Bind("FullName") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Synopsis" SortExpression="SynopsisNeeded" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbSynopsisNeeded" runat="server" Text='<%# Bind("SynopsisNeeded") %>'
                                    Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/images/delete.png" />
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
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlmstChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlmstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT LanguageId, FullName FROM mst_Language where active='1' languageid in (1,2,4,7,8) ORDER BY FullName">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSRegionalMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT a.RowID,a.synopsisneeded, b.ChannelId, a.RegionalName, c.LanguageId, c.FullName FROM mst_ChannelRegionalName a join mst_Channel b on a.ChannelId = b.ChannelId join mst_Language c on a.LanguageId=c.LanguageId where a.ChannelId=@ChannelId"
        DeleteCommand="sp_mst_ChannelRegionalName" DeleteCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtChannel" Name="ChannelId" PropertyName="Text"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
