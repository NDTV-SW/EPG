<%@ Page Title="Copy Programme Logos" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="CopyProgrammeLogos.aspx.vb" Inherits="EPG.CopyProgrammeLogos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function source() {
            var e = document.getElementById("MainContent_ddlSource");
            var strUser = e.options[e.selectedIndex].value;
            var imgSrc = document.getElementById("imgSrc");
            imgSrc.src = "http://epgops.ndtv.com/uploads/" + strUser;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />

    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>Copy Programme Logos</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="row">
                                <div class="form-group">
                                    <label>
                                        Source Programme</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" onchange="JavaScript:source();" />
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row text-center">
                                <img id="imgSrc" width="200px" height="200px" alt="Source Image" src="" />
                            </div>
                            <div class="row text-center">
                                <br />

                            </div>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search channel or programme" />
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-8 col-xs-8 text-left">
                                <asp:CheckBox ID="chkExact" runat="server" Text="Exact" Checked="false" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" />
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-8 col-xs-8 text-right">
                                <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="btn btn-success" />
                            </div>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table" OnSorting="SortRecords"
                                AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                PageSize="100" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ProgId" SortExpression="progid">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbProgId" Text='<%# Bind("progid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("ChannelId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Programme" SortExpression="progname">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbProgName" Text='<%# Bind("progname") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Logo" SortExpression="programlogo">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbProgramLogo" Text='<%# Bind("programlogo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Copy">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCopy" runat="server" />
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

                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        source();
    </script>
</asp:Content>
