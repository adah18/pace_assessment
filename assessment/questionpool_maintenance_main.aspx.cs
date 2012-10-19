using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Data;
using System.Diagnostics;

namespace PAOnlineAssessment
{
    public partial class questionpool_maintenance_main : System.Web.UI.Page
    {
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Declare Questions Pool
        List<Constructors.QuestionPool> QuestionsList;
        //declare a list of topic
        List<Constructors.Topics> TopicList = new List<Constructors.Topics>(new Collections().GetTopic());
        //Declare new Login User Class
        LoginUser LUser;
        //Instantiate Global Forms
        GlobalForms DefaultForms = new Collections().getDefaultForms();
              
        //Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if a User Is Logged In
            try
            {
                LUser = (LoginUser)Session["LoggedUser"];

                if ((bool)Session["Authenticated"] == false)
                {
                    Response.Redirect(ResolveUrl(DefaultForms.frm_index));
                    HideControl();
                }

                if (Validator.CanbeAccess("10", LUser.AccessRights) == false)
                {
                    Debug.WriteLine("Page cannot be accessed");

                    Validator.AlertBack("Page cannot be accessed.", "../block_user.aspx");

                }
            }
            //Redirect to Login Page when no User is logged in
            catch
            {
                Response.Redirect(ResolveUrl(DefaultForms.frm_index));
            }

            if (IsPostBack == false)
            {
                //set the current quarter
                LoadTopic();
                ddlQuarter.SelectedValue = Session["Quarter"].ToString();
                ConvertListToDataTable();
                BindQuestions();
            }
        }

        void LoadTopic()
        {
            cboTopic.Items.Clear();
            cboTopic.Items.Add(new ListItem("No Topic", "0"));
            TopicList.ForEach(tl =>
            {
                if (tl.Status == "A")
                {
                    cboTopic.Items.Add(new ListItem(tl.Description, tl.TopicID.ToString()));
                }
            });
        }
        //Convert List to DataTable for Viewing and Editing
        public DataTable ConvertListToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowNo"));
            dt.Columns.Add(new DataColumn("QuestionPoolID"));
            dt.Columns.Add(new DataColumn("SubjectID"));
            dt.Columns.Add(new DataColumn("SubjectDescription"));
            dt.Columns.Add(new DataColumn("LevelID"));
            dt.Columns.Add(new DataColumn("LevelDescription"));
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
            dt.Columns.Add(new DataColumn("Status"));
            dt.Columns.Add(new DataColumn("UserCreated"));
            dt.Columns.Add(new DataColumn("DateCreated"));
            dt.Columns.Add(new DataColumn("LastUpdateUser"));
            dt.Columns.Add(new DataColumn("LastUpdateDate"));
            dt.Columns.Add(new DataColumn("HasImage"));
            
            QuestionsList = new List<Constructors.QuestionPool>(new Collections().getQuestionPool());

