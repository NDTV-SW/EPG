<%@ Page Language="vb" Title="Edit Programme"  AutoEventWireup="false"  CodeBehind="EditDiscrepancyCentral.aspx.vb" Inherits="EPG.EditDiscrepancyCentral" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>Select Employee</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad() {
            if (document.getElementById('hfSend').value == "2") {
                var qs = getQueryStrings();
                var LangId = qs["LangId"];
                var Mode = qs["Mode"];
                var dt = qs["dt"];
                var option = qs["option"];
                var onair = qs["onair"];
                var controlid = qs["controlid"];
                var today = qs["today"];
                var airtelDTH = qs["airtelDTH"];
                
                window.opener.location = window.opener.location.href = "ProgramDiscrepancyCentralImport.aspx?LangId=" + LangId + "&mode=" + Mode + "&option=" + option + "&onair=" + onair + "&controlid=" + controlid + "&today=" + today + "&airtelDTH=" + airtelDTH; 
                
                self.close();
                window.close();
                if (window.opener && !window.opener.closed) {
                }
                return false;
            }
            var str = document.getElementById("txtText").value;
            document.getElementById("lbLabel").value = str.length;
        }
        function getQueryStrings() {
            var assoc = {};
            var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
            var queryString = location.search.substring(1);
            var keyValues = queryString.split('&');
            for (var i in keyValues) {
                var key = keyValues[i].split('=');
                if (key.length > 1) {
                    try
                    {
                    //alert("abc");
                    assoc[decode(key[0])] = decode(key[1]);
                    
                    }
                    catch(err) { //alert(err);
                     }
                    
                }
            }
            
            return assoc;
        }

        function winclose(channelid) {
            try {
            }
            catch (err) { }
            window.opener.location = window.opener.location.href = "ProgramCentralEdit.aspx?channelid='" + channelid + "'";
            self.close();
            window.close();
            if (window.opener && !window.opener.closed) {
            }
            return false;
        }
        function countChara(val) {
            var len = val.value.length;
            document.getElementById("lbLabel").value = val.value.length;
        };
    </script>

    <style type="text/css">
        .style1
        {
            height: 25px;
        }
        #lbLabel
        {
            width: 37px;
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
                    &nbsp;&nbsp;&nbsp;Edit Discrepancy Central
                </h2>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="main1">
        <div align= "center">
    <table width="99%" class="whitetable" border="1px" cellpadding="2px">
        <tr>
            <th align="right" class="style1">
                <asp:Label ID="lbDispProgname" runat="server" Text="English Programme" />
            </th>
            <td align="left" class="style1">
                <asp:Label ID="lbProgname1" runat="server" Text="Text" />
            </td>
        </tr>
        <tr>
            <th align="right" class="style1">
                <asp:Label ID="lbDispSynopsis" runat="server" Text="English Synopsis" />
            </th>
            <td align="left" class="style1">
                <asp:Label ID="lbSynopsis1" runat="server" Text="Text" />
            </td>
        </tr>
        <tr>
                <th align="right">
                    <asp:Label ID="lbRegionalProg" runat="server" Text ="Regional Programme" Visible="false" />
                </th>
                <td align="left">
                    <asp:TextBox ID="txtRegProg" runat="server" Width="500px" visible="false" />
                </td>
        </tr>
        <tr>
                <th align="right">
                    <asp:Label ID="lbRegional" runat="server" Text ="Regional Synopses" />
                </th>
                <td align="left">
                    <asp:TextBox ID="txtText" runat="server" Width="500px" Height="80px" TextMode="MultiLine" onkeyup="countChara(this)" />
                    <input id="lbLabel" type="text" runat="server" readonly="readonly" maxlength="3"/>
                </td>
        </tr>
        <tr>
                <td align="center" colspan="4">
                     <asp:Label ID="lberror" runat="server" Text="*" Visible="false" style="font-size: xx-small; color: #FF0000; font-weight: 700;"/>
                </td>
        </tr>
         <tr>
            <td colspan="5" align="center">
                <asp:Button ID="BtnSubmit" runat="server" Text="Save" Width="130px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="Close" Visible="false" />
                <asp:Label ID="lbProgId" runat="server" Visible="false" />
                <asp:Label ID="lbLangId" runat="server" Visible="false" />
                <asp:Label ID="lbMode" runat="server" Visible="false" />

                <asp:Label ID="lbProgName" runat="server" Visible="false" />
                <asp:Label ID="lbSynopsis" runat="server" Visible="false" />
                <asp:Label ID="lbRegProgName" runat="server" Visible="false" />
                <asp:Label ID="lbRegSynopsis" runat="server" Visible="false" />
                <asp:Label ID="lbEpisodeNo" runat="server" Visible="false" />
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





