<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="school_year_maintenance.aspx.cs" Inherits="PAOnlineAssessment.maintenance.school_year_maintenance" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Display Settings - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            <h2 style="background-color: #FFFFFF">
                <span class="PageHeader" lang="en-ph">School Year Maintenance</span>
            </h2>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <center>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <asp:ImageButton ID="imgleft" runat="server" ImageUrl="~/images/left.png" 
                            Width="10px" onclick="imgleft_Click"  />
                        <asp:TextBox ID="txtschoolyear" runat="server" Height="20px" Width="150px" 
                                style="text-align: center; font-size:medium;" ReadOnly="True"></asp:TextBox>
                        <asp:ImageButton ID="imgright" runat="server" ImageUrl="~/images/right.png" 
                            Width="10px" onclick="imgright_Click" />
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </center>
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="When you change the default setting of the school year,
                           some of the functions will use the new school year setting." Width="500px"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        &nbsp;
                        <asp:TextBox ID="TextBox1" runat="server" Visible="False">0</asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2" align="right">
                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="ButtonTemplate" 
                            Height="28px" onclick="lnkSave_Click" Width="88px">Save</asp:LinkButton>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" /> 
    </form>
</body>
</html>
