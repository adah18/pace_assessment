<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student_dashboard.aspx.cs" Inherits="PAOnlineAssessment.student.student_dashboard" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Student's Dashboard - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
	        
            <table style="width:100%;">
                <tr>
                    <td colspan="3">
                        <span class="PageHeader" lang="en-ph">Welcome </span>
                        <asp:Label ID="lblLoggedFirstname" runat="server" CssClass="PageHeader"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                            <span class="LoginSubNote" lang="en-ph"><span class="style5">What would you like 
                            to do?</span></span></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr class="PageSubHeader">
                    <td colspan="3">
                        <span lang="en-ph">Academic Activities</span></td>
                </tr>
                <tr class="PageSubHeader">
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    
                    <td><center>
                            
                            <asp:ImageButton ID="ImgAssessmentHistory" runat="server" 
                            CssClass="LargeButtonTemplate" Height="128px" 
                            ImageUrl="~/images/dashboard_icons/quizzes.png" Width="128px" onclick="ImgAssessmentHistory_Click" 
                             />
                        <br />
                            <span class="GridPagerButtons" lang="en-ph">Assessment<br />
                        History</span>
                        </center>
                     </td>
                
                    <td><center>
                        <asp:ImageButton ID="imgTakeAssessment" runat="server" 
                            CssClass="LargeButtonTemplate" Height="128px" 
                            ImageUrl="~/images/dashboard_icons/comment.png" Width="128px" 
                            onclick="imgTakeAssessment_Click" />
                        <br />
                        <span class="GridPagerButtons" lang="en-ph">Take an<br />
                        Assessment</span></center></td>
                    
                    <td><center>
                        <asp:ImageButton ID="ImageButton3" runat="server" 
                            CssClass="LargeButtonTemplate" Height="128px" 
                            ImageUrl="~/images/dashboard_icons/chart.png" Width="128px" 
                            onclick="ImageButton3_Click" />
                        <br />
                        <span class="GridPagerButtons" lang="en-ph">My<br />
                        Grades</span></center></td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr class="PageSubHeader">
                    <td colspan="3"><span lang="en-ph">Account Settings</span></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><center>
                        <asp:ImageButton ID="imgChangePassword" runat="server" 
                            CssClass="LargeButtonTemplate" Height="128px" 
                            ImageUrl="~/images/dashboard_icons/lock_settings.png" Width="128px" 
                            onclick="imgChangePassword_Click" />
                        <br />
                        <span class="GridPagerButtons" lang="en-ph">Change Current<br />
                        Password</span></center></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        </div>
        <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
