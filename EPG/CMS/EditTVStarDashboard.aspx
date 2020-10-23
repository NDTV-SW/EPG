<%@ Page Language="vb" Title="Edit Programme" AutoEventWireup="false" 
    CodeBehind="EditTVStarDashboard.aspx.vb" Inherits="EPG.EditTVStarDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link rel="icon" type="image/ico" href="./Images/favicon.ico" />
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <%--<meta http-equiv="refresh" content="2" />--%>
    <meta name="HandheldFriendly" content="True" />
    <meta name="MobileOptimized" content="320" />
    <%--<meta name="viewport" content="width=device-width" />--%>
    <meta name="description" content="New Delhi Television Ltd - EPG Services Division" />
    <meta name="author" content="Hemant Sajnani, Kautilya Rastogi" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../styles/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        @media (min-width:1200px)
        {
            .container
            {
                width: 98%;
            }
        }
        .nav > li > a
        {
            padding: 10px 10px;
        }
        .hyper
        {
            color: White;
            font-weight: bold;
            margin-top: 0px;
        }
        .noFormatting
        {
            text-decoration: none;
        }
        .SuperScript
        {
            font-size: xx-small;
            vertical-align: top;
        }
        .formLabel
        {
            width: 100px;
        }
        .footer
        {
            text-align: center;
            text-decoration: none;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {

            if (document.getElementById('hfSend').value == "2") {
                var qs = getQueryStrings();
                var controlid = qs["controlid"];


                window.opener.location = window.opener.location.href = "CMSTvStarDashBoard.aspx?controlid=" + controlid;
                self.close();
                window.close();
                if (window.opener && !window.opener.closed) {
                }
                return false;
            }
            var str = document.getElementById("txtDescription").value;
            document.getElementById("lbLabel1").value = str.length;

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
            window.opener.location = window.opener.location.href = "CMSTvStarDashBoard.aspx?channelid='" + channelid + "'";
            self.close();
            window.close();
            if (window.opener && !window.opener.closed) {

            }
            return false;

        }


        function countChar() {
            var str = document.getElementById("txtDescription").value;
            document.getElementById("lbLabel").value = str.length;
        }

        function countChara(val) {
            var len = val.value.length;
            document.getElementById("lbLabel1").value = val.value.length;
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
<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
<!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
<body onload="javascript:pageLoad();">
    <div class="js">
        <!--this is supposed to be on the HTML element but codepen won't let me do it-->
        <div id="preloader">
        </div>
        <form id="Form1" runat="server">
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                            class="icon-bar"></span><span class="icon-bar"></span>
                    </button>
                    <a id="ndtvHome" href="~/cms/default.aspx" class="navbar-brand" runat="server">
                        <asp:Image ID="imgLogo" ImageUrl="images/ndtv_logo.png" runat="server" /><span class="hyper">
                            &nbsp;CMS</span> </a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav navbar-right">
                        <li><a>
                            <div class="loginDisplay hyper">
                                <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                                    <AnonymousTemplate>
                                        [ <a href="~/Account/Login.aspx" id="HeadLoginStatus" runat="server">Log In</a>
                                        ]
                                    </AnonymousTemplate>
                                    <LoggedInTemplate>
                                        Welcome <span class="bold">
                                            <asp:LoginName ID="HeadLoginName" runat="server" />
                                        </span>! [
                                        <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out"
                                            LogoutPageUrl="~/Account/Login.aspx" />
                                        <a>]</a>
                                    </LoggedInTemplate>
                                </asp:LoginView>
                            </div>
                        </a></li>
                    </ul>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="container">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                            EDIT TV STAR</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12 col-sm-12">
                            <div class="col-lg-3 col-sm-3">
                                <div class="form-group">
                                    <label>
                                        Channel</label>
                                    <div class="input-group">
                                        <asp:Label ID="lbChannel" runat="server" Text="Channel" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-3">
                                <div class="form-group">
                                    <label>
                                        Programme</label>
                                    <div class="input-group">
                                        <asp:Label ID="lbProgramme" runat="server" Text="Channel" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-3">
                                <div class="form-group">
                                    <label>
                                        English Name</label>
                                    <div class="input-group">
                                        <asp:Label ID="lbName" runat="server" Text="Name" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-3">
                                <div class="form-group">
                                    <label>
                                        English Role Name</label>
                                    <div class="input-group">
                                        <asp:Label ID="lbRole" runat="server" Text="Role" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-3">
                                <div class="form-group">
                                    <label>
                                        English Description</label>
                                    <div class="input-group">
                                        <asp:Label ID="lbDescription" runat="server" Text="Description" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-sm-12">
                            <div class="col-lg-4 col-sm-4">
                                <div class="form-group">
                                    <label>
                                        Regional Celebrity Name</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtProfileName" runat="server" CssClass="form-control" />
                                        <span class="input-group-addon">
                                            <asp:RequiredFieldValidator ID="RFVtxtProfileName" runat="server" ControlToValidate="txtProfileName"
                                                ErrorMessage="*" /></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-sm-4">
                                <div class="form-group">
                                    <label>
                                        Regional RoleName</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-sm-4">
                                <div class="form-group">
                                    <label>
                                        Regional Description</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="4" runat="server" onkeyup="countChara(this)"
                                            CssClass="form-control" />
                                        <span class="input-group-addon"></span>
                                    </div>
                                    <%--<input id="lbLabel1" type="text" readonly="readonly" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-sm-12 text-center">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Save" CssClass="btn btn-success" />
                            <asp:Button ID="BtnCancel" runat="server" Text="Close" Visible="false" CssClass="btn btn-danger" />
                        </div>
                        <div class="col-lg-12 col-sm-12">
                            <asp:Label ID="lberror" runat="server" Text="*" Visible="false" Style="font-size: xx-small;
                                color: #FF0000; font-weight: 700;" />
                            <asp:Label ID="lbRowId" runat="server" Visible="false" />
                            <asp:Label ID="lbProfileId" runat="server" Visible="false" />
                            <asp:Label ID="lbprogid" runat="server" Visible="false" />
                            <asp:Label ID="lbLanguageid" runat="server" Visible="false" />
                            <asp:Label ID="lbMode" runat="server" Visible="false" />
                            <input type="hidden" id="hfSend" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
        <div class="panel-footer nav navbar-fixed-bottom" style="background-color: Black;
            opacity: 0.4; filter: alpha(opacity=40); color: White; height: 35px;">
            <footer>
			<p class="footer">&copy; Copyright 
				<a href="http://www.ndtv.com/" target="_blank" id="copyright"></a>NDTV&nbsp;Limited 2015. All rights reserved.</p>
		    </footer>
        </div>
    </div>
    <script type="text/javascript">
        document.getElementById("preloader").style.display = "none";
    </script>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="http://epgops.ndtv.com/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="http://epgops.ndtv.com/js/bootstrap-dialog.js" type="text/javascript"></script>
    <script src="http://epgops.ndtv.com/js/bootbox.min.js" type="text/javascript"></script>
    <script src="http://epgops.ndtv.com/js/bootstrap-dialog.js" type="text/javascript"></script>
</body>
</html>
