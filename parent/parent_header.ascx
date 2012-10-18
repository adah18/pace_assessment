<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="parent_header.ascx.cs" Inherits="PAOnlineAssessment.Parent.parent_header" %>
<html>
    <head>
        <script type="text/javascript">
            function logout() {
                if (confirm('Do you really want to logout?')) {
                    window.location = '../../logout.aspx';
                }
            }
        </script>
    </head>
    <body>

      <div class="header">
        <div class="header_resize">
          <div class="logo">
            <h1><a href=""><small></small>
              <img src="../images/logo.png" alt="" /></a></h1>
          </div>
          <div class="logo_text"><a href="http://www.paceacademy.edu.ph">Pace Academy Website</a></div>
          <div class="clr"></div>
        </div>
        <div class="headert_text_resize">

          <div class="menu">
           <%-- <ul>
              <li><a href="parent_dashboard.aspx">Home</a></li>
              <li><a href="parent_select_child.aspx">Select Child</a></li>
              <li><a href="parent_view_grades.aspx">Student Grades</a></li>
              <li><a href="parent_view_grades.aspx">Change Password</a></li>
              <li><a href="parent_view_grades.aspx">Edit Profile</a></li>
              <li><a href="" onclick="logout(); return false;">Logout</a></li>
            </ul>--%>
                <span class="preload1"></span>
                <span class="preload2"></span> 
            <ul id="nav">
	<li class="top"><a href="parent_dashboard.aspx" class="top_link"><span>Home</span></a></li>
	<%--<li class="top"><a href="#nogo2" id="products" class="top_link"><span class="down">Products</span></a>
	</li>--%>
	<li class="top"><a href="#nogo22" id="services" class="top_link"><span class="down">Activities</span></a>
		<ul class="sub">
			<li><a href="parent_select_child.aspx">Select Child</a></li>
			<li><a href="parent_view_grades.aspx">View Child Grades</a></li>
		</ul>
	</li>
	<li class="top"><a href="#nogo27" id="contacts" class="top_link"><span class="down">User Settings</span></a>
	<ul class="sub">
			<li><a href="edit_profile.aspx">Edit Profile</a></li>
			<li><a href="change_password.aspx">Change Password</a></li>
			<li><a href="" onclick="logout(); return false;">Logout</a></li>
		</ul>
	</li>

</ul>
          </div>
          <div class="clr"></div>
          <h2 class="bigtext">Welcome 
             <span> <asp:Label ID="lblLoggedFirstname" runat="server" Text=""></asp:Label></span><br />
          
            <br />
            </h2>
          <div class="headert_text">
            <p>&nbsp;</p>
          </div>
          <div class="clr"></div>
        </div>
      </div>        
    
    </body>
</html>