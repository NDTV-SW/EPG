<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="SiteAccount.Master"
    CodeBehind="login.aspx.vb" Inherits="EPG.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4 col-sm-12 col-xs-12">
            <div class="form-signin">
                <h2 class="form-signin-heading">
                    Please sign in</h2>
                <label for="txtinputEmail" class="sr-only">
                    Email address</label>
                <asp:TextBox ID="txtInputEmail" runat="server" AutoCompleteType="email" class="form-control"
                    placeholder="User Name" required autofocus />
                <label for="txtInputPassword" class="sr-only">
                    Password</label>
                <asp:TextBox ID="txtInputPassword" runat="server" type="password" class="form-control"
                    placeholder="Password" required />
                <asp:Label ID="lbStatus" runat="server" Visible="False" CssClass="btn alert-danger" />
                <asp:Button ID="btnLogin" runat="server" class="btn btn-lg btn-primary btn-block"
                    Text="Sign in" UseSubmitBehavior="true" />
            </div>
            <asp:HyperLink ID="hy" runat="server" Text="Forgot Password!" NavigateUrl="forgotpassword.aspx" />
        </div>
    </div>
</asp:Content>
