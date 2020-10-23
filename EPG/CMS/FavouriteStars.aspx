<%@ Page Title="Favourite Stars" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="FavouriteStars.aspx.vb" Inherits="EPG.FavouriteStars" %>

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
                    Favourite Stars</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-3 col-md-3 col-sm-3">
                    <div class="form-group">
                        <label>
                            Select Language</label>
                        <div class="input-group">
                            <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" DataSourceID="sqlDSLanguage"
                                DataTextField="FullName" AutoPostBack="true" DataValueField="languageid" />
                                <span class="input-group-addon">
                                     <asp:SqlDataSource ID="sqlDSLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select languageid,fullname from mst_language where active=1 order by fullname">
                                    </asp:SqlDataSource>
                                </span>
                        </div>
                    </div>
                </div>
               
                <div class="col-lg-3 col-md-3 col-sm-3">
                    <div class="form-group">
                        <label>
                            Select Celebrity</label>
                        <div class="input-group">
                            <asp:DropDownList ID="ddlCelebrity" runat="server" CssClass="form-control" DataSourceID="sqlDSCelebrity"
                                DataTextField="Name" DataValueField="rowid" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="sqlDSCelebrity" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select rowid,name from tmdb_celebrity where verified=1 and languageid=@languageid and [name]<>'' order by 2">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlLanguage" Name="languageid" PropertyName="SelectedValue"
                                                Direction="Input" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </span>
                        </div>
                    </div>
                </div>
                
                <div class="col-lg-3 col-md-3 col-sm-3">
                    <div class="form-group">
                        <label>
                            &nbsp;</label>
                        <div class="input-group">
                            <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-info" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                            <asp:Label ID="lbRowId" runat="server" Text="0" Visible="false" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3">
                    &nbsp;
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                    <asp:GridView ID="grdFavouriteStars" runat="server" AutoGenerateColumns="False" CssClass="table"
                        DataSourceID="sqlDSFavouriteStars" AllowPaging="True" AllowSorting="True" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" PageSize="20" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label runat="server" ID="lbRowid" Text='<%# Bind("Rowid") %>' visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Language" SortExpression="Fullname">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbLanguageid" Text='<%# Bind("Languageid") %>' Visible="false" />
                                    <asp:Label runat="server" ID="lbLanguage" Text='<%# Bind("Fullname") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Celebrity" SortExpression="Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbCelebrityId" Text='<%# Bind("CelebrityId") %>' Visible="false" />
                                    <asp:Label runat="server" ID="lbCelebrity" Text='<%# Bind("Name") %>' />
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
                    <asp:SqlDataSource ID="sqlDSFavouriteStars" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                        SelectCommand="select a.*, b.FullName,c.Name from mst_celebrity_favourites a join mst_language b on a.languageid=b.LanguageID join tmdb_celebrity c on a.celebrityid=c.tmdbCelebrityId where a.languageid=@languageid order by 2">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlLanguage" Name="languageid" PropertyName="SelectedValue"
                                Direction="Input" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
