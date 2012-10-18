using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;

namespace PAOnlineAssessment.assessment
{
    public partial class quizcreation_addupdate : System.Web.UI.Page
    {
        //Instantiate new collection
        Collections cls = new Collections();
        //Instantiate new list ofregistration term
        List<Constructors.RegistrationTerm> oRegistrationTerm;
        //Instantiate new list of question pool
        List<Constructors.QuestionPool> oQuestionPool;
        //Instantiate new list of subject
        List<Constructors.Subject> oSubject;
        //Instantiate new list of assessment type
        List<Constructors.AssessmentType> oAssessmentType;
        //Instantiate new list
        List<Constructors.GradingView> oSubjectList;
        //Instantiate new topic
        List<Constructors.Topics> oTopic;
        //Get collection of forms
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

                if (Validator.CanbeAccess("8", LUser.AccessRights) == false)
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

            if (IsPostBack == false)
            {
                //Loading of Assessment Type
                LoadAssessment();
                //Loading of Levels
                LoadLevels();
                //Loading of Subjects
                LoadSubjects();
                //Loading/Creating of new Data Table for Question
                LoadQuestion();
                //Loading/Creating of new Data Table for Feedback
                LoadFeedBack();
                //load of topic
                LoadTopic();
                //Loading of Question Bank
                LoadQuestionBank("");

                //Removed the Feedback
                dgFeedback.Visible = false;
                lblFeedback.Visible = false;

                rdoSchedule.SelectedIndex = 0;
                Visibility();
            }

            LoadSiteMap();
            ddlQuarter.SelectedValue = Session["Quarter"].ToString();
        }

        void LoadQuestionBank(string SubjectID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("QuestionID"));
            dt.Columns.Add(new DataColumn("Question"));
            dt.Columns.Add(new DataColumn("Checked"));

            if (string.IsNullOrEmpty(SubjectID))
            {
                dt.Rows.Add("", "No Question Available.", "False");
            }
            else
            {
                oQuestionPool = new List<Constructors.QuestionPool>(cls.getQuestionPool());
                oQuestionPool.ForEach(qp =>
                {
                    if (qp.SubjectID.ToString() == SubjectID && qp.Quarter == ddlQuarter.SelectedItem.ToString() && qp.Status == "A")
                    {
                        dt.Rows.Add(qp.QuestionPoolID, qp.Question ,"False");
                    }
                });
            }

