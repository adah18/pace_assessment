using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Data.SqlClient;
using PAOnlineAssessment.Classes;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.IO;


namespace PAOnlineAssessment.parent
{
    public partial class parent_view_grades : System.Web.UI.Page
    {
        //Declare Collection
        Collections cls = new Collections();
        //Declare List for Stduent Registration View
        List<Constructors.StudentRegistrationView> StudentList = new List<Constructors.StudentRegistrationView>(new Collections().getStudentRegistrationView());
        //Declare list for grading view
        List<Constructors.GradingView> GradingView = new List<Constructors.GradingView>(new Collections().getGradingView());
        //Declare List for assessment type
        List<Constructors.Assessment> AssessmentType;
        //declare list for assessment list
        List<Constructors.Assessment> AssessmentList;
        //Declare List of Answer List
        List<Constructors.StudentAnswers> StudentAnswersList;
        //Declare List of StudentAccounts
        List<Constructors.StudentAccount> StudentAccountList;
        //Declare Lits of Assessment Details
        List<Constructors.AssessmentDetails> AssessmentDetailsList;

        SystemProcedures sp = new SystemProcedures();
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        List<Constructors.ParentChildGrades> ChildList = new List<Constructors.ParentChildGrades>(new Collections().GetChild());

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In
            try
            {
                //Get Login User Info from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "P")
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }
                //Get Current Student Info from the Session Variable
                //CStudent = (CurrentStudent)Session["CurrentStudent"];
                string Trigger = LUser.Username;
            }
            //Redirect to the Index Page
            catch
            {
                //Redirect to the Index page
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            if (!IsPostBack)
            {
                //load all child that has been selected by the parent
                LoadChild();
                cboSubjects.Items.Add(new ListItem("--- Select Subject ---", "0"));

                //add a datasource to the main gridview to show that nothing has been selected yet
                DataTable dt = new DataTable();
                dt.Columns.Add("Blank Column");
                grdStudentsList.DataSource = dt;
                grdStudentsList.DataBind();
            }
        }

        //function for loading all the student that has been selected by the parent in the combobox
        void LoadChild()
        {
            cboChild.Items.Clear();
            cboChild.Items.Add(new ListItem("--- Select Child's Name ---", "0"));  
            StudentList.ForEach(sl => 
            {
                ChildList.ForEach(cl => 
                {
                    if (sl.SchoolYear == Session["CurrentSchoolYear"].ToString() && cl.StudentID == sl.StudentID && cl.ParentUserID == LUser.UserID && cl.Status!="D")
                    {
                        cboChild.Items.Add(new ListItem(sl.LastName + ", " + sl.FirstName, sl.StudentID.ToString()));  
                    }
                });
            });
        }

        protected void cboChild_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Call a function that get the current level of the selected student and section also
            GetLevelandSection();

