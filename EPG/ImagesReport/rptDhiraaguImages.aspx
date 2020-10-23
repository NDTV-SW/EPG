<%@ Page Title="Dhiraagu Images Report" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="rptDhiraaguImages.aspx.vb" Inherits="EPG.rptDhiraaguImages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Client/Channel Wise Images Report</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Operator</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control" DataSourceID="sqlDSOperator"
                                    DataTextField="name" DataValueField="operatorid" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>
                                Select Days</label>
                            <div class="input-group">
                                <asp:RadioButtonList ID="rblDays" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Text="Today/Tomorrow" Value="2" />
                                    <asp:ListItem Text="Next 3 days" Value="3" />
                                    <asp:ListItem Text="Next 4 days" Value="4" />
                                    <asp:ListItem Text="Next 5 days" Value="5" />
                                    <asp:ListItem Text="Next 6 days" Value="6" />
                                    <asp:ListItem Text="Next 7 days" Value="7" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdImagesReport" runat="server" CssClass="table" AutoGenerateColumns="True"
                    ShowFooter="True" DataSourceID="sqlDSImagesReport" CellPadding="4" EmptyDataText="No record found !"
                    ForeColor="#333333" GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                    SortedDescendingHeaderStyle-CssClass="sortdesc-header" ShowHeader="True" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                    PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Font-Bold="true" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.no.">
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSImagesReport" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="rptImagesDynamic" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter Name="days" ControlID="rblDays" DbType="Int16" PropertyName="SelectedValue" />
            <asp:ControlParameter Name="operatorid" ControlID="ddlOperator" DbType="Int16" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSOperator" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select operatorid,name from mst_dthcableoperators where operatorid in (301) order by 2" />
</asp:Content>
