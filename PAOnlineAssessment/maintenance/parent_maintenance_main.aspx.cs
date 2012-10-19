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

namespace PAOnlineAssessment.maintenance
{
    public partial class parent_maintenance_main : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.ParentAccounts> ParentList;
        List<Constructors.ParentChilds> ParentChildList;
        SystemProcedures sp = new SystemProcedures();
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        protected void Page_Load(object sender, EventArgs e)
        {

            //Check if a User is Logged In
            try
            {
                //Get Login User Info from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                //Get Current Student Info from the Session Variable
                //CStudent = (CurrentStudent)Session["CurrentStudent"];
                string Trigger = LUser.Username;

                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                if (Validator.CanbeAccess("1", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed");

                    Validator.AlertBack("Access Denied!","../block_user.aspx");
                    //Response.Redirect("../block.aspx");
                   
                }
            }
            //Redirect to the Index Page
            catch
            {
                //Redirect to the Index page
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            if (!IsPostBack)
            {
                LoadParents();
            }

            if (Request.QueryString["approve"] == "1")
            {
                sp.ActivateParent("[User]", "UserID", Request.QueryString["uid"], LUser.Username);
                Response.Redirect("parent_maintenance_main.aspx?success=1");
                
            }

            if (Request.QueryString["view_students"] == "1")
            {
                //Request.QueryString["view_students"] = "2";
                mpeStudentVerification.Show();
                //LoadChild();
            }
        }

        void LoadParents()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentID");
            dt.Columns.Add("UserID");
            dt.Columns.Add("Username");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("Status");


            ParentList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
            ParentList.ForEach(pl =>
            {
                if (pl.Status == "D")
                {
                    dt.Rows.Add(pl.ParentID, pl.ParentID, pl.Username, pl.Firstname, pl.Lastname,pl.Status);
                }
                
            });

            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "0", "No Record Found","","","");
            }

            Session["ParentList"] = dt;
            BindParents();
        
        }
        void BindParents()
        {
            DataGrid.DataSource = Session["ParentList"];
            DataGrid.DataBind();
        }

        int ChildInputted(int ParentID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Count");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("Level");
            dt.Columns.Add("Section");

            int x = 1;
            int ctr = 1;
            ParentChildList = new List<Constructors.ParentChilds>(cls.GetParentChild(ParentID));
            ParentChildList.ForEach(pl => 
            {
                dt.Rows.Add(ctr,pl.Firstname,pl.Lastname,pl.YearLevel,pl.Section);
                ctr++;
            });
            if (dt.Rows.Count < 1)
            {
                 dt.Rows.Add("","No Child Info",0,0);
                x = 0;
            }

            Session["ChildList"] = dt;
            return x;
        }
        protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for(int i=0; i < DataGrid.Rows.Count; i++)
            {
                Label lblid = (Label)DataGrid.Rows[i].FindControl("lblParentID");
                ImageButton imgApprove = (ImageButton)DataGrid.Rows[i].FindControl("imgApprove");
                ImageButton imgView = (ImageButton)DataGrid.Rows[i].FindControl("imgView");
                GridView gvChild = (GridView)DataGrid.Rows[i].FindControl("gvChild");
                imgApprove.OnClientClick = "if(confirm('Do you really want to approve this parent?')){window.location='parent_maintenance_main.aspx?approve=1&uid=" + lblid.Text + "';} return false;";
                imgView.OnClientClick = "window.location='parent_information_view.aspx?uid=" + lblid.Text + "';";
                if (lblid.Text == "0")
                {
                    imgApprove.Enabled = false;
                    imgView.Enabled = false;
                }

                if (ChildInputted(Convert.ToInt32(lblid.Text)) == 1)
                {
                    gvChild.DataSource = Session["ChildList"];
                    gvChild.DataBind();
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
                lblPageCount.Text = "of " + DataGrid.PageCount.ToString();
            }
        }




        //Executed when the Page Number DropDownList's value has changed [Pagination]
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            DataGrid.PageIndex = cboPageNumber.SelectedIndex;
            BindParents();
        }

    

        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            SearchField(txtSearchQuery.Text);
        }

        void SearchField(string field)
        {
            DataTable dt = Session["ParentList"] as DataTable;
            dt.Rows.Clear();
            ParentList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
            ParentList.ForEach(pl =>
            {
                if (pl.Status == "D")
                {
                    switch (cboSearchQuery.SelectedValue)
                    { 
                        case "Username":
                            if (pl.Username.ToLower().Contains(field.ToLower()))
                            {
                                dt.Rows.Add(pl.ParentID, pl.ParentID, pl.Username, pl.Firstname, pl.Lastname, pl.Status);
                            }
                            break;
                        case "Firstname":
                            if (pl.Firstname.ToLower().Contains(field.ToLower()))
                            {
                                dt.Rows.Add(pl.ParentID, pl.ParentID, pl.Username, pl.Firstname, pl.Lastname, pl.Status);
                            }
                            break;
                        case "Lastname":
                            if (pl.Lastname.ToLower().Contains(field.ToLower()))
                            {
                                dt.Rows.Add(pl.ParentID, pl.ParentID, pl.Username, pl.Firstname, pl.Lastname, pl.Status);
                            }
                            break;
                    }
                }

            });

            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "0", "No Record Found", "", "", "");
            }

            Session["ParentList"] = dt;
            BindParents();
        }
        protected void DataGrid0_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void imgView_Click(object sender, ImageClickEventArgs e)
        {
          
        }
        //Executed when the Last Page Button has been clicked [Pagination]
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = DataGrid.PageCount;
            BindParents();
        }

        //Executed when the First Page Button has been clicked [Pagination]
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = 0;
            BindParents();
        }
        //next linkbutton [Pagination]
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex != DataGrid.PageCount)
            {
                DataGrid.PageIndex = DataGrid.PageIndex + 1;
                BindParents();
            }
        }
        //previous link button [Pagination]
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex != 0)
            {
                DataGrid.PageIndex = DataGrid.PageIndex - 1;
                BindParents();
            }
        }

    }
}
