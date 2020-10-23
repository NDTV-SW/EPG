<%@ Page Title="Upload Schedule #9" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="UploadSchedule9_TimeZone.aspx.vb" Inherits="EPG.UploadSchedule9_TimeZone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .ajax__combobox_itemlist
        {
            position: absolute !important;
            height: 300px !important;
            top: auto !important;
            left: auto !important;
        }
    </style>
    <script type="text/javascript">
        function SetContextKey(aceid, exelmovie) {
            //
            //alert(exelmovie);
            $find(aceid).set_contextKey(exelmovie);
            //alert(aceid);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Upload Schedule #9 TimeZone</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="btn-lg alert-success">
                            <b>Note :</b> Sample Format of Excel to Upload
                        </div>
                        <asp:Image ID="imgSampleScedUpload" ImageUrl="~/Images/sampleupload9.JPG" runat="server"
                            CssClass="btn-lg alert-success table" />
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                                    <span class="input-group-addon">
                                        <asp:Label ID="lbIsMovieChannel" runat="server" />
                                        <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtChannel" FirstRowSelected="true" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Original Time Zone</label>
                                <asp:DropDownList ID="ddlTZ" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="IST" Value="0" />
                                    <asp:ListItem Text="Hong Kong/Singapore" Value="-150" />
                                    <asp:ListItem Text="Indonesia" Value="-90" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select File</label>
                                <div class="input-group">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1"
                                            runat="server" Text="*" ErrorMessage="Only .xls file Supported !" ValidationExpression="^.*\.(xls|XLS)$"
                                            ForeColor="Red" ValidationGroup="VGUploadSched" />
                                        <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server"
                                            Text="*" ErrorMessage="Please select file first !" ForeColor="Red" ValidationGroup="VGUploadSched" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    &nbsp;
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnUpload" runat="server" Text="UPLOAD" UseSubmitBehavior="false"
                                        CssClass="btn btn-info" ValidationGroup="VGUploadSched" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-heading text-center">
                    <div class="btn-lg alert-info">
                        Excel Data</div>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:GridView ID="grdExcelData" runat="server" AllowPaging="True" AllowSorting="True"
                            CssClass="table" CellPadding="4" ForeColor="Black" GridLines="Vertical" PagerSettings-PageButtonCount="10"
                            PageSize="100" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
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
                        <asp:TextBox ID="txtProgName" runat="server" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId">
    </asp:SqlDataSource>
</asp:Content>
