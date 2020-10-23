<%@ Page Language="vb" Title="Translated Files" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="TranslatedFiles.aspx.vb" Inherits="EPG.TranslatedFiles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Translated Files</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-6 col-sm-12 col-md-offset-3">
                    <asp:GridView ID="grd" runat="server" CssClass="table" CellPadding="4" DataKeyNames="Id"
                        AutoGenerateColumns="false" DataSourceID="sqlDS" ForeColor="Black" GridLines="Vertical"
                        Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="20"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#" SortExpression="RowId" Visible="True">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbID" runat="server" Text='<%# Bind("id") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="fullname" HeaderText="Language" />
                            <asp:BoundField DataField="dateadded1" HeaderText="Added" />
                            <asp:BoundField DataField="addedby" HeaderText="by" />
                            <asp:TemplateField HeaderText="File Received">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkReceived" runat="server" Checked='<%# Bind("fileReceived") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="updated at" HeaderText="Updated at" />
                            <asp:BoundField DataField="updated by" HeaderText="Updated by" />
                            <asp:TemplateField HeaderText="File Received">
                                <ItemTemplate>
                                    <asp:Button ID="btnReceived" runat="server" CommandName="received" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'
                                        Text="Received" CssClass="btn alert-info btn-xs" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                        SelectCommand="select top 20 a.id,b.fullname,a.dateadded,convert(varchar,a.dateadded,106) + ' ' + convert(varchar,a.dateadded,108) dateadded1 ,a.addedby,isnull(a.fileReceived,0) fileReceived,convert(varchar,a.filereceivedat,106) + ' ' + convert(varchar,a.filereceivedat,108)  [Updated At],a.filereceivedupdatedby [Updated By] from mst_translationfiles a join mst_language b on a.languageid=b.languageid order by dateadded desc" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
