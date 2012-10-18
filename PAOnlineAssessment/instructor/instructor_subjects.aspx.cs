using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.instructor
{
    public partial class instructor_subjects : System.Web.UI.Page
    {
        //Instantiate New Login User Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Instantiate New Collections Class
        Collections cls = new Collections();
        //
        List<Constructors.User> UsersList;
        //Declare List for subject list
        List<Constructors.GradingView> SubjectList;

        #region "Load Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Logged In User Info from Session Variable
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

            //Load url in sitemap
            LoadSiteMap();
            //Check if the page is not postback
            if (!IsPostBack)
            {
                //Get all the assigned subject for the teacher
                LoadTeachers(Convert.ToInt32(LUser.UserGroupID));
               
            }

        }

        //Load subjects
        void GetSubjects()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SubjectID");
            dt.Columns.Add("SectionID");
            dt.Columns.Add("SubjectDescription");
            dt.Columns.Add("LevelDescription");
            dt.Columns.Add("SectionDescription");
            
            SubjectList = new List<Constructors.GradingView>(cls.getGradingView());
            SubjectList.ForEach(sl =>
            {
                if (sl.TeacherID.ToString() == cboTeacher.SelectedValue && sl.SchoolYear == Session["CurrentSchoolYear"].ToString())
                {
                    switch (cboSearchQuery.SelectedValue)
                    {
                        case "Section":
                            if (sl.LevelSection.ToLower().Contains(txtSearchQuery.Text.ToLower()))
                            {
                                dt.Rows.Add(
                                sl.SubjectID,
                                sl.SectionID,
                                sl.Description,
                                sl.GradingID,
                                sl.LevelSection
                                );
                            }
                            break;
                        case "Subject":
                            if(sl.Description.ToLower().Contains(txtSearchQuery.Text.ToLower()))
                            {
                                dt.Rows.Add(
                                sl.SubjectID,
                                sl.SectionID,
                                sl.Description,
                                sl.GradingID,
                                sl.LevelSection
                                );
                            }
                            break;

                        case "Grade":
                            if(sl.LevelSection.ToLower().Contains(txtSearchQuery.Text.ToLower()))
                            {
                                dt.Rows.Add(
                                sl.SubjectID,
                                sl.SectionID,
                                sl.Description,
                                sl.GradingID,
                                sl.LevelSection
                                );
                            }
                            break;
                        default:
                            break;
                    }
                }
            });

            dt.DefaultView.Sort = "SubjectDescription, LevelDescription, SectionDescription";
            Session["SubjectLists"] = dt;
            grdLoadSubjects.DataSource = Session["SubjectLists"];
            grdLoadSubjects.DataBind();

            SetButtonUrl();
        }

        void SetButtonUrl()
        {
            for (int i = 0; i < grdLoadSubjects.Rows.Count; i++)
            {
                Label lblSubjectID = (Label)grdLoadSubjects.Rows[i].FindControl("lblSubjectID");
                Label lblSectionID = (Label)grdLoadSubjects.Rows[i].FindControl("lblSectionID");

                ImageButton btnView = (ImageButton)grdLoadSubjects.Rows[i].FindControl("imgView");
                string EditUrl = ResolveUrl(DefaultForms.frm_instructor_studentsview) + "?subid=" + lblSubjectID.Text + "&secid=" + lblSectionID.Text;
                btnView.PostBackUrl = EditUrl;
            }
        }
        //create a site map
        void LoadSiteMap()
        {
            //add a name
            SiteMap1.RootNode = "Dashboard";
            //add a tool tip text
            SiteMap1.RootNodeToolTip = "Dashboard";
            //add a postbackurl
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_instructor_dashboard);

            SiteMap1.ParentNode = "Academic Activites";
            SiteMap1.ParentNodeToolTip = "Click to go back to Question Pool Main";
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_instructor_dashboard);

            //add a text for current node
            SiteMap1.CurrentNode = "My Subjects";
        }
        #endregion
        #region "Search Method"
        //Void for seacrh method
        void SearchMethod()
        {
            DataTable dt = (DataTable)Session["SubjectLists"];
            dt.Rows.Clear();
            SubjectList = new List<Constructors.GradingView>(cls.getGradingView());
            SubjectList.ForEach(sl =>
            {
                //Check if the Textbox has text
                if (txtSearchQuery.Text != "")
                {
                    switch (cboSearchQuery.SelectedValue)
                    {
                        //for subject
                        case "Subject":
                            {
                                if (sl.Description.ToLower().Contains(txtSearchQuery.Text.ToLower()) && sl.TeacherID == LUser.UserID && sl.SchoolYear == Session["CurrentSchoolYear"].ToString())
                                {
                                    dt.Rows.Add(sl.Description, sl.GradingID, sl.LevelSection);
                                }
                            }
                            break;
                        //for grades
                        case "Grade":
                            {
                                if (sl.LevelSection.ToLower().Contains(txtSearchQuery.Text.ToLower()) && sl.TeacherID == LUser.UserID && sl.SchoolYear == Session["CurrentSchoolYear"].ToString())
                                {
                                    dt.Rows.Add(sl.Description, sl.GradingID, sl.LevelSection);
                                }
                            }
                            break;
                        //for Sections
                        case "Section":
                            {
                                if (sl.LevelSection.ToLower().Contains(txtSearchQuery.Text.ToLower()) && sl.TeacherID == LUser.UserID && sl.SchoolYear == Session["CurrentSchoolYear"].ToString())
                                {
                                    dt.Rows.Add(sl.Description, sl.GradingID, sl.LevelSection);
                                }
                            }
                            break;
                    }

                    dt.DefaultView.Sort = "SubjectDescription, LevelDescription, SectionDescription";
                    Session["SubjectLists"] = dt;
                    grdLoadSubjects.DataSource = Session["SubjectLists"];
                    grdLoadSubjects.DataBind();
                }
                else
                {
                    //if textbox not have text
                    GetSubjects();
                }
            });
        }

        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            //call search method
            //SearchMethod();
            GetSubjects();
        }
        #endregion
        #region "GridView Events"
        protected void grdLoadSubjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //For Paging
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                cboPageNumber.Items.Clear();
                for (int x = 1; x <= grdLoadSubjects.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                lblPageCount.Text = "of " + grdLoadSubjects.PageCount.ToString();
                cboPageNumber.SelectedIndex = grdLoadSubjects.PageIndex;
            }
        }
        //bind the paged 
        void PagingData()
        {
            grdLoadSubjects.DataSource = (DataTable)Session["SubjectLists"];
            grdLoadSubjects.DataBind();
        }
        //executed when the First Page Linkbutton has been clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            grdLoadSubjects.PageIndex = 0;
            PagingData();
        }
        //executed when the Previous Page Linkbutton has been clicked
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            if (grdLoadSubjects.PageIndex != 0)
            {
                grdLoadSubjects.PageIndex -= 1;
                PagingData();
            }
        }
        //executed when the combo box page has been change
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            grdLoadSubjects.PageIndex = cboPageNumber.SelectedIndex;
            PagingData();
        }
        //executed when the Next Page Linkbutton has been clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (grdLoadSubjects.PageIndex != grdLoadSubjects.PageCount)
            {
                grdLoadSubjects.PageIndex += 1;
                PagingData();
            }
        }
        //executed when the Last Page Linkbutton has been clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            grdLoadSubjects.PageIndex = grdLoadSubjects.PageCount;
            PagingData();
        }



        #endregion

        //befor gridview is rendered
        protected void grdLoadSubjects_PreRender(object sender, EventArgs e)
        {
            //Debug.WriteLine(LUser.UserGroupID);
            grdLoadSubjects.TopPagerRow.Visible = true;
            grdLoadSubjects.BottomPagerRow.Visible = true;
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
            GetSubjects();  
        }

        protected void cboTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubjects();   
        }
    }
}
