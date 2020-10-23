<%@ Page Title="Yupp TV Regional Movie Images Status" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="rptYuppMovieImages.aspx.vb" Inherits="EPG.rptYuppMovieImages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Yupp TV Regional Movie Images Status</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Select Days</label>
                            <div class="input-group">
                                <asp:RadioButtonList ID="rblDays" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"> 
                                    <asp:ListItem Selected="True" Text="Today/Tomorrow" Value="2" />
                                    <asp:ListItem Text="Next 7 days" Value="7" />
                                    <asp:ListItem Text="After 7 days" Value="8" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <fieldset class="table">
                    <legend>Movie Channels</legend>
                
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
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    </fieldset>
                <fieldset class="table">
                    <legend>Movies on Regional GEC Channels</legend>
                <asp:GridView ID="grdGECImagesReport" runat="server" CssClass="table" AutoGenerateColumns="True"
                    ShowFooter="True" DataSourceID="sqlDSGECImagesReport" CellPadding="4" EmptyDataText="No record found !"
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
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    </fieldset>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSImagesReport" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="rptYuppMovieImages" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter Name="days" ControlID="rblDays" DbType="Int16" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSGECImagesReport" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="rptYuppGECImages" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter Name="days" ControlID="rblDays" DbType="Int16" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
