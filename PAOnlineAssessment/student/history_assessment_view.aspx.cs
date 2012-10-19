using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;

namespace PAOnlineAssessment.student
{
    public partial class history_assessment_view : System.Web.UI.Page
    {
        //Instantite New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Instantiate New List of Assessments
        List<Constructors.AssessmentView> oAssessment;
        //Instantiate New List of Assessment Details
        List<Constructors.AssessmentDetails> oAssessmentDetails;
        //Instantiate New List of Questions
        List<Constructors.QuestionPool> oQuestions;
        //Declare New LoginUser Class
        LoginUser LUser;
        //Declare New CurrentStudent Class
        CurrentStudent CStudent;
        //Instantiate new Global Forms class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate new Collections Class
        Collections cls = new Collections();
        //Declare New StudentAnswers Class
        List<Constructors.StudentAnswers> oStudentAnswer;
        public string RandomQuestion = "", RandomAnswer = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In
            try
            {
                //Get Login User Info from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                //Get Current Student Info from the Session Variable
                CStudent = (CurrentStudent)Session["CurrentStudent"];
                string Trigger = LUser.Username;

                //check if the user is login and a student
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "S")
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

            if (Convert.ToInt32(Request.QueryString["aid"]) <= 0)
            {
                Response.Redirect(ResolveUrl(Request.RawUrl) + "?aid=1");
            }

            if (IsPostBack == false)
            {
                Debug.WriteLine("First Load.");
                //Load site map for navogation
                LoadSitemapDetails();
                //Load Assessment details
                LoadAssessmentDetails();
                //Load Questions
                LoadQuestion();
                //Load Student answer
                LoadStudentAnswers();
                //Load remarks
                LoadRemarks();
                //Load correct remarks
                LoadCorrectRemarks();
                //Set the page size of the grid to 10
                dgAssessment.PageSize = 10;
                //Bind all data
                BindData();
                //load the students score
                LoadStudentsScore();
            }
        }

        //Load site map details for navigation
        public void LoadSitemapDetails()
        {
            SiteMap.RootNode = "Dashboard";
            SiteMap.RootNodeToolTip = "Click to go back to Dashboard";
            SiteMap.RootNodeURL = ResolveUrl(DefaultForms.frm_default_dashboard);

            SiteMap.ParentNode = "Assessment History List";
            SiteMap.ParentNodeToolTip = "Click to go back to the Assessment History List";
            SiteMap.ParentNodeURL = ResolveUrl(DefaultForms.frm_history_assessment_main);

            SiteMap.CurrentNode = "Review Assessment";
        }

        //Load assessment details
        void LoadAssessmentDetails()
        {
            //Instantiate new list of assessment
            oAssessment = new List<Constructors.AssessmentView>(cls.getAssessmentView());
            
            oAssessment.ForEach(a =>
                {
                    if (a.AssessmentID == Convert.ToInt32(Request.QueryString["aid"]))
                    {
                        lblAssessmentIntroduction.Text = a.Introduction;
                        lblAssessmentTitle.Text = a.Title;
                        lblAssessmentIntroduction.Text = a.Introduction;
                        lblAssessmentTitle.Text = a.Title;
                        lblSubject.Text = a.SubjectDescription;
                        lblTeacher.Text = a.TeacherFirstname + " " + a.TeacherLastname;
                        lblExamType.Text = a.AssessmentTypeDescription;
                        if (a.Schedule == "Yes")
                        {
                            lblScheduledDate.Text = Convert.ToDateTime(a.DateStart).ToShortDateString() + " to " + Convert.ToDateTime(a.DateEnd).ToShortDateString();
                            lblScheduleTime.Text = Convert.ToDateTime(a.TimeStart).ToShortTimeString() + " to " + Convert.ToDateTime(a.TimeEnd).ToShortTimeString();
                        }
                        else if (a.Schedule == "No")
                        {
                            lblScheduledDate.Text = "-";
                            lblScheduleTime.Text = "-";
                        }
                        RandomAnswer = a.RandomAnswer;
                        RandomQuestion = a.RandomQuestions;
                        return;
                    }
                });
        }

