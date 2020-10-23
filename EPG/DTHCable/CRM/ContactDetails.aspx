<%@ Page Title="Contact Details" Language="vb" AutoEventWireup="false" MasterPageFile="../SiteDTH.Master"
    CodeBehind="ContactDetails.aspx.vb" Inherits="EPG.ContactDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading text-center">
            <h3>
                Contact Details</h3>
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
                        Name</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="name" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Phone</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder="phone" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Mail ID</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" placeholder="mail" />
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
            <div class="col-md-8">
                <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlds" EmptyDataText="no record found"
                    EmptyDataRowStyle-CssClass="alert alert-danger text-center">
                    <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" />
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
        SelectCommand="select a.id,b.ClientName [Client],b.City,a.Name,a.Contact,a.MailId [Mail],a.Active from crm_contact a join crm b on a.crmid=b.id where a.crmid=@crmid order by a.active desc,a.name">
        <SelectParameters>
            <asp:ControlParameter Name="crmid" ControlID="ddlClient" PropertyName="Selectedvalue"
                DbType="Int16" Direction="Input" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqldsClient" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select '0' id, '-- Select --' clientname union select id,clientname from crm order by 2" />
</asp:Content>
