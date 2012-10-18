<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="makeup_exam_list.aspx.cs" Inherits="PAOnlineAssessment.instructor.makeup_exam_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Make Up Exam - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 87px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            <h2 style="background-color: #FFFFFF"><span class="PageHeader" lang="en-ph">Make-Up Exams</span></h2>
            <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                    <table style="width: 500px;">
                    <tr>
                        <td>
                        <span class="PageSubHeader" lang="en-ph">Course Details</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" valign="top">
                        <span class="FieldTitle" lang="en-ph">Teacher:</span>
                        </td>
                        <td>
                            <span class="LoginSubNote" lang="en-ph">
                            <asp:DropDownList ID="cboTeacher" runat="server" AutoPostBack="True" 
                                CssClass="GridPagerButtons" 
                                onselectedindexchanged="cboTeacher_SelectedIndexChanged" Width="175px">
                            </asp:DropDownList>
                            </span>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Select a teacher</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" valign="top">
                        <span class="FieldTitle" lang="en-ph">Grade / Level:</span>
                        </td>
                        <td>
                        <asp:DropDownList ID="cboGradeLevel" runat="server" CssClass="GridPagerButtons" 
                                Width="175px" AutoPostBack="True" 
                                onselectedindexchanged="cboGradeLevel_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Grade / Level of the students you want to view.</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" valign="top">
                        <span class="FieldTitle" lang="en-ph">Section:</span>
                        </td>
                        <td>
                        <asp:DropDownList ID="cboSection" runat="server" CssClass="GridPagerButtons" 
                                Width="175px" onselectedindexchanged="cboSection_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Section of the students you want to view.</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" valign="top">
                        <span class="FieldTitle" lang="en-ph">Subject:</span>
                        </td>
                        <td >
                        <asp:DropDownList ID="cboSubject" runat="server" CssClass="GridPagerButtons" 
                                Width="175px" 
                                onselectedindexchanged="cboSubject_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Subject of the students you want to view.</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" valign="top">
                        <span class="FieldTitle" lang="en-ph">Assessment Type:</span>
                        </td>
                        <td >
                        <asp:DropDownList ID="cboAssessmentType" runat="server" CssClass="GridPagerButtons" 
                                Width="175px" AutoPostBack="True" 
                                onselectedindexchanged="cboAssessmentType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Type of Assesment you want to view.</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" valign="top">
                        <span class="FieldTitle" lang="en-ph">Assessment:</span>
                        </td>
                        <td>
                        <asp:DropDownList ID="cboAssessment" runat="server" CssClass="GridPagerButtons" 
                                Width="175px" >
                        </asp:DropDownList>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Select an Assessment you want to view.</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                             <asp:LinkButton ID="lnkView" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="87px" onclick="lnkView_Click">View</asp:LinkButton>
                        </td>
                    </tr>
                    </table>
                    </td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <span class="PageSubHeader" lang="en-ph">Student List</span> &nbsp;
                        <font color="red"><asp:Label ID="lblError" runat="server" Text="*"></asp:Label></font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="dgStudent" runat="server" CssClass="GridPagerButtons" 
                            AutoGenerateColumns="False" Width="700px" 
                            onrowdatabound="dgStudent_RowDataBound" PageSize="100">
                            <Columns>
                                <asp:TemplateField HeaderText="Selection" ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" StudentID='<%# Eval("StudentID") %>' />
                                        
                                        <asp:Label ID="lblStudentNumber" runat="server" Visible="false" Text='<%# Eval("StudentNumber") %>'></asp:Label>
                                        <asp:Label ID="lblStudentID" runat="server" Visible="false" Text='<%# Eval("StudentID") %>'></asp:Label>
                                    </ItemTemplate>
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                Wrap="False" />
                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastName" runat="server" CssClass="GridPagerButtons"><%# Eval("LastName") %></asp:Label>
                                    </ItemTemplate>
                                
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirstName" runat="server" CssClass="GridPagerButtons"><%# Eval("FirstName") %></asp:Label>
                                    </ItemTemplate>
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" CssClass="GridPagerButtons" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssessmentStatus" runat="server" CssClass="GridPagerButtons" Text='<%# Eval("AssessmentStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <center>
                                <br />
                                <span class="PageSubHeaderAlternate" lang="en-ph">No Record Found.</span>
                                <br />
                                <span class="PageSubHeaderAlternate" lang="en-ph">Check if the assessment was closed.</span>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        
                    </td>
                </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            <table style="width: 100%;">
                <tr>
                    <td align="right">
                        <asp:LinkButton ID="lnkOpen" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="87px" ToolTip="Open Assessment" 
                            onclick="lnkOpen_Click">Open</asp:LinkButton>
                        <asp:LinkButton ID="lnkClose" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="87px" ToolTip="Close Assessment" 
                            onclick="lnkClose_Click">Close</asp:LinkButton>
                    </td>
                </tr>
            </table>
            </center>
   </div>
   </div>
   <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
