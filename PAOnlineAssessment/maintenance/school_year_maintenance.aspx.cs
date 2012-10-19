using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.maintenance
{
    public partial class school_year_maintenance : System.Web.UI.Page
    {
        //instantiate user login
        LoginUser LUser;

        //call all forms
        GlobalForms DefaultForms = new Collections().getDefaultForms();

        //instantiate quarter
        List<Collections.Quarter> oQuarter;

        //instantiate collection
        Collections cls = new Collections();

        //public int
        int YearStart, YearEnd;
        //query string 
        string qry = "";
        bool isNew;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User Is Logged In
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                if (Validator.CanbeAccess("19", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed");

                    Validator.AlertBack("Access Denied!", "../block_user.aspx");
                }
            }
            //Redirect to Login Page when no User is logged in
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            //check if postback
            if (!Page.IsPostBack)
            {
                //current school year
                LoadCurrentSchoolYear();
            }
        }

        void LoadCurrentSchoolYear()
        {
            //instantiate new list
            oQuarter = new List<Constructors.Quarter>(cls.getQuarter());
            //loop through the lisr
            oQuarter.ForEach(q =>
            {
                //check if the current school year
                if (q.isCurrentSY == "YES")
                {
                    //set the current school year
                    string[] SY = q.SchoolYear.Split('-');

                    //set to session variable
                    Session["YearStart"] = SY[0];
                    Session["YearEnd"] = SY[1];

                    //display the value
                    YearStart = Convert.ToInt32(Session["YearStart"]);
                    YearEnd = Convert.ToInt32(Session["YearEnd"]);
                    txtschoolyear.Text = YearStart.ToString() + "-" + YearEnd.ToString();
                    return;
                }
            });
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            //get the session value
            YearStart = Convert.ToInt32(Session["YearStart"]);
            YearEnd = Convert.ToInt32(Session["YearEnd"]);

            isNew = true;

            oQuarter = new List<Constructors.Quarter>(cls.getQuarter());
            //loop through the list
            oQuarter.ForEach(q =>
            {
                //check if school year was existing
                if (q.SchoolYear == txtschoolyear.Text)
                    isNew = false;
            });

            //check if new
            if (isNew == true)
            {
                //for new school year
                //removed the current school year setting
                qry = "UPDATE [Quarter] SET isCurrentSY='NO' WHERE isCurrentSY='YES'";
                cls.ExecuteNonQuery(qry);

                //save the new current school year
                //1st quarter
                qry = "INSERT INTO [Quarter] (SchoolYear, Quarter, DateFrom, DateTo, LastUpdateDate, LastUpdateUser, isCurrentSY) VALUES ('" + txtschoolyear.Text + "','1st','" + YearStart.ToString() + "-06-01','" + YearStart.ToString() + "-08-16','" + DateTime.Now.ToString() + "','" + LUser.Username + "','YES')";
                cls.ExecuteNonQuery(qry);
                Debug.WriteLine("1st: " + qry);

                //2nd quarter
                qry = "INSERT INTO [Quarter] (SchoolYear, Quarter, DateFrom, DateTo, LastUpdateDate, LastUpdateUser, isCurrentSY) VALUES ('" + txtschoolyear.Text + "','2nd','" + YearStart.ToString() + "-08-17','" + YearStart.ToString() + "-10-31','" + DateTime.Now.ToString() + "','" + LUser.Username + "','YES')";
                cls.ExecuteNonQuery(qry);
                Debug.WriteLine("2nd: " + qry);

                //3rd quarter
                qry = "INSERT INTO [Quarter] (SchoolYear, Quarter, DateFrom, DateTo, LastUpdateDate, LastUpdateUser, isCurrentSY) VALUES ('" + txtschoolyear.Text + "','3rd','" + YearStart.ToString() + "-11-02','" + YearEnd.ToString() + "-01-16','" + DateTime.Now.ToString() + "','" + LUser.Username + "','YES')";
                cls.ExecuteNonQuery(qry);
                Debug.WriteLine("3rd: " + qry);

                //4th quarter
                qry = "INSERT INTO [Quarter] (SchoolYear, Quarter, DateFrom, DateTo, LastUpdateDate, LastUpdateUser, isCurrentSY) VALUES ('" + txtschoolyear.Text + "','4th','" + YearEnd.ToString() + "-01-17','" + YearEnd.ToString() + "-03-31','" + DateTime.Now.ToString() + "','" + LUser.Username + "','YES')";
                cls.ExecuteNonQuery(qry);
                Debug.WriteLine("4th: " + qry);
            }
            else if(isNew == false)
            {
                //for existing school year
                //removed the current saved setting
                qry = "UPDATE [Quarter] SET isCurrentSY='NO' WHERE isCurrentSY='YES'";
                cls.ExecuteNonQuery(qry);

                //update the current school year
                qry = "UPDATE [Quarter] SET isCurrentSY='YES' WHERE SchoolYear='"  + txtschoolyear.Text + "'";
                cls.ExecuteNonQuery(qry);
            }
            //set the new school year
            Session["CurrentSchoolYear"] = txtschoolyear.Text;

            //check the current quarter
            Session["Quarter"] = cls.CurrentQuarter();
            Validator.AlertBack("School year setting has been saved successfully. ", ResolveUrl(DefaultForms.frm_school_year_maintenance));
        }

        protected void imgleft_Click(object sender, ImageClickEventArgs e)
        {
            //decrease school year
            YearStart = Convert.ToInt32(Session["YearStart"]) - 1;
            YearEnd = Convert.ToInt32(Session["YearEnd"]) - 1;

            //send value to session
            Session["YearStart"] = YearStart;
            Session["YearEnd"] = YearEnd;
            txtschoolyear.Text = YearStart.ToString() + "-" + YearEnd.ToString();
        }

        protected void imgright_Click(object sender, ImageClickEventArgs e)
        {
            //increase school year
            YearStart = Convert.ToInt32(Session["YearStart"]) + 1;
            YearEnd = Convert.ToInt32(Session["YearEnd"]) + 1;

            //send value to session
            Session["YearStart"] = YearStart;
            Session["YearEnd"] = YearEnd;
            txtschoolyear.Text = YearStart.ToString() + "-" + YearEnd.ToString();
        }
    }
}
