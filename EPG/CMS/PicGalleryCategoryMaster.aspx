<%@ Page Title="Pic Gallery Category Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master" CodeBehind="PicGalleryCategoryMaster.aspx.vb" Inherits="EPG.PicGalleryCategoryMaster" %>
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
                <h3>Picture Gallery Category Master</h3>
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
                            <label>Category Name</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" ValidationGroup="VGPicGalleryCategory" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" />
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                            <asp:Label ID="lbCategoryId" runat="server" Visible="false" />
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
                        <asp:GridView ID="grdPicGalleryCategory" runat="server" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="sqlDSPicGalleryCategory"
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
                                <asp:TemplateField HeaderText="ID" SortExpression="categoryid">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcategoryid" runat="server" Text='<%#Eval("categoryid") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Name" SortExpression="categoryname">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcategoryname" runat="server" Text='<%#Eval("categoryname") %>' />
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
                            SelectCommand="select categoryid,categoryname from picgallery_category order by 2" SelectCommandType="Text" />
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
