<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_privilege.aspx.cs" Inherits="PAOnlineAssessment.maintenance.user_privilege" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmUpperBody.ascx" tagname="frmUpperBody" tagprefix="uc2" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Group Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <uc1:frmHeader ID="frmHeader1" runat="server" />
     <div id="bodytopmainPan">
        <div id="bodytopPan">
	        <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">User Privilege</span></h2> 
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <p style="width: 700px; text-align:right;">
                <span lang="en-ph">&nbsp;</span><span lang="en-ph">&nbsp;</span>&nbsp;</p>
            </ContentTemplate>
            </asp:UpdatePanel>   --%>  
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Here are the modules 
                <b><asp:Label ID="lblUsergroup" runat="server" Label=""></asp:Label></b> can be 
                accessed:&nbsp;
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                <br />
                <font color="red" style="font-style:italic" size="1">Note: You must logout first to apply 
                the changes</font>
                
                <br /><br />
                <b><asp:CheckBox ID="chkMaintenance" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chkMaintenance_CheckedChanged" 
                    Text="   Maintenance Tools" /></b>
                <br />
                
                 Here are the module under Maintenance Tools:
                
                <br /><br />
                
                <asp:CheckBoxList ID="chklMaintenance" runat="server">
                </asp:CheckBoxList>
                <hr /> 
                
                <b><asp:CheckBox ID="chkAssessment" runat="server" AutoPostBack="True" 
                    Text="   Assessment Tools" 
                    oncheckedchanged="chkAssessment_CheckedChanged" /></b>
                <br />
                Here are the module under Assessment Tools:<br /><br />
                
                <asp:CheckBoxList ID="chklAssessment" runat="server">
                </asp:CheckBoxList>
                <hr /> 
                
                <b><asp:CheckBox ID="chkAcademic" runat="server" AutoPostBack="True" 
                    Text="   Academic Activities" 
                    oncheckedchanged="chkAcademic_CheckedChanged"/></b>
                <br />
                Here are the module under Academic Activities:<br /><br />
                
                <asp:CheckBoxList ID="chklAcademic" runat="server">
                    
                </asp:CheckBoxList>
            </ContentTemplate>
            </asp:UpdatePanel>
	        
            <br />
            <asp:LinkButton ID="btnSubmit" CssClass="ButtonTemplate" runat="server" BackColor="Transparent" 
                BorderStyle="None" Height="28px" Width="88px" onclick="btnSubmit_Click">Submit</asp:LinkButton>
	        
            <asp:LinkButton ID="btnSubmit0" CssClass="ButtonTemplate" runat="server" BackColor="Transparent" 
                BorderStyle="None" Height="28px" Width="88px" 
                onclientclick="if(confirm(&quot;Do you really want to cancel?&quot;)){ window.location='usergroup_maintenance_main.aspx';} return false;">Cancel</asp:LinkButton>
    </div>
    </div>
    <uc3:frmFooter ID="frmFooter1" runat="server" />    
    </form>
</body>
</html>
