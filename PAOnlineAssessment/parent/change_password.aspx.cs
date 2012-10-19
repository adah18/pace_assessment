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
    public partial class change_password : System.Web.UI.Page
    {
        Collections cls = new Collections();
        LoginUser LUser = new LoginUser();
        //instantiate new collection
        GlobalForms dForms = new Collections().getDefaultForms();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LUser = Session["LoggedUser"] as LoginUser;
                //check if the user is login and a parent
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "P")
                {
                    Response.Redirect(ResolveUrl(dForms.frm_index));
                }
            }
            catch
            {
                Response.Redirect(ResolveUrl(dForms.frm_index));
            }
        }

        bool CanBeSaved()
        {
            bool x = true;
            if (string.IsNullOrEmpty(txtOldPassword.Text))
            {
                vlOldPassword.Text = "* Current Password is Required";
                x = false;
            }
            else if (LUser.Password != txtOldPassword.Text)
            {
                vlOldPassword.Text = "* Password entered did not match your current Password";
                x = false;
            }
            else
            {
                vlOldPassword.Text = "* ";
            }

            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                vlNewPassword.Text = "* New Password is Required";
                x = false;
            }
            else
            {
                if (txtNewPassword.Text != txtCPassword.Text)
                {
                    vlNewPassword.Text = "* New Password and Confirm Password did not match.";
                    x = false;
                }

                else if (txtOldPassword.Text == txtNewPassword.Text)
                {
                    vlNewPassword.Text = "* Your new password must be different from your old one.";
                    x = false;
                }

                else
                {
                    vlNewPassword.Text = "* ";
                }
            }

            return x;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CanBeSaved())
            {
                string sql = "Update PaceAssessment.dbo.[User] Set Password='" + txtNewPassword.Text + "', UpdateDate=getdate(),UpdateUser='" + LUser.Username + "' Where UserID=" + LUser.UserID;
                if (cls.ExecuteNonQuery(sql) == 1)
                {
                    Validator.AlertBack("Password has been changed successfully.", "parent_dashboard.aspx");
                }
            }
        }
    }
}
