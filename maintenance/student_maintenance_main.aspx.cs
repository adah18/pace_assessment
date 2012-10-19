using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using PAOnlineAssessment.Classes;
using System.Data;


namespace PAOnlineAssessment.maintenance
{
    public partial class student_maintenance_main : System.Web.UI.Page
    {
        /////////////////////////////////////////
        //-------------------------------------//
        //--- Declarations & Instantiations ---//
        //-------------------------------------//
        /////////////////////////////////////////

        //Instantiate New List of Student Accounts
        List<Constructors.StudentAccount> StudentAccountList = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
        //Instantiate New LoginUser Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms(); 
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        
        /////////////////////////
        //---------------------//
        //--- System Events ---//
        //---------------------//
        /////////////////////////

        //Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            //Validate if a User is Logged in
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                //Check if Logged in User is an Administrator
                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                if (Validator.CanbeAccess("3", LUser.AccessRights) == false)
                {
                    Validator.AlertBack("Access Denied!", "../block_user.aspx");
                }
            }
            //Redirects to the Login Page when no User is Logged In
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }


            //Checks if Postback
            if (IsPostBack == false)
            {
                LoadUserAccounts();
            }

            
                //validate if URL is for activation
                if (Convert.ToInt32(Request.QueryString["sid"]) > 0 && (string)Request.QueryString["action"] == "activate")
                {
                    int AffectedRows = sys.ActivateRecord("Student", "StudentID", Request.QueryString["sid"], LUser.Username);
                    if (AffectedRows > 0)
                    {
                        Response.Write("<script>alert('Student Account has been activated successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_student_maintenance_main) + "'</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Action cannot continue. Please check your entry')</script>");
                    }
                    
                }
                //validate if URL is for deactivation
                else if (Convert.ToInt32(Request.QueryString["sid"]) > 0 && (string)Request.QueryString["action"] == "deactivate")
                {
                    int AffectedRows = sys.DeactivateRecord("Student", "StudentID", Request.QueryString["sid"], LUser.Username);
                    if (AffectedRows > 0)
                    {
                        Response.Write("<script>alert('Student Account has been deactivated successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_student_maintenance_main) + "'</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Action cannot continue. Please check your entry')</script>");
                    }
                }
                //redirect to the student maintenance main page
                else
                {
                    ResolveUrl(DefaultForms.frm_student_maintenance_main);
                }
            
        }

        ///////////////////////
        //-------------------//
        //--- Load Events ---//
        //-------------------//
        ///////////////////////

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
                        if (SAList.StudentNumber.ToLower().Contains(txtSearchQuery.Text.ToLower()))
                        {
                            dt.Rows.Add(SAList.StudentID,SAList.StudentNumber,SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;
                //Full Name is Selected
                case "FullName":
                    StudentAccountList.ForEach(SAList =>
                    {
                        string FullName = SAList.Firstname + " " + SAList.Lastname;
                        if (FullName.ToLower().Contains(txtSearchQuery.Text.ToLower()))
                        {
                            dt.Rows.Add(SAList.StudentID, SAList.StudentNumber, SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;

                case "EmailAddress":
                    StudentAccountList.ForEach(SAList =>
                    {
                       if (SAList.EmailAddress.ToLower().Contains(txtSearchQuery.Text.ToLower()))
                        {
                            dt.Rows.Add(SAList.StudentID, SAList.StudentNumber, SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;
                //Available is Selected
                case "A":
                    StudentAccountList.ForEach(SAList =>
                    {
                        if (SAList.Status == "A")
                        {
                            dt.Rows.Add(SAList.StudentID, SAList.StudentNumber, SAList.Firstname, SAList.Lastname, SAList.Password, SAList.EmailAddress, SAList.EmailVerified, SAList.AdminVerified, SAList.Status, SAList.UserCreated, SAList.DateCreated, SAList.LastUpdateUser, SAList.LastUpdateDate);
                        }
                    });
                    break;
                //Deactivated is Selected
                case "D":
                    StudentAccountList.ForEach(SAList =>
                    {
                        if (SAList.Status == "D")
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
        
        ////////////////////////////////////////
        //------------------------------------//
        //--- GridView Events & Procedures ---//
        //------------------------------------//
        ////////////////////////////////////////

        //executed when the GridView has been data bound
        protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < DataGrid.Rows.Count; x++)
            {
                //declaration
                ImageButton imgEdit = (ImageButton)DataGrid.Rows[x].FindControl("imgEdit");
                ImageButton imgDeactivate = (ImageButton)DataGrid.Rows[x].FindControl("imgDeactivate");
                Label lblStatus = (Label)DataGrid.Rows[x].FindControl("lblStatus");
                Label lblStudentID = (Label)DataGrid.Rows[x].FindControl("lblStudentID");                    
                Label lblEmailVerified = (Label)DataGrid.Rows[x].FindControl("Label2");
                Label lblAdminVerified = (Label)DataGrid.Rows[x].FindControl("Label3");

                //change text when verified
                switch (lblEmailVerified.Text)
                {
                    case "1":
                        lblEmailVerified.Text = "True";
                        break;
                    case "0":
                        lblEmailVerified.Text = "False";
                        break;
                }

                //change text when verified
                switch (lblAdminVerified.Text)
                {
                    case "1":
                        lblAdminVerified.Text = "True";
                        break;
                    case "0":
                        lblAdminVerified.Text = "False";
                        break;
                }

                //if a record was present
                if (lblStudentID.Text != "0")
                {
                    string EditURL = ResolveUrl(DefaultForms.frm_student_maintenance_addupdate) + "?action=edit&sid=" + lblStudentID.Text;
                    imgEdit.PostBackUrl = EditURL;
                    imgEdit.ImageUrl = "~/images/icons/page_edit.gif";
                    imgEdit.Enabled = true;
                    imgEdit.ToolTip = "Edit Student Account Details";

                    if (lblStatus.Text == "A")
                    {
                        string DeactivateURL = ResolveUrl(DefaultForms.frm_student_maintenance_main) + "?action=deactivate&sid=" + lblStudentID.Text;                        
                        imgDeactivate.OnClientClick = "if (confirm('Are you sure you want to deactivate this Student Account?')) {window.location='"+DeactivateURL+"';} return false;";
                        imgDeactivate.ImageUrl = "~/images/icons/page_delete.gif";                        
                        imgDeactivate.Enabled = true;
                        imgDeactivate.ToolTip = "Deactivate Student Account";
                    }
                    else
                    {
                        string ActivateURL = ResolveUrl(DefaultForms.frm_student_maintenance_main) + "?action=activate&sid=" + lblStudentID.Text;
                        imgDeactivate.OnClientClick = "if (confirm('Are you sure you want to activate this Student Account?')) {window.location='" + ActivateURL + "';} return false;";
                        imgDeactivate.ImageUrl = "~/images/icons/page_tick.gif";
                        imgDeactivate.Enabled = true;
                        imgDeactivate.ToolTip = "Activate Student Account";
                    }
                }
                //if no record found
                else
                {
                    imgEdit.PostBackUrl = "";
                    imgEdit.Enabled = false;
                    imgEdit.ToolTip = "";
                    imgEdit.ImageUrl = "~/images/icons/page_edit_disabled.gif";
                }
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");                
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                
                cboPageNumber.Items.Clear();
                for (int x = 1; x <= DataGrid.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = DataGrid.PageIndex;
                lblPageCount.Text = "of " +DataGrid.PageCount.ToString();
            }
        }             

        //previous link button [Pagination]
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex != 0)
            {
                DataGrid.PageIndex = DataGrid.PageIndex - 1;
                LoadUserAccounts();
            }
        }

        //Executed when the Last Page Button has been clicked [Pagination]
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = DataGrid.PageCount;            
            LoadUserAccounts();
        }

        //Executed when the First Page Button has been clicked [Pagination]
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = 0;
            LoadUserAccounts();
        }

        //Executed when the Page Number DropDownList's value has changed [Pagination]
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
        
        //next linkbutton [Pagination]
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex != DataGrid.PageCount)
            {
                DataGrid.PageIndex = DataGrid.PageIndex + 1;
                LoadUserAccounts();
            }
        }

        //Executed before rendering the gridview
        protected void DataGrid_PreRender(object sender, EventArgs e)
        {
            DataGrid.BottomPagerRow.Visible = true;
            DataGrid.BottomPagerRow.BorderColor = System.Drawing.Color.Black;
        }
    }
}
