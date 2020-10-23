<%@ Page Title="Dummy EPG Channels" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="DummyEPGChannels.aspx.vb" Inherits="EPG.DummyEPGChannels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Dummy EPG Channels</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>
                            Channel</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtChannel" runat="server" CssClass="form-control" AutoPostBack="true" />
                            <span class="input-group-addon">
                                <asp:AutoCompleteExtender ID="ACE_txtChannel" runat="server" ServiceMethod="SearchChannel"
                                    MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="txtChannel" FirstRowSelected="true" />
                                <asp:RequiredFieldValidator ID="RFV_txtChannel" runat="server" ControlToValidate="txtChannel"
                                    Text="*" ForeColor="Red" ValidationGroup="VG" />
                                <asp:Label ID="lbChannelGenre" runat="server" Text="." />
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>
                            Copy Last Week</label>
                        <asp:CheckBox ID="chkCopyLastWeek" runat="server" CssClass="form-control" Checked="false"  />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>
                            Dummy Programme</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtDummyProgramme" runat="server" CssClass="form-control" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtDummyProgramme" runat="server" ControlToValidate="txtDummyProgramme"
                                    Text="*" ForeColor="Red" ValidationGroup="VG" />
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>
                            Dummy Synopsis</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtDummySynopsis" runat="server" CssClass="form-control" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtDummySynopsis" runat="server" ControlToValidate="txtDummySynopsis"
                                    Text="*" ForeColor="Red" ValidationGroup="VG" />
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label>
                            Dummy Duration</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtDummyDuration" runat="server" CssClass="form-control" />
                            <span class="input-group-addon">
                                <asp:RequiredFieldValidator ID="RFV_txtDummyDuration" runat="server" ControlToValidate="txtDummyDuration"
                                    Text="*" ForeColor="Red" ValidationGroup="VG" />
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>
                            &nbsp;&nbsp;</label>
                        <div class="input-group">
                            <asp:CheckBox ID="chkActive" runat="server" Text=" Active" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn alert-info" ValidationGroup="VG" />&nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn alert-danger" />
                            <asp:Label ID="lbID" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>
                        Filter by</label>
                    <asp:CheckBox ID="chkSearchCopyLastWeek" runat="server" Checked="false" Text="Copy Last Week"
                        AutoPostBack="true" />
                </div>
                <asp:GridView ID="grd" runat="server" CssClass="table" DataKeyNames="id" DataSourceID="sqlDS"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/images/edit.png" />
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Channel" DataField="channelid" />
                        <asp:BoundField HeaderText="Genre" DataField="genrename" />
                        <asp:BoundField HeaderText="Name_of_Programme" DataField="progname" />
                        <asp:BoundField HeaderText="Synopsis" DataField="progsynopsys" />
                        <asp:BoundField HeaderText="Duration" DataField="progduration" />
                         <asp:TemplateField HeaderText="Copy_Last_Week">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkcopylastweek1" runat="server" Checked='<%#Bind("copylastweek") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive1" runat="server" Checked='<%#Bind("Active") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommandType="Text" SelectCommand="select a.id,a.channelid,b.genrename,a.progname,a.progsynopsys,a.progduration,a.copylastweek,a.active from mst_insertdummyepg a join mst_channel c on a.channelid=c.channelid join mst_genre b  on c.genreid=b.genreid where a.copylastweek=@copylastweek order by a.active desc,b.genrename,a.channelid"
            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
            <SelectParameters>
                <asp:ControlParameter Name="copylastweek" ControlID="chkSearchCopyLastWeek" PropertyName="Checked" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
