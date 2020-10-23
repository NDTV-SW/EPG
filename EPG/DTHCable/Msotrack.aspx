<%@ Page Title="MSO Tracking" Language="vb" MasterPageFile="SiteDTH.Master" AutoEventWireup="false"
    CodeBehind="Msotrack.aspx.vb" Inherits="EPG._DefaultMso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .noFormatting
        {
            text-decoration: none;
        }
    </style>
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <h1>&nbsp;&nbsp;&nbsp;MSO Tracking System <asp:TextBox ID="txtRowId" runat="server" Visible="false"/></h1>
                
            </div>
        </div>
        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
            &nbsp;
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
        <div class="row">
            
            <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12 ">
                <div class="form-group">
                    <label>Name</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtOperatorName" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <label>Email</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <label>Service Start date</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtServiceStartDate"  runat="server" CssClass="form-control" />
                        <asp:CalendarExtender ID="CE_txtServiceStartDate" TargetControlID="txtServiceStartDate" PopupButtonID="txtServiceStartDate"
                            runat="server" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <label>Billing Per Month</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtBillingPerMonth" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12 ">
                <div class="form-group">
                    <label>City</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <label>Contact Number</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <label>Billing Start date</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtBillingStartDate" runat="server" CssClass="form-control" />
                        <asp:CalendarExtender ID="CE_txtBillingStartDate" TargetControlID="txtBillingStartDate" PopupButtonID="txtBillingStartDate"
                            runat="server" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <label>Remarks</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-info"  /> &nbsp; &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-danger" />
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-6 col-sm-12 col-xs-12 ">
                <div class="form-group">
                    <label>Contact Person</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
                <div class="form-group">
                    <label>&nbsp;</label>
                    <div class="input-group form-control">
                        <asp:CheckBox ID="chkDealSealed" Text="&nbsp;&nbsp;Deal Sealed" runat="server"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkAgreementSigned" Text="&nbsp;&nbsp; Agreement Signed" runat="server" />
                    </div>
                </div> 
                 <div class="form-group">
                    <label>Agreement End date</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtAgreementEndDate" runat="server" CssClass="form-control" />
                        <asp:CalendarExtender ID="CE_txtAgreementEndDate" TargetControlID="txtAgreementEndDate" PopupButtonID="txtAgreementEndDate"
                            runat="server" />
                        <span class="input-group-addon"><span class="glyphicon glyphicon-asterisk"></span></span>
                    </div>
                </div>
            </div>
        </div>
        </div>
        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
            &nbsp;
        </div>
        <div class="row">
            <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 ">
                &nbsp;
            </div>--%>
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 ">
                <asp:GridView ID="grdMSOs" runat="server" GridLines="Vertical" AutoGenerateColumns="False"
                    CssClass="table" DataSourceID="sqlDSMSO" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                    BorderWidth="1px" CellPadding="4" ForeColor="Black" AllowSorting="true">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="../Images/edit.png"/>
                        <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="../Images/delete.png"/>
                        <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <asp:Label ID="lbId" runat="server" Text='<%#Eval("ROWID") %>' Visible="false" />
                                <asp:Label ID="lbSno" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="OperatorName">
                            <ItemTemplate>
                                <asp:Label ID="lbMSOName" runat="server" Text='<%#Eval("OperatorName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City" SortExpression="City">
                            <ItemTemplate>
                                <asp:Label ID="lbMSOCity" runat="server" Text='<%#Eval("CITY") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Person" SortExpression="ContactPerson">
                            <ItemTemplate>
                                <asp:Label ID="lbMSOContactPerson" runat="server" Text='<%#Eval("CONTACTPERSON") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact No." SortExpression="ConatctNumber">
                            <ItemTemplate>
                                <asp:Label ID="lbMSOContactNumber" runat="server" Text='<%#Eval("CONTACTNUMBER") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="ContactEmail">
                            <ItemTemplate>
                                <asp:Label ID="lbMSOContactEmail" runat="server" Text='<%#Eval("CONTACTEMAIL") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deal Sealed" SortExpression="dealsealed">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMSODealSealed" runat="server" Text="" Checked='<%#Eval("dealsealed") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agreement Signed" SortExpression="agreementsigned">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAgreementSigned" runat="server" Text="" Checked='<%#Eval("agreementsigned") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Services Start date" SortExpression="ServicesStartdate">
                            <ItemTemplate>
                                <asp:Label ID="lbServiceStartDate" runat="server" Text='<%#Eval("ServicesStartdate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agreement End Date" SortExpression="AgreementEndDate">
                            <ItemTemplate>
                                <asp:Label ID="lbAgreementEndDate" runat="server" Text='<%#Eval("AgreementEndDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Billing Start Date" SortExpression="BillingStartDate">
                            <ItemTemplate>
                                <asp:Label ID="lbBillingStartDate" runat="server" Text='<%#Eval("BillingStartDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Billing/Month" SortExpression="BillingPerMonth">
                            <ItemTemplate>
                                <asp:Label ID="lbBillingPerMonth" runat="server" Text='<%#Eval("BillingPerMonth") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lbMSORemarks" runat="server" Text='<%#Eval("REMARKS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Updated" SortExpression="LastUpdated">
                            <ItemTemplate>
                                <asp:Label ID="lbLastUpdated" runat="server" Text='<%#Eval("LastUpdated") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                <br /><br /><br />
                <div class="clearfix">
                </div>
            </div>
            <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 ">
                &nbsp;
            </div>
            <asp:SqlDataSource ID="sqlDSMSO" runat="server" SelectCommand="select * from DISTRIBUTIONMSOTRACK order by 9 desc,10 desc"
                SelectCommandType="Text" ConnectionString="<%$ ConnectionStrings:EPGConnectionString1 %>">
            </asp:SqlDataSource>
        </div>
</asp:Content>
