<%@ Page Title="Channel Contact Details" Language="vb" MasterPageFile="~/SiteEPGBootstrap.Master"
    AutoEventWireup="false" CodeBehind="CompanyDetails.aspx.vb" Inherits="EPG.CompanyDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Channel Contact Details</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Channel</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" DataSourceID="SqlDsChannelMaster"
                                        DataTextField="ChannelId" DataValueField="ChannelId" AutoPostBack="true" />
                                    <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT Channelid from mst_channel ORDER BY Channelid"></asp:SqlDataSource>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Company Name</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" DataSourceID="SqlDsCompanyMaster"
                                        DataTextField="CompanyName" DataValueField="CompanyId" AutoPostBack="true" />
                                    <asp:SqlDataSource ID="SqlDsCompanyMaster" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT CompanyId, CompanyName FROM mst_Company where active='1' and companyid in (select companyid from mst_channel where channelid=@channelid) ORDER BY CompanyName">
                                        <SelectParameters>
                                            <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue" Direction="Input" /> 
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Point Person Name</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtPointPersonName" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtPointPersonName" ControlToValidate="txtPointPersonName"
                                            runat="server" ForeColor="Red" Text="*"
                                            ValidationGroup="RFGCompanyDetails"></asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Designation</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPointPersonName"
                                            runat="server" ForeColor="Red" Text="*"
                                            ValidationGroup="RFGCompanyDetails"></asp:RequiredFieldValidator></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    DOB
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:CalendarExtender ID="CalendarExtender1" DefaultView="Days" Format="MM/dd/yyyy"
                                            Enabled="True" PopupPosition="BottomLeft" runat="server" TargetControlID="txtdob">
                                        </asp:CalendarExtender>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Email
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <asp:RequiredFieldValidator ID="RFVtxtEmail" ControlToValidate="txtEmail" runat="server"
                                            ForeColor="Red" Text="*" ValidationGroup="RFGCompanyDetails"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REVEmail" ValidationExpression="^[_A-Za-z0-9-]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$"
                                            ControlToValidate="txtEmail" runat="server" ForeColor="Red" Text="*"
                                            ValidationGroup="RFGCompanyDetails"></asp:RegularExpressionValidator></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Mobile
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Point Person Name 2
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtPointPerson2Name" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Email 2
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEmail2" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Mobile 2
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtMobile1" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Landline
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtLandline" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                                <label>
                                    Extenstion
                                </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtExtension" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                        
                                    <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
                                    <asp:Button ID="btnAddCompanyDetails" runat="server" Text="Add" ValidationGroup="RFGCompanyDetails"
                                        UseSubmitBehavior="false" CssClass="btn btn-info" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                            <div class="form-group">
                               
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
                                    <span class="input-group-addon">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" /></span>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                    <div class="row">    
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                             
                                    <asp:GridView ID="grdCompanymaster" runat="server" AutoGenerateColumns="False" EmptyDataText="No record found"
                                        CssClass="table" EmptyDataRowStyle-BackColor="Aqua" CellPadding="4" DataKeyNames="CompanyId"
                                        DataSourceID="sqlDSCompanyDetails" ForeColor="Black" GridLines="Vertical" Width="100%"
                                        AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header" SortedDescendingHeaderStyle-CssClass="sortdesc-header"
                                        AllowPaging="True" PageSize="20" BackColor="White" BorderColor="#DEDFDE" 
                                        BorderStyle="None" BorderWidth="1px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="true" />
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
                                            <asp:TemplateField HeaderText="Channel Id" SortExpression="ChannelId" Visible="True">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Person" SortExpression="PointPersonName" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbPointPersonName" runat="server" Text='<%# Bind("PointPersonName") %>'
                                                        Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" SortExpression="Designation" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbDesignation" runat="server" Text='<%# Bind("Designation") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOB" SortExpression="DOB" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbDOB" runat="server" Text='<%# Bind("DOB") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email" SortExpression="Email" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEmail" runat="server" Text='<%# Bind("Email") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile" SortExpression="Mobile" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbMobile" runat="server" Text='<%# Bind("Mobile") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Person 2" SortExpression="PointPerson2Name"
                                                Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbPointPerson2Name" runat="server" Text='<%# Bind("PointPerson2Name") %>'
                                                        Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email2" SortExpression="Email2" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEmail2" runat="server" Text='<%# Bind("Email2") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile 2" SortExpression="Mobile1" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbMobile1" runat="server" Text='<%# Bind("Mobile1") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Landline" SortExpression="Landline" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbLandline" runat="server" Text='<%# Bind("Landline") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Extension" SortExpression="Extension" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbExtension" runat="server" Text='<%# Bind("Extension") %>' Visible="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png" />
                                            <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/images/delete.png" />
                                        </Columns>

<EmptyDataRowStyle BackColor="Aqua"></EmptyDataRowStyle>

                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                  <br />
                                        <asp:SqlDataSource ID="sqlDSCompanyDetails" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand=""
                                            />
                                  
                                </div>                            
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <label>
                                    Company Details
                                </label>
                                <div class="input-group">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                                        CssClass="table" EmptyDataRowStyle-BackColor="Red" CellPadding="4" DataSourceID="sqlDSCompDetail"
                                        ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                            <asp:BoundField DataField="Address" HeaderText="Address" />
                                            <asp:BoundField DataField="pointpersonName" HeaderText="Contact Person" />
                                            <asp:BoundField DataField="Designation" HeaderText="" />
                                            <asp:BoundField DataField="Email" HeaderText="E-Mail" />
                                            <asp:BoundField DataField="mobile" HeaderText="" />
                                            <asp:BoundField DataField="extension" HeaderText="Extension" />
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
                                    <span class="input-group-addon">
                                        <asp:SqlDataSource ID="sqlDSCompDetail" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                            SelectCommand="sp_companyDetails_view" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
