<%@ Page Title="Celebrity Regional" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="CelebrityRegional.aspx.vb" Inherits="EPG.CelebrityRegional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Celebrity Regional</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select Celebrity</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlCelebrity" runat="server" DataTextField="name" DataValueField="rowid"
                                        DataSourceID="sqlDSCelebrity" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="sqlDSCelebrity" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="select name,rowid from tmdb_celebrity where verified=1 order by 1">
                                        </asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Regional Name</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtRegName" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select Language</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="fullname" DataValueField="languageid"
                                        DataSourceID="sqlDSLanguage" CssClass="form-control"/>
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="SqlDSLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="select LanguageID,FullName from mst_language where active=1">
                                        </asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Place of Birth</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtPlaceOfBirth" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Bio-graphy</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtBioGraphy" runat="server" TextMode="MultiLine" Rows="3" MaxLength="2000"
                                        CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                            <asp:Label ID="lbRowid" runat="server" Text="" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-heading text-left">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <div class="form-group">
                            <label>
                                Search Celebrity</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch1" runat="server" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <div class="form-group">
                            <label>
                                Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlSearchLanguage" runat="server" DataTextField="FullName"
                                    DataValueField="LanguageID" DataSourceID="SqlDsLanguage" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <asp:GridView ID="grdCelebrity" runat="server" BackColor="White" Width="100%" DataSourceID="sqlDSCelebrityMaster"
                CssClass="table" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="50" ForeColor="Black"
                GridLines="Vertical" EmptyDataText="No record Found..." AllowSorting="true">
                <AlternatingRowStyle BackColor="White" />
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
                    <asp:TemplateField HeaderText="ID" SortExpression="rowid" Visible="True">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                            <asp:Label ID="lbRowid" runat="server" Text='<%# Bind("rowid") %>' Visible="false" />
                            <asp:Label ID="lbcelebrityid" runat="server" Text='<%# Bind("celebrityid") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label ID="lbCelebName" runat="server" Text='<%# Bind("name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Birth Place" SortExpression="PlaceofBirth">
                        <ItemTemplate>
                            <asp:Label ID="lbPlaceofBirth" runat="server" Text='<%# Bind("PlaceofBirth") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Biography" SortExpression="Biography">
                        <ItemTemplate>
                            <asp:Label ID="lbBiography" runat="server" Text='<%# Bind("Biography") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Regional Name" SortExpression="regname">
                        <ItemTemplate>
                            <asp:Label ID="lbRegCelebName" runat="server" Text='<%# Bind("regname") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Regional Birth Place" SortExpression="regPlaceofBirth">
                        <ItemTemplate>
                            <asp:Label ID="lbRegPlaceofBirth" runat="server" Text='<%# Bind("regPlaceofBirth") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Regional Biography" SortExpression="regBiography">
                        <ItemTemplate>
                            <asp:Label ID="lbRegBiography" runat="server" Text='<%# Bind("regBiography") %>' />
                            <asp:Label ID="lbLanguageId" runat="server" Text='<%# Bind("LanguageId") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sqlDSCelebrityMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
