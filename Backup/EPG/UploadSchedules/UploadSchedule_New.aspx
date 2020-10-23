<%@ Page Title="Upload Schedule #1 New" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="UploadSchedule_New.aspx.vb" Inherits="EPG.UploadSchedule_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script type="text/javascript">

    function BeginProcess(channelid, gridindex) {
        $("#MainContent_grdData_hyAddNew_" + gridindex).html("<img src='../images/yellow.png' />");
        // Create an iframe.
        var iframe = document.createElement("iframe");
        //progname = document.getElementById('MainContent_grdData_ddlPrograms_' + gridindex + '_TextBox_' + gridindex).value;
        progname = document.getElementById('MainContent_grdData_lbExcelProgName_' + gridindex).innerHTML;
        genre = document.getElementById('MainContent_grdData_ddlGenre_' + gridindex).value;
        episode = document.getElementById('MainContent_grdData_lbEpisodeNo_' + gridindex).innerHTML;
        rating = document.getElementById('MainContent_grdData_ddlRating_' + gridindex).value;
        movie = document.getElementById('MainContent_grdData_chkIsMovie_' + gridindex).checked;
        status = document.getElementById('MainContent_grdData_lbStatus_' + gridindex).innerHTML;
        
        if (movie) {
            movie = '1';
        }
        else {
            movie = '0';
        }
        
//        alert('Prog --- ' + progname);
//        alert('genre --- ' + genre);
//        alert('epi --- ' + episode);
//        alert('rating --- ' + rating);
//        alert('movie --- ' + movie);
//        alert('status --- ' + status);

        iframe.src = "ProcessTask.aspx?gridindex=" + gridindex + "&channelid=" + channelid + "&progname=" + progname + "&genre=" + genre + "&episode=" + episode + "&rating=" + rating + "&movie=" + movie + "&status=" + status;

        document.body.appendChild(iframe);

    }

    
    function UpdateProgress(Message, controlid, status) {
        //alert(Message);
        //alert(controlid);
        $("#" + controlid).html(Message);
        if (status == 3) {
            $("#" + controlid).html("<img src='../images/red.png' />")
        }
        if (status == 2) {
            
            $("#" + controlid).html("<img src='../images/blue.png' />")

        }
        if (status == 1) {
            $("#" + controlid).html("<img src='../images/green.png' />")
        }
        if (status == 0) {
            $("#" + controlid).html("<img src='../images/red.png' />")
        }
    }
    </script>   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Upload Schedule #1 New</h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="btn-lg alert-success">
                            <b>Note :</b> Sample Format of Excel to Upload
                        </div>
                        <asp:Image ID="imgSampleScedUpload" ImageUrl="~/Images/SampleScedUpload.JPG" runat="server"
                            CssClass="btn-lg alert-success table" />
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                                    <span class="input-group-addon">
                                        <asp:Label ID="lbIsMovieChannel" runat="server" />
                                        <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtChannel" FirstRowSelected="true" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Select File</label>
                                <div class="input-group">
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:RegularExpressionValidator ID="REVFileUpload1" ControlToValidate="FileUpload1"
                                            runat="server" Text="*" ErrorMessage="Only .xls file Supported !" ValidationExpression="^.*\.(xls|XLS)$"
                                            ForeColor="Red" ValidationGroup="VGUploadSched" />
                                        <asp:RequiredFieldValidator ID="RFVFileUpload1" ControlToValidate="FileUpload1" runat="server"
                                            Text="*" ErrorMessage="Please select file first !" ForeColor="Red" ValidationGroup="VGUploadSched" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    Tentative ?</label>
                                <div class="input-group">
                                    <asp:CheckBox ID="chkTentative" runat="server" Checked="false" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbUploadError" runat="server" ForeColor="Red" Text="Please select file first!"
                                        Visible="false" />
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnUpload" runat="server" Text="UPLOAD" UseSubmitBehavior="false"
                                        CssClass="btn btn-info" ValidationGroup="VGUploadSched" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbEPGBuiltMessage" runat="server" ForeColor="Red" Text="EPG Built Successfully"
                                        Visible="false" />
                                </label>
                                <div class="input-group">
                                    <asp:Button ID="btnBuildEPG" runat="server" Text="BUILD EPG" Visible="True" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-heading text-center">
                    <div class="btn-lg alert-danger">
                        To Update Program name with new one, simply click update, else select all other
                        fields from dropdowns to Add new Program.
                    </div>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grdBuildEPGError" runat="server" CssClass="table table-responsive"
                        RowStyle-BackColor="RosyBrown" EmptyDataRowStyle-BackColor="LawnGreen" EmptyDataText="No Error found"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" Visible="False" />
                            <asp:BoundField DataField="channelid" HeaderText="Channel" SortExpression="channelid" />
                            <asp:BoundField DataField="errormsg" HeaderText="Error Message" SortExpression="errormsg" />
                            <asp:BoundField DataField="errortimestamp1" HeaderText="Timestamp" SortExpression="errortimestamp" />
                        </Columns>
                        <EmptyDataRowStyle BackColor="LawnGreen"></EmptyDataRowStyle>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
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
                    <div class="text-right table">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-xs btn-info" />
                    </div>
                    <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive"
                        AllowPaging="True" PageSize="20" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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
                            <asp:TemplateField HeaderText="Movies from Master">
                                <ItemTemplate>
                                    <div class="form-group">
                                       
                                            <div class="col-lg-8 col-md-8 col-sm-8">
                                                <asp:TextBox ID="txtSearchMovie" runat="server" />
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <%--<span class="input-group-addon">--%>
                                                <asp:AutoCompleteExtender ID="ACE_txtSearchMovie" runat="server" ServiceMethod="SearchMovie"
                                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="txtSearchMovie" FirstRowSelected="true" UseContextKey="true" />
                                                <asp:Button ID="btnUpdateMovie" runat="server" Text="Update" CommandName="updatemovie"
                                                    CssClass="btn-info btn-xs" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                                <%--</span>--%>
                                            </div>
                                        
                                    </div>
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
                                    <asp:DropDownList ID="ddlGenre" runat="server" DataSourceID="SqlDsGenre" DataTextField="genrename"
                                        DataValueField="genreid" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rating">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlRating" runat="server" DataSourceID="SqlDsRating" DataTextField="ratingid"
                                        DataValueField="ratingid" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Series">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlSeries" runat="server">
                                        <asp:ListItem Text="Disabled" Value="0" />
                                        <asp:ListItem Text="Enabled" Value="1" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlActive" runat="server">
                                        <asp:ListItem Text="Active" Value="1" />
                                    </asp:DropDownList>
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
                            <asp:TemplateField HeaderText="Add">
                                <ItemTemplate>
                                   <asp:HyperLink ID="hyAddNew" runat="server"><img src='../images/blue.png' alt="Add" /></asp:HyperLink>
                                    <%--<asp:Button ID="Btn_AddNew" runat="server" CausesValidation="False" CommandName="AddNew"
                                        Text="Add New" CssClass="btn btn-info btn-xs" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="Btn_Updt" runat="server" CausesValidation="False" CommandName="Updt"
                                        Text="Update" CssClass="btn btn-info btn-xs" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                    <asp:GridView ID="grdGenre" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive"
                        CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" Width="50%" />
                        <Columns>
                            <asp:BoundField DataField="ExcelGenre" HeaderText="ExcelGenre" SortExpression="ExcelGenre" />
                            <asp:BoundField DataField="MstGenre" HeaderText="MstGenre" SortExpression="MstGenre"
                                Visible="false" />
                            <asp:BoundField DataField="PossibleAction" HeaderText="PossibleAction" SortExpression="PossibleAction"
                                Visible="false" />
                            <asp:TemplateField HeaderText="Available Genre">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_AvailableGenre" runat="server" />
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
                                        Text="Update" CssClass="btn btn-info" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'
                                        Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right">
                        <asp:Button ID="btnUpdateGenre" runat="server" CssClass="btn btn-info" Text="Update Genre" />
                    </div>
                </div>
                <div class="panel-heading text-center">
                    <div class="btn-lg alert-info">
                        Excel Data</div>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:GridView ID="grdExcelData" runat="server" AllowPaging="True" AllowSorting="True"
                            CssClass="table" CellPadding="4" ForeColor="Black" GridLines="Vertical" PagerSettings-PageButtonCount="10"
                            PageSize="100" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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
                        </asp:GridView>
                        <asp:TextBox ID="txtProgName" runat="server" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT ChannelId FROM mst_Channel where active='1' ORDER BY ChannelId">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSgrdData" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="sp_epg_validate_progname" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtChannel" Name="ChannelId" PropertyName="Text"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDsGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select * from dbo.fn_ProgGenre(@channelid) order by 3,2">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtChannel" Name="ChannelId" PropertyName="Text"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSPrograms" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="SqlDsRating" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select ratingid from mst_parentalrating where ratingid='U' order by ratingid">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDSAvailableGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select * from dbo.fn_ProgGenre(@channelid) order by 3,2">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtChannel" Name="ChannelId" PropertyName="Text"
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
