<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="questionpool_maintenance_update.aspx.cs" Inherits="PAOnlineAssessment.assessment.questionpool_maintenance_update" %>

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
        </style>
</head>
<body>
    <form id="form1" runat="server">    
    <uc1:frmHeader ID="frmHeader1" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan" class="style3">
            <h2 style="background-color: #FFFFFF"><span lang="en-ph" class="PageHeader">Edit Questions</span></h2>
            <table style="width:100%;">
                <tr class="LoginHeader">
                    <td>
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>
                    </td>
                </tr>
                <tr>
                    <td class="style6" valign=top>
                  <span lang="en-ph" class="PageSubHeader">
                                 Question Field<br />
                  <p>
                      <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
                  </p>
                                <asp:Label ID="Label10" runat="server" Visible="False"></asp:Label>
                                <br />
                                </span>
         <table cellspacing="1" class=style7" >
            <tr>
            <td class="style1" valign="top" ><span class="FieldTitle" lang="en-ph">Type</span></td>
            <td class="style2">&nbsp</td>
                <td class="style6">
                    <asp:Label ID="lblQuestionType" runat="server" Text="Label"></asp:Label>
                </td>
                </tr>
             <tr>
            <td class="style1" valign="top" ><span class="FieldTitle" lang="en-ph">Quarter</span></td>
            <td class="style2">&nbsp</td>
                <td class="style6">
                    <asp:DropDownList ID="ddlQuarter" runat="server" Height="16px" Width="175px">
                        <asp:ListItem>1st</asp:ListItem>
                        <asp:ListItem>2nd</asp:ListItem>
                        <asp:ListItem>3rd</asp:ListItem>
                        <asp:ListItem>4th</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                                            <span class="LoginSubNote" lang="en-ph">Select a Quarter for the question</span></td>
                
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
                <tr>
                <td><span class="FieldTitle" lang="en-ph">Questions</span></td>
                 <td class="style2">&nbsp</td>
                <td class="style6">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtQuestion" runat="server" Height="103px" 
                        TextMode="MultiLine" Width="297px"></asp:TextBox></td>
                            <td>
                                <asp:Image ID="loadPicture" runat="server" Height="83px" Width="90px" 
                        Visible="true" BorderStyle="Solid" BorderWidth="1" />
                                <br />
                                <center>
                                <asp:ImageButton ID="imgPreView" ImageUrl="~/images/icons/page_find.gif" runat="server" ToolTip="Image Preview" Visible="true"  />
                                    <asp:ModalPopupExtender ID="imgPreView_ModalPopupExtender" runat="server" 
                                        BackgroundCssClass="modalBackground" CancelControlID="imgclose" 
                                        DynamicServicePath="" Enabled="True" PopupControlID="PicturePanel" 
                                        TargetControlID="imgPreView">
                                    </asp:ModalPopupExtender>
                                &nbsp;
                                <asp:ImageButton ID="imgRemove" runat="server" 
                                                ImageUrl="~/images/icons/action_stop.gif" ToolTip="Remove Image" 
                                                onclick="imgRemove_Click" Visible="true"/>
                                </center>
                                <asp:Label ID="lblFileName" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                            <td align="left" valign="bottom">
                                <span class="PageSubHeader" lang="en-ph">
                    <asp:Label ID="vlSubjectList1" runat="server" CssClass="ValidationNotice" 
                        Text="*"></asp:Label>
                    </span>
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                     <asp:DropDownList ID="cboCorrect" runat="server" Height="16px" Visible="False" 
                        Width="132px" AutoPostBack="True" 
                        onselectedindexchanged="cboCorrect_SelectedIndexChanged">
                        <asp:ListItem>True</asp:ListItem>
                        <asp:ListItem>False</asp:ListItem>
                    </asp:DropDownList>
                        <span class="PageSubHeader" lang="en-ph">
                        <asp:Label ID="vlSubjectList2" runat="server" CssClass="ValidationNotice" 
                            Text="*"></asp:Label>
                        </span>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                   
                </td>
                </tr>
                </tr>
                <tr>
                  <td><span class="FieldTitle" lang="en-ph">Correct Answer Remarks</span></td>
                   <td class="style2">&nbsp</td>
                <td class="style6">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="295px"></asp:TextBox>
                </td>
                </tr>
     
                <tr>
                  <td><span class="FieldTitle" lang="en-ph" >Choices</span></td>
                   <td class="style2">&nbsp</td>
                <td>
                <table>
                <tr>
                    <th>
                        Choices
                    </th>
                    <th>
                        Remarks
                    </th>
                </tr>
                    <tr>
                        <td > 
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                               <asp:Label ID="Label6"  runat="server" Visible="False" Text="Choice 1" ></asp:Label>
                                <asp:TextBox ID="txtChoice1" runat="server" Width="124px"></asp:TextBox>
                                        <span class="PageSubHeader" lang="en-ph">
                                        <asp:Label ID="vlSubjectList5" runat="server" CssClass="ValidationNotice" 
                                            Text="*"></asp:Label>
                                        </span>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                     
                        </td>
                        <td>
                             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                   <asp:TextBox ID="txtRemark1" runat="server" Width="124px"></asp:TextBox>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            
                        </td>
                    </tr>
                    <tr>
                        <td> <asp:Label ID="Label7"  runat="server" Visible="False" Text="Choice 2" ></asp:Label>
                    <asp:TextBox ID="txtChoice2" runat="server" Width="124px"></asp:TextBox>
                            <span class="PageSubHeader" lang="en-ph">
                            <asp:Label ID="vlSubjectList6" runat="server" CssClass="ValidationNotice" 
                                Text="*"></asp:Label>
                            </span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark2" runat="server" Width="124px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td> <asp:Label ID="Label8"  runat="server" Visible="False" Text="Choice 3" ></asp:Label>
                   <asp:TextBox ID="txtChoice3" runat="server" Width="124px"></asp:TextBox>
                            <span class="PageSubHeader" lang="en-ph">
                            <asp:Label ID="vlSubjectList8" runat="server" CssClass="ValidationNotice" 
                                Text="*"></asp:Label>
                            </span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark3" runat="server" Width="124px"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td> <asp:Label ID="Label9"  runat="server" Visible="False" Text="Choice 4" ></asp:Label>
                    <asp:TextBox ID="txtChoice4" runat="server" Width="124px"></asp:TextBox>
                            <span class="PageSubHeader" lang="en-ph">
                            <asp:Label ID="vlSubjectList10" runat="server" CssClass="ValidationNotice" 
                                Text="*"></asp:Label>
                            </span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark4" runat="server" Width="124px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;</td>
            </tr>
    </table>
                        <center>
                                <br />
                        </center>
    
                        <br />

                    </td>
                </tr>
                <tr>
                                        <td >
                                            <span class="ValidationNotice" lang="en-ph">Fields marked with an (*) are 
                                            required.</span></td>
                                   
                                    </tr>
                                    <tr>
                                        <td class="style1" align="center">
                                            <asp:Button ID="btnSubmit" runat="server" BackColor="Transparent" 
                                                BorderStyle="None" CssClass="ButtonTemplate" Height="34px" Text="Submit" 
                                                Width="88px" onclick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" BackColor="Transparent" 
                                                BorderStyle="None" CssClass="ButtonTemplate" Height="34px" Text="cancel" 
                                                Width="88px" onclick="btnCancel_Click" /></td>
                                    
                                    </tr>
            </table>
            <asp:Panel ID="PicturePanel" runat="server" BackColor="White">
                        <table width="500px">
                        <tr>
                            <td align="right"><asp:ImageButton ID="imgclose" runat="server" ImageUrl="~/images/icons/action_stop.gif" /></td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td align="center"><asp:Image ID="panelpicture" runat="server" Width="400px" Height="300px" /></td>
                        </tr>
                        <tr><td>
                            <asp:HiddenField ID="hidChoice1" runat="server" />
                            <asp:HiddenField ID="hidChoice2" runat="server" />
                            <asp:HiddenField ID="hidChoice3" runat="server" />
                            <br />
                            <asp:HiddenField ID="hidRemark1" runat="server" />
                            <asp:HiddenField ID="hidRemark2" runat="server" />
                            <asp:HiddenField ID="hidRemark3" runat="server" />
                            </td></tr>
                        </table>
                        </asp:Panel>
        </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