            int RowCount = 1;
            string HasImage = "";
            switch (cboSearchQuery.SelectedValue)
            {
                case "Subject":
                     QuestionsList.ForEach(ql => 
                     {
                         if (ql.SubjectDescription.ToLower().Contains(txtSearchQuery.Text.ToLower()) && ql.Quarter == ddlQuarter.SelectedItem.ToString())
                         {
                             if(ql.ImageID != 0)
                             {
                                 HasImage = "Yes";
                             }
                             else
                             {
                                 HasImage = "No";
                             }
                             dt.Rows.Add(RowCount, ql.QuestionPoolID, ql.SubjectID, ql.SubjectDescription, ql.LevelID, ql.LevelDescription, ql.Question, ql.CorrectAnswer, ql.CorrectAnswerRemark, ql.Choice1, ql.Choice1Remark, ql.Choice2, ql.Choice2Remark, ql.Choice3, ql.Choice3Remark, ql.Choice4, ql.Choice4Remark, ql.Status, ql.UserCreated, ql.DateCreated, ql.LastUpdateUser, ql.LastUpdateDate, HasImage);
                             RowCount++;
                         }
                     });
                    break;
                case "Level":
                    QuestionsList.ForEach(ql =>
                    {
                        if (ql.LevelDescription.ToLower().Contains(txtSearchQuery.Text.ToLower()) && ql.Quarter == ddlQuarter.SelectedItem.ToString())
                        {
                            dt.Rows.Add(RowCount, ql.QuestionPoolID, ql.SubjectID, ql.SubjectDescription, ql.LevelID, ql.LevelDescription, ql.Question, ql.CorrectAnswer, ql.CorrectAnswerRemark, ql.Choice1, ql.Choice1Remark, ql.Choice2, ql.Choice2Remark, ql.Choice3, ql.Choice3Remark, ql.Choice4, ql.Choice4Remark, ql.Status, ql.UserCreated, ql.DateCreated, ql.LastUpdateUser, ql.LastUpdateDate, HasImage);
                            RowCount++;
                        }
                    });
                    break;
                case "Question":
                    QuestionsList.ForEach(ql =>
                    {
                        if (ql.Question.ToLower().Contains(txtSearchQuery.Text.ToLower()) && ql.Quarter == ddlQuarter.SelectedItem.ToString())
                        {
                            dt.Rows.Add(RowCount, ql.QuestionPoolID, ql.SubjectID, ql.SubjectDescription, ql.LevelID, ql.LevelDescription, ql.Question, ql.CorrectAnswer, ql.CorrectAnswerRemark, ql.Choice1, ql.Choice1Remark, ql.Choice2, ql.Choice2Remark, ql.Choice3, ql.Choice3Remark, ql.Choice4, ql.Choice4Remark, ql.Status, ql.UserCreated, ql.DateCreated, ql.LastUpdateUser, ql.LastUpdateDate, HasImage);
                            RowCount++;
                        }
                    });
                    break;
                case "Topic":
                    QuestionsList.ForEach(ql => 
                    {
                        if (ql.TopicID.ToString() == cboTopic.SelectedValue && ql.Quarter == ddlQuarter.SelectedItem.ToString())
                        {
                            dt.Rows.Add(RowCount, ql.QuestionPoolID, ql.SubjectID, ql.SubjectDescription, ql.LevelID, ql.LevelDescription, ql.Question, ql.CorrectAnswer, ql.CorrectAnswerRemark, ql.Choice1, ql.Choice1Remark, ql.Choice2, ql.Choice2Remark, ql.Choice3, ql.Choice3Remark, ql.Choice4, ql.Choice4Remark, ql.Status, ql.UserCreated, ql.DateCreated, ql.LastUpdateUser, ql.LastUpdateDate, HasImage);
                            RowCount++;
                        }
                    });
                    break;
                case "A":
                    QuestionsList.ForEach(ql =>
                    {
                        if (ql.Status == "A" && ql.Quarter == ddlQuarter.SelectedItem.ToString())
                        {
                            dt.Rows.Add(RowCount, ql.QuestionPoolID, ql.SubjectID, ql.SubjectDescription, ql.LevelID, ql.LevelDescription, ql.Question, ql.CorrectAnswer, ql.CorrectAnswerRemark, ql.Choice1, ql.Choice1Remark, ql.Choice2, ql.Choice2Remark, ql.Choice3, ql.Choice3Remark, ql.Choice4, ql.Choice4Remark, ql.Status, ql.UserCreated, ql.DateCreated, ql.LastUpdateUser, ql.LastUpdateDate, HasImage);
                            RowCount++;
                        }
                    });
                    break;
                case "D":
                    QuestionsList.ForEach(ql =>
                    {
                        if (ql.Status == "D" && ql.Quarter == ddlQuarter.SelectedItem.ToString())
                        {
                            dt.Rows.Add(RowCount, ql.QuestionPoolID, ql.SubjectID, ql.SubjectDescription, ql.LevelID, ql.LevelDescription, ql.Question, ql.CorrectAnswer, ql.CorrectAnswerRemark, ql.Choice1, ql.Choice1Remark, ql.Choice2, ql.Choice2Remark, ql.Choice3, ql.Choice3Remark, ql.Choice4, ql.Choice4Remark, ql.Status, ql.UserCreated, ql.DateCreated, ql.LastUpdateUser, ql.LastUpdateDate, HasImage);
                            RowCount++;
                        }
                    });
                    break;
            }
            
           

            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Add("", 0, "", "", "", "No Record Found");
            }

            cboPageSize.Items.Clear();
            for (int x = 1; x < RowCount; x++)
            {
                cboPageSize.Items.Add(x.ToString());
                cboPageSize.Items[cboPageSize.Items.Count - 1].Value = x.ToString();
            }
            try
            {
                cboPageSize.Items.FindByText(grdLoadedQuestions.PageSize.ToString()).Selected = true;
            }
            catch
            {

            }
                //Session["LoadedQuestions"] = dt;
            return dt;
        }

        //Bind DataTable to GridView
        public void BindQuestions()
        {
        //grdLoadedQuestions.DataSource = (DataTable)Session["LoadedQuestions"];
            grdLoadedQuestions.DataSource = ConvertListToDataTable();
            grdLoadedQuestions.DataBind();
        }       


        ///////////////////////////////
        //---------------------------//
        //--- GridView Procedures ---//
        //---------------------------//
        ///////////////////////////////

        #region "GridView Procedures"

        //Executed when the First Page Linkbutton has been clicked
        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            grdLoadedQuestions.PageIndex = 0;
            BindQuestions();
        }

        //Executed when the Page Number DropDownList has been changed
        protected void cboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboPageNumber = (DropDownList)sender;
            grdLoadedQuestions.PageIndex = cboPageNumber.SelectedIndex;
            BindQuestions();
        }

        //executed when the Last Page linkbutton has been clicked
        protected void lnkLast_Click(object sender, EventArgs e)
        {
            grdLoadedQuestions.PageIndex = grdLoadedQuestions.PageCount;
            BindQuestions();
        }

        //executed when the GridView has been Data Bound
        protected void grdLoadedQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int x = 0; x < grdLoadedQuestions.Rows.Count; x++)
            {
                Label lblQuestionsPoolID = (Label)grdLoadedQuestions.Rows[x].FindControl("lblQuestionPoolID");

                ImageButton imgEdit = (ImageButton)grdLoadedQuestions.Rows[x].FindControl("imgEdit");
                imgEdit.ToolTip = "Edit Question";
                string EditURL = ResolveUrl(DefaultForms.frm_questionpool_maintenance_update) + "?qid=" + lblQuestionsPoolID.Text;
                imgEdit.PostBackUrl = EditURL;

                ImageButton imgDeactivate = (ImageButton)grdLoadedQuestions.Rows[x].FindControl("imgDeactivate");
                
                Panel pnlQuestionsPanel = (Panel)grdLoadedQuestions.Rows[x].FindControl("pnlQuestionsPanel");
                Label lblQuestionType = (Label)grdLoadedQuestions.Rows[x].FindControl("lblQuestionType");
                                
                Label lblChoice1 = (Label)grdLoadedQuestions.Rows[x].FindControl("lblChoice1");
                Label lblChoice1Remark = (Label)grdLoadedQuestions.Rows[x].FindControl("lblChoice1Remark");
                Label lblChoice2 = (Label)grdLoadedQuestions.Rows[x].FindControl("lblChoice2");
                Label lblChoice2Remark = (Label)grdLoadedQuestions.Rows[x].FindControl("lblChoice2Remark");
                Label lblChoice3 = (Label)grdLoadedQuestions.Rows[x].FindControl("lblChoice3");
                Label lblChoice3Remark = (Label)grdLoadedQuestions.Rows[x].FindControl("lblChoice3Remark");
                Label lblChoice4 = (Label)grdLoadedQuestions.Rows[x].FindControl("lblChoice4");
                Label lblChoice4Remark = (Label)grdLoadedQuestions.Rows[x].FindControl("lblChoice4Remark");


                //No Record Found
                if (lblQuestionsPoolID.Text == "0")
                {
                    pnlQuestionsPanel.Visible = false;
                    imgEdit.ImageUrl = "~/images/icons/page_edit_disabled.gif";
                    imgEdit.Enabled = false;
                    imgDeactivate.ImageUrl = "~/images/icons/page_delete_disabled.gif";
                    imgDeactivate.Enabled = false;
                }
                else
                {
                    pnlQuestionsPanel.Visible = true;
                    imgEdit.ImageUrl = "~/images/icons/page_edit.gif";
                    imgEdit.Enabled = true;
                    imgDeactivate.ImageUrl = "~/images/icons/page_delete.gif";
                    imgDeactivate.Enabled = true;
                }

                if (lblChoice1.Text != "" && lblChoice2.Text != "" && lblChoice3.Text == "" && lblChoice4.Text == "")
                {
                    lblQuestionType.Text = "True / False";
                }
                else if (lblChoice1.Text == "" && lblChoice2.Text == "" && lblChoice3.Text == "" && lblChoice4.Text == "")
                {
                    lblQuestionType.Text = "Identification / Fill in the Blanks";
                }
                else if (lblChoice1.Text != "" && lblChoice2.Text != "" && lblChoice3.Text != "")
                {
                    lblQuestionType.Text = "Multiple Choice";
                }

            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageCount = (Label)e.Row.FindControl("lblPageCount");
                lblPageCount.Text = "of " + grdLoadedQuestions.PageCount;
                DropDownList cboPageNumber = (DropDownList)e.Row.FindControl("cboPageNumber");
                cboPageNumber.Items.Clear();
                for (int x = 1; x <= grdLoadedQuestions.PageCount; x++)
                {
                    cboPageNumber.Items.Add(x.ToString());
                }
                cboPageNumber.SelectedIndex = grdLoadedQuestions.PageIndex;
            }
        }

        //executed when the Next Page Linkbutton has been clicked
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            if (grdLoadedQuestions.PageIndex != grdLoadedQuestions.PageCount)
            {
                grdLoadedQuestions.PageIndex = grdLoadedQuestions.PageIndex + 1;
                BindQuestions();
            }
        }

        //executed when the Previous Page Linkbutton has been clicked
        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            if (grdLoadedQuestions.PageIndex != 0)
            {
                grdLoadedQuestions.PageIndex = grdLoadedQuestions.PageIndex - 1;
                BindQuestions();
            }
        }

        //executed when questions per page dropdownlist has been change
        protected void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdLoadedQuestions.PageSize = Convert.ToInt32(cboPageSize.SelectedValue);
            BindQuestions();
        }

        //executed when the search button has been clicked
        protected void imgSearchQuery_Click(object sender, ImageClickEventArgs e)
        {
            ConvertListToDataTable();
            grdLoadedQuestions.PageIndex = 0;
            BindQuestions();
        }

        //executed before the GridView was rendered
        protected void grdLoadedQuestions_PreRender(object sender, EventArgs e)
        {
            GridViewRow pagerRow = (GridViewRow)grdLoadedQuestions.BottomPagerRow;
            if (pagerRow != null && pagerRow.Visible == false)
            {
                pagerRow.Visible = true;
            }
        }


        //executed when the show details link button has been clicked
        protected void grdLoadedQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowHide")
            {
                LinkButton CurrentButton = (LinkButton)e.CommandSource;
                Debug.WriteLine("Button Trigger: " + CurrentButton.Text);
                GridViewRow CurrentRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Panel panel1 = (Panel)CurrentRow.FindControl("pnlChoices");
                if (CurrentButton.Text == "Show Details")
                {
                    panel1.Visible = true;
                    CurrentButton.Text = "Hide Details";
                }
                else
                {
                    panel1.Visible = false;
                    CurrentButton.Text = "Show Details";
                }
            }
        }

        #endregion

        protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConvertListToDataTable();
            grdLoadedQuestions.PageIndex = 0;
            BindQuestions();
        }

        protected void cboSearchQuery_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void HideControl()
        { 
            if (this.cboSearchQuery.SelectedValue == "Topic")
            {
                cboTopic.Visible = true;
                txtSearchQuery.Visible = false;
                ConvertListToDataTable();
                grdLoadedQuestions.PageIndex = 0;
                BindQuestions();
            }
            else 
            {
                if (cboSearchQuery.SelectedValue == "A" || cboSearchQuery.SelectedValue == "D")
                {
                    txtSearchQuery.Enabled = false;
                    ConvertListToDataTable();
                    grdLoadedQuestions.PageIndex = 0;
                    BindQuestions();
                }
                else 
                {
                    txtSearchQuery.Enabled = true;
                }
                cboTopic.Visible = false;
                txtSearchQuery.Visible = true;
            }
        }

        protected void cboTopic_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
