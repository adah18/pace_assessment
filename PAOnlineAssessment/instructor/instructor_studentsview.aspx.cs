using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Drawing;
using PAOnlineAssessment.Classes;
using System.Diagnostics;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace PAOnlineAssessment.instructor
{
    public partial class instructor_studentsview : System.Web.UI.Page
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
        //Declare list of requirements header
        List<Constructors.RequirementHeader> RequirementHeaderList;
        //Declare list of requirement
        List<Constructors.Requirement> RequirementList;
        //Declare list of requirement list
        List<Constructors.RequirementSubitem> SubitemList;
        //Declare List of student grades
        List<Constructors.StudentGrades> StudentGradeList;
        //Declare List of Student Registration
        List<Constructors.StudentRegistrationView> RegistrationList;

        //Declare list of Requirement
        //Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                //check the user authentication
                if (LUser.UserGroupID == "1" || LUser.UserGroupID == "3")
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
            //Verify if page load is post back
            if (!IsPostBack)
            {
                //check if the selected subjects has assessment
                if (isHaveAssessment())
                {
                    lnkSave.Attributes.Add("onclick", "return confirm('Do you really want to save the grades to Pace Grading System?')");
                    
                    //load the quarter
                    cboQuarter.SelectedValue = Session["Quarter"].ToString();

                    //Load All Students based on subject selected
                    LoadAllStudent();
                    //Get all assessment type of the selected subject
                    GetAssessmentTypes();
                    //get all subitems 
                    GetAllSubitems();
                    //get all the scores of the students
                    GetScores();
                    //Generate all data gathered
                    GenerateGridViewData();
                    //load the merged data to main grid
                    LoadToDynamicGridView();
                }
                else
                {
                    Response.Write("<script>alert('No assessment found.');window.location='" + ResolveUrl(DefaultForms.frm_instructor_subjects) + "'</script>");
                }
            }
        }

        //Load info for the site map
        public void LoadSiteMapDetails()
        {
            SiteMap1.RootNode = "Dashboard";
            SiteMap1.RootNodeToolTip = "Dashboard";
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.ParentNode = "My Subjects";
            SiteMap1.ParentNodeToolTip = "Click to go back to My Subjects";
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_instructor_subjects);

            SiteMap1.CurrentNode = "Teachers View";
        }

        //verify if the selected subject has assessment
        bool isHaveAssessment()
        {
            bool isHave = false;
            //instantiate new list
            AssessmentList = new List<Constructors.Assessment>(cls.getAssessment());
            AssessmentList.ForEach(al =>
            {
                //check if has assessment
                if (al.SubjectID == Convert.ToInt32(Request.QueryString["subid"]))
                {
                    isHave = true;
                    return;
                }
            });

            return isHave;
        }

        protected void cboSearchQuery_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboAssessment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cboQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //function for getting registration id
        string GetRegistrationtID(string oName)
        {
            string x = "", z = "";
            // load all the student from the selected subject and section that is registered in pace assessment
            DataTable dt_student = (DataTable)Session["StudentLists"];
            for (int i = 0; i < dt_student.Rows.Count; i++)
            {
                Debug.WriteLine("Student Name = " + dt_student.Rows[i][1].ToString() + " == " + oName);
                //check the studensts name
                if ((dt_student.Rows[i][1].ToString() == oName))
                {
                    //get the student number
                    z = dt_student.Rows[i][0].ToString();
                    break;
                }
            }

            x = cls.ExecuteScalar("Select RegistrationID From PaceRegistration.dbo.StudentRegistrationView Where StudentNumber=" + z + " and SchoolYear='" + Session["CurrentSchoolYear"].ToString() + "'");
            //check if has registration id found
            if(string.IsNullOrEmpty(x))
                x = "0";

            return x;
        }



        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            lblErr.Text = "";
            lblErr.Visible = false;
            for (int i = 1; i < grdStudentsList.Columns.Count; i++)
            {
                grdStudentsList.Columns[i].Visible = false;
            }
            //get all subitems 
            GetAllSubitems();
            //get all the scores of the students
            GetScores();
            //Generate all data gathered
            GenerateGridViewData();
            //load the merged data to main grid
            LoadToDynamicGridView();

        }

        #region "Loading Scores"
        //load all students based on the selected subject
        void LoadAllStudent()
        {
            // retreive the subject id and section id from querystring
            int SubjectID = Convert.ToInt32(Request.QueryString["subid"]);
            int SectionID = Convert.ToInt32(Request.QueryString["secid"]);
            //create datatable that will temporarily hold all the students 
            DataTable dt = new DataTable();
            //create column
            dt.Columns.Add("StudentNumber");
            dt.Columns.Add("StudentName");
            dt.Columns.Add("StudentID");
            //Declare new studentlist
            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            //Declare new RegistrationTerm
            RegistrationTerm = new List<Constructors.RegistrationTerm>(cls.getRegistrationTerm());
            //Declare New StudentAccount List
            StudentAccountList = new List<Constructors.StudentAccount>(cls.getStudentAccounts());

            StudentList.ForEach(sl =>
            {
                //verify if the section id match the section id of the student
                if (sl.SectionID == SectionID && sl.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    StudentAccountList.ForEach(sa =>
                    {
                        //verify if the studentnumber in studentlist match the studentnumber in studentaccountlist
                        if (sl.StudentNumber == sa.StudentNumber)
                        {
                            //if all requirement match add the student to the datatable
                            dt.Rows.Add(
                                sa.StudentNumber,
                                sa.Lastname + ", " + sa.Firstname,
                               sl.StudentID
                                );
                        }
                    });
                }
            });

            //pass the value of a datatable to a session in case of postback the value will not lost.
            Session["StudentLists"] = dt;
            //grdView.DataSource = Session["StudentLists"];
            //grdView.DataBind();

        }

        //get all the assessment type for the selected subject
        void GetAssessmentTypes()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(Request.QueryString["subid"]);
            int SectionID = Convert.ToInt32(Request.QueryString["secid"]);
            DataTable dt = new DataTable();
            dt.Columns.Add("AssessmentTypeID");
            dt.Columns.Add("Title");

            //get all assessment type id
            AssessmentType = new List<Constructors.Assessment>(cls.getAssessmentTypeID(SubjectID, Session["CurrentSchoolYear"].ToString()));
            //loop through the list of assessment types
            AssessmentType.ForEach(at =>
            {
                dt.Rows.Add(at.AssessmentTypeID.ToString());
            });

            //get the title for the assessment type
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //collect the title of the assessment types
                string title = cls.ExecuteScalar("Select Description from PaceAssessment.dbo.AssessmentType Where AssessmentTypeID=" + Convert.ToInt32(dt.Rows[i][0].ToString()) + " ");
                if (title != "")
                {
                    //if title is not empty then add the title to the data table
                    dt.Rows[i][1] = title;
                }
            }
            //pass the value of the data table to session
            Session["AssessmentTypes"] = dt;

            //clear the combo box
            cboAssessment.Items.Clear();
            cboAssessment.Items.Add(new ListItem("--- Select Type ---", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //add the assessments
                cboAssessment.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
            }
        }

        //Get all the sub items for each type
        void GetAllSubitems()
        {
            //get the subject and section id
            int SubjectID = Convert.ToInt32(Request.QueryString["subid"]);
            int SectionID = Convert.ToInt32(Request.QueryString["secid"]);
            //get the value for the session
            DataTable AssessmentTypes = (DataTable)Session["AssessmentTypes"];
            //create data table
            DataTable dt = new DataTable();
            //create column
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Title");
            dt.Columns.Add("AssessmentTypeID");

            //CHECK IF THE QUARTER IS SELECTED OR NOT
            if (cboQuarter.SelectedValue != "")
            {
                for (int i = 0; i < 1; i++)
                {
                    AssessmentType = new List<Constructors.Assessment>(cls.getAssessment());
                    AssessmentType.ForEach(at =>
                    {
                        //CHECK IF THE SELECTED QUARTER IS EQUAL TO THE QUARTERS
                        if (at.Quarter == cboQuarter.SelectedValue)
                        {
                            if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(cboAssessment.SelectedValue) == at.AssessmentTypeID)
                            {
                                dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                            }
                        }
                    });
                }
            }
            else
            {
                //search for subitem that will match the selected subject and assessment typeid
                for (int i = 0; i < 1; i++)
                {
                    AssessmentType = new List<Constructors.Assessment>(cls.getAssessment());
                    AssessmentType.ForEach(at =>
                    {
                        if (at.SchoolYear == Session["CurrentSchoolYear"].ToString() && SubjectID == at.SubjectID && Convert.ToInt32(cboAssessment.SelectedValue) == at.AssessmentTypeID)
                        {
                            dt.Rows.Add(at.AssessmentID, at.Title, at.AssessmentTypeID);
                        }
                    });
                }
            }
            //pass the data to a session
            Session["Subitems"] = dt;
        }

        //Generate all data gathered
        void GenerateGridViewData()
        {            
            //declare data table for scores
            DataTable Scores = (DataTable)Session["Scores"];
            //declare data table for student list
            DataTable StudentLists = (DataTable)Session["StudentLists"];
            //declare data table for subitems
            DataTable Subitems = (DataTable)Session["Subitems"];
            //declare a data table
            DataTable dt = new DataTable();
           
            //create columns
            dt.Columns.Add("Count");
            dt.Columns.Add("StudentName");

            DataTable dtTotalPoints = new DataTable();
            dtTotalPoints.Columns.Add("TotalPoints");

            List<Constructors.AssessmentDetails> DetailList = new List<Constructors.AssessmentDetails>(cls.getAssessmentDetails());
            
            //add columns based on all sub items
            for (int i = 0; i < Subitems.Rows.Count; i++)
            {
                dt.Columns.Add(Subitems.Rows[i][1].ToString());
            }

            //arranged the names
            StudentLists.DefaultView.Sort = "StudentName";
            DataView dv = StudentLists.DefaultView;

            //add the data in the grid view
            int count = 0;
            //loop through the list
            foreach (DataRowView dvRow in dv)
            {
                count++;
                dt.Rows.Add(count, dvRow[1].ToString());
                for (int j = 0; j < Subitems.Rows.Count; j++)
                {
                    dt.Rows[dt.Rows.Count - 1][j + 2] = GetAssessmentScore(Convert.ToInt32(dvRow[2]), Convert.ToInt32(Subitems.Rows[j][0]));
                }
            }

            ////Add Data to the data table
            //for (int i = 0; i < StudentLists.Rows.Count; i++)
            //{
            //    //add count number and student name
            //    dt.Rows.Add(i + 1, StudentLists.Rows[i][1].ToString());
            //    for (int j = 0; j < Subitems.Rows.Count; j++)
            //    {
            //        dt.Rows[dt.Rows.Count - 1][j + 2] = GetAssessmentScore(Convert.ToInt32(StudentLists.Rows[i][2]), Convert.ToInt32(Subitems.Rows[j][0]));
            //    }
               
            //}

            //getting total points
            for (int j = 0; j < Subitems.Rows.Count; j++)
            {
                int totalscore = 0;
                //loop through the list
                DetailList.ForEach(dl =>
                {
                    //count the scores
                    if (dl.AssessmentID == Convert.ToInt32(Subitems.Rows[j][0]))
                    {
                        totalscore += dl.Points;
                    }
                });
                Debug.WriteLine(totalscore.ToString());
                dtTotalPoints.Rows.Add(totalscore);
            }

            Session["TotalPoints"] = dtTotalPoints;
            Debug.WriteLine(dtTotalPoints.Rows.Count.ToString());
            //pass the data tables value to a session
            Session["MainGrid"] = dt;
            grdView.DataSource = Session["MainGrid"];
            grdView.DataBind();
        }

        string GetAssessmentScore(int StudentID, int AssessmentID)
        {
            string final_score = "";
            //instantiate new list
            AssessmentList = new List<Constructors.Assessment>(cls.getAssessment());
            //loop through the list
            AssessmentList.ForEach(al =>
            {
                //check the assessment
                if (al.AssessmentID == AssessmentID)
                {
                    //check if assessment was taken
                    if (isAssessmentTaken(StudentID, AssessmentID))
                    {
                        double score = 0;
                        //instantiate new list
                        StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
                        //loop through the list 
                        StudentAnswersList.ForEach(sa =>
                        {
                            //check the assessment id and student id
                            if (sa.StudentID == StudentID && sa.AssessmentID == AssessmentID)
                            {
                                //get the correct answer
                                string ans = cls.ExecuteScalar("Select CorrectAnswer From PaceAssessment.dbo.QuestionPoolView Where QuestionPoolID=" + sa.QuestionPoolID + "");
                                //check if the answer is correct
                                if (ans.ToLower() == sa.SelectedAnswer.ToLower())
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
                        //collect the dates
                        string DateS = Convert.ToDateTime(al.DateStart).ToShortDateString() + " " + al.TimeStart;
                        DateTime DateStart = Convert.ToDateTime(DateS);
                        string DateE = Convert.ToDateTime(al.DateEnd).ToShortDateString() + " " + al.TimeEnd;
                        DateTime DateEnd = Convert.ToDateTime(DateE);
                        if (DateStart >= DateTime.Now)
                        {
                            //if the assessment is not yet taken
                            final_score = "-";
                        }
                        else if (DateEnd < DateTime.Now)
                        {
                            //assessment is not taken
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
            //instantiate new list
            StudentAnswersList = new List<Constructors.StudentAnswers>(cls.getStudentAnswersList());
            //loop through the list
            StudentAnswersList.ForEach(sa =>
            {
                //check the student and assessment 
                if (sa.StudentID == StudentID && sa.AssessmentID == AssessmentID)
                {
                    x = true;
                    return;
                }
            });

            return x;
        }

        //get the scores of a students for the assessment
        void GetScores()
        {
            //collecting the scores
            DataTable StudentLists = (DataTable)Session["StudentLists"];
            DataTable Subitems = (DataTable)Session["Subitems"];
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("AssessmentID");
            dt.Columns.Add("Score");
            dt.Columns.Add("Title");
        }

        #endregion

        #region "Create Column Dynamically"
        //Create Gridview with dynamic templates
        void LoadToDynamicGridView()
        {
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
            grdStudentsList.Columns[1].ItemStyle.Width = Unit.Pixel(150);
            grdStudentsList.Columns[1].HeaderStyle.Width = Unit.Pixel(150);
            for (int i = 0; i < grdStudentsList.Columns.Count; i++)
            {
                grdStudentsList.Columns[1].HeaderStyle.Width = Unit.Pixel(20);

                grdStudentsList.Columns[i].HeaderStyle.BackColor = Color.White;
                grdStudentsList.Columns[i].HeaderStyle.BorderColor = Color.Black;
                grdStudentsList.Columns[i].HeaderStyle.BorderStyle = BorderStyle.Solid;
                grdStudentsList.Columns[i].HeaderStyle.ForeColor = Color.Blue;
                grdStudentsList.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                grdStudentsList.Columns[i].HeaderStyle.VerticalAlign = VerticalAlign.Middle;
                grdStudentsList.Columns[i].HeaderStyle.Wrap = false;

                grdStudentsList.Columns[1].ItemStyle.Width = Unit.Pixel(20);
                grdStudentsList.Columns[i].ItemStyle.BackColor = Color.White;
                grdStudentsList.Columns[i].ItemStyle.BorderColor = Color.Black;
                grdStudentsList.Columns[i].ItemStyle.BorderStyle = BorderStyle.Solid;
                grdStudentsList.Columns[i].ItemStyle.ForeColor = Color.Black;
                grdStudentsList.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                grdStudentsList.Columns[i].ItemStyle.VerticalAlign = VerticalAlign.Middle;
                grdStudentsList.Columns[i].ItemStyle.Wrap = false;
            }
            grdStudentsList.Columns[0].Visible = false;
            //add datasource to the grid view 
            grdStudentsList.DataSource = Session["MainGrid"];
            grdStudentsList.DataBind();
        }


        //for assessment type header in the grid view
        protected void grdStudentsList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable AssessmentType = (DataTable)Session["AssessmentTypes"];
                DataTable Subitems = (DataTable)Session["Subitems"];
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell;


                //generate a header for no. column
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

                //generate a header for students' name column
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

                //generate header for assessments
                if (Subitems.Rows.Count > 0)
                {
                    for (int i = 0; i < AssessmentType.Rows.Count; i++)
                    {
                        if (AssessmentType.Rows[i][0].ToString() == cboAssessment.SelectedValue)
                        {
                            int count = 0;
                            for (int j = 0; j < Subitems.Rows.Count; j++)
                            {
                                if (AssessmentType.Rows[i][0].ToString() == Subitems.Rows[j][2].ToString()) count++;
                            }
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
                }

                grdStudentsList.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }
        #endregion

        #region "Exporting Files"

        public void ExportToExcel()
        {
            DataTable dtTotal = (DataTable)Session["TotalPoints"];
            Debug.WriteLine("row count " + dtTotal.Rows.Count.ToString());
            string NewFileName = Session.SessionID;
            //copy the grading template
            File.Copy(Server.MapPath("~/ExcelTemplate/GradingTemplate.xlsx"), Server.MapPath("~/TempFolder/" + NewFileName + ".xlsx"), true);

            DataTable dt = (DataTable)Session["MainGrid"];

            //create a new excel package using the copied excel
            ExcelPackage pck = new ExcelPackage(new FileInfo(Server.MapPath("~/TempFolder/" + NewFileName + ".xlsx")));

            //select a sheet ine the excel to edit
            var ws = pck.Workbook.Worksheets["GradingSheet"];
            
            //create a cell for Excel Title
            ws.Cells[1, 1].Value = "GRADING SHEET"; // Heading Name
            ws.Cells[1, 1].Style.Font.Bold = true;
            ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true; //Merge columns start and end range
            ws.Cells[1, 1, 1, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //add description of the subject
            ws.Cells[2, 1].Value = cls.ExecuteScalar("Select Description From PaceRegistration.dbo.Subject Where SubjectID = " + Request.QueryString["subid"]); // Heading Name
            ws.Cells[2, 1, 2, dt.Columns.Count].Merge = true; //Merge columns start and end range
            ws.Cells[2, 1, 2, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[2, 1].Style.Font.Bold = true;

            //add the description of the section and level
            ws.Cells[3, 1].Value = cls.ExecuteScalar("SELECT Distinct([Level-Section]) FROM [PaceRegistration].[dbo].[GradingView] Where SectionID = " + Request.QueryString["secid"] + "AND SchoolYear='" + Session["CurrentSchoolYear"].ToString() + "'"); // Heading Name
            ws.Cells[3, 1, 3, dt.Columns.Count].Merge = true; //Merge columns start and end range
            ws.Cells[3, 1, 3, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[3, 1].Style.Font.Bold = true;
            //add the quarter used in the system 
            ws.Cells[4, 1].Value = cboQuarter.SelectedItem.Text + " Quarter";
            ws.Cells[4, 1, 4, dt.Columns.Count].Merge = true; //Merge columns start and end range
            ws.Cells[4, 1, 4, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[4, 1].Style.Font.Bold = true;

            //add caption for every column
            ws.Cells[5, 1].Value = " ";
            ws.Cells[5, 1].Style.Font.Bold = true;

            ws.Cells[5, 2].Value = "Student Name";
            ws.Cells[5, 2].Style.Font.Bold = true;

            ws.Cells[5, 3].Value = cboAssessment.SelectedItem.Text;
            ws.Cells[5, 3].Style.Font.Bold = true;
            ws.Cells[5, 3, 5, dt.Columns.Count].Merge = true;
            ws.Cells[5, 3, 5, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //edit cells for assessment names
            for (int j = 0; j < dt.Columns.Count - 2; j++)
            {
                ws.Cells[6, j + 3].Value = dt.Columns[j + 2].ColumnName.ToString();
                Color blue = Color.Blue;
                ws.Cells[6, j + 3].Style.Font.Color.SetColor(blue);
            }
            ws.Cells[7, 2].Value = "Total Score:";
            ws.Cells[7, 2].Style.Font.Bold = true;

            //add the total points of every assessment in the 7th row
            for (int i = 0; i < dtTotal.Rows.Count; i++)
            {
                ws.Cells[7, i + 3].Value = dtTotal.Rows[i][0].ToString();
                ws.Cells[7, i + 3].Style.Font.Bold = true;
            }

            //add all the grades to the excel 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    int RowNum = i + 1;
                    ws.Cells[RowNum + 7, j + 1].Value = dt.Rows[i][j];
                    if (j != 1)
                    {
                        ws.Cells[RowNum + 7, j + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                }
            }  
          
            Response.Clear();
            pck.SaveAs(Response.OutputStream);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=GradingSheet_" + DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+ DateTime.Now.Day.ToString() + ".xlsx");
            Response.End();

            File.Delete(Server.MapPath("~/TempFolder/" + NewFileName + ".xlsx"));
            Debug.WriteLine(Server.MapPath("~/TempFolder/" + NewFileName + ".xlsx").ToString());
            
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
                
                //generate a header for no. column
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

                //generate header for student name
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

                //generate header for subitems
                if (Subitems.Rows.Count > 0)
                {
                    for (int i = 0; i < AssessmentType.Rows.Count; i++)
                    {
                        if (AssessmentType.Rows[i][0].ToString() == cboAssessment.SelectedValue)
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
                    }
                }
                
                grdView.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            lblErr.Visible = false;
            lblErr.Text = "";
            DataTable SubItems = (DataTable)Session["Subitems"];
            Debug.WriteLine("Row Count: " + SubItems.Rows.Count);
            //if has subitems
            if (SubItems.Rows.Count > 0)
            {
                grdView.Visible = true;
                // call the excel function
                ExportToExcel();
                grdView.Visible = false;
                lblErr.Visible = false;
                lblErr.Text = "";
            }
            else
            {
                lblErr.Visible = true;
                lblErr.Text = "Action cannot continue. No assessment to be exported. ";

                for (int i = 1; i < grdStudentsList.Columns.Count; i++)
                {
                    grdStudentsList.Columns[i].Visible = false;
                }

                //Load All Students based on subject selected
                LoadAllStudent();
                //Get all assessment type of the selected subject
                GetAssessmentTypes();
                //get all subitems 
                GetAllSubitems();
                //get all the scores of the students
                GetScores();
                //Generate all data gathered
                GenerateGridViewData();
                //load the merged data to main grid
                LoadToDynamicGridView();
            }
        }

        #endregion

        #region "Saving Directly from Pace Assessment to PaceRegistration"

        ////////////////////////////////////////////////////////////////////////////////
        ///////Codes for Saving Directly from Pace Assessment to PaceRegistration///////
        ////////////////////////////////////////////////////////////////////////////////

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["Subitems"];
            //check if has subitems
            if (dt.Rows.Count > 0)
            {
                //click the search button when saving grades
                imgSearchQuery_Click(null, null);
                //load all assessment type for the selected subject and section
                DataTable dt_assessment = (DataTable)Session["AssessmentTypes"];
                //load all the data from the main grid
                DataTable dt_maingrid = (DataTable)Session["MainGrid"];
                //load all the total points of every assessment
                DataTable dt_total = (DataTable)Session["TotalPoints"];

                //check ig the requirement header exists
                if (RequirementHeaderExist(cboAssessment.SelectedItem.Text))
                {
                    //check if the requirement exists
                    if (RequirementExists(Convert.ToInt32(Session["RequirementHeaderID"].ToString()), Convert.ToInt32(GradingID()), Convert.ToInt32(cboQuarter.SelectedItem.Text.Substring(0, 1))))
                    {
                        for (int i = 0; i < dt_total.Rows.Count; i++)
                        {
                            //check if assessment exist in the pace registration
                            if (AssessmentAlreadyExists(dt_maingrid.Columns[i + 2].Caption) == false)
                            {
                                //insert new assessment as requirement subitem
                                string oSql = "Insert into PaceRegistration.dbo.RequirementSubitems(RequirementID,TotalPoints,DateCreated,UserCreated)Values(" + Session["RequirementID"].ToString() + ", '" + dt_total.Rows[i][0].ToString() + "',getdate(), '" + LUser.Username + "')";
                                if (cls.ExecuteNonQuery(oSql) == 1)
                                {
                                    string SubItemID = cls.ExecuteScalar("SELECT MAX(RequirementSubitemID) FROM PaceRegistration.dbo.RequirementSubitems WHERE RequirementID='" + Session["RequirementID"].ToString() + "' and  TotalPoints='" + dt_total.Rows[i][0].ToString() + "'");
                                    //insert the assessment in the pace registration table
                                    InsertNewAssessment(dt_maingrid.Columns[i + 2].Caption, SubItemID);

                                    Debug.WriteLine("Total Count: " + dt_maingrid.Rows.Count.ToString());
                                    for (int k = 0; k < dt_maingrid.Rows.Count; k++)
                                    {
                                        //insert new score for the new assessment
                                        oSql = "Insert Into PaceRegistration.dbo.StudentGrades(RequirementSubitemID, RegistrationID, Score, LastUpdateUser, LastUpdateDate)Values(" + SubItemID + ", " + GetRegistrationtID(dt_maingrid.Rows[k][1].ToString()) + ",'" + dt_maingrid.Rows[k][i + 2].ToString() + "','" + LUser.Username + "',getdate())";
                                        if (cls.ExecuteNonQuery(oSql) == 1)
                                        {
                                            Debug.WriteLine("Counter: " + k.ToString());
                                            //Validator.AlertBack("Grade has been saved to Grading System successfully.", "instructor_studentsview.aspx?subid=" + Request.QueryString["subid"] + "&secid=" + Request.QueryString["secid"]);
                                        }
                                    }

                                    //update card grade
                                    GetRequirements(Convert.ToInt32(GradingID()), Convert.ToInt32(cboQuarter.SelectedItem.Text.Substring(0, 1)));
                                }
                            }
                            else
                            {
                                //update the existing assessment
                                Debug.WriteLine("Total Count: " + dt_maingrid.Rows.Count.ToString());
                                for (int k = 0; k < dt_maingrid.Rows.Count; k++)
                                {
                                    string oSql = "";
                                    //check if the student's grade was entered in the assessment
                                    string Grade = cls.ExecuteScalar("SELECT * FROM PaceRegistration.dbo.StudentGrades WHERE RegistrationID='" + GetRegistrationtID(dt_maingrid.Rows[k][1].ToString()) + "' and RequirementSubitemID='" + Session["req_subitem_id"].ToString() + "'");
                                    if (string.IsNullOrEmpty(Grade))
                                    {
                                        //insert new score for the new assessment
                                        oSql = "Insert Into PaceRegistration.dbo.StudentGrades(RequirementSubitemID, RegistrationID, Score, LastUpdateUser, LastUpdateDate)Values(" + Session["req_subitem_id"].ToString() + ", " + GetRegistrationtID(dt_maingrid.Rows[k][1].ToString()) + ",'" + dt_maingrid.Rows[k][i + 2].ToString() + "','" + LUser.Username + "',getdate())";
                                    }
                                    else
                                    {
                                        //updating the score
                                        oSql = "Update PaceRegistration.dbo.StudentGrades Set Score=" + dt_maingrid.Rows[k][i + 2].ToString() + ", LastUpdateDate=getdate(), LastUpdateUser='" + LUser.Username + "' Where RequirementSubitemID=" + Session["req_subitem_id"].ToString() + " and RegistrationID=" + GetRegistrationtID(dt_maingrid.Rows[k][1].ToString()) + "";

                                    }
                                    if (cls.ExecuteNonQuery(oSql) == 1)
                                    {
                                        Debug.WriteLine("Counter: " + k.ToString());
                                    }
                                }
                                //update the card grade
                                GetRequirements(Convert.ToInt32(GradingID()), Convert.ToInt32(cboQuarter.SelectedItem.Text.Substring(0, 1)));
                            }
                        }
                        
                    }
                    else
                    {
                        Validator.AlertBack("Requirement doesnt exists.Must create a requirement in grading system first.", "instructor_studentsview.aspx?subid=" + Request.QueryString["subid"] + "&secid=" + Request.QueryString["secid"] + "");
                    }
                }
                else
                {
                    Validator.AlertBack("Requirement header doesnt exists.Must create a requirement header in grading system first.", "instructor_studentsview.aspx?subid=" + Request.QueryString["subid"] + "&secid=" + Request.QueryString["secid"] + "");
                }

                Validator.AlertBack("Grade has been saved to Grading System successfully.", "instructor_studentsview.aspx?subid=" + Request.QueryString["subid"] + "&secid=" + Request.QueryString["secid"]);
                lblErr.Text = "";
                lblErr.Visible = false;
            }
            else
            {
                //no item to upload
                lblErr.Visible = true;
                lblErr.Text = "Action cannot continue. No assessment to be saved.";

                for (int i = 1; i < grdStudentsList.Columns.Count; i++)
                {
                    grdStudentsList.Columns[i].Visible = false;
                }

                //load all students
                LoadAllStudent();

                //get all assessment type of the selected subject
                GetAssessmentTypes();

                //get all subitems
                GetAllSubitems();

                //get all scores
                GetScores();

                //generate the gridview
                GenerateGridViewData();
                
                //load the grid view
                LoadToDynamicGridView();
            }
        }

        void InsertNewAssessment(string title, string subitemid)
        {
            //insert new assessments
            string sql = "Insert into PaceRegistration.dbo.Assessment(RequirementID, RequirementSubitemID, Title, Quarter)Values(" + Session["RequirementID"] + "," + subitemid + ",'" + title + "', " + cboQuarter.SelectedItem.Text.Substring(0, 1) + ")";
            cls.ExecuteNonQuery(sql);
        }

        bool AssessmentAlreadyExists(string title)
        {
            bool value = false;   
                                
            //get the requirement sub item                                               
            string RequirementSubitemID = cls.ExecuteScalar("Select RequirementSubitemID From PaceRegistration.dbo.Assessment Where Quarter=" + cboQuarter.SelectedItem.Text.Substring(0, 1) + " And RequirementID=" + Session["RequirementID"] + " And Title='" + title + "'");

            //pass the value
            Session["req_subitem_id"] = RequirementSubitemID;

            //check if has value
            if (string.IsNullOrEmpty(RequirementSubitemID) == false)
            {
                value = true;
            }

            return value;
        }

        //function for Getting the grading id of the section
        string GradingID()
        {
            string value = "0";
            value = cls.ExecuteScalar("Select GradingID From PaceRegistration.dbo.GradingView Where SectionID=" + Request.QueryString["secid"] + " And SubjectID=" + Request.QueryString["subid"] + " and SchoolYear='" + Session["CurrentSchoolYear"].ToString() + "'");
            //check if has grading id
            if (string.IsNullOrEmpty(value))
                value = "0";

            return value;
        }

        //function for checking if the requirement header exists
        bool RequirementHeaderExist(string requirement)
        {
            bool value = false;
            //instantiate new list
            RequirementHeaderList = new List<Constructors.RequirementHeader>(cls.GetRequirementHeader());
            //loop through the list
            RequirementHeaderList.ForEach(rl =>
            {
                //check if requirement description has found
                if (rl.RequirementDescription.ToLower() == requirement.ToLower())
                {
                    value = true;
                    Session["RequirementHeaderID"] = rl.RequirementHeaderID;
                    return;
                }
            });

            return value;
        }
        //function for checking if the requirement exists
        bool RequirementExists(int req_header_id, int grading_id, int qtr)
        {
            bool value = false;
            //instantiate new list
            RequirementList = new List<Constructors.Requirement>(cls.GetRequirement());
            //loop through the list
            RequirementList.ForEach(rl =>
            {
                //check the requirement
                if (rl.Quarter == qtr && rl.RequirementHeaderID == req_header_id && rl.GradingID == grading_id)
                {
                    value = true;
                    Session["RequirementID"] = rl.RequirementID;
                    return;
                }
            });

            return value;
        }
        #endregion
 
        #region "Updating CardGrade"

        ///////////////////////////////////////////
        ///////Codes for Updating Card Grade///////
        ///////////////////////////////////////////

        //function for updating card grade in the database
        void UpdateGrade(int Grade, int RegistrationID)
        {
            string isExist = cls.ExecuteScalar("SELECT * FROM PaceRegistration.dbo.CardGrade WHERE GradingID='" + GradingID() + "' and RegistrationID='" + RegistrationID + "'");
            string sql = "";
            //check if student already have a card grade
            if (!string.IsNullOrEmpty(isExist))
                //for the student with card grade
                sql = "Update PaceRegistration.dbo.CardGrade Set Quarter" + cboQuarter.SelectedItem.Text.Substring(0, 1) + "=" + Grade + "  Where GradingID=" + GradingID() + " And RegistrationID=" + RegistrationID;
            else
                //for student who dont have card grade
                sql = "INSERT INTO [CardGrade] ([GradingID],[RegistrationID],[Quarter" + cboQuarter.SelectedItem.Text.Substring(0, 1) + "]) VALUES ('" + GradingID() + "','" + RegistrationID + "','" + Grade + "')";
            //execute query
            cls.ExecuteNonQuery(sql);
        }

        //function for getting requirement for the selected section and subject
        void GetRequirements(int grading_id, int qtr)
        {
            Debug.WriteLine("Grading id = " + grading_id.ToString());
            DataTable dt = new DataTable();
            dt.Columns.Add("RequirementID");
            dt.Columns.Add("RequirementHeaderID");
            dt.Columns.Add("GradingID");
            dt.Columns.Add("DropLowest");
            dt.Columns.Add("Quarter");
            dt.Columns.Add("Percent");

            //instantiate new list
            RequirementList = new List<Constructors.Requirement>(cls.GetRequirement());
            //loop through the list
            RequirementList.ForEach(rl =>
            {
                //check if the grading id and quarter is equal with their value in the database
                if (rl.GradingID == grading_id && rl.Quarter == qtr)
                {
                    dt.Rows.Add(rl.RequirementID, rl.RequirementHeaderID, rl.GradingID, rl.DropLowest, rl.Quarter, rl.Percent);
                }
            });

            Session["Requirements"] = dt;
            //call get subitems function
            GetSubitems();
            //call get student grades functions
            GetStudentGrades();
        }

        //function for getting all sub items for all requirement
        void GetSubitems()
        {
            //collect the data tables
            DataTable dtrequirement = (DataTable)Session["Requirements"];
            DataTable dt = new DataTable();
            dt.Columns.Add("RequirementSubitemID");
            dt.Columns.Add("RequirementID");
            dt.Columns.Add("TotalPoints");

            double TotalPoints = 0;
            //instantiate new list
            SubitemList = new List<Constructors.RequirementSubitem>(cls.GetRequirementSubItems());
            //loop through the list
            SubitemList.ForEach(sl =>
            {
                //loop through the list of data table
                for (int i = 0; i < dtrequirement.Rows.Count; i++)
                {
                    //find all the subitems for the requirements retreived then add to the data table
                    if (sl.RequirementID.ToString() == dtrequirement.Rows[i][0].ToString())
                    {
                        dt.Rows.Add(sl.RequirementSubitemID, sl.RequirementID, sl.TotalPoints);
                        TotalPoints += sl.TotalPoints;
                    }
                }

            });
            Session["Subitems"] = dt;
            Session["GradeTotalPoints"] = TotalPoints;
        }


        //function for getting all the student grades from the database
        void GetStudentGrades()
        {
            //collect the data tables
            DataTable dtrequirement = (DataTable)Session["Requirements"];
            DataTable dt = new DataTable();
            dt.Columns.Add("RequirementSubitemID");
            dt.Columns.Add("RequirementID");
            dt.Columns.Add("TotalPoints");
            dt.Columns.Add("RegistrationID");
            dt.Columns.Add("Score");

            double TotalPoints = 0;

            //instantiate new list
            StudentGradeList = new List<Constructors.StudentGrades>(cls.GetStudentGrades());
            //loop through the list
            StudentGradeList.ForEach(sg => 
            {
                for (int i = 0; i < dtrequirement.Rows.Count; i++)
                {
                    //get all the student grades that is equal to the requirements reteived by the system then add to the data table
                    if (sg.RequirementID.ToString() == dtrequirement.Rows[i][0].ToString())
                    {
                        dt.Rows.Add(sg.RequirementSubitemID, sg.RequirementID, sg.TotalPoints,sg.RegistrationID, sg.Score);
                        TotalPoints += sg.TotalPoints;
                    }
                }
            });

            Session["StudentGrades"] = dt;

            // Call function student registration
            GetStudentRegistration();
        
        }
        //function for getting all the registration id and name of the student of the selected section
        void GetStudentRegistration()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("Name");

            //instantiate new list
            RegistrationList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            //loop through the list
            RegistrationList.ForEach(rl => 
            {
                //check if the section is equal to the selected section the add the student info to the datatable
                if (rl.SectionID.ToString() == Request.QueryString["secid"] && rl.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    dt.Rows.Add(rl.RegistrationID, rl.LastName +", "+rl.FirstName);
                }
            });
            Session["RegistrationID"] = dt;

            //call function for computing the grades
            GetComputeCardGrade();
        }
        //function for computing the card grades
        void GetComputeCardGrade()
        {
            //collect the data tables
            DataTable dt_requirements = (DataTable)Session["Requirements"];
            DataTable dt_registrationid = (DataTable)Session["RegistrationID"];
            DataTable dt_studentgrades = (DataTable)Session["StudentGrades"];

            //loop all the student in the selection section
            for(int k = 0; k< dt_registrationid.Rows.Count; k++)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Grade");
                dt.Columns.Add("Percent");

                double totalGrade = 0;
                double percent = 0;

                Debug.WriteLine("RegistrationID: " + dt_registrationid.Rows[k][0].ToString());
                //loop all the requirement
                for (int i = 0; i < dt_requirements.Rows.Count; i++)
                {
                    double totalScore = 0;
                    double grade = 0;
                    //loop all the student grades
                    for (int j = 0; j < dt_studentgrades.Rows.Count; j++)
                    {
                        //check if the requirement id from requirement table is equal to 
                        //the requirement id in student grades table
                        if (dt_requirements.Rows[i][0].ToString() == dt_studentgrades.Rows[j][1].ToString())
                        {
                            //check if the registration id is equal to each other
                            if (dt_studentgrades.Rows[j][3].ToString() == dt_registrationid.Rows[k][0].ToString())
                            {
                                //add total score of the student
                                totalScore += Convert.ToDouble(dt_studentgrades.Rows[j][4].ToString());
                            }
                        }
                    }
                   
                    Debug.WriteLine("Total Score: " + totalScore);
                    //get the percent of the requirement
                    percent = Convert.ToDouble(dt_requirements.Rows[i][5].ToString());
                   
                    //divide the totalscore to totalpoint of the assessment
                    if(GetTotalPoints(dt_requirements.Rows[i][0].ToString()) > 0)
                        grade = totalScore / GetTotalPoints(dt_requirements.Rows[i][0].ToString());

                    //multiply grade to 100
                    grade *= 100;  

                    //then multiply to the percent of the requirement
                    grade *= percent;

                    //add the grade to datatable
                    dt.Rows.Add(decimal.Round(Convert.ToDecimal(grade),2), percent);
                }
                
                //loop all the added grade for the student
                for (int z = 0; z < dt.Rows.Count; z++)
                {
                    //get the sum of grade from all of the requirements of the student
                    totalGrade += Convert.ToDouble(dt.Rows[z][0].ToString());
                }

                Debug.WriteLine("Total Grade: " + totalGrade.ToString());
                ////if the total grade is lower than 65 set the value of the grade to 65
                if (totalGrade < 65) totalGrade = 65;

                ////Round Off the total grade to remove the decimal 
                totalGrade = Math.Round(totalGrade, 0, MidpointRounding.AwayFromZero);
                
                //call the update grade function
                UpdateGrade(Convert.ToInt32(totalGrade.ToString()), Convert.ToInt32(dt_registrationid.Rows[k][0].ToString()));
            }
        }

        //function for getting the total points of a requirement
        int GetTotalPoints(string requirement_id)
        {
            int value = 0;
            DataTable dt_subitems = (DataTable)Session["Subitems"];
            //loop through the list
            for (int i = 0; i < dt_subitems.Rows.Count; i++)
            {
                //check the subitem id
                if (dt_subitems.Rows[i][1].ToString() == requirement_id)
                {
                    //get the value of the total points of the requirement
                    value += Convert.ToInt32(dt_subitems.Rows[i][2].ToString());
                    return value;
                }
            }

            return value;
        }
        #endregion

 
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
                             * its CommandName is set to "Edit"  like that of 'edit' button 
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
}
