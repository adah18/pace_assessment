using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.assessment
{
    public partial class assessmenttype_maintenance_main : System.Web.UI.Page
    {
        //Instantiate New Collections Class
        Collections cls = new Collections();
        //Instantiate New List of Assessment Type
        List<Constructors.AssessmentType> AssessmentTypeList;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Declare Login User Class;
        LoginUser LUser;
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();


        //Page Load Event    
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In
            try
            {
                //Get User Details from Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == true)
                {
                }
                else
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_login));
                }

                if (Validator.CanbeAccess("12", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed");

                    Validator.AlertBack("Access Denied!", "../block_user.aspx");
                }
            }
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_login));
            }

            //Check if request is Postback
            if (IsPostBack == false)
            {
                //Load Listof Assessment Types
                LoadAssessmentTypes();
            }
            if (Convert.ToInt32(Request.QueryString["atid"]) > 0)
                {
                    if (Request.QueryString["action"] == "deactivate")
                    {
                        int AffectedRows = sys.DeactivateRecord("AssessmentType", "AssessmentTypeID", Request.QueryString["atid"], LUser.Username);
                        if (AffectedRows > 0)
                        {
                            Response.Write("<script>alert('Assessment Type has been deactivated successfully.'); window.location='"+ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main)+"'</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Action cannot continue. Please check your entry')</script>");
                        }
                    }
                    else if (Request.QueryString["action"] == "activate")
                    {
                        int AffectedRows = sys.ActivateRecord("AssessmentType", "AssessmentTypeID", Request.QueryString["atid"], LUser.Username);
                        if (AffectedRows > 0)
                        {
                            Response.Write("<script>alert('Assessment Type has been activated successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main) + "'</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Action cannot continue. Please check your entry')</script>");
                        }
                    }
                }
            
        }

        //Load List of Assessment Types
        public void LoadAssessmentTypes()
        {
            //Assign Data Source to the Grid View
            DataGrid.DataSource = ConvertToDataTable();
            //Bind Data Source to the Grid View
            DataGrid.DataBind();
        }

        //Convert List to Data Table
        public DataTable ConvertToDataTable()
        {
            //Instantiate New Data Table
            DataTable dt = new DataTable();

            //Add Columns to the Newly created DataTable
            dt.Columns.Add(new DataColumn("AssessmentTypeID"));
            dt.Columns.Add(new DataColumn("Description"));
            dt.Columns.Add(new DataColumn("Status"));
            dt.Columns.Add(new DataColumn("UserCreated"));
            dt.Columns.Add(new DataColumn("DateCreated"));
            dt.Columns.Add(new DataColumn("LastUpdateUser"));
            dt.Columns.Add(new DataColumn("LastUpdateDate"));

            //Instantiate New List of AssessmentTypes
            AssessmentTypeList = new List<Constructors.AssessmentType>(new Collections().getAssessmentType());

            //Search Dialog Box
            switch (cboSearchQuery.SelectedValue)
            {
                case "Description":
                    AssessmentTypeList.ForEach(at => 
                    {
                        if (at.Description.ToLower().Contains(txtSearchQuery.Text.ToLower()))
                        {
                            dt.Rows.Add(at.AssessmentTypeID, at.Description, at.Status, at.UserCreated, at.DateCreated, at.LastUpdateUser, at.LastUpdateDate);
                        }

                    });
                    break;
                case "A":
                    AssessmentTypeList.ForEach(at =>
                    {
                        if (at.Status == "A")
                        {
                            dt.Rows.Add(at.AssessmentTypeID, at.Description, at.Status, at.UserCreated, at.DateCreated, at.LastUpdateUser, at.LastUpdateDate);
                        }

                    });
                    break;
                case "D":
                    AssessmentTypeList.ForEach(at =>
                    {
                        if (at.Status == "D")
                        {
                            dt.Rows.Add(at.AssessmentTypeID, at.Description, at.Status, at.UserCreated, at.DateCreated, at.LastUpdateUser, at.LastUpdateDate);
                        }

                    });
                    break;
            }
            
            //If Rows in the DataTable is Empty
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(0, "No Record Found");
            }
            
            return dt;
        }


        ///////////////////////////////
        //---------------------------//
        //--- GridView Procedures ---//
        //---------------------------//
        ///////////////////////////////

        #region "Grid View Procedures"

        //executed when the rows has been data bound
        protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < DataGrid.Rows.Count; x++)
            {

                Label lblAssessmentTypeID = (Label)DataGrid.Rows[x].FindControl("lblAssessmentTypeID");

                ImageButton imgEdit = (ImageButton)DataGrid.Rows[x].FindControl("imgEdit");
                string EditURL = ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_addupdate) + "?action=edit&atid=" + lblAssessmentTypeID.Text;
                imgEdit.ToolTip = "Edit Assessment Type";
                imgEdit.PostBackUrl = EditURL;                
                
                ImageButton imgDeactivate = (ImageButton)DataGrid.Rows[x].FindControl("imgDeactivate");
                Label lblStatus = (Label)DataGrid.Rows[x].FindControl("lblStatus");
               

                if (lblStatus.Text == "A")
                {
                    string DeactivateURL = ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main) + "?action=deactivate&atid=" + lblAssessmentTypeID.Text;
                    imgDeactivate.ImageUrl = "~/images/icons/page_delete.gif";
                    imgDeactivate.ToolTip = "Deactivate Assessment Type";
                    imgDeactivate.OnClientClick = "if (confirm('Are you sure you want to deactivate this Assessment Type?')){window.location='" + DeactivateURL + "'} return false;";
                    Debug.WriteLine("DeactivateURL: " + DeactivateURL);
                }
                else
                {
                    string DeactivateURL = ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main) + "?action=activate&atid=" + lblAssessmentTypeID.Text;
                    imgDeactivate.ImageUrl = "~/images/icons/page_tick.gif";
                    imgDeactivate.ToolTip = "Activate Assessment Type";
                    imgDeactivate.OnClientClick = "if (confirm('Are you sure you want to activate this Assessment Type?')){window.location='" + DeactivateURL + "'} return false;";
                    Debug.WriteLine("ActivateURL: " + DeactivateURL);
                }

                if (lblAssessmentTypeID.Text == "0")
                {
                    imgEdit.ToolTip = "";
                    imgEdit.Enabled = false;
                    imgEdit.ImageUrl = "~/images/icons/page_edit_disabled.gif";
                    imgDeactivate.ToolTip = "";
                    imgDeactivate.Enabled = false;
                    imgDeactivate.ImageUrl = "~/images/icons/page_delete_disabled.gif";
                }
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                cboPageNumber.Items.Clear();
                for (int x = 1; x <= DataGrid.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                lblPageCount.Text = "of " + DataGrid.PageCount.ToString();
                cboPageNumber.SelectedIndex = DataGrid.PageIndex;
            }
        }

        //executed when the First Page Linkbutton has been clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = 0;
            LoadAssessmentTypes();
        }

        //Executed when the Previous Page Linkbutton has been clicked
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex != 0)
            {
                DataGrid.PageIndex = DataGrid.PageIndex - 1;
                LoadAssessmentTypes();
            }
        }

        //Executed when the PageNumber ComboBox's Values has been changed
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            DataGrid.PageIndex = cboPageNumber.SelectedIndex;
            LoadAssessmentTypes();
        }

        //Executed when the Next Page Linkbutton has been clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (DataGrid.PageIndex != DataGrid.PageCount)
            {
                DataGrid.PageIndex = DataGrid.PageIndex + 1;
                LoadAssessmentTypes();
            }
        }

        //Executed when the Last Page Linkbutton has been clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            DataGrid.PageIndex = DataGrid.PageCount;
            LoadAssessmentTypes();
        }

        //Executed when the Search Button has been clicked
        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            DataGrid.PageIndex = 0;
            LoadAssessmentTypes();
        }
        #endregion

        protected void DataGrid_PreRender(object sender, EventArgs e)
        {
            DataGrid.BottomPagerRow.Visible = true;
            DataGrid.BottomPagerRow.BorderColor = System.Drawing.Color.Black;
        }

        protected void DataGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
