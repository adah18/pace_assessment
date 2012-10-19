using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment
{
    public partial class Default : System.Web.UI.Page
    {
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_index));
        }
    }
}
