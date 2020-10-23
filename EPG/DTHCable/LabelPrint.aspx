<%@ Page Title="Label Prints" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="LabelPrint.aspx.vb" Inherits="EPG.LabelPrint" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>Label Prints</b>
            </div>
            <div class="panel-body">
                <div class="col-md-12">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label>
                                Company</label>
                            <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label>
                                Address Line 1</label>
                            <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control"  />
                        </div>
                        <div class="form-group">
                            <label>
                                Address Line 2</label>
                            <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="form-control"  />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                City</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label>
                                State</label>
                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label>
                                District</label>
                            <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Pin Code</label>
                            <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label>
                                Contact</label>
                            <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label>
                                Active</label>
                            <asp:CheckBox ID="chkActive" runat="server" CssClass="form-control" Checked="true" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnAdd" CssClass="btn alert-info" runat="server" Text="Add" />
                            <asp:Button ID="btnCancel" CssClass="btn alert-danger" runat="server" Text="Cancel" />
                            <asp:Label ID="lbID" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>
                            Select Label Format</label>
                        <asp:DropDownList ID="ddlLabelFormats" runat="server" CssClass="form-control" AutoPostBack="true" />
                    </div>
                </div>
                <div class="col-md-3">
                    <br />
                    <asp:HyperLink ID="hyPrint" runat="server" Text="Print Address Labels" CssClass="btn btn-info"
                        Target="_blank" />
                </div>
            </div>
            <div class="panel-body">
                <asp:GridView ID="grd" runat="server" CssClass="table" DataKeyNames="id" DataSourceID="sqlDS">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="select id,name,company,addressline1,addressline2,city,statename,district,pincode,contact,active from mst_operatorAddress order by active desc,company">
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
