using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment
{
    public partial class default_dashboard : System.Web.UI.Page
    {
        //Declare LoginUser Variable
        LoginUser LUser;
        //Instantiate New Global Form Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User Is Logged In
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];               
            }

            //Redirect to Login Page when no User is logged in
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }

            //Fill LUser Class Variable with class stored in the Session Variable
            LUser = (LoginUser)Session["LoggedUser"];
            
            //Check if Logged In User is an Administrator
            if ((string)Session["UserGroupID"] == "1")
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_admin_dashboard));
            }
            //Check if Logged In User is a Teacher
            else if ((string)Session["UserGroupID"] == "3")
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_instructor_dashboard));
            }
            //Check if Logged In User is a Student
            else if ((string)Session["UserGroupID"] == "S")
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_student_dashboard));
            }
            else if ((string)Session["UserGroupID"] == "P")
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_parent_dashboard));
            }
            //Other Users
            else
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }

        }
    }
}
