<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="forgot_password.aspx.cs" Inherits="PAOnlineAssessment.forgot_password" %>
<%@ Register src="frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="frmFooter.ascx" tagname="frmFooter" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Forgot Password - Pace Academy Online Assessment System</title>
    <link href="scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="scripts/styles/Font%20Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            text-align: right;
            width: 260px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">    
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
    <div id="bodytopPan">    
    <table style="width:100%;">
        <tr>
            <td colspan="2">
                <span class="PageHeader" lang="en-ph">Forgot Password</span></td>
        </tr>
        <tr>
            <td colspan="2">
                <span class="LoginSubNote">If you have forgotten your user name and password, we 
                can re-send your login details to your registered email address.&nbsp;&nbsp;
                Please fill in your details below.</span></td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <span class="FieldTitle">E<span lang="en-ph">mail Address</span></span><span 
                    class="FieldTitle" lang="en-ph">: </span></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="TextboxTemplate" 
                    Width="250px"></asp:TextBox>
                <span lang="en-ph">&nbsp;</span><asp:Label ID="vlEmailAddress" runat="server" 
                    CssClass="ValidationNotice" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign=top>
                <span class="LoginSubNote" lang="en-ph">Enter your Email Address used during the 
                Account Registration.</span></td>
        </tr>
        <tr>
            <td colspan="2" valign=top>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblNotification" runat="server" CssClass="ValidationNotice" 
                    Text="Fields marked with an (*) are required."></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-position: center center; background-image: url('images/separator.jpg'); background-repeat: repeat-x;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <span lang="en-ph">&nbsp; </span>
                <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                    BorderStyle="None" CssClass="ButtonTemplate" Height="28px" 
                    onclick="lnkLogin_Click" Text="Submit" Width="88px" 
                    onclientclick="this.value='Submitting...'; this.disabled=true; btnCancel.disabled=true;" 
                    UseSubmitBehavior="False" />
                <span lang="en-ph">&nbsp;</span><asp:Button ID="btnCancel" runat="server" 
                    BackColor="Transparent" BorderStyle="None" CssClass="ButtonTemplate" 
                    Height="28px" Text="Cancel" Width="88px" onclick="btnCancel_Click" 
                    UseSubmitBehavior="False" />
                </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>
    </div>
    <uc4:frmFooter ID="frmFooter1" runat="server" /> 
    </form>
</body>
</html>
