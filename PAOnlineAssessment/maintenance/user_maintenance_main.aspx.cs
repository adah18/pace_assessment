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
    public partial class user_maintenance_main : System.Web.UI.Page
    {        
        //Collections cls = new Collections();
        List<Constructors.User> UserList = new List<Constructors.User>(new Collections().getUsers());
        //Instantiate new LoginUser Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();


        //Page Load event
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

                if (Validator.CanbeAccess("5", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed");

                    Validator.AlertBack("Access Denied!", "../block_user.aspx");

                }
            }
            //Redirects to the Login Page when no User is Logged In
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }

            if (IsPostBack == false)
            {
                LoadUsers();
            }
        }

        //Load List of Users to the Grid View
        public void LoadUsers()
        {
            DataGrid.DataSource = ConvertListToDataTable();
            DataGrid.DataBind();
        }

        //Create a New Datatable containing the List of Users
        public DataTable ConvertListToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("UserGroupID"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("Username"));
            dt.Columns.Add(new DataColumn("Password"));
            dt.Columns.Add(new DataColumn("FirstName"));
            dt.Columns.Add(new DataColumn("LastName"));
            dt.Columns.Add(new DataColumn("Status"));            
            dt.Columns.Add(new DataColumn("UserCreated"));
            dt.Columns.Add(new DataColumn("DateCreated"));
            dt.Columns.Add(new DataColumn("UpdateUser"));
            dt.Columns.Add(new DataColumn("UpdateDate"));

            switch (cboSearchQuery.SelectedValue)
            {
                case "Username":
                    Debug.WriteLine("--- Username Search [User Maintenance Main] ---");
                    UserList.ForEach(ul =>
                    {                        
                        if (ul.UserGroupID == 3 && ul.Username.ToLower().Contains(txtSearchQuery.Text.ToLower().Trim()))
                        {
                            Debug.WriteLine("UserID: " + ul.UserID.ToString() + "\tUsername: " + ul.Username.ToLower());
                            dt.Rows.Add(ul.UserID, ul.UserGroupID, ul.UserGroupDescription, ul.Username, ul.Password, ul.FirstName, ul.LastName, ul.Status, ul.UserCreated, ul.DateCreated, ul.UpdateUser, ul.UpdateDate);
                        }
                    });
                    break;

                case "UserGroup":
                    Debug.WriteLine("--- User Group Search [User Maintenance Main] ---");
                    UserList.ForEach(ul =>
                    {                        
                        if (ul.UserGroupID == 3 && ul.UserGroupDescription.ToLower().Contains(txtSearchQuery.Text.ToLower().Trim()))
                        {                            
                            Debug.WriteLine("UserID: " + ul.UserID.ToString() + "\tUser Group: " + ul.UserGroupDescription.ToLower());
                            dt.Rows.Add(ul.UserID, ul.UserGroupID, ul.UserGroupDescription, ul.Username, ul.Password, ul.FirstName, ul.LastName, ul.Status, ul.UserCreated, ul.DateCreated, ul.UpdateUser, ul.UpdateDate);
                        }
                    });
                    break;

                case "FullName":
                    Debug.WriteLine("--- Full Name Search [User Maintenance Main] ---");
                    UserList.ForEach(ul =>
                    {                        
                        string FullName = ul.FirstName + " " + ul.LastName;
                        if (ul.UserGroupID == 3 && FullName.ToLower().Contains(txtSearchQuery.Text.ToLower().Trim()))
                        {
                            Debug.WriteLine("UserID: " + ul.UserID.ToString() + "\tFull Name: " + FullName.ToLower());
                            dt.Rows.Add(ul.UserID, ul.UserGroupID, ul.UserGroupDescription, ul.Username, ul.Password, ul.FirstName, ul.LastName, ul.Status, ul.UserCreated, ul.DateCreated, ul.UpdateUser, ul.UpdateDate);
                        }
                    });
                    break;
                
                case "A":
                    Debug.WriteLine("--- Available Search [User Maintenance Main] ---");
                    UserList.ForEach(ul =>
                    {                        
                        if (ul.UserGroupID == 3 && ul.Status == "A")
                        {
                            dt.Rows.Add(ul.UserID, ul.UserGroupID, ul.UserGroupDescription, ul.Username, ul.Password, ul.FirstName, ul.LastName, ul.Status, ul.UserCreated, ul.DateCreated, ul.UpdateUser, ul.UpdateDate);
                        }
                    });
                    break;

                case "D":
                    Debug.WriteLine("--- Deactivated Search [User Maintenance Main] ---");
                    UserList.ForEach(ul =>
                    {
                        if (ul.UserGroupID == 3 && ul.Status == "D")
                        {
                            dt.Rows.Add(ul.UserID, ul.UserGroupID, ul.UserGroupDescription, ul.Username, ul.Password, ul.FirstName, ul.LastName, ul.Status, ul.UserCreated, ul.DateCreated, ul.UpdateUser, ul.UpdateDate);
                        }
                    });
                    break;
            }

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(0, 0, "", "No Record Found.");
            }

            return dt;
        }

        //executed when the GridView has been Databound  
        protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                lblPageCount.Text = "of " + DataGrid.PageCount;
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                for (int x = 1; x <= DataGrid.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = DataGrid.PageIndex;
            }
        }

        //Executed when the PageNumber DropDownlist has been changed
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            DataGrid.PageIndex = cboPageNumber.SelectedIndex;
            LoadUsers();
        }

        //first page linkbutton
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = 0;
            LoadUsers();
        }

        //last page linkbutton
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = DataGrid.PageCount;
            LoadUsers();
        }

        //search button
        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            DataGrid.PageIndex = 0;
            LoadUsers();            
        }

        //previous linkbutton
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex > 0)
            {
                DataGrid.PageIndex = DataGrid.PageIndex - 1;
                LoadUsers();
            } 
        }

        //next link button
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex < DataGrid.PageCount)
            {
                DataGrid.PageIndex = DataGrid.PageIndex + 1;
                LoadUsers();
            } 
        }

    }
}
