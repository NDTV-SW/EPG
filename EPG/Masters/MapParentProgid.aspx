<%@ Page Title="Map Parent Programme" Language="vb" MasterPageFile="~/SiteEPGBootstrap.Master"
    AutoEventWireup="false" CodeBehind="MapParentProgid.aspx.vb" Inherits="EPG.MapParentProgid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Map Parent Programme</h3>
            </div>
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Channel</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true"
                                    placeholder="Select Channel" />
                                <span class="input-group-addon">
                                    <asp:Label ID="lbIsMovieChannel" runat="server" />
                                    <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtChannel" FirstRowSelected="true" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        &nbsp;
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                Create new parent program </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtParentProgram" runat="server" CssClass="form-control"
                                     ValidationGroup="vg" />
                                 
                                <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtParentProgram" runat="server" Text="*" ForeColor="Red" ControlToValidate="txtParentProgram" ValidationGroup="vg" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                        <div class="form-group">
                            <label>
                                &nbsp; </label>
                            <div class="input-group">
                                <asp:Button ID="btnAddParent" runat="server" Text="Add Parent Program" CssClass="btn btn-info" ValidationGroup="vg" />
                            </div>
                        </div>
                    </div>
                </div>    
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 alert-danger alert h4">
                    Note: For programs to appear in Parent Dropdown, Series needs to be enabled for them.<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; And to set parent program mapping, series needs to be enabled for them as well.
                </div>
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 text-right">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" />
                </div>
                    <asp:GridView ID="grdMap" AutoGenerateColumns="False" runat="server" CssClass="table"
                        DataSourceID="sqlDSMap" AllowSorting="False" AllowPaging="True" PageSize="50"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prog Id" SortExpression="ProgId">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgId" runat="server" Text='<%# Bind("ProgId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prog Name" SortExpression="ProgName">
                                <ItemTemplate>
                                    <asp:Label ID="lbProgName" runat="server" Text='<%# Bind("ProgName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parent Prog Id" SortExpression="Parent ProgId">
                                <ItemTemplate>
                                    <asp:Label ID="lbParentProgId" runat="server" Text='<%# Bind("Parent_ProgId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parent Prog Name" SortExpression="ParentProgName">
                                <ItemTemplate>
                                    <asp:Label ID="lbParentProgName" runat="server" Text='<%# Bind("ParentProgName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Air Date" SortExpression="progdate">
                                <ItemTemplate>
                                    <asp:Label ID="lbairdate" runat="server" Text='<%# Bind("airdate") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Select Parent Programme">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlParentProgram" runat="server" CssClass="form-control"
                                         />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Episode">
                                <ItemTemplate>
                                    <asp:Label ID="lbParentEpisode" runat="server" Text='<%# Bind("parent_episode") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Series">
                                <ItemTemplate>
                                    <asp:Label ID="lbSeriesEnabled" runat="server" Text='<%# Bind("SeriesEnabled") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="Remove Parent" ControlStyle-CssClass="btn btn-xs btn-danger" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDSMap" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                        SelectCommand="select *,(convert(varchar,progdate,106) + ' ' + convert(varchar,progtime,108)) airdate  from fn_getMapParentProgram(@channelid) order by 3,4,2"
                        DeleteCommand="select getdate()">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtChannel" Name="ChannelId" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
