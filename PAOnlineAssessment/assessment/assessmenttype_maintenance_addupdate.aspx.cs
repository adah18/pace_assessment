using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using PAOnlineAssessment.Classes;

namespace PAOnlineAssessment.assessment
{
    public partial class assessmenttype_maintenance_addupdate : System.Web.UI.Page
    {
        //Instantiate New Login User Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Instantiate New Collections Class
        Collections cls = new Collections();
        //Declare List of Assessment Types
        List<Constructors.AssessmentType> AssessmentTypeList;
        
        //Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Logged In User Info from Session Variable
            try
            {                
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == true)
                {
                }
                else
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_login));
                }
            }
            //if No Logged In User, redirect to Login Screen
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            //Check if Load type is postback
            if (IsPostBack == false)
            {
                LoadAssessmentTypeDetails();
            }
        }
        //Validate Textbox
        public bool ValidatorFields()
        {
            bool status = true;

            if (Validator.isEmpty(txtDescription.Text))
            {
                vlDescription.Text = "* Required Field";
                status = false;
            }
            else
            {
                vlDescription.Text = "*";
            }

            return status;
        }

        //submit button
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Validate Field for NULL values
            if (ValidatorFields())
            {
                //Check if Mode is Update
                if (Request.QueryString["action"] == "edit")
                {
                    string qry = "UPDATE PaceAssessment.dbo.AssessmentType SET Description = '" + Validator.Finalize(txtDescription.Text) + "', LastUpdateUser = '" + LUser.Username + "', LastUpdateDate = getdate() WHERE AssessmentTypeID ='" + Request.QueryString["atid"] + "'";
                    if (cls.ExecuteNonQuery(qry) > 0)
                    {
                        Response.Write("<script>alert('Assessment type has been updated successfully.'); window.location='" + ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main) + "';</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Action cannot continue. Please Try Again.')</script>");
                    }
                }
                //if mode is register
                else
                {
                    string qry = "INSERT INTO [PaceAssessment].dbo.[AssessmentType](Description, Status, UserCreated, DateCreated, LastUpdateUser, LastUpdateDate) " +
                                 "VALUES('" + Validator.Finalize(txtDescription.Text) + "', 'A','" + LUser.Username + "' , getdate(),'" + LUser.Username + "', getdate())";
                    if (cls.ExecuteNonQuery(qry) > 0)
                    {
                        Response.Write("<script>alert('Assessment type has been added successfully'); window.location='" + ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main) + "';</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Action cannot continue. Please Try Again.')</script>");
                    }
                }
            }
        }

        //cancel button
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_assessmenttype_maintenance_main));
        }
        //Load Assessment Type Details
        public void LoadAssessmentTypeDetails()
        {
            AssessmentTypeList = new List<Constructors.AssessmentType>(new Collections().getAssessmentType());
            AssessmentTypeList.ForEach(at => 
            {
                if (at.AssessmentTypeID.ToString() == Request.QueryString["atid"])
                {
                    txtDescription.Text = at.Description;                    
                    return;
                }
            });

        }
        
    }
}
