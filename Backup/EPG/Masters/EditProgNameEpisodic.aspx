﻿<%@ Page Language="vb" Title="Edit Programme"  AutoEventWireup="false"  CodeBehind="EditProgNameEpisodic.aspx.vb" Inherits="EPG.EditProgNameEpisodic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>Select Employee</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function pageLoad() {
            if (document.getElementById('hfSend').value == "2") {
                var qs = getQueryStrings();
                var controlid = qs["controlid"];
                var onair = qs["onair"];
                var index = qs["index"];

                window.opener.location = window.opener.location.href = "ProgramCentralEditEpisodic.aspx?onair=" + onair + "&controlid=" + controlid + "&index=" + index + "";
                self.close();
                window.close();
                if (window.opener && !window.opener.closed) {
                }
                return false;
            }
            var str = document.getElementById("txtSynopsis").value;
            document.getElementById("lbLabel").value = str.length;

        }
        
        function getQueryStrings() {
            try{
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
            catch(err){
                alert(err);
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


        function countChar() {
            var str = document.getElementById("txtSynopsis").value;
            //alert(str);
            document.getElementById("lbLabel").value = str.length;
        }

        function countChara(val) {
            var len = val.value.length;
            document.getElementById("lbLabel").value=val.value.length;
        };
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
                    &nbsp;&nbsp;&nbsp;Edit Programme Episodic Shows
                </h2>
            </div>
            
            <div class="clear">
                
            </div>
        </div>
        <div class="main1">
        <div align= "center">
    <table width="99%" class="whitetable" border="1px" cellpadding="2px">
        <tr>
            <th colspan="2" align="right">
                <h2>
                    Channel
                </h2>
            </th>
            <td colspan="2" align="left">
                <h2>
                    <asp:Label ID="lbChannel" runat="server" Text="Channel" />
                </h2>
            </td>
        </tr>
        <tr>
            <th align="left">
                Programme
            </th>
        
            <td align="left">
                <asp:Label ID="lbProgramme" runat="server" Text="Programme" />
            </td>
        
            <th align="left">
                Language
            </th>
        
            <td align="left">
                <asp:Label ID="lbLanguage" runat="server" Text="Langauge" />
            </td>
        </tr>
        <tr>
                <td align="center" colspan="2">
                    <b>Programme</b></td>
                <td align="center" colspan="2">
               <b>Synopsis</b>
                </td>
                
                
        </tr>
        <tr>
                <td align="center" colspan="2">
                    <asp:TextBox ID="txtProgname" runat="server" Width="200px" Height="80px" TextMode="MultiLine"/>
                    <asp:RequiredFieldValidator ID="RFVtxtProgname" runat="server" ControlToValidate="txtProgname" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td align="center" colspan="2">
                    <asp:TextBox ID="txtSynopsis" Width="300px" Height="80px" TextMode="MultiLine" runat="server" onkeyup="countChara(this)" />
                    <input id="lbLabel" type="text" readonly="readonly" maxlength="3"/>
                    <%--<input id="btnCount" type="button" value="Count" onclick="javascript:countChar();" />--%>
                    
                </td>
               
        </tr>
        <tr>
                <td align="center" colspan="4">
                     <asp:Label ID="lberror" runat="server" Text="*" Visible="false" style="font-size: xx-small; color: #FF0000; font-weight: 700;"/>
                </td>
        </tr>
         <tr>
            <td colspan="5" align="center">
                
                <asp:Button ID="BtnSubmit" runat="server" Text="Save" Width="70px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="Close" Visible="false" />
                <asp:Label ID="lbProgId" runat="server" Visible="false" />
                <asp:Label ID="lbLangId" runat="server" Visible="false" />
                <asp:Label ID="lbEpisodeNo" runat="server" Visible="false" />
                </td>             
        </tr>
        </table>
    </div>
               
        </div>

      
        
    </div>
    </form>
</body>
</html>





