<%@ Page Language="vb" Title="Show XML Mailed"  AutoEventWireup="false"  CodeBehind="showXMLMailed.aspx.vb" Inherits="EPG.showXMLMailed" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>Select Employee</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function pageLoad() {
            if (document.getElementById('hfSend').value == "2") {
                //window.opener.location = window.opener.location.href = "xmlmailed.aspx";
                self.close();
                window.close();
                if (window.opener && !window.opener.closed) {
                }
                return false;
            }
        }

        function winclose(channelid) {
            try {

            }
            catch (err) { }
            window.opener.location = window.opener.location.href = "ProgramCentralEdit.aspx?channelid='" + channelid + "'";
            //window.close()
            //window.opener.window.location.href += "?update=done";
            //window.opener.window.location.reload();
            self.close();
            window.close();
            if (window.opener && !window.opener.closed) {

                //window.opener.location.reload();
            }
            // window.onunload = function (e) {
            //   opener.gvTransactionsBind(); //or
            // opener.document.getElementById('someid').innerHTML = 'update content of parent window';
            //};
            return false;

            //popup_window = window.open("");
            // popup_window.close ();
        }
    </script>
    
    <style type="text/css">
        #lbLabel
        {
            width: 23px;
        }
        #btnCount
        {
            width: 47px;
        }
    </style>
    
</head>
<body onload="javascript:pageLoad();">
    <form id="Form1" runat="server">
    <div class="page1">
        <div class="header">
             <div class="title">
                <h2><asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Images/epg7.png" Width="180px"/>
                    &nbsp;&nbsp;&nbsp;Show XML Mailed
                </h2>
            </div>
            
            <div class="clear">
                
            </div>
        </div>
        <div class="main1">
        <div align= "center">
    <table width="99%" class="whitetable" border="1px" cellpadding="2px">
        <tr>
            <th align="right">
                <h2>
                    Channel
                </h2>
            </th>
            <td align="left">
                <h2>
                    <asp:Label ID="lbChannel" runat="server" Text="Channel" />
                </h2>
            </td>
        </tr>
        <tr>
            
        
            
            <td colspan="2" align="left">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="6" ForeColor="#333333" GridLines="Vertical" AllowSorting="true">
            <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="ChannelID">
                <ItemTemplate>
                    <asp:Label ID="lbChannelID" runat="server" Text='<%#bind("ChannelID") %>'/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Min XML Generated" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbminXMLDateTime" runat="server" Text='<%#bind("minXMLDateTime") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Min FTP Time" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbminFTPDateTime" runat="server" Text='<%#bind("minFTPDateTime") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Max XML Generated" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbmaxXMLDateTime" runat="server" Text='<%#bind("maxXMLDateTime") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Max FTP Time" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbmaxFTPDateTime" runat="server" Text='<%#bind("maxFTPDateTime") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date">
                <ItemTemplate>
                    <asp:Label ID="lbStartDate" runat="server" Text=""/>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="End Date">
                <ItemTemplate>
                    <asp:Label ID="lbEndDate" runat="server" Text=""/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="FileName" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbXMLFileName" runat="server" Text='<%#bind("XMLFileName") %>' Visible="false"/>
                    <asp:HyperLink ID="hyXMLFileName" Target="_blank" runat="server" Text='<%#bind("XMLFileName") %>' ToolTip="To save XML right click and select (Save Link as)"/>
                </ItemTemplate>
            </asp:TemplateField>

            

            

            <asp:TemplateField HeaderText="Min Mailed Date" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lbMinXMLMailedOn" runat="server" Text='<%#bind("minxmlmailedon") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Max Mailed Date" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lbMaxXMLMailedOn" runat="server" Text='<%#bind("maxxmlmailedon") %>'/>
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
            </td>
        </tr>
         <tr>
            <td colspan="5" align="center">
                
                <asp:Button ID="btnClose" runat="server" Text="Close" Width="70px" />
                
                 <input type="hidden" id="hfSend" runat="server" />   
                </td>
             
        </tr>
        </table>
    </div>
               
        </div>

      
        
    </div>
    </form>
</body>
</html>





