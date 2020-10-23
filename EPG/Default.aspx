<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="EPG._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="lblAccess" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"
        Visible="False"></asp:Label>
    <asp:Label ID="lbPasswordExpire" runat="server" Font-Bold="True" Font-Size="Medium"
        ForeColor="Red"></asp:Label>
    <br />
    <asp:Button ID="btnRefreshChannelList" runat="server" Text="Refresh Channels" CssClass="btn btn-info" />
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="panel panel-info">
                <div class="panel-heading text-center alert alert-info">
                    <h4>URGENT 8th/10th Day EPG Missing</h4>
                    <asp:Button ID="btn8and10" runat="server" Text="View" CssClass="btn btn-info" />
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grd10Days" CssClass="table table-striped" runat="server" 
                        AutoGenerateColumns="true" AllowSorting="true" EmptyDataText="...No Record found .....">
                    </asp:GridView>
                </div>
                
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="panel panel-info">
            <div class="panel-heading text-center">
                    <h4>Minimum 8 days EPG Data required</h4>
                    <asp:Button ID="btn8Day" runat="server" Text="View" CssClass="btn btn-info" />
                </div>
            <div class="panel-body">
                <asp:GridView ID="grdEPGMissing8Days" CssClass="table table-striped" runat="server"
                    AutoGenerateColumns="false" AllowSorting="true" EmptyDataText="...No Record found .....">
                    <Columns>
                        <asp:TemplateField HeaderText="Channel" SortExpression="ChannelId">
                            <ItemTemplate>
                                <asp:Label ID="lbChannel" runat="server" Text='<%#Eval("ChannelId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EPG available for(Days)" SortExpression="noOfDays">
                            <ItemTemplate>
                                <asp:Label ID="lbnoOfDays" runat="server" Text='<%#Eval("noOfDays") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EPG From" SortExpression="EPGFrom">
                            <ItemTemplate>
                                <asp:Label ID="lbEPGFrom" runat="server" Text='<%#Eval("EPGFrom") %>' />
                            </ItemTemplate>
                            <ControlStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EPG To" SortExpression="EPGTo">
                            <ItemTemplate>
                                <asp:Label ID="lbEPGTo" runat="server" Text='<%#Eval("EPGTo") %>' />
                            </ItemTemplate>
                            <ControlStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operators" SortExpression="Operators">
                            <ItemTemplate>
                                <asp:Label ID="lbOperators" runat="server" Text='<%#Eval("Operators") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
            </div>
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <div class="panel panel-info">
                <div class="panel-heading text-center alert alert-info">
                    <h4>URGENT Images Required for new Programmes</h4>
                    <asp:Button ID="btnImagesRequired" runat="server" Text="View" CssClass="btn btn-info" />
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grdImagesRequired" CssClass="table table-striped" runat="server" 
                        AutoGenerateColumns="true" AllowSorting="true" EmptyDataText="...No Record found .....">
                    </asp:GridView>
                </div>
                
            </div>
        </div>
    </div>
    
        <asp:Button ID="btnRefreshAPI" runat="server" Text="Refresh API" CssClass="btn btn-danger"
            style="position:absolute ;bottom:50px;right:0"
         />    
    
    
    <asp:SqlDataSource ID="sqlDS_EPGMissing8Days" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDS_EPGMissing10Days" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDS_ImagesRequired" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />

</asp:Content>
