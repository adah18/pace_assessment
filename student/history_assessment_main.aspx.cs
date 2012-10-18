using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Collections.Specialized;
using System.Diagnostics;

namespace PAOnlineAssessment.student
{
    public partial class history_assessment_main : System.Web.UI.Page
    {
        //Instantiate new collection
        Collections cls = new Collections();
        //Instantiate new forms
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate new system procedures
        SystemProcedures sys = new SystemProcedures();
        //Instantiate new login user
        LoginUser LUser;
        //Instantiate current student log in
        CurrentStudent CStudent;
        //Instantiate new list of assessment
        List<Constructors.AssessmentView> oAssessmentView;
        //Instantiate new list of student answer
        List<Constructors.StudentAnswers> oStudentAnswer;
        //Instantiate new list of assessment type
        List<Constructors.AssessmentType> oAssessmentType;
        //DynamicallyTemplatedGridViewHandler Event;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Logged In User Info from Session Variable
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

            if (IsPostBack == false)
            {
                ddlQuarter.SelectedValue = Session["Quarter"].ToString();
                //Load all assessment type
                LoadAssessmentType();
                //Load all past assessment
                LoadAssessmentHistory();
            }

            //Load site maps for navigation
            LoadSitemapDetails();
        }

        //Load site maps for navigation
        public void LoadSitemapDetails()
        {
            SiteMap.RootNode = "Dashboard";
            SiteMap.RootNodeToolTip = "Click to go back to Dashboard";
            SiteMap.RootNodeURL = ResolveUrl(DefaultForms.frm_default_dashboard);

            SiteMap.ParentNode = "Academic Activities";

            SiteMap.CurrentNode = "Assessment History";
        }

        //Load all past assessment
        void LoadAssessmentHistory()
        {
            string LevelID = cls.ExecuteScalar("Select prs.LevelID from PaceRegistration.dbo.Student prs inner join PaceAssessment.dbo.Student pas on (pas.StudentNumber = prs.StudentNumber) where pas.StudentID='" + LUser.UserID + "'");
            //string LevelID = cls.ExecuteScalar(query);
            oAssessmentView = new List<Constructors. AssessmentView>(cls.getAssessmentView());
            oStudentAnswer = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());

            DataTable dt = (DataTable)Session["AssessmentType"];
            dgAssessments.DataSource = dt;
            dgAssessments.DataBind();
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                GridView grdView = (GridView)dgAssessments.Rows[row].FindControl("dgAssessmentType");
                grdView.Caption = "<span class='PageSubHeader' lang='en-ph'>" + dt.Rows[row][0].ToString() + "</span>";
                
                DataTable dta = new DataTable();
                dta.Columns.Add("SubjectDescription");
                dta.Columns.Add("Title");
                dta.Columns.Add("AssessmentID");
                dta.Columns.Add("AssessmentType");
                dta.Columns.Add("DateStart");
                dta.Columns.Add("DateEnd");
                dta.Columns.Add("TimeStart");
                dta.Columns.Add("TimeEnd");
                dta.Columns.Add("Status");

