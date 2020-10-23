<%@ Page Title="Import Transliteration" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="ImportTransliteration.aspx.vb"
    Inherits="EPG.ImportTransliteration" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
    <div class="panel panel-info">
        <div class="panel-heading text-center">
            <h4>
                Import Transliteration</h4>
        </div>
        <div class="panel-body"><div class="row">
        <asp:Image ID="imgSample" runat="server" ImageUrl="~/images/sampledictionaryImport.jpg" />
                <div class="col-md-2">
                    <div class="form-group">
                        <label>
                            Language
                        </label>
                        <div class="input-group">
                            <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Value="10">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>
                        </label>
                        <div class="input-group">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                            <span class="input-group-addon">
                                <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1"
                                    runat="server" Text="*" ErrorMessage="Only .xls file Supported !" ValidationExpression="^.*\.(xls|XLS)$"
                                    ForeColor="Red" ValidationGroup="VGImportPC" />
                                <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server"
                                    Text="*" ErrorMessage="Please select file first !" ForeColor="Red" ValidationGroup="VGImportPC" />
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>
                        </label>
                        <div class="input-group">
                            <asp:Button ID="btnUpload" runat="server" Text="Import" Enabled="true" ValidationGroup="VGImportPC"
                                UseSubmitBehavior="false" CssClass="btn btn-info" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lbError" runat="server" CssClass="label alert-danger" />
                </div>
            </div>
            
            <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlDS" />
        </div>
    </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select top 1000 * from dictionary_dump_import where languageid=@languageid order by lastupdate desc">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlLanguage" Direction="Input" Name="Languageid"
                Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
