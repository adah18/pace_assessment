<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assessment_evaluation.aspx.cs" Inherits="PAOnlineAssessment.instructor.assessment_evaluation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assessment Evaluation - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 25%;
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
            <h2 style="background-color: #FFFFFF"><span class="PageHeader" lang="en-ph">Assessment Evaluation</span></h2>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <center>
            <table style="width: 100%;">
                <tr>
                    <td>
                    <table style="width: 500px;">
                    <tr>
                        <td class="style1">
                        <span class="PageSubHeader" lang="en-ph">Course Details</span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style1">
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
                        <td valign="top" class="style1">
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
                        <td valign="top" class="style1">
                        <span class="FieldTitle" lang="en-ph">Section:</span>
                        </td>
                        <td>
                        <asp:DropDownList ID="cboSection" runat="server" CssClass="GridPagerButtons" 
                                Width="175px" onselectedindexchanged="cboSection_SelectedIndexChanged" 
                                AutoPostBack="True">
                        </asp:DropDownList>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Section of the students you want to view.</span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style1">
                        <span class="FieldTitle" lang="en-ph">Subject:</span>
                        </td>
                        <td >
                        <asp:DropDownList ID="cboSubject" runat="server" CssClass="GridPagerButtons" 
                                Width="175px" 
                                onselectedindexchanged="cboSubject_SelectedIndexChanged" 
                                AutoPostBack="True">
                        </asp:DropDownList>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Subject of the students you want to view.</span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style1">
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
                        <td valign="top" class="style1">
                        <span class="FieldTitle" lang="en-ph">Assessment:</span>
                        </td>
                        <td>
                        <asp:DropDownList ID="cboAssessment" runat="server" CssClass="GridPagerButtons" 
                                Width="175px" AutoPostBack="True" >
                        </asp:DropDownList>
                        <br />
                        <span class="LoginSubNote" lang="en-ph">Select an Assessment you want to view.</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                             <asp:Label ID="Label1" runat="server" Text="0" Visible="False"></asp:Label>
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
                        <span class="PageSubHeader" lang="en-ph">Evaluation Report</span> &nbsp;
                        <font color="red"><asp:Label ID="lblError" runat="server" Text="*" 
                            Visible="False"></asp:Label></font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="dgStudent" runat="server" CssClass="GridPagerButtons" 
                            AutoGenerateColumns="False" Width="700px" PageSize="100">
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowCount" runat="server" CssClass="GridPagerButtons" 
                                            Text='<%# Eval("RowCount") + ". " %>'></asp:Label>
                                    </ItemTemplate>
                                
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" Width="30px" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Question">
                                    <ItemTemplate>
                                        
                                        <asp:Label ID="lblQuestion" runat="server" CssClass="GridPagerButtons"><%# Eval("Question") %></asp:Label>
                                    </ItemTemplate>
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Left" VerticalAlign="Middle" 
                                        Width="570px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Correct">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCorrect" runat="server" CssClass="GridPagerButtons"><%# Eval("Correct") + " %" %></asp:Label>
                                    </ItemTemplate>
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Incorrect">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInCorrect" runat="server" CssClass="GridPagerButtons" 
                                            Text='<%# Eval("Incorrect") + " %" %>'></asp:Label>
                                    </ItemTemplate>
                                
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <center>
                                <br />
                                <span class="PageSubHeaderAlternate" lang="en-ph">No Record Found.</span>
                                <br />
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">Summary:</span>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">The easiest question(s) that most students answered correctly is/are: &nbsp;
                        <asp:Label ID="lblEasy" runat="server" Text="0"></asp:Label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">
                        The most difficult question(s) that most students answered incorrectly is/are: &nbsp;
                        <asp:Label ID="lblHard" runat="server" Text="0"></asp:Label>
                        </span>
                    </td>
                </tr>
                
                <tr>
                    <td>
                    &nbsp;
                        <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <span class="PageSubHeader" lang="en-ph">Students' Grade Report</span>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">Note: This table show the summary report 
                        of the students grade in the entire class.</span>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <asp:GridView ID="dgPercentage" runat="server" CssClass="GridPagerButtons" 
                            AutoGenerateColumns="False" Width="700px" PageSize="20">
                            <Columns>
                                <asp:TemplateField HeaderText="Range">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowCount" runat="server" CssClass="GridPagerButtons" 
                                            Text='<%# Eval("FirstRange") + " - " + Eval("LastRange") %>'></asp:Label>
                                    </ItemTemplate>
                                
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Number of Students">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowCount" runat="server" CssClass="GridPagerButtons" 
                                            Text='<%# Eval("Count") %>'></asp:Label>
                                    </ItemTemplate>
                                
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Student Percentage (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowCount" runat="server" CssClass="GridPagerButtons" 
                                            Text='<%# Eval("Percentage") %>'></asp:Label>
                                    </ItemTemplate>
                                
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" />
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
                    <td>
                        <span class="FieldTitle" lang="en-ph">Summary:</span>
                    </td>
                </tr>
                
                <tr>
                
                    <td><span class="FieldTitle" lang="en-ph">The total number of students in the class: 
                        &nbsp; <asp:Label ID="Label3" runat="server" Text="0"></asp:Label></span></td>
                
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
                
                
            </table>
            </center>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
