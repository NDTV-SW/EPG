<%@ Page Title="SONY LIV Videos" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="SonyLiv.aspx.vb" Inherits="EPG.SonyLiv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <br />
    <div class="container">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Sony Liv Videos</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Select Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" AutoPostBack="true"
                                        DataSourceID="sqlDSChannel" DataTextField="channelid" DataValueField="channelid" />
                                    <asp:SqlDataSource ID="sqlDSChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select channelid from mst_channel where onair=1 and CompanyId in (6,61) order by ChannelID" />
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
                                        SelectCommand="select  ' ---Select---' programme,'0' progid union select ltrim(rtrim(progname)) progname, progid from mst_program where channelid=@channelid and progid in (select distinct progid from mst_epg where channelid=@channelid) order by 1">
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
                                    Video Title</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtVideoTitle" runat="server" CssClass="form-control" ValidationGroup="VGSonyLiv" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtVideoTitle" runat="server" ControlToValidate="txtVideoTitle"
                                            ValidationGroup="VGSonyLiv" Text="*" ErrorMessage="Title required" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    SonyLiv URL</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="form-control" ValidationGroup="VGSonyLiv" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtVideoUrl" runat="server" ControlToValidate="txtVideoUrl"
                                            ValidationGroup="VGSonyLiv" Text="*" ErrorMessage="Video URL required" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Episode</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEpisode" runat="server" CssClass="form-control" ValidationGroup="VGSonyLiv" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REV_txtEpisode" ControlToValidate="txtEpisode"
                                            ValidationExpression="^\d+" Display="Static" Text="*" ErrorMessage="Episode should be numbers"
                                            ForeColor="Red" runat="server" ValidationGroup="VGSonyLiv" />
                                        <asp:RequiredFieldValidator ID="RFV_txtEpisode" runat="server" ControlToValidate="txtEpisode"
                                            ValidationGroup="VGSonyLiv" Text="*" ErrorMessage="Episode number required" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Publish Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtPublishDate" runat="server" CssClass="form-control" ValidationGroup="VGSonyLiv" />
                                    <span class="input-group-addon">
                                        <asp:CalendarExtender ID="CE_txtPublishDate" runat="server" PopupButtonID="img_txtPublishDate"
                                            TargetControlID="txtPublishDate" />
                                        <asp:Image ID="img_txtPublishDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:RequiredFieldValidator ID="RFVtxtPublishDate" runat="server" ControlToValidate="txtPublishDate"
                                            ValidationGroup="VGSonyLiv" Text="*" ErrorMessage="Publish date required" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Show Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtShowDate" runat="server" CssClass="form-control" ValidationGroup="VGSonyLiv" />
                                    <span class="input-group-addon">
                                        <asp:CalendarExtender ID="CEtxtShowDate" runat="server" PopupButtonID="img_txtShowDate"
                                            TargetControlID="txtShowDate" />
                                        <asp:Image ID="img_txtShowDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:RequiredFieldValidator ID="RVFtxtShowDate" runat="server" ControlToValidate="txtShowDate"
                                            ValidationGroup="VGSonyLiv" Text="*" ErrorMessage="Show date required" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Synopsis</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtSynopsis" runat="server" CssClass="form-control" ValidationGroup="VGSonyLiv"
                                        TextMode="MultiLine" Rows="3" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtSynopsis" runat="server" ControlToValidate="txtSynopsis"
                                            ValidationGroup="VGSonyLiv" Text="*" ErrorMessage="Synopsis" ForeColor="Red" />
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
                                <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VGSonyLiv" />
                                <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                <asp:Label ID="lbRowId" runat="server" Visible="false" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <asp:ValidationSummary ID="VS" runat="server" ValidationGroup="VGSonyLiv" CssClass="alert-danger" />
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
                                        CssClass="form-control" DataTextField="channelid" DataValueField="channelid" />
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="sqlDSChannelIdSearch" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="select '--Select--' channelid union select channelid from mst_channel where onair=1 and CompanyId in (6,61) order by 1" />
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
                                    <asp:CheckBox ID="chkVerifiedSearch" runat="server" CssClass="form-control" Checked="false" />
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
                        CssClass="table" BackColor="White" BorderColor="#DEDFDE" OnSorting="SortRecords"
                        BorderStyle="None" PageSize="50" AllowPaging="true" BorderWidth="1px" CellPadding="4"
                        ForeColor="Black" AllowSorting="true" EmptyDataText="----No Record Found----"
                        EmptyDataRowStyle-HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbRowId" runat="server" Text='<%#Eval("rowId") %>' Visible="false" />
                                    <asp:Label ID="lbProgid" runat="server" Text='<%#Eval("progid") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel" SortExpression="channelid">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannelId" runat="server" Text='<%#Eval("channelid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme" SortExpression="progname">
                                <ItemTemplate>
                                    <asp:Label ID="lbprogname" runat="server" Text='<%#Eval("progname") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Video Title" SortExpression="VideoTitle">
                                <ItemTemplate>
                                    <asp:Label ID="lbVideoTitle" runat="server" Text='<%#Eval("VideoTitle") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Synopsis" SortExpression="synopsis">
                                <ItemTemplate>
                                    <asp:Label ID="lbSynopsis" runat="server" Text='<%#Eval("synopsis") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Genre" SortExpression="genre">
                                <ItemTemplate>
                                    <asp:Label ID="lbGenre" runat="server" Text='<%#Eval("genre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Video Url" SortExpression="VideoUrl">
                                <ItemTemplate>
                                    <asp:Label ID="lbVideoUrl" runat="server" Text='<%#Eval("VideoUrl") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Show AirDate" SortExpression="showonairdate">
                                <ItemTemplate>
                                    <asp:Label ID="lbShowAirdate" runat="server" Text='<%#Eval("showonairdate") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                    <asp:SqlDataSource ID="sqlDSSonyLiv" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
