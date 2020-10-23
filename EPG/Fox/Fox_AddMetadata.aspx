<%@ Page Language="vb" Title="Fox Upload meta data"  MasterPageFile="~/SiteEPGBootStrap.Master" MaintainScrollPositionOnPostback="true"
    EnableViewState="true" AutoEventWireup="false" CodeBehind="Fox_AddMetadata.aspx.vb" Inherits="EPG.Fox_AddMetadata" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openWin(progid) {
            window.open("viewProgTVStars.aspx?progid=" + progid, "Programme TV Stars", "width=850,height=550,toolbar=0,left=150,top=100,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                   ADD META DATA</h3>
            </div>
            <div class="panel-body container">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Shelf</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlShelf" runat="server" DataTextField="Shelf_Name" DataValueField="Shelf_Id"
                                    DataSourceID="sqlDSShelf" CssClass="form-control" AutoPostBack="true" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Title</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlTitle" runat="server" DataTextField="Shelf_Title" DataValueField="Shelf_Id"
                                    CssClass="form-control" AutoPostBack="true" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Season No </label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlSeasonNo" runat="server" DataTextField="Shelf_SeasonNo" DataValueField="Shelf_Id"
                                   CssClass="form-control" AutoPostBack="true" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                  <%--  <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                High TRP channels</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkHighTRP" runat="server" CssClass="form-control" Checked="true"
                                    AutoPostBack="true" Text="" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Missing Programmes</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkMissing" runat="server" CssClass="form-control" Checked="true"
                                    AutoPostBack="true" Text="" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                In EPG</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkInEPG" runat="server" CssClass="form-control" Checked="true"
                                    AutoPostBack="true" Text="" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grdSeriesData" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                         PageSize="50" AllowPaging="true" AllowSorting="true" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting"
                        AutoGenerateColumns="false">
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
                        <Columns>
                             <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtType" runat="server" Text='<%# Eval("Type") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesTitle" runat="server" Text='<%# Eval("SeriesTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesTitle" runat="server" Text='<%# Eval("SeriesTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Season" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeasonNumber" runat="server" Text='<%# Eval("SeasonNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeasonNumber" runat="server" Text='<%# Eval("SeasonNumber") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeTitle" runat="server" Text='<%# Eval("EpisodeTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeTitle" runat="server" Text='<%# Eval("EpisodeTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="EpisodeNumber" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeNumber" runat="server" Text='<%# Eval("EpisodeNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeNumber" runat="server" Text='<%# Eval("EpisodeNumber") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Network" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblNetwork" runat="server" Text='<%# Eval("Network") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNetwork" runat="server" Text='<%# Eval("Network") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Rating" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRating" runat="server" Text='<%# Eval("Rating") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRating" runat="server" Text='<%# Eval("Rating") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="Genre" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblGenre" runat="server" Text='<%# Eval("Genre") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGenre" runat="server" Text='<%# Eval("Genre") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="RunTime" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRunTime" runat="server" Text='<%# Eval("RunTime") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRunTime" runat="server" Text='<%# Eval("RunTime") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="SeriesDescription" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesDescription" runat="server" Text='<%# Eval("SeriesDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesDescription" runat="server" Text='<%# Eval("SeriesDescription") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="EpisodeShortSynopsis" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeShortSynopsis" runat="server" Text='<%# Eval("EpisodeShortSynopsis") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeShortSynopsis" runat="server" Text='<%# Eval("EpisodeShortSynopsis") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField> 
                   <asp:TemplateField HeaderText="ActorCharacter" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblActorCharacter" runat="server" Text='<%# Eval("ActorCharacter") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActorCharacter" runat="server" Text='<%# Eval("ActorCharacter") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Country_Origin" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry_Origin" runat="server" Text='<%# Eval("Country_Origin") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCountry_Origin" runat="server" Text='<%# Eval("Country_Origin") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Original_Language" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblOriginal_Language" runat="server" Text='<%# Eval("Original_Language") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOriginal_Language" runat="server" Text='<%# Eval("Original_Language") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Col1" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol1" runat="server" Text='<%# Eval("Col1") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol1" runat="server" Text='<%# Eval("Col1") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Col2" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol2" runat="server" Text='<%# Eval("Col2") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol2" runat="server" Text='<%# Eval("Col2") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                    ItemStyle-Width="150" />--%>

                    <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  

                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdMoviesData" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                         PageSize="50" AllowPaging="true" AllowSorting="true" OnRowEditing="OnRowMoviesEditing" OnRowCancelingEdit="OnRowMoviesCancelingEdit" OnRowUpdating="OnRowMoviesUpdating" 
                        AutoGenerateColumns="false">
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
                        <Columns>
                             <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtType" runat="server" Text='<%# Eval("Type") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesTitle" runat="server" Text='<%# Eval("OriginalTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesTitle" runat="server" Text='<%# Eval("OriginalTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="ReleaseYear" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblReleaseYear" runat="server" Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtReleaseYear" runat="server" Text='<%# Eval("ReleaseYear") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="CountryofOrigin" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCountryofOrigin" runat="server" Text='<%# Eval("CountryofOrigin") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCountryofOrigin" runat="server" Text='<%# Eval("CountryofOrigin") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Genre" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblGenre" runat="server" Text='<%# Eval("Genre") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGenre" runat="server" Text='<%# Eval("Genre") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="OriginalLanguage" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblOriginalLanguage" runat="server" Text='<%# Eval("OriginalLanguage") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOriginalLanguage" runat="server" Text='<%# Eval("OriginalLanguage") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Rating" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRating" runat="server" Text='<%# Eval("Rating") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRating" runat="server" Text='<%# Eval("Rating") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>                
                 <asp:TemplateField HeaderText="RunTime" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRunTime" runat="server" Text='<%# Eval("RunTime") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRunTime" runat="server" Text='<%# Eval("RunTime") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Country Metadata" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry_Metadata" runat="server" Text='<%# Eval("Country_Metadata") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCountry_Metadata" runat="server" Text='<%# Eval("Country_Metadata") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="Language Metadata" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblLanguage_Metadata" runat="server" Text='<%# Eval("Language_Metadata") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLanguage_Metadata" runat="server" Text='<%# Eval("Language_Metadata") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField> 
                   <asp:TemplateField HeaderText="ActorShortSynopsis" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblActorShortSynopsis" runat="server" Text='<%# Eval("ShortSynopsis") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtShortSynopsis" runat="server" Text='<%# Eval("ShortSynopsis") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Actor/Character" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblActor_Character" runat="server" Text='<%# Eval("Actor_Character") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActor_Character" runat="server" Text='<%# Eval("Actor_Character") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Director" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblDirector" runat="server" Text='<%# Eval("Director") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDirector" runat="server" Text='<%# Eval("Director") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Writer" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblWriter" runat="server" Text='<%# Eval("Writer") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtWriter" runat="server" Text='<%# Eval("Writer") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Producer" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblProducer" runat="server" Text='<%# Eval("Producer") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtProducer" runat="server" Text='<%# Eval("Producer") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                
                 <asp:TemplateField HeaderText="Col1" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol1" runat="server" Text='<%# Eval("Col1") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol1" runat="server" Text='<%# Eval("Col1") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Col2" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol2" runat="server" Text='<%# Eval("Col2") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol2" runat="server" Text='<%# Eval("Col2") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                    ItemStyle-Width="150" />--%>

                    <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  

                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdNetNeoData" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                         PageSize="50" AllowPaging="true" AllowSorting="true" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting"
                        AutoGenerateColumns="false">
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
                        <Columns>
                             <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtType" runat="server" Text='<%# Eval("Type") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesTitle" runat="server" Text='<%# Eval("SeriesTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesTitle" runat="server" Text='<%# Eval("SeriesTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Season" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeasonNumber" runat="server" Text='<%# Eval("SeasonNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeasonNumber" runat="server" Text='<%# Eval("SeasonNumber") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeTitle" runat="server" Text='<%# Eval("EpisodeTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeTitle" runat="server" Text='<%# Eval("EpisodeTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="EpisodeNumber" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeNumber" runat="server" Text='<%# Eval("EpisodeNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeNumber" runat="server" Text='<%# Eval("EpisodeNumber") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Network" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblNetwork" runat="server" Text='<%# Eval("Network") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNetwork" runat="server" Text='<%# Eval("Network") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Rating" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRating" runat="server" Text='<%# Eval("Rating") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRating" runat="server" Text='<%# Eval("Rating") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="Genre" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblGenre" runat="server" Text='<%# Eval("Genre") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGenre" runat="server" Text='<%# Eval("Genre") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="RunTime" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRunTime" runat="server" Text='<%# Eval("RunTime") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRunTime" runat="server" Text='<%# Eval("RunTime") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="SeriesDescription" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesDescription" runat="server" Text='<%# Eval("SeriesDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesDescription" runat="server" Text='<%# Eval("SeriesDescription") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="EpisodeShortSynopsis" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeShortSynopsis" runat="server" Text='<%# Eval("EpisodeShortSynopsis") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeShortSynopsis" runat="server" Text='<%# Eval("EpisodeShortSynopsis") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField> 
                   <asp:TemplateField HeaderText="ActorCharacter" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblActorCharacter" runat="server" Text='<%# Eval("ActorCharacter") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActorCharacter" runat="server" Text='<%# Eval("ActorCharacter") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Country_Origin" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry_Origin" runat="server" Text='<%# Eval("Country_Origin") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCountry_Origin" runat="server" Text='<%# Eval("Country_Origin") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Original_Language" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblOriginal_Language" runat="server" Text='<%# Eval("Original_Language") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOriginal_Language" runat="server" Text='<%# Eval("Original_Language") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Col1" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol1" runat="server" Text='<%# Eval("Col1") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol1" runat="server" Text='<%# Eval("Col1") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Col2" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol2" runat="server" Text='<%# Eval("Col2") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol2" runat="server" Text='<%# Eval("Col2") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                    ItemStyle-Width="150" />--%>

                    <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  

                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdFoxlifeData" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                         PageSize="50" AllowPaging="true" AllowSorting="true" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting"
                        AutoGenerateColumns="false">
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
                        <Columns>
                             <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtType" runat="server" Text='<%# Eval("Type") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesTitle" runat="server" Text='<%# Eval("SeriesTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesTitle" runat="server" Text='<%# Eval("SeriesTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Season" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeasonNumber" runat="server" Text='<%# Eval("SeasonNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeasonNumber" runat="server" Text='<%# Eval("SeasonNumber") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeTitle" runat="server" Text='<%# Eval("EpisodeTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeTitle" runat="server" Text='<%# Eval("EpisodeTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="EpisodeNumber" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeNumber" runat="server" Text='<%# Eval("EpisodeNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeNumber" runat="server" Text='<%# Eval("EpisodeNumber") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Network" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblNetwork" runat="server" Text='<%# Eval("Network") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNetwork" runat="server" Text='<%# Eval("Network") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Rating" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRating" runat="server" Text='<%# Eval("Rating") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRating" runat="server" Text='<%# Eval("Rating") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="Genre" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblGenre" runat="server" Text='<%# Eval("Genre") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGenre" runat="server" Text='<%# Eval("Genre") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="RunTime" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRunTime" runat="server" Text='<%# Eval("RunTime") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRunTime" runat="server" Text='<%# Eval("RunTime") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="SeriesDescription" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesDescription" runat="server" Text='<%# Eval("SeriesDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesDescription" runat="server" Text='<%# Eval("SeriesDescription") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="EpisodeShortSynopsis" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeShortSynopsis" runat="server" Text='<%# Eval("EpisodeShortSynopsis") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeShortSynopsis" runat="server" Text='<%# Eval("EpisodeShortSynopsis") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField> 
                   <asp:TemplateField HeaderText="ActorCharacter" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblActorCharacter" runat="server" Text='<%# Eval("ActorCharacter") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActorCharacter" runat="server" Text='<%# Eval("ActorCharacter") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Country_Origin" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry_Origin" runat="server" Text='<%# Eval("Country_Origin") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCountry_Origin" runat="server" Text='<%# Eval("Country_Origin") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Original_Language" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblOriginal_Language" runat="server" Text='<%# Eval("Original_Language") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOriginal_Language" runat="server" Text='<%# Eval("Original_Language") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Col1" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol1" runat="server" Text='<%# Eval("Col1") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol1" runat="server" Text='<%# Eval("Col1") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Col2" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol2" runat="server" Text='<%# Eval("Col2") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol2" runat="server" Text='<%# Eval("Col2") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                    ItemStyle-Width="150" />--%>

                    <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  

                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdBabyTvData" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                         PageSize="50" AllowPaging="true" AllowSorting="true" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting"
                        AutoGenerateColumns="false">
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
                        <Columns>
                             <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtType" runat="server" Text='<%# Eval("Type") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesTitle" runat="server" Text='<%# Eval("SeriesTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesTitle" runat="server" Text='<%# Eval("SeriesTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Season" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeasonNumber" runat="server" Text='<%# Eval("SeasonNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeasonNumber" runat="server" Text='<%# Eval("SeasonNumber") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeTitle" runat="server" Text='<%# Eval("EpisodeTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeTitle" runat="server" Text='<%# Eval("EpisodeTitle") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="EpisodeNumber" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeNumber" runat="server" Text='<%# Eval("EpisodeNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeNumber" runat="server" Text='<%# Eval("EpisodeNumber") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Network" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblNetwork" runat="server" Text='<%# Eval("Network") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNetwork" runat="server" Text='<%# Eval("Network") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Rating" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRating" runat="server" Text='<%# Eval("Rating") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRating" runat="server" Text='<%# Eval("Rating") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="Genre" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblGenre" runat="server" Text='<%# Eval("Genre") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGenre" runat="server" Text='<%# Eval("Genre") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="RunTime" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblRunTime" runat="server" Text='<%# Eval("RunTime") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRunTime" runat="server" Text='<%# Eval("RunTime") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="SeriesDescription" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblSeriesDescription" runat="server" Text='<%# Eval("SeriesDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSeriesDescription" runat="server" Text='<%# Eval("SeriesDescription") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>  
                  <asp:TemplateField HeaderText="EpisodeShortSynopsis" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblEpisodeShortSynopsis" runat="server" Text='<%# Eval("EpisodeShortSynopsis") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEpisodeShortSynopsis" runat="server" Text='<%# Eval("EpisodeShortSynopsis") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField> 
                   <asp:TemplateField HeaderText="ActorCharacter" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblActorCharacter" runat="server" Text='<%# Eval("ActorCharacter") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActorCharacter" runat="server" Text='<%# Eval("ActorCharacter") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Country_Origin" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry_Origin" runat="server" Text='<%# Eval("Country_Origin") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCountry_Origin" runat="server" Text='<%# Eval("Country_Origin") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Original_Language" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblOriginal_Language" runat="server" Text='<%# Eval("Original_Language") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOriginal_Language" runat="server" Text='<%# Eval("Original_Language") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Col1" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol1" runat="server" Text='<%# Eval("Col1") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol1" runat="server" Text='<%# Eval("Col1") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Col2" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblCol2" runat="server" Text='<%# Eval("Col2") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCol2" runat="server" Text='<%# Eval("Col2") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                    ItemStyle-Width="150" />--%>

                    <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
  <%--  <asp:SqlDataSource ID="sqlDSShelf" runat="server" SelectCommand="select Shelf_Name,Shelf_Id from Fox_Shelf_MST where Shelf_Active=1 order by 1"
        SelectCommandType="Text" ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'>
        <SelectParameters>
            <asp:ControlParameter Name="highTRP" ControlID="chkHighTRP" PropertyName="Checked" Direction="Input" />
            <asp:ControlParameter Name="Shelf_Title" ControlID="ddlTitle" PropertyName="SelectedValue" Direction="Input" />
            <asp:ControlParameter Name="languageid" ControlID="ddlLanguage" PropertyName="SelectedValue" Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
     <asp:SqlDataSource ID="sqlDSShelf" runat="server" SelectCommand="select '--All Shelf--' Shelf_Name, '0' 'Shelf_Id' union select Shelf_Name,Shelf_Id from Fox_Shelf_MST where Shelf_Active=1 order by 2"
        ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'></asp:SqlDataSource>
  <%--  <asp:SqlDataSource ID="sqlDSTitle" runat="server" SelectCommand=" select '--All Title--' Shelf_Title, '0' 'Shelf_Id'"
        ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'></asp:SqlDataSource>--%>
 <%--   <asp:SqlDataSource ID="sqlDSSeasonNo" runat="server" SelectCommand="select '--All Season--' Shelf_SeasonNo, '0' 'Shelf_Id'"
        ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'></asp:SqlDataSource>--%>
    <%--<asp:SqlDataSource ID="sqlDSData" runat="server" SelectCommand="sp_getTVStarDiscrepancy"
        SelectCommandType="StoredProcedure" ConnectionString='<%$ConnectionStrings:EPGConnectionString1 %>'>
        <SelectParameters>
            <asp:ControlParameter Name="Shelf_Id" ControlID="ddlShelf" PropertyName="SelectedValue"
                Direction="Input" />
            <asp:ControlParameter Name="languageid" ControlID="ddlLanguage" PropertyName="SelectedValue"
                Direction="Input" />
                <asp:ControlParameter Name="Shelf_Title" ControlID="ddlTitle" PropertyName="SelectedValue"
                Direction="Input" />
            <asp:ControlParameter Name="highTRP" ControlID="chkHighTRP" PropertyName="Checked"
                Direction="Input" />
            <asp:ControlParameter Name="missing" ControlID="chkMissing" PropertyName="Checked"
                Direction="Input" />
            <asp:ControlParameter Name="inepg" ControlID="chkInEPG" PropertyName="Checked" Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
</asp:Content>