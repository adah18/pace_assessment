
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instructor_subjects.aspx.cs" Inherits="PAOnlineAssessment.instructor.instructor_subjects" %>
<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>
<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>
<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Teacher's Subject(s) - Pace Academy Online Assessment System</title>
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
            <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">My Subjects</span></h2>
            <table style="width:100%;">
                <tr>
                    <td class="style6" valign=top>
                  <center>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
                  <span lang="en-ph" class="PageSubHeader">
                  
                      <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                  
                    <table>
                    <tr>
                        <td>
                            Teacher:<asp:DropDownList ID="cboTeacher" runat="server" AutoPostBack="True" CssClass="GridPagerButtons" 
                                onselectedindexchanged="cboTeacher_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtSearchQuery" runat="server" Width="177px" 
                                CssClass="GridPagerButtons"></asp:TextBox>
                            <asp:DropDownList ID="cboSearchQuery" runat="server" 
                                CssClass="GridPagerButtons" Height="16px" Width="93px">
                                <asp:ListItem Value="Subject">Subject</asp:ListItem>
                                <asp:ListItem Value="Grade">Grade / Level</asp:ListItem>
                                <asp:ListItem Value="Section">Section</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ImageButton ID="imgSearchQuery" runat="server" CssClass="GridPagerButtons" 
                                ImageUrl="~/images/icons/page_find.gif" onclick="imgSearchQuery_Click" 
                                ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                                   <span class="PageSubHeader">
                  <asp:GridView ID="grdLoadSubjects" runat="server" AllowPaging="True" 
                      AutoGenerateColumns="False" CssClass="GridPagerButtons" 
                       PageSize="20" Width="700px" onrowdatabound="grdLoadSubjects_RowDataBound" 
                                       onprerender="grdLoadSubjects_PreRender">
                      <PagerSettings Position="TopAndBottom" />
                      <Columns>
                      <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                        
                                                            <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" 
                                                                CommandName="ViewItem" ImageUrl="~/images/icons/page_find.gif" Text="Edit" 
                                                                ToolTip="View Grades" />
                                                          
                                                            <asp:Label ID="lblSubjectID" 
                                                                runat="server" Text = '<%# Eval("SubjectID") %>' Visible="False"></asp:Label>
                                                                <asp:Label ID="lblSectionID" 
                                                                runat="server" Text = '<%# Eval("SectionID") %>' Visible="False"></asp:Label>
                                                        </ItemTemplate>
                                                         <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                            
                     
                      </asp:TemplateField>
                          <asp:TemplateField HeaderText="Subject">
                              <EditItemTemplate>
                                  <asp:TextBox ID="TextBox1" runat="server" 
                                      Text='<%# Bind("SubjectDescription") %>'></asp:TextBox>
                              </EditItemTemplate>
                              <ItemTemplate>
                                  <asp:Label ID="Label4a" runat="server" Text='<%# Bind("SubjectDescription") %>'></asp:Label>
                              </ItemTemplate>
                              <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                  ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                              <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                  ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                          </asp:TemplateField>
                         
                          <asp:TemplateField HeaderText="Grade/Level and Section">
                              <EditItemTemplate>
                                  <asp:TextBox ID="TextBox3" runat="server" 
                                      Text='<%# Bind("SectionDescription") %>'></asp:TextBox>
                              </EditItemTemplate>
                              <ItemTemplate>
                                  <asp:Label ID="Label4" runat="server" CssClass="GridPagerButtons" 
                                      Text='<%# Bind("SectionDescription") %>'></asp:Label>
                              </ItemTemplate>
                              <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                  ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                  Wrap="False" />
                              <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                  ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                          </asp:TemplateField>
                      </Columns>
                      <PagerTemplate>
                         <div style="text-align: right">
                                                        <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                                            onclick="lnkFirst_Click" ToolTip="First Page">First</asp:LinkButton>
                                                        <span class="GridPagerButtons"><span lang="en-ph">&nbsp;| </span></span>
                                                        <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="GridPagerButtons" 
                                                            onclick="lnkPrevious_Click" ToolTip="Previous Page">Previous</asp:LinkButton>
                                                        <span class="GridPagerButtons"><span lang="en-ph">&nbsp;| Page </span></span>
                                                        <asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                                            CssClass="GridPagerButtons" 
                                                            onselectedindexchanged="cboPageNumber_SelectedIndexChanged" Width="100px">
                                                        </asp:DropDownList>
                                                        <span class="GridPagerButtons"><span lang="en-ph">&nbsp;</span></span><asp:Label 
                                                            ID="lblPageCount" runat="server" CssClass="GridPagerButtons" Text="of 0"></asp:Label>
                                                        <span class="GridPagerButtons"><span lang="en-ph">&nbsp;| </span></span>
                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="GridPagerButtons" 
                                                            onclick="lnkNext_Click" ToolTip="Next Page">Next</asp:LinkButton>
                                                        <span class="GridPagerButtons"><span lang="en-ph">&nbsp;| </span></span>
                                                        <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                                            onclick="lnkLast_Click" ToolTip="Last Page">Last</asp:LinkButton>
                                                    </div>
                      </PagerTemplate>
                      <PagerStyle BorderWidth="0px" HorizontalAlign="Right" VerticalAlign="Middle" />
                      <EmptyDataTemplate>
                          <br />
                          <center>
                          No Record Found.
                          </center>
                          <br />
                      </EmptyDataTemplate>
                  </asp:GridView>
                  </span>
                    </td>
          
                    </tr>
                    </table>
                 
                                </span>
           </ContentTemplate>
        </asp:UpdatePanel>
                        <center>
                                <br />
                        </center>
    
                        <br />

                    </td>
                </tr>
                <tr>
                    <td >
                        &nbsp;</td>
               
                </tr>
                <tr>
                    <td class="style1" align="center">
                        &nbsp;</td></center>
                
                </tr>
            </table>
    </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
