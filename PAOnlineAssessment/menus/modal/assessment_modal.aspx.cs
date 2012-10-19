using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment.menus.modal
{
    public partial class assessment_modal : System.Web.UI.Page
    {
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        SystemProcedures sys = new SystemProcedures();
        LoginUser LUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User Is Logged In
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if (LUser.UserGroupID == "1" || LUser.UserGroupID == "3")
                {
                    if (LUser.UserGroupID == "1")
                    {
                        imgTeacher.Visible = false;
                        imgAdmin.Visible = true;
                    }
                    else
                    {
                        imgTeacher.Visible = true;
                        imgAdmin.Visible = false;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Access Denied!'); window.location='" + ResolveUrl(DefaultForms.frm_index) + "';</script>");
                }
            }
            //Redirect to Login Page when no User is logged in
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

        }
    }
}
