<%@ Page Title="TV Star Language Mapping" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master" CodeBehind="CMSTvStarMapping.aspx.vb" Inherits="EPG.CMSTvStarMapping" %>
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
                <h3>TV Star Language Mapping</h3>
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
                            <label>Select TV Star</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlTvStar" CssClass="form-control" runat="server"  DataTextField="name" DataValueField="ProfileID" DataSourceID="sqlDSTvStar" /> 
                                <asp:SqlDataSource ID="sqlDSTvStar" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                                    SelectCommand="select ProfileID,name from mst_tvstars order by name"/>
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>TV Star Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlLanguageName" CssClass="form-control" runat="server"  DataTextField="Fullname" DataValueField="languageid" DataSourceID="sqlDSLanguage" /> 
                                <asp:SqlDataSource ID="sqlDSLanguage" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                                    SelectCommand="select * from mst_language where active=1"/>
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
                <div class="row">
                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                        <hr />
                        <h3>SEARCH</h3>     
                        <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12 ">
                            <div class="form-group">
                                <label></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ValidationGroup="VGBigTVSearch" />        
                                    <span class="input-group-addon"></span>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6 col-sm-12 col-xs-12 ">
                            <div class="form-group">
                                <label></label>
                                <div class="input-group">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="VGBigTVSearch" CssClass="btn btn-info" />
                                </div>
                            </div>
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
                        <asp:GridView ID="grdTvStarMapping" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                            CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="20" AllowPaging="true"
                            BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true" EmptyDataText="----No Record Found----"
                            EmptyDataRowStyle-HorizontalAlign="Center">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>                  
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProfileId" runat="server" Text='<%#Eval("profileId") %>' Visible="false" />
                                        <asp:Label ID="lbRowId" runat="server" Text='<%#Eval("rowId") %>' Visible="false" />
                                        <asp:Label ID="lbLanguageId" runat="server" Text='<%#Eval("languageId") %>' Visible="false" />
                                        <asp:Label ID="lbSno" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbName" runat="server" Text='<%#Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Language" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:Label ID="lbLanguage" runat="server" Text='<%#Eval("FullName") %>' />
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
                        <asp:SqlDataSource ID="sqlDSTvStarMapping" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
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
