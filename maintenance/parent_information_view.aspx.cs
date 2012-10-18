using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;

namespace PAOnlineAssessment.maintenance
{
    public partial class parent_information_view : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.ParentAccounts> ParentList;
        List<Constructors.ParentChilds> ParentChildList;
        List<Constructors.Sections> SectionList;
        List<Constructors.Levels> LevelList;
        List<Constructors.StudentRegistrationView> StudentList;
        SystemProcedures sp = new SystemProcedures();
        LoginUser LUser;
        List<Constructors.Quarter> oQuarter;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        int levelID = 0;
        int sectionID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Get Login User Info from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                //Get Current Student Info from the Session Variable
                //CStudent = (CurrentStudent)Session["CurrentStudent"];
                string Trigger = LUser.Username;
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == false)
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
                //get the current school year
                oQuarter = new List<Constructors.Quarter>(cls.getQuarter());
                Session["CurrentSchoolYear"] = "2010-2011";
                oQuarter.ForEach(q =>
                {
                    if (q.isCurrentSY == "YES")
                    {
                        Session["CurrentSchoolYear"] = q.SchoolYear;
                    }
                });

                StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
                if (Request.QueryString["uid"] != null)
                {
                    LoadParentInformation();
                    CanBeActivated();
                }
                else
                {
                    Validator.AlertBack("Select a Parent First", "parent_maintenance_main.aspx");
                }
            }

        }

        void CanBeActivated()
        {
            Button1.Enabled = true;
            Button1.ToolTip = "Activate Account";
            for (int i = 0; i < gvChildInfo.Rows.Count; i++)
            {
                Label lblStatus = gvChildInfo.Rows[i].FindControl("lblStatus") as Label;
                Debug.WriteLine(lblStatus.Text);
                if (lblStatus.Text == "0")
                {
                    Button1.Enabled = false;
                    Button1.ToolTip = "Parent cannot be activated";
                }
            }
            //ToolkitScriptManager1.RegisterDataItem(btnSubmit, "Amp");
        }

        void LoadParentInformation()
        {
            ParentList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
            ParentList.ForEach(pl => 
            {
                if (pl.ParentID.ToString() == Request.QueryString["uid"])
                {
                    lblFirstname.Text = pl.Firstname;
                    lblLastname.Text = pl.Lastname;
                    lblUsername.Text = pl.Username;
                    lblEmail.Text = pl.EmailAddress;
                    LoadStudents(pl.ParentID);
                    return;
                }
            });

        }

        void LoadStudents(int ParentID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Count");
            dt.Columns.Add("StudentID");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("Level");
            dt.Columns.Add("LevelID");
            ParentChildList = new List<Constructors.ParentChilds>(cls.GetParentChild(ParentID));
            int ctr = 0;

            ParentChildList.ForEach(pl =>
            {
                ctr++;
                dt.Rows.Add(ctr, "0" ,pl.Firstname, pl.Lastname, LevelDescription(Convert.ToInt32(pl.YearLevel)), pl.YearLevel);
            });

            Session["ChildList"] = dt;
            gvChildInfo.DataSource = Session["ChildList"];
            gvChildInfo.DataBind();
        }

        int MatchStudent(string fname, string lname, string levelid)
        {
            Label lblSFirstname = new Label();
            Label lblSLastname =new Label();
            Label lbllevelID = new Label();
            Label lblLevel = new Label();

            lblSFirstname.Text = fname;
            lblSLastname.Text = lname;
            lbllevelID.Text = levelid;
            lblLevel.Text = LevelDescription(Convert.ToInt32(levelid));

            string strfirst = lblSFirstname.Text;
            if (strfirst.Length < 4)
            {   
                strfirst = strfirst + " ";
            }
            else
            {
                strfirst = strfirst.Substring(0, 4);
            }
            string strlast = lblSLastname.Text;

            if (strlast.Length < 4)
            {
                strlast = strlast + " ";
            }
            else
            {
                strlast = strlast.Substring(0, 4);
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("YearLevel");
            dt.Columns.Add("Status");
            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentList.ForEach(s =>
            {
                if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.LevelID.ToString() == lbllevelID.Text)
                {
                    if (s.FirstName.ToLower() == fname.ToLower() && s.LastName.ToLower() == lname.ToLower())
                    {
                        dt.Rows.Add(s.StudentID, s.FirstName, s.LastName, lblLevel.Text, "A");
                    }
                }
            });
            return dt.Rows.Count;
        }


        string LevelDescription(int levelid)
        {
            string level = "";
            LevelList = new List<Constructors.Levels>(cls.GetLevels());
            LevelList.ForEach(l => 
            {
                if (levelid == l.LevelID)
                {
                    level = l.LevelDescription;
                }
            });
            return level;
        }
        string SectionDescription(int sectionid)
        {
            string section="";
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

        protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

       

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (sp.ActivateParent("[User]", "UserID", Request.QueryString["uid"], LUser.Username) == 1)
            {
                if (LinkStudents() == 1)
                {
                    Response.Redirect("parent_maintenance_main.aspx?success=1");
                }
                else
                {
                    Validator.AlertBack("Something went wrong while adding the students. Pelase try again later", "parent_maintenance_main.aspx");
                }
            }
        }

        int LinkStudents()
        {
            int x = 1;
            int studentid = 0;
            int sectionid = 0;
            for (int i = 0; i < gvChildInfo.Rows.Count; i++)
            {
                Label lblSFirstname = gvChildInfo.Rows[i].FindControl("lblFirstname") as Label;
                Label lblSLastname = gvChildInfo.Rows[i].FindControl("lblLastname") as Label;
                Label lbllevelID = gvChildInfo.Rows[i].FindControl("lblLevelID") as Label;

                StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
                StudentList.ForEach(s =>
                    {
                        if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.LevelID.ToString() == lbllevelID.Text)
                        {
                            if (s.FirstName.ToLower() == lblSFirstname.Text.ToLower() && s.LastName.ToLower() == lblSLastname.Text.ToLower())
                            {
                                studentid = s.StudentID;
                                sectionid = s.SectionID;
                            }
                        }
                    });

                string sql = "Insert into PaceAssessment.dbo.[ParentChild](ParentUserID, StudentID, YearLevel, Section, Status)Values('" + Request.QueryString["uid"].ToString() + "','" + studentid.ToString() + "','" + lbllevelID.Text + "','" + sectionid.ToString() + "', 'A')";
                if (cls.ExecuteNonQuery(sql) != 1)
                {
                    x = 0;
                }
            }
            return x;
        }
        protected void btnVer_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnVer = sender as ImageButton;

            string row_count = btnVer.Attributes["RowNumber"];
            Debug.WriteLine(row_count);           

            Label lblSFirstname = gvChildInfo.Rows[Convert.ToInt32(row_count) - 1].FindControl("lblFirstname") as Label;
            Label lblSLastname = gvChildInfo.Rows[Convert.ToInt32(row_count) - 1].FindControl("lblLastname") as Label;
            Label lbllevelID = gvChildInfo.Rows[Convert.ToInt32(row_count) - 1].FindControl("lblLevelID") as Label;
            Label lblLevel  = gvChildInfo.Rows[Convert.ToInt32(row_count) - 1].FindControl("lblLevel") as Label;

            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("YearLevel");
            dt.Columns.Add("Section");
            dt.Columns.Add("Status");

            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentList.ForEach(s =>
            {
                Debug.WriteLine("School Year:" + Session["CurrentSchoolYear"].ToString());
                if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.LevelID.ToString() == lbllevelID.Text)
                {
                    //if (s.FirstName.ToLower().Contains(strfirst.ToLower()) || s.LastName.ToLower().Contains(strlast.ToLower()))
                    //{
                        dt.Rows.Add(s.StudentID, s.FirstName, s.LastName, lblLevel.Text, "", "A");
                    //}
                }
            });

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add("0", "No Record Found", "", "", "", "");
            }

            Session["StudentMatch"] = dt;
            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
            lblRowIndex.Text = (int.Parse(row_count) - 1).ToString();
            mpeStudentModal.Show();
        }

        void Search()
        {
            int RowIndex = Convert.ToInt32(lblRowIndex.Text);

            Label lbllevelID = gvChildInfo.Rows[RowIndex].FindControl("lblLevelID") as Label;
            Label lblLevel = gvChildInfo.Rows[RowIndex].FindControl("lblLevel") as Label;

            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("YearLevel");
            dt.Columns.Add("Section");
            dt.Columns.Add("Status");

            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentList.ForEach(s =>
            {

                if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.LevelID.ToString() == lbllevelID.Text)
                {
                    if (s.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) || s.LastName.ToLower().Contains(txtSearch.Text.ToLower()))
                    {
                        dt.Rows.Add(s.StudentID, s.FirstName, s.LastName, lblLevel.Text, "", "A");
                    }
                }
            });

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add("0", "No Record Found", "", "", "", "");
            }

            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
        }
         
        protected void gvChildInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        { 
            for (int i = 0; i < gvChildInfo.Rows.Count; i++)
            {
                Label lblSFirstname = gvChildInfo.Rows[i].FindControl("lblFirstname") as Label;
                Label lblSLastname = gvChildInfo.Rows[i].FindControl("lblLastname") as Label;
                Label lbllevelID = gvChildInfo.Rows[i].FindControl("lblLevelID") as Label;
                Label lblLevel = gvChildInfo.Rows[i].FindControl("lblLevel") as Label;
                Label lblStatus = gvChildInfo.Rows[i].FindControl("lblStatus") as Label;
                Image imgStatus = gvChildInfo.Rows[i].FindControl("imgStatus") as Image;

                if (MatchStudent(lblSFirstname.Text, lblSLastname.Text, lbllevelID.Text) > 0)
                {
                    imgStatus.ImageUrl = "~/images/icons/Check-icon.png";
                    //imgStatus.ToolTip = "Student has match student(s) registered in pace assessment";
                    lblStatus.Text = "1";
                }
                else
                {
                    imgStatus.ImageUrl = "~/images/icons/cross-icon.png";
                    //imgStatus.ImageUrl = "Student did not match any student(s) registered in pace assessment";
                    lblStatus.Text = "0";
                }
            }

            CanBeActivated();
        }


        int GetStudentID(string fname, string lname, string levelid)
        {
            Label lblSFirstname = new Label();
            Label lblSLastname = new Label();
            Label lbllevelID = new Label();
            Label lblLevel = new Label();

            lblSFirstname.Text = fname;
            lblSLastname.Text = lname;
            lbllevelID.Text = levelid;
            lblLevel.Text = LevelDescription(Convert.ToInt32(levelid));

            int id = 0;
            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentList.ForEach(s =>
            {

                if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.LevelID.ToString() == lbllevelID.Text)
                {
                    if (s.FirstName.ToLower() == fname.ToLower() || s.LastName.ToLower() == lname.ToLower())
                    {
                        id = s.StudentID;
                    }
                }
            });

            return id;
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
           Search();
        }

        protected void lnkReplace_Click(object sender, EventArgs e)
        {
            int RowIndex = Convert.ToInt32(lblRowIndex.Text);

            LinkButton lnk = sender as LinkButton;
            
            int StudentID = Convert.ToInt32(lnk.Attributes["StudentID"].ToString());

            Label lblSFirstname = gvChildInfo.Rows[RowIndex].FindControl("lblFirstname") as Label;
            Label lblSLastname = gvChildInfo.Rows[RowIndex].FindControl("lblLastname") as Label;
            Label lbllevelID = gvChildInfo.Rows[RowIndex].FindControl("lblLevelID") as Label;
            Label lblLevel = gvChildInfo.Rows[RowIndex].FindControl("lblLevel") as Label;
            Label lblStatus = gvChildInfo.Rows[RowIndex].FindControl("lblStatus") as Label;
            Image imgStatus = gvChildInfo.Rows[RowIndex].FindControl("imgStatus") as Image;

            DataTable dt = (DataTable)Session["ChildList"];

            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentList.ForEach(s =>
                {
                    if (s.SchoolYear == Session["CurrentSchoolYear"].ToString() && s.StudentID == StudentID)
                    {
                        lblSFirstname.Text = s.FirstName;
                        lblSLastname.Text = s.LastName;
                        dt.Rows[RowIndex][2] = s.FirstName;
                        dt.Rows[RowIndex][3] = s.LastName;
                        dt.AcceptChanges();
                    }
                });

            Session["ChildList"] = dt;
            gvChildInfo.DataSource = Session["ChildList"];
            gvChildInfo.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            btnSubmit_Click(this, new EventArgs());
        }

        protected void gvChildInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void DataGrid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount0");
                lblPageCount.Text = "of " + DataGrid1.PageCount;
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber0");
                cboPageNumber.Items.Clear();
                for (int x = 1; x <= DataGrid1.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = DataGrid1.PageIndex;
            }
        }

        protected void lnkFirst0_Click(object sender, EventArgs e)
        {
            DataGrid1.PageIndex = 0;
            DataGrid1.DataSource = (DataTable)Session["StudentMatch"];
            DataGrid1.DataBind();
        }

        protected void lnkLast0_Click(object sender, EventArgs e)
        {
            DataGrid1.PageIndex = DataGrid1.PageCount;
            DataGrid1.DataSource = (DataTable)Session["StudentMatch"];
            DataGrid1.DataBind();
        }

        protected void cboPageNumber0_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber0 = (DropDownList)sender;
            DataGrid1.PageIndex = cboPageNumber0.SelectedIndex;
            DataGrid1.DataSource = (DataTable)Session["StudentMatch"];
            DataGrid1.DataBind();
        }

    }
}
