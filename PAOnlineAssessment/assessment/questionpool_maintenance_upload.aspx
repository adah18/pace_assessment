<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="questionpool_maintenance_upload.aspx.cs" Inherits="PAOnlineAssessment.questionpool_maintenance_upload" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>

<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Question Pool Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 190px;
            height: 18px;
        }
        .style2
        {
            width: 30px;
            height: 18px;
        }
        .style3
        {
            width: 528px;
        }
        .style6
        {
            height: 18px;
        }
        .style7
        {
            width: 100%;
        }
        .style8
        {
            height: 20px;
            width: 1px;
        }
        .style9
        {
            text-align: right;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">    
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan" class="style3">
            <h2 style="background-color: #FFFFFF"><span class="PageHeader">U<span lang="en-ph">pload</span></span><span lang="en-ph" class="PageHeader"> Questions</span></h2>
            <center>
            <table style="width:100%;">
                <tr class="LoginHeader">
                    <td colspan="3">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                  <span lang="en-ph" class="PageSubHeader">
                      <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                                </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <span class="PageSubHeader" lang="en-ph">Course Details</span></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:UpdatePanel ID="CourseDetailsPanel" runat="server">
                            <ContentTemplate>
                                <table class="style7">
                                    <tr>
                                        <td class="style1" valign=top>
                                            <span class="FieldTitle" lang="en-ph">Grade 
                        / Level:</span></td>
                                        <td class="style2">
                                            &nbsp;</td>
                                        <td class="style6">
                                            <asp:DropDownList ID="cboGradeLevel" 
                            runat="server" CssClass="GridPagerButtons" 
                                                                Width="175px" AutoPostBack="True" 
                                                                
                                                onselectedindexchanged="cboGradeLevel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <span lang="en-ph">&nbsp;</span><asp:Label 
                                                                ID="vlGradeLevel" runat="server" 
                                                                CssClass="ValidationNotice" Text="*"></asp:Label>
                                            <br />
                                            <span class="LoginSubNote"><span lang="en-ph">Grade / Level &nbsp;the assessment will 
                                            be created on</span></span></td>
                                    </tr>
                                    <tr>
                                        <td class="style1" valign="top">
                                            <span class="FieldTitle" lang="en-ph">Subject:</span></td>
                                        <td class="style2">
                                        </td>
                                        <td class="style6">
                                            <asp:DropDownList ID="cboSubjectList" runat="server" 
                                                                CssClass="GridPagerButtons" Width="175px" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="cboSubjectList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <span lang="en-ph">&nbsp;</span><asp:Label ID="vlSubjectList" runat="server" 
                                                CssClass="ValidationNotice" Text="*"></asp:Label>
                                            <br />
                                            <span class="LoginSubNote" lang="en-ph">Subject the assessment will be created 
                                                            on</span></td>
                                    </tr>
                                    <tr>
                                        <td class="style1" valign="top">
                                            <span class="FieldTitle" lang="en-ph">Quarter:</span></td>
                                        <td class="style2">
                                        </td>
                                        <td class="style6">
                                            <asp:DropDownList ID="ddlQuarter" runat="server" 
                                                                CssClass="GridPagerButtons" Width="175px">
                                                                <asp:ListItem>1st</asp:ListItem>
                                                                <asp:ListItem>2nd</asp:ListItem>
                                                                <asp:ListItem>3rd</asp:ListItem>
                                                                <asp:ListItem>4th</asp:ListItem>
                                            </asp:DropDownList>
                                            <span lang="en-ph">&nbsp;</span><asp:Label ID="Label2" runat="server" 
                                                CssClass="ValidationNotice" Text="*"></asp:Label>
                                            <br />
                                            <span class="LoginSubNote" lang="en-ph">Quarter / Grading period the assessment will be created 
                                                            on</span></td>
                                    </tr>
                                    <tr>
                                        <td class="style1" valign="top">
                                            <span class="FieldTitle" lang="en-ph">Topic:</span></td>
                                        <td class="style2">
                                        </td>
                                        <td class="style6">
                                            <asp:DropDownList ID="cboTopic" runat="server" 
                                                                CssClass="GridPagerButtons" Width="175px">
                                            </asp:DropDownList>
                                            <span lang="en-ph">&nbsp;</span><br />
                                            <span class="LoginSubNote" lang="en-ph">Select a topic the question belongs</span></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>

                <tr>
                    <td class="style6" valign=top colspan="3">
                        <span class="PageSubHeader" lang="en-ph">Questions
                        <asp:Label ID="vlLoadedQuestions" runat="server" CssClass="ValidationNotice" 
                            Text="*"></asp:Label>
                        </span></td>
                </tr>
                <tr>
                    <td class="style1" valign=top>
                        <span class="FieldTitle" lang="en-ph">Load Questions:</span></td>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style6">
                                <asp:FileUpload ID="fpExcelUploader" runat="server" 
                                    CssClass="NormalText" />
                                <span lang="en-ph">&nbsp;</span><asp:LinkButton ID="lnkUpload" runat="server" 
                                    CssClass="ButtonTemplate" Height="28px" onclick="lnkUpload_Click" 
                            Width="88px">Upload</asp:LinkButton>
                                
                                <br />
                                <span lang="en-ph" class="PageSubHeader">                                
                                <asp:LinkButton ID="linkButton" runat="server" Font-Bold="False" 
                                    Font-Size="9px" ForeColor="#0066FF" onclick="linkButton_Click" 
                                    ToolTip="Download athe template for uploadiung question">Download Question Template</asp:LinkButton>
                                </span>
                                
                                <br />                               
                                
                                <asp:Label ID="lblNotification" runat="server" CssClass="ValidationNotice" 
                                    Text="NOTE: Only MS Excel Files are Allowed" Visible="False"></asp:Label>
                                <br />
                    </td>
                </tr>
                <tr>
                    <td class="style1" valign=top>
                        &nbsp;</td>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style6">
                                &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1" valign=top>
                        &nbsp;</td>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style6">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboQuestionsPerPage" 
    runat="server" Width="100px" 
                                    CssClass="GridPagerButtons" 
    oninit="cboQuestionsPerPage_Init" AutoPostBack="True" 
    onselectedindexchanged="cboQuestionsPerPage_SelectedIndexChanged" Visible="False">
                                        </asp:DropDownList>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="style1" valign=top>
                        &nbsp;</td>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6" valign=top colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6" colspan="3">
                        <asp:UpdatePanel ID="GridViewPanel" runat="server" Visible="False">
                            <ContentTemplate>
                                <span lang="en-ph" class="PageSubHeader">
                                
                                Uploaded Questions
                                <br />
                                <br />
                                <asp:GridView ID="grdLoadedQuestions" runat="server" AutoGenerateColumns="False" 
                                    Width="700px" AllowPaging="True" 
                                    onrowdatabound="grdLoadedQuestions_RowDataBound" PageSize="1" 
                                    onrowediting="grdLoadedQuestions_RowEditing" CssClass="GridPagerButtons" 
                                    onrowcancelingedit="grdLoadedQuestions_RowCancelingEdit" 
                                    onrowupdating="grdLoadedQuestions_RowUpdating" 
                                    onrowdeleting="grdLoadedQuestions_RowDeleting">
                                    <PagerSettings Position="TopAndBottom" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
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
                                                    ToolTip="Edit Question" />
                                                &nbsp;<asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" 
                                                    CommandName="Delete" ImageUrl="~/images/icons/page_delete.gif" Text="Delete" 
                                                    ToolTip="Delete This Question" 
                                                    onclientclick="return confirm('Are you sure you want to Delete this Question?')" />
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question No.">
                                            <EditItemTemplate>
                                                <span class="LoginSubHeader" lang="en-ph">
                                                <asp:Label ID="Label1" runat="server" CssClass="FieldTitle" 
                                                    Text='<%# Eval("RowNo") %>'></asp:Label>
                                                </span>
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                Question No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" CssClass="FieldTitle" 
                                                    Text='<%# Eval("RowNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Width="60px" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question">
                                            <EditItemTemplate>
                                                <span class="LoginSubHeader" lang="en-ph">
                                                <table class="FieldTitle" style="width:100%;">
                                                    <tbody class="style7">
                                                        <tr>
                                                            <td colspan="2">
                                                                <span lang="en-ph">Question:
                                                                </span>
                                                                <asp:TextBox ID="txtQuestion" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Question") %>' Width="500px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <span lang="en-ph">Correct Answer:
                                                                </span>
                                                                <asp:TextBox ID="txtCorrectAnswer" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("CorrectAnswer") %>' Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <caption>
                                                        &lt;<tr>
                                                            <td colspan="2">
                                                                Correct Answer Remark<span lang="en-ph">:
                                                                <asp:TextBox ID="txtCorrectAnswerRemark" runat="server" 
                                                                    CssClass="GridPagerButtons" Text='<%# Eval("CorrectAnswerRemark") %>' 
                                                                    Width="300px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                <span lang="en-ph">Choice 1:
                                                                <asp:TextBox ID="txtChoice1" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Choice1") %>' Width="200px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Remarks:
                                                                <asp:TextBox ID="txtChoice1Remark" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Choice1Remark") %>' Width="300px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Choice 2:
                                                                <asp:TextBox ID="txtChoice2" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Choice2") %>' Width="200px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Remarks:
                                                                <asp:TextBox ID="txtChoice2Remark" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Choice2Remark") %>' Width="300px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Choice 3:
                                                                <asp:TextBox ID="txtChoice3" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Choice3") %>' Width="200px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Remarks:
                                                                <asp:TextBox ID="txtChoice3Remark" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Choice3Remark") %>' Width="300px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Choice 4:
                                                                <asp:TextBox ID="txtChoice4" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Choice4") %>' Width="200px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Remarks:
                                                                <asp:TextBox ID="txtChoice4Remark" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Choice4Remark") %>' Width="300px"></asp:TextBox>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        </tbody>
                                                    </caption>
                                                </table>
                                                </span>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <table class="FieldTitle" style="width:100%;">
                                                    <tbody class="style7">
                                                        <tr>
                                                            <td colspan="2">
                                                                <span lang="en-ph">Question:
                                                                <asp:Label ID="lblQuestion" runat="server" CssClass="GridPagerButtons"><%# Eval("Question") %></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <span lang="en-ph">Correct Answer:
                                                                <asp:Label ID="lblCorrectAnswer" runat="server" CssClass="GridPagerButtons"><%# Eval("CorrectAnswer") %></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                       <tr>
                                                            <td colspan="2">
                                                                Correct Answer Remark<span lang="en-ph">:
                                                                <asp:Label ID="lblCorrectAnswerRemark" runat="server" 
                                                                    CssClass="GridPagerButtons"><%# Eval("CorrectAnswerRemark") %></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Choice 1:
                                                                <asp:Label ID="lblChoice1" runat="server" CssClass="GridPagerButtons"><%# Eval("Choice1") %></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Remarks:
                                                                <asp:Label ID="lblChoice1Remark" runat="server" CssClass="GridPagerButtons"><%# Eval("Choice1Remark")%></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Choice 2:
                                                                <asp:Label ID="lblChoice2" runat="server" CssClass="GridPagerButtons"><%# Eval("Choice2")%></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Remarks:
                                                                <asp:Label ID="lblChoice2Remark" runat="server" CssClass="GridPagerButtons"><%# Eval("Choice2Remark")%></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Choice 3:
                                                                <asp:Label ID="lblChoice3" runat="server" CssClass="GridPagerButtons"><%# Eval("Choice3")%></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Remarks:
                                                                <asp:Label ID="lblChoice3Remark" runat="server" CssClass="GridPagerButtons"><%# Eval("Choice3Remark")%></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Choice 4:
                                                                <asp:Label ID="lblChoice4" runat="server" CssClass="GridPagerButtons"><%# Eval("Choice4")%></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style8">
                                                                &nbsp;</td>
                                                            <td>
                                                                <span lang="en-ph">Remarks:
                                                                <asp:Label ID="lblChoice4Remark" runat="server" CssClass="GridPagerButtons"><%# Eval("Choice4Remark") %></asp:Label>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        <div>
                                            <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkFirst_Click" ToolTip="Go to First Page">First</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;|&nbsp;<asp:LinkButton ID="lnkPrev" runat="server" 
                                                CssClass="GridPagerButtons" onclick="lnkPrev_Click">Previous</asp:LinkButton>
                                            &nbsp;| </span>
                                            <span class="LoginSubHeader" lang="en-ph"><span lang="en-ph">
                                            <asp:Label ID="Label1" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                                CssClass="GridPagerButtons" 
                                                onselectedindexchanged="cboPageNumber_SelectedIndexChanged" 
                                                ToolTip="Jump to Page">
                                            </asp:DropDownList>
                                            &nbsp;<asp:Label ID="lblPageCount" runat="server" CssClass="GridPagerButtons" 
                                                Text="of "></asp:Label>
                                            |
                                            <asp:LinkButton ID="lnkNext" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkNext_Click">Next</asp:LinkButton>
                                            &nbsp;|
                                            <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkLast_Click" ToolTip="Go to Last Page">Last</asp:LinkButton>
                                            </span></span>
                                        </div>
                                    </PagerTemplate>
                                    <PagerStyle BorderWidth="0px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:GridView>
                                </span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="style6" colspan="3">
                                <span lang="en-ph" class="PageSubHeader">                                
                                <table style="width: 700px;">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="background-image: url('../../images/separator.jpg'); background-repeat: repeat-x">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                <span lang="en-ph" class="PageSubHeader">
                                
                                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" onclick="lnkSave_Click" Width="88px">Save</asp:LinkButton>
&nbsp;
                                            <asp:LinkButton ID="lnkCancel" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="88px" onclick="lnkCancel_Click" 
                                                onclientclick="return confirm('Cancel Changes?')">Cancel</asp:LinkButton>
                                </span>
                                        </td>
                                    </tr>
                        </table>
                                </span></td>
                </tr>
            </table>
            </center>
    </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
