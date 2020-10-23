<%@ Page Title="YouTube Videos API" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="CMSyouTubeVideosAPI.aspx.vb" Inherits="EPG.CMSyouTubeVideosAPI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <%--<asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
            <br />
            <div class="container">
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading text-center">
                            <h3>
                                Youtube Videos from API</h3>
                        </div>
                        <%--<div class="panel-body">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Youtube Channel ID</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtChannelID" runat="server" CssClass="form-control" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Youtube Channel Name</label>
                                        <div class="input-group">
                                            <asp:Label ID="lbChannelName" runat="server" CssClass="form-control" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Video Id</label>
                                        <div class="input-group">
                                            <asp:Label ID="lbVideoID" runat="server" CssClass="form-control" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="col-lg-2 col-md-2 col-sm-6">
                                    <div class="form-group">
                                        <label>
                                            Select Channel</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlChannelId" runat="server" DataSourceID="sqlDSChannelId" CssClass="form-control" DataTextField="ChannelId" DataValueField="Channelid" AutoPostBack="true" />
                                            <span class="input-group-addon">
                                                <asp:SqlDataSource ID="sqlDSChannelId" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                 SelectCommand="Select '--Select--' Channelid, '0' priority union select Channelid,0 from mst_channel where onair=1 and publicchannel=1 order by 2,1" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6">
                                    <div class="form-group">
                                        <label>
                                            Select Program</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlProgram" runat="server" DataSourceID="sqlDSProgram" CssClass="form-control" DataTextField="progname" DataValueField="progid" />
                                            <span class="input-group-addon">
                                                <asp:SqlDataSource ID="sqlDSProgram" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                   SelectCommand="Select progid,progname from mst_program where active=1 and channelid=@channelid order by 2">
                                                   <SelectParameters>
                                                        <asp:ControlParameter ControlID="ddlChannelId" Name="channelid" Direction="Input" PropertyName="SelectedValue" />
                                                   </SelectParameters>
                                                   </asp:SqlDataSource>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6">
                                    <div class="form-group">
                                        <label>
                                            Video Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtVideoTitle" runat="server" CssClass="form-control" ValidationGroup="VGYouTubeVideos"  />
                                            <span class="input-group-addon">
                                                <asp:RequiredFieldValidator ID="RFVtxtVideoTitle" runat="server" ControlToValidate="txtVideoTitle" ValidationGroup="VGYouTubeVideos" 
                                                    ForeColor="Red" Font-Bold="true" Text="*" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-6">
                                    <div class="form-group">
                                        <label>
                                            Description</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" ValidationGroup="VGYouTubeVideos"  />
                                            <span class="input-group-addon">
                                                <asp:RequiredFieldValidator ID="RFVtxtDescription" runat="server" ControlToValidate="txtDescription" ValidationGroup="VGYouTubeVideos" 
                                                    ForeColor="Red" Font-Bold="true" Text="*" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
<%--                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group">
                                        <label>
                                            Published at</label>
                                        <div class="input-group">
                                            <asp:Label ID="lbPublishedAt" runat="server" CssClass="form-control" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="col-lg-2 col-md-2 col-sm-6">
                                    <div class="form-group">
                                        <label>
                                            Verified</label>
                                        <div class="input-group">
                                            <asp:CheckBox ID="chkVerified" runat="server" CssClass="form-control" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-lg-2 col-md-2 col-sm-6">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" CssClass="btn btn-info" ValidationGroup="VGYouTubeVideos" Visible="false" />
                                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                        <asp:Label ID="lbRowId" runat="server" Visible="False" />
                                    </div>
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
                                            Search Channel</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlChannelSearch" runat="server" DataSourceID="sqlDSChannelId" CssClass="form-control" DataTextField="ChannelId" DataValueField="Channelid"   />
                                            <span class="input-group-addon">
                                                
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
                                                <asp:RequiredFieldValidator ID ="RFV_txtStartDate" runat="server" ControlToValidate="txtStartDate" Text="*" ForeColor="Red"  ValidationGroup="VG" />
                                                <asp:Image ID="imgtxtStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                                <asp:CalendarExtender ID="CE_txtStartDate" runat="server" PopupButtonID="imgtxtStartDate" TargetControlID="txtStartDate" />
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
                                                <asp:RequiredFieldValidator ID ="TFV_txtEndDate" ControlToValidate="txtEndDate" runat="server" Text="*" ForeColor="Red"  ValidationGroup="VG" />
                                                <asp:Image ID="imgtxtEndDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                                <asp:CalendarExtender ID="CE_txtEndDate" runat="server" PopupButtonID="imgtxtEndDate" TargetControlID="txtEndDate" />
                                            </span>
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
                                CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="20"
                                AllowPaging="true" BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true"
                                EmptyDataText="----No Record Found----" EmptyDataRowStyle-HorizontalAlign="Center">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                             <%#Container.DataItemIndex+1 %>
                                            <asp:Label ID="lbRowId" runat="server" Text='<%#Eval("rowId") %>' Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Button ID="btnUpdate" runat="server" Text="Verify" CssClass="btn btn-info btn-xs"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' CommandName="verify" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Verified">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkVerify" runat="server" Checked='<%#Eval("verified") %>' />
                                            <asp:Label ID="lbDescription" runat="server" Text='<%#Eval("description") %>' Visible="false" />
                                            <asp:Label ID="lbVideoName" runat="server" Text='<%#Eval("ytVideoName") %>' Visible="false" />
                                            <asp:Label ID="lbProgId" runat="server" Text='<%#Eval("progid") %>' Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Channelid">
                                        <ItemTemplate>
                                            <asp:Label ID="lbChannelid" runat="server" Text='<%#Eval("Channelid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="progname" HeaderText="Programme" SortExpression="progname" />
                                    <asp:BoundField DataField="ytChannelid" HeaderText="YT Channel" SortExpression="ytChannelid" />
                                    <asp:BoundField DataField="ytVideoName" HeaderText="VideoName" SortExpression="ytVideoName" />
                                    <asp:TemplateField HeaderText="Video">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyView" runat="server" Text="View" Target="_blank" NavigateUrl='<%#Eval("ytVideourl") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                                    
                                    
                                    <asp:TemplateField HeaderText="Thumbnail">
                                        <ItemTemplate>
                                            <asp:Image ID="imgImage" runat="server" ImageUrl='<%#Eval("imageurl1") %>' AlternateText="Image" Height="50px" Width="50px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="publishedat" HeaderText="Publish Time" SortExpression="publishedat" />
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
