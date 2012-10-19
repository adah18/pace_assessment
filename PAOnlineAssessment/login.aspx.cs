using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PAOnlineAssessment
{
    public partial class login : System.Web.UI.Page
    {
        //Instantiate New Collections Class 
        Collections cls = new Collections();
        //Instantiate new List of Users
        List<Constructors.User> UserList = new List<Constructors.User>(new Collections().getUsers());
        //Instantiate new List of Students
        List<Constructors.StudentAccount> StudentAccountList = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
        //Instantiate LoggedUser Class
        LoginUser LUser;
        //Instantiate GlobalForms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();

        //List<Constructors.ParentView> ParentList = new List<Constructors.ParentView>(new Collections().GetParent());

        List<Constructors.ParentAccounts> ParentList = new List<Constructors.ParentAccounts>(new Collections().GetParentAcount());
        String Quarter = new Collections().CurrentQuarter();

        List<Constructors.DisplaySettings> oDisplaySettings;

        List<Constructors.Quarter> oQuarter;
        //Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Session.Clear();
                Session.Abandon();
                Session["AccessRights"] = "-";
            }

            //get the quarter
            Session["Quarter"] = Quarter;
            Session["ctr"] = "0";
            Session["AID"] = "0";
            //check the display settings
            oDisplaySettings = new List<Constructors.DisplaySettings>(cls.GetDays());
            oDisplaySettings.ForEach(ds =>
                {
                    Session["Days"] = ds.Days.ToString();
                    Session["Registration"] = ds.Registration;
                });

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

        //Check Fields for Null Values
        public bool ValidateFields()
        {
            bool Status = true;

            if (Validator.isEmpty(txtUsername.Text))
            {
                vlUsername.Text = "* Please enter your username.";
                Status = false;
            }
            else
            {
                vlUsername.Text = "*";
            }

            if (Validator.isEmpty(txtPassword.Text))
            {
                vlPassword.Text = "* Please enter your password.";
                Status = false;
            }
            else
            {
                vlPassword.Text = "*";
            }

            return Status;
        }

        //Login Button        
        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            SubmitLogin();
        }
        

        //Login Procedure
        public void SubmitLogin()
        {
            //Check Fields for NULL Values
            if (ValidateFields())
            {
                //Check if User is in the User List
                if (isUserRegistered(txtUsername.Text, txtPassword.Text))
                {
                    Debug.WriteLine("User is Registered. Checking User Group Permissions");
                    //Check if User is Allowed to Access the System
                    if (isNotAccessDenied(txtUsername.Text, txtPassword.Text))
                    {
                        Debug.WriteLine("User Group is permitted. Checking Account Status");
                        //Check if User Account is Activated
                        if (isNotActivated(txtUsername.Text, txtPassword.Text) == false)
                        {
                            //Pack Logged In User to Class
                            Debug.WriteLine("User Account is Available");
                            UserLoginClass(txtUsername.Text, txtPassword.Text);
                            if ((string)Session["UserGroupID"] == "1")
                            {
                                Response.Redirect("maintenance/admin_dashboard.aspx");
                            }
                            else if ((string)Session["UserGroupID"] == "3")
                            {
                                Response.Redirect("instructor/instructor_dashboard.aspx");
                            }
                            Debug.WriteLine((string)Session["UserGroupID"] + "< - UserGroupID");
                        }
                        //Account Deactivated
                        else
                        {
                            Debug.WriteLine("Account is Deactivated");
                            lblNotification.Text = "Your Account has been Deactivated.<br>Please contact your Administrator.";
                            Response.Write("<script>alert('Your Account has been Deactivated.\\nPlease contact your Administrator.')</script>");
                        }
                    }
                    //Access Denied
                    else
                    {
                        Debug.WriteLine("Access Denied.");
                        lblNotification.Text = "Access is Denied.<br>Please contact your Administrator.";
                        Response.Write("<script>alert('Access is Denied.\\nPlease contact your Administrator.')</script>");                        
                    }
                }
                //Check if User is in the Student List
                else if (isStudentRegistered(txtUsername.Text, txtPassword.Text))
                {
                    Debug.WriteLine("Student is Registered. Checking User is Verified");
                    //Check if Account has been either Email Verified or Admin Verified
                    if (isStudentAdminVerified(txtUsername.Text, txtPassword.Text) || isStudentEmailVerified(txtUsername.Text, txtPassword.Text))
                    {
                        //Check if Account is Verified by an Admin
                        if (isStudentAdminVerified(txtUsername.Text, txtPassword.Text))
                        {
                            //Pack Logged In Student to Class
                            StudentLoginClass(txtUsername.Text, txtPassword.Text);
                            Response.Redirect(ResolveUrl(DefaultForms.frm_student_dashboard));
                            Debug.WriteLine("Success!");
                        }
                        //Account still Pending Activation
                        else
                        {
                            Debug.WriteLine("Student is not Admin Verified.");
                            lblNotification.Text = "Your account is still pending for activation.<br>Please contact your Administrator.";
                            Response.Write("<script>alert('Your account is still pending for activation.\\r\\nPlease contact your Administrator.')</script>");
                        }
                    }
                    //If Account wasn't verified
                    else
                    {
                        //Check if Email hasn't been verified
                        if (isStudentEmailVerified(txtUsername.Text, txtPassword.Text) == false)
                        {
                            Debug.WriteLine("Student is not Email Verified.");
                            lblNotification.Text = "Your email address is still not verified.<br>Follow the link sent to your inbox to verify your email.";
                            Response.Write("<script>alert('Your email address is still not verified.\\r\\nFollow the link sent to your inbox to verify your email.')</script>");
                        }
                    }
                } 
                else if (isParentRegistered(txtUsername.Text, txtPassword.Text))
                {
                    Debug.WriteLine("Parent Register");
                    ParentLogin(txtUsername.Text, txtPassword.Text);
                    Response.Redirect("parent/parent_dashboard.aspx");
                    Debug.WriteLine("UserGroupID:" + Session["UserGroupID"].ToString());
                }
                //When user is not in the User List and Student List
                else
                {
                    Debug.WriteLine("Invalid Login Details. Please Try Again.");
                    lblNotification.Text = "Invalid Login Details.<br>Please Try Again.";
                    Response.Write("<script>alert('Invalid Login Details.\\r\\nPlease Try Again.')</script>");

                }
            }
        }
    

        ///////////////////////////////////
        //-------------------------------//
        //---Teachers and System Users---//
        //-------------------------------//
        ///////////////////////////////////

        #region "Teachers and System Users and Parents"

        //Check if User is Registered
        public bool isUserRegistered(string Username, string Password)
        {
            Debug.WriteLine("***Checking if the User is Registered.***");
            bool Result = false;
            Username = Username.Trim();
            Password = Password.Trim();            

            UserList.ForEach(u => 
            {
                Debug.WriteLine(u.Username + " == " + Username);
                if (u.Username == Username && u.Password == Password)
                {
                  
                    Result = true;
                    return;                    
                }
            });

            return Result;
        }

        //Check if User is Allowed to Access the System
        public bool isNotAccessDenied(string Username, string Password)
        {
            Debug.WriteLine("***Checking if User Group is Denied***");
            bool Result = false;
            Username = Username.Trim();
            Password = Password.Trim();

            UserList.ForEach(u =>
            {
                if (u.Username == Username && u.Password == Password)
                {
                    if (u.UserGroupID == 1 || u.UserGroupID == 3)
                    {
                        Result = true;
                        return;
                    }
                }
            });


            return Result;
        }

        //Check if User Status is Available
        public bool isNotActivated(string Username, string Password)
        {
            Debug.WriteLine("***Checking if User is Deactivated***");
            bool Result = false;
            Username = Username.Trim();
            Password = Password.Trim();

            UserList.ForEach(u =>
            {
                if (u.Username == Username && u.Password == Password & u.Status == "D")
                {
                    Debug.WriteLine("User Found: " + u.Username);
                    Result = true;
                    return;
                    
                }
            });

            
            return Result;
        }

