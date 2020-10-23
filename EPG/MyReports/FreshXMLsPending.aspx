<%@ Page Title="Fresh XMLs Pending" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/SiteEPGBootstrap.Master" CodeBehind="FreshXMLsPending.aspx.vb"
    Inherits="EPG.FreshXMLsPending" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        if (inputList[i].disabled == true) {
                            
                            row.style.backgroundColor = "red";
                            inputList[i].checked = false;
                        }
                        else {
                            row.style.backgroundColor = "aqua";
                            inputList[i].checked = true;
                        }
                        
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "aqua";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#C2D69B";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading text-center">
                <h3>
                    Fresh XMLs Pending</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <asp:Button ID="btnGenerateRequest" runat="server" Text="Generate" CssClass="btn btn-info" />
                    <asp:GridView ID="grdReport" CssClass="table" runat="server" ShowFooter="True" DataSourceID="sqlDSReport"
                        CellPadding="4" EmptyDataText="No record found !" ForeColor="Black" GridLines="Vertical"
                        Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" PagerSettings-NextPageImageUrl="~/Images/next.jpg"
                        PagerSettings-PreviousPageImageUrl="~/Images/Prev.gif" Font-Names="Verdana" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#CCCC99" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" Font-Size="Larger" ForeColor="White" />
                        <PagerSettings NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/Prev.gif">
                        </PagerSettings>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkChecked" runat="server" onclick="Check_Click(this)" />
                                    <asp:Label ID="lbChannelId" runat="server" Text='<%# Eval("ChannelID") %>' Visible="false"/>
                                    <asp:Label ID="lbTotalCount" runat="server" Text='<%# Eval("TotalCount") %>' Visible="false"/>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sqlDSReport" runat="server" SelectCommand="select *, TotalCount=(select count(*) from vw_php_job_mail_xml_alert where convert(datetime, epgavailabletill)> convert(datetime, xmldatetime)) from vw_php_job_mail_xml_alert where convert(datetime, epgavailabletill)> convert(datetime, xmldatetime) order by 1"
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" />
                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select *, TotalCount=(select count(*) from vw_php_job_mail_xml_alert where convert(datetime, epgavailabletill)> convert(datetime, xmldatetime)) from vw_php_job_mail_xml_alert where convert(datetime, epgavailabletill)> convert(datetime, xmldatetime) order by 1"
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1%>" />--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
