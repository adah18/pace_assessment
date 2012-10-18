using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;


namespace PAOnlineAssessment
{
    public partial class pending_student_accounts : System.Web.UI.Page
    {
        //Instantiate New Collections Class
        Collections cls = new Collections();
        //Instantiate New List of Student Accounts
        List<Constructors.StudentAccount> StudentAccountList = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
        //Declare New Login User Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //instantiate new list
        List<Constructors.StudentRegistrationView> StudentRegistrationView;
        
        //Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            //Validates if a User is Logged In
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];                
                //Check if User is Administrator
                LUser = (LoginUser)Session["LoggedUser"];

                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                if (Validator.CanbeAccess("4", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed");

                    Validator.AlertBack("Access Denied!", "../block_user.aspx");
                }
            }
            //Redirects to the Login Page
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }

            //Checks if Postback
            if (IsPostBack == false)
            {
                LoadUserAccounts();


                if (Convert.ToInt32(Request.QueryString["activate"]) > 0)
                {
                    Debug.WriteLine("Activate: " + Request.QueryString["activate"]);
                    LUser = (LoginUser)Session["LoggedUser"];

                    //Update Query
                    string qry = "Update [PaceAssessment].dbo.[Student] set AdminVerified = '1', LastUpdateUser='" + LUser.Username + "', LastUpdateDate=GETDATE() WHERE StudentID='" + Request.QueryString["activate"] + "'";

                    //Checks if Affected Rows are more than 1
                    if (cls.ExecuteNonQuery(qry) > 0)
                    {
                        Debug.WriteLine("***Successfully Updated Student***");

                        try
                        {
                            StudentAccountList = new List<Constructors.StudentAccount>(cls.getStudentAccounts());
                            StudentRegistrationView = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());

                            //loop through the list
                            StudentAccountList.ForEach(sa =>
                            {
                                //find the students to be activated
                                if (sa.StudentID.ToString() == Request.QueryString["activate"])
                                {
                                    //check if has student number
                                    if (!string.IsNullOrEmpty(sa.StudentNumber.Trim()))
                                    {
                                        StudentRegistrationView.ForEach(sr =>
                                            {
                                                if (sr.SchoolYear == Session["CurrentSchoolYear"].ToString() && sr.StudentNumber.ToLower() == sa.StudentNumber.ToLower())
                                                {
                                                    qry = "UPDATE PaceAssessment.dbo.Student SET FirstName='" + sr.FirstName + "', LastName='" + sr.LastName + "' WHERE StudentID='" + Request.QueryString["activate"] + "'";
                                                    cls.ExecuteNonQuery(qry);
                                                }
                                            });
                                    }
                                }
                            });
                        }
                        catch
                        {

                        }
                        Response.Write("<script>alert('Student has been activated successfully.'); window.location='"+ResolveUrl(DefaultForms.frm_pending_student_accounts)+"';</script>");
                    }
                    else
                    {
                        Debug.WriteLine("ERROR: Action cannot continue. Please review your entry.");
                        Debug.WriteLine("Query String: " + qry);
                        Response.Write("<script>alert('Action cannot continue. Please review your entry.')</script>");
                    }
                }

            }
                 
           
        }

        //Load User Accounts to the GridView
        public void LoadUserAccounts()
        {
            DataGrid.DataSource = ConvertClassListToDataTable();
            DataGrid.DataBind();
        }

        //Convert List Object to DataTable
        public DataTable ConvertClassListToDataTable()
        {
            //Declare new datatable
            DataTable dt = new DataTable();

            //Add Columns
            dt.Columns.Add(new DataColumn("StudentID"));
            dt.Columns.Add(new DataColumn("StudentNumber"));
            dt.Columns.Add(new DataColumn("Firstname"));
            dt.Columns.Add(new DataColumn("Lastname"));
            dt.Columns.Add(new DataColumn("Password"));
            dt.Columns.Add(new DataColumn("EmailAddress"));
            dt.Columns.Add(new DataColumn("EmailVerified"));
            dt.Columns.Add(new DataColumn("AdminVerified"));
            dt.Columns.Add(new DataColumn("Status"));
            dt.Columns.Add(new DataColumn("UserCreated"));
            dt.Columns.Add(new DataColumn("DateCreated"));
            dt.Columns.Add(new DataColumn("LastUpdateUser"));
            dt.Columns.Add(new DataColumn("LastUpdateDate"));

            //Switch statement for search
            switch (cboSearchQuery.SelectedValue)
            {
                //Student Number is Selected
                case "StudentNumber":
                    StudentAccountList.ForEach(SAList =>
                    {
                        if (SAList.StudentNumber.ToLower().Contains(txtSearchQuery.Text.ToLower()) && SAList.AdminVerified == "0")
                        {
                            dt.Rows.Add(SAList.StudentID, SAList.StudentNumber, SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;
                //Full Name is Selected
                case "FullName":
                    StudentAccountList.ForEach(SAList =>
                    {
                        string FullName = SAList.Firstname + " " + SAList.Lastname;
                        if (FullName.ToLower().Contains(txtSearchQuery.Text.ToLower()) && SAList.AdminVerified == "0")
                        {
                            dt.Rows.Add(SAList.StudentID, SAList.StudentNumber, SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;

                case "EmailAddress":
                    StudentAccountList.ForEach(SAList =>
                    {
                        if (SAList.EmailAddress.ToLower().Contains(txtSearchQuery.Text.ToLower()) && SAList.AdminVerified == "0")
                        {
                            dt.Rows.Add(SAList.StudentID, SAList.StudentNumber, SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;
                //Available is Selected
                case "A":
                    StudentAccountList.ForEach(SAList =>
                    {
                        if (SAList.Status == "A" && SAList.AdminVerified == "0")
                        {
                            dt.Rows.Add(SAList.StudentID, SAList.StudentNumber, SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;
                //Deactivated is Selected
                case "D":
                    StudentAccountList.ForEach(SAList =>
                    {
                        if (SAList.Status == "D" && SAList.AdminVerified == "0")
                        {
                            dt.Rows.Add(SAList.StudentID, SAList.StudentNumber, SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;
            }

            //Check if there are records in the Data Table
            if (dt.Rows.Count <= 0)
            {
                //Append a Message to Notify the User
                dt.Rows.Add(0, "No Record Found.");
            }

            //Return declared DataTable
            return dt;
        }

        //Executed when the Last Page Button has been clicked [Search]
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = DataGrid.PageCount;            
            LoadUserAccounts();
        }

        //Executed when the First Page Button has been clicked [Search]
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = 0;
            LoadUserAccounts();
        }

        //Executed when the Page Number DropDownList's value has changed [Search]
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            DataGrid.PageIndex = cboPageNumber.SelectedIndex;
            LoadUserAccounts();
        }

        //Executed when the Search Button has been clicked [Search]
        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            DataGrid.PageIndex = 0;
            LoadUserAccounts();
        }


        protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < DataGrid.Rows.Count; x++)
            {
                ImageButton imgApprove = (ImageButton)DataGrid.Rows[x].FindControl("imgApprove");
                Label lblStudentID = (Label)DataGrid.Rows[x].FindControl("lblStudentID");

                string DeactivateURL = ResolveUrl(DefaultForms.frm_pending_student_accounts)+"?activate=" + lblStudentID.Text;
                if (lblStudentID.Text == "0")
                {
                    imgApprove.ImageUrl = "~/images/icons/page_tick_disabled.gif";
                    imgApprove.OnClientClick = "return false;";
                }
                else
                {
                    imgApprove.ImageUrl = "~/images/icons/page_tick.gif";
                    imgApprove.OnClientClick = "if(confirm('Are you sure you want to activate this Student?')){window.location='" + DeactivateURL + "';} return false;";
                }
                Debug.WriteLine(DeactivateURL);
            }
        }

    }
}
