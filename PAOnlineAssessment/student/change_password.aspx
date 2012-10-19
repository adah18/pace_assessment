<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="change_password.aspx.cs" Inherits="PAOnlineAssessment.student.change_password" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Change Password - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style5
        {
            left: 0px;
            top: 0px;
        }
        .style7
        {
            width: 27px;
        }
        .style8
        {
            color: #666666;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
            width: 114px;
        }
        .style9
        {
            width: 140px;
        }
        .style10
        {
            width: 157px;
        }
        .style11
        {
            color: #666666;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
            text-align: left;
        }
        .style12
        {
            color: #666666;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">   
    <uc1:frmHeader ID="frmHeader1" runat="server" />
        <div id="bodytopmainPan">
        <div id="bodytopPan">
            <h2 h2 style="background-color: #fff"><span class="PageHeader" lang="en-ph">Change Password</span></h2>
            <table style="width:100%;">
                <tr>
                    <td>
                        <uc3:SiteMap ID="SiteMap" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMode" runat="server" CssClass="PageSubHeader" 
                            Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="TextboxTemplate" lang="en-ph">Current Password</span></td>
                </tr>
                <tr>
                    <td>
                        <table style="width:100%;">
                            <tr>
                                <td class="style8" style="text-align:right;">
                                    <span lang="en-ph">Old Password:</span></td>
                                <td class="style7">
                                    &nbsp;</td>
                                <td class="style10">
                                    <asp:TextBox ID="txtOldPassword" runat="server" Width="175px" 
                                        CssClass="TextboxTemplate" TextMode="Password"></asp:TextBox>
                                </td>
                                <td class="ValidationNotice">
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="vlOldPassword" runat="server" 
                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <span class="TextboxTemplate" lang="en-ph">New Password</span></td>
                </tr>
                
                <tr>
                    <td>
                        <table style="width:100%;">
                            <tr>
                                <td class="style8" style="text-align:right;">
                                    <span lang="en-ph">New Password:</span></td>
                                <td class="style7">
                                    &nbsp;</td>
                                <td class="style9">
                                    <asp:TextBox ID="txtNewPassword" runat="server" Width="175px" 
                                        CssClass="TextboxTemplate" TextMode="Password"></asp:TextBox>
                                </td>
                                <td class="ValidationNotice">
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="vlNewPassword" runat="server" 
                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8" style="text-align:right;">
                                    <span lang="en-ph">Confirm Password:</span></td>
                                <td class="style7">
                                    &nbsp;</td>
                                <td class="style9">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" Width="175px" 
                                        CssClass="TextboxTemplate" TextMode="Password"></asp:TextBox>
                                </td>
                                <td class="ValidationNotice">
                                    <span lang="en-ph">&nbsp;</span></td>
                            </tr>
                            <tr>
                                <td class="style8" style="text-align:right;">
                                    &nbsp;</td>
                                <td class="style7">
                                    &nbsp;</td>
                                <td class="style9">
                                    &nbsp;</td>
                                <td class="ValidationNotice">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style11" colspan="4">
                                    <span class="ValidationNotice" lang="en-ph">Fields marked with an (*) are 
                                    required.</span></td>
                            </tr>
                            <tr>
                                <td class="style12" 
                                    style="background-position: center center; text-align:right; background-image: url('../../images/separator.jpg'); background-repeat: repeat-x;" 
                                    colspan="3">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8" style="text-align:right;">
                                    &nbsp;</td>
                                <td class="style7">
                                    &nbsp;</td>
                                <td colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" CssClass="ButtonTemplate" Height="34px" Text="Submit" 
                                        ToolTip="Submit" Width="88px" onclick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" CssClass="ButtonTemplate" Height="34px" Text="Cancel" 
                                        ToolTip="Cancel" Width="88px" onclick="btnCancel_Click" />
                                </td>
                            </tr>
                            </table>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
        </div>
        </div>
      <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
