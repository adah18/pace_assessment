using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using PAOnlineAssessment.Classes;
using System.Data;


namespace PAOnlineAssessment
{
    public partial class instructor_dashboard : System.Web.UI.Page
    {
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        LoginUser LUser;

        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                lblLoggedFirstname.Text = LUser.Firstname;                
                if ((string)Session["UserGroupID"] == "3")
                {

                }
                else
                {
                    Response.Write("<script>alert('Access Denied!'); window.location='../index.aspx';</script>");
                }
            }
            catch
            {
                Response.Redirect("../login.aspx");
            }

            if (IsPostBack == false) 
            {
               
            }
        }

        protected void imgCreateAssessment_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_quizcreation_addupdate));
        }

        protected void imgMySubjects_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_instructor_subjects));
        }

        protected void imgViewAllAssessments_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_quizcreation_main));
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_advisers_studentsview));
        }

        protected void imgMakeUp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_makeup_exam_list));
        }


    }
}
