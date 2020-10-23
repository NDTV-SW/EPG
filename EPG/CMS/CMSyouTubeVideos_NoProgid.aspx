<%@ Page Title="YouTube Videos" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="CMSyouTubeVideos_NoProgid.aspx.vb" Inherits="EPG.CMSyouTubeVideos_NoProgid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <%-- <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
    <br />
    <div class="container">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Youtube Videos</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Video Title</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtVideoTitle" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtVideoTitle" runat="server" ControlToValidate="txtVideoTitle"
                                            ValidationGroup="VGYouTubeVideos" Text="*" ErrorMessage="Title required" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Hindi Video Title</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtVideoTitleHindi" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFV_txtVideoTitle" runat="server" ControlToValidate="txtVideoTitle"
                                            ValidationGroup="VGYouTubeVideos" Text="*" ErrorMessage="Hindi Title required" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Genre</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlGenre" runat="server" DataSourceID="SqlDsGenreMaster" DataTextField="GenreName"
                                        CssClass="form-control" DataValueField="GenreId" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDsGenreMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select GenreID, GenreName from mst_Genre where genrecategory='P' order by 2">
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Sub Genre</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlSubGenre" runat="server" AutoCompleteMode="SuggestAppend"
                                        DropDownStyle="DropDownList" ItemInsertLocation="Append" DataSourceID="SqlDsSubGenreMaster"
                                        DataTextField="SubGenreName" CssClass="form-control" DataValueField="SubGenreId"
                                        Visible="True">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDsSubGenreMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT SubGenreId, SubGenreName FROM mst_SubGenre where GENREID=@GENREID ORDER BY SubGenreName">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlGenre" Name="GenreId" PropertyName="SelectedValue"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Youtube URL</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REVtxtVideoUrl" runat="server" ControlToValidate="txtVideoUrl"
                                            ValidationGroup="VGYouTubeVideos" ForeColor="Red" Text="*" ErrorMessage="Video URL not proper"
                                            ValidationExpression="^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$" />
                                        <asp:RequiredFieldValidator ID="RFVtxtVideoUrl" runat="server" ControlToValidate="txtVideoUrl"
                                            ValidationGroup="VGYouTubeVideos" Text="*" ErrorMessage="Video URL required"
                                            ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Episode</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEpisode" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REV_txtEpisode" ControlToValidate="txtEpisode"
                                            ValidationExpression="^\d+" Display="Static" Text="*" ErrorMessage="Episode should be numbers"
                                            ForeColor="Red" runat="server" ValidationGroup="VGYouTubeVideos" />
                                        <asp:RequiredFieldValidator ID="RFV_txtEpisode" runat="server" ControlToValidate="txtEpisode"
                                            ValidationGroup="VGYouTubeVideos" Text="*" ErrorMessage="Episode number required"
                                            ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Season</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSeason" runat="server" Text="1" CssClass="form-control" ValidationGroup="VGYouTubeVideos" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REVtxtSeason" ControlToValidate="txtSeason" ValidationExpression="^\d+"
                                            Display="Static" Text="*" ErrorMessage="Season should be numbers" ForeColor="Red"
                                            runat="server" ValidationGroup="VGYouTubeVideos" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Likes</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtLikes" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REVtxtLikes" ControlToValidate="txtLikes" ValidationExpression="^\d+"
                                            Display="Static" Text="*" ErrorMessage="Likes should be numbers" ForeColor="Red"
                                            runat="server" ValidationGroup="VGYouTubeVideos" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Synopsis</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSynopsis" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos"
                                        TextMode="MultiLine" Rows="3" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Hindi Synopsis</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSynopsisHindi" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos"
                                        TextMode="MultiLine" Rows="3" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    KeyWords (comma separated)</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos"
                                        TextMode="MultiLine" Rows="3" />
                                    <span class="input-group-addon">
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Verified</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkVerified" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VGYouTubeVideos" />
                                <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                <asp:Label ID="lbRowId" runat="server" Visible="false" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <asp:ValidationSummary ID="VS" runat="server" ValidationGroup="VGYouTubeVideos" CssClass="alert-danger" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    Search Title</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    Search Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannelSearch" runat="server" DataSourceID="sqlDSChannelIdSearch"
                                        CssClass="form-control" DataTextField="ChannelName" DataValueField="youtubeChannelId" />
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="sqlDSChannelIdSearch" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="select '0' youtubeChannelId, '--Select--' ChannelName, '0' priority union select youtubeChannelId, ChannelName, '1' priority  from mst_youTubeChannels where available = 1 order by 3,2" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    Publish Date From</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ValidationGroup="VG" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFV_txtStartDate" runat="server" ControlToValidate="txtStartDate"
                                            Text="*" ForeColor="Red" ValidationGroup="VG" />
                                        <asp:Image ID="imgtxtStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtStartDate" runat="server" PopupButtonID="imgtxtStartDate"
                                            TargetControlID="txtStartDate" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    Publish Date To</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ValidationGroup="VG" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="TFV_txtEndDate" ControlToValidate="txtEndDate" runat="server"
                                            Text="*" ForeColor="Red" ValidationGroup="VG" />
                                        <asp:Image ID="imgtxtEndDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CE_txtEndDate" runat="server" PopupButtonID="imgtxtEndDate"
                                            TargetControlID="txtEndDate" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <label>
                                    Verified?</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkVerifiedSearch" runat="server" CssClass="form-control" Checked="true" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:Button ID="btnFilter" runat="server" Text="Search" CssClass="btn btn-info" ValidationGroup="VG" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grd" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                        DataSourceID="sqlDSYouTubeVideos" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" PageSize="50" AllowPaging="true" BorderWidth="1px" CellPadding="4"
                        ForeColor="Black" AllowSorting="true" EmptyDataText="----No Record Found----"
                        EmptyDataRowStyle-HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbRowId" runat="server" Text='<%#Eval("rowId") %>' Visible="false" />
                                    <asp:Label ID="lbSno" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="VideoTitle" HeaderText="VideoTitle" />
                            <asp:BoundField DataField="VideoTitle_hin" HeaderText="VideoTitle Hindi" />
                            <asp:BoundField DataField="Synopsis" HeaderText="Synopsis" />
                            <asp:BoundField DataField="Synopsis_Hin" HeaderText="Synopsis Hindi" />
                            <asp:TemplateField HeaderText="Publish Date" SortExpression="PublishDate">
                                <ItemTemplate>
                                    <asp:Label ID="lbPublishDate" runat="server" Text='<%#Eval("PublishDate") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Episode">
                                <ItemTemplate>
                                    <asp:Label ID="lbEpisodeId" runat="server" Text='<%#Eval("episodeId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duration (in secs)" SortExpression="duration">
                                <ItemTemplate>
                                    <asp:Label ID="lbDuration" runat="server" Text='<%#Eval("Duration") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hyView" runat="server" Text="View" Target="_blank" />
                                    <asp:Label ID="lbVideoUrl" runat="server" Text='<%#Eval("videourl") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Verified" SortExpression="Verified">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkVerified" runat="server" Checked='<%#Eval("Verified") %>' />
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
                    <asp:SqlDataSource ID="sqlDSYouTubeVideos" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
