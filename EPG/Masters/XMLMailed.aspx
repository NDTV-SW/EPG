<%@ Page Title="XML Mailed" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master" CodeBehind="XMLMailed.aspx.vb" Inherits="EPG.XMLMailed" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function openWin(rowid) {
            window.open("EditXMLMailDate.aspx?rowid=" + rowid, "XML Mailed On", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>XML Mailed</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" placeholder="Leave blank for All" />
                                    <span class="input-group-addon">
                                        <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtChannel" FirstRowSelected="true" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table"
                            CellPadding="6" ForeColor="#333333" GridLines="Vertical"  DataKeyNames="XMLFileName">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="RowId" SortExpression="rowid" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRowid" runat="server" Text='<%#bind("rowid") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ChannelID" SortExpression="ChannelID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelID" runat="server" Text='<%#bind("ChannelID") %>' Visible="false" />
                                        <asp:HyperLink ID="hyChannelID" Target="_blank" runat="server" Text='<%#bind("ChannelID") %>' ToolTip="Click to see last 10 XML generated and Mailed" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="XML Generated at" SortExpression="XMLDateTime" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lbXMLDateTime" runat="server" Text='<%#bind("XMLDateTime") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Start Date" SortExpression="XMLDateTime">
                                    <ItemTemplate>
                                        <asp:Label ID="lbStartDate" runat="server" Text="" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="End Date" SortExpression="XMLDateTime">
                                    <ItemTemplate>
                                        <asp:Label ID="lbEndDate" runat="server" Text="" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FileName" SortExpression="XMLFileName" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lbXMLFileName" runat="server" Text='<%#bind("XMLFileName") %>' Visible="false" />
                                        <asp:HyperLink ID="hyXMLFileName" Target="_blank" runat="server" Text='<%#bind("XMLFileName") %>' ToolTip="To save XML right click and select (Save Link as)" />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FTP Date" SortExpression="FTPDateTime" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lbFTPDateTime" runat="server" Text='<%#bind("FTPDateTime") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="XML Generated till MAX" SortExpression="maxdateSend" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lbmaxdateSend" runat="server" Text='<%#bind("maxdateSend") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="XML Missing for" Visible="True">
                                    <ItemTemplate>
                                        <asp:Label ID="lbmissingdates" runat="server" Text='<%#bind("missingdates") %>' />
                                    </ItemTemplate>
                                    <ControlStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" SortExpression="" Visible="True">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyXMLMailedOn" runat="server" Text='Edit' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
