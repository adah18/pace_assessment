using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment.student
{
    public partial class assessment_success : System.Web.UI.Page
    {
        //instantiate new GlobalForms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Declare LoginUser Class
        LoginUser LUser;
        //Declare CurrentStudent class
        CurrentStudent CStudent;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                CStudent = (CurrentStudent)Session["CurrentStudent"];

                if (LUser.UserGroupID == "S")
                {

                }
                else
                {
                    Response.Write("<script>alert('Access Denied!'); window.location='"+ResolveUrl(DefaultForms.frm_index)+"';</script>");
                }
            }
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }
            LoadSitemapDetails();
        }

        //Load Sitemap Details for navigation
        public void LoadSitemapDetails()
        {
            SiteMap1.RootNode = "Dashboard";
            SiteMap1.RootNodeToolTip = "Click to go back to Dashboard";
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_default_dashboard);

            SiteMap1.ParentNode = "Academic Activities";

            SiteMap1.CurrentNode = "Take Assessment";
        }

        //Home LinkButton has been clicked
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_index));
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_history_assessment_view)+"?aid="+Request.QueryString["assid"]);
        }
    }
}
