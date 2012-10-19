using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;

namespace PAOnlineAssessment.assessment
{
    public partial class quizcreation_main : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.Assessment> oAssessment;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        SystemProcedures sys = new SystemProcedures();
        LoginUser LUser;
        public string UserGroupID = "";
        List<Constructors.AssessmentView> oAssessmentView;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User Is Logged In
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                if (Validator.CanbeAccess("7", LUser.AccessRights) == false)
                {
                    //Debug.WriteLine("Page cannot be accessed");

                    Validator.AlertBack("Access Denied!", "../block_user.aspx");

                }
            }
            //Redirect to Login Page when no User is logged in
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            if (IsPostBack == false)
            {
                //set the quarter
                ddlQuarter.SelectedValue = Session["Quarter"].ToString();
                if (LUser.UserGroupID == "1")
                {
                    LoadAssessment();
                }
                else if (LUser.UserGroupID == "3")
                {
                    LoadTeacherAssessment();
                }
            }

            //Check if has assessment
            if (Convert.ToInt32(Request.QueryString["aid"]) > 0)
            {
                //Check if the action to be taken is deactivate
                if (Request.QueryString["action"] == "deactivate")
                {
                    //Updating the status of assessment
                    int AffectedRows = sys.DeactivateRecord("Assessment", "AssessmentID", Request.QueryString["aid"], LUser.Username);
                    if (AffectedRows > 0)
                    {
                        Response.Write("<script>alert('Assessment has been deactivated successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Action cannot continue. Please check your entry'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                    }
                }
                //Check if the actionto to be take is activate
                else if (Request.QueryString["action"] == "activate")
                {
                    //Updating the status of assessment
                    int AffectedRows = sys.ActivateRecord("Assessment", "AssessmentID", Request.QueryString["aid"], LUser.Username);
                    if (AffectedRows > 0)
                    {
                        Response.Write("<script>alert('Assessment has been activated successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Action cannot continue. Please check your entry'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                    }
                }
                else if (Request.QueryString["action"] == "open")
                {
                    //check if the assessment is already taken
                    string AssessmentID = cls.ExecuteScalar("SELECT * FROM PaceAssessment.dbo.StudentAnswers where AssessmentID='" + Request.QueryString["aid"] + "'");
                    if (string.IsNullOrEmpty(AssessmentID))
                    {
                        AssessmentID = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.Assessment where AssessmentID='" + Request.QueryString["aid"] + "' and Status='A'");
                        if (string.IsNullOrEmpty(AssessmentID))
                        {
                            Response.Write("<script>alert('Action cannot continue. Please check if the assessment is active.'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                        }
                        else
                        {
                            string qry = "Update PaceAssessment.dbo.Assessment set ScheduleStatus='Open',LastUpdateUser='" + LUser.Username + "',LastUpdateDate=getdate(),DateStart=getdate() where AssessmentID='" + Request.QueryString["aid"] + "'";
                            cls.ExecuteNonQuery(qry);
                            Response.Write("<script>alert('Assessment has been opened successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Action cannot continue. The assessment has already been closed.'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                    }
                }
                else if (Request.QueryString["action"] == "close")
                {
                    string AssessmentID = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.Assessment where AssessmentID='" + Request.QueryString["aid"] + "' and Status='A'");
                    if (string.IsNullOrEmpty(AssessmentID))
                    {
                        Response.Write("<script>alert('Action cannot continue. Please check if the assessment is active.'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                    }
                    else
                    {
                        string qry = "Update PaceAssessment.dbo.Assessment set ScheduleStatus='Close',LastUpdateUser='" + LUser.Username + "',LastUpdateDate=getdate(),DateEnd=getdate() where AssessmentID='" + Request.QueryString["aid"] + "'";
                        cls.ExecuteNonQuery(qry);
                        Response.Write("<script>alert('Assessment has been closed successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_quizcreation_main) + "'</script>");
                    }
                    
                }
            }
        }

        //loading of assessment created by a teacher/user
        void LoadTeacherAssessment()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AssessmentID"));
            dt.Columns.Add(new DataColumn("Title"));
            dt.Columns.Add(new DataColumn("AssessmentType"));
            dt.Columns.Add(new DataColumn("Level"));
            dt.Columns.Add(new DataColumn("Subject"));
            dt.Columns.Add(new DataColumn("ScheduleType"));
            dt.Columns.Add(new DataColumn("Status"));
            dt.Columns.Add(new DataColumn("ScheduleStatus"));

            oAssessmentView = new List<Constructors.AssessmentView>(cls.getAssessmentView());
            int row = 0;

            switch (ddlSearch.SelectedItem.ToString())
            {
                //Loading based on assessment type
                case "Assessment Type":
                    oAssessmentView.ForEach(a =>
                    {
                        if (!string.IsNullOrEmpty(txtSearch.Text))
                        {
                            string filter = cls.ExecuteScalar("Select AssessmentTypeID from PaceAssessment.dbo.AssessmentType where Description like '" + txtSearch.Text + "%'");
                            if (a.AssessmentTypeID.ToString() == filter && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                        else
                        {
                            if (a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                            }
                        }
                    });
                    break;
                //Loading based on level
                case "Level":
                    oAssessmentView.ForEach(a =>
                    {
                        if (!string.IsNullOrEmpty(txtSearch.Text))
                        {
                            string filter = cls.ExecuteScalar("Select distinct LevelID from PaceAssessment.dbo.RegistrationTermView where LevelDescription like '" + txtSearch.Text + "%'");
                            if (a.LevelID.ToString() == filter && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                        else
                        {
                            if (a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                    });
                    break;
                //Loading based on subject
                case "Subject":
                    oAssessmentView.ForEach(a =>
                    {
                        if (!string.IsNullOrEmpty(txtSearch.Text))
                        {
                            string filter = cls.ExecuteScalar("Select distinct SubjectID from PaceAssessment.dbo.RegistrationTermView where SubjectDescription like '" + txtSearch.Text + "%' and LevelID='" + a.LevelID + "'");
                            if (a.SubjectID.ToString() == filter && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                        else
                        {
                            if (a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                    });
                    break;
                //Loading based in title
                case "Title":
                    oAssessmentView.ForEach(a =>
                    {
                        if (a.Title.ToLower().Contains(txtSearch.Text.ToLower()) && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                        {
                            if (a.Schedule == "Yes")
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                            }
                            else
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                            }
                            row++;
                        }
                    });
                    break;
                //Loading based on Status = A
                case "A - Available":
                    oAssessmentView.ForEach(a =>
                    {
                        if (a.Status == "A" && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                        {
                            if (a.Schedule == "Yes")
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                            }
                            else
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                            }
                            row++;
                        }
                    });
                    break;
                //Loading based on Status = D
                case "D - Deactivated":
                    oAssessmentView.ForEach(a =>
                    {
                        if (a.Status == "D" && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.UserID.ToString() == LUser.UserID.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                        {
                            string Description = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType where AssessmentTypeID='" + a.AssessmentTypeID.ToString() + "'");
                            if (a.Schedule == "Yes")
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                            }
                            else
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                            }
                            row++;
                        }
                    });
                    break;
            }

            //Check if has record
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add("0", "No Record Found", "", "", "", "", "");
            }

            Session["Assessment"] = dt;

            dgAssessment.DataSource = Session["Assessment"];
            dgAssessment.DataBind();
        }

        //Loading of assessment
        void LoadAssessment()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AssessmentID"));
            dt.Columns.Add(new DataColumn("Title"));
            dt.Columns.Add(new DataColumn("AssessmentType"));
            dt.Columns.Add(new DataColumn("Level"));
            dt.Columns.Add(new DataColumn("Subject"));
            dt.Columns.Add(new DataColumn("ScheduleType"));
            dt.Columns.Add(new DataColumn("Status"));
            dt.Columns.Add(new DataColumn("ScheduleStatus"));

            oAssessmentView = new List<Constructors.AssessmentView>(cls.getAssessmentView());
            int row = 0;

            switch (ddlSearch.SelectedItem.ToString())
            {
                //Loading based on assessment type
                case "Assessment Type":
                    oAssessmentView.ForEach(a =>
                    {
                        if (!string.IsNullOrEmpty(txtSearch.Text))
                        {
                            string filter = cls.ExecuteScalar("Select AssessmentTypeID from PaceAssessment.dbo.AssessmentType where Description like '" + txtSearch.Text + "%'");

                            if (a.AssessmentTypeID.ToString() == filter && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                        else
                        {
                            if (a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                    });
                    break;
                //Loading based on level
                case "Level":
                    oAssessmentView.ForEach(a =>
                    {
                        if (!string.IsNullOrEmpty(txtSearch.Text))
                        {
                            string filter = cls.ExecuteScalar("Select distinct LevelID from PaceAssessment.dbo.RegistrationTermView where LevelDescription like '" + txtSearch.Text + "%'");

                            if (a.LevelID.ToString() == filter && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                        else
                        {
                            if (a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                    }
                    );
                    break;
                //Loading based on subject
                case "Subject":
                    oAssessmentView.ForEach(a =>
                    {
                        if (!string.IsNullOrEmpty(txtSearch.Text))
                        {
                            string filter = cls.ExecuteScalar("Select distinct SubjectID from PaceAssessment.dbo.RegistrationTermView where SubjectDescription like '" + txtSearch.Text + "%' and LevelID='" + a.LevelID + "'");

                            if (a.SubjectID.ToString() == filter && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                        else
                        {
                            if (a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (a.Schedule == "Yes")
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                                }
                                else
                                {
                                    dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                                }
                                row++;
                            }
                        }
                    });
                    break;
                //Loading based in title
                case "Title":
                    oAssessmentView.ForEach(a =>
                    {
                        if (a.Title.ToLower().Contains(txtSearch.Text.ToLower()) && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                        {
                            if (a.Schedule == "Yes")
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                            }
                            else
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                            }
                            row++;
                        }
                    });
                    break;
                //Loading based on Status = A
                case "A - Available":
                    oAssessmentView.ForEach(a =>
                    {
                        if (a.Status == "A" && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                        {
                            if (a.Schedule == "Yes")
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                            }
                            else
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                            }
                            row++;
                        }
                    });
                    break;
                //Loading based on Status = D
                case "D - Deactivated":
                    oAssessmentView.ForEach(a =>
                    {
                        if (a.Status == "D" && a.Quarter == ddlQuarter.SelectedItem.ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString())
                        {
                            if (a.Schedule == "Yes")
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Manual", a.Status, a.ScheduleStatus);
                            }
                            else
                            {
                                dt.Rows.Add(a.AssessmentID, a.Title, a.AssessmentTypeDescription, a.LevelDescription, a.SubjectDescription, "Open/Close", a.Status, a.ScheduleStatus);
                            }
                            row++;
                        }
                    });
                    break;
            }

            //Check if has record
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add("0", "No Record Found", "", "", "", "", "");
            }

            Session["Assessment"] = dt;

            dgAssessment.DataSource = Session["Assessment"];
            dgAssessment.DataBind();
        }

        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            //Loading of assessment based on entered data
            if (LUser.UserGroupID == "1")
            {
                LoadAssessment();
            }
            else if (LUser.UserGroupID == "3")
            {
                LoadTeacherAssessment();
            }
        }

        protected void dgAssessment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < dgAssessment.Rows.Count; x++)
            {   
                ImageButton imgEdit = (ImageButton)dgAssessment.Rows[x].FindControl("imgEdit");
                ImageButton imgPreview = (ImageButton)dgAssessment.Rows[x].FindControl("imgPreview");
                ImageButton imgDeactivate = (ImageButton)dgAssessment.Rows[x].FindControl("imgDeactivate");
                ImageButton imgCloseOpen = (ImageButton)dgAssessment.Rows[x].FindControl("imgCloseOpen");

                Label lblStatus = (Label)dgAssessment.Rows[x].FindControl("lblStatus");
                Label lblAssessmentID = (Label)dgAssessment.Rows[x].FindControl("lblAssessmentID");
                Label lblScheduleType = (Label)dgAssessment.Rows[x].FindControl("lblScheduleType");
                Label lblScheduleStatus = (Label)dgAssessment.Rows[x].FindControl("lblScheduleStatus");

                imgEdit.ToolTip = "Edit Assessment";
                string EditURL = "";
                if (LUser.UserGroupID == "1")
                    EditURL = ResolveUrl(DefaultForms.frm_assessment_admin_update) + "?qid=" + lblAssessmentID.Text;
                else
                    EditURL = ResolveUrl(DefaultForms.frm_quizcreation_update) + "?qid=" + lblAssessmentID.Text;

                imgEdit.PostBackUrl = EditURL;

                imgPreview.ToolTip = "Preview Assessment";
                string EditURL1 = ResolveUrl(DefaultForms.frm_preview_assessment) + "?assid=" + lblAssessmentID.Text;
                imgPreview.PostBackUrl = EditURL1;

                if (lblStatus.Text == "A")
                {
                    string DeactivateURL = ResolveUrl(DefaultForms.frm_quizcreation_main) + "?action=deactivate&aid=" + lblAssessmentID.Text;
                    imgDeactivate.ImageUrl = "~/images/icons/page_delete.gif";
                    imgDeactivate.ToolTip = "Deactivate Assessment";
                    imgDeactivate.OnClientClick = "if (confirm('Are you sure you want to deactivate this Assessment?')){window.location='" + DeactivateURL + "'} return false;";
                }
                else
                {
                    string DeactivateURL = ResolveUrl(DefaultForms.frm_quizcreation_main) + "?action=activate&aid=" + lblAssessmentID.Text;
                    imgDeactivate.ImageUrl = "~/images/icons/page_tick.gif";
                    imgDeactivate.ToolTip = "Activate Assessment";
                    imgDeactivate.OnClientClick = "if (confirm('Are you sure you want to activate this Assessment?')){window.location='" + DeactivateURL + "'} return false;";
                }

                if (lblScheduleType.Text == "Open/Close")
                {
                    string opencloseUrl = "";
                    if (lblScheduleStatus.Text == "Close")
                    {
                        opencloseUrl = ResolveUrl(DefaultForms.frm_quizcreation_main) + "?action=open&aid=" + lblAssessmentID.Text; ;
                        imgCloseOpen.ImageUrl = "~/images/icons/page_key.gif";
                        imgCloseOpen.ToolTip = "Open Assessment";
                        imgCloseOpen.OnClientClick = "if (confirm('Are you sure you want to open this Assessment?')){window.location='" + opencloseUrl + "'} return false;";
                    }
                    else
                    {
                        opencloseUrl = ResolveUrl(DefaultForms.frm_quizcreation_main) + "?action=close&aid=" + lblAssessmentID.Text; ;
                        imgCloseOpen.ImageUrl = "~/images/icons/page_lock.gif";
                        imgCloseOpen.ToolTip = "Close Assessment";
                        imgCloseOpen.OnClientClick = "if (confirm('Are you sure you want to close this Assessment?')){window.location='" + opencloseUrl + "'} return false;";
                    }
                }
                else
                {
                    imgCloseOpen.ToolTip = "";
                    imgCloseOpen.Enabled = false;
                    imgCloseOpen.ImageUrl = "~/images/icons/page_key_disabled.gif";
                }

                if (lblAssessmentID.Text == "0")
                {
                    imgEdit.ToolTip = "";
                    imgEdit.Enabled = false;
                    imgEdit.ImageUrl = "~/images/icons/page_edit_disabled.gif";

                    imgDeactivate.ToolTip = "";
                    imgDeactivate.Enabled = false;
                    imgDeactivate.ImageUrl = "~/images/icons/page_delete_disabled.gif";

                    imgPreview.ToolTip = "";
                    imgPreview.Enabled = false;
                    imgPreview.ImageUrl = "~/images/icons/page_find_disabled.gif";

                    imgCloseOpen.ToolTip = "";
                    imgCloseOpen.Enabled = false;
                    imgCloseOpen.ImageUrl = "~/images/icons/page_key_disabled.gif";
                }
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                lblPageCount.Text = "of " + dgAssessment.PageCount;
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                cboPageNumber.Items.Clear();
                for (int x = 1; x <= dgAssessment.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = dgAssessment.PageIndex;
            }
        }

        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            dgAssessment.PageIndex = 0;
            if (LUser.UserGroupID == "1")
            {
                LoadAssessment();
            }
            else if (LUser.UserGroupID == "3")
            {
                LoadTeacherAssessment();
            }
        }

        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (dgAssessment.PageIndex != 0)
            {
                dgAssessment.PageIndex = dgAssessment.PageIndex - 1;
                if (LUser.UserGroupID == "1")
                {
                    LoadAssessment();
                }
                else if (LUser.UserGroupID == "3")
                {
                    LoadTeacherAssessment();
                }
            }
        }

        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            dgAssessment.PageIndex = cboPageNumber.SelectedIndex;
            if (LUser.UserGroupID == "1")
            {
                LoadAssessment();
            }
            else if (LUser.UserGroupID == "3")
            {
                LoadTeacherAssessment();
            }
            
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (dgAssessment.PageIndex != dgAssessment.PageCount)
            {
                dgAssessment.PageIndex = dgAssessment.PageIndex + 1;
                if (LUser.UserGroupID == "1")
                {
                    LoadAssessment();
                }
                else if (LUser.UserGroupID == "3")
                {
                    LoadTeacherAssessment();
                }
            }
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {
            dgAssessment.PageIndex = dgAssessment.PageCount;
            if (LUser.UserGroupID == "1")
            {
                LoadAssessment();
            }
            else if (LUser.UserGroupID == "3")
            {
                LoadTeacherAssessment();
            }
        }

        protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LUser.UserGroupID == "1")
            {
                LoadAssessment();
            }
            else
            {
                LoadTeacherAssessment();
            }
        }
    }
}

