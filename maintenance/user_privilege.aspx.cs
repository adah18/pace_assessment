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
    public partial class user_privilege : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.Usergroup> UsergroupList = new List<Constructors.Usergroup>(new Collections().GetUsergroup());
        //Instantiate new log in user
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();

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

            if (!IsPostBack)
            {
                LoadModules();
                if (Request.QueryString["ugid"] != null)
                {
                    LoadUsergroupDetails();
                    if (string.IsNullOrEmpty(lblUsergroup.Text))
                    {
                        Response.Redirect("usergroup_maintenance_main.aspx");
                    }
                    if (Request.QueryString["ugid"] == "1")
                    {
                        chkMaintenance.Enabled = false;
                        chklMaintenance.Enabled = false;
                        chkAssessment.Enabled = false;
                        chklAssessment.Enabled = false;
                        chkAcademic.Enabled = false;
                        chklAcademic.Enabled = false;
                    }
                    else if (Request.QueryString["ugid"] == "2")
                    {
                        //chkAcademic.Enabled = false;
                        //chklAcademic.Enabled = false;
                    }

                }
                else
                {
                    Validator.AlertBack("Select a usergroup first","usergroup_maintenance_main.aspx");
                }
            }
        }

        void LoadUsergroupDetails()
        {
            UsergroupList.ForEach(ugl => 
            {
                if (ugl.UserGroupID.ToString() == Request.QueryString["ugid"])
                {
                    lblUsergroup.Text = ugl.Description;
                    Label1.Text = ugl.AccessRights;
                }
            });

            if (Request.QueryString["ugid"] == "2")
            {
                Label1.Text += "7-8-9-10-13-14-15-16-";
            }

            for (int i = 0; i < chklMaintenance.Items.Count; i++)
            {
                string ryts = "-" + chklMaintenance.Items[i].Value + "-";
                if (Label1.Text.ToLower().Contains(ryts.ToLower()))
                {
                    chklMaintenance.Items[i].Selected = true;
                }
            }

            for (int i = 0; i < chklAssessment.Items.Count; i++)
            {
                string ryts = "-" + chklAssessment.Items[i].Value + "-";
                if (Label1.Text.ToLower().Contains(ryts.ToLower()))
                {
                    chklAssessment.Items[i].Selected = true;
                }
            }

            for (int i = 0; i < chklAcademic.Items.Count; i++)
            {
                string ryts = "-" + chklAcademic.Items[i].Value + "-";
                if (Label1.Text.ToLower().Contains(ryts.ToLower()))
                {
                    chklAcademic.Items[i].Selected = true;
                }
            }

            int xm = 0;
            for (int i = 0; i < chklMaintenance.Items.Count; i++)
            {
                if (chklMaintenance.Items[i].Selected == false)
                {
                    xm = 1;
                }
            }
            if (xm == 1)
            { 
                chkMaintenance.Checked = false;
            }
            else
            {
                chkMaintenance.Checked = true;
            }

            int xa = 0;
            for (int i = 0; i < chklAssessment.Items.Count; i++)
            {
                if (chklAssessment.Items[i].Selected == false)
                {
                    xa = 1;
                }
            }

            if (xa == 1)
            {
                chkAssessment.Checked = false;
            }
            else
            {
                chkAssessment.Checked = true;
            }

            int xc = 0;
            for (int i = 0; i < chklAcademic.Items.Count; i++)
            {
                if (chklAcademic.Items[i].Selected == false)
                {
                    xc = 1;
                }
            }

            if (xc == 1)
            {
                chkAcademic.Checked = false;
            }
            else
            {
                chkAcademic.Checked = true;
            }

        }

        void LoadModules()
        {
            //for the maintenance tools
            //display maintenance
            chklMaintenance.Items.Add(new ListItem(" <b>Display Maintenance</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for the assessment display and parent's registration page", "0"));
            //parent pending accounts
            chklMaintenance.Items.Add(new ListItem(" <b>Pending Parent Accounts</b><br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for all pending registration of parent", "1"));
            //parent pending child request
            chklMaintenance.Items.Add(new ListItem(" <b>Pending Parent-Child Request</b><br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for all pending request of parent to view the grades of their children", "17"));
            //pending student accounts
            chklMaintenance.Items.Add(new ListItem(" <b>Pending Student Accounts</b><br> &nbsp;&nbsp;&nbsp;&nbsp; Module for all pending registration of student", "4"));
            //quarter maintenance
            chklMaintenance.Items.Add(new ListItem(" <b>Quarter Maintenance</b><br> &nbsp;&nbsp;&nbsp;&nbsp; Module for updating the quarter's date(s)", "2"));
            //school year maintenance
            chklMaintenance.Items.Add(new ListItem(" <b>School Year Maintenance</b><br> &nbsp;&nbsp;&nbsp;&nbsp; Module for updating the current school year setting", "19"));
            //student maintenance
            chklMaintenance.Items.Add(new ListItem(" <b>Student Maintenance</b><br> &nbsp;&nbsp;&nbsp;&nbsp; Module for student registered in the system", "3"));
            //user maintenance
            chklMaintenance.Items.Add(new ListItem(" <b>User Maintenance</b><br> &nbsp;&nbsp;&nbsp;&nbsp; Module for user maintenance of the system", "5"));
            //user group maintenance
            chklMaintenance.Items.Add(new ListItem(" <b>Usergroup Maintenance</b><br> &nbsp;&nbsp;&nbsp;&nbsp; Module for usergroups", "6"));
            //chkRights.Items.Add(new ListItem("User Maintenance", "1"));

            //for the assessment tools
            //assessment maintenance
            chklAssessment.Items.Add(new ListItem(" <b>Assessment Maintenance</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module where all the assessment created can be view", "7"));
            //create assessment
            chklAssessment.Items.Add(new ListItem(" <b>Create Assessment</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for creating assessment", "8"));
            //create questions manually
            chklAssessment.Items.Add(new ListItem(" <b>Create Questions Manually</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for creating questions manually", "9"));
            //question pool maintenance
            chklAssessment.Items.Add(new ListItem(" <b>Question Pool Maintenance</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module where all the question created can be viewed", "10"));
            //topic maintenance
            chklAssessment.Items.Add(new ListItem(" <b>Topic Maintenance</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module where the topic can be created", "11"));
            //assessment type
            chklAssessment.Items.Add(new ListItem(" <b>Type Maintenance</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module where user can create assessment types", "12"));
            //upload questions
            chklAssessment.Items.Add(new ListItem(" <b>Upload Question</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for uploading questions", "13"));

            //for the academic activities
            //assessment evaluation
            chklAcademic.Items.Add(new ListItem(" <b>Assessment Evaluation</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for viewing the assessment evaluation", "18"));
            //make up
            chklAcademic.Items.Add(new ListItem(" <b>Make-Up Exams</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for viewing all assessment", "16"));
            //student grades
            chklAcademic.Items.Add(new ListItem(" <b>Student Grades</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for viewing all the student's grades", "15"));
            //teacher subject
            chklAcademic.Items.Add(new ListItem(" <b>Teacher's Subject</b><br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Module for viewing all subject", "14"));
            
    }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (Request.QueryString["ugid"] == "1")
            {
                sql = "Update [Usergroup] Set AccessRights='-0-1-2-3-4-5-6-7-8-9-10-11-12-13-14-15-16-17-18-19-', LastUpdateDate=getdate(),LastUpdateUser='" + LUser.Username + "' Where UserGroupID=" + Request.QueryString["ugid"];
            }
            else
            {
                string Rights = CreateString();
                sql = "Update Usergroup Set AccessRights='" + Rights + "', LastUpdateDate=getdate(),LastUpdateUser='" + LUser.Username + "' Where UserGroupID=" + Request.QueryString["ugid"];
            }
            if (cls.ExecuteNonQuery(sql) == 1)
            {
                Validator.AlertBack("Access rights of " + lblUsergroup.Text.ToLower() + " has been updated successfully.", "usergroup_maintenance_main.aspx");
            }
            else
            {
                Validator.AlertBack("Access rights has not been updated successfully.","usergroup_maintenance_main.aspx");
            }
        }

        string CreateString()
        {
            string rights="-";
            for (int i = 0; i < chklMaintenance.Items.Count; i++)
            {
                if (chklMaintenance.Items[i].Selected == true)
                {
                    rights += chklMaintenance.Items[i].Value + "-";
                }
            }
            for (int i = 0; i < chklAssessment.Items.Count; i++)
            {
                if (chklAssessment.Items[i].Selected == true)
                {
                    rights += chklAssessment.Items[i].Value + "-";
                }
            }

            for (int i = 0; i < chklAcademic.Items.Count; i++)
            {
                if (chklAcademic.Items[i].Selected == true)
                {
                    rights += chklAcademic.Items[i].Value + "-";
                }
            }

            return rights;
        }

        protected void chkMaintenance_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMaintenance.Checked == true)
            {
                for (int i = 0; i < chklMaintenance.Items.Count; i++)
                {
                    chklMaintenance.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < chklMaintenance.Items.Count; i++)
                {
                    chklMaintenance.Items[i].Selected = false;
                }
            }
        }
        
        protected void chkAssessment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAssessment.Checked == true)
            {
                for (int i = 0; i < chklAssessment.Items.Count; i++)
                {
                    chklAssessment.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < chklAssessment.Items.Count; i++)
                {
                    chklAssessment.Items[i].Selected = false;
                }
            }
        }

        protected void chkAcademic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAcademic.Checked == true)
            {
                for (int i = 0; i < chklAcademic.Items.Count; i++)
                {
                    chklAcademic.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < chklAcademic.Items.Count; i++)
                {
                    chklAcademic.Items[i].Selected = false;
                }
            }
        }
    }
}
