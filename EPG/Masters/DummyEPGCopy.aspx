<%@ Page Title="Dummy EPG Copy" Language="vb" MasterPageFile="~/SiteEPGBootStrap.Master"
    AutoEventWireup="false" CodeBehind="DummyEPGCopy.aspx.vb" Inherits="EPG.DummyEPGCopy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function BeginProcess() {
            var iframe = document.createElement("iframe");
            iframe.src = "DummyEPGProcess.aspx?tilldate=" + document.getElementById("MainContent_txtTillDate").value + "&CopyLastWeek=" + document.getElementById("MainContent_chkCopyLastWeek").checked;
            
            iframe.width = "20";
            iframe.height = "20";
            //alert("DummyEPGProcess.aspx?tilldate=" + document.getElementById("MainContent_txtTillDate").value + "&CopyLastWeek=" + document.getElementById("MainContent_chkCopyLastWeek").checked);
            document.body.appendChild(iframe);
        }

        function UpdateProgress(Message, controlid, status) {
            $("#" + controlid).html(Message);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Dummy EPG Copy</h3>
            </div>
            <div class="panel-body">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>
                            Till Date</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtTillDate" placeholder="Start Date" runat="server" CssClass="form-control"
                                ValidationGroup="VG" />
                            <span class="input-group-addon">
                                <asp:Image ID="imgTillDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                <asp:CalendarExtender ID="CE_txtTillDate" runat="server" TargetControlID="txtTillDate"
                                    PopupButtonID="imgTillDateCalendar" />
                                <asp:RequiredFieldValidator ID="RFVtxtTillDate" ControlToValidate="txtTillDate" runat="server"
                                    ForeColor="Red" Text="*" ValidationGroup="VG" />
                            </span>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>
                        Last Week EPG</label>
                    <asp:CheckBox ID="chkCopyLastWeek" runat="server" CssClass="form-control" Checked="false" />
                </div>
                <div class="col-md-3">
                    <br />
                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn alert-info" ValidationGroup="VG" />
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn alert-warning" />&nbsp;&nbsp;
                    <asp:HyperLink ID="hyProcess" runat="server" NavigateUrl="javascript:BeginProcess();"
                        Text="Process All Channels" CssClass="btn btn-info" />&nbsp;&nbsp;
                </div>
                <div class="col-md-3">
                    <br />
                    <label id="lbStatus" class="btn btn-success">
                        Hello</label>
                </div>
                <asp:GridView ID="grd" runat="server" CssClass="table" DataSourceID="sqlDS" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Channel" DataField="channelid" />
                        <asp:BoundField HeaderText="Genre" DataField="genrename" />
                        <asp:BoundField HeaderText="Available_Till" DataField="datetill" />
                        <asp:BoundField HeaderText="Days" DataField="noofdays" />
                        <asp:BoundField HeaderText="Name_of_Programme" DataField="progname" />
                        <asp:BoundField HeaderText="Synopsis" DataField="progsynopsys" />
                        <asp:BoundField HeaderText="Duration" DataField="progduration" />
                        <asp:BoundField HeaderText="Copy_Last_Week" DataField="copylastweek" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:SqlDataSource ID="sqlDS" runat="server" SelectCommandType="Text" SelectCommand="select * from fn_insertdummyepg(@TillDate,@CopyLastWeek) order by genrename,channelid"
            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtTillDate" Name="TillDate" PropertyName="Text" />
                <asp:ControlParameter ControlID="chkCopyLastWeek" Name="CopyLastWeek" PropertyName="Checked" />
                
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
