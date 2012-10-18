using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Globalization;
using System.Data.OleDb;
using System.Data;

namespace PAOnlineAssessment.instructor
{
    public partial class makeup_exam_list : System.Web.UI.Page
    {
        LoginUser LUser;

        GlobalForms DefaultForms = new Collections().getDefaultForms();

        Collections cls = new Collections();

        List<Constructors.RegistrationTerm> oRegistrationTerm;

        List<Constructors.Subject> oSubject;

        List<Constructors.AssessmentType> oAssessmentType;

        List<Constructors.AssessmentView> oAssessment;

        List<Constructors.GradingView> oGradingView;

        List<Constructors.StudentRegistrationView> oStudentListRegistration;

        List<Constructors.StudentAccount> oStudentListAssessment;

        List<Constructors.StudentAnswers> oStudentAnswer;

        List<Constructors.User> UsersList;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User Is Logged In
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if (LUser.UserGroupID == "1" || LUser.UserGroupID == "3")
                {

                }
                else
                {
                    Response.Write("<script>alert('Access Denied!'); window.location='" + ResolveUrl(DefaultForms.frm_index) + "';</script>");
                }

                if (Validator.CanbeAccess("16", LUser.AccessRights) == false)
                {
                    Validator.AlertBack("Access Denied!", "../block_user.aspx");
                }
            }
            //Redirect to Login Page when no User is logged in
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            if (!Page.IsPostBack)
            {
                LoadTeachers(Convert.ToInt32(LUser.UserGroupID));

                loadLevels();

                loadSubjects();

                loadSections();

                loadAssessmentType();

                loadAssessment();

                loadStudentList();
            }
        }

        void LoadTeachers(int UGID)
        {
            //Create a new table with lastname, firstname and teacherid/userid
            DataTable dt = new DataTable();
            //Adding new column in the data table
            dt.Columns.Add("LastName");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("TeacherID");

            UsersList = new List<Constructors.User>(cls.getUsers());
            UsersList.ForEach(ul =>
            {
                if (ul.UserGroupID == 3)
                {
                    dt.Rows.Add(ul.LastName, ul.FirstName, ul.UserID);
                    cboTeacher.Items.Add(new ListItem(ul.LastName + ", " + ul.FirstName, ul.UserID.ToString()));
                }
            });

            //clear the items
            cboTeacher.Items.Clear();

            //Sorting Teachers Name alphabetically
            DataView dv = new DataView(dt);
            dv.Sort = " LastName, FirstName";
            foreach (DataRowView view in dv)
            {
                cboTeacher.Items.Add(view[0].ToString() + ", " + view[1].ToString());
                cboTeacher.Items[cboTeacher.Items.Count - 1].Value = view[2].ToString();
            }
            
            if (UGID == 1)
            {
                cboTeacher.Enabled = true;
            }
            else
            {
                for (int i = 0; i < cboTeacher.Items.Count; i++)
                {
                    if (cboTeacher.Items[i].Value == LUser.UserID.ToString())
                    {
                        cboTeacher.Items[i].Selected = true;
                    }
                }
                cboTeacher.Enabled = false;
            }
            //GetSubjects();
        }
        void ShowTheDetails(int AssessmentID, int SectionID)
        {
            oAssessment = new List<Constructors.AssessmentView>(cls.getAssessmentView());
            oAssessment.ForEach(a =>
            {
                if (a.AssessmentID == AssessmentID)
                {
                    cboGradeLevel.SelectedValue = a.LevelID.ToString();

                    loadSections();
                    cboSection.SelectedValue = SectionID.ToString();

                    loadSubjects();
                    cboSubject.SelectedValue = a.SubjectID.ToString();

                    cboAssessmentType.SelectedValue = a.AssessmentTypeID.ToString();
                    loadAssessment();
                    cboAssessment.SelectedValue = a.AssessmentID.ToString();
                    
                }
            });

            loadStudentList();
        }

        void loadAssessmentType()
        {
            cboAssessmentType.Items.Clear();
            cboAssessmentType.Items.Add("--- Select Assessment Type ---");
            cboAssessmentType.Items[cboAssessmentType.Items.Count - 1].Value = "0";

            oAssessmentType = new List<Constructors.AssessmentType>(cls.getAssessmentType());

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AssessmentTypeID"));
            dt.Columns.Add(new DataColumn("AssessmentDescription"));

            oAssessmentType.ForEach(at =>
            {
                if (at.Status == "A")
                    dt.Rows.Add(at.AssessmentTypeID, at.Description);
            });

            //Loop through Filtered Duplicate values in the DataTable
            foreach (DataRow drow in dt.DefaultView.ToTable(true, new string[] { "AssessmentTypeID", "AssessmentDescription" }).Rows)
            {
                //Add to Grade / Level DropDownList
                cboAssessmentType.Items.Add(drow[1].ToString());
                cboAssessmentType.Items[cboAssessmentType.Items.Count - 1].Value = drow[0].ToString();
            }
        }

        void loadLevels()
        {
            cboGradeLevel.Items.Clear();
            cboGradeLevel.Items.Add("--- Select Level ---");
            cboGradeLevel.Items[cboGradeLevel.Items.Count - 1].Value = "0";

            //Instantiate new List
            oRegistrationTerm = new List<Constructors.RegistrationTerm>(cls.getRegistrationTerm());
            oGradingView = new List<Constructors.GradingView>(cls.getGradingView());

            //Create a DataTable containing the LevelID and LevelDescription
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LevelID"));
            dt.Columns.Add(new DataColumn("LevelDescription"));

            oGradingView.ForEach(gv =>
            {
                if (gv.TeacherID.ToString() == cboTeacher.SelectedValue && gv.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    //Loop through List of Registration Terms
                    oRegistrationTerm.ForEach(rt =>
                    {
                        if (rt.SchoolYear == Session["CurrentSchoolYear"].ToString() && gv.LevelID == rt.LevelID)
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
                cboGradeLevel.Items.Add(drow[1].ToString());
                cboGradeLevel.Items[cboGradeLevel.Items.Count - 1].Value = drow[0].ToString();
            }
        }

        void loadSubjects()
        {
            //clear all items that inside the combobox
            cboSubject.Items.Clear();
            cboSubject.Items.Add("--- Select Subject ---");
            cboSubject.Items[cboSubject.Items.Count - 1].Value = "0";

            oSubject = new List<Constructors.Subject>(new Collections().getSubjectList());
            oGradingView = new List<Constructors.GradingView>(cls.getGradingView());

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SubjectID"));
            dt.Columns.Add(new DataColumn("SubjectDescription"));

            oGradingView.ForEach(gv =>
            {
                if (gv.TeacherID.ToString() == cboTeacher.SelectedValue.ToString() && gv.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    oSubject.ForEach(s =>
                    {
                        if (s.LevelID == Convert.ToInt32(cboGradeLevel.SelectedValue) && gv.SubjectID == s.SubjectID)
                        {
                            dt.Rows.Add(s.SubjectID, s.Description);
                        }
                    });
                }
            });

            foreach (DataRow dRow in dt.DefaultView.ToTable(true, new string[] { "SubjectID", "SubjectDescription" }).Rows)
            {
                cboSubject.Items.Add(dRow[1].ToString());
                cboSubject.Items[cboSubject.Items.Count - 1].Value = dRow[0].ToString();
            }
        }

        void loadSections()
        {
            cboSection.Items.Clear();
            cboSection.Items.Add("--- Select Section ---");
            cboSection.Items[cboSection.Items.Count - 1].Value = "0";

            oRegistrationTerm = new List<Constructors.RegistrationTerm>(cls.getRegistrationTerm());
            oGradingView = new List<Constructors.GradingView>(cls.getGradingView());

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SectionID"));
            dt.Columns.Add(new DataColumn("SectionDescription"));

            oGradingView.ForEach(gv =>
            {
                if (gv.TeacherID.ToString() == cboTeacher.SelectedValue && gv.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    oRegistrationTerm.ForEach(s =>
                    {
                        if (s.SchoolYear == gv.SchoolYear && s.LevelID == Convert.ToInt32(cboGradeLevel.SelectedValue) && gv.SectionID == s.SectionID)
                        {
                            dt.Rows.Add(s.SectionID, s.SectionDescription);
                        }
                    });
                }
            });

            foreach (DataRow dRow in dt.DefaultView.ToTable(true, new string[] { "SectionID", "SectionDescription" }).Rows)
            {
                cboSection.Items.Add(dRow[1].ToString());
                cboSection.Items[cboSection.Items.Count - 1].Value = dRow[0].ToString();
            }
        }

        protected void cboGradeLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSubjects();
            loadSections();
            loadAssessment();
            loadStudentList();
        }

        void loadAssessment()
        {
            cboAssessment.Items.Clear();
            cboAssessment.Items.Add("--- Select Assessment ---");
            cboAssessment.Items[cboAssessment.Items.Count - 1].Value = "0";

            oAssessment = new List<Constructors.AssessmentView>(cls.getAssessmentView());

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AssessmentID"));
            dt.Columns.Add(new DataColumn("Title"));

            oAssessment.ForEach(a =>
            {
                if (a.Quarter == Session["Quarter"].ToString() && a.SchoolYear == Session["CurrentSchoolYear"].ToString() && a.UserID.ToString() == cboTeacher.SelectedValue && a.LevelID == Convert.ToInt32(cboGradeLevel.SelectedValue) && a.SubjectID == Convert.ToInt32(cboSubject.SelectedValue) && a.AssessmentTypeID == Convert.ToInt32(cboAssessmentType.SelectedValue))
                {
                    dt.Rows.Add(a.AssessmentID, a.Title);
                }
            });

            foreach (DataRow dRow in dt.DefaultView.ToTable(true, new string[] { "AssessmentID", "Title" }).Rows)
            {
                cboAssessment.Items.Add(dRow[1].ToString());
                cboAssessment.Items[cboAssessment.Items.Count - 1].Value = dRow[0].ToString();
            }
        }

        protected void cboAssessmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAssessment();
            loadStudentList();
        }

        protected void cboSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAssessment();
            loadStudentList();
        }

        protected void cboSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAssessment();
            loadStudentList();
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            loadStudentList();
        }

        void loadStudentList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("StudentID"));
            dt.Columns.Add(new DataColumn("StudentNumber"));
            dt.Columns.Add(new DataColumn("LastName"));
            dt.Columns.Add(new DataColumn("FirstName"));
            dt.Columns.Add(new DataColumn("Status"));
            dt.Columns.Add(new DataColumn("AssessmentStatus"));

            oStudentListAssessment = new List<Constructors.StudentAccount>(cls.getStudentAccounts());
            oStudentListRegistration = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            oStudentAnswer = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());

            string Status = "";
            string AssessmentStatus = "";

            oAssessment = new List<Constructors.AssessmentView>(cls.getAssessmentView());
            oAssessment.ForEach(a =>
            {
                if (a.AssessmentID == Convert.ToInt32(cboAssessment.SelectedValue))
                {
                    //check if has schedule
                    if (a.Schedule == "Yes")
                    {
                        //check if the assessment is close by its schedule
                        if (Convert.ToDateTime(a.DateEnd).Date < DateTime.Now.Date)
                        {
                            oStudentListRegistration.ForEach(slr =>
                            {
                                oStudentListAssessment.ForEach(sla =>
                                {
                                    //check if has same student number
                                    if (slr.SchoolYear == Session["CurrentSchoolYear"].ToString() && sla.StudentNumber == slr.StudentNumber && slr.LevelID.ToString() == cboGradeLevel.SelectedValue && slr.SectionID.ToString() == cboSection.SelectedValue)
                                    {
                                        if (a.MakeUp.Contains("-" + slr.StudentID + "-"))
                                        {
                                            AssessmentStatus = "Opened";
                                        }
                                        else if (a.NoMakeUp.Contains("-" + slr.StudentID + "-"))
                                        {
                                            AssessmentStatus = "Closed";
                                        }
                                        else
                                        {
                                            AssessmentStatus = "";
                                        }

                                        string Check = cls.ExecuteScalar("Select StudentAnswerID FROM PaceAssessment.dbo.StudentAnswers WHERE StudentID=" + slr.StudentID + " and AssessmentID=" + a.AssessmentID + "");
                                        if (!string.IsNullOrEmpty(Check))
                                        {
                                            Status = "Taken";
                                        }
                                        else
                                        {
                                            Status = "Not Taken";
                                        }


                                        dt.Rows.Add(slr.StudentID, slr.StudentNumber, sla.Lastname, sla.Firstname, Status, AssessmentStatus);
                                    }
                                });
                            });
                        }
                    }
                    else if (a.Schedule == "No")
                    {
                        if (a.ScheduleStatus == "Close")
                        {
                            string AssessmentID = cls.ExecuteScalar("SELECT AssessmentID FROM PaceAssessment.dbo.StudentAnswers where AssessmentID='" + cboAssessment.SelectedValue + "'");
                            if (!string.IsNullOrEmpty(AssessmentID))
                            {
                                oStudentListRegistration.ForEach(slr =>
                                {
                                    oStudentListAssessment.ForEach(sla =>
                                    {
                                        //check if has same student number
                                        if (slr.SchoolYear == Session["CurrentSchoolYear"].ToString() && sla.StudentNumber == slr.StudentNumber && slr.LevelID.ToString() == cboGradeLevel.SelectedValue && slr.SectionID.ToString() == cboSection.SelectedValue)
                                        {
                                            if (a.MakeUp.Contains("-" + slr.StudentID + "-"))
                                            {
                                                AssessmentStatus = "Opened";
                                            }
                                            else if (a.NoMakeUp.Contains("-" + slr.StudentID + "-"))
                                            {
                                                AssessmentStatus = "Closed";
                                            }
                                            else
                                            {
                                                AssessmentStatus = "";
                                            }

                                            string Check = cls.ExecuteScalar("Select StudentAnswerID FROM PaceAssessment.dbo.StudentAnswers WHERE StudentID=" + slr.StudentID + " and AssessmentID=" + a.AssessmentID + "");
                                            if (!string.IsNullOrEmpty(Check))
                                            {
                                                Status = "Taken";
                                            }
                                            else
                                            {
                                                Status = "Not Taken";
                                            }


                                            dt.Rows.Add(slr.StudentID, slr.StudentNumber, sla.Lastname, sla.Firstname, Status, AssessmentStatus);
                                        }
                                    });
                                });
                            }
                        }
                    }
                }
            });

            Session["MakeUp"] = dt;
            dgStudent.DataSource = Session["MakeUp"];
            dgStudent.DataBind();
        }

        protected void dgStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)Session["MakeUp"];
            for (int row = 0; row < dgStudent.Rows.Count; row++)
            {
                Label lblStudentID = (Label)dgStudent.Rows[row].FindControl("lblStudentID");
                Label lblStudentNumber = (Label)dgStudent.Rows[row].FindControl("lblStudentNumber");
                Label lblStatus = (Label)dgStudent.Rows[row].FindControl("lblStatus");
                Label lblAssessmentStatus = (Label)dgStudent.Rows[row].FindControl("lblAssessmentStatus");
                CheckBox chk = (CheckBox)dgStudent.Rows[row].FindControl("chk");

                if (lblStatus.Text == "Taken")
                {
                    chk.Checked = false;
                    chk.Enabled = false;
                    if (!string.IsNullOrEmpty(lblAssessmentStatus.Text.Trim()))
                    {
                        chk.Checked = true;
                    }
                }
                else
                {
                    string MakeUp = cls.ExecuteScalar("SELECT MakeUp FROM Assessment where AssessmentID='" + cboAssessment.SelectedValue + "'");
                    string NoMakeUp = cls.ExecuteScalar("SELECT NoMakeUp FROM Assessment where AssessmentID='" + cboAssessment.SelectedValue + "'");

                    if (MakeUp.Contains("-" + lblStudentID.Text + "-"))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["StudentID"].ToString() == lblStudentID.Text)
                            {
                                dr["AssessmentStatus"] = "Opened";
                                dt.AcceptChanges();
                            }
                        }
                        chk.Enabled = false;
                        chk.Checked = true;
                    }
                    else if (NoMakeUp.Contains("-" + lblStudentID.Text + "-"))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["StudentID"].ToString() == lblStudentID.Text)
                            {
                                dr["AssessmentStatus"] = "Closed";
                                dt.AcceptChanges();
                            }
                        }
                        chk.Enabled = false;
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Enabled = true;
                        chk.Checked = false;

                        //actions
                        //string OpenUrl = ResolveUrl(DefaultForms.frm_makeup_exam_list) + "?action=open&sid=" + lblStudentID.Text + "&aid=" + cboAssessment.SelectedValue + "&sec=" + cboSection.SelectedValue;
                        //string CloseUrl = ResolveUrl(DefaultForms.frm_makeup_exam_list) + "?action=close&sid=" + lblStudentID.Text + "&aid=" + cboAssessment.SelectedValue + "&sec=" + cboSection.SelectedValue;
                        //imgClose.OnClientClick = "if (confirm('Are you sure you want to close this Assessment?')){window.location='" + CloseUrl + "'} return false;";
                        //imgOpen.OnClientClick = "if (confirm('Are you sure you want to open this Assessment?')){window.location='" + OpenUrl + "'} return false;";
                    }
                }
            }
        }

        void OpenFunction()
        {
            string MakeUp = cls.ExecuteScalar("SELECT MakeUp FROM PaceAssessment.dbo.Assessment WHERE AssessmentID='" + Request.QueryString["aid"] + "'");
            MakeUp = MakeUp + Request.QueryString["sid"] + "-";

            string qry = "UPDATE PaceAssessment.dbo.Assessment SET MakeUp='" + MakeUp + "' WHERE AssessmentID='" + Request.QueryString["aid"] + "'";
            cls.ExecuteNonQuery(qry);
            Response.Write("<script>alert('Assessment has been opened successfully.')</script>");
            //ShowTheDetails(Convert.ToInt32(Request.QueryString["aid"].ToString()));
            loadStudentList();
        }

        void CloseFunction()
        {
            string NoMakeUp = cls.ExecuteScalar("SELECT NoMakeUp FROM PaceAssessment.dbo.Assessment WHERE AssessmentID='" + Request.QueryString["aid"] + "'");
            NoMakeUp = NoMakeUp + Request.QueryString["sid"] + "-";

            string qry = "UPDATE PaceAssessment.dbo.Assessment SET NoMakeUp='" + NoMakeUp + "' WHERE AssessmentID='" + Request.QueryString["aid"] + "'";
            cls.ExecuteNonQuery(qry);
            Response.Write("<script>alert('Assessment has been closed successfully.')</script>");
            //ShowTheDetails(Convert.ToInt32(Request.QueryString["aid"].ToString()));
            loadStudentList();
        }

        protected void cboTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadLevels();
            loadSections();
            loadSubjects();
            loadAssessment();
            loadStudentList();
        }

        protected void lnkOpen_Click(object sender, EventArgs e)
        {
            OpenAssessment();
        }

        void OpenAssessment()
        {
            string qry = "";
            lblError.Text = "*";
            DataTable dt = (DataTable)Session["MakeUp"];
            if (cboAssessment.SelectedIndex > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    int success = 0;
                    for (int row = 0; row < dgStudent.Rows.Count; row++)
                    {
                        Label lblStudentiID = (Label)dgStudent.Rows[row].FindControl("lblStudentID");
                        CheckBox chk = (CheckBox)dgStudent.Rows[row].FindControl("chk");

                        string MakeUp = cls.ExecuteScalar("SELECT MakeUp FROM PaceAssessment.dbo.Assessment WHERE AssessmentID='" + cboAssessment.SelectedValue + "'");
                        MakeUp = MakeUp + lblStudentiID.Text + "-";

                        if (chk.Enabled == true && chk.Checked == true)
                        {
                            qry = "UPDATE PaceAssessment.dbo.Assessment SET MakeUp='" + MakeUp + "' WHERE AssessmentID='" + cboAssessment.SelectedValue + "'";
                            cls.ExecuteNonQuery(qry);
                            success++;
                        }
                    }

                    if (success > 0)
                    {
                        Response.Write("<script>alert('Assessment has been opened successfully to the selected student(s). ')</script>");
                        ShowTheDetails(Convert.ToInt32(cboAssessment.SelectedValue.ToString()),Convert.ToInt32(cboSection.SelectedValue.ToString()));
                    }
                    else
                    {
                        lblError.Text = "* Action cannot continue. Please make a selection.";
                    }
                }
                else
                {
                    lblError.Text = "* Action cannot continue. Please check your inputs.";
                }
            }
            else
            {
                lblError.Text = "* Please select an assessment first.";
            }
        }

        protected void lnkClose_Click(object sender, EventArgs e)
        {
            string qry = "";
            lblError.Text = "*";
            DataTable dt = (DataTable)Session["MakeUp"];
            if (cboAssessment.SelectedIndex > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    int success = 0;
                    for (int row = 0; row < dgStudent.Rows.Count; row++)
                    {
                        Label lblStudentiID = (Label)dgStudent.Rows[row].FindControl("lblStudentID");
                        CheckBox chk = (CheckBox)dgStudent.Rows[row].FindControl("chk");

                        string NoMakeUp = cls.ExecuteScalar("SELECT NoMakeUp FROM PaceAssessment.dbo.Assessment WHERE AssessmentID='" + cboAssessment.SelectedValue + "'");
                        NoMakeUp = NoMakeUp + lblStudentiID.Text + "-";

                        if (chk.Enabled == true && chk.Checked == true)
                        {
                            qry = "UPDATE PaceAssessment.dbo.Assessment SET NoMakeUp='" + NoMakeUp + "' WHERE AssessmentID='" + cboAssessment.SelectedValue + "'";
                            cls.ExecuteNonQuery(qry);
                            success++;
                        }
                    }

                    if (success > 0)
                    {
                        Response.Write("<script>alert('Assessment has been closed successfully to the selected student(s). ')</script>");
                        ShowTheDetails(Convert.ToInt32(cboAssessment.SelectedValue.ToString()), Convert.ToInt32(cboSection.SelectedValue.ToString()));
                    }
                    else
                    {
                        lblError.Text = "* Action cannot continue. Please make a selection.";
                    }
                }
                else
                {
                    lblError.Text = "* Action cannot continue. Please check your inputs.";
                }
            }
            else
            {
                lblError.Text = "* Please select an assessment first.";
            }
        }
    }
}
