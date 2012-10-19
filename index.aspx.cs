using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment
{
    public partial class index : System.Web.UI.Page
    {
        //Declare LoginUser Variable
        LoginUser LUser;
        //Instantiate Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        String Quarter = new Collections().CurrentQuarter();
        List<Constructors.DisplaySettings> oDays = new List<Constructors.DisplaySettings>(new Collections().GetDays());
        List<Constructors.Quarter> oQuarter;
        Collections cls = new Collections();
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["Quarter"] = Quarter;
            oDays.ForEach(d =>
                {
                    Session["Days"] = d.Days.ToString();
                    Session["Registration"] = d.Registration;
                });
            //Check if a User Is Logged In
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
            }

            //Redirect to Login Page when no User is logged in
            catch
            {
                Response.Redirect(DefaultForms.frm_login);
            }
           
            //Redirect to Default Dashboard

            Response.Redirect(ResolveUrl(DefaultForms.frm_default_dashboard));

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
        }

    }
}

