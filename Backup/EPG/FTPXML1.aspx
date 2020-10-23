<%@ Page Title="FTP XML" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="FTPXML1.aspx.vb" Inherits="EPG.FTPXML1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="container">--%>
    <div class="container">
        <div class="panel-body">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                            FTP XMLs</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                            <div class="btn-lg alert-danger">
                                <b>Note :</b> Channels in red row are <b>not POWERED</b> by NDTV.
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                            <asp:Button ID="btnFTP1" Text="FTP Selected" CssClass="btn btn-info" runat="server"
                                Visible="True" />
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record found!"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowSorting="True" EmptyDataRowStyle-CssClass="alert-info text-center h3"
                                DataKeyNames="ChannelID" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ChannelID" SortExpression="ChannelID" Visible="True">
                                        <ItemTemplate>
                                            <asp:Label ID="lbChannelID" runat="server" Text='<%#bind("ChannelID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="XmlDateTime" SortExpression="XmlDateTime" Visible="True">
                                        <ItemTemplate>
                                            <asp:Label ID="lbXmlDateTime" runat="server" Text='<%#bind("XmlDateTime") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="XmlFileName" SortExpression="XmlFileName">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyXmlFileName" runat="server" Target="_blank" Text='<%#bind("XmlFileName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date" SortExpression="Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbStartDate" runat="server" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date" SortExpression="End Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbEndDate" runat="server" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="True">
                                        <ItemTemplate>
                                            <asp:Label ID="lbRowId" runat="server" Text='<%#bind("RowId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FTP" SortExpression="AirtelFTP" Visible="True">
                                        <ItemTemplate>
                                            <asp:Label ID="lbAirtelFTP" runat="server" Text='<%#bind("AirtelFTP") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mail" SortExpression="AirtelMail" Visible="True">
                                        <ItemTemplate>
                                            <asp:Label ID="lbAirtelMail" runat="server" Text='<%#bind("AirtelMail") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CatchupFlag" SortExpression="CatchupFlag" Visible="True">
                                        <ItemTemplate>
                                            <asp:Label ID="lbCatchupFlag" runat="server" Text='<%#bind("CatchupFlag") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FTP" SortExpression="CreationTime">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFTP" runat="server" Checked="false" />
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
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                            <asp:Button ID="btnFTP" Text="FTP Selected" CssClass="btn btn-info" runat="server"
                                Visible="True" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
