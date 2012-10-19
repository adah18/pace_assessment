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
    public partial class quizcreation_update : System.Web.UI.Page
    {
        //Instantiate new list of Assessment
        List<Constructors.Assessment> oAssessment;
        //Instantiate new list of Assessment Details
        List<Constructors.AssessmentDetails> oAssessmentDetails;
        //Instantiate new list of subject
        List<Constructors.Subject> oSubject;
        //Instantiate new list of registration term
        List<Constructors.RegistrationTerm> oRegistrationTerm;
        //Instantiate new list of assessment feedback
        List<Constructors.AssessmentFeedback> oAssessmentFeedback;
        //Instanitate new list of assessment type
        List<Constructors.AssessmentType> oAssessmentType;
        //Instantiate new list of question pool
        List<Constructors.QuestionPool> oQuestionPool;
        //Instantiate new list
        List<Constructors.GradingView> oSubjectList;
        //Get new forms
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Get the login user
        LoginUser LUser;
        //Get new collection
        Collections cls = new Collections();
        List<Constructors.Topics> oTopic;

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

            LoadSiteMap();

            if (IsPostBack == false)
            {
                //Load Assessment Type
                LoadAssessment();
                //Load levels
                LoadLevels();
                //Load Subjects
                LoadSubjects();
                //Load Question Bank
                LoadQuestionBank("");

                //Load the details of selected assessment
                LoadAssessmentDetails();

                //Removed the Feedback
                dgFeedback.Visible = false;
                lblFeedback.Visible = false;

                
            }
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
                        dt.Rows.Add(qp.QuestionPoolID, qp.Question, "False");
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
            });

            //Loop through Filtered Duplicate values in the DataTable
            foreach (DataRow drow in dt.DefaultView.ToTable(true, new string[] { "LevelID", "LevelDescription" }).Rows)
            {
                //Add to Grade / Level DropDownList
                ddlLevel.Items.Add(drow[1].ToString());
                ddlLevel.Items[ddlLevel.Items.Count - 1].Value = drow[0].ToString();
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
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_quizcreation_main);

            //add a text for current node
            SiteMap1.CurrentNode = "Edit Assessment";
        }

        void LoadAssessment()
        {
            //Instantiate new List
            oAssessmentType = new List<Constructors.AssessmentType>(cls.getAssessmentType());
            oAssessmentType.ForEach(a =>
            {
                if (a.Status == "A")
                {
                    ddlAssessmentType.Items.Add(a.Description);
                    ddlAssessmentType.Items[ddlAssessmentType.Items.Count - 1].Value = a.AssessmentTypeID.ToString();
                }
            });

            //cherk if has assessment type
           if(ddlAssessmentType.Items.Count == 0)
               Validator.AlertBack("No assessment type. Please create assessment type first.", ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main));
        }

        void LoadSubjects()
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("--- Select Subject ---");
            ddlSubject.Items[ddlSubject.Items.Count - 1].Value = "0";

            //Instantiate new List
            oSubject = new List<Constructors.Subject>(new Collections().getSubjectList());
            oSubjectList = new List<Constructors.GradingView>(cls.getGradingView());

            DataTable dt = new DataTable();
            //Create a DataTable containing the SubjectID and SubjectDescription
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
                //Add to Grade / Level DropDownList
                ddlSubject.Items.Add(dRow[1].ToString());
                ddlSubject.Items[ddlSubject.Items.Count - 1].Value = dRow[0].ToString();
            }
        }

        void LoadAssessmentDetails()
        {
            //Instantiate new List
            oAssessment = new List<Constructors.Assessment>(cls.getAssessment());
            //Instantiate new List
            oAssessmentDetails = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());
            //Instantiate new List
            oAssessmentFeedback = new List<Constructors.AssessmentFeedback>(cls.getAssessmentFeedback());
            
            //Loop through the list of assessment
            oAssessment.ForEach(a =>
                {
                    //Check if has same assessment id
                    if (a.AssessmentID.ToString() == Request.QueryString["qid"])
                    {
                        //Display assessment details
                        string AssessmentType = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType where AssessmentTypeID='" + a.AssessmentTypeID.ToString() + "'");
                        ddlAssessmentType.SelectedValue = AssessmentType;
                        txtTitle.Text = a.Title;
                        txtIntroduction.Text = a.Introduction;

                        //Check if has schedule
                        if (a.Schedule == "Yes")
                        {
                            rdoSchedule.SelectedIndex = 1;
                            //Schedule of the exam
                            txtStartDate.Text = Convert.ToDateTime(a.DateStart).ToShortDateString();
                            txtEndDate.Text = Convert.ToDateTime(a.DateEnd).ToShortDateString();
                            ddlStartHour.SelectedValue = SetTimeHour(a.TimeStart);
                            ddlStartMin.SelectedValue = SetTimeMinute(a.TimeStart);
                            ddlStartAMPM.SelectedValue = SetTimeAMPM(a.TimeStart);
                            ddlEndHour.SelectedValue = SetTimeHour(a.TimeEnd);
                            ddlEndMin.SelectedValue = SetTimeMinute(a.TimeEnd);
                            ddlEndAMPM.SelectedValue = SetTimeAMPM(a.TimeEnd);
                        }
                            //no schedule
                        else
                        {
                            rdoSchedule.SelectedIndex = 0;

                            txtStartDate.Text = "";
                            txtEndDate.Text = "";
                        }

                        //set the visibility
                        Visibility();

                        //set the random question and answer
                        rbQuestion.Checked = Convert.ToBoolean(a.RandomQuestion);
                        rbAnswer.Checked = Convert.ToBoolean(a.RandomAnswer);

                        ddlLevel.SelectedValue = a.LevelID.ToString();
                        LoadSubjects();
                        ddlSubject.SelectedValue = a.SubjectID.ToString();

                        ddlQuarter.SelectedValue = a.Quarter.ToString();

                        //Load Question Bank
                        string SubjectID = cls.ExecuteScalar("Select s.SubjectID from PaceRegistration.dbo.Subject s inner join PaceRegistration.dbo.Level l on (s.LevelID=l.LevelID) where s.Description='" + ddlSubject.SelectedItem.ToString() + "' and l.Description='" + ddlLevel.SelectedItem.ToString() + "'");
                        LoadQuestionBank(SubjectID);

                        LoadQuestion();

                        LoadTopic();
                        DataTable dt = (DataTable)Session["LoadQuestions"];
                        for (int row = 0; row < dt.Rows.Count; row++)
                        {
                            if (dt.Rows[row][1].ToString() == "No Questions Selected.")
                            {
                                dt.Rows.RemoveAt(row);
                            }
                        }

                        //Loop through the assessment details or loading of questions
                        oAssessmentDetails.ForEach(ad =>
                            {
                                //Check if has same assessment id
                                if (a.AssessmentID == ad.AssessmentID)
                                {
                                    //Loop through the question pool
                                    oQuestionPool = new List<Constructors.QuestionPool>(cls.getQuestionPool());
                                    oQuestionPool.ForEach(qp =>
                                        {
                                            //Check if has same question pool id
                                            if (ad.QuestionPoolID == qp.QuestionPoolID)
                                            {
                                                dt.Rows.Add(qp.QuestionPoolID, qp.Question, ad.Points);
                                            }
                                        });
                                }
                            }
                        );

                        //Binding the data
                        Session["LoadQuestions"] = dt;
                        dgQuestions.DataSource = (DataTable)Session["LoadQuestions"];
                        dgQuestions.DataBind();

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
                            }
                        }

                        Session["LoadQuestionBank"] = dtqb;

                        //Load the feedback data table
                        LoadFeedBack();
                        dt = (DataTable)Session["LoadFeedback"];
                        //Looping through the assessment feedback table
                        oAssessmentFeedback.ForEach(afb =>
                            {
                                if (a.AssessmentID == afb.AssessmentID)
                                {
                                    dt.Rows.Add(afb.GradeBoundary, afb.Feedback);
                                }
                            }
                            );

                        Session["LoadFeedback"] = dt;
                        dgFeedback.DataSource = Session["LoadFeedback"];
                        dgFeedback.DataBind();
                        FeedbackDelete();
                    }
                }
                );
        }

        void LoadQuestion()
        {
            //Creating new data table for questions
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("QuestionID"));
            dt.Columns.Add(new DataColumn("Questions"));
            dt.Columns.Add(new DataColumn("Points"));

            dt.Rows.Add("0", "No Questions Selected.", "0");

            Session["LoadQuestions"] = dt;
            dgQuestions.DataSource = Session["LoadQuestions"];
            dgQuestions.DataBind();
        }

        void LoadFeedBack()
        {
            //Creating new data table for feedback
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("GradeBoundary"));
            dt.Columns.Add(new DataColumn("Feedback"));

            Session["LoadFeedback"] = dt;
            dgFeedback.DataSource = Session["LoadFeedback"];
            dgFeedback.DataBind();
        }

        private string SetTimeHour(string Time)
        {
            //Set the hour
            string[] num = Time.Split(new char[] { ':' });
            if (Convert.ToInt32(num[0]) > 12)
            {
                num[0] = (Convert.ToInt32(num[0]) - 12).ToString();
                if (Convert.ToInt32(num[0]) > 10)
                    num[0] = "0" + num[0].ToString();
            }

            return num[0];
        }

        private string SetTimeMinute(string Time)
        {
            //Set the minute
            string[] num = Time.Split(new char[] { ':' });
            return num[1];
        }

        private string SetTimeAMPM(string Time)
        {
            //Set the AM and PM
            string[] num = Time.Split(new char[] { ':' });
            if (Convert.ToInt32(num[0]) > 12)
                return "PM";
            else
                return "AM";
        }

        protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuestionBank("");
            LoadSubjects();
            LoadQuestion();
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SubjectID = cls.ExecuteScalar("Select s.SubjectID from PaceRegistration.dbo.Subject s inner join PaceRegistration.dbo.Level l on (s.LevelID=l.LevelID) where s.Description='" + ddlSubject.SelectedItem.ToString() + "' and l.Description='" + ddlLevel.SelectedItem.ToString() + "'");
            LoadQuestionBank(SubjectID);
            LoadQuestion();
            LoadTopic();
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

        //Bind the data table questions
        void BindDataQuestions()
        {
            dgQuestions.DataSource = (DataTable)Session["LoadQuestions"];
            dgQuestions.DataBind();
        }

        //Function that checks if the question is existing
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

        //Executed when the row is edited
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

        //Executed when the row is data bound
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

        //Executed when the row is deleted
        protected void dgQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //load the questions and question banks
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

        //Diable the delete if has only singel item
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

        protected void dgFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        //Bind the data table feedback
        void BindDataFeedback()
        {
            dgFeedback.DataSource = (DataTable)Session["LoadFeedback"];
            dgFeedback.DataBind();
        }

        //Executed when the row is deleted
        protected void dgFeedback_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["LoadFeedback"];
            dt.Rows[e.RowIndex].Delete();
            dt.AcceptChanges();

            dgFeedback.EditIndex = -1;
            BindDataFeedback();
            FeedbackDelete();
        }

        //Executed when the row is edited
        protected void dgFeedback_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgFeedback.EditIndex = e.NewEditIndex;
            BindDataFeedback();
        }

        //Executed when the row is updated
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
                                else if (!dt.Rows[row][0].ToString().Contains("-"))
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[row][0].ToString().Replace("%", "")))
                                    {
                                        int numm = int.Parse(dt.Rows[row][0].ToString().Replace("%", ""));
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

        //Executed when link first is clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            dgQuestions.PageIndex = 0;
            BindDataQuestions();
        }

        //Executed when link previous is clicked
        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            if (dgQuestions.PageIndex != 0)
            {
                dgQuestions.PageIndex = dgQuestions.PageIndex - 1;
                BindDataQuestions();
            }
        }

        //Executed when link next is clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (dgQuestions.PageIndex != dgQuestions.PageCount)
            {
                dgQuestions.PageIndex = dgQuestions.PageIndex + 1;
                BindDataQuestions();
            }
        }

        //Executed when link last is clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            dgQuestions.PageIndex = dgQuestions.PageCount;
            BindDataQuestions();
        }

        //Executed when the user change the page number
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            dgQuestions.PageIndex = cboPageNumber.SelectedIndex;
            BindDataQuestions();
        }

        //Executed when the rows was cancelled
        protected void dgQuestions_RowcCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgQuestions.EditIndex = -1;
            BindDataQuestions();

        }

        //Executed when the row was udpated
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

        //Executed when the row was cancelled
        protected void dgFeedback_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgFeedback.EditIndex = -1;
            BindDataFeedback();
            FeedbackDelete();
        }

        //Executed when the row was added
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Check if required fields are met
            if (RequiredFieldValidator() == false)
            {
                string AssessmentTypeID = cls.ExecuteScalar("select [AssessmentTypeID] from PaceAssessment.dbo.AssessmentType where Description='" + ddlAssessmentType.SelectedItem.ToString() + "'");
                string AssessmentID = cls.ExecuteScalar("select [AssessmentID] from PaceAssessment.dbo.Assessment where Title='" + txtTitle.Text + "' and AssessmentTypeID='" + AssessmentTypeID + "'");
                //Check the duplication if assessment title
                if (!string.IsNullOrEmpty(AssessmentID))
                {
                    if (AssessmentID != Request.QueryString["qid"])
                    {
                        lblQuiz.Text = "*" + ddlAssessmentType.SelectedItem.ToString() + " Title already exist."; 
                        return;
                    }
                }

                //Updating the assessment
                string qry = "";
                qry = "UPDATE PaceAssessment.dbo.Assessment SET AssessmentTypeID='" + Validator.Finalize(AssessmentTypeID) + "',LevelID='" + Validator.Finalize(ddlLevel.SelectedValue) + "',SubjectID='" + Validator.Finalize(ddlSubject.SelectedValue) + "',Title='" + Validator.Finalize(txtTitle.Text) + "',Introduction='" + Validator.Finalize(txtIntroduction.Text) + "',DateStart='" + Convert.ToDateTime(txtStartDate.Text).ToShortDateString() + "',DateEnd='" + Convert.ToDateTime(txtEndDate.Text).ToShortDateString() + "',TimeStart='" + getTime(ddlStartHour.SelectedItem.ToString(), ddlStartMin.SelectedItem.ToString(), ddlStartAMPM.SelectedItem.ToString()) + "',TimeEnd='" + getTime(ddlEndHour.SelectedItem.ToString(), ddlEndMin.SelectedItem.ToString(), ddlEndAMPM.SelectedItem.ToString()) + "',LastUpdateDate='" + DateTime.Now.ToString() + "',LastUpdateUser='admin' where AssessmentID='" + Request.QueryString["qid"] + "'";
                cls.ExecuteNonQuery(qry);

                AssessmentID = cls.ExecuteScalar("select [AssessmentID] from PaceAssessment.dbo.Assessment where Title='" + txtTitle.Text + "' and AssessmentTypeID='" + AssessmentTypeID + "'");
                
                //Deleting the assessment details
                qry = "Delete from PaceAssessment.dbo.AssessmentDetails where AssessmentID='" + AssessmentID + "'";
                cls.ExecuteNonQuery(qry);
                
                DataTable dt = (DataTable)Session["LoadQuestions"];
                //Inserting the modified questions
                foreach (DataRow dRow in dt.Rows)
                {
                    string QuestionPoolID = cls.ExecuteScalar("Select QuestionPoolID from PaceAssessment.dbo.QuestionPool where SubjectID='" + ddlSubject.SelectedValue + "' and LevelID='" + ddlLevel.SelectedValue + "' and Question='" + dRow["Questions"].ToString() + "'");
                    qry = "Insert into PaceAssessment.dbo.AssessmentDetails (AssessmentID,QuestionPoolID,Points) values ('" + Validator.Finalize(AssessmentID) + "','" + Validator.Finalize(QuestionPoolID) + "','" + Validator.Finalize(dRow["Points"].ToString()) + "')";
                    cls.ExecuteNonQuery(qry);
                }

                //Deleting the assessment feedback
                qry = "Delete from PaceAssessment.dbo.AssessmentFeedback where AssessmentID='" + AssessmentID + "'";
                cls.ExecuteNonQuery(qry);

                dt = (DataTable)Session["LoadFeedback"];
                //Inserting the modified feedback
                foreach (DataRow dRow in dt.Rows)
                {
                    qry = "Insert into PaceAssessment.dbo.AssessmentFeedback (AssessmentID,GradeBoundary,Feedback) values ('" + Validator.Finalize(AssessmentID) + "','" + Validator.Finalize(dRow["GradeBoundary"].ToString()) + "','" + Validator.Finalize(dRow["Feedback"].ToString()) + "')";
                    cls.ExecuteNonQuery(qry);
                }

                Response.Write("<script>alert('Assessment have been updated successfully.');</script>");
                
            }
        }

        //Get the time
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

        //Function for to check if the title exist
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

        //Function as required field validator
        private bool RequiredFieldValidator()
        {
            bool haserror;
            haserror = false;

            //Check if the title is not empty
            if (Validator.isEmpty(txtTitle.Text))
            {
                lblQuiz.Text = "* Required Field.";
                haserror = true;
            }
            else
                lblQuiz.Text = "*";

            //Check if the introduction is not empty
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

            //Check if has selected the default
            if (Validator.isDefaultSelected(ddlLevel.SelectedValue))
            {
                lblLevel.Text = "* Please Specify Grade / Level";
                haserror = true;
                //return haserror;
            }
            else
                lblLevel.Text = "*";

            //Check if has selected the default
            if (Validator.isDefaultSelected(ddlSubject.SelectedValue))
            {
                lblSubject.Text = "* Please Specify Subject";
                haserror = true;
                //return haserror;
            }
            else
                lblSubject.Text = "*";

            DataTable dt = (DataTable)Session["LoadQuestions"];
            //Check if has question to be save
            if (dt.Rows.Count == 0)
            {
                lblQuestion.Text = "* Please add Questions.";
                haserror = true;
            }
            else
            {
                lblQuestion.Text = "*";
                foreach (DataRow drow in dt.Rows)
                {
                    if (drow["QuestionID"].ToString() == "0")
                    {
                        lblQuestion.Text = "* Please add Questions.";
                        haserror = true;
                    }
                }
            }

            dt = (DataTable)Session["LoadFeedback"];
            //Check if has feedback
            if (dt.Rows.Count == 0)
            {
                lblFeedback.Text = "* Please add Feedback.";
                haserror = true;
            }
            else
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    //Check if the has an empty rows
                    if (Validator.isEmpty(dRow["Feedback"].ToString()) || Validator.isEmpty(dRow["GradeBoundary"].ToString()))
                    {
                        lblFeedback.Text = "* Please review your work. A row contains an empty value.";
                        haserror = true;
                        break;
                    }
                    else
                    {
                        //Check if the boundary entered is valid
                        if (CountCharacterInString("-", dRow["GradeBoundary"].ToString()) <= 1)
                        {
                            //Check if has range "-"
                            if (dRow["GradeBoundary"].ToString().Contains("-"))
                            {
                                string[] num = dRow["GradeBoundary"].ToString().Split(new char[] { '-' });
                                int num1, num2;
                                bool isnum1 = int.TryParse(num[0].ToString().Replace("%", ""), out num1);
                                bool isnum2 = int.TryParse(num[1].ToString().Replace("%", ""), out num2);
                                //Check if grade boundary entered with range is valid
                                if (isnum1 && isnum2)
                                {
                                    //Check if grade boundary is correct
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
                                //Grade boundary without range "-"
                                int num1;
                                string num = dRow["GradeBoundary"].ToString().Replace("%", "");
                                bool isnum1 = int.TryParse(num, out num1);
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

        //Count the apprearance of a character
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
            //Check if required fields are met
            if (RequiredFieldValidator() == false)
            {
                string AssessmentID = cls.ExecuteScalar("select [AssessmentID] from PaceAssessment.dbo.Assessment where Title='" + txtTitle.Text + "' and AssessmentTypeID='" + ddlAssessmentType.SelectedValue + "' and SubjectID='" + ddlSubject.SelectedValue + "' and LevelID='" + ddlLevel.SelectedValue + "'");
                //Check the duplication if assessment title
                if (!string.IsNullOrEmpty(AssessmentID))
                {
                    if (AssessmentID != Request.QueryString["qid"])
                    {
                        lblQuiz.Text = "*" + ddlAssessmentType.SelectedItem.ToString() + " Title already exist.";
                        return;
                    }
                }

                //Updating the assessment
                string qry = "";

                //check if what type or schedule
                if (rdoSchedule.SelectedIndex == 0)
                {
                    //query for no schedule
                    qry = "UPDATE PaceAssessment.dbo.Assessment SET AssessmentTypeID='" + Validator.Finalize(ddlAssessmentType.SelectedValue) + "',LevelID='" + Validator.Finalize(ddlLevel.SelectedValue) + "',SubjectID='" + Validator.Finalize(ddlSubject.SelectedValue) + "',Title='" + Validator.Finalize(txtTitle.Text) + "',Introduction='" + Validator.Finalize(txtIntroduction.Text) + "',Schedule='No',ScheduleStatus='Close',DateStart=getdate(),DateEnd=getdate(),TimeStart='',TimeEnd='',Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "',RandomQuestion='" + Validator.Finalize(rbQuestion.Checked.ToString()) + "',RandomAnswer='" + Validator.Finalize(rbAnswer.Checked.ToString()) + "',LastUpdateUser='" + LUser.Username + "',LastUpdateDate=getdate() where AssessmentID='" + Request.QueryString["qid"] + "'";
                }
                else if (rdoSchedule.SelectedIndex == 1)
                {
                    //query for with schedule
                    qry = "UPDATE PaceAssessment.dbo.Assessment SET AssessmentTypeID='" + Validator.Finalize(ddlAssessmentType.SelectedValue) + "',LevelID='" + Validator.Finalize(ddlLevel.SelectedValue) + "',SubjectID='" + Validator.Finalize(ddlSubject.SelectedValue) + "',Title='" + Validator.Finalize(txtTitle.Text) + "',Introduction='" + Validator.Finalize(txtIntroduction.Text) + "',Schedule='Yes',ScheduleStatus='Close',DateStart='" + Convert.ToDateTime(txtStartDate.Text).ToShortDateString() + "',DateEnd='" + Convert.ToDateTime(txtEndDate.Text).ToShortDateString() + "',TimeStart='" + Convert.ToDateTime(ddlStartHour.SelectedItem.ToString() + ":" + ddlStartMin.SelectedItem.ToString() + " " + ddlStartAMPM.SelectedItem.ToString()) + "',TimeEnd='" + Convert.ToDateTime(ddlEndHour.SelectedItem.ToString() + ":" + ddlEndMin.SelectedItem.ToString() + " " + ddlEndAMPM.SelectedItem.ToString()) + "',Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "',RandomQuestion='" + Validator.Finalize(rbQuestion.Checked.ToString()) + "',RandomAnswer='" + Validator.Finalize(rbAnswer.Checked.ToString()) + "',LastUpdateUser='" + LUser.Username + "',LastUpdateDate=getdate() where AssessmentID='" + Request.QueryString["qid"] + "'";
                }

                cls.ExecuteNonQuery(qry);

                AssessmentID = cls.ExecuteScalar("select [AssessmentID] from PaceAssessment.dbo.Assessment where Title='" + txtTitle.Text + "' and AssessmentTypeID='" + ddlAssessmentType.SelectedValue + "' and SubjectID='" + ddlSubject.SelectedValue + "' and LevelID='" + ddlLevel.SelectedValue + "'");

                //Deleting the assessment details
                qry = "Delete from PaceAssessment.dbo.AssessmentDetails where AssessmentID='" + AssessmentID + "'";
                cls.ExecuteNonQuery(qry);

                DataTable dt = (DataTable)Session["LoadQuestions"];
                //Inserting the modified questions
                foreach (DataRow dRow in dt.Rows)
                {
                    //string QuestionPoolID = cls.ExecuteScalar("Select QuestionPoolID from PaceAssessment.dbo.QuestionPool where SubjectID='" + ddlSubject.SelectedValue + "' and LevelID='" + ddlLevel.SelectedValue + "' and Question='" + dRow["Questions"].ToString() + "'");
                    qry = "Insert into PaceAssessment.dbo.AssessmentDetails (AssessmentID,QuestionPoolID,Points) values ('" + Validator.Finalize(AssessmentID) + "','" + Validator.Finalize(dRow["QuestionID"].ToString()) + "','" + Validator.Finalize(dRow["Points"].ToString()) + "')";
                    cls.ExecuteNonQuery(qry);
                }

                //Deleting the assessment feedback
                qry = "Delete from PaceAssessment.dbo.AssessmentFeedback where AssessmentID='" + AssessmentID + "'";
                cls.ExecuteNonQuery(qry);

                dt = (DataTable)Session["LoadFeedback"];
                //Inserting the modified feedback
                foreach (DataRow dRow in dt.Rows)
                {
                    qry = "Insert into PaceAssessment.dbo.AssessmentFeedback (AssessmentID,GradeBoundary,Feedback) values ('" + Validator.Finalize(AssessmentID) + "','" + Validator.Finalize(dRow["GradeBoundary"].ToString()) + "','" + Validator.Finalize(dRow["Feedback"].ToString()) + "')";
                    cls.ExecuteNonQuery(qry);
                }

                GetAssID(Convert.ToInt32(AssessmentID));

            }
        }

        void GetAssID(int AssessmentID)
        {
            int assid = Convert.ToInt32(cls.ExecuteScalar("Select MAX(AssessmentID) From PaceAssessment.dbo.Assessment"));
            Response.Write("<script>alert('Assessment have been updated successfully.');window.location='" + ResolveUrl(DefaultForms.frm_preview_assessment) + "?assid=" + AssessmentID.ToString() + "'</script>");
            //Response.Write("<script>alert('Assessment have been updated successfully.');window.location='" + ResolveUrl(DefaultForms.frm_default_dashboard) + "'</script>");
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_quizcreation_main));
        }

        //Executed when the row has been updated
        protected void dgFeedback_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            FeedbackDelete();
        }

        //Executed when the combo box has been changed
        protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lstAQuestions.Items.Clear();
            LoadSubjects();
            LoadQuestion();
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

        protected void lnkFirst_DB_Click(object sender, EventArgs e)
        {
            dgQuestionBank.PageIndex = 0;
            BindDataQuestionBank();
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
                    //check if has same question id
                    if (drow["QuestionID"].ToString() == lblQuestionID.Text)
                    {
                        //check if the check box was checked
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

        protected void chkQuestion_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SubjectID = cls.ExecuteScalar("Select s.SubjectID from PaceRegistration.dbo.Subject s inner join PaceRegistration.dbo.Level l on (s.LevelID=l.LevelID) where s.Description='" + ddlSubject.SelectedItem.ToString() + "' and l.Description='" + ddlLevel.SelectedItem.ToString() + "'");
            LoadTopicQuestion(SubjectID);
            checkboxEnabledDisabled();
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

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //for selecting all unselected questions
            //loop through the list
            for (int row = 0; row < dgQuestionBank.Rows.Count; row++)
            {
                CheckBox chkQuestion = (CheckBox)dgQuestionBank.Rows[row].FindControl("chkQuestion");
                Label lblQuestions = (Label)dgQuestionBank.Rows[row].FindControl("lblQuestions");
                //check if has questions
                if (lblQuestions.Text != "No Question Available.")
                {
                    //checl if the ite m is enabked
                    if (chkQuestion.Enabled == true)
                    {
                        //check the question
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
            //for deselect of questions
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
                        //check the question
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
