<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="student_maintenance_main.aspx.cs" Inherits="PAOnlineAssessment.maintenance.student_maintenance_main" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmUpperBody.ascx" tagname="frmUpperBody" tagprefix="uc2" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Student Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:frmHeader ID="frmHeader1" runat="server" />
     <div id="bodytopmainPan">
        <div id="bodytopPan">
	        <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">Student Maintenance</span></h2>     
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <center> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <p style="width: 700px; text-align:right;">
                <asp:TextBox ID="txtSearchQuery" runat="server" CssClass="GridPagerButtons" 
                    Width="175px"></asp:TextBox>
                <span lang="en-ph">&nbsp;</span><asp:DropDownList ID="cboSearchQuery" runat="server" 
                    CssClass="GridPagerButtons" width="100px">
                    <asp:ListItem Value="StudentNumber">Student Number</asp:ListItem>
                    <asp:ListItem Value="FullName">Full Name</asp:ListItem>
                    <asp:ListItem Value="EmailAddress">Email Address</asp:ListItem>
                    <asp:ListItem Value="A">A - Available</asp:ListItem>
                    <asp:ListItem Value="D">D - Deactivated</asp:ListItem>
                    
                </asp:DropDownList>
                <span lang="en-ph">&nbsp;</span><asp:ImageButton ID="imgSearchQuery" 
                    runat="server" ImageUrl="~/images/icons/page_find.gif" 
                    onclick="imgSearchQuery_Click" ToolTip="Search" 
                    CssClass="GridPagerButtons" />
&nbsp;<asp:GridView ID="DataGrid" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" onrowdatabound="DataGrid_RowDataBound" 
                    Width="700px" PageSize="20" onprerender="DataGrid_PreRender">
                     <PagerSettings Mode="NextPrevious" />
                    <Columns>
                        <asp:TemplateField HeaderText="Action">
                            <EditItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                                    CommandName="Update" Text="Update" />
                                &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                    CommandName="Cancel" Text="Cancel" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" 
                                    CommandName="Edit" 
                                    ImageUrl="~/images/icons/page_edit.gif" Text="Edit" 
                                    ToolTip="Edit Student Details" Width="17px" />
                                &nbsp;<asp:ImageButton ID="imgDeactivate" runat="server" CausesValidation="False" 
                                    CommandName="Deactivate" Enabled="False" 
                                    ImageUrl="~/images/icons/page_delete_disabled.gif" Text="Delete" 
                                    ToolTip="Deactivate User" />
                                <asp:Label ID="lblStudentID" runat="server" Text='<%# Eval("StudentID") %>' 
                                    Visible="False"></asp:Label>
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
                        <asp:TemplateField HeaderText="Email Address">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email&lt;br&gt;Verified">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EmailVerified") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("EmailVerified") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account&lt;br&gt;Verified">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("AdminVerified") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("AdminVerified") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Eval("Status") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BorderStyle="Solid" />
                    <PagerTemplate>
                        <div class="style2">
                            <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                onclick="lnkFirst_Click" ToolTip="Go to First Page">First</asp:LinkButton>
                            <span lang="en-ph">&nbsp;|&nbsp;<asp:LinkButton ID="lnkPrevious" runat="server" 
                                CssClass="LinkButtonTemplate" onclick="lnkPrevious_Click">Previous</asp:LinkButton>
                            &nbsp;|&nbsp;<asp:Label ID="Label1" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                            &nbsp;<asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                CssClass="GridPagerButtons" 
                                onselectedindexchanged="cboPageNumber_SelectedIndexChanged" 
                                ToolTip="Jump to Page" width="40px">
                            </asp:DropDownList>
                            &nbsp;<asp:Label ID="lblPageCount" runat="server" CssClass="GridPagerButtons" 
                                Text="of "></asp:Label>
                            &nbsp;|
                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="GridPagerButtons" 
                                onclick="lnkNext_Click">Next</asp:LinkButton>
                            &nbsp;| </span>
                            <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                onclick="lnkLast_Click" ToolTip="Go to Last Page">Last</asp:LinkButton>
                        </div>
                    </PagerTemplate>
                    <PagerStyle BorderColor="#F4F4F4" BorderStyle="None" HorizontalAlign="Right" 
                         VerticalAlign="Middle" />
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
