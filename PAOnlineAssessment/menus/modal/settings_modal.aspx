<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="settings_modal.aspx.cs" Inherits="PAOnlineAssessment.menus.modal.settings_modal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Genious Web</title>    
    <link href="../../scripts/styles/Font%20Style.css" rel="stylesheet" />
     <script type="text/javascript">
            function logout() {
                if (confirm('Do you really want to logout?')) {
                    window.location = '../../../logout.aspx';
                }
            }
        </script>
</head>

<body>
    <form id="form1" runat="server">

     <table style="width:100%;">
          <tr>
              <td colspan="4">
                  <span lang="en-ph" class="PageHeader">Parent Activities</span></td>
          </tr>
          <tr>
              <td colspan="4">
                  <span lang="en-ph" class="PageHeader">&nbsp;</span></td>
          </tr>
          <tr>
              <td colspan="4">
                  <span lang="en-ph" class="PageHeader">&nbsp;</span></td>
          </tr>
          <tr>
              <td >
                  &nbsp;</td>
          </tr>
          
          <tr>
              <td><center>
                  <span class="GridPagerButtons" lang="en-ph"><br />
                  </span></center>
              </td>
              <td><center>
                  <asp:ImageButton ID="ImageButton3" runat="server" CssClass="LargeButtonTemplate" 
                      Height="128px" ImageAlign="Middle" ImageUrl="~/images/dashboard_icons/process.png" 
                      onclientclick="window.location='../../Parent/change_password.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">Change Password</span></center>
              </td>
              <td><center>
                  
                  <asp:ImageButton ID="ImageButton2" runat="server" 
                      CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                      ImageUrl="~/images/dashboard_icons/users_settings.png" 
                      onclientclick="window.location='../../Parent/edit_profile.aspx'; return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">Edit Profile</span></center>
              </td>
              <td><center>
                  <asp:ImageButton ID="ImageButton1" runat="server" 
                      CssClass="LargeButtonTemplate" Height="128px" ImageAlign="Middle" 
                      ImageUrl="~/images/dashboard_icons/full_page.png" 
                      onclientclick="logout(); return false;" 
                      Width="128px" />
                  <br />
                  <span class="GridPagerButtons" lang="en-ph">Logout</span></center>
              </td>
              <td><center>
                  <span class="GridPagerButtons" lang="en-ph"><br />&nbsp;
                  </span></center>
              </td>
              
          </tr>
          </table>
      
    </form>
</body>
</html>
