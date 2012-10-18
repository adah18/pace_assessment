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
    public partial class topic_maintenance_addupdate : System.Web.UI.Page
    {

        Collections cls = new Collections();
        List<Constructors.Topics> TopicList = new List<Constructors.Topics>(new Collections().GetTopic());
        List<Constructors.Levels> LevelList = new List<Constructors.Levels>(new Collections().GetLevels());
        List<Constructors.Subject> SubjectList = new List<Constructors.Subject>(new Collections().getSubjectList());
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        int tid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSiteMapDetails();
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

            tid = Convert.ToInt32(Request.QueryString["tid"]);
            if (!IsPostBack)
            {
                cboSubject.Items.Add(new ListItem("--- Select Subject ---", "0"));
                LoadLevels();
                    

                if (Request.QueryString["mode"] == "edit")
                {
                    LoadLevels();
                    LoadTopicDetails();
                }
            }

      
        }
        public void LoadSiteMapDetails()
        {

            SiteMap1.RootNode = "Dashboard";
            SiteMap1.RootNodeToolTip = "Dashboard";
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_index);
            SiteMap1.ParentNode = "Topic Maintenance";
            SiteMap1.ParentNodeToolTip = "Click to go back to Topic Maintenance";
            SiteMap1.ParentNodeURL = ResolveUrl("topic_maintenance_main.aspx");

            //check if mode is Register or Update
            if (Convert.ToInt32(Request.QueryString["tid"]) > 0)
            {
                SiteMap1.CurrentNode = "Update Topic";
            }
            else
            {
                SiteMap1.CurrentNode = "Add New Topic";
            }
            //lblMode.Text = SiteMap1.CurrentNode;
        }


        void LoadTopicDetails()
        {          
            TopicList.ForEach(tl => 
            {
                if (tl.TopicID == tid)
                {
                    txtDescription.Text = tl.Description;
                    for (int i = 0;i < cboLevel.Items.Count; i++)
                    {
                        if (cboLevel.Items[i].Value == tl.LevelID.ToString())
                        {
                            cboLevel.Items[i].Selected = true;
                        }
                    }
                    LoadSubjects();

                    for (int i = 0; i < cboSubject.Items.Count; i++)
                    {
                        if (cboSubject.Items[i].Value == tl.SubjectID.ToString())
                        {
                            cboSubject.Items[i].Selected = true;
                        }
                    }
                }
            
            });
        }

        void LoadLevels()
        {
            cboLevel.Items.Clear();
            cboLevel.Items.Add(new ListItem("--- Select Level ---", "0"));
            LevelList.ForEach(l => 
            {
                if (l.Status == "A")
                {
                    cboLevel.Items.Add(new ListItem(l.LevelDescription, l.LevelID.ToString()));
                }
            });
        }

        void LoadSubjects()
        {
            cboSubject.Items.Clear();

            cboSubject.Items.Add(new ListItem("--- Select Subject ---", "0"));

            SubjectList.ForEach(sl => 
            {
                if (sl.LevelID.ToString() == cboLevel.SelectedValue)
                { 
                    //if()
                    cboSubject.Items.Add(new ListItem(sl.Description, sl.SubjectID.ToString()));
                }
            });
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CanBeSaved())
            {
                if (Request.QueryString["mode"] == "edit")
                {
                    string sql = "Update [Topics] Set Description='" + txtDescription.Text + "', LevelID=" + cboLevel.SelectedValue + ", SubjectID=" + cboSubject.SelectedValue + ", LastUpdateDate=getdate(), LastUpdateUser='" + "zeEk" + "' Where TopicID=" + tid;
                    Debug.WriteLine(sql);
                    if (cls.ExecuteNonQuery(sql) == 1)
                    {
                        Validator.AlertBack("Topic has been updated successfully.", "topic_maintenance_main.aspx");
                    }
                    else
                    {
                        Validator.AlertBack("Action cannot continue.", "topic_maintenance_main.aspx");
                    }
                }
                else
                {
                    string sql = "Insert into [Topics](LevelID, SubjectID, Description, Status, DateCreated, UserCreated, LastUpdateDate, LastUpdateUser)Values(" + cboLevel.SelectedValue + ", " + cboSubject.SelectedValue + ", '" + txtDescription.Text + "', 'A',getdate(),'zeek',getdate(),'zeek')";
                    if (cls.ExecuteNonQuery(sql) == 1)
                    {
                        Validator.AlertBack("Topic has been saved successfully.", "topic_maintenance_main.aspx");
                    }
                    else
                    {
                        Validator.AlertBack("Action cannot continue.", "topic_maintenance_main.aspx");
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_topic_maintenance_main));
        }

        bool CanBeSaved()
        {
            bool x = true;
            if (Validator.isEmpty(txtDescription.Text))
            {
                lblTopic.Text = "* Description is a required field";
                x = false;
            }
            else if (Validator.isNotValid(txtDescription.Text))
            {
                lblTopic.Text = "* Description contains invalid characters";
                x = false;
            }

            else
            {
                lblTopic.Text = "* ";
            }

            if (cboLevel.SelectedValue == "0")
            {
                lblLevel.Text = "* Level is a required field";
                x = false;
            }
            else
            {
                lblLevel.Text = "* ";
            }

            if (cboSubject.SelectedValue == "0")
            {
                lblSubject.Text = "* Subject is a required field";
                x = false;
            }
            else
            {
                lblSubject.Text = "* ";
            }

            if (Validator.isEmpty(txtDescription.Text) == false && cboSubject.SelectedValue != "0" && cboLevel.SelectedValue != "0")
            {

                if (IsExisting(txtDescription.Text, tid))
                {
                    lblTopic.Text = "* Topic already Exists";
                    x = false;
                }
                else
                {
                    lblTopic.Text = "* ";
                }
            }
            return x;
        }

        bool IsExisting(string field, int field_id)
        {
            bool x = false;
            TopicList.ForEach(tl =>
            {
                if (tl.Description == field && tl.TopicID != field_id)
                {
                    if (cboLevel.SelectedValue == tl.LevelID.ToString() && cboSubject.SelectedValue == tl.SubjectID.ToString())
                    {
                        x = true;
                    }
                }
            });
            return x;
        }

        protected void cboLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubjects();
            //
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = cboLevel.ClientID;
            UpdatePanel2.Triggers.Add(trigger);
        }



    }
}
