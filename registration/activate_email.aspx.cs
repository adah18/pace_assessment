using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.registration
{
    public partial class activate_email : System.Web.UI.Page
    {
        //Instantiate New List of Student Accounts
        List<Constructors.StudentAccount> StudentAccountList; // = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
        //Instantiate Collections Class
        Collections cls = new Collections();
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Check if Page Load is Postback
            if (IsPostBack == false)
            {
                //If Parameter [Activate] is not NULL
                if (Convert.ToInt32(Request.QueryString["activate"]) > 0)
                {
                    //Get Student Info
                    Constructors.StudentAccount StudentInfo = (Constructors.StudentAccount)getRegistrationDetails(Request.QueryString["activate"]);
                    lblRegisteredEmail.Text = StudentInfo.EmailAddress;
                    lblRegisteredTo.Text = StudentInfo.Firstname + " " + StudentInfo.Lastname;
                }
                //IF Null
                else
                {
                    //Redirects to the Main Page
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }
            }
           
           
        }

        //Return a Class Contaning the Student Info
        public object getRegistrationDetails(string StudentID)
        {
            //Instantiate New List of Students
            Constructors.StudentAccount StudentInfo = new Constructors.StudentAccount();
            StudentAccountList  = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
            
            //Loop through Records
            StudentAccountList.ForEach(SAList => 
            {
                
                if (SAList.StudentID == Convert.ToInt32(StudentID))
                {
                    StudentInfo.EmailAddress = SAList.EmailAddress;
                    StudentInfo.Firstname = SAList.Firstname;
                    StudentInfo.Lastname = SAList.Lastname;
                    StudentInfo.StudentID = SAList.StudentID;
                    StudentInfo.EmailVerified = SAList.EmailVerified;
                    return;
                }                                
            });

            return StudentInfo;            
        }

        //Occurs when the AJAX Timer Ticked
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //Set Class StudentInfo to the Class Returned by the Procedure
            Constructors.StudentAccount StudentInfo = (Constructors.StudentAccount)getRegistrationDetails(Request.QueryString["activate"]);

            //Check if Email is already verified
            if (StudentInfo.EmailVerified == "0")
            {
                //Disables the Timer
                Timer1.Enabled = false;

                //Update Query
                string qry = "Update [PaceAssessment].dbo.[Student] set EmailVerified = '1', LastUpdateUser='-', LastUpdateDate=GETDATE() WHERE StudentID='" + Request.QueryString["activate"] + "'";

                //Checks if Affected Rows are more than 1
                if (cls.ExecuteNonQuery(qry) > 0)
                {
                    Debug.WriteLine("***Successfully Updated Student***");
                    Response.Write("<script>alert('Your Email Address has been activated successfully.'); window.location='"+ResolveUrl(DefaultForms.frm_index)+"';</script>");
                }
                else
                {

                    Debug.WriteLine("ERROR: Action cannot continue. Please review your entry.");
                    Debug.WriteLine("Query String: " + qry);
                    Response.Write("<script>alert('Action cannot continue. Please review your entry.')</script>");
                }
            }
            //Email is already activated
            else
            {
                Response.Write("<script>alert('Your Email Address is already activated.'); window.location='" + ResolveUrl(DefaultForms.frm_index) + "';</script>");
            }

        }        
    }
}
