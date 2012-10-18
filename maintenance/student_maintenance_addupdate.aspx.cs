using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.maintenance
{
    public partial class student_maintenance_addupdate : System.Web.UI.Page
    {
        ////////////////////////
        //--------------------//
        //--- Declarations ---//
        //--------------------//
        ////////////////////////

        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Declare LoginUser Class
        LoginUser LUser;
        //Instantiate New Collections Class
        Collections cls = new Collections();
        //Instantiate New List of Student Accounts
        List<Constructors.StudentAccount> StudentAccountList;
        List<Constructors.StudentRegistrationView> RegistrationViewList;
        
        /////////////////////////
        //---------------------//
        //--- System Events ---//
        //---------------------//
        /////////////////////////

        //Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            //Try to get Logged in User Details in the Session Variable
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }
            }
            //Redirect to the Login Screen if no User is Logged in
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }
            //Load Details of the Site Map Control
            LoadSiteMapDetails();

            //Check if URL Parameter contains the Student ID
            if (Convert.ToInt32(Request.QueryString["sid"]) > 0 && Request.QueryString["action"] == "edit")
            {
                //Check if Load Type is Postback
                if (IsPostBack == false)
                {
                    RegistrationViewList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
                    //Load Student Account Details
                    LoadStudentDetails();
                }
            }

            //If Parameter is not in the URL
            else
            {
                //Redirect to the Student Maintenance Main Page
                Response.Redirect(ResolveUrl(DefaultForms.frm_student_maintenance_main));
            }
        }

        //Load Details of the Site Map Control
        public void LoadSiteMapDetails()
        {
            SiteMap.RootNode = "Dashboard";
            SiteMap.RootNodeToolTip = "Click to go back to Dashboard";
            SiteMap.RootNodeURL = ResolveUrl(DefaultForms.frm_default_dashboard);

            SiteMap.ParentNode = "Student Maintenance";
            SiteMap.ParentNodeToolTip = "Click to go back to Student Maintenance";
            SiteMap.ParentNodeURL = ResolveUrl(DefaultForms.frm_student_maintenance_main);

            SiteMap.CurrentNode = lblMode.Text;
        }

        //Submit Button
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //error handler
            bool haserror;
            string qry = "";
            //Validate Fields for NULL Characters
            if (ValidateFields())
            {
                //set the defaults
                haserror = false;
                string StudentID = "";

                //loop through the list
                RegistrationViewList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
                RegistrationViewList.ForEach(r =>
                    {
                        //get the student id
                        if (r.SchoolYear == Session["CurrentSchoolYear"].ToString() && r.StudentNumber == txtStudentNumber.Text)
                        {
                            StudentID = r.StudentID.ToString();
                            return;
                        }
                    });

                //check if student number exist
                if (!string.IsNullOrEmpty(StudentID))
                {
                    StudentID = cls.ExecuteScalar("SELECT StudentID FROM PaceAssessment.dbo.Student WHERE StudentNumber='" + txtStudentNumber.Text + "' and StudentID!='" + Request.QueryString["sid"] + "'");
                    //check the student number
                    if (!string.IsNullOrEmpty(StudentID))
                    {
                        vlStudentNumber.Text = "* Student Number already exist.";
                        haserror = true;
                    }

                    //validation for email, check if the email already exist in the assessment
                    //if (!string.IsNullOrEmpty(txtEmailAddress.Text))
                    //{
                    //    StudentID = cls.ExecuteScalar("SELECT StudentID FROM PaceAssessment.dbo.Student WHERE EmailAddress='" + txtEmailAddress.Text + "' and StudentID!='" + Request.QueryString["sid"] + "'");
                    //    //check the email address
                    //    if (!string.IsNullOrEmpty(StudentID))
                    //    {
                    //        vlEmailAddress.Text = "* Email Address already exist.";
                    //        haserror = true;
                    //    }
                    //}
                }
                else
                {
                    haserror = true;
                    vlStudentNumber.Text = "* Student Number doesn't exist in the Pace Registration.";
                }

	            //check if has error
                if (haserror == false)
                {
                    if (Request.QueryString["action"] == "edit")
                    {
                        //loop through the list
                        RegistrationViewList.ForEach(r =>
                            {
                                //check if has the same registration number
                                if (r.SchoolYear == Session["CurrentSchoolYear"].ToString() && txtStudentNumber.Text == r.StudentNumber)
                                {
                                    qry = "UPDATE [PaceAssessment].dbo.[Student] SET StudentNumber='" + Validator.Finalize(txtStudentNumber.Text) + "', FirstName='" + Validator.Finalize(txtFirstName.Text) + "', LastName='" + Validator.Finalize(txtLastName.Text) + "', EmailAddress='" + Validator.Finalize(txtEmailAddress.Text) + "', LastUpdateUser='" + Validator.Finalize(LUser.Username) + "', LastUpdateDate=GETDATE() WHERE StudentID='" + Request.QueryString["sid"] + "'";
                                }
                            });
                        
                        Debug.WriteLine(qry);
                        //check if has students was updated
                        if (cls.ExecuteNonQuery(qry) > 0)
                        {
                            Response.Write("<script>alert('Student information has been updated successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_student_maintenance_main) + "'</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Action can not continue. Please check your entries.')</script>");
                        }
                    }
                }
            }
        }

        //Load Student Account Details
        public void LoadStudentDetails()
        {
            StudentAccountList = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
            StudentAccountList.ForEach(sa => 
            {
                if (sa.StudentID.ToString() == Request.QueryString["sid"])
                {
                    txtStudentNumber.Text = sa.StudentNumber;
                    txtEmailAddress.Text = sa.EmailAddress;
                    txtFirstName.Text = sa.Firstname;
                    txtLastName.Text = sa.Lastname;
                    return;
                }
            });
        }

        //Cancel Button
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_student_maintenance_main));
        }
        //////////////////////
        //------------------//
        //--- Validators ---//
        //------------------//
        //////////////////////

        //Validate Fields for NULL Characters
        public bool ValidateFields()
        {
            bool status = true;

            //check if has student number
            if (Validator.isEmpty(txtStudentNumber.Text))
            {
                vlStudentNumber.Text = "* Required Field.";
                    status = false;
            }
            else
            {
                vlStudentNumber.Text ="*";
            }

            //check if has email add
            if (Validator.isEmpty(txtEmailAddress.Text))
            {
                //vlEmailAddress.Text = "* Required Field.";
                //status = false; 
            }
            else
            {
                if (Validator.isEmailNotValid(txtEmailAddress.Text))
                {
                    vlEmailAddress.Text = "* The Email Address entered is not valid.";
                    status = false;
                }
                else
                {
                    vlEmailAddress.Text = "";
                }
            }

            //check if has firstname
            if (Validator.isEmpty(txtFirstName.Text))
            {
                vlFirstName.Text = "* Required Field";
                status = false;
            }
            else
            {
                vlFirstName.Text = "*";
            }

            if (Validator.isEmpty(txtLastName.Text))
            {
                vlLastName.Text = "* Required Field";
                status = false;
            }
            else
            {
                vlLastName.Text = "*";
            }

            return status;
        }





       
    }
}
