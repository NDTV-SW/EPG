<%@ Page Title="Program Images Uploaded" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="ProgramImagesUploaded.aspx.vb" Inherits="EPG.ProgramImagesUploaded" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sc" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Program Images Uploaded</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    From Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" placeholder="Can't be left blank" />
                                   <span class="input-group-addon">
                                        <asp:Image ID="imgtxtFromDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CEtxtFromDate" runat="server" TargetControlID="txtFromDate" PopupButtonID="imgtxtFromDate" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    To Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" placeholder="Can't be left blank" />
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgtxtToDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CEtxtToDate" runat="server" TargetControlID="txtToDate" PopupButtonID="imgtxtToDate" />
                                    </span>
                                </div>
                            </div>
                        </div>
                         <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    User/Channel-Wise Count</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkUserChannelWiseCount" runat="server" GroupName="radio" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    User-Wise Count</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkUserWiseCount" runat="server" GroupName="radio" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Portrait</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkPortrait" runat="server"  />
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Yupp Channel Count</label>
                                <div class="input-group">--%>
                                    <asp:CheckBox ID="chkYupp" runat="server" GroupName="radio" Visible="false" />
                                <%--</div>
                            </div>
                        </div>--%>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
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
                        <Columns>
                               <asp:TemplateField HeaderText="#">
                                   <ItemTemplate>
                                       <%#Container.DataItemIndex+1 %>
                                   </ItemTemplate>
                               </asp:TemplateField>
                        </Columns>
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
