<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student_maintenance_addupdate.aspx.cs" Inherits="PAOnlineAssessment.maintenance.student_maintenance_addupdate" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Student Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 120px;
        }
        .style3
        {
            width: 21px;
        }
        .style4
        {
            text-align: left;
            width: 147px;
        }
        .style5
        {
            width: 147px;
        }
        .style6
        {
            width: 120px;
            text-align: right;
        }
        .style7
        {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">   
    <uc1:frmHeader ID="frmHeader1" runat="server" />
        <div id="bodytopmainPan">
        <div id="bodytopPan">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table style="width:100%;">
                <tr class="PageHeader">
                    <td>
                        <h2 style="background-color: #FFFFFF"><span lang="en-ph">Student Maintenance</span></td></h2
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <uc3:SiteMap ID="SiteMap" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMode" runat="server" CssClass="PageSubHeader" 
                            Text="Edit Student Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="TextboxTemplate" lang="en-ph">User Account Details:</span></td>
                </tr>
                <tr>
                    <td>
                        <table style="width:100%;">
                            <tr>
                                <td class="style6">
                                    <span class="FieldTitle" lang="en-ph">Student Number:</span></td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style4">
                                    <asp:TextBox ID="txtStudentNumber" runat="server" CssClass="TextboxTemplate" 
                                        Width="175px"></asp:TextBox>
                                </td>
                                <td>
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="vlStudentNumber" runat="server" 
                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <span class="FieldTitle" lang="en-ph">Email Address:</span></td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style4">
                                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="TextboxTemplate" 
                                        Width="175px"></asp:TextBox>
                                </td>
                                <td>
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="vlEmailAddress" runat="server" 
                                        CssClass="ValidationNotice" Text=""></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style5">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="TextboxTemplate" lang="en-ph">Personal Details:</span></td>
                </tr>
                <tr>
                    <td>
                        <table style="width:100%;">
                            <tr>
                                <td class="style6">
                                    <span class="FieldTitle" lang="en-ph">First Name:</span></td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style4">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextboxTemplate" 
                                        Width="175px"></asp:TextBox>
                                </td>
                                <td class="style7">
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="vlFirstName" runat="server" 
                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <span class="FieldTitle" lang="en-ph">Last Name:</span></td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style4">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="TextboxTemplate" 
                                        Width="175px"></asp:TextBox>
                                </td>
                                <td class="style7">
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="vlLastName" runat="server" 
                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style5">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                        <span lang="en-ph" class="ValidationNotice">Fields marked with an (*) are required.</span></td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" 
                                    style="background-image: url('../../images/separator.jpg'); background-repeat: repeat-x; background-position: center center">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" CssClass="ButtonTemplate" Height="28px" 
                                        onclick="btnSubmit_Click" Text="Submit" ToolTip="Submit Changes" Width="88px" />
                                    <asp:Button ID="btnCancel" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" CssClass="ButtonTemplate" Height="28px" Text="Cancel" 
                                        ToolTip="Cancel Changes" Width="88px" onclick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        &nbsp;</td>
                </tr>
                </table>
        </div>
        </div>
        <uc2:frmFooter ID="frmFooter1" runat="server" />  
    </form>
</body>
</html>
