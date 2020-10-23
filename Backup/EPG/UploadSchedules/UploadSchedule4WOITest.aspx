<%@ Page Title="WOI AUTO UPLOAD MODULE #4" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="UploadSchedule4WOITest.aspx.vb" Inherits="EPG.UploadSchedule4WOITest" %>

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
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />
    <div class="panel panel-default">
        <div class="panel-heading text-center">
            <h3>
                WOI AUTO UPLOAD MODULE #4
            </h3>
        </div>
        <div class="panel-body">
            <div class="row">
                
                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" placeholder="Select Channel" />
                        <span class="input-group-addon">
                            
                            <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="txtChannel" FirstRowSelected="true" />
                        </span>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 text-right">
                    <div class="input-group">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                        <span class="input-group-addon">
                            <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1"
                                runat="server" Text="xls/zip" ValidationExpression="^.*\.(xls|XLS|zip|ZIP)$"
                                ForeColor="Red" ValidationGroup="AUtoWOI" />
                            <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server"
                                Text="*"  ForeColor="Red" ValidationGroup="AUtoWOI" />
                        </span>
                    </div>
                </div>
                <div class="col-lg-1 col-md-1 text-left">
                    <asp:Button ID="btnUpload" runat="server" Text="UPLOAD" CssClass="btn btn-sm btn-info"
                        ValidationGroup="AUtoWOI" />
                </div>
                <div class="col-lg-6 col-md-6 text-center alert-info">
                    <asp:Label ID="lbChannel" runat="server" CssClass="h3" Text="Channel" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 text-right">
                    <b>Last</b>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                    <asp:DropDownList ID="ddlHour" runat="server" CssClass="form-control" AutoPostBack="true">
                        <asp:ListItem Text="1 Hour" Value="1" Selected="True" />
                        <asp:ListItem Text="2 Hours" Value="2" />
                        <asp:ListItem Text="4 Hours" Value="4" />
                        <asp:ListItem Text="8 Hours" Value="8" />
                        <asp:ListItem Text="12 Hours" Value="12" />
                        <asp:ListItem Text="16 Hours" Value="16" />
                    </asp:DropDownList>
                </div>
                
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <asp:GridView ID="grdUploaded" runat="server" AllowPaging="True" AllowSorting="True"  OnSorting="SortRecords"
                    PageSize="150" CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <asp:GridView ID="grdExcelData" runat="server" AllowPaging="True" AllowSorting="True"
                    CssClass="table" CellPadding="4" ForeColor="Black" GridLines="Vertical" PagerSettings-PageButtonCount="10"
                    PageSize="150" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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
            </div>
        </div>
    </div>
    
    <br />
    <br />
    <br />
</asp:Content>
