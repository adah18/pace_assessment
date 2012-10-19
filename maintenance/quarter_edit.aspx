<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quarter_edit.aspx.cs" Inherits="PAOnlineAssessment.maintenance.quarter_edit" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Quarter Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" type="text/css" />
    <!-- Contact Form CSS files -->
    <link type='text/css' href="../scripts/styles/modal_basic.css" rel='stylesheet' media='screen' /> 
    
     <script type="text/javascript" src="../assessment/datepicker/src/js/jscal2.js"></script> 
    <script type="text/javascript" src="../assessment/datepicker/src/js/lang/en.js"></script> 
    <link rel="stylesheet" type="text/css" href="../assessment/datepicker/src/css/jscal2.css" /> 
    <link rel="stylesheet" type="text/css" href="../assessment/datepicker/src/css/border-radius.css" /> 
    <link rel="stylesheet" type="text/css" href="../assessment/datepicker/src/css/steel/steel.css" /> 
    <style type="text/css">
        .ImageCaption
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: normal;
            color: #7c7900;
            font-size: 11px;
        }
        .style1
        {
            width: 133px;
        }
        </style>    

</head>


<body>
    <form id="form1" runat="server">
    <uc1:frmHeader ID="frmHeader1" runat="server" />   
   <div id="bodytopmainPan">
        <div id="bodytopPan">
            <h2 style="background-color: #FFFFFF"><span class="PageHeader" lang="en-ph">Quarter Maintenance</span></h2>
        	<p>
                <table style="width:100%;">
                    <tr>
                        <td colspan="3">
                        <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                            
                        </td>
                    </tr>
                    <tr>
                       <td colspan="4">
                           <span class="TextboxTemplate" lang="en-ph">Quarter Details</span></td>
                       </td>
                    </tr>
                    <tr>
                        <td colspan="4"> 
                            <table>
                                <tr>
                                   <td class="style1">
                                     <span class="FieldTitle">Quarter: </span>
                                   </td>
                                   <td>
                                       <span class="GridPagerButtons"><asp:Label ID="lblQuarter" runat="server" Text=""></asp:Label></span>
                                       <br />
                                       </td>
                                </tr>
                                <tr>
                                   <td class="style1">
                                     <span class="FieldTitle">Quarter Start: </span>
                                   </td>
                                   <td>
                                       <asp:TextBox ID="txtDateFrom" CssClass="GridPagerButtons" runat="server" Width="165px"></asp:TextBox>
                                       <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                           ImageUrl="~/images/icons/calendar.gif" 
                                         ToolTip="Open Calendar" onclientclick="return false;" />
                                         
                                         <br />
                                       <span class="LoginSubNote"><span lang="en-ph">Please enter a date the quarter 
                                       start</span></span><script type="text/javascript">                                                             //<![CDATA[
                                         Calendar.setup({
                                            inputField: "txtDateFrom",
                                            trigger: "ImageButton1",
                                            onSelect: function() { this.hide() },
                                            showTime: 12,
                                            dateFormat: "%m/%d/%Y"
                                         });
                                        </script>
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lbl0" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   <td class="style1">
                                     <span class="FieldTitle">Quarter End: </span>
                                   </td>
                                   <td>
                                       <asp:TextBox ID="txtDateTo" CssClass="GridPagerButtons" runat="server" Width="165px"></asp:TextBox>
                                       <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                           ImageUrl="~/images/icons/calendar.gif" 
                                           ToolTip="Open Calendar" onclientclick="return false;" />
                                           
                                           <script type="text/javascript">                                               //<![CDATA[
                                               Calendar.setup({
                                                   inputField: "txtDateTo",
                                                   trigger: "ImageButton2",
                                                   onSelect: function() { this.hide() },
                                                   showTime: 12,
                                                   dateFormat: "%m/%d/%Y"
                                               });
                                        </script>
                                       <br />
                                       <span class="LoginSubNote"><span lang="en-ph">Please enter a date the quarter 
                                       end</span></span>
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lbl1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                       <span class="FieldTitle">School Year: </span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSchoolYear" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td align="right">
                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="ButtonTemplate" 
                            Height="28px" Width="88px" onclick="lnkSave_Click">Update</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        </td>
                    </tr>
                    <tr>
                    <td>&nbsp;</td>
                    </tr>
                    </table>
         </p>
        </div>
        </div>
     <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
