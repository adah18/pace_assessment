<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="questionpool_maintenance_main.aspx.cs" Inherits="PAOnlineAssessment.questionpool_maintenance_main" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Questions Pool Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
        .style3
        {
            width: 232px;
            text-align: left;
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
        	<h2 style="background-color: #FFFFFF"><span class="PageHeader" >Questions Pool</span></h2>
            <table style="width:100%;">
                <tr>
                    <td colspan="3">
                    <center>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <span  class="PageSubHeader">
                                    <asp:Panel ID="pnlQuestionsPerPage" runat="server" Width="700px">
                                        <div class="style1">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style3">
                                                        <span class="PageSubHeader" >
                                                        <asp:Label ID="Label4" runat="server" CssClass="GridPagerButtons" 
                                                            Text="Questions Per Page:"></asp:Label>
                                                        &nbsp;<asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="True" 
                                                            CssClass="GridPagerButtons" 
                                                            onselectedindexchanged="cboPageSize_SelectedIndexChanged" Width="50px">
                                                        </asp:DropDownList>
                                                        </span>
                                                    </td>
                                                    <td>
                                                        <span class="FieldTitle" lang="en-ph">Quarter:</span>&nbsp<asp:DropDownList 
                                                            ID="ddlQuarter" runat="server" AutoPostBack="True"  
                                                            CssClass="GridPagerButtons" 
                                                            onselectedindexchanged="ddlQuarter_SelectedIndexChanged"  Width="40px">
                                                            <asp:ListItem Value="1st">1st</asp:ListItem>
                                                            <asp:ListItem Value="2nd">2nd</asp:ListItem>
                                                            <asp:ListItem Value="3rd">3rd</asp:ListItem>
                                                            <asp:ListItem Value="4th">4th</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtSearchQuery" runat="server" CssClass="GridPagerButtons" 
                                                            Width="175px"></asp:TextBox>
                                                        <asp:DropDownList ID="cboTopic" Width="100px" CssClass="GridPagerButtons" runat="server" 
                                                            AutoPostBack="True" onselectedindexchanged="cboTopic_SelectedIndexChanged" 
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                        <span >&nbsp;</span><asp:DropDownList ID="cboSearchQuery" runat="server" 
                                                            CssClass="GridPagerButtons" AutoPostBack="True" 
                                                            onselectedindexchanged="cboSearchQuery_SelectedIndexChanged"  Width="100px">
                                                            <asp:ListItem Value="Subject">Subject</asp:ListItem>
                                                            <asp:ListItem Value="Level">Grade / Level</asp:ListItem>                                                            
                                                            <asp:ListItem Value="Question">Question</asp:ListItem>
                                                            <asp:ListItem>Topic</asp:ListItem>
                                                            <asp:ListItem Value="A">A - Available</asp:ListItem>
                                                            <asp:ListItem Value="D">D - Deactivated</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <span >&nbsp;</span><asp:ImageButton ID="imgSearchQuery" runat="server" 
                                                            CssClass="GridPagerButtons" ImageUrl="~/images/icons/page_find.gif" 
                                                            onclick="imgSearchQuery_Click" ToolTip="Search" />
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                <asp:GridView ID="grdLoadedQuestions" runat="server" AutoGenerateColumns="False" 
                                    Width="700px" AllowPaging="True" 
                                    onrowdatabound="grdLoadedQuestions_RowDataBound" PageSize="5" 
                                    CssClass="GridPagerButtons" onrowcommand="grdLoadedQuestions_RowCommand">
                                    <PagerSettings Position="TopAndBottom" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" 
                                                    CommandName="Update" ImageUrl="~/images/icons/page_tick.gif" Text="Update" />
                                                &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                    CommandName="Cancel" ImageUrl="~/images/icons/action_stop.gif" Text="Cancel" />
                                                &nbsp;<asp:Label ID="lblQuestionPoolID" runat="server" CssClass="GridPagerButtons"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" 
                                                    ImageUrl="~/images/icons/page_edit.gif" Text="Edit" />
                                                &nbsp;<asp:ImageButton ID="imgDeactivate" runat="server" CausesValidation="False" 
                                                    CommandName="Delete" ImageUrl="~/images/icons/page_delete.gif" Text="Delete" Visible="false" />
                                                <asp:Label ID="lblQuestionPoolID" runat="server" CssClass="GridPagerButtons" 
                                                    Text='<%# Eval("QuestionPoolID") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question No.">
                                            <EditItemTemplate>
                                                <span class="LoginSubHeader" >
                                                <asp:Label ID="Label1" runat="server" CssClass="FieldTitle" 
                                                    Text='<%# Eval("RowNo") %>'></asp:Label>
                                                </span>
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                Question No.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" CssClass="GridPagerButtons" 
                                                    Text='<%# Eval("RowNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Width="60px" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" 
                                                    Text='<%# Bind("SubjectDescription") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SubjectDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grade / Level">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" 
                                                    Text='<%# Bind("LevelDescription") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass="GridPagerButtons" 
                                                    Text='<%# Bind("LevelDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Question">
                                            <EditItemTemplate>
                                                <span class="LoginSubHeader" >
                                                <asp:Panel ID="pnlQuestionsPanel" runat="server" ScrollBars="Horizontal" 
                                                    Width="700px">
                                                    <span class="PageSubHeader" ><span class="LoginSubHeader" 
                                                        >
                                                    <table class="FieldTitle" style="width:100%;">
                                                        <tbody class="style7">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <span >Question: </span>
                                                                    <asp:TextBox ID="txtQuestion" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Question") %>' Width="500px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <span >Correct Answer: </span>
                                                                    <asp:TextBox ID="txtCorrectAnswer" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("CorrectAnswer") %>' Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <span >Remarks:
                                                                    <asp:TextBox ID="txtCorrectAnswerRemark" runat="server" 
                                                                        CssClass="GridPagerButtons" Text='<%# Eval("CorrectAnswerRemark") %>' 
                                                                        Width="300px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span >Choice 1:
                                                                    <asp:TextBox ID="txtChoice1" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Choice1") %>' Width="200px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span >Remarks:
                                                                    <asp:TextBox ID="txtChoice1Remark" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Choice1Remark") %>' Width="300px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span >Choice 2:
                                                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Choice2") %>' Width="200px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span >Remarks:
                                                                    <asp:TextBox ID="txtChoice2Remark" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Choice2Remark") %>' Width="300px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span >Choice 3:
                                                                    <asp:TextBox ID="txtChoice3" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Choice3") %>' Width="200px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span >Remarks:
                                                                    <asp:TextBox ID="txtChoice3Remark" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Choice3Remark") %>' Width="300px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span >Choice 4:
                                                                    <asp:TextBox ID="txtChoice4" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Choice4") %>' Width="200px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style8">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span >Remarks:
                                                                    <asp:TextBox ID="txtChoice4Remark" runat="server" CssClass="GridPagerButtons" 
                                                                        Text='<%# Eval("Choice4Remark") %>' Width="300px"></asp:TextBox>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    </span></span>
                                                </asp:Panel>
                                                </span>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Panel ID="pnlQuestionsPanel" runat="server">
                                                    <span class="PageSubHeader">
                                                    <table class="FieldTitle" style="width:100%;">
                                                        <tbody class="style7">
                                                            <tr>
                                                                <td>
                                                                    <span class="LoginSubNote">Question:
                                                                    <asp:Label ID="lblQuestion" runat="server" CssClass="GridPagerButtons"><%# Eval("Question") %></asp:Label>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="LoginSubNote">
                                                                    <span lang="en-ph">Question Type: </span>
                                                                    <asp:Label ID="lblQuestionType" runat="server" CssClass="GridPagerButtons"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="LoginSubNote">
                                                                    <span lang="en-ph">Has Picture: </span>
                                                                    <asp:Label ID="lblHasPicture" runat="server" CssClass="GridPagerButtons"><%# Eval("HasImage") %></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="PageSubHeader">
                                                                    <asp:LinkButton ID="lnkShowHide" runat="server" CommandName="ShowHide" 
                                                                        CssClass="GridPagerButtons">Show Details</asp:LinkButton>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlChoices" runat="server" Visible="False">
                                                                        <table style="width:100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Correct Answer: </span>
                                                                                    <asp:Label ID="lblCorrectAnswer" runat="server" CssClass="GridPagerButtons" 
                                                                                        Text='<%# Eval("CorrectAnswer") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Remarks: </span>
                                                                                    <asp:Label ID="lblCorrectAnswerRemark" runat="server" 
                                                                                        CssClass="GridPagerButtons" Text='<%# Eval("CorrectAnswerRemark") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Choice 1: </span>
                                                                                    <asp:Label ID="lblChoice1" runat="server" CssClass="GridPagerButtons" Text='<%# Eval("Choice1") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Remarks: </span>
                                                                                    <asp:Label ID="lblChoice1Remark" runat="server" CssClass="LinkButtonTemplate" Text='<%# Eval("Choice1Remark") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <span class="PageSubHeader">
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Choice 2: </span>
                                                                                    <asp:Label ID="lblChoice2" runat="server" CssClass="GridPagerButtons" Text='<%# Eval("Choice2") %>'></asp:Label>
                                                                                </td>
                                                                                </span>
                                                                            </tr>
                                                                            <tr>
                                                                                <span class="PageSubHeader">
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Remarks: </span>
                                                                                    <asp:Label ID="lblChoice2Remark" runat="server" CssClass="LinkButtonTemplate" Text='<%# Eval("Choice2Remark") %>'></asp:Label>
                                                                                </td>
                                                                                </span>
                                                                            </tr>
                                                                            <tr>
                                                                                <span class="PageSubHeader">
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Choice 3: </span>
                                                                                    <asp:Label ID="lblChoice3" runat="server" CssClass="GridPagerButtons" Text='<%# Eval("Choice3") %>'></asp:Label>
                                                                                </td>
                                                                                </span>
                                                                            </tr>
                                                                            <tr>
                                                                                <span class="PageSubHeader">
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Remarks: </span>
                                                                                    <asp:Label ID="lblChoice3Remark" runat="server" CssClass="LinkButtonTemplate" Text='<%# Eval("Choice3Remark") %>'></asp:Label>
                                                                                </td>
                                                                                </span>
                                                                            </tr>
                                                                            <tr>
                                                                                <span class="PageSubHeader">
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Choice 4: </span>
                                                                                    <asp:Label ID="lblChoice4" runat="server" CssClass="GridPagerButtons" Text='<%# Eval("Choice4") %>'></asp:Label>
                                                                                </td>
                                                                                </span>
                                                                            </tr>
                                                                            <tr>
                                                                                <span class="PageSubHeader">
                                                                                <td>
                                                                                    <span class="LoginSubNote" lang="en-ph">Remarks: </span>
                                                                                    <asp:Label ID="lblChoice4Remark" runat="server" CssClass="LinkButtonTemplate" Text='<%# Eval("Choice4Remark") %>'></asp:Label>
                                                                                </td>
                                                                                </span>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    </span>
                                                </asp:Panel>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Status" HeaderText="Status">
                                            <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Width="60px" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerTemplate>
                                        <div>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style1">
                                                        <span class="PageSubHeader" >
                                                        <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                                            onclick="lnkFirst_Click" ToolTip="Go to First Page">First</asp:LinkButton>
                                                        <span >&nbsp;|&nbsp;<asp:LinkButton ID="lnkPrev" runat="server" 
                                                            CssClass="GridPagerButtons" onclick="lnkPrev_Click">Previous</asp:LinkButton>
                                                        &nbsp;| </span><span class="LoginSubHeader" ><span >
                                                        <asp:Label ID="Label3" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                                        &nbsp;<asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                                            CssClass="GridPagerButtons" 
                                                            onselectedindexchanged="cboPageNumber_SelectedIndexChanged" 
                                                            ToolTip="Jump to Page"  Width="50px">
                                                        </asp:DropDownList>
                                                        &nbsp;<asp:Label ID="lblPageCount" runat="server" CssClass="GridPagerButtons" 
                                                            Text="of "></asp:Label>
                                                        |
                                                        <asp:LinkButton ID="lnkNext" runat="server" CssClass="GridPagerButtons" 
                                                            onclick="lnkNext_Click">Next</asp:LinkButton>
                                                        &nbsp;|
                                                        <asp:LinkButton ID="lnkLast" runat="server" CssClass="GridPagerButtons" 
                                                            onclick="lnkLast_Click" ToolTip="Go to Last Page">Last</asp:LinkButton>
                                                        </span></span></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </PagerTemplate>
                                    <PagerStyle BorderWidth="0px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <EmptyDataTemplate>
                                        <br />
                                        No Record Found.
                                        <br />
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                </span>
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
