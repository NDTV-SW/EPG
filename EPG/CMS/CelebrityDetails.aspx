<%@ Page Language="vb" Title="Celebrity Details"  AutoEventWireup="false"  CodeBehind="CelebrityDetails.aspx.vb" Inherits="EPG.CelebrityDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>Select Employee</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    
    <link href="../lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../lightbox.js" type="text/javascript"></script>    

    <script type="text/javascript">
        function close() {
                //self.close();
           
        }
        
    </script>
    
    <style type="text/css">
        #lbLabel
        {
            width: 38px;
        }
        #btnCount
        {
            width: 47px;
        }
    </style>
    
</head>
<body onload="javascript:pageLoad();">
    <form id="Form1" runat="server">
                
                 <input type="hidden" id="hfSend" runat="server" />   
    <div class="page1">
        <div class="header">
             <div class="title">
                <h2><asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Images/epg7.png" Width="180px"/>
                    &nbsp;&nbsp;&nbsp;Celebrity Details
                </h2>
            </div>
        </div>    
        
    <table width="99%" class="whitetable" border="1px" cellpadding="2px">
        <tr>
            <td>
                <h2><asp:Label ID="lbCelebrityName" runat="server" Text="Celebrity" /></h2>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="grdCelebDetails" runat="server" BackColor="White"  AutoGenerateColumns="true"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" EmptyDataText="No record found..." >
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
                <br />
                <asp:Label ID="lbCelebId" runat="server" Visible="False" />
                <asp:Label ID="lbMode" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>





