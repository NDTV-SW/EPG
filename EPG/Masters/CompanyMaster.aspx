<%@ Page Title="Company Master" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
   CodeBehind="CompanyMaster.aspx.vb" Inherits="EPG.CompanyMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <script type="text/javascript">
       function AlertUser(obj) {
           if (obj.checked) {
           }
           else {
               if (obj.value != '') {
                   var doit;
                   doit = confirm('Do you want to set this language as In-Active?');
                   if (doit == true)
                   { obj.checked = false; }
                   else
                   { obj.checked = true; }
               }
           }  //function Link
       }
   </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:ScriptManager ID="ScriptManager1" runat="server"/>
   <div class="container">
    <h2>Company Master</h2>
    <div class="row">
        <form role="form">
            <div class="col-lg-6">
                <%--<div class="well well-sm"><strong><span class="glyphicon glyphicon-asterisk"></span>Required Field</strong></div>--%>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><label>Id &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></span>
                        <asp:TextBox ID="txtCompId" class="form-control" required runat="server" ReadOnly="true" MaxLength="3" ></asp:TextBox>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><label for="InputEmail"> Name &nbsp;&nbsp;&nbsp;&nbsp;</label></span>
                        <asp:TextBox ID="txtCompanyName" class="form-control" required runat="server"  placeholder="Enter Company Name" MaxLength="200"></asp:TextBox>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><label>Address</label></span>
                        <asp:TextBox ID="txtCompanyAddress" class="form-control" required placeholder="Enter Address" runat="server" MaxLength="200"></asp:TextBox>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><label for="InputMessage">PAN No.&nbsp;</label></span>                    
                        <asp:TextBox ID="txtPanNumber" class="form-control" required placeholder="Enter PAN Number" runat="server" MaxLength="10"></asp:TextBox>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><label for="InputMessage">Active&nbsp;&nbsp;&nbsp;</label></span>
                        <asp:CheckBox ID="chkActive" class="form-control" runat="server" Checked="true" onclick="javascript:AlertUser(this);"/>
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>  
                <div class="form-group">
                  <asp:Button ID="btnAddCompany" CssClass="btn btn-info" runat="server" Text="Add" />
                  <asp:TextBox ID="txtHiddenId" runat="server" Visible="False" Text="0"></asp:TextBox>
                  <%--<asp:Button ID="btnCancel"  class="btn btn-info" runat="server" Text="Cancel"/>--%>
                </div>

            </div>
        </form>
    </div>

    <div class="row">
    <div class="col-lg-12">
        <asp:GridView ID="grdCompanymaster" runat="server" AutoGenerateColumns="False" DataKeyNames="CompanyId"
            DataSourceID="sqlDSCompanyMaster" GridLines="Vertical" AllowSorting="True" CssClass="table table-striped" AllowPaging="false" >
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
                <asp:TemplateField HeaderText="Address" SortExpression="Address" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lbAddress" runat="server" Text='<%# Bind("Address") %>' Visible="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PAN" SortExpression="PAN" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lbPAN" runat="server" Text='<%# Bind("PAN") %>' Visible="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Images/Edit.png"/>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images/delete.png"/>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sqlDSCompanyMaster" runat="server" 
                ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                SelectCommand="SELECT * FROM mst_Company where active='1' order by CompanyId"
                DeleteCommand="sp_mst_company" DeleteCommandType="StoredProcedure">
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
