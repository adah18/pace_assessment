﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.student
{
    public partial class available_assessments : System.Web.UI.Page
    {
        //List Of All Assessments
        List<Constructors.AssessmentView> AssessmentViewList;
        //Declare CurrentStudent Class Variable
        CurrentStudent CStudent;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Declare LoginUser Class Variable
        LoginUser LUser;
        //Instantiate New Collections Class
        Collections cls = new Collections();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In | 
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                CStudent = (CurrentStudent)Session["CurrentStudent"];
                if ((bool)Session["Authenticated"] == true && (string)Session["UserGroupID"] == "S")
                {

                }
                else
                {                    
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }
            }
            //redirect to default screen
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            //if page load is postback
            if (IsPostBack == false)
            {
                ConvertAssessmentListToDataTable();
                //Load Assessments available to the currently logged in student
                LoadAvailableAssessments();
            }
            //Load Sitemap Details for navigation    
            
            LoadSitemapDetails();
        }

        //Load Sitemap Details for navigation
        public void LoadSitemapDetails()
        {
            SiteMap.RootNode = "Dashboard";
            SiteMap.RootNodeToolTip = "Click to go back to Dashboard";
            SiteMap.RootNodeURL = ResolveUrl(DefaultForms.frm_default_dashboard);

            SiteMap.ParentNode = "Academic Activities";

            SiteMap.CurrentNode = "Take Assessment";
        }

        //Load Assessments available to the currently logged in student
        public void LoadAvailableAssessments()
        {
            grdAvailableAssessments.DataSource = Session["AssessmentViewList"];
            grdAvailableAssessments.DataBind();
        }

        //Convert List of Assessments to Data table
        public DataTable ConvertAssessmentListToDataTable()
        {
            AssessmentViewList = new List<Constructors.AssessmentView>(new Collections().getAssessmentView());

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("AssessmentID"));
            dt.Columns.Add(new DataColumn("AssessmentTypeID"));
            dt.Columns.Add(new DataColumn("AssessmentTypeDescription"));
            dt.Columns.Add(new DataColumn("UserID"));
            dt.Columns.Add(new DataColumn("TeacherFirstName"));
            dt.Columns.Add(new DataColumn("TeacherLastName"));
            dt.Columns.Add(new DataColumn("LevelID"));
            dt.Columns.Add(new DataColumn("LevelDescription"));
            dt.Columns.Add(new DataColumn("SubjectID"));
            dt.Columns.Add(new DataColumn("SubjectDescription"));
            dt.Columns.Add(new DataColumn("Title"));
            dt.Columns.Add(new DataColumn("Introduction"));
            dt.Columns.Add(new DataColumn("DateStart"));
            dt.Columns.Add(new DataColumn("DateEnd"));
            dt.Columns.Add(new DataColumn("TimeStart"));
            dt.Columns.Add(new DataColumn("TimeEnd"));
            dt.Columns.Add(new DataColumn("Status"));
            dt.Columns.Add(new DataColumn("UserCreated"));
            dt.Columns.Add(new DataColumn("DateCreated"));
            dt.Columns.Add(new DataColumn("LastUpdateUser"));
            dt.Columns.Add(new DataColumn("LastUpdateDate"));

            AssessmentViewList.ForEach(av => 
            {
                string LevelID = cls.ExecuteScalar("Select prs.LevelID from PaceRegistration.dbo.Student prs inner join PaceAssessment.dbo.Student pas on (pas.StudentNumber = prs.StudentNumber) where pas.StudentID='" + LUser.UserID + "'");
                if (av.LevelID == CStudent.LevelID)
                {
                    string TeacherName = av.TeacherFirstname + " " + av.TeacherLastname;
                    if (av.Status == "A" && av.SchoolYear == Session["CurrentSchoolYear"].ToString())
                    {
                        if (av.Title.ToLower().Contains(txtSearchQuery.Text.ToLower()) || av.SubjectDescription.ToLower().Contains(txtSearchQuery.Text.ToLower()) || av.Introduction.ToLower().Contains(txtSearchQuery.Text.ToLower()) || TeacherName.ToLower().Contains(txtSearchQuery.Text.ToLower()))
                        {
                            if (av.Schedule == "Yes")
                            {
                                if (Convert.ToDateTime(av.DateEnd).Date >= DateTime.Now.Date)
                                {
                                    if (cls.ExecuteScalar("select * from StudentAnswers where AssessmentID = '" + av.AssessmentID + "' and StudentID = '" + CStudent.StudentID + "'") == string.Empty)
                                    {
                                        dt.Rows.Add(av.AssessmentID, av.AssessmentTypeID, av.AssessmentTypeDescription, av.UserID, av.TeacherFirstname, av.TeacherLastname, av.LevelID, av.LevelDescription, av.SubjectID, av.SubjectDescription, av.Title, av.Introduction, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), av.Status, av.UserCreated, av.DateCreated, av.LastUpdateUser, av.LastUpdateDate);
                                        Debug.WriteLine(av.AssessmentID);
                                    }
                                }
                                else
                                {
                                    if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                    {
                                        string CheckAnswer = cls.ExecuteScalar("SELECT * FROM PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                        if (string.IsNullOrEmpty(CheckAnswer))
                                        {
                                            dt.Rows.Add(av.AssessmentID, av.AssessmentTypeID, av.AssessmentTypeDescription, av.UserID, av.TeacherFirstname, av.TeacherLastname, av.LevelID, av.LevelDescription, av.SubjectID, av.SubjectDescription, av.Title, av.Introduction, Convert.ToDateTime(av.DateStart).ToLongDateString(), Convert.ToDateTime(av.DateEnd).ToLongDateString(), Convert.ToDateTime(av.TimeStart).ToShortTimeString(), Convert.ToDateTime(av.TimeEnd).ToShortTimeString(), av.Status, av.UserCreated, av.DateCreated, av.LastUpdateUser, av.LastUpdateDate);
                                            Debug.WriteLine(av.AssessmentID);
                                        }
                                    }
                                }
                            }
                            else if (av.Schedule == "No")
                            {
                                if (av.ScheduleStatus == "Open")
                                {
                                    string CheckAnswer = cls.ExecuteScalar("SELECT * FROM PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                    if (string.IsNullOrEmpty(CheckAnswer))
                                    {
                                        dt.Rows.Add(av.AssessmentID, av.AssessmentTypeID, av.AssessmentTypeDescription, av.UserID, av.TeacherFirstname, av.TeacherLastname, av.LevelID, av.LevelDescription, av.SubjectID, av.SubjectDescription, av.Title, av.Introduction, "-", "-", "-", "-", av.Status, av.UserCreated, av.DateCreated, av.LastUpdateUser, av.LastUpdateDate);
                                        Debug.WriteLine(av.AssessmentID);
                                    }
                                }
                                else if (av.ScheduleStatus == "Close")
                                {
                                    if (av.MakeUp.Contains("-" + CStudent.StudentID + "-"))
                                    {
                                        string CheckAnswer = cls.ExecuteScalar("SELECT * FROM PaceAssessment.dbo.StudentAnswers where AssessmentID='" + av.AssessmentID + "' and StudentID='" + CStudent.StudentID + "'");
                                        if (string.IsNullOrEmpty(CheckAnswer))
                                        {
                                            dt.Rows.Add(av.AssessmentID, av.AssessmentTypeID, av.AssessmentTypeDescription, av.UserID, av.TeacherFirstname, av.TeacherLastname, av.LevelID, av.LevelDescription, av.SubjectID, av.SubjectDescription, av.Title, av.Introduction, "-", "-", "-", "-", av.Status, av.UserCreated, av.DateCreated, av.LastUpdateUser, av.LastUpdateDate);
                                            Debug.WriteLine(av.AssessmentID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            dt.DefaultView.Sort = "SubjectDescription";

            Session["AssessmentViewList"] = dt;
            return dt;
        }

        ////////////////////////////
        //------------------------//
        //--- Grid View Events ---//
        //------------------------//
        ////////////////////////////

        //First page link button
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            grdAvailableAssessments.PageIndex = 0;
            LoadAvailableAssessments();
        }

        //Previous page link button
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (grdAvailableAssessments.PageIndex != 0)
            {
                grdAvailableAssessments.PageIndex = grdAvailableAssessments.PageIndex-1;
                LoadAvailableAssessments();
            }
        }

        //Next poge link button
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (grdAvailableAssessments.PageIndex != grdAvailableAssessments.PageCount)
            {
                grdAvailableAssessments.PageIndex = grdAvailableAssessments.PageIndex + 1;
                LoadAvailableAssessments();
            }
        }

        //Last Page linkbutton
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            grdAvailableAssessments.PageIndex = grdAvailableAssessments.PageCount;
            LoadAvailableAssessments();
        }

        //Grid has been data bound
        protected void grdAvailableAssessments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < grdAvailableAssessments.Rows.Count; x++)
            {
                ImageButton imgTakeAssessment = (ImageButton)grdAvailableAssessments.Rows[x].FindControl("imgTakeAssessment");
                Label lblDateStart = (Label)grdAvailableAssessments.Rows[x].FindControl("lblDateStart");
                Label lblDateEnd = (Label)grdAvailableAssessments.Rows[x].FindControl("lblDateEnd");

                Label lblTimeStart = (Label)grdAvailableAssessments.Rows[x].FindControl("lblTimeStart");
                Label lblTimeEnd = (Label)grdAvailableAssessments.Rows[x].FindControl("lblTimeEnd");

                Label lblAssessmentID = (Label)grdAvailableAssessments.Rows[x].FindControl("lblAssessmentID");
                string AssessmentURL = ResolveUrl(DefaultForms.frm_take_assessment) + "?assid=" + lblAssessmentID.Text;

                string Schedule = cls.ExecuteScalar("SELECT Schedule FROM PaceAssessment.dbo.Assessment WHERE AssessmentID='" + lblAssessmentID.Text + "'");
                if (Schedule == "Yes")
                {
                    string start = Convert.ToDateTime(lblTimeStart.Text).ToString();
                    string end = Convert.ToDateTime(lblTimeEnd.Text).ToString();
                    if (isAssessmentAvailable(int.Parse(lblAssessmentID.Text.ToString()),CStudent.StudentID, Convert.ToDateTime(start), Convert.ToDateTime(end)))
                    {
                        Debug.WriteLine("Assessment URL:" + AssessmentURL);
                        imgTakeAssessment.PostBackUrl = AssessmentURL;
                        //imgTakeAssessment.OnClientClick = "window.location='" + AssessmentURL + "'";
                    }
                    else
                    {
                        imgTakeAssessment.OnClientClick = "alert('You can only take this assessment in the scheduled date and time.'); return false;";
                    }
                }
                else
                {
                    Debug.WriteLine("Assessment URL:" + AssessmentURL);
                    imgTakeAssessment.PostBackUrl = AssessmentURL;
                    //imgTakeAssessment.OnClientClick = "window.location='" + AssessmentURL + "'";
                }
            }
            
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                DropDownList cboAssessmentPerPage = (DropDownList)e.Row.FindControl("cboAssessmentPerPage");
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");

                lblPageCount.Text = "of " + grdAvailableAssessments.PageCount.ToString();

                DataTable dt = (DataTable)ConvertAssessmentListToDataTable();

                for (int x = 1; x <= grdAvailableAssessments.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = grdAvailableAssessments.PageIndex;

                for (int y = 1; y <= dt.Rows.Count; y++)
                {
                    cboAssessmentPerPage.Items.Add(y.ToString());
                }
                cboAssessmentPerPage.SelectedIndex = grdAvailableAssessments.PageSize - 1;
            }
        }
        
        //Page number dropdownlist 
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            grdAvailableAssessments.PageIndex = cboPageNumber.SelectedIndex;
            LoadAvailableAssessments();
        }

        //AssessmentPerPage dropdownlist
        protected void cboAssessmentPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboAssessmentPerPage = (DropDownList)sender;
            grdAvailableAssessments.PageSize = cboAssessmentPerPage.SelectedIndex + 1;
            LoadAvailableAssessments();
        }

        //executed before rendering the grid view
        protected void grdAvailableAssessments_PreRender(object sender, EventArgs e)
        {
            if (grdAvailableAssessments.BottomPagerRow != null)
            {
                grdAvailableAssessments.BottomPagerRow.Visible = true;
            }
            if (grdAvailableAssessments.TopPagerRow != null)
            {
                grdAvailableAssessments.TopPagerRow.Visible = true;
            }
        }

        //////////////////////////////////////
        //----------------------------------//
        //--- Searching/Record Filtering ---//
        //----------------------------------//
        //////////////////////////////////////

        //search button
        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            grdAvailableAssessments.PageIndex = 0;
            LoadAvailableAssessments();
        }


        /////////////////////
        //-----------------//
        //--- Functions ---//
        //-----------------//
        /////////////////////
        public bool isAssessmentAvailable(int AssessmentID,int StudentID,DateTime Start,DateTime End)
        {
            Debug.WriteLine(DateTime.Now + " == " + Start + " && + " + DateTime.Now + "  == " + End);
            if (DateTime.Now == Start && DateTime.Now == End)
            {
                return true;
            }
            else if (DateTime.Now >= Start && DateTime.Now <= End)
            {
                return true;
            }
            else
            {
                string Checker = cls.ExecuteScalar("SELECT [MakeUp] FROM PaceAssessment.dbo.Assessment WHERE AssessmentID='" + AssessmentID + "'");
                if (Checker.Contains("-" + StudentID + "-"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}
