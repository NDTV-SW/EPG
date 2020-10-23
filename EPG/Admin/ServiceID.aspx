<%@ Page Title="Update Service ID" Language="vb" MasterPageFile="SiteAdmin.Master"
    AutoEventWireup="false" CodeBehind="ServiceID.aspx.vb" Inherits="EPG.ServiceID" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Update ServiceID</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Channel</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFV_txtChannel" runat="server" ControlToValidate="txtChannel" ForeColor="Red" Text="*" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                ServiceID</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtServiceID" runat="server" CssClass="form-control"/>
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFV_txtServiceID" runat="server" ControlToValidate="txtServiceID" ForeColor="Red" Text="*" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                AirtelMail</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkAirtelMail" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                AirtelFTP</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkAirtelFTP" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Channel_Genre</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlChannelGenre" runat="server" CssClass="form-control" DataSourceID="sqlDSChannelGenre" DataValueField="channel_genre" DataTextField="channel_genre" />
                                <asp:SqlDataSource ID="sqlDSChannelGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"  SelectCommand="select distinct channel_genre from mst_channel order by 1" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-info" Text="Update" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grd" runat="server" BackColor="White" BorderColor="#DEDFDE" CssClass="table"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        EmptyDataText="No record Found" EmptyDataRowStyle-CssClass="alert alert-danger">
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
                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/edit.png" ShowSelectButton="true" />
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbChannelid" runat="server" Visible="false" Text='<%#eval("channelid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
