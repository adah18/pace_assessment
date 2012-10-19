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

namespace PAOnlineAssessment.Parent
{
    public partial class edit_profile : System.Web.UI.Page
    {
        GlobalForms dForms = new Collections().getDefaultForms();
        Collections cls = new Collections();
        List<Constructors.ParentAccounts> ParentList;
        LoginUser LUser = new LoginUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LUser = Session["LoggedUser"] as LoginUser;
                if ((bool)Session["Authenticated"] == false || (string)Session["UserGroupID"] != "P")
                {
                    Response.Redirect(ResolveUrl(dForms.frm_index));
                }
            }
            catch
            {
                Response.Redirect(ResolveUrl(dForms.frm_index));
            }

            if (!IsPostBack)
            {
                LoadInformation();
            }
        }

        void LoadInformation()
        {
            ParentList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
            ParentList.ForEach(pl => 
            {
                if (pl.ParentID == LUser.UserID)
                {
                    lblUsername.Text = pl.Username;
                    for (int i = 0; i < pl.Password.Length; i++)
                    {
                        lblPassword.Text += "* ";
                    }
                    txtEmail.Text = pl.EmailAddress;
                    txtFirstname.Text = pl.Firstname;
                    txtLastname.Text = pl.Lastname;
                }
            
            });
        }

        bool CanbeSaved()
        {
            bool x = true;
            if (string.IsNullOrEmpty(txtFirstname.Text))
            {
                vlFirstname.Text = "* Firstname is Required.";
                x = false;
            }
            else if (Validator.isNotValid(txtFirstname.Text))
            {
                vlFirstname.Text = "* Please avoid using ' or -- .";
                x = false;
            }
            else
            {
                vlFirstname.Text = "* ";
            }

            if (string.IsNullOrEmpty(txtLastname.Text))
            {
                vlLastname.Text = "* Lastname is Required.";
                x = false;
            }
            else if (Validator.isNotValid(txtLastname.Text))
            {
                vlLastname.Text = "* Please avoid using ' or -- .";
                x = false;
            }
            else
            {
                vlLastname.Text = "* ";
            }

            lblEmail.Text = "* ";
            if (Validator.isEmailNotValid(txtEmail.Text) == true)
            {
                lblEmail.Text = "* Email address is not valid.";
                x = false;
            }
            else
            {
                if (Validator.isNotValid(txtEmail.Text))
                {
                    lblEmail.Text = "* Please avoid using ' or -- .";
                    x = false;
                }
            }

            return x;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CanbeSaved())
            {
                string sql = "Update PaceAssessment.dbo.[User] Set Firstname='" + txtFirstname.Text + "', Lastname='" + txtLastname.Text + "',UpdateDate=getdate(), UpdateUser='" + LUser.Username + "', EmailAddress='" + txtEmail.Text + "' Where UserID=" + LUser.UserID;
                if (cls.ExecuteNonQuery(sql) == 1)
                {
                    LUser.Firstname = txtFirstname.Text;
                    LUser.Lastname = txtLastname.Text;
                    Session["LoggedUser"] = LUser;
                    Validator.AlertBack("Your profile has been edited succesfully", "parent_dashboard.aspx");
                }
            }
        }
    }
}
