<%@ Page Title="Programme Images Report" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootStrap.Master"
    CodeBehind="rptImagesMissingDateWise.aspx.vb" Inherits="EPG.rptImagesMissingDateWise" %>

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
                var isPortrait = $("#MainContent_jsIsPortrait").val();
                var vControlId = $("#jsControlId").val();
                var vimgControlid = $("#jsImageId").val();
                var fileName = $("#MainContent_jsFileName").val();

                $("#" + vControlId).html(fileName);
                if (isPortrait == "0") {
                    $("#" + vimgControlid).attr("src", "../uploads/" + $("#MainContent_jsFileName").val());
                }
                else {
                    $("#" + vimgControlid).attr("src", "../uploads/portrait/" + $("#MainContent_jsFileName").val());

                }
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Programme Images Missing DateWise</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                For Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtStartDate" placeholder="Start Date" runat="server" CssClass="form-control"
                                    ValidationGroup="RFGViewEPG" />
                                <span class="input-group-addon">
                                    <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate"
                                        PopupButtonID="imgStartDateCalendar" />
                                    <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate"
                                        runat="server" ForeColor="Red" Text="*" ValidationGroup="VG" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Landscape Missing</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkLandscapeMissing" runat="server" Checked="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Portrait Missing</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkPortraitMissing" runat="server" Checked="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Res. Mismatch</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkResMismatch" runat="server" Checked="false" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Size Mismatch</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkSizeMismatch" runat="server" Checked="false" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                TRP > 70</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkHighTRP" runat="server" Checked="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Priority Channels</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkPriority" runat="server" Checked="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Channel Genre</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddl" runat="server" CssClass="form-control" DataSourceID="sqlDSGenre"
                                    DataValueField="genreid" DataTextField="genrename" />
                                <asp:SqlDataSource ID="sqlDSGenre" runat="server" SelectCommand="select genreid,genrename from mst_genre where genreid in (select distinct genreid from mst_channel) union select '0','--select--' order by 2"
                                    SelectCommandType="Text" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:Button ID="btnView" runat="server" CssClass="btn alert-info" Text="View" ValidationGroup="VG" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grdImagesReport" runat="server" CssClass="table" AutoGenerateColumns="False"
                    ShowFooter="True" CellPadding="4" EmptyDataText="No record found !" ForeColor="#333333"
                    GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                    SortedDescendingHeaderStyle-CssClass="sortdesc-header" ShowHeader="True" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                    PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Font-Bold="true" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.no.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1  %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ProgID">
                            <ItemTemplate>
                                <asp:Label ID="lbProgID" runat="server" Text='<%# eval("progid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Channel">
                            <ItemTemplate>
                                <asp:Label ID="lbChannelId" runat="server" Text='<%# eval("channelid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Program">
                            <ItemTemplate>
                                <asp:Label ID="lbProgname" runat="server" Text='<%# eval("progname") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                               <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                    <asp:Image ID="img" runat="server" Width="60px" Height="40px" />
                                </asp:HyperLink>
                                <asp:Label ID="lbProgLogo" runat="server" Text='<%# eval("proglogo") %>' Visible="false" />
                                <asp:HyperLink ID="hyEditLandscape" runat="server" Text="Upload Image" ImageUrl="../images/upload.png" />
                                <asp:Label ID="lbFilename" runat="server" Visible="false" />
                                <div id="d" runat="server" ><%# eval("imagedim") %> - <%# Eval("imagesize")%> kb</div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image Portrait">
                            <ItemTemplate>
                            <asp:HyperLink rel="lightbox" ID="hyLogoPortrait" runat="server">
                                <asp:Image ID="imgPortrait" runat="server" Width="40px" Height="60px" />
                                </asp:HyperLink>
                                <asp:Label ID="lbProgLogoPortrait" runat="server" Text='<%# eval("proglogoportrait") %>'
                                    Visible="false" />
                                <asp:HyperLink ID="hyEditPortrait" runat="server" Text="Upload Image" ImageUrl="../images/upload.png" />
                                <asp:Label ID="lbFilenamePortrait" runat="server" Visible="false" />
                                <div id="dp" runat="server" ><%# Eval("imagedimportrait")%> - <%# Eval("imagesizeportrait")%> kb</div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
            <input type="hidden" id="jsImagePortraitId" />
            <input type="hidden" id="jsFileName" size="50" runat="server" />
            <input type="hidden" id="jsIsPortrait" size="50" runat="server" />
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

        function showDiv(jsImageId, jsProgId, jsFileName, isPortrait) {
            try {

                $("#jsImageId").val(jsImageId);
                document.getElementById("MainContent_jsProgId").value = jsProgId;
                document.getElementById("MainContent_jsFileName").value = jsFileName;
                document.getElementById("MainContent_jsIsPortrait").value = isPortrait;

                $("#popme").delay(200).slideDown(300);
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

        $('#ctl00_MainContent_AsyncFileUpload1_ctl02').on('change', function () {
        //alert('This file size is: ' + (this.files[0].size/1024/1024).toFixed(2) + " MB");
        });

   
			setTimeout(myfonction, 2000); 
            function myfonction()
			{
				var i;
				for (i = 0; i < 500; i++) {

					try {
						var url = document.getElementById('MainContent_grdImagesReport_img_' + i).getAttribute("src");
						var url1 = document.getElementById('MainContent_grdImagesReport_imgPortrait_' + i).getAttribute("src");
						//getMeta(url,'MainContent_grdImagesReport_lbRes_' + i);
						//getMeta(url1,'MainContent_grdImagesReport_lbResP_' + i);
					}
					catch (err) { 
						console.log(err.message);
					}

				}
			}
			function getMeta(url,img){
				$("<img/>",{
					load : function(){
						document.getElementById(img).innerHTML = this.width + ' x ' + this.height;
						//alert(this.width+' '+this.height);
					},
					src  : url
				});
			}
      
        
        
    </script>
</asp:Content>
