<%@ Page Title="Dictionary Master" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="DictionaryMaster.aspx.vb" Inherits="EPG.DictionaryMaster"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Dictionary Master</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select Language</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" AutoCompleteMode="SuggestAppend"
                                        DropDownStyle="DropDownList" ItemInsertLocation="Append" AppendDataBoundItems="true"
                                        AutoPostBack="true" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Fetch and Feed" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="grdDictionary" runat="server" EmptyDataRowStyle-ForeColor="Red"
                            EmptyDataText="No record Found." CellPadding="4" DataSourceID="sqlDSDictionary"
                            CssClass="table" ForeColor="Black" GridLines="Vertical" Width="100%" AllowSorting="True"
                            SortedAscendingHeaderStyle-CssClass="sortasc-header" 
                            SortedDescendingHeaderStyle-CssClass="sortdesc-header" BackColor="White" 
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataRowStyle ForeColor="Red"></EmptyDataRowStyle>

                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlDSDictionary" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="rpt_mst_dictionary" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlLanguage" Direction="Input" Name="Languageid"
                                    Type="Int64" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
