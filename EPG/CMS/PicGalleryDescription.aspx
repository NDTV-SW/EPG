<%@ Page Title="Pic Gallery Description" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master" CodeBehind="PicGalleryDescription.aspx.vb" Inherits="EPG.PicGalleryDescription" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .noFormatting
        {
            text-decoration: none;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <asp:UpdatePanel ID="upd" runat="server">
    <ContentTemplate>
        <br />
        <div class="row">
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1"></div>
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                <h3>Picture Gallery Description</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                &nbsp;
            </div>
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                <div class="row">  
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Select Gallery</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlGallery" runat="server" DataSourceID="sqlDSGallery" DataTextField="GalleryName" DataValueField="galleryID" CssClass="form-control"/>
                                <asp:SqlDataSource id="sqlDSGallery" runat="server" SelectCommand="select a.galleryID, c.categoryname + ' (' + b.Name + ')' GalleryName from picgallery_master  a join mst_tvstars b on a.profileid=b.ProfileID join picgallery_category c on a.categoryid=c.categoryid order by 2"
                                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Gallery Title</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Synopsis</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtSynopsis" runat="server" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Select Tv Star</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlLanguage" runat="server" DataSourceID="sqlDSLanguage" DataTextField="FullName" DataValueField="languageId" CssClass="form-control"/>
                                <asp:SqlDataSource id="sqlDSLanguage" runat="server" SelectCommand="select languageid,fullname from mst_language order by 2"
                                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" />
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                            <asp:Label ID="lbRowId" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                &nbsp;
            </div>
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 ">
                    <hr />
                    <h3>REPORT</h3>
                        <asp:GridView ID="grdPicGallery" runat="server" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="sqlDSPicGalleryCategory"
                            CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="500" AllowPaging="true"
                            BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true" EmptyDataText="----No Record Found----"
                            EmptyDataRowStyle-HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSno" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gallery ID" SortExpression="categoryid">
                                    <ItemTemplate>
                                        <asp:Label ID="lbGalleryId" runat="server" Text='<%#Eval("galleryId") %>' />
                                        <asp:Label ID="lbRowId" runat="server" Text='<%#Eval("rowId") %>' Visible="false" />
                                        <asp:Label ID="lblanguageId" runat="server" Text='<%#Eval("languageId") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lbTitle" runat="server" Text='<%#Eval("Title") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Synopsis" SortExpression="Synopsis">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%#Eval("Synopsis") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Language" SortExpression="Fullname">
                                    <ItemTemplate>
                                        <asp:Label ID="lbFullname" runat="server" Text='<%#Eval("Fullname") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modified" SortExpression="ModifiedBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lbModifiedBy" runat="server" Text='<%#Eval("ModifiedBy") %>' />
                                        <asp:Label ID="lbdateModified" runat="server" Text='<%#Eval("dateModified") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="../Images/Edit.png"/>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlDSPicGalleryCategory" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="select a.rowid,a.galleryID,a.title,a.synopsis,a.languageid,a.datemodified,a.modifiedby,b.FullName from picgallery_desc a join mst_language b on a.languageid=b.LanguageID"
                            SelectCommandType="Text" />
                        <br /><br /><br />
                        <div class="clearfix">
                        </div>
                    </div>
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                &nbsp;
            </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