        //Bind the data
        void BindData()
        {
            dgAssessment.DataSource = Session["AssessmentQuestion"];
            dgAssessment.DataBind();
            GridView1.DataSource = Session["AssessmentQuestion"];
            GridView1.DataBind();
            GridView2.DataSource = Session["Remark"];
            GridView2.DataBind();
        }

        //Load questions
        void LoadQuestion()
        {
            //Declare new DataTable
            DataTable dt = new DataTable();

            //Add Columns to the Newly Declared DataTable
            dt.Columns.Add(new DataColumn("AssessmentID"));
            dt.Columns.Add(new DataColumn("QuestionPoolID"));
            dt.Columns.Add(new DataColumn("Points"));
            dt.Columns.Add(new DataColumn("Question"));
            dt.Columns.Add(new DataColumn("CorrectAnswer"));
            dt.Columns.Add(new DataColumn("CorrectAnswerRemark"));
            dt.Columns.Add(new DataColumn("Choice1"));
            dt.Columns.Add(new DataColumn("Choice1Remark"));
            dt.Columns.Add(new DataColumn("Choice2"));
            dt.Columns.Add(new DataColumn("Choice2Remark"));
            dt.Columns.Add(new DataColumn("Choice3"));
            dt.Columns.Add(new DataColumn("Choice3Remark"));
            dt.Columns.Add(new DataColumn("Choice4"));
            dt.Columns.Add(new DataColumn("Choice4Remark"));
            dt.Columns.Add(new DataColumn("SelectedAnswer"));
            dt.Columns.Add(new DataColumn("RowOrder"));
            dt.Columns.Add(new DataColumn("FileName"));

            //Instantiate new list of assessment details
            oAssessmentDetails = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());
            //Instantiate new list of questions
            oQuestions = new List<Constructors.QuestionPool>(cls.getQuestionPool());

            int rowcount = 0;
            oAssessmentDetails.ForEach(ad =>
                {
                    if (ad.AssessmentID == Convert.ToInt32(Request.QueryString["aid"]))
                    {
                        oQuestions.ForEach(q =>
                            {
                                if (q.QuestionPoolID == ad.QuestionPoolID)
                                {
                                    rowcount++;
                                    dt.Rows.Add(ad.AssessmentID,q.QuestionPoolID,ad.Points,q.Question,q.CorrectAnswer,q.CorrectAnswerRemark,q.Choice1,q.Choice1Remark,q.Choice2,q.Choice2Remark,q.Choice3,q.Choice3Remark,q.Choice4,q.Choice4Remark,"",rowcount.ToString(),q.ImageFileName);
                                }
                            });
                    }
                });

            Session["AssessmentQuestion"] = dt;

            cboQuestionsPerPage.Items.Clear();
            for (int x = 1; x <= dt.Rows.Count; x++)
            {
                cboQuestionsPerPage.Items.Add(x.ToString());
            }

            //Reset Page Number dropdownlist
            cboPageNumber.Items.Clear();
            for (int x = 1; x <= dgAssessment.PageCount; x++)
            {
                cboPageNumber.Items.Add(x.ToString());
            }

