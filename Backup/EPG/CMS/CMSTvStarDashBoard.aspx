<%@ Page Title="TV Star Dashboard" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="CMSTvStarDashBoard.aspx.vb" Inherits="EPG.CMSTvStarDashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .noFormatting
        {
            text-decoration: none;
        }
    </style>
    <script>
        function openWin(profileid, progid, langid,mode,controlid) {
            window.open("EditTVStarDashboard.aspx?profileid=" + profileid + "&progid=" + progid + "&langid=" + langid + "&mode=" + mode + "&controlid=" + controlid, "Edit TV Star", "width=1000,height=500,toolbar=0,left=150,top=150,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
        <div class="container">
            <h3>TV Star Dashboard</h3>
            <div class="row">
                <asp:GridView ID="grdTvStarDashboard" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                    CssClass="table" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" PageSize="200"
                    AllowPaging="true" BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true"
                    EmptyDataText="----No Record Found----" EmptyDataRowStyle-HorizontalAlign="Center">
                    <AlternatingRowStyle BackColor="White" />
                    
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.no.">
                            <ItemTemplate>
                                <asp:Label ID="lbSno" runat="server"/>
                                <asp:Label ID="lbRowid" runat="server" Text='<%#Eval("rowid") %>' Visible="false" />
                                <asp:Label ID="lbProfileId" runat="server" Text='<%#Eval("profileid") %>' Visible="false" />
                                <asp:Label ID="lbProgId" runat="server" Text='<%#Eval("progid") %>' Visible="false" />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Channel" DataField="channelid" />
                        <asp:BoundField HeaderText="Programme" DataField="progname" />
                        
                         <asp:TemplateField HeaderText="English Name">
                            <ItemTemplate>
                                <asp:Label ID="lbEngProfileName" runat="server" Text='<%#Eval("engprofilename") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Eng Role" DataField="engrolename" />
                        <asp:BoundField HeaderText="Eng Desc" DataField="engroledesc" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyEngEdit" runat="server" Text="Edit" />
                            </ItemTemplate> 
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Hin Name">
                            <ItemTemplate>
                                <asp:Label ID="lbHinProfileName" runat="server" Text='<%#Eval("hinprofilename") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Hin Role" DataField="hinrolename" />
                        <asp:BoundField HeaderText="Hin Desc" DataField="hinroledesc" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyHinEdit" runat="server" Text="Edit" />
                            </ItemTemplate> 
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tam Name">
                            <ItemTemplate>
                                <asp:Label ID="lbTamProfileName" runat="server" Text='<%#Eval("tamprofilename") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Tam Role" DataField="tamrolename" />
                        <asp:BoundField HeaderText="Tam Desc" DataField="tamroledesc" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hyTamEdit" runat="server" Text="Edit" />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Tel Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbTelProfileName" runat="server" Text='<%#Eval("telprofilename") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Tel Role" DataField="telrolename" Visible="false" />
                        <asp:BoundField HeaderText="Tel Desc" DataField="telroledesc" Visible="false" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyTelEdit" runat="server" Text="Edit" />
                            </ItemTemplate> 
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mar Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbMarProfileName" runat="server" Text='<%#Eval("marprofilename") %>' />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Mar Role" DataField="marrolename" Visible="false" />
                        <asp:BoundField HeaderText="Mar Desc" DataField="marroledesc" Visible="false" />
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyMarEdit" runat="server" Text="Edit" />
                            </ItemTemplate> 
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
