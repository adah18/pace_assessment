<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="review_assessment_list.aspx.cs" Inherits="PAOnlineAssessment.student.review_assessment_list" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assessment - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
        
            <table style="width:100%;">
                <tr>
                    <td>
                        <span class="PageHeader" lang="en-ph">Review Assessments</span></td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <uc3:SiteMap ID="SiteMap" runat="server" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <span class="PageSubHeader" lang="en-ph">Find Assessment</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSearchQuery" runat="server" CssClass="GridPagerButtons" 
                                                Width="175px"></asp:TextBox>
                                            <span lang="en-ph">&nbsp;</span><asp:ImageButton ID="imgSearchQuery" runat="server" 
                                                CssClass="GridPagerButtons" ImageUrl="~/images/icons/page_find.gif" 
                                                onclick="imgSearchQuery_Click" ToolTip="Search" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="PageSubHeader" lang="en-ph">List of Assessments you had Taken 
                                            already</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdAvailableAssessments" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" BorderStyle="None" GridLines="None" 
                                                onprerender="grdAvailableAssessments_PreRender" 
                                                onrowdatabound="grdAvailableAssessments_RowDataBound" PageSize="8" 
                                                ShowHeader="False" Width="700px">
                                                <PagerSettings Position="TopAndBottom" />
                                                <RowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Available Assessments">
                                                        <ItemTemplate>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style1" rowspan="4" style="width: 120px;">
                                                                        <center>
                                                                            <asp:ImageButton ID="imgTakeAssessment" runat="server" 
                                                                                CssClass="MediumButtonTemplate" Height="64px" 
                                                                                ImageUrl="~/images/dashboard_icons/accept_page.png" ToolTip="Take Assessment" 
                                                                                Width="64px" />
                                                                            <br />
                                                                            <span class="GridPagerButtons" lang="en-ph">Review<br />Assessment</span></center>
                                                                    </td>
                                                                    <td class="PageSubHeader">
                                                                        <asp:Label ID="lblSubjectDescription" runat="server" 
                                                                            CssClass="PageSubHeader" Text='<%# Eval("SubjectDescription") %>'></asp:Label>
                                                                        <span class="PageSubHeader"><span lang="en-ph">&nbsp;- </span></span>
                                                                        <asp:Label ID="lblTitle" runat="server" CssClass="PageSubHeader" 
                                                                            Text='<%# Eval("Title") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <asp:Label ID="lblIntroduction" runat="server" 
                                                                            CssClass="GridPagerButtons" Text='<%# Eval("Introduction") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblAssessmentID" runat="server" Text='<%# Eval("AssessmentID") %>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Exam Type:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <asp:Label ID="lblAssessmentTypeDescription" runat="server" 
                                                                                        CssClass="TextboxTemplate" Text='<%# Eval("AssessmentTypeDescription") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Date:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <asp:Label ID="lblDateStart" runat="server" CssClass="TextboxTemplate" 
                                                                                        Text='<%# Eval("DateStart") %>'></asp:Label>
                                                                                    <asp:Label ID="Label2" runat="server" CssClass="TextboxTemplate" 
                                                                                        Text="&amp;nbsp; to  &amp;nbsp;"></asp:Label>
                                                                                    <asp:Label ID="lblDateEnd" runat="server" CssClass="TextboxTemplate" 
                                                                                        Text='<%# Eval("DateEnd") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Time:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <asp:Label ID="lblTimeStart" runat="server" CssClass="TextboxTemplate" 
                                                                                        Text='<%# Eval("TimeStart") %>'></asp:Label>
                                                                                    <asp:Label ID="Label3" runat="server" CssClass="TextboxTemplate" 
                                                                                        Text="&amp;nbsp; to  &amp;nbsp;"></asp:Label>
                                                                                    <asp:Label ID="lblTimeEnd" runat="server" CssClass="TextboxTemplate" 
                                                                                        Text='<%# Eval("TimeEnd") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Teacher:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <asp:Label ID="lblTeacherFirstName" runat="server" CssClass="TextboxTemplate" 
                                                                                        Text='<%# Eval("TeacherFirstName") %>'></asp:Label>
                                                                                    <span lang="en-ph">&nbsp;</span><asp:Label ID="lblTeacherLastName" runat="server" 
                                                                                        CssClass="TextboxTemplate" Text='<%# Eval("TeacherLastName") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td class="style1" rowspan="4" style="width: 120px;">
                                                                        <center>
                                                                            <input type="image" name="grdAvailableAssessments$ctl04$ImageButton1" 
                                                    id="grdAvailableAssessments_ctl04_ImageButton1" title="Take Assessment" 
                                                    class="MediumButtonTemplate" Src="../images/dashboard_icons/pencil_page.png" 
                                                    style="height:64px;width:64px;border-width:0px;" />
                                                                            <br />
                                                                            <span class="GridPagerButtons" lang="en-ph">Take<br />Assessment</span></center>
                                                                    </td>
                                                                    <td class="style4">
                                                                        <span ID="grdAvailableAssessments_ctl04_lblSubjectDescription" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                        <span class="PageSubHeaderAlternate"><span lang="en-ph">&nbsp;- </span></span>
                                                                        <span ID="grdAvailableAssessments_ctl04_lblTitle" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span ID="grdAvailableAssessments_ctl04_lblIntroduction" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Exam Type:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl04_lblAssessmentTypeDescription" 
                                                                                        class="TextboxTemplate">Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Date:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl04_lblDateStart" class="TextboxTemplate">
                                                                                    Databound</span> <span ID="grdAvailableAssessments_ctl04_Label2" 
                                                                                        class="TextboxTemplate">&nbsp; to &nbsp;</span>
                                                                                    <span ID="grdAvailableAssessments_ctl04_lblDateEnd" class="TextboxTemplate">
                                                                                    Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Time:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl04_lblTimeStart" class="TextboxTemplate">
                                                                                    Databound</span> <span ID="grdAvailableAssessments_ctl04_Label3" 
                                                                                        class="TextboxTemplate">&nbsp; to &nbsp;</span>
                                                                                    <span ID="grdAvailableAssessments_ctl04_lblTimeEnd" class="TextboxTemplate">
                                                                                    Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Teacher:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl04_lblTeacherFirstName" 
                                                                                        class="TextboxTemplate">Databound</span> <span lang="en-ph">&nbsp;</span><span 
                                                                                        ID="grdAvailableAssessments_ctl04_lblTeacherLastName" class="TextboxTemplate">Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style1" rowspan="4" style="width: 120px;">
                                                                        <center>
                                                                            <input type="image" name="grdAvailableAssessments$ctl06$ImageButton1" 
                    id="grdAvailableAssessments_ctl06_ImageButton1" title="Take Assessment" 
                    class="MediumButtonTemplate" Src="../images/dashboard_icons/pencil_page.png" 
                    style="height:64px;width:64px;border-width:0px;" />
                                                                            <br />
                                                                            <span class="GridPagerButtons" lang="en-ph">Take<br />Assessment</span></center>
                                                                    </td>
                                                                    <td class="style4">
                                                                        <span ID="grdAvailableAssessments_ctl06_lblSubjectDescription" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                        <span class="PageSubHeaderAlternate"><span lang="en-ph">&nbsp;- </span></span>
                                                                        <span ID="grdAvailableAssessments_ctl06_lblTitle" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span ID="grdAvailableAssessments_ctl06_lblIntroduction" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Exam Type:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl06_lblAssessmentTypeDescription" 
                                                                                        class="TextboxTemplate">Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Date:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl06_lblDateStart" class="TextboxTemplate">
                                                                                    Databound</span> <span ID="grdAvailableAssessments_ctl06_Label2" 
                                                                                        class="TextboxTemplate">&nbsp; to &nbsp;</span>
                                                                                    <span ID="grdAvailableAssessments_ctl06_lblDateEnd" class="TextboxTemplate">
                                                                                    Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Time:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl06_lblTimeStart" class="TextboxTemplate">
                                                                                    Databound</span> <span ID="grdAvailableAssessments_ctl06_Label3" 
                                                                                        class="TextboxTemplate">&nbsp; to &nbsp;</span>
                                                                                    <span ID="grdAvailableAssessments_ctl06_lblTimeEnd" class="TextboxTemplate">
                                                                                    Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Teacher:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl06_lblTeacherFirstName" 
                                                                                        class="TextboxTemplate">Databound</span> <span lang="en-ph">&nbsp;</span><span 
                                                                                        ID="grdAvailableAssessments_ctl06_lblTeacherLastName" class="TextboxTemplate">Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <HeaderTemplate>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style1" rowspan="4" style="width: 120px;">
                                                                        <center>
                                                                            <input type="image" name="grdAvailableAssessments$ctl05$ImageButton1" 
                    id="grdAvailableAssessments_ctl05_ImageButton1" title="Take Assessment" 
                    class="MediumButtonTemplate" Src="../images/dashboard_icons/pencil_page.png" 
                    style="height:64px;width:64px;border-width:0px;" />
                                                                            <br />
                                                                            <span class="GridPagerButtons" lang="en-ph">Take<br />Assessment</span></center>
                                                                    </td>
                                                                    <td class="style4">
                                                                        <span ID="grdAvailableAssessments_ctl05_lblSubjectDescription" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                        <span class="PageSubHeaderAlternate"><span lang="en-ph">&nbsp;- </span></span>
                                                                        <span ID="grdAvailableAssessments_ctl05_lblTitle" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style4">
                                                                        <span ID="grdAvailableAssessments_ctl05_lblIntroduction" 
                                                                            class="PageSubHeaderAlternate">Databound</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Exam Type:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl05_lblAssessmentTypeDescription" 
                                                                                        class="TextboxTemplate">Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Date:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl05_lblDateStart" class="TextboxTemplate">
                                                                                    Databound</span> <span ID="grdAvailableAssessments_ctl05_Label2" 
                                                                                        class="TextboxTemplate">&nbsp; to &nbsp;</span>
                                                                                    <span ID="grdAvailableAssessments_ctl05_lblDateEnd" class="TextboxTemplate">
                                                                                    Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Time:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl05_lblTimeStart" class="TextboxTemplate">
                                                                                    Databound</span> <span ID="grdAvailableAssessments_ctl05_Label3" 
                                                                                        class="TextboxTemplate">&nbsp; to &nbsp;</span>
                                                                                    <span ID="grdAvailableAssessments_ctl05_lblTimeEnd" class="TextboxTemplate">
                                                                                    Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style3">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Teacher:</span></td>
                                                                                <td class="style2">
                                                                                    &nbsp;</td>
                                                                                <td class="TextboxTemplate">
                                                                                    <span ID="grdAvailableAssessments_ctl05_lblTeacherFirstName" 
                                                                                        class="TextboxTemplate">Databound</span> <span lang="en-ph">&nbsp;</span><span 
                                                                                        ID="grdAvailableAssessments_ctl05_lblTeacherLastName" class="TextboxTemplate">Databound</span>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="Label4" runat="server" CssClass="GridPagerButtons" 
                                                                    Text="Assessments per Page:"></asp:Label>
                                                                <span lang="en-ph">&nbsp;</span><asp:DropDownList ID="cboAssessmentPerPage" 
                                                                    runat="server" AutoPostBack="True" CssClass="GridPagerButtons" 
                                                                    onselectedindexchanged="cboAssessmentPerPage_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="GridPagerButtons" style="text-align: right;">
                                                                <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                                                    onclick="lnkFirst_Click" ToolTip="First page">First</asp:LinkButton>
                                                                <span lang="en-ph">&nbsp;| </span>
                                                                <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="GridPagerButtons" 
                                                                    onclick="lnkPrevious_Click" ToolTip="Previous page">Previous</asp:LinkButton>
                                                                <span lang="en-ph">&nbsp;| </span>
                                                                <asp:Label ID="Label5" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                                                <span lang="en-ph">&nbsp;</span><asp:DropDownList ID="cboPageNumber" runat="server" 
                                                                    AutoPostBack="True" CssClass="GridPagerButtons" 
                                                                    onselectedindexchanged="cboPageNumber_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <span lang="en-ph">&nbsp;</span><asp:Label ID="lblPageCount" runat="server" 
                                                                    Text="of 0"></asp:Label>
                                                                <span lang="en-ph">&nbsp;| </span>
                                                                <asp:LinkButton ID="lnkNext" runat="server" CssClass="GridPagerButtons" 
                                                                    onclick="lnkNext_Click" ToolTip="Next page">Next</asp:LinkButton>
                                                                <span lang="en-ph">&nbsp;| </span>
                                                                <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                                                    onclick="lnkLast_Click" ToolTip="Last page">Last</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </PagerTemplate>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span class="PageSubHeaderAlternate" lang="en-ph">No Assessment Found</span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <AlternatingRowStyle BackColor="#F4F4F4" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" /> 
    </form>
</body>
</html>
