<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="advisers_studentsview.aspx.cs" Inherits="PAOnlineAssessment.instructor.advisers_studentsview" %>
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
            height: 17px;
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
            <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">My Subject</span></h2>
            <center>
            <table >
                <tr>
                    <td  valign="top">
                        <asp:Panel ID="pHandler" runat="server" ScrollBars="Auto">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <span class="PageSubHeader" lang="en-ph">
                                    
                                        <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                                    
                                    <table>
                                        <tr>
                                            <td align="left" class="style7">
                            Teacher:&nbsp;<asp:DropDownList ID="cboTeacher" runat="server" AutoPostBack="True" CssClass="GridPagerButtons" 
                                onselectedindexchanged="cboTeacher_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                                            </td>
                                            <td align="right" >
                                                <asp:TextBox ID="txtSearchQuery" runat="server" 
                        CssClass="GridPagerButtons" Width="177px" Visible="False"></asp:TextBox>
                        
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="False" 
                                                    Font-Names="Tahoma" Font-Size="Small" Text="Quarter:"></asp:Label>
                                                
                                                <asp:DropDownList ID="cboQuarter" runat="server" CssClass="GridPagerButtons" 
                                                    AutoPostBack="True" Width="60px" 
                                                    onselectedindexchanged="cboQuarter_SelectedIndexChanged">
                                                    <asp:ListItem Value="1st">1st</asp:ListItem>
                                                    <asp:ListItem Value="2nd">2nd</asp:ListItem>
                                                    <asp:ListItem Value="3rd">3rd</asp:ListItem>
                                                    <asp:ListItem Value="4th">4th</asp:ListItem>
                                                </asp:DropDownList>
                                                    
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" 
                                                    Font-Names="Tahoma" Font-Size="Small" Text="Subjects: "></asp:Label>
                                                <asp:DropDownList ID="cboSubjects" runat="server" 
                        CssClass="GridPagerButtons" AutoPostBack="True" Width="150px"
                                                    onselectedindexchanged="cboSubjects_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:ImageButton ID="imgSearchQuery" runat="server" 
                        CssClass="GridPagerButtons" ImageUrl="~/images/icons/page_find.gif" ToolTip="View" Visible="False"  
                                                     />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <span class="PageSubHeader">
                                                <div style="overflow:scroll; width:720px">
                                                <asp:GridView ID="grdStudentsList" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="GridPagerButtons" PageSize="2" 
                                                    onrowcreated="grdStudentsList_RowCreated" UseAccessibleHeader="False" 
                                                    Width="700px">
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
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
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
                                    </table>
                                    </span>
                                    <br />
                                    <asp:GridView ID="grdView" runat="server" onrowcreated="grdView_RowCreated" 
                                        onrowdatabound="grdView_RowDataBound" 
                                        onselectedindexchanged="grdView_SelectedIndexChanged" 
                                        Width="700px" Visible="False">
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                                        <td >
                                            <asp:LinkButton ID="lbtnExport" runat="server" onclick="lbtnExport_Click" 
                                                Visible="False">Export To Excel</asp:LinkButton>
                                        </td>
                                   
                                    </tr>
                                    <tr>
                                        <td class="style1" align="center">
                                            &nbsp;</td>
                                    
                                    </tr>
            </table>
            </center>
    </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
