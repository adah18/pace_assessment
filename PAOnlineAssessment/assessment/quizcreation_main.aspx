<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="quizcreation_main.aspx.cs" Inherits="PAOnlineAssessment.assessment.quizcreation_main" %>
<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Assessment Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
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
            <h2 style="background-color: #FFFFFF"><span class="PageHeader" lang="en-ph">Assessment Maintenance</span></h2>
            <table style="width:100%;">
                <tr>
                    <td colspan="3">
                   <center>  
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="right">
                            <span class="FieldTitle" lang="en-ph">Quarter:</span>&nbsp<asp:DropDownList 
                                                            ID="ddlQuarter" Width="40px" runat="server" AutoPostBack="True"  
                                                            CssClass="GridPagerButtons" 
                                                            onselectedindexchanged="ddlQuarter_SelectedIndexChanged">
                                                            <asp:ListItem>1st</asp:ListItem>
                                                            <asp:ListItem>2nd</asp:ListItem>
                                                            <asp:ListItem>3rd</asp:ListItem>
                                                            <asp:ListItem>4th</asp:ListItem>
                                                        </asp:DropDownList>
                            <asp:TextBox ID="txtSearch" runat="server" Width="150px" 
                                CssClass="GridPagerButtons"></asp:TextBox>
                            <asp:DropDownList
                                ID="ddlSearch" runat="server" Width="150px" CssClass="GridPagerButtons">
                                <asp:ListItem>Assessment Type</asp:ListItem>
                                <asp:ListItem>Level</asp:ListItem>
                                <asp:ListItem>Subject</asp:ListItem>
                                <asp:ListItem>Title</asp:ListItem>
                                <asp:ListItem Value="A">A - Available</asp:ListItem>
                                <asp:ListItem Value="D">D - Deactivated</asp:ListItem>
                            </asp:DropDownList>
                            <asp:ImageButton ID="imgSearchQuery" runat="server" 
                                CssClass="GridPagerButtons" ImageUrl="~/images/icons/page_find.gif" 
                                onclick="imgSearchQuery_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="dgAssessment" runat="server" AutoGenerateColumns="False" 
                                    Width="700px" AllowPaging="True" 
                                onrowdatabound="dgAssessment_RowDataBound" PageSize="20">
                                    <Columns>
                                        <asp:TemplateField  HeaderText="Action">
                                            <EditItemTemplate>
                                            <asp:ImageButton ID="imgUpdate" runat="server" CausesValidation="True" 
                                                    CommandName="Update" ImageUrl="~/images/icons/page_tick.gif" Text="Update" 
                                                    ToolTip="Update Changes" />
                                                &nbsp;<asp:ImageButton ID="imgCancel" runat="server" CausesValidation="False" 
                                                    CommandName="Cancel" ImageUrl="~/images/icons/action_stop.gif" Text="Cancel" 
                                                    ToolTip="Cancel Changes" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" 
                                                    CommandName="Edit" ImageUrl="~/images/icons/page_edit.gif" Text="Edit" 
                                                    ToolTip="Edit Assessment" />
                                            <asp:ImageButton ID="imgDeactivate" runat="server" CausesValidation="False" 
                                                                CommandName="DeleteItem" ImageUrl="~/images/icons/page_delete.gif" 
                                                                Text="Delete" />
                                            <asp:ImageButton ID="imgPreview" runat="server" CausesValidation="False" 
                                                                CommandName="Preview" ImageUrl="~/images/icons/page_find.gif"
                                                                Text="Preview" />
                                                                
                                             <asp:ImageButton ID="imgCloseOpen" runat="server" CausesValidation="False" 
                                                                CommandName="CloseOpen" ImageUrl="~/images/icons/page.gif"
                                                                Text="Close" />
                                                                
                                            <asp:Label ID="lblAssessmentID" runat="server" Text='<%# Eval("AssessmentID") %>' Visible="False"></asp:Label>
                                             <asp:Label ID="lblScheduleStatus" runat="server" Text='<%# Eval("ScheduleStatus") %>' Visible="False"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" Width="80px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterTemplate>
                                            <asp:ImageButton text="Add" ID="imgAdd" runat="server" ImageUrl="~/images/icons/page_new.gif" CommandName="Add" />
                                        </FooterTemplate>
                         
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assessment Type">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("AssessmentType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Level">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Level") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Schedule Type">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblScheduleType" runat="server" Text='<%# Eval("ScheduleType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                                        <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                            ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                            Wrap="False" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Status">
                                            
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
                                    <PagerTemplate>
                                    <div style="text-align: right">
                                        <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                            onclick="lnkFirst_Click" ToolTip="First Page">First</asp:LinkButton>
                                        <span class="GridPagerButtons"><span lang="en-ph">&nbsp;| </span></span>
                                        <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="GridPagerButtons" 
                                            onclick="lnkPrevious_Click" ToolTip="Previous Page">Previous</asp:LinkButton>
                                        <span class="GridPagerButtons"><span lang="en-ph">&nbsp;| Page </span></span>
                                        <asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                            CssClass="GridPagerButtons" Width="40px"
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
                    <tr>
                        <td>
                            &nbsp;</td>
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