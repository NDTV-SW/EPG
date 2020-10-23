<%@ Page Title="User Master" Language="vb" MasterPageFile="SiteAdmin.Master" AutoEventWireup="false"
    CodeBehind="UserMaster.aspx.vb" Inherits="EPG.UserMaster" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function checkChange() {
            if (document.getElementById('MainContent_chkIsManager').checked) {
                document.getElementById('MainContent_ddlManagedBy').disabled = true;
                document.getElementById('MainContent_ddlManagedBy').selectedIndex = 0;
            } else {
                document.getElementById('MainContent_ddlManagedBy').disabled = false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    User Master</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="User Name" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtUserName" runat="server" ControlToValidate="txtUserName"
                                    ValidationGroup="VG" Text="*" ForeColor="Red" />
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password"
                                TextMode="Password" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtPassword" runat="server" ControlToValidate="txtPassword"
                                    ValidationGroup="VG" Text="*" ForeColor="Red" />
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox ID="txtRepeatPassword" runat="server" CssClass="form-control" placeholder="Reconfirm Password"
                                TextMode="Password" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtRepeatPassword" runat="server" ControlToValidate="txtRepeatPassword"
                                    ValidationGroup="VG" Text="*" ForeColor="Red" />
                                <asp:CompareValidator ID="CV_txtPassword" runat="server" ControlToValidate="txtPassword"
                                    ControlToCompare="txtRepeatPassword" ValidationGroup="VG" Text="*" ForeColor="Red" />
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                <asp:ListItem Text="USER" Value="USER" />
                                <asp:ListItem Text="SUPERUSER" Value="SUPERUSER" />
                                <asp:ListItem Text="ADMIN" Value="ADMIN" />
                            </asp:DropDownList>
                            <span class="input-group-addon"></span>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox ID="txtMailId" runat="server" TextMode="Email" CssClass="form-control"
                                placeholder="E-mail" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtMailId" runat="server" ControlToValidate="txtMailId"
                                    ValidationGroup="VG" Text="*" ForeColor="Red" />
                                <asp:RegularExpressionValidator ID="REV_txtMailId" runat="server" ControlToValidate="txtMailId"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="VG"
                                    Text="*" ForeColor="Red" />
                            </span>
                        </div>
                    </div>
                    <div class="form-inline form-group">
                        <div class="form-group">
                            <asp:CheckBox ID="chkActive" runat="server" CssClass="form-control" Checked="true"
                                Text="Active" />
                        </div>
                        <div class="form-group">
                            <asp:CheckBox ID="chkPasswordChangeRequired" runat="server" CssClass="form-control"
                                Checked="true" Text="Change Password on Login ?" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" Text="Add" ValidationGroup="VG"
                                CausesValidation="true" UseSubmitBehavior="true" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Clear" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:Label ID="lbStatus" runat="server" CssClass="alert alert-danger" Visible="false"
                                Text="User ID already Exists !" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                    <asp:GridView ID="grd" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                        BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" CssClass="table"
                        DataSourceID="sqlDS" AllowPaging="true" PageSize="30" AllowSorting="true">
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
                            <asp:CommandField ShowSelectButton="true" ShowDeleteButton="true" ButtonType="Image"
                                SelectImageUrl="~/Images/edit.png" DeleteImageUrl="~/Images/delete.png" />
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                    <asp:Label ID="lbusername" runat="server" Text='<%#Bind("username") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select uname UserName,Mailid [Mail] ,Roles,convert(varchar,lastlogindate,106) + ' ' + convert(varchar,lastlogindate,108) [Last Login], Active from mstUsers order by active desc,1"
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" DeleteCommand="select Getdate()" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
