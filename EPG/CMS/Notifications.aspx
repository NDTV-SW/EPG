<%@ Page Title="Notifications" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="Notifications.aspx.vb" Inherits="EPG.Notifications" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="http://epgops.ndtv.com/lightbox.css" rel="stylesheet" type="text/css" />
    <script src="http://epgops.ndtv.com/lightbox.js" type="text/javascript"></script>
    <%--<script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <script src="../js/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../js/calendar-en.min.js" type="text/javascript"></script>
    <link href="../css/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtKillTime  .ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%Y/%m/%d %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
        function getProg() {
            window.open("notificationSelectProg.aspx", "Select Programme", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
            //alert("Clicked Select");
        }
        function pushNotification(theUrl) {

            var frame = document.createElement("iframe");
            frame.src = 'http://apis.epgops.ndtv.com:3021/notification/publish?id=' + theUrl;
            frame.style.position = "relative";
            frame.style.left = "-9999px";
            document.body.appendChild(frame);
            alert('Notification sent');
        }
        function pushTestNotification(theUrl) {

            var frame = document.createElement("iframe");
            frame.src = 'http://apis.epgops.ndtv.com:3021/notification/publish-test?id=' + theUrl;
            frame.style.position = "relative";
            frame.style.left = "-9999px";
            document.body.appendChild(frame);
            alert('Notification sent');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Notifications</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Title</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Rows="2" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFV_txtTitle" runat="server" ControlToValidate="txtTitle"
                                            Text="*" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Body</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtBody" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Rows="2" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFV_txtBody" runat="server" ControlToValidate="txtBody"
                                            Text="*" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Category</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="home">Home</asp:ListItem>
                                        <asp:ListItem Value="shows">Shows</asp:ListItem>
                                        <asp:ListItem Value="movies">Movies</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Kill Time</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtKillTime" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFV_txtKillTime" runat="server" ControlToValidate="txtKillTime"
                                            Text="*" ForeColor="Red" />
                                        <img src="../images/calendar.png" />
                                    </span>
                                </div>
                            </div>
                        </div>
                       <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    &nbsp;</label>
                                <div class="input-group">
                                    <asp:HyperLink ID="hySelectProgName" runat="server" Text="Select Programme" onClick="javascript:getProg();"
                                        CssClass="btn-block btn btn-warning" />
                                </div>
                            </div>
                        </div> 
                        <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6 ">
                            <div class="form-group pull-right">
                                <label>
                                    Auto?</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkNotifyType" runat="server"  />
                                </div>
                            </div>
                        </div>
                        
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Action URL</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtURL" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Rows="3" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REV_txtURL" runat="server" ValidationExpression="^((http[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)?$"
                                            ControlToValidate="txtURL" Text="*" ForeColor="Red" />
                                        <asp:RequiredFieldValidator ID="RFV_txtURL" runat="server" ControlToValidate="txtURL"
                                            Text="*" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Keywords (comma separated)</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Rows="3" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="REV_txtKeywords" runat="server" ControlToValidate="txtKeywords"
                                            Text="*" ForeColor="Red" Enabled="false" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Genre Keywords (comma separated)</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtGenreKeywords" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Rows="3" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtGenreKeywords" runat="server" ControlToValidate="txtGenreKeywords"
                                            Text="*" ForeColor="Red" Enabled="false" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Language Keywords (comma separated)</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtLangKeywords" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Rows="3" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtLangKeywords" runat="server" ControlToValidate="txtLangKeywords"
                                            Text="*" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Repeat</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkRepeat" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                            <div class="form-group">
                                <label>
                                    Repeat After Duration</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlRepeatDuration" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="30">0 Hr 30 Mins</asp:ListItem>
                                        <asp:ListItem Value="60">1 Hr 0 mins</asp:ListItem>
                                        <asp:ListItem Value="90">1 Hr 30 Mins</asp:ListItem>
                                        <asp:ListItem Value="120">2 Hrs 0 mins</asp:ListItem>
                                        <asp:ListItem Value="150">2 Hrs 30 Mins</asp:ListItem>
                                        <asp:ListItem Value="180">3 Hrs 0 mins</asp:ListItem>
                                        <asp:ListItem Value="210">3 Hrs 30 Mins</asp:ListItem>
                                        <asp:ListItem Value="240">4 Hrs 0 mins</asp:ListItem>
                                        <asp:ListItem Value="270">4 Hrs 30 Mins</asp:ListItem>
                                        <asp:ListItem Value="300">5 Hrs 0 mins</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Notify All</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkNotifyAll" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Upload Image</label>
                                <div class="input-group">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:Button ID="btnUpload" Text="Upload" runat="server" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Image</label>
                                <div class="input-group">
                                    <asp:Image ID="imgPreview" runat="server" Width="144" Height="54" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-info" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                    <asp:Label ID="lbRowid" runat="server" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                    <asp:GridView ID="grdNotifications" runat="server" AutoGenerateColumns="False" CssClass="table"
                        DataSourceID="sqlDSBanners" AllowPaging="True" AllowSorting="True" CellPadding="4"
                        ForeColor="Black" GridLines="Vertical" PageSize="20" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbRowId" runat="server" Text='<%#EVAL("rowid") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbTitle" Text='<%# Bind("Title") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Body" SortExpression="Body">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbBody" Text='<%# Bind("Body") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" SortExpression="Category">
                                <ItemTemplate>
                                    <asp:Label ID="lbCategory" runat="server" Text='<%#EVAL("Category") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ActionURL" SortExpression="ActionURL">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbActionURL" Text='<%# Bind("ActionURL") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="KillTime" SortExpression="KillTime">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbKillTime" Text='<%# Bind("KillTime") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Repeat" SortExpression="Repeat">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkRepeat" Checked='<%# Bind("Repeat") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Repeat Duration" SortExpression="RepeatDuration">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbRepeatDuration" Text='<%# Bind("RepeatDuration") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Keywords" SortExpression="Keywords">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbKeywords" Text='<%# Bind("Keywords") %>' />
                                    <asp:Label runat="server" ID="lbGenreKeyword" Text='<%# Bind("GenreKeyword") %>'
                                        Visible="false" />
                                    <asp:Label runat="server" ID="lbnotificationtype" Text='<%# Bind("notificationtype") %>'
                                        Visible="false" />
                                    <asp:Label runat="server" ID="lbLanguageKeyword" Text='<%# Bind("LanguageKeyword") %>'
                                        Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notify All" SortExpression="notifyall">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkNotifyAll" Checked='<%# Bind("notifyall") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server" NavigateUrl='<%# Bind("img") %>'>
                                        <asp:Image runat="server" ID="imglogo" ImageUrl='<%# Bind("img") %>' AlternateText="NDTV"
                                            Width="150px" Height="60px" />
                                    </asp:HyperLink></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Keywords" SortExpression="Keywords">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hyPush" runat="server" NavigateUrl=""><img src="../Images/push.jpg" /></asp:HyperLink>
                                    <asp:HyperLink ID="hyTestPush" runat="server" NavigateUrl="" CssClass=" btn btn-xs btn-info">Test Push  </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" />
                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Images/delete.png" />
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
                    <asp:SqlDataSource ID="sqlDSBanners" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                        SelectCommand="select * from mst_notifications order by killtime desc"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
