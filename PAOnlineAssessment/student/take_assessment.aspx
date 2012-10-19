<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="take_assessment.aspx.cs" Inherits="PAOnlineAssessment.student.take_assessment" EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    
    <title>Take Assessment - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .RadioButtonWidth label   
        {
            display: block;
            float: left;
            padding-top:4px;
        } 
        .RadioButtonWidth input   
        {
            width: 30px;
            display: block;
            float: left;
        } 
        .style4
        {
            color: #3f5330;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            font-weight: bold;
            vertical-align: middle;
            text-align: right;
        }
        .style5
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1"
                runat="server">
                <Scripts><asp:ScriptReference Path="~/scripts/js/webkit.js" /></Scripts>
    </asp:ToolkitScriptManager>
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
            
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblAssessmentTitle" runat="server" CssClass="PageHeader"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAssessmentIntroduction" runat="server" 
                            CssClass="GridPagerButtons"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <span class="TextboxTemplate" lang="en-ph">Assessment Details</span></td>
                </tr>
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">Subject: </span>
                        <asp:Label ID="lblSubject" runat="server" CssClass="NormalText"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">Teacher: </span>
                        <asp:Label ID="lblTeacher" runat="server" CssClass="NormalText"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">Exam Type: </span>
                        <asp:Label ID="lblExamType" runat="server" CssClass="NormalText"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">Scheduled Date: </span>
                        <asp:Label ID="lblScheduledDate" runat="server" CssClass="NormalText"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <span class="FieldTitle" lang="en-ph">Scheduled Time: </span>
                        <asp:Label ID="lblScheduleTime" runat="server" CssClass="NormalText"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
                
                <tr>
                    <td>
                        
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="GridPagerButtons" 
                            Text="Questions Per Page:"></asp:Label>
                                            <span lang="en-ph">&nbsp;</span><asp:DropDownList ID="cboQuestionsPerPage" 
                            runat="server" CssClass="GridPagerButtons" AutoPostBack="True" 
                                                onselectedindexchanged="cboQuestionsPerPage_SelectedIndexChanged">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style4">
                                            <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                        onclick="lnkFirst_Click">First</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;| </span>
                                            <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="GridPagerButtons" 
                                        onclick="lnkPrevious_Click">Previous</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;| </span>
                                            <asp:Label ID="Label2" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                            <span lang="en-ph">&nbsp;</span><asp:DropDownList ID="cboPageNumber" 
                                        runat="server" CssClass="GridPagerButtons" AutoPostBack="True" 
                                                onselectedindexchanged="cboPageNumber_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <span lang="en-ph">&nbsp;</span><asp:Label 
                                        ID="lblPageCount" runat="server" 
                                        CssClass="GridPagerButtons" Text="of 0"></asp:Label>
                                            <span lang="en-ph">&nbsp;| </span>
                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="GridPagerButtons" 
                                        onclick="lnkNext_Click">Next</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;| </span>
                                            <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                        onclick="lnkLast_Click">Last</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="background-image: url('../../images/separator.jpg'); background-repeat: repeat-x; background-position: center center">
                        &nbsp;</td>
                </tr>
                
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                            <ContentTemplate>
                                <asp:GridView ID="grdAssessment" runat="server" AutoGenerateColumns="False" 
                            Width="100%" 
                            ShowHeader="False" AllowPaging="True" >
                                    <PagerSettings Mode="NextPrevious" Visible="False" />
                                    <RowStyle BorderStyle="None" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Questions">
                                            <ItemTemplate>
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="lblRowCount" runat="server" CssClass="GridPagerButtons" Text='<%# Eval("RowOrder") + ")" %>'></asp:Label>
                                                        </td>
                                                        <td >
                                                            &nbsp;</td>
                                                        <td align="left" style="width:88%">
                                                            <asp:Label ID="lblQuestion" runat="server" CssClass="GridPagerButtons" 
                                                        Text='<%# Eval("Question") %>' Width="100%" style="display:inline"></asp:Label>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                            
                                                        <td valign="top" align="left">
                                                            <asp:RadioButtonList ID="rboAnswer" runat="server" 
                                                        CssClass="LoginSubNote RadioButtonWidth" 
                                                        onselectedindexchanged="rboMultipleChoice_SelectedIndexChanged" 
                                                                AutoPostBack="True"
                                                                >
                                                            </asp:RadioButtonList>
                                                            <asp:TextBox ID="txtAnswer" runat="server" CssClass="GridPagerButtons" 
                                                                Width="250px" ontextchanged="txtAnswer_TextChanged" AutoPostBack="True" ></asp:TextBox>
                                                        </td>
                                                         <td align="center" style="width:10%">
                                                        <asp:Image ID="loadPicture" Visible="true" runat="server" Width="75px" Height="75px" />
                                                            <br />
                                                            <asp:LinkButton ID="lnkView" Visible="true" runat="server" CssClass="GridPagerButtons" 
                                                                FileName='<%# Eval("FileName") %>' onclick="lnkView_Click">View Image</asp:LinkButton>
                                                         <asp:Label runat="server" ID="lblFileName" Visible="false" Text='<%# Eval("FileName") %>' ></asp:Label>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="lblQuestionPoolID" runat="server" 
                                            Text='<%# Eval("QuestionPoolID") %>' Visible="False"></asp:Label>
                                                <span lang="en-ph">&nbsp;</span><asp:Label ID="lblPoints" 
                                            runat="server" Text='<%# Eval("Points") %>' Visible="False"></asp:Label>
                                                <span lang="en-ph">&nbsp;</span><asp:Label 
                                            ID="lblQuestionCorrectAnswer" runat="server" 
                                            Text='<%# Eval("CorrectAnswer") %>' Visible="False"></asp:Label>
                                                <span lang="en-ph">&nbsp;</span><asp:Label 
                                            ID="lblSelectedAnswer" runat="server" Text='<%# Eval("SelectedAnswer") %>' 
                                                    Visible="False"></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#F4F4F4" BorderColor="#F4F4F4" BorderStyle="Solid" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="background-image: url('../../images/separator.jpg'); background-repeat: repeat-x; background-position: center center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:GridView ID="GridView1" runat="server" Visible="False">
                        </asp:GridView>
                        </td>
                </tr>
                <tr>
                    <td class="style5">
                        <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                            BorderStyle="None" CssClass="ButtonTemplate" Height="28px" Text="Submit" 
                            Width="88px" onclick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
            <asp:Button ID="Button1" runat="server" Text="Button" style="display: none" />
            <asp:ModalPopupExtender ID="Button1_ModalPopupExtender" runat="server" 
                BackgroundCssClass="modalBackground" CancelControlID="imgclose" 
                DynamicServicePath="" Enabled="True" PopupControlID="PicturePanel" 
                TargetControlID="Button1">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PicturePanel" runat="server" BackColor="White">
                
                        <table width="500px">
                        <tr>
                            <td align="right"><asp:ImageButton ID="imgclose" runat="server" ImageUrl="~/images/icons/action_stop.gif" /></td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                        
                            <td align="center"><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate><asp:Image ID="panelpicture" runat="server" Width="400px" Height="300px" /></ContentTemplate>        
                </asp:UpdatePanel></td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        </table>
                
                        </asp:Panel>
        </div>
    </div> 
    <uc2:frmFooter ID="frmFooter1" runat="server" />   
    </form>
</body>
</html>
