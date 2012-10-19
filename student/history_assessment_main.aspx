<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="history_assessment_main.aspx.cs" Inherits="PAOnlineAssessment.student.history_assessment_main" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assessment History - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <h2 style="background-color: #fff"><span class="PageHeader" lang="en-ph">Assessment History</span></h2>
            <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table >
                    <tr>
                        <td><uc3:SiteMap ID="SiteMap" runat="server" Visible="false" /></td>
                    </tr>
                    <tr> 
                        <td align="right">
                            <span class="FieldTitle" lang="en-ph">Quarter:</span>&nbsp<asp:DropDownList 
                                ID="ddlQuarter" runat="server" AutoPostBack="True"  
                                CssClass="GridPagerButtons" 
                                onselectedindexchanged="ddlQuarter_SelectedIndexChanged" Width="60px">
                                <asp:ListItem>1st</asp:ListItem>
                                <asp:ListItem>2nd</asp:ListItem>
                                <asp:ListItem>3rd</asp:ListItem>
                                <asp:ListItem>4th</asp:ListItem>
                            </asp:DropDownList>
                           <asp:TextBox ID="txtSearch" runat="server" Width="208px" 
                                CssClass="GridPagerButtons"></asp:TextBox>
                           <asp:DropDownList
                                ID="ddlSearch" runat="server" CssClass="GridPagerButtons" Width="100px">
                                    <asp:ListItem>Subject</asp:ListItem>
                                    <asp:ListItem>Title</asp:ListItem>
                                    <asp:ListItem>Taken</asp:ListItem>
                                    <asp:ListItem>Not Taken</asp:ListItem>
                                </asp:DropDownList>
                           <asp:ImageButton ID="imgSearchQuery" runat="server" 
                                CssClass="GridPagerButtons" ImageUrl="~/images/icons/page_find.gif" 
                                onclick="imgSearchQuery_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="dgAssessments" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" PageSize="20" Width="700px" 
                                onrowdatabound="dgAssessments_RowDataBound">
                                <Columns>
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                        
                                            <asp:GridView ID="dgAssessmentType" runat="server" AutoGenerateColumns="False" 
                                                Width="700px" onrowdatabound="dgAssessmentType_RowDataBound" 
                                                PageSize="100">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                                <table style="width: 100%;">
                                                                <tr align="center">
                                                                    <td rowspan="3" align="center" class="style1">
                                                                        <center>
                                                                            <asp:ImageButton ID="imgTakeAssessment" runat="server" 
                                                                                CssClass="MediumButtonTemplate" Height="61px" 
                                                                                ImageUrl="~/images/dashboard_icons/accept_page.png" ToolTip="Review Assessment" 
                                                                                Width="62px" />
                                                                                <br />
                                                                            <span class="GridPagerButtons" lang="en-ph">View<br />Assessment</span></center>
                                                                            
                                                                    </td>
                                                                    <td class="PageSubHeader" align="left">
                                                                        <asp:Label ID="lblSubjectDescription" runat="server" 
                                                                            CssClass="PageSubHeader" Text='<%# Eval("SubjectDescription") %>'></asp:Label>
                                                                        <span class="PageSubHeader"><span lang="en-ph">&nbsp;- </span></span>
                                                                        <asp:Label ID="lblTitle" runat="server" CssClass="PageSubHeader" 
                                                                            Text='<%# Eval("Title") %>'></asp:Label>
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
                                                                                <td class="style3" align="left">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Date:</span></td>
                                                                                
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
                                                                                <td class="style3" align="left">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Scheduled Time:</span></td>
                                                                                
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
                                                                            <td class="style3" align="left">
                                                                                    <span class="TextboxTemplate" lang="en-ph">Status:</span></td>
                                                                                
                                                                                <td class="TextboxTemplate">
                                                                                    <asp:Label ID="Label1" runat="server" CssClass="TextboxTemplate" 
                                                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                        </ItemTemplate>
                                                        <HeaderStyle />
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <center><span class="PageSubHeaderAlternate" lang="en-ph">No Assessment Found</span></center></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" Width="700px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            </center>
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
