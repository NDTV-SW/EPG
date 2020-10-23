<%@ Page Title="Upcoming Notifications" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="NotificationsUpcoming.aspx.vb" Inherits="EPG.NotificationsUpcoming" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
     <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                  Upcoming Notifications</h3>
            </div>
            <div class="panel-body">
        <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceId="sqlds" 
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" ForeColor="Black" GridLines="Vertical" EmptyDataText="-- No Upcoming Notifications --" EmptyDataRowStyle-CssClass="h4 text-center alert alert-danger" >
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
                        <%# Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        </div>
        
        <asp:SqlDataSource ID="sqlds" runat="server" SelectCommand="select Title,Body,Category,Languagekeyword Languages,PublishAt,KillTime from mst_notifications where notificationtype='auto' and killtime>dbo.getlocaldate() order by killtime"
         ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    </div>
</asp:Content>
