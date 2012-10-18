using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using PAOnlineAssessment.Classes;
using System.Globalization;
using System.Data.OleDb;
using System.Data;


namespace PAOnlineAssessment.parent
{
    public partial class parent_dashboard : System.Web.UI.Page
    {
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In
            try
            {
                //Get Login User Info from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                string Trigger = LUser.Username;
                //Label lblLoggedFirstname = (Label)parent_header1.FindControl("lblLoggedFirstname");
                //lblLoggedFirstname.Text = LUser.Firstname;
            }
            //Redirect to the Index Page
            catch
            {
                //Redirect to the Index page
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void imgUserMaintenance_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("parent_select_child.aspx");
        }

        protected void imgAssessment_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("parent_view_grades.aspx");
        }
    }
}
