<%@ control Language="C#" AutoEventWireup="True" CodeBehind="frmHeader.ascx.cs" Inherits="PAOnlineAssessment.frmHeader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Pace Academy Online Assessment System</title>
    <link href="../scripts/styles/style.css" rel="stylesheet" type="text/css" /> 
   <link rel="stylesheet" href="../scripts/styles/tinydropdown.css" type="text/css" />
<script type="text/javascript" src="../scripts/jquery/tinydropdown.js"></script>

<script type="text/javascript">
    
    function VerifyLogout() {
        if (confirm('Are you sure you want to log out?')) {
            window.location = "../../logout.aspx";
        }
        else {
            window.location = "#";            
        }
    }

</script>

</head>

<body>

<div id="topPan"  style="overflow: visible;">
	<a href="../index.aspx"><img src="../images/logo.png" alt="Genious Web" width="100" 
        height="200" border="0" title="Pace Academy Online Assessment System" /></a>		
	<div class="nav">
               <ul id="menu" class="menu">
                   <%                  
                       
                    try
                    {
                        if ((bool)Session["Authenticated"] == true )
                        {
                          // Response.WriteFile(ResolveUrl("~/menus/subheader/subheader_admin.txt"));
                            if (Session["UserGroupID"] == "S")
                            {
                                Response.WriteFile(ResolveUrl("~/menus/subheader/subheader_student.txt"));
                            }
                            else if (Session["UserGroupID"] == "P")
                            {
                                Response.WriteFile(ResolveUrl("~/menus/subheader/subheader_parent.txt"));
                            }
                            else
                            {
                            %>
                                <li><a href="#" style="width: 1px;">&nbsp;</a></li>
                                <li><a href="../../index.aspx" style="width: 100px;">Home</a></li>
                                <li><a href="#" style="width: 175px;">Maintenance Tools</a>
	                                <ul>
	                                    <% if (Session["AccessRights"].ToString().Contains("-0-"))
                                        {%>
		                                <li><a href="../../maintenance/assessment_display_setting.aspx" style="width: 175px;">Display Maintenance</a></li>
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-1-"))
                                     {%>
		                                <li><a href="../../maintenance/parent_maintenance_main.aspx" style="width: 175px;">Pending Parent Accounts</a></li>           
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-17-"))
                                     {%>
		                                <li><a href="../../maintenance/parent_pending_students.aspx" style="width: 175px;">Pending Parent-Child Requests</a></li>
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-4-"))
                                     {%>
		                                <li><a href="../../maintenance/pending_student_accounts.aspx" style="width: 175px;">Pending Student Accounts</a></li>
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-2-"))
                                     {%>
		                                <li><a href="../../maintenance/quarter_maintenance.aspx" style="width: 175px;">Quarter Maintenance</a></li>
		                                <% } %>
        		                        
        		                        <% if (Session["AccessRights"].ToString().Contains("-19-"))
                                     {%>
		                                <li><a href="../../maintenance/school_year_maintenance.aspx" style="width: 175px;">School Year Maintenance</a></li>
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-3-"))
                                     {%>
		                                <li><a href="../../maintenance/student_maintenance_main.aspx" style="width: 200px;">Student Maintenance</a></li>
        		                        <% } %>
        		                        
        		                        <% if (Session["AccessRights"].ToString().Contains("-5-"))
                                     {%>
		                                <li><a href="../../maintenance/user_maintenance_main.aspx" style="width: 175px;">User Maintenance</a></li>
		                                <% } %>
        		                        
		                                <% if (Session["AccessRights"].ToString().Contains("-6-"))
                                     {%>
		                                <li><a href="../../maintenance/usergroup_maintenance_main.aspx" style="width: 175px;">User Group Maintenance</a></li>
		                                <% } %>
		                                
	                                </ul>
                                </li>
                                  		        
                                <li><a href="#"  style="width: 200px;">Assessment Tools</a>
	                                <ul>
	                                    <% if (Session["AccessRights"].ToString().Contains("-7-"))
                                        {%>
		                                <li><a href="../../assessment/quizcreation_main.aspx" style="width: 200px;">Assessment Maintenance</a></li>
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-8-"))
                                     {
                                         if (Session["UserGroupID"].ToString() == "1")
                                         {%>
		                                <li><a href="../../assessment/assessment_admin_add.aspx" style="width: 200px;">Create Assessment</a></li>
								        <% }
                                         else
                                         {%>
								        <li><a href="../../assessment/quizcreation_addupdate.aspx" style="width: 200px;">Create Assessment</a></li>
		                                <% }
                                     }%>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-9-"))
                                     {%>
		                                <li><a href="../../assessment/questionpool_maintenance_manual.aspx" style="width: 200px;">Create Questions Manually</a></li>
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-10-"))
                                     {%>
		                                <li><a href="../../assessment/questionpool_maintenance_main.aspx" style="width: 200px;">Question Pool Maintenance</a></li> 
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-11-"))
                                     {%>
		                                <li><a href="../../maintenance/topic_maintenance_main.aspx" style="width: 200px;">Topic Maintenance</a></li>
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-12-"))
                                     {%>
		                                <li><a href="../../assessment/assessmenttype_maintenance_main.aspx" style="width: 200px;">Type Maintenance</a></li>   
		                                <% } %>
		                                
		                                <% if (Session["AccessRights"].ToString().Contains("-13-"))
                                     {%>
		                                <li><a href="../../assessment/questionpool_maintenance_upload.aspx" style="width: 200px;">Upload Questions from Excel File</a></li> 
		                                <% } %>          
	                                </ul>
                                </li>
                                
                                <li><a href="#"  style="width: 150px;">Academic Activities</a>
	                            <ul>
	                                <% if (Session["AccessRights"].ToString().Contains("-18-"))
                                    {%>
	                                <li><a href="../../instructor/assessment_evaluation.aspx" style="width: 150px;">Assessment Evaluation</a></li>
	                                <% } %>
	                                
	                                <% if (Session["AccessRights"].ToString().Contains("-16-"))
                                    {%>
		                            <li><a href="../../instructor/makeup_exam_list.aspx" style="width: 150px;">Make-Up Exams</a></li>
		                            <% } %>
		                            
		                            <% if (Session["AccessRights"].ToString().Contains("-15-"))
                                 {%>
		                            <li><a href="../../instructor/advisers_studentsview.aspx"style="width: 150px;">Student Grades</a></li>
		                            <% } %>
		                            
	                                <% if (Session["AccessRights"].ToString().Contains("-14-"))
                                    {%>
		                            <li><a href="../../instructor/instructor_subjects.aspx" style="width: 150px;">Teacher's Subjects</a></li>
		                            <% } %>
		                            
		                            
	                            </ul>
                            </li>
                            <li><a href="javascript:VerifyLogout();" style="width: 80px;">Logout</a></li>   
                            <%

                       }
                        }
                        
                        //else if ((bool)Session["Authenticated"] == true && ((string)Session["UserGroupID"] == "2"))
                        //{
                        //      Response.WriteFile(ResolveUrl("~/menus/subheader/subheader_teacher.txt"));
                        //}
                        //else if ((bool)Session["Authenticated"] == true && ((string)Session["UserGroupID"] == "S"))
                        //{
                        //    Response.WriteFile(ResolveUrl("~/menus/subheader/subheader_student.txt"));
                        //}
                        //else
                        //{
                        //    Response.WriteFile(ResolveUrl("~/menus/subheader/subheader_guest.txt"));
                        //}
                    }
                    catch
                    {
                        %>
                        <li><a href="../../login.aspx" style="width: 100px">Login</a></li>
                        <li id="separator">&nbsp;</li>
                        <li><a style="width: 100px;">Sign-up</a>
                        <ul>
                        <li><a href="../../registration/signup.aspx" style="width: 100px;">Student</a></li>
                        <% if (Session["Registration"].ToString().Contains("On"))
                           {    %>
                        <li><a href="../../registration/signup_parent.aspx" style="width: 100px;">Parent</a></li>
                        <% } %>
                        </ul>
                        </li>
                        <%
                        //Response.WriteFile(ResolveUrl("~/menus/subheader/subheader_guest.txt"));
                    }  
                       
                       %>                         
                </ul>    
 	 </div>
</div> 

<script type="text/javascript">
    var dropdown = new TINY.dropdown.init("dropdown", { id: 'menu', active: 'menuhover' });
</script>



</body>
</html>
