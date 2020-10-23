<%@ Page Title="Filter Mails Received" Language="vb" MasterPageFile="SiteStar.Master"
    AutoEventWireup="false" CodeBehind="MailsReceivedFilter.aspx.vb" Inherits="EPG.MailsReceivedFilter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info text-center">
        <div class="panel-heading">
            <h3>
                Filter Mails Received
            </h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>
                            Channel</label>
                        <div class="input-group">
                            <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" DataSourceID="sqlDSChannel"
                                DataTextField="channelid" DataValueField="ord" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFVddlChannel" ControlToValidate="ddlChannel" runat="server"
                                    ForeColor="Red" Text="*" InitialValue="0" ValidationGroup="VG" />
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <label>
                            Start Date</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtStartDate" placeholder="Start Date" runat="server" CssClass="form-control"
                                ValidationGroup="VG" />
                            <span class="input-group-addon">
                                <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate"
                                    PopupButtonID="imgStartDateCalendar" />
                                <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate"
                                    runat="server" ForeColor="Red" Text="*" ValidationGroup="VG" />
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <label>
                            End Date</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtEndDate" placeholder="End Date" runat="server" CssClass="form-control"
                                ValidationGroup="VG" />
                            <span class="input-group-addon">
                                <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate"
                                    PopupButtonID="imgEndDateCalendar" />
                                <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server"
                                    ForeColor="Red" Text="*" ValidationGroup="VG" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate"
                                    ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date"
                                    ErrorMessage="*" ValidationGroup="VG" />
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <label>
                            Mail Received by</label>
                        <div class="col-md-12">
                            <asp:Label ID="lbMailReceivedby" runat="server" CssClass="btn btn-block alert-info" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <label>
                            Mail Subject</label>
                        <div class="col-md-12">
                            <asp:Label ID="lbMailSubject" runat="server" CssClass="btn btn-block alert-info" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <label>
                            Mail Received At</label>
                        <div class="col-md-12">
                            <asp:Label ID="lbMailReceivedAt" runat="server" CssClass="btn btn-block alert-info" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <label>
                            Attachment Name</label>
                        <div class="col-md-12">
                            <asp:Label ID="lbAttachmentName" runat="server" CssClass="btn btn-block alert-info" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <label>
                            &nbsp;</label>
                        <div class="input-group">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn alert-success"
                                ValidationGroup="VG" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn alert-danger" />
                            <asp:Label ID="lbID" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-body alert-warning">
            <div class="col-md-3">
                <div class="form-group">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="enter text to search" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
            </div>
            <div class="col-md-1">
                <div class="form-group">
                    <div class="input-group">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn alert-info" Text="search" />
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-body">
            <asp:GridView ID="grd" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                DataSourceID="sqlDS" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Vertical" AutoGenerateColumns="True">
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
                    <asp:CommandField ButtonType="Image" ShowSelectButton="true" SelectImageUrl="~/Images/edit.png" />
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                            <asp:Label ID="lbID" runat="server" Text='<%#Bind("id") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select top 1000 id,channelid,mailreceivedby,mailsubject,attachmentname,mailreceivedat from fpc_mailattachmentingest where (attachmentname like '%.xls' or  attachmentname like '%.xlsx') and cast(insertedat as date)>=cast(dbo.getlocaldate()-5 as date) and mailreceivedby not like '%ndtv%' and (mailreceivedby like '%' + @search + '%' or mailsubject like '%' + @search + '%' or attachmentname like '%' + @search + '%') order by insertedat desc">
        <SelectParameters>
            <asp:ControlParameter Name="search" ControlID="txtSearch" PropertyName="Text" ConvertEmptyStringToNull="false" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select channelid ord,channelid from mst_channel where companyid=5 union select '0','--Select--' order by 2" />
</asp:Content>
