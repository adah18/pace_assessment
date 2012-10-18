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

namespace PAOnlineAssessment.instructor
{
    public partial class advisers_studentsview : System.Web.UI.Page
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
        //user list
        List<Constructors.User> UsersList;
        List<Constructors.GradingView> GradingView;
        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is logged in
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if (LUser.UserGroupID == "1" || LUser.UserGroupID == "3")
                {

                }
                else
                {
                    Response.Write("<script>alert('Access Denied!'); window.location='" + ResolveUrl(DefaultForms.frm_instructor_subjects) + "';</script>");
                }

                if (Validator.CanbeAccess("14", LUser.AccessRights) == false)
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

            LoadSiteMapDetails();
            if (!IsPostBack)
            {
                cboQuarter.SelectedValue = Session["Quarter"].ToString();
                LoadTeachers(Convert.ToInt32(LUser.UserGroupID));
                LoadAllSubjects();
                grdStudentsList.DataSource = "";
                grdStudentsList.DataBind();
            }

            txtSearchQuery.Text = cboSubjects.SelectedValue.ToString();
            if (LUser.UserGroupID != "1")
            {
                if (isHaveAdvisory())
                {
                    //Debug.WriteLine("SectionID: " + Session["SectionID"].ToString());
                    if (LoadAllStudent() > 0)
                    {
                        GetAssessmentTypes();
                        GetAllSubitems();
                        GetScores();
                    }
                    else
                    {
                        Response.Write("<script>alert('No students found.');window.location='" + ResolveUrl(DefaultForms.frm_index) + "'</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('You dont have an assigned class. Ask the administrator for confirmation.');window.location='" + ResolveUrl(DefaultForms.frm_index) + "'</script>");
                }
            }
            else
            {
                if (cboSubjects.Items.Count > 0)
                {
                    if (isHaveAdvisory())
                    {
                        //Debug.WriteLine("SectionID: " + Session["SectionID"].ToString());
                        if (LoadAllStudent() > 0)
                        {
                            GetAssessmentTypes();
                            GetAllSubitems();
                            GetScores();
                        }
                    }
                }
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
        //Load info for the site map
        public void LoadSiteMapDetails()
        {
            SiteMap1.RootNode = "Dashboard";
            SiteMap1.RootNodeToolTip = "Dashboard";
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.ParentNode = "Academic Activities";
            SiteMap1.ParentNodeToolTip = "Click to go back to My Subjects";
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.CurrentNode = "My Advisory Class";
        }
        //verify if the Teacher has advisory class
        private bool isHaveAdvisory()
        {
            bool isHave = false;
            GradingView = new List<Constructors.GradingView>(cls.getGradingView());
            GradingView.ForEach(gv =>
            {
                if (gv.AdvisoryTeacherID == cboTeacher.SelectedValue && gv.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    Session["SectionID"] = gv.SectionID;
                    isHave = true;
                }
            });
            return isHave;
        }

        #region "Loading Events"
        //load all students based on the selected subject

        void LoadAllSubjects()
        {
            //clear the subjects
            cboSubjects.Items.Clear();
            cboSubjects.Items.Add(new ListItem("--- Select Subject ---", "0"));
            
            //instantiate new list
            GradingView = new List<Constructors.GradingView>(cls.getGradingView());
            //loop through the list
            GradingView.ForEach(gv =>
            {
                if (gv.AdvisoryTeacherID == cboTeacher.SelectedValue && gv.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    cboSubjects.Items.Add(new ListItem(gv.Description, gv.SubjectID.ToString()));
                }
            });
        }

        private int LoadAllStudent()
        {
            int SectionID = (int)Session["SectionID"];
            //create datatable that will temporarily hold all the students 
            DataTable dt = new DataTable();
            //create column
            dt.Columns.Add("StudentNumber");
            dt.Columns.Add("StudentName");
            dt.Columns.Add("StudentID");

            int count = 0;
            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentList.ForEach(sl => 
            {
                if (sl.SectionID == SectionID && sl.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    StudentAccountList = new List<Constructors.StudentAccount>(cls.getStudentAccounts());
                    StudentAccountList.ForEach(sa =>
                    {
                        if (sa.StudentNumber == sl.StudentNumber)
                        {
                            //
                            string RealStudID = cls.ExecuteScalar("Select StudentID From PaceRegistration.dbo.Student Where StudentNumber='" + sa.StudentNumber + "'");
                            count++;
                            dt.Rows.Add(
                                sa.StudentNumber,
                                sa.Lastname + ", " + sa.Firstname,
                                RealStudID
                            );
                        }
                    });
                }
            });

            Session["StudentsList"] = dt;
            //grdView.DataSource = Session["StudentsList"];
            //grdView.DataBind();
            return count;
        }
        //get all the assessment type for the selected subject
        void GetAssessmentTypes()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(Session["SectionID"]);
            DataTable dt = new DataTable();
            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            AssessmentType.ForEach(at =>
            {
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
            Session["AssessmentTypes"] = dt;
            //grdView.DataSource = Session["AssessmentTypes"];
            //grdView.DataBind();

        }
        //Get all the sub items for each type
        void GetAllSubitems()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(cboSubjects.SelectedValue);
            int SectionID = Convert.ToInt32(Session["SectionID"]);
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
                    if (at.Quarter == cboQuarter.SelectedValue.ToString() && at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(AssessmentTypes.Rows[i][0].ToString()) == at.AssessmentTypeID)
                    {
                        dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                    }
                });

            }
            //pass the data to a session
            Session["Subitems"] = dt;
            //grdView.DataSource = Session["Subitems"];
            //grdView.DataBind();
        }

        //get the scores of a students for the assessment
        void GetScores()
        {
            DataTable StudentLists = (DataTable)Session["StudentsList"];
            DataTable Subitems = (DataTable)Session["Subitems"];
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Score");
            dt.Columns.Add("Title");
            GenerateGridViewData();
        }

        //Generate all data gathered
        void GenerateGridViewData()
        {
            //declare data table for scores
            DataTable Scores = (DataTable)Session["Scores"];
            //declare data table for student list
            DataTable StudentLists = (DataTable)Session["StudentsList"];
            //declare data table for subitems
            DataTable Subitems = (DataTable)Session["Subitems"];
            //declare a data table
            DataTable dt = new DataTable();
            //create columns
            dt.Columns.Add("Count");
            dt.Columns.Add("StudentName");

            //add columns based on all sub items
            Debug.WriteLine("--- Subitems Check ---");
            for (int i = 0; i < Subitems.Rows.Count; i++)
            {
                dt.Columns.Add(Subitems.Rows[i][1].ToString());
                Debug.WriteLine("Subitem: " + Subitems.Rows[i][1].ToString());
            }

            //arranged the names
            StudentLists.DefaultView.Sort = "StudentName";
            DataView dv = StudentLists.DefaultView;

            int count = 0;
            foreach (DataRowView dvRow in dv)
            {
                count++;
                dt.Rows.Add(count, dvRow[1].ToString());
                for (int j = 0; j < Subitems.Rows.Count; j++)
                {
                    dt.Rows[dt.Rows.Count - 1][j + 2] = GetAssessmentScore(Convert.ToInt32(dvRow[2]), Convert.ToInt32(Subitems.Rows[j][0]));
                }
            }

            //Add Data to the data table
            //for (int i = 0; i < StudentLists.Rows.Count; i++)
            //{
            //    dt.Rows.Add(i + 1, StudentLists.Rows[i][1].ToString());
            //    for (int j = 0; j < Subitems.Rows.Count; j++)
            //    {
            //        dt.Rows[dt.Rows.Count - 1][j + 2] = GetAssessmentScore(Convert.ToInt32(StudentLists.Rows[i][2]), Convert.ToInt32(Subitems.Rows[j][0]));
            //    }
            //}

            //pass the data tables value to a session
            dt.DefaultView.Sort = "StudentName";
            Session["MainGrid"] = dt;
            grdView.DataSource = Session["MainGrid"];
            grdView.DataBind();

           if (isHaveAssessment()) 
               LoadToDynamicGridView();
        }

        string GetAssessmentScore(int StudentID, int AssessmentID)
        {
            string final_score = "";
            AssessmentList = new List<Constructors.Assessment>(cls.getAssessment());
            AssessmentList.ForEach(al =>
            {
                if (al.AssessmentID == AssessmentID)
                {
                    if (isAssessmentTaken(StudentID, AssessmentID))
                    {
                        double score = 0;
                        StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                        StudentAnswersList.ForEach(sa =>
                        {
                            if (sa.StudentID == StudentID && sa.AssessmentID == AssessmentID)
                            {
                                string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                if (ans == sa.SelectedAnswer)
                                {
                                    //get the points for the question
                                    string points = cls.ExecuteScalar("Select Points From PaceAssessment.dbo.AssessmentDetails Where AssessmentID=" + sa.AssessmentID + "");
                                    //add the point to the current score
                                    score += Convert.ToDouble(points);
                                }
                                else
                                {
                                    //if the selected score did not match no point will be added
                                    score += 0;
                                }

                            }
                        });

                        final_score = score.ToString();
                    }
                    else
                    {   
                        string DateS = Convert.ToDateTime(al.DateStart).ToShortDateString() + " " + al.TimeStart;
                        DateTime DateStart = Convert.ToDateTime(DateS);
                        string DateE = Convert.ToDateTime(al.DateEnd).ToShortDateString() + " " + al.TimeEnd;
                        DateTime DateEnd = Convert.ToDateTime(DateE);
                        if (DateStart >= DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not yet taken");
                            final_score = "-";
                        }
                        else if (DateEnd < DateTime.Now)
                        {
                            //Debug.WriteLine(Subitems.Rows[j][1].ToString() + " is not taken");
                            final_score = "0";
                        }
                        else
                        {
                            final_score = "-";
                        }
                    }

                }
            });

            return final_score;
        }

        bool isAssessmentTaken(int StudentID, int AssessmentID)
        {
            bool x = false;
            StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
            StudentAnswersList.ForEach(sa => 
            {
                if (sa.StudentID == StudentID && sa.AssessmentID == AssessmentID)
                {
                    x = true;
                }
            });
            return x;
        }
        bool isHaveAssessment()
        {
            bool isHave;
            DataTable dt = (DataTable)Session["Subitems"];
            int i;
            for (i = 0; i < dt.Rows.Count; i++) { }
            if (i == 0)
            {
                isHave = false;
                grdStudentsList.DataSource = "";
                grdStudentsList.DataBind();
                
            }
            else
            {
                isHave = true;
               
            }
            return isHave;          
        }

        //Create Gridview with dynamic templates
        void LoadToDynamicGridView()
        {
            grdStudentsList.Columns.Clear();
            //create new template
            TemplateField RowCount = new TemplateField();
            //add info for the item template
            RowCount.ItemTemplate = new DynamicallyTemplatedGridViewHandler(ListItemType.Item, "Count", "Count");
            //add info for the header template
            //RowCount.HeaderTemplate = new DynamicallyTemplatedGridViewHandler(ListItemType.Header, "No", "No");
            //add the template to the fgrid view
            grdStudentsList.Columns.Add(RowCount);

            TemplateField StudentName = new TemplateField();
            StudentName.ItemTemplate = new DynamicallyTemplatedGridViewHandler(ListItemType.Item, "StudentName", "StudentName");
            //StudentName.HeaderTemplate = new DynamicallyTemplatedGridViewHandler(ListItemType.Header, "StudentName", "StudentName");
            grdStudentsList.Columns.Add(StudentName);

            //get the score for creating its templates
            DataTable types = (DataTable)Session["Subitems"];
            for (int i = 0; i < types.Rows.Count; i++)
            {
                TemplateField AssessmentType = new TemplateField();

                AssessmentType.ItemTemplate = new DynamicallyTemplatedGridViewHandler(ListItemType.Item, types.Rows[i][1].ToString(), types.Rows[i][1].ToString());
                AssessmentType.HeaderTemplate = new DynamicallyTemplatedGridViewHandler(ListItemType.Header, types.Rows[i][1].ToString(), types.Rows[i][1].ToString());
                grdStudentsList.Columns.Add(AssessmentType);
            }

            //generating the design of the grid view
            grdStudentsList.Columns[1].ItemStyle.Width = Unit.Pixel(200);
            grdStudentsList.Columns[1].HeaderStyle.Width = Unit.Pixel(200);
            for (int i = 0; i < grdStudentsList.Columns.Count; i++)
            {
                grdStudentsList.Columns[0].HeaderStyle.Width = Unit.Pixel(20);

                grdStudentsList.Columns[i].HeaderStyle.BackColor = Color.White;
                grdStudentsList.Columns[i].HeaderStyle.BorderColor = Color.Black;
                grdStudentsList.Columns[i].HeaderStyle.BorderStyle = BorderStyle.Solid;
                grdStudentsList.Columns[i].HeaderStyle.ForeColor = Color.Blue;
                grdStudentsList.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                grdStudentsList.Columns[i].HeaderStyle.VerticalAlign = VerticalAlign.Middle;
                grdStudentsList.Columns[i].HeaderStyle.Wrap = false;

                grdStudentsList.Columns[0].ItemStyle.Width = Unit.Pixel(20);
                grdStudentsList.Columns[i].ItemStyle.BackColor = Color.White;
                grdStudentsList.Columns[i].ItemStyle.BorderColor = Color.Black;
                grdStudentsList.Columns[i].ItemStyle.BorderStyle = BorderStyle.Solid;
                grdStudentsList.Columns[i].ItemStyle.ForeColor = Color.Black;
                grdStudentsList.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                grdStudentsList.Columns[i].ItemStyle.VerticalAlign = VerticalAlign.Middle;
                grdStudentsList.Columns[i].ItemStyle.Wrap = false;
            }

            grdStudentsList.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //grdStudentsList.Columns[0].Visible = false;
            //add datasource to the grid view
            grdStudentsList.DataSource = Session["MainGrid"];
            grdStudentsList.DataBind();
            //grdView.DataSource = Session["StudentsList"];
            //grdView.DataBind();
        }
        #endregion

        protected void grdStudentsList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable AssessmentType = (DataTable)Session["AssessmentTypes"];
                DataTable Subitems = (DataTable)Session["Subitems"];
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell;

                HeaderCell = new TableCell();
                HeaderCell.Text = "No";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.VerticalAlign = VerticalAlign.Middle;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#3F5330");
                HeaderCell.BorderColor = Color.Black;
                HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.ForeColor = Color.White;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Wrap = false;
                HeaderCell.Width = Unit.Pixel(50);
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Name";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.VerticalAlign = VerticalAlign.NotSet;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#3F5330");
                HeaderCell.BorderColor = Color.Black;
                HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.ForeColor = Color.White;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Wrap = false;
                HeaderGridRow.Cells.Add(HeaderCell);

                for (int i = 0; i < AssessmentType.Rows.Count; i++)
                {
                    int count = 0;
                    for (int j = 0; j < Subitems.Rows.Count; j++) 
                    { 
                        if (AssessmentType.Rows[i][0].ToString() == Subitems.Rows[j][2].ToString()) 
                            count++; 
                    }

                    if (count != 0)
                    {
                        HeaderCell = new TableCell();
                        HeaderCell.Text = AssessmentType.Rows[i][1].ToString();
                        HeaderCell.ColumnSpan = count;
                        HeaderCell.VerticalAlign = VerticalAlign.Middle;
                        HeaderCell.BackColor = ColorTranslator.FromHtml("#3F5330");
                        HeaderCell.BorderColor = Color.Black;
                        HeaderCell.BorderStyle = BorderStyle.Solid;
                        HeaderCell.ForeColor = Color.White;
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        HeaderCell.Wrap = false;
                        HeaderGridRow.Cells.Add(HeaderCell);
                    }
                }
                grdStudentsList.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }

        protected void grdView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region "Exporting Files"
        public void ExportToExcel()
        {
            string attachment = "attachment; filename=MyClassScores"+ cboSubjects.SelectedItem.Text +".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grdStudentsList.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void grdView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable AssessmentType = (DataTable)Session["AssessmentTypes"];
                DataTable Subitems = (DataTable)Session["Subitems"];
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell;



                HeaderCell = new TableCell();
                HeaderCell.Text = "No";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.VerticalAlign = VerticalAlign.Middle;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#3F5330");
                HeaderCell.BorderColor = Color.Black;
                HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.ForeColor = Color.White;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Wrap = false;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Name";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.VerticalAlign = VerticalAlign.Middle;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#3F5330");
                HeaderCell.BorderColor = Color.Black;
                HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.ForeColor = Color.White;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Wrap = false;
                HeaderGridRow.Cells.Add(HeaderCell);

                for (int i = 0; i < AssessmentType.Rows.Count; i++)
                {
                    int count = 0;
                    for (int j = 0; j < Subitems.Rows.Count; j++) { if (AssessmentType.Rows[i][0].ToString() == Subitems.Rows[j][2].ToString()) count++; }

                    HeaderCell = new TableCell();
                    HeaderCell.Text = AssessmentType.Rows[i][1].ToString();
                    HeaderCell.ColumnSpan = count;
                    HeaderCell.VerticalAlign = VerticalAlign.Middle;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#3F5330");
                    HeaderCell.BorderColor = Color.Black;
                    HeaderCell.BorderStyle = BorderStyle.Solid;
                    HeaderCell.ForeColor = Color.White;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderCell.Wrap = false;
                    HeaderGridRow.Cells.Add(HeaderCell);
                }
                grdView.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }
        #endregion

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            grdView.Visible = true;
            ExportToExcel();
            grdView.Visible = false;
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < grdStudentsList.Columns.Count; i++)
            {
                try
                {
                    if (i == 0) grdView.Columns[0].HeaderStyle.Width = Unit.Pixel(20);
                    grdView.Columns[i].HeaderStyle.BackColor = Color.White;
                    grdView.Columns[i].HeaderStyle.BorderColor = Color.Black;
                    grdView.Columns[i].HeaderStyle.BorderStyle = BorderStyle.Solid;
                    grdView.Columns[i].HeaderStyle.ForeColor = Color.Blue;
                    grdView.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    grdView.Columns[i].HeaderStyle.VerticalAlign = VerticalAlign.Middle;
                    grdView.Columns[i].HeaderStyle.Wrap = false;

                    grdView.Columns[0].ItemStyle.Width = Unit.Pixel(20);
                    grdView.Columns[i].ItemStyle.BackColor = Color.White;
                    grdView.Columns[i].ItemStyle.BorderColor = Color.Black;
                    grdView.Columns[i].ItemStyle.BorderStyle = BorderStyle.Solid;
                    grdView.Columns[i].ItemStyle.ForeColor = Color.Black;
                    grdView.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    grdView.Columns[i].ItemStyle.VerticalAlign = VerticalAlign.Middle;
                    grdView.Columns[i].ItemStyle.Wrap = false;
                }
                catch
                { }
            }
        }

        protected void cboTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAllSubjects();
        }

        protected void cboSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void cboQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
