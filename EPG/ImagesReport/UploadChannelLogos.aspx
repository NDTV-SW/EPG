<%@ Page Title="Channel Logos" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master" AutoEventWireup="false" CodeBehind="UploadChannelLogos.aspx.vb" Inherits="EPG.UploadChannelLogos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function clientMessage() {
            document.getElementById("label1").innerHTML = "File Uploaded.";
        }
    </script>

    <link href="lightbox.css" rel="stylesheet" type="text/css" />
    <script src="lightbox.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>Upload Channel Logos</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Select Channel
                                </label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannelName" runat="server" CssClass="form-control"
                                        DataSourceID="SqlDSChannel" DataTextField="Channelid"
                                        DataValueField="Channelid">
                                    </asp:DropDownList>
                                    <span class="input-group-addon">

                                        <asp:SqlDataSource ID="SqlDSChannel" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="select channelid from mst_channel where Onair=1 and active=1 and sendepg=1 and companyid<>28 order by channelid"></asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Upload Image
                                </label>
                                <div class="input-group">
                                    <%--OnUploadedComplete="doUpload"--%>
                                    <asp:AsyncFileUpload ID="AjaxFileUpload11" runat="server"
                                        UploadingBackColor="#82CAFA"
                                        CompleteBackColor="#FFFFFF"
                                        Filter="*.jpg;*.bmp;*.gif;*.png|Supported Images Types (*.jpg;*.bmp;*.gif;*.png)"
                                        OnClientUploadComplete="clientMessage"
                                        ThrobberID="Throbber"
                                        ProgressInterval="50"
                                        SaveBufferSize="128"
                                        EnableProgress="OnSubmit, OnPreview" />
                                    <asp:Image ID="Throbber" runat="server"
                                        ImageUrl="~/Images/loader.gif" />

                                    <strong><font style="color: Green"><div id="label1"></div></font></strong>

                                    <span style="color: Red">Only .jpg, .jpeg & .png files supported</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;
                                </label>
                                <div class="input-group">
                                    <asp:Label ID="lbStatus" runat="server" Style="color: #FF0000"></asp:Label>
                                    <asp:Button ID="btnUpload" runat="server" Text="UPDATE" CssClass="btn btn-info" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove Image" CssClass="btn btn-danger" />
                                </div>
                            </div>
                        </div>
						<div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <div class="input-group" CssClass="form-control">
                                    <asp:HyperLink rel="lightbox" ID="hyChannelLogo" runat="server" >
                                        <asp:Image ID="imgChannellogo" Visible="true" runat="server" AlternateText="NDTV" />
                                    </asp:HyperLink>
                                    <span class="input-group-addon">
                                        <asp:Label ID="lbImageSize" runat="server" />
                                    </span>
                                </div>
                                
                            </div>
                            <br /><br /><br />
                        </div>
                        <asp:DataList ID="grdChannelLogo" runat="server" CssClass="table"
                            DataSourceID="sqlDSProgImage" RepeatColumns="4"
                            RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <table border="1" cellpadding="10px" cellspacing="0">
                                    <tr>
                                        <td width="200px" align="left">
                                            <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("channelid") %>' />
                                        </td>
                                        <td align="left">
                                            <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                                <asp:Image runat="server" ID="imglogo" AlternateText="NDTV" Width="50px" Height="50px" />
                                            </asp:HyperLink></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <table border="1" cellpadding="10px" cellspacing="0">
                                    <tr>
                                        <th width="200px" align="left">
                                            <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("channelid") %>' />
                                        </th>
                                        <td align="left">
                                            <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                                <asp:Image runat="server" ID="imglogo" AlternateText="NDTV" Width="50px" Height="50px" />
                                            </asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </AlternatingItemTemplate>

                        </asp:DataList>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSProgImage" runat="server" SelectCommand="select  channelid from mst_channel where Onair=1 and active=1 and sendepg=1 and companyid<>28 order by channelid" SelectCommandType="Text"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlChannelName" Name="channelid" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
