<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup_parent.aspx.cs" Inherits="PAOnlineAssessment.registration.signup_rparent" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>
<%@ Register assembly="MSCaptcha" namespace="MSCaptcha" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style6
        {
            width: 73%;
        }
        .style9
        {
            width: 263px;
        }
        .style11
        {
            width: 161px;
        }
        .style12
        {
            color: #7C7900;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: bold;
            vertical-align: middle;
            font-variant: small-caps;
            width: 77px;
        }
        .style13
        {
            color: #7C7900;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: bold;
            vertical-align: middle;
            font-variant: small-caps;
            width: 77px;
            height: 20px;
        }
        .style14
        {
            width: 161px;
            height: 20px;
        }
        .style15
        {
            width: 263px;
            height: 20px;
        }
        .style16
        {
            color: #7C7900;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: bold;
            vertical-align: middle;
            font-variant: small-caps;
            height: 20px;
        }
        .style17
        {
            padding: 0px;
            margin: 0px;
            color: #666666;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 9px;
            font-weight: bold;
            vertical-align: middle;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:frmHeader ID="frmHeader1" runat="server" />    
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            <table class="style6">
                <tr>
                    <td colspan="3">
                        <span lang="en-ph"><span class="PageHeader">Register
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        </span></span></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <span class="LoginSubNote" lang="en-ph">Sign Up for an Account</span></td>
                </tr>
                <tr>
                    <td colspan="3" 
                        style="background-position: center; background-image: url('../../images/separator.jpg'); background-repeat: repeat-x">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <span lang="en-ph"><span class="PageSubHeader">First Name</span> </span>
                        <asp:Label ID="vlFirstName" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextboxTemplate" 
                            Width="250px"></asp:TextBox>
                    </td>
                    <td class="style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span class="LoginSubNote" lang="en-ph">Please Enter your First Name</span></td>
                    <td class="style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style12">
                        &nbsp;</td>
                    <td class="style11">
                        &nbsp;</td>
                    <td class="style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <span class="PageSubHeader">
                        <span lang="en-ph">Last Name</span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                        <asp:Label ID="vlLastName" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style16" colspan="2">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="TextboxTemplate" 
                            Width="250px"></asp:TextBox>
                    </td>
                    <td class="style15">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span class="LoginSubNote" lang="en-ph">Please Enter your Last Name</span></td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style13">
                        &nbsp;</td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td colspan="3">
                        <span class="PageSubHeader">
                        <span lang="en-ph">Username</span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                        <asp:Label ID="vlUsername" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style16" colspan="2">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="TextboxTemplate" 
                            Width="250px"></asp:TextBox>
                    </td>
                    <td class="style15">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span class="LoginSubNote" lang="en-ph">Please Enter your Desired Username</span></td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style13">
                        &nbsp;</td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <span class="PageSubHeader">
                        <span lang="en-ph">Password</span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                        <asp:Label ID="vlPassword" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="TextboxTemplate" 
                            Width="250px" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style15">
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="TextboxTemplate" 
                            Width="250px" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="LoginSubNote">
                        <span lang="en-ph">Please Enter your Desired Password</span></td>
                    <td class="style14">
                        &nbsp;</td>
                    <td>
                        <span class="LoginSubNote" lang="en-ph">Re-enter your Desired Password</span></td>
                </tr>
                <tr>
                    <td class="LoginSubNote">
                        &nbsp;</td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <span class="PageSubHeader">
                        <span lang="en-ph">Email Address</span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                        <asp:Label ID="vlEmailAddress" runat="server" CssClass="ValidationNotice" 
                            Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style16" colspan="2">
                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="TextboxTemplate" 
                            Width="250px"></asp:TextBox>
                    </td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span class="LoginSubNote" lang="en-ph">Please Enter a Valid Email Address for 
                        verification</span></td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="LoginSubNote">
                        &nbsp;</td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                        <table>
                                <tr>
                                    <td>
                                        <span lang="en-ph"><span class="PageSubHeader">Student Information</span> </span>
                                        <asp:Label ID="vlStudent" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvChildInfo" runat="server" AutoGenerateColumns="False" 
                                            PageSize="1" onrowdatabound="gvChildInfo_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgAction" runat="server" ImageUrl="~/images/icons/page_tick.gif" Visible="false" />
                                                    <asp:Label ID="lblCount" runat="server" Text='<%# Eval("Count") %>'></asp:Label>    
                                                </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Firstname">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFirstname1" runat="server" AutoPostBack="True" 
                                                        ontextchanged="txtFirstname1_TextChanged" Text='<%# Eval("Firstname") %>' RowCount='<%# Eval("Count") %>'></asp:TextBox>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lastname">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLastname1" runat="server" AutoPostBack="True" 
                                                        ontextchanged="txtLastname1_TextChanged" Text='<%# Eval("Lastname") %>' RowCount='<%# Eval("Count") %>' ></asp:TextBox>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Level">
                                                <ItemTemplate>
                                                <asp:DropDownList ID="cboLevel1" runat="server" Width="150px" AutoPostBack="true" onselectedindexchanged="cboLevel1_SelectedIndexChanged"  RowCount='<%# Eval("Count") %>' ></asp:DropDownList>
                                                    <asp:Label ID="lblLevel" runat="server" Text='<%# Eval("LevelID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                <asp:DropDownList ID="cboSection1" runat="server" Width="150px"  
                                                        RowCount='<%# Eval("Count") %>' AutoPostBack="True" 
                                                        onselectedindexchanged="cboSection1_SelectedIndexChanged" ></asp:DropDownList>
                                                <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SectionID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                         <span class="LoginSubNote" lang="en-ph">Please enter all of your children that is registered in Pace Online Assessment</span>
                                    </td>
                                </tr>
                 
                        </table>
                                                    
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="LoginSubNote">
                        &nbsp;</td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                       <%--
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="PageSubHeader" lang="en-ph">Captcha </span>
                                            <asp:Label ID="vlCaptcha" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cc1:CaptchaControl ID="upCaptcha" runat="server" Width="75px" 
                                                CacheStrategy="Session" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCaptcha" runat="server" Width="250px" 
                                                CssClass="TextboxTemplate" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="LoginSubNote">
                                        <td>
                                            <span lang="en-ph">Please type the text shown on the image</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>--%>
                     
                    </td>
                </tr>
                <tr class="ValidationNotice">
                    <td colspan="3">
                        <asp:Label ID="lblNotification" runat="server" CssClass="ValidationNotice" 
                            Text="Fields marked with an (*) are required."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="LoginSubNote" colspan="3" 
                        style="background-position: center; background-image: url('../../images/separator.jpg'); background-repeat: repeat-x">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17" colspan="3">
                        <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                            BorderStyle="None" CssClass="ButtonTemplate" Height="34px" 
                            onclientclick="this.value='Submitting...';  return true; this.disabled=true;" 
                            Text="Register" Width="88px" onclick="btnSubmit_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style17" colspan="3">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        &nbsp;</div>    
    <uc3:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
