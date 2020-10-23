<%@ Page Title="All Reports" Language="vb" MasterPageFile="Site.Master" AutoEventWireup="false" CodeBehind="ReportsAll.aspx.vb" Inherits="EPG.ReportsAll" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
        <script type="text/javascript">
            function openWin(rowid) {
                window.open("Masters/EditXMLMailDate.aspx?rowid=" + rowid, "XML Mailed On", "width=650,height=330,toolbar=0,left=300,top=250,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
            }
            function openXmlMailed(channelid) {
                window.open("Masters/showXMLMailed.aspx?channelid=" + channelid, "Channel XMl Mailed", "width=950,height=550,toolbar=0,left=150,top=150,scrollbars=no,location=0,menubar=0,resizable=no,status=0");
            }
        </script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server"/>
    <asp:Panel ID="pnlFeature" runat="server" Visible="True">
        <asp:TabContainer ID="TabContainer1" runat="server" AutoPostBack="False" >            
            <asp:TabPanel runat="server"  ID="TabBuildEPGTransactions" HeaderText="Build EPG Transactions">
                <ContentTemplate>
                    <asp:Table ID="Table1" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
                                <asp:Table ID="tblProgramGrid" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                                    <asp:TableRow>
                    <asp:TableHeaderCell>Select Channel</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:ComboBox ID="ddlChannelId" runat="server"   AutoCompleteMode="SuggestAppend"
                           DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="true" Width="180px">
                        </asp:ComboBox>
                        <asp:SqlDataSource ID="SqlDsChannelMaster" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                           SelectCommand="select ChannelID from mst_Channel where active=1 and SendEPG=1 ORDER BY ChannelId">
                        </asp:SqlDataSource>
                    </asp:TableCell>
                </asp:TableRow>
                                    <asp:TableRow>
                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="2">
                     <asp:GridView ID="grdEPGTransactions" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                        EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" CellPadding="4" HorizontalAlign="Center"
                        ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                        SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                           <asp:BoundField DataField="ChannelID" HeaderText="ChannelID"/>
                           <asp:BoundField DataField="EPGDate1" HeaderText="EPGDate"/>
                           <asp:BoundField DataField="LastUpdate1" HeaderText="LastUpdate"/>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#D7E2F7" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                     </asp:GridView>
                     <asp:SqlDataSource ID="sqlDSProgramDetails" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                        SelectCommand="select channelid,CONVERT(varchar,epgdate,106) epgdate1,epgdate,CONVERT(varchar,lastupdate,109) lastupdate1,lastupdate from mst_build_epg_transactions where channelid=@ChannelId and CONVERT(varchar,lastupdate,112)>=CONVERT(varchar,dbo.GetLocalDate(),112) order by epgdate desc">
                        <SelectParameters>
                                 <asp:ControlParameter ControlID="ddlChannelId" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                              </SelectParameters>
                     </asp:SqlDataSource>
                  </asp:TableCell>
               </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TABXMLMailed" runat="server"  HeaderText="XML Mailed">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceid="SqlDsXMLMailed" 
            CellPadding="6" ForeColor="#333333" GridLines="Vertical" AllowSorting="true" DataKeyNames="XMLFileName">
            <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="S.No">
                <ItemTemplate>
                    <asp:Label ID="lbSno" runat="server" />
                </ItemTemplate>
                <ControlStyle Width="50px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="RowId" SortExpression="rowid" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lbRowid" runat="server" Text='<%#bind("rowid") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="ChannelID" SortExpression="ChannelID">
                <ItemTemplate>
                    <asp:Label ID="lbChannelID" runat="server" Text='<%#bind("ChannelID") %>' visible="false"/>
                    <asp:HyperLink ID="hyChannelID" Target="_blank" runat="server" Text='<%#bind("ChannelID") %>' ToolTip="Click to see last 10 XML generated and Mailed"/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="XML Generated at" SortExpression="XMLDateTime" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbXMLDateTime" runat="server" Text='<%#bind("XMLDateTime") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Start Date" SortExpression="XMLDateTime">
                <ItemTemplate>
                    <asp:Label ID="lbStartDate" runat="server" Text=""/>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="End Date" SortExpression="XMLDateTime">
                <ItemTemplate>
                    <asp:Label ID="lbEndDate" runat="server" Text=""/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="FileName" SortExpression="XMLFileName" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbXMLFileName" runat="server" Text='<%#bind("XMLFileName") %>' Visible="false"/>
                    <asp:HyperLink ID="hyXMLFileName" Target="_blank" runat="server" Text='<%#bind("XMLFileName") %>' ToolTip="To save XML right click and select (Save Link as)"/>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField HeaderText="FTP Date" SortExpression="FTPDateTime" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbFTPDateTime" runat="server" Text='<%#bind("FTPDateTime") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="XML Generated till MAX" SortExpression="maxdateSend" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbmaxdateSend" runat="server" Text='<%#bind("maxdateSend") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="XML Missing for" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="lbmissingdates" runat="server" Text='<%#bind("missingdates") %>'/>
                </ItemTemplate>
                <ControlStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" SortExpression="" Visible="True">
                <ItemTemplate>
                    <asp:HyperLink ID="hyXMLMailedOn" runat="server" Text='Edit'/>
                </ItemTemplate>
            </asp:TemplateField>
            
             
        </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
                    <asp:SqlDataSource ID="SqlDsXMLMailed" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
        SelectCommand="Select X.Rowid , X.ChannelId, XmlDateTime, XmlFilename,FTPDateTime,
        maxdateSend=(select max(epgdate) from aud_epg_xml_ftp where ChannelId=x.ChannelId  and xmlfilename is not null ) ,
        dbo.fn_missing_dates (X.ChannelID,(select max(epgdate) from aud_epg_xml_ftp where ChannelId=x.ChannelId  and xmlfilename is not null )) missingdates
        From aud_epg_xml_ftp X,
        (Select max(rowid) RowId, ChannelId from aud_epg_xml_ftp where xmlfilename is not null Group by ChannelId) Y 
        where X.ChannelId = Y.ChannelId And X.RowId = Y.RowId 
        and xmlfilename is not null 
        and X.ChannelId in (Select ChannelId from mst_channel where onair = 1)
        order by X.Rowid desc" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TABErrorReport" runat="server" HeaderText="Error Report">
                <ContentTemplate>
                    <asp:Table ID="Table2" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
                                <h2>
               Error Logs
            </h2>
                                     <b>Select Date</b>
                                <asp:TextBox ID="txtErrorDate" runat="server"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender TargetControlID="txtErrorDate" ID="ExttxtErrorDate" WatermarkText="Select Error Date" runat="server"></asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CE_ErrorDate" DefaultView="Days" Format="MM/dd/yyyy"  Enabled="True" PopupButtonID="imgtxtErrorDate" PopupPosition="BottomLeft" runat="server" TargetControlID="txtErrorDate" />
                                <asp:Image ID="imgtxtErrorDate" runat="server" ImageUrl="~/Images/calendar.png" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnView" runat="server" Text="VIEW" Font-Bold="true"/>
                                <br /><br />
                                <asp:Table ID="Table3" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                                            <asp:GridView ID="grdErrorReport" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                                CellPadding="4" ForeColor="#333333" GridLines="Vertical" Width="100%" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                                SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                   <asp:BoundField DataField="ErrorDateTime" HeaderText="TimeStamp" SortExpression="ErrorDateTime1" />
                                                   <asp:BoundField DataField="ErrorPage" HeaderText="Error Page" SortExpression="ErrorPage"/>
                                                   <asp:BoundField DataField="ErrorSource" HeaderText="Source" SortExpression="ErrorSource"/>
                                                   <asp:BoundField DataField="ErrorType" HeaderText="Type" SortExpression="ErrorType" />
                                                   <asp:BoundField DataField="ErrorMessage" HeaderText="Message" SortExpression="ErrorMessage" />
                                                   <asp:BoundField DataField="LoggedInUser" HeaderText="User" SortExpression="LoggedInUser" />
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#D7E2F7" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                             </asp:GridView>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TABFTPReport" runat="server" HeaderText="FTP Report">
                <ContentTemplate>
                    <asp:Table ID="Table4" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
                            <h2>
                               FTP Logs
                            </h2>
                                <b>Select Date</b>
                            <asp:TextBox ID="txtFTPDate" runat="server"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender TargetControlID="txtFTPDate" ID="ExttxtFTPDate" WatermarkText="Select FTP Date" runat="server"></asp:TextBoxWatermarkExtender>
                            <asp:CalendarExtender ID="CE_FTPDate" DefaultView="Days" Format="MM/dd/yyyy"  Enabled="True" PopupButtonID="imgtxtFTPDate" PopupPosition="BottomLeft" runat="server" TargetControlID="txtFTPDate" />
                            <asp:Image ID="imgtxtFTPDate" runat="server" ImageUrl="~/Images/calendar.png" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnFTPView" runat="server" Text="VIEW" Font-Bold="true"/>
                            <br /><br />
                            <asp:Table ID="Table5" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                               <asp:TableRow>
                                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                                     <asp:GridView ID="grdFTPReport" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                        CellPadding="4" ForeColor="#333333" GridLines="Vertical" Width="100%" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                        SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                           <asp:BoundField DataField="Channelid" HeaderText="Channel" SortExpression="Channelid" />
                                           <asp:BoundField DataField="Filename" HeaderText="File" SortExpression="Filename"/>
                                           <asp:BoundField DataField="ftpdate1" HeaderText="FTP Time" SortExpression="ftpdate"/>
                                           <asp:BoundField DataField="LoggedInUser" HeaderText="User" SortExpression="LoggedInUser" />
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#D7E2F7" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                     </asp:GridView>
                                  </asp:TableCell>
                               </asp:TableRow>
                            </asp:Table>
                         </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TABEPGMissingInfo" runat="server" HeaderText="EPG Missing Info">
                <ContentTemplate>
                    <asp:Table ID="Table6" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                      <asp:TableRow>
                         <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
                            <h2>
                               EPG Missing Information
                            </h2>
                            <asp:Table ID="Table7" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                               <asp:TableRow>
                                  <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                                     <asp:GridView ID="grdEPGMissingInfo" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                        CellPadding="4" DataSourceID="sqlDSEPGMissingInfo" 
                                        ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                        SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="50">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                           <asp:BoundField DataField="ProgID" HeaderText="ProgID" SortExpression="PROGID" />
                                           <asp:BoundField DataField="ChannelID" HeaderText="Channel" SortExpression="ChannelID"/>
                                           <asp:BoundField DataField="Progname" HeaderText="Programme" SortExpression="Progname"/>
                                           <asp:BoundField DataField="Synopsis" HeaderText="Synopsis" SortExpression="Synopsis" />
                           
                                           <asp:TemplateField HeaderText="Programme Date" SortExpression="ProgDate" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbProgdate" runat="server" Text='<%# Bind("ProgDate") %>' Visible="true" />
                                                </ItemTemplate>
                                           </asp:TemplateField>
                           
                                           <asp:BoundField DataField="Progtime" HeaderText="Programme Time" SortExpression="Progtime" />
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#D7E2F7" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                     </asp:GridView>
                                     <asp:SqlDataSource ID="sqlDSEPGMissingInfo" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                                        SelectCommand="sp_missing_epg_info" SelectCommandType="StoredProcedure">
                                     </asp:SqlDataSource>
                                  </asp:TableCell>
                               </asp:TableRow>
                            </asp:Table>
                         </asp:TableCell>
                      </asp:TableRow>
                   </asp:Table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabChannelOnairOffAir" runat="server" HeaderText="Channel Onair-OffAir">
                <ContentTemplate>
                    <div>
                        <b>Note:-</b>
                            <h3  style="color:green">Off-Air Channels in Green have EPG data available</h3>
                            <h3  style="color:green">Channels in Red have EPG data available Equal or Less than Tomorrow.</h3>
                            <h3  style="color:green">Channels in Orange have FTP to Airtel as False.</h3>
                        <asp:Table ID="tbChannelOnairOffAir" runat="server" Width="95%">
                            <asp:TableRow>
                                <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="Top">
                                    <fieldset>
                                <legend>On-Air Channels</legend>
                                <asp:GridView ID="grdChannelOnAir" runat="server" CellPadding="4" DataSourceID="sqlDSgrdChannelOnAir"
                                    ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="false">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" Font-Size="Medium" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnOffAir"  Text="Off-Air" CommandName="OffAir" runat="server" BackColor="RosyBrown" Font-Bold="true"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="S.No." Visible="True">
                                            <ItemTemplate>
                                                <asp:Label ID="lbSno" runat="server"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="RowId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="On-Air Channels">
                                            <ItemTemplate>
                                                <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>        

                                        <asp:TemplateField HeaderText="Airtel FTP">
                                            <ItemTemplate>
                                                <asp:Label ID="lbAirtelFTP" runat="server" Text='<%# Bind("AirtelFTP") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>        

                                        <asp:TemplateField HeaderText="EPG Till">
                                            <ItemTemplate>
                                                <asp:Label ID="lbEPGAvailableTill" runat="server" Text='<%# Bind("EPGAvailableTill") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </fieldset>
                                </asp:TableCell>
                                <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="Top">
                                    <fieldset>
                                <legend>Off-Air Channels</legend>
                                <asp:GridView ID="grdChannelOffAir" runat="server" CellPadding="4"  DataSourceID="sqlDSgrdChannelOffAir"
                                    ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="false">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" Font-Size="Medium" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnOnAir"  Text="On-Air" CommandName="OnAir" runat="server" BackColor="LightGreen" Font-Bold="true"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                
                                        <asp:TemplateField HeaderText="S.No." Visible="True">
                                            <ItemTemplate>
                                                <asp:Label ID="lbSno" runat="server"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="RowId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Off-Air Channels">
                                            <ItemTemplate>
                                                <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EPG Till">
                                            <ItemTemplate>
                                                <asp:Label ID="lbEPGAvailableTill" runat="server" Text='<%# Bind("EPGAvailableTill") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbColor" runat="server" Text='<%# Bind("Color") %>'  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </fieldset>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                         
                            <asp:SqlDataSource ID="sqlDSgrdChannelOnAir" runat="server" SelectCommandType="Text"
                            SelectCommand="select x.RowId, x.ChannelId,x.airtelFTP,(select convert(varchar,max(progdate),106) from mst_epg where channelid=x.channelid) EPGAvailableTill from mst_channel x where x.Active=1 and x.Onair=1 order by x.ChannelId" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="sqlDSgrdChannelOffAir" runat="server" SelectCommandType="StoredProcedure"
                            SelectCommand="sp_offair_channels_details" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
    
                            </asp:SqlDataSource>
                            </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TABUpdateServiceID" runat="server" HeaderText="Update Service ID">
                <ContentTemplate>
                          <h2>
                             Update Service ID
                          </h2>
                          <asp:Table ID="tblChannelMaster" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="60%">
                             <asp:TableRow>
                                <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                                   Select Channel
                                </asp:TableHeaderCell>
                                <asp:TableCell HorizontalAlign="Left">
                                   <asp:ComboBox ID="ddlChannel" runat="server"  AutoCompleteMode="SuggestAppend"
                                      DropDownStyle="DropDownList" ItemInsertLocation="Append"
                                      DataSourceID="SqlDsCompanyMaster" DataTextField="ChannelID" 
                                      DataValueField="ChannelID" AutoPostBack="True">
                                   </asp:ComboBox>
                                   <asp:SqlDataSource ID="SqlDsCompanyMaster" runat="server" 
                                      ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                                      SelectCommand="SELECT ChannelID FROM mst_Channel where onair='1' order by ChannelID">
                                   </asp:SqlDataSource>
                                   <br /><br />
                                </asp:TableCell>
                             </asp:TableRow>
                             <asp:TableRow>
                                <asp:TableHeaderCell HorizontalAlign="Right" VerticalAlign="Top">
                                   Service ID
                                </asp:TableHeaderCell>
                                <asp:TableCell HorizontalAlign="Left">
                                   <asp:TextBox ID="txtServiceID" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                                   <asp:TextBoxWatermarkExtender TargetControlID="txtServiceID" ID="ExttxtServiceID" WatermarkText="Enter Service ID" runat="server"></asp:TextBoxWatermarkExtender>
                                   <br />
                                   <asp:RequiredFieldValidator ID="RFVtxtServiceID" ControlToValidate="txtServiceID" runat="server" ForeColor="Red" Text="* (Service ID can not be left blank.)" ValidationGroup="RFGServiceID"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                             </asp:TableRow>
                     
                             <asp:TableRow>
                                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                   <asp:TextBox ID="txtRowId" runat="server" Visible="False"></asp:TextBox>
                                   <asp:TextBox ID="txtChannelID" runat="server" Visible="False"></asp:TextBox>
                                   <asp:Button ID="btnUpdateServiceId" runat="server" Text="Update" ValidationGroup="RFGServiceID" UseSubmitBehavior="false" Visible="false"/>
                                   &nbsp;&nbsp;
                                   <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false"/>
                                </asp:TableCell>
                             </asp:TableRow>
                          </asp:Table>
                          <br />
                          <asp:Table ID="tblServiceID" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="60%">
                             <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Left">
                                   <asp:GridView ID="grdServiceID" runat="server" AutoGenerateColumns="False" 
                                      CellPadding="4" DataKeyNames="ChannelId" DataSourceID="sqlDSServiceID" 
                                      ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                                      SortedDescendingHeaderStyle-CssClass="sortdesc-header">
                                      <AlternatingRowStyle BackColor="White" />
                                      <Columns>
                                         <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="False">
                                            <ItemTemplate>
                                               <asp:Label ID="lbRowId" runat="server" Text='<%# Bind("RowId") %>' Visible="true" />
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ChannelId" SortExpression="ChannelId" Visible="true">
                                            <ItemTemplate>
                                               <asp:Label ID="lbChannelId" runat="server" Text='<%# Bind("ChannelId") %>' Visible="true" />
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ServiceId" SortExpression="ServiceId" Visible="true">
                                            <ItemTemplate>
                                               <asp:Label ID="lbServiceId" runat="server" Text='<%# Bind("ServiceId") %>' Visible="true" />
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/images/Edit.png"/>
                                         <asp:CommandField ShowDeleteButton="False" ButtonType="Image"  DeleteImageUrl="~/images/delete.png"/>
                                      </Columns>
                                      <EditRowStyle BackColor="#2461BF" />
                                      <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                      <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                      <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                      <RowStyle BackColor="#EFF3FB" />
                                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                      <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                      <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                      <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                      <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                   </asp:GridView>
                                   <%--SelectCommand="SELECT a.ChannelId ChannelId,c.Fullname, a.catchupflag,a.active, a.RowId Rowid, a.CompanyId CompanyId, b.CompanyName,a.sendEPG sendEPG FROM mst_Channel a join mst_Company b on a.CompanyId = b.CompanyId join mst_language c on a.ChannelLanguage=c.languageid where a.COMPANYID=@COMPANYID and a.active=1"--%>
                                   <asp:SqlDataSource ID="sqlDSServiceID" runat="server" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                                      SelectCommand="SELECT rowid,ChannelID, ServiceID FROM mst_Channel where onair='1' order by ChannelID">
                                   </asp:SqlDataSource>
                                </asp:TableCell>
                             </asp:TableRow>
                          </asp:Table>
              </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TABModifiedSynopsis" runat="server" HeaderText="Synopsis Modified">
                <ContentTemplate>
                    <asp:Table ID="Table8" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                          <asp:TableRow>
                             <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="95%">
                                <h2>
                                   Synopsis Modified Report
                                </h2>
                                <asp:Table ID="Table9" runat="server" GridLines="both" BorderWidth="2" CellPadding="5" CellSpacing="0" Width="100%">
                                    <asp:TableRow>
                                    <asp:TableHeaderCell>
                                            Select Channel
                                        </asp:TableHeaderCell>
                                        <asp:TableCell>
                                            <asp:ComboBox ID="ddlChannelModSynopsis" runat="server"   AutoCompleteMode="SuggestAppend" DataSourceID="SqlDsChannelMasterModSynopsis"
                                               DataValueField="ChannelId" DataTextField="ChannelId" DropDownStyle="DropDownList" ItemInsertLocation="Append" AutoPostBack="true" Width="180px">
                                            </asp:ComboBox>
                                            <asp:SqlDataSource ID="SqlDsChannelMasterModSynopsis" runat="server" 
                                               ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                                               SelectCommand="select * from mst_Channel where active=1 and SendEPG=1 ORDER BY ChannelId">
                                            </asp:SqlDataSource>
                                        </asp:TableCell>
                                        <asp:TableHeaderCell>
                                            Select Language
                                        </asp:TableHeaderCell>
                                        <asp:TableCell>
                                            <asp:DropDownList ID="ddlLanguage" runat="server" DataSourceID="SqlDSLanguage"
                                                 DataValueField="LanguageId" DataTextField="FullName" AutoPostBack="true" >
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDSLanguage" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                                                SelectCommand="SELECT LanguageId, FullName FROM mst_Language where active='1' ORDER BY FullName">                                
                                            </asp:SqlDataSource>
                                        </asp:TableCell>
                                        <asp:TableHeaderCell>
                                            OnAir / OffAir
                                        </asp:TableHeaderCell>
                                        <asp:TableCell>
                                            <asp:CheckBox ID="chkOnAir" runat="server" Checked="true" AutoPostBack="true" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                    
                                        <asp:TableHeaderCell>
                                            Select Date
                                        </asp:TableHeaderCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtDate" runat="server" Width="150px" Enabled="true" ValidationGroup="RFGViewEPG"></asp:TextBox>
                                            <asp:Image ID="imgDateCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                            <asp:CalendarExtender ID="CE_txtDate" runat="server" TargetControlID="txtDate" PopupButtonID="imgDateCalendar" />
                                            <asp:TextBoxWatermarkExtender TargetControlID="txtDate" ID="ExttxtDate" WatermarkText="Select Start Date" runat="server"></asp:TextBoxWatermarkExtender>
                                        </asp:TableCell>
                                        <asp:TableHeaderCell>
                                            Select Hour
                                        </asp:TableHeaderCell>
                                        <asp:TableCell ColumnSpan="3">
                                            <asp:DropDownList ID="ddlHour" runat="server" AutoPostBack="true">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                <asp:ListItem Value="23">23</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">
                                            <asp:Button ID="Button1" runat="server" Text="View" Font-Bold="True" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                   <asp:TableRow>
                                      <asp:TableCell HorizontalAlign="Left" ColumnSpan="6">
                                      <%--a--%>
                     
                                         <asp:SqlDataSource ID="sqlDSModSynopsis" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                                            SelectCommand="rpt_modified_synopsis" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                               <asp:controlParameter ControlID="ddlChannelModSynopsis" Direction="Input" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                                               <asp:controlParameter ControlID="ddlLanguage" Direction="Input" Name="languageId" PropertyName="SelectedValue" Type="String" />
                                               <asp:controlParameter ControlID="chkOnair" Direction="Input" Name="onair" PropertyName="Checked" Type="Boolean" />
                                               <asp:controlParameter ControlID="txtDate" Direction="Input" Name="date" PropertyName="Text" Type="String" />
                                               <asp:controlParameter ControlID="ddlHour" Direction="Input" Name="hour" PropertyName="SelectedValue" Type="Int16" />
                                            </SelectParameters>
                        
                                         </asp:SqlDataSource>
                                      </asp:TableCell>
                                   </asp:TableRow>
                                </asp:Table>
                             </asp:TableCell>
                          </asp:TableRow>
                    </asp:Table>
                    <asp:GridView ID="grdRepModSynopsis" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                            CellPadding="4" DataSourceID="sqlDSModSynopsis" 
                            ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                            SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="50">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="ChannelI">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelID" runat="server" Text='<%#Eval("ChannelId") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbChannelID" runat="server" Text='<%#Eval("ChannelId") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Program Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProgName" runat="server" Text='<%#Eval("Program Name") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbProgName" runat="server" Text='<%#Eval("Program Name") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Synopsis">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%#Eval("Synopsis") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbSynopsis" runat="server" Text='<%#Eval("Synopsis") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Regional ProgName">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRegProgName" runat="server" Text='<%#Eval("Regional ProgName") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRegProgName" runat="server" Text='<%#Eval("Regional ProgName") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Regional Synopsis">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRegSynopsis" runat="server" Text='<%#Eval("Regional Synopsis") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRegSynopsis" runat="server" Text='<%#Eval("Regional Synopsis") %>' Width="100%" TextMode="MultiLine" Height="50px" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ProgId">
                                    <ItemTemplate>
                                        <asp:Label ID="lbProgId" runat="server" Text='<%#Eval("ProgId") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbProgId" runat="server" Text='<%#Eval("ProgId") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action User">
                                    <ItemTemplate>
                                        <asp:Label ID="lbActionUser" runat="server" Text='<%#Eval("ActionUser") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbActionUser" runat="server" Text='<%#Eval("ActionUser") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbActionDate" runat="server" Text='<%#Eval("ActionDate") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbActionDate" runat="server" Text='<%#Eval("ActionDate") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                           
                                <asp:CommandField ShowEditButton="true" /> 
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#D7E2F7" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                  </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </asp:Panel> 
    
    
     
</asp:Content>

