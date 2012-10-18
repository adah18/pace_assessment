<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assessment_display_setting.aspx.cs" Inherits="PAOnlineAssessment.maintenance.assessment_display_setting" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Display Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>
    <uc1:frmHeader ID="frmHeader1" runat="server" />   
    
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">Display Maintenance</span> 
                
            </h2>
            <table style="width: 100%" >
             <center>   
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <tr>
                    <td style="width:15%" valign="top" ><span class="FieldTitle" lang="en-ph">&nbsp;</span></td>
                    <td style="width:60%">
                        <table>
                            <tr>
                                <td rowspan="2" valign="middle">
                                    <asp:TextBox ID="txtDays" runat="server" CssClass="NormalText" Font-Size="Medium" Width="150px" Height="20px"></asp:TextBox>
                                </td>
                                <td valign="middle">
                                    <asp:ImageButton ID="imgUp" ImageUrl="~/images/up.gif" runat="server" Height="10px" />
                                    &nbsp;
                                    <br />
                                    <asp:ImageButton ID="imgDown" runat="server" ImageUrl="~/images/down.gif" Height="10px" />
                                </td>
                                
                            </tr>
                            
                            
                        </table>
                        <%--<span class="LoginSubNote"><span lang="en-ph">No. of days the assessment will be viewed by the students.</span></span>--%>
                        <span class="LoginSubNote"><span lang="en-ph">No. of days given to the students to view their past assessment.</span></span>
                        <br />
                        <asp:Label CssClass="ValidationNotice" ID="lblErr" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                    &nbsp;<asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="txtDays" Minimum="0" TargetButtonDownID="imgDown" TargetButtonUpID="imgUp" >
                        </asp:NumericUpDownExtender>
                    </td>
                    
                </tr>
                </ContentTemplate>
                </asp:UpdatePanel>
                </center>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><span class="FieldTitle" lang="en-ph">&nbsp;</span></td>
                    <td>
                    <span class="FieldTitle" lang="en-ph">
                        <asp:CheckBox ID="chk" runat="server" 
                             />
                        &nbsp;
                        Enable the Parent to register their own account.
                    </span>
                    </td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="88px" onclick="lnkSave_Click">Save</asp:LinkButton></td>
                </tr>
            </table>       
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />   
    </form>
</body>
</html>
