using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment
{
    public partial class frmFooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //set session timeout
                Session.Timeout = 120;

                //compute
                string SecondsTimeout = ((Session.Timeout * 60) + 1).ToString();

                string javascriptstr = "var count = " + SecondsTimeout + "; var counter = setInterval(\"timer()\", 1000); function timer() { count = count - 1;  if (count <= 0) { clearInterval(counter); alert('Your session has expired. Please Login again to continue.'); location.href = \"../logout.aspx\"; return; }} ";
                Page pg = (Page)HttpContext.Current.Handler;

                if (pg.ClientScript.IsStartupScriptRegistered("SessionTimer") == false)
                {
                    pg.ClientScript.RegisterStartupScript(pg.GetType(), "SessionTimer", javascriptstr, true);
                }
            }
            catch
            {

            }
        }
    }
}