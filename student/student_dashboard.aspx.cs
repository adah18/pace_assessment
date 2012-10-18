using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.student
{
    public partial class student_dashboard : System.Web.UI.Page
    {
        //Instantiate New GlobalForms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Declare Login Class
        LoginUser LUser;
        //Declare CurrentStudent Class
        CurrentStudent CStudent;

        //page load
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In
            try
            {
                //Get Logged in User Details from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];

                //check if the user is login and a student
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "S")
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                //Add First name in the Header
                lblLoggedFirstname.Text = LUser.Firstname;
                //Get Student Details from the Session Variable CurrentStudent
                CStudent = (CurrentStudent)Session["CurrentStudent"];
                string DebugLine = "LEVEL ID: " + CStudent.LevelID + "\r\nSECTION ID: " + CStudent.SectionID;
                Debug.WriteLine(DebugLine);

            }
            //If no User is logged in, redirect to login page
            catch
            {
                //Redirect to the Login Page
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }

        }

        //executed when the change password button has been clicked
        protected void imgChangePassword_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_change_password));
        }

        //executed when the take assessment button has been clicked
        protected void imgTakeAssessment_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_available_assessments));
        }

        //executed when the review assessment button has been clicked
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_review_assessment_list));
        }

        protected void ImgAssessmentHistory_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_history_assessment_main));
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_student_gradesview));
        }
    }
}
