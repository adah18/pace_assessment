<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quarter_maintenance.aspx.cs" Inherits="PAOnlineAssessment.maintenance.quarter_maintenance" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Quarter Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" type="text/css" />
    <!-- Contact Form CSS files -->
    <link type='text/css' href="../scripts/styles/modal_basic.css" rel='stylesheet' media='screen' /> 
    
     <script src="../assessment/datepicker/src/js/jscal2.js"></script> 
    <script src="../assessment/datepicker/src/js/lang/en.js"></script> 
    <link rel="stylesheet" type="text/css" href="../assessment/datepicker/src/css/jscal2.css" /> 
    <link rel="stylesheet" type="text/css" href="../assessment/datepicker/src/css/border-radius.css" /> 
    <link rel="stylesheet" type="text/css" href="../assessment/datepicker/src/css/steel/steel.css" /> 
    <style type="text/css">
        .ImageCaption
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: normal;
            color: #7c7900;
            font-size: 11px;
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
            <h2 style="background-color: #FFFFFF"><span class="PageHeader">Quarter Maintenance</span></h2>
        	
                <table style="width:100%;">
                    <tr>
                        <td colspan="4">
                        <center>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><p style="width: 700px; text-align:right;">
                        <asp:GridView ID="gvQuarter" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" 
                            Width="700px" PageSize="4" onrowediting="gvQuarter_RowEditing" 
                                onrowdatabound="gvQuarter_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Action">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                                            CommandName="Update" Text="Update" />
                                        &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                            CommandName="Cancel" Text="Cancel" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                            CommandName="Edit" Enabled="true" 
                                            ImageUrl="~/images/icons/page_edit.gif" Text="Edit" 
                                            ToolTip="Edit Quarter" />
                                        &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                            CommandName="Delete" Enabled="False" 
                                            ImageUrl="~/images/icons/page_delete_disabled.gif" Text="Delete" 
                                            ToolTip="" />
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Width="60px" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quarter">
                                    <EditItemTemplate>  
                                        <asp:TextBox ID="txtQuarter" runat="server" Text='<%# Eval("Quarter") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuarter" runat="server" Text='<%# Eval("Quarter") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quarter Start">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQStart" runat="server" Text='<%# Eval("DateFrom") %>'></asp:TextBox>
                                        <asp:ImageButton ID="imgCalendar" runat="server" ImageUrl="~/images/icons/calendar.gif"
                                            onclientclick="return false;" />
                                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                                                            
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQStart" runat="server" Text='<%# Eval("DateFrom") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                </asp:TemplateField>
                                
                               <asp:TemplateField HeaderText="Quarter End">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQEnd" runat="server" Text='<%# Eval("DateTo") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblQEnd" runat="server" Text='<%# Eval("DateTo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="School Year">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSchoolYear" runat="server" Text='<%# Eval("SchoolYear") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSchoolYear" runat="server" Text='<%# Eval("SchoolYear") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="False" />
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Status" HeaderText="Status">
                                <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                    ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                    Wrap="False" />
                                <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                    ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                    Wrap="False" />
                                </asp:BoundField>--%>
                            </Columns>
                            <FooterStyle BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                            <PagerTemplate>
                                <div class="style2">
                                </div>
                            </PagerTemplate>
                            <PagerStyle BorderColor="#F4F4F4" BorderStyle="None" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:GridView>
                    </p></ContentTemplate>
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
