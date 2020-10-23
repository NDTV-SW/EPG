<%@ Page Title="Verify Rich Meta" Language="vb" AutoEventWireup="false" MasterPageFile="~/RichMeta/SiteRich.Master"
    CodeBehind="verifyrichmeta.aspx.vb" Inherits="EPG.verifyrichmeta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sc1" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">Verify Rich Meta</li>
        </ol>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-body">
                    <table class="table table-bordered table-striped">
                        <tr>
                            <th>
                                Type
                                <asp:RequiredFieldValidator ID="RFV_ddlType" runat="server" ControlToValidate="ddlType"
                                    Text="*Req" ForeColor="Red" ValidationGroup="VG" InitialValue="0" />
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--select--" Value="0" />
                                    <asp:ListItem Text="Movie" Value="Movie" />
                                    <asp:ListItem Text="Show" Value="Show" />
                                    <asp:ListItem Text="News" Value="News" />
                                    <asp:ListItem Text="Sports" Value="Sports" />
                                </asp:DropDownList>
                            </td>
                            <th>
                                Broadcaster
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBroadcaster" runat="server" CssClass="form-control" DataSourceID="sqlDSBroadcaster"
                                    DataValueField="companyid" DataTextField="companyname" />
                            </td>
                            
                            <th>
                                Name
                                <asp:RequiredFieldValidator ID="RFV_txtName" runat="server" ControlToValidate="txtName"
                                    Text="*Req" ForeColor="Red" ValidationGroup="VG" />
                            </th>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Display Name
                            </th>
                            <td>
                                <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Original Language
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlOriginalLanguage" runat="server" CssClass="form-control"
                                    DataSourceID="sqlDSLanguage" DataValueField="languageid" DataTextField="fullname" />
                            </td>
                            <th>
                                Dubbed Language
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDubbedLanguage" runat="server" CssClass="form-control" DataSourceID="sqlDSLanguage"
                                    DataValueField="languageid" DataTextField="fullname" />
                            </td>
                           
                            <th>
                                Parental Guide
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlParentalGuide" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="U" Value="U" />
                                    <asp:ListItem Text="UA" Value="UA" />
                                    <asp:ListItem Text="A" Value="A" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Season No.
                            </th>
                            <td>
                                <asp:TextBox ID="txtSeasonNo" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Season Name
                            </th>
                            <td>
                                <asp:TextBox ID="txtSeasonName" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Release Year
                            </th>
                            <td>
                                <asp:TextBox ID="txtReleaseYear" runat="server" CssClass="form-control" />
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
                                Star Cast
                            </th>
                            <td>
                                <asp:TextBox ID="txtStarCast" runat="server" CssClass="form-control" TextMode="MultiLine"
                                    Rows="3" />
                            </td>
                            <th>
                                Synopsis
                            </th>
                            <td>
                                <asp:TextBox ID="txtSynopsis" runat="server" CssClass="form-control" TextMode="MultiLine"
                                    Rows="3" />
                            </td>
                            <th>
                                Long Synopsis
                            </th>
                            <td>
                                <asp:TextBox ID="txtLongSynopsis" runat="server" CssClass="form-control" TextMode="MultiLine"
                                    Rows="3" />
                            </td>
                            <th>
                                Tags
                            </th>
                            <td>
                                <asp:TextBox ID="txtTags" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Writer
                            </th>
                            <td>
                                <asp:TextBox ID="txtWriter" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Director
                            </th>
                            <td>
                                <asp:TextBox ID="txtDirector" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Producer
                            </th>
                            <td>
                                <asp:TextBox ID="txtProducer" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Trivia
                            </th>
                            <td>
                                <asp:TextBox ID="txtTrivia" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Rating
                            </th>
                            <td>
                                <asp:TextBox ID="txtRating" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Awards
                            </th>
                            <td>
                                <asp:TextBox ID="txtAwards" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Genre
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlGenre" runat="server" CssClass="form-control" DataSourceID="sqlDSGenre"
                                    DataTextField="genrename" DataValueField="genreid" AutoPostBack="true" />
                            </td>
                            <th>
                                Sub-Genre
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlSubGenre" runat="server" CssClass="form-control" DataSourceID="sqlDSSubGenre"
                                    DataTextField="subgenrename" DataValueField="genreid" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Sports Type
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlSportsType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Test" values="Test" />
                                </asp:DropDownList>
                            </td>
                            <th>
                                Series
                            </th>
                            <td>
                                <asp:TextBox ID="txtSeries" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Team 1
                            </th>
                            <td>
                                <asp:TextBox ID="txtTeam1" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Team 2
                            </th>
                            <td>
                                <asp:TextBox ID="txtTeam2" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Venue
                            </th>
                            <td>
                                <asp:TextBox ID="txtVenue" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                Show Host
                            </th>
                            <td>
                                <asp:TextBox ID="txtShowHost" runat="server" CssClass="form-control" />
                            </td>
                            <th>
                                <asp:CheckBox ID="chkVerified" runat="server" Text="Verified" CssClass="form-control chk" />
                            </th>
                            <th>
                                <asp:CheckBox ID="chkActive" runat="server" Text="Active" CssClass="form-control chk" />
                            </th>
                            <th colspan="2">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn alert-danger1 btn-sm"
                                    ValidationGroup="VG" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn alert-danger1 btn-sm" />
                                <asp:Label ID="lbId" runat="server" Visible="false" />
                            </th>
                        </tr>
                        </table>
                </div>
                <div class="panel-heading">
                    
                    <div class="col-md-2">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="search" CssClass="form-control" />
                        
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-xs alert-danger1" />
                    </div>
                </div>
                <div class="panel-body">
                    
                        <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-striped"
                            DataSourceID="sqlDS" EmptyDataText="--no record found--" DataKeyNames="id" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                             SelectedRowStyle-BackColor="#D0021B" SelectedRowStyle-ForeColor="#ffffff"
                            >
                            <Columns>
                                <asp:CommandField ButtonType="Image" ShowSelectButton="true" SelectImageUrl="~/Images/edit.png" />
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select * from v_verify_richmeta where isnull(verified,0)=0 order by type,name"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSBroadcaster" runat="server" SelectCommand="select '0' companyid, '-- select --' companyname union select CompanyId,CompanyName from mst_company where active=1 order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSLanguage" runat="server" SelectCommand="select '0' languageid, '-- select --' fullname  union select languageid,fullname from mst_language where active=1  order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSGenre" runat="server" SelectCommand="select '0' genreid, '-- select --' genrename  union  select genreid,genrename from mst_genre where genrecategory='P' order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSSubGenre" runat="server" SelectCommand="select genreid,subgenrename from mst_subgenre where genreid=@genreid order by 2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlGenre" PropertyName="SelectedValue" Name="genreid"
                Type="Int16" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
