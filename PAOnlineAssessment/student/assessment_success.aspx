<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assessment_success.aspx.cs" Inherits="PAOnlineAssessment.student.assessment_success" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>

<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Take Assessment- Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            <table style="width:100%;">
                <tr>
                    <td colspan="2">
                        <span class="PageHeader" lang="en-ph">Answers Submitted</span></td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <uc3:SiteMap ID="SiteMap1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" class="style1">
                        <span class="GridPagerButtons" lang="en-ph">You have successfully completed this 
                        assessment.<br />
                        <br />
                        You may review your assessment for incorrect answers<br />
                        </span></td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td><Center>
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            CssClass="LargeButtonTemplate" Height="128px" 
                            ImageUrl="~/images/dashboard_icons/home.png" Width="128px" 
                            onclick="ImageButton1_Click" />
                        <br />
                        <span class="GridPagerButtons" lang="en-ph">Student Dashboard</span></Center></td>
                    <td><center>
                        <asp:ImageButton ID="ImageButton2" runat="server" 
                            CssClass="LargeButtonTemplate" Height="128px" 
                            ImageUrl="~/images/dashboard_icons/accept_page.png" Width="128px" 
                            onclick="ImageButton2_Click" />
                        <br />
                        <span class="GridPagerButtons" lang="en-ph">Review Assessment</span></center></td>
                </tr>
            </table>	
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