#endregion

        //////////////////
        //--------------//
        //---Students---//
        //--------------//
        //////////////////


        #region "Students"

        //Check if Student is Registered
        public bool isStudentRegistered(string Username, string Password)
        {
            Debug.WriteLine("***Checking if the Student is Registered.***");
            bool Result = false;
            Username = Username.Trim();
            Password = Password.Trim();

            StudentAccountList.ForEach(sa => 
            {
                if (sa.StudentNumber == Username && sa.Password == Password)
                {
                    Result = true;
                    return;
                }
            });

            return Result;
        }

        //Check if Student's Email is verified
        public bool isStudentEmailVerified(string Username, string Password)
        {
            Debug.WriteLine("***Checking if the Student Email is Verified.***");
            bool Result = false;
            Username = Username.Trim();
            Password = Password.Trim();

            StudentAccountList.ForEach(sa => 
            {
                if (sa.StudentNumber == Username && sa.Password == Password && sa.EmailVerified == "1")
                {
                    Result = true;
                    return;
                }
            });

            return Result;
        }

        //Check if Student is verified by the admin
        public bool isStudentAdminVerified(string Username, string Password)
        {
            Debug.WriteLine("***Checking if the Student is Verified by the Admin***");
            bool Result = false;
            Username = Username.Trim();
            Password = Password.Trim();

            StudentAccountList.ForEach(sa =>
            {
                if (sa.StudentNumber == Username && sa.Password == Password && sa.AdminVerified == "1")
                {
                    Result = true;
                    return;
                }
            });

            return Result;
        }

        #endregion


        public bool isParentRegistered(string username, string password)
        {
            bool value = false;
            ParentList.ForEach(p => 
            {
                
                if (username == p.Username && password == p.Password)
                {
                    Debug.WriteLine(username + "==" + p.Username + " && " + password + "==" + p.Password + ", " + p.Status);
                   
                    if (p.Status == "A")
                    {
                        value = true;
                    }
                }
            });
            return value;
        }
        /////////////////////////
        //---------------------//
        //---Data Procedures---//
        //---------------------//
        /////////////////////////

        #region "Data Procedures"

        //Pack Logged In Student to Class
        public void StudentLoginClass(string Username, string Password)
        {
            Debug.WriteLine("***Packing Student Info To Class***");
        
            LUser = new LoginUser();

            Username = Username.Trim();
            Password = Password.Trim();

            StudentAccountList.ForEach(sa =>
            {
                if (sa.StudentNumber == Username && sa.Password == Password)
                {
                    LUser.UserID = sa.StudentID;
                    LUser.UserGroupID = "S";
                    LUser.Username = Username;
                    LUser.Password = Password;
                    LUser.Lastname = sa.Lastname;
                    LUser.Firstname = sa.Firstname;
                    LUser.DateCreated = sa.DateCreated;
                    LUser.UserCreated = sa.UserCreated;
                    LUser.LastUpdateDate = sa.LastUpdateDate;
                    LUser.LastUpdateUser = sa.LastUpdateUser;
                    LUser.Description = "Student";
                    LUser.Status = sa.Status;
                    LUser.DateToday = DateTime.Today.ToShortDateString();
                    Session["Authenticated"] = true;
                    Session["LoggedUser"] = LUser;
                    
                    Session["UserGroupID"] = LUser.UserGroupID;  
                    return;
                }
            });

            CurrentStudent CStudent = new CurrentStudent();
            string qry = "SELECT * FROM PaceRegistration.dbo.StudentRegistrationView WHERE StudentNumber='" + Username + "'";
            Debug.WriteLine(qry);
            SqlDataReader dr = cls.ExecuteReader(qry);
            dr.Read();
            if (dr.HasRows)
            {
                CStudent.StudentID = (int)dr["StudentID"];
                CStudent.StudentNumber = dr["StudentNumber"].ToString();
                CStudent.SectionID = (int)dr["SectionID"];
                CStudent.LevelID = (int)dr["CurrentLevelID"];
                CStudent.SchoolYear = dr["SchoolYear"].ToString();
                Session["CurrentStudent"] = CStudent;
            }
         
        }

        //Pack Logged In User to Class
        public void UserLoginClass(string Username, string Password)
        {
            Debug.WriteLine("***Packing User Info To Class***");

            LUser = new LoginUser();

            Username = Username.Trim();
            Password = Password.Trim();

            UserList.ForEach(u =>
            {
                if (u.Username == Username && u.Password == Password)
                {
                    LUser.UserID = u.UserID;
                    LUser.UserGroupID = u.UserGroupID.ToString();
                    LUser.Username = Username;
                    LUser.Password = Password;
                    LUser.Lastname = u.LastName;
                    LUser.Firstname = u.FirstName;
                    LUser.DateCreated = u.DateCreated;
                    LUser.UserCreated = u.UserCreated;
                    LUser.LastUpdateDate = u.UpdateDate;
                    LUser.LastUpdateUser = u.UpdateUser;
                    LUser.Description = u.UserGroupDescription;
                    LUser.Status = u.Status;
                    LUser.DateToday = DateTime.Today.ToShortDateString();

                    int id = 0;
                    if (u.UserGroupID == 3)
                    {
                        id = 2;
                        LUser.AccessRights = GetAccessRights(id);
                    }
                    else
                    {
                        LUser.AccessRights = GetAccessRights(u.UserGroupID);
                    }
                    Session["AccessRights"] = LUser.AccessRights ;
                    Session["Authenticated"] = true;
                    Session["LoggedUser"] = LUser;
                    Session["UserGroupID"] = LUser.UserGroupID;
                    return;
                }
            });
        }

        string GetAccessRights(int usergroupid)
        {
            
            string ryts = "-";
            ryts = cls.ExecuteScalar("Select AccessRights From Usergroup Where UserGroupID=" + usergroupid);
            Debug.WriteLine("Select AccessRights From Usergroup Where UserGroupID=" + usergroupid);
            return ryts;
        }
        public void ParentLogin(string username, string password)
        {
            LUser = new LoginUser();
            ParentList.ForEach(p =>
            {
                if (username == p.Username && password == p.Password)
                {
                    if (p.Status == "A")
                    {

                        LUser.UserID = p.ParentID;
                        LUser.UserGroupID = "P";
                        LUser.Username = p.Username;
                        LUser.Password = p.Password;
                        LUser.Firstname = p.Firstname;
                        LUser.Lastname = p.Lastname;
                        LUser.Status = p.Status;
                        LUser.DateToday = DateTime.Today.ToShortDateString();
                        Session["Authenticated"] = true;
                        Session["LoggedUser"] = LUser;
                        Session["UserGroupID"] = "P";
                        return;
                    }
                }
            });
        }   
        #endregion

        //Executed when the Forgot Password Button has been clicked
        protected void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_forgot_password));
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SubmitLogin();
        }
    }
}
