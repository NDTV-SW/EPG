<%@ Page Title="About Us" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
   CodeBehind="About.aspx.vb" Inherits="EPG.About" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <h2>
      About
   </h2>
   <p>
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
         DataSourceID="SqlDataSource1">
         <Columns>
            <asp:BoundField DataField="ProgramName" HeaderText="ProgramName" 
               SortExpression="ProgramName" />
            <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
            <asp:BoundField DataField="Duration" HeaderText="Duration" 
               SortExpression="Duration" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
               SortExpression="Description" />
            <asp:BoundField DataField="EpisodeNo" HeaderText="EpisodeNo" 
               SortExpression="EpisodeNo" />
            <asp:BoundField DataField="ShowwiseDescription" 
               HeaderText="ShowwiseDescription" SortExpression="ShowwiseDescription" />
         </Columns>
      </asp:GridView>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
         SelectCommand="SELECT * FROM [mapEpg1]"></asp:SqlDataSource>
      Put content here.
   </p>
</asp:Content>

