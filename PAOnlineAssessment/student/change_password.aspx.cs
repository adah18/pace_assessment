using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.student
{
    public partial class change_password : System.Web.UI.Page
    {
        //Declare new Login User Class()
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Instantiate New Collections Class
        Collections cls = new Collections();


        //Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User is Logged In
            try 
            {
                LUser = (LoginUser)Session["LoggedUser"];
                //check if the user is login and a student
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "S")
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }

                if ((string)Session["UserGroupID"] != "S")
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }
            }
            //Redirect to Login Page if no User is found
            catch 
            {                
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }
            //Load Sitemap Settings
            LoadSitemapDetails();
            if (IsPostBack == false)
            {
                Debug.WriteLine(LUser.Password);
            }
        }

        //Load Sitemap settings
        public void LoadSitemapDetails()
        {
            SiteMap.RootNode = "Dashboard";
            SiteMap.RootNodeToolTip = "Click to go back to Dashboard";
            SiteMap.RootNodeURL = ResolveUrl(DefaultForms.frm_default_dashboard);

            SiteMap.ParentNode = "Account Settings";                       

            SiteMap.CurrentNode = lblMode.Text;
            
        }

        //validate textboxes
        public bool ValidateFields()
        {
            bool status = true;

            //validate if field is empty
            if (Validator.isEmpty(txtOldPassword.Text))
            {
                vlOldPassword.Text = "* Required Field.";
                status = false;
            }   
            //if old password field have a value
            else
            {
                //validate if old password entered matches the currently used password
                if (Validator.isNotEqual(txtOldPassword.Text,LUser.Password))
                {
                    vlOldPassword.Text = "* Password doesn't match your current password.";
                    status = false;
                }
                //if password matches
                else
                {
                    vlOldPassword.Text = "*";
                }
            }

            //If Password Field is empty
            if (Validator.isEmpty(txtNewPassword.Text))
            {
                vlNewPassword.Text = "* Required Field.";
                status = false;
            }
            //if password fields have values
            else
            {
                //validate if passwords entered
                if (Validator.isNotEqual(txtNewPassword.Text, txtConfirmPassword.Text))
                {
                    vlNewPassword.Text = "* Passwords doesn't match.";
                    status = false;
                }

                else if (Validator.Equals(txtOldPassword.Text, txtNewPassword.Text))
                {
                    vlNewPassword.Text = "* Your new password must be different from your old one.";
                    status = false;
                }

                else
                {
                    vlNewPassword.Text = "*";
                }
            }

            return status;
        }


        //submit button
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                string qry = "UPDATE [PaceAssessment].dbo.[Student] SET Password='"+Validator.Finalize(txtNewPassword.Text)+"',LastUpdateDate=GETDATE(), LastUpdateUser='"+LUser.Username+"' WHERE StudentID='"+LUser.UserID+"'";
                if (cls.ExecuteNonQuery(qry) > 0)
                {
                    LUser.Password = Validator.Finalize(txtNewPassword.Text);
                    Session["LoggedUser"] = LUser;
                    Response.Write("<script>alert('Your password has been updated successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_index) + "'</script>");
                }
                else
                {
                    Response.Write("<script>alert('Action not completed.\\r\\nPlease review your entries.');</script>");
                }
            }
            else
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_index));
        }
    }
}
