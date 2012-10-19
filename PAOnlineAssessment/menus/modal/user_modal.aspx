<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_modal.aspx.cs" Inherits="PAOnlineAssessment.menus.modal.user_modal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Genious Web</title>    
    <link href="../../scripts/styles/Font%20Style.css" rel="stylesheet" 
        type="text/css" />

</head>

<body>
    <form id="form1" runat="server">

     <table style="width:100%;">
          <tr>
              <td colspan="3">
                  <span lang="en-ph" class="PageHeader">Maintenance Tools</span></td>
          </tr>
          <tr>
              <td >
                  &nbsp;</td>
          </tr>
          
          <tr>
              <td><center>
                  <asp:ImageButton ID="ImageButton1" runat="server" 
                      CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                      ImageUrl="~/images/dashboard_icons/quizzes.png" 
                      onclientclick="window.location='../../maintenance/assessment_display_setting.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">Display<br />
                  Maintenance</span></center>
              </td>
              <td><center>
                  <asp:ImageButton ID="ImageButton2" runat="server" 
                      CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                      ImageUrl="~/images/dashboard_icons/Todo.png" 
                      onclientclick="window.location='../../maintenance/parent_maintenance_main.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">Pending
                  Parent<br />
                                    Accounts Maintenance</span></center>
              </td>
              <td><center>
                  <asp:ImageButton ID="ImageButton3" runat="server" CssClass="LargeButtonTemplate" 
                      Height="128px" ImageAlign="Middle" ImageUrl="~/images/dashboard_icons/Todo.png" 
                      onclientclick="window.location='../../maintenance/pending_student_accounts.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">Pending Student<br />
                  Accounts Maintenance</span></center>
              </td>
              
              <td><center>
                  <asp:ImageButton ID="imgSystemUsers" runat="server" 
                      CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                      ImageUrl="~/images/dashboard_icons/calendar.png" 
                      onclientclick="window.location='../../maintenance/quarter_maintenance.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">Quarter Maintenance<br />&nbsp;
                  </span></center>
              </td>
              
          </tr>
          
          <tr>
              <td >
                  &nbsp;</td>
          </tr>
          
          <tr>
              <td><center>
                  <asp:ImageButton ID="ImageButton5" runat="server" CssClass="LargeButtonTemplate" 
                      Height="128px" ImageAlign="Middle" ImageUrl="~/images/dashboard_icons/clock.png" 
                      onclientclick="window.location='../../maintenance/school_year_maintenance.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">School Year <br /> Maintenance
                  </span></center>
              </td>
              
              <td><center>
                  <asp:ImageButton ID="imgStudentAccounts" runat="server" 
                      CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                      ImageUrl="~/images/dashboard_icons/users.png" 
                      onclientclick="window.location='../../maintenance/student_maintenance_main.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">Students Account<br />
                  Maintenance</span></center>
              </td>
              
              <td><center>
                  <asp:ImageButton ID="ImageButton4" runat="server" CssClass="LargeButtonTemplate" 
                      Height="128px" ImageAlign="Middle" ImageUrl="~/images/dashboard_icons/users_settings.png" 
                      onclientclick="window.location='../../maintenance/usergroup_maintenance_main.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">User Group 
                  <br />
                  Maintenance</span></center>
              </td>
              
              <td><center>
                  <asp:ImageButton ID="imgPending" runat="server" CssClass="LargeButtonTemplate" 
                      Height="128px" ImageAlign="Middle" ImageUrl="~/images/dashboard_icons/user.png" 
                      onclientclick="window.location='../../maintenance/user_maintenance_main.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">User Maintenance<br />&nbsp;
                  </span></center>
              </td>
              
              
          </tr>
          
          <tr>
              <td >
                  &nbsp;</td>
              <td >
                  &nbsp;</td>
              <td >
                  &nbsp;</td>
          </tr>
          
          </table>
      
    </form>
</body>
</html>
