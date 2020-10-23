<%@ Page Title="Program Master" Language="vb" MasterPageFile="~/SiteEPGBootstrap.Master"
    AutoEventWireup="false" CodeBehind="ProgramMaster.aspx.vb" Inherits="EPG.ProgramMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function AlertSeries(obj) {
            if (!obj.checked) {
            }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to set Series Enabled for this Program?');
                    if (doit == true)
                    { obj.checked = true; }
                    else
                    { obj.checked = false; }
                }
            }  //function Link
        }
        function AlertCatchup(obj) {
            if (!obj.checked) {
            }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to enable Catchup Flag for this Program?');
                    if (doit == true)
                    { obj.checked = true; }
                    else
                    { obj.checked = false; }
                }
            }  //function Link
        }
        function AlertEpisodic(obj) {
            if (!obj.checked) {
            }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to enable Episodic Synopsis for this Program?');
                    if (doit == true)
                    { obj.checked = true; }
                    else
                    { obj.checked = false; }
                }
            }  //function Link
        }
        function AlertActive(obj) {
            if (obj.checked) {

            }
            else {
                if (obj.value != '') {
                    var doit;
                    doit = confirm('Do you want to set this Program as InActive?');
                    if (doit == true)
                    { obj.checked = false; }
                    else
                    { obj.checked = true; }
                }

            }  //function Link
        }

        $(document).ready(function () {
            $('#ddlChannelName').bind("keyup", function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    $('#txtProgramName').focus();
                }

            });
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <%--<asp:UpdatePanel ID="updProgramMaster" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Program Master</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 ">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 ">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannelName" runat="server" AutoCompleteMode="SuggestAppend"
                                        CssClass="form-control" DropDownStyle="DropDownList" ItemInsertLocation="Append"
                                        DataSourceID="SqlDsChannelMaster" DataTextField="ChannelId" DataValueField="ChannelId"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT ChannelId FROM mst_Channel where active=1 ORDER BY ChannelId">
                                    </asp:SqlDataSource>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 ">
                            <div class="form-group">
                                <label>
                                    Programme Name</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtProgramName" placeholder="Enter Programme name" CssClass="form-control"
                                        runat="server" MaxLength="200"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtProgramName" ControlToValidate="txtProgramName"
                                            runat="server" ForeColor="Red" Text="*" ValidationGroup="RFGProgramMaster"></asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Genre</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlGenre" runat="server" DataSourceID="SqlDsGenreMaster" DataTextField="GenreName"
                                        CssClass="form-control" DataValueField="GenreId" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDsGenreMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select * from dbo.fn_ProgGenre(@channelid) order by 3,2">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue"
                                                Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Sub Genre</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlSubGenre" runat="server" AutoCompleteMode="SuggestAppend"
                                        DropDownStyle="DropDownList" ItemInsertLocation="Append" DataSourceID="SqlDsSubGenreMaster"
                                        DataTextField="SubGenreName" CssClass="form-control" DataValueField="SubGenreId"
                                        Visible="True">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDsSubGenreMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT SubGenreId, SubGenreName FROM mst_SubGenre where GENREID=@GENREID ORDER BY SubGenreName">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlGenre" Name="GenreId" PropertyName="SelectedValue"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    Rating</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlParentalRatingMaster" runat="server" AutoCompleteMode="SuggestAppend"
                                        CssClass="form-control" DropDownStyle="DropDownList" ItemInsertLocation="Append"
                                        DataSourceID="SqlDsParentalRatingMaster" DataTextField="RatingId" DataValueField="RatingId"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDsParentalRatingMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT RatingId FROM mst_ParentalRating ORDER BY RatingId"></asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <div class="form-group">
                                <label>
                                    Show Time</label>
                                <div class="input-group">
                                    <div class="form-control">
                                        <asp:DropDownList ID="ddlHour" runat="server">
                                            <asp:ListItem Value="HH">HH</asp:ListItem>
                                            <asp:ListItem Value="00">00</asp:ListItem>
                                            <asp:ListItem Value="01">01</asp:ListItem>
                                            <asp:ListItem Value="02">02</asp:ListItem>
                                            <asp:ListItem Value="03">03</asp:ListItem>
                                            <asp:ListItem Value="04">04</asp:ListItem>
                                            <asp:ListItem Value="05">05</asp:ListItem>
                                            <asp:ListItem Value="06">06</asp:ListItem>
                                            <asp:ListItem Value="07">07</asp:ListItem>
                                            <asp:ListItem Value="08">08</asp:ListItem>
                                            <asp:ListItem Value="09">09</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="21">21</asp:ListItem>
                                            <asp:ListItem Value="22">22</asp:ListItem>
                                            <asp:ListItem Value="23">23</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMinutes" runat="server">
                                            <asp:ListItem Value="MM">MM</asp:ListItem>
                                            <asp:ListItem Value="00">00</asp:ListItem>
                                            <asp:ListItem Value="01">01</asp:ListItem>
                                            <asp:ListItem Value="02">02</asp:ListItem>
                                            <asp:ListItem Value="03">03</asp:ListItem>
                                            <asp:ListItem Value="04">04</asp:ListItem>
                                            <asp:ListItem Value="05">05</asp:ListItem>
                                            <asp:ListItem Value="06">06</asp:ListItem>
                                            <asp:ListItem Value="07">07</asp:ListItem>
                                            <asp:ListItem Value="08">08</asp:ListItem>
                                            <asp:ListItem Value="09">09</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="21">21</asp:ListItem>
                                            <asp:ListItem Value="22">22</asp:ListItem>
                                            <asp:ListItem Value="23">23</asp:ListItem>
                                            <asp:ListItem Value="24">24</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="26">26</asp:ListItem>
                                            <asp:ListItem Value="27">27</asp:ListItem>
                                            <asp:ListItem Value="28">28</asp:ListItem>
                                            <asp:ListItem Value="29">29</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="31">31</asp:ListItem>
                                            <asp:ListItem Value="32">32</asp:ListItem>
                                            <asp:ListItem Value="33">33</asp:ListItem>
                                            <asp:ListItem Value="34">34</asp:ListItem>
                                            <asp:ListItem Value="35">35</asp:ListItem>
                                            <asp:ListItem Value="36">36</asp:ListItem>
                                            <asp:ListItem Value="37">37</asp:ListItem>
                                            <asp:ListItem Value="38">38</asp:ListItem>
                                            <asp:ListItem Value="39">39</asp:ListItem>
                                            <asp:ListItem Value="40">40</asp:ListItem>
                                            <asp:ListItem Value="41">41</asp:ListItem>
                                            <asp:ListItem Value="42">42</asp:ListItem>
                                            <asp:ListItem Value="43">43</asp:ListItem>
                                            <asp:ListItem Value="44">44</asp:ListItem>
                                            <asp:ListItem Value="45">45</asp:ListItem>
                                            <asp:ListItem Value="46">46</asp:ListItem>
                                            <asp:ListItem Value="47">47</asp:ListItem>
                                            <asp:ListItem Value="48">48</asp:ListItem>
                                            <asp:ListItem Value="49">49</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="51">51</asp:ListItem>
                                            <asp:ListItem Value="52">52</asp:ListItem>
                                            <asp:ListItem Value="53">53</asp:ListItem>
                                            <asp:ListItem Value="54">54</asp:ListItem>
                                            <asp:ListItem Value="55">55</asp:ListItem>
                                            <asp:ListItem Value="56">56</asp:ListItem>
                                            <asp:ListItem Value="57">57</asp:ListItem>
                                            <asp:ListItem Value="58">58</asp:ListItem>
                                            <asp:ListItem Value="59">59</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <span class="input-group-addon">
                                        <asp:Label ID="lbError" runat="server" ForeColor="Red" Text="*" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Duration</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDuration" runat="server" ValidationGroup="RFGProgramMaster" CssClass="form-control"
                                        placeholder="numbers" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[0-9]+$"
                                            ControlToValidate="txtDuration" ValidationGroup="RFGProgramMaster" ErrorMessage="*"
                                            ForeColor="Red" /></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    TRP</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtprogramtrp" runat="server" ValidationGroup="RFGProgramMaster"
                                        CssClass="form-control" placeholder="numbers" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[0-9]+$"
                                            ControlToValidate="txtprogramtrp" ValidationGroup="RFGProgramMaster" ErrorMessage="*"
                                            ForeColor="Red" /></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>
                            &nbsp;
                        </label>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 ">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkSeries" runat="server" CssClass="form-control" Checked="false"
                                        Text="Series Enabled" onclick="javascript:AlertSeries(this);" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkCatchUp" runat="server" CssClass="form-control" Checked="false"
                                        Text="Catchup Flag" onclick="javascript:AlertCatchup(this);" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkEpisodicSynopsis" runat="server" CssClass="form-control" Checked="false"
                                        Text="Episodic Synopsis" onclick="javascript:AlertEpisodic(this);" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="form-control" Checked="True"
                                        Enabled="False" Text="Active" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkIsMovie" runat="server" CssClass="form-control" Checked="True"
                                        Text="Is Movie" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                        <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
                        <asp:Button ID="btnAddProgram" runat="server" CssClass="btn btn-success" Text="Add"
                            ValidationGroup="RFGProgramMaster" UseSubmitBehavior="false" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 text-right">
                        Search By
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Program name" Value="progname" />
                            <asp:ListItem Text="Synopsis" Value="synopsis" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <asp:TextBox ID="txtProgName" runat="server" CssClass="form-control" placeholder="search text" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <asp:CheckBox ID="chkInEPG" runat="server" Checked="True" CssClass="form-control"
                            Text="In EPG" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" />
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:GridView ID="grdProgrammaster1" AutoGenerateColumns="False" runat="server" CssClass="table"
                        AllowSorting="False" AllowPaging="True" PageSize="20" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Prog Id" SortExpression="ProgId" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgId" runat="server" Text='<%# Bind("ProgId") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prog Name" SortExpression="ProgName">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgName" runat="server" Text='<%# Bind("ProgName") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Synopsis" SortExpression="Synopsis" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lbSynopsis" runat="server" Text='<%# Bind("Synopsis") %>' Visible="true" />
                                </ItemTemplate>
                                <ControlStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Genre Id" SortExpression="GenreId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbGenreId" runat="server" Text='<%# Bind("GenreId") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubGenre Id" SortExpression="SubGenreId" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbSubGenreId" runat="server" Text='<%# Bind("SubGenreId") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rating Id" SortExpression="RatingId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbRatingId" runat="server" Text='<%# Bind("RatingId") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Genre" SortExpression="GenreName">
                                <ItemTemplate>
                                    <asp:Label ID="lbGenreName" runat="server" Text='<%# Bind("GenreName") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubGenre" SortExpression="SubGenreName" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lbSubGenreName" runat="server" Text='<%# Bind("SubGenreName") %>'
                                        Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Series" SortExpression="SeriesEnabled">
                                <ItemTemplate>
                                    <asp:Label ID="lbSeriesEnabled" runat="server" Text='<%# Bind("SeriesEnabled") %>'
                                        Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CatchupFlag" SortExpression="CatchupFlag">
                                <ItemTemplate>
                                    <asp:Label ID="lbCatchupFlag" runat="server" Text='<%# Bind("CatchupFlag") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                                <ItemTemplate>
                                    <asp:Label ID="lbActive" runat="server" Text='<%# Bind("Active") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Episodic Synopsis" SortExpression="episodicSynopsis">
                                <ItemTemplate>
                                    <asp:Label ID="lbepisodicSynopsis" runat="server" Text='<%# Bind("episodicSynopsis") %>'
                                        Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="showairtime" SortExpression="showairtime">
                                <ItemTemplate>
                                    <asp:Label ID="lbshowairtime" runat="server" Text='<%# Bind("showairtime") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duration" SortExpression="duration">
                                <ItemTemplate>
                                    <asp:Label ID="lbDuration" runat="server" Text='<%# Bind("Duration") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TRP" SortExpression="programtrp">
                                <ItemTemplate>
                                    <asp:Label ID="lbprogramtrp" runat="server" Text='<%# Bind("programtrp") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Movie" SortExpression="Movie">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMovie" runat="server" Checked='<%# Bind("ismovie") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Images/Edit.png" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images/delete.png" />
                        </Columns>
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
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
