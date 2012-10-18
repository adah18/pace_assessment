using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic;
namespace PAOnlineAssessment.maintenance
{
    public partial class quarter_edit : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.Quarter> QuarterList;
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Declare Login User Class;
        LoginUser LUser;

        public string schoolyear;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get Logged In User Info from Session Variable
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];
                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                }
            }
            //if No Logged In User, redirect to Login Screen
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            LoadSiteMap();
            //schoolyear = cls.ExecuteScalar("Select SchoolYear From PaceRegistration.dbo.SchoolDays Order By SchoolYear Desc");
            if (!Page.IsPostBack)
            {
                LoadQuarterData();
                //Response.Redirect("quarter_maintenance.aspx");
                Debug.WriteLine(schoolyear);
            }
        }

        void LoadQuarterData()
        {
            //Response.Write("<script>alert('" + txtDateFrom.Text + "')</script>");
            QuarterList = new List<Constructors.Quarter>(cls.getQuarter());
            QuarterList.ForEach(q => 
            {
                if (q.Quarters == Request.QueryString["qtr"] && q.isCurrentSY == "YES")
                {
                    lblQuarter.Text = q.Quarters;
                    txtDateFrom.Text = Convert.ToDateTime(q.DateFrom).ToShortDateString();
                    txtDateTo.Text = Convert.ToDateTime(q.DateTo).ToShortDateString();
                    lblSchoolYear.Text = q.SchoolYear;
                    schoolyear = q.SchoolYear;
                }
            });
        }

        void LoadSiteMap()
        {
            //add a name
            SiteMap1.RootNode = "Dashboard";
            //add a tool tip text
            SiteMap1.RootNodeToolTip = "Dashboard";
            //add a postbackurl
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.ParentNode = "Quarter Maintenance";
            SiteMap1.ParentNodeToolTip = "Click to go back to Assessment Maintenance Main";
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_quarter_maintenance);

            //add a text for current node
            SiteMap1.CurrentNode = "Edit Quarter";
        }


        //Validate all the controls in the field
        bool ValidateForms()
        {
            //error handler
            bool haserror = false;

            DateTime DateFrom, DateTo;

            //for the date from
            lbl0.Text = "* ";
            //check if empty
            if (Validator.isEmpty(txtDateFrom.Text))
            {
                haserror = true;
                lbl0.Text = "* Required Field";
            }
            else
            {
                //check if date is valid
                if (DateTime.TryParse(txtDateFrom.Text, out DateFrom) == true)
                {
                    //check is the range between the current school year
                    if (CheckDate(Convert.ToDateTime(txtDateFrom.Text)) == 0)
                    {
                        haserror = true;
                        lbl0.Text = "* Date did not match in the school year.";
                    }
                }
                else
                {
                    //not a valid date
                    haserror = true;
                    lbl0.Text = "* Please enter a valid date.";
                }
            }

            //for the date to
            lbl1.Text = "* ";
            //check if has data
            if (Validator.isEmpty(txtDateTo.Text))
            {
                haserror = true;
                lbl1.Text = "* Required Field";
            }
            else
            {
                //check if the date is valid
                if (DateTime.TryParse(txtDateTo.Text, out DateTo) == true)
                {
                    //check if the date is on range
                    if (CheckDate(Convert.ToDateTime(txtDateTo.Text)) == 0)
                    {
                        haserror = true;
                        lbl1.Text = "* Date did not match in the school year.";
                    }
                }
                else
                {
                    //not a valid date
                    haserror = true;
                    lbl1.Text = "* Please enter a valid date.";
                }
            }
               
            return haserror;
        }


        //function for checking date
        public int CheckDate(DateTime date)
        {
            int x = 0;
            schoolyear = lblSchoolYear.Text;
            Debug.WriteLine(schoolyear);
            int year = date.Year;
            int startyear = Convert.ToInt32(schoolyear.Substring(0, 4));
            int endyear = Convert.ToInt32(schoolyear.Substring(5, 4));
            if (year > endyear || year < startyear)
               x = 0;
            else
                x = 1;

            return x;
        }

        //Triggers when the Save Button is clicked
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            //error handler
            bool haserror;
            DateTime DateFrom = DateTime.Now, DateTo = DateTime.Now;

            if (ValidateForms() == false)
            {
                //set to false
                haserror = false;

                //check date from and date to
                if (Convert.ToDateTime(txtDateFrom.Text) >= (Convert.ToDateTime(txtDateTo.Text)))
                {
                    haserror = true;
                    lbl0.Text = "* Please check your dates.";
                }

                //instantiate new list
                QuarterList = new List<Constructors.Quarter>(cls.getQuarter());

                if (haserror == false)
                {
                    //validation for 1st quarter
                    if (Request.QueryString["qtr"] == "1st")
                    {
                        //loop through the list
                        QuarterList.ForEach(q =>
                        {
                            //get the date of 4th quarter
                            if (q.Quarters == "4th" && q.isCurrentSY == "YES")
                            {
                                DateTo = Convert.ToDateTime(q.DateTo);
                            }

                            //get the date of 2nd quarter
                            if (q.Quarters == "2nd" && q.isCurrentSY == "YES")
                            {
                                DateFrom = Convert.ToDateTime(q.DateFrom);
                            }
                        });

                        Debug.WriteLine(DateTo + " >= " + txtDateFrom.Text);
                        //check date from if overlaps
                        if (DateTo <= Convert.ToDateTime(txtDateFrom.Text))
                        {
                            haserror = true;
                            lbl0.Text = "* Date entered overlaps. Please check the dates of 4th Quarter.";
                        }

                        Debug.WriteLine(DateFrom + " >= " + txtDateTo.Text);
                        //check date to if overlaps
                        if (DateFrom <= (Convert.ToDateTime(txtDateTo.Text)))
                        {
                            haserror = true;
                            lbl1.Text = "* Date entered overlaps. Please check the dates of 2nd Quarter.";
                        }
                    }

                    //validation for 2nd quarter
                    if (Request.QueryString["qtr"] == "2nd")
                    {
                        //loop through the list
                        QuarterList.ForEach(q =>
                        {
                            //get the date of 1st quarter
                            if (q.Quarters == "1st" && q.isCurrentSY == "YES")
                            {
                                DateTo = Convert.ToDateTime(q.DateTo);
                            }

                            //get the date of 3rd quarter
                            if (q.Quarters == "3rd" && q.isCurrentSY == "YES")
                            {
                                DateFrom = Convert.ToDateTime(q.DateFrom);
                            }
                        });

                        Debug.WriteLine(DateTo + " >= " + txtDateFrom.Text);
                        //check date from if overlaps
                        if (DateTo >= Convert.ToDateTime(txtDateFrom.Text))
                        {
                            haserror = true;
                            lbl0.Text = "* Date entered overlaps. Please check the dates of 1st Quarter.";
                        }

                        Debug.WriteLine(DateFrom + " <= " + txtDateTo.Text);
                        //check date to if overlaps
                        if (DateFrom <= (Convert.ToDateTime(txtDateTo.Text)))
                        {
                            haserror = true;
                            lbl1.Text = "* Date entered overlaps. Please check the dates of 3rd Quarter.";
                        }
                    }

                    //validation for 3rd quarter
                    if (Request.QueryString["qtr"] == "3rd")
                    {
                        //loop through the list
                        QuarterList.ForEach(q =>
                        {
                            //get the date of 2nd quarter
                            if (q.Quarters == "2nd" && q.isCurrentSY == "YES")
                            {
                                DateTo = Convert.ToDateTime(q.DateTo);
                            }

                            //get the date of 4th quarter
                            if (q.Quarters == "4th" && q.isCurrentSY == "YES")
                            {
                                DateFrom = Convert.ToDateTime(q.DateFrom);
                            }
                        });

                        Debug.WriteLine(DateTo + " >= " + txtDateFrom.Text);
                        //check date from if overlaps
                        if (DateTo >= Convert.ToDateTime(txtDateFrom.Text))
                        {
                            haserror = true;
                            lbl0.Text = "* Date entered overlaps. Please check the dates of 2nd Quarter.";
                        }

                        Debug.WriteLine(DateFrom + " <= " + txtDateTo.Text);
                        //check date to if overlaps
                        if (DateFrom <= (Convert.ToDateTime(txtDateTo.Text)))
                        {
                            haserror = true;
                            lbl1.Text = "* Date entered overlaps. Please check the dates of 4th Quarter.";
                        }
                    }

                    //validation for 4th quarter
                    if (Request.QueryString["qtr"] == "4th")
                    {
                        //loop through the list
                        QuarterList.ForEach(q =>
                        {
                            //get the date of 3rd quarter
                            if (q.Quarters == "3rd" && q.isCurrentSY == "YES")
                            {
                                DateTo = Convert.ToDateTime(q.DateTo);
                            }

                            //get the date of 1st quarter
                            if (q.Quarters == "1st" && q.isCurrentSY == "YES")
                            {
                                DateFrom = Convert.ToDateTime(q.DateFrom);
                            }
                        });

                        Debug.WriteLine(DateTo + " >= " + txtDateFrom.Text);
                        //check date from if overlaps
                        if (DateTo >= Convert.ToDateTime(txtDateFrom.Text))
                        {
                            haserror = true;
                            lbl0.Text = "* Date entered overlaps. Please check the dates of 3rd Quarter.";
                        }

                        Debug.WriteLine(DateFrom + " <= " + txtDateTo.Text);
                        //check date to if overlaps
                        if (DateFrom >= (Convert.ToDateTime(txtDateTo.Text)))
                        {
                            haserror = true;
                            lbl1.Text = "* Date entered overlaps. Please check the dates of 1st Quarter.";
                        }
                    }
                }
                //check if has no error
                if (haserror == false)
                {
                    string sql = "UPDATE [Quarter] SET DateFrom = '" + txtDateFrom.Text + "', DateTo = '" + txtDateTo.Text + "', LastUpdateDate=getdate(), LastUpdateUser='" + LUser.Username + "' WHERE [Quarter]='" + Request.QueryString["qtr"] + "' and SchoolYear='" + lblSchoolYear.Text + "'";
                    cls.ExecuteNonQuery(sql);
                    Session["Quarter"] = cls.CurrentQuarter();
                    Response.Write("<script>alert('Quarter has been updated successfully'); window.location='" + ResolveUrl(DefaultForms.frm_quarter_maintenance) + "'</script>");
                }
            }
        }
    }
}
