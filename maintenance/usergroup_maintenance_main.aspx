<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usergroup_maintenance_main.aspx.cs" Inherits="PAOnlineAssessment.maintenance.usergroup_maintenance_main" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmUpperBody.ascx" tagname="frmUpperBody" tagprefix="uc2" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Group Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:frmHeader ID="frmHeader1" runat="server" />
     <div id="bodytopmainPan">
        <div id="bodytopPan">
	        <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">Usergroup Maintenance</span></h2>     
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <center> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <p style="width: 700px; text-align:right;">
                <asp:TextBox ID="txtSearchQuery" runat="server" CssClass="GridPagerButtons" 
                    Width="175px"></asp:TextBox><asp:DropDownList ID="cboSearchQuery" runat="server" 
                    CssClass="GridPagerButtons" width="100px">
                    <asp:ListItem Value="Description">Description</asp:ListItem>
                    <asp:ListItem Value="A">A - Available</asp:ListItem>
                    <asp:ListItem Value="D">D - Deactivated</asp:ListItem>
                    
                </asp:DropDownList>
                <span lang="en-ph">&nbsp;</span><asp:ImageButton ID="imgSearchQuery" 
                    runat="server" ImageUrl="~/images/icons/page_find.gif" 
                    onclick="imgSearchQuery_Click" ToolTip="Search" 
                    CssClass="GridPagerButtons" />
&nbsp;
<asp:GridView ID="gvUsergroup" runat="server" Width="700px" 
                                                AutoGenerateColumns="False" AllowPaging="True" 
                                                onrowdatabound="gvUsergroup_RowDataBound" PageSize="20" ShowFooter="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <FooterTemplate>
                                                            <asp:ImageButton ID="imgNew" runat="server" CommandName="AddNew" 
                                                                ImageUrl="~/images/icons/page_new.gif" 
                                                                PostBackUrl="~/maintenance/usergroup_maintenance_addupdate.aspx" 
                                                                ToolTip="Add New Usergroup" />
                                                            <span lang="en-ph">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" 
                                                                CommandName="EditItem" ImageUrl="~/images/icons/page_edit.gif" Text="Edit" 
                                                                ToolTip="Edit Usergroup" />
                                                            &nbsp;<asp:ImageButton ID="btnStatus" runat="server" CausesValidation="False" 
                                                                CommandName="DeleteItem" ImageUrl="~/images/icons/page_delete.gif" 
                                                                Text="Delete" />&nbsp;
                                                                <asp:ImageButton ID="btnUserPriv" ImageUrl="~/images/icons/page_user.gif" runat="server" ToolTip="View User's Privilege" />    
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("UsergroupID") %>' Visible="false" ></asp:Label>
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
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" />
                                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle BackColor="White" BorderColor="White" BorderStyle="None" />
                                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BorderStyle="None" />
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
                                                            onselectedindexchanged="cboPageNumber_SelectedIndexChanged">
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
