<%@ Page Title="Channels FPC Distribution" Language="vb" MasterPageFile="SiteStar.Master"
    AutoEventWireup="false" CodeBehind="FPC_Distribution_Channels.aspx.vb" Inherits="EPG.FPC_Distribution_Channels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading">
            <h2>
                FPC DISTRIBUTION CHANNELS
            </h2>
        </div>
        <div class="panel-body">
            <table class="table table-bordered table-hover">
                <tr>
                    <th class="alert-success">
                        Client
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control" AutoPostBack="true"
                            DataSourceID="sqlds" DataTextField="clientname" DataValueField="id" />
                    </td>
                    <th class="alert-success">
                        Channelid
                    </th>
                    <td>
                        <asp:TextBox ID="txtChannelid" runat="server" CssClass="form-control" />
                        <asp:AutoCompleteExtender ID="ACE_txtChannelid" runat="server" ServiceMethod="SearchChannel"
                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                            TargetControlID="txtChannelid" FirstRowSelected="true" />
                    </td>
                    <th class="alert-success">
                        Service ID
                    </th>
                    <td>
                        <asp:TextBox ID="txtServiceID" runat="server" CssClass="form-control" />
                    </td>
                    <th class="alert-success">
                        Channel Display Name
                    </th>
                    <td>
                        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" />
                    </td>
                    <th class="alert-success">
                        File Naming Format
                    </th>
                    <td>
                        <asp:TextBox ID="txtFileNaming" runat="server" CssClass="form-control" />
                    </td>
                    <th class="alert-success">
                        Onair
                    </th>
                    <td>
                        <asp:CheckBox ID="chkOnair" runat="server" Checked="true" />
                    </td>
                    <th class="alert-success">
                        <asp:Button ID="btnSave" runat="server" Text="SAVE" />
                        <asp:Label ID="lbID" runat="server" Visible="false" />
                    </th>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlDSGrd" 
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4" ForeColor="Black" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" ShowSelectButton="true" SelectImageUrl="~/Images/edit.png" />
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                        <asp:Label ID="lbID" runat="server" Text='<%#Bind("id") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select id,client, client + ' (' + convert(varchar(10),id) + ')' clientname from fpc_distribution where active=1 order by 2" />
    <asp:SqlDataSource ID="sqlDSGrd" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select   id,ClientId,ChannelId,ServiceID,DisplayName,FileNaming,OnAir,LastUpdate from fpc_distribution_channels where clientid=@id order by onair desc,channelid">
        <SelectParameters>
            <asp:ControlParameter Name="id" ControlID="ddlClient" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
