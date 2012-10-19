using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment
{
    public partial class frmHeader : System.Web.UI.UserControl
    {
        LoginUser LUser;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        List<Constructors.DisplaySettings> oDay = new List<Constructors.DisplaySettings>(new Collections().GetDays());
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the new
            oDay.ForEach(d =>
            {
                Session["Days"] = d.Days.ToString();
                Session["Registration"] = d.Registration;
            });
        }
    }
}
