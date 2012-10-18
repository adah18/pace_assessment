<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="parent_select_child.aspx.cs" Inherits="PAOnlineAssessment.parent.parent_select_child" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Parent-Child Request - Pace Academy Online Assessment System</title>
<link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<uc1:frmHeader ID="frmHeader" runat="server" />
<div id="bodytopmainPan">
        <div id="bodytopPan">
        <h2><span class="PageHeader" >Select Child</span></h2>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
        <table>
            <tr>
                <td><span class="FieldTitle" >Grade / Level</span></td>
                <td>
                    <asp:DropDownList ID="cboLevel" runat="server" CssClass="GridPagerButtons"  Width="175px" AutoPostBack="True" onselectedindexchanged="cboGradeLevel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <span lang="en-ph">&nbsp;</span><asp:Label  ID="vlGradeLevel" runat="server" CssClass="ValidationNotice" Text="*"></asp:Label>
                                            <br />
                                            <span class="LoginSubNote"><span lang="en-ph">Select a Grade/Year Level</span></span>
                </td>
            </tr>
            
            <tr>
                <td><span class="FieldTitle">Section</span></td>
                <td>
                    <asp:DropDownList ID="cboSection" runat="server" 
                                                                CssClass="GridPagerButtons" Width="175px" 
                                                AutoPostBack="True" onselectedindexchanged="cboSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <span lang="en-ph">&nbsp;</span><asp:Label ID="vlSubjectList" runat="server" 
                                                CssClass="ValidationNotice" Text="*"></asp:Label>
                                            <br />
                                            <span class="LoginSubNote" lang="en-ph">Select a section to view all the 
                                            students</span>
                </td>
            </tr>
            
            <tr>
                <td>&nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="2">
                    <span class="LoginSubNote" lang="en-ph">Select your child here:</span>
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvStudentList" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" CssClass="GridPagerButtons" PageSize="100" 
                                    Width="700px">
                                    <PagerSettings Position="TopAndBottom" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkTick" runat="server" />
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("StudentID") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                    <asp:BoundField DataField="Firstname" HeaderText="Firstname">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Lastname" HeaderText="Lastname">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="YearLevel" HeaderText="Level">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Section" HeaderText="Section">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                    </Columns> 
                                    <PagerTemplate>
                                        <div>
                                            <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkFirst_Click" ToolTip="Go to First Page">First</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;|&nbsp;<asp:LinkButton ID="lnkPrev" runat="server" 
                                                CssClass="GridPagerButtons" onclick="lnkPrev_Click">Previous</asp:LinkButton>
                                            &nbsp;| </span><span class="LoginSubHeader" lang="en-ph"><span lang="en-ph">
                                            <asp:Label ID="Label11" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                                CssClass="GridPagerButtons" 
                                                onselectedindexchanged="cboPageNumber_SelectedIndexChanged" 
                                                ToolTip="Jump to Page" Width="38px">
                                            </asp:DropDownList>
                                            &nbsp;<asp:Label ID="lblPageCount" runat="server" CssClass="GridPagerButtons" 
                                                Text="of "></asp:Label>
                                            |
                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkNext_Click">Next</asp:LinkButton>
                                            &nbsp;|
                                            <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkLast_Click" ToolTip="Go to Last Page">Last</asp:LinkButton>
                                            </span></span>
                                        </div>
                                    </PagerTemplate>
                                    <PagerStyle BorderWidth="0px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:GridView>
                </td>
            </tr>
            
            <tr>
                <td>&nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                                                  BorderStyle="None" CssClass="ButtonTemplate" Height="34px" 
                                                  onclientclick="this.value='Submitting...';  return true; this.disabled=true;" 
                                                  Text="Select" Width="91px" onclick="btnSubmit_Click" />
                                    <asp:GridView ID="GridView1" runat="server" Visible="False">
                                    </asp:GridView>
                                    <asp:HiddenField ID="hidGridCount" runat="server" />
                </td>
            </tr>
            
            <tr>
                <td>&nbsp;</td>
            </tr>
            
            <tr>
                <td><span class="LoginSubNote" lang="en-ph">Selected child/children:</span></td>
            </tr>
            
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvSelectedStudent" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" CssClass="GridPagerButtons" PageSize="20" 
                                    Width="700px" onrowdatabound="gvSelectedStudent_RowDataBound" 
                        onrowdeleting="gvSelectedStudent_RowDeleting">
                                    <PagerSettings Position="TopAndBottom" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRemove" CausesValidation="false" ToolTip="Delete Child" CommandName="Delete" ImageUrl="~/images/icons/page_delete.gif" runat="server" onclientclick="return confirm('Are you sure you want to delete this child?')" />
                                                <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserID") %>' Visible ="false"></asp:Label>
                                                <asp:Label ID="lblStudentID" runat="server" Text='<%# Eval("StudentID") %>' Visible ="false"></asp:Label>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' Visible ="false"></asp:Label>
                                                
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                    <asp:BoundField DataField="Firstname" HeaderText="Firstname">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Lastname" HeaderText="Lastname">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="YearLevel" HeaderText="Level">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Section" HeaderText="Section">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#777777" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                    </Columns> 
                                    
                                    <EmptyDataTemplate>
                                                                <br />
                                                                <center>
                                                                    No child found.
                                                                </center>
                                                                <br />
                                                            </EmptyDataTemplate>
                                    
                                    <PagerTemplate>
                                        <div>
                                            <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkFirst_Click" ToolTip="Go to First Page">First</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;|&nbsp;<asp:LinkButton ID="lnkPrev" runat="server" 
                                                CssClass="GridPagerButtons" onclick="lnkPrev_Click">Previous</asp:LinkButton>
                                            &nbsp;| </span><span class="LoginSubHeader" lang="en-ph"><span lang="en-ph">
                                            <asp:Label ID="Label11" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                                CssClass="GridPagerButtons" 
                                                onselectedindexchanged="cboPageNumber_SelectedIndexChanged" 
                                                ToolTip="Jump to Page" Height="18px" Width="37px">
                                            </asp:DropDownList>
                                            &nbsp;<asp:Label ID="lblPageCount" runat="server" CssClass="GridPagerButtons" 
                                                Text="of "></asp:Label>
                                            |
                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkNext_Click">Next</asp:LinkButton>
                                            &nbsp;|
                                            <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkLast_Click" ToolTip="Go to Last Page">Last</asp:LinkButton>
                                            </span></span>
                                        </div>
                                    </PagerTemplate>
                                    <PagerStyle BorderWidth="0px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:GridView>
                </td>
            </tr>
            
            <tr>
                <td>&nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="2" align="right">
                    
                                                  
                </td>
            </tr>
        </table>
            
            </ContentTemplate>
            </asp:UpdatePanel>
            <div style="text-align:right">
            <asp:Button ID="btnSave" runat="server" BackColor="Transparent" 
                                                  BorderStyle="None" CssClass="ButtonTemplate" Height="34px" 
                                                  onclientclick="this.value='Submitting...';  return true; this.disabled=true;" 
                                                  Text="Save" Width="91px" onclick="btnSave_Click" />
            </div>
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
</form>
</body>
</html>