<%@ Page Language="vb" Title="Edit Programme" AutoEventWireup="false" MasterPageFile="~/SiteLess.Master"
    CodeBehind="EditProgNameEpisodic.aspx.vb" Inherits="EPG.EditProgNameEpisodic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">


        function getQueryStrings() {
            try {
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
            catch (err) {
                alert(err);
            }
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


        function countChar() {
            var str = document.getElementById("txtSynopsis").value;
            //alert(str);
            document.getElementById("lbLabel").innerHTML = str.length;
        }

        function countChara(val) {
            var len = val.value.length;
            document.getElementById("lbLabel").innerHTML = val.value.length;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" onload="javascript:pageLoad();">
    <div class="container">
        <input type="hidden" id="hfSend" runat="server" />
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h4>
                    <span class="label label-warning">
                        <asp:Label ID="lbChannel" runat="server" Text="Channel" />
                        -
                        <asp:Label ID="lbLanguage" runat="server" Text="Langauge" />
                    </span>
                    <br />
                    <span class="label label-info">Programme:
                        <asp:Label ID="lbProgramme" runat="server" Text="Programme" />
                        - Episode:
                        <asp:Label ID="lbEpisodeNo" runat="server" /></span>
                </h4>
            </div>
            <div class="panel-body">
                <div class="row alert-info">
                    <div class="col-xs-4">
                        <div class="form-group">
                            <label>
                                Epiodic Programme</label>
                            <div class="input-group">
                                <asp:Label ID="lbEpiProgname" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-8">
                        <div class="form-group">
                            <label>
                                Epiodic Synopsis</label>
                            <div class="input-group">
                                <asp:Label ID="lbEpiSynopsis" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-4">
                    <div class="form-group">
                        <label>
                            Programme</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtProgname" runat="server" TextMode="MultiLine" Rows="8" CssClass="form-control" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFVtxtProgname" runat="server" ControlToValidate="txtProgname"
                                    ErrorMessage="*" />
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-xs-8">
                    <div class="form-group">
                        <label>
                            Synopsis</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtSynopsis" TextMode="MultiLine" Rows="8" runat="server" CssClass="form-control"
                                onkeyup="countChara(this)" />
                            <span class="input-group-addon">
                                <p id="lbLabel">
                                </p>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 text-center">
                    <asp:Label ID="lberror" runat="server" Text="*" Visible="false" Style="font-size: xx-small;
                        color: #FF0000; font-weight: 700;" />
                    <asp:Button ID="BtnSubmit" runat="server" Text="Save" CssClass="btn btn-info" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnCancel" runat="server" Text="Close" Visible="false" />
                    <asp:Label ID="lbProgId" runat="server" Visible="false" />
                    <asp:Label ID="lbLangId" runat="server" Visible="false" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        if (document.getElementById('MainContent_hfSend').value == "2") {
            var qs = getQueryStrings();
            var controlid = qs["controlid"];
            var index = qs["index"];
            var frompage = qs["frompage"];
            //alert('frompage:' + frompage);
            if (typeof frompage == 'undefined') {
                frompage="ProgramCentralEditEpisodic.aspx"
                window.opener.location = window.opener.location.href = "ProgramCentralEditEpisodic.aspx?controlid=" + controlid + "&index=" + index + "";
            }
            else {
                window.opener.location = window.opener.location.href = "PCEpiSearch.aspx?controlid=" + controlid + "&index=" + index + "";
            }

            self.close();
            window.close();
            if (window.opener && !window.opener.closed) {
            }
        }
        var str = document.getElementById("txtSynopsis").value;
        document.getElementById("lbLabel").innerHTML = str.length;

      
    </script>
</asp:Content>
