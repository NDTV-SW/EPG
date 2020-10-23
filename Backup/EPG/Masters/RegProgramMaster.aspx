<%@ Page Title="Programme Regional Names" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="RegProgramMaster.aspx.vb" Inherits="EPG.RegProgramMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   <script type="text/javascript">
       $(document).ready(function () {
           $('#ddlChannelName').bind("keyup", function (e) {
               var code = (e.keyCode ? e.keyCode : e.which);
               if (code == 13) {
                   $('#txtRegionalName').focus();
               }

           });
       });
   </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <asp:ScriptManager ID="ScriptManager1" runat="server"/>
   <asp:UpdatePanel ID="updPanelRegProgramMaster" runat="server">
      <ContentTemplate>
         <fieldset>
            <legend>Programme Regional Names</legend>
                  <table class="CSSTableGenerator">
                     <tr>
                        <th width="15%">
                           Select Channel
                        </th>
                        <td width="35%">
                           <asp:ComboBox ID="ddlChannelName" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append"  Width="200px"
                              DataSourceID="SqlDSChannel" DataTextField="Channelid" 
                              DataValueField="Channelid" AutoPostBack="True">
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlDSChannel" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT Channelid FROM mst_channel where active=1 order by channelid">                                
                           </asp:SqlDataSource>
                           <br />
                        </td>
                     
                        <th width="15%">
                           Programme Name
                        </th>
                        <td width="35%">
                           <asp:ComboBox ID="ddlRegionalProgram" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append" Width="200px" 
                              DataSourceID="SqlmstProgram" DataTextField="ProgName" 
                              DataValueField="ProgId" AutoPostBack="True">
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlmstProgram" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT ProgId, ProgName FROM mst_Program where channelid=@channelid and active='1' ORDER BY ProgName">
                              <SelectParameters>
                                 <asp:ControlParameter ControlID="ddlChannelName" Name="channelid" PropertyName="SelectedValue" Type="String" />
                              </SelectParameters>
                           </asp:SqlDataSource>
                           <br />
                        </td>
                     </tr>
                     <tr>
                        <th>
                           Language
                        </th>
                        <td>
                           <asp:ComboBox ID="ddlLanguage" runat="server" AutoCompleteMode="SuggestAppend"
                              DropDownStyle="DropDownList" ItemInsertLocation="Append" Width="200px">
                           </asp:ComboBox>
                           <asp:SqlDataSource ID="SqlmstLanguage" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT LanguageId, FullName FROM mst_Language where active='1' ORDER BY FullName">                                
                           </asp:SqlDataSource>
                           <br />
                        </td>
                        <th>
                           Programme Regional Name
                        </th>
                        <td>
                           <asp:TextBox ID="txtRegionalName" runat="server" Width="250px" MaxLength="150"></asp:TextBox>
                           <asp:TextBoxWatermarkExtender TargetControlID="txtRegionalName" ID="ExttxtRegionalName" WatermarkText="Enter Programme Regional Name" runat="server"></asp:TextBoxWatermarkExtender>
                           <br />
                           <asp:RequiredFieldValidator ID="RFVtxtRegionalName" ControlToValidate="txtRegionalName" runat="server" ForeColor="Red" Text="* (Programme Regional name can not be left blank.)" ValidationGroup="RFGRegionalName"></asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <th>
                           Episode no.
                        </th>
                        <td>
                           <asp:TextBox ID="txtEpisodeNo" Width="200px" runat="server" MaxLength="4"></asp:TextBox>
                        </td>
                        <th>
                           Programme Synopsis
                        </th>
                        <td>
                           <asp:TextBox ID="txtRegionalDescription" runat="server" Width="350px" MaxLength="200" TextMode="MultiLine" Height="80"></asp:TextBox>
                           <asp:TextBoxWatermarkExtender TargetControlID="txtRegionalDescription" ID="ExttxtRegionalDescription" WatermarkText="Enter Programme Regional Description" runat="server"></asp:TextBoxWatermarkExtender>
                           
                        </td>
                     </tr>
                     <tr>
                        <td colspan="4">
                            <div class="buttonCenter">
                               <asp:TextBox ID="txtHiddenId1" runat="server" Visible="False" Text="0"></asp:TextBox>
                               <asp:Button ID="btnAddRegionalName" runat="server" Text="Add" ValidationGroup="RFGRegionalName" UseSubmitBehavior="false" Width="100px"/>
                               &nbsp;&nbsp;
                               <asp:Button ID="btnCancel1" runat="server" Text="Cancel" Width="100px"/>
                           </div>
                        </td>
                     </tr>
                  </table>
                  <table class="CSSTableGenerator">
                     <tr>
                        <td>
                           <asp:GridView ID="grdRegionalNames" runat="server" AutoGenerateColumns="False" 
                              CellPadding="4" DataKeyNames="RowID" DataSourceID="sqlDSRegionalMaster" 
                              ForeColor="#333333" GridLines="Vertical" Width="100%" AllowSorting="True" SortedAscendingHeaderStyle-CssClass="sortasc-header"
                              SortedDescendingHeaderStyle-CssClass="sortdesc-header" AllowPaging="true" PageSize="20">
                              <Columns>
                                 <asp:TemplateField HeaderText="RowId" SortExpression="RowId" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbRowId" Text='<%# Bind("RowId") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ProgId" SortExpression="ProgId" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbProgId" Text='<%# Bind("ProgId") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Languageid" SortExpression="Languageid" Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbLanguageid" Text='<%# Bind("Languageid") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ProgName" SortExpression="ProgName">
                                    <ItemTemplate>
                                       <asp:Label ID="lbProgName" Text='<%# Bind("ProgName") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Synopsis" SortExpression="Synopsis">
                                    <ItemTemplate>
                                       <asp:Label ID="lbSynopsis" Text='<%# Bind("Synopsis") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Language" SortExpression="Fullname">
                                    <ItemTemplate>
                                       <asp:Label ID="lbFullname" Text='<%# Bind("Fullname") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 

                                 <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/Images/Edit.png"/>
                                 <asp:CommandField ShowDeleteButton="True" ButtonType="Image"  DeleteImageUrl="~/Images/delete.png"/>
                                 <asp:TemplateField HeaderText="Episode no." SortExpression="EpisodeNo">
                                    <ItemTemplate>
                                       <asp:Label ID="lbEpisodeNo" Text='<%# Bind("EpisodeNo") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                
                              </Columns>                              
                           </asp:GridView>
                           <asp:SqlDataSource ID="sqlDSRegionalMaster" runat="server" 
                              ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>" 
                              SelectCommand="SELECT a.RowID, a.progid, b.ChannelId, a.progName, c.LanguageId, c.FullName, a.synopsis, a.episodeno FROM mst_ProgramRegional a join mst_Program b on a.progid = b.ProgID join mst_Language c on a.LanguageId=c.LanguageId where b.channelid=@ChannelId and a.progid=@ProgId order by episodeno"
                              DeleteCommand="sp_mst_ProgramRegional" DeleteCommandType="StoredProcedure">
                              <SelectParameters>
                                 <asp:ControlParameter ControlID="ddlChannelName" Name="ChannelId" PropertyName="SelectedValue" Type="String" />
                                 <asp:ControlParameter ControlID="ddlRegionalProgram" Name="ProgId" PropertyName="SelectedValue" Type="Int32" />
                              </SelectParameters>
                           </asp:SqlDataSource>
                        </td>
                     </tr>
                  </table>
         </fieldset>     
      </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>

