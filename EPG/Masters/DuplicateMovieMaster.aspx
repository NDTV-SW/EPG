<%@ Page Title="Duplicate Movie Master" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="DuplicateMovieMaster.aspx.vb" Inherits="EPG.DuplicateMovieMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>Duplicate Movie Master</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Movie Name</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtMovieName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFVtxtChannelName" ControlToValidate="txtMovieName"
                                        runat="server" ForeColor="Red" Text="*" ErrorMessage="Movie name can not be left blank."
                                        ValidationGroup="RFGMovieMaster"></asp:RequiredFieldValidator>
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlLanguageid" runat="server" CssClass="form-control" DataTextField="FullName"
                                    DataValueField="LanguageID" DataSourceID="SqlDSLanguage" AutoPostBack="true" />
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12 text-right ">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtHiddenId" runat="server" Visible="false" Text="0"></asp:TextBox>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="RFGMovieMaster"
                                    UseSubmitBehavior="false" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="Black" DataSourceID="SqlDsGrid"
                        CssClass="table" GridLines="Vertical" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="True" PageSize="50"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="True">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:Label ID="lbId" runat="server" Text='<%#Eval("Id") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Movie Name" SortExpression="Movie_Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbMovieName" runat="server" Text='<%# Bind("Movie_Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Added By" DataField="addedon" SortExpression="addedon" />
                            <asp:BoundField HeaderText="Added On" DataField="addedby" SortExpression="addedby" />
                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/images/delete.png" />
                        </Columns>
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
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDsGrid" runat="server" ConnectionString="<%$ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select * from mst_duplicate_movies where Languageid=@languageid order by 2"
        DeleteCommand="select dbo.getlocaldate()"
        >
        <SelectParameters>
            <asp:ControlParameter Name="languageid" ControlID="ddlLanguageid" PropertyName="SelectedValue" DbType="Int16" />

        </SelectParameters>

    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDsLanguage" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select LanguageID,FullName from mst_language where active=1 order by 1"
        
        ></asp:SqlDataSource>

</asp:Content>