            //load all the subject of the selected student
            LoadAllSubjects();
        }


        //function for getting the current level and section
        void GetLevelandSection()
        {
            //clear the details 1st
            hidSection.Value = "0";
            hidLevel.Value = "0";

            //loop through the list
            StudentList.ForEach(sl =>
            {
                //check the student id
                if (sl.StudentID.ToString() == cboChild.SelectedValue)
                {
                    string LevelID = cls.ExecuteScalar("SELECT LevelID FROM PaceRegistration.dbo.Student WHERE StudentNumber='" + sl.StudentNumber + "'");
                    if (!string.IsNullOrEmpty(LevelID))
                        hidLevel.Value = LevelID;
                    //get the section and level
                    hidSection.Value = sl.SectionID.ToString();
                    return;
                }
            });
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //check the qhat quarter was selected
            if (cboQuarter.SelectedValue != "0")
            {
                remove_datasource();
                switch (cboQuarter.SelectedValue.ToString())
                {
                    case "1st":
                        VGetAssessmentTypes();
                        VGetAllSubitems();
                        grdStudentsList.Visible = true;
                        break;
                    case "2nd":
                        VGetAssessmentTypes_2nd();
                        VGetAllSubitems_2nd();
                        grdStudentsList_2nd.Visible = true;
                        break;
                    case "3rd":

                        VGetAssessmentTypes_3rd();
                        VGetAllSubitems_3rd();
                        grdStudentsList_3rd.Visible = true;
                        break;
                    case "4th":
                        VGetAssessmentTypes_4th();
                        VGetAllSubitems_4th();
                        grdStudentsList_4th.Visible = true;
                        break;
                }
            }
        }

        void remove_datasource()
        {
            grdStudentsList.DataSource = "";
            grdStudentsList.DataBind();
            grdStudentsList.Visible = false;

            grdStudentsList_2nd.DataSource = "";
            grdStudentsList_2nd.DataBind();
            grdStudentsList_2nd.Visible = false;

            grdStudentsList_3rd.DataSource = "";
            grdStudentsList_3rd.DataBind();
            grdStudentsList_3rd.Visible = false;

            grdStudentsList_4th.DataSource = "";
            grdStudentsList_4th.DataBind();
            grdStudentsList_4th.Visible = false;
        }
        void LoadAllSubjects()
        {
            //clear the subjects
            cboSubjects.Items.Clear();
            cboSubjects.Items.Add(new ListItem("--- Select Subject ---", "0"));

            //Create a DataTable containing the SubjectID and SubjectDescription
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SubjectID"));
            dt.Columns.Add(new DataColumn("SubjectDescription"));

            //GradingView.ForEach(gv =>
            //{
            //    if (gv.LevelID.ToString() == hidLevel.Value && gv.SectionID.ToString() == hidSection.Value)
            //    {
            //        Debug.WriteLine("LevelID: " + hidLevel.Value + ", SectionID: " + hidSection.Value);
            //        cboSubjects.Items.Add(new ListItem(gv.Description, gv.SubjectID.ToString()));
            //    }
            //});

            //Loop through List of Subject
            GradingView.ForEach(gv =>
            {
                if (gv.SchoolYear == Session["CurrentSchoolYear"].ToString() && gv.LevelID.ToString() == hidLevel.Value)
                {
                    //Add SubjectID and SubjectDescription to the Datatable
                    dt.Rows.Add(gv.SubjectID, gv.Description);
                }
            });

            //Loop through Filtered Duplicate values in the DataTable
            foreach (DataRow dRow in dt.DefaultView.ToTable(true, new string[] { "SubjectID", "SubjectDescription" }).Rows)
            {
                cboSubjects.Items.Add(dRow[1].ToString());
                cboSubjects.Items[cboSubjects.Items.Count - 1].Value = dRow[0].ToString();
            }

        }

        protected void cboSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region "Code for Vertical Grid"

        void GetStudentnumberAndSection()
        {
            StudentAccountList = new List<Constructors.StudentAccount>(cls.getStudentAccounts());
            StudentAccountList.ForEach(sl =>
            {

                if (LUser.UserID.ToString() == sl.StudentID.ToString())
                {
                    StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
                    StudentList.ForEach(st =>
                    {
                        if (st.SchoolYear == Session["CurrentSchoolYear"].ToString() && st.StudentNumber == sl.StudentNumber)
                        {
                            Session["StudentID"] = st.StudentID;
                            Session["SectionID"] = st.SectionID;
                            hidStudentNumber.Value = st.StudentID.ToString();
                            return;
                        }
                    });
                }
            });

            //Response.Write("<script>alert('" + StudentIDz + "');</script>");
        }

        // IN THIS PART ALL CODES ARE THE SAME BUT DIFFERENT IN QUARTERS
        #region "for 1st Quarter"
        void VGetAssessmentTypes()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(hidSection.Value);
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
                //getting assessmenttypeid in table assessment
                //Debug.WriteLine(at.Quarter + "<- Quarter");
                dt.Rows.Add(at.AssessmentTypeID.ToString(), "-");
            });

            //get the title for the assessment type
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + " ");

                if (title != "")
                {
                    //if title is not empty then add the title to the data table
                    dt.Rows[i][1] = title;

                    //dt.Rows[i][2] = "1st";
                }
            }

            //pass the value of the data table to session
            Session["AssessmentTypes"] = dt;
            grdStudentsList.DataSource = Session["AssessmentTypes"];
            grdStudentsList.DataBind();
            //grdView.DataBind();

        }


        void VGetAllSubitems()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(hidSection.Value);
            //get the value for the session
            DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes"];
            //create data table
            DataTable dt = new DataTable();
            //create column
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmentTypeID");
            //search for subitem that will match the selected subject and assessment typeid
            for (int i = 0; i < AssessmentTypes.Rows.Count; i++)
            {
                AssessmentType = new List<Constructors.Assessment>(cls.getAssessment());
                AssessmentType.ForEach(at =>
                {
                    if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID)
                    {
                        if (at.Quarter == "1st")
                        {
                            dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                        }
                    }
                });
            }
            //pass the data to a session
            Session["Subitems"] = dt;
            //grdView.DataSource = Session["Subitems"];
            //grdView.DataBind();
            vGetScore();
            GenerateGrid();
        }
        //generate the main grid view pass all the value needed.
        void GenerateGrid()
        {
            grdStudentsList.Columns[0].HeaderText = "1st Quarter";

            for (int i = 0; i < grdStudentsList.Rows.Count; i++)
            {
                GridView grdScore = (GridView)grdStudentsList.Rows[i].FindControl("grdScores");
                DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes"];
                DataTable Subitems = (DataTable)Session["Subitems"];
                DataTable Scores = (DataTable)Session["Scores"];
                Label lblTitle = (Label)grdStudentsList.Rows[i].FindControl("lblTitle");
                DataTable dt = new DataTable();
                dt.Columns.Add("Description");
                dt.Columns.Add("Score");
                dt.Columns.Add("Status");
                dt.Columns.Add("DateTaken");
                dt.Columns.Add("Quarter");
                dt.Columns.Add("Total");
                for (int j = 0; j < AssessmentTypes.Rows.Count; j++)
                {
                    if (lblTitle.Text == AssessmentTypes.Rows[j][1].ToString())
                    {
                        Debug.WriteLine("GridRow:" + i.ToString() + " title: " + AssessmentTypes.Rows[j][1].ToString());

                        for (int k = 0; k < Scores.Rows.Count; k++)
                        {
                            if (AssessmentTypes.Rows[j][0].ToString() == Scores.Rows[k][4].ToString())
                            {
                                dt.Rows.Add(Scores.Rows[k][3].ToString(), Scores.Rows[k][2].ToString(), Scores.Rows[k][5].ToString(), Scores.Rows[k][7].ToString(), Scores.Rows[k][8].ToString(), GetTotalPoint(Convert.ToInt32(Scores.Rows[k][1])));
                            }
                        }
                    }
                    else
                    { }

                }
                grdScore.DataSource = dt;
                grdScore.DataBind();
            }
        }
        //Get Total Point for every Assessment
        int GetTotalPoint(int assessmentid)
        {
            int total = 0;
            AssessmentDetailsList = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());
            AssessmentDetailsList.ForEach(ad =>
            {
                if (ad.AssessmentID == assessmentid)
                {
                    total += ad.Points;
                }
            });
            Debug.WriteLine(total.ToString() + " <= Total Points");
            return total;
        }
        //Store all taken assessment
        void GetAssessmentTaken()
        {
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems"];
            //data table for all assessment that has been taken
            DataTable dt_taken = new DataTable();
            dt_taken.Columns.Add("AssessmentID");
            for (int i = 0; i < 1; i++)
            {

                for (int j = 0; j < Subitems.Rows.Count; j++)
                {

                    StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                    StudentAnswersList.ForEach(sa =>
                    {
                        //Debug.WriteLine(StudentIDz);
                        if (sa.StudentID.ToString() == cboChild.SelectedValue)
                        {
                            //Get the score if one of the assessment id from database match the assessment id from session
                            if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                            {
                                dt_taken.Rows.Add(sa.AssessmentID);
                            }
                        }
                    });
                    Session["AssessmentTaken"] = dt_taken;
                }
            }
            //grdView.DataSource = Session["AssessmentTaken"];
            //grdView.DataBind();
        }
        //checking if the assessment is taken
        bool isTaken(string assess_id)
        {
            bool x = false;
            DataTable dt = (DataTable)Session["AssessmentTaken"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (assess_id == dt.Rows[i][0].ToString())
                {
                    x = true;
                }
            }
            return x;
        }

        void vGetScore()
        {
            GetAssessmentTaken();
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems"];
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Score");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmenTypeID");
            dt.Columns.Add("Status");
            dt.Columns.Add("ExamDate");
            dt.Columns.Add("DateTaken");
            dt.Columns.Add("Quarter");
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < Subitems.Rows.Count; j++)
                {
                    DateTime DateStart = DateTime.Now;
                    DateTime DateEnd = DateTime.Now;
                    DateTime TimeEnd = DateTime.Now;
                    DateTime DateTaken = DateTime.Now;

                    string qtr = "", date_blank;
                    int AID_grid = Convert.ToInt32(Subitems.Rows[j][0]);
                    int AID_data = 0;
                    string status = "";
                    string final_score = "0";
                    int studentID = Convert.ToInt32(cboChild.SelectedValue);
                    //Pass the current assessment id from session
                    int x = 0;

                    AssessmentList = new List<Constructors.Assessment>(cls.getAssessment());
                    AssessmentList.ForEach(al =>
                    {


                        if (Convert.ToInt32(Subitems.Rows[j][0]) == al.AssessmentID)
                        {
                            if (isTaken(al.AssessmentID.ToString()))
                            {
                                x = 1;
                                status = "Taken";
                                qtr = al.Quarter;
                            }
                            else
                            {
                                x = 0;
                                string DateS = Convert.ToDateTime(al.DateStart).ToShortDateString() + " " + al.TimeStart;
                                DateStart = Convert.ToDateTime(DateS);
                                string DateE = Convert.ToDateTime(al.DateEnd).ToShortDateString() + " " + al.TimeEnd;
                                DateEnd = Convert.ToDateTime(DateE);
                                AID_data = al.AssessmentID;
                                qtr = al.Quarter;
                            }
                        }

                    });

                    if (x == 0)
                    {
                        if (DateStart > DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not yet taken");
                            final_score = "-";
                            status = "Not Yet Taken";


                        }
                        else if (DateEnd < DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not taken");
                            final_score = "0";
                            status = "Not Taken";
                        }
                        date_blank = "-";
                    }
                    else
                    {
                        double score = 0;
                        Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is taken");
                        StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                        StudentAnswersList.ForEach(sa =>
                        {
                            if (sa.StudentID.ToString() == cboChild.SelectedValue)
                            {
                                //Get the score if one of the assessment id from database match the assessment id from session
                                if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                                {
                                    string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                    if (ans == sa.SelectedAnswer)
                                    {
                                        //add score if the answer is correct
                                        string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + "");
                                        score += Convert.ToDouble(points);
                                    }
                                    else
                                    {
                                        //add zero to the score if the answer is wrong
                                        score += 0;
                                    }
                                    studentID = sa.StudentID;
                                    AID_data = sa.AssessmentID;
                                    DateTaken = Convert.ToDateTime(sa.LastUpdateDate);

                                }
                            }
                        });

                        final_score = score.ToString();
                        date_blank = DateTaken.ToShortDateString();
                    }
                    if (AID_data == AID_grid)
                    {
                        //if the assesment id from database and session match the score will be added to the list
                        dt.Rows.Add(cboChild.SelectedValue, Subitems.Rows[j][0].ToString(), final_score, Subitems.Rows[j][1].ToString(), Subitems.Rows[j][2].ToString(), status, DateStart.ToShortDateString(), date_blank, qtr);
                    }
                }
            }

            Session["Scores"] = dt;
            //grdView.DataSource = Session["Scores"];
            //grdView.DataBind();
        }
        #endregion

        #region "for 2nd quarter"
        void VGetAssessmentTypes_2nd()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(hidSection.Value);
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
                //getting assessmenttypeid in table assessment
                //Debug.WriteLine(at.Quarter + "<- Quarter");
                dt.Rows.Add(at.AssessmentTypeID.ToString(), "-");
            });

            //get the title for the assessment type
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + " ");

                if (title != "")
                {
                    //if title is not empty then add the title to the data table
                    dt.Rows[i][1] = title;

                    //dt.Rows[i][2] = "1st";
                }
            }

            //pass the value of the data table to session
            Session["AssessmentTypes_2nd"] = dt;
            grdStudentsList_2nd.DataSource = Session["AssessmentTypes_2nd"];
            grdStudentsList_2nd.DataBind();
            //grdView.DataBind();

        }


        void VGetAllSubitems_2nd()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(hidSection.Value);
            //get the value for the session
            DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_2nd"];
            //create data table
            DataTable dt = new DataTable();
            //create column
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmentTypeID");
            //search for subitem that will match the selected subject and assessment typeid
            for (int i = 0; i < AssessmentTypes.Rows.Count; i++)
            {
                AssessmentType = new List<Constructors.Assessment>(cls.getAssessment());
                AssessmentType.ForEach(at =>
                {
                    if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID)
                    {
                        if (at.Quarter == "2nd")
                        {
                            dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                        }

                    }
                });
            }
            //pass the data to a session
            Session["Subitems_2nd"] = dt;
            //grdView.DataSource = Session["Subitems"];
            //grdView.DataBind();
            vGetScore_2nd();
            GenerateGrid_2nd();
        }
        //generate the main grid view pass all the value needed.
        void GenerateGrid_2nd()
        {
            grdStudentsList_2nd.Columns[0].HeaderText = "2nd Quarter";
            for (int i = 0; i < grdStudentsList_2nd.Rows.Count; i++)
            {
                GridView grdScore = (GridView)grdStudentsList_2nd.Rows[i].FindControl("grdScores_2nd");
                DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_2nd"];
                DataTable Subitems = (DataTable)Session["Subitems_2nd"];
                DataTable Scores = (DataTable)Session["Scores_2nd"];
                Label lblTitle = (Label)grdStudentsList_2nd.Rows[i].FindControl("lblTitle_2nd");
                DataTable dt = new DataTable();
                dt.Columns.Add("Description");
                dt.Columns.Add("Score");
                dt.Columns.Add("Status");
                dt.Columns.Add("DateTaken");
                dt.Columns.Add("Quarter");
                dt.Columns.Add("Total");
                for (int j = 0; j < AssessmentTypes.Rows.Count; j++)
                {

                    if (lblTitle.Text == AssessmentTypes.Rows[j][1].ToString())
                    {
                        Debug.WriteLine("GridRow:" + i.ToString() + " title: " + AssessmentTypes.Rows[j][1].ToString());

                        for (int k = 0; k < Scores.Rows.Count; k++)
                        {
                            if (AssessmentTypes.Rows[j][0].ToString() == Scores.Rows[k][4].ToString())
                            {
                                dt.Rows.Add(Scores.Rows[k][3].ToString(), Scores.Rows[k][2].ToString(), Scores.Rows[k][5].ToString(), Scores.Rows[k][7].ToString(), Scores.Rows[k][8].ToString(), GetTotalPoint_2nd(Convert.ToInt32(Scores.Rows[k][1])));
                            }
                        }
                    }
                    else
                    { }

                }
                grdScore.DataSource = dt;
                grdScore.DataBind();
            }
        }
        //Get Total Point for every Assessment
        int GetTotalPoint_2nd(int assessmentid)
        {
            int total = 0;
            AssessmentDetailsList = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());
            AssessmentDetailsList.ForEach(ad =>
            {
                if (ad.AssessmentID == assessmentid)
                {
                    total += ad.Points;
                }
            });
            Debug.WriteLine(total.ToString() + " <= Total Points");
            return total;
        }
        //Store all taken assessment
        void GetAssessmentTaken_2nd()
        {
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems_2nd"];
            //data table for all assessment that has been taken
            DataTable dt_taken = new DataTable();
            dt_taken.Columns.Add("AssessmentID");
            for (int i = 0; i < 1; i++)
            {

                for (int j = 0; j < Subitems.Rows.Count; j++)
                {

                    StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                    StudentAnswersList.ForEach(sa =>
                    {
                        //Debug.WriteLine(StudentIDz);
                        if (sa.StudentID.ToString() == cboChild.SelectedValue)
                        {
                            //Get the score if one of the assessment id from database match the assessment id from session
                            if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                            {
                                dt_taken.Rows.Add(sa.AssessmentID);
                            }
                        }
                    });
                    Session["AssessmentTaken_2nd"] = dt_taken;
                }
            }
            //grdView.DataSource = Session["AssessmentTaken"];
            //grdView.DataBind();
        }
        //checking if the assessment is taken
        bool isTaken_2nd(string assess_id)
        {
            bool x = false;
            DataTable dt = (DataTable)Session["AssessmentTaken_2nd"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (assess_id == dt.Rows[i][0].ToString())
                {
                    x = true;
                }
            }
            return x;
        }

        void vGetScore_2nd()
        {
            GetAssessmentTaken_2nd();
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems_2nd"];
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Score");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmenTypeID");
            dt.Columns.Add("Status");
            dt.Columns.Add("ExamDate");
            dt.Columns.Add("DateTaken");
            dt.Columns.Add("Quarter");
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < Subitems.Rows.Count; j++)
                {
                    DateTime DateStart = DateTime.Now;
                    DateTime DateEnd = DateTime.Now;
                    DateTime TimeEnd = DateTime.Now;
                    DateTime DateTaken = DateTime.Now;

                    string qtr = "", date_blank;
                    int AID_grid = Convert.ToInt32(Subitems.Rows[j][0]);
                    int AID_data = 0;
                    string status = "";
                    string final_score = "0";
                    int studentID = Convert.ToInt32(cboChild.SelectedValue);
                    //Pass the current assessment id from session
                    int x = 0;

                    AssessmentList = new List<Constructors.Assessment>(cls.getAssessment());
                    AssessmentList.ForEach(al =>
                    {

                        if (Convert.ToInt32(Subitems.Rows[j][0]) == al.AssessmentID)
                        {
                            if (isTaken_2nd(al.AssessmentID.ToString()))
                            {
                                x = 1;
                                status = "Taken";
                                qtr = al.Quarter;
                            }
                            else
                            {
                                x = 0;
                                string DateS = Convert.ToDateTime(al.DateStart).ToShortDateString() + " " + al.TimeStart;
                                DateStart = Convert.ToDateTime(DateS);
                                string DateE = Convert.ToDateTime(al.DateEnd).ToShortDateString() + " " + al.TimeEnd;
                                DateEnd = Convert.ToDateTime(DateE);
                                AID_data = al.AssessmentID;
                                qtr = al.Quarter;
                            }
                        }

                    });

                    if (x == 0)
                    {
                        if (DateStart > DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not yet taken");
                            final_score = "-";
                            status = "Not Yet Taken";


                        }
                        else if (DateEnd < DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not taken");
                            final_score = "0";
                            status = "Not Taken";
                        }
                        date_blank = "-";
                    }
                    else
                    {
                        double score = 0;
                        Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is taken");
                        StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                        StudentAnswersList.ForEach(sa =>
                        {
                            if (sa.StudentID.ToString() == cboChild.SelectedValue)
                            {
                                //Get the score if one of the assessment id from database match the assessment id from session
                                if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                                {
                                    string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                    if (ans == sa.SelectedAnswer)
                                    {
                                        //add score if the answer is correct
                                        string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + "");
                                        score += Convert.ToDouble(points);
                                    }
                                    else
                                    {
                                        //add zero to the score if the answer is wrong
                                        score += 0;
                                    }
                                    studentID = sa.StudentID;
                                    AID_data = sa.AssessmentID;
                                    DateTaken = Convert.ToDateTime(sa.LastUpdateDate);

                                }
                            }
                        });

                        final_score = score.ToString();
                        date_blank = DateTaken.ToShortDateString();
                    }
                    if (AID_data == AID_grid)
                    {
                        //if the assesment id from database and session match the score will be added to the list
                        dt.Rows.Add(cboChild.SelectedValue, Subitems.Rows[j][0].ToString(), final_score, Subitems.Rows[j][1].ToString(), Subitems.Rows[j][2].ToString(), status, DateStart.ToShortDateString(), date_blank, qtr);
                    }
                }
            }

            Session["Scores_2nd"] = dt;
            //grdView.DataSource = Session["Scores"];
            //grdView.DataBind();
        }
        #endregion

        #region "for 3rd quarter"
        void VGetAssessmentTypes_3rd()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(hidSection.Value);
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
                //getting assessmenttypeid in table assessment
                //Debug.WriteLine(at.Quarter + "<- Quarter");
                dt.Rows.Add(at.AssessmentTypeID.ToString(), "-");
            });

            //get the title for the assessment type
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + " ");

                if (title != "")
                {
                    //if title is not empty then add the title to the data table
                    dt.Rows[i][1] = title;

                    //dt.Rows[i][2] = "1st";
                }
            }

            //pass the value of the data table to session
            Session["AssessmentTypes_3rd"] = dt;
            grdStudentsList_3rd.DataSource = Session["AssessmentTypes_3rd"];
            grdStudentsList_3rd.DataBind();
            //grdView.DataBind();

        }


        void VGetAllSubitems_3rd()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(hidSection.Value);
            //get the value for the session
            DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_3rd"];
            //create data table
            DataTable dt = new DataTable();
            //create column
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmentTypeID");
            //search for subitem that will match the selected subject and assessment typeid
            for (int i = 0; i < AssessmentTypes.Rows.Count; i++)
            {
                AssessmentType = new List<Constructors.Assessment>(cls.getAssessment());
                AssessmentType.ForEach(at =>
                {
                    if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID)
                    {
                        if (at.Quarter == "3rd")
                        {
                            dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                        }
                    }
                });
            }
            //pass the data to a session
            Session["Subitems_3rd"] = dt;
            //grdView.DataSource = Session["Subitems"];
            //grdView.DataBind();
            vGetScore_3rd();
            GenerateGrid_3rd();
        }
        //generate the main grid view pass all the value needed.
        void GenerateGrid_3rd()
        {
            grdStudentsList_3rd.Columns[0].HeaderText = "3rd Quarter";
            for (int i = 0; i < grdStudentsList_3rd.Rows.Count; i++)
            {
                GridView grdScore = (GridView)grdStudentsList_3rd.Rows[i].FindControl("grdScores_3rd");
                DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_3rd"];
                DataTable Subitems = (DataTable)Session["Subitems_3rd"];
                DataTable Scores = (DataTable)Session["Scores_3rd"];
                Label lblTitle = (Label)grdStudentsList_3rd.Rows[i].FindControl("lblTitle_3rd");
                DataTable dt = new DataTable();
                dt.Columns.Add("Description");
                dt.Columns.Add("Score");
                dt.Columns.Add("Status");
                dt.Columns.Add("DateTaken");
                dt.Columns.Add("Quarter");
                dt.Columns.Add("Total");
                for (int j = 0; j < AssessmentTypes.Rows.Count; j++)
                {

                    if (lblTitle.Text == AssessmentTypes.Rows[j][1].ToString())
                    {
                        Debug.WriteLine("GridRow:" + i.ToString() + " title: " + AssessmentTypes.Rows[j][1].ToString());

                        for (int k = 0; k < Scores.Rows.Count; k++)
                        {
                            if (AssessmentTypes.Rows[j][0].ToString() == Scores.Rows[k][4].ToString())
                            {
                                dt.Rows.Add(Scores.Rows[k][3].ToString(), Scores.Rows[k][2].ToString(), Scores.Rows[k][5].ToString(), Scores.Rows[k][7].ToString(), Scores.Rows[k][8].ToString(), GetTotalPoint_3rd(Convert.ToInt32(Scores.Rows[k][1])));
                            }
                        }
                    }
                    else
                    { }

                }
                grdScore.DataSource = dt;
                grdScore.DataBind();
            }
        }
        //Get Total Point for every Assessment
        int GetTotalPoint_3rd(int assessmentid)
        {
            int total = 0;
            AssessmentDetailsList = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());
            AssessmentDetailsList.ForEach(ad =>
            {
                if (ad.AssessmentID == assessmentid)
                {
                    total += ad.Points;
                }
            });
            Debug.WriteLine(total.ToString() + " <= Total Points");
            return total;
        }
        //Store all taken assessment
        void GetAssessmentTaken_3rd()
        {
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems_3rd"];
            //data table for all assessment that has been taken
            DataTable dt_taken = new DataTable();
            dt_taken.Columns.Add("AssessmentID");
            for (int i = 0; i < 1; i++)
            {

                for (int j = 0; j < Subitems.Rows.Count; j++)
                {

                    StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                    StudentAnswersList.ForEach(sa =>
                    {
                        //Debug.WriteLine(StudentIDz);
                        if (sa.StudentID.ToString() == cboChild.SelectedValue)
                        {
                            //Get the score if one of the assessment id from database match the assessment id from session
                            if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                            {
                                dt_taken.Rows.Add(sa.AssessmentID);
                            }
                        }
                    });
                    Session["AssessmentTaken_3rd"] = dt_taken;
                }
            }
            //grdView.DataSource = Session["AssessmentTaken"];
            //grdView.DataBind();
        }
        //checking if the assessment is taken
        bool isTaken_3rd(string assess_id)
        {
            bool x = false;
            DataTable dt = (DataTable)Session["AssessmentTaken_3rd"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (assess_id == dt.Rows[i][0].ToString())
                {
                    x = true;
                }
            }
            return x;
        }

        void vGetScore_3rd()
        {
            GetAssessmentTaken_3rd();
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems_3rd"];
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Score");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmenTypeID");
            dt.Columns.Add("Status");
            dt.Columns.Add("ExamDate");
            dt.Columns.Add("DateTaken");
            dt.Columns.Add("Quarter");
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < Subitems.Rows.Count; j++)
                {
                    DateTime DateStart = DateTime.Now;
                    DateTime DateEnd = DateTime.Now;
                    DateTime TimeEnd = DateTime.Now;
                    DateTime DateTaken = DateTime.Now;

                    string qtr = "", date_blank;
                    int AID_grid = Convert.ToInt32(Subitems.Rows[j][0]);
                    int AID_data = 0;
                    string status = "";
                    string final_score = "0";
                    int studentID = Convert.ToInt32(cboChild.SelectedValue);
                    //Pass the current assessment id from session
                    int x = 0;

                    AssessmentList = new List<Constructors.Assessment>(cls.getAssessment());
                    AssessmentList.ForEach(al =>
                    {


                        if (Convert.ToInt32(Subitems.Rows[j][0]) == al.AssessmentID)
                        {
                            if (isTaken_3rd(al.AssessmentID.ToString()))
                            {
                                x = 1;
                                status = "Taken";
                                qtr = al.Quarter;
                            }
                            else
                            {
                                x = 0;
                                string DateS = Convert.ToDateTime(al.DateStart).ToShortDateString() + " " + al.TimeStart;
                                DateStart = Convert.ToDateTime(DateS);
                                string DateE = Convert.ToDateTime(al.DateEnd).ToShortDateString() + " " + al.TimeEnd;
                                DateEnd = Convert.ToDateTime(DateE);
                                AID_data = al.AssessmentID;
                                qtr = al.Quarter;
                            }
                        }

                    });

                    if (x == 0)
                    {
                        if (DateStart > DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not yet taken");
                            final_score = "-";
                            status = "Not Yet Taken";


                        }
                        else if (DateEnd < DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not taken");
                            final_score = "0";
                            status = "Not Taken";
                        }
                        date_blank = "-";
                    }
                    else
                    {
                        double score = 0;
                        Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is taken");
                        StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                        StudentAnswersList.ForEach(sa =>
                        {
                            if (sa.StudentID.ToString() == cboChild.SelectedValue)
                            {
                                //Get the score if one of the assessment id from database match the assessment id from session
                                if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                                {
                                    string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                    if (ans == sa.SelectedAnswer)
                                    {
                                        //add score if the answer is correct
                                        string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + "");
                                        score += Convert.ToDouble(points);
                                    }
                                    else
                                    {
                                        //add zero to the score if the answer is wrong
                                        score += 0;
                                    }
                                    studentID = sa.StudentID;
                                    AID_data = sa.AssessmentID;
                                    DateTaken = Convert.ToDateTime(sa.LastUpdateDate);

                                }
                            }
                        });

                        final_score = score.ToString();
                        date_blank = DateTaken.ToShortDateString();
                    }
                    if (AID_data == AID_grid)
                    {
                        //if the assesment id from database and session match the score will be added to the list
                        dt.Rows.Add(cboChild.SelectedValue, Subitems.Rows[j][0].ToString(), final_score, Subitems.Rows[j][1].ToString(), Subitems.Rows[j][2].ToString(), status, DateStart.ToShortDateString(), date_blank, qtr);
                    }
                }
            }

            Session["Scores_3rd"] = dt;
            //grdView.DataSource = Session["Scores"];
            //grdView.DataBind();
        }
        #endregion

        #region "for 4th quarter"
        void VGetAssessmentTypes_4th()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(hidSection.Value);
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
                //getting assessmenttypeid in table assessment
                //Debug.WriteLine(at.Quarter + "<- Quarter");
                dt.Rows.Add(at.AssessmentTypeID.ToString(), "-");
            });

            //get the title for the assessment type
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + " ");

                if (title != "")
                {
                    //if title is not empty then add the title to the data table
                    dt.Rows[i][1] = title;

                    //dt.Rows[i][2] = "1st";
                }
            }

            //pass the value of the data table to session
            Session["AssessmentTypes_4th"] = dt;
            grdStudentsList_4th.DataSource = Session["AssessmentTypes_4th"];
            grdStudentsList_4th.DataBind();
            //grdView.DataBind();

        }


        void VGetAllSubitems_4th()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(hidSection.Value);
            //get the value for the session
            DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_4th"];
            //create data table
            DataTable dt = new DataTable();
            //create column
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmentTypeID");
            //search for subitem that will match the selected subject and assessment typeid
            for (int i = 0; i < AssessmentTypes.Rows.Count; i++)
            {
                AssessmentType = new List<Constructors.Assessment>(cls.getAssessment());
                AssessmentType.ForEach(at =>
                {
                    if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID)
                    {
                        if (at.Quarter == "4th")
                        {
                            dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                        }
                    }
                });
            }
            //pass the data to a session
            Session["Subitems_4th"] = dt;
            //grdView.DataSource = Session["Subitems"];
            //grdView.DataBind();
            vGetScore_4th();
            GenerateGrid_4th();
        }
        //generate the main grid view pass all the value needed.
        void GenerateGrid_4th()
        {
            grdStudentsList_4th.Columns[0].HeaderText = "4th Quarter";
            for (int i = 0; i < grdStudentsList_4th.Rows.Count; i++)
            {
                GridView grdScore = (GridView)grdStudentsList_4th.Rows[i].FindControl("grdScores_4th");
                DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_4th"];
                DataTable Subitems = (DataTable)Session["Subitems_4th"];
                DataTable Scores = (DataTable)Session["Scores_4th"];
                Label lblTitle = (Label)grdStudentsList_4th.Rows[i].FindControl("lblTitle_4th");
                DataTable dt = new DataTable();
                dt.Columns.Add("Description");
                dt.Columns.Add("Score");
                dt.Columns.Add("Status");
                dt.Columns.Add("DateTaken");
                dt.Columns.Add("Quarter");
                dt.Columns.Add("Total");
                for (int j = 0; j < AssessmentTypes.Rows.Count; j++)
                {

                    if (lblTitle.Text == AssessmentTypes.Rows[j][1].ToString())
                    {
                        Debug.WriteLine("GridRow:" + i.ToString() + " title: " + AssessmentTypes.Rows[j][1].ToString());

                        for (int k = 0; k < Scores.Rows.Count; k++)
                        {
                            if (AssessmentTypes.Rows[j][0].ToString() == Scores.Rows[k][4].ToString())
                            {
                                dt.Rows.Add(Scores.Rows[k][3].ToString(), Scores.Rows[k][2].ToString(), Scores.Rows[k][5].ToString(), Scores.Rows[k][7].ToString(), Scores.Rows[k][8].ToString(), GetTotalPoint_4th(Convert.ToInt32(Scores.Rows[k][1])));
                            }
                        }
                    }
                    else
                    { }

                }
                grdScore.DataSource = dt;
                grdScore.DataBind();
            }
        }
        //Get Total Point for every Assessment
        int GetTotalPoint_4th(int assessmentid)
        {
            int total = 0;
            AssessmentDetailsList = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());
            AssessmentDetailsList.ForEach(ad =>
            {
                if (ad.AssessmentID == assessmentid)
                {
                    total += ad.Points;
                }
            });
            Debug.WriteLine(total.ToString() + " <= Total Points");
            return total;
        }
        //Store all taken assessment
        void GetAssessmentTaken_4th()
        {
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems_4th"];
            //data table for all assessment that has been taken
            DataTable dt_taken = new DataTable();
            dt_taken.Columns.Add("AssessmentID");
            for (int i = 0; i < 1; i++)
            {

                for (int j = 0; j < Subitems.Rows.Count; j++)
                {

                    StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                    StudentAnswersList.ForEach(sa =>
                    {
                        //Debug.WriteLine(StudentIDz);
                        if (sa.StudentID.ToString() == cboChild.SelectedValue)
                        {
                            //Get the score if one of the assessment id from database match the assessment id from session
                            if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                            {
                                dt_taken.Rows.Add(sa.AssessmentID);
                            }
                        }
                    });
                    Session["AssessmentTaken_4th"] = dt_taken;
                }
            }
            //grdView.DataSource = Session["AssessmentTaken"];
            //grdView.DataBind();
        }
        //checking if the assessment is taken
        bool isTaken_4th(string assess_id)
        {
            bool x = false;
            DataTable dt = (DataTable)Session["AssessmentTaken_4th"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (assess_id == dt.Rows[i][0].ToString())
                {
                    x = true;
                }
            }
            return x;
        }

        void vGetScore_4th()
        {
            GetAssessmentTaken_4th();
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems_4th"];
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Score");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmenTypeID");
            dt.Columns.Add("Status");
            dt.Columns.Add("ExamDate");
            dt.Columns.Add("DateTaken");
            dt.Columns.Add("Quarter");
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < Subitems.Rows.Count; j++)
                {
                    DateTime DateStart = DateTime.Now;
                    DateTime DateEnd = DateTime.Now;
                    DateTime TimeEnd = DateTime.Now;
                    DateTime DateTaken = DateTime.Now;

                    string qtr = "", date_blank;
                    int AID_grid = Convert.ToInt32(Subitems.Rows[j][0]);
                    int AID_data = 0;
                    string status = "";
                    string final_score = "0";
                    int studentID = Convert.ToInt32(cboChild.SelectedValue);
                    //Pass the current assessment id from session
                    int x = 0;

                    AssessmentList = new List<Constructors.Assessment>(cls.getAssessment());
                    AssessmentList.ForEach(al =>
                    {


                        if (Convert.ToInt32(Subitems.Rows[j][0]) == al.AssessmentID)
                        {
                            if (isTaken_4th(al.AssessmentID.ToString()))
                            {
                                x = 1;
                                status = "Taken";
                                qtr = al.Quarter;
                            }
                            else
                            {
                                x = 0;
                                string DateS = Convert.ToDateTime(al.DateStart).ToShortDateString() + " " + al.TimeStart;
                                DateStart = Convert.ToDateTime(DateS);
                                string DateE = Convert.ToDateTime(al.DateEnd).ToShortDateString() + " " + al.TimeEnd;
                                DateEnd = Convert.ToDateTime(DateE);
                                AID_data = al.AssessmentID;
                                qtr = al.Quarter;
                            }
                        }

                    });

                    if (x == 0)
                    {
                        if (DateStart > DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not yet taken");
                            final_score = "-";
                            status = "Not Yet Taken";


                        }
                        else if (DateEnd < DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not taken");
                            final_score = "0";
                            status = "Not Taken";
                        }
                        date_blank = "-";
                    }
                    else
                    {
                        double score = 0;
                        Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is taken");
                        StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                        StudentAnswersList.ForEach(sa =>
                        {
                            if (sa.StudentID.ToString() == cboChild.SelectedValue)
                            {
                                //Get the score if one of the assessment id from database match the assessment id from session
                                if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                                {
                                    string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                    if (ans == sa.SelectedAnswer)
                                    {
                                        //add score if the answer is correct
                                        string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + "");
                                        score += Convert.ToDouble(points);
                                    }
                                    else
                                    {
                                        //add zero to the score if the answer is wrong
                                        score += 0;
                                    }
                                    studentID = sa.StudentID;
                                    AID_data = sa.AssessmentID;
                                    DateTaken = Convert.ToDateTime(sa.LastUpdateDate);

                                }
                            }
                        });

                        final_score = score.ToString();
                        date_blank = DateTaken.ToShortDateString();
                    }
                    if (AID_data == AID_grid)
                    {
                        //if the assesment id from database and session match the score will be added to the list
                        dt.Rows.Add(cboChild.SelectedValue, Subitems.Rows[j][0].ToString(), final_score, Subitems.Rows[j][1].ToString(), Subitems.Rows[j][2].ToString(), status, DateStart.ToShortDateString(), date_blank, qtr);
                    }
                }
            }

            Session["Scores_4th"] = dt;
            //grdView.DataSource = Session["Scores"];
            //grdView.DataBind();
        }
        #endregion

        #endregion

        protected void grdStudentsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
