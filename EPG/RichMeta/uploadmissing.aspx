<%@ Page Title="Upload Missing" Language="vb" AutoEventWireup="false" MasterPageFile="~/RichMeta/SiteRich.Master"
    CodeBehind="uploadmissing.aspx.vb" Inherits="EPG.uploadmissing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sc1" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">Upload Missing</li>
        </ol>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    
                    <div class="col-md-3">
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-info" Text="Export Missing" />
                    </div>
                    <div class="col-md-3">
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
                    <div class="col-md-3">
                        <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-info" Text="Upload RichMeta" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lbUploadError" runat="server" ForeColor="Red" Text="Please select file first!"
                            Visible="false" />
                    </div>
                    <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-striped"
                        PageSize="30" AllowPaging="true" DataSourceID="sqlDS" EmptyDataText="--no record found--"
                        EmptyDataRowStyle-CssClass="alert alert-danger text-center" SelectedRowStyle-BackColor="#D0021B"
                        SelectedRowStyle-ForeColor="#ffffff" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Type" DataField="Type" />
                            <asp:BoundField HeaderText="Name" DataField="Name" />
                            <asp:BoundField HeaderText="parentalguide" DataField="parentalguide" />
                            <asp:BoundField HeaderText="seasonno" DataField="seasonno" />
                            <asp:BoundField HeaderText="seasonname" DataField="seasonname" />
                            <asp:BoundField HeaderText="synopsis" DataField="synopsis" />
                            <asp:BoundField HeaderText="starcast" DataField="starcast" />
                            <asp:BoundField HeaderText="director" DataField="director" />
                            <asp:BoundField HeaderText="trivia" DataField="trivia" />
                            <asp:BoundField HeaderText="releaseyear" DataField="releaseyear" />
                            <asp:BoundField HeaderText="Producer" DataField="producer" />
                            <asp:BoundField HeaderText="Country" DataField="Country" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select * from v_rich_missingdata  order by type,name"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
</asp:Content>