                switch (ddlSearch.SelectedItem.ToString())
                {
                        //Load all past assessment based on subject and quarter
                    case "Subject":
                        oAssessmentView.ForEach(av =>
                        {
                            if(av.Status == "A" && av.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (dt.Rows[row][0].ToString().ToLower() == av.AssessmentTypeDescription.ToString().ToLower() && av.LevelID.ToString() == LevelID && av.Quarter == ddlQuarter.SelectedItem.ToString())
                                {
                                    TimeSpan Day = DateTime.Now.Date.Subtract(Convert.ToDateTime(av.DateEnd).Date);
                                    Debug.WriteLine("Assessment Day: " + Day.Days.ToString() + " <= Assesement Display: " + Session["Days"].ToString());
                                    if (Day.Days <= Convert.ToInt32(Session["Days"].ToString()))
                                    {
                                        if (av.Schedule == "Yes")
                                        {
                                            string check = cls.ExecuteScalar("SELECT * FROM PaceAssessment.dbo.StudentAnswers where AssessmentID=' " + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                            if (!string.IsNullOrEmpty(check))
                                            {
                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = "Taken.";
                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToDateTime(av.DateEnd).Date < DateTime.Now.Date && Convert.ToDateTime(av.DateStart).Date < DateTime.Now.Date)
                                                {
                                                    if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                    {
                                                        if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                        {
                                                            if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                            {
                                                                string Status = "Not Taken.";
                                                                dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                            }
                                                        }
                                                        else if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                        {
                                                            if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                            {
                                                                string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                                if (!string.IsNullOrEmpty(Status))
                                                                {
                                                                    Status = "Taken";
                                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                            {
                                                                string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                                if (!string.IsNullOrEmpty(Status))
                                                                {
                                                                    Status = "Taken.";
                                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                                else if (Convert.ToDateTime(av.DateEnd).Date == DateTime.Now.Date)
                                                {
                                                    if (Convert.ToDateTime(av.TimeEnd) < DateTime.Now && Convert.ToDateTime(av.TimeStart) < DateTime.Now)
                                                    {
                                                        if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                        {
                                                            if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                            {
                                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                                {
                                                                    string Status = "Not Taken.";
                                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                                }
                                                            }
                                                            else if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                            {
                                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                                {
                                                                    string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                                    if (!string.IsNullOrEmpty(Status))
                                                                    {
                                                                        Status = "Taken";
                                                                        dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                                {
                                                                    string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                                    if (!string.IsNullOrEmpty(Status))
                                                                    {
                                                                        Status = "Taken.";
                                                                        dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (av.Schedule == "No")
                                        {
                                            if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                            {
                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = "Not Taken.";
                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                                }
                                            }
                                            else if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                            {
                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                    if (!string.IsNullOrEmpty(Status))
                                                    {
                                                        Status = "Taken";
                                                        dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                    if (!string.IsNullOrEmpty(Status))
                                                    {
                                                        Status = "Taken.";
                                                        dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        );
                        break;
                        //Load all past assessment based on title and quarter
                    case "Title":
                        oAssessmentView.ForEach(av =>
                        {
                            if (av.Status == "A" && av.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (dt.Rows[row][0].ToString().ToLower() == av.AssessmentTypeDescription.ToString().ToLower() && av.LevelID.ToString() == LevelID && av.Quarter == ddlQuarter.SelectedItem.ToString())
                                {
                                    TimeSpan Day = DateTime.Now.Date.Subtract(Convert.ToDateTime(av.DateEnd).Date);
                                    if (Day.Days <= Convert.ToInt32(Session["Days"].ToString()))
                                    {
                                        if (av.Schedule == "Yes")
                                        {
                                            string check = cls.ExecuteScalar("SELECT * FROM PaceAssessment.dbo.StudentAnswers where AssessmentID=' " + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                            if (!string.IsNullOrEmpty(check))
                                            {
                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = "Taken.";
                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToDateTime(av.DateEnd).Date < DateTime.Now.Date && Convert.ToDateTime(av.DateStart).Date < DateTime.Now.Date)
                                                {
                                                    if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                    {
                                                        if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                        {
                                                            string Status = "Not Taken.";
                                                            dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                        }
                                                    }
                                                    else if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                    {
                                                        if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                        {
                                                            string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                            if (!string.IsNullOrEmpty(Status))
                                                            {
                                                                Status = "Taken.";
                                                                dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                        {
                                                            string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                            if (!string.IsNullOrEmpty(Status))
                                                            {
                                                                Status = "Taken.";
                                                                dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                            }
                                                        }
                                                    }

                                                }
                                                else if (Convert.ToDateTime(av.DateEnd).Date == DateTime.Now.Date)
                                                {
                                                    if (Convert.ToDateTime(av.TimeEnd) < DateTime.Now && Convert.ToDateTime(av.TimeStart) < DateTime.Now)
                                                    {
                                                        if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                        {
                                                            if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                            {
                                                                string Status = "Not Taken.";
                                                                dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                            }
                                                        }
                                                        else if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                        {
                                                            if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                            {
                                                                string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                                if (!string.IsNullOrEmpty(Status))
                                                                {
                                                                    Status = "Taken.";
                                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                            {
                                                                string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                                if (!string.IsNullOrEmpty(Status))
                                                                {
                                                                    Status = "Taken.";
                                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (av.Schedule == "No")
                                        {
                                            if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                            {
                                                if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = "Not Taken.";
                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                                }
                                            }
                                            else if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                            {
                                                if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                    if (!string.IsNullOrEmpty(Status))
                                                    {
                                                        Status = "Taken.";
                                                        dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                    if (!string.IsNullOrEmpty(Status))
                                                    {
                                                        Status = "Taken.";
                                                        dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        );
                        break;
                        //Load all past assessment that the student have taken based on quarter
                    case "Taken":
                        oAssessmentView.ForEach(av =>
                        {
                            if (av.Status == "A" && av.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (dt.Rows[row][0].ToString().ToLower() == av.AssessmentTypeDescription.ToString().ToLower() && av.LevelID.ToString() == LevelID && av.Quarter == ddlQuarter.SelectedItem.ToString())
                                {
                                    TimeSpan Day = DateTime.Now.Date.Subtract(Convert.ToDateTime(av.DateEnd).Date);
                                    if (Day.Days <= Convert.ToInt32(Session["Days"].ToString()))
                                    {
                                        if (av.Schedule == "Yes")
                                        {
                                            string check = cls.ExecuteScalar("SELECT * FROM PaceAssessment.dbo.StudentAnswers where AssessmentID=' " + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                            if (!string.IsNullOrEmpty(check))
                                            {
                                                if (av.SubjectDescription.ToLower().Contains(txtSearch.Text.ToLower()))
                                                {
                                                    string Status = "Taken.";
                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToDateTime(av.DateEnd).Date < DateTime.Now.Date && Convert.ToDateTime(av.DateStart).Date < DateTime.Now.Date)
                                                {
                                                    if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                    {
                                                        string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                        if (!string.IsNullOrEmpty(Status))
                                                        {
                                                            Status = "Taken.";
                                                            dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                        if (!string.IsNullOrEmpty(Status))
                                                        {
                                                            Status = "Taken.";
                                                            dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                        }
                                                    }
                                                }
                                                else if (Convert.ToDateTime(av.DateEnd).Date == DateTime.Now.Date)
                                                {
                                                    if (Convert.ToDateTime(av.TimeEnd) < DateTime.Now && Convert.ToDateTime(av.TimeStart) < DateTime.Now)
                                                    {
                                                        if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                        {
                                                            string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                            if (!string.IsNullOrEmpty(Status))
                                                            {
                                                                Status = "Taken.";
                                                                dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                            if (!string.IsNullOrEmpty(Status))
                                                            {
                                                                Status = "Taken.";
                                                                dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (av.Schedule == "No")
                                        {
                                            if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                            {
                                                string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                if (!string.IsNullOrEmpty(Status))
                                                {
                                                    Status = "Taken.";
                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                                }
                                            }
                                            else
                                            {
                                                string Status = cls.ExecuteScalar("Select AssessmentID from PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                                if (!string.IsNullOrEmpty(Status))
                                                {
                                                    Status = "Taken.";
                                                    dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        );
                        break;
                        //Load all pass assessment that the student didnt take based on quarter
                    case "Not Taken":
                        oAssessmentView.ForEach(av =>
                        {
                            if (av.Status == "A" && av.SchoolYear == Session["CurrentSchoolYear"].ToString())
                            {
                                if (dt.Rows[row][0].ToString().ToLower() == av.AssessmentTypeDescription.ToString().ToLower() && av.LevelID.ToString() == LevelID && av.Quarter == ddlQuarter.SelectedItem.ToString())
                                {
                                    TimeSpan Day = DateTime.Now.Date.Subtract(Convert.ToDateTime(av.DateEnd).Date);
                                    if (Day.Days <= Convert.ToInt32(Session["Days"].ToString()))
                                    {
                                        if (av.Schedule == "Yes")
                                        {

                                            if (Convert.ToDateTime(av.DateEnd).Date < DateTime.Now.Date && Convert.ToDateTime(av.DateStart).Date < DateTime.Now.Date)
                                            {
                                                if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                {
                                                    if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                    {
                                                        string Status = "Not Taken.";
                                                        dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                    }
                                                }
                                            }
                                            else if (Convert.ToDateTime(av.DateEnd).Date == DateTime.Now.Date)
                                            {
                                                if (Convert.ToDateTime(av.TimeEnd) < DateTime.Now && Convert.ToDateTime(av.TimeStart) < DateTime.Now)
                                                {
                                                    if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                                    {
                                                        if (av.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                                                        {
                                                            string Status = "Not Taken.";
                                                            dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), Status);
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                        else if (av.Schedule == "No")
                                        {
                                            if (av.NoMakeUp.Contains("-" + CStudent.StudentID + "-"))
                                            {
                                                string Status = "Not Taken.";
                                                dta.Rows.Add(av.SubjectDescription, av.Title, av.AssessmentID.ToString(), av.AssessmentTypeDescription, "-", "-", "-", "-", Status);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        );
                        break;
                }

                dta.DefaultView.Sort = "SubjectDescription";
                grdView.DataSource = dta;
                grdView.DataBind();
            }
        }

        //Load all assessment type 
        void LoadAssessmentType()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("AssessmentDescription");
            oAssessmentType = new List<Constructors.AssessmentType>(cls.getAssessmentType());
            oAssessmentType.ForEach(at =>
                {
                    dt.Rows.Add(at.Description);
                }
                );

            Session["AssessmentType"] = dt;
            dgAssessments.DataSource = Session["AssessmentType"];
            dgAssessments.DataBind();
        }

        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            //Load all past assessment based on selected category
            LoadAssessmentHistory();
        }

        //Executed when the button first was clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            dgAssessments.PageIndex = 0;
            LoadAssessmentHistory();
        }

        //Executed when the button previous was clicked
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (dgAssessments.PageIndex != 0)
            {
                dgAssessments.PageIndex = dgAssessments.PageIndex - 1;
                LoadAssessmentHistory();
            }
        }

        //Executed when the page number was changed
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            dgAssessments.PageIndex = cboPageNumber.SelectedIndex;
            LoadAssessmentHistory();
        }

        //Executed when the button next was clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (dgAssessments.PageIndex != dgAssessments.PageCount)
            {
                dgAssessments.PageIndex = dgAssessments.PageIndex + 1;
                LoadAssessmentHistory();
            }
        }

        //Executed when the button last was clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            dgAssessments.PageIndex = dgAssessments.PageCount;
            LoadAssessmentHistory();
        }

        //Executed when the grid view was data bound
        protected void dgAssessments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < dgAssessments.Rows.Count; x++)
            {
                GridView dgAssessmentType = (GridView)dgAssessments.Rows[x].FindControl("dgAssessmentType");
                for (int y = 0; y < dgAssessmentType.Rows.Count; y++)
                {
                    ImageButton imgTakeAssessment = (ImageButton)dgAssessmentType.Rows[y].FindControl("imgTakeAssessment");
                    Label lblAssessmentID = (Label)dgAssessmentType.Rows[y].FindControl("lblAssessmentID");

                    //string PreviewUrl = ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main) + "?aid=" + lblAssessmentID.Text;
                    string PreviewUrl = ResolveUrl(DefaultForms.frm_history_assessment_main) + "?aid=" + lblAssessmentID.Text;
                    imgTakeAssessment.ToolTip = "View Assessment";
                    imgTakeAssessment.PostBackUrl = PreviewUrl;
                }
            }
        }

        #region "dynamic"
        public class DynamicallyTemplatedGridViewHandler : IBindableTemplate
        {
            ListItemType ItemType;
            string FieldName;
            string InfoType;

            public DynamicallyTemplatedGridViewHandler(ListItemType item_type, string field_name, string info_type)
            {
                ItemType = item_type;
                FieldName = field_name;
                InfoType = info_type;
            }

            public void InstantiateIn(System.Web.UI.Control Container)
            {
                switch (ItemType)
                {
                    case ListItemType.Header:
                        Literal header_ltrl = new Literal();
                        header_ltrl.Text = "<b>" + FieldName + "</b>";
                        Container.Controls.Add(header_ltrl);
                        break;
                    case ListItemType.Item:
                        switch (InfoType)
                        {
                            case "Command":
                                ImageButton edit_button = new ImageButton();
                                edit_button.ID = "edit_button";
                                edit_button.ImageUrl = "~/images/edit.gif";
                                edit_button.CommandName = "Edit";
                                edit_button.Click += new ImageClickEventHandler(edit_button_Click);
                                edit_button.ToolTip = "Edit";
                                Container.Controls.Add(edit_button);

                                ImageButton delete_button = new ImageButton();
                                delete_button.ID = "delete_button";
                                delete_button.ImageUrl = "~/images/delete.gif";
                                delete_button.CommandName = "Delete";
                                delete_button.ToolTip = "Delete";
                                delete_button.OnClientClick = "return confirm('Are you sure to delete the record?')";
                                Container.Controls.Add(delete_button);

                                /* Similarly add button for insert.
                                * It is important to know when 'insert' button is added
                                * its CommandName is set to "Edit" like that of 'edit' button
                                * only because we want the GridView enter into Edit mode,
                                * and this time we also want the text boxes for corresponding fields empty*/
                                ImageButton insert_button = new ImageButton();
                                insert_button.ID = "insert_button";
                                insert_button.ImageUrl = "~/images/insert.bmp";
                                insert_button.CommandName = "Edit";
                                insert_button.ToolTip = "Insert";
                                insert_button.Click += new ImageClickEventHandler(insert_button_Click);
                                Container.Controls.Add(insert_button);

                                break;

                            default:
                                Label field_lbl = new Label();
                                field_lbl.ID = FieldName;
                                field_lbl.Text = String.Empty; //we will bind it later through 'OnDataBinding' event
                                field_lbl.DataBinding += new EventHandler(OnDataBinding);
                                Container.Controls.Add(field_lbl);
                                break;

                        }
                        break;
                    case ListItemType.EditItem:
                        if (InfoType == "Command")
                        {
                            ImageButton update_button = new ImageButton();
                            update_button.ID = "update_button";
                            update_button.CommandName = "Update";
                            update_button.ImageUrl = "~/images/update.gif";
                            if ((int)new Page().Session["InsertFlag"] == 1)
                                update_button.ToolTip = "Add";
                            else
                                update_button.ToolTip = "Update";
                            update_button.OnClientClick = "return confirm('Are you sure to update the record?')";
                            Container.Controls.Add(update_button);

                            ImageButton cancel_button = new ImageButton();
                            cancel_button.ImageUrl = "~/images/cancel.gif";
                            cancel_button.ID = "cancel_button";
                            cancel_button.CommandName = "Cancel";
                            cancel_button.ToolTip = "Cancel";
                            Container.Controls.Add(cancel_button);

                        }
                        else// for other 'non-command' i.e. the key and non key fields, bind textboxes with corresponding field values
                        {
                            TextBox field_txtbox = new TextBox();
                            field_txtbox.ID = FieldName;
                            field_txtbox.Text = String.Empty;
                            // if Inert is intended no need to bind it with text..keep them empty
                            if ((int)new Page().Session["InsertFlag"] == 0)
                                field_txtbox.DataBinding += new EventHandler(OnDataBinding);
                            Container.Controls.Add(field_txtbox);
                        }
                        break;

                }

            }

            public IOrderedDictionary ExtractValues(Control Container)
            {
                OrderedDictionary dictionary = new OrderedDictionary();
                if (ItemType == ListItemType.EditItem)
                {
                    string field_text = ((TextBox)Container.Controls[0]).Text;
                    dictionary.Add(FieldName, field_text);
                }
                else
                {
                    string field_text = ((Label)Container.Controls[0]).Text;
                    dictionary.Add(FieldName, field_text);

                }
                return dictionary;
            }
            //just sets the insert flag ON so that we ll be able to decide in OnRowUpdating event whether to insert or update
            protected void insert_button_Click(Object sender, EventArgs e)
            {
                new Page().Session["InsertFlag"] = 1;
            }
            //just sets the insert flag OFF so that we ll be able to decide in OnRowUpdating event whether to insert or update
            protected void edit_button_Click(Object sender, EventArgs e)
            {
                new Page().Session["InsertFlag"] = 0;
            }

            private void OnDataBinding(object sender, EventArgs e)
            {

                object bound_value_obj = null;
                Control ctrl = (Control)sender;
                IDataItemContainer data_item_container = (IDataItemContainer)ctrl.NamingContainer;
                bound_value_obj = DataBinder.Eval(data_item_container.DataItem, FieldName);

                switch (ItemType)
                {
                    case ListItemType.Item:
                        Label field_ltrl = (Label)sender;
                        field_ltrl.Text = bound_value_obj.ToString();

                        break;
                    case ListItemType.EditItem:
                        TextBox field_txtbox = (TextBox)sender;
                        field_txtbox.Text = bound_value_obj.ToString();

                        break;
                }
            }
        }
        #endregion

        //Executed when the grid view was databound
        protected void dgAssessmentType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < dgAssessments.Rows.Count; x++)
            {
                GridView dgAssessmentType = (GridView)dgAssessments.Rows[x].FindControl("dgAssessmentType");
                for (int y = 0; y < dgAssessmentType.Rows.Count; y++)
                {
                    ImageButton imgTakeAssessment = (ImageButton)dgAssessmentType.Rows[y].FindControl("imgTakeAssessment");
                    Label lblAssessmentID = (Label)dgAssessmentType.Rows[y].FindControl("lblAssessmentID");

                    string PreviewUrl = ResolveUrl(DefaultForms.frm_history_assessment_view) + "?aid=" + lblAssessmentID.Text;
                    imgTakeAssessment.ToolTip = "View Assessment";
                    imgTakeAssessment.PostBackUrl = PreviewUrl;
                }
            }
        }

        protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load all past assessment based on quarter
            LoadAssessmentHistory();
        }
    }
}
