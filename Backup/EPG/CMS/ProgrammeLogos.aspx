<%@ Page Title="Programme Logos" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="ProgrammeLogos.aspx.vb" Inherits="EPG.ProgrammeLogos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #black_overlay
        {
            display: none;
            position: fixed;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
        }
        #popme
        {
            display: none;
            position: fixed;
            top: 25%;
            left: 30%;
            width: 30%;
            height: 25%;
            padding: 16px;
            border: 5px solid orange;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        #saving
        {
            display: none;
            position: fixed;
            top: 47%;
            left: 47%;
            width: 40%;
            height: 40%;
            
            z-index: 1002;
            overflow: auto;
        }
        #popmeVideo
        {
            display: none;
            position: fixed;
            top: 40%;
            left: 40%;
            width: 30%;
            height: 25%;
            padding: 16px;
            border: 5px solid orange;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        #popme1
        {
            position: fixed;
            top: 20%;
            left: 22%;
            width: 200;
            display: none;
            z-index: 10;
            background: rgba(0, 0, 0, 0.5);
            padding-left: 0.5%;
            padding-right: 0.5%;
        }
        #demo3
        {
            position: fixed;
            top: 7%;
            left: 40%;
            right: 1%;
            display: none;
        }
    </style>
    <script type="text/javascript">
        function hideDiv() {
            $("#popme").slideUp(200);
            $("#black_overlay").slideUp(200);
        }
        function hideVideoDiv() {
            $("#popmeVideo").slideUp(200);
            $("#black_overlay").slideUp(200);
        }
        function clientMessage() {
            try {
                var vControlId = $("#jsControlId").val();
                var vimgControlid = $("#jsImageId").val();
                var fileName = $("#MainContent_jsFileName").val();

                $("#" + vControlId).html(fileName);
                $("#" + vimgControlid).attr("src", "../uploads/" + $("#MainContent_jsFileName").val());
                hideDiv();
            }
            catch (ex) {
                alert(ex);
            }
        }

    </script>
    <link href="http://epgops.ndtv.com/lightbox.css" rel="stylesheet" type="text/css" />
    <script src="http://epgops.ndtv.com/lightbox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            initLightbox();
        }
    </script>
    
            <div class="container">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                            <h3>
                                Programme Logos</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            Search Programme</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtSearch1" runat="server" CssClass="form-control" 
                                                placeholder="search text required for Select All option" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            Channel</label>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlChannel" runat="server" DataTextField="ChannelID1" DataValueField="ChannelID"
                                                DataSourceID="SqlmstChannel" CssClass="form-control" />
                                            <asp:SqlDataSource ID="SqlmstChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                                SelectCommand="SELECT '0' Channelid,'Select All' Channelid1, 0 p union select channelid,channelid,1 p FROM mst_channel where active=1 order by p,Channelid">
                                            </asp:SqlDataSource>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            Missing</label>
                                        <div class="input-group">
                                            <asp:CheckBox ID="chkMissing" runat="server" AutoPostBack="false" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                            From Script?</label>
                                        <div class="input-group">
                                            <asp:CheckBox ID="chkScript" runat="server" AutoPostBack="false" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-3">
                                    <div class="form-group">
                                        <label>
                                        </label>
                                        <div class="input-group">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                            <asp:GridView ID="grdProgImage" runat="server" AutoGenerateColumns="False" CssClass="table"
                                AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                PageSize="100" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ProgId" SortExpression="progid">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbProgId" Text='<%# Bind("progid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("ChannelId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Programme" SortExpression="progname">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbProgName" Text='<%# Bind("progname") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Logo" SortExpression="programlogo">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbProgLogo" Text='<%# Bind("programlogo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PROMO VideoURL" SortExpression="VideoURL">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbVideoURL" Text='<%# Bind("VideoURL") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="color" SortExpression="color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbcolor" Text='<%# Bind("color") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Next Airing" SortExpression="NextAiring">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbNextAiring" Text='<%# Bind("NextAiring") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Duration" SortExpression="Duration">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbDuration" Text='<%# Bind("Duration") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                                <asp:Image runat="server" ID="imglogo" AlternateText="NDTV" Width="50px" Height="50px" />
                                            </asp:HyperLink></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyEdit" runat="server" Text="Upload Image" ImageUrl="../images/upload.png" />
                                            <asp:HyperLink ID="hyVideo" runat="server" Text="Upload Image" ImageUrl="../images/video.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="fileName" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbFileName" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Images/remove.png" />
                                </Columns>
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
                            <asp:SqlDataSource ID="sqlDSProgImage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
            <%-- POPUP for uploading image and updating video URL STARTS --%>
            <div id="popme">
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 text-center">
                    <h3>
                        Upload Programme Image
                    </h3>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-right">
                    <button type="button" style="float: right" onclick="hideDiv();">
                        X</button>
                    <input type="hidden" id="jsProgId" size="50" runat="server" />
                    <input type="hidden" id="jsImageId" />
                    <input type="hidden" id="jsVideoId" />
                    <input type="hidden" id="jsFileName" size="50" runat="server" />
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="#82CAFA"
                        CompleteBackColor="#FFFFFF" Filter="*.jpg;*.jpeg|Supported Images Types (*.jpg;*.jpeg)"
                        OnClientUploadComplete="clientMessage" OnUploadedComplete="AsyncFileUpload1_UploadedComplete"
                        ThrobberID="Image1" ProgressInterval="50" SaveBufferSize="128" Width="200px"
                        EnableProgress="OnSubmit, OnPreview" />
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                    <span style="color: Red">only .jpg and .jpeg files supported</span>
                </div>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loader.gif" />
                <br />
            </div>

            <div id="popmeVideo">
                <div id="saving">
                    <img src="../images/saving.gif" />
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 text-center">
                    <h3>
                        Update PROMO URL
                    </h3>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-right">
                    <button type="button" style="float: right" onclick="hideVideoDiv();">
                        X</button>
                </div>
                <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                    <input id="inPromoVideoUrl" type="text" class="form-control" placeholder="promo video url" />
                </div>
                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                    <input type="button" class="btn btn-success" value="Update" onclick="ValidateURL()" />
                </div>
            </div>
            <%-- POPUP for uploading image and updating video URL ENDS --%>
            <div id="black_overlay">
            </div>
            <br />
            <br />
            <br />
            <script type="text/javascript">

                function SavingOn(onoff) {
                    if (onoff == 1) {
                        $("#saving").slideDown(200);
                    }
                    else {
                        $("#saving").delay(200).slideUp(200);
                    }
                }

                function showDiv(jsImageId, jsProgId, jsFileName, jsVideoId) {
                    try {

                        $("#jsImageId").val(jsImageId);
                        $("#jsVideoId").val(jsVideoId);

                        document.getElementById("MainContent_jsFileName").value = jsFileName;
                        document.getElementById("MainContent_jsProgId").value = jsProgId;

                        $("#popme").delay(200).slideDown(300);
                        $("#black_overlay").delay(200).slideDown(200);
                    }
                    catch (ex) {
                        alert(ex.ToString());
                    }
                }

                function showVideo(videourl, jsProgId, jsFileName, jsVideoId) {
                    try {

                        $("#jsImageId").val(jsImageId);
                        $("#jsVideoId").val(jsVideoId);
                        //alert(videourl);
                        // $("#inPromoVideoUrl").val(videourl);

                        $("#inPromoVideoUrl").val($("#" + jsVideoId).html());

                        document.getElementById("MainContent_jsFileName").value = jsFileName;
                        document.getElementById("MainContent_jsProgId").value = jsProgId;

                        $("#popmeVideo").delay(200).slideDown(300);
                        $("#black_overlay").delay(200).slideDown(200);
                    }
                    catch (ex) {
                        alert(ex.ToString());
                    }
                }

                function alertContents(httpRequest) {

                    if (httpRequest.readyState == 4) {
                        // everything is good, the response is received
                        if ((httpRequest.status == 200) || (httpRequest.status == 0)) {
                            // FIXME: perhaps a better example is to *replace* some text in the page.
                            var result = httpRequest.responseText;
                            console.log(result);
                            var obj = JSON.parse(result);
                            if (obj.message[0].success == "1") {
                                var vProgid = obj.result[0].progid;
                                var vVideoURL = obj.result[0].videoURL;
                                var vVideoID = $("#jsVideoId").val();
                                $("#" + vVideoID).html(vVideoURL);
                            }
                            else {
                                alert("Error while updating" + obj.result[0].error)
                            }
                            SavingOn(0);
                            hideVideoDiv()
                        } else {
                            alert('There was a problem with the request. ' + httpRequest.status + httpRequest.responseText);
                        }
                    }
                }
                function ValidateURL() {
                    try {
                        //alert("abc");
                        SavingOn(1);
                        mode = "updateVideoURL";
                        progid = $("#MainContent_jsProgId").val();
                        controlid = $("#jsVideoId").val();
                        var vPromoVideoURL = $("#inPromoVideoUrl").val();

                        var httpRequest = new XMLHttpRequest();
                        httpRequest.onreadystatechange = function () { alertContents(httpRequest); };
                        httpRequest.open("GET", "serviceExecute.aspx?mode=" + mode + "&progid=" + progid + "&value=" + vPromoVideoURL, true);
                        httpRequest.send(null);
                        
                    }
                    catch (ex) {
                        alert(ex.ToString());
                    }
                }
            </script>
       
</asp:Content>
