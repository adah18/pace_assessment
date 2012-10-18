<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="quizcreation_addupdate.aspx.cs" Inherits="PAOnlineAssessment.assessment.quizcreation_addupdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../frmHeader.ascx" tagname="frmHeader" tagprefix="uc1" %>
<%@ Register src="../frmFooter.ascx" tagname="frmFooter" tagprefix="uc2" %>

<%@ Register src="../SiteMap.ascx" tagname="SiteMap" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assessment Maintenance - Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />
    <script src="datepicker/src/js/jscal2.js"></script> 
    <script src="datepicker/src/js/lang/en.js"></script> 
    <link rel="stylesheet" type="text/css" href="datepicker/src/css/jscal2.css" /> 
    <link rel="stylesheet" type="text/css" href="datepicker/src/css/border-radius.css" /> 
    <link rel="stylesheet" type="text/css" href="datepicker/src/css/steel/steel.css" /> 
</head>
<body>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
    <uc1:frmHeader ID="frmHeader2" runat="server" />
    <div id="bodytopmainPan">
        <div id="bodytopPan">
        <h2 style="background-color: #FFFFFF"><span class="PageHeader" lang="en-ph">Create Assessment</span></h2>
        <uc3:SiteMap ID="SiteMap1" runat="server" Visible="false" />
        <table>
        <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
        <table cellpadding="1" cellspacing="1" style="width:100%;">
        
        <tr>
            <td><span class="PageSubHeader" lang="en-ph">Course Details</span></td>
        </tr>
        
        <tr>
            <td valign="top">
                <span class="FieldTitle" lang="en-ph">Level</span>
            </td>
            <td>
                <asp:DropDownList 
                    ID="ddlLevel" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlLevel_SelectedIndexChanged" 
                    CssClass="GridPagerButtons" Width="200px">
                </asp:DropDownList>
                <asp:Label ID="lblLevel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                <br />
                <span class="LoginSubNote" lang="en-ph">Grade / Year Level the assessment will be created for.</span>
            </td>
        </tr>
        
            <tr>
                <td valign="top">
                    <span class="FieldTitle" lang="en-ph">Subject</span>&nbsp
                </td>
                <td>
                    <asp:DropDownList 
                        ID="ddlSubject" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlSubject_SelectedIndexChanged" 
                        CssClass="GridPagerButtons" Width="200px">
                    </asp:DropDownList>
                    <asp:Label ID="lblSubject" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    <br />
                    <span class="LoginSubNote" lang="en-ph">Subject the assessment will be created for.</span>
                </td>
            </tr>
        
        <tr>
            <td valign="top">
                    <span class="FieldTitle" lang="en-ph">Quarter</span>&nbsp
                </td>
            <td>
                            
                <asp:DropDownList 
                                ID="ddlQuarter" runat="server" AutoPostBack="True"  
                                CssClass="GridPagerButtons"
                                onselectedindexchanged="ddlQuarter_SelectedIndexChanged" Width="200px" Visible="True" Enabled="false">
                                <asp:ListItem Value="1st">1st</asp:ListItem>
                                <asp:ListItem Value="2nd">2nd</asp:ListItem>
                                <asp:ListItem Value="3rd">3rd</asp:ListItem>
                                <asp:ListItem Value="4th">4th</asp:ListItem>
                            </asp:DropDownList>
                            
                <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                <br />
                    <span class="LoginSubNote" lang="en-ph">Quarter / Grading Period the assessment will be created for.</span>
            </td>
        </tr>
        
        <tr><td colspan="2" >&nbsp;</td></tr>
        <tr><td colspan="2" ><span class="PageSubHeader" lang="en-ph">Assessment Details</span></td></tr>
        
        <tr>
            <td align="left" valign="top" ><span class="FieldTitle" lang="en-ph">Assessment Type:</span></td>
            <td align="left" >
                <asp:DropDownList ID="ddlAssessmentType" runat="server" Width="200px" CssClass="GridPagerButtons">
                </asp:DropDownList >
                <asp:Label ID="lblQuiz0"
                    runat="server" Text="*" ForeColor="Red"></asp:Label>
                <br />
                <span class="LoginSubNote" lang="en-ph">Type of Assessment you want to create.</span>
            </td>
        </tr>
        <tr>
            <td valign="top"><span class="FieldTitle" lang="en-ph">Title</span></td>
            <td><asp:TextBox ID="txtTitle" CssClass="NormalText" runat="server" Width="460px"></asp:TextBox>
                <asp:Label ID="lblQuiz"
                    runat="server" Text="*" ForeColor="Red"></asp:Label>
                <br />
                <span class="LoginSubNote" lang="en-ph">Title of the assessment that will be 
                created.</span>
            </td>
        </tr>
        <tr>
            <td valign="top" ><span class="FieldTitle" lang="en-ph">Introduction</span></td>
            <td >
                <asp:TextBox ID="txtIntroduction" CssClass="NormalText" runat="server" TextMode="MultiLine" 
                    Height="150px" Width="460px"></asp:TextBox>
                <asp:Label ID="lblIntroduction"
                    runat="server" Text="*" ForeColor="Red"></asp:Label>
                <br />
                <span class="LoginSubNote" lang="en-ph">Introduction for the assessment that will be 
                created.</span>
            </td>
        </tr>
        
        <tr>
            <td>&nbsp;</td>
        </tr>
        
        
        
        <tr>
            <td colspan="2">
                <span class="PageSubHeader" lang="en-ph">Schedule Details</span>
                &nbsp;
                <asp:Label ID="lblSchedule" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td valign="top">
                <span class="FieldTitle" lang="en-ph"><asp:Label ID="Label4" runat="server" Text="Type"></asp:Label></span>
            </td>
                
            <td>
                <span class="FieldTitle" lang="en-ph">
                <asp:RadioButtonList ID="rdoSchedule" runat="server"  
                    RepeatDirection="Horizontal" 
                    onselectedindexchanged="rdoSchedule_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="Auto" Text="Open/Close"></asp:ListItem>
                <asp:ListItem Value="Manual" Text="Manually"></asp:ListItem>
                </asp:RadioButtonList>
                <span class="LoginSubNote" lang="en-ph">Type of schedule for the assessment that will be created.</span>
            </td>
        </tr>
        
        <tr>
            <td valign="top"><span class="FieldTitle" lang="en-ph">
                <asp:Label ID="lblCaptionDate" runat="server" Text="Date/Time"></asp:Label></span></td>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td><span class="FieldTitle" lang="en-ph"><asp:Label ID="lblCaptionDateFrom" runat="server" Text="From:"></asp:Label></span>
                        &nbsp;
                            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>&nbsp;
                            <asp:ImageButton ID="imgStartDate" ImageUrl="~/images/icons/calendar.gif" 
                                runat="server" Width="16px" />
                         
                            <asp:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtStartDate" Format="MMMM d, yyyy" PopupButtonID="imgStartDate" >
                            </asp:CalendarExtender>
                         &nbsp; 
                         &nbsp;
                            <span class="FieldTitle" lang="en-ph"><asp:Label ID="lblCaptionDateTo" runat="server" Text="To:"></asp:Label></span> &nbsp
                            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                            &nbsp; 
                            <asp:ImageButton ID="imgEndDate" ImageUrl="~/images/icons/calendar.gif" 
                                runat="server" Width="16px" />
                         
                            <asp:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtEndDate" Format="MMMM d, yyyy" PopupButtonID="imgEndDate" >
                            </asp:CalendarExtender>
                            </td>
                    </tr>
                    
                    <tr>
                        
                        <td ><span class="FieldTitle" lang="en-ph"><asp:Label ID="lblCaptionTimeFrom" runat="server" Text="From:"></asp:Label></span> &nbsp&nbsp<asp:DropDownList 
                                ID="ddlStartHour" runat="server" CssClass="GridPagerButtons" Width="40px">
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>&nbsp&nbsp<asp:DropDownList ID="ddlStartMin" runat="server" 
                                CssClass="GridPagerButtons" Width="40px">
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                            <asp:ListItem>32</asp:ListItem>
                            <asp:ListItem>33</asp:ListItem>
                            <asp:ListItem>34</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>36</asp:ListItem>
                            <asp:ListItem>37</asp:ListItem>
                            <asp:ListItem>38</asp:ListItem>
                            <asp:ListItem>39</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>41</asp:ListItem>
                            <asp:ListItem>42</asp:ListItem>
                            <asp:ListItem>43</asp:ListItem>
                            <asp:ListItem>44</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>46</asp:ListItem>
                            <asp:ListItem>47</asp:ListItem>
                            <asp:ListItem>48</asp:ListItem>
                            <asp:ListItem>49</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>51</asp:ListItem>
                            <asp:ListItem>52</asp:ListItem>
                            <asp:ListItem>53</asp:ListItem>
                            <asp:ListItem>54</asp:ListItem>
                            <asp:ListItem>55</asp:ListItem>
                            <asp:ListItem>56</asp:ListItem>
                            <asp:ListItem>57</asp:ListItem>
                            <asp:ListItem>58</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>&nbsp
                            <asp:DropDownList ID="ddlStartAMPM" runat="server" CssClass="GridPagerButtons" Width="40px">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                            </asp:DropDownList>&nbsp&nbsp 
                            <span class="FieldTitle" lang="en-ph"><asp:Label ID="lblCaptionTimeTo" runat="server" Text="To:"></asp:Label></span>&nbsp<asp:DropDownList 
                                ID="ddlEndHour" runat="server" Width="40px" CssClass="GridPagerButtons">
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>&nbsp&nbsp<asp:DropDownList ID="ddlEndMin" runat="server" 
                                CssClass="GridPagerButtons">
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>16</asp:ListItem>
                            <asp:ListItem>17</asp:ListItem>
                            <asp:ListItem>18</asp:ListItem>
                            <asp:ListItem>19</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>21</asp:ListItem>
                            <asp:ListItem>22</asp:ListItem>
                            <asp:ListItem>23</asp:ListItem>
                            <asp:ListItem>24</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>26</asp:ListItem>
                            <asp:ListItem>27</asp:ListItem>
                            <asp:ListItem>28</asp:ListItem>
                            <asp:ListItem>29</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>31</asp:ListItem>
                            <asp:ListItem>32</asp:ListItem>
                            <asp:ListItem>33</asp:ListItem>
                            <asp:ListItem>34</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>36</asp:ListItem>
                            <asp:ListItem>37</asp:ListItem>
                            <asp:ListItem>38</asp:ListItem>
                            <asp:ListItem>39</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>41</asp:ListItem>
                            <asp:ListItem>42</asp:ListItem>
                            <asp:ListItem>43</asp:ListItem>
                            <asp:ListItem>44</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>46</asp:ListItem>
                            <asp:ListItem>47</asp:ListItem>
                            <asp:ListItem>48</asp:ListItem>
                            <asp:ListItem>49</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>51</asp:ListItem>
                            <asp:ListItem>52</asp:ListItem>
                            <asp:ListItem>53</asp:ListItem>
                            <asp:ListItem>54</asp:ListItem>
                            <asp:ListItem>55</asp:ListItem>
                            <asp:ListItem>56</asp:ListItem>
                            <asp:ListItem>57</asp:ListItem>
                            <asp:ListItem>58</asp:ListItem>
                            <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>&nbsp 
                            <asp:DropDownList ID="ddlEndAMPM" Width="40px" runat="server" CssClass="GridPagerButtons">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                            </asp:DropDownList>
                            </td>                
                    </tr>
                </table>
            </td>
        </tr>
        
        </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
        <td>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width:100%;">
                    <tr>
                        <td align="left" >
                            <span class="PageSubHeader" lang="en-ph">Question Details</span>
                            &nbsp;
                            <asp:Label ID="lblQuestion" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<span class="FieldTitle" lang="en-ph"><asp:CheckBox ID="rbQuestion" runat="server" Text="Random Question" />&nbsp;&nbsp;<asp:CheckBox ID="rbAnswer" runat="server" Text="Random Answer" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" >
                            <span class="FieldTitle" lang="en-ph">
                                <asp:Label ID="Label2" runat="server" Text="Quarter:" Visible="false"></asp:Label></span>&nbsp
                            
                            <span class="FieldTitle" lang="en-ph">
                                <asp:Label ID="Label5" runat="server" Text="Topic:" Visible="true"></asp:Label></span>
                                &nbsp;
                            <asp:DropDownList ID="ddlTopic" runat="server" CssClass="GridPagerButtons" 
                                AutoPostBack="True" onselectedindexchanged="ddlTopic_SelectedIndexChanged" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td ><span class="PageSubHeader" lang="en-ph">Available Question(s):</span></td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="FieldTitle" 
                                Text="Select All" Width="100px" 
                                oncheckedchanged="chkSelectAll_CheckedChanged" AutoPostBack="True" />&nbsp; &nbsp;<asp:CheckBox 
                                ID="chkDeselectAll" runat="server" CssClass="FieldTitle" 
                                Text="Deselect All" Width="100px" AutoPostBack="True" 
                                oncheckedchanged="chkDeselectAll_CheckedChanged" />
                                <br /><br />
                            <asp:GridView ID="dgQuestionBank" runat="server" AllowPaging="True" EmptyDataRowStyle-ForeColor="ActiveBorder" 
                                AutoGenerateColumns="False" Width="700px" PageSize="50" 
                                 style="margin-left: 0px" onrowdatabound="dgQuestionBank_RowDataBound">
                                <PagerSettings Position="TopAndBottom" />
                                <EmptyDataRowStyle ForeColor="ActiveBorder" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkQuestion" runat="server" 
                                                oncheckedchanged="chkQuestion_CheckedChanged" />
                                            <asp:Label ID="lblQuestionID" Visible="false" runat="server" Text='<%# Eval("QuestionID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Questions">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestions" runat="server" Style="display:inline;" CssClass="LabelClass" Text='<%# Eval("Question") %>' Width="100%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="90%" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Left" VerticalAlign="Middle"/>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <div>
                                            <asp:LinkButton ID="lnkFirst_DB" runat="server" CssClass="GridPagerButtons" 
                                                ToolTip="Go to First Page" onclick="lnkFirst_DB_Click">First</asp:LinkButton>
                                            <span lang="en-ph">&nbsp;|&nbsp;<asp:LinkButton ID="lnkPrev_DB" runat="server" 
                                                CssClass="GridPagerButtons" onclick="lnkPrev_DB_Click">Previous</asp:LinkButton>
                                            &nbsp;| </span>
                                            <span class="LoginSubHeader" lang="en-ph"><span lang="en-ph">
                                            <asp:Label ID="lblPage_DB" runat="server" CssClass="GridPagerButtons" Text="Page"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="cboPageNumber_DB" runat="server" AutoPostBack="True" 
                                                CssClass="GridPagerButtons" ToolTip="Jump to Page" 
                                                onselectedindexchanged="cboPageNumber_DB_SelectedIndexChanged" Width="40px">
                                            </asp:DropDownList>
                                            &nbsp;<asp:Label ID="lblPageCount_DB" runat="server" CssClass="GridPagerButtons" 
                                                Text="of "></asp:Label>
                                            |
                                            <asp:LinkButton ID="lnkNext_DB" runat="server" CssClass="GridPagerButtons" 
                                                onclick="lnkNext_DB_Click" >Next</asp:LinkButton>
                                            &nbsp;|
                                            <asp:LinkButton ID="lnkLast_DB" runat="server" CssClass="GridPagerButtons" 
                                                ToolTip="Go to Last Page" onclick="lnkLast_DB_Click">Last</asp:LinkButton>
                                            </span></span>
                                        </div>
                                </PagerTemplate>
                                <PagerStyle BorderWidth="0px" HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                   </tr>
                        <tr>
                        <td  align="center">
                            <asp:LinkButton ID="lnkSave0" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="87px" 
                        onclick="lnkSave0_Click">Add</asp:LinkButton>
                           <br />
                        </td>
                        </tr>
                        <tr>
                            <td><span class="PageSubHeader" lang="en-ph">Selected Question(s):</span></td>
                        </tr>
                        
                        <tr>
                        <td align="justify" valign="top" >
                            <asp:GridView ID="dgQuestions" runat="server" AllowPaging="True" EmptyDataRowStyle-ForeColor="ActiveBorder" 
                                AutoGenerateColumns="False"  OnRowEditing="dgQuestions_RowEditing"
                                OnRowUpdating="dgQuestions_RowUpdating" 
                                OnRowCancelingEdit="dgQuestions_RowcCancelingEdit" 
                                OnRowDeleting="dgQuestions_RowDeleting" Width="700px" PageSize="100" 
                                onrowdatabound="dgQuestions_RowDataBound" style="margin-left: 0px">
                                <PagerSettings Position="TopAndBottom" />
                                <EmptyDataRowStyle ForeColor="ActiveBorder" />
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
                                                    ToolTip="Edit Points" />
                                                <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" 
                                                    CommandName="Delete" ImageUrl="~/images/icons/page_delete.gif" Text="Delete" 
                                                    ToolTip="Delete This Question" 
                                                    onclientclick="return confirm('Are you sure you want to Delete this Question?')" />
                                            
                                            <asp:Label ID="lblQuestionID" Visible="false" runat="server" Text='<%# Eval("QuestionID") %>'></asp:Label> 
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Questions">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestions" runat="server" Style="display:inline" Text='<%# Eval("Questions") %>' Width="100%"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="80%" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Points">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPoints" runat="server" Width="80%"
                                                Text='<%# Eval("Points") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoints" runat="server" Text='<%# Eval("Points") %>' Width="100%" ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
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
                                                ToolTip="Jump to Page" Width="40px">
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
                            </tr>
                        </td>
                    </tr>
                </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td>
            <span lang="en-ph" class="ValidationNotice">Fields marked with an (*) are required.</span></td></tr>
            <tr><td>&nbsp;</td></tr>
        <tr>
            <td align="right"  colspan="3">
                
                <asp:LinkButton ID="lnkSave" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="88px" onclick="lnkSave_Click">Save</asp:LinkButton>
