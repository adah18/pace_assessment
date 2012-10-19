<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="questionpool_maintenance_manual.aspx.cs" Inherits="PAOnlineAssessment.assessment.questionpool_maintenance_manual" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>

<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.DynamicData" tagprefix="cc1" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
        .style10
        {
            height: 22px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan" class="style3">
            <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">Add Questions</span></h2>
            <center>
            <table style="width:100%;">
                <tr>
                  <td colspan="3" class="style10">
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
                                            <span class="LoginSubNote" lang="en-ph">Subject of the assessment will be created 
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
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
                  <span lang="en-ph" class="PageSubHeader">
                                 Question Field
                                <asp:Label ID="vlSubjectList0" runat="server" 
                      CssClass="ValidationNotice" Text="*" ></asp:Label>
                                <br />
                  <h6>Fields with <span class="ValidationNotice">*</span> are required</h6>
                                <br />
                                </span>
         <table cellspacing="1" class=style7" >
            <tr>
            <td class="style1" valign="top" ><span class="FieldTitle" lang="en-ph">Type</span></td>
            <td class="style2">&nbsp</td>
                <td class="style6">
                    <asp:DropDownList ID="cboType" runat="server" CssClass="GridPagerButtons" Width="175px" AutoPostBack="True"  
                        onselectedindexchanged="cboType_SelectedIndexChanged">
                        <asp:ListItem Value="0">-Select Type-</asp:ListItem>
                        <asp:ListItem Value="1">True or False</asp:ListItem>
                        <asp:ListItem Value="2">Multiple Choice</asp:ListItem>
                        <asp:ListItem Value="3">Fill in the Blanks</asp:ListItem>
                    </asp:DropDownList>
                    <span class="ValidationNotice">*</span></td>
                </tr>
                <tr>
                <td><span class="FieldTitle" lang="en-ph">Questions</span></td>
                 <td class="style2">&nbsp</td>
                <td class="style6">
              
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:TextBox ID="txtQuestion" runat="server" Height="103px" 
                        TextMode="MultiLine" Width="297px"></asp:TextBox>
                                <span class="ValidationNotice">*</span></td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Image ID="loadPicture" runat="server" Height="83px" Width="90px" 
                                            Visible="true" BorderStyle="Solid" ImageUrl="~/images/dashboard_icons/attachment.png" BorderWidth="1" />
                                
                                            <asp:Label ID="lblFileName" runat="server" Text="" Visible="false"></asp:Label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="imgPreView" ImageUrl="~/images/icons/page_find.gif" 
                                                runat="server" ToolTip="Image Preview" Visible="true" Height="16px"  />
                                            
                                            <asp:ModalPopupExtender ID="imgPreView_ModalPopupExtender" runat="server" 
                                                CancelControlID="imgclose" DynamicServicePath="" Enabled="True" 
                                                PopupControlID="PicturePanel" TargetControlID="imgPreView" 
                                                DropShadow="True" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
                                            
                                            &nbsp;
                                            <asp:ImageButton ID="imgRemove" runat="server" 
                                                ImageUrl="~/images/icons/action_stop.gif" ToolTip="Remove Image" 
                                                onclick="imgRemove_Click" Visible="true"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                
                <tr>
                   <td class="style11"><span class="FieldTitle" lang="en-ph"></span></td>
                   <td></td>
                   <td>
                       <asp:FileUpload ID="fileUpload" runat="server" CssClass="NormalText" />
                       <asp:LinkButton ID="lnkUpload" runat="server" CssClass="ButtonTemplate" Height="28px"
                            Width="88px" onclick="lnkUpload_Click">Upload</asp:LinkButton>
   
                            <br />
                            <asp:Label ID="l" runat="server" Width="196px" class="LoginSubNote" Text="Insert Picture in the Question" Visible="true"></asp:Label>
                            <br />
                            <asp:Label ID="lblNotification" runat="server" CssClass="ValidationNotice" 
                                    Text="Note: Only Picture / Image Files are allowed." Visible="true"></asp:Label>
                            <br />
                   </td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                
                <tr>
                  <td><span class="FieldTitle" lang="en-ph">Correct Answer</span></td>
                   <td class="style2">&nbsp</td>
                <td class="style6">
                    <asp:TextBox ID="txtCorrect" runat="server" Width="295px"></asp:TextBox>
                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>--%>
                            <asp:DropDownList ID="cboCorrect" runat="server" Height="16px" Visible="False" 
                                Width="132px" AutoPostBack="true" 
                        onselectedindexchanged="cboCorrect_SelectedIndexChanged">
                                <asp:ListItem>True</asp:ListItem>
                                <asp:ListItem>False</asp:ListItem>
                           </asp:DropDownList>
                      <span class="ValidationNotice">*</span><%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <br />
                    <span class="LoginSubNote"><span lang="en-ph">This will be the correct answer in 
                    the question</span></span></td>
                </tr>
                </tr>
                
                <tr>
                  <td><span class="FieldTitle" lang="en-ph">Correct Answer Remarks</span></td>
                   <td class="style2">&nbsp</td>
                <td class="style6">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="295px"></asp:TextBox>
                    <br />
                    <span class="LoginSubNote"><span lang="en-ph">This will be the correct remark 
                    for the student</span></span></td>
                </tr>
     
                <tr>
                  <td><span class="FieldTitle" lang="en-ph" style="display:none">Choices</span></td>
                   <td class="style2">&nbsp</td>
                <td>
                <% if(cboType.SelectedIndex != 3 && cboType.SelectedIndex != 0){ %>
                <table>
                <tr>
                    <th>
                        Choices
                    <span class="ValidationNotice">*</span></th>
                    <th>
                        Remarks
                    </th>
                </tr>
                    <tr>
                        <td > <asp:Label ID="Label6"  runat="server" Visible="False" Text="Choice 1" ></asp:Label>
                    <asp:TextBox ID="txtChoice1" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td> <asp:Label ID="Label7"  runat="server" Visible="False" Text="Choice 2" ></asp:Label>
                    <asp:TextBox ID="txtChoice2" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td> <asp:Label ID="Label8"  runat="server" Visible="False" Text="Choice 3" ></asp:Label>
                   <asp:TextBox ID="txtChoice3" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark3" runat="server"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td> <asp:Label ID="Label9"  runat="server" Visible="False" Text="Choice 4" ></asp:Label>
                    <asp:TextBox ID="txtChoice4" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark4" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <% } %>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;</td>
            </tr>
    </table>
           </ContentTemplate>
           <Triggers>
            <asp:PostBackTrigger ControlID="lnkUpload" />
           </Triggers>
        </asp:UpdatePanel>
              <center>
                                <span lang="en-ph" class="PageSubHeader">                                
                                
                                            
                                </span>
                
                    <br />
                <asp:LinkButton ID="lnkSave0" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="88px" 
                        onclick="lnkSave0_Click">Add</asp:LinkButton>
                                <br />
                    <asp:Label ID="Label4" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                <br />
                        </center>
    
        <br />

                    </td>
                </tr>
                       <tr>
                    <td class="style1" valign=top>
                        <span class="FieldTitle" lang="en-ph">Questions Per Page:         &nbsp;</td>
                    <td class="style6">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboQuestionsPerPage" 
    runat="server" Width="100px" 
                                    CssClass="GridPagerButtons" 
    oninit="cboQuestionsPerPage_Init" AutoPostBack="True" 
    onselectedindexchanged="cboQuestionsPerPage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                        <span class="LoginSubNote" lang="en-ph">Shows the number of questions to display 
                                        per page</span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="style6" colspan="3">
                        <asp:UpdatePanel ID="GridViewPanel" runat="server" Visible="true">
                            <ContentTemplate>
                                <span lang="en-ph" class="PageSubHeader">
                                
                                Added Questions
                                <br />
                                <asp:GridView ID="grdAddedQuestion" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" CssClass="GridPagerButtons" PageSize="1" 
                                    Width="700px" onrowdatabound="grdAddedQuestion_RowDataBound" 
                                    onrowdeleting="grdAddedQuestion_RowDeleting" 
                                    onrowediting="grdAddedQuestion_RowEditing">
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
                                                    ToolTip="Edit Question" Visible="false"/>
                                                &nbsp;<asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" 
                                                    CommandName="Delete" ImageUrl="~/images/icons/page_delete.gif" 
                                                    onclientclick="return confirm('Are you sure you want to Delete this Question?')" 
                                                    Text="Delete" ToolTip="Delete This Question" />
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
                                                <asp:Label ID="Label10" runat="server" CssClass="FieldTitle" 
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
                                                                <span lang="en-ph">Question: </span>
                                                                <asp:TextBox ID="txtQuestion0" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("Question") %>' Width="500px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <span lang="en-ph">Correct Answer: </span>
                                                                <asp:TextBox ID="txtCorrectAnswer" runat="server" CssClass="GridPagerButtons" 
                                                                    Text='<%# Eval("CorrectAnswer") %>' Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <span lang="en-ph">Remarks:
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
                                                                <asp:TextBox ID="txtChoice5" runat="server" CssClass="GridPagerButtons" 
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
                                                                <asp:TextBox ID="txtChoice6" runat="server" CssClass="GridPagerButtons" 
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
                                                                <asp:TextBox ID="txtChoice7" runat="server" CssClass="GridPagerButtons" 
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
                                                                <asp:TextBox ID="txtChoice8" runat="server" CssClass="GridPagerButtons" 
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
                                                            <td rowspan="5">
                                                                <asp:Image ID="img" ImageUrl='<%# "uploaded_images/temp_file/" + Eval("ImageFileName") %>' runat="server" Height="100px" Width="100px" />
                                                                <asp:Label ID="lblFileName" runat="server" Visible="false" Text='<%# Eval("ImageFileName")  %>' ></asp:Label>
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <span lang="en-ph">Remarks:
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
                                    <EmptyDataTemplate>
                                        <br />
                                            <center>
                                            <span class="GridPagerButtons">No Question.</span>
                                            </center>
                                        <br />
                                    </EmptyDataTemplate>
                                    <PagerTemplate>
                                        <div>
                                            <asp:LinkButton ID="lnkFirst" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkFirst_Click" ToolTip="Go to First Page">First</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;|&nbsp;<asp:LinkButton ID="lnkPrev" runat="server" 
                                                CssClass="GridPagerButtons" onclick="lnkPrev_Click">Previous</asp:LinkButton>
                                            &nbsp;| </span><span class="LoginSubHeader" lang="en-ph"><span lang="en-ph">
                                            <asp:Label ID="Label11" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="cboPageNumber" runat="server" AutoPostBack="True" 
                                                CssClass="GridPagerButtons" 
                                                onselectedindexchanged="cboPageNumber_SelectedIndexChanged" 
                                                ToolTip="Jump to Page"  Width="40px">
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
                                                Height="28px" Width="88px" onclick="lnkSave_Click">Save</asp:LinkButton>
&nbsp;
                                            <asp:LinkButton ID="lnkCancel" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="88px"
                                                onclientclick="return confirm('Cancel Changes?')" 
                                                onclick="lnkCancel_Click">Cancel</asp:LinkButton>
                                </span>
                                        </td>
                                    </tr>
                        </table>
                                </span></td>
                </tr>
            </table>
            </center>
            <center>
            <asp:Panel ID="PicturePanel" runat="server" BackColor="White">
                        <table width="500px">
                        <tr>
                            <td align="right"><asp:ImageButton ID="imgclose" runat="server" ImageUrl="~/images/icons/action_stop.gif" /></td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td align="center"><asp:Image ID="panelpicture" runat="server" Width="400px" Height="300px" /></td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        </table>
                        </asp:Panel>
         </center>                        
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
