<%@ Page Title="Dictionary Master" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="DictionaryMaster.aspx.vb" Inherits="EPG.DictionaryMaster"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Dictionary Master</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select Language</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" AutoCompleteMode="SuggestAppend"
                                        DropDownStyle="DropDownList" ItemInsertLocation="Append" AppendDataBoundItems="true"
                                     />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-info" Text="View" /> &nbsp; &nbsp;
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" Text="Fetch and Feed" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="grdDictionary" runat="server" CssClass="table" >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
