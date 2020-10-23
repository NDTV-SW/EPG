<%@ Page Title="Rich Link Missing" Language="vb" AutoEventWireup="false" MasterPageFile="~/verify/Siteverify.Master"
    CodeBehind="program.aspx.vb" Inherits="EPG.verifyprogram" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sc1" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">Verify Program</li>
        </ol>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" DataSourceID="sqlDSChannel" DataTextField="channelid" DataValueField="channelid" />
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="search" CssClass="form-control" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox ID="chkVerified" runat="server" Text="All/only Non verified" Checked="false" ForeColor="White" Font-Size="X-Small"  />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-xs alert-danger1" />
                    </div>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-striped"
                        AutoGenerateColumns="false" DataSourceID="sqlDS" EmptyDataText="--no record found--"
                        DataKeyNames="channelid" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                         EditRowStyle-BackColor="#D0021B" EditRowStyle-ForeColor="#999999" AllowSorting="true"
                        AllowPaging="true" PageSize="20">
                        <Columns>
                            <asp:CommandField ShowEditButton="true" UpdateText="Update" CancelText="Cancel" ControlStyle-CssClass="btn alert-danger1 btn-xs" />
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CheckBoxField HeaderText="Verified" DataField="verified" />
                            <asp:BoundField DataField="companyname" HeaderText="Company" ReadOnly="true" />
                            <asp:BoundField DataField="channelid" HeaderText="Channel" ReadOnly="true" />
                            <asp:BoundField DataField="trp" HeaderText="TRP" />
                            <asp:TemplateField HeaderText="Desc">
                                <ItemTemplate>
                                    <asp:Label ID="lbchanneldesc" runat="server" Text='<%#bind("channeldesc") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtchanneldesc" runat="server" Text='<%#bind("channeldesc") %>' CssClass="form-control" TextMode="MultiLine" Rows="5" />
                                </EditItemTemplate>
                                <ControlStyle Width="250px" />

                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="hdchannel" HeaderText="HD" />
                            <asp:CheckBoxField DataField="seriesenabled" HeaderText="Series" />
                            <asp:CheckBoxField DataField="seasonrequired" HeaderText="Season" />
                            <asp:TemplateField HeaderText="channel_genre">
                                <ItemTemplate>
                                    <asp:Label ID="lbchannel_genre" runat="server" Text='<%#bind("channel_genre") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlchannel_genre" runat="server" DataTextField="channel_genre" DataValueField="channel_genre" DataSourceID="sqlDSChannelGenre" SelectedValue=<%# Bind("channel_genre") %> />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Genre">
                                <ItemTemplate>
                                    <asp:Label ID="lbgenrename" runat="server" Text='<%#bind("genrename") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlgenrename" runat="server" DataTextField="genrename" DataValueField="genreid" DataSourceID="sqlDSGenre" SelectedValue=<%# Bind("genreid") %> />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="movie_channel" HeaderText="IsMovie" />
                            <asp:TemplateField HeaderText="Movie Lang">
                                <ItemTemplate>
                                    <asp:Label ID="lbMovieLang" runat="server" Text='<%#bind("movielanguage") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlMovieLang" runat="server" DataTextField="fullname" DataValueField="languageid" DataSourceID="sqlDSLanguage" SelectedValue=<%# Bind("movielangid") %> />
                                </EditItemTemplate>
                            </asp:TemplateField>



                            
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select * from v_verify_program where channelid=@channelid and case when @verified=1 then @verified else isnull(verified,0) end =@verified and progname like '%' + @progname + '%'"
        UpdateCommand="update mst_channel set verified=@verified,verifiedby=@loggedinuser,verifiedat=dbo.getlocaldate(),trp=@trp
        ,seasonrequired=@seasonrequired,channeldesc=@channeldesc,hdchannel=@hdchannel,seriesenabled=@seriesenabled
        ,movielangid=case when @movielangid=0 then null else @movielangid end
        ,movie_channel=@movie_channel,channel_genre=@channel_genre,genreid=@genreid
         where channelid=@channelid;
         insert into aud_mst_channel(channelid,ServiceId,PlayoutSource,ActivationSourceId,CatchupFlag,active,ActionUser,ActionType,verified,verifiedby,verifiedat,trp,seasonrequired,channeldesc,hdchannel,seriesenabled,movielangid,movie_channel,channel_genre,genreid) values
         (@channelid,@channelid,@channelid,1,0,1,@loggedinuser,'Verify',@verified,@loggedinuser,dbo.getlocaldate(),@trp,@seasonrequired,@channeldesc,@hdchannel,@seriesenabled,@movielangid,@movie_channel,@channel_genre,@genreid)
         "
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
        <asp:ControlParameter ControlID="ddlChannel" PropertyName="SelectedValue" Name="channelid"
                 />
            <asp:ControlParameter ControlID="txtSearch" PropertyName="Text" Name="progname"
                ConvertEmptyStringToNull="false" />
            <asp:ControlParameter ControlID="chkVerified" PropertyName="Checked" Name="verified"
                ConvertEmptyStringToNull="false" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="loggedinuser" Type="String" ConvertEmptyStringToNull="false"/>
            <asp:Parameter Name="movielangid" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlDSChannel" runat="server" SelectCommand="select channelid from mst_channel where onair=1 order by channelid"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    
    <asp:SqlDataSource ID="sqlDSBroadcaster" runat="server" SelectCommand="select '0' companyid, '-- select --' companyname union select CompanyId,CompanyName from mst_company where active=1 order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSLanguage" runat="server" SelectCommand="select '0' languageid, '-- select --' fullname  union select languageid,fullname from mst_language where active=1  order by 1"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    
    <asp:SqlDataSource ID="sqlDSGenre" runat="server" SelectCommand="select '0' genreid, '-- select --' genrename  union  select genreid,genrename from mst_genre where genrecategory='S' order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSChannelGenre" runat="server" SelectCommand="select '' channel_genre union select distinct channel_genre from mst_channel order by 1"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    
</asp:Content>
