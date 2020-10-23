<%@ Page Title="STAR Home Operators" Language="vb" MasterPageFile="SiteStar.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="EPG.StarDefault" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="panel panel-default">
        <div class="panel-heading text-center">
            <h3>Star Home Page</h3>
        </div>
    
        <div class="panel-body">
            <h4>
                EPG Availability for International Channels</h4>
            <asp:GridView ID="grdInt" runat="server" CssClass="table" HeaderStyle-CssClass="text-center"
                DataSourceID="sqlDSInt" CellPadding="3" GridLines="Vertical" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" AllowSorting="true">
                <HeaderStyle BackColor="#D0021B" ForeColor="White" />
                <AlternatingRowStyle BackColor="#ffe1e9" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-body">
            <h4>
                EPG Availability for Domestic Channels</h4>
            <asp:GridView ID="grd" runat="server" CssClass="table" HeaderStyle-CssClass="text-center"
                DataSourceID="sqlDS" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" AllowSorting="true" >
                <HeaderStyle BackColor="#D0021B" ForeColor="White" />
                <AlternatingRowStyle BackColor="#ffe1e9" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="col-md-6">
        <div class="panel panel-default">
            <div class="panel-body">
            <h4>
                Live matches coming in next 3 days</h4>
            <asp:GridView ID="grdLive" runat="server" CssClass="table" HeaderStyle-CssClass="text-center"
                DataSourceID="sqlDSLive" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" AllowSorting="true" >
                <HeaderStyle BackColor="#D0021B" ForeColor="White" />
                <AlternatingRowStyle BackColor="#ffe1e9" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="panel panel-default">
        <div class="panel-body">
            <h4>
                Semi-Final / Final coming in next 7 days</h4>
            <asp:GridView ID="grdFinal" runat="server" CssClass="table" HeaderStyle-CssClass="text-center"
                DataSourceID="sqlDSFinal" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" AllowSorting="true" >
                <HeaderStyle BackColor="#D0021B" ForeColor="White" />
                <AlternatingRowStyle BackColor="#ffe1e9" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </div>
    <div class="col-md-6">
        <div class="panel panel-default">
        <div class="panel-body">
            <h4>
                Images Missing next 48 Hours</h4>
            <asp:GridView ID="grdImages" runat="server" CssClass="table" HeaderStyle-CssClass="text-center"
                DataSourceID="sqlDSImages" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" AllowSorting="true" >
                <HeaderStyle BackColor="#D0021B" ForeColor="White" />
                <AlternatingRowStyle BackColor="#ffe1e9" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select Channel,Category,Genre,[Available Till],[Image Missing],[EpisodicMissing],[MovieMissing] from fpc_v_dashboard where isnull(type,'')='Domestic' order by 3,1"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSInt" runat="server" SelectCommand="select Channel,Category,Genre,[Available Till],[Image Missing],[EpisodicMissing],[MovieMissing] from fpc_v_dashboard where type='International' order by 3,1"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSChannels" runat="server" SelectCommand="select '0' channelid,'--All Channels--' channelidname union select channelid,channelid channelidname from fm_channels where active=1 order by 1"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSLanguage" runat="server" SelectCommand="select 0 languageid,'--All Languages--' fullname union select languageid,fullname from mst_language where active=1 and languageid in( select distinct languageid from fm_channels where active=1 and isnull(languageid,0)<>1) order by 1"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSGenre" runat="server" SelectCommand="select 0 genreid,'--All Genres--' genrename union select genreid,genrename from mst_genre where genrecategory='S' and genreid in (select distinct genreid from fm_channels where active=1 and isnull(isdeleted,0)<>1) order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />

    <asp:SqlDataSource ID="sqlDSLive" runat="server" SelectCommand="select b.ChannelId,b.Progname, convert(varchar, a.Progdate,106) AirDate,convert(varchar,a.Progtime,108) AirTime from mst_epg a join mst_program b on a.progid=b.progid and a.channelid in (select channelid from mst_channel where companyid=5 and onair=1 and active=1 and sendepg=1 and genreid=101) and b.progname like 'Live:%' and a.progdate between convert(varchar,dbo.getlocaldate(),112) and  convert(varchar,dbo.getlocaldate()+2,112) order by a.Progdate,a.Progtime"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSFinal" runat="server" SelectCommand="select b.ChannelId,b.Progname, convert(varchar, a.Progdate,106) AirDate,convert(varchar,a.Progtime,108) AirTime from mst_epg a join mst_program b on a.progid=b.progid and a.channelid in (select channelid from mst_channel where companyid=5 and onair=1 and active=1 and sendepg=1) and b.progname like '%Final%' and a.progdate between convert(varchar,dbo.getlocaldate(),112) and  convert(varchar,dbo.getlocaldate()+6,112) order by b.channelid,a.Progdate,a.Progtime"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSImages" runat="server" SelectCommand="select distinct b.ChannelId,b.Progname from mst_epg a join mst_program b on a.progid=b.progid and a.channelid in (select channelid from mst_channel where companyid=5 and onair=1 and active=1 and sendepg=1) and a.progdate between convert(varchar,dbo.getlocaldate(),112) and  convert(varchar,dbo.getlocaldate()+1,112) and len(isnull(b.programLogo,'')) < 3 order by b.channelid"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />


        

</asp:Content>
