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
    public partial class parent_pending_view : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.ParentAccounts> ParentList;
        List<Constructors.ParentChilds> ParentChildList;
        List<Constructors.ParentChildGrades> ChildList;
        List<Constructors.StudentRegistrationView> StudentList;
        List<Constructors.Sections> SectionList;
        List<Constructors.Levels> LevelList;
        SystemProcedures sp = new SystemProcedures();
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Get Login User Info from the Session Variable
                LUser = (LoginUser)Session["LoggedUser"];
                //Get Current Student Info from the Session Variable
                //CStudent = (CurrentStudent)Session["CurrentStudent"];
                string Trigger = LUser.Username;
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

            if (!IsPostBack)
            {
                LoadInfo();
            }
        }



        void LoadInfo()
        {
            ParentList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
            ParentList.ForEach(pl => 
            {
                if (pl.ParentID.ToString() == Request.QueryString["uid"])
                {
                    lblName.Text = pl.Firstname + " " + pl.Lastname;
                    hidParentID.Value = pl.ParentID.ToString();
                }
            });
            LoadStudents();
        }

        void LoadStudents()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ChildID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Level");
            dt.Columns.Add("Section");
            dt.Columns.Add("Status");
            dt.Columns.Add("LevelID");
            dt.Columns.Add("SectionID");


            ChildList = new List<Constructors.ParentChildGrades>(cls.GetChild());
            ChildList.ForEach(cl => 
            {
                if (cl.ParentUserID.ToString() == hidParentID.Value && cl.Status == "D")
                {
                    dt.Rows.Add(cl.ChildID, GetStudentName(cl.StudentID), LevelDescription(cl.SectionID), SectionDescription(cl.LevelID), cl.Status, cl.LevelID, cl.SectionID);
                }
            });
            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "No Record Found", "", "");
            }

            Session["ChildList"] = dt;
            DataGrid.DataSource = dt;
            DataGrid.DataBind();
            LoadApproveStudent();
        }

        void LoadApproveStudent()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ChildID");
            dt.Columns.Add("Name");
            dt.Columns.Add("YearLevel");
            dt.Columns.Add("Section");
            dt.Columns.Add("Status");
           


            ChildList = new List<Constructors.ParentChildGrades>(cls.GetChild());
            ChildList.ForEach(cl =>
            {
                if (cl.ParentUserID.ToString() == hidParentID.Value && cl.Status == "A")
                {
                    dt.Rows.Add(cl.ChildID, GetStudentName(cl.StudentID), LevelDescription(cl.SectionID), SectionDescription(cl.LevelID), cl.Status);
                }
            });
            if (dt.Rows.Count < 1)
            {
                dt.Rows.Add("0", "No Record Found", "", "");
            }

            Session["ApprovedChild"] = dt;
            gvStudent.DataSource = dt;
            gvStudent.DataBind();
        }

        string GetStudentName(int StudentID)
        {
            string name = "";
            StudentList = new List<Constructors.StudentRegistrationView>(cls.getStudentRegistrationView());
            StudentList.ForEach(sl =>
            {
                if (sl.SchoolYear == Session["CurrentSchoolYear"].ToString() && StudentID == sl.StudentID)
                {
                    name = sl.FirstName + " " + sl.LastName;
                }

            });
            return name;
        }
        string LevelDescription(int levelid)
        {
            Debug.WriteLine(levelid);
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

        protected void DataGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            int x = 0;
            for (int i = 0; i < DataGrid.Rows.Count; i++)
            {
                CheckBox chk = DataGrid.Rows[i].FindControl("chkApprove") as CheckBox;
                Label lblID = DataGrid.Rows[i].FindControl("lblID") as Label;
                if (chk.Checked == true)
                {
                    string sql = "Update PaceAssessment.dbo.ParentChild Set Status='A' Where ChildID=" + lblID.Text;
                    if (cls.ExecuteNonQuery(sql)==1)
                    {
                        x++;// 
                    }
                }
            }
            if (x > 0)
            {
                Validator.AlertBack("Request child has been approved", "parent_pending_students.aspx");
            }
            else
            {
                Validator.AlertBack("No student has been approved", "parent_pending_students.aspx");
            }
        }
    }
}
