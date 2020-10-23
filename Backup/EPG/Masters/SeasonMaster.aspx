<%@ Page Title="Season Master" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SeasonMaster.aspx.vb" Inherits="EPG.SeasonMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"/>
    <h2>
      Season Master
   </h2>
   <table border="1" cellpadding="4px">
    <tr>
        <th>Select Channel</th>
        <td>
            <asp:ComboBox ID="ddlChannelName" runat="server"  Width="200px"  AutoCompleteMode="SuggestAppend"
               DropDownStyle="DropDownList" ItemInsertLocation="Append"  AutoPostBack="True"
               DataSourceID="SqlDsChannelMaster" DataTextField="ChannelID" 
               DataValueField="ChannelID">
            </asp:ComboBox>
            <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="SELECT ChannelID FROM mst_Channel where active='1' and  seasonrequired='1' and seriesenabled='1' ORDER BY ChannelID">
            </asp:SqlDataSource>    
        </td>
        <th>Select Programme</th>
        <td>
            <asp:ComboBox ID="ddlProgramme" runat="server"  Width="200px"  AutoCompleteMode="SuggestAppend"
               DropDownStyle="DropDownList" ItemInsertLocation="Append"
               DataSourceID="SqlDsProgrammeMaster" DataTextField="progname" 
               DataValueField="progid">
            </asp:ComboBox>
            <asp:SqlDataSource ID="SqlDsProgrammeMaster" runat="server" 
               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
               SelectCommand="SELECT progid,progname FROM mst_program where active='1' and channelid=@channelid ORDER BY 2">
               <SelectParameters>
               <asp:ControlParameter ControlID="ddlChannelName" Name="channelid" PropertyName="SelectedValue" Type="String" />
               </SelectParameters>
            </asp:SqlDataSource>    
        </td>
    </tr>
    <tr>
        <th>
            Season no.
        </th>
        <td>
            <asp:TextBox ID="txtSeasonNo" runat="server" ValidationGroup="RFGSeasonMaster" /><br />
            <asp:RequiredFieldValidator ID="RFVtxtSeasonNo" ControlToValidate="txtSeasonNo" runat="server" ForeColor="Red" Text="* (Season cannot be left blank Required.)" ValidationGroup="RFGSeasonMaster" />
        </td>
        <th>
            Start Date
        </th>
        <td>
        <asp:TextBox ID="txtStartDate" runat="server" Width="150px" Enabled="true" ValidationGroup="RFGSeasonMaster"></asp:TextBox>
            <asp:Image ID="imgStartDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CE_txtStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgStartDateCalendar" />
            <asp:TextBoxWatermarkExtender TargetControlID="txtStartDate" ID="ExttxtStartDate" WatermarkText="Select Start Date" runat="server"></asp:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="RFVtxtStartDate" ControlToValidate="txtStartDate" runat="server" ForeColor="Red" Text="* (Start date can not be left blank.)" ValidationGroup="RFGSeasonMaster" />
        </td>
        <th>
            End date
        </th>
        <td>
            <asp:TextBox ID="txtEndDate" runat="server" Width="150px" Enabled="true" ValidationGroup="RFGSeasonMaster"></asp:TextBox>
            <asp:Image ID="imgEndDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CE_txtEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="imgEndDateCalendar" />
            <asp:TextBoxWatermarkExtender TargetControlID="txtEndDate" ID="ExttxtEndDate" WatermarkText="Select End date" runat="server"></asp:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="RFVtxtEndDate" ControlToValidate="txtEndDate" runat="server" ForeColor="Red" Text="* (End date can not be left blank.)" ValidationGroup="RFGSeasonMaster" />
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Operator="LessThanEqual" ForeColor="Red" Type="Date" ErrorMessage="* (End date cannot be less than Start date)"
               ValidationGroup="RFGSeasonMaster"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btnAdd" runat="server" Text="Add" Width="80px" ValidationGroup="RFGSeasonMaster" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:GridView ID="grdSeasons"  runat="server" BackColor="White" width="100%" DataSourceID="sqlDSSeasonMaster"
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="false"
                ForeColor="Black" GridLines="Vertical" EmptyDataText="No record Found..." AllowSorting="true" >
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
                    <asp:TemplateField HeaderText="progid" SortExpression="progid" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbprogid" runat="server" Text='<%# Bind("progid") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId">
                        <ItemTemplate>
                            <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Programme" SortExpression="progname" >
                        <ItemTemplate>
                            <asp:Label ID="lbprogname" runat="server" Text='<%# Bind("progname") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Season" SortExpression="seasonNo">
                        <ItemTemplate>
                            <asp:Label ID="lbseasonNo" runat="server" Text='<%# Bind("seasonNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate">
                        <ItemTemplate>
                            <asp:Label ID="lbStartDate" runat="server" Text='<%# Bind("StartDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EndDate" SortExpression="EndDate">
                        <ItemTemplate>
                            <asp:Label ID="lbEndDate" runat="server" Text='<%# Bind("EndDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png"/>
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/images/delete.png"/>
                    
                </Columns>
            </asp:GridView>
             <asp:SqlDataSource ID="sqlDSSeasonMaster" runat="server" 
                ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                SelectCommand="select a.progid,b.ChannelId,b.ProgName,a.SeasonNo,CONVERT(varchar,a.Startdate,106) Startdate,CONVERT(varchar,a.EndDate,106) EndDate from mst_program_seasons a join mst_program b on a.ProgID=b.ProgID where a.channelid=@ChannelID order by progname"
                DeleteCommand="select dbo.GetLocalDate()" >
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelID" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            </td>
        </tr>
   </table>
</asp:Content>

