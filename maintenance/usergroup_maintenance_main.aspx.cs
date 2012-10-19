using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;


namespace PAOnlineAssessment.maintenance
{
    public partial class usergroup_maintenance_main : System.Web.UI.Page
    {
        Collections cls = new Collections();

        List<Constructors.Usergroup> UsergroupList = new List<Constructors.Usergroup>(new Collections().GetUsergroup());
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        LoginUser LUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Logged In User Info from Session Variable
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                if (Validator.CanbeAccess("6", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed");

                    Validator.AlertBack("Access Denied!", "../block_user.aspx");
                }
            }
            //if No Logged In User, redirect to Login Screen
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }


            if (!IsPostBack)
            {
                LoadUsergroup();
            }
            if (Request.QueryString["change_stat"] == "1")
            {
                SystemProcedures sp = new SystemProcedures();
                if (Request.QueryString["stat"] == "A")
                {
                    if (sp.DeactivateRecord("USergroup", "UserGroupID", Request.QueryString["ugid"], "zeEk") == 1)
                    {
                        Validator.AlertBack("Usergroup has been deactivated successfully.", "usergroup_maintenance_main.aspx");
                    }
                }
                else
                {
                    if (sp.ActivateRecord("USergroup", "UserGroupID", Request.QueryString["ugid"], "zeEk") == 1)
                    {
                        Validator.AlertBack("Usergroup has been activated successfully.", "usergroup_maintenance_main.aspx");
                    }
                }
            }
        }

        void LoadUsergroup()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UsergroupID");
            dt.Columns.Add("Description");
            dt.Columns.Add("Status");
            UsergroupList.ForEach(ugl =>
            {
                dt.Rows.Add(ugl.UserGroupID,ugl.Description,ugl.Status);
            });
            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "No Record Found");
            }

            Session["UsergroupList"] = dt;
            BindData();
        }

        void BindData()
        {
            gvUsergroup.DataSource = Session["UsergroupList"];
            gvUsergroup.DataBind();
        }




        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            SearchUserGroup(txtSearchQuery.Text);
        }

        void SearchUserGroup(string field)
        {
            DataTable dt = (DataTable)Session["UsergroupList"];
            dt.Rows.Clear();
            UsergroupList.ForEach(ugl => 
            {
                switch (cboSearchQuery.SelectedValue)
                { 
                    case "Description":
                        if (ugl.Description.ToLower().Contains(field.ToLower()))
                        {
                            dt.Rows.Add(ugl.UserGroupID, ugl.Description, ugl.Status);
                        }
                        break;
                    case "A":
                        if (ugl.Status == "A")
                        {
                            dt.Rows.Add(ugl.UserGroupID, ugl.Description, ugl.Status);
                        }
                        break;
                    case "D":
                        if (ugl.Status == "D")
                        {
                            dt.Rows.Add(ugl.UserGroupID, ugl.Description, ugl.Status);
                        }
                        break;
                }
            });

            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "No Record Found");
            }

            Session["UsergroupList"] = dt;
            BindData();
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            gvUsergroup.PageIndex = 0;
            BindData();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (gvUsergroup.PageIndex != 0)
            {
                gvUsergroup.PageIndex -= 1;
                BindData();
            }
        }

        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            gvUsergroup.PageIndex = Convert.ToInt32(cboPageNumber.SelectedValue);
            BindData();
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (gvUsergroup.PageIndex != gvUsergroup.PageCount)
            {
                gvUsergroup.PageIndex += 1;
                BindData();
            }
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {
            gvUsergroup.PageIndex = gvUsergroup.PageCount;
            BindData();
        }

        protected void gvUsergroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < gvUsergroup.Rows.Count; i++)
            {
                Label lblID = (Label)gvUsergroup.Rows[i].FindControl("lblID");
                Label lblStatus = (Label)gvUsergroup.Rows[i].FindControl("lblStatus");
                ImageButton btnEdit = (ImageButton)gvUsergroup.Rows[i].FindControl("btnEdit");
                ImageButton btnStatus = (ImageButton)gvUsergroup.Rows[i].FindControl("btnStatus");
                ImageButton btnUserPriv = (ImageButton)gvUsergroup.Rows[i].FindControl("btnUserPriv");


                if (lblID.Text == "0")
                {
                    btnEdit.Enabled = false;
                    btnStatus.Enabled = false;
                    btnUserPriv.Enabled = false;
                    btnEdit.ImageUrl = "~/images/icons/page_edit_disabled.gif";
                    btnStatus.ImageUrl = "~/images/icons/page_delete_disabled.gif";
                    btnUserPriv.ImageUrl = "~/images/icons/page_user.gif";
                }
                else
                {
                    btnUserPriv.Enabled = true;
                    btnUserPriv.ImageUrl = "~/images/icons/page_user.gif";
                    btnUserPriv.PostBackUrl = "user_privilege.aspx?ugid=" + lblID.Text;
                    btnEdit.ImageUrl = "~/images/icons/page_edit.gif";
                    btnEdit.Enabled = true;
                    btnStatus.Enabled = true;
                    btnEdit.PostBackUrl = "usergroup_maintenance_addupdate.aspx?mode=edit&ugid=" + lblID.Text;
                    btnEdit.ToolTip = "Edit Usergroup";
                    btnStatus.Enabled = true;
                    if (lblStatus.Text == "A")
                    {
                        btnStatus.ImageUrl = "~/images/icons/page_delete.gif";
                        btnStatus.ToolTip = "Deactivate Usergroup";
                        btnStatus.OnClientClick = "if(confirm('Do you want to deactivate this Usergroup?')){window.location='usergroup_maintenance_main.aspx?change_stat=1&stat=" + lblStatus.Text + "&ugid=" + lblID.Text + "';}return false;";
                    }
                    else
                    {
                        btnStatus.ImageUrl = "~/images/icons/page_tick.gif";
                        btnStatus.ToolTip = "Activate Usergroup";
                        btnStatus.OnClientClick = "if(confirm('Do you want to activate this Usergroup?')){window.location='usergroup_maintenance_main.aspx?change_stat=1&stat=" + lblStatus.Text + "&ugid=" + lblID.Text + "';}return false;";
                    }
                }


                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                    Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                    cboPageNumber.Items.Clear();
                    for (int x = 1; x <= gvUsergroup.PageCount; x++)
                    {
                        cboPageNumber.Items.Add(x.ToString());
                    }
                    lblPageCount.Text = "of " + gvUsergroup.PageCount.ToString();
                    cboPageNumber.SelectedIndex = gvUsergroup.PageIndex;
                }
            }

        }


    }
}
