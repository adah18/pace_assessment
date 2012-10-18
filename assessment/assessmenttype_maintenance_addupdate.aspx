<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assessmenttype_maintenance_addupdate.aspx.cs" Inherits="PAOnlineAssessment.assessment.assessmenttype_maintenance_addupdate" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assessment Type Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 137px;
        }
        .style2
        {
            width: 29px;
        }
        .style3
        {
            width: 137px;
            text-align: right;
        }
        .style4
        {
            width: 92px;
        }
        .style5
        {
            text-align: right;
        }
        .style6
        {
            text-align: left;
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
	        <h2 style="background-color: #FFFFFF"><span class="PageHeader" lang="en-ph">Assessment Type Maintenance</span></h2>
            <table style="width:100%;">
                <tr>
                    <td><table style="width:700px;">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblMode" runat="server" CssClass="PageSubHeader"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="style6" colspan="4">
                                            <span class="TextboxTemplate" lang="en-ph">Assessment Type Details</span></td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <span class="FieldTitle" lang="en-ph">Description:</span></td>
                                        <td class="style2">
                                            &nbsp;</td>
                                        <td class="style4">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="TextboxTemplate" 
                                                Width="175px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <span lang="en-ph">&nbsp;</span><asp:Label ID="vlDescription" runat="server" 
                                                CssClass="ValidationNotice" Text="*"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="style2">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="style2">
                                            &nbsp;</td>
                                        <td class="style4">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <span class="ValidationNotice" lang="en-ph">Fields marked with an (*) are 
                                            required.</span></td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            &nbsp;</td>
                                        <td class="style2">
                                            &nbsp;</td>
                                        <td colspan="2">
                                            <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                                                BorderStyle="None" CssClass="ButtonTemplate" Height="34px" Text="Submit" 
                                                Width="88px" onclick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" BackColor="Transparent" 
                                                BorderStyle="None" CssClass="ButtonTemplate" Height="34px" Text="cancel" 
                                                Width="88px" onclick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>                        
                    </td>
                </tr>
            </table>
    </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
