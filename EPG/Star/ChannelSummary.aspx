<%@ Page Title="Channel Summary" Language="vb" MasterPageFile="SiteStar.Master" AutoEventWireup="false"
    CodeBehind="ChannelSummary.aspx.vb" Inherits="EPG.ChannelSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info text-center">
        <div class="panel-heading">
            <h2>
                Channel Summary
            </h2>
        </div>
        <div class="col-lg-12">
            <table class="table table-bordered">
                <tr>
                    <th class="alert-success">
                        Channel
                    </th>
                    <th>
                        <asp:TextBox ID="txtChannel" CssClass="form-control" runat="server"></asp:TextBox>
                    </th>
                    <th class="alert-success">
                        SD/HD
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlSDHD" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="SD" Value="SD"></asp:ListItem>
                            <asp:ListItem Text="HD" Value="HD"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                    <th class="alert-success">
                        Channel Type
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlChannelType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Domestic" Value="Domestic"></asp:ListItem>
                            <asp:ListItem Text="International" Value="International"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                    <th class="alert-success">
                        Category
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataSourceID="sqlDSCatgeory"
                            DataTextField="category" DataValueField="category" />
                    </th>
                    <th class="alert-success">
                        Single FPC for All regions
                    </th>
                    <th>
                        <asp:CheckBox ID="chkSingleFPCforAllRegions" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        Genre
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlGenre" runat="server" CssClass="form-control" DataSourceID="sqlDSGenre"
                            DataTextField="GenreName" DataValueField="Genrename" />
                    </th>
                    <th class="alert-success">
                        Generic Synopsis
                    </th>
                    <th>
                        <asp:CheckBox ID="chkGenericSynopsis" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Episode number
                    </th>
                    <th>
                        <asp:CheckBox ID="chkEpisodeNo" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Star Cast
                    </th>
                    <th>
                        <asp:CheckBox ID="chkStarCast" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Guest Name
                    </th>
                    <th>
                        <asp:CheckBox ID="chkGuestName" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        Provider Name 1
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderName1" CssClass="form-control" runat="server" />
                    </th>
                    <th class="alert-success">
                        Provider Contact 1
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderContact1" runat="server" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Provider Mail 1
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderMail1" runat="server" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Escalation POC 1
                    </th>
                    <th>
                        <asp:TextBox ID="txtEscalationPoc1" runat="server" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Escalation POC Contact 1
                    </th>
                    <th>
                        <asp:TextBox ID="txtEscalationPOCContact1" runat="server" CssClass="form-control" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        Provider Name 2
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderName2" runat="server" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Provider Contact 2
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderContact2" runat="server" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Provider Mail 2
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderMail2" runat="server" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Escalation POC 2
                    </th>
                    <th>
                        <asp:TextBox ID="txtEscalationPoc2" runat="server" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Escalation POC Contact 2
                    </th>
                    <th>
                        <asp:TextBox ID="txtEscalationPOCContact2" runat="server" CssClass="form-control" />
                    </th>
                </tr>
                <tr class="alert-info">
                    <th>
                        Provider Name 3
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderName3" runat="server" CssClass="form-control" />
                    </th>
                    <th>
                        Provider Mail 3
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderMail3" runat="server" CssClass="form-control" />
                    </th>
                    <th>
                        Provider Contact 3
                    </th>
                    <th>
                        <asp:TextBox ID="txtProviderContact3" runat="server" CssClass="form-control" />
                    </th>
                    <th>
                        Escalation POC 3
                    </th>
                    <th>
                        <asp:TextBox ID="txtEscalationPoc3" runat="server" CssClass="form-control" />
                    </th>
                    <th>
                        Escalation POC Contact 3
                    </th>
                    <th>
                        <asp:TextBox ID="txtEscalationPOCContact3" runat="server" CssClass="form-control" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        Press Release
                    </th>
                    <th>
                        <asp:CheckBox ID="chkPressRelease" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Episodic Synopsis Prog Level
                    </th>
                    <th>
                        <asp:CheckBox ID="chkEpiodicSynopsisProgLevel" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Episodic Synopsis in bulk for Multiple Episodes
                    </th>
                    <th>
                        <asp:CheckBox ID="chkEPisodicSynopsisInBulkfForMultipleEpisodes" runat="server" Checked="true"
                            CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Episodic Title
                    </th>
                    <th>
                        <asp:CheckBox ID="chkEPisodicTitle" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Feed
                    </th>
                    <th>
                        <asp:TextBox ID="txtFeed" runat="server" CssClass="form-control" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        Generic Image
                    </th>
                    <th>
                        <asp:CheckBox ID="chkGenericImage" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Episodic Image
                    </th>
                    <th>
                        <asp:CheckBox ID="chkEpisodicImage" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Special Programs
                    </th>
                    <th>
                        <asp:CheckBox ID="chkSpecialPrograms" runat="server" Checked="true" CssClass="form-control" />
                    </th>
                    <th class="alert-success">
                        Images of Special Programs
                    </th>
                    <th>
                        <asp:CheckBox ID="chkImagesofSpecialPrograms" runat="server" CssClass="form-control"
                            Checked="true" />
                    </th>
                    <th class="alert-success">
                        Active
                    </th>
                    <th>
                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-success" colspan="8">
                        &nbsp;
                    </th>
                    <th class="alert-success" colspan="2">
                        <asp:Button ID="btnSaveMe" runat="server" Text="SAVE" CssClass="btn alert-info" />
                        <asp:Label ID="lbID" runat="server" Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="CLEAR" CssClass="btn alert-danger" />
                    </th>
                </tr>
            </table>
        </div>
        <div class="panel-body">
            <asp:GridView ID="grd" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                DataSourceID="sqlDS" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Vertical" AutoGenerateColumns="false">
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
                    <asp:CommandField ButtonType="Image" ShowSelectButton="true" SelectImageUrl="~/Images/edit.png" />
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                            <asp:Label ID="lbID" runat="server" Text='<%#Bind("id") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="channelid" HeaderText="Channel" />
                    <asp:BoundField DataField="feed" HeaderText="Feed" />
                    <asp:BoundField DataField="Genre" HeaderText="Genre" />
                    <asp:BoundField DataField="GenericSynopsis" HeaderText="Generic Synopsis" />
                    <asp:BoundField DataField="SingleFPCForAllRegions" HeaderText="Single FPC for All Regions" />
                    <asp:BoundField DataField="EpisodicSynopsisProgLevel" HeaderText="Epi Syn Prog Level" />
                    <asp:BoundField DataField="EpisodicSynopsisInBulkForMultipleEpisodes" HeaderText="Epi Syn In Bulk For Multi Epi" />
                    <asp:BoundField DataField="EpisodicTitle" HeaderText="Episodic Title" />
                    <asp:BoundField DataField="EpisodeNo" HeaderText="Episode No." />
                    <asp:BoundField DataField="StarCast" HeaderText="Star Cast" />
                    <asp:BoundField DataField="GuestName" HeaderText="Guest Name" />
                    <asp:BoundField DataField="GenericImage" HeaderText="Generic Image" />
                    <asp:BoundField DataField="EpisodicImage" HeaderText="Episodic Image" />
                    <asp:BoundField DataField="PressRelease" HeaderText="Press Release" />
                    <asp:BoundField DataField="SpecialPrograms" HeaderText="Special Programs" />
                    <asp:BoundField DataField="ImagesofSpecialPrograms" HeaderText="Images Special Programs" />
                    <asp:BoundField DataField="ProviderName1" HeaderText="Provider Name 1" />
                    <asp:BoundField DataField="ProviderMail1" HeaderText="Mail" />
                    <asp:BoundField DataField="ProviderContact1" HeaderText="Contact" />
                    <asp:BoundField DataField="ProviderName2" HeaderText="Provider Name 2" />
                    <asp:BoundField DataField="ProviderMail2" HeaderText="Mail" />
                    <asp:BoundField DataField="ProviderContact2" HeaderText="Contact" />
                    <asp:BoundField DataField="ProviderName3" HeaderText="Provider Name 3" />
                    <asp:BoundField DataField="ProviderMail3" HeaderText="Mail" />
                    <asp:BoundField DataField="ProviderContact3" HeaderText="Contact" />
                    <asp:BoundField DataField="EscalationPOC1" HeaderText="Escalation POC 1" />
                    <asp:BoundField DataField="EscalationPOCContact1" HeaderText="Contact" />
                    <asp:BoundField DataField="EscalationPOC2" HeaderText="Escalation POC 2" />
                    <asp:BoundField DataField="EscalationPOCContact2" HeaderText="Contact" />
                    <asp:BoundField DataField="EscalationPOC3" HeaderText="Escalation POC 3" />
                    <asp:BoundField DataField="EscalationPOCContact3" HeaderText="Contact" />
                    
                    <asp:BoundField DataField="active" HeaderText="active" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select id,Channelid,Genre,SingleFPCForAllRegions,GenericSynopsis,EpisodicSynopsisProgLevel,EpisodicSynopsisInBulkForMultipleEpisodes,EpisodicTitle,EpisodeNo,StarCast,GuestName,GenericImage,EpisodicImage,PressRelease,SpecialPrograms,ImagesofSpecialPrograms,ProviderName1,ProviderMail1,ProviderContact1,ProviderName2,ProviderMail2,ProviderContact2,ProviderName3,ProviderMail3,ProviderContact3,EscalationPOC1,EscalationPOC2,EscalationPOC3,EscalationPOCContact1,EscalationPOCContact2,EscalationPOCContact3, active,feed from fpc_channelsummary order by channelid" />
    <asp:SqlDataSource ID="sqlDSCatgeory" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select distinct category from fpc_channelsummary order by 1" />
    <asp:SqlDataSource ID="sqlDSGenre" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select genrename from mst_genre where genrecategory='S' order by 1" />
</asp:Content>
