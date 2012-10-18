using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;

namespace PAOnlineAssessment.maintenance
{
    public partial class quarter_maintenance : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.Quarter> QuarterList;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Declare Login User Class;
        LoginUser LUser;
        string schoolyear = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Logged In User Info from Session Variable
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];

                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                if (Validator.CanbeAccess("2", LUser.AccessRights) == false)
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

          

            if (!IsPostBack)
            {
                //schoolyear = cls.ExecuteScalar("Select SchoolYear From PaceRegistration.dbo.SchoolDays Order By SchoolYear Desc");
                Debug.WriteLine(schoolyear);
                LoadQuarterList();
            }
        }

        void LoadQuarterList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Quarter");
            dt.Columns.Add("DateFrom");
            dt.Columns.Add("DateTo");
            dt.Columns.Add("SchoolYear");
            QuarterList = new List<Constructors.Quarter>(cls.getQuarter());
            QuarterList.ForEach(q => 
            {
                if (q.isCurrentSY == "YES")
                    dt.Rows.Add(q.Quarters, Convert.ToDateTime(q.DateFrom).ToShortDateString(), Convert.ToDateTime(q.DateTo).ToShortDateString(), q.SchoolYear);
            });

            Session["QuarterList"] = dt;
            BindData();
        }
        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void gvQuarter_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvQuarter.EditIndex = e.NewEditIndex;
            //BindData();
        }


        void BindData()
        {
            gvQuarter.DataSource = Session["QuarterList"];
            gvQuarter.DataBind();
        }

        protected void gvQuarter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < gvQuarter.Rows.Count; i++)
            {
                Label qtr = (Label)gvQuarter.Rows[i].FindControl("lblQuarter");
                ImageButton edit = (ImageButton)gvQuarter.Rows[i].FindControl("ImageButton1");
                edit.PostBackUrl = DefaultForms.frm_quarter_edit + "?qtr=" + qtr.Text;

                //Debug.WriteLine(cls.CurrentQuarter());
                if (qtr.Text == cls.CurrentQuarter())
                {
                    qtr.Font.Bold = true;
                }
            }
        }


    }
}
