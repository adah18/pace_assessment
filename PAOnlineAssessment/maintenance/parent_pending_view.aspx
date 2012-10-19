<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="parent_pending_view.aspx.cs" Inherits="PAOnlineAssessment.maintenance.parent_pending_view" %>
<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmUpperBody.ascx" tagname="frmUpperBody" tagprefix="uc2" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pending Student Accounts - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />

    <style type="text/css">
        .style1
        {
            width: 109px;
        }
    </style>
</head>
<body><form id="form1" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <uc1:frmHeader ID="frmHeader1" runat="server" />
     <div id="bodytopmainPan">
        <div id="bodytopPan">
	        <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">Parent Pending Child Request</span> 
                
            </h2>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <span class="FieldTitle" lang="en-ph">Parent Information</span>
                    </td>
                </tr>
                
                <tr>
                    <td class="style1">
                        <span class="FieldTitle" lang="en-ph">Parent Name:</span>
                        
                    </td>
                    <td>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                       &nbsp;
                    </td>
                    <td>
                        <asp:LinkButton ID="btnView" runat="server" onclientclick="return false;">View All Child</asp:LinkButton>
                        <asp:ModalPopupExtender ID="btnView_ModalPopupExtender" runat="server" 
                            BackgroundCssClass="modalBackground" CancelControlID="btnClose" 
                            DynamicServicePath="" Enabled="True" PopupControlID="pStudentPanel" 
                            TargetControlID="btnView">
                        </asp:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span class="LoginSubNote" lang="en-ph">Here is the list of pending student selected by the parents as their child needs to view their grades for approval:</span>
                        <br />
                    <asp:GridView ID="DataGrid" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" onrowdatabound="DataGrid_RowDataBound" 
                    Width="100%" PageSize="20">
                    <Columns>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                
                                <span lang="en-ph">
                                    <asp:CheckBox ID="chkApprove" runat="server"/>
                                &nbsp;</span><asp:Label ID="lblID" runat="server" 
                                    Text='<%# Eval("ChildID") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Width="60px" Wrap="False" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Student Name">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
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
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="True" />
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Section">
                        <ItemTemplate>
                            <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
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
                    <FooterStyle BorderStyle="Solid" />
                    <PagerTemplate>
                        <div class="style2">
                            <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" ToolTip="Go to First Page">First Page</asp:LinkButton>
                            <span lang="en-ph">&nbsp;|
                            <asp:Label ID="Label1" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                            &nbsp;<asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                CssClass="GridPagerButtons" 
                                ToolTip="Jump to Page" Width="40px">
                            </asp:DropDownList>
                            &nbsp;<asp:Label ID="lblPageCount" runat="server" CssClass="GridPagerButtons" 
                                Text="of "></asp:Label>
                            &nbsp;| </span>
                            <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                 ToolTip="Go to Last Page">Last Page</asp:LinkButton>
                        </div>
                    </PagerTemplate>
                    <PagerStyle BorderColor="#F4F4F4" BorderStyle="None" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                </asp:GridView>
                        <asp:HiddenField ID="hidParentID" runat="server" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                            onclick="btnApprove_Click" /> &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                    </td>
                </tr>
            </table>

         <br />
      
         <asp:Panel ID="pStudentPanel" runat="server" BackColor="White" Width="500px">
             <table width="100%">
                 <tr>
                     <td style="width: 50%;">
                         &nbsp;
                     </td>
                     <td align="right" style="width: 50%;">
                         <asp:ImageButton ID="btnClose" runat="server" 
                             ImageUrl="~/images/icons/cross-icon.png" ToolTip="Close Modal" />
                     </td>
                 </tr>
                 <tr>
                     <td align="center" colspan="2">
                         <span style="width:auto;">Here are the list of student selected by parent and 
                         already approved:</span>
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                             <ContentTemplate>
                                 &nbsp;<asp:GridView ID="gvStudent" runat="server" AllowPaging="True" 
                                     AutoGenerateColumns="False" onrowdatabound="DataGrid_RowDataBound" PageSize="5" 
                                     Width="490px">
                                     <Columns>
                                         <asp:BoundField DataField="Name" HeaderText="Student Firstname">
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
                                         <asp:BoundField DataField="Section" HeaderText="Section">
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
                                             <asp:LinkButton ID="lnkFirst0" runat="server" CssClass="GridPagerButtons" 
                                                 ToolTip="Go to First Page">First PageFirst Page</asp:LinkButton>
                                             <span lang="en-ph">&nbsp;|&nbsp;|
                                             <asp:Label ID="Label3" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                             &nbsp;<asp:DropDownList ID="cboPageNumber0" runat="server" AutoPostBack="True" 
                                                 CssClass="GridPagerButtons" ToolTip="Jump to Page" Width="48px">
                                             </asp:DropDownList>
                                             &nbsp;<asp:Label ID="lblPageCount0" runat="server" CssClass="GridPagerButtons" 
                                                 Text="of "></asp:Label>
                                             &nbsp;|&nbsp;| </span>
                                             <asp:LinkButton ID="lnkLast0" runat="server" CssClass="GridPagerButtons" 
                                                 ToolTip="Go to Last Page">Last PageLast Page</asp:LinkButton>
                                         </div>
                                     </PagerTemplate>
                                     <PagerStyle BorderColor="#F4F4F4" BorderStyle="None" />
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                 </asp:GridView>
                                 <br />
                             </ContentTemplate>
                         </asp:UpdatePanel>
                     </td>
                 </tr>
             </table>
             <br />
         </asp:Panel>
    </div>    
    </div>
    <uc3:frmFooter ID="frmFooter1" runat="server" />
    </form>

</body>
    <script type="text/javascript">
        var parent_activation = getQuerystring('success');
        if (parent_activation == '1') {
            alert("Parent has been successfully Activated");
        }
        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return default_;
            else
                return qs[1];
        }    
    </script>
</html>
