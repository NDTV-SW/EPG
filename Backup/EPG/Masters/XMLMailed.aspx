<%@ Page Title="XML Mailed" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="XMLMailed.aspx.vb" Inherits="EPG.XMLMailed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
            function openWin(rowid) {
                window.open("EditXMLMailDate.aspx?rowid=" + rowid, "XML Mailed On", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
            }
            function openXmlMailed(channelid) {
                window.open("showXMLMailed.aspx?channelid=" + channelid, "Channel XMl Mailed", "width=950,height=550,toolbar=0,left=150,top=150,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
            }   
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceid="SqlDsXMLMailed" 
            CellPadding="6" ForeColor="#333333" GridLines="Vertical" AllowSorting="true" DataKeyNames="XMLFileName">
            <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <asp:Label ID="lbSno" runat="server" />
                </ItemTemplate>
                <ControlStyle Width="50px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="RowId" SortExpression="rowid" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lbRowid" runat="server" Text='<%#bind("rowid") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="ChannelID" SortExpression="ChannelID">
                <ItemTemplate>
                    <asp:Label ID="lbChannelID" runat="server" Text='<%#bind("ChannelID") %>' visible="false"/>
                    <asp:HyperLink ID="hyChannelID" Target="_blank" runat="server" Text='<%#bind("ChannelID") %>' ToolTip="Click to see last 10 XML generated and Mailed"/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="XML Generated at" SortExpression="XMLDateTime" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbXMLDateTime" runat="server" Text='<%#bind("XMLDateTime") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Start Date" SortExpression="XMLDateTime">
                <ItemTemplate>
                    <asp:Label ID="lbStartDate" runat="server" Text=""/>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="End Date" SortExpression="XMLDateTime">
                <ItemTemplate>
                    <asp:Label ID="lbEndDate" runat="server" Text=""/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="FileName" SortExpression="XMLFileName" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbXMLFileName" runat="server" Text='<%#bind("XMLFileName") %>' Visible="false"/>
                    <asp:HyperLink ID="hyXMLFileName" Target="_blank" runat="server" Text='<%#bind("XMLFileName") %>' ToolTip="To save XML right click and select (Save Link as)"/>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="FTP Date" SortExpression="FTPDateTime" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbFTPDateTime" runat="server" Text='<%#bind("FTPDateTime") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="XML Generated till MAX" SortExpression="maxdateSend" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbmaxdateSend" runat="server" Text='<%#bind("maxdateSend") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="XML Missing for" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbmissingdates" runat="server" Text='<%#bind("missingdates") %>'/>
                </ItemTemplate>
                <ControlStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" SortExpression="" Visible="True">
                <ItemTemplate>
                    <asp:HyperLink ID="hyXMLMailedOn" runat="server" Text='Edit'/>
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
    <asp:SqlDataSource ID="SqlDsXMLMailed" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
        SelectCommand="Select X.Rowid , X.ChannelId, XmlDateTime, XmlFilename,FTPDateTime,
        maxdateSend=(select max(epgdate) from aud_epg_xml_ftp where ChannelId=x.ChannelId  and xmlfilename is not null ) ,
        dbo.fn_missing_dates (X.ChannelID,(select max(epgdate) from aud_epg_xml_ftp where ChannelId=x.ChannelId  and xmlfilename is not null )) missingdates
        From aud_epg_xml_ftp X,
        (Select max(rowid) RowId, ChannelId from aud_epg_xml_ftp where xmlfilename is not null Group by ChannelId) Y 
        where X.ChannelId = Y.ChannelId And X.RowId = Y.RowId 
        and xmlfilename is not null 
        and X.ChannelId in (Select ChannelId from mst_channel where onair = 1 and ( airtelftp=1 or airtelmail=1))
        order by X.Rowid desc" />
        
</asp:Content>
