<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_profile.aspx.cs" Inherits="PAOnlineAssessment.Parent.edit_profile" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Edit Profile - Pace Academy Online Assessment System</title>
<link href="../scripts/styles/Font%20Style.css"rel="stylesheet" type="text/css" />
<%--<script type="text/javascript" src="../scripts/jquery/parent_js/cufon-yui.js"></script>
<script type="text/javascript" src="../scripts/jquery/parent_js/georgia.js"></script>
<script type="text/javascript" src="../scripts/jquery/parent_js/cuf_run.js"></script>
--%>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            <h2><span class="PageHeader">Edit Profile</span></h2>
            <table>
                <tr>
                    <td>
                        <span class="PageSubHeader">Account Information</span>
                    </td>
                </tr>
                
                <tr>
                    <td><span class="FieldTitle">Username</span></td>
                    <td><span class="GridPagerButtons"><asp:Label ID="lblUsername" runat="server"></asp:Label></span></td>
                </tr>
                
                <tr>
                    <td><span class="FieldTitle">Password</span></td>
                    <td><span class="GridPagerButtons"><asp:Label ID="lblPassword" runat="server"></asp:Label></span></td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
                
                <tr>
                    <td><span class="PageSubHeader">Basic Information</span></td>
                </tr>
                
                <tr>
                    <td><span class="FieldTitle">First Name</span></td>
                    <td><span class="GridPagerButtons"><asp:TextBox ID="txtFirstname" runat="server" 
                                            Width="200px"></asp:TextBox></span>
                                            
                                            </td>
                    <td><asp:Label ID="vlFirstname" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label></td>
                </tr>
                
                <tr>
                    <td><span class="FieldTitle">Last Name</span></td>
                    <td><span class="GridPagerButtons"><asp:TextBox ID="txtLastname" runat="server" 
                                            Width="200px"></asp:TextBox>
                                        </span>
                                        
                                        </td>
                    <td><asp:Label ID="vlLastname" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label></td>
                </tr>
                
                <tr>
                    <td><span class="FieldTitle">Email Address</span></td>
                    <td><span class="GridPagerButtons"><asp:TextBox ID="txtEmail" runat="server" 
                                            Width="200px"></asp:TextBox></span>
                                            </td>
                    <td><asp:Label ID="lblEmail" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label></td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
                
                <tr>
                    <td colspan="2" align="right">
                     <asp:Button ID="Button1" runat="server" BackColor="Transparent" 
                                                  BorderStyle="None" CssClass="ButtonTemplate" Height="26px" 
                                                  Text="Save" Width="85px" onclick="Button1_Click"  />                       
                                            </td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                    
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>  
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />  
</form>
</body>
</html>