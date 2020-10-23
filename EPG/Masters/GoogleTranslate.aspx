<%@ Page Title="Google Translate" Language="vb" MasterPageFile="~/SiteEPGBootstrap.Master" AutoEventWireup="false" CodeBehind="GoogleTranslate.aspx.vb" Inherits="EPG.GoogleTranslate" MaintainScrollPositionOnPostback ="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
   <%--<asp:UpdatePanel ID="updProgramMaster" runat="server">
      <ContentTemplate>--%>
   <table>
      <tr>
         <td align="center" valign="top" width="50%">
         <h2>Google Translate</h2>
            <table align="center">
               <tr>
                  <th align="right" colspan="2">
                     Select Language
                  </th>
                  <td colspan="2">
                     <asp:DropDownList ID="ddlLanguage" runat="server"   AutoCompleteMode="SuggestAppend"
                        DropDownStyle="DropDownList" ItemInsertLocation="Append"   AppendDataBoundItems="true" AutoPostBack="false">
                        <%--<asp:ListItem Value="10">Select</asp:ListItem>--%>
                     </asp:DropDownList>
                  </td>
                      <th>
                            English Name
                      </th>
                      <td>
                            <asp:TextBox ID="txtEngName" runat="server" Width="200px" TextMode="MultiLine" />
                      </td>
                      <th>
                            Regional Name
                      </th>
                      <td>
                            <asp:TextBox ID="txtRegName" runat="server" Width="200px" TextMode="MultiLine" />
                      </td>
                  </tr>
                  <tr>
                  <td colspan="8" align="center">
                     <asp:Button ID="btnTranslate" runat="server" Text="Translate" />
                     <%--<asp:Button ID="btnInsert" runat="server" Text="Insert" />--%>
                  </td>

               </tr>
            </table>
         </td>
      </tr>
   </table>
   
</asp:Content>
