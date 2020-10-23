<%@ Page Title="Report Images" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="ReportImages.aspx.vb" Inherits="EPG.ReportImages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Report Images</h3>
            </div>
            <div class="panel-body">
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
                            Category</label>
                        <div class="input-group">
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" >
                                <asp:ListItem Text="--Select--" Value="0" />
                                <asp:ListItem Text="L-dim" Value="L-dim" />
                                <asp:ListItem Text="P-dim" Value="P-dim" />
                                <asp:ListItem Text="L-missing" Value="L-missing" />
                                <asp:ListItem Text="P-missing" Value="P-missing" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>
                            Priority Channels</label>
                        <div class="input-group">
                            <asp:CheckBox ID="chkPriority" runat="server" Checked="false" enabled="false" />
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
                 <asp:GridView ID="grdSummary" runat="server" CssClass="table" DataSourceID="SqlDSSummary" BackColor="White"
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
                <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="SqlDS" BackColor="White"
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
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select ChannelId,Progname,Category from fn_images(@startdate,@enddate,@priority,@operatorid) where category= case when @category='0' then category else @category end  order by channelid,progname">
        <SelectParameters>
            <asp:ControlParameter Name="startdate" ControlID="txtStartDate" PropertyName="Text" />
            <asp:ControlParameter Name="enddate" ControlID="txtEndDate" PropertyName="Text" />
            <asp:ControlParameter Name="operatorid" ControlID="ddlOperator" PropertyName="SelectedValue" />
            <asp:ControlParameter Name="priority" ControlID="chkPriority" PropertyName="Checked" />
            <asp:ControlParameter Name="category" ControlID="ddlCategory" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSSummary" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select count(*), category from fn_images(@startdate,@enddate,@priority,@operatorid) group by category">
        <SelectParameters>
            <asp:ControlParameter Name="startdate" ControlID="txtStartDate" PropertyName="Text" />
            <asp:ControlParameter Name="enddate" ControlID="txtEndDate" PropertyName="Text" />
            <asp:ControlParameter Name="operatorid" ControlID="ddlOperator" PropertyName="SelectedValue" />
            <asp:ControlParameter Name="priority" ControlID="chkPriority" PropertyName="Checked" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSOperator" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select operatorid,name from mst_dthcableoperators where operatorid in (214,262,301,222,248,123,187,335) order by 2" />
</asp:Content>
