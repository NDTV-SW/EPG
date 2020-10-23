<%@ Page Title="Op Details DETAILS" Language="vb" MasterPageFile="SiteDTH.Master"
    AutoEventWireup="false" CodeBehind="OperatorDetails.aspx.vb" Inherits="EPG.OperatorDetails" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h3>
                            <h3>
                                Operators Details</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Name</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Full Name</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Contact Person</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Contact Email</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Contact Phone</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtContactPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        FTP</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkFTP" runat="server" CssClass="form-control" />
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        FTP IP</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtFTPip" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        FTP Username</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtFTPuserName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        FTP Password</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtFTPPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        FTP Port</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtFTPPort" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        SSH Fingerprint</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtFTPfingerPrint" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Mail Subject</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtMailSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Mail Update Subject</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtMailUpdateSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Mail Body</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtMailBody" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Send Attachment</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkSendAttachment" runat="server" CssClass="form-control" />
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                <div class="form-group">
                                    <label>
                                        Parent Operator</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlOperatorParent" runat="server" CssClass="form-control" />
                                        <span class="input-group-addon"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Save" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" />
                                <asp:Label ID="lbOperatorID" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                    CssClass="table" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" AllowSorting="True"
                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                    <AlternatingRowStyle BackColor="White" />
                                    <FooterStyle BackColor="#CCCC99" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#F7F7DE" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                    <SortedAscendingHeaderStyle BackColor="#848384" />
                                    <SortedDescendingCellStyle BackColor="#EAEAD3" BorderStyle="Inset" />
                                    <SortedDescendingHeaderStyle BackColor="#575357" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" />
                                        <asp:TemplateField HeaderText="ID" SortExpression="OperatorID" Visible="True">
                                            <ItemTemplate>
                                                <asp:Label ID="lbOperatorID" runat="server" Text='<%# Bind("OperatorID") %>' Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="true">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="lbActive" runat="server" Checked='<%# Bind("Active") %>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FTP" SortExpression="FTP" Visible="true">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="lbFTP" runat="server" Checked='<%# Bind("FTP") %>' Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name"></asp:BoundField>
                                        <asp:BoundField HeaderText="FullName" DataField="fullname" SortExpression="fullname" />
                                        <asp:BoundField HeaderText="POC" DataField="contactperson" SortExpression="contactperson" />
                                        <asp:BoundField HeaderText="E-mail" DataField="ContactEMail" SortExpression="ContactEMail" />
                                        <asp:BoundField HeaderText="Phone" DataField="ContactPhone" SortExpression="ContactPhone" />
                                        <asp:BoundField HeaderText="OperatorType" DataField="OperatorType" SortExpression="OperatorType"
                                            Visible="false" />
                                        <asp:BoundField HeaderText="FTPIP" DataField="FTPIP" SortExpression="FTPIP" Visible="false" />
                                        <asp:BoundField HeaderText="FTPUserName" DataField="FTPUserName" SortExpression="FTPUserName"
                                            Visible="false" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="SELECT * FROM mst_dthcableoperators order by active desc,FTP desc,priority,name" />
    <asp:SqlDataSource ID="sqlDSOperatorList" runat="server" SelectCommand="select operatorid,name + ' (' + case when active=1 then 'Y' else 'N' end +')' name from mst_dthcableoperators order by active desc,2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
</asp:Content>
