<%@ Page Title="View Rich" Language="vb" AutoEventWireup="false" MasterPageFile="~/RichMeta/SiteRichLess.Master"
    CodeBehind="viewrich.aspx.vb" Inherits="EPG.viewrich" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sc1" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">View Rich</li>
        </ol>
    </div>
    <asp:Label ID="lbError" runat="server" CssClass="alert-danger h2" Visible="false" Text="No record found !" />
    <div class="row" id="divRich" runat="server">
        
                    <table class="table table-bordered table-striped table-hover">
                        <tr>
                            <th>
                                Type
                            </th>
                            <td>
                                <asp:Label ID="lbType" runat="server" CssClass="h3 text-danger"  />
                            </td>
                            <th>
                                Name
                            </th>
                            <td>
                                <asp:Label ID="lbName" runat="server" CssClass="h3 text-danger" />
                            </td>
                             <th>
                                Year
                            </th>
                            <td>
                                <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" />
                            </td>
                           
                        </tr>
                        <tr>
                            <th>
                                Orig Lang
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlOrigLangauge" runat="server" CssClass="form-control" DataSourceID="sqlDSLanguage"
                                    DataTextField="Fullname" DataValueField="languageid" />
                            </td>
                            <th>
                                Dubbed Lang
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDubbedLanguage" runat="server" CssClass="form-control" DataSourceID="sqlDSLanguage"
                                    DataTextField="Fullname" DataValueField="languageid" />
                            </td>
                            <th>
                                Season no
                            </th>
                            <td>
                                <asp:TextBox ID="txtSeasonNo" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Genre
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlGenre" runat="server" CssClass="form-control" DataSourceID="sqlDSGenre"
                                    DataTextField="genrename" DataValueField="genreid" AutoPostBack="true" />
                                
                            </td>
                            <th>
                                Subgenre
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlSubGenre" runat="server" CssClass="form-control" DataSourceID="sqlDSSubGenre"
                                    DataTextField="subgenrename" DataValueField="subgenreid" />
                                
                            </td>
                             <th>
                                Season Name
                            </th>
                            <td>
                                <asp:TextBox ID="txtSeasonName" runat="server"  CssClass="form-control"/>
                            </td>
                           
                        </tr>
                        
                        
                        <tr>
                         <th>
                                Synopsis
                            </th>
                            <td>
                                <asp:TextBox ID="txtSynopsis" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                            </td>
                            <th>
                                Starcast
                            </th>
                            <td>
                                <asp:TextBox ID="txtStarCast" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                            </td>
                            <th>
                                Director
                            </th>
                            <td>
                                <asp:TextBox ID="txtDirector" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                            </td>
                            
                        </tr>
                        <tr>
                        <th>
                                Producer
                            </th>
                            <td>
                                <asp:TextBox ID="txtProducer" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                            </td>
                            <th>
                                Writer
                            </th>
                            <td>
                                <asp:TextBox ID="txtWriter" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" />
                            </td>
                             <th>
                                Country
                            </th>
                            <td>
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                         <th>
                                Verified
                            </th>
                            <td>
                                <asp:CheckBox ID="chkVerified" runat="server" CssClass="form-control" />
                            </td>
                            <td class="text-center">
                                <asp:Button ID="btnAdd" runat="server" Text="Update" CssClass="btn btn-sm alert-info" />&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbID" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
        
    </div>
    <div class="row">
        <h3 class="alert-danger">Richmeta linked with:</h3>
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
    <asp:SqlDataSource ID="sqlDSLanguage" runat="server" SelectCommand="select '0' languageid, '-- select --' fullname  union select languageid,fullname from mst_language where active=1  order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSGenre" runat="server" SelectCommand="select '0' genreid, '-- select --' genrename  union  select genreid,genrename from mst_genre where genrecategory='P' order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSSubGenre" runat="server" SelectCommand="select subgenreid,subgenrename from mst_subgenre where genreid=@genreid order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlGenre" PropertyName="SelectedValue" Name="genreid"
                Type="Int16" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
