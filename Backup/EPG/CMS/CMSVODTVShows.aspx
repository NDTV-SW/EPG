<%@ Page Title="VOD TV Shows" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="CMSVODTVShows.aspx.vb" Inherits="EPG.CMSVODTVShows" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <br />
            <div class="container">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h3>
                                VOD TV Shows</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Platform</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlPlatform" runat="server" CssClass="form-control" DataSourceID="sqlDSPlatform"
                                                DataTextField="PlatformName" DataValueField="PlatformId" />
                                            <asp:SqlDataSource ID="sqlDSPlatform" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="select platformid,platformname from mst_vodplatforms order by 2" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Channel</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" AutoPostBack="true"
                                                DataSourceID="sqlDSChannel" DataTextField="channelid" DataValueField="channelid" />
                                            <asp:SqlDataSource ID="sqlDSChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="select channelid from mst_channel where onair=1 and companyid<>28 order by 1" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Select Programme</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlProgramme" CssClass="form-control" runat="server" DataTextField="programme"
                                                DataValueField="ProgId" DataSourceID="sqlDSProgramme" />
                                            <asp:SqlDataSource ID="sqlDSProgramme" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="select ProgName programme,progid from mst_program where ChannelId=@channelid and active=1 order by 1">
                                                <SelectParameters>
                                                    <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue"
                                                        DbType="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            VOD URL</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="form-control" ValidationGroup="VGVODTVShows" />
                                            <span class="input-group-addon">
                                                <%--<asp:RegularExpressionValidator ID="REVtxtVideoUrl" runat="server" ControlToValidate="txtVideoUrl" ValidationGroup="VGVODTVShows" ForeColor="Red"
                                                    Text="*" ErrorMessage="Video URL not proper" ValidationExpression="^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$" />--%>
                                                <asp:RequiredFieldValidator ID="RFVtxtVideoUrl" runat="server" ControlToValidate="txtVideoUrl"
                                                    ValidationGroup="VGVODTVShows" Text="*" ErrorMessage="Video URL required" ForeColor="Red" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Price</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" ValidationGroup="VGVODTVShows" />
                                            <span class="input-group-addon">
                                                <asp:RegularExpressionValidator ID="REVtxtPrice" runat="server" ControlToValidate="txtPrice"
                                                    ValidationGroup="VGVODTVShows" ForeColor="Red" Text="*" ErrorMessage="Price should be numberic"
                                                    ValidationExpression="^\d+" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            KeyWords (comma separated)</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="form-control" ValidationGroup="VGVODTVShows"
                                                TextMode="MultiLine" Rows="3" />
                                            <span class="input-group-addon">
                                                <asp:RequiredFieldValidator ID="RFVtxtKeywords" runat="server" ControlToValidate="txtKeywords"
                                                    ValidationGroup="VGVODTVShows" Text="*" ErrorMessage="Keywords required" ForeColor="Red" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VGVODTVShows" />
                                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                        <asp:Label ID="lbRowId" runat="server" Visible="false" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <asp:ValidationSummary ID="VS" runat="server" ValidationGroup="VGVODTVShows" CssClass="alert-danger" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Search Programme" CssClass="form-control" />
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="grd" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                                CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="20"
                                AllowPaging="true" BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true"
                                EmptyDataText="----No Record Found----" EmptyDataRowStyle-HorizontalAlign="Center">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbRowId" runat="server" Text='<%#Eval("rowId") %>' Visible="false" />
                                            <asp:Label ID="lbProgid" runat="server" Text='<%#Eval("progid") %>' Visible="false" />
                                            <asp:Label ID="lbChannelId" runat="server" Text='<%#Eval("channelid") %>' Visible="false" />
                                            <asp:Label ID="lbSno" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Platform" SortExpression="PlatformName">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPlatformName" runat="server" Text='<%#Eval("PlatformName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Programme" SortExpression="progname">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprogname" runat="server" Text='<%#Eval("progname") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Video Url" SortExpression="VideoUrl">
                                        <ItemTemplate>
                                            <asp:Label ID="lbVideoUrl" runat="server" Text='<%#Eval("VideoUrl") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Keywords" SortExpression="keywords">
                                        <ItemTemplate>
                                            <asp:Label ID="lbKeywords" runat="server" Text='<%#Eval("keywords") %>' Visible="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" SortExpression="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPrice" runat="server" Text='<%#Eval("Price") %>' Visible="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modified at" SortExpression="modifiedBy">
                                        <ItemTemplate>
                                            <asp:Label ID="lbmodifiedBy" runat="server" Text='<%#Eval("modifiedBy") %>' />
                                            <asp:Label ID="lbmodifiedAt" runat="server" Text='<%#Eval("modifiedAt") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="../Images/Edit.png" />
                                    <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="../Images/delete.png" />
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
                            <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
