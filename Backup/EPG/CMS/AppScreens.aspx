<%@ Page Title="App Screens" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="AppScreens.aspx.vb" Inherits="EPG.AppScreens" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="http://epgops.ndtv.com/lightbox.css" rel="stylesheet" type="text/css" />
    <script src="http://epgops.ndtv.com/lightbox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    App Screens</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-3 col-md-3 col-sm-3">
                    <div class="form-group">
                        <label>Screen ID</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtScreenId" runat="server" CssClass="form-control" />
                            <span class="input-group-addon"></span>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3">
                    <div class="form-group">
                        <label >Title</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                            <span class="input-group-addon"></span>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3">
                     <div class="form-group">
                        <label>&nbsp;</label>
                        <div class="input-group">
                            <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-info" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                            <%--<asp:Label ID="lbScreenId" runat="server" Text="0" />--%>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3">
                    &nbsp;
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                    <asp:GridView ID="grdAppScreens" runat="server" AutoGenerateColumns="False" CssClass="table"
                        DataSourceID="sqlDSAppScreens" AllowPaging="True" AllowSorting="True" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" PageSize="20" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ScreenID" SortExpression="ScreenID">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbScreenID" Text='<%# Bind("ScreenID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbTitle" Text='<%# Bind("Title") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" />
                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Images/delete.png" />
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
                    <asp:SqlDataSource ID="sqlDSAppScreens" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                        SelectCommand="select * from app_mst_screenids order by 2"></asp:SqlDataSource>
                </div>
            </div>
    </div>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
