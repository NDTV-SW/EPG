<%@ Page Title="Ureqa Synopsis" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCMS.Master"
    CodeBehind="UreqaSynopsis.aspx.vb" Inherits="EPG.UreqaSynopsis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Ureqa Synopsis</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Select Channel</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlChannel" runat="server" DataTextField="channelid" DataValueField="channelid"
                                    DataSourceID="SqlmstChannel" CssClass="form-control" AutoPostBack="true" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="SqlmstChannel" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT channelid FROM mst_channel where onair=1 and publicchannel=1 order by 1">
                                    </asp:SqlDataSource>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Select Programme</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlProgramme" runat="server" DataTextField="Progname" DataValueField="Progid"
                                    DataSourceID="sqlMstProgram" CssClass="form-control" AutoPostBack="true" />
                                <span class="input-group-addon">
                                    <asp:SqlDataSource ID="sqlMstProgram" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                                        SelectCommand="SELECT progid,Progname from mst_program where active=1 and channelid=@channelid and progid in (select progid from mst_epg where channelid=@channelid and convert(varchar, progdate,112) >= convert(varchar, dbo.getLocalDate(),112) ) order by progname">
                                        <SelectParameters>
                                            <asp:ControlParameter Name="channelid" ControlID="ddlChannel" PropertyName="SelectedValue"
                                                Direction="Input" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Select Language</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value="0" />
                                    <asp:ListItem Text="English" Value="1" />
                                    <asp:ListItem Text="Hindi" Value="2" />
                                </asp:DropDownList>
                                <span class="input-group-addon"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                Synopsis</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtSynopsis" runat="server" CssClass="form-control" TextMode="MultiLine"
                                    Rows="3" />
                                <span class="input-group-addon">
                                    <asp:RequiredFieldValidator ID="RFVtxtSynopsis" runat="server" ValidationGroup="VG"
                                        ControlToValidate="txtSynopsis" Text="*" ForeColor="Red" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                        <div class="form-group">
                            <label>
                                &nbsp;</label>
                            <div class="input-group">
                                <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-info" ValidationGroup="VG" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                                <asp:Label ID="lbID" runat="server" Visible="true" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="grd" runat="server" DataSourceID="sqlDSGrd" CssClass="table" BackColor="White"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
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
                        <asp:CommandField ButtonType="Image" ShowSelectButton="true"  SelectImageUrl="~/Images/edit.png"  />
                        <asp:CommandField ButtonType="Image" ShowDeleteButton="true"  DeleteImageUrl="~/Images/delete.png"  />
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="lbProgid" runat="server" Text='<%#Eval("progid") %>' Visible="false" />
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Channel">
                            <ItemTemplate>
                                <asp:Label ID="lbChannelId" runat="server" Text='<%#Eval("channelid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Programme">
                            <ItemTemplate>
                                <asp:Label ID="lbProgname" runat="server" Text='<%#Eval("progname") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Language ID">
                            <ItemTemplate>
                                <asp:Label ID="lbLanguageId" runat="server" Text='<%#Eval("languageid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Synopsis">
                            <ItemTemplate>
                                <asp:Label ID="lbSynopsis" runat="server" Text='<%#Eval("synopsis") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSGrd" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select a.progid,b.Channelid,b.Progname,a.synopsis,a.languageid,a.lastupdate from mst_ureqasynopsis a join mst_program b on a.progid=b.progid and a.progid=@progid">
        <SelectParameters>
            <asp:ControlParameter Name="progid" ControlID="ddlProgramme" PropertyName="SelectedValue"
                Direction="Input" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
