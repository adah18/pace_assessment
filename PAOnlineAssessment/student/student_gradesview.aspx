<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student_gradesview.aspx.cs" Inherits="PAOnlineAssessment.student.student_gradesview" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>
<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>View Grades - Pace Academy Online Assessment System</title>
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
        </style>
</head>
<body>
    <form id="form1" runat="server">   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager> 
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
 
        <div id="bodytopPan" class="style3">
        
            <table style="width:100%;">
                <tr>
                    <td>
                        <span lang="en-ph" class="PageHeader">My Subjects</span></td>
                </tr>
                <tr>
                    <td class="style6" valign="top">
                        <asp:Panel ID="pHandler" runat="server" ScrollBars="Auto">
                        <center>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <span class="PageSubHeader" lang="en-ph">
                                    <p>
                                        <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                                    </p>
                                    <br />
                                    <table>
                                        <tr>
                                            <td align="right">
                                                <asp:TextBox ID="txtSearchQuery" runat="server" 
                        CssClass="GridPagerButtons" Width="177px" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="cboQuarter" runat="server" CssClass="GridPagerButtons" 
                                                    AutoPostBack="True" Width="60px" 
                                                    onselectedindexchanged="cboQuarter_SelectedIndexChanged">
                                                    <asp:ListItem Value="1st">1st</asp:ListItem>
                                                    <asp:ListItem Value="2nd">2nd</asp:ListItem>
                                                    <asp:ListItem Value="3rd">3rd</asp:ListItem>
                                                    <asp:ListItem Value="4th">4th</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="PageSubHeader" lang="en-ph">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" 
                                                    Font-Names="Tahoma" Font-Size="Small" Text="Subjects: "></asp:Label>
                                                </span>
                                                <asp:DropDownList ID="cboSubjects" runat="server" 
                        CssClass="GridPagerButtons" AutoPostBack="True" Height="16px" 
                                                    onselectedindexchanged="cboSubjects_SelectedIndexChanged" Width="150px">
                                                </asp:DropDownList>
                                                <asp:ImageButton ID="imgSearchQuery" runat="server" 
                        CssClass="GridPagerButtons" ImageUrl="~/images/icons/page_find.gif" ToolTip="Search" Visible="False" 
                                                    onclick="imgSearchQuery_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="PageSubHeader" lang="en-ph">
                                                <span class="PageSubHeader">
                                                <asp:GridView ID="grdStudentsList_1st" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="GridPagerButtons" PageSize="1" ShowHeader="True" 
                                                    UseAccessibleHeader="False" Width="700px">
                                                    <PagerSettings Position="TopAndBottom" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                    <asp:GridView ID="grdScores" runat="server" AutoGenerateColumns="False" 
                                                                        Width="700px">
                                                                        <columns>
                                                                            <asp:TemplateField HeaderText="Assessment Title">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStubitems" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="40%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pts.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblScore0" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date Taken">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldatetaken" runat="server" Text='<%# Bind("DateTaken") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Quarter" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblqtr" runat="server" Text='<%# Bind("Quarter") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                        </columns>
                                                                        <EmptyDataTemplate>
                                                                            <table width="100%">
                                                                                <tr style="BackGround: white;">
                                                                                    <td align="center">
                                                                                        <span style="color: #6abd78;">No Record Found</span>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </center>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                            <HeaderStyle BackColor="#6abd78" BorderColor="Black" BorderStyle="Solid" 
                                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                                Wrap="False" />
                                                            <ItemStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                                ForeColor="white" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <center>
                                                            No Assessment found.
                                                        </center>
                                                        <br />
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                <asp:GridView ID="grdStudentsList_2nd" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="GridPagerButtons" PageSize="1" ShowHeader="True" 
                                                    UseAccessibleHeader="False" Width="700px">
                                                    <PagerSettings Position="TopAndBottom" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                    <asp:GridView ID="grdScores" runat="server" AutoGenerateColumns="False" 
                                                                        Width="700px">
                                                                        <columns>
                                                                            <asp:TemplateField HeaderText="Assessment Title">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStubitems" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="40%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pts.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblScore0" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date Taken">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldatetaken" runat="server" Text='<%# Bind("DateTaken") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Quarter">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblqtr" runat="server" Text='<%# Bind("Quarter") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                        </columns>
                                                                        <EmptyDataTemplate>
                                                                            <table width="100%">
                                                                                <tr style="BackGround: white;">
                                                                                    <td align="center">
                                                                                        <span style="color: #6abd78;">No Record Found</span>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </center>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                            <HeaderStyle BackColor="#6abd78" BorderColor="Black" BorderStyle="Solid" 
                                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                                Wrap="False" />
                                                            <ItemStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                                ForeColor="white" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <center>
                                                            No Assessment found.
                                                        </center>
                                                        <br />
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                <asp:GridView ID="grdStudentsList_3rd" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="GridPagerButtons" PageSize="1" ShowHeader="True" 
                                                    UseAccessibleHeader="False" Width="700px">
                                                    <PagerSettings Position="TopAndBottom" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                    <asp:GridView ID="grdScores" runat="server" AutoGenerateColumns="False" 
                                                                        Width="700px">
                                                                        <columns>
                                                                            <asp:TemplateField HeaderText="Assessment Title">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStubitems" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="40%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pts.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblScore0" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date Taken">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldatetaken" runat="server" Text='<%# Bind("DateTaken") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Quarter" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblqtr" runat="server" Text='<%# Bind("Quarter") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                        </columns>
                                                                        <EmptyDataTemplate>
                                                                            <table width="100%">
                                                                                <tr style="BackGround: white;">
                                                                                    <td align="center">
                                                                                        <span style="color: #6abd78;">No Record Found</span>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </center>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                            <HeaderStyle BackColor="#6abd78" BorderColor="Black" BorderStyle="Solid" 
                                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                                Wrap="False" />
                                                            <ItemStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                                ForeColor="white" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <center>
                                                            No Assessment found.
                                                        </center>
                                                        <br />
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                <asp:GridView ID="grdStudentsList_4th" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="GridPagerButtons" PageSize="1" ShowHeader="True" 
                                                    UseAccessibleHeader="False" Width="700px">
                                                    <PagerSettings Position="TopAndBottom" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                    <asp:GridView ID="grdScores" runat="server" AutoGenerateColumns="False" 
                                                                        Width="700px">
                                                                        <columns>
                                                                            <asp:TemplateField HeaderText="Assessment Title">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStubitems" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="40%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Pts.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblScore0" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date Taken">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldatetaken" runat="server" Text='<%# Bind("DateTaken") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Quarter" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblqtr" runat="server" Text='<%# Bind("Quarter") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                    HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                    ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" 
                                                                                    Wrap="False" />
                                                                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                    Wrap="False" />
                                                                            </asp:TemplateField>
                                                                        </columns>
                                                                        <EmptyDataTemplate>
                                                                            <table width="100%">
                                                                                <tr style="BackGround: white;">
                                                                                    <td align="center">
                                                                                        <span style="color: #6abd78;">No Record Found</span>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </center>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                            <HeaderStyle BackColor="#6abd78" BorderColor="Black" BorderStyle="Solid" 
                                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                                Wrap="False" />
                                                            <ItemStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                                ForeColor="white" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                Wrap="False" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <center>
                                                            No Assessment found.
                                                        </center>
                                                        <br />
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                </span>
                                                </span>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        
                                        
                                       
                                    </table>
                                    </span>
                                    <br />
                                    <asp:GridView ID="grdView" runat="server" Visible="False">
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </center>
                        </asp:Panel>
                     
                    </td>
                </tr>
                <tr>
                                        <td >
                                            &nbsp;</td>
                                   
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
