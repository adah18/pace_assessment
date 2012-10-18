<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="parents_dashboard.aspx.cs" Inherits="PAOnlineAssessment.parent.parents_dashboard" %>

<%@ Register Src="~/frmHeader.ascx" TagName="frmHeader" TagPrefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" Tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Parent Dashboard - Pace Academy Online Assessment System</title>
<meta http-equiv="content-type" content="text/html; charset=utf-8" />

<link href="../scripts/styles/parent_style.css"rel="stylesheet" type="text/css" />
<link href="../scripts/styles/Font Style.css"rel="stylesheet" type="text/css" />
<%--<script type="text/javascript" src="../scripts/jquery/parent_js/cufon-yui.js"></script>
<script type="text/javascript" src="../scripts/jquery/parent_js/georgia.js"></script>
<script type="text/javascript" src="../scripts/jquery/parent_js/cuf_run.js"></script>
--%>
<link rel="stylesheet" type="text/css" href="parent_menu/pro_drop_1.css" />
<link type='text/css' href="../scripts/styles/modal_basic.css" rel='stylesheet' media='screen' />

<script src="parent_menu/stuHover.js" type="text/javascript"></script>

</head>
<body>
<form id="form1" runat="server">
<!-- START PAGE SOURCE -->
<div class="main">
<uc1:frmHeader ID="frmHeader1" runat="server" />
  <div class="body">
    <div class="body_resize">
      <div class="left">
        <h2>Dashboard </h2>
       <%-- Start Content --%>
        <table width="100%">
            <tr class="tr">
                        <td><center>
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/images/dashboard_icons/folder_full.png" 
                                CssClass="LargeButtonTemplate parent_act_modal" Height="128px" 
                                ImageAlign="Middle" Width="128px" /><br />
                                <span lang="en-ph" class="LinkButtonTemplate">Activities</span>
                             </center>
                        </td>
                         <td><center>
                            &nbsp;
                             </center>
                        </td>
                        <td><center>
                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                ImageUrl="~/images/dashboard_icons/user.png" 
                                CssClass="LargeButtonTemplate settings_modal" Height="128px" 
                                ImageAlign="Middle" Width="128px" /><br />
                                <span lang="en-ph" class="LinkButtonTemplate">User Settings</span>
                             </center>
                            <%--<asp:ImageButton ID="imgAssessment" runat="server" 
                                ImageUrl="~/images/dashboard_icons/full_page.png" 
                                CssClass="LargeButtonTemplate" Height="128px" 
                                ImageAlign="Middle" Width="128px" onclick="imgAssessment_Click" 
                                onclientclick="logout(); return false;" /><br />
                            <span lang="en-ph" class="LinkButtonTemplate">Logout</span></center>--%>
                        </td>
                    </tr>
        </table>
       <%-- End Content --%>
        &nbsp;<div class="bg"></div>
        <h2> &nbsp;</h2>
        <h2>&nbsp;</h2>
      </div>
      <div class="right">
        </div>
        
        </div>
      </div>
      <div class="clr">
    </div>
    </div>
</div>
<uc2:frmFooter ID="frmFooter1" runat="server" />
<!-- END PAGE SOURCE -->
</form>
<script type='text/javascript' src="../scripts/jquery/jquery.js"></script>
<script type='text/javascript' src="../scripts/jquery/jquery.simplemodal.js"></script>
<script type='text/javascript' src="../scripts/jquery/modal_basic.js"></script>
</body>
</html>