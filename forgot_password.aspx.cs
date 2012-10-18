using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;


namespace PAOnlineAssessment
{
    public partial class forgot_password : System.Web.UI.Page
    {
        //Instantiate new List of Users
        List<Constructors.User> UserList = new List<Constructors.User>(new Collections().getUsers());
        //Instantiate new List of Student Accounts
        List<Constructors.StudentAccount> StudentAccountList = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Declare Class Containing Info
        RetrieveUser RInfo;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();

        List<Constructors.ParentAccounts> ParentList = new List<Constructors.ParentAccounts>(new Collections().GetParentAcount());
        //Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        //Check Fields for Null Values
        public bool ValidateFields()
        {
            bool Status = true;

            if (Validator.isEmpty(txtEmailAddress.Text))
            {
                vlEmailAddress.Text = "* Please enter your Email Address";
                Status = false;
            }
            else
            {
                if (Validator.isEmailNotValid(txtEmailAddress.Text))
                {
                    Response.Write("<script>alert('You have entered an Invalid Email Address')</script>");
                    vlEmailAddress.Text = "* Invalid Email Address";
                    Status = false;
                }
                else
                {
                    vlEmailAddress.Text = "*";
                }   
             
            }

            return Status;
        }

        //Login Button        
        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            //Check Fields for NULL Values
            if (ValidateFields())
            {
                if (CheckIfEmailIsRegistered(txtEmailAddress.Text))
                {
                    RInfo = (RetrieveUser)ReturnMatchedStudentAccount(txtEmailAddress.Text);
                    sys.SendLoginRequestEmail(RInfo);
                    Response.Write("<script>alert('You User Login Information has been sent to your Email.\\r\\nKindly check your Email.'); window.location='"+ResolveUrl(DefaultForms.frm_login)+"';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Email Address is not Registered in the System.\\r\\nPlease Try Again.')</script>");
                    vlEmailAddress.Text = "* Email Address is not Registered in the System.";
                }               
               
            }
        }

        //Return User Account that matched the inputted Email Address
        public object ReturnMatchedStudentAccount(string EmailAddress)
        {
            RetrieveUser RInfo = new RetrieveUser();
            int x = 0;
            StudentAccountList.ForEach(SAList => 
            {              
                if (SAList.EmailAddress.ToLower() == EmailAddress.ToLower())
                {
                    x = 1;
                    RInfo.Firstname = Validator.Finalize(SAList.Firstname);
                    RInfo.Lastname = Validator.Finalize(SAList.Lastname);
                    RInfo.EmailAddress = Validator.Finalize(EmailAddress);
                    RInfo.Username = Validator.Finalize(SAList.StudentNumber);
                    RInfo.Password = Validator.Finalize(SAList.Password);
                    return;
                }

            });
            if (x == 0)
            {
                ParentList.ForEach(pl =>
                {
                    if (pl.EmailAddress.ToLower() == EmailAddress.ToLower())
                    {
                        RInfo.Firstname = Validator.Finalize(pl.Firstname);
                        RInfo.Lastname = Validator.Finalize(pl.Lastname);
                        RInfo.EmailAddress = Validator.Finalize(EmailAddress);
                        RInfo.Username = Validator.Finalize(pl.Username);
                        RInfo.Password = Validator.Finalize(pl.Password);
                        return;
                    }
                });
                
            }
            return RInfo;
        }

        //Check if Email Address Entered is Registered in the System
        public bool CheckIfEmailIsRegistered(string EmailAddress)
        {
            bool tobeReturned = false;

            StudentAccountList.ForEach(ul =>
            {
                if (ul.EmailAddress.ToLower() == EmailAddress.ToLower())
                {
                    tobeReturned = true;
                }
            });


            if (tobeReturned == false)
            {
                ParentList.ForEach(pl => 
                {
                    if(pl.EmailAddress.ToLower() == EmailAddress.ToLower())
                    {
                        tobeReturned = true;
                    }
                });
            }

            return tobeReturned;
        }

        //Cancel BUtton
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_login));
        }
    }
}
