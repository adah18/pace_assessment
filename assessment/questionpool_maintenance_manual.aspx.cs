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


namespace PAOnlineAssessment.assessment
{
    public partial class questionpool_maintenance_manual : System.Web.UI.Page
    {

        //Instantiate new Collections Class
        Collections cls = new Collections();
        //Declare a List containing RegistrationTerm Class
        List<Constructors.RegistrationTerm> RegistrationTermList;
        //Declare a List containing List of Subjects
        List<Constructors.Subject> SubjectList;
        //Declare List containing List of Topics
        List<Constructors.Topics> TopicList = new List<Constructors.Topics>(new Collections().GetTopic());
        //Declare a LoginUser Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();

        int ctr = 0;
        #region "controls"
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

                if (Validator.CanbeAccess("9", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed");

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
                //Check the selected value of question type 0 means nothing chosen.
                ShowChoices(0);
                //Create Column For Data Table
                CreateColumns();
                //Declare Session For Counting All the Question
                Session["counter"] = 0;
                //Load All Levels
                LoadLevels();
                //Load All Subject for Selected Level
                LoadSubjects();

                LoadTopic();

                imgPreView.Enabled = false;
                imgRemove.Enabled = false;
                imgRemove.ImageUrl = "~/images/icons/action_stop_disabled.gif";
                imgPreView.ImageUrl = "~/images/icons/page_find_disabled.gif";

                ddlQuarter.SelectedValue = Session["Quarter"].ToString();
            }

            LoadSiteMap();
        }


        void LoadTopic()
        {
            cboTopic.Items.Clear();
            cboTopic.Items.Add(new ListItem("--- Select Topic ---", "0"));
            TopicList.ForEach(tl =>
            {
                if (tl.Status == "A" && tl.SubjectID.ToString() == cboSubjectList.SelectedValue && tl.LevelID.ToString() == cboGradeLevel.SelectedValue)
                {
                    cboTopic.Items.Add(new ListItem(tl.Description, tl.TopicID.ToString()));
                }
            });
        }

