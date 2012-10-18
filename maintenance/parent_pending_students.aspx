<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="parent_pending_students.aspx.cs" Inherits="PAOnlineAssessment.maintenance.parent_pending_students" %>
<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmUpperBody.ascx" tagname="frmUpperBody" tagprefix="uc2" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc3" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pending Pending Child Request - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />

</head>
<body><form id="form1" runat="server">
<uc1:frmHeader ID="frmHeader1" runat="server" />
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
     <div id="bodytopmainPan">
        <div id="bodytopPan">
	        <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">Parent Pending Child Request</span> 
            </h2>
            <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <p style="width: 700px; text-align:right;">
                 <asp:TextBox ID="txtSearchQuery" runat="server" CssClass="GridPagerButtons" 
                     Width="175px"></asp:TextBox>
                <span lang="en-ph">
                 <asp:DropDownList ID="cboSearchQuery" runat="server" 
                     CssClass="GridPagerButtons" Width="100px">
                     <asp:ListItem>Firstname</asp:ListItem>
                     <asp:ListItem>Lastname</asp:ListItem>
                 </asp:DropDownList>
                 </span>
                 <asp:ImageButton ID="imgSearchQuery" runat="server" CssClass="GridPagerButtons" 
                     Height="16px" ImageUrl="~/images/icons/page_find.gif" 
                     onclick="imgSearchQuery_Click" ToolTip="Search" />
<asp:GridView ID="DataGrid" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" onrowdatabound="DataGrid_RowDataBound" 
                    Width="700px" PageSize="20">
                    <Columns>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgApprove" runat="server" CausesValidation="False" 
                                    CommandName="Approve" 
                                    ImageUrl="~/images/icons/page_tick.gif" Text="Edit" 
                                    ToolTip="Activate Student" Visible="false" />
                                <span lang="en-ph">
                                <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" 
                                    CommandName="Approve" ImageUrl="~/images/icons/page_find.gif" Text="Edit" 
                                    ToolTip="Activate Student" onclick="imgView_Click" ParentID='<%# Eval("ParentID") %>' />
                                &nbsp;</span><asp:Label ID="lblParentID" runat="server" 
                                    Text='<%# Eval("ParentID") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Width="60px" Wrap="False" />
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
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLastname" runat="server" Text='<%# Eval("Lastname") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLastname" runat="server" Text='<%# Eval("Lastname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="False" />
                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" 
                                Wrap="True" />
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="No. of Requests">
                        <ItemTemplate>
                            <asp:Label ID="lblCount" runat="server" Text='<%# Eval("Count") %>'></asp:Label>
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
                <p style="width: 700px; text-align:right;">
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </p>
            </ContentTemplate>
            </asp:UpdatePanel>
            </center>
            </div>

         <br />
         <asp:Label ID="Label2" runat="server"></asp:Label>
         <asp:ModalPopupExtender ID="mpeStudentVerification" runat="server" 
             BackgroundCssClass="modalBG" CancelControlID="btnClose" DynamicServicePath="" 
             Enabled="True" PopupControlID="pStudentPanel" TargetControlID="Label2">
         </asp:ModalPopupExtender>
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
