<%@ Page Title="Operator Channels" Language="vb" MasterPageFile="SiteDTH.Master"
    AutoEventWireup="false" CodeBehind="OperatorChannels.aspx.vb" Inherits="EPG.DTHOperatorChannels" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">

        function BeginProcess(rowid, gridindex) {
            // Create an iframe.
            var iframe = document.createElement("iframe");


            // Point the iframe to the location of
            //  the long running process.
            var x = document.getElementById("MainContent_GridView1_chkOnair_" + gridindex).checked;
            if (x) {
                //alert("Checked");
                iframe.src = "ProcessTask.aspx?rowid=" + rowid + "&gridindex=" + gridindex + "&checked=1";
            }
            else {
                //alert("Not Checked");
                iframe.src = "ProcessTask.aspx?rowid=" + rowid + "&gridindex=" + gridindex + "&checked=0";
            }

            document.body.appendChild(iframe);

            // Disable the button and blur it.
            document.getElementById('MainContent_GridView1_hySubmit_' + gridindex).disabled = true;
            document.getElementById('MainContent_GridView1_hySubmit_' + gridindex).blur();
        }

        function BeginProcessChannel(rowid, gridindex) {
            // Create an iframe.
            var iframe = document.createElement("iframe");


            // Point the iframe to the location of
            //  the long running process.
            var x = document.getElementById("MainContent_GridView1_txtUpdateChannel_" + gridindex).value;

            iframe.src = "ProcessTask.aspx?rowid=" + rowid + "&gridindex=" + gridindex + "&channelid=" + x;

            document.body.appendChild(iframe);

            // Disable the button and blur it.
            document.getElementById('MainContent_GridView1_hySubmitChannel_' + gridindex).disabled = true;
            document.getElementById('MainContent_GridView1_hySubmitChannel_' + gridindex).blur();
        }

        function UpdateProgress(Message, controlid, status) {
            //alert(Message);
            //alert(controlid);
            $("#" + controlid).html(Message);
            if (status == 3) {
                $("#" + controlid).addClass("btn-danger");
                $("#" + controlid).html("<img src='../images/red.png' />")

            }
            if (status == 2) {
                $("#" + controlid).removeClass("btn-success");
                $("#" + controlid).removeClass("btn-danger");
                $("#" + controlid).html("<img src='../images/blue.png' />")

            }
            if (status == 1) {
                $("#" + controlid).removeClass("btn-danger");
                $("#" + controlid).addClass("btn-success");
                $("#" + controlid).html("<img src='../images/green.png' />")
            }
            if (status == 0) {
                $("#" + controlid).addClass("btn-danger");
                $("#" + controlid).html("<img src='../images/red.png' />")
            }
        }
    </script>
    <style type="text/css">
        #spanonair, #spanchannel
        {
            padding: 0px 0px;
        }
      
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="panel panel-default">
        <div class="panel-heading text-center">
            <h3>
                Operator Channels</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <label>
                            Select Operator</label>
                        <div class="input-group">
                            <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control" AutoPostBack="true" />
                            <span class="input-group-addon"></span>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <label>
                        Select Channel</label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlChannel" runat="server" CssClass="form-control" AutoPostBack="false" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <label>
                        Service Id</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtServiceId" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <label>
                        Operator Channel</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtOperatorChannel" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <label>
                        Channel No.</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtChannelno" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <label>
                        TSID</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtTSID" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                    <label>
                        Frequency</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtFrequency" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-6 col-xs-6">
                    <label>
                        Onair</label>
                    <div class="input-group">
                        <asp:CheckBox ID="chkOnAir" runat="server" CssClass="form-control" Checked="true" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <label>
                        Time Diff</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtTimeDiff" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <br />
                <%--<div class="col-lg-4 col-md-4 col-sm-4 col-xs-4 text-center">
                        <asp:Button ID="btnUpdateOnair" runat="server" Text="Update Onair" CssClass="btn btn-success" />
                    </div>--%>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                    <asp:Label ID="lbID" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        CssClass="table" AutoGenerateColumns="True" AllowSorting="True" BackColor="White"
                        DataSourceID="sqlDSGrid" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" SortExpression="OperatorID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbOperatorID" runat="server" Text='<%# Bind("OperatorID") %>' Visible="true" />
                                    <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("rowid") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/Images/edit.png" />
                            <asp:TemplateField HeaderText="Onair" SortExpression="onair">
                                <ItemTemplate>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkOnair" runat="server" CssClass="form-control" Checked='<%# Bind("onair") %>' />
                                        <span id="spanonair" class="input-group-addon">
                                            <asp:HyperLink ID="hySubmit" runat="server" NavigateUrl="" onclick="BeginProcess(); return false;"><img src="../images/blue.png" /></asp:HyperLink>
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Channels From Master">
                                <ItemTemplate>
                                    <div class="input-group" style="width: 180px">
                                        <asp:TextBox ID="txtUpdateChannel" CssClass="form-control" runat="server" />
                                        <span id="spanchannel" class="input-group-addon">
                                            <asp:HyperLink ID="hySubmitChannel" runat="server" NavigateUrl="" onclick="BeginProcessChannel(); return false;"><img src="../images/blue.png" /></asp:HyperLink>
                                            <asp:AutoCompleteExtender ID="ACE_txtUpdateChannel" runat="server" ServiceMethod="SearchChannel"
                                                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                TargetControlID="txtUpdateChannel" FirstRowSelected="true" UseContextKey="false" />
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDSOperatorList" runat="server" SelectCommand="select operatorid,name + ' (' + case when active=1 then 'Y' else 'N' end +'-' + operatorid + ')' name from mst_dthcableoperators order by active desc,2"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSChannel" runat="server" SelectCommand="select channelid from mst_channel where companyid<>28 order by channelid"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" />
    <asp:SqlDataSource ID="sqlDSGrid" runat="server" SelectCommand="select ChannelID,ServiceID,OperatorChannelID,ChannelNo,TSID,isnull(OnAir,0) onair,Genre=(select genrename from mst_genre where genreid in (select genreid from mst_channel where channelid=x.channelid)),Frequency,epgtimeDiff,RowID from dthcable_channelmapping x where operatorid=@operatorid order by onair desc, channelid"
        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
        <SelectParameters>
            <asp:ControlParameter Name="operatorid" ControlID="ddlOperator" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
