<%@ Page Title="Upload Schedule #7 GMT" Language="vb" MasterPageFile="~/Site.Master"
    AutoEventWireup="false" CodeBehind="UploadSchedule7_GMT.aspx.vb" Inherits="EPG.UploadSchedule7_GMT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ddlChannelName').bind("keyup", function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    javascript: __doPostBack('ddlChannelName', '')
                }

            });
        });
      
      
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Upload Schedule GMT MODULE 7
    </h2>
    <asp:Table ID="Table1" runat="server" Width="95%" GridLines="both" BorderWidth="2"
        CellPadding="5" CellSpacing="0">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                <asp:Image ID="imgSampleScedUpload" ImageUrl="~/Images/Sampleuploadschedule7.PNG"
                    runat="server" />
                <br />
                <span style="color: green">Sample Format of CSV to Upload</span>
                <br />
                <br />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell HorizontalAlign="Right" Width="15%">
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                Select Channel
            </asp:TableHeaderCell>
            <asp:TableCell HorizontalAlign="Left" Width="20%" VerticalAlign="Top">
                <asp:ComboBox ID="ddlChannelName" runat="server" AutoCompleteMode="SuggestAppend"
                    DropDownStyle="DropDownList" ItemInsertLocation="Append" D AutoPostBack="true">
                </asp:ComboBox>
                <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId">
                </asp:SqlDataSource>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left" Width="25%">
                <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1"
                    runat="server" Text="*" ErrorMessage="Only .CSV file Supported !" ValidationExpression="^.*\.(csv|CSV)$"
                    ForeColor="Red" ValidationGroup="VGUploadSched" />
                <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server"
                    Text="*" ErrorMessage="Please select file first !" ForeColor="Red" ValidationGroup="VGUploadSched" />
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="15%">
                <asp:Button ID="btnUpload" runat="server" Text="UPLOAD" UseSubmitBehavior="false"
                    ValidationGroup="VGUploadSched" />
                <asp:Label ID="lbUploadError" runat="server" ForeColor="Red" Text="Please select file first!"
                    Visible="false" />
            </asp:TableCell><asp:TableCell Width="25%">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="VGUploadSched"
                    ForeColor="Red" />
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" VerticalAlign="Top" Width="15%">
                <br />
                <asp:CheckBox ID="chkTentative" runat="server" Text="Tentative ?" Checked="false" />
            </asp:TableCell>
            <asp:TableCell ColumnSpan="3" HorizontalAlign="Left">
                <asp:Button ID="btnBuildEPG" runat="server" Text="BUILD EPG" Visible="True" />
                <br />
                <asp:Label ID="lbEPGBuiltMessage" runat="server" ForeColor="Red" Text="EPG Built Successfully"
                    Visible="false" />
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                <asp:GridView ID="grdBuildEPGError" runat="server" RowStyle-BackColor="RosyBrown"
                    EmptyDataRowStyle-BackColor="LawnGreen" EmptyDataText="No Error found" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" Visible="False" />
                        <asp:BoundField DataField="channelid" HeaderText="Channel" SortExpression="channelid" />
                        <asp:BoundField DataField="errormsg" HeaderText="Error Message" SortExpression="errormsg" />
                        <asp:BoundField DataField="errortimestamp1" HeaderText="Timestamp" SortExpression="errortimestamp" />
                    </Columns>
                </asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">
                <asp:GridView ID="grdEpiMissing" runat="server" CssClass="table table-responsive"
                    RowStyle-BackColor="RosyBrown" EmptyDataRowStyle-CssClass="alert alert-success"
                    EmptyDataText="Episode number available for all Series enabled shows" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    CellPadding="4" ForeColor="Black" GridLines="Vertical" Caption="Episode number missing for Series enabled shows">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbRowId" runat="server" Text='<%# Eval("rowId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="vprogName" HeaderText="Programme" SortExpression="vprogName" />
                        <asp:BoundField DataField="vProgid" HeaderText="ProgId" SortExpression="vProgid" />
                        <asp:BoundField DataField="vDate" HeaderText="Date" SortExpression="vDate" />
                        <asp:BoundField DataField="vTime" HeaderText="Time" SortExpression="vTime" />
                        <asp:BoundField DataField="vDuration" HeaderText="Duration" SortExpression="vDuration" />
                        <asp:TemplateField HeaderText="Episode">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEpisodeNo" runat="server" Text='<%# Eval("vEpisodeNo") %>' ValidationGroup="VGEpi"
                                    ClientIDMode="Predictable" />
                                <asp:RequiredFieldValidator ID="RFVtxtEpisodeNo" ControlToValidate="txtEpisodeNo"
                                    runat="server" ForeColor="Red" Text="Required" ValidationGroup="VGEpi" ClientIDMode="Predictable" />
                                <asp:RegularExpressionValidator ID="REVtxtEpisodeNo" runat="server" ControlToValidate="txtEpisodeNo"
                                    ForeColor="Red" ValidationExpression="^[0-9]+$" Text="Numbers Only" ValidationGroup="VGEpi"
                                    ClientIDMode="Predictable" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnUpdateEpi" runat="server" CausesValidation="False" CommandName="UpdateEpi"
                                    ValidationGroup="VGEpi" ClientIDMode="Predictable" Text="Update" CssClass="btn btn-info btn-sm"
                                    CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE"></RowStyle>
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                <span style="color: red">To Update Program Name with new one, simply click update,<br />
                    else select all other fields from dropdowns to Add new Program. </span>
                <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                    PageSize="20" DataSourceID="SqlDSgrdData" CellPadding="4" ForeColor="#333333"
                    GridLines="Vertical" Width="50%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ExcelProgName">
                            <ItemTemplate>
                                <asp:Label ID="lbExcelProgName" runat="server" Text='<%# Eval("ExcelProgName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Available Programs">
                            <ItemTemplate>
                                <asp:ComboBox ID="ddlPrograms" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                                    ItemInsertLocation="Append" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PossibleAction" HeaderText="Action" SortExpression="PossibleAction" />
                        <asp:TemplateField HeaderText="ChannelId">
                            <ItemTemplate>
                                <asp:Label ID="lbChannelId" runat="server" Text='<%# Eval("ChannelId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Genre">
                            <ItemTemplate>
                                <asp:ComboBox ID="ddlGenre" runat="server" Width="100px" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDownList" ItemInsertLocation="Append" DataSourceID="SqlDsGenre"
                                    DataTextField="genrename" DataValueField="genreid">
                                </asp:ComboBox>
                            </ItemTemplate>
                            <ControlStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rating">
                            <ItemTemplate>
                                <asp:ComboBox ID="ddlRating" runat="server" Width="40px" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDownList" ItemInsertLocation="Append" DataSourceID="SqlDsRating"
                                    DataTextField="ratingid" DataValueField="ratingid">
                                </asp:ComboBox>
                            </ItemTemplate>
                            <ControlStyle Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Series">
                            <ItemTemplate>
                                <asp:ComboBox ID="ddlSeries" runat="server" Width="50px" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDownList" ItemInsertLocation="Append">
                                    <asp:ListItem Text="Disabled" Value="0" />
                                    <asp:ListItem Text="Enabled" Value="1" />
                                </asp:ComboBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:ComboBox ID="ddlActive" runat="server" Width="40px" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDownList" ItemInsertLocation="Append">
                                    <asp:ListItem Text="Active" Value="1" />
                                </asp:ComboBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Episode No.">
                            <ItemTemplate>
                                <asp:Label ID="lbEpisodeNo" runat="server" Text='<%# Eval("EpisodeNo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemTemplate>
                                <asp:Label ID="lbStatus" Text='<%# Bind("Status") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is Movie">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsMovie" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Btn_AddNew" runat="server" CausesValidation="False" CommandName="AddNew"
                                    Text="Add New" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Btn_Updt" runat="server" CausesValidation="False" CommandName="Updt"
                                    Text="Update" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
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
                <asp:SqlDataSource ID="SqlDSgrdData" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="sp_epg_validate_progname" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDsGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="select * from dbo.fn_ProgGenre(@channelid) order by 3,2">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqlDSPrograms" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
                <asp:SqlDataSource ID="SqlDsRating" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="select ratingid from mst_parentalrating where ratingid='U' order by ratingid">
                </asp:SqlDataSource>
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
            <br />
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                <asp:GridView ID="grdGenre" runat="server" AutoGenerateColumns="false" DataSourceID="SqlDSgrdGenre"
                    CellPadding="4" ForeColor="#333333" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" Width="50%" />
                    <Columns>
                        <asp:BoundField DataField="ExcelGenre" HeaderText="ExcelGenre" SortExpression="ExcelGenre" />
                        <asp:BoundField DataField="MstGenre" HeaderText="MstGenre" SortExpression="MstGenre"
                            Visible="false" />
                        <asp:BoundField DataField="PossibleAction" HeaderText="PossibleAction" SortExpression="PossibleAction"
                            Visible="false" />
                        <asp:TemplateField HeaderText="Available Genre">
                            <ItemTemplate>
                                <asp:ComboBox ID="ddl_AvailableGenre" runat="server" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDownList" ItemInsertLocation="Append">
                                </asp:ComboBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkUpdateGenre" runat="server" Checked="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Btn_Updt" runat="server" CausesValidation="False" CommandName="Updt"
                                    Text="Update" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'
                                    Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
                <asp:Button ID="btnUpdateGenre" runat="server" Text="Update Genre" />
                <asp:SqlDataSource ID="SqlDSAvailableGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="select * from dbo.fn_ProgGenre(@channelid) order by 3,2">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDSgrdGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="sp_epg_validate_genre" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
            <br />
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                <asp:GridView ID="grdExcelData" runat="server" AutoGenerateColumns="true" AllowPaging="true"
                    AllowSorting="true" DataSourceID="SqlDSgrdExcelData" CellPadding="4" ForeColor="#333333"
                    GridLines="Vertical" Width="80%" PagerSettings-PageButtonCount="10" PageSize="100">
                    <AlternatingRowStyle BackColor="White" />
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
                <asp:SqlDataSource ID="SqlDSgrdExcelData" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="select [Program Name], Genre, convert(varchar,[Date],106), convert(varchar,[Time],108), Duration, [Description],[Episode No],[Show-wise Description],channelID from map_EPGExcel where channelID=@ChannelId order by rowid">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:TextBox ID="txtProgName" runat="server" Visible="false" />
            </asp:TableCell></asp:TableRow>
    </asp:Table>
</asp:Content>
