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



namespace PAOnlineAssessment.registration
{
    public partial class signup_parent : System.Web.UI.Page
    {
        Collections cls = new Collections();
        List<Constructors.Levels> Levels;
        List<Constructors.ParentAccounts> ParentList;
        List<Constructors.Sections> Sections;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                AddTemplateValue();
            }

        }

        void AddTemplateValue()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Count");
            dt.Columns.Add("Firstname");
            dt.Columns.Add("Lastname");
            dt.Columns.Add("LevelID");
            dt.Columns.Add("SectionID");
            dt.Columns.Add("Filename");
            int count = dt.Rows.Count + 1;
            dt.Rows.Add(count, "", "", "", "", count);
            Session["ChildNumber"] = dt;
            BindData();
        }

        void BindData()
        {
            gvChildInfo.DataSource = Session["ChildNumber"];
            gvChildInfo.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (this.isRequiredFieldsOk())
            {
                string sql = "Insert Into [User](UserGroupID, Username, Password, Firstname, Lastname, Status, UserCreated, DateCreated, UpdateUser, UpdateDate, EmailAddress)Values('3', '" + txtUsername.Text + "', '" + txtPassword.Text + "', '" + txtFirstName.Text + "', '" + txtLastName.Text + "','D', '" + txtUsername.Text + "', getdate(), '" + txtUsername.Text + "',getdate(),'" + txtEmailAddress.Text + "')";
                Debug.WriteLine(sql);
                if (cls.ExecuteNonQuery(sql) == 1)
                {
                    int UserID = Convert.ToInt32(cls.ExecuteScalar("Select UserID from [User] Where Username='" + txtUsername.Text + "'"));
                    //sql = "Insert into [Parent] (ParentUserID, ChildFirstname, ChildLastname, YearLevel, Section)Values(" + UserID + ", '" + txtSFirstName.Text + "', '" + txtSLastName.Text + "', " + cboLevel.SelectedValue + ", " + cboSection.SelectedValue + ")";
                    //Debug.WriteLine(sql);
                    //if (cls.ExecuteNonQuery(sql) == 1)
                    //{
                    //    Validator.AlertBack("Your registration has been saved. Please wait for your account approval","../index.aspx");
                    //}

                    SaveStudents(UserID);
                }
            }
        }

        void SaveStudents(int UserID)
        {
            int x = 0;

            for (int i = 0; i < gvChildInfo.Rows.Count; i++)
            {
                TextBox txtSLastName = gvChildInfo.Rows[i].FindControl("txtLastname1") as TextBox;
                TextBox txtSFirstName = gvChildInfo.Rows[i].FindControl("txtFirstname1") as TextBox;
                DropDownList cboLevel = gvChildInfo.Rows[i].FindControl("cboLevel1") as DropDownList;
                string sql = "";
                sql = "Insert into [Parent] (ParentUserID, ChildFirstname, ChildLastname, YearLevel, Section)Values(" + UserID + ", '" + txtSFirstName.Text + "', '" + txtSLastName.Text + "', " + cboLevel.SelectedValue + ",0)";

                if (cls.ExecuteNonQuery(sql) == 1)
                {
                    //Validator.AlertBack("Your registration has been saved. Please wait for your account approval", "../index.aspx");
                    Debug.WriteLine(txtSFirstName.Text + " " + txtSLastName.Text + " has been saved successfully ");
                }
                else
                {
                    x = 1;
                }
            }
            if (x == 1)
            {
                Validator.AlertBack("Your registration has not been saved. Database Error", "../index.aspx");
            }
            else
            {
                Validator.AlertBack("Your registration has been saved. Please wait for your account approval", "../index.aspx");
            }
        }


        bool isRequiredFieldsOk()
        {
            bool x = true;
            if (Validator.isEmpty(txtFirstName.Text))
            {
                vlFirstName.Text = "* First Name is Required";
                x = false;
            }
            else
            {
                vlFirstName.Text = "* ";
            }
            if (Validator.isEmpty(txtLastName.Text))
            {
                vlLastName.Text = "* Last Name is Required";
                x = false;
            }
            else
            {
                vlLastName.Text = "* ";
            }
            //if (Validator.isEmpty(txtSFirstName.Text))
            //{
            //    vlSFirstName.Text = "* Student's First Name is Required";
            //    x = false;
            //}
            //else
            //{
            //    vlUsername.Text = "* ";
            //}

            //if (Validator.isEmpty(txtSFirstName.Text))
            //{
            //    vlSFirstName.Text = "* Student's First Name is Required";
            //    x = false;
            //}
            //else
            //{
            //    vlSFirstName.Text = "* ";
            //}
            //if (Validator.isEmpty(txtSLastName.Text))
            //{
            //    vlSLastName.Text = "* Student's Last Name is Required";
            //    x = false;
            //}
            //else
            //{
            //    vlSLastName.Text = "* ";
            //}
            if (Validator.isEmpty(txtUsername.Text))
            {
                vlUsername.Text = "* Username is Required.";
                x = false;
            }
            else
            {
                vlUsername.Text = "* ";
            }

            if (Validator.isEmpty(txtPassword.Text) || Validator.isEmpty(txtConfirmPassword.Text))
            {
                vlPassword.Text = "* Password is Required.";
                x = false;
            }
            else if (Validator.isNotEqual(txtPassword.Text, txtConfirmPassword.Text))
            {
                vlPassword.Text = "* Inputed Password did not match. Please Try Again.";
                x = false;
            }
            else
            {
                vlPassword.Text = "* ";
            }
            if (Validator.isEmpty(txtEmailAddress.Text))
            {
                vlEmailAddress.Text = "* Email Address is Required.";
                x = false;
            }
            else if (Validator.isEmailNotValid(txtEmailAddress.Text))
            {
                vlEmailAddress.Text = "* Invalid Email Format. Please Try Again.";
                x = false;
            }
            else if (EmailExists(txtEmailAddress.Text))
            {
                vlEmailAddress.Text = "* Email Address already used by other parents.";
                x = false;
            }
            else
            {
                vlEmailAddress.Text = "* ";
            }
            //if (cboLevel.SelectedValue.ToString() == "0")
            //{
            //    vlLevel.Text = "* Student's Level is Required";
            //    x = false;
            //}
            //else
            //{
            //    vlLevel.Text = "* ";
            //}
            //if (cboSection.SelectedValue.ToString() == "0")
            //{
            //    vlSection.Text = "* Student's Section is Required";
            //    x = false;
            //}
            //else
            //{
            //    vlSection.Text = "* ";
            //}

            //check if captcha is enabled
            if (UpdatePanel2.Visible == true)
            {
                if (upCaptcha.Visible == true)
                {
                    //Validate if Textbox is Null
                    if (Validator.isEmpty(txtCaptcha.Text))
                    {
                        vlCaptcha.Text = "* Please enter the text shown in the Image.";
                        x = false;
                    }
                    else
                    {
                        //Validate Entered Text
                        upCaptcha.ValidateCaptcha(txtCaptcha.Text.ToUpper());
                        //Checks if Entered Text matches the Generated Captcha
                        if (upCaptcha.UserValidated == false)
                        {
                            vlCaptcha.Text = "* The text you entered does not match the text shown in the image.";
                            x = false;
                        }
                        else
                        {
                            vlCaptcha.Text = "*";
                        }
                    }
                }
            }

            //check if has child's name entered
            if (gvChildInfo.Rows.Count > 0)
            {
                int z = 0;
                int ctr = 0;
                for (int i = 0; i < gvChildInfo.Rows.Count; i++)
                {
                    TextBox txtLname = gvChildInfo.Rows[i].FindControl("txtLastname1") as TextBox;
                    TextBox txtFname = gvChildInfo.Rows[i].FindControl("txtFirstname1") as TextBox;
                    DropDownList cboLevel1 = gvChildInfo.Rows[i].FindControl("cboLevel1") as DropDownList;


                    if (string.IsNullOrEmpty(txtFname.Text)) { txtFname.BackColor = System.Drawing.Color.LightGoldenrodYellow; z = 1; } else txtFname.BackColor = System.Drawing.Color.White;
                    if (string.IsNullOrEmpty(txtLname.Text)) { txtLname.BackColor = System.Drawing.Color.LightGoldenrodYellow; z = 1; } else txtLname.BackColor = System.Drawing.Color.White;
                    if (cboLevel1.SelectedValue == "0") { cboLevel1.BackColor = System.Drawing.Color.LightGoldenrodYellow; z = 1; } else cboLevel1.BackColor = System.Drawing.Color.White;
                    
                }
                if (ctr == gvChildInfo.Rows.Count)
                {
                    z = 1;
                }


                if (z == 1)
                {
                    vlStudent.Text = "Please enter all of your children that is registered in Pace Online Assessment.";
                    x = false;
                }
            }
            else
            {
                vlStudent.Text = "*";
            }

            return x;

        }

        protected void gvChildInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < gvChildInfo.Rows.Count; i++)
            {
                TextBox txtLname = gvChildInfo.Rows[i].FindControl("txtLastname1") as TextBox;
                TextBox txtFname = gvChildInfo.Rows[i].FindControl("txtFirstname1") as TextBox;
                DropDownList cboLevel1 = gvChildInfo.Rows[i].FindControl("cboLevel1") as DropDownList;
                ImageButton btnStatus = gvChildInfo.Rows[i].FindControl("btnStatus") as ImageButton;
                Label lblLevel = gvChildInfo.Rows[i].FindControl("lblLevel") as Label;
                LinkButton btnRemove = gvChildInfo.Rows[i].FindControl("btnRemove") as LinkButton;
                if (gvChildInfo.Rows.Count < 2)
                {
                    btnRemove.Enabled = false;
                }

                lblLevel.Visible = false;

                cboLevel1.Items.Clear();
                cboLevel1.Items.Add(new ListItem("Select a Level", "0"));
                Levels = new List<Constructors.Levels>(cls.GetLevels());
                Levels.ForEach(l =>
                {
                    if (l.Status == "A")
                    {
                        cboLevel1.Items.Add(new ListItem(l.LevelDescription, l.LevelID.ToString()));
                    }
                });

            }
        }

        protected void cboLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboLevel1 = (DropDownList)sender;
            int LevelID = Convert.ToInt32(cboLevel1.SelectedValue);
            int RowNumber = Convert.ToInt32(cboLevel1.Attributes["RowCount"]);
            DropDownList cboSection1 = (DropDownList)gvChildInfo.Rows[RowNumber - 1].FindControl("cboSection1");
            cboSection1.Items.Clear();
            cboSection1.Items.Add(new ListItem("Select a Section", "0"));
            Sections = new List<Constructors.Sections>(cls.GetSection());
            Sections.ForEach(s =>
            {
                if (s.LevelID == LevelID)
                {
                    cboSection1.Items.Add(new ListItem(s.SectionDescription, s.SectionID.ToString()));
                }
            });
        }

        protected void txtFirstname1_TextChanged(object sender, EventArgs e)
        {
            TextBox txtFname = sender as TextBox;
            CheckCompleteRowData(Convert.ToInt32(txtFname.Attributes["RowCount"]));
        }

        void CheckCompleteRowData(int Rownum)
        {
            TextBox txtLname = gvChildInfo.Rows[Rownum - 1].FindControl("txtLastname1") as TextBox;
            TextBox txtFname = gvChildInfo.Rows[Rownum - 1].FindControl("txtFirstname1") as TextBox;
            DropDownList cboSection1 = gvChildInfo.Rows[Rownum - 1].FindControl("cboSection1") as DropDownList;
            DropDownList cboLevel1 = gvChildInfo.Rows[Rownum - 1].FindControl("cboLevel1") as DropDownList;
            ImageButton btnStatus = gvChildInfo.Rows[Rownum - 1].FindControl("btnStatus") as ImageButton;


            int x = 0;
            if (txtFname.Text == "" || txtLname.Text == "" || cboSection1.SelectedValue == "0" || cboSection1.SelectedValue.Trim() == "")
            {
                x = 1;
            }

            if (x == 1)
            {

            }
            else
            {
                if (CheckLastRow())
                {

                    DataTable dt = Session["ChildNumber"] as DataTable;

                    int count = dt.Rows.Count + 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            Label lblCount = gvChildInfo.Rows[i].FindControl("lblCount") as Label;
                            TextBox txtLname1 = gvChildInfo.Rows[i].FindControl("txtLastname1") as TextBox;
                            TextBox txtFname1 = gvChildInfo.Rows[i].FindControl("txtFirstname1") as TextBox;
                            DropDownList cboSection2 = gvChildInfo.Rows[i].FindControl("cboSection1") as DropDownList;
                            DropDownList cboLevel2 = gvChildInfo.Rows[i].FindControl("cboLevel1") as DropDownList;
                            ImageButton btnStatus1 = gvChildInfo.Rows[i].FindControl("btnStatus") as ImageButton;

                            if (dt.Rows[i][0].ToString() == lblCount.Text)
                            {
                                dt.Rows[i]["Firstname"] = txtFname1.Text;
                                dt.Rows[i]["Lastname"] = txtLname1.Text;
                                dt.Rows[i]["LevelID"] = cboLevel2.SelectedValue;
                                dt.Rows[i]["SectionID"] = cboSection2.SelectedValue;
                            }
                        }
                        catch
                        {
                            Debug.WriteLine(i);
                        }
                    }
                    dt.Rows.Add(count, "", "", "", "", count);
                    Session["ChildNumber"] = dt;
                    BindData();
                }
            }
        }

        bool CheckLastRow()
        {
            bool value = true;
            int i = gvChildInfo.Rows.Count - 1;

            TextBox txtLname = gvChildInfo.Rows[i].FindControl("txtLastname1") as TextBox;
            TextBox txtFname = gvChildInfo.Rows[i].FindControl("txtFirstname1") as TextBox;
            DropDownList cboSection1 = gvChildInfo.Rows[i].FindControl("cboSection1") as DropDownList;
            DropDownList cboLevel1 = gvChildInfo.Rows[i].FindControl("cboLevel1") as DropDownList;
            ImageButton btnStatus = gvChildInfo.Rows[i].FindControl("btnStatus") as ImageButton;


            if (txtFname.Text == "" || txtLname.Text == "" || cboSection1.SelectedValue == "0" || cboSection1.SelectedValue.Trim() == "")
            {
                value = false;
            }



            return value;
        }
        protected void txtLastname1_TextChanged(object sender, EventArgs e)
        {
            TextBox txtLname = sender as TextBox;
            CheckCompleteRowData(Convert.ToInt32(txtLname.Attributes["RowCount"]));
        }
        protected void cboSection1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboSection1 = sender as DropDownList;
            CheckCompleteRowData(Convert.ToInt32(cboSection1.Attributes["RowCount"]));
        }

        bool EmailExists(string field)
        {
            bool value = false;
            ParentList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
            ParentList.ForEach(pl =>
            {
                if (pl.EmailAddress == field && pl.ParentID != 0)
                {
                    value = true;
                }
            });
            return value;
        }

        bool UsernameExists(string field)
        {
            bool value = false;
            ParentList = new List<Constructors.ParentAccounts>(cls.GetParentAcount());
            ParentList.ForEach(pl =>
            {
                if (pl.EmailAddress == field && pl.ParentID != 0)
                {
                    value = true;
                }
            });
            return value;
        }

        void check_row_count()
        {
            LinkButton btnRemove = gvChildInfo.Rows[0].FindControl("btnRemove") as LinkButton;
            if (gvChildInfo.Rows.Count < 2)
            {
                btnRemove.Enabled = false;

            }
            else
            {
                btnRemove.Enabled = true;

            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            DataTable dt = Session["ChildNumber"] as DataTable;

            int count = dt.Rows.Count + 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    Label lblCount = gvChildInfo.Rows[i].FindControl("lblCount") as Label;
                    TextBox txtLname1 = gvChildInfo.Rows[i].FindControl("txtLastname1") as TextBox;
                    TextBox txtFname1 = gvChildInfo.Rows[i].FindControl("txtFirstname1") as TextBox;
                    DropDownList cboSection2 = gvChildInfo.Rows[i].FindControl("cboSection1") as DropDownList;
                    DropDownList cboLevel2 = gvChildInfo.Rows[i].FindControl("cboLevel1") as DropDownList;
                    ImageButton btnStatus1 = gvChildInfo.Rows[i].FindControl("btnStatus") as ImageButton;

                    if (dt.Rows[i][0].ToString() == lblCount.Text)
                    {
                        dt.Rows[i]["Firstname"] = txtFname1.Text;
                        dt.Rows[i]["Lastname"] = txtLname1.Text;
                        dt.Rows[i]["LevelID"] = cboLevel2.SelectedValue;
                        dt.Rows[i]["SectionID"] = cboSection2.SelectedValue;
                    }
                }
                catch
                {
                    Debug.WriteLine(i);
                }
            }
            dt.Rows.Add(count, "", "", "", "", count);
            Session["ChildNumber"] = dt;
            BindData();
            check_row_count();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (gvChildInfo.Rows.Count > 1)
            {
                LinkButton btnRemove = sender as LinkButton;
                int x = Convert.ToInt32(btnRemove.Attributes["RowCount"]);
                DataTable dt = Session["ChildNumber"] as DataTable;
                dt.Rows[x - 1].Delete();
                Session["ChildNumber"] = dt;
                BindData();
                check_row_count();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            upCaptcha.ValidateCaptcha(txtCaptcha.Text);
            if (upCaptcha.UserValidated == false)
            {
                vlCaptcha.Text = "* invalid captcha";
            }
            else
            {
                vlCaptcha.Text = "* ";
            }
        }
    }
}
