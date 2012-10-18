<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="pending_student_accounts.aspx.cs" Inherits="PAOnlineAssessment.pending_student_accounts" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmUpperBody.ascx" tagname="frmUpperBody" tagprefix="uc2" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pending Student Accounts - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
</head>
<body><form id="form1" runat="server">
    <uc1:frmHeader ID="frmHeader1" runat="server" />
     <div id="bodytopmainPan">
        <div id="bodytopPan">
	        <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">Pending Student Accounts</span> <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </h2>            
	        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <p style="width: 700px; text-align:right;">
                <span lang="en-ph" class="GridPagerButtons">&nbsp;</span><asp:TextBox 
                     ID="txtSearchQuery" runat="server" CssClass="GridPagerButtons" 
                    Width="175px"></asp:TextBox>
                <span lang="en-ph"></span>
                <asp:DropDownList ID="cboSearchQuery" Width="100px" runat="server" 
                    CssClass="GridPagerButtons">
                    <asp:ListItem Value="StudentNumber">Student Number</asp:ListItem>
                    <asp:ListItem Value="FullName">Full Name</asp:ListItem>
                    <asp:ListItem Value="A">A - Available</asp:ListItem>
                    <asp:ListItem Value="D">D - Deactivated</asp:ListItem>
                    <asp:ListItem Value="EmailAddress">Email Address</asp:ListItem>
                </asp:DropDownList>
                <span lang="en-ph"></span><asp:ImageButton ID="imgSearchQuery" 
                    runat="server" ImageUrl="~/images/icons/page_find.gif" 
                    onclick="imgSearchQuery_Click" ToolTip="Search" 
                    CssClass="GridPagerButtons" />
<asp:GridView ID="DataGrid" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" onrowdatabound="DataGrid_RowDataBound" 
                    Width="700px" PageSize="20">
                    <Columns>
                        <asp:TemplateField HeaderText="Action">
                            <EditItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                                    CommandName="Update" Text="Update" />
                                &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                    CommandName="Cancel" Text="Cancel" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgApprove" runat="server" CausesValidation="False" 
                                    CommandName="Approve" 
                                    ImageUrl="~/images/icons/page_tick.gif" Text="Edit" 
                                    ToolTip="Activate Student" />
                                <span lang="en-ph">&nbsp;</span><asp:Label ID="lblStudentID" runat="server" 
                                    Text='<%# Eval("StudentID") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Width="60px" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="StudentNumber" HeaderText="Student Number">
                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                            Wrap="False" />
                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                            Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Full Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Firstname") + " " + Eval("Lastname") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Firstname") + " " + Eval("Lastname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="True" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="EmailAddress" HeaderText="Email Address">
                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                            Wrap="False" />
                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                            Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status">
                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                            Wrap="False" />
                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                            Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BorderStyle="Solid" />
                    <PagerTemplate>
                        <div class="style2">
                            <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                onclick="lnkFirst_Click" ToolTip="Go to First Page">First Page</asp:LinkButton>
                            <span lang="en-ph">&nbsp;|
                            <asp:Label ID="Label1" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                            &nbsp;<asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                CssClass="GridPagerButtons" 
                                onselectedindexchanged="cboPageNumber_SelectedIndexChanged" 
                                ToolTip="Jump to Page" Width="40px">
                            </asp:DropDownList>
                            &nbsp;<asp:Label ID="lblPageCount" runat="server" CssClass="GridPagerButtons" 
                                Text="of "></asp:Label>
                            &nbsp;| </span>
                            <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                onclick="lnkLast_Click" ToolTip="Go to Last Page">Last Page</asp:LinkButton>
                        </div>
                    </PagerTemplate>
                    <PagerStyle BorderColor="#F4F4F4" BorderStyle="None" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                </asp:GridView>
            </p>
            </ContentTemplate>
            </asp:UpdatePanel>
            </center>
    </div>
    </div>
    <uc3:frmFooter ID="frmFooter1" runat="server" />   
    </form>
</body>
</html>
