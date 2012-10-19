<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usergroup_maintenance_addupdate.aspx.cs" Inherits="PAOnlineAssessment.maintenance.usergroup_maintenance_addupdate" %>
<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>
<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Group Maintenance - Pace Academy Online Assessment System</title>
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
        .style6
        {
            width: 120px;
            text-align: right;
        }
        .style7
        {
            text-align: left;
        }
        .style8
        {
            width: 120px;
            text-align: right;
            height: 19px;
        }
        .style9
        {
            width: 21px;
            height: 19px;
        }
        .style10
        {
            text-align: left;
            width: 147px;
            height: 19px;
        }
        .style11
        {
            height: 19px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">   
    <uc1:frmHeader ID="frmHeader1" runat="server" />
        <div id="bodytopmainPan">
        <div id="bodytopPan">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <h2 style="background-color: #FFFFFF"><span class="PageHeader">Usergroup<span lang="en-ph"> Maintenance</span></span></h2>
            <table style="width:100%;">
                <tr>
                    <td>
                        <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width:100%;">
                            <tr>
                                <td class="style6">
                                    <span class="FieldTitle" lang="en-ph">Description:</span></td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style4">
                                    <asp:TextBox ID="txtDescription" runat="server" Height="20px" Width="175px"></asp:TextBox>
                                </td>
                                <td>
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="lblDescription" runat="server" 
                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                
                                <td>
                                  &nbsp;
                                </td>
                            </tr>
                            <tr>
                                
                                <td colspan="4">
                                  <b>Fields with <span class="ValidationNotice">*</span> are required fields.</b>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <table style="width:100%;">                      
                           <tr>
                                <td class="style1">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td>
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