            //Check if has there are questions available
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add("", "No Question Available.", "False");
            }

            Session["LoadQuestionBank"] = dt;
            dgQuestionBank.DataSource = Session["LoadQuestionBank"];
            dgQuestionBank.DataBind();

            foreach (GridViewRow dg in dgQuestionBank.Rows)
            {
                CheckBox chkQuestion = (CheckBox)dg.FindControl("chkQuestion");
                Label lblQuestions = (Label)dg.FindControl("lblQuestions");

                chkQuestion.Enabled = true;
                if (lblQuestions.Text == "No Question Available.")
                {
                    chkQuestion.Enabled = false;
                }
            }
        }

        void LoadTopicQuestion(string SubjectID)
        {
            DataTable dtqp = (DataTable)Session["LoadQuestionBank"];
            DataTable dtq = (DataTable)Session["LoadQuestions"];

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("QuestionID"));
            dt.Columns.Add(new DataColumn("Question"));
            dt.Columns.Add(new DataColumn("Checked"));

            oTopic = new List<Constructors.Topics>(cls.GetTopic());
            oQuestionPool = new List<Constructors.QuestionPool>(cls.getQuestionPool());

            oQuestionPool.ForEach(qp =>
                {
                    if (qp.SubjectID.ToString() == SubjectID && qp.Quarter == ddlQuarter.SelectedItem.ToString() && qp.Status == "A")
                    {
                        if (ddlTopic.SelectedValue == "0")
                        {
                            string Checked = "False";
                            foreach (DataRow drow in dtq.Rows)
                            {
                                if (drow["QuestionID"].ToString() == qp.QuestionPoolID.ToString())
                                {
                                    Checked = "True";
                                }
                            }
                            dt.Rows.Add(qp.QuestionPoolID, qp.Question, Checked);
                        }
                        else
                        {
                            oTopic.ForEach(t =>
                            {
                                if (t.TopicID == qp.TopicID)
                                {
                                    if (t.TopicID.ToString() == ddlTopic.SelectedValue)
                                    {
                                        string Checked = "False";
                                        foreach (DataRow drow in dtq.Rows)
                                        {
                                            if (drow["QuestionID"].ToString() == qp.QuestionPoolID.ToString())
                                            {
                                                Checked = "True";
                                            }
                                        }
                                        dt.Rows.Add(qp.QuestionPoolID, qp.Question, Checked);
                                    }
                                }
                            });
                        }
                    }
                });

            //Check if has there are questions available
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add("", "No Question Available.", "False");
            }

            Session["LoadQuestionBank"] = dt;
            dgQuestionBank.DataSource = Session["LoadQuestionBank"];
            dgQuestionBank.DataBind();

            foreach (GridViewRow dg in dgQuestionBank.Rows)
            {
                CheckBox chkQuestion = (CheckBox)dg.FindControl("chkQuestion");
                Label lblQuestions = (Label)dg.FindControl("lblQuestions");

                chkQuestion.Enabled = true;
                if (lblQuestions.Text == "No Question Available.")
                {
                    chkQuestion.Enabled = false;
                }
            }
        }

        void LoadSiteMap()
        {
            //add a name
            SiteMap1.RootNode = "Dashboard";
            //add a tool tip text
            SiteMap1.RootNodeToolTip = "Dashboard";
            //add a postbackurl
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.ParentNode = "Assessment Maintenance";
            SiteMap1.ParentNodeToolTip = "Click to go back to Assessment Maintenance Main";
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_quizcreation_main + "?uid=" + LUser.UserID);

            //add a text for current node
            SiteMap1.CurrentNode = "Create New Assessment";
        }

        void LoadAssessment()
        {
            //Instantiate new List
            oAssessmentType = new List<Constructors.AssessmentType>(cls.getAssessmentType());
            //Loop through the Assessment type list
            ddlAssessmentType.Items.Clear();
            oAssessmentType.ForEach(a =>
                {
                    if (a.Status == "A")
                    {
                        //Add Description to the drop down list
                        ddlAssessmentType.Items.Add(a.Description);
                        ddlAssessmentType.Items[ddlAssessmentType.Items.Count - 1].Value = a.AssessmentTypeID.ToString();
                    }
                });

            //check if has assessment type
            if (ddlAssessmentType.Items.Count == 0)
            {
                Validator.AlertBack("No assessment type. Please create assessment type first.", ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main));
            }
        }

        //load topic 
        void LoadTopic()
        {
            ddlTopic.Items.Clear();
            ddlTopic.Items.Add("--- Select Topic ---");
            ddlTopic.Items[ddlTopic.Items.Count - 1].Value = "0";

            oTopic = new List<Constructors.Topics>(cls.GetTopic());

            oTopic.ForEach(t =>
                {
                    if (t.Status == "A" && t.LevelID.ToString() == ddlLevel.SelectedValue && t.SubjectID.ToString() == ddlSubject.SelectedValue.ToString())
                    {
                        ddlTopic.Items.Add(t.Description);
                        ddlTopic.Items[ddlTopic.Items.Count - 1].Value = t.TopicID.ToString();
                    }
                });
        }

        void LoadLevels()
        {
            ddlLevel.Items.Clear();
            ddlLevel.Items.Add("--- Select Level ---");
            ddlLevel.Items[ddlLevel.Items.Count - 1].Value = "0";

            //Instantiate new List
            oRegistrationTerm = new List<Constructors.RegistrationTerm>(new Collections().getRegistrationTerm());
            oSubjectList = new List<Constructors.GradingView>(cls.getGradingView());

            //Create a DataTable containing the LevelID and LevelDescription
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LevelID"));
            dt.Columns.Add(new DataColumn("LevelDescription"));

            //Loop through the list of subject handle by the teacher
            oSubjectList.ForEach(sl =>
                {
                    if (sl.TeacherID == LUser.UserID)
                    {
                        //Loop through List of Registration Terms
                        oRegistrationTerm.ForEach(rt =>
                        {
                            if (sl.SchoolYear == rt.SchoolYear && rt.SchoolYear == Session["CurrentSchoolYear"].ToString() && sl.LevelID == rt.LevelID)
                            {
                                //Add LevelID and LevelDescription to the Datatable
                                dt.Rows.Add(rt.LevelID, rt.LevelDescription);
                            }
                        });
                    }
                }
                );
            

            //Loop through Filtered Duplicate values in the DataTable
            foreach (DataRow drow in dt.DefaultView.ToTable(true, new string[] { "LevelID", "LevelDescription" }).Rows)
            {
                //Add to Grade / Level DropDownList
                ddlLevel.Items.Add(drow[1].ToString());
                ddlLevel.Items[ddlLevel.Items.Count - 1].Value = drow[0].ToString();
            }
        }

        void LoadSubjects()
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("--- Select Subject ---");
            ddlSubject.Items[ddlSubject.Items.Count - 1].Value = "0";

            //Instantiate new List
            oSubject = new List<Constructors.Subject>(new Collections().getSubjectList());
            oSubjectList = new List<Constructors.GradingView>(cls.getGradingView());

            //Create a DataTable containing the SubjectID and SubjectDescription
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SubjectID"));
            dt.Columns.Add(new DataColumn("SubjectDescription"));

            //Loop through the list of teacher with their subject
            oSubjectList.ForEach(sl =>
                {
                    if (sl.TeacherID == LUser.UserID)
                    {
                        //Loop through List of Subject
                        oSubject.ForEach(s =>
                        {
                            if (s.LevelID == Convert.ToInt32(ddlLevel.SelectedValue) && sl.SubjectID == s.SubjectID)
                            {
                                //Add SubjectID and SubjectDescription to the Datatable
                                dt.Rows.Add(s.SubjectID, s.Description);
                            }
                        });
                    }
                });

            //Loop through Filtered Duplicate values in the DataTable
            foreach (DataRow dRow in dt.DefaultView.ToTable(true, new string[] { "SubjectID", "SubjectDescription" }).Rows)
            {
                ddlSubject.Items.Add(dRow[1].ToString());
                ddlSubject.Items[ddlSubject.Items.Count - 1].Value = dRow[0].ToString();
            }
        }

        //Loading of Subject based on level
        protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuestionBank("");
            LoadSubjects();
            LoadTopic();
            LoadQuestion();
        }

        //Loading of Questions based on Level and Subject
        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SubjectID = cls.ExecuteScalar("Select s.SubjectID from PaceRegistration.dbo.Subject s inner join PaceRegistration.dbo.Level l on (s.LevelID=l.LevelID) where s.Description='" + ddlSubject.SelectedItem.ToString() + "' and l.Description='" + ddlLevel.SelectedItem.ToString() + "'");
            LoadQuestionBank(SubjectID);
            LoadQuestion();
            LoadTopic();
        }

        //Check the selected question if already been selected
        private bool CheckQuestions(string Question)
        {
            bool sameQuestion = false;
            DataTable dt = (DataTable)Session["LoadQuestions"];
            foreach (DataRow dRow in dt.Rows)
            {
                if (dRow["Questions"].ToString().ToLower() == Question.ToLower())
                {
                    sameQuestion = true;
                    break;
                }
            }
            return sameQuestion;
        }

        //Bind the data table for questions
        void BindDataQuestions()
        {
            dgQuestions.DataSource = (DataTable)Session["LoadQuestions"];
            dgQuestions.DataBind();
        }

        //Loading/Creating of new data table questions
        void LoadQuestion()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("QuestionID"));
            dt.Columns.Add(new DataColumn("Questions"));
            dt.Columns.Add(new DataColumn("Points"));

            dt.Rows.Add("0", "No Questions Selected.", "0");

            Session["LoadQuestions"] = dt;
            dgQuestions.DataSource = Session["LoadQuestions"];
            dgQuestions.DataBind();
        }

        //Executed when the row has been updated
        protected void dgQuestions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow grdRow = dgQuestions.Rows[e.RowIndex];
            TextBox txtPoints = (TextBox)grdRow.FindControl("txtPoints");

            DataTable dt = (DataTable)Session["LoadQuestions"];
            dt.Rows[e.RowIndex][2] = txtPoints.Text;
            dt.AcceptChanges();

            dgQuestions.EditIndex = -1;
            BindDataQuestions();
        }

        //Executed when the row has been edited
        protected void dgQuestions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgQuestions.EditIndex = e.NewEditIndex;
            BindDataQuestions();
        }

        void QuestionButtons()
        {
            if (dgQuestions.Rows.Count == 1)
            {
                ImageButton imgDelete = (ImageButton)dgQuestions.Rows[0].FindControl("imgDelete");
                ImageButton imgEdit = (ImageButton)dgQuestions.Rows[0].FindControl("imgEdit");
                Label lblQuestions = (Label)dgQuestions.Rows[0].FindControl("lblQuestions");
                
                if (lblQuestions.Text == "No Questions Selected.")
                {
                    imgEdit.ToolTip = "";
                    imgEdit.Enabled = false;
                    imgEdit.ImageUrl = "~/images/icons/page_edit_disabled.gif";
                    imgDelete.ToolTip = "";
                    imgDelete.Enabled = false;
                    imgDelete.ImageUrl = "~/images/icons/page_delete_disabled.gif";
                }
            }
        }

        //Executed when the row has been data bound
        protected void dgQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            QuestionButtons();
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                lblPageCount.Text = "of " + dgQuestions.PageCount;
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                for (int x = 1; x <= dgQuestions.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = dgQuestions.PageIndex;
            }
        }

        //Executed when the row has been cancelled
        protected void dgQuestions_RowcCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgQuestions.EditIndex = -1;
            BindDataQuestions();
        }

        //Executed when the row has deleted
        protected void dgQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //load the data table questions and question bank
            DataTable dt = (DataTable)Session["LoadQuestions"];
            DataTable dtqb = (DataTable)Session["LoadQuestionBank"];

            GridViewRow grdRow = dgQuestions.Rows[e.RowIndex];
            Label lblQuestionID = (Label)grdRow.FindControl("lblQuestionID");

            foreach (DataRow dqb in dtqb.Rows)
            {
                if (dqb["QuestionID"].ToString() == lblQuestionID.Text)
                {
                    dqb["Checked"] = "False";
                    dtqb.AcceptChanges();
                }
            }

            Session["LoadQuestionBank"] = dtqb;

            foreach (DataRow dq in dt.Rows)
            {
                if (lblQuestionID.Text == dq["QuestionID"].ToString())
                {
                    dq.Delete();
                    dt.AcceptChanges();
                    break;
                }
            }

            Session["LoadQuestions"] = dt;

            foreach (GridViewRow dg in dgQuestionBank.Rows)
            {
                Label lblqid = (Label)dg.FindControl("lblQuestionID");
                CheckBox chkQuestion = (CheckBox)dg.FindControl("chkQuestion");
                if (lblqid.Text == lblQuestionID.Text)
                {
                    chkQuestion.Checked = false;
                    chkQuestion.Enabled = true;
                }
            }

            if (dt.Rows.Count == 0)
            {
                LoadQuestion();
            }

            dgQuestions.EditIndex = -1;
            BindDataQuestions();

            checkboxEnabledDisabled();
        }

        void test()
        {
            //load question and load question bank
            DataTable dtq = (DataTable)Session["LoadQuestions"];
            DataTable dtqb = (DataTable)Session["LoadQuestionBank"];

            //show the checked and unchecked checkboxes , diabled and enabled checkboxes
            foreach (DataRow dqrow in dtq.Rows)
            {
                foreach (DataRow dqbrow in dtqb.Rows)
                {
                    if (dqrow["QuestionID"].ToString() == dqbrow["QuestionID"].ToString())
                    {
                        dqbrow["Checked"] = "True";
                        dtqb.AcceptChanges();
                        foreach (GridViewRow dgrow in dgQuestionBank.Rows)
                        {
                            Label lblQuestionID = (Label)dgrow.FindControl("lblQuestionID");
                            CheckBox chkQuestion = (CheckBox)dgrow.FindControl("chkQuestion");
                            if (lblQuestionID.Text == dqbrow["QuestionID"].ToString())
                            {
                                if (dqbrow["Checked"].ToString() == "True")
                                {
                                    chkQuestion.Checked = true;
                                    chkQuestion.Enabled = false;
                                }
                                else
                                {
                                    chkQuestion.Checked = false;
                                    chkQuestion.Enabled = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        dqbrow["Checked"] = "False";
                        dtqb.AcceptChanges();
                        foreach (GridViewRow dgrow in dgQuestionBank.Rows)
                        {
                            Label lblQuestionID = (Label)dgrow.FindControl("lblQuestionID");
                            CheckBox chkQuestion = (CheckBox)dgrow.FindControl("chkQuestion");
                            if (lblQuestionID.Text == dqbrow["QuestionID"].ToString())
                            {
                                if (dqbrow["Checked"].ToString() == "True")
                                {
                                    chkQuestion.Checked = true;
                                    chkQuestion.Enabled = false;
                                }
                                else
                                {
                                    chkQuestion.Checked = false;
                                    chkQuestion.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }

            Session["LoadQuestionBank"] = dtqb;
        }

        //Executed when the previous button has been clicked
        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            if (dgQuestions.PageIndex != 0)
            {
                dgQuestions.PageIndex = dgQuestions.PageIndex - 1;
                BindDataQuestions();
            }
        }

        //Executed when the first button page has been clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            dgQuestions.PageIndex = 0;
            BindDataQuestions();
        }

        //Executed when the next button page has been clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (dgQuestions.PageIndex != dgQuestions.PageCount)
            {
                dgQuestions.PageIndex = dgQuestions.PageIndex + 1;
                BindDataQuestions();
            }
        }

        //Executed when the last button has been clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            dgQuestions.PageIndex = dgQuestions.PageCount;
            BindDataQuestions();
        }

        //Executed when the page number has been changed
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            dgQuestions.PageIndex = cboPageNumber.SelectedIndex;
            BindDataQuestions();
        }

        //Load/Creating a new data table feedback
        void LoadFeedBack()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GradeBoundary"));
            dt.Columns.Add(new DataColumn("Feedback"));

            dt.Rows.Add("0%","Poor.");
            
            Session["LoadFeedback"] = dt;
            dgFeedback.DataSource = Session["LoadFeedback"];
            dgFeedback.DataBind();
            FeedbackDelete();
        }

        //Bind the data table for feedback
        void BindDataFeedback()
        {
            dgFeedback.DataSource = (DataTable)Session["LoadFeedback"];
            dgFeedback.DataBind();
        }

        protected void dgFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        void FeedbackDelete()
        {
            if (dgFeedback.Rows.Count == 1)
            {
                ImageButton imgDelete = (ImageButton)dgFeedback.Rows[0].FindControl("imgDelete");
                imgDelete.ToolTip = "";
                imgDelete.Enabled = false;
                imgDelete.ImageUrl = "~/images/icons/page_delete_disabled.gif";
            }
        }

        //Executed when the rows has been deleted
        protected void dgFeedback_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["LoadFeedback"];
            dt.Rows[e.RowIndex].Delete();
            dt.AcceptChanges();

            dgFeedback.EditIndex = -1;
            BindDataFeedback();
            FeedbackDelete();
        }

        //Executed when the rows has been edited
        protected void dgFeedback_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgFeedback.EditIndex = e.NewEditIndex;
            BindDataFeedback();
        }

        //Executed when the rows has been updated
        protected void dgFeedback_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblFeedback.Text = "*";
            GridViewRow grdRow = dgFeedback.Rows[e.RowIndex];
            TextBox txtGradeBoundary = (TextBox)grdRow.FindControl("txtGradeBoundary");
            TextBox txtFeedback = (TextBox)grdRow.FindControl("txtFeedback");

            DataTable dt = (DataTable)Session["LoadFeedback"];

            //Validation for grade boundary
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                if (CountCharacterInString("-", txtGradeBoundary.Text) <= 1)
                {
                    //Check if the grade boundary entered has range "-"
                    if (txtGradeBoundary.Text.Contains("-"))
                    {
                        //Get the value of grade boundary
                        string[] num = txtGradeBoundary.Text.ToString().Split(new char[] { '-' });
                        int num1, num2;
                        bool isnum1 = int.TryParse(num[0].ToString().Replace("%", ""), out num1);
                        bool isnum2 = int.TryParse(num[1].ToString().Replace("%", ""), out num2);
                        //Check if the grade baoundary entered is valid
                        if (isnum1 && isnum2)
                        {
                            //Check if the grade boundary entered is correct
                            if (num1 > num2)
                            {
                                lblFeedback.Text = "* Please review your work. Grade Boundary may contain percent or number from 0-100";
                                return;
                            }
                            else
                            {
                                //Check if the value to be compared is range "-"
                                if (dt.Rows[row][0].ToString().Contains("-"))
                                {
                                    string[] bound = dt.Rows[row][0].ToString().Split(new char[] { '-' });
                                    int bound1 = int.Parse(bound[0].ToString().Replace("%", ""));
                                    int bound2 = int.Parse(bound[1].ToString().Replace("%", ""));
                                    //Check if the value being compared and compared to is the same
                                    if (num1 == bound1 && num2 == bound2 && e.RowIndex == row)
                                    {
                                        lblFeedback.Text = "*";
                                        break;
                                    }
                                    else if (num1 == bound1 && num2 == bound2 && e.RowIndex != row)
                                    {
                                        //Check if grade boundary exist
                                        lblFeedback.Text = "* Duplicate grade boundary.";
                                        return;
                                    }
                                    else
                                    {
                                        //Check if the grade boundary entered is out of range
                                        if (bound1 < num1 && bound1 < num2 && bound2 < num1 && bound2 < num2)
                                        {
                                            //True
                                        }
                                        else if (num1 < bound1 && num1 < bound2 && bound2 > num1 && bound2 < num2)
                                        {
                                            //True
                                        }
                                        else
                                        {
                                            lblFeedback.Text = "* Please review your work. Please arrange it from Lowest to Highest.";
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[row][0].ToString().Replace("%", "")))
                                    {
                                        int numm = int.Parse(dt.Rows[row][0].ToString().Replace("%", ""));
                                        //lblFeedback.Text = num1.ToString() + " " + num2.ToString();
                                        if (num1 <= numm && num2 <= numm)
                                        {
                                            lblFeedback.Text = "* Please review your work. Please arrange it from Lowest to Highest.";
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        //Not valid grade boundary
                        else
                        {
                            lblFeedback.Text = "* Please review your work. Grade Boundary may contain percent or number 0-100";
                            return; ;
                        }
                    }
                    //Grade boundary entered is not in range
                    else
                    {
                        int num1;
                        string num = txtGradeBoundary.Text.ToString().Replace("%", "");
                        bool isnum1 = int.TryParse(num, out num1);
                        //Check if the grade boundary entered is valid
                        if (!isnum1)
                        {
                            lblFeedback.Text = "* Please review your work. Grade Boundary may contain percent or number 0-100";
                            return; ;
                        }
                        else
                        {
                            //Check if the boundary being compared is not in range "-"
                            if (!dt.Rows[row][0].ToString().Contains("-"))
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[row][0].ToString().Replace("%", "")))
                                {
                                    int numm = int.Parse(dt.Rows[row][0].ToString().Replace("%", ""));
                                    //Check if grade boundary already exist
                                    if (txtGradeBoundary.Text.Replace("%", "") == dt.Rows[row][0].ToString().Replace("%", "") && e.RowIndex != row)
                                    {
                                        lblFeedback.Text = "* Duplicate grade boundary";
                                        return;
                                    }
                                    else if (Convert.ToInt32(txtGradeBoundary.Text.Replace("%", "")) < numm)
                                    {
                                        lblFeedback.Text = "* Please review your work. Please arrange it from Lowest to Highest.";
                                        return;
                                    }
                                }
                            }
                            //Grade boundary in range "-"
                            else
                            {
                                string[] bound = dt.Rows[row][0].ToString().Split(new char[] { '-' });
                                int bound1 = int.Parse(bound[0].ToString().Replace("%", ""));
                                int bound2 = int.Parse(bound[1].ToString().Replace("%", ""));
                                //Check if the value entered is existing

                                if (txtGradeBoundary.Text.Replace("%", "") == bound1.ToString() || txtGradeBoundary.Text.Replace("%", "") == bound2.ToString())
                                {
                                    lblFeedback.Text = "* Please review your work. Grade boundary was already in used.";
                                    return;
                                }
                                else if (Convert.ToInt32(txtGradeBoundary.Text.Replace("%", "")) >= bound1 && Convert.ToInt32(txtGradeBoundary.Text.Replace("%", "")) <= bound2)
                                {
                                    lblFeedback.Text = "* Grade Boundary was already in used.";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblFeedback.Text = "* Please review your work. Grade Boundary may contain percent or number 0-100";
                    return;
                }
            }

            dt.Rows[e.RowIndex][0] = txtGradeBoundary.Text;
            dt.Rows[e.RowIndex][1] = txtFeedback.Text;
            dt.AcceptChanges();

            dgFeedback.EditIndex = -1;
            BindDataFeedback();
            FeedbackDelete();

        }

        //Executed when the rows has been cancelled
        protected void dgFeedback_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgFeedback.EditIndex = -1;
            BindDataFeedback();
            FeedbackDelete();
        }

        //Executed when the adding a new row
        protected void dgFeedback_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                DataTable dt = (DataTable)Session["LoadFeedback"];
                dt.Rows.Add();
                Session["LoadFeedback"] = dt;
                dgFeedback.DataSource = Session["LoadFeedback"];
                dgFeedback.DataBind();
                dgFeedback.EditIndex = dt.Rows.Count;
                BindDataFeedback();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Check if the required fields are met
            if (RequiredFieldValidator() == false)
            {
                string AssessmentTypeID = cls.ExecuteScalar("select [AssessmentTypeID] from PaceAssessment.dbo.AssessmentType where Description='" + ddlAssessmentType.SelectedItem.ToString() + "'");
                string AssessmentID = cls.ExecuteScalar("select Count([AssessmentID]) from PaceAssessment.dbo.Assessment where Title='" + txtTitle.Text + "' and AssessmentTypeID='" + AssessmentTypeID + "'");
                //Check if the title already exist based on Title and AssessmentType
                if (CheckTitle(AssessmentID) == false)
                {
                    string qry = "";
                    //Inserting new assessment to the database
                    qry = "Insert into PaceAssessment.dbo.Assessment (AssessmentTypeID,LevelID,SubjectID,Title,Introduction,DateStart,DateEnd,TimeStart,TimeEnd,Status,UserCreated,DateCreated,LastUpdateUser,LastUpdateDate,UserID,MakeUp,NoMakeUp) values ('" +
                         AssessmentTypeID + "','" + Validator.Finalize(ddlLevel.SelectedValue) + "','" + Validator.Finalize(ddlSubject.SelectedValue) + "','" + Validator.Finalize(txtTitle.Text) + "','" +
                         Validator.Finalize(txtIntroduction.Text) + "','" + Convert.ToDateTime(txtStartDate.Text).ToShortDateString() + "','" + Convert.ToDateTime(txtEndDate.Text).ToShortDateString() + "','" + Convert.ToDateTime(ddlStartHour.SelectedItem.ToString() + ":" + ddlStartMin.SelectedItem.ToString() + " " + ddlStartAMPM.SelectedItem.ToString()) +
                         "','" + Convert.ToDateTime(ddlEndHour.SelectedItem.ToString() + ":" + ddlEndMin.SelectedItem.ToString() + " " +  ddlEndAMPM.SelectedItem.ToString()) + "','A','"+LUser.Username+"',GETDATE(),'" + LUser.Username + "',GETDATE(),'"+ LUser.UserID + "','-','-')";
                    cls.ExecuteNonQuery(qry);

                    AssessmentID = cls.ExecuteScalar("select [AssessmentID] from PaceAssessment.dbo.Assessment where Title='" + txtTitle.Text + "' and AssessmentTypeID='" + AssessmentTypeID + "'");
                    DataTable dt = (DataTable)Session["LoadQuestions"];
                    foreach (DataRow dRow in dt.Rows)
                    {
                        //Inserting new assessments question to the database
                        string QuestionPoolID = cls.ExecuteScalar("Select QuestionPoolID from PaceAssessment.dbo.QuestionPool where SubjectID='" + ddlSubject.SelectedValue + "' and LevelID='" + ddlLevel.SelectedValue + "' and Question='" + dRow["Questions"].ToString() + "'");
                        qry = "Insert into PaceAssessment.dbo.AssessmentDetails (AssessmentID,QuestionPoolID,Points) values ('" + Validator.Finalize( AssessmentID) + "','" + Validator.Finalize( QuestionPoolID) + "','" + Validator.Finalize(dRow["Points"].ToString()) + "')";
                        cls.ExecuteNonQuery(qry);
                    }

                    dt = (DataTable)Session["LoadFeedback"];
                    foreach (DataRow dRow in dt.Rows)
                    {
                        //inserting new assessment feedback to the database
                        qry = "Insert into PaceAssessment.dbo.AssessmentFeedback (AssessmentID,GradeBoundary,Feedback) values ('" + Validator.Finalize(AssessmentID) + "','" + Validator.Finalize(dRow["GradeBoundary"].ToString()) + "','" + Validator.Finalize(dRow["Feedback"].ToString()) + "')";
                        cls.ExecuteNonQuery(qry);
                    }
                    Response.Write("<script>alert('Assessment have been saved successfully.');</script>");
                }
            }
        }

        //Set the time for saving in the database
        private string getTime(string Hour,string Min,string AMPPM)
        {
            string Time = "";
            if (AMPPM == "PM")
            {
                Hour = (Convert.ToInt32(Hour) + 12).ToString();
                Time = Hour + ":" + Min;
            }
            else
            {
                Time = Hour + ":" + Min;
            }
            return Time;
        }

        //Check the title function in the database
        private bool CheckTitle(string AssessmentID)
        {
            if (AssessmentID == "0")
                return false;
            else
            {
                lblQuiz.Text = "*" + ddlAssessmentType.SelectedItem.ToString() + " Title already exist.";
                return true;
            }
        }

        //Check function for required fields
        private bool RequiredFieldValidator()
        {
            //error handler
            bool haserror;
            haserror = false;

            //Check if the Title is not empty
            if (Validator.isEmpty(txtTitle.Text))
            {
                lblQuiz.Text = "* Required Field.";
                haserror = true;
            }
            else
                lblQuiz.Text = "*";

            //Check if the Introduction is not empty
            if (Validator.isEmpty(txtIntroduction.Text))
            {
                lblIntroduction.Text = "* Required Field.";
                haserror = true;
            }
            else
                lblIntroduction.Text = "*";

            //Check if open/close type of schedule
            if (rdoSchedule.SelectedIndex == 0)
            {
                lblSchedule.Text = "*";
            }
                //Check if manual type of schedule
            else if (rdoSchedule.SelectedIndex == 1)
            {
                //Check if has date
                if (Validator.isEmpty(txtStartDate.Text) || Validator.isEmpty(txtEndDate.Text))
                {
                    lblSchedule.Text = "* Date is a Required Field.";
                    haserror = true;
                }
                else
                {
                    DateTime start;
                    DateTime end;
                    if (DateTime.TryParse(txtStartDate.Text, out start) && DateTime.TryParse(txtEndDate.Text, out end))
                    {
                        if (start > end)
                        {
                            lblSchedule.Text = "* Please enter a valid value.";
                            haserror = true;
                        }
                        else
                        {
                            //Check if has same date
                            if (start == end)
                            {
                                //Same Date
                                //Check if entered time is valid or correct
                                if (Convert.ToDateTime(ddlStartHour.SelectedItem.ToString() + ":" + ddlStartMin.SelectedItem.ToString() + " " + ddlStartAMPM.SelectedItem.ToString()) > Convert.ToDateTime(ddlEndHour.SelectedItem.ToString() + ":" + ddlEndMin.SelectedItem.ToString() + " " + ddlEndAMPM.SelectedItem.ToString()))
                                {
                                    lblSchedule.Text = "* Time TO must be higher than Time FROM.";
                                    haserror = true;
                                }
                                else
                                    lblSchedule.Text = "*";
                            }
                        }
                    }
                    else
                    {
                        lblSchedule.Text = "* Please enter a valid date.";
                        haserror = true;
                    }
                }
            }

            //Check if the default value is selected
            if (Validator.isDefaultSelected(ddlLevel.SelectedValue))
            {
                lblLevel.Text = "* Please Specify Grade / Level";
                haserror = true;
                //return haserror;
            }
            else
                lblLevel.Text = "*";

            //Check if the default value is selected
            if (Validator.isDefaultSelected(ddlSubject.SelectedValue))
            {
                lblSubject.Text = "* Please Specify Subject";
                haserror = true;
                //return haserror;
            }
            else
                lblSubject.Text = "*";

            //Check if has selected Questions
            DataTable dt = (DataTable)Session["LoadQuestions"];
            if (dt.Rows.Count == 1)
            {
                lblQuestion.Text = "* Please add Questions.";
                haserror = true;
            }
            else
            {
                lblQuestion.Text = "*";
                foreach(DataRow drow in dt.Rows)
                {
                    if (drow["QuestionID"].ToString() == "0")
                    {
                        lblQuestion.Text = "* Please add Questions.";
                        haserror = true;
                    }
                }
            }

            //Check if has a feedback
            dt = (DataTable)Session["LoadFeedback"];
            if (dt.Rows.Count == 0)
            {
                lblFeedback.Text = "* Please add Feedback.";
                haserror = true;
            }
            else
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    //Check if the has empty rows
                    if (Validator.isEmpty(dRow["Feedback"].ToString()) || Validator.isEmpty(dRow["GradeBoundary"].ToString()))
                    {
                        lblFeedback.Text = "* Please review your work. A row contains an empty value.";
                        haserror = true;
                        break;
                    }
                    else
                    {
                        //Count if the entered character is only one
                        if (CountCharacterInString("-", dRow["GradeBoundary"].ToString()) <= 1)
                        {
                            //Check if has boudary separator "-"
                            if (dRow["GradeBoundary"].ToString().Contains("-"))
                            {
                                string[] num = dRow["GradeBoundary"].ToString().Split(new char[] { '-' });
                                int num1, num2;
                                bool isnum1 = int.TryParse(num[0].ToString().Replace("%",""), out num1);
                                bool isnum2 = int.TryParse(num[1].ToString().Replace("%", ""), out num2);
                                if (isnum1 && isnum2)
                                {
                                    //Check if the first value is less than the second values
                                    if (num1 > num2)
                                    {
                                        lblFeedback.Text = "* Please review your work. Grade Boundary may contain percent or number 0-100";
                                        haserror = true;
                                        break;
                                    }
                                    else
                                        lblFeedback.Text = "*";
                                }
                                else
                                {
                                    lblFeedback.Text = "* Please review your work. Grade Boundary may contain percent or number 0-100";
                                    haserror = true;
                                    break;
                                }
                            }
                            else
                            {
                                int num1;
                                string num = dRow["GradeBoundary"].ToString().Replace("%", "");
                                bool isnum1 = int.TryParse(num, out num1);
                                //Check if the valued entered is a number
                                if (!isnum1)
                                {
                                    lblFeedback.Text = "* Please review your work. Grade Boundary may contain percent or number 0-100";
                                    haserror = true;
                                    break;
                                }
                                else
                                    lblFeedback.Text = "*";
                            }
                        }
                        else
                        {
                            lblFeedback.Text = "* Please review your work. Grade Boundary may contain percent or number 0-100";
                            haserror = true;
                            break;
                        }
                    }
                }
            }
            
            return haserror;
        }

        //Count the appearance of the character 
        private static int CountCharacterInString(string CharacterToFind, string Expression)
        {
            int ExpressionCount = Expression.Length;
            string Temporary = Expression.Replace(CharacterToFind, "");
            int TemporaryCount = Temporary.Length;
            if (Expression.Length - Temporary.Length == 0)
            {
                return 0;
            }
            else
            {
                return Expression.Length - Temporary.Length;
            }
        }

        //Adding the selected question to the data table
        protected void lnkSave0_Click(object sender, EventArgs e)
        {
            BindDataQuestionBank();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("QuestionID"));
            dt.Columns.Add(new DataColumn("Questions"));
            dt.Columns.Add(new DataColumn("Points"));

            lblQuestion.Text = "*";

            DataTable dtBank = (DataTable)Session["LoadQuestionBank"];
            DataTable dtq = (DataTable)Session["LoadQuestions"];

            //adding the questions in the data table
            foreach (DataRow drow in dtBank.Rows)
            {
                if (drow["Checked"].ToString() == "True")
                {
                    dt.Rows.Add(drow["QuestionID"], drow["Question"], "1");
                }
            }

            //modifying the questions points.
            foreach (DataRow drow in dtq.Rows)
            {
                foreach (DataRow dq in dt.Rows)
                {
                    if (drow["QuestionID"].ToString() == dq["QuestionID"].ToString())
                    {
                        dq["Points"] = drow["Points"].ToString();
                        dt.AcceptChanges();
                    }
                }
            }

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                foreach (DataRow dr in dtq.Rows)
                {
                    if (dt.Rows[row][0].ToString() == dr["QuestionID"].ToString())
                    {
                        dt.Rows.Remove(dt.Rows[row]);
                    }
                }
            }

            //add the previous data
            foreach (DataRow dr in dtq.Rows)
            {
                dt.Rows.Add(dr["QuestionID"].ToString(), dr["Questions"].ToString(), dr["Points"].ToString());
            }

            //check
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                if (dt.Rows[row][1].ToString() == "No Questions Selected.")
                {
                    dt.Rows.Remove(dt.Rows[row]);
                }
            }

            if (dt.Rows.Count > 0)
            {

                Session["LoadQuestions"] = dt;
                dgQuestions.DataSource = Session["LoadQuestions"];
                dgQuestions.DataBind();

                checkboxEnabledDisabled();
            }
            else
            {
                LoadQuestion();
                string SubjectID = cls.ExecuteScalar("Select s.SubjectID from PaceRegistration.dbo.Subject s inner join PaceRegistration.dbo.Level l on (s.LevelID=l.LevelID) where s.Description='" + ddlSubject.SelectedItem.ToString() + "' and l.Description='" + ddlLevel.SelectedItem.ToString() + "'");
                LoadTopicQuestion(SubjectID);
                checkboxEnabledDisabled();
                lblQuestion.Text = "* No question selected.";
            }

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            //Check if the required fields are met
            if (RequiredFieldValidator() == false)
            {
                string AssessmentID = cls.ExecuteScalar("select Count([AssessmentID]) from PaceAssessment.dbo.Assessment where Title='" + txtTitle.Text + "' and AssessmentTypeID='" + ddlAssessmentType.SelectedValue + "' and SubjectID='" + ddlSubject.SelectedValue + "' and LevelID='" + ddlLevel.SelectedValue + "'");
                
                //Check if the title already exist based on Title and AssessmentType
                if (CheckTitle(AssessmentID) == false)
                {
                    string qry = "";
                    //Inserting new assessment to the database
                    //check if what type of schedule
                    if (rdoSchedule.SelectedIndex == 0)
                    {
                        //query for no schedule
                        qry = "Insert into PaceAssessment.dbo.Assessment (UserID,AssessmentTypeID,LevelID,SubjectID,Title,Introduction,Schedule,ScheduleStatus,DateStart,DateEnd,TimeStart,TimeEnd,Quarter,RandomQuestion,RandomAnswer,Status,UserCreated,DateCreated,LastUpdateUser,LastUpdateDate,MakeUp,NoMakeUp,SchoolYear) values ('" +
                         LUser.UserID + "','" + Validator.Finalize(ddlAssessmentType.SelectedValue) + "','" + Validator.Finalize(ddlLevel.SelectedValue) + "','" + Validator.Finalize(ddlSubject.SelectedValue) + "','" + Validator.Finalize(txtTitle.Text) + "','" +
                         Validator.Finalize(txtIntroduction.Text) + "','No','Close',getdate(),getdate(),'','','" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "','" + Validator.Finalize(rbQuestion.Checked.ToString()) + "','" + Validator.Finalize(rbAnswer.Checked.ToString()) + "','A','" + LUser.Username + "','" + DateTime.Now.ToShortDateString() + "','" + LUser.Username + "','" + DateTime.Now.ToString() + "','-','-','" + Session["CurrentSchoolYear"] + "')";
                    }
                    else
                    {
                        //query with schedule
                        qry = "Insert into PaceAssessment.dbo.Assessment (UserID,AssessmentTypeID,LevelID,SubjectID,Title,Introduction,Schedule,ScheduleStatus,DateStart,DateEnd,TimeStart,TimeEnd,Quarter,RandomQuestion,RandomAnswer,Status,UserCreated,DateCreated,LastUpdateUser,LastUpdateDate,MakeUp,NoMakeUp,SchoolYear) values ('" +
                         LUser.UserID + "','" + Validator.Finalize(ddlAssessmentType.SelectedValue) + "','" + Validator.Finalize(ddlLevel.SelectedValue) + "','" + Validator.Finalize(ddlSubject.SelectedValue) + "','" + Validator.Finalize(txtTitle.Text) + "','" +
                         Validator.Finalize(txtIntroduction.Text) + "','Yes','Close','" + Convert.ToDateTime(txtStartDate.Text).ToShortDateString() + "','" + Convert.ToDateTime(txtEndDate.Text).ToShortDateString() + "','" + Convert.ToDateTime(ddlStartHour.SelectedItem.ToString() + ":" + ddlStartMin.SelectedItem.ToString() + " " + ddlStartAMPM.SelectedItem.ToString()) +
                         "','" + Convert.ToDateTime(ddlEndHour.SelectedItem.ToString() + ":" + ddlEndMin.SelectedItem.ToString() + " " + ddlEndAMPM.SelectedItem.ToString()) + "','" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "','" + Validator.Finalize(rbQuestion.Checked.ToString()) + "','" + Validator.Finalize(rbAnswer.Checked.ToString()) + "','A','" + LUser.Username + "','" + DateTime.Now.ToShortDateString() + "','" + LUser.Username + "','" + DateTime.Now.ToString() + "','-','-','" + Session["CurrentSchoolYear"] + "')";
                    }
                    //execute the query
                    cls.ExecuteNonQuery(qry);
                    
                    AssessmentID = cls.ExecuteScalar("select [AssessmentID] from PaceAssessment.dbo.Assessment where Title='" + txtTitle.Text + "' and AssessmentTypeID='" + ddlAssessmentType.SelectedValue + "' and SubjectID='" + ddlSubject.SelectedValue + "' and LevelID='" + ddlLevel.SelectedValue + "'");
                    
                    DataTable dt = (DataTable)Session["LoadQuestions"];
                    foreach (DataRow dRow in dt.Rows)
                    {
                        //Inserting new assessments question to the database
                        //string QuestionPoolID = cls.ExecuteScalar("Select QuestionPoolID from PaceAssessment.dbo.QuestionPool where SubjectID='" + ddlSubject.SelectedValue + "' and LevelID='" + ddlLevel.SelectedValue + "' and Question='" + dRow["Questions"].ToString() + "'");
                        qry = "Insert into PaceAssessment.dbo.AssessmentDetails (AssessmentID,QuestionPoolID,Points) values ('" + Validator.Finalize(AssessmentID) + "','" + Validator.Finalize(dRow["QuestionID"].ToString()) + "','" + Validator.Finalize(dRow["Points"].ToString()) + "')";
                        cls.ExecuteNonQuery(qry);
                    }

                    dt = (DataTable)Session["LoadFeedback"];
                    foreach (DataRow dRow in dt.Rows)
                    {
                        //Inserting new assessment feedback to the database
                        qry = "Insert into PaceAssessment.dbo.AssessmentFeedback (AssessmentID,GradeBoundary,Feedback) values ('" + Validator.Finalize(AssessmentID) + "','" + Validator.Finalize(dRow["GradeBoundary"].ToString()) + "','" + Validator.Finalize(dRow["Feedback"].ToString()) + "')";
                        cls.ExecuteNonQuery(qry);
                    }

                    GetAssID();
                }
            }
        }

        void GetAssID()
        {
            int assid = Convert.ToInt32(cls.ExecuteScalar("Select MAX(AssessmentID) From PaceAssessment.dbo.Assessment"));
            Response.Write("<script>alert('Assessment have been saved successfully.');window.location='" + ResolveUrl(DefaultForms.frm_preview_assessment) + "?assid=" + assid.ToString() + "'</script>");
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_instructor_dashboard));
        }

        protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuestion();
            LoadSubjects();
        }

        //Bind the data table for questions
        void BindDataQuestionBank()
        {
            DataTable dt = (DataTable)Session["LoadQuestionBank"];

            //Save the changes made before proceeding to the another page.
            foreach (DataRow drow in dt.Rows)
            {
                foreach (GridViewRow dgrow in dgQuestionBank.Rows)
                {
                    Label lblQuestionID = (Label)dgrow.FindControl("lblQuestionID");
                    CheckBox chkQuestion = (CheckBox)dgrow.FindControl("chkQuestion");
                    if (drow["QuestionID"].ToString() == lblQuestionID.Text)
                    {
                        if (chkQuestion.Checked == true)
                        {
                            drow["Checked"] = "True";
                            dt.AcceptChanges();
                        }
                        else
                        {
                            drow["Checked"] = "False";
                            dt.AcceptChanges();
                        }
                    }
                }
            }

            Session["LoadQuestionBank"] = dt;
            dgQuestionBank.DataSource = (DataTable)Session["LoadQuestionBank"];
            dgQuestionBank.DataBind();

            //Show the check/uncheck question in the view
            foreach (DataRow drow in dt.Rows)
            {
                foreach (GridViewRow dgrow in dgQuestionBank.Rows)
                {
                    Label lblQuestionID = (Label)dgrow.FindControl("lblQuestionID");
                    CheckBox chkQuestion = (CheckBox)dgrow.FindControl("chkQuestion");
                    if (drow["QuestionID"].ToString() == lblQuestionID.Text)
                    {
                        if (drow["Checked"].ToString() == "True")
                        {
                            chkQuestion.Checked = true;
                        }
                        else
                        {
                            chkQuestion.Checked = false;
                        }
                    }
                }
            }

            //show the enabled and disabled checkbox for selected question
            checkboxEnabledDisabled();
        }

        void checkboxEnabledDisabled()
        {
            //load question and load question bank
            DataTable dtq = (DataTable)Session["LoadQuestions"];
            DataTable dtqb = (DataTable)Session["LoadQuestionBank"];

            foreach (DataRow dqrow in dtq.Rows)
            {
                foreach (DataRow dqbrow in dtqb.Rows)
                {
                    if (dqrow["QuestionID"].ToString() == dqbrow["QuestionID"].ToString())
                    {
                        foreach (GridViewRow dgrow in dgQuestionBank.Rows)
                        {
                            Label lblQuestionID = (Label)dgrow.FindControl("lblQuestionID");
                            CheckBox chkQuestion = (CheckBox)dgrow.FindControl("chkQuestion");
                            if (lblQuestionID.Text == dqbrow["QuestionID"].ToString())
                            {
                                if (dqbrow["Checked"].ToString() == "True")
                                {
                                    chkQuestion.Enabled = false;
                                    chkQuestion.Checked = true;
                                }
                                else
                                {
                                    chkQuestion.Enabled = true;
                                    chkQuestion.Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void dgQuestionBank_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount_DB = (Label)e.Row.FindControl("lblPageCount_DB");
                lblPageCount_DB.Text = "of " + dgQuestionBank.PageCount;
                DropDownList cboPageNumber_DB = (DropDownList)e.Row.FindControl("cboPageNumber_DB");
                for (int x = 1; x <= dgQuestionBank.PageCount; x++)
                {
                    cboPageNumber_DB.Items.Add(x.ToString());
                }
                cboPageNumber_DB.SelectedIndex = dgQuestionBank.PageIndex;
            }
        }

        protected void lnkFirst_DB_Click(object sender, EventArgs e)
        {
            dgQuestionBank.PageIndex = 0;
            BindDataQuestionBank();
        }

        protected void lnkPrev_DB_Click(object sender, EventArgs e)
        {
            if (dgQuestionBank.PageIndex != 0)
            {
                dgQuestionBank.PageIndex = dgQuestionBank.PageIndex - 1;
                BindDataQuestionBank();
            }
        }

        protected void cboPageNumber_DB_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataQuestionBank();
            DropDownList cboPageNumber_DB = (DropDownList)sender;
            dgQuestionBank.PageIndex = cboPageNumber_DB.SelectedIndex;
            BindDataQuestionBank();
        }

        protected void lnkNext_DB_Click(object sender, EventArgs e)
        {
            if (dgQuestionBank.PageIndex != dgQuestionBank.PageCount)
            {
                dgQuestionBank.PageIndex = dgQuestionBank.PageIndex + 1;
                BindDataQuestionBank();
            }
        }

        protected void lnkLast_DB_Click(object sender, EventArgs e)
        {
            dgQuestionBank.PageIndex = dgQuestionBank.PageCount;
            BindDataQuestionBank();
        }

        protected void chkQuestion_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdoSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            Visibility();
        }

        void Visibility()
        {
            if (rdoSchedule.SelectedIndex == 0)
            {
                imgStartDate.Enabled = false;
                imgEndDate.Enabled = false;
                ddlStartAMPM.Enabled = false;
                ddlStartHour.Enabled = false;
                ddlStartMin.Enabled = false;
                ddlEndAMPM.Enabled = false;
                ddlEndHour.Enabled = false;
                ddlEndMin.Enabled = false;
            }
            else
            {
                imgStartDate.Enabled = true;
                imgEndDate.Enabled = true;
                ddlStartAMPM.Enabled = true;
                ddlStartHour.Enabled = true;
                ddlStartMin.Enabled = true;
                ddlEndAMPM.Enabled = true;
                ddlEndHour.Enabled = true;
                ddlEndMin.Enabled = true;
            }
        }

        protected void ddlTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SubjectID = cls.ExecuteScalar("Select s.SubjectID from PaceRegistration.dbo.Subject s inner join PaceRegistration.dbo.Level l on (s.LevelID=l.LevelID) where s.Description='" + ddlSubject.SelectedItem.ToString() + "' and l.Description='" + ddlLevel.SelectedItem.ToString() + "'");
            LoadTopicQuestion(SubjectID);
            checkboxEnabledDisabled();
        }

        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //loop through the list
            for (int row = 0; row < dgQuestionBank.Rows.Count; row++)
            {
                CheckBox chkQuestion = (CheckBox)dgQuestionBank.Rows[row].FindControl("chkQuestion");
                Label lblQuestions = (Label)dgQuestionBank.Rows[row].FindControl("lblQuestions");
                //check if has questions
                if (lblQuestions.Text != "No Question Available.")
                {
                    //check if item is enabled
                    if (chkQuestion.Enabled == true)
                    {
                        //check the item
                        chkQuestion.Checked = true;
                    }
                }
            }
            //reset the checkbox
            chkSelectAll.Checked = false;
            chkDeselectAll.Checked = false;
        }

        protected void chkDeselectAll_CheckedChanged(object sender, EventArgs e)
        {
            //loop through the list of tsble
            for (int row = 0; row < dgQuestionBank.Rows.Count; row++)
            {
                CheckBox chkQuestion = (CheckBox)dgQuestionBank.Rows[row].FindControl("chkQuestion");
                Label lblQuestions = (Label)dgQuestionBank.Rows[row].FindControl("lblQuestions");
                //checked if has questions
                if (lblQuestions.Text != "No Question Available.")
                {
                    //check if item is enabled
                    if (chkQuestion.Enabled == true)
                    {
                        //uncheck all items
                        chkQuestion.Checked = false;
                    }
                }
            }
            //reset the checkbox
            chkSelectAll.Checked = false;
            chkDeselectAll.Checked = false;
        }
    }
}
