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
    public partial class take_assessment : System.Web.UI.Page
    {        
        ////////////////////////
        //--------------------//
        //--- Declarations ---//
        //--------------------//
        ////////////////////////

        //Instantite New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Instantiate New List of Assessments
        List<Constructors.AssessmentView> AssessmentViewList;
        //Instantiate New List of Assessment Details
        List<Constructors.AssessmentDetails> AssessmentDetailsList;
        //Instantiate New List of Questions
        List<Constructors.QuestionPool> QuestionPoolList;
        //Declare New LoginUser Class
        LoginUser LUser;
        //Declare New CurrentStudent Class
        CurrentStudent CStudent;
        //Instantiate new Global Forms class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate new Collections Class
        Collections cls = new Collections();
        public string RandomQuestion = "";
        public string RandomAnswer = "";

        /////////////////////////
        //---------------------//
        //--- System Events ---//
        //---------------------//
        /////////////////////////

        //Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In
            try
            {
                //Get Login User Info from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                //Get Current Student Info from the Session Variable
                CStudent = (CurrentStudent)Session["CurrentStudent"];

                //check if the user is login and a student
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "S")
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                string Trigger = LUser.Username;
            }
            //Redirect to the Index Page
            catch
            {
                //Redirect to the Index page
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }
        
            if (Convert.ToInt32(Request.QueryString["assid"]) <= 0)
            {
                Response.Redirect(ResolveUrl(Request.RawUrl)+"?assid=1");
            }

            if(IsPostBack == false)
            {
                Debug.WriteLine("--- First Loading of Details ---");
                grdAssessment.PageSize = 10;
                LoadAssessmentDetails();
                ConvertToDataTable();
                LoadQuestionsDetails();
            }
        }

        //Load Assessment Details [Assessment Info]
        public void LoadAssessmentDetails()
        {
            Debug.WriteLine("Load Assessment Details");
            AssessmentViewList = new List<Constructors.AssessmentView>(new Collections().getAssessmentView());
            AssessmentViewList.ForEach(a => 
            {
                if (a.AssessmentID == Convert.ToInt32(Request.QueryString["assid"]))
                {
                    lblAssessmentIntroduction.Text = a.Introduction;
                    lblAssessmentTitle.Text = a.Title;
                    lblSubject.Text = a.SubjectDescription;
                    lblTeacher.Text = a.TeacherFirstname + " " + a.TeacherLastname;
                    lblExamType.Text = a.AssessmentTypeDescription;
                    lblScheduledDate.Text = Convert.ToDateTime(a.DateStart).ToShortDateString() + " to " + Convert.ToDateTime(a.DateEnd).ToShortDateString();
                    lblScheduleTime.Text = Convert.ToDateTime(a.TimeStart).ToShortTimeString() + " to " + Convert.ToDateTime(a.TimeEnd).ToShortTimeString();
                    RandomQuestion = a.RandomQuestions;
                    RandomAnswer = a.RandomAnswer;
                    return;
                }
            });
        }

        //Bind DataSource to GridView
        public void LoadQuestionsDetails()
        {
            Debug.WriteLine("Data Binding");
            //bind data table
            grdAssessment.DataSource = (DataTable)Session["AssessmentTable"];
            grdAssessment.DataBind();
            GridView1.DataSource = Session["AssessmentTable"];
            GridView1.DataBind();

            for (int x = 0; x < grdAssessment.Rows.Count; x++)
            {
                RadioButtonList rboMultipleChoice = (RadioButtonList)grdAssessment.Rows[x].FindControl("rboAnswer");
                Label lblSelectedAnswer = (Label)grdAssessment.Rows[x].FindControl("lblSelectedAnswer");
                TextBox txtAnswer = (TextBox)grdAssessment.Rows[x].FindControl("txtAnswer");
                Label lblQuestionID = (Label)grdAssessment.Rows[x].FindControl("lblQuestionPoolID");

                Label lblFileName = (Label)grdAssessment.Rows[x].FindControl("lblFileName");
                Image loadPicture = (Image)grdAssessment.Rows[x].FindControl("loadPicture");
                LinkButton lnkView = (LinkButton)grdAssessment.Rows[x].FindControl("lnkView");

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

                rboMultipleChoice.DataSource = GetShuffledChoicesFromDataTable(lblQuestionID.Text);
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
            for (int x = 1; x <= grdAssessment.PageCount; x++)
            {
                cboPageNumber.Items.Add(x.ToString());
            }

            cboPageNumber.SelectedIndex = grdAssessment.PageIndex;
            lblPageCount.Text = "of " + grdAssessment.PageCount.ToString();
        }

        //Converts the List of Questions into a DataTable
        public void ConvertToDataTable()
        {
            Debug.WriteLine("Convert to Data Table");
            //Declare new DataTable
            DataTable dt = new DataTable();

            try
            {
                if (Session["ctr"].ToString() == "" || Session["ctr"].ToString() == "0")
                {

                }

                if (Session["AID"].ToString() != Request.QueryString["assid"])
                {
                    Session["ctr"] = "0";
                    Session["AID"] = Request.QueryString["assid"];
                    Debug.WriteLine("New Assessment " + Request.QueryString["assid"] + " and counter " + Session["ctr"].ToString() + "");
                }
                else
                {
                    Session["ctr"] = "1";
                    Session["AID"] = Request.QueryString["assid"];
                }
            }
            catch
            {
                Session["ctr"] = "0";
                Session["AID"] = Request.QueryString["assid"];
            }

            if (Session["ctr"].ToString() == "" || Session["ctr"].ToString() == "0")
            {   
                Debug.WriteLine("Counter: " + Session["ctr"].ToString() + ", Create Data Source, First DataBind");
                Session["ctr"] = "1";
                Session["AID"] = Request.QueryString["assid"];

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
                dt.Columns.Add(new DataColumn("ShuffleID"));
                dt.Columns.Add(new DataColumn("RowOrder"));
                dt.Columns.Add(new DataColumn("FileName"));

                //Instantiate New List of Assessment Details
                AssessmentDetailsList = new List<Constructors.AssessmentDetails>(new Collections().getAssessmentDetails());
                //Instantiate New List of Questions
                QuestionPoolList = new List<Constructors.QuestionPool>(new Collections().getQuestionPool());

                //Loop Through list of Assessment Details
                AssessmentDetailsList.ForEach(ad =>
                {
                    //declare variables to be used
                    int AssessmentID, QuestionPoolID, Points;
                    string Question, CorrectAnswer, CorrectAnswerRemark,
                           Choice1, Choice1Remark, Choice2,
                           Choice2Remark, Choice3, Choice3Remark,
                           Choice4, Choice4Remark, FileName;

                    //if assessment id matched the current assessment
                    if (ad.AssessmentID == Convert.ToInt32(Request.QueryString["assid"]))
                    {
                        AssessmentID = ad.AssessmentID;
                        Points = ad.Points;
                        //loop through list of questions
                        QuestionPoolList.ForEach(qp =>
                        {
                            if (qp.QuestionPoolID == ad.QuestionPoolID)
                            {
                                FileName = qp.ImageFileName;
                                QuestionPoolID = qp.QuestionPoolID;
                                Question = qp.Question;
                                CorrectAnswer = qp.CorrectAnswer;
                                CorrectAnswerRemark = qp.CorrectAnswerRemark;

                                if (RandomAnswer == "True")
                                {
                                    //request a new shuffled choices
                                    string[] ShuffledChoices = (string[])ReshuffleChoices(new string[] { qp.Choice1, qp.Choice2, qp.Choice3, qp.Choice4 });

                                    //get new choices from array of shuffled choices
                                    Choice1 = ShuffledChoices[0];
                                    Choice2 = ShuffledChoices[1];
                                    Choice3 = ShuffledChoices[2];
                                    Choice4 = ShuffledChoices[3];
                                }
                                else
                                {
                                    Choice1 = qp.Choice1;
                                    Choice2 = qp.Choice2;
                                    Choice3 = qp.Choice3;
                                    Choice4 = qp.Choice4;
                                }

                                //add remarks
                                Choice1Remark = qp.Choice1Remark;
                                Choice2Remark = qp.Choice2Remark;
                                Choice3Remark = qp.Choice3Remark;
                                Choice4Remark = qp.Choice4Remark;

                                //add new row to the datatable
                                dt.Rows.Add(AssessmentID, QuestionPoolID, Points, Question, CorrectAnswer, CorrectAnswerRemark, Choice1, Choice1Remark, Choice2, Choice2Remark, Choice3, Choice3Remark, Choice4, Choice4Remark, "", "", "", FileName);

                                //stop looping
                                //return;
                            }
                            //

                        });
                        //stop looping
                        //return;
                    }
                });

                //set session variable to the datatable
                Session["AssessmentTable"] = dt;

                if (RandomQuestion == "True")
                    //Shuffle List of Questions
                    ReshuffleQuestions();
                else
                    NonShuffleQuestions();

                //Reset Questions per page dropdownlist
                cboQuestionsPerPage.Items.Clear();
                for (int x = 1; x <= dt.Rows.Count; x++)
                {
                    cboQuestionsPerPage.Items.Add(x.ToString());
                }

                //Reset Page Number dropdownlist
                cboPageNumber.Items.Clear();
                for (int x = 1; x <= grdAssessment.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }

                //set the currently selected index of the PageNumber DropDownList to the Current Page Index
                cboPageNumber.SelectedIndex = grdAssessment.PageIndex;
                //set the total page count of the current page
                lblPageCount.Text = "of " + grdAssessment.PageCount.ToString();
            }
            else
            {
                Debug.WriteLine("Counter: " + Session["ctr"] + ", Just Bind the Table");
            }
        }

        //Submit Button
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Validate For Unanswered Questions
            if (ValidateAnswers())
            {
                //Record Answers to the Database
                RecordAnswers();
            }
            else
            {
                jsCall("Some questions are still left unanswered.\\r\\nPlease review your answers before submitting.");
            }
        }

        bool isValidSelectedAnswer()
        {
            int count = 0;
            Debug.WriteLine("Validate Student's Answers: ");
            for (int x = 0; x < grdAssessment.Rows.Count; x++)
            {
                RadioButtonList rboMultipleChoice = (RadioButtonList)grdAssessment.Rows[x].FindControl("rboAnswer");
                Label lblSelectedAnswer = (Label)grdAssessment.Rows[x].FindControl("lblSelectedAnswer");
                TextBox txtAnswer = (TextBox)grdAssessment.Rows[x].FindControl("txtAnswer");
                Label lblQuestionID = (Label)grdAssessment.Rows[x].FindControl("lblQuestionPoolID");

                Label lblFileName = (Label)grdAssessment.Rows[x].FindControl("lblFileName");
                Image loadPicture = (Image)grdAssessment.Rows[x].FindControl("loadPicture");
                LinkButton lnkView = (LinkButton)grdAssessment.Rows[x].FindControl("lnkView");

                if (txtAnswer.Text != "")
                {
                    Debug.WriteLine(lblQuestionID.Text + ": " + txtAnswer.Text);
                    count++;
                }
                else
                {
                    for (int i = 0; i < rboMultipleChoice.Items.Count; i++)
                    {
                        if (rboMultipleChoice.Items[i].Selected == true)
                        {
                            Debug.WriteLine(lblQuestionID.Text + ": " + rboMultipleChoice.Items[i].Text);
                            count++;
                        }
                    }
                }
            }

            Debug.WriteLine("Total Items: " + (grdAssessment.Rows.Count) + ", Items with Answer: " + count.ToString());
            if (count != grdAssessment.Rows.Count)
                return false;
            else
                return true;
        }
        ///////////////////////////
        //-----------------------//
        //--- GridView Events ---//
        //-----------------------//
        ///////////////////////////

        //executed when row has been databound
        protected void grdAssessment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Debug.WriteLine("Row Data Bound");
            
        }

        //executed when the radio button list's value has been changed
        protected void rboMultipleChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Label1.Text = Label1.Text + ",1";
            RadioButtonList source = (RadioButtonList)sender;
            GridViewRow gRow = (GridViewRow)source.NamingContainer;
            Label lblQuestionID = (Label)gRow.FindControl("lblQuestionPoolID");
            UpdateSelectedAnswers(lblQuestionID.Text, source.SelectedValue);
            LoadQuestionsDetails();
        }

        //change the values of the selected answer column in the datatable  [textbox]
        protected void txtAnswer_TextChanged(object sender, EventArgs e)
        {
            TextBox source = (TextBox)sender;
            GridViewRow gRow = (GridViewRow)source.NamingContainer;
            Label lblQuestionID = (Label)gRow.FindControl("lblQuestionPoolID");
            UpdateSelectedAnswers(lblQuestionID.Text, source.Text);
            LoadQuestionsDetails();
        } 

        //previous page
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (grdAssessment.PageIndex > 0)
            {
                grdAssessment.PageIndex = grdAssessment.PageIndex - 1;
                LoadQuestionsDetails();
            }
        }

        //next page
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (grdAssessment.PageIndex <= grdAssessment.PageCount)
            {
                grdAssessment.PageIndex = grdAssessment.PageIndex + 1;
                LoadQuestionsDetails();
            }
        }

        //change the values of the selected answer column in the datatable [radio button list]
        public void UpdateSelectedAnswers(string QuestionID,string SelectedAnswer)
        {
            DataTable dt = (DataTable)Session["AssessmentTable"];
            
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if ((string)dt.Rows[x]["QuestionPoolID"] == QuestionID)
                {
                    dt.Rows[x]["SelectedAnswer"] = SelectedAnswer;
                    //Debug.WriteLine("Answers Changed: " + SelectedAnswer);
                }
            }

            dt.AcceptChanges();            
            Session["AssessmentTable"] = dt;

            Debug.WriteLine("SELECT ANSWERS:");
            foreach (DataRow dr in dt.Rows)
            {
                Debug.WriteLine(dr["SelectedAnswer"]);
            }
        }

        //first page linkbutton
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            grdAssessment.PageIndex = 0;
            LoadQuestionsDetails();
        }

        //last page linkbutton
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            grdAssessment.PageIndex = grdAssessment.PageCount;
            LoadQuestionsDetails();
        }
        
        //page number dropdownlist
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAssessment.PageIndex = cboPageNumber.SelectedIndex;
            LoadQuestionsDetails();
        }
        
        //questions per page dropdownlist
        protected void cboQuestionsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAssessment.PageSize = cboQuestionsPerPage.SelectedIndex + 1;
            LoadQuestionsDetails();
        }

        /////////////////////
        //-----------------//
        //--- Functions ---//
        //-----------------//
        /////////////////////

        //return the shuffled string array based on the input
        public string[] ReshuffleChoices(string[] Choices)
        {
            string[] ReturnChoices;

            for (int x = 0; x < Choices.Length; x++)
            {
                //Debug.WriteLine("Array [" + x.ToString() + "]: " + Choices[x].ToString());
            }

            Random rng = new Random();
            for (int i = Choices.Length -1; i > 0; i--)
            {
                int swapIndex = rng.Next(i + 1);
                if (swapIndex != i)
                {
                    object tmp = Choices[swapIndex];
                    Choices[swapIndex] = Choices[i];
                    Choices[i] = tmp.ToString();
                }
            }

            for (int x = 0; x < Choices.Length; x++)
            {
                //Debug.WriteLine("Array [" + x.ToString() + "]: " + Choices[x].ToString());
            }

            ReturnChoices = Choices;

            return ReturnChoices;
        }

        //get shuffled choices from the data table
        public string[] GetShuffledChoicesFromDataTable(string QuestionPoolID)
        {
            string[] ReturnChoices = new string[] { "haha" };

            DataTable dt = (DataTable)Session["AssessmentTable"];
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if ((string)dt.Rows[x]["QuestionPoolID"] == QuestionPoolID)
                {
                    ReturnChoices = new string[] { dt.Rows[x]["Choice1"].ToString(), dt.Rows[x]["Choice2"].ToString(), dt.Rows[x]["Choice3"].ToString(), dt.Rows[x]["Choice4"].ToString() };
                }
            }            
            return ReturnChoices;
        }
                      
        //shuffle the questions loaded from the database
        public DataTable ReshuffleQuestions()
        {
            DataTable Input = (DataTable)Session["AssessmentTable"];

            Random rnd = new Random();
            for (int x = 0; x < Input.Rows.Count; x++)
            {
                Input.Rows[x]["ShuffleID"] = rnd.Next(3500);
            }
                        
            Input.DefaultView.Sort = "ShuffleID ASC";
            DataTable dt = Input.DefaultView.ToTable();

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                dt.Rows[x]["RowOrder"] = Convert.ToString(x + 1);
            }

            Session["AssessmentTable"] = dt;
            
            return dt;
        }

        public DataTable NonShuffleQuestions()
        {
            DataTable dt = (DataTable)Session["AssessmentTable"];

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                dt.Rows[row]["RowOrder"] = Convert.ToString(row + 1);
            }

            Session["AssessmentTable"] = dt;

            return dt;
        }

        //validate for unanswered questions
        public bool ValidateAnswers()
        {
            bool status = true;

            DataTable dt = (DataTable)Session["AssessmentTable"];

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Validator.isEmpty(dt.Rows[x]["SelectedAnswer"].ToString()))
                {
                    status = false;
                }
            }

            return status;
        }

        //////////////////////////////
        //--------------------------//
        //--- Special Procedures ---//
        //--------------------------//
        //////////////////////////////

        //Record the Answers to the Database
        public void RecordAnswers()
        {
            if (ValidateAnswers())
            {
                DataTable dt = (DataTable)Session["AssessmentTable"];
                foreach (DataRow dRow in dt.Rows)
                {
                    string qry = "INSERT INTO [PaceAssessment].dbo.StudentAnswers(StudentID,SchoolYear,AssessmentID,QuestionPoolID,SelectedAnswer,LastUpdateUser,LastUpdateDate) " +
                                 "VALUES(" + CStudent.StudentID + ",'" + CStudent.SchoolYear + "'," + Request.QueryString["assid"] + ",'" + dRow["QuestionPoolID"] + "','" + dRow["SelectedAnswer"] + "','" + LUser.Username +"',GetDate())";
                    Debug.WriteLine(qry);
                    if (cls.ExecuteNonQuery(qry) > 0)
                    {
                        //jsCall("Your answers have been sent successfully.");
                        //Response.Redirect(ResolveUrl(DefaultForms.frm_assessment_success) + "?assid=" + Request.QueryString["assid"]);
                        jScript("alert('Your answers have been submitted successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_assessment_success) + "?assid=" + Request.QueryString["assid"] + "';");
                    }
                    else
                    {
                        jsCall("Action cannot proceed.\\r\\nPlease check your entry");
                    }
                }
            }
            else
            {             
                jsCall("Some questions are still left unanswered.\\r\\nPlease review your answers before submitting.");
            }
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
            Debug.WriteLine(pic.ImageUrl);
            Button1_ModalPopupExtender.Show();
        }
    }
}
