<%@ Page Title="Upload Excel" Language="vb" MasterPageFile="~/SiteBootStrap.Master"
    AutoEventWireup="false" CodeBehind="uploadExcelImportError.aspx.vb" Inherits="EPG.uploadExcelImportError" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--<asp:ScriptManager ID="scm1" runat="server" />
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
    <div class="container">
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading text-center">
                    <h3>
                        Upload Excel</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="col-lg-4 col-md-12">
                                <div class="input-group">
                                    <asp:FileUpload ID="fupUpload" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVfupUpload" runat="server" ControlToValidate="fupUpload"
                                            Text="* Select File" ForeColor="Red" ValidationGroup="VGUploadBig" />
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-12">
                                <div class="input-group">
                                    <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="../images/uploadxls.png"
                                        ValidationGroup="VGUploadBig" />
                                    <asp:RadioButtonList ID="rbHDR" runat="server" Visible="false">
                                        <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-12">
                                &nbsp;
                            </div>
                            <div class="col-lg-4 col-md-12 text-right">
                                <div class="input-group">
                                    <asp:Button ID="btnUpdateMultiple" Text="Update Multiple" runat="server" CssClass="btn btn-info" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                        <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="PageIndexChanging"
                            AutoGenerateColumns="False" CssClass="table" AllowPaging="True" PageSize="20"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical">
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Channel">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelMissing" runat="server" Text='<%#Eval("ChannelMissing") %>' Visible="false" />
                                        <asp:Label ID="lbEPGProgram" runat="server" Text='<%#Eval("EPGProgram") %>' Visible="false" />
                                        <asp:Label ID="lbInvalidErrorType" runat="server" Text='<%#Eval("InvalidErrorType") %>' Visible="false" />
                                        <asp:Label ID="lbChannelName" runat="server" Text='<%#Eval("ChannelName") %>' />
                                        <asp:DropDownList ID="ddlChannels" runat="server" DataSourceID="sqlDSChannelName"
                                            DataTextField="ChannelId" DataValueField="ChannelId" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EPG Displayed" SortExpression="DisplayedOnRDTV">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDisplayedOnRDTV" runat="server" Text='<%#Eval("DisplayedOnRDTV") %>' />
                                        <asp:DropDownList ID="ddlProgramRDTV" runat="server" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Programme" SortExpression="ActualRunningProgram">
                                    <ItemTemplate>
                                        <asp:Label ID="lbActualRunningProgram" runat="server" Text='<%#Eval("ActualRunningProgram") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error Type" SortExpression="ErrorType">
                                    <ItemTemplate>
                                        <asp:Label ID="lbErrorType" runat="server" Text='<%#Eval("ErrorType") %>' />
                                        <asp:DropDownList ID="ddlErrorTypes" runat="server" DataSourceID="sqlDSErrorType"
                                            DataTextField="ErrorType" DataValueField="ErrorType" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Error Date-Time" SortExpression="ErrorDateTime">
                                    <ItemTemplate>
                                        <asp:Label ID="lbErrorDateTime" runat="server" Text='<%#Eval("ErrorDateTime") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time Slot" SortExpression="TimeSlot">
                                    <ItemTemplate>
                                        <asp:Label ID="lbTimeSlot" runat="server" Text='<%#Eval("TimeSlot") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnUpdateMismatch" runat="server" CausesValidation="False" CommandName="updatemismatch"
                                            ImageUrl="~/Images/update.png" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update Multiple">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkUpdate" runat="server" Checked="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSChannelName" runat="server" SelectCommandType="Text"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" SelectCommand="select channelid,replace(replace(operatorchannelid,'&amp','&'),';','') operatorchannelid from dthcable_channelmapping where operatorid=124 order by channelid" />
    <asp:SqlDataSource ID="sqlDSErrorType" runat="server" SelectCommandType="Text" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select * from error_type where ErrorTypeID in (1,6,8,9,10) order by 2" />
    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
