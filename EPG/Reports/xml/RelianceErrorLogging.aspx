<%@ Page Title="Reliance Digital EPG Errors Logging" Language="vb" MasterPageFile="~/SiteBootStrap.Master"
    AutoEventWireup="false" CodeBehind="RelianceErrorLogging.aspx.vb" Inherits="EPG.RelianceErrorLogging" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <script type="text/javascript">
        function SetContextKey() {
            $find('<%=ACE_txtActual.ClientID%>').set_contextKey($get("<%=ddlChannelName.ClientID %>").value);
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="scm1" runat="server" />
    <%--<asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="row">
            <div class="panel panel-default">
                <%--<div class="panel-heading text-center">
                            <h3>
                                Platform Master</h3>
                        </div>--%>
                <div class="panel-body">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Select Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannelName" runat="server" DataTextField="operatorchannelid"
                                        DataValueField="channelid" DataSourceID="sqlDSChannelName" CssClass="form-control"
                                        AutoPostBack="true" />
                                    <asp:SqlDataSource ID="sqlDSChannelName" runat="server" SelectCommandType="Text"
                                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" SelectCommand="select channelid,replace(replace(operatorchannelid,'&amp','&'),';','') operatorchannelid from dthcable_channelmapping where operatorid=124 and onair=1 order by operatorchannelid" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Select EPG Programme</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlProgramme" runat="server" DataTextField="progname" DataValueField="progid"
                                        DataSourceID="sqlDSddlProgramme" CssClass="form-control" />
                                    <asp:SqlDataSource ID="sqlDSddlProgramme" runat="server" SelectCommandType="Text"
                                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" SelectCommand="select 0 progid ,'Others' progname,0 priority union select progid,progname, 1 'priority' from mst_program where channelid=@channelid and ProgID in (select distinct ProgID from mst_epg where ProgDate>GETDATE()-5) order by 3 desc,2">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlChannelName" Name="channelid" PropertyName="SelectedValue"
                                                Direction="Input" DbType="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Actual running Program</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtActual" runat="server" CssClass="form-control" onkeyup="SetContextKey()" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFV_txtActual" runat="server" ControlToValidate="txtActual"
                                            Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="Actual running Programme Required"
                                            ValidationGroup="VGBigTV" />
                                        <asp:AutoCompleteExtender ID="ACE_txtActual" runat="server" ServiceMethod="SearchProgramme"
                                            MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                            TargetControlID="txtActual" FirstRowSelected="true" UseContextKey="true" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Error Type</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlErrorType" runat="server" DataTextField="ErrorType" DataValueField="ErrorTypeId"
                                        DataSourceID="sqlDSErrorType" CssClass="form-control" />
                                    <asp:SqlDataSource ID="sqlDSErrorType" runat="server" SelectCommandType="Text" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="select * from error_type where ErrorTypeID in (1,6,8,9,10)" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Error Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ValidationGroup="VGBigTV" />
                                    <asp:CalendarExtender ID="CEtxtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgStartDateCalendar" />
                                    <span class="input-group-addon">
                                        <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:RequiredFieldValidator ID="RFVtxtDate" runat="server" ControlToValidate="txtDate"
                                            ValidationGroup="VGBigTV" Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="Error Date Required" />
                                        <asp:CompareValidator ID="CVtxtDate" runat="server" Text="*" ErrorMessage="Date can't be greater than today"
                                            Operator="LessThanEqual" Type="Date" ControlToValidate="txtDate" ForeColor="Red"
                                            ValidationGroup="VGBigTV" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Error Time</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" ValidationGroup="VGBigTV" />
                                    <span class="input-group-addon alert-danger">24 hr format only
                                        <asp:MaskedEditExtender ID="maskTime" runat="server" AcceptAMPM="false" AutoComplete="true"
                                            InputDirection="LeftToRight" Mask="99:99" MaskType="Time" MessageValidatorTip="true"
                                            TargetControlID="txtTime" />
                                        <asp:RequiredFieldValidator ID="RFVtxtTime" runat="server" ControlToValidate="txtTime"
                                            Font-Bold="true" ForeColor="Red" Text="*" ErrorMessage="Error Time Required"
                                            ValidationGroup="VGBigTV" />
                                        <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="maskTime"
                                            ValidationGroup="VGBigTV" ControlToValidate="txtTime" Display="Dynamic" EmptyValueMessage="Enter Time"
                                            ErrorMessage="Error time" InvalidValueMessage="Time invalid" IsValidEmpty="true"
                                            MaximumValue="23:59:59" ForeColor="Red" />
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <label>
                                    Remarks</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            &nbsp;
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <asp:ValidationSummary ID="VSBIGTVError" ForeColor="Red" runat="server" ValidationGroup="VGBigTV" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                              
                                    <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VGBigTV" />
                                    &nbsp; &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                    &nbsp; &nbsp;
                                    
                                    <input type="hidden" onclick="showDiv()" value="Upload Excel" class="btn btn-info" />
                                
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-2 col-md-2">
                            <div class="input-group">
                                <asp:DropDownList ID="ddlChannel" CssClass="form-control" runat="server" DataTextField="OperatorChannelid"
                                    DataValueField="ChannelId" DataSourceID="sqlDSChannel" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="sqlDSChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>"
                                        SelectCommand="Select '---All Channels---' channelid,'---All Channels---' operatorchannelid,0
    union select channelid,replace(replace(operatorchannelid,'&amp','&'),';','') operatorchannelid,1
    from dthcable_channelmapping where operatorid=124 and onair=1 order by 3" />
                                </span>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2">
                            <div class="input-group">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ValidationGroup="VGBigTVSearch" />
                                <span class="input-group-addon">
                                    <asp:CalendarExtender ID="CE_txtStartDate" runat="server" PopupButtonID="img_txtStartDate"
                                        TargetControlID="txtStartDate" />
                                    <asp:Image ID="img_txtStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                </span>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2">
                            <div class="input-group">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ValidationGroup="VGBigTVSearch" />
                                <span class="input-group-addon">
                                    <asp:CalendarExtender ID="CE_txtEndDate" runat="server" PopupButtonID="img_EndDate"
                                        TargetControlID="txtEndDate" />
                                    <asp:CompareValidator ID="CVtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                                        ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date"
                                        ErrorMessage="End date cannot be less than Startdate" Text="*" ValidationGroup="VGBigTVSearch"></asp:CompareValidator>
                                    <asp:Image ID="img_EndDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                </span>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2">
                            <div class="input-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="VGBigTVSearch"
                                    CssClass="btn btn-info" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grdRelianceError" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                        CssClass="table" DataSourceID="sqlDSRelianceError" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true"
                        EmptyDataText="----No Record Found----" EmptyDataRowStyle-HorizontalAlign="Center"
                        PageSize="50" AllowPaging="true">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbId" runat="server" Text='<%#Eval("ROWID") %>' Visible="false" />
                                    <asp:Label ID="lbSno" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channel" SortExpression="OperatorChannelId">
                                <ItemTemplate>
                                    <asp:Label ID="lbChannelId" runat="server" Text='<%#Eval("OperatorChannelId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Programme" SortExpression="ProgName">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgName" runat="server" Text='<%#Eval("ProgName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ErrorType" SortExpression="ErrorType">
                                <ItemTemplate>
                                    <asp:Label ID="lbErrorType" runat="server" Text='<%#Eval("ErrorType") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Error Date Time" SortExpression="ErrorDateTime">
                                <ItemTemplate>
                                    <asp:Label ID="lbErrorDateTime" runat="server" Text='<%#Eval("ErrorDateTime")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correct On Airtel/TataSky" SortExpression="correctOnAirtel" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCorrectOnAirtel" runat="server" Checked='<%#Eval("correctOnAirtel") %>'
                                        Enabled="false" />
                                    <asp:CheckBox ID="chkCorrectOnTataSky" runat="server" Checked='<%#Eval("CorrectOnTataSky")%>'
                                        Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Loggedby" SortExpression="AddedBY">
                                <ItemTemplate>
                                    <asp:Label ID="lbAddedBY" runat="server" Text='<%#Eval("AddedBY") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lbRemarks" runat="server" Text='<%#Eval("Remarks") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Logged At" SortExpression="lastupdate">
                                <ItemTemplate>
                                    <asp:Label ID="lblastupdate" runat="server" Text='<%#Eval("lastupdate") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Accepted" SortExpression="AcceptedbyNDTV">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAccepted" runat="server" Checked='<%#Eval("AcceptedbyNDTV")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Error Cause" SortExpression="AcceptedbyNDTV">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlCauseId" runat="server" DataTextField="Cause" DataValueField="CauseId"
                                        DataSourceID="SqlDsErrorCause" />
                                    <asp:Label ID="lbErrorCauseId" runat="server" Visible="false" Text='<%#Eval("ErrorCauseId")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnUpdate" runat="server" CausesValidation="False" CommandName="update"
                                        ImageUrl="~/Images/update.png" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="../Images/delete.png" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <sortedascendingcellstyle backcolor="#FBFBF2" />
                        <sortedascendingheaderstyle backcolor="#848384" />
                        <sorteddescendingcellstyle backcolor="#EAEAD3" />
                        <sorteddescendingheaderstyle backcolor="#575357" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDSRelianceError" runat="server" SelectCommand="sp_error_display_bigTV"
                        SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>"
                        UpdateCommand="select Getdate()">
                        <SelectParameters>
                            <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:ControlParameter Name="StartDate" ControlID="txtStartDate" PropertyName="Text"
                                Type="DateTime" />
                            <asp:ControlParameter Name="EndDate" ControlID="txtEndDate" PropertyName="Text" Type="DateTime" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3 col-md-3 col-sm-3 hide">
        <div class="form-group">
            <label>
                Correct on Airtel DTH</label>
            <div class="input-group">
                <asp:CheckBox ID="chkCorrectOnAirtelDTH" runat="server" Checked="false" CssClass="form-control" />
                <span class="input-group-addon"></span>
            </div>
        </div>
    </div>
    <div class="col-lg-3 col-md-3 col-sm-3 hide">
        <div class="form-group">
            <label>
                Correct on TataSky DTH</label>
            <div class="input-group">
                <asp:CheckBox ID="chkCorrectOnTataSkyDTH" runat="server" Checked="false" CssClass="form-control" />
                <span class="input-group-addon"></span>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDsErrorCause" runat="server" SelectCommandType="Text" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT * from error_cause" />
    <%--        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
