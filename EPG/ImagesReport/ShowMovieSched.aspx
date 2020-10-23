<%@ Page Title="Show Movie Schedule" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteLess.Master"
    CodeBehind="ShowMovieSched.aspx.vb" Inherits="EPG.ShowMovieSched" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="hfSend" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Movie Airing Schedule</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grdProgSched" runat="server" CssClass="table" GridLines="Vertical"
                        EmptyDataText="No record found ...." CellPadding="4" ForeColor="Black" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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
                    </asp:GridView>
                </div>
                <%--<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                    <asp:Button ID="btnClose" CssClass="btn btn-danger" runat="server" Text="Close" />
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
