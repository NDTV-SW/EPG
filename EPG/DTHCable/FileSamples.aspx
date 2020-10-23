<%@ Page Title="File Samples" Language="vb" MasterPageFile="SiteDTH.Master"
    AutoEventWireup="false" CodeBehind="FileSamples.aspx.vb" Inherits="EPG.FileSamples" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    <h3>
                        EPG File Samples    </h3>
            </div>
            <div class="panel-body">
              
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
                                <asp:TemplateField HeaderText="Procedure">
                                    <ItemTemplate>
                                        <asp:HyperLink id="hyFileSample" runat="server" Target="_blank" NavigateUrl='<%# Bind("url") %>' Text='<%# Bind("functionname") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                            SelectCommand="SELECT distinct functionname, 'filesamples/' + FunctionName  +'.' + case EpgType when'zee' then 'xls' else EpgType end url from mst_dthcableoperators where functionname<>'' and Active=1  order by 1">
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
        </div>
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
