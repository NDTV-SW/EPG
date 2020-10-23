<%@ Page Title="Corona Cases" Language="vb" AutoEventWireup="false" MasterPageFile="~/Corona/SiteCorona.Master"
    CodeBehind="Default.aspx.vb" Inherits="EPG.DefaultCorona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="sc1" runat="server" />
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="#"><em class="fa fa-home"></em></a></li>
            <li class="active">District Wise Corona Cases</li>
        </ol>
    </div>
    <div class="row">
        <div class="panel panel-info">
            <div class="panel-body">
                <table class="table table-bordered table-striped">
                    <tr>
                        <th>
                            State
                            <asp:RequiredFieldValidator ID="RFV_ddlState" runat="server" ControlToValidate="ddlState"
                                Text="*" ForeColor="Red" ValidationGroup="VG" InitialValue="0" />
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Text="--select--" Value="0" />
                                <asp:ListItem Text="Movie" Value="Movie" />
                                <asp:ListItem Text="Show" Value="Show" />
                                <asp:ListItem Text="News" Value="News" />
                                <asp:ListItem Text="Sports" Value="Sports" />
                            </asp:DropDownList>
                        </td>
                        <th>
                            <div class="input-group">
                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="add state" />
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFV_txtState" runat="server" ControlToValidate="txtState"
                                        Text="<b>*</b>" ForeColor="Red" ValidationGroup="VGState" />
                                </span>
                            </div>
                        </th>
                        <td>
                            <asp:Button ID="btnAddState" runat="server" Text="Add State" ValidationGroup="VGState"
                                CssClass="btn alert-danger1 btn-xs" />
                        </td>
                        <th>
                            District
                            <asp:RequiredFieldValidator ID="RFV_ddlDistrict" runat="server" ControlToValidate="ddlDistrict"
                                Text="*" ForeColor="Red" ValidationGroup="VG" InitialValue="0" />
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--select--" Value="0" />
                                <asp:ListItem Text="U" Value="U" />
                                <asp:ListItem Text="UA" Value="UA" />
                                <asp:ListItem Text="A" Value="A" />
                            </asp:DropDownList>
                        </td>
                        <th>
                            <div class="input-group">
                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" placeholder="add district" />
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFV_txtDistrict" runat="server" ControlToValidate="txtDistrict"
                                        Text="<b>*</b>" ForeColor="Red" ValidationGroup="VGDistrict" />
                                </span>
                            </div>
                        </th>
                        <td>
                            <asp:Button ID="btnAddDistrict" runat="server" Text="Add District" ValidationGroup="VGDistrict"
                                CssClass="btn alert-danger1 btn-xs" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Confirmed Cases
                            <asp:RequiredFieldValidator ID="RFV_txtConfirmed" runat="server" ControlToValidate="txtConfirmed"
                                Text="*" ForeColor="Red" ValidationGroup="VG" />
                        </th>
                        <td>
                            <asp:TextBox ID="txtConfirmed" runat="server" CssClass="form-control" />
                        </td>
                        <th>
                            Recovered Cases
                            <asp:RequiredFieldValidator ID="RFV_txtRecovered" runat="server" ControlToValidate="txtRecovered"
                                Text="*" ForeColor="Red" ValidationGroup="VG" />
                        </th>
                        <td>
                            <asp:TextBox ID="txtRecovered" runat="server" CssClass="form-control" />
                        </td>
                        <th>
                            Deaths
                            <asp:RequiredFieldValidator ID="RFV_txtDeath" runat="server" ControlToValidate="txtDeath"
                                Text="*" ForeColor="Red" ValidationGroup="VG" />
                        </th>
                        <td>
                            <asp:TextBox ID="txtDeath" runat="server" CssClass="form-control" />
                        </td>
                        <th colspan="2">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn alert-danger1 btn-sm"
                                ValidationGroup="VG" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn alert-danger1 btn-sm" />
                            <asp:Label ID="lbId" runat="server" Visible="false" />
                        </th>
                    </tr>
                </table>
            </div>
            <div class="panel-heading">
                <div class="col-md-2">
                    <asp:TextBox ID="txtSearch" runat="server" placeholder="search" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-xs alert-danger1" />
                </div>
            </div>
            <div class="panel-body">
                <asp:GridView ID="grd1" runat="server" CssClass="table table-bordered table-striped"
                    EmptyDataText="--no record found--" DataKeyNames="id" EmptyDataRowStyle-CssClass="alert alert-danger text-center"
                    SelectedRowStyle-BackColor="#D0021B" SelectedRowStyle-ForeColor="#ffffff" AutoGenerateColumns="false">
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="true" DeleteImageUrl="~/Images/delete.png"
                            ItemStyle-CssClass="alert-danger1" />
                        <asp:CommandField ButtonType="Image" ShowSelectButton="true" SelectImageUrl="~/Images/edit.png" />
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="type" HeaderText="type" />
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyName" runat="server" Text='<%#Eval("name") %>' />
                                <asp:Label ID="lbId" runat="server" Text='<%#Eval("id") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Lang" HeaderText="Lang" />
                        <asp:BoundField DataField="parentalguide" HeaderText="PG" />
                        <asp:BoundField DataField="rating" HeaderText="Rating" />
                        <asp:BoundField DataField="synopsis" HeaderText="Synopsis" />
                        <asp:BoundField DataField="releaseyear" HeaderText="Year" />
                        <asp:BoundField DataField="country" HeaderText="Country" />
                        <asp:BoundField DataField="genrename1" HeaderText="Genre1" />
                        <asp:BoundField DataField="genrename2" HeaderText="Genre2" />
                        <asp:BoundField DataField="verified" HeaderText="Verified" />
                        <asp:BoundField DataField="active" HeaderText="Active" />
                        <asp:BoundField DataField="updatedby" HeaderText="By" />
                        <asp:BoundField DataField="updatedat" HeaderText="At" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <%--<asp:SqlDataSource ID="sqlDS" runat="server" SelectCommand="select  top 100 id,type ,name ,Lang=(select fullname from mst_language where languageid=x.originallanguageid), parentalguide ,rating,synopsis,releaseyear,country,genrename1,genrename2,verified,active,updatedby,updatedat from richmeta x where name like '%' + @search + '%' and type=@type order by 2,3"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" DeleteCommand="delete from richmeta where id=@id;update mst_program set richmetaid=0 where richmetaid=@id">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSearch" PropertyName="Text" Name="search" ConvertEmptyStringToNull="false" />
            <asp:ControlParameter ControlID="ddlType" PropertyName="SelectedValue" Name="type"
                ConvertEmptyStringToNull="false" />
        </SelectParameters>
        <DeleteParameters>
            <asp:ControlParameter ControlID="grd1" Name="id" PropertyName="SelectedDataKey" />
        </DeleteParameters>
    </asp:SqlDataSource>--%>
</asp:Content>
