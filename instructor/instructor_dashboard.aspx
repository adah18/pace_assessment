<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="instructor_dashboard.aspx.cs" Inherits="PAOnlineAssessment.instructor_dashboard" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Teachers' Dashboard - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" type="text/css" />
    <!-- Contact Form CSS files -->
    <link type='text/css' href="../scripts/styles/modal_basic.css" rel='stylesheet' media='screen' />
    
    
    <style type="text/css">
        .ImageCaption
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: normal;
            color: #7c7900;
            font-size: 11px;
        }
        .style2
        {
            font-weight: bold;
        }
        .style3
        {
            font-weight: bold;
            vertical-align: middle;
        }
        .style4
        {
            font-size: 14px;
            color: #3F5330;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:frmHeader ID="frmHeader1" runat="server" />
   <div id="bodytopmainPan">
        <div id="bodytopPan">
        	<p>
                <table style="width:100%;">
                    <tr>
                        <td colspan="3" class="PageHeader" valign=middle>
                            <span class="LoginHeader" lang="en-ph">Welcome </span>
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
                    
                    <tr>
                        <td colspan="3"><span class="PageSubHeader" lang="en-ph">Tools</span></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td><center>
                            <asp:ImageButton ID="imgUserMaintenance" runat="server" 
                                CssClass="LargeButtonTemplate user_modal" Height="128px" ImageAlign="Middle" 
                                ImageUrl="~/images/dashboard_icons/process.png" Width="128px" /><br />
                            <span class="LinkButtonTemplate">Maintenance Tools</span></center>
                        </td>
                        
                        <td><center>
                            <asp:ImageButton ID="imgAssessment" runat="server" 
                                ImageUrl="~/images/dashboard_icons/page_process.png" 
                                CssClass="LargeButtonTemplate assessment_modal" Height="128px" 
                                ImageAlign="Middle" Width="128px" /><br />
                            <span lang="en-ph" class="LinkButtonTemplate">Assessment Tools</span></center>
                        </td>
                        
                        <td><center>
                            &nbsp;
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td colspan="3" class="style4">
                            <span lang="en-ph"><span class="style3">Academic Activities</span></span></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td><center>
                            <asp:ImageButton ID="imgMySubjects" runat="server" 
                                ImageUrl="~/images/dashboard_icons/folder_full.png" 
                                CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                                Width="128px" onclick="imgMySubjects_Click" />
                            <br />
                            <span class="GridPagerButtons" lang="en-ph"><span class="style2">My<br />
                            Subjects</span></span><br /></center>
                        </td>
                        <td>
                            <center>
                                <asp:ImageButton ID="ImageButton4" runat="server" 
                                    CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                                    ImageUrl="~/images/dashboard_icons/chart.png" Width="128px" 
                                    onclick="ImageButton4_Click" />
                                <br />
                                <span class="GridPagerButtons" lang="en-ph">My Students&#39;<br />
                                Grades</span><br />
                            </center>
                        </td>
                        <td><center>
                             <asp:ImageButton ID="imgMakeUp" runat="server" 
                                CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                                ImageUrl="~/images/dashboard_icons/Todo.png" 
                                Width="128px" onclick="imgMakeUp_Click" />
                            <br />
                            <span class="GridPagerButtons" lang="en-ph">View All <br />Make-Up Exams</span>
                        </center>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    
                </table>
            </p>
    </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
    
<!-- Load jQuery, SimpleModal and Basic JS files -->
<script type='text/javascript' src="../scripts/jquery/jquery.js"></script>
<script type='text/javascript' src="../scripts/jquery/jquery.simplemodal.js"></script>
<script type='text/javascript' src="../scripts/jquery/modal_basic.js"></script>
</body>
</html>
