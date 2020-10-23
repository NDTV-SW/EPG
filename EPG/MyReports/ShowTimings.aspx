<%@ Page Title="Change in Show Timings" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="ShowTimings.aspx.vb" Inherits="EPG.ShowTimings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Show Timings</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    ProgID / ProgName</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtProgIds" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                            <label>
                                    Criteria</label>
                                <div class="input-group">
                                    
                                    <div class="form-control">
                                        <asp:RadioButton ID="rbProgId" runat="server" Text="ProgId" Checked="true" GroupName="abc" />
                                        <asp:RadioButton ID="rbProgName" runat="server" Text="Programme" Checked="false" GroupName="abc" />
                                    </div>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" placeholder="Can be left blank" />
                                    <span class="input-group-addon">
                                        <%--<asp:Image ID="imgtxtDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="u" runat="server" />
                                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>--%>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <br />
                            <div class="form-group">
                                
                                <asp:Button ID="btnSearch" CssClass="btn btn-info" runat="server" Text="Search" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="grdImagesReport" CssClass="table" runat="server" ShowFooter="True"
                        CellPadding="4" EmptyDataText="No record found !" ForeColor="Black" GridLines="Vertical"
                        Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                        PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" 
                        Font-Names="Verdana" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
                        BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Size="Larger" 
                            ForeColor="White" />

<PagerSettings NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/Prev.gif"></PagerSettings>

                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
