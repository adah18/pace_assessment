using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using PAOnlineAssessment.Classes;
using System.Globalization;
using System.Data.OleDb;
using System.Data;

namespace PAOnlineAssessment.parent
{
    public partial class parent_select_child : System.Web.UI.Page
    {
        //declare collection class
        Collections cls = new Collections();
        List<Constructors.ParentView> ParentList;
        List<Constructors.Sections> SectionList;
        List<Constructors.Levels> LevelList;
        List<Constructors.StudentRegistrationView> StudentList;
        List<Constructors.StudentAccount> StudAccountList = new List<Constructors.StudentAccount>(new Collections().getStudentAccounts());
        List<Constructors.ParentChildGrades> ChildList = new List<Constructors.ParentChildGrades>(new Collections().GetChild());
        SystemProcedures sp = new SystemProcedures();
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In
            try
            {
                //Get Login User Info from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                //Get Current Student Info from the Session Variable
                //CStudent = (CurrentStudent)Session["CurrentStudent"];
                //check if the user is login and a parent
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "P")
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

            if (!IsPostBack)
            {
                cboLevel.Items.Clear();
                cboLevel.Items.Add(new ListItem("--- Select Level ---", "0"));

                cboSection.Items.Clear();
                cboSection.Items.Add(new ListItem("--- Select Section ---", "0"));

                //load all levels
                LoadLevels();
                //load the header for grid view
                LoadTemplate();

                hidGridCount.Value = gvSelectedStudent.Rows.Count.ToString();
                //load the selected child in the database
                SelectedChild();
            }

            if (Request.QueryString["Remove"] == "1")
            {
                //uid=57&sid=13
                RemoveFromTheList();
            }
            //Validator.AlertBack("Selected students have been saved successfully.", ResolveUrl(DefaultForms.frm_parent_dashboard));
        }