        protected void lnkSave0_Click(object sender, EventArgs e)
        {
            if (validator())
            {
                //create a counter for question added
                ctr = Convert.ToInt32(Session["counter"]);
                //increment variable ctr by 1 for every question added to the gridview
                ctr += 1;
                //pass the value to the session
                Session["counter"] = ctr.ToString();
                //call void for adding questions
                AddQuestion(ctr);
                txtQuestion.Text = "";
                txtCorrect.Text = "";
                txtRemarks.Text = "";
                ChoicesFields();
                //display all choices for the selected type of question
                ShowChoices(Convert.ToInt32(cboType.SelectedValue));
                vlSubjectList0.Text = "* ";
                GridViewPanel.Visible = true;

                lblFileName.Text = "";
                imgPreView.Enabled = false;
                imgRemove.Enabled = false;
                imgRemove.ImageUrl = "~/images/icons/action_stop_disabled.gif";
                imgPreView.ImageUrl = "~/images/icons/page_find_disabled.gif";
                loadPicture.ImageUrl = "~/images/dashboard_icons/attachment.png";
            }
            else
            {
                vlSubjectList0.Text = "* Please complete all the fields in this part";
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            //check first i fall the required fields is met
            if (ValidateFields())
            {
                DataTable dt = (DataTable)Session["Questions"];
                int SuccessCount = 0;
                LUser = (LoginUser)Session["LoggedUser"];
                foreach (DataRow dRow in dt.Rows)
                {
                    //Insert the added question to database
                    string qry = "INSERT INTO [PaceAssessment].dbo.[QuestionPool](SubjectID, LevelID, TopicID, Question, CorrectAnswer, CorrectAnswerRemark, Choice1, Choice1Remark, Choice2, Choice2Remark, Choice3, Choice3Remark, Choice4, Choice4Remark, Status, UserCreated, DateCreated, LastUpdateUser, LastUpdateDate, Quarter) " +
                          "VALUES('" + Validator.Finalize(cboSubjectList.SelectedValue) + "', '" + Validator.Finalize(cboGradeLevel.SelectedValue) + "', " + cboTopic.SelectedValue + " ,'" + Validator.Finalize(dRow["Question"].ToString()) + "', '" + Validator.Finalize(dRow["CorrectAnswer"].ToString()) + "', '" + Validator.Finalize(dRow["CorrectAnswerRemark"].ToString()) + "', '" + Validator.Finalize(dRow["Choice1"].ToString()) + "','" + Validator.Finalize(dRow["Choice1Remark"].ToString()) + "','" + Validator.Finalize(dRow["Choice2"].ToString()) + "','" + Validator.Finalize(dRow["Choice2Remark"].ToString()) + "','" + Validator.Finalize(dRow["Choice3"].ToString()) + "','" + Validator.Finalize(dRow["Choice3Remark"].ToString()) + "','" + Validator.Finalize(dRow["Choice4"].ToString()) + "','" + Validator.Finalize(dRow["Choice4Remark"].ToString()) + "','A','" + Validator.Finalize(LUser.Username) + "',getdate(),'" + Validator.Finalize(LUser.Username) + "',getdate(),'" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "')";
                    SuccessCount = SuccessCount + cls.ExecuteNonQuery(qry);
                    if (dRow["ImageFileName"].ToString() != "")
                    {
                        string QuestionPoolID = cls.ExecuteScalar("Select MAX(QuestionPoolID) from PaceAssessment.dbo.QuestionPool where SubjectID='" + cboSubjectList.SelectedValue + "' and LevelID='" + cboGradeLevel.SelectedValue + "' and Question='" + dRow["Question"].ToString() + "' and CorrectAnswer='" + dRow["CorrectAnswer"].ToString() + "' and CorrectAnswerRemark='" + dRow["CorrectAnswerRemark"].ToString() + "'");
                        SaveFileFolder(QuestionPoolID, dRow["ImageFileName"].ToString(), new FileInfo(dRow["ImageFileName"].ToString()).Extension);
                        qry = "INSERT INTO PaceAssessment.dbo.Image (QuestionPoolID,FileName) values ('" + QuestionPoolID + "','" + QuestionPoolID + new FileInfo(dRow["ImageFileName"].ToString()).Extension + "')";
                        cls.ExecuteNonQuery(qry);
                    };
                }
                if (SuccessCount > 0)
                {
                    Response.Write("<script>alert('Questions have been saved successfully.'); window.location='"+ResolveUrl(DefaultForms.frm_questionpool_maintenance_main)+"'</script>");
                }
                else
                {
                    Response.Write("<script>alert('Action cannot continue. Please Review your entry')</script>");
                }
            }
        }

        void SaveFileFolder(string QuestionPoolID, string ImageFileName, string FileExtension)
        {
            //check if file exist
            if (System.IO.File.Exists(Server.MapPath(Defaults.FolderPath + QuestionPoolID + FileExtension)))
            {
                //delete the current file
                File.Delete(Server.MapPath(Defaults.FolderPath + QuestionPoolID + FileExtension));
                System.IO.File.Copy(Server.MapPath(Defaults.TempFolderPath + ImageFileName), Server.MapPath(Defaults.FolderPath + QuestionPoolID + FileExtension));

                //delete the picture in the temp folder
                File.Delete(Server.MapPath(Defaults.TempFolderPath + ImageFileName));
            }
            else
            {
                //save the picture
                System.IO.File.Copy(Server.MapPath(Defaults.TempFolderPath + ImageFileName), Server.MapPath(Defaults.FolderPath + QuestionPoolID + FileExtension));
                //delete the picture in the temp folder
                File.Delete(Server.MapPath(Defaults.TempFolderPath + ImageFileName));
            }
        }

        protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //display all choices for the selected type of question
            ShowChoices(Convert.ToInt32(cboType.SelectedValue));
        }

