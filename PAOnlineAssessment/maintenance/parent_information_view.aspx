<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="parent_information_view.aspx.cs" Inherits="PAOnlineAssessment.maintenance.parent_information_view" EnableEventValidation="false" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>
<%@ Register assembly="MSCaptcha" namespace="MSCaptcha" tagprefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Parent Information View - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style6
        {
            width: 73%;
        }
        .style9
        {
            width: 263px;
        }
        .style11
        {
            width: 161px;
        }
        .style12
        {
            color: #7C7900;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: bold;
            vertical-align: middle;
            font-variant: small-caps;
            width: 77px;
        }
        .style13
        {
            color: #7C7900;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: bold;
            vertical-align: middle;
            font-variant: small-caps;
            width: 77px;
            height: 20px;
        }
        .style14
        {
            width: 161px;
            height: 20px;
        }
        .style15
        {
            width: 263px;
            height: 20px;
        }
        .style17
        {
            padding: 0px;
            margin: 0px;
            color: #666666;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 9px;
            font-weight: bold;
            vertical-align: middle;
            text-align: right;
        }
        .style20
        {
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>
    <uc1:frmHeader ID="frmHeader1" runat="server" />    
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            <h2 style="background-color:#fff"><span class="PageHeader">Account Verification
            </span></h2>
            <center>
            <table class="style6">
                <tr>
                    <td colspan="3">
                        <span class="PageSubHeader" lang="en-ph">Parent Information&nbsp;</span></td>
                    
                </tr>
                <tr>
                    <td colspan="3">
                        <span lang="en-ph"><span class="FieldTitle">First Name: </span> </span>
 
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span class="PageSubHeader"><asp:Label ID="lblFirstname" runat="server"></asp:Label></span>
                    </td>
                    <td class="style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style12">
                        &nbsp;</td>
                    <td class="style11">
                        &nbsp;</td>
                    <td class="style9">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <span class="FieldTitle">
                        <span lang="en-ph">Last Name: </span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                        
                    </td>
                </tr>
                <tr>
                    <td class="style9" colspan="2">
                        <span class="PageSubHeader">
                             <asp:Label ID="lblLastname" runat="server" Text=""></asp:Label>
                        </span>
                    </td>
                    <td class="style15">
                    </td>
                </tr>

                <tr>
                    <td class="style13">
                        &nbsp;</td>
                    <td class="style14">
                        &nbsp;</td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td colspan="3">
                        <span class="FieldTitle">
                        <span lang="en-ph">Username: </span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                    </td>
                </tr>
                <tr>
                    <td class="style9" colspan="2">
                        <span class="PageSubHeader">
                           <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                        </span>
                    </td>
                    <td class="style15">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                  <tr>
                    <td colspan="3">
                        <span class="FieldTitle">
                        <span lang="en-ph">Email Address: </span></span><span class="LoginSubHeader" lang="en-ph"> </span>
                    </td>
                </tr>
                <tr>
                    <td class="style9" colspan="2">
                        <span class="PageSubHeader">
                           <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                        </span>
                    </td>
                    <td class="style15">
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="btnVerify" runat="server" onclientclick="return false;"></asp:LinkButton>
                        <asp:ModalPopupExtender ID="mpeStudentModal" runat="server" 
                            BackgroundCssClass="modalBackground" CancelControlID="btnClose" DynamicServicePath="" 
                            Enabled="True" PopupControlID="pStudentPanel" TargetControlID="btnVerify">
                        </asp:ModalPopupExtender>
                        <br />
                        <br />
                        <span class="LoginSubNote"><span lang="en-ph">List of students need to be verified entered by the parent during registration</span></span></td>
                    <td class="style15">
                        &nbsp;</td>
                </tr>
                <!-- Tinanggal n row-->
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvChildInfo" runat="server" AutoGenerateColumns="False" 
                                            PageSize="20" Width="568px" 
                                    onrowdatabound="gvChildInfo_RowDataBound" 
                                    onrowupdating="gvChildInfo_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgAction" runat="server" ImageUrl="~/images/icons/page_tick.gif" Visible="false" />
                                                            <asp:Label ID="lblCount" runat="server" Text='<%# Eval("Count") %>'></asp:Label>    
                                                        </ItemTemplate>
                                                      <HeaderStyle Width="10px" BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Firstname">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFirstname" runat="server" Text='<%# Eval("Firstname") %>'></asp:Label>
                                                           
                                                        </ItemTemplate>
                                                           <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lastname">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLastname" runat="server" Text='<%# Eval("Lastname") %>'></asp:Label>
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
                                                        <asp:Label ID="lblLevel" runat="server" Text='<%# Eval("Level") %>'></asp:Label>
                                                        <asp:Label ID="lblLevelID" runat="server" Text='<%# Eval("LevelID") %>' Visible="false" ></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                           <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Name Match(es)">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgStatus" runat="server" />
                                                            <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                          <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Matched Name(s)">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnVer" ImageUrl="~/images/icons/page_find.gif" 
                                                                runat="server" RowNumber='<%# Eval("Count") %>' onclick="btnVer_Click" ToolTip="View all student that match with the input"/>
                                                        </ItemTemplate>
                                                          <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                        ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                        Wrap="False" />
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                                <EmptyDataTemplate>
                                                <center>
                                                <br />
                                                <span class="PageSubHeaderAlternate" lang="en-ph">No Child Found.</span>
                                                
                                                </center>
                                            </EmptyDataTemplate>
                                  </asp:GridView>
                                  <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="style20">

                     &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="LoginSubNote" colspan="3" 
                        style="background-position: center; background-image: url('../../images/separator.jpg'); background-repeat: repeat-x">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" BackColor="Transparent" 
                                    BorderStyle="None" CssClass="ButtonTemplate" Height="34px"
                                    Text="Activate" Width="88px" onclick="Button1_Click" 
                                CausesValidation="False" />
                            <asp:Button ID="btnCancel" runat="server" BackColor="Transparent" 
                            BorderStyle="None" CssClass="ButtonTemplate" Height="34px" 
                            onclientclick="window.location='parent_maintenance_main.aspx';" 
                            Text="Cancel" Width="88px" />
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                            BorderStyle="None" CssClass="ButtonTemplate" Height="34px" 
                            onclientclick="this.value='Submitting...';  return true; this.disabled=true;" 
                            Text="Activate" Width="88px" onclick="btnSubmit_Click" 
                            CausesValidation="False" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td class="style17" colspan="3">
                        &nbsp;</td>
                </tr>
            </table>

        <asp:Panel ID="pStudentPanel" runat="server" BackColor="White" Width="500px">
         <table width="100%">
            <tr>
                <td style="width: 50%;">
                    &nbsp;
                </td>
                <td style="width: 50%;" align="right">
                    <asp:ImageButton ID="btnClose" runat="server" 
                        ImageUrl="~/images/icons/cross-icon.png" ToolTip="Close Modal" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                <span style="width:auto;">Here are the list of student(s):</span>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                                <table>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblRowIndex" runat="server" Text="0" Visible="False"></asp:Label>
                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="btnSearch" ImageUrl="~/images/icons/page_find.gif" 
                                                                runat="server" onclick="btnSearch_Click" />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                    <asp:GridView ID="DataGrid1" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" onrowdatabound="DataGrid1_RowDataBound" 
                                    PageSize="10" Width="490px">
                                    <Columns>
                                        <asp:BoundField DataField="Firstname" HeaderText="Firstname">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Lastname" HeaderText="Lastname">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="YearLevel" HeaderText="Level">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Section" HeaderText="Section" Visible="False">
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
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkReplace" runat="server" StudentID='<%# Eval("StudentID") %>'
                                                    ToolTip="Replace the current student" onclick="lnkReplace_Click">Replace</asp:LinkButton>
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
                                        <div style="text-align:center" >
                                            <asp:LinkButton ID="lnkFirst0" runat="server" CssClass="GridPagerButtons" 
                                                ToolTip="Go to First Page" onclick="lnkFirst0_Click">First Page</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;|&nbsp;|
                                            <asp:Label ID="Label3" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="cboPageNumber0" runat="server" AutoPostBack="True" 
                                                CssClass="GridPagerButtons" 
                                                ToolTip="Jump to Page" 
                                                onselectedindexchanged="cboPageNumber0_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp;<asp:Label ID="lblPageCount0" runat="server" CssClass="GridPagerButtons" 
                                                Text="of "></asp:Label>
                                            &nbsp;|&nbsp;| </span>
                                            <asp:LinkButton ID="lnkLast0" runat="server" CssClass="GridPagerButtons" 
                                               ToolTip="Go to Last Page" onclick="lnkLast0_Click">Last Page</asp:LinkButton>
                                        </div>
                                    </PagerTemplate>
                                    <PagerStyle BorderColor="#F4F4F4" BorderStyle="None" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    
                                </asp:GridView>
                                        </td>
                                    </tr>
                                    
                                </table>
                                &nbsp;
                                <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
         </table>
         <br />
         </asp:Panel>
         </div>
        &nbsp;</div> 
        <uc3:frmFooter ID="frmFooter1" runat="server" />   
    </form>
</body>
</html>