&nbsp;
                                            <asp:LinkButton ID="lnkCancel" runat="server" CssClass="ButtonTemplate" 
                                                Height="28px" Width="88px"
                                                onclientclick="return confirm('Cancel Changes?')" 
                                                onclick="lnkCancel_Click">Cancel</asp:LinkButton>
                &nbsp;</td>
        </tr>
        <tr>
        <td>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="width:100%;">
                    <tr>
                        <td align="left"><span class="PageSubHeader" lang="en-ph"></span>
                            <asp:Label ID="lblFeedback" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="dgFeedback" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                Width="700px" PageSize="50" 
                                onrowcancelingedit="dgFeedback_RowCancelingEdit" 
                                onrowdatabound="dgFeedback_RowDataBound" onrowdeleting="dgFeedback_RowDeleting" 
                                onrowediting="dgFeedback_RowEditing" 
                                onrowupdating="dgFeedback_RowUpdating" 
                                onrowcommand="dgFeedback_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" 
                                                    CommandName="Edit" ImageUrl="~/images/icons/page_edit.gif" Text="Edit" 
                                                    ToolTip="Edit Feedback" />
                                            <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" 
                                                    CommandName="Delete" ImageUrl="~/images/icons/page_delete.gif" Text="Delete" 
                                                    ToolTip="Delete This Feedback" 
                                                    onclientclick="return confirm('Are you sure you want to Delete this Feedback?')" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgUpdate" runat="server" CausesValidation="True" 
                                                    CommandName="Update" ImageUrl="~/images/icons/page_tick.gif" Text="Update" 
                                                    ToolTip="Update Changes" />
                                                &nbsp;<asp:ImageButton ID="imgCancel" runat="server" CausesValidation="False" 
                                                    CommandName="Cancel" ImageUrl="~/images/icons/action_stop.gif" Text="Cancel" 
                                                    ToolTip="Cancel Changes" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton text="Add" ID="imgAdd" runat="server" ImageUrl="~/images/icons/page_new.gif" CommandName="Add" />
                                        </FooterTemplate>
                                        <FooterStyle BorderStyle="None" />
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" Width="30px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grade Boundary">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGradeBoundary" runat="server" Text='<%# Eval("GradeBoundary") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGradeBoundary" runat="server" Text='<%# Eval("GradeBoundary") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                         <FooterStyle BorderStyle="None" />
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Feedback">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFeedback" runat="server" Text='<%# Eval("Feedback") %>' TextMode="MultiLine" Width="350px"></asp:TextBox> 
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Feedback") %>' Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#3F5330" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="350px" 
                                                Wrap="False" />
                                            <ItemStyle BackColor="White" BorderColor="Black" BorderStyle="Solid" 
                                                ForeColor="#3F5330" HorizontalAlign="Center" VerticalAlign="Middle" />
                                             <FooterStyle BorderStyle="None" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        </tr>
        
        </table>
    </div>
    </div>
    <uc2:frmFooter ID="frmFooter1" runat="server" />
    </form>
</body>
</html>
