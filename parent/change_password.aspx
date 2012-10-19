<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="change_password.aspx.cs" Inherits="PAOnlineAssessment.parent.change_password" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Change Password - Pace Academy Online Assessment System</title>
<link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />

</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<uc1:frmHeader ID="frmHeader" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
        <h2><span class="PageHeader">Change Password</span></h2>
         <table>
            <tr>
                <td>
                    <div>
                        <span class="PageSubHeader"><span lang="en-ph">Old Password</span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                        <asp:Label ID="vlOldPassword" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtOldPassword" runat="server" CssClass="TextboxTemplate" Width="250px" TextMode="Password"></asp:TextBox>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Please Enter your Current Password</span>
                    </div>
                    <div>&nbsp;</div><div>&nbsp;</div>
                    
                    <div>
                        <span class="PageSubHeader"><span lang="en-ph">New Password</span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                        <asp:Label ID="vlNewPassword" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="TextboxTemplate" Width="250px" TextMode="Password"></asp:TextBox>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Please Enter your New Password</span>
                    </div>
                    <div>
                        <span class="PageSubHeader"><span lang="en-ph">Confirm Password</span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                        <asp:Label ID="vlCPassword" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtCPassword" runat="server" CssClass="TextboxTemplate" Width="250px" TextMode="Password"></asp:TextBox>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Please Re-Type your New Password</span>
                    </div>
                    <div>&nbsp;</div><div>&nbsp;</div>
                    <div style="text-align:right">
                        <asp:Button ID="Button1" runat="server" BackColor="Transparent" 
                                                  BorderStyle="None" CssClass="ButtonTemplate" Height="26px" 
                                                  Text="Save" Width="85px" onclick="Button1_Click"  />
                    </div>
                </td>
            </tr>
        </table>
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter" runat="server" />
</form>
</body>
</html>