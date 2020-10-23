<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="SiteAccount.Master"
    CodeBehind="ChangePassword.aspx.vb" Inherits="EPG.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-lg-4 col-lg-offset-4 col-   md-4 col-md-offset-4 col-sm-12 col-xs-12">
            <div class="form-signin">
                <h2 class="form-signin-heading">
                    Change Password</h2>
                <label for="txtOldPassword" class="sr-only">
                    Email address</label>
                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" class="form-control"
                    placeholder="Old Password" required autofocus />
                <label for="txtNewPassword" class="sr-only">
                    Email address</label>
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" class="form-control"
                    placeholder="New Password" required autofocus />
                <label for="txtConfirmPassword" class="sr-only">
                    Email address</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" class="form-control"
                    placeholder="Confirm Password" required autofocus />
                <asp:CompareValidator ID="CV_txtPassword" runat="server" ControlToValidate="txtNewPassword"
                                    ControlToCompare="txtConfirmPassword" Text="Password does not Match" ForeColor="Red" />
                <asp:Label ID="lbStatus" runat="server" Visible="False" CssClass="btn alert-danger" />
                <asp:Button ID="btnSubmit" runat="server" class="btn btn-lg btn-primary btn-block"
                    Text="Change Password" UseSubmitBehavior="true" CausesValidation="true" />
            </div>
        </div>
    </div>
</asp:Content>