        //Cancel Button
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_admin_dashboard));
        }

        #endregion
        #region "Functions"
        //Validate the question type
        void ShowChoices(int value)
        {
            //If nothing is selected
            Label4.Text = cboType.SelectedValue;
            
            if (value == 0)
            {
                txtCorrect.Visible = true;
                cboCorrect.Visible = false;
                Session["QuestionType"] = "none";
                ChoicesFields();
            }
            //If True or False
            else if (value == 1)
            {
                Session["QuestionType"] = "True or False";
                ChoicesFields();
                Label6.Visible = true;
                //Label7.Visible = true;
                if (cboCorrect.SelectedItem.Text.ToLower() == "true")
                {
                    txtChoice1.Text = "False";
                }
                else
                {
                    txtChoice1.Text = "True";
                }
               
                txtChoice2.Text = "True/False";
                txtChoice1.Visible = true;
                //txtChoice2.Visible = true;
                txtChoice1.ReadOnly = true;
                txtChoice2.ReadOnly = true;
                txtRemark1.Visible = true;
                //txtRemark2.Visible = true;
                txtCorrect.Visible = false;
                cboCorrect.Visible = true;
            
            }
             //if Multiple Choices
            else if (value == 2)
            {
                Session["QuestionType"] = "Multiple Choices";
                ChoicesFields();
                Label6.Visible = true;
                Label7.Visible = true;
                Label8.Visible = true;
                //Label9.Visible = true;
                txtChoice1.Visible = true;
                txtChoice2.Visible = true;
                txtChoice3.Visible = true;
               // txtChoice4.Visible = true;
                txtRemark1.Visible = true;
                txtRemark2.Visible = true;
                txtRemark3.Visible = true;
                //txtRemark4.Visible = true;
                txtCorrect.Visible = true;
                cboCorrect.Visible = false;
            }
                //If Fill in the blank
            else if (value == 3)
            {
                cboCorrect.Visible = false;
                txtCorrect.Visible = true;
                Session["QuestionType"] = "Blank";
                ChoicesFields();
            }
        }
        //Clear all fields
        void ChoicesFields()
        {
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            Label9.Visible = false;
            txtChoice1.Text = "";
            txtChoice2.Text = "";
            txtChoice3.Text = "";
            txtChoice4.Text = "";
            txtChoice1.Visible = false;
            txtChoice2.Visible = false;
            txtChoice3.Visible = false;
            txtChoice4.Visible = false;
            txtChoice1.ReadOnly = false;
            txtChoice2.ReadOnly = false;
            txtChoice3.ReadOnly = false;
            txtChoice4.ReadOnly = false;
            txtRemark1.Text = "";
            txtRemark2.Text = "";
            txtRemark3.Text = "";
            txtRemark4.Text = "";
            txtRemark1.Visible = false;
            txtRemark2.Visible = false;
            txtRemark3.Visible = false;
            txtRemark4.Visible = false;
        }
        //Add question to the data table
        void AddQuestion(int count)
        {
            string CorrectRemark = txtRemarks.Text;
            if (CorrectRemark.Trim().Length < 1)
            {
                CorrectRemark = "You got the correct answer";
            }

            DataTable dt = (DataTable)Session["Questions"];
            if (cboType.SelectedIndex == 1) 
            {
                //if the question is true of false

                if (cboCorrect.SelectedItem.Text.ToLower() == "True")
                {
                    dt.Rows.Add(count, txtQuestion.Text, cboCorrect.SelectedItem.Text, CorrectRemark, "True", CorrectRemark, "False", IncorrectRemark(txtRemark1.Text), txtChoice3.Text, txtRemark3.Text, txtChoice4.Text, txtRemark4.Text, lblFileName.Text);
                }
                else
                {
                    dt.Rows.Add(count, txtQuestion.Text, cboCorrect.SelectedItem.Text, CorrectRemark, "True", IncorrectRemark(txtRemark1.Text), "False", CorrectRemark, txtChoice3.Text, txtRemark3.Text, txtChoice4.Text, txtRemark4.Text, lblFileName.Text);
                }

            }
            else
            {
                  //if the question is a multiple choice or fill in the blank
                if (Validator.isEmpty(txtChoice1.Text) && Validator.isEmpty(txtChoice2.Text) && Validator.isEmpty(txtChoice3.Text))
                {
                    dt.Rows.Add(count, txtQuestion.Text, txtCorrect.Text, CorrectRemark, txtChoice1.Text, txtRemark1.Text, txtChoice2.Text, txtRemark2.Text, txtChoice3.Text, txtRemark3.Text, txtChoice4.Text, txtRemark4.Text, lblFileName.Text);                
                }
                else
                {
                    Random x = new Random();
                    int pos = x.Next(1, 4);
                    if (pos == 1)
                    {
                        dt.Rows.Add(count, txtQuestion.Text, txtCorrect.Text, CorrectRemark, txtChoice1.Text, IncorrectRemark(txtRemark1.Text), txtChoice2.Text, IncorrectRemark(txtRemark2.Text), txtChoice3.Text, IncorrectRemark(txtRemark3.Text), txtCorrect.Text, txtRemarks.Text, lblFileName.Text);                                    
                    }
                    else if (pos == 2)
                    {
                        dt.Rows.Add(count, txtQuestion.Text, txtCorrect.Text, CorrectRemark, txtChoice1.Text, IncorrectRemark(txtRemark1.Text), txtChoice2.Text, IncorrectRemark(txtRemark2.Text), txtCorrect.Text, txtRemarks.Text, txtChoice3.Text, IncorrectRemark(txtRemark3.Text), lblFileName.Text);                
                    }
                    else if(pos == 3)
                    {
                        dt.Rows.Add(count, txtQuestion.Text, txtCorrect.Text, CorrectRemark, txtChoice1.Text, IncorrectRemark(txtRemark1.Text),txtCorrect.Text, CorrectRemark, txtChoice2.Text, IncorrectRemark(txtRemark2.Text), txtChoice3.Text, IncorrectRemark(txtRemark3.Text), lblFileName.Text);     
                    }
                    else if (pos == 4)
                    {
                        dt.Rows.Add(count, txtQuestion.Text, txtCorrect.Text, CorrectRemark,txtCorrect.Text,CorrectRemark, txtChoice1.Text, IncorrectRemark(txtRemark1.Text), txtChoice2.Text, IncorrectRemark(txtRemark2.Text), txtChoice3.Text, IncorrectRemark(txtRemark3.Text),lblFileName.Text);     
                    }

                }
            }
            Session["Questions"] = dt;
            grdAddedQuestion.DataSource = Session["Questions"];
            grdAddedQuestion.DataBind();
        }

        //for validating default message if the remarks is empty or not
        string IncorrectRemark(string remark)
        {
            string value = "Your answer is not correct";
            if (remark.Trim().Length > 0)
            {
                value = remark;
            }
            return value;
        }
        //Create Columns for datatable
        void CreateColumns()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowNo"));
            dt.Columns.Add(new DataColumn("Question"));
            dt.Columns.Add(new DataColumn("CorrectAnswer"));
            dt.Columns.Add(new DataColumn("CorrectAnswerRemark"));
            dt.Columns.Add(new DataColumn("Choice1"));
            dt.Columns.Add(new DataColumn("Choice1Remark"));
            dt.Columns.Add(new DataColumn("Choice2"));
            dt.Columns.Add(new DataColumn("Choice2Remark"));
            dt.Columns.Add(new DataColumn("Choice3"));
            dt.Columns.Add(new DataColumn("Choice3Remark"));
            dt.Columns.Add(new DataColumn("Choice4"));
            dt.Columns.Add(new DataColumn("Choice4Remark"));
            dt.Columns.Add(new DataColumn("ImageFileName"));

            Session["Questions"] = dt;

            grdAddedQuestion.DataSource = Session["Questions"];
            grdAddedQuestion.DataBind();
        }
        //checking all the required fields in adding question fiekd
        bool validator()
        {
            bool validator1 = true;

            if (Validator.isDefaultSelected(cboType.SelectedValue)) validator1 = false;
            if (Validator.isEmpty(txtQuestion.Text)) validator1 = false;
           
            //if (Validator.isEmpty(txtRemarks.Text)) validator1 = false;
            if (Validator.isDefaultSelected(cboType.SelectedValue))
            {
                if (Validator.isEmpty(txtCorrect.Text)) validator1 = false;
  
            }

            //True or False
            if (cboType.SelectedIndex == 1)
            {
                //if (Validator.isEmpty(txtRemark1.Text)) validator1 = false;
                if (Validator.isEmpty(txtChoice1.Text)) validator1 = false;
                //if (Validator.isEmpty(txtRemark2.Text)) validator1 = false;
                //if (Validator.isEmpty(txtChoice2.Text)) validator1 = false;
              
            }
                //for the multiple choice question
            else if (cboType.SelectedIndex == 2)
            {
                //if (Validator.isEmpty(txtRemark1.Text)) validator1 = false;
                if (Validator.isEmpty(txtChoice1.Text)) validator1 = false;
                //if (Validator.isEmpty(txtRemark2.Text)) validator1 = false;
                if (Validator.isEmpty(txtChoice2.Text)) validator1 = false;
                //if (Validator.isEmpty(txtRemark3.Text)) validator1 = false;
                if (Validator.isEmpty(txtChoice3.Text)) validator1 = false;
                //if (Validator.isEmpty(txtRemark4.Text)) validator1 = false;
                //if (Validator.isEmpty(txtChoice4.Text)) validator1 = false;
            }
            else if (cboType.SelectedIndex == 3)
            {
                if (Validator.isEmpty(txtCorrect.Text)) 
                    validator1 = false;
            }


            if (fileUpload.HasFile)
            {
                lblNotification.Text = "* Upload the selected image first";
                validator1 = false;
            }
            return validator1;
        }

        //validate required fields
        public bool ValidateFields()
        {
            bool Status = true;

            //Validate Grade Level DropDownList
            if (Validator.isDefaultSelected(cboGradeLevel.SelectedValue))
            {
                vlGradeLevel.Text = "* Please Specify Grade \\ Level";
                Status = false;
            }
            else
            {
                vlGradeLevel.Text = "*";
            }

            //Validate Subject DropDownList
            if (Validator.isDefaultSelected(cboSubjectList.SelectedValue))
            {
                vlSubjectList.Text = "* Please Specify Subject";
                Status = false;
            }
            else
            {
                vlSubjectList.Text = "*";
            }
            //validate if a question has been added.
            int counter = Convert.ToInt32(Session["counter"]);
            if (counter == Convert.ToInt32(0))
            {
                vlSubjectList0.Text = "* Please Add A Question";
                Status = false;
            }
            else
            {
                vlSubjectList0.Text = "* ";
            }
            return Status;
        }
        #endregion
        #region "Loading Levels and subjects"
        public void LoadLevels()
        {
            cboGradeLevel.Items.Clear();
            cboGradeLevel.Items.Add("--- Select Level ---");
            cboGradeLevel.Items[cboGradeLevel.Items.Count - 1].Value = "0";

            //Instantiate new List
            RegistrationTermList = new List<Constructors.RegistrationTerm>(new Collections().getRegistrationTerm());

            //Create a DataTable containing the LevelID and LevelDescription
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LevelID"));
            dt.Columns.Add(new DataColumn("LevelDescription"));

            //Loop through List of Registration Terms
            RegistrationTermList.ForEach(rt =>
            {
                //Add LevelID and LevelDescription to the Datatable
                dt.Rows.Add(rt.LevelID, rt.LevelDescription);
            });

            //Loop through Filtered Duplicate values in the DataTable
            foreach (DataRow drow in dt.DefaultView.ToTable(true, new string[] { "LevelID", "LevelDescription" }).Rows)
            {
                //Add to Grade / Level DropDownList
                cboGradeLevel.Items.Add(drow[1].ToString());
                cboGradeLevel.Items[cboGradeLevel.Items.Count - 1].Value = drow[0].ToString();
            }
        }
        public void LoadSubjects()
        {
            //clear all items that inside the combobox
            cboSubjectList.Items.Clear();
            cboSubjectList.Items.Add("--- Select Subject ---");
            cboSubjectList.Items[cboSubjectList.Items.Count - 1].Value = "0";

            SubjectList = new List<Constructors.Subject>(new Collections().getSubjectList());

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SubjectID"));
            dt.Columns.Add(new DataColumn("SubjectDescription"));

            SubjectList.ForEach(s =>
            {
                if (s.LevelID == Convert.ToInt32(cboGradeLevel.SelectedValue))
                {
                    dt.Rows.Add(s.SubjectID, s.Description);
                }
            });

            foreach (DataRow dRow in dt.DefaultView.ToTable(true, new string[] { "SubjectID", "SubjectDescription" }).Rows)
            {
                Debug.WriteLine("Subjects: " + dRow[1] + dRow[0]);
                cboSubjectList.Items.Add(dRow[1].ToString());
                cboSubjectList.Items[cboSubjectList.Items.Count - 1].Value = dRow[0].ToString();
            }

        }
        //load the subject for the corresspinfing levelevery yje user change subjects
        protected void cboGradeLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadTopic();
        }

        
        #endregion
        #region "GridView Events"
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            grdAddedQuestion.PageIndex = cboPageNumber.SelectedIndex;
            BindLoadedQuestionsToGridView();
        }

        //executed when the Last Page Link button has been clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            grdAddedQuestion.PageIndex = grdAddedQuestion.PageCount;
            BindLoadedQuestionsToGridView();
        }

        //executed when the First Page Linkbutton has been Clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            grdAddedQuestion.PageIndex = 0;
            BindLoadedQuestionsToGridView();
        }
        //executed when the Next Page Linkbutton has been clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (grdAddedQuestion.PageIndex != grdAddedQuestion.PageCount)
            {
                grdAddedQuestion.PageIndex = grdAddedQuestion.PageIndex + 1;
                BindLoadedQuestionsToGridView();
            }
        }

        //executed when the Previous Page Linkbutton has been clicked
        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            if (grdAddedQuestion.PageIndex != 0)
            {
                grdAddedQuestion.PageIndex = grdAddedQuestion.PageIndex - 1;
                BindLoadedQuestionsToGridView();
            }
        }

        //bind the data on a grid view
        public void BindLoadedQuestionsToGridView()
        {
            grdAddedQuestion.DataSource = (DataTable)Session["Questions"];
          
            grdAddedQuestion.DataBind();
        }
        protected void grdAddedQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                lblPageCount.Text = "of " + grdAddedQuestion.PageCount;
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                for (int x = 1; x <= grdAddedQuestion.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = grdAddedQuestion.PageIndex;
            }
        }

        protected void grdAddedQuestion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["Questions"];
            dt.Rows[e.RowIndex].Delete();
            dt.AcceptChanges();
            Session["Questions"] = dt;
            grdAddedQuestion.EditIndex = -1;
            BindLoadedQuestionsToGridView();


            ctr = Convert.ToInt32(Session["counter"]);
            ctr -= 1;
            Session["counter"] = ctr.ToString();

            UpdateRowCount(ctr);
        }
        void UpdateRowCount(int ctr)
        {
            DataTable dt = (DataTable)Session["Questions"];
            for (int x = 1; x <= ctr; x++)
            {
                dt.Rows[x - 1][0] = x;
            }
            dt.AcceptChanges();
            Session["Questions"] = dt;

            grdAddedQuestion.DataSource = Session["Questions"];
            grdAddedQuestion.DataBind();
        }

        protected void grdAddedQuestion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAddedQuestion.EditIndex = e.NewEditIndex;
            BindLoadedQuestionsToGridView();
        }
        #endregion

        protected void cboQuestionsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdAddedQuestion.PageSize = Convert.ToInt32(cboQuestionsPerPage.SelectedItem.Text);
            BindLoadedQuestionsToGridView();
        }
        //Load Number of Items in the Questions per page DropDownList
        protected void cboQuestionsPerPage_Init(object sender, EventArgs e)
        {
            for (int x = 1; x <= 10; x++)
            {
                cboQuestionsPerPage.Items.Add(x.ToString());
            }
        }

        void LoadSiteMap()
        {
            //add a name
            SiteMap1.RootNode = "Dashboard";
            //add a tool tip text
            SiteMap1.RootNodeToolTip = "Dashboard";
            //add a postbackurl
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_index);

            SiteMap1.ParentNode = "Question Pool Main";
            SiteMap1.ParentNodeToolTip = "Click to go back to Question Pool Main";
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_questionpool_maintenance_main);

            //add a text for current node
            SiteMap1.CurrentNode = "Adding Question Manual";
        }

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            lblNotification.Visible = true;

            //check if has file
            if (fileUpload.HasFile == true)
            {
                string fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                //check if file is valid
                if (fileExtension.Contains(".png") || fileExtension.Contains(".jpg") || fileExtension.Contains(".jpeg") || fileExtension.Contains(".gif"))
                {
                    string targetlocation = Server.MapPath(Defaults.TempFolderPath + LUser.UserID.ToString() + fileUpload.FileName);

                    //temporary save the file
                    fileUpload.PostedFile.SaveAs(targetlocation);
                    lblNotification.Text = "Image / Picture Successfully Loaded.";

                    loadPicture.ImageUrl = Defaults.TempImagePath + LUser.UserID.ToString() + fileUpload.FileName;
                    panelpicture.ImageUrl = Defaults.TempImagePath + LUser.UserID.ToString() + fileUpload.FileName;
                    lblFileName.Text = LUser.UserID.ToString() + fileUpload.FileName;

                    imgPreView.Enabled = true;
                    imgRemove.Enabled = true;
                    imgRemove.ImageUrl = "~/images/icons/action_stop.gif";
                    imgPreView.ImageUrl = "~/images/icons/page_find.gif";
                }
                else
                {
                    lblNotification.Text = "Load Error. Only Image / Picture Files are allowed.";

                    imgPreView.Enabled = false;
                    imgRemove.Enabled = false;
                    imgRemove.ImageUrl = "~/images/icons/action_stop_disabled.gif";
                    imgPreView.ImageUrl = "~/images/icons/page_find_disabled.gif";
                    loadPicture.ImageUrl = "~/images/dashboard_icons/attachment.png";
                }
            } //no file selected
            else
            {
                lblNotification.Text = "No File Selected. Click Browse to select you file.";

                imgPreView.Enabled = false;
                imgRemove.Enabled = false;
                imgRemove.ImageUrl = "~/images/icons/action_stop_disabled.gif";
                imgPreView.ImageUrl = "~/images/icons/page_find_disabled.gif";
                loadPicture.ImageUrl = "~/images/dashboard_icons/attachment.png";
            }
        }

        protected void imgRemove_Click(object sender, ImageClickEventArgs e)
        {
            System.IO.File.Delete(Server.MapPath(Defaults.TempFolderPath + lblFileName.Text));
            imgPreView.Enabled = false;
            imgRemove.Enabled = false;
            imgRemove.ImageUrl = "~/images/icons/action_stop_disabled.gif";
            imgPreView.ImageUrl = "~/images/icons/page_find_disabled.gif";
            loadPicture.ImageUrl = "~/images/dashboard_icons/attachment.png";
            fileUpload = null;
            lblNotification.Text = "Note: Only Picture / Image Files are allowed.";
            lblFileName.Text = "";
        }

        protected void cboCorrect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCorrect.SelectedItem.Text.ToLower() == "true")
            {
                txtChoice1.Text = "False";
            }
            else
            {
                txtChoice1.Text = "True";
            }
        }

        protected void cboSubjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTopic();
        }
        
    }
}
