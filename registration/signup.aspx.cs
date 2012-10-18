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
    public partial class signup : System.Web.UI.Page
    {
        //Instantiate new Collections Class
        Collections cls = new Collections();
        //Instantiate New List of Student Accounts
        List<Collections.StudentAccount> StudentAccountList; // = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures(); 
        //Declare a list for parent accounts
        List<Constructors.ParentAccounts> ParentAccountList;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //instantiate new list
        List<Constructors.StudentRegistrationView> StudentRegistrationView;
        List<Constructors.Quarter> oQuarter;
        //Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //get the current school year
            oQuarter = new List<Constructors.Quarter>(cls.getQuarter());
            Session["CurrentSchoolYear"] = "2010-2011";
            oQuarter.ForEach(q =>
            {
                if (q.isCurrentSY == "YES")
                {
                    Session["CurrentSchoolYear"] = q.SchoolYear;
                }
            });
        }

        //Check Fields for NULL and Invalid Characters
        public bool validatefields()
        {
            bool Status = true;

            //validate First Name Field
            if (Validator.isEmpty(txtFirstName.Text))
            {
                vlFirstName.Text = "* Please specify your First Name.";
                Status = false;
            }
            else 
            {
                vlFirstName.Text = "*";
            }

            vlStudentNumber.Text = "";
            //if entered with student number
            if (Validator.isEmpty(txtStudentNumber.Text) == false)
            {
                //check if student number exist in Pace Assessment
                if (isStudentNumberExist(txtStudentNumber.Text) == true)
                {
                    vlStudentNumber.Text = "* Student Number is already registered in the System.";
                    Status = false;
                }

                //check if student number exist in Pace Registration
                if (isStudentNumberValid(txtStudentNumber.Text) == false)
                {
                    vlStudentNumber.Text = "* Invalid Student Number.";
                    Status = false;
                }
            }
           
            //Validate Last Name
            if (Validator.isEmpty(txtLastName.Text))
            {
                vlLastName.Text = "* Please specify your Last Name.";
                Status = false;
            }
            else
            {
                vlLastName.Text = "*";
            }

            //Validate Password Fields for NULL Values
            if (Validator.isEmpty(txtPassword.Text) || Validator.isEmpty(txtConfirmPassword.Text))
            {
                vlPassword.Text = "* Please complete the Passwords Field.";
                Status = false;
            }
            else
            {
                //Validate Password Fields if Strings Matched
                if (Validator.isNotEqual(txtPassword.Text, txtConfirmPassword.Text))
                {
                    vlPassword.Text = "* Passwords does not match. Please Try Again.";
                    Status = false;
                }
                else
                {
                    vlPassword.Text = "*";
                }
            }


            vlEmailAddress.Text = "";
            //validate Email Field for NULL Values
            if (Validator.isEmpty(txtEmailAddress.Text))
            {
                //vlEmailAddress.Text = "* Please specify a valid Email Address.";
                //Status = false;
            }
            else
            {
                //Check if string is a valid Email
                if (Validator.isEmailNotValid(txtEmailAddress.Text))
                {
                    vlEmailAddress.Text = "* Invalid Email Address.";
                    Status = false;
                }

                //else
                //{
                //    //Check if Email already exists in the List of Accounts
                //    if (CheckIfEmailExists(txtEmailAddress.Text))
                //    {
                //        vlEmailAddress.Text = "* Email Address is already registered in the System.";
                //        Status = false;
                //    }
                //    else
                //    {
                //        vlEmailAddress.Text = "";
                //    }
                //}
            }

            //check if captcha is enabled
            if (upCaptcha.Visible == true)
            {
                //Validate if Textbox is Null
                if (Validator.isEmpty(txtCaptcha.Text))
                {
                    vlCaptcha.Text = "* Please enter the text shown in the Image.";
                    Status = false;
                }
                else 
                {
                    //Validate Entered Text
                    ccRegister.ValidateCaptcha(txtCaptcha.Text.ToUpper());
                    //Checks if Entered Text matches the Generated Captcha
                    if (ccRegister.UserValidated == false)
                    {
                        vlCaptcha.Text = "* The text you entered does not match the text shown in the image.";
                        Status = false;
                    }
                    else
                    {
                        vlCaptcha.Text = "*";
                    }
                }
            }

            return Status;
        }

        //check if student exist in the pace assessment
        bool isStudentNumberExist(string StudentNumber)
        {
            bool isExist = false;
            //instantiate new list
            StudentAccountList = new List<Constructors.StudentAccount>(cls.getStudentAccounts());
            //loop through the list
            StudentAccountList.ForEach(s =>
                {
                    //check if student number already exist
                    if (s.StudentNumber.ToLower() == StudentNumber.ToLower())
                        isExist = true;
                });

            //return result
            return isExist;
        }

        //check if student number exist in the Pace Registration
        bool isStudentNumberValid(string StudentNumber)
        {
            bool isValid = false;
            //instantiate new list
            StudentRegistrationView = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentRegistrationView.ForEach(s =>
                {
                    //check if exist in Pace Registration
                    if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.StudentNumber.ToLower() == StudentNumber.ToLower())
                        isValid = true;
                });

            //return result
            return isValid;
        }

        protected void lnkRegister_Click(object sender, EventArgs e)
        {            
            if (validatefields())
            {                
                string qry = "INSERT INTO [PaceAssessment].dbo.[Student] (StudentNumber, Firstname, Lastname, Password, EmailAddress, EmailVerified, AdminVerified, Status, UserCreated, DateCreated, LastUpdateUser, LastUpdateDate) ";
                qry = qry +  "VALUES('"+ Validator.Finalize(txtStudentNumber.Text) + "', '" + Validator.Finalize(txtFirstName.Text) + "', '" + Validator.Finalize(txtLastName.Text) + "', '" + Validator.Finalize(txtPassword.Text) + "', '"+ Validator.Finalize(txtEmailAddress.Text) + "', '0', '0', 'A', '-', GETDATE(), '-', GETDATE())";
                if (cls.ExecuteNonQuery(qry) > 0)
                {
                    Debug.WriteLine("A New User has been Registered.");
                    //Send Activation Email
                    sys.SendActivationEmail(ConvertUserDetailsToClass(), Request.Url.GetLeftPart(UriPartial.Authority)+ResolveUrl(DefaultForms.frm_activate_email)+"?activate=", Page.Session.SessionID);
                    //Alert Ulert
                    Response.Write("<script>alert('You are Successfully Registered.\\r\\nPlease check your email address to verify your account.'); window.location='"+Page.ResolveUrl("~/index.aspx")+"';</script>");                        
                    
                }
                else
                {
                    Debug.WriteLine("ERROR: Action cannot continue. Please review your entry.");
                    Response.Write("<script>alert('Action cannot continue. Please review your entry.')</script>");
                }
            }            
        }

        public bool CheckIfEmailExists(string Email)
        {

            StudentAccountList = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());

            bool tobeReturned = false;
            StudentAccountList.ForEach(ul => 
            {
                
                if (ul.EmailAddress.ToLower() == Email.ToLower())
                {
                    tobeReturned = true;
                    return;
                }
            });

            if (tobeReturned == false)
            {
                ParentAccountList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
                ParentAccountList.ForEach(pl => 
                {
                    if (pl.EmailAddress == Email)
                    {
                        tobeReturned = true;
                    }
                });
            }
            return tobeReturned;            
        }

        public object ConvertUserDetailsToClass()
        {
            Constructors.StudentAccount newStudentAccount = new Constructors.StudentAccount();
            StudentAccountList = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());

            StudentAccountList.ForEach(sa => 
            {
                if (sa.Firstname == Validator.Finalize(txtFirstName.Text) && sa.Lastname == Validator.Finalize(txtLastName.Text) && sa.Password == Validator.Finalize(txtPassword.Text))
                {
                    newStudentAccount.Firstname = sa.Firstname;
                    newStudentAccount.Lastname = sa.Lastname;
                    newStudentAccount.EmailAddress = sa.EmailAddress;
                    newStudentAccount.StudentID = sa.StudentID;
                    return;
                }
            });

            return newStudentAccount;
        }
    }
}
