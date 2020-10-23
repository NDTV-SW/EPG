<%@ Page Title="YouTube Channel Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="CMSyouTubeChannelMaster.aspx.vb" Inherits="EPG.CMSyouTubeChannelMaster" %>

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
                                Youtube Channel Master</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Select Channel</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlChannel" CssClass="form-control" runat="server" DataSourceID="sqlDSChannel"
                                                DataTextField="ChannelId" DataValueField="ChannelId" />
                                            <asp:SqlDataSource ID="sqlDSChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="Select Channelid from mst_channel where onair=1 and companyid<>28 order by channelid" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Youtube Channel Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtChannelName" runat="server" CssClass="form-control" ValidationGroup="VGYTChannels" />
                                            <span class="input-group-addon">
                                                <asp:RequiredFieldValidator ID="RFVtxtChannelName" runat="server" ControlToValidate="txtChannelName"
                                                    ErrorMessage="Channel Name Required" Text="*" ForeColor="Red" ValidationGroup="VGYTChannels" /></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Launch Date on our Platform</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtLaunchDate" placeholder="Launch date" runat="server" CssClass="form-control"
                                                ValidationGroup="VGYTChannels" />
                                            <span class="input-group-addon">
                                                <asp:CalendarExtender ID="CE_txtLaunchDate" runat="server" PopupButtonID="img_txtLaunchDate"
                                                    TargetControlID="txtLaunchDate" />
                                                <asp:Image ID="img_txtLaunchDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                                <asp:RequiredFieldValidator ID="RFVtxtLaunchDate" runat="server" ControlToValidate="txtLaunchDate"
                                                    ErrorMessage="Launch Date Required" Text="*" ForeColor="Red" ValidationGroup="VGYTChannels" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Channel Genre</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlGenre" runat="server" CssClass="form-control" DataSourceID="sqlDSGenre"
                                                DataTextField="GenreName" DataValueField="GenreId" />
                                            <asp:SqlDataSource ID="sqlDSGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="select * from mst_genre where genreCategory='P' order by GenreName" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Available</label>
                                        <div class="input-group">
                                            <asp:CheckBox ID="chkAvailable" runat="server" CssClass="form-control" Checked="false" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Language</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" DataSourceID="sqlDSLanguage"
                                                DataTextField="FullName" DataValueField="LanguageId" />
                                            <asp:SqlDataSource ID="sqlDSLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="select languageId,fullname from mst_language where active=1 order by 2" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Youtube URL</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="form-control" ValidationGroup="VGYTChannels" />
                                            <span class="input-group-addon">
                                                <asp:RegularExpressionValidator ID="REVtxtVideoUrl" runat="server" ControlToValidate="txtVideoUrl"
                                                    ValidationGroup="VGYTChannels" ForeColor="Red" Text="*" ErrorMessage="Video URL not proper"
                                                    ValidationExpression="^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$" />
                                                <asp:RequiredFieldValidator ID="RFVtxtVideoUrl" runat="server" ControlToValidate="txtVideoUrl"
                                                    ValidationGroup="VGYTChannels" Text="*" ErrorMessage="Video URL required"
                                                    ForeColor="Red" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VGYTChannels" />
                                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                        <asp:Label ID="lbYouTubeChannelId" runat="server" Visible="false" />
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <asp:ValidationSummary ID="VS" runat="server" ValidationGroup="VGYTChannels" CssClass="alert-danger" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-3 col-md-3">
                                    <asp:TextBox ID="txtSearch" placeholder="Search Channel" runat="server" CssClass="form-control"
                                        ValidationGroup="VGYTChannelsSearch" />
                                </div>
                                <div class="col-lg-3 col-md-3">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="VGYTChannelsSearch"
                                        CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                                <asp:GridView ID="grd" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                                    CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="20"
                                    AllowPaging="true" BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true"
                                    EmptyDataText="----No Record Found----" EmptyDataRowStyle-HorizontalAlign="Center">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbYouTubeChannelId" runat="server" Text='<%#Eval("YouTubeChannelId") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lbSno" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ChannelID" SortExpression="Channelid">
                                            <ItemTemplate>
                                                <asp:Label ID="lbChannelid" runat="server" Text='<%#Eval("Channelid") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Channel Name" SortExpression="ChannelName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbChannelName" runat="server" Text='<%#Eval("ChannelName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Youtube URL" SortExpression="YoutubeURL">
                                            <ItemTemplate>
                                                <asp:Label ID="lbYoutubeURL" runat="server" Text='<%#Eval("youtubeurl") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Launch Date" SortExpression="LaunchDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbLaunchDate" runat="server" Text='<%#Eval("LaunchDate2") %>' Visible="false" />
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("LaunchDate1") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Genre" SortExpression="GenreId">
                                            <ItemTemplate>
                                                <asp:Label ID="lbGenreId" runat="server" Text='<%#Eval("GenreId") %>' Visible="false" />
                                                <asp:Label ID="lbGenreName" runat="server" Text='<%#Eval("GenreName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Language" SortExpression="FullName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbLanguage" runat="server" Text='<%#Eval("FullName") %>' />
                                                <asp:Label ID="lbLanguageId" runat="server" Text='<%#Eval("languageId") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Available" SortExpression="Available">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAvailable" runat="server" Checked='<%#Eval("Available") %>' />
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
                                <asp:SqlDataSource ID="sqlDSTvStar" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
