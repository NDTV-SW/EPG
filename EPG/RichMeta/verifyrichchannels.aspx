<%@ Page Title="Verify Rich Meta" Language="vb" AutoEventWireup="false" MasterPageFile="~/RichMeta/SiteRich.Master"
    CodeBehind="verifyrichchannels.aspx.vb" Inherits="EPG.verifyrichchannels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function openWin(richmetaid) {
            window.open("viewrich.aspx?id=" + richmetaid, "View Rich", "width=1000,height=480,toolbar=0,left=100,top=100,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">Verify Rich Channels</li>
        </ol>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    Channel</label>
                <div class="input-group">
                    <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" DataSourceID="sqlDSChannel"
                        DataTextField="channelname" DataValueField="channelid" />
                    <asp:SqlDataSource ID="sqlDSChannel" runat="server" SelectCommand="select '0' channelid,'-- select --' channelname union select channelid,channelid channelname from mst_channel where onair=1 and companyid in (5,189) and genreid<>101 and genreid<>102 order by 1"
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                    <span class="input-group-addon"></span>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    Start Date</label>
                <div class="input-group">
                    <asp:TextBox ID="txtStartDate" placeholder="Start Date" runat="server" CssClass="form-control"
                        ValidationGroup="RFGViewEPG" />
                    <span class="input-group-addon">
                        <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                        <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate"
                            PopupButtonID="imgStartDateCalendar" />
                        <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate"
                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGViewEPG" />
                    </span>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    End Date</label>
                <div class="input-group">
                    <asp:TextBox ID="txtEndDate" placeholder="End Date" runat="server" CssClass="form-control"
                        ValidationGroup="RFGViewEPG" />
                    <span class="input-group-addon">
                        <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                        <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate"
                            PopupButtonID="imgEndDateCalendar" />
                        <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server"
                            ForeColor="Red" Text="*" ValidationGroup="RFGViewEPG" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate"
                            ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date"
                            ErrorMessage="*" ValidationGroup="RFGViewEPG" />
                    </span>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>
                    &nbsp;</label>
                <div class="input-group">
                    <asp:Button ID="btnView" runat="server" Text="View" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-striped"
            DataSourceID="sqlDS" EmptyDataText="--no record found--" DataKeyNames="richmetaid" AllowSorting="true"
            AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
            SelectedRowStyle-BackColor="#D0021B" SelectedRowStyle-ForeColor="#ffffff">
            <Columns>
                <asp:CommandField ButtonType="Image" ShowSelectButton="true" SelectImageUrl="~/Images/edit.png" />
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="progid" HeaderText="ProgId" />
                <asp:BoundField DataField="progname" HeaderText="Program" />
                <asp:BoundField DataField="genre" HeaderText="Genre" />
                <asp:BoundField DataField="subgenre" HeaderText="Sub-Genre" />
                <asp:BoundField DataField="ratingid" HeaderText="Rating" />
                <asp:BoundField DataField="ismovie" HeaderText="Is Movie" />
                <asp:TemplateField HeaderText="View Rich" SortExpression="verified">
                    <ItemTemplate>
                        <asp:HyperLink ID="hyRichMetaId" runat="server" Text='<%# Bind("richmetaid") %>' />
                        <asp:CheckBox ID="chkVerified" runat="server" Checked='<%# Bind("verified") %>' Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="img" runat="server" Width="144px" Height="72px" ImageUrl='<%# Bind("programlogo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select progid,progname,genre=(select genrename from mst_genre where genreid=x.genreid),subgenre=(select subgenrename from mst_subgenre where subgenreid=x.subgenreid),ratingid,'http://epgops.ndtv.com/uploads/' + programlogo programlogo,ismovie,isnull(richmetaid,0),verified=isnull((select verified from richmeta where id=isnull(x.richmetaid,0)),0), richmetaid from mst_program x where channelid=@channelid and  progid in (select distinct progid from mst_epg where channelid=@channelid and progdate between @startdate and @enddate) order by progname"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue" />
            <asp:ControlParameter Name="startdate" ControlID="txtStartDate" PropertyName="Text" />
            <asp:ControlParameter Name="enddate" ControlID="txtEndDate" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
