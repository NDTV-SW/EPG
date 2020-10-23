<%@ Page Title="Script Images Report" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="ScriptImagesReport.aspx.vb" Inherits="EPG.ScriptImagesReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Script Images Report</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>
                                Operator</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control" DataSourceID="sqlDSOperator"
                                    DataTextField="name" DataValueField="operatorid" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
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
                    <div class="col-md-2">
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
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>
                                Portrait</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkPortrait" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:Button ID="btnView" runat="server" CssClass="btn btn-info" Text="Refresh" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="SqlDS" BackColor="White"
                        ShowFooter="true" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btn" runat="server" CssClass="btn btn-info" Text="View" CommandName="view"
                                        CommandArgument='<%# Eval("channelid")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-md-6">
                    <asp:GridView ID="grdReport" runat="server" CssClass="table" BackColor="White" ShowFooter="true"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                        GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select count(*) cnt,a.channelid,cast(b.trp as integer) TRP from mst_program a join mst_channel b on a.channelid=b.channelid where case when @portrait=1 then programlogoportrait else  programlogo end like '%script%' and progid in (select progid from mst_epg where progdate between @startdate and @enddate) and a.channelid in (select channelid from dthcable_channelmapping where operatorid=@operatorid and onair=1) group by a.channelid,b.trp order by 3 desc,1 desc,2">
        <SelectParameters>
            <asp:ControlParameter Name="startdate" ControlID="txtStartDate" PropertyName="Text" />
            <asp:ControlParameter Name="enddate" ControlID="txtEndDate" PropertyName="Text" />
            <asp:ControlParameter Name="operatorid" ControlID="ddlOperator" PropertyName="SelectedValue" />
            <asp:ControlParameter Name="portrait" ControlID="chkPortrait" PropertyName="Checked" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSOperator" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select operatorid,name from mst_dthcableoperators where operatorid in (214,232,222,248,123) order by 2" />
</asp:Content>
