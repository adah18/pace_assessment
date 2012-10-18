using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment.parent
{
    public partial class parent_dashboard1 : System.Web.UI.Page
    {
        //Instantiate New GlobalForms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Declare Login Class
        LoginUser LUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Get Logged in User Details from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                //check if the user was login and if the user is a parent
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "P")
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }
                //Add First name in the Header
                lblLoggedFirstname.Text = LUser.Firstname;
            }
            catch
            {
                //Redirect to the Login Page
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_parent_view_grades));
        }

        protected void ImgAssessmentHistory_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_parent_select_child));
        }

        protected void imgChangePassword_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_parent_change_password));
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_parent_edit_profile));
        }
    }
}
