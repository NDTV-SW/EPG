<%@ Page Title="Search Movie" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="movieAll.aspx.vb" Inherits="EPG.movieAll" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function openWin(id) {
            window.open("movieDetails.aspx?id=" + id, "Edit Programme", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Table ID="tbMovieDetails" runat="server" Visible="false"  BorderColor="LightGray" BorderStyle="Solid" CellPadding="4" CellSpacing="4" BorderWidth="1">
        <asp:TableRow>
            <asp:TableHeaderCell HorizontalAlign="Left">
                ID
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Label ID="lbID" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
             <asp:TableHeaderCell HorizontalAlign="Left">
                Star Cast
            </asp:TableHeaderCell><asp:TableCell>
                <asp:Label ID="lbOrigTitle" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
             <asp:TableHeaderCell HorizontalAlign="Left">
                Title
            </asp:TableHeaderCell><asp:TableCell>
                <asp:Label ID="lbTitle" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
             <asp:TableHeaderCell HorizontalAlign="Left">
                Release Date
            </asp:TableHeaderCell><asp:TableCell>
                <asp:Label ID="lbReleaseDate" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
             <asp:TableHeaderCell HorizontalAlign="Left">
                Popularity
            </asp:TableHeaderCell><asp:TableCell>
                <asp:Label ID="lbPopularity" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
             <asp:TableHeaderCell HorizontalAlign="Left">
                Tagline
            </asp:TableHeaderCell><asp:TableCell>
                <asp:Label ID="lbTagline" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
             <asp:TableHeaderCell HorizontalAlign="Left">
                Overview
            </asp:TableHeaderCell><asp:TableCell>
                <asp:Label ID="lbOverview" runat="server" />
            </asp:TableCell></asp:TableRow></asp:Table><asp:Table ID="tbSearch" runat="server">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Search Movie</asp:TableHeaderCell><asp:TableCell>
                <asp:TextBox ID="txtSearch" runat="server"/>
            </asp:TableCell><asp:TableCell>
                <asp:Button ID="btnSearch" runat="server" Text="Search" />            
                <asp:Button ID="btnUpdate" runat="server" Text="Synchronize Movie Master with TMDB"  BackColor="RosyBrown"/>            
            </asp:TableCell></asp:TableHeaderRow><asp:TableRow>
            <asp:TableCell ColumnSpan="3">
               <asp:GridView ID="grdMovieResults" runat="server" CellPadding="4" ForeColor="#333333"
                    EmptyDataText="No Record Found" 
                    GridLines="Vertical"><AlternatingRowStyle BackColor="White" /><EditRowStyle 
                    BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" 
                    ForeColor="White" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" 
                    ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" 
                    HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle 
                    BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle 
                    BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle 
                    BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                </asp:TableCell></asp:TableRow></asp:Table>
                <h1>All Movies in Movie Master</h1>
                <asp:GridView ID="grdMovieMaster" runat="server" CellPadding="4" ForeColor="#333333"
                    EmptyDataText="No Record Found"  DataSourceID="sqlDSMovieMaster" AutoGenerateColumns="false"
                    AllowSorting="true" AllowPaging="true" PageSize="500"
                    GridLines="Vertical"><AlternatingRowStyle BackColor="White" /><EditRowStyle 
                    BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" 
                    ForeColor="White" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" 
                    ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" 
                    HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle 
                    BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle 
                    BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle 
                    BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <Columns>
                        <asp:TemplateField HeaderText="rowid" SortExpression="rowid" Visible="false">
                           <ItemTemplate>
                              <asp:Label ID="lbrowid" runat="server" Text='<%# Bind("rowid") %>' />
                           </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Movie" SortExpression="Movie">
                           <ItemTemplate>
                              <asp:Label ID="lbMovie" runat="server" Text='<%# Bind("Movie") %>' />
                           </ItemTemplate>
                           <ControlStyle Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Synopsis" SortExpression="Synopsis">
                           <ItemTemplate>
                              <asp:Label ID="lbSynopsis" runat="server" Text='<%# Bind("Synopsis") %>' />
                           </ItemTemplate>
                           <ControlStyle Width="500px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Starcast" SortExpression="Starcast">
                           <ItemTemplate>
                              <asp:Label ID="lbStarcast" runat="server" Text='<%# Bind("Starcast") %>' />
                           </ItemTemplate>
                           <ControlStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Release Year" SortExpression="ReleaseYear">
                           <ItemTemplate>
                              <asp:Label ID="lbReleaseYear" runat="server" Text='<%# Bind("ReleaseYear") %>' />
                           </ItemTemplate>
                           <ControlStyle Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView> 
                <asp:SqlDataSource ID="sqlDSMovieMaster" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                     SelectCommand="select rowid,moviename Movie, Synopsis, Starcast,releaseYear from mst_moviesdb order by starcast desc, releaseyear desc">
                  </asp:SqlDataSource>                
</asp:Content>