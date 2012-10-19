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

namespace PAOnlineAssessment
{
    public partial class questionpool_maintenance_upload : System.Web.UI.Page
    {
        //Instantiate new Collections Class
        Collections cls = new Collections();
        //Declare a List containing RegistrationTerm Class
        List<Constructors.RegistrationTerm> RegistrationTermList;
        //Declare a List containing List of Subjects
        List<Constructors.Subject> SubjectList;
        //Declare a List Containing list of topics
        List<Constructors.Topics> TopicList = new List<Constructors.Topics>(new Collections().GetTopic());
        //Declare a LoginUser Class
        LoginUser LUser;
        //Instantiate New GlobalForms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();



        //Load Event
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

                if (Validator.CanbeAccess("13", LUser.AccessRights) == false)
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

            if (IsPostBack == false)
            {
                LoadSiteMap();
                LoadLevels();

                LoadSubjects();
                LoadTopic();

                ddlQuarter.SelectedValue = Session["Quarter"].ToString();
            }
        }

        void LoadTopic()
        {
            cboTopic.Items.Clear();
            cboTopic.Items.Add(new ListItem("--- Select Topic ---", "0"));
            TopicList.ForEach(tl => 
            {
                if (tl.Status == "A" && tl.LevelID.ToString() == cboGradeLevel.SelectedValue && tl.SubjectID.ToString() == cboSubjectList.SelectedValue)
                {
                    cboTopic.Items.Add(new ListItem(tl.Description, tl.TopicID.ToString()));
                }
            });
        }


        //Executed when the Upload button has been clicked
        protected void lnkUpload_Click(object sender, EventArgs e)
        {          

            //Check if a File was Selected
            if (fpExcelUploader.HasFile == true)
            {                
                Debug.WriteLine("File Found");
                GridViewPanel.Visible = true;

                //check if file is an Excel 2007 file
                if (fpExcelUploader.FileName.ToLower().Contains(".xlsx"))
                {
                    //Map Location for the Uploaded File
                    string TargetLocation = Server.MapPath("~" + "\\uploaded_spreadsheets\\" + Session.SessionID.ToString() + ".xlsx");
                    //Save the Uploaded File to the Mapped Location
                    fpExcelUploader.PostedFile.SaveAs(TargetLocation);
                    //Prompt the User that the File has been uploaded successfully
                    lblNotification.Text = "Successfully Loaded: " + fpExcelUploader.PostedFile.FileName;
                    lblNotification.Visible = true;
                    //get data from the ExcelFile
                    DataSet ds = cls.ExecuteXLSXQuery("select * from [Sheet1$]", TargetLocation);

                    //Check if the Columns in the Uploaded Excel File is Valid
                    if (isExcelColumnValid(ds) == true)
                    {
                        //Check if Questions, Correct Answer and Correct Answer Remark is Null
                        if (isExcelRowsValid(ds) == true)
                        {
                            LoadedQuestionsDataTable(ds);
                            BindLoadedQuestionsToGridView();
                        }
                        //if a row contained a null value
                        else
                        {
                            Debug.WriteLine("a row contained a null value");
                            lblNotification.Text = "Load Error. Please check the rows of the file to be uploaded";
                            lblNotification.Visible = true;
                            GridViewPanel.Visible = false;   
                        }

                    }
                    //if columns doesn't match, prompt the user
                    else
                    {
                        Debug.WriteLine("Columns does not match");
                        lblNotification.Text = "Load Error. Please check the columns of the file to be uploaded";
                        lblNotification.Visible = true;
                        GridViewPanel.Visible = false;   
                    }
                }
                //check if file is an Excel 2003 file
                else if (fpExcelUploader.FileName.ToLower().Contains(".xls"))
                {
                    //Map Location for the Uploaded File
                    string TargetLocation = Server.MapPath("~" + "\\uploaded_spreadsheets\\" + Session.SessionID.ToString() + ".xls");
                    //Save the Uploaded File to the Mapped Location
                    fpExcelUploader.PostedFile.SaveAs(TargetLocation);

                    lblNotification.Text = "Successfully Loaded: " + fpExcelUploader.PostedFile.FileName;
                    lblNotification.Visible = true;

                    //Get all Data in the Excel File
                    DataSet ds = cls.ExecuteXLSQuery("select * from [Sheet1$]", TargetLocation);

                    //Check the Uploaded Excel File if Valid
                    if (isExcelColumnValid(ds))
                    {
                        //Check if Questions, Correct Answer and Correct Answer Remark is Null
                        if (isExcelRowsValid(ds) == true)
                        {
                            LoadedQuestionsDataTable(ds);
                            BindLoadedQuestionsToGridView();
                        }
                        //if a row contained a null value
                        else
                        {
                            Debug.WriteLine("a row contained a null value");
                            lblNotification.Text = "Load Error. Please check the rows of the file to be uploaded";
                            lblNotification.Visible = true;
                            GridViewPanel.Visible = false;
                        }
                    }
                    //prompt the user if not valid
                    else
                    {
                        Debug.WriteLine("Columns does not match");
                        lblNotification.Text = "Load Error. Please check the columns of the file to be uploaded";
                        lblNotification.Visible = true;
                        GridViewPanel.Visible = false;   
                    }

                }
                //if not a valid excel file
                else
                {
                    Debug.WriteLine("Invalid File");
                    lblNotification.Text = "Load Error. Only MS Excel Files are Allowed";
                    lblNotification.Visible = true;
                    GridViewPanel.Visible = false;     
                }
             
            }
             
            //if no file was selected
            else
            {
                Debug.WriteLine("No File Found");
                lblNotification.Text = "No File Uploaded. Click Browse to select your file.";
                lblNotification.Visible = true;
                GridViewPanel.Visible = false;     
            }

            string SpreadSheetLocation = Server.MapPath("~" + "\\uploaded_spreadsheets\\");

            Loop:
            foreach (string Files in Directory.GetFiles(SpreadSheetLocation))
            {               
                File.Delete(Files);
                goto Loop;
            }
        }

