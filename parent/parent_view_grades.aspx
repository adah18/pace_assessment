<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="parent_view_grades.aspx.cs" Inherits="PAOnlineAssessment.parent.parent_view_grades" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>View Grades - Pace Academy Online Assessment System</title>
<link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
<%--<script type="text/javascript" src="../scripts/jquery/parent_js/cufon-yui.js"></script>
<script type="text/javascript" src="../scripts/jquery/parent_js/georgia.js"></script>
<script type="text/javascript" src="../scripts/jquery/parent_js/cuf_run.js"></script>
--%>
<link rel="stylesheet" type="text/css" href="parent_menu/pro_drop_1.css" />

<script src="parent_menu/stuHover.js" type="text/javascript"></script>
    <style type="text/css">
        .style6
        {
            height: 18px;
        }
        .style7
        {
            height: 36px;
        }
        .style8
        {
            height: 35px;
        }
        </style>
</head>
<body> 
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<uc1:frmHeader ID="frmHeader1" runat="server" />
<div id="bodytopmainPan">
        <div id="bodytopPan">
            <h2><span class="PageHeader" >Child's Grades</span></h2>
            
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                
                <table>
                    <tr>
                        <td>
                            <span class="FieldTitle" lang="en-ph">Child's Name </span>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboChild" runat="server" CssClass="GridPagerButtons"  
                                                            Width="200px" Height="16px" AutoPostBack="True" 
                                                            onselectedindexchanged="cboChild_SelectedIndexChanged">
                                                        </asp:DropDownList>
                            <span lang="en-ph">&nbsp;</span><asp:Label 
                                                                        ID="vlGradeLevel" runat="server" 
                                                                        CssClass="ValidationNotice" Text="*"></asp:Label>                             
                            <br />
                            <span class="LoginSubNote"><span lang="en-ph">Select a name to view your child&#39;s 
                                            grade</span></span>
                            <asp:HiddenField ID="hidSection" runat="server" />
                            <asp:HiddenField ID="hidLevel" runat="server" />   
                        </td>
                        
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <span class="FieldTitle" lang="en-ph">Subject </span>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboSubjects" runat="server" CssClass="GridPagerButtons" 
                                                           Width="200px" 
                                                              onselectedindexchanged="cboSubjects_SelectedIndexChanged" Height="18px">
                                                        </asp:DropDownList>                                         
                                                    <span lang="en-ph">&nbsp;</span>
                          <asp:Label 
                                                                        ID="Label1" runat="server" 
                                                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                            <br />
                            <span class="LoginSubNote"><span lang="en-ph">Select a subject you want to view 
                                            the grades.</span></span>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <span class="FieldTitle" lang="en-ph">Quarter </span>
                        </td>
                        <td>
                            <span lang="en-ph">
                                                    <asp:DropDownList ID="cboQuarter" runat="server" AutoPostBack="false" 
                                                        CssClass="GridPagerButtons" Height="18px" Width="100px">
                                                        <asp:ListItem Value="1st">1st</asp:ListItem>
                                                        <asp:ListItem Value="2nd">2nd</asp:ListItem>
                                                        <asp:ListItem Value="3rd">3rd</asp:ListItem>
                                                        <asp:ListItem Value="4th">4th</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </span><asp:Label 
                                                                        ID="Label2" runat="server" 
                                                                        CssClass="ValidationNotice" Text="*"></asp:Label>
                            <br />
                            <span class="LoginSubNote"><span lang="en-ph">Select a quarter you want to view the grades.</span></span>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                                                  BorderStyle="None" CssClass="ButtonTemplate" Height="26px"  
                                                  Text="View" Width="100px" onclick="btnSubmit_Click" ToolTip="View Grades"  />
                            <asp:HiddenField ID="hidStudentNumber" runat="server" />               
                        </td>
                        <td style="width: 45%">&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                                 
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grdStudentsList" 
                                                            runat="server" AutoGenerateColumns="False" 
                                                            CssClass="GridPagerButtons"
                                                            PageSize="1" UseAccessibleHeader="False" 
                                Width="700px" ShowHeader="False">
                                                            <PagerSettings Position="TopAndBottom" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="1st Quarter">
                                                                    <ItemTemplate>
                                                                        <center>
                                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                            <asp:GridView ID="grdScores" runat="server" AutoGenerateColumns="False" 
                                                                                Width="700px" PageSize="100">
                                                                                <columns>
                                                                                    <asp:TemplateField HeaderText="Assessment Title">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblStubitems" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                            HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                        <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                            ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                            Wrap="False" Width="40%" />
                                                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                                                            Wrap="False" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Pts.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" 
                                                                                            HorizontalAlign="Center" VerticalAlign="Bottom" />
                                                                                        <HeaderStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="None" 
                                                                                            ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                            Wrap="False" Width="5%" />
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
                                                                                            ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                            Wrap="False" Width="5%" />
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
                                                                                            ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                            Wrap="False"  Width="20%" />
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
                                                                                            ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                            Wrap="False"  Width="20%" />
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
                                                                                            ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                            Wrap="False"  Width="10%" />
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
                            <asp:GridView ID="grdStudentsList_2nd" runat="server" 
                                                            AutoGenerateColumns="False" 
                                CssClass="GridPagerButtons" PageSize="1" 
                                                            UseAccessibleHeader="False" Width="700px" 
                                ShowHeader="False">
                                                            <PagerSettings Position="TopAndBottom" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="2nd Quarter">
                                                                    <ItemTemplate>
                                                                        <center>
                                                                            <asp:Label ID="lblTitle_2nd" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                            <asp:GridView ID="grdScores_2nd" runat="server" AutoGenerateColumns="False" 
                                                                                Width="700px" PageSize="100">
                                                                                <columns>
                                                                                    <asp:TemplateField HeaderText="Assessment Title">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblStubitems_2nd" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblScore_2nd" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblScore_2nd" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblstatus_2nd" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lbldatetaken_2nd" runat="server" Text='<%# Bind("DateTaken") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblqtr_2nd" runat="server" Text='<%# Bind("Quarter") %>'></asp:Label>
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
                        
                        <asp:GridView ID="grdStudentsList_3rd" runat="server" 
                                                            AutoGenerateColumns="False" 
                                CssClass="GridPagerButtons" PageSize="1" 
                                                            UseAccessibleHeader="False" Width="700px" 
                                ShowHeader="False">
                                                            <PagerSettings Position="TopAndBottom" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="3rd Quarter">
                                                                    <ItemTemplate>
                                                                        <center>
                                                                            <asp:Label ID="lblTitle_3rd" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                            <asp:GridView ID="grdScores_3rd" runat="server" AutoGenerateColumns="False" 
                                                                                Width="700px" PageSize="100">
                                                                                <columns>
                                                                                    <asp:TemplateField HeaderText="Assessment Title">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblStubitems_3rd" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblScore_3rd" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblScore_3rd" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblstatus_3rd" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lbldatetaken_3rd" runat="server" Text='<%# Bind("DateTaken") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblqtr_3rd" runat="server" Text='<%# Bind("Quarter") %>'></asp:Label>
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
                            <asp:GridView ID="grdStudentsList_4th" runat="server" 
                                                            AutoGenerateColumns="False" 
                                CssClass="GridPagerButtons" PageSize="1" 
                                                            UseAccessibleHeader="False" Width="700px" 
                                ShowHeader="False">
                                                            <PagerSettings Position="TopAndBottom" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="4th Quarter">
                                                                    <ItemTemplate>
                                                                        <center>
                                                                            <asp:Label ID="lblTitle_4th" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                                                            <asp:GridView ID="grdScores_4th" runat="server" AutoGenerateColumns="False" 
                                                                                Width="700px" PageSize="100">
                                                                                <columns>
                                                                                    <asp:TemplateField HeaderText="Assessment Title">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblStubitems_4th" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblScore_4th" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblScore_4th" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblstatus_4th" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lbldatetaken_4th" runat="server" Text='<%# Bind("DateTaken") %>'></asp:Label>
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
                                                                                            <asp:Label ID="lblqtr_4th" runat="server" Text='<%# Bind("Quarter") %>'></asp:Label>
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
                        </td>
                    </tr>
                    
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr><td>&nbsp;</td></tr>
                </table>
                
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
</form>
</body>
</html>