<%@ Page Title="Status Update" Language="vb" AutoEventWireup="false" MasterPageFile="../SiteDTH.Master"
    CodeBehind="StatusUpdate.aspx.vb" Inherits="EPG.StatusUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel panel-info">
        <div class="panel-heading text-center">
            <h3>
                Status Update</h3>
        </div>
        <div class="panel-body">
            <div class="col-md-4">
                <div class="form-group">
                    <label>
                        Select Client</label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control" DataSourceID="sqldsClient"
                            DataTextField="clientname" DataValueField="id" AutoPostBack="true" />
                        <div class="input-group-addon">
                            <asp:RequiredFieldValidator ID="RFV_ddlClient" runat="server" ControlToValidate="ddlClient"
                                ValidationGroup="VG" ForeColor="Red" Text="*" InitialValue="0" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Status Date</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" placeholder="date" />
                        <div class="input-group-addon">
                            <asp:Image ID="imgCalStatusDate" runat="server" ImageUrl="~/Images/calendar.png" />
                            <asp:CalendarExtender ID="CE_txtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgCalStatusDate" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Remarks</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" placeholder="remarks" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        FollowUp?</label>
                    <div class="input-group">
                        <asp:CheckBox ID="chkFollowup" runat="server" CssClass="form-control" AutoPostBack="true" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div id="divFollowUp1" class="form-group" runat="server" visible="false">
                    <label>
                        FollowUp By</label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlFollowupBy" runat="server" CssClass="form-control">
                            <asp:ListItem Text="AnyOne" Value="AnyOne" />
                            <asp:ListItem Text="Hemant" Value="Hemant" />
                            <asp:ListItem Text="Tarun" Value="Tarun" />
                            <asp:ListItem Text="Kautilya" Value="Kautilya" />
                        </asp:DropDownList>
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div id="divFollowUp" class="form-group" runat="server" visible="false">
                    <label>
                        FollowUp Date</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtFollowupDate" runat="server" CssClass="form-control" placeholder="followup date" />
                        <div class="input-group-addon">
                            <asp:Image ID="imgCalFollowupDate" runat="server" ImageUrl="~/Images/calendar.png" />
                            <asp:CalendarExtender ID="CE_txtFolloupDate" runat="server" TargetControlID="txtFollowupDate" PopupButtonID="imgCalFollowupDate" />
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
            <div class="col-md-8">
                <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlds" EmptyDataText="no record found"
                    EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" />
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                                <asp:Label ID="lbID" runat="server" Text='<%# Bind("id") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select a.id,b.ClientName [Client],b.City ,convert(varchar,a.statusdate,106) [Date] ,a.Remarks,a.Followup,convert(varchar,a.followupdate,106) [On],a.FollowUpBy [By],a.Active from crm_statusupdate a join crm b on a.crmid=b.id where a.crmid=@crmid order by a.active desc,a.statusdate">
        <SelectParameters>
            <asp:ControlParameter Name="crmid" ControlID="ddlClient" PropertyName="Selectedvalue"
                DbType="Int16" Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqldsClient" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                SelectCommand="select '0' id, '-- Select --' clientname union select id,clientname from crm order by 2" />
</asp:Content>
