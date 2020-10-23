<%@ Page Title="FPC Distribution" Language="vb" MasterPageFile="SiteStar.Master"
    AutoEventWireup="false" CodeBehind="FPC_Distribution.aspx.vb" Inherits="EPG.FPC_Distribution" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-info text-center">
        <div class="panel-heading">
            <h2>
                FPC DISTRIBUTION
            </h2>
        </div>
        <div class="col-lg-12">
            <table class="table table-bordered">
                <tr>
                    <th class="alert-success">
                        Client
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtclient" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtclient" runat="server" ControlToValidate="txtclient"
                            ErrorMessage="Client Name is Mandatory" ForeColor="Red"></asp:RequiredFieldValidator>
                    </th>
                    <th class="alert-success">
                        Feed Type
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlfeedtype" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Select Feed Type--" Value=""></asp:ListItem>
                            <asp:ListItem Text="Regular" Value="Regular"></asp:ListItem>
                            <asp:ListItem Text="Highlights" Value="Highlights"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvfeedtype" runat="server" ControlToValidate="ddlfeedtype"
                            ErrorMessage="Feed Type is Mandatory" ForeColor="Red"></asp:RequiredFieldValidator>
                    </th>
                    <th class="alert-success">
                        Feed Format
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlfeedformat" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Select Feed Format--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="XML" Value="XML"></asp:ListItem>
                            <asp:ListItem Text="XLS" Value="XLS"></asp:ListItem>
							<asp:ListItem Text="XLSX" Value="XLSX"></asp:ListItem>
                            <asp:ListItem Text="XLS-Template1" Value="XLS-Template1"></asp:ListItem>
                            <asp:ListItem Text="XLS-Template2" Value="XLS-Template2"></asp:ListItem>
                            <asp:ListItem Text="XLS-Template3" Value="XLS-Template3"></asp:ListItem>
                            <asp:ListItem Text="JSON" Value="JSON"></asp:ListItem>
                            <asp:ListItem Text="TXT" Value="TXT"></asp:ListItem>
                            <asp:ListItem Text="CSV" Value="CSV"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                    <th class="alert-success">
                        Main Tz
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlTZ" runat="server" CssClass="form-control" DataSourceID="sqlDSTZ"
                            DataTextField="timezonedesc" DataValueField="timezone" />
                        <asp:RequiredFieldValidator ID="rfvTZ" runat="server" ControlToValidate="ddlTZ" ErrorMessage="TimeZone is Mandatory"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        Field Mapping
                    </th>
                    <th colspan="3">
                        <asp:TextBox CssClass="form-control" ID="txtfieldmapping" runat="server" TextMode="MultiLine"
                            Rows="4"></asp:TextBox>
                    </th>
                    <th class="alert-success">
                        Single File
                    </th>
                    <td>
                        <asp:CheckBox ID="chkSingleFile" runat="server" Checked="true" />
                    </td>
                    <th class="alert-success">
                        Sheet Type
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlSheetType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Single" Value="single"></asp:ListItem>
                            <asp:ListItem Text="Daily" Value="daily"></asp:ListItem>
                            <asp:ListItem Text="Weekly" Value="weekly"></asp:ListItem>
                            <asp:ListItem Text="Weekly-Daily" Value="weekly-daily"></asp:ListItem>
                        </asp:DropDownList>
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        Master Frequency
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlfrequency" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Select Master Frequency--" Value="0"></asp:ListItem>
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
                            <%--<asp:ListItem Text="Daily" Value="daily"></asp:ListItem>
                            <asp:ListItem Text="Weekly" Value="weekly"></asp:ListItem>--%>
                            <%--<asp:ListItem Text="Weekly (Sheet)" Value="Weekly-sheet"></asp:ListItem>--%>
                            <%--<asp:ListItem Text="Monthly" Value="monthly"></asp:ListItem>--%>
                            <%--<asp:ListItem Text="Monthly (Sheet)" Value="Monthly-sheet"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvfrequency" runat="server" ControlToValidate="ddlfrequency"
                            ErrorMessage="Frequency is Mandatory" ForeColor="Red" InitialValue="0" />
                    </th>
                    <th class="alert-success">
                        Update Frequency
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlSubFrequency" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Select Updates Frequency--" Value=""></asp:ListItem>
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
                    </th>
                    <th class="alert-success">
                        Master Time
                    </th>
                    <th>
                        <asp:TextBox ID="txtMasterTime" runat="server" CssClass="form-control" placeholder="hh24:mi | separated" />
                        <asp:RequiredFieldValidator ID="rfvMasterTime" runat="server" ControlToValidate="txtMasterTime"
                            ErrorMessage="* MasterTime" ForeColor="Red"></asp:RequiredFieldValidator>
                    </th>
                    <th class="alert-success">
                        Days
                    </th>
                    <th>
                        <asp:CheckBoxList ID="chkWeekList" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Sun" Selected="True">Sun</asp:ListItem>
                            <asp:ListItem Value="Mon" Selected="True">Mon</asp:ListItem>
                            <asp:ListItem Value="Tue" Selected="True">&nbsp;Tue&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Wed" Selected="True">&nbsp;Wed&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Thu" Selected="True">&nbsp;Thu&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Fri" Selected="True">&nbsp;Fri&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Sat" Selected="True">&nbsp;Sat&nbsp;</asp:ListItem>
                        </asp:CheckBoxList>
                    </th>
                </tr>
                <tr>
                    <th class="alert-warning">
                        Send Mail
                    </th>
                    <th>
                        <asp:CheckBox ID="chkSendMail" runat="server" AutoPostBack="true" />
                    </th>
                    <th class="alert-success">
                        Mail To
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtmailto" runat="server" TextMode="MultiLine"
                            Rows="4" />
                        <asp:RequiredFieldValidator ID="rfvmailto" runat="server" ControlToValidate="txtmailto"
                            ErrorMessage="Mail To is Mandatory" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                    </th>
                    <th class="alert-success">
                        Mail CC
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtmailcc" runat="server" TextMode="MultiLine"
                            Rows="4" />
                    </th>
                    <th class="alert-success">
                        Mail BCC
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtmailbcc" runat="server" TextMode="MultiLine"
                            Rows="4" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        Mail From Name
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtMailFromName" runat="server" />
                    </th>
                    <th class="alert-success">
                        Mail From
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtMailFrom" runat="server" />
                    </th>
                    <th class="alert-warning">
                        Function Name
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtFunctionName" runat="server" />
                    </th>
                    <th class="alert-warning">
                        Disclaimer
                    </th>
                    <th>
                        <asp:CheckBox CssClass="form-control" ID="chkDisclaimer" runat="server" Checked="true" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-warning">
                        Send Ftp
                    </th>
                    <th>
                        <asp:CheckBox ID="chkSendFTP" runat="server" AutoPostBack="true" />
                    </th>
                    <th class="alert-success">
                        FTP IP
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtftpip" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtftpip" runat="server" ControlToValidate="txtftpip"
                            ErrorMessage="FTP IP To is Mandatory" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                    </th>
                    <th class="alert-success">
                        FTP User
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtftpuser" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvftpuser" runat="server" ControlToValidate="txtftpuser"
                            ErrorMessage="FTP User To is Mandatory" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                    </th>
                    <th class="alert-success">
                        FTP Pass
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtftppass" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvftppass" runat="server" ControlToValidate="txtftppass"
                            ErrorMessage="FTP Pass To is Mandatory" ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        FTP Fingerprint
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtfingerprint" runat="server"></asp:TextBox>
                    </th>
                    <th class="alert-success">
                        Ftp Port
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtftpport" runat="server"></asp:TextBox>
                    </th>
                    <th class="alert-success">
                        FTP Dir
                    </th>
                    <th>
                        <asp:TextBox CssClass="form-control" ID="txtFtpDir" runat="server"></asp:TextBox>
                    </th>
                </tr>
                <tr>
                    <th class="alert-warning">
                        Master File
                    </th>
                    <th>
                        <asp:CheckBox ID="chkMasterFile" runat="server" Checked="true" />
                    </th>
                    <th class="alert-warning">
                        Updates
                    </th>
                    <th>
                        <asp:CheckBox ID="chkUpdates" runat="server" Checked="true" />
                    </th>
                    <th class="alert-success">
                        ZIP
                    </th>
                    <th>
                        <asp:CheckBox ID="chkZip" runat="server" Checked="true" />
                    </th>
                    <th class="alert-success">
                        ZIP Upload
                    </th>
                    <th>
                        <asp:CheckBox ID="chkZipUpload" runat="server" Checked="true" />
                    </th>
                </tr>
                <tr>
                    <th class="alert-success">
                        FPC Backup
                    </th>
                    <th>
                        <asp:CheckBox ID="chkFpcBkp" runat="server" Checked="true" />
                    </th>
                    <th class="alert-warning">
                        Active
                    </th>
                    <th>
                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                    </th>
                    <th class="alert-warning">
                        Distribution Market
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlMarket" runat="server" CssClass="form-control">
                            <asp:ListItem Text="" Value="" />
                            <asp:ListItem Text="Domestic" Value="Domestic" />
                            <asp:ListItem Text="International" Value="International" />
                        </asp:DropDownList>
                    </th>
                    <th>
                        <center>
                            <asp:Button ID="btnSaveMe" runat="server" Text="SAVE" CssClass="btn alert-success" />
                            <asp:Label ID="lbID" runat="server" Visible="false" />
                        </center>
                    </th>
                </tr>
                <tr>
                    <th colspan="6">
                        &nbsp;
                    </th>
                    <th>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="search client" />
                    </th>
                    <th>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn alert-info"
                            UseSubmitBehavior="false" CausesValidation="false" />
                    </th>
                </tr>
            </table>
        </div>
        <div class="panel-body">
            <asp:GridView ID="grd" runat="server" CssClass="table" BackColor="White" BorderColor="#DEDFDE"
                AllowSorting="true" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Vertical" DataSourceID="sqlDS">
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
                            <asp:CheckBox ID="chkActiveGrd" runat="server" Checked='<%#Bind("active") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlDS" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select id,active,client,feedtype,feedformat,market,sendmail,sendftp,masterfile,updates,maintz,masterday,mastertime,singlefile,sheettype,frequency,subfrequency,fieldmapping,mailto,mailcc,mailbcc,ftpip,ftpuser,ftppass,ftpfingerprint,ftpport,zipupload,zip,ftpdir,fpcbackup,lastupdate from fpc_distribution where (client like '%' + @search + '%' or mailto like '%' + @search + '%' or mailcc like '%' + @search + '%' or mailbcc like '%' + @search + '%' or feedformat like '%' + @search + '%') order by active desc,client">
        <SelectParameters>
            <asp:ControlParameter Name="search" ControlID="txtSearch" PropertyName="Text" ConvertEmptyStringToNull="false" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDStZ" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>"
        SelectCommand="select timezone,timezonedesc from fpc_timezone order by 1" />
</asp:Content>
