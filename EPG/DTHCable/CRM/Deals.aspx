<%@ Page Title="Deals" Language="vb" AutoEventWireup="false" MasterPageFile="../SiteDTH.Master"
    CodeBehind="Deals.aspx.vb" Inherits="EPG.Deals" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <div class="panel panel-info">
                <div class="panel-heading text-center">
                    <h3>
                        Deals</h3>
                </div>
                <div class="panel-body">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Select Client</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control" DataSourceID="sqldsClient"
                                    DataTextField="clientname" DataValueField="id" AutoPostBack="false" />
                                <div class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFV_ddlClient" runat="server" ControlToValidate="ddlClient" ValidationGroup="VG" ForeColor="Red" Text="*" InitialValue="0" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Pricing</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPricing" runat="server" CssClass="form-control" placeholder="pricing" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Start Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="start date" />
                                <div class="input-group-addon">
                                    <asp:Image ID="imgCaltxtStartDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate"
                                        PopupButtonID="imgCaltxtStartDate" />
                                    <asp:RequiredFieldValidator ID="RFV_txtStartDate" runat="server" ControlToValidate="txtStartDate"
                                        ForeColor="Red" Text="*" ValidationGroup="VG" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                End Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="end date" />
                                <div class="input-group-addon">
                                    <asp:Image ID="imgCaltxtEndDate" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate"
                                        PopupButtonID="imgCaltxtEndDate" />
                                    <asp:RequiredFieldValidator ID="RFV_txtEndDate" runat="server" ControlToValidate="txtEndDate"
                                        ForeColor="Red" Text="*" ValidationGroup="VG" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Excluding Taxes</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkExcludingTaxes" runat="server" CssClass="form-control" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                No. of Channels</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtChannelsSubscribed" runat="server" CssClass="form-control" placeholder="channels subscribed" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                length of programme and synopsis</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtProgLengthAndSynopsis" runat="server" CssClass="form-control"
                                    placeholder="length of programme and synopsis" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                system? PSI/CASESIS/ Gospell</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtSystemName" runat="server" CssClass="form-control" placeholder="system name" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                format</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtFormat" runat="server" CssClass="form-control" placeholder="format" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Language</label>
                            <div class="input-group">
                                <asp:ListBox ID="lstLanguage" runat="server" CssClass="form-control" SelectionMode="Multiple"
                                    Rows="3" DataTextField="fullname" DataValueField="fullname" DataSourceID="sqlDSLanguage" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Other Features</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtOtherFeatures" runat="server" CssClass="form-control" placeholder="other features" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Contract status</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlContractStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Paying" Value="Paying" />
                                    <asp:ListItem Text="Not Paying" Value="Not Paying" />
                                    <asp:ListItem Text="Barter" Value="Barter" />
                                    <asp:ListItem Text="Provision" Value="Provision" />
                                </asp:DropDownList>
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Concerned Person</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtConcerned" runat="server" CssClass="form-control" placeholder="concerned person" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Designation</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="designation" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Address</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="address" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                GST</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtGST" runat="server" CssClass="form-control" placeholder="gst" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                PAN</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" placeholder="pan" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>
                                Active</label>
                            <div class="input-group">
                                <asp:CheckBox ID="chkActive" runat="server" CssClass="form-control" Checked="true" />
                                <div class="input-group-addon">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn alert-info" ValidationGroup="VG" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn alert-danger" />
                                <asp:Label ID="lbID" runat="server" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body alert-info">
                    <div class="col-md-2">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="search by name/city"
                            CssClass="form-control" />
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-- All Status--" Value="0" />
                            <asp:ListItem Text="Paying" Value="Paying" />
                            <asp:ListItem Text="Not Paying" Value="Not Paying" />
                            <asp:ListItem Text="Barter" Value="Barter" />
                            <asp:ListItem Text="Provision" Value="Provision" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" />
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlds" EmptyDataText="no record found"
                            EmptyDataRowStyle-CssClass="alert alert-danger text-center" AllowPaging="True"
                            PageSize="100" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" DataKeyNames="Id">
                            <EmptyDataRowStyle CssClass="alert alert-danger text-center"></EmptyDataRowStyle>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" ShowDeleteButton="true" DeleteImageUrl="~/Images/delete.png" />
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label ID="lbID" runat="server" Text='<%# Bind("id") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                SelectCommand="select * from crm_fn_deal(@search,@paying) order by active desc,2"
                 DeleteCommand="update crm_deal set isdeleted=1 where id=@id"
                >
                <SelectParameters>
                    <asp:ControlParameter Name="search" ControlID="txtSearch" PropertyName="Text" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="paying" ControlID="ddlSearchStatus" PropertyName="SelectedValue"
                        ConvertEmptyStringToNull="false" />
                </SelectParameters>
                <DeleteParameters>
                <asp:ControlParameter ControlID="grd" Name="id" PropertyName="SelectedDataKey" />
            </DeleteParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sqldsClient" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                SelectCommand="select '0' id, '-- Select --' clientname union select id,clientname from crm order by 2" />
            <asp:SqlDataSource ID="sqlDSLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                SelectCommand="select languageid,fullname from mst_language where active=1 order by 1" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
