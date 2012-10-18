<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instructor_studentsview.aspx.cs" Inherits="PAOnlineAssessment.instructor.instructor_studentsview" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>
<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Student's Adviser - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 190px;
            height: 18px;
        }
        .style3
        {
            width: 528px;
        }
        .style6
        {
            height: 18px;
        }
        .style7
        {
            height: 28px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>   
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
 
        <div id="bodytopPan" class="style3">
            <h2><span lang="en-ph" class="PageHeader">My Subjects</span></h2>
            <table style="width:100%;">
                <tr>
                    <td class="style6" valign="top">
                        <asp:Panel ID="pHandler" runat="server" ScrollBars="Auto">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <span class="PageSubHeader" lang="en-ph">
                                    
                                        <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                                    <table>
                                        <tr>
                                            <td align="right">
                                            <font size="2">Quarter:<span class="PageHeader" lang="en-ph"><span 
                                                    class="PageSubHeader" lang="en-ph">
                                                <asp:DropDownList ID="cboQuarter" runat="server" CssClass="GridPagerButtons"
                                                    onselectedindexchanged="cboQuarter_SelectedIndexChanged">
                                                    <asp:ListItem>1st</asp:ListItem>
                                                    <asp:ListItem>2nd</asp:ListItem>
                                                    <asp:ListItem>3rd</asp:ListItem>
                                                    <asp:ListItem>4th</asp:ListItem>
                                                </asp:DropDownList>
                                                </span></span></font>
                                                &nbsp;<font size="2"> Assessment Type:</font>
                                                <asp:DropDownList ID="cboAssessment" runat="server" CssClass="GridPagerButtons"
                                                    onselectedindexchanged="cboAssessment_SelectedIndexChanged" Width="150px">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtSearchQuery" runat="server" 
                        CssClass="GridPagerButtons" Width="177px" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="cboSearchQuery" runat="server" 
                        CssClass="GridPagerButtons" Visible="False" 
                                                    onselectedindexchanged="cboSearchQuery_SelectedIndexChanged" Height="17px" 
                                                    Width="105px">
                                                    <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                                    <asp:ListItem Value="MiddleName">Middle Name</asp:ListItem>
                                                    <asp:ListItem Value="Last Name">Last Name</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:ImageButton ID="imgSearchQuery" runat="server" 
                        CssClass="GridPagerButtons" ImageUrl="~/images/icons/page_find.gif" ToolTip="Search" 
                                                    onclick="imgSearchQuery_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="PageSubHeader">
                                                <div style="overflow:scroll; width:720px;">
                                                <asp:GridView ID="grdStudentsList" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="GridPagerButtons" PageSize="2" 
                                                    onrowcreated="grdStudentsList_RowCreated" Width="700px">
                                                    <PagerSettings Position="TopAndBottom" />
                                                 <Columns>
                                                    <asp:TemplateField HeaderText="Action">
                                                       
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" 
                                                                CommandName="EditItem" ImageUrl="~/images/icons/page_edit.gif" Text="Edit" 
                                                                ToolTip="Edit Assessment Type" />
                                                            &nbsp;<asp:ImageButton ID="imgDeactivate" runat="server" CausesValidation="False" 
                                                                CommandName="DeleteItem" ImageUrl="~/images/icons/page_delete.gif" 
                                                                Text="Delete" />
                                                            <asp:Label ID="lblAssessmentTypeID" 
                                                                runat="server" Text = "a" Visible="False"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                            HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                    </asp:TemplateField>
                                                   
                                                </Columns>

                                                    <EmptyDataTemplate>
                                                        <br />
                                                         <center>
                                                        No Assessment found for Subject.
                                                        </center>
        
                                                        <br />
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                </div>
                                                </span>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;<asp:Label ID="lblErr" Style="color:Red; font-size:12px" runat="server" Text="" Visible="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                    
                                    </span>
                                  
                                    <asp:GridView ID="grdView" runat="server" onrowcreated="grdView_RowCreated" 
                                        Width="700px" Visible="false">
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                     
                    </td>
                </tr>
                <tr>
                                        <td class="style7" >
                                            <asp:LinkButton ID="lnkExport" runat="server" onclick="lnkExport_Click" 
                                                onclientclick="this.disabled = true; ">Export to Excel</asp:LinkButton>
                                        &nbsp;|
                                            <asp:LinkButton ID="lnkSave" runat="server" onclick="lnkSave_Click">Save Grades to Grading System</asp:LinkButton>
                                            <br />
                                        </td>
                                   
                                    </tr>
                                    <tr>
                                        <td class="style1" align="center">
                                            &nbsp;</td>
                                    
                                    </tr>
            </table>
    </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
