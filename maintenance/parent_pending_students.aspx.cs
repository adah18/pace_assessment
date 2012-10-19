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
    public partial class parent_pending_students : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.ParentAccounts> ParentList;
        List<Constructors.ParentChilds> ParentChildList;
        List<Constructors.ParentChildGrades> ChildList;
        SystemProcedures sp = new SystemProcedures();
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();

        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
            //Redirect to the Index Page
            catch
            {
                //Redirect to the Index page
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            if (!IsPostBack)
            {
                Parents();
            }
        }

        void Parents()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentID");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("Count");

            string sql = "SELECT Distinct(ParentUserID)FROM [PaceAssessment].[dbo].[ParentChild]";
            cls.dr = cls.ExecuteReader(sql);
            if (cls.dr.HasRows)
            {
                while (cls.dr.Read())
                {
                    dt.Rows.Add(cls.dr["ParentUserID"].ToString(),"","","");
                }
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int x = 0;
                    ParentList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
                    ParentList.ForEach(pl =>
                    {
                        //Debug.WriteLine("Parent ID" + pl.ParentID.ToString() + "==  " + dt.Rows[i][0].ToString());
                        if (dt.Rows[i][0].ToString() == pl.ParentID.ToString())
                        {
                            int count = CountPendingStudents(pl.ParentID);
                            Debug.WriteLine(count +  " " + dt.Rows[i][0].ToString());
                            if (count > 0)
                            {
                                dt.Rows[i][1] = pl.Firstname;
                                dt.Rows[i][2] = pl.Lastname;
                                dt.Rows[i][3] = count;
                            }
                            else
                            {
                                x = 1;
                            }
                        }
                    });
                    if (x == 1)
                    {
                        dt.Rows[i].Delete();
                        i--;
                    }
                    
                }

            }
            //Debug.WriteLine(dt.Rows.Count + " " + dt.Rows[0][0].ToString());
            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "No Record Found","","");
            }
            Session["ParentAccountList"] = dt;
            BindData();
        }

        void BindData()
        {
            DataGrid.DataSource = Session["ParentAccountList"];
            DataGrid.DataBind();
        }

        int CountPendingStudents(int ParentID)
        {
            
            int x = 0;
            
            ChildList = new List<Constructors.ParentChildGrades>(cls.GetChild());
            ChildList.ForEach(cl =>
            {

                if (cl.ParentUserID == ParentID)
                {
                    if (cl.Status == "D")
                    {
                        x++;
                    }
                }
            });

            return x;
        }

        protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < DataGrid.Rows.Count; i++)
            {
                Label lblid = (Label)DataGrid.Rows[i].FindControl("lblParentID");
                ImageButton imgApprove = (ImageButton)DataGrid.Rows[i].FindControl("imgApprove");
                ImageButton imgView = (ImageButton)DataGrid.Rows[i].FindControl("imgView");
                GridView gvChild = (GridView)DataGrid.Rows[i].FindControl("gvChild");
                imgApprove.OnClientClick = "if(confirm('Do you really want to approve this parent?')){window.location='parent_maintenance_main.aspx?approve=1&uid=" + lblid.Text + "';} return false;";
                imgView.OnClientClick = "window.location='parent_pending_view.aspx?uid=" + lblid.Text + "';";
                if (lblid.Text == "0")
                {
                    imgApprove.Enabled = false;
                    imgView.Enabled = false;
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
            BindData();
        }

        //Executed when the Last Page Button has been clicked [Pagination]
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = DataGrid.PageCount;
            BindData();
        }

        //Executed when the First Page Button has been clicked [Pagination]
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = 0;
            BindData();
        }
        //next linkbutton [Pagination]
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex != DataGrid.PageCount)
            {
                DataGrid.PageIndex = DataGrid.PageIndex + 1;
                BindData();
            }
        }
        //previous link button [Pagination]
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex != 0)
            {
                DataGrid.PageIndex = DataGrid.PageIndex - 1;
                BindData();
            }
        }

        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            SearchField(txtSearchQuery.Text);
        }


        void SearchField(string field)
        {
            Parents();
            DataTable dtList = Session["ParentAccountList"] as DataTable;
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentID");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("Count");
           
            Debug.WriteLine(dtList.Rows.Count);
            for (int i = 0; i < dtList.Rows.Count; i++)
            {
               
                switch (cboSearchQuery.SelectedValue)
                { 
                    case "Firstname":
                        //Debug.WriteLine(dtList.Rows[i][1].ToString() + " firstname");
                        if (dtList.Rows[i][1].ToString().ToLower().Contains(field.ToLower()))
                        {
                            dt.Rows.Add(dtList.Rows[i][0].ToString(), dtList.Rows[i][1].ToString(), dtList.Rows[i][2].ToString(), dtList.Rows[i][3].ToString());
                        }
                        break;
                    case "Lastname":
                        if (dtList.Rows[i][2].ToString().ToLower().Contains(field.ToLower()))
                        {
                            dt.Rows.Add(dtList.Rows[i][0].ToString(), dtList.Rows[i][1].ToString(), dtList.Rows[i][2].ToString(), dtList.Rows[i][3].ToString());
                        }
                        break;
                }
            }
            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "No Record Found", "", "");
            }
            Session["ParentAccountList"] = dt;
            BindData();
        }
        protected void imgView_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}