        void RemoveFromTheList()
        {
            DataTable dt = Session["Student"] as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == Request.QueryString["uid"] && dt.Rows[i][1].ToString() == Request.QueryString["sid"])
                {
                    dt.Rows[i].Delete();
                }
            }
            Session["Student"] = dt;
            gvSelectedStudent.DataSource = Session["Student"];
            gvSelectedStudent.DataBind();
        }


        //load all level in the database
        void LoadLevels()
        {
            cboLevel.Items.Clear();
            cboLevel.Items.Add(new ListItem("--- Select Level ---", "0"));
            LevelList = new List<Constructors.Levels>(cls.GetLevels());
            LevelList.ForEach(l =>
            {
                if (l.Status == "A")
                {
                    cboLevel.Items.Add(new ListItem(l.LevelDescription, l.LevelID.ToString()));
                }
            });
        }

        protected void cboGradeLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load all section based on the selected level
            LoadSection(Convert.ToInt32(cboLevel.SelectedValue.ToString()));
        }

        //load all section from database
        void LoadSection(int LevelID)
        {
            //
            cboSection.Items.Clear();
            cboSection.Items.Add(new ListItem("--- Select Section ---", "0"));
            SectionList = new List<Constructors.Sections>(cls.GetSection());
            SectionList.ForEach(s =>
            {
                if (s.LevelID == LevelID)
                {
                    cboSection.Items.Add(new ListItem(s.SectionDescription, s.SectionID.ToString()));
                }
            });
        }

        //load a no record data in grid at first
        void LoadTemplate()
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("UserID");
            dt.Columns.Add("StudentID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("YearLevel");
            dt.Columns.Add("Section");
            dt.Columns.Add("Status");

            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0","0", "No Record Found", "", "", "");
            }

            Session["StudentList"] = dt;
            BindData();
            gvSelectedStudent.DataSource = dt;
            gvSelectedStudent.DataBind();
        }
        //load a no record data in grid at first
        void LoadTemplateSelected()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("UserID");
            dt.Columns.Add("StudentID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("YearLevel");
            dt.Columns.Add("Section");
            dt.Columns.Add("Status");

            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0","0", "No Student Selected Yet", "", "", "");
            }
            gvSelectedStudent.DataSource = dt;
            gvSelectedStudent.DataBind();
        }

        //load all students depends on the selected year level and section
        //that is registered in pace assessment
        void LoadStudent()
        {
 
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("YearLevel");
            dt.Columns.Add("Section");

            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentList.ForEach(s =>
            {
                StudAccountList.ForEach(sa => 
                {
                    if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && sa.StudentNumber == s.StudentNumber)
                    {
                        if (s.SectionID == Convert.ToInt32(cboSection.SelectedValue) && s.LevelID == Convert.ToInt32(cboLevel.SelectedValue))
                        {
                            dt.Rows.Add(s.StudentID, s.FirstName, s.LastName, s.LevelDescription, SectionDescription(s.SectionID));
                        }
                    }
                });
            });

            //if nothing was found
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add("0", "No Record Found", "", "", "");
            }

            Session["StudentList"] = dt;
            BindData();
        }

        //get the data in session to grid viuew
        void BindData()
        {
            gvStudentList.DataSource = Session["StudentList"];
            gvStudentList.DataBind();

            for (int row = 0; row < gvStudentList.Rows.Count; row++)
            {
                CheckBox chk = (CheckBox)gvStudentList.Rows[row].FindControl("chkTick");
                Label lbl = (Label)gvStudentList.Rows[row].FindControl("lblID");

                if (lbl.Text == "0")
                {
                    chk.Enabled = false;
                }
                else
                {
                    chk.Enabled = true;
                }
            }
        }
        #region "Grid View Events"

        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            gvStudentList.PageIndex = 0;
            BindData();
        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            if (gvStudentList.PageIndex != 0)
            {
                gvStudentList.PageIndex = gvStudentList.PageIndex - 1;
            }
        }

        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            gvStudentList.PageIndex = cboPageNumber.SelectedIndex;
            BindData();
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (gvStudentList.PageIndex != gvStudentList.PageCount)
            {
                gvStudentList.PageIndex = gvStudentList.PageIndex + 1;
                BindData();
            }
        }

        protected void lnkLast_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSave0_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {

        }
        #endregion
        protected void cboSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load all student
            LoadStudent();
           
        }
        // for adding all the selected student in the grid
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            for (int i = 0; i < gvStudentList.Rows.Count; i++)
            {
                //check if the checkbox is checked or not
                CheckBox chkTick = (CheckBox)gvStudentList.Rows[i].FindControl("chkTick");
                Label lblID = (Label)gvStudentList.Rows[i].FindControl("lblID");
                if (chkTick.Checked)
                {
                    dt.Rows.Add(lblID.Text);
                }
            }
            Session["SelectedStudent"] = dt;
            AddSelectedStudent();
            BindData();
        }


        //load all selected child from database
        void SelectedChild() 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserID");
            dt.Columns.Add("StudentID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("YearLevel");
            dt.Columns.Add("Section");
            dt.Columns.Add("SectionID");
            dt.Columns.Add("LevelID");
            dt.Columns.Add("Status");
            ChildList.ForEach(c => 
            {
                if (c.ParentUserID == LUser.UserID)
                {
                   
                    StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
                    StudentList.ForEach(s =>
                    {
                        if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.StudentID == c.StudentID)
                        {
                             dt.Rows.Add(LUser.UserID,s.StudentID, s.FirstName, s.LastName, s.LevelDescription, SectionDescription(s.SectionID), s.SectionID, s.LevelID,c.Status);
                        }
                    });
                }
                
            });

            GridView1.DataSource = dt;
            GridView1.DataBind();
            //already on the list
            Session["Student"] = dt;
            gvSelectedStudent.DataSource = dt;
            gvSelectedStudent.DataBind();
        }


        //add selected child to the grid
        void AddSelectedStudent()
        {
            DataTable dtID = (DataTable)Session["SelectedStudent"];
            DataTable dt = (DataTable)Session["Student"];

            for (int i = 0; i < dtID.Rows.Count; i++)
            {
                StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
                StudentList.ForEach(s =>
                {
                    if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.StudentID.ToString() == dtID.Rows[i][0].ToString())
                    {
                        if (s.SectionID == Convert.ToInt32(cboSection.SelectedValue) && s.LevelID == Convert.ToInt32(cboLevel.SelectedValue))
                        {
                            //check if the selected student is already in the list
                            if (isNotExisting(s.StudentID))
                            {
                                dt.Rows.Add(LUser.UserID, s.StudentID, s.FirstName, s.LastName, s.LevelDescription, SectionDescription(s.SectionID), s.SectionID, s.LevelID, 'D');
                            }
                            else
                            {
                                Debug.WriteLine("Student already exists in your database.");
                            }
                        }
                    }
                });
            }
            Session["Student"] = dt;
            gvSelectedStudent.DataSource = Session["Student"];
            gvSelectedStudent.DataBind();
        }

       //function that retrieve section description from database
        string SectionDescription(int sectionid)
        {
            string section = "";
            SectionList = new List<Constructors.Sections>(cls.GetSection());
            SectionList.ForEach(sl =>
            {
                if (sl.SectionID == sectionid)
                {
                    section = sl.SectionDescription;
                }
            });


            return section;
        }

        //save the student in the database
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //check if the selected students number change or not
            //if (gvSelectedStudent.Rows.Count == 0)
            //{
            //    //check if the all the student has been saved successfully
              

            //}
            //else
            //{
            //    Validator.AlertBack("Select a child before you can save to the database","parent_select_child.aspx");
            //}
            
            if (ChangeStudent())
            {
                if (StudentSaved())
                {
                    //Validator.AlertBack("Selected student has been saved successfully", "parent_dashboard.aspx");
                }
                Debug.WriteLine("Student Saved!");
                Validator.AlertBack("Selected students have been saved successfully.", ResolveUrl(DefaultForms.frm_parent_dashboard));
            }
        }

        //function for saving student
        bool StudentSaved()
        {
            bool value = true;
            DataTable dt = (DataTable)Session["Student"];
            //Debug.WriteLine("ang row count ay : " +dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string sql = "Insert Into PaceAssessment.dbo.ParentChild(ParentUserID, StudentID, Yearlevel, Section,Status)Values(" + LUser.UserID + ", " + dt.Rows[i][1].ToString() + ", " + dt.Rows[i][6].ToString() + ", " + dt.Rows[i][7].ToString() + ",'" + dt.Rows[i][8].ToString() + "')";
                //check if the data has been saved successfully in the database
                if (cls.ExecuteNonQuery(sql) != 1)
                {
                    value = false;
                }
                ////check if the student is not yet existing in the database
                //if (CanbeSaved(Convert.ToInt32(dt.Rows[i][0])))
                //{
                   
                //}
            }
                return value;
        }
        //function for checking if the selected student is already in the grid
        bool isNotExisting(int studentID)
        {
            bool value = true;
                DataTable dt = (DataTable)Session["Student"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (studentID.ToString() == dt.Rows[i][1].ToString())
                    {
                        value = false;
                    }
                }
               
           return value;
        }

        //function for checking if the student that will be saved is already exist in the database
        bool CanbeSaved(int studentID)
        {
            bool value = true;
            //DataTable dt = (DataTable)Session["old_session_student"];
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (studentID.ToString() == GridView1.Rows[i].Cells[1].Text)
                {
                    value = false;
                }
            
            }
            return value;
        }

        protected void gvSelectedStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //for (int i = 0; i < gvSelectedStudent.Rows.Count; i++)
            //{
            //    ImageButton btnRemove = gvSelectedStudent.Rows[i].FindControl("btnRemove") as ImageButton;
            //    Label lblUserID = gvSelectedStudent.Rows[i].FindControl("lblUserID") as Label;
            //    Label lblStudentID = gvSelectedStudent.Rows[i].FindControl("lblStudentID") as Label;

            //    btnRemove.OnClientClick="if(confirm('Do you want to remove this student as your child?')) { window.location='parent_select_child.aspx?remove=1&uid=" + lblUserID.Text + "&sid=" + lblStudentID.Text + "'; } return false;";
            //}
        }


        bool ChangeStudent()
        {
            bool x = true;
            string sql = "Delete From PaceAssessment.dbo.[ParentChild] Where ParentUserID=" + LUser.UserID;
            cls.ExecuteNonQuery(sql);
            return x;
        }

        protected void gvSelectedStudent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow grdRow = gvSelectedStudent.Rows[e.RowIndex];
            Label lblID = (Label)grdRow.FindControl("lblStudentID");
            
            DataTable dt = (DataTable)Session["Student"];

            //loop through the list of selected students
            foreach (DataRow dr in dt.Rows)
            {
                //find the student id
                if (dr["StudentID"].ToString() == lblID.Text)
                {
                    dr.Delete();
                    dt.AcceptChanges();
                    break;
                }
            }

            //save the changes
            Session["Student"] = dt;
            //bind the grid
            gvSelectedStudent.DataSource = Session["Student"];
            gvSelectedStudent.DataBind();

            BindData();
        }
    }
}
