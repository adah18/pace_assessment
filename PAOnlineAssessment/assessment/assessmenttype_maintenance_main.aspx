<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="assessmenttype_maintenance_main.aspx.cs" Inherits="PAOnlineAssessment.assessment.assessmenttype_maintenance_main" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assessment Type Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            color: #3f5330;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            font-weight: bold;
            vertical-align: middle;
            text-align: right;
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
            <h2 style="background-color: #FFFFFF"><span class="PageHeader" lang="en-ph">Assessment Type Maintenance</span></h2>
            <table style="width:100%;">
                <tr>
                    <td colspan="3">
                    <center>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table style="width:700px;">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td class="style1">
                                            <asp:TextBox ID="txtSearchQuery" runat="server" CssClass="GridPagerButtons" 
                                                Width="175px"></asp:TextBox>
                                            <span lang="en-ph">&nbsp;</span><asp:DropDownList ID="cboSearchQuery" runat="server" 
                                                CssClass="GridPagerButtons" Width="100px">
                                                <asp:ListItem>Description</asp:ListItem>
                                                <asp:ListItem Value="A">A - Available</asp:ListItem>
                                                <asp:ListItem Value="D">D - Deactivated</asp:ListItem>
                                            </asp:DropDownList>
                                            <span lang="en-ph">&nbsp;</span><asp:ImageButton ID="imgSearchQuery" runat="server" 
                                                CssClass="GridPagerButtons" ImageUrl="~/images/icons/page_find.gif" 
                                                ToolTip="Search" onclick="imgSearchQuery_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="DataGrid" runat="server" Width="700px" 
                                                AutoGenerateColumns="False" AllowPaging="True" 
                                                onrowdatabound="DataGrid_RowDataBound" PageSize="20" ShowFooter="True" 
                                                onprerender="DataGrid_PreRender" 
                                                onselectedindexchanged="DataGrid_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                                                                CommandName="Update" Text="Update" />
                                                            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                CommandName="Cancel" Text="Cancel" />
                                                                <span lang="en-ph">&nbsp;</span><asp:Label ID="lblAssessmentTypeID" 
                                                                runat="server" Text='<%# Eval("AssessmentTypeID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:ImageButton ID="imgNew" runat="server" CommandName="AddNew" 
                                                                ImageUrl="~/images/icons/page_new.gif" 
                                                                PostBackUrl="~/assessment/assessmenttype_maintenance_addupdate.aspx" 
                                                                ToolTip="Add New Assessment Type" />
                                                                
                                                            <span lang="en-ph">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" 
                                                                CommandName="EditItem" ImageUrl="~/images/icons/page_edit.gif" Text="Edit" 
                                                                ToolTip="Edit Assessment Type" />
                                                            &nbsp;<asp:ImageButton ID="imgDeactivate" runat="server" CausesValidation="False" 
                                                                CommandName="DeleteItem" ImageUrl="~/images/icons/page_delete.gif" 
                                                                Text="Delete" />
                                                                
                                                            <asp:Label ID="lblAssessmentTypeID" 
                                                                runat="server" Text = '<%# Eval("AssessmentTypeID") %>' Visible="False"></asp:Label>
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
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
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
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </center>
                    </td>
                </tr>
            </table>
    </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
