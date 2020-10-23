<%@ Page Title="File Upload" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="UploadChannelLogos.aspx.vb" Inherits="EPG.UploadChannelLogos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <script type="text/javascript">
       function clientMessage() {
           document.getElementById("label1").innerHTML = "File Uploaded.";
       }
	   </script>
        
        <link href="lightbox.css" rel="stylesheet" type="text/css" />
        <script src="lightbox.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
    <fieldset>
            <legend>Channel Logos</legend>
            <div style="text-align:center">
                  <table width="80%">
                     <tr>
                        <th width="15%" valign="top" align="right">
                           Select Channel
                        </th>
                        <td width="35%"  valign="top" align="left">
                           <asp:ComboBox ID="ddlChannelName" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append"  Width="200px"
                              DataSourceID="SqlDSChannel" DataTextField="Channelid" 
                              DataValueField="Channelid" AutoPostBack="False">
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlDSChannel" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT Channelid FROM mst_channel where active=1 order by channelid">                                
                           </asp:SqlDataSource>
                           <br />
                        </td>
                     
                        <td rowspan="2" valign="top" width="10%">
                        <asp:HyperLink rel="lightbox" ID="hyChannelLogo" runat="server">
                            <asp:Image ID="imgChannellogo" Visible="true" Width="100px" Height="100px" runat="server" AlternateText="NDTV" /><br />
                            <asp:Label ID="lbImageSize" runat="server" />
                        </asp:HyperLink></td></tr><tr>
                     <th align="right">Upload Image</th><td align="left" valign="bottom">
                     <%--OnUploadedComplete="doUpload"--%>
                        <asp:AsyncFileUpload ID="AjaxFileUpload11" runat="server"
		                    UploadingBackColor="#82CAFA"
		                    CompleteBackColor = "#FFFFFF"
		                    Filter="*.jpg;*.bmp;*.gif;*.png|Supported Images Types (*.jpg;*.bmp;*.gif;*.png)"
		                    OnClientUploadComplete="clientMessage"
		                    ThrobberID="Throbber"
                            ProgressInterval="50"
                            SaveBufferSize="128"
                            Width="200px"
                            EnableProgress="OnSubmit, OnPreview"
                              />
		                
                        <asp:Image ID="Throbber" runat="server"
		                    ImageUrl = "~/Images/loader.gif"/>
                            
                            <strong><font style="color:Green"><div id="label1"></div></font></strong>
                            
                            <span style="color:Red">Only .jpg, .jpeg & .png files supported</span> </td><td>
                        <asp:Label 
                                  ID="lbStatus" runat="server" style="color: #FF0000" ></asp:Label></td><td align="left">
                        <asp:Button ID="btnUpload" runat="server" Text="UPDATE" />
                        &nbsp;&nbsp; <asp:Button ID="btnRemove" runat="server" Text="Remove Image" />
                    </td>
                    
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:DataList ID="grdChannelLogo" runat="server" 
                          DataSourceID="sqlDSProgImage" RepeatColumns="3" 
                          RepeatDirection="Horizontal" >
                          <ItemTemplate>
                                <table border="1" cellpadding="10px" cellspacing="0"><tr>
                                    <td width="200px" align="left">
                                        <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("channelid") %>' />        
                                    </td>
                                    <td align="left">
                                        <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                        <asp:Image runat="server" ID="imglogo"  AlternateText="NDTV" Width="50px" Height="50px" />
                                        </asp:HyperLink></td></tr></table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
							<table border="1" cellpadding="10px" cellspacing="0">
								<tr>
                                    <th width="200px" align="left">
                                        <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("channelid") %>' />        
                                    </th>
                                    <td align="left">
                                        <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                        <asp:Image runat="server" ID="imglogo"  AlternateText="NDTV" Width="50px" Height="50px" />
                                        </asp:HyperLink>
									</td>
								</tr></table>
                        </AlternatingItemTemplate>
                                
                                        </asp:DataList><%--<asp:GridView ID="grdProgImage" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sqlDSProgImage" AllowPaging="True" AllowSorting="True" 
                        CellPadding="4" ForeColor="#333333" GridLines="vertical" PageSize="200">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Channel Id" SortExpression="channelid">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbChannelId" Text='<%# Bind("channelid") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:HyperLink rel="lightbox" ID="hyLogo" runat="server">
                                        <asp:Image runat="server" ID="imglogo"  AlternateText="NDTV" Width="50px" Height="50px" />
                                    </asp:HyperLink></ItemTemplate></asp:TemplateField><asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Images/Edit.png"/>
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
                    </asp:GridView>--%><asp:SqlDataSource ID="sqlDSProgImage" runat="server" SelectCommand="select  channelid from mst_channel where Onair=1 and companyid<>28 order by channelid" SelectCommandType="Text"
                        ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" >
                              <SelectParameters>
                                 <asp:ControlParameter ControlID="ddlChannelName" Name="channelid" PropertyName="SelectedValue" Type="String" />
                              </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
            </div>
    </fieldset>
        </asp:Content>