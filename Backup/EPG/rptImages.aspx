<%@ Page Title="Programme Images Report" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="rptImages.aspx.vb" Inherits="EPG.rptImages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Programme Images Report</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Show JPR Channels Only</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkJPRonly" runat="server" AutoPostBack="true" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdImagesReport" runat="server" CssClass="table" AutoGenerateColumns="True"
                    ShowFooter="True" DataSourceID="sqlDSImagesReport" CellPadding="4" EmptyDataText="No record found !"
                    ForeColor="#333333" GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                    SortedDescendingHeaderStyle-CssClass="sortdesc-header" ShowHeader="True" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                    PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Font-Bold="true" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.no.">
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSImagesReport" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="rptImages" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter Name="JPROnly" ControlID="chkJPRonly" DbType="Boolean" PropertyName="Checked" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
