using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using PAOnlineAssessment.Classes;
using System.Data;


namespace PAOnlineAssessment.system
{
    public partial class admin_dashboard : System.Web.UI.Page
    {
        //Instantiate New User
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();

        //Load Event
        protected void Page_Load(object sender, EventArgs e)
        {            
            //Try to get Logged in user info in the session variable
            try
            {
                //Fill declared variable with info in the session variable
                LUser = (LoginUser)Session["LoggedUser"];
                //get first name
                lblLoggedFirstname.Text = LUser.Firstname;                
                //check if usergroup is administrator
                if ((string)Session["UserGroupID"] == "1")
                {

                }
                    //if not administrator
                else
                {
                    Response.Write("<script>alert('Access Denied!'); window.location='"+ResolveUrl(DefaultForms.frm_index)+"';</script>");
                }
            }
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }

            if (IsPostBack == false) 
            {
               
            }
            
        }

        //View All Assessments
        protected void imgViewCreatedAssessments_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_quizcreation_main));
        }

        //Create new Assessment
        protected void imgCreateAssessment_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_assessment_admin_add));
        }
    }
}
