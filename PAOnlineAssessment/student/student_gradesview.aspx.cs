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
using PAOnlineAssessment.instructor;

namespace PAOnlineAssessment.student
{
    public partial class student_gradesview : System.Web.UI.Page
    {
        //Instantiate New Login User Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Instantiate New Collections Class
        Collections cls = new Collections();
        //Declare List of Answer List
        List<Constructors.StudentAnswers> StudentAnswersList;
        //Declare List of students
        List<Constructors.StudentRegistrationView> StudentList;
        //Declare List of StudentAccounts
        List<Constructors.StudentAccount> StudentAccountList;
        //Declare List of Assessment
        List<Constructors.Assessment> AssessmentList;
        //Declare list of Registration Terms
        List<Constructors.RegistrationTerm> RegistrationTerm;
        //Declare list of assessments
        List<Constructors.Assessment> AssessmentType;
        //
        List<Constructors.GradingView> GradingView;
        
        //Declare Lits of Assessment Details
        List<Constructors.AssessmentDetails> AssessmentDetailsList;

        //variable used in this form
        int StudentIDz = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is logged in
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == true && (string)Session["UserGroupID"] == "S")
                {

                }
                else
                {
                    Response.Write("<script>alert('Access Denied!'); window.location='" + ResolveUrl(DefaultForms.frm_instructor_subjects) + "';</script>");
                }
            }
            //if No Logged In User, redirect to Login Screen
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            LoadSiteMapDetails();

            if (!IsPostBack)
            {
                GetStudentnumberAndSection();
                GetSubjects();
                //GetStudentID();
                cboQuarter.SelectedValue = Session["Quarter"].ToString();
                //check if has item
                if (cboSubjects.Items.Count > 0)
                {
                    cboSubjects.SelectedIndex = 0;
                }
                LoadQuarter();
            }

            Debug.WriteLine("Selected Subject: " + cboSubjects.SelectedValue.ToString() + ", " + cboSubjects.SelectedItem.ToString());
            //load the 1st quarter assessment
            GetAssessmentTypes_1st();
            GetAllSubitems_1st();

            //load the 2nd quarter assessment
            GetAssessmentTypes_2nd();
            GetAllSubitems_2nd();

            //load the 3rd quarter assessment
            GetAssessmentTypes_3rd();
            GetAllSubitems_3rd();

            //load the 4th quarter assessment
            GetAssessmentTypes_4th();
            GetAllSubitems_4th();
        }

        void LoadQuarter()
        {
            grdStudentsList_1st.Visible = false;
            grdStudentsList_2nd.Visible = false;
            grdStudentsList_3rd.Visible = false;
            grdStudentsList_4th.Visible = false;

            if (cboQuarter.SelectedValue == "1st")
                grdStudentsList_1st.Visible = true;

            else if (cboQuarter.SelectedValue == "2nd")
                grdStudentsList_2nd.Visible = true;

            else if (cboQuarter.SelectedValue == "3rd")
                grdStudentsList_3rd.Visible = true;

            else if (cboQuarter.SelectedValue == "4th")
                grdStudentsList_4th.Visible = true;
        }

        //check assessment for the 1st quarter
        bool isHaveAssessment_1st()
        {
            bool isHave;
            DataTable dt = (DataTable)Session["AssessmentTypes_1st"];
            int i;
            for (i = 0; i < dt.Rows.Count; i++) { }
            if (i == 0)
            {
                isHave = false;
                grdStudentsList_1st.DataSource = "";
                grdStudentsList_1st.DataBind();
            }
            else
            {
                isHave = true;
            }

            return isHave;
        }

        //check assessment for the 2nd quarter
        bool isHaveAssessment_2nd()
        {
            bool isHave;
            DataTable dt = (DataTable)Session["AssessmentTypes_2nd"];
            int i;
            for (i = 0; i < dt.Rows.Count; i++) { }
            if (i == 0)
            {
                isHave = false;
                grdStudentsList_2nd.DataSource = "";
                grdStudentsList_2nd.DataBind();
            }
            else
            {
                isHave = true;
            }
            return isHave;
        }

        //check assessment for the 3rd quarter
        bool isHaveAssessment_3rd()
        {
            bool isHave;
            DataTable dt = (DataTable)Session["AssessmentTypes_3rd"];
            int i;
            for (i = 0; i < dt.Rows.Count; i++) { }
            if (i == 0)
            {
                isHave = false;
                grdStudentsList_3rd.DataSource = "";
                grdStudentsList_3rd.DataBind();
            }
            else
            {
                isHave = true;
            }
            return isHave;
        }

        //check assessment for the 4th quarter
        bool isHaveAssessment_4th()
        {
            bool isHave;
            DataTable dt = (DataTable)Session["AssessmentTypes_4th"];
            int i;
            for (i = 0; i < dt.Rows.Count; i++) { }
            if (i == 0)
            {
                isHave = false;
                grdStudentsList_4th.DataSource = "";
                grdStudentsList_4th.DataBind();
            }
            else
            {
                isHave = true;
            }
            return isHave;
        }

        void GetStudentnumberAndSection()
        {
            //set the values
            Session["StudentID"] = "0";
            Session["SectionID"] = "0";
            Session["LvlID"] = "0";

            StudentAccountList = new List<Constructors.StudentAccount>(cls.getStudentAccounts());
            StudentAccountList.ForEach(sl =>
            {
                if (LUser.UserID.ToString() == sl.StudentID.ToString())
                {
                    //get the level
                    string LevelID = cls.ExecuteScalar("SELECT LevelID FROM PaceRegistration.dbo.Student WHERE StudentNumber='"+ sl.StudentNumber  + "'");
                    if (!string.IsNullOrEmpty(LevelID))
                        Session["LvlID"] = LevelID;

                    //get the student id
                    string StudentID = cls.ExecuteScalar("SELECT StudentID FROM PaceRegistration.dbo.Student WHERE StudentNumber='" + sl.StudentNumber + "'");
                    StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
                    StudentList.ForEach(st =>
                    {
                        //collect the student number and section
                        if (st.SchoolYear == Session["CurrentSchoolYear"].ToString() && st.StudentNumber == sl.StudentNumber)
                        {   
                            Session["SectionID"] = st.SectionID.ToString();
                            if (!string.IsNullOrEmpty(StudentID))
                                Session["StudentID"] = StudentID;
                            return;
                        }
                    });
                }
            });

            //Response.Write("<script>alert('" + StudentIDz + "');</script>");
        }
     
        void GetSubjects()
        {
            //clear items first
            cboSubjects.Items.Clear();
            cboSubjects.Items.Add("--- Select Subject ---");
            cboSubjects.Items[cboSubjects.Items.Count - 1].Value = "0";

            //Create a DataTable containing the SubjectID and SubjectDescription
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SubjectID"));
            dt.Columns.Add(new DataColumn("SubjectDescription"));

            GradingView = new List<Constructors.GradingView>(cls.getGradingView());
            //Loop through List of Subject
            GradingView.ForEach(gv =>
            {
                if (gv.SchoolYear == Session["CurrentSchoolYear"].ToString() && gv.LevelID == Convert.ToInt32(Session["LvlID"].ToString()))
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

            ////pinapasa ung section id
            //int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
            ////loop through the list
            //GradingView.ForEach(gv =>
            //{
            //    //load the subjects
            //    if (SectionID == gv.SectionID)
            //    {
            //        cboSubjects.Items.Add(gv.Description);
            //        cboSubjects.Items[cboSubjects.Items.Count - 1].Value = gv.SubjectID.ToString();
            //        //cboSubjects.Items.Add(new ListItem(gv.Description,gv.SubjectID.ToString()));
            //    }
            //});
        }

        void GetAssessmentTypes_2nd()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue.ToString());
            int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
                //getting assessmenttypeid in table assessment
                dt.Rows.Add(at.AssessmentTypeID.ToString());
            });

            //get the title for the assessment type
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + "");

                if (title != "")
                {
                    //if title is not empty then add the title to the data table
                    dt.Rows[i][1] = title;
                }
            }

            //pass the value of the data table to session
            Session["AssessmentTypes_2nd"] = dt;
            grdStudentsList_2nd.DataSource = Session["AssessmentTypes_2nd"];
            grdStudentsList_2nd.DataBind();
            //grdView.DataBind();
        }

        void GetAssessmentTypes_3rd()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue.ToString());
            int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
                //getting assessmenttypeid in table assessment
                dt.Rows.Add(at.AssessmentTypeID.ToString());
            });

            //get the title for the assessment type
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + " ");

                if (title != "")
                {
                    //if title is not empty then add the title to the data table
                    dt.Rows[i][1] = title;
                }
            }

            //pass the value of the data table to session
            Session["AssessmentTypes_3rd"] = dt;
            grdStudentsList_3rd.DataSource = Session["AssessmentTypes_3rd"];
            grdStudentsList_3rd.DataBind();
            //grdView.DataBind();
        }

        void GetAssessmentTypes_4th()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue.ToString());
            int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
                //getting assessmenttypeid in table assessment
                dt.Rows.Add(at.AssessmentTypeID.ToString());
            });

            //get the title for the assessment type
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + " ");

                if (title != "")
                {
                    //if title is not empty then add the title to the data table
                    dt.Rows[i][1] = title;
                }
            }

            //pass the value of the data table to session
            Session["AssessmentTypes_4th"] = dt;
            grdStudentsList_4th.DataSource = Session["AssessmentTypes_4th"];
            grdStudentsList_4th.DataBind();
            //grdView.DataBind();
        }

        void GetAssessmentTypes_1st()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue.ToString());
            int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
                //getting assessmenttypeid in table assessment
                dt.Rows.Add(at.AssessmentTypeID.ToString());
            });

            if (dt.Rows.Count != 0)
            {
                //get the title for the assessment type
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + " ");

                    if (title != "")
                    {
                        //if title is not empty then add the title to the data table
                        dt.Rows[i][1] = title;
                    }
                }
            }

            //pass the value of the data table to session
            Session["AssessmentTypes_1st"] = dt;
            grdStudentsList_1st.DataSource = Session["AssessmentTypes_1st"];
            grdStudentsList_1st.DataBind();
            //grdView.DataBind();
        }

        public void LoadSiteMapDetails()
        {
            SiteMap1.RootNode = "Dashboard";
            SiteMap1.RootNodeToolTip = "Dashboard";
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.ParentNode = "Academic Activities";
            SiteMap1.ParentNodeToolTip = "Click to go back to My Subjects";
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.CurrentNode = "My Grades";
        }

        protected void cboSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchQuery.Text = cboSubjects.SelectedValue.ToString();
            //Debug.WriteLine("Selected Subject: " + cboSubjects.SelectedValue.ToString());
        }

        void GetAllSubitems_2nd()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue.ToString());
            int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
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
                    if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID && at.Quarter == "2nd")
                    {
                        dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                    }
                });
            }
            //pass the data to a session
            Session["Subitems_2nd"] = dt;
            //grdView.DataSource = Session["Subitems_2nd"];
            //grdView.DataBind();
            GetScore_2nd();
            GenerateGrid_2nd();
        }

        void GetAllSubitems_3rd()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue.ToString());
            int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
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
                    if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID && at.Quarter == "3rd")
                    {
                        dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                    }
                });
            }
            //pass the data to a session
            Session["Subitems_3rd"] = dt;
            //grdView.DataSource = Session["Subitems_2nd"];
            //grdView.DataBind();
            GetScore_3rd();
            GenerateGrid_3rd();
        }

        void GetAllSubitems_4th()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue.ToString());
            int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
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
                    if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID && at.Quarter == "4th")
                    {
                        dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                    }
                });
            }
            //pass the data to a session
            Session["Subitems_4th"] = dt;
            //grdView.DataSource = Session["Subitems_2nd"];
            //grdView.DataBind();
            GetScore_4th();
            GenerateGrid_4th();
        }

        void GetAllSubitems_1st()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue.ToString());
            int SectionID = Convert.ToInt32(Session["SectionID"].ToString());
            //get the value for the session
            DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_1st"];
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
                    if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID && at.Quarter == "1st")
                    {
                        dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                    }
                });
            }
            //pass the data to a session
            Session["Subitems_1st"] = dt;
            grdView.DataSource = Session["Subitems_1st"];
            grdView.DataBind();
            GetScore_1st();
            GenerateGrid_1st();
        }
            
        //generate the 1st quarter grid view pass all the value needed.
        void GenerateGrid_1st()
        {
            grdStudentsList_1st.Columns[0].HeaderText = "1ST QUARTER";
            grdStudentsList_1st.Columns[0].HeaderStyle.ForeColor = Color.DarkGreen;
            grdStudentsList_1st.Columns[0].HeaderStyle.BackColor = Color.Beige;
            for (int i = 0; i < grdStudentsList_1st.Rows.Count; i++)
            {
                GridView grdScore = (GridView)grdStudentsList_1st.Rows[i].FindControl("grdScores");
                DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_1st"];
                DataTable Subitems = (DataTable)Session["Subitems_1st"];
                DataTable Scores = (DataTable)Session["Scores_1st"];
                Label lblTitle = (Label)grdStudentsList_1st.Rows[i].FindControl("lblTitle");
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

        //generate the 2nd quarter grid view pass all the value needed.
        void GenerateGrid_2nd()
        {
            grdStudentsList_2nd.Columns[0].HeaderText = "2ND QUARTER";
            grdStudentsList_2nd.Columns[0].HeaderStyle.ForeColor = Color.DarkGreen;
            grdStudentsList_2nd.Columns[0].HeaderStyle.BackColor = Color.Beige;
            for (int i = 0; i < grdStudentsList_2nd.Rows.Count; i++)
            {
                GridView grdScore = (GridView)grdStudentsList_2nd.Rows[i].FindControl("grdScores");
                DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_2nd"];
                DataTable Subitems = (DataTable)Session["Subitems_2nd"];
                DataTable Scores = (DataTable)Session["Scores_2nd"];
                Label lblTitle = (Label)grdStudentsList_2nd.Rows[i].FindControl("lblTitle");
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

        //generate the 3rd quarter grid view pass all the value needed.
        void GenerateGrid_3rd()
        {
            grdStudentsList_3rd.Columns[0].HeaderText = "3RD QUARTER";
            grdStudentsList_3rd.Columns[0].HeaderStyle.ForeColor = Color.DarkGreen;
            grdStudentsList_3rd.Columns[0].HeaderStyle.BackColor = Color.Beige;
            for (int i = 0; i < grdStudentsList_3rd.Rows.Count; i++)
            {
                GridView grdScore = (GridView)grdStudentsList_3rd.Rows[i].FindControl("grdScores");
                DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_3rd"];
                DataTable Subitems = (DataTable)Session["Subitems_3rd"];
                DataTable Scores = (DataTable)Session["Scores_3rd"];
                Label lblTitle = (Label)grdStudentsList_3rd.Rows[i].FindControl("lblTitle");
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

        //generate the 4th quarter grid view pass all the value needed.
        void GenerateGrid_4th()
        {
            grdStudentsList_4th.Columns[0].HeaderText = "4TH QUARTER";
            grdStudentsList_4th.Columns[0].HeaderStyle.ForeColor = Color.DarkGreen;
            grdStudentsList_4th.Columns[0].HeaderStyle.BackColor = Color.Beige;
            for (int i = 0; i < grdStudentsList_4th.Rows.Count; i++)
            {
                GridView grdScore = (GridView)grdStudentsList_4th.Rows[i].FindControl("grdScores");
                DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes_4th"];
                DataTable Subitems = (DataTable)Session["Subitems_4th"];
                DataTable Scores = (DataTable)Session["Scores_4th"];
                Label lblTitle = (Label)grdStudentsList_4th.Rows[i].FindControl("lblTitle");
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

        //Store all taken assessment
        void GetAssessmentTaken_1st()
        { 
        // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems_1st"];
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
                        if (sa.StudentID == Convert.ToInt32(Session["StudentID"].ToString()))
                        {
                            //Get the score if one of the assessment id from database match the assessment id from session
                            if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                            {
                                dt_taken.Rows.Add(sa.AssessmentID);
                            }
                        }
                    });
                    Session["AssessmentTaken_1st"] = dt_taken;
                }
            }
            grdView.DataSource = Session["AssessmentTaken_1st"];
            grdView.DataBind();    
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
                        if (sa.StudentID == Convert.ToInt32(Session["StudentID"].ToString()))
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
            grdView.DataSource = Session["AssessmentTaken_2nd"];
            grdView.DataBind();
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
                        if (sa.StudentID == Convert.ToInt32(Session["StudentID"].ToString()))
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
            grdView.DataSource = Session["AssessmentTaken_3rd"];
            grdView.DataBind();
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
                        if (sa.StudentID == Convert.ToInt32(Session["StudentID"].ToString()))
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
            grdView.DataSource = Session["AssessmentTaken_4th"];
            grdView.DataBind();
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

        //checking if the assessment is taken
        bool isTaken_1st(string assess_id)
        {
            bool x = false;
            DataTable dt = (DataTable)Session["AssessmentTaken_1st"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (assess_id == dt.Rows[i][0].ToString())
                {
                    x = true;
                }
            }
            return x;
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

        void GetScore_1st()
        {
            GetAssessmentTaken_1st();
            // DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems_1st"];
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
                    int studentID = Convert.ToInt32(Session["StudentID"].ToString());
                    //Pass the current assessment id from session
                    int x = 0;

                    AssessmentList = new List<Constructors.Assessment>(cls.getAssessment());
                    AssessmentList.ForEach(al =>
                    {
                        if (Convert.ToInt32(Subitems.Rows[j][0]) == al.AssessmentID)
                        {
                            if (isTaken_1st(al.AssessmentID.ToString()))
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
                            if (sa.StudentID == Convert.ToInt32(Session["StudentID"].ToString()))
                            {
                                //Get the score if one of the assessment id from database match the assessment id from session
                                if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                                {
                                    string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                    if (ans == sa.SelectedAnswer)
                                    {
                                        //add score if the answer is correct
                                        string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + " and QuestionPoolID=" + sa.QuestionPoolID + "");
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
                        dt.Rows.Add(Convert.ToInt32(Session["StudentID"].ToString()), Subitems.Rows[j][0].ToString(), final_score, Subitems.Rows[j][1].ToString(), Subitems.Rows[j][2].ToString(), status, DateStart.ToShortDateString(), date_blank, qtr);
                    }
                }
            }

            Session["Scores_1st"] = dt;
            //grdView.DataSource = Session["Scores"];
            //grdView.DataBind();
        }

        void GetScore_2nd()
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
                    int studentID = Convert.ToInt32(Session["StudentID"].ToString());
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
                            if (sa.StudentID == Convert.ToInt32(Session["StudentID"].ToString()))
                            {
                                //Get the score if one of the assessment id from database match the assessment id from session
                                if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                                {
                                    string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                    if (ans == sa.SelectedAnswer)
                                    {
                                        //add score if the answer is correct
                                        string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + " and QuestionPoolID=" + sa.QuestionPoolID + "");
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
                        dt.Rows.Add(Convert.ToInt32(Session["StudentID"].ToString()), Subitems.Rows[j][0].ToString(), final_score, Subitems.Rows[j][1].ToString(), Subitems.Rows[j][2].ToString(), status, DateStart.ToShortDateString(), date_blank, qtr);
                    }
                }
            }

            Session["Scores_2nd"] = dt;
            //grdView.DataSource = Session["Scores"];
            //grdView.DataBind();
        }

        void GetScore_3rd()
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
                    int studentID = Convert.ToInt32(Session["StudentID"].ToString());
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
                            if (sa.StudentID == Convert.ToInt32(Session["StudentID"].ToString()))
                            {
                                //Get the score if one of the assessment id from database match the assessment id from session
                                if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                                {
                                    string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                    if (ans == sa.SelectedAnswer)
                                    {
                                        //add score if the answer is correct
                                        string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + " and QuestionPoolID=" + sa.QuestionPoolID + "");
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
                        dt.Rows.Add(Convert.ToInt32(Session["StudentID"].ToString()), Subitems.Rows[j][0].ToString(), final_score, Subitems.Rows[j][1].ToString(), Subitems.Rows[j][2].ToString(), status, DateStart.ToShortDateString(), date_blank, qtr);
                    }
                }
            }

            Session["Scores_3rd"] = dt;
            //grdView.DataSource = Session["Scores"];
            //grdView.DataBind();
        }

        void GetScore_4th()
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
                    int studentID = Convert.ToInt32(Session["StudentID"].ToString());
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
                            if (sa.StudentID == Convert.ToInt32(Session["StudentID"].ToString()))
                            {
                                //Get the score if one of the assessment id from database match the assessment id from session
                                if (sa.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                                {
                                    string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                    if (ans == sa.SelectedAnswer)
                                    {
                                        //add score if the answer is correct
                                        string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + " and QuestionPoolID=" + sa.QuestionPoolID + "");
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
                        dt.Rows.Add(Convert.ToInt32(Session["StudentID"].ToString()), Subitems.Rows[j][0].ToString(), final_score, Subitems.Rows[j][1].ToString(), Subitems.Rows[j][2].ToString(), status, DateStart.ToShortDateString(), date_blank, qtr);
                    }
                }
            }

            Session["Scores_4th"] = dt;
            //grdView.DataSource = Session["Scores"];
            //grdView.DataBind();
        }


        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void cboQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdStudentsList_1st.Visible = false;
            grdStudentsList_2nd.Visible = false;
            grdStudentsList_3rd.Visible = false;
            grdStudentsList_4th.Visible = false;
            switch (cboQuarter.SelectedIndex)
            {
                case 0:
                    grdStudentsList_1st.Visible = true;
                    break;

                case 1:

                    grdStudentsList_2nd.Visible = true;
                    break;

                case 2:

                    grdStudentsList_3rd.Visible = true;
                    break;

                case 3:

                    grdStudentsList_4th.Visible = true;
                    break;
                
            }
 
        }
        //Create Gridview with dynamic templates

    }
}