        //Load Number of Items in the Questions per page DropDownList
        protected void cboQuestionsPerPage_Init(object sender, EventArgs e)
        {
            for (int x = 1; x <= 10; x++)
            {
                cboQuestionsPerPage.Items.Add(x.ToString());
            }
        }
             
    
        /////////////////////////
        //---------------------//
        //--- System Events ---//
        //---------------------//
        /////////////////////////

        #region "System Events"
        void LoadSiteMap()
        {
            //add a name
            SiteMap1.RootNode = "Dashboard";
            //add a tool tip text
            SiteMap1.RootNodeToolTip = "Dashboard";
            //add a postbackurl
            SiteMap1.RootNodeURL = ResolveUrl(DefaultForms.frm_admin_dashboard);

            SiteMap1.ParentNode = "Question Pool Main";
            SiteMap1.ParentNodeToolTip = "Click to go back to Question Pool Main";
            SiteMap1.ParentNodeURL = ResolveUrl(DefaultForms.frm_questionpool_maintenance_main);

            //add a text for current node
            SiteMap1.CurrentNode = "Upload Questions";
        }
        //Bind DataTable to GridView, from SessionVariable[LoadedQuestions]
        public void BindLoadedQuestionsToGridView()
        {
            grdLoadedQuestions.DataSource = (DataTable)Session["LoadedQuestions"];
            grdLoadedQuestions.PageSize = Convert.ToInt32(cboQuestionsPerPage.SelectedItem.Text);
            grdLoadedQuestions.DataBind();
        }

