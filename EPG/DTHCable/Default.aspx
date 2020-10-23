<%@ Page Title="Cable Operators" Language="vb" MasterPageFile="SiteDTH.Master"
    AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="EPG.DTHDefault" %>

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
                                DTH Cable Operators</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <asp:RequiredFieldValidator ID="RFV_txtName" runat="server" ForeColor="Red" Text="*" ControlToValidate="txtName" ValidationGroup="VG" />
                                            </span>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            City</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            System Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtSystemName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            SP Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtFunctionName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Folder Name</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtFolder" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Recipient List</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtRecepientList" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                Rows="3" />
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            CC List</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtCCList" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            EPG Type</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtEPGType" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <div class="form-group">
                                        <label>
                                            Select Query</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtSelectQuery" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                Rows="3"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <div class="form-group">
                                        <label>
                                            Days to Send</label>
                                        <div class="input-group">
                                            <asp:CheckBoxList ID="chkWeekList" runat="server" CssClass="form-control" RepeatDirection="Horizontal"
                                                Style="font-size: small">
                                                <asp:ListItem Value="1" Selected="True">&nbsp;Sun&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2" Selected="True">&nbsp;Mon&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="3" Selected="True">&nbsp;Tue&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="4" Selected="True">&nbsp;Wed&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="5" Selected="True">&nbsp;Thurs&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="6" Selected="True">&nbsp;Fri&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="7" Selected="True">&nbsp;Sat&nbsp;</asp:ListItem>
                                            </asp:CheckBoxList>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Start Day&nbsp;<span class="style2">0 is today, +1</span></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtStartday" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            No Of Days</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtNoOfDays" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Priority</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtPriority" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Operator Type</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtOperatorType" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Time To Send 1</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txttimetosend1" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Time To Send 2</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txttimetosend2" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Time To Send 3</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txttimetosend3" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Time To Send 4</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txttimetosend4" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Suffix (ZEE_<span class="style2">ddMMMyyyy</span>)</label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtFileFormat" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Prefix &nbsp;<span class="style2">ChannelId/ServiceId</span></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtFilePrefixField" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            SID &nbsp;<span class="style2">ChannelId/ServiceId</span></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtSIDField" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Filler &nbsp;<span class="style3"> ZEE</span><span class="style2">_NDTV_</span><span
                                                class="style3">dMy</span></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtFillerString" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Replace String &nbsp;<span class="style3"> ZEE</span><span class="style2">_</span><span
                                                class="style3">TV</span></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtChannelReplaceString" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkHourlyUpdates" runat="server" Text="Updates" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkMultipleDays" runat="server" Text="Multi-Days" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkMultipleChannels" runat="server" Text="Multi Channels in 1 file"
                                            CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkReturnsOutput" runat="server" Text="Returns output" CssClass="form-control">
                                        </asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkMergeFiles" runat="server" Text="Merge Files" CssClass="form-control">
                                        </asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                                    <div class="form-group">
                                        <label>
                                            Is Proper XML
                                        </label>
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkIsProperXML" runat="server" CssClass="form-control"></asp:CheckBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <div class="form-group">
                                        <label>
                                            Default XML &nbsp;<span class="style2">* use &quot;-[]&quot; for &quot;&lt;&quot; and
                                                &quot;{}&quot; for &quot;&gt;&quot;</span></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDefaultXMLString" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <div class="form-group">
                                        <label>
                                            XML String&nbsp;<span class="style2">* use &quot;-[]&quot; for &quot;&lt;&quot; and
                                                &quot;{}&quot; for &quot;&gt;&quot;</span></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtXMLString" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" ValidationGroup="VG" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                <asp:Label ID="lbOperatorID" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                <asp:GridView ID="GridView1" runat="server" DataKeyNames="operatorid" AllowSorting="true"
                    CssClass="table" DataSourceID="SqlDataSource1" AutoGenerateColumns="False">
                    
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" />
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" SortExpression="OperatorID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lbOperatorID" runat="server" Text='<%# Bind("OperatorID") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="OperatorID" SortExpression="OperatorID">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name"></asp:BoundField>
                        <asp:BoundField HeaderText="Type" DataField="EpgType" SortExpression="EpgType"></asp:BoundField>
                        <asp:TemplateField HeaderText="Active" SortExpression="Active" Visible="true">
                            <ItemTemplate>
                                <asp:CheckBox ID="lbActive" runat="server" Checked='<%# Bind("Active") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Updates" SortExpression="HourlyUpdate" Visible="true">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkUpdates" runat="server" Checked='<%# Bind("HourlyUpdate") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Multiple Channels" SortExpression="multipleChannels"
                            Visible="False">
                            <ItemTemplate>
                                <asp:CheckBox ID="lbMultipleChannels" runat="server" Checked='<%# Bind("multipleChannels") %>'
                                    Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Priority" SortExpression="Priority" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lbPriority" runat="server" Text='<%# Bind("Priority") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Start Day" DataField="startDay" SortExpression="startDay">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="No. of Days" DataField="noofdays" SortExpression="noofdays">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Recipients" DataField="RecipientList" SortExpression="RecipientList">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Function" DataField="FunctionName" SortExpression="FunctionName">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Days To Send" DataField="DaysToSend" SortExpression="DaysToSend">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Send Time" DataField="TimeSend1" SortExpression="timetosend1">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Send Time 2" DataField="TimeSend2" SortExpression="TimeToSend2">
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
                    SelectCommand="SELECT *,left(convert(varchar,TimeToSend1,108),5) timeSend1,left(convert(varchar,TimeToSend2,108),5) timeSend2,left(convert(varchar,TimeToSend3,108),5) timeSend3,left(convert(varchar,TimeToSend4,108),5) timeSend4 FROM mst_dthcableoperators order by active desc,priority,timetosend1,timetosend2,name">
                </asp:SqlDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
