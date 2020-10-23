<%@ Page Title="Upload Movies Logos" Language="vb" MasterPageFile="~/Site.Master"
    AutoEventWireup="false" CodeBehind="UploadMovieLogos.aspx.vb" Inherits="EPG.UploadMovieLogos" %>

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
    <fieldset>
        <legend>Upload Movie Logos</legend>
        <div style="text-align: center">
            <table width="98%">
                <tr>
                    <th width="10%" valign="top" align="right">
                        Select Movie
                    </th>
                    <td width="40%" valign="top" align="left">
                        <asp:ComboBox ID="ddlMovieName" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                            ItemInsertLocation="Append" Width="200px" DataSourceID="SqlDSMovieName" DataTextField="MovieName"
                            DataValueField="RowId" AutoPostBack="True">
                        </asp:ComboBox>
                        <asp:SqlDataSource ID="SqlDSMovieName" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="select rowid,moviename from mst_moviesdb order by moviename">
                        </asp:SqlDataSource>
                        <br />
                    </td>
                    <td>
                        <asp:Label ID="lbStatus" runat="server" Style="color: #FF0000"></asp:Label>
                    </td>
                    <td rowspan="3" valign="top" width="20%">
                        <asp:HyperLink rel="lightbox" ID="hyProgramLogo" runat="server">
                            <asp:Image ID="imgProgramlogo" Visible="true" Width="100px" Height="100px" runat="server" AlternateText="NDTV" /><br />
                            <asp:Label ID="lbImageSize" runat="server" />
                        </asp:HyperLink></td><td rowspan="2" align="left" width="30%">
                        
                        <b>Starcast:</b> <asp:Label ID="lbStarCast" runat="server" /><br />
                        <b>Genre:</b> <asp:Label ID="lbGenre" runat="server" /><br />
                        <b>Release Year:</b> <asp:Label ID="lbReleaseYear" runat="server" /><br />
                        <b>Genre:</b> <asp:Label ID="lbLanguageId" runat="server" /><br />
                        <b>Director:</b> <asp:Label ID="lbDirector" runat="server" /><br />
                        <b>Country:</b> <asp:Label ID="lbcountry" runat="server" /><br />
                    </td>
                </tr>
                <tr>
                    <th align="right">
                        Upload Image </th><td align="left" valign="bottom">
                        <%--OnUploadedComplete="doUpload"--%>
                        <asp:AsyncFileUpload ID="AjaxFileUpload11" runat="server" UploadingBackColor="#82CAFA"
                            CompleteBackColor="#FFFFFF" Filter="*.jpg;*.bmp;*.gif;*.png|Supported Images Types (*.jpg;*.bmp;*.gif;*.png)"
                            OnClientUploadComplete="clientMessage" ThrobberID="Throbber" ProgressInterval="50"
                            SaveBufferSize="128" Width="200px" EnableProgress="OnSubmit, OnPreview" />
                        <asp:Image ID="Throbber" runat="server" ImageUrl="~/Images/loader.gif" />
                        <strong><font style="color: Green">
                            <div id="label1">
                            </div>
                        </font></strong><span style="color: Red">Only .jpg & .jpeg files supported</span> </td><td align="left">
                        <asp:Button ID="btnUpload" runat="server" Text="UPDATE" />&nbsp; <asp:Button ID="btnRemove" runat="server" Text="Remove Image" />
                    </td>
                </tr>
                <!--tr>
                <td>&nbsp;</td>
                <td colspan="4" align="left"><span style="color:red; font-weight:bold;"><br />NOTE: <br />1. Rows in GREEN are part of this channel’s EPG for next 7 days. <br />2. Rows in BLUE are part of EPG but not in next 7 days. </span></td></tr><tr-->
                <tr>
                    <td colspan="4" align="center">
                        <hr />
                        
                        <span style="color:Black; font-weight:bold; font-size:large">IMAGES MISSING FOR THESE MOVIES</span></td></tr>
                        <tr>
                            <th align="right">
                                Select Language
                            </th>
                            <td align="left">
                                <asp:DropDownList ID="ddlLanguage" runat="server" DataTextField="FullName" DataValueField="languageid"
                                    DataSourceID="sqlDSLanguage"  AutoPostBack="true" />
                                <asp:SqlDataSource ID="sqlDSLanguage" runat="server" SelectCommand="select * from mst_language where active=1 order by languageid"
                                    SelectCommandType="Text" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
                                </asp:SqlDataSource>
                            
                                <b>Movies that are in EPG</b>
                                <asp:CheckBox ID="chkInEPG" Checked="true" runat="server" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr>
                        
                    <td colspan="5" align="left">
                        <asp:GridView ID="grdProgImage" runat="server" AutoGenerateColumns="False" DataSourceID="sqlDSProgImage"
                            AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="vertical"
                            PageSize="100">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Row Id" SortExpression="RowId">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbRowId" Text='<%# Bind("RowId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MovieName" SortExpression="MovieName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbMovieName" Text='<%# Bind("MovieName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="StarCast" SortExpression="StarCast">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbStarCast" Text='<%# Bind("StarCast") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ReleaseYear" SortExpression="ReleaseYear">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbReleaseYear" Text='<%# Bind("ReleaseYear") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Language" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbFullName" Text='<%# Bind("FullName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TMDBImageUrl" SortExpression="TMDBImageUrl">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbTMDBImageUrl" Text='<%# Bind("TMDBImageUrl") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                            <asp:Image runat="server" ID="imglogo" AlternateText="NDTV" Width="50px" Height="50px" />
                                        </asp:HyperLink></ItemTemplate></asp:TemplateField><asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Images/Edit.png" />
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlDSProgImage" runat="server" SelectCommand="rpt_movies"
                            SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
                            <SelectParameters>
                                <asp:ControlParameter Name="movielangid" ControlID="ddlLanguage" PropertyName="SelectedValue" Type="String" />
                                <asp:ControlParameter ControlID="chkInEPG" Name="InEPG" PropertyName="checked" Type="Boolean" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    </asp:Content>