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
    public partial class usergroup_maintenance_addupdate : System.Web.UI.Page
    {
        Collections cls = new Collections();

        List<Constructors.Usergroup> UsergroupList = new List<Constructors.Usergroup>(new Collections().GetUsergroup());
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        int ug_id = 0;
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
            }
            //if No Logged In User, redirect to Login Screen
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }
              LoadSiteMapDetails();
            if (Request.QueryString["mode"] == "edit")
            {
                if (!IsPostBack)
                {
                    LoadUserGroupDetails();
                  
                }
                if (Request.QueryString["ugid"] != null)
                {
                    ug_id = Convert.ToInt32(Request.QueryString["ugid"]);
                }
                else
                {
                    Validator.AlertBack("Please select a user group", "usergroup_maintenance_main.aspx");
                }
            }
        }

        //Load SiteMap Details
        public void LoadSiteMapDetails()
        {

            SiteMap1.RootNode = "Dashboard";
            SiteMap1.RootNodeToolTip = "Dashboard";
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.ParentNode = "Usergroup Maintenance";
            SiteMap1.ParentNodeToolTip = "Click to go back to User Group Maintenance";
            SiteMap1.ParentNodeURL = ResolveUrl("usergroup_maintenance_main.aspx");

            //check if mode is Register or Update
            if (Convert.ToInt32(Request.QueryString["ugid"]) > 0)
            {
                SiteMap1.CurrentNode = "Update Usergroup";
            }
            else
            {
                SiteMap1.CurrentNode = "Add New Usergroup";
            }
            //lblMode.Text = SiteMap1.CurrentNode;

        }
        void LoadUserGroupDetails()
        {
            UsergroupList.ForEach(ugl =>
            {
                if (ugl.UserGroupID.ToString() == Request.QueryString["ugid"])
                {
                    txtDescription.Text = ugl.Description;
                }
            });
        }
        
        bool CanBeSaved()
        {
            bool value = true;
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                lblDescription.Text = "Description is required field";
                value = false;
            }
            else if (Validator.isNotValid(txtDescription.Text))
            {
                lblDescription.Text = "Description contains invalid characters";
                value = false;
            }

            else if (IsExisting(txtDescription.Text,ug_id))
            {
                lblDescription.Text = "Description already exists";
                value = false;
            }
            else
            {
                lblDescription.Text = "* ";
            }
            return value;
        }

        bool IsExisting(string field, int field_id)
        {
            bool x = false;
            UsergroupList.ForEach(ugl =>
            {
                if (ugl.Description == field && ugl.UserGroupID != field_id)
                {
                    x = true;
                }
            });
            return x;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CanBeSaved())
            {
               if(Request.QueryString["mode"] == "edit")
               {
                   string sql = "Update Usergroup Set Description='" + txtDescription.Text + "', LastUpdateDate=getdate(), LastUpdateUser='zeek' Where UserGroupID="+ Request.QueryString["ugid"];
                   if (cls.ExecuteNonQuery(sql) == 1)
                   {
                       Validator.AlertBack("Usergroup has been updated successfully", "usergroup_maintenance_main.aspx");
                   }
                   else
                   {
                       Validator.AlertBack("Usergroup has not been updated successfully", "usergroup_maintenance_main.aspx");
                   }
               }
               else
               {
                 string sql = "Insert into [Usergroup](Description,AccessRights,Status,DateCreated, UserCreated, LastUpdateDate, LastUpdateUser)Values('" + txtDescription.Text + "','-','A',getdate(),'zeek',getdate(),'zeek')";

                 if (cls.ExecuteNonQuery(sql) == 1)
                 {
                    Validator.AlertBack("Usergroup has been created successfully", "usergroup_maintenance_main.aspx");
                 }
                 else
                 {
                    Validator.AlertBack("Usergroup has not been created successfully", "usergroup_maintenance_main.aspx");
                 }
               }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_usergroup_maintenance_main));
        }
    }
}
