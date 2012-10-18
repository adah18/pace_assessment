<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="login.aspx.cs" Inherits="PAOnlineAssessment.login" %>

<%@ Register src="frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="frmFooter.ascx" tagname="frmFooter" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login - Pace Academy Online Assessment System</title>
    <link href="scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="scripts/styles/Font%20Style.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .tdPageHeader
        {
            background-color: #9c9c9c;
            color: #fff;
            font: Century Gothic;
            font-size: 16px;
            font-weight: bold;
            vertical-align:middle;
        }
        
        .style2
        {
            text-align: right;
            width: 289px;
        }
        .login
        {
            width: 290px;
            height: 350px;
            background-color: #f0f0f0;
            border: solid 3px #e0e0e0;
            float: left;
        }
        .image
        {
            width: 588px;
            height: 350px;
            border: solid 3px #e0e0e0;
            margin-left: 10px;
            float: left;
        }
        .news
        {
            width: 895px;
            height: 300px;
            border: solid 3px #e0e0e0;
            float: left;
            margin-top: 10px;
        }
        .style5
        {
            width: 300px;
            background-image: url('images/separator.jpg');
            background-repeat: repeat-x;
            background-position: center;
        }
        .style6
        {
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin" 
    defaultfocus="txtUsername">    
    <uc1:frmHeader ID="frmHeader1" runat="server" />        
    <div id="loginbodytopmainPan">
        <div id="loginbodytopPan">
          <div class="login">   
            <table width="290px" border="0">
                <tr align="center">
                    <td class="tdPageHeader" align="center">
                        <span lang="en-ph">LOGIN</span></td>
                </tr>
            </table>
            <table width="290px" cellpadding="5">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <span class="PageSubHeader">
                        <span lang="en-ph">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Username</span></span><span class="LoginSubHeader" lang="en-ph"> </span><asp:Label ID="vlUsername" runat="server" 
                            CssClass="ValidationNotice" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="TextboxTemplate" 
                            Width="250px"></asp:TextBox>
                        <span lang="en-ph">&nbsp;</span></td>
                </tr>
                <tr>
                    <td align="center">
                        <span class="LoginSubNote" lang="en-ph">Enter your Pace Academy Registration 
                        System Username.</span><div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign=top>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <span class="PageSubHeader">
                        <span lang="en-ph">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Password</span></span><span class="LoginSubHeader" lang="en-ph"> </span><asp:Label ID="vlPassword" runat="server" 
                            CssClass="ValidationNotice" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="TextboxTemplate" 
                            TextMode="Password" Width="250px"></asp:TextBox>
                        <span lang="en-ph">&nbsp;</span></td>
                </tr>
                <tr>
                    <td align="center">
                        <span class="LoginSubNote" lang="en-ph">Enter the Password that accompanies your 
                        Username.</span></td>
                </tr>
                <tr>
                    <td class="style6">
                        </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblNotification" runat="server" CssClass="ValidationNotice" 
                            Text="Fields marked with an (*) are required."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style5" align="center">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:LinkButton ID="lnkForgotPassword" runat="server" 
                            CssClass="LinkButtonTemplate" ToolTip="Retrieve User Login" 
                            onclick="lnkForgotPassword_Click">Forgot your Password?</asp:LinkButton>
                        <span lang="en-ph">&nbsp;&nbsp;<asp:Button ID="btnLogin" 
                            runat="server" BorderStyle="None" CssClass="ButtonTemplate" Height="28px" 
                            onclick="btnLogin_Click" Text="Login" Width="88px" 
                            ToolTip="Click to Login" />
                        &nbsp;</span></td>
                </tr>
                <tr>
                    <td class="style2">
                    </td>
                </tr>
            </table>
          </div>
        <div class="image">
        <img alt="" src="images/Koala.jpg" height="350px" width="588px" />
        </div>
        <div class="news">
        NEWS
        </div>
        </div>
    </div>
    <uc4:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
