<%@ Page Title="Channel Rename" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="ChannelRename.aspx.vb" Inherits="EPG.ChannelRename" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>Channel Rename</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Select Channel</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtChannelName" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                New Channel Name</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtNewChannelName" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 text-right ">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info" /> &nbsp; &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                    </div>
                </div>
            </div>


            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grd" runat="server" DataSourceID="sqlDS" CellPadding="4"
                        ForeColor="Black" AutoGenerateColumns="True"
                        CssClass="table" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="50"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                    <asp:Label ID="lbRowId" runat="server" Text='<%# eval("RowId") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/images/delete.png" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                        SelectCommand="select * from mst_channelrename order by rowid desc">
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
