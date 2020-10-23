<%@ Page Title="Rich Link Missing" Language="vb" AutoEventWireup="false" MasterPageFile="~/RichMeta/SiteRich.Master"
    CodeBehind="richlinkmissing.aspx.vb" Inherits="EPG.richlinkmissing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sc1" runat="server" />
    <div class="row">   
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">Rich Link Missing</li>
        </ol>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" DataSourceID="sqlDSChannel"
                            DataTextField="channelname" DataValueField="channelid" AutoPostBack="true" />
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkWithInfo" runat="server" Text="With Info" CssClass="form-control" AutoPostBack="true" Checked="false" />
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox ID="chkMovie" runat="server" Text="only Movie Channels" CssClass="form-control" AutoPostBack="true" Checked="false" />
                    </div>
                    <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-striped"
                        PageSize="30" AllowPaging="true" DataSourceID="sqlDS" EmptyDataText="--no record found--"
                        EmptyDataRowStyle-CssClass="alert alert-danger text-center" SelectedRowStyle-BackColor="#D0021B"
                        SelectedRowStyle-ForeColor="#ffffff" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Channelid" DataField="Channelid" />
                            <asp:BoundField HeaderText="ProgName" DataField="ProgName" />
                            <asp:BoundField HeaderText="Genre" DataField="Genre" />
                            <asp:BoundField HeaderText="Duration" DataField="Duration" />
                            <asp:BoundField HeaderText="Year" DataField="releaseyear" />
                            <asp:BoundField HeaderText="Actor" DataField="actor" />
                            <asp:BoundField HeaderText="Director" DataField="director" />
                            <asp:BoundField HeaderText="Award" DataField="awards" />
                            <asp:BoundField HeaderText="Producer" DataField="producer" />
                            <asp:BoundField HeaderText="Lang" DataField="origlang" />
                            <asp:BoundField HeaderText="Dubbed" DataField="dubbedlang" />
                            <asp:BoundField HeaderText="Country" DataField="origincountry" />
                           
                            <asp:TemplateField HeaderText="Rich Meta Linking">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgid" runat="server" Text='<%# Eval("progid") %>' Visible="false" />
                                    <asp:Label ID="lbChannelid" runat="server" Text='<%# Eval("Channelid") %>' Visible="false" />
                                    <asp:TextBox ID="txtSearchRichMeta" runat="server" CssClass="form-control" />
                                    <asp:AutoCompleteExtender ID="ACE_txtSearchRichMeta" runat="server" ServiceMethod="SearhRichMeta"
                                        MinimumPrefixLength="1" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtSearchRichMeta" FirstRowSelected="true" UseContextKey="true" />
                                </ItemTemplate>
                                <ControlStyle Width="300px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Show" Value="Show" />
                                        <asp:ListItem Text="Movie" Value="Movie" />
                                        <asp:ListItem Text="News" Value="News" />
                                        <asp:ListItem Text="Sports" Value="Sports" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ControlStyle Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Add">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddRich" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                        <asp:Button ID="btnUpdateRich" runat="server" CssClass="btn btn-info" Text="Link RichMeta" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select * from v_program_richlinkmissing1 where case when @onlymovie=1 then channelgenreid else '107' end = '107' and case when @withinfo=1 then isnull(actor,'') else '0' end <> '' and  case when @channelid='0' then @channelid else channelid end = @channelid  order by channelid,progname"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter Name="withinfo" ControlID="chkWithInfo" PropertyName="Checked"
                Type="Boolean" />
            <asp:ControlParameter Name="onlymovie" ControlID="chkMovie" PropertyName="Checked"
                Type="Boolean" />
                
                
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSChannel" runat="server" SelectCommand="select '0' channelid,'-- select --' channelname union select channelid,channelid channelname from mst_channel where onair=1 AND  (companyid = 5 or (companyid = 58 and movie_channel=1) or  (companyid = 189 and channelid like 'UTV%')) and genreid<>101 and genreid<>102 and genreid<>101 and genreid<>102 order by 1"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
</asp:Content>
