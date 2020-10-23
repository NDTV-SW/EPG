<%@ Page Language="vb" Title="Genre Master" AutoEventWireup="false" MasterPageFile="~/SiteEPGBootstrap.Master"
    CodeBehind="GenreMaster.aspx.vb" Inherits="EPG.GenreMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
        function hideDiv() {
            $("#popme").slideUp(200);
            $("#black_overlay").slideUp(200);
        }
        function clientMessage() {
            try {

                var vControlId = $("#jsControlId").val();
                var vimgControlid = $("#jsImageId").val();
                var vHyControlId = $("#jsHyControlId").val();

                var fileName = $("#MainContent_jsFileName").val();
                //alert(vControlId);
                $("#" + vControlId).html(fileName);
                //                alert($("#MainContent_jsFileName").val());
                //                alert(fileName);
                //                alert(vimgControlid);
                $("#" + vHyControlId).attr("href", "../uploads/genre/" + $("#MainContent_jsFileName").val());
                $("#" + vimgControlid).attr("src", "../uploads/genre/" + $("#MainContent_jsFileName").val());



                hideDiv();
            }
            catch (ex) {
                alert("Error : " + ex.Message);
            }


        }
        function showDiv(jsControlId, jsHyControlId, jsImageId, jsGenreId, jsFileName) {
            try {
                $("#jsImageId").val(jsImageId);
                $("#jsControlId").val(jsControlId);
                $("#jsHyControlId").val(jsHyControlId);

                document.getElementById("MainContent_jsFileName").value = jsFileName;
                document.getElementById("MainContent_jsGenreId").value = jsGenreId;

                $("#popme").delay(200).slideDown(300);
                $("#black_overlay").delay(200).slideDown(200);
            }
            catch (ex) {
                alert(ex.ToString());
            }
        }
            
    </script>
    <link href="../lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../lightbox.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Genre Master</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                    &nbsp;</div>
                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Genre Id</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtGenreID" runat="server" CssClass="form-control" MaxLength="4" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtGenreID" ControlToValidate="txtGenreID" runat="server"
                                            ForeColor="Red" Text="*" ValidationGroup="RFGGenreMaster" />
                                        <asp:RegularExpressionValidator ID="REVtxtGenreID" runat="server" ControlToValidate="txtGenreID"
                                            ForeColor="Red" ValidationExpression="^[0-9]+$" Text="* No.s" ValidationGroup="RFGGenreMaster" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Genre Name</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtGenreName" runat="server" CssClass="form-control" MaxLength="50" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtGenreName" ControlToValidate="txtGenreName"
                                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGGenreMaster" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Event/Service</label>
                                <div class="input-group">
                                    <asp:RadioButtonList ID="RBCategory" runat="server" CssClass="form-control" RepeatDirection="Horizontal"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="Service" Value="S" Selected="True" />
                                        <asp:ListItem Text="Event" Value="E" />
                                        <asp:ListItem Text="Program" Value="P" />
                                    </asp:RadioButtonList>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <br />
                            <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0" />
                            <asp:Button ID="btnAddGenre" runat="server" Text="Add" ValidationGroup="RFGGenreMaster"
                                CssClass="btn btn-info" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:GridView ID="grdGenreMaster" runat="server" AutoGenerateColumns="False" CssClass="table"
                                CellPadding="4" DataKeyNames="GenreId" DataSourceID="sqlDSGenreMaster" ForeColor="Black"
                                GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="20"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sno.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="false" />
                                            <asp:Label ID="lbSno" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Genre Id" SortExpression="GenreId" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lbGenreId" runat="server" Text='<%# Bind("GenreId") %>' Visible="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Genre Name" SortExpression="GenreName" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lbGenreName" runat="server" Text='<%# Bind("GenreName") %>' Visible="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Genre Category" SortExpression="GenreCategory" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lbGenreCategory" runat="server" Text='<%# Bind("GenreCategory") %>'
                                                Visible="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image" SortExpression="profilePath">
                                        <ItemTemplate>
                                            <asp:Label ID="lbGenrePicURL" runat="server" Text='<%#Eval("GenrePic") %>' Visible="false" />
                                            <asp:HyperLink ID="hyLogo" rel="lightbox" runat="server" NavigateUrl='<%# Bind("GenrePic") %>'>
                                                <asp:Image ID="imgGenrePic" runat="server" ImageUrl='<%# Bind("GenrePic") %>' Width="60px"
                                                    Height="60px" />
                                            </asp:HyperLink>
                                            <asp:Label ID="lbFileName" runat="server" Visible="false" Text='<%#Eval("filename") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Upload Logo">
                                        <ItemTemplate>
                                            <br />
                                            <asp:HyperLink ID="hyUpload" runat="server" Text="Upload Image" ImageUrl="~/Images/upload.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Priority">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpriority" runat="server" Text='<%# Bind("priority") %>' />
                                            <asp:ImageButton ID="btnUp" runat="server" ImageUrl="~/Images/cal_plus.gif" CommandName="up"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                            <asp:ImageButton ID="btnDown" runat="server" ImageUrl="~/Images/cal_minus.gif" CommandName="down"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Images/Edit.png" />
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images/delete.png" />
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
                            <asp:SqlDataSource ID="sqlDSGenreMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                SelectCommand="SELECT *,(replace(genrename,'/','_') + '_' + convert(varchar,genreid) + '.jpg') filename FROM mst_Genre where genrecategory=@genrecategory  order by priority"
                                DeleteCommand="sp_mst_Genre" DeleteCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="RBCategory" PropertyName="SelectedValue" Name="genrecategory" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                    &nbsp;</div>
            </div>
        </div>
    </div>
    <div id="popme">
        <button type="button" style="float: right" onclick="hideDiv();">
            X</button><br />
        <input type="hidden" id="jsGenreId" size="50" runat="server" />
        <input type="hidden" id="jsImageId" size="50" />
        <input type="hidden" id="jsControlId" size="50" />
        <input type="hidden" id="jsHyControlId" size="50" />
        <input type="hidden" id="jsFileName" size="50" runat="server" />
        <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" UploadingBackColor="#82CAFA"
            CompleteBackColor="#FFFFFF" Filter="*.jpg;*.jpeg;*.png|Supported Images Types (*.jpg;*.jpeg;*.png)"
            OnClientUploadComplete="clientMessage" OnUploadedComplete="AsyncFileUpload1_UploadedComplete"
            ThrobberID="Image1" ProgressInterval="50" SaveBufferSize="128" Width="200px"
            EnableProgress="OnSubmit, OnPreview" />
        <br />
        <span style="color: Red">only .png, .jpg and .jpeg files supported</span>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/loader.gif" />
        <br />
    </div>
    <div id="black_overlay">
    </div>
</asp:Content>