            //set the currently selected index of the PageNumber DropDownList to the Current Page Index
            cboPageNumber.SelectedIndex = dgAssessment.PageIndex;
            //set the total page count of the current page
            lblPageCount.Text = "of " + dgAssessment.PageCount.ToString();
        }

        //Load student answers
        void LoadStudentAnswers()
        {
            oStudentAnswer = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
            oAssessmentDetails = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());
            oQuestions = new List<Constructors.QuestionPool>(cls.getQuestionPool());
            DataTable dt = (DataTable)Session["AssessmentQuestion"];

            string Check = cls.ExecuteScalar("Select StudentAnswerID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + Request.QueryString["aid"] + "' and StudentID='" + CStudent.StudentID + "'");
            //Check if the assessment is taken or not
            if (!string.IsNullOrEmpty(Check))
            {
                //taken
                oStudentAnswer.ForEach(sa =>
                    {
                        if (sa.StudentID == CStudent.StudentID)
                        {
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                if ((string)dt.Rows[x]["QuestionPoolID"] == sa.QuestionPoolID.ToString())
                                {
                                    dt.Rows[x]["SelectedAnswer"] = sa.SelectedAnswer;
                                    dt.AcceptChanges();
                                }
                            }
                        }
                    });
            }
            else
            {
                //not taken
                oAssessmentDetails.ForEach(ad =>
                {
                    if (ad.AssessmentID == Convert.ToInt32(Request.QueryString["aid"]))
                    {
                        oQuestions.ForEach(q =>
                        {
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                if ((string)dt.Rows[x]["QuestionPoolID"] == q.QuestionPoolID.ToString())
                                {
                                    dt.Rows[x]["SelectedAnswer"] = "";
                                    dt.AcceptChanges();
                                }
                            }
                        });
                    }
                });
            }

            Session["AssessmentQuestion"] = dt;
        }

        //Load the remarks
        void LoadRemarks()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("QuestionID"));
            dt.Columns.Add(new DataColumn("Choice"));
            dt.Columns.Add(new DataColumn("Remark"));

            oAssessmentDetails.ForEach(ad =>
            {
                if (ad.AssessmentID.ToString() == Request.QueryString["aid"])
                {
                    oQuestions.ForEach(qp =>
                    {
                        dt.Rows.Add(qp.QuestionPoolID, qp.Choice1, qp.Choice1Remark);
                        dt.Rows.Add(qp.QuestionPoolID, qp.Choice2, qp.Choice2Remark);
                        dt.Rows.Add(qp.QuestionPoolID, qp.Choice3, qp.Choice3Remark);
                        dt.Rows.Add(qp.QuestionPoolID, qp.Choice4, qp.Choice4Remark);
                    });

                }
            });

            Session["Remark"] = dt;
        }

        //Load the correct remarks of the correct answer
        void LoadCorrectRemarks()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("QuestionID"));
            dt.Columns.Add(new DataColumn("Choice"));
            dt.Columns.Add(new DataColumn("Remark"));

            oAssessmentDetails.ForEach(ad =>
            {
                if (ad.AssessmentID.ToString() == Request.QueryString["assid"])
                {
                    oQuestions.ForEach(qp =>
                    {
                        dt.Rows.Add(qp.QuestionPoolID, qp.CorrectAnswer, qp.CorrectAnswerRemark);

                    });

                }
            });

            Session["CorrectRemark"] = dt;
        }

        //Executed when the page size was changed
        protected void cboQuestionsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgAssessment.PageSize = cboQuestionsPerPage.SelectedIndex + 1;
            BindData();
        }

        //Executed when the button first was clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            dgAssessment.PageIndex = 0;
            BindData();
        }

        //Executed when the button previous was clicked
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (dgAssessment.PageIndex > 0)
            {
                dgAssessment.PageIndex = dgAssessment.PageIndex - 1;
                BindData();
            }
        }

        //Executed when the button next was clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (dgAssessment.PageIndex <= dgAssessment.PageCount)
            {
                dgAssessment.PageIndex = dgAssessment.PageIndex + 1;
                BindData();
            }
        }

        //Executed when the button last was clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            dgAssessment.PageIndex = dgAssessment.PageCount;
            BindData();
        }

        //Executed when the page number was changed
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgAssessment.PageIndex = cboPageNumber.SelectedIndex;
            BindData();
        }

        protected void rboMultipleChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtAnswer_TextChanged(object sender, EventArgs e)
        {

        }

        void LoadStudentsScore()
        {
            int rawscore = 0;
            int totalscore = 0;

            DataTable dtQuestion = (DataTable)Session["AssessmentQuestion"];
            oQuestions = new List<Constructors.QuestionPool>(cls.getQuestionPool());
            oAssessmentDetails = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());

            oQuestions.ForEach(q =>
                {
                    foreach (DataRow dr in dtQuestion.Rows)
                    {
                        if (dr["QuestionPoolID"].ToString() == q.QuestionPoolID.ToString())
                        {
                            oAssessmentDetails.ForEach(ad =>
                                {
                                    if (ad.AssessmentID.ToString() == Request.QueryString["aid"].ToString() && ad.QuestionPoolID == q.QuestionPoolID)
                                    {
                                        totalscore = totalscore + ad.Points;

                                        if (dr["SelectedAnswer"].ToString().ToLower() == q.CorrectAnswer.ToString().ToLower())
                                        {
                                            rawscore = rawscore + ad.Points;
                                        }
                                    }
                                });
                        }
                    }
                });

            lblTotalScore.Text = totalscore.ToString();
            lblRawScore.Text = rawscore.ToString();
        }

        //Executed when row has been data bound
        protected void dgAssessment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < dgAssessment.Rows.Count; x++)
            {
                RadioButtonList rboMultipleChoice = (RadioButtonList)dgAssessment.Rows[x].FindControl("rboAnswer");
                Label lblSelectedAnswer = (Label)dgAssessment.Rows[x].FindControl("lblSelectedAnswer");
                TextBox txtAnswer = (TextBox)dgAssessment.Rows[x].FindControl("txtAnswer");
                Label lblQuestionID = (Label)dgAssessment.Rows[x].FindControl("lblQuestionPoolID");
                Label lblRemark = (Label)dgAssessment.Rows[x].FindControl("lblRemark");
                Label lblCaptionRemark = (Label)dgAssessment.Rows[x].FindControl("lblCaptionRemark");
                Label lblCorrectAnswer = (Label)dgAssessment.Rows[x].FindControl("lblCorrectAnswer");
                Label lblCaptionCorrectAnswer = (Label)dgAssessment.Rows[x].FindControl("lblCaptionCorrectAnswer");
                Image imgStatus = (Image)dgAssessment.Rows[x].FindControl("imgStatus");
                Label lblPoints = (Label)dgAssessment.Rows[x].FindControl("lblPoints");

                Label lblFileName = (Label)dgAssessment.Rows[x].FindControl("lblFileName");
                Image loadPicture = (Image)dgAssessment.Rows[x].FindControl("loadPicture");
                LinkButton lnkView = (LinkButton)dgAssessment.Rows[x].FindControl("lnkView");

                if (lblFileName.Text != "")
                {
                    if (System.IO.File.Exists(Server.MapPath(Defaults.FolderPath + lblFileName.Text)))
                        loadPicture.ImageUrl = Defaults.ImagePath + lblFileName.Text;
                    else
                        loadPicture.ImageUrl = Defaults.ImagePath + "NoImage.jpg";

                    loadPicture.Visible = true;
                    lnkView.Text = "View Image";
                }
                else
                {
                    loadPicture.ImageUrl = "";
                    loadPicture.Visible = false;
                    lnkView.Text = "";
                }

                lblRemark.Visible = true;
                lblCaptionRemark.Visible = true;

                //Check if the answer is correct
                if (isAnswerCorrect(lblQuestionID.Text, lblSelectedAnswer.Text) == true)
                {   
                    //Answer correct
                    imgStatus.ImageUrl = "~/images/dashboard_icons/accept.png";
                    imgStatus.ToolTip = "Correct";
                    lblCorrectAnswer.Visible = false;
                    lblCaptionCorrectAnswer.Visible = false;
                }
                else
                {   
                    //Answer not correct
                    imgStatus.ImageUrl = "~/images/dashboard_icons/delete.png";
                    imgStatus.ToolTip = "Incorrect";
                    lblCorrectAnswer.Visible = true;
                    lblCaptionCorrectAnswer.Visible = true;
                    
                    //not taken
                    if (lblSelectedAnswer.Text == "")
                    {
                        imgStatus.Visible = true;
                        lblRemark.Visible = true;
                        lblCaptionRemark.Visible = true;
                    }

                    /*if (rboMultipleChoice.Equals(null))
                    {
                        jsCall("Some questions are still left unanswered.\\r\\nPlease review your answers before submitting.");
                    }*/
                }

                //Set the right remarks for entered answer
                lblRemark.Text = GetSelectedAnswerRemark(lblQuestionID.Text, lblSelectedAnswer.Text);
                rboMultipleChoice.DataSource = GetChoices(lblQuestionID.Text);
                rboMultipleChoice.DataBind();
                txtAnswer.Text = lblSelectedAnswer.Text;

            reloop:
                for (int y = 0; y < rboMultipleChoice.Items.Count; y++)
                {
                    if (rboMultipleChoice.Items[y].Text == string.Empty)
                    {
                        rboMultipleChoice.Items.RemoveAt(y);
                        goto reloop;
                    }
                    if (rboMultipleChoice.Items[y].Text == lblSelectedAnswer.Text)
                    {
                        rboMultipleChoice.ClearSelection();
                        rboMultipleChoice.Items[y].Selected = true;
                    }
                }

                if (rboMultipleChoice.Items.Count > 0)
                {
                    txtAnswer.Visible = false;
                }
                else
                {
                    txtAnswer.Visible = true;
                }
            }

            cboPageNumber.Items.Clear();
            for (int x = 1; x <= dgAssessment.PageCount; x++)
            {
                cboPageNumber.Items.Add(x.ToString());
            }
            cboPageNumber.SelectedIndex = dgAssessment.PageIndex;
            lblPageCount.Text = "of " + dgAssessment.PageCount.ToString();
        }

        //Return the corresponding choices of the question
        public string[] GetChoices(string QuestionPoolID)
        {
            string[] ReturnChoices = new string[] { "choice" };

            DataTable dt = (DataTable)Session["AssessmentQuestion"];
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if ((string)dt.Rows[x]["QuestionPoolID"] == QuestionPoolID)
                {
                    ReturnChoices = new string[] { dt.Rows[x]["Choice1"].ToString(), dt.Rows[x]["Choice2"].ToString(), dt.Rows[x]["Choice3"].ToString(), dt.Rows[x]["Choice4"].ToString() };
                }
            }
            return ReturnChoices;
        }

        //Check if the answer is correct
        public bool isAnswerCorrect(string QuestionPoolID, string SelectedAnswer)
        {
            bool answerstatus = false;

            DataTable dt1 = (DataTable)Session["Remark"];
            DataTable dt2 = (DataTable)Session["CorrectRemark"];

            //Instantiate new list of questions
            oQuestions = new List<Constructors.QuestionPool>(new Collections().getQuestionPool());

            oQuestions.ForEach(qp =>
            {
                if (qp.QuestionPoolID.ToString() == QuestionPoolID)
                {
                   
                    if (qp.CorrectAnswer.ToLower() == SelectedAnswer.ToLower())
                    {
                        answerstatus = true;
                    }
                    else
                    {
                        answerstatus = false;
                    }
                }
            });

            return answerstatus;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_history_assessment_main));
        }

        //Get the corresponding remarks based on the students answer
        public string GetSelectedAnswerRemark(string QuestionPoolID, string SelectedAnswer)
        {
            string tobereturned = "";
            bool remarkfound = false;

            DataTable dt1 = (DataTable)Session["Remark"];
            DataTable dt2 = (DataTable)Session["CorrectRemark"];

            foreach (DataRow dRow in dt1.Rows)
            {
                if ((string)dRow["QuestionID"] == QuestionPoolID && (string)dRow["Choice"] == SelectedAnswer)
                {
                    tobereturned = (string)dRow["Remark"];
                    remarkfound = true;
                }
            }
            if (remarkfound == false)
            {
                foreach (DataRow dRow in dt2.Rows)
                {
                    if ((string)dRow["QuestionID"] == QuestionPoolID && (string)dRow["Choice"] == SelectedAnswer)
                    {
                        tobereturned = (string)dRow["Remark"];
                        remarkfound = true;
                    }
                }
            }

            if (tobereturned == string.Empty)
            {
                tobereturned = "Incorrect.";
            }

            return tobereturned;
        }

        public string FileName = "";
        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnkView = sender as LinkButton;
            Image pic = (Image)PicturePanel.FindControl("panelpicture");
            FileName = lnkView.Attributes["FileName"];

            if (System.IO.File.Exists(Server.MapPath(Defaults.FolderPath + FileName)))
                pic.ImageUrl = Defaults.ImagePath + FileName;
            else
                pic.ImageUrl = Defaults.ImagePath + "NoImage.jpg";

            pic.Visible = true;
            Button1_ModalPopupExtender.Show();
        }

        protected void jsCall(string alert)
        {
            string script = @"alert('" + alert + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", script, true);
        }

        public void jScript(string expression)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsCall", expression, true);
        }
    }
}
