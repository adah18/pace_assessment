<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topic_maintenance_addupdate.aspx.cs" Inherits="PAOnlineAssessment.maintenance.topic_maintenance_addupdate" %>
<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>
<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Topic Maintenance - Pace Academy Online Assessment System</title>
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
            <h2 style="background-color: #FFFFFF"><span class="PageHeader">Topic Maintenance</span></h2>

            <table style="width:100%;">
                <tr>
                    <td>
                        <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <span class="TextboxTemplate" lang="en-ph">Topic Details:<br />
                        
                        </span>
                        </td>
                </tr>
                <tr>
                    <td>
                        <table style="width:100%;">
                            <tr>
                                <td class="style6" valign="top">
                                    <span class="FieldTitle" lang="en-ph">Topic Description:</span></td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style4">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="TextboxTemplate" 
                                        Width="175px" Height="58px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="lblTopic" runat="server" 
                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <span class="FieldTitle" lang="en-ph">Level:</span></td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style4">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboLevel" runat="server" Width="178px" 
                                                AutoPostBack="True" onselectedindexchanged="cboLevel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="lblLevel" runat="server" 
                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                             <tr>
                                <td class="style6">
                                    <span class="FieldTitle" lang="en-ph">Subject:</span></td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style4">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboSubject" runat="server" Width="178px">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                                </td>
                                <td>
                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="lblSubject" runat="server" 
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
