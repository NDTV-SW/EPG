<%@ Page Title="Notifications" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteLess.Master"
    CodeBehind="notificationSelectProg.aspx.vb" Inherits="EPG.notificationSelectProg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function setProgid() {
            var e = document.getElementById("MainContent_ddlProgramme");
            var strProgId = e.options[e.selectedIndex].value;

            var f = window.opener.document.getElementById("MainContent_ddlCategory");
            var strType = f.options[f.selectedIndex].value;
            
            window.opener.document.getElementById('MainContent_txtURL').value
            //alert(strUser);
            
            window.opener.document.getElementById('MainContent_txtURL').value = 'http://search-ureqa-bulk6ohxl52baeqfsacpdzidra.ap-southeast-1.es.amazonaws.com/' + strType + '/all/_search?sort=airtime&size=1&q=' + strProgId;
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-5 col-xs-5">
                <div class="form-group">
                    <label>
                        Select Channel</label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlChannel" runat="server" DataTextField="channelid" DataValueField="channelid"
                            DataSourceID="SqlmstChannel" CssClass="form-control" AutoPostBack="true" />
                        <asp:SqlDataSource ID="SqlmstChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="SELECT channelid FROM mst_channel where onair=1 and publicchannel=1 order by 1">
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-5 col-xs-5">
                <div class="form-group">
                    <label>
                        Select Programme</label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlProgramme" runat="server" DataTextField="Progname" DataValueField="Progid"
                            DataSourceID="sqlMstProgram" CssClass="form-control" AutoPostBack="true" />
                        <asp:SqlDataSource ID="sqlMstProgram" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="SELECT progid,Progname from mst_program where active=1 and channelid=@channelid and progid in (select progid from mst_epg where channelid=@channelid and convert(varchar, progdate,112) >= convert(varchar, dbo.getLocalDate(),112) ) order by progname">
                            <SelectParameters>
                                <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue"
                                    Direction="Input" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-2 col-xs-2">
                <div class="form-group">
                    <label>
                        &nbsp;</label>
                    <div class="input-group">
                        <asp:HyperLink ID="hySelect" runat="server" onclick="javascript:setProgid();" Text="Select"
                            CssClass="btn btn-warning" />
                    </div>
                </div>
            </div>
        </div>
        <asp:GridView ID="grd" runat="server" DataSourceID="sqlDSGrd" CssClass="table" />
        <asp:SqlDataSource ID="sqlDSGrd" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
            SelectCommand="SELECT top 10 a.progid,convert(varchar,b.Progdate,106) + ' '+convert(varchar,b.Progtime,108) AirTime from mst_program a join mst_epg b on a.progid=b.progid where convert(varchar,b.progdate,112)>=convert(varchar,dbo.getlocaldate(),112) and a.progid=@progid">
            <SelectParameters>
                <asp:ControlParameter Name="progid" ControlID="ddlProgramme" PropertyName="SelectedValue"
                    Direction="Input" Type="Int64" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
