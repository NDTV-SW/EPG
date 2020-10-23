<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="SiteAccount.Master"
    CodeBehind="reset.aspx.vb" Inherits="EPG.reset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-lg-4 col-lg-offset-4 col-   md-4 col-md-offset-4 col-sm-12 col-xs-12">
            <div class="form-signin">
                <h2 class="form-signin-heading">
                   Reset Password</h2>
                <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="New Password" TextMode="Password" />
                    
                <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" placeholder="Confirm Password" TextMode="Password" />
                
                
                <asp:Button ID="btnReset" runat="server" class="btn btn-lg btn-primary btn-block"
                    Text="Reset Password" UseSubmitBehavior="true" CausesValidation="true" />
            </div>
            <asp:HyperLink ID="hy" runat="server" Text="Return to Login!" NavigateUrl="login.aspx" /> &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="RFV_txtPassword" runat="server" ControlToValidate="txtPassword" ForeColor="Red" Text="*Password Req" />
            <asp:RequiredFieldValidator ID="RFV_txtNewPassword" runat="server" ControlToValidate="txtNewPassword" ForeColor="Red" Text="*Confirm Password Req." />
            <asp:CompareValidator ID="CE_MatchPass" runat="server" ControlToValidate="txtPassword" ControlToCompare="txtNewPassword" Operator="Equal" ForeColor="Red"
            Text="Passwords do not match" />
        </div>
    </div>
</asp:Content>
