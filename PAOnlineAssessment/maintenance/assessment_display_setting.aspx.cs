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

namespace PAOnlineAssessment.maintenance
{
    public partial class assessment_display_setting : System.Web.UI.Page
    {
        Collections cls = new Collections();

        List<Constructors.DisplaySettings> oDisplaySettings;

        //Declare a LoginUser Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();

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

                if (Validator.CanbeAccess("0", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed.");

                    Validator.AlertBack("Access Denied!", "../block_user.aspx");
                }
            }
            //Redirect to Login Page when no User is logged in
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            if (!IsPostBack)
            {
                oDisplaySettings = new List<Constructors.DisplaySettings>(cls.GetDays());

                oDisplaySettings.ForEach(ds =>
                    {
                        txtDays.Text = ds.Days.ToString();
                        if (ds.Registration == "Off")
                            chk.Checked = false;
                        else
                            chk.Checked = true;
                    });
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            int days;
            string OnOff = "";
            if (!string.IsNullOrEmpty(txtDays.Text))
            {
                if (int.TryParse(txtDays.Text, out days) && Convert.ToInt32(txtDays.Text) > 0)
                {
                    lblErr.Text = "";
                    if (chk.Checked == true)
                        OnOff = "On";
                    else
                        OnOff = "Off";
                    
                    string qry = "UPDATE PaceAssessment.dbo.Settings SET Days='" + days + "',Registration='" + OnOff + "'";
                    cls.ExecuteNonQuery(qry);

                    Session["Days"] = txtDays.Text;
                    Session["Registration"] = OnOff;
                    Response.Write("<script>alert('System Settings has been saved successfully.'); window.location='" + ResolveUrl("~/maintenance/assessment_display_setting.aspx") + "'</script>");
                }
                else
                {
                    lblErr.Text = "This must be an integer ex: 1";
                }
            }
            else
            {
                lblErr.Text = "Days is a required field.";
            }
        }
    }
}
