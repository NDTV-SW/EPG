<%@ Page Title="CRM Home" Language="vb" AutoEventWireup="false" MasterPageFile="../SiteDTH.Master"
    CodeBehind="Default.aspx.vb" Inherits="EPG._DefaultCRM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading text-center">
            <h3>
                Add Lead</h3>
        </div>
        <div class="panel-body">
            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        Lead Type</label>
                    <div class="input-group">
                        <asp:ListBox ID="lstLeadType" runat="server" CssClass="form-control" SelectionMode="Multiple"
                            Rows="3">
                            <asp:ListItem Text="EPG" Value="EPG" />
                            <asp:ListItem Text="Metadata" Value="Metadata" />
                            <asp:ListItem Text="EPG Distribution" Value="EPG Distribution" />
                        </asp:ListBox>
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Client Name</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        City</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        State</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Country</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Subscriber Base</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtSubscriberBase" runat="server" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Approximate Deal value</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtDealvalue" runat="server" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        Other Info</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtOtherInfo" runat="server" CssClass="form-control" />
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
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>
                        Deal Status</label>
                    <div class="input-group">
                        <asp:CheckBox ID="chkDealLost" runat="server" Text="Lost" CssClass="form-control"
                            AutoPostBack="true" />
                        <asp:CheckBox ID="chkDealWon" runat="server" Text="Won" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div id="divLostR" class="form-group" runat="server" visible="false">
                    <label>
                        Lost Reason</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtLostReason" runat="server" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div id="divLostRem" class="form-group" runat="server" visible="false">
                    <label>
                        Lost Remarks</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtLostRemarks" runat="server" CssClass="form-control" />
                        <div class="input-group-addon">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn alert-info" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn alert-danger" />
                        <asp:Label ID="lbID" runat="server" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-body alert-info">
            <div class="col-md-2">
                <asp:TextBox ID="txtSearch" runat="server" placeholder="search by name/city" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlLead" runat="server" CssClass="form-control">
                    <asp:ListItem Text="-- All Leads--" Value="0" />
                    <asp:ListItem Text="EPG" Value="EPG" />
                    <asp:ListItem Text="Metadata" Value="Metadata" />
                    <asp:ListItem Text="EPG Distribution" Value="EPG Distribution" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" />
            </div>
        </div>
        <div class="panel-body">
            <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlds" 
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" ForeColor="Black" GridLines="Vertical" DataKeyNames="id">
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" ShowDeleteButton="true" DeleteImageUrl="~/Images/delete.png"  />
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
    <asp:SqlDataSource ID="sqlds" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                SelectCommand="select * from crm_fn_lead(@search,@leadtype) order by active desc,2,3"
                DeleteCommand="update crm set isdeleted=1 where id=@id"
                >
                <SelectParameters>
                    <asp:ControlParameter Name="search" ControlID="txtSearch" PropertyName="Text" ConvertEmptyStringToNull="false" />
                    <asp:ControlParameter Name="leadtype" ControlID="ddlLead" PropertyName="SelectedValue"
                        ConvertEmptyStringToNull="false" />
                </SelectParameters>
                <DeleteParameters>
                <asp:ControlParameter ControlID="grd" Name="id" PropertyName="SelectedDataKey" />
            </DeleteParameters>
            </asp:SqlDataSource>
</asp:Content>
