<%@ Page Title="Movie Logos" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="MovieLogos.aspx.vb" Inherits="EPG.MovieLogos" %>

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
            top: 35%;
            left: 35%;
            width: 25%;
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
        function countSynLong(field, maxChar) {
            //            if ($(field).val().length > maxChar) {
            //                $(field).val($(field).val().substr(0, maxChar));
            //            }
            var len = field.value.length;
            $("#lbSynLong").html(len + '(' + maxChar + ')');
        };

        function countSyn(field, maxChar) {
            //            if ($(field).val().length > maxChar) {
            //                $(field).val($(field).val().substr(0, maxChar));
            //            }
            var len = field.value.length;
            $("#lbSyn").html(len + '(' + maxChar + ')');
        };



        function hideDiv() {
            $("#popme").slideUp(200);
            $("#black_overlay").slideUp(200);
        }

        function openMovieSchedule(progid) {
            window.open("ShowMovieSched.aspx?progid=" + progid, "Movie Schedule", "width=650,height=400,toolbar=0,left=300,top=150,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }

        function clientMessage() {
            try {
                //alert("file Uploaded");
                //$("#jsStatus").text("File Uploaded");
                var vControlId = $("#jsControlId").val();
                var vimgControlid = $("#jsImageId").val();
                var vHyControlId = $("#jsHyControlId").val();

                var fileName = $("#MainContent_jsFileName").val();
                //alert(vControlId);
                $("#" + vControlId).html(fileName);
                $("#" + vHyControlId).attr("href", "../uploads/Movieimages/" + $("#MainContent_jsFileName").val());
                $("#" + vimgControlid).attr("src", "../uploads/Movieimages/" + $("#MainContent_jsFileName").val());


                hideDiv();
            }
            catch (ex) {
                alert("Error : " + ex.Message);
            }


        }

    </script>
    <link href="../lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../lightbox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            initLightbox();
        }
    </script>
    <%--<asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <strong>Movie Master</strong>
            </div>
            <div class="panel-body">
                <div class="col-lg-4">
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Name</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Release Year</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtReleaseYear" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos" />
                            <span class="input-group-addon">
                                <asp:RegularExpressionValidator ID="REVtxtReleaseYear" runat="server" ControlToValidate="txtReleaseYear"
                                    ForeColor="Red" ValidationExpression="^[0-9]+$" Text="* numbers" ValidationGroup="VGMovieLogos"></asp:RegularExpressionValidator></span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Star Cast</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtStarCast1" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Country</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Movie Language</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:DropDownList ID="ddlMovieLanguage" runat="server" DataSourceID="sqlDSLanguage"
                                DataTextField="FullName" DataValueField="Languageid" CssClass="form-control" />
                            <asp:SqlDataSource ID="sqlDSLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                SelectCommand="select languageid,fullname from mst_language where active=1" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Genre1</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:DropDownList ID="ddlGenre1" runat="server" CssClass="form-control" DataSourceID="sqlDSGenre"
                                DataTextField="GenreName" DataValueField="GenreName" />
                            <asp:SqlDataSource ID="sqlDSGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                SelectCommand="select distinct genre genreName from mst_moviesdb
                                        union
                                        select distinct genre2 genreName from mst_moviesdb
                                        order by 1" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Genre2</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:DropDownList ID="ddlGenre2" runat="server" CssClass="form-control" DataSourceID="sqlDSGenre"
                                DataTextField="GenreName" DataValueField="GenreName" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Writer</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtWriter" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Synopsis</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtSynopsis" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos"
                                TextMode="MultiLine" Rows="4" onkeyup="countSyn(this,200)" />
                            <span class="input-group-addon">
                                <div id="lbSyn">
                                </div>
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Long Synopsis</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtLongSynopsis" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos"
                                TextMode="MultiLine" Rows="4" onkeyup="countSynLong(this,1000)" />
                            <span class="input-group-addon">
                                <div id="lbSynLong">
                                </div>
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Director</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtDirector" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Verified</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:CheckBox ID="chkIsVerified" runat="server" Checked="false" Text="Verified" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Trivia</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtTrivia" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos"
                                TextMode="MultiLine" Rows="2" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <small>Awards</small></label>
                        <div class="col-sm-9 input-group">
                            <asp:TextBox ID="txtAwards" runat="server" CssClass="form-control" ValidationGroup="VGMovieLogos"
                                TextMode="MultiLine" Rows="2" />
                        </div>
                    </div>
                    <div class="form-group text-center">
                        <asp:Button ID="btnUpdate" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VGMovieLogos" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                        <asp:Label ID="lbRowId" runat="server" Visible="true" />
                    </div>
                    <div class="form-group alert-danger">
                        <asp:Label ID="lbReport" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <small>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <asp:TextBox ID="txtSearch1" runat="server" placeholder="search text" CssClass="input-group-sm btn btn-default" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="FullName" DataValueField="LanguageID"
                                DataSourceID="SqlmstLanguage" CssClass="form-control" />
                            <asp:SqlDataSource ID="SqlmstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                SelectCommand="select 0 LanguageID,'Select All' FullName, 0 p union select LanguageID,FullName, 1 p from mst_language where active=1 order by p">
                            </asp:SqlDataSource>
                        </div>
                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-7">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <asp:CheckBox ID="chkusefilters" runat="server" Text="Use Filters" Checked="True" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                    <asp:CheckBox ID="chkWithImage" runat="server" Text="Image" Checked="False" />
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <asp:CheckBox ID="chkWithSynopsis" runat="server" Text="Synopsis" Checked="False" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                    <asp:CheckBox ID="chkWithReleaseYear" runat="server" Text="Release" Checked="False" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4">
                                    <asp:CheckBox ID="chkWithStarCast" runat="server" Text="Cast" Checked="False" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <asp:CheckBox ID="chkInCurrentEPG" runat="server" Text="in EPG" Checked="True" />
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <asp:CheckBox ID="chkVerified" runat="server" Text="Verified" Checked="False" />
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <asp:CheckBox ID="chkExact" runat="server" Text="Exact" Checked="False" />
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                    <asp:CheckBox ID="chkAwards" runat="server" Text="Awards" Checked="False" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info btn-sm" />
                            &nbsp;
                            <asp:Button ID="btnVerifyMultiple" runat="server" Text="Verify Multiple" CssClass="btn btn-success btn-sm" />
                        </div>
                    </div>
                </div>
                <div class="panel-body" style="max-height: 1800px; overflow-y: scroll;">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:GridView ID="grdMovieLogos" runat="server" BackColor="White" Width="100%" DataSourceID="sqlDSMovieLogos"
                            CssClass="table" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="20" ForeColor="Black"
                            GridLines="Vertical" EmptyDataText="No record Found..." AllowSorting="true">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="moviename">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyName" runat="server" NavigateUrl='<%# Eval("rowid", "javascript:openMovieSchedule({0})") %>'
                                            Text='<%#Eval("moviename") %>' />
                                        <asp:Label ID="lbName" runat="server" Text='<%#Eval("moviename") %>' Visible="false" />
                                        <asp:Label ID="lbRowid" runat="server" Text='<%#Eval("rowid") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Synopsis" SortExpression="synopsis">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%#Eval("synopsis") %>' />
                                        <asp:Label ID="lbLOngSynopsis" runat="server" Text='<%#Eval("longsynopsis") %>' Visible="False" />
                                        <asp:Label ID="lbTrivia" runat="server" Text='<%#Eval("trivia") %>' Visible="False" />
                                        <asp:Label ID="lbAwards" runat="server" Text='<%#Eval("awards") %>' Visible="False" />
                                        <asp:Label ID="lbWriter" runat="server" Text='<%#Eval("writer") %>' Visible="False" />
                                        <asp:Label ID="lbDirector" runat="server" Text='<%#Eval("Director") %>' Visible="False" />
                                    </ItemTemplate>
                                    <ControlStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Star Cast" SortExpression="StarCast">
                                    <ItemTemplate>
                                        <asp:Label ID="lbStarCast" runat="server" Text='<%#Eval("StarCast") %>' />
                                    </ItemTemplate>
                                    <ControlStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Release Year" SortExpression="releaseYear">
                                    <ItemTemplate>
                                        <asp:Label ID="lbreleaseYear" runat="server" Text='<%#Eval("releaseYear") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country" SortExpression="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCountry" runat="server" Text='<%#Eval("Country") %>' />
                                    </ItemTemplate>
                                    <ControlStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Language" SortExpression="Language">
                                    <ItemTemplate>
                                        <asp:Label ID="lbMovieLangId" runat="server" Text='<%#Eval("MovieLangId") %>' Visible="false" />
                                        <asp:Label ID="lbMovieLanguage" runat="server" Text='<%#Eval("MovieLanguage") %>' />
                                        <asp:Label ID="lbLanguageId" runat="server" Text='<%#Eval("LanguageId") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Genre" SortExpression="Genre">
                                    <ItemTemplate>
                                        <asp:Label ID="lbGenre" runat="server" Text='<%#Eval("genre") %>' Visible="true" />
                                        <asp:Label ID="lbGenre2" runat="server" Text='<%#Eval("genre2") %>' Visible="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image" SortExpression="profilePath">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtmdbImageURL" runat="server" Text='<%#Eval("tmdbImageURL") %>' Visible="false" />
                                        <asp:HyperLink ID="hyLogo" rel="lightbox" runat="server" NavigateUrl='<%# Bind("tmdbImageURL") %>'>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Image ID="imgCelebrityImage" runat="server" ImageUrl='<%# Bind("tmdbImageURL") %>'
                                                Width="100px" Height="100px" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:HyperLink>
                                        <asp:Label ID="lbFileName" runat="server" Visible="false" Text='<%# Bind("filename") %>' />
                                    </ItemTemplate>
                                    <ControlStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upload">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyUpload" runat="server" Text="Upload Image" ImageUrl="../images/upload.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Images/remove.png" />
                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png" />
                                <asp:TemplateField HeaderText="Verified">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkVerified" runat="server" Checked='<%#Eval("verified") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlDSMovieLogos" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
                        </asp:SqlDataSource>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </small>
    </div>
    <div id="popme">
        <button type="button" style="float: right" onclick="hideDiv();">
            X</button><br />
        <input type="hidden" id="jsProfileId" size="50" runat="server" />
        <input type="hidden" id="jsImageId" size="50" />
        <input type="hidden" id="jsControlId" size="50" />
        <input type="hidden" id="jsHyControlId" size="50" />
        <input type="hidden" id="jsFileName" size="50" runat="server" />
        <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="#82CAFA"
            CompleteBackColor="#FFFFFF" Filter="*.jpg;*.jpeg|Supported Images Types (*.jpg;*.jpeg)"
            OnClientUploadComplete="clientMessage" OnUploadedComplete="AsyncFileUpload1_UploadedComplete"
            ThrobberID="Image1" ProgressInterval="50" SaveBufferSize="128" Width="200px"
            EnableProgress="OnSubmit, OnPreview" />
        <br />
        <span style="color: Red">only .jpg and .jpeg files supported</span>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loader.gif" />
        <br />
    </div>
    <div id="black_overlay">
    </div>
    <script type="text/javascript">
        function showDiv(jsControlId, jsHyControlId, jsImageId, jsProgId, jsFileName) {
            try {
                $("#jsImageId").val(jsImageId);
                $("#jsControlId").val(jsControlId);
                $("#jsHyControlId").val(jsHyControlId);

                document.getElementById("MainContent_jsFileName").value = jsFileName;
                document.getElementById("MainContent_jsProfileId").value = jsProgId;

                $("#popme").delay(200).slideDown(300);
                $("#black_overlay").delay(200).slideDown(200);
            }
            catch (ex) {
                alert(ex.ToString());
            }
        }
    </script>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
