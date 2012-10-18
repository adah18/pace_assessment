<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activate_email.aspx.cs" Inherits="PAOnlineAssessment.registration.activate_email" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>

<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Activate Email - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style5
        {
            width: 174px;
        }
        .style8
        {
            width: 238px;
        }
        .style9
        {
            width: 42%;
        }
        .style10
        {
            font-weight: bold;
        }
        .style11
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">    
    <uc1:frmHeader ID="frmHeader1" runat="server" />
        <div id="bodytopmainPan">
            <div id="bodytopPan" >
            	
                <table style="width:100%;">
                    <tr>
                        <td colspan="3">
                            <span class="PageHeader" lang="en-ph">Email Activation</span></td>
                    </tr>
                    <tr>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style8">
                            &nbsp;</td>
                        <td class="style9">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style11" colspan="3">
                            <span class="style10" lang="en-ph">Processing. Please Wait</span></td>
                    </tr>
                    <tr class="ValidationNotice">
                        <td class="style11" colspan="3">
                            <span lang="en-ph">Please do not close this page.</span></td>
                    </tr>
                    <tr class="ValidationNotice">
                        <td class="style11" colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr class="ValidationNotice">
                        <td class="style11" colspan="3">
                            <asp:Image ID="imgLoadingBar" runat="server" 
                                ImageUrl="~/images/Loading_Bar.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style8">
                            &nbsp;</td>
                        <td class="style9">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style11" colspan="3">
                            <span lang="en-ph">Registered Email: </span>
                            <asp:Label ID="lblRegisteredEmail" runat="server" CssClass="style10"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style11" colspan="3">
                            <span lang="en-ph">Registered To: </span>
                            <asp:Label ID="lblRegisteredTo" runat="server" CssClass="style10"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style8">
                            &nbsp;</td>
                        <td class="style9">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:Timer ID="Timer1" runat="server" Interval="4000" ontick="Timer1_Tick">
                            </asp:Timer>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:frmFooter ID="frmFooter1" runat="server" /> 
    </form>
</body>
</html>
