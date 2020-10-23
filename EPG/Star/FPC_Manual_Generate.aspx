<%@ Page Title="FPC Manual Generate" Language="vb" MasterPageFile="SiteStar.Master"
    AutoEventWireup="false" CodeBehind="FPC_Manual_Generate.aspx.vb" Inherits="EPG.FPC_Manual_Generate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info">
        <div class="panel-heading">
            <h2>
                FPC Manual Generate
            </h2>
        </div>
        <div class="panel-body">
            <div class="col-md-2">
                <div class="form-group">
                    <label>
                        Client</label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlClient" DataSourceID="Sdsdd" DataTextField="clientname"
                            DataValueField="id" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                            AutoPostBack="True">
                            <asp:ListItem Text="--Select Client--" Value="" />
                        </asp:DropDownList>
                        <span class="input-group-addon">&nbsp; </span>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label>
                        Channel</label>
                    <div class="input-group">
                        <asp:ListBox ID="ddlchannel" runat="server" DataSourceID="Sdsdd1" DataTextField="serviceid"
                            DataValueField="id" SelectionMode="Multiple" CssClass="form-control" />
                        <span class="input-group-addon">&nbsp; </span>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <label>
                        Master Frequency</label>
                    <div class="input-group">
                        <asp:DropDownList ID="ddlfrequency" runat="server" CssClass="form-control" AutoPostBack="true">
                            <asp:ListItem Text="--Select Master Frequency--" Value=""></asp:ListItem>
                            <asp:ListItem Text="Today" Value="today"></asp:ListItem>
                            <asp:ListItem Text="Tomorrow" Value="tomorrow"></asp:ListItem>
                            <asp:ListItem Text="Next 7 days" Value="7 days"></asp:ListItem>
                            <asp:ListItem Text="Next 14 days" Value="14 days"></asp:ListItem>
                            <asp:ListItem Text="Current Week" Value="current week"></asp:ListItem>
                            <asp:ListItem Text="Next Week" Value="next week"></asp:ListItem>
                            <asp:ListItem Text="Current and Next Week" Value="current and next week"></asp:ListItem>
                            <asp:ListItem Text="Next 30 days" Value="30 days"></asp:ListItem>
                            <asp:ListItem Text="Next 60 days" Value="60 days"></asp:ListItem>
                            <asp:ListItem Text="Current Month" Value="current month"></asp:ListItem>
                            <asp:ListItem Text="Next Month" Value="next month"></asp:ListItem>
                            <asp:ListItem Text="Next to Next Month" Value="next to next month"></asp:ListItem>
                            <asp:ListItem Text="Current And Next Month" Value="current and next month"></asp:ListItem>
                        </asp:DropDownList>
                        <span class="input-group-addon">&nbsp; </span>
                    </div>
                </div>
            </div>
            <div id="divStartDate" runat="server" class="col-md-2">
                <div class="form-group">
                    <label>
                        Start Date</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="DateTimeLocal"
                            Text="2019-08-13T00:00" />
                    </div>
                </div>
            </div>
            <div id="divEndDate" runat="server" class="col-md-2">
                <div class="form-group">
                    <label>
                        End Date</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="DateTimeLocal"
                            Text="2019-08-13T00:00" />
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <br />
                <button type="submit" class="btn btn-info" onclick="javascript:GetSelectedTextValue();">
                    Generate
                </button>
                <asp:Label ID="lbID" runat="server" Visible="False" />
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="Sdsdd" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" SelectCommand="select id,client, client + ' (' + convert(varchar(10),id) + ')'
    clientname from fpc_distribution where active=1 order by active desc, client" />
    <asp:SqlDataSource ID="Sdsdd1" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1
    %>" SelectCommand="select * from fpc_distribution_channels where clientid=@id order
    by channelid">
        <SelectParameters>
            <asp:ControlParameter Name="id" ControlID="ddlClient" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <script type="text/javascript">
        function GetSelectedTextValue() {
            var i;
            var ddlClient = document.getElementById("MainContent_ddlClient");
            var ddlChannel = document.getElementById("MainContent_ddlchannel");
            var ddlfrequency = document.getElementById("MainContent_ddlfrequency");
            var selectedText = ddlClient.options[ddlClient.selectedIndex].innerHTML;
            var selectedValue = ddlClient.value;
            var frequencyText = ddlfrequency.options[ddlfrequency.selectedIndex].innerHTML;
            var frequencyValue = ddlfrequency.value;



            var str = '';
            for (i = 0; i < ddlChannel.length; i++) {
                var selectedText2 = ddlChannel.options[i].value;
                if (ddlChannel.options[i].selected) {
                    if (i == ddlChannel.length - 1) {
                        str = str + selectedText2;
                    }
                    else {
                        str = str + selectedText2 + ',';
                    }
                }
            }
            //console.log('?client' + selectedValue + '&channel=' + str);frequencyValue

            var url = "http://52.76.29.250/epgdistibution/processcustom.py" + "?client=" + selectedValue + "&channel=" + str + "&frequency=" + frequencyValue;
            //alert("'" + frequencyValue + "'");
            if (frequencyValue == "") {

                try {
                    var startdate = document.getElementById("MainContent_txtStartDate").value;
                    var enddate = document.getElementById("MainContent_txtEndDate").value;

                    startdate = startdate.substring(0, startdate.indexOf("T"));
                    enddate = enddate.substring(0, enddate.indexOf("T"));
                }
                catch (ex) {

                }
                //alert(startdate);
                //alert(enddate);

                url = url + "&fromdate=" + startdate + "|" + enddate;
            }
            console.log(url);
            //alert(url);
            window.open(url, "Manual Generate", "width=500,height=300,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
            //alert("Selected Text: " + selectedText + " Value: " + selectedValue + "SelectedText: " + selectedText2);
        }
        
    </script>
</asp:Content>
