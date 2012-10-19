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
    public partial class topic_maintenance_main : System.Web.UI.Page
    {

        Collections cls = new Collections();
        List<Constructors.Topics> TopicList = new List<Constructors.Topics>(new Collections().GetTopic());
        List<Constructors.Levels> LevelList = new List<Constructors.Levels>(new Collections().GetLevels());
        List<Constructors.Subject> SubjectList = new List<Constructors.Subject>(new Collections().getSubjectList());
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

                if (Validator.CanbeAccess("11", LUser.AccessRights) == false)
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
                LoadTopics();
            }

            if (Request.QueryString["change_stat"] == "1")
            {
                SystemProcedures sp = new SystemProcedures();
                if (Request.QueryString["stat"] == "A")
                {
                    if (sp.DeactivateRecord("Topics", "TopicID", Request.QueryString["tid"], "zeEk") == 1)
                    {
                        Validator.AlertBack("Topic has been successfully deactivated", "topic_maintenance_main.aspx");
                    }
                }
                else
                {
                    if (sp.ActivateRecord("Topics", "TopicID", Request.QueryString["tid"], "zeEk") == 1)
                    {
                        Validator.AlertBack("Topic has been successfully activated", "topic_maintenance_main.aspx");
                    }
                }
            }
        }

        void LoadTopics()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TopicID");
            dt.Columns.Add("Topic");
            dt.Columns.Add("Level");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Status");
            TopicList.ForEach(tl =>
            {
                dt.Rows.Add(tl.TopicID, tl.Description, GetLevelDescription(tl.LevelID), GetSubjectDescription(tl.SubjectID), tl.Status);
            });
            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0","No Record Found");
            }

            Session["TopicList"] = dt;
            BindData();
        }

        string GetLevelDescription(int level_id)
        {
            string value = "";
            LevelList.ForEach(l => 
            {
                if (l.LevelID == level_id)
                {
                    value = l.LevelDescription;   
                }
            });

            return value;
        }


        string GetSubjectDescription(int subject_id)
        {
            string value = "";
            SubjectList.ForEach(sl => 
            {
                if (sl.SubjectID == subject_id)
                {
                    value = sl.Description;
                }
            
            });

            return value;
        }

        void BindData()
        {
            gvTopic.DataSource = Session["TopicList"];
            gvTopic.DataBind();
        }


        void SearchTopic(string field)
        {
            DataTable dt = (DataTable)Session["TopicList"];
            dt.Rows.Clear();

            TopicList.ForEach(tl => 
            {
                switch (cboSearchQuery.SelectedValue)
                { 
                    case "Topic":
                        if (tl.Description.ToLower().Contains(field.ToLower()))
                        {
                            dt.Rows.Add(tl.TopicID, tl.Description, GetLevelDescription(tl.LevelID), GetSubjectDescription(tl.SubjectID), tl.Status);
                        }
                        break;
                    case "Subject":
                        SubjectList.ForEach(sl => 
                        {
                            if (sl.Description.ToLower().Contains(field.ToLower()))
                            {
                                if (sl.SubjectID == tl.SubjectID)
                                {
                                    dt.Rows.Add(tl.TopicID, tl.Description, GetLevelDescription(tl.LevelID), GetSubjectDescription(tl.SubjectID), tl.Status);
                                }
                            }
                        });
                        break;

                    case "Level":
                        LevelList.ForEach(l =>
                        {
                            if (l.LevelDescription.ToLower().Contains(field.ToLower()))
                            {
                                if (l.LevelID == tl.LevelID)
                                {
                                    dt.Rows.Add(tl.TopicID, tl.Description, GetLevelDescription(tl.LevelID), GetSubjectDescription(tl.SubjectID), tl.Status);
                                }
                            }
                        });
                        break;
                    case "A":
                        if (tl.Status == "A")
                        {
                            dt.Rows.Add(tl.TopicID, tl.Description, GetLevelDescription(tl.LevelID), GetSubjectDescription(tl.SubjectID), tl.Status);
                            Debug.WriteLine("Checking mobstaz");
                        }
                        break;
                    case "D":
                        if (tl.Status == "D")
                        {
                            dt.Rows.Add(tl.TopicID, tl.Description, GetLevelDescription(tl.LevelID), GetSubjectDescription(tl.SubjectID), tl.Status);
                        }
                        break;                
                }
                
            });
            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "No Record Found");
            }

            Session["TopicList"] = dt;
            BindData();
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            gvTopic.PageIndex = 0;
            BindData();
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (gvTopic.PageIndex != 0)
            {
                gvTopic.PageIndex -= 1;
                BindData();
            }
        }

        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;

            //Debug.WriteLine("Previous Page Index " + gvTopic.PageIndex);
            gvTopic.PageIndex = Convert.ToInt32(cboPageNumber.SelectedValue) - 1;
            BindData();
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (gvTopic.PageIndex != gvTopic.PageCount)
            {
                gvTopic.PageIndex += 1;
                BindData();
            }

        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {
            gvTopic.PageIndex = gvTopic.PageCount;
            BindData();
        }

        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            SearchTopic(txtSearchQuery.Text);
        }

        protected void gvTopic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < gvTopic.Rows.Count; i++)
            {
                Label lblID = (Label)gvTopic.Rows[i].FindControl("lblID");
                Label lblStatus = (Label)gvTopic.Rows[i].FindControl("lblStatus");
                ImageButton btnEdit = (ImageButton)gvTopic.Rows[i].FindControl("btnEdit");
                ImageButton btnStatus = (ImageButton)gvTopic.Rows[i].FindControl("btnStatus");


                
                if (lblID.Text == "0")
                {
                    btnEdit.Enabled = false;
                    btnStatus.Enabled = false;
                    btnEdit.ImageUrl = "~/images/icons/page_edit_disabled.gif";
                    btnStatus.ImageUrl = "~/images/icons/page_delete_disabled.gif";
                }
                else
                {
                    btnEdit.ImageUrl = "~/images/icons/page_edit.gif";
                    btnEdit.Enabled = true;
                    btnStatus.Enabled = true;
                    btnEdit.PostBackUrl = "topic_maintenance_addupdate.aspx?mode=edit&tid=" + lblID.Text;
                    btnEdit.ToolTip = "Edit Topic";
                    btnStatus.Enabled = true;
                    if (lblStatus.Text == "A")
                    {
                        btnStatus.ImageUrl = "~/images/icons/page_delete.gif";
                        btnStatus.ToolTip = "Deactivate Topic";
                        btnStatus.OnClientClick = "if(confirm('Do you want to deactivate this topic?')){window.location='topic_maintenance_main.aspx?change_stat=1&stat=" + lblStatus.Text + "&tid=" + lblID.Text + "';}return false;";
                    }
                    else
                    {
                        btnStatus.ImageUrl = "~/images/icons/page_tick.gif";
                        btnStatus.ToolTip = "Activate Topic";
                        btnStatus.OnClientClick = "if(confirm('Do you want to activate this topic?')){window.location='topic_maintenance_main.aspx?change_stat=1&stat=" + lblStatus.Text + "&tid=" + lblID.Text + "';}return false;";
                    }
                }


                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                    Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                    cboPageNumber.Items.Clear();
                    for (int x = 1; x <= gvTopic.PageCount; x++)
                    {
                        cboPageNumber.Items.Add(x.ToString());
                    }
                    lblPageCount.Text = "of " + gvTopic.PageCount.ToString();
                    cboPageNumber.SelectedIndex = gvTopic.PageIndex;
                }

            }
        }
    }
}
