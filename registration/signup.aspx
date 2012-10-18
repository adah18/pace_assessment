<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="PAOnlineAssessment.registration.signup" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>
<%@ Register assembly="MSCaptcha" namespace="MSCaptcha" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Registration - Pace Academy Online Assessment System</title>
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
                    <td colspan="2">
                        &nbsp;</td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <span class="PageSubHeader" lang="en-ph">Student Number </span>
                        <asp:Label ID="vlStudentNumber" runat="server" CssClass="ValidationNotice" Text=""></asp:Label>
                        </td>
                </tr>
                <tr>
                    <td class="style16" colspan="2">
                        <asp:TextBox ID="txtStudentNumber" runat="server" CssClass="TextboxTemplate" 
                            Width="250px"></asp:TextBox>
                    </td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span class="LoginSubNote" lang="en-ph">Please Enter your Student Number 
                        (Optional)</span></td>
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
                            Text=""></asp:Label>
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
                    <td colspan="3">
                        <span class="LoginSubNote" lang="en-ph">Please Enter a Valid Email Address for 
                        verification (Optional)</span></td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:UpdatePanel ID="upCaptcha" runat="server" Visible="false">
                            <ContentTemplate>
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
                                            <cc1:CaptchaControl ID="ccRegister" runat="server" Width="75px" 
                                                CacheStrategy="Session" CaptchaBackgroundNoise="None" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCaptcha" runat="server" Width="250px" 
                                                CssClass="TextboxTemplate"></asp:TextBox>
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
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                            onclick="lnkRegister_Click" 
                            onclientclick="this.value='Submitting...';  return true; this.disabled=true;" 
                            Text="Register" Width="88px" />
                    </td>
                </tr>
                <tr>
                    <td class="style17" colspan="3">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        &nbsp;
        </div>
       <uc3:frmFooter ID="frmFooter1" runat="server" />    
    </form>
</body>
</html>
