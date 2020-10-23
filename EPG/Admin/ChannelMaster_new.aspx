<%@ Page Title="Channel Master" Language="vb" MasterPageFile="SiteAdmin.Master"
    AutoEventWireup="false" CodeBehind="ChannelMaster_new.aspx.vb" Inherits="EPG.ChannelMaster_new" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function AlertCatchUp(obj) {
            if (!obj.checked) { }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to set Catchup as True?');
                    if (doit == true)
                    { obj.checked = true; }
                    else
                    { obj.checked = false; }
                }
            }  //function Link
        }

        function AlertActive(obj) {
            if (obj.checked) { }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to set this Channel as Inactive?');
                    if (doit == true)
                    { obj.checked = false; }
                    else
                    { obj.checked = true; }
                }

            }  //function Link
        }

        function AlertSendEPG(obj) {
            if (obj.checked) { }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to set EPG Sending of this Channel as In-Active?');
                    if (doit == true)
                    { obj.checked = false; }
                    else
                    { obj.checked = true; }
                }

            }  //function Link
        }

        $(document).ready(function () {
            $('#ddlCompanyName').bind("keyup", function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    $('#txtChannelName').focus();
                }

            });
        });
        $(document).ready(function () {
            $('#ddlCompanyName1').bind("keyup", function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    $('#txtChannelName').focus();
                }

            });
        });
        $(document).ready(function () {
            $('#ddlRegionalChannel').bind("keyup", function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    $('#txtChannelName').focus();
                }

            });
        });
       
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Channel Master</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Company Name</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" DataSourceID="SqlDsCompanyMaster"
                                    DataTextField="CompanyName" DataValueField="CompanyId" />
                                
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Channel Name</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtChannelName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFVtxtChannelName" ControlToValidate="txtChannelName"
                                        runat="server" ForeColor="Red" Text="*" ErrorMessage="Channel name can not be left blank."
                                        ValidationGroup="RFGChannelMaster"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Channel Genre</label>
                            <div class="input-group">
                                <asp:DropDownList ID="cmbChannelGenre" runat="server" CssClass="form-control" DataSourceID="sqlDSChannelGenre"
                                    DataTextField="genreName" DataValueField="genreId" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Channel Language ID</label>
                            <div class="input-group">
                                 <asp:DropDownList ID="ddlChannelLanguageid" runat="server" CssClass="form-control" DataTextField="FullName"
                                    DataValueField="LanguageID" DataSourceID="SqlmstLanguage" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Channel Language</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtLanguage" runat="server" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Movie Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlMovieLanguage" runat="server" CssClass="form-control" DataTextField="FullName"
                                    DataValueField="LanguageID" DataSourceID="SqlmstLanguage" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Movie Channel</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkMovieChannel" runat="server" Checked="false" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Active</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkActive" runat="server" Checked="True" CssClass="form-control"
                                    onclick="javascript:AlertActive(this);" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Send EPG</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkSendEPG" runat="server" Checked="True" CssClass="form-control"
                                    onclick="javascript:AlertSendEPG(this);" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                CatchUp Flag</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkCatchUpFlag" runat="server" CssClass="form-control" Checked="false"
                                    onclick="javascript:AlertCatchUp(this);" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Series Enabled</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkSeriesEnabled" runat="server" Checked="False" CssClass="form-control" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                TRP</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtTRP" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                        <div class="form-group">
                            <label>
                                Public</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkPublicChannel" runat="server" Checked="False" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        &nbsp;
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 text-right ">
                        <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
                        <asp:Button ID="btnAddChannel" runat="server" Text="Add" ValidationGroup="RFGChannelMaster"
                            UseSubmitBehavior="false" CssClass="btn btn-info" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 text-left ">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                    </div>
                </div>
            </div>
        
            <div class="panel-heading text-center">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Search Channel" CssClass="form-control" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6 text-left">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6 text-left">
                        <asp:Button ID="btnAllPublicChannels" runat="server" Text="All Public Channels" CssClass="btn btn-info" />
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grdChannelmaster" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        DataKeyNames="ChannelId"  ForeColor="Black"
                        CssClass="table" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="50"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#" SortExpression="RowId" Visible="True">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbRowId" runat="server" Text='<%# eval("RowId") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Id" SortExpression="CompanyId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbCompanyId" runat="server" Text='<%# Bind("CompanyId") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbCompanyName" runat="server" Text='<%# Bind("CompanyName") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Language" SortExpression="ChannelLanguage" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannelLanguage" runat="server" Text='<%# Bind("ChannelLanguage") %>' Visible="false" />
                                    <asp:Label ID="lbChannelLanguageName" runat="server" Text='<%# Bind("ChannelLanguagename") %>' Visible="true" />
                                    <asp:Label ID="lbChannelLanguageId" runat="server" Text='<%# Bind("ChannelLanguageid") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active / SendEPG" SortExpression="Active" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Bind("Active") %>' /> &nbsp;/ &nbsp;
                                    <asp:CheckBox ID="chkSendEPG" runat="server" Checked='<%# Bind("SendEPG") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OnAir" SortExpression="Onair" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkOnair" runat="server" Checked='<%# Bind("Onair") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Catchup / Series" SortExpression="CatchupFlag" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCatchupFlag" runat="server" Checked='<%# Bind("CatchupFlag") %>' /> &nbsp;/ &nbsp;
                                     <asp:CheckBox ID="chkSeriesEnabled" runat="server" Checked='<%# Bind("SeriesEnabled") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Movie Channel" SortExpression="Movie_Channel" Visible="true">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMovieChannel" runat="server" Checked='<%# Bind("Movie_Channel") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Movie Lang" SortExpression="movieLanguage" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbmovieLanguage" runat="server" Text='<%# Bind("movieLanguage") %>'
                                        Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Movie Lang Id" SortExpression="MovieLangId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbMovieLangId" runat="server" Text='<%# Bind("MovieLangId") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Genre" SortExpression="GenreName" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbGenreName" runat="server" Text='<%# Bind("GenreName") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Genre Id" SortExpression="GenreId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbGenreId" runat="server" Text='<%# Bind("GenreId") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="TRP" SortExpression="trp">
                                <ItemTemplate>
                                    <asp:Label ID="lbTRP" runat="server" Text='<%# Bind("trp") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Public" SortExpression="publicChannel">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkpublicChannel" runat="server" Checked='<%# Bind("publicChannel") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png" />
                            <asp:CommandField ShowDeleteButton="False" ButtonType="Image" DeleteImageUrl="~/images/delete.png" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center"  />
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
    </div> 
    <asp:SqlDataSource ID="SqlDsCompanyMaster" runat="server" ConnectionString="<%$
    ConnectionStrings:EPGConnectionString1 %>" SelectCommand="SELECT CompanyId, CompanyName
    FROM mst_Company where active='1' ORDER BY CompanyName"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSChannelGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" SelectCommand="Select genreid,genrename from mst_genre where genreCategory='s'
    order by 2"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlmstLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select
    LanguageID,FullName from mst_language where active=1"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlDSChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" SelectCommand="rptChannels" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" PropertyName="Text" Name="search" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:SqlDataSource>
    <%--</ContentTemplate>
    <Triggers> <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
    </Triggers> </asp:UpdatePanel>--%>
</asp:Content>
