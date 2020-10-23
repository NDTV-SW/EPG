<%@ Page Language="vb" Title="Edit Programme"  AutoEventWireup="false"  CodeBehind="EditXMLMailDate.aspx.vb" Inherits="EPG.EditXMLMailDate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>Select Employee</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function pageLoad() {
            if (document.getElementById('hfSend').value == "2") {
                var qs = getQueryStrings();
                var rowid = qs["rowid"];
                
                window.opener.location = window.opener.location.href = "xmlMailed.aspx";
                self.close();
                window.close();
                if (window.opener && !window.opener.closed) {
                }
                return false;
            }
        }
        function getQueryStrings() {
            var assoc = {};
            var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
            var queryString = location.search.substring(1);
            var keyValues = queryString.split('&');

            for (var i in keyValues) {
                var key = keyValues[i].split('=');
                if (key.length > 1) {
                    assoc[decode(key[0])] = decode(key[1]);
                }
            }

            return assoc;
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
    
</head>
<body onload="javascript:pageLoad();">
    <form id="Form1" runat="server">
    <div class="page1">
        <div class="header">
             <div class="title">
                <h2><asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Images/epg7.png" Width="180px"/>
                    &nbsp;&nbsp;&nbsp;XML mailed on
                </h2>
            </div>
            
            <div class="clear">
                
            </div>
        </div>
        <div class="main1">
        <div align= "center">
    <table width="99%" class="whitetable" border="1px" cellpadding="2px">
        
        <tr>
        <th>
         Select Date
                </th>
                <td align="left">
                    <asp:TextBox ID="txtXMLMailedOn" runat="server" Width="200px"/>
                    <asp:CalendarExtender ID="CEtxtXMLMailedOn" DefaultView="Days" Format="MM/dd/yyyy"  Enabled="True" PopupPosition="BottomLeft" runat="server" TargetControlID="txtXMLMailedOn" />
                    <asp:RequiredFieldValidator ID="RFVtxtXMLMailedOn" runat="server" 
                        ControlToValidate="txtXMLMailedOn" ErrorMessage="*" style="color: #FF0000"></asp:RequiredFieldValidator>
                </td>
                <td align="center" colspan="2">
                    &nbsp;</td>
               
        </tr>
         <tr>
            <td colspan="5" align="center">
                
                <asp:Button ID="BtnSubmit" runat="server" Text="Save" Width="70px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="Close" Visible="false" />
                <asp:Label ID="lbRowid" runat="server" Visible="false" />
                <asp:ScriptManager ID="ScriptManager1" runat="server" />         
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