        //executed when the grade / level dropdown list has been changed
        protected void cboGradeLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadTopic();
        }

        //save button
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                DataTable dt = (DataTable)Session["LoadedQuestions"];
                int SuccessCount = 0;
                LUser = (LoginUser)Session["LoggedUser"];
                foreach (DataRow dRow in dt.Rows)
                {
                    string qry = "INSERT INTO [PaceAssessment].dbo.[QuestionPool]( SubjectID, LevelID, TopicID, Question, CorrectAnswer, CorrectAnswerRemark, Choice1, Choice1Remark, Choice2, Choice2Remark, Choice3, Choice3Remark, Choice4, Choice4Remark, Status, UserCreated, DateCreated, LastUpdateUser, LastUpdateDate,Quarter) " +
                           "VALUES('" + Validator.Finalize(cboSubjectList.SelectedValue) + "', '" + Validator.Finalize(cboGradeLevel.SelectedValue) + "', " + cboTopic.SelectedValue + " ,'" + Validator.Finalize(dRow["Question"].ToString()) + "', '" + Validator.Finalize(dRow["CorrectAnswer"].ToString()) + "', '" + Validator.Finalize(dRow["CorrectAnswerRemark"].ToString()) + "', '" + Validator.Finalize(dRow["Choice1"].ToString()) + "','" + Validator.Finalize(dRow["Choice1Remark"].ToString()) + "','" + Validator.Finalize(dRow["Choice2"].ToString()) + "','" + Validator.Finalize(dRow["Choice2Remark"].ToString()) + "','" + Validator.Finalize(dRow["Choice3"].ToString()) + "','" + Validator.Finalize(dRow["Choice3Remark"].ToString()) + "','" + Validator.Finalize(dRow["Choice4"].ToString()) + "','" + Validator.Finalize(dRow["Choice4Remark"].ToString()) + "','A','" + Validator.Finalize(LUser.Username) + "',getdate(),'" + Validator.Finalize(LUser.Username) + "',getdate(),'" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "')";
                    SuccessCount = SuccessCount + cls.ExecuteNonQuery(qry);
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

        //cancel button
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_index));
        }
        
        #endregion


        ///////////////////////////////
        //---------------------------//
        //--- GridView Procedures ---//
        //---------------------------//
        ///////////////////////////////

        #region "GridView Procedures"

        //executed when the Page Number DropDownList has been clicked
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            grdLoadedQuestions.PageIndex = cboPageNumber.SelectedIndex;
            BindLoadedQuestionsToGridView();
        }

        //executed when the Last Page Link button has been clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            grdLoadedQuestions.PageIndex = grdLoadedQuestions.PageCount;
            BindLoadedQuestionsToGridView();
        }

        //executed when the First Page Linkbutton has been Clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            grdLoadedQuestions.PageIndex = 0;
            BindLoadedQuestionsToGridView();
        }

        //Row Updating
        protected void grdLoadedQuestions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow grdRow = grdLoadedQuestions.Rows[e.RowIndex];
            TextBox txtQuestion = (TextBox)grdRow.FindControl("txtQuestion");
            TextBox txtCorrectAnswer = (TextBox)grdRow.FindControl("txtCorrectAnswer");
            TextBox txtCorrectAnswerRemark = (TextBox)grdRow.FindControl("txtCorrectAnswerRemark");
            TextBox txtChoice1 = (TextBox)grdRow.FindControl("txtChoice1");
            TextBox txtChoice1Remark = (TextBox)grdRow.FindControl("txtChoice1Remark");
            TextBox txtChoice2 = (TextBox)grdRow.FindControl("txtChoice2");
            TextBox txtChoice2Remark = (TextBox)grdRow.FindControl("txtChoice2Remark");
            TextBox txtChoice3 = (TextBox)grdRow.FindControl("txtChoice3");
            TextBox txtChoice3Remark = (TextBox)grdRow.FindControl("txtChoice3Remark");
            TextBox txtChoice4 = (TextBox)grdRow.FindControl("txtChoice4");
            TextBox txtChoice4Remark = (TextBox)grdRow.FindControl("txtChoice4Remark");

            DataTable dt = (DataTable)Session["LoadedQuestions"];
            dt.Rows[e.RowIndex][1] = txtQuestion.Text;
            dt.Rows[e.RowIndex][2] = txtCorrectAnswer.Text;
            dt.Rows[e.RowIndex][3] = txtCorrectAnswerRemark.Text;
            dt.Rows[e.RowIndex][4] = txtChoice1.Text;
            dt.Rows[e.RowIndex][5] = txtChoice1Remark.Text;
            dt.Rows[e.RowIndex][6] = txtChoice2.Text;
            dt.Rows[e.RowIndex][7] = txtChoice2Remark.Text;
            dt.Rows[e.RowIndex][8] = txtChoice3.Text;
            dt.Rows[e.RowIndex][9] = txtChoice3Remark.Text;
            dt.Rows[e.RowIndex][10] = txtChoice4.Text;
            dt.Rows[e.RowIndex][11] = txtChoice4Remark.Text;
            dt.AcceptChanges();

            grdLoadedQuestions.EditIndex = -1;
            BindLoadedQuestionsToGridView();

        }

        //Row Deleting
        protected void grdLoadedQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["LoadedQuestions"];
            dt.Rows[e.RowIndex].Delete();
            dt.AcceptChanges();

            grdLoadedQuestions.EditIndex = -1;
            BindLoadedQuestionsToGridView();
        }


        //executed when the Gridview has been data bound
        protected void grdLoadedQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                lblPageCount.Text = "of " + grdLoadedQuestions.PageCount;
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                for (int x = 1; x <= grdLoadedQuestions.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = grdLoadedQuestions.PageIndex;

            }
        }

        //executed when the edit button has been clicked
        protected void grdLoadedQuestions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdLoadedQuestions.EditIndex = e.NewEditIndex;
            BindLoadedQuestionsToGridView();
        }

        //executed when the questions per page dropdownlist has been changed
        protected void cboQuestionsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdLoadedQuestions.PageSize = Convert.ToInt32(cboQuestionsPerPage.SelectedItem.Text);
            BindLoadedQuestionsToGridView();
        }

        //executed when the Next Page Linkbutton has been clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (grdLoadedQuestions.PageIndex != grdLoadedQuestions.PageCount)
            {
                grdLoadedQuestions.PageIndex = grdLoadedQuestions.PageIndex + 1;
                BindLoadedQuestionsToGridView();
            }
        }

        //executed when the Previous Page Linkbutton has been clicked
        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            if (grdLoadedQuestions.PageIndex != 0)
            {
                grdLoadedQuestions.PageIndex = grdLoadedQuestions.PageIndex - 1;
                BindLoadedQuestionsToGridView();
            }
        }

        //executed when row editing has been cancelled
        protected void grdLoadedQuestions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdLoadedQuestions.EditIndex = -1;
            BindLoadedQuestionsToGridView();
        }
        
        #endregion

        ///////////////////////
        //-------------------//
        //--- Validations ---//
        //-------------------//
        ///////////////////////

        #region "Validations"

        //validate Uploaded Excel file if columns are correct
        public bool isExcelColumnValid(DataSet ds)
        {
            bool status = true;
            int MismatchedColumn = 0;
            //string array containing the list of columns
            string[] DefaultColumns = new string[] { "Question", "CorrectAnswer", "CorrectAnswerRemark", "Choice1", "Choice1Remark", "Choice2", "Choice2Remark", "Choice3", "Choice3Remark" };

           
            //Check if the number of columns in the excel file matches the number of default columns
            if (ds.Tables[0].Columns.Count > DefaultColumns.Length)
            {
                MismatchedColumn = 1;
            }
            else
            {
                //Validate if each column matches the specific default column
                for (int x = 0; x < ds.Tables[0].Columns.Count; x++)
                {
                    if (ds.Tables[0].Columns[x].ColumnName != DefaultColumns[x])
                    {                        
                        MismatchedColumn++;
                    }
                }
            }

            //if number of mismatched columns is greater than 0
            if (MismatchedColumn > 0)
            {
                //return false
                status = false;                
            }
            else
            {                
                //return true
                status = true;                
            }


                return status;
        }

        //Validate DropDownLists on the Page
        public bool ValidateFields()
        {
            bool Status = true;

            //Validate Grade Level DropDownList
            if (Validator.isDefaultSelected(cboGradeLevel.SelectedValue))
            {
                vlGradeLevel.Text = "* Please Specify Grade / Level";
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

            //Validate Excel File
            try
            {
                DataTable dt = (DataTable)Session["LoadedQuestions"];
                if (dt.Rows.Count == 0)
                {
                    vlLoadedQuestions.Text = "* Please Upload an Excel File containing the Questions";
                    Status = false;
                }
                else
                {
                    vlLoadedQuestions.Text = "*";
                }
            }
            catch
            {
                vlLoadedQuestions.Text = "* Please Upload an Excel File containing the Questions";
                Status = false;
            }
            return Status;
        }

        //Validate Rows for NULL Values
        public bool isExcelRowsValid(DataSet ds)
        {
            bool status = true;
            int NullRows = 0;

            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                if (Validator.isEmpty(dRow["Question"].ToString()) || Validator.isEmpty(dRow["CorrectAnswer"].ToString()))
                {
                    NullRows++;
                }
            }
            //Response.Write("<script>alert(" + NullRows.ToString() + ");</script>");
      
            if (NullRows > 0)
            {
                status = false;
            }
            else
            {
                status = true;
            }

            return status;   
        }

        #endregion

        ///////////////////////////
        //-----------------------//
        //--- Load Procedures ---//
        //-----------------------//
        ///////////////////////////

        #region "Load Procedures"
        
        //

        //Load Contents of the Uploaded Excel File
        public void LoadedQuestionsDataTable(DataSet Source)
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

            int RowCount = 1;
            Random random = new Random();
            foreach (DataRow dRow in Source.Tables[0].Rows)
            {
                int type = 0;
                //generate a random value for ta variable
                int pos = random.Next(1, 4);
                string CorrectChoice = dRow["CorrectAnswer"].ToString();
                string CorrectRemark = "";
                //check if the correct answer remark is empty or not
                if (dRow["CorrectAnswerRemark"].ToString().Trim().Length < 1)
                {
                    CorrectRemark = "You Got The Correct Answer";
                }
                else
                {
                    CorrectRemark = dRow["CorrectAnswerRemark"].ToString();
                }

                //checking if the uploaded question is a fill in the blank or true/false or a multiple choices
                if ( dRow["Choice2"].ToString().Trim() == "" && dRow["Choice3"].ToString().Trim() == "")
                {
                    if (dRow["CorrectAnswer"].ToString().ToLower() == "t" || dRow["CorrectAnswer"].ToString().ToLower() == "true" || dRow["CorrectAnswer"].ToString().ToLower() == "false" ||  dRow["CorrectAnswer"].ToString().ToLower() == "f")
                    {
                        type = 2;
                    }
                    else
                    {
                        type = 3;
                    }

                }
                else
                {
                    type = 1;
                }

                //add value to datatble
                if (type == 1)
                {
                    switch (pos)
                    {
                        case 1:
                            dt.Rows.Add(RowCount, dRow["Question"], dRow["CorrectAnswer"], CorrectRemark, dRow["Choice1"], IncorrectRemark(dRow["Choice1Remark"].ToString()), dRow["Choice2"], IncorrectRemark(dRow["Choice2Remark"].ToString()), dRow["Choice3"], IncorrectRemark(dRow["Choice3Remark"].ToString()), CorrectChoice, CorrectRemark);
                            break;
                        case 2:
                            dt.Rows.Add(RowCount, dRow["Question"], dRow["CorrectAnswer"], CorrectRemark, CorrectChoice, CorrectRemark, dRow["Choice1"], IncorrectRemark(dRow["Choice1Remark"].ToString()), dRow["Choice2"], IncorrectRemark(dRow["Choice2Remark"].ToString()), dRow["Choice3"], IncorrectRemark(dRow["Choice3Remark"].ToString()));
                            break;
                        case 3:
                            dt.Rows.Add(RowCount, dRow["Question"], dRow["CorrectAnswer"], CorrectRemark, dRow["Choice1"], IncorrectRemark(dRow["Choice1Remark"].ToString()), CorrectChoice, CorrectRemark, dRow["Choice2"], IncorrectRemark(dRow["Choice2Remark"].ToString()), dRow["Choice3"], IncorrectRemark(dRow["Choice3Remark"].ToString()));
                            break;
                        case 4:
                            dt.Rows.Add(RowCount, dRow["Question"], dRow["CorrectAnswer"], CorrectRemark, dRow["Choice1"], IncorrectRemark(dRow["Choice1Remark"].ToString()), dRow["Choice2"], IncorrectRemark(dRow["Choice2Remark"].ToString()), CorrectChoice, CorrectRemark, dRow["Choice3"], IncorrectRemark(dRow["Choice3Remark"].ToString()));
                            break;
                    }
                }
                else if (type == 2)
                {
                    if (dRow["CorrectAnswer"].ToString().ToLower() == "f" || dRow["CorrectAnswer"].ToString().ToLower() == "false")
                    {
                        dt.Rows.Add(RowCount, dRow["Question"], "FALSE", CorrectRemark, "TRUE", IncorrectRemark(dRow["Choice1Remark"].ToString()), "FALSE", CorrectRemark, "", "", "", "");
                    }
                    else
                    {
                        dt.Rows.Add(RowCount, dRow["Question"], "TRUE", CorrectRemark, "TRUE", CorrectRemark, "FALSE", IncorrectRemark(dRow["Choice1Remark"].ToString()), "", "", "", "");
                    }

                }
                else if (type == 3)
                {
                    dt.Rows.Add(RowCount, dRow["Question"], dRow["CorrectAnswer"], CorrectRemark, "", "", "", "", "", "", "", "");
                }
                RowCount++;       
            }
            Session["LoadedQuestions"] = dt;
        }

        //checking if the choices remark is empty or not
        string IncorrectRemark(string remark)
        {
            Debug.WriteLine(remark.Trim().Length.ToString());
            string value = "";

            if (remark.Trim().Length > 0)
            {
                value = remark;
            }
            else
            {
                value = "Your answer is not correct";
            }

            return value;
        }

        //Load Distinct List of Levels
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

        //Load List of Subjects based on the Selected School Year, Level and Section
        public void LoadSubjects()
        {
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
        #endregion

        //Download Template for question
        protected void linkButton_Click(object sender, EventArgs e)
        {
            bool forceDownload = true;

            string path = MapPath("../ExcelTemplate/Template.xlsx");
            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            string type = "";

            vlLoadedQuestions.Text = "*";
            // set known types based on file extension  
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".htm":
                    case ".html":
                        type = "text/HTML";
                        break;

                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".doc":
                    case ".rtf":
                        type = "Application/msword";
                        break;

                    case ".xls":
                    case ".xlsx":
                        type = "Application/x-msexcel";
                        break;
                }
            }
            if (forceDownload)
            {
                Response.AppendHeader("content-disposition",
                    "attachment; filename=" + name);
            }
            if (type != "")
            Response.ContentType = type;
            Response.WriteFile(path);
            Response.End();    
        }

        protected void cboSubjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTopic();
        }
    }
}
