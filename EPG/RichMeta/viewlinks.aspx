<%@ Page Title="View Links" Language="vb" AutoEventWireup="false" MasterPageFile="~/RichMeta/SiteRichLess.Master"
    CodeBehind="viewlinks.aspx.vb" Inherits="EPG.viewlinks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sc1" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">View Links</li>
        </ol>
    </div>
    <div class="row">
        <asp:GridView ID="grd" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="no record found" EmptyDataRowStyle-CssClass="alert-danger text-center" AutoGenerateColumns="false">
            <Columns>
                
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                            <asp:Label ID="lbID" runat="server" Text='<%#Eval("progid") %>' Visible="false" />
                            <asp:Label ID="lbRichMetaId" runat="server" Text='<%#Eval("RichMetaId") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="channelid" HeaderText="Channel" />
                    <asp:BoundField DataField="progname" HeaderText="Program" />
                    <asp:CommandField ButtonType="Button" DeleteText="Remove Link" ShowDeleteButton="true" 
                    ItemStyle-CssClass="alert-danger1" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
