<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="SiteAccount.Master"
    CodeBehind="ForgotPassword.aspx.vb" Inherits="EPG.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-lg-4 col-lg-offset-4 col-   md-4 col-md-offset-4 col-sm-12 col-xs-12">
            <div class="form-signin">
                <h2 class="form-signin-heading">
                    Forgot Password</h2>
                <asp:TextBox ID="txtMailId" runat="server" class="form-control" placeholder="Enter Mail ID" />
                
                <asp:Button ID="btnSubmit" runat="server" class="btn btn-lg btn-primary btn-block"
                    Text="Get Password" UseSubmitBehavior="true" CausesValidation="true" />
            </div>
            <asp:HyperLink ID="hy" runat="server" Text="Return to Login!" NavigateUrl="login.aspx" /> &nbsp;&nbsp;&nbsp;
            <asp:RegularExpressionValidator ID="REV_txtMailId" runat="server" ControlToValidate="txtMailId"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                Text="Invalid Mail-ID" ForeColor="Red" />
            <asp:RequiredFieldValidator ID="RFV_txtMailId" runat="server" ControlToValidate="txtMailId"
                Text="Mail-ID required!" ForeColor="Red" />
            
        </div>
    </div>
</asp:Content>
