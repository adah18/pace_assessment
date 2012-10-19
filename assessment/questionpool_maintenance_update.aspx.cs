using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using PAOnlineAssessment.Classes;
using System.Diagnostics;
namespace PAOnlineAssessment.assessment
{
    public partial class questionpool_maintenance_update : System.Web.UI.Page
    {
        //Instantiate New Login User Class
        LoginUser LUser;
        //Instantiate New Global Forms Class
        GlobalForms DefaultForms = new Collections().getDefaultForms();
        //Instantiate New System Procedures Class
        SystemProcedures sys = new SystemProcedures();
        //Declare of list for topic
        List<Constructors.Topics> TopicList = new List<Constructors.Topics>(new Collections().GetTopic());
        //Instantiate New Collections Class
        Collections cls = new Collections();
        //Declare List of Assessment Types
        List<Constructors.QuestionPool> QuestionsList;
       
        int identifyer = 0;


        #region "Controls and Page Load"
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
            //Load Site Map
            LoadSiteMap();

            //Check if Page Load is Postback
            if (IsPostBack == false)
            {
                LoadTopic();
                LoadQuestionDetails();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl(DefaultForms.frm_default_dashboard));
        }


        void LoadTopic()
        {
            cboTopic.Items.Clear();
            cboTopic.Items.Add(new ListItem("--- Select Topic ---", "0"));
            TopicList.ForEach(tl =>
            {
                if (tl.Status == "A")
                {
                    cboTopic.Items.Add(new ListItem(tl.Description, tl.TopicID.ToString()));
                }
            });
        }

        string DefaultRemark(string remark,int type)
        {
            string value = "";
            if (type == 1)
            {
                value = remark;
                if (value.Trim().Length < 1)
                {
                    value = "You got the correct answer";
                }
            }
            else
            {
                value = remark;
                if (value.Trim().Length < 1)
                {
                    value = "Your answer is not correct";
                }
            }
            return value;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validator())
            {
                string qry="";
                if (Label10.Text == "3")
                {
                    qry = "UPDATE PaceAssessment.dbo.QuestionPool SET TopicID=" + cboTopic.SelectedValue + ", Question='" + Validator.Finalize(txtQuestion.Text) + "',CorrectAnswer='" + Validator.Finalize(cboCorrect.Text) + "',CorrectAnswerRemark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text,1)) + "', Choice1='" + Validator.Finalize(txtChoice1.Text) + "',Choice1Remark='" + Validator.Finalize(DefaultRemark(txtRemark1.Text,2)) + "', Choice2='" + Validator.Finalize(txtChoice2.Text) + "',Choice2Remark='" + Validator.Finalize(txtRemark2.Text) + "', Choice3='" + Validator.Finalize(txtChoice3.Text) + "',Choice3Remark='" + Validator.Finalize(txtRemark3.Text) + "', Choice4='" + Validator.Finalize(txtChoice4.Text) + "',Choice4Remark='" + Validator.Finalize(txtRemark4.Text) + "', LastUpdateUser = '" + LUser.Username + "', LastUpdateDate = getdate(),Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "' WHERE QuestionPoolID ='" + Request.QueryString["qid"] + "'";
                }
                else if(Label10.Text == "1")
                {
                    if(cboCorrect.SelectedItem.Text=="True")
                    {
                        qry = "UPDATE PaceAssessment.dbo.QuestionPool SET TopicID=" + cboTopic.SelectedValue + ", Question='" + Validator.Finalize(txtQuestion.Text) + "',CorrectAnswer='" + Validator.Finalize(cboCorrect.Text) + "',CorrectAnswerRemark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice1='" + Validator.Finalize("True") + "',Choice1Remark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice2='" + Validator.Finalize("False") + "',Choice2Remark='" + Validator.Finalize(DefaultRemark(txtRemark2.Text, 2)) + "', Choice3='" + Validator.Finalize(txtChoice3.Text) + "',Choice3Remark='" + Validator.Finalize(txtRemark3.Text) + "', Choice4='" + Validator.Finalize(txtChoice4.Text) + "',Choice4Remark='" + Validator.Finalize(txtRemark4.Text) + "', LastUpdateUser = '" + LUser.Username + "', LastUpdateDate = getdate(),Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "' WHERE QuestionPoolID ='" + Request.QueryString["qid"] + "'";
                    }
                    else
                    {
                        qry = "UPDATE PaceAssessment.dbo.QuestionPool SET TopicID=" + cboTopic.SelectedValue + ", Question='" + Validator.Finalize(txtQuestion.Text) + "',CorrectAnswer='" + Validator.Finalize(cboCorrect.Text) + "',CorrectAnswerRemark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice1='" + Validator.Finalize("True") + "',Choice1Remark='" + Validator.Finalize(DefaultRemark(txtRemark1.Text, 2)) + "', Choice2='" + Validator.Finalize("False") + "',Choice2Remark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice3='" + Validator.Finalize(txtChoice3.Text) + "',Choice3Remark='" + Validator.Finalize(txtRemark3.Text) + "', Choice4='" + Validator.Finalize(txtChoice4.Text) + "',Choice4Remark='" + Validator.Finalize(txtRemark4.Text) + "', LastUpdateUser = '" + LUser.Username + "', LastUpdateDate = getdate(),Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "' WHERE QuestionPoolID ='" + Request.QueryString["qid"] + "'";
                    }
                }
                else if(Label10.Text == "2")
                {
                    Random x = new Random();
                    int pos = x.Next(1,4);
                    if (pos == 1)
                    {
                        qry = "UPDATE PaceAssessment.dbo.QuestionPool SET TopicID=" + cboTopic.SelectedValue + ", Question='" + Validator.Finalize(txtQuestion.Text) + "',CorrectAnswer='" + Validator.Finalize(txtCorrect.Text) + "',CorrectAnswerRemark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice1='" + Validator.Finalize(txtChoice1.Text) + "',Choice1Remark='" + Validator.Finalize(DefaultRemark(txtRemark1.Text, 2)) + "', Choice2='" + Validator.Finalize(txtChoice2.Text) + "',Choice2Remark='" + Validator.Finalize(DefaultRemark(txtRemark2.Text, 2)) + "', Choice3='" + Validator.Finalize(txtChoice3.Text) + "',Choice3Remark='" + Validator.Finalize(DefaultRemark(txtRemark3.Text, 2)) + "', Choice4='" + Validator.Finalize(txtCorrect.Text) + "',Choice4Remark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', LastUpdateUser = '" + LUser.Username + "', LastUpdateDate = getdate(),Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "' WHERE QuestionPoolID ='" + Request.QueryString["qid"] + "'";
                    }
                    else if (pos == 2)
                    {
                        qry = "UPDATE PaceAssessment.dbo.QuestionPool SET TopicID=" + cboTopic.SelectedValue + ", Question='" + Validator.Finalize(txtQuestion.Text) + "',CorrectAnswer='" + Validator.Finalize(txtCorrect.Text) + "',CorrectAnswerRemark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice1='" + Validator.Finalize(txtChoice1.Text) + "',Choice1Remark='" + Validator.Finalize(DefaultRemark(txtRemark1.Text, 2)) + "', Choice2='" + Validator.Finalize(txtChoice2.Text) + "',Choice2Remark='" + Validator.Finalize(DefaultRemark(txtRemark2.Text, 2)) + "' , Choice3='" + Validator.Finalize(txtCorrect.Text) + "',Choice3Remark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "' ,  Choice4='" + Validator.Finalize(txtChoice3.Text) + "',Choice4Remark='" + Validator.Finalize(DefaultRemark(txtRemark3.Text, 2)) + "', LastUpdateUser = '" + LUser.Username + "', LastUpdateDate = getdate(),Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "' WHERE QuestionPoolID ='" + Request.QueryString["qid"] + "'";
                    }
                    else if (pos == 3)
                    {
                        qry = "UPDATE PaceAssessment.dbo.QuestionPool SET TopicID=" + cboTopic.SelectedValue + ", Question='" + Validator.Finalize(txtQuestion.Text) + "',CorrectAnswer='" + Validator.Finalize(txtCorrect.Text) + "',CorrectAnswerRemark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice1='" + Validator.Finalize(txtChoice1.Text) + "',Choice1Remark='" + Validator.Finalize(DefaultRemark(txtRemark1.Text, 2)) + "', Choice2='" + Validator.Finalize(txtCorrect.Text) + "',Choice2Remark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice3='" + Validator.Finalize(txtChoice2.Text) + "',Choice3Remark='" + Validator.Finalize(DefaultRemark(txtRemark2.Text, 2)) + "' ,  Choice4='" + Validator.Finalize(txtChoice3.Text) + "',Choice4Remark='" + Validator.Finalize(DefaultRemark(txtRemark3.Text, 2)) + "', LastUpdateUser = '" + LUser.Username + "', LastUpdateDate = getdate(),Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "' WHERE QuestionPoolID ='" + Request.QueryString["qid"] + "'";
                    }
                    else if (pos == 4)
                    {
                        qry = "UPDATE PaceAssessment.dbo.QuestionPool SET TopicID=" + cboTopic.SelectedValue + ", Question='" + Validator.Finalize(txtQuestion.Text) + "',CorrectAnswer='" + Validator.Finalize(txtCorrect.Text) + "',CorrectAnswerRemark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice1='" + Validator.Finalize(txtCorrect.Text) + "',Choice1Remark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice2='" + Validator.Finalize(txtCorrect.Text) + "',Choice2Remark='" + Validator.Finalize(DefaultRemark(txtRemarks.Text, 1)) + "', Choice2='" + Validator.Finalize(txtChoice1.Text) + "',Choice2Remark='" + Validator.Finalize(DefaultRemark(txtRemark1.Text, 2)) + "', Choice3='" + Validator.Finalize(txtChoice2.Text) + "',Choice3Remark='" + Validator.Finalize(DefaultRemark(txtRemark2.Text, 2)) + "' ,  Choice4='" + Validator.Finalize(txtChoice3.Text) + "',Choice4Remark='" + Validator.Finalize(DefaultRemark(txtRemark3.Text, 2)) + "', LastUpdateUser = '" + LUser.Username + "', LastUpdateDate = getdate(),Quarter='" + Validator.Finalize(ddlQuarter.SelectedItem.ToString()) + "' WHERE QuestionPoolID ='" + Request.QueryString["qid"] + "'";   
                    }
                    
                }
                
                if (cls.ExecuteNonQuery(qry) > 0)
                {

                    string ImageID = cls.ExecuteScalar("SELECT ImageID from PaceAssessment.dbo.Image where QuestionPoolID='" + Request.QueryString["qid"] + "'");
                    //check if has image file in the database
                    if (!string.IsNullOrEmpty(ImageID))
                    {
                        //check if has image to be save
                        if (lblFileName.Text != "")
                        {
                            //check if exist in the temp folder
                            if (System.IO.File.Exists(Server.MapPath(Defaults.TempFolderPath + lblFileName.Text)))
                            {
                                //image from the database
                                SaveFileFolder(Request.QueryString["qid"].ToString(), lblFileName.Text, new FileInfo(lblFileName.Text).Extension);
                                qry = "UPDATE PaceAssessment.dbo.Image set FileName='" + Request.QueryString["qid"] + new FileInfo(lblFileName.Text).Extension + "' where QuestionPoolID='" + Request.QueryString["qid"] + "'";
                                cls.ExecuteNonQuery(qry);
                            }
                        }
                        else
                        {
                            qry = "UPDATE PaceAssessment.dbo.Image set FileName='' where QuestionPoolID='" + Request.QueryString["qid"] + "'";
                            cls.ExecuteNonQuery(qry);
                        }
                    }
                        //no file name
                    else
                    {
                        if (lblFileName.Text != "")
                        {
                            SaveFileFolder(Request.QueryString["qid"], lblFileName.Text, new FileInfo(lblFileName.Text).Extension);
                            qry = "INSERT INTO PaceAssessment.dbo.Image (QuestionPoolID,FileName) values ('" + Request.QueryString["qid"] + "','" + Request.QueryString["qid"] + new FileInfo(lblFileName.Text).Extension + "')";
                            cls.ExecuteNonQuery(qry);
                        }
                    }
                    
                    Response.Write("<script>alert('Question has been updated successfully'); window.location='" + ResolveUrl(DefaultForms.frm_questionpool_maintenance_main) + "';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Action cannot continue. Please Try Again.')</script>");
                }
            }
        }

        #endregion
        #region "Validators"
        bool validator()
        {
            bool validator1 = true;
            //if (Validator.isDefaultSelected(cboType.SelectedValue)) validator1 = false;
            if (Validator.isEmpty(txtQuestion.Text))
            {
                vlSubjectList1.Text = "Required Field";
                validator1 = false;
            }
            else
            {
                vlSubjectList1.Text = "* ";
            }

        

            if (Label10.Text == "1")
            {
                //choice 1
                if (Validator.isEmpty(txtRemark1.Text))
                {
                    vlSubjectList5.Text = "Required Field";
                    validator1 = false;
                }
                else
                {
                    vlSubjectList5.Text = "* ";
                }
             
         
             

            }
            if (Label10.Text == "2")
            {
                //choice 1
                if (Validator.isEmpty(txtChoice1.Text))
                {
                    vlSubjectList5.Text = "Required Field";
                    validator1 = false;
                }
                else
                {
                    vlSubjectList5.Text = "* ";
                }

                //choice 2
           
                if (Validator.isEmpty(txtChoice2.Text))
                {
                    vlSubjectList6.Text = "Required Field";
                    validator1 = false;
                }
                else
                {
                    vlSubjectList6.Text = "* ";
                }
             
                if (Validator.isEmpty(txtChoice3.Text))
                {
                    vlSubjectList8.Text = "Required Field";
                    validator1 = false;
                }
                else
                {
                    vlSubjectList8.Text = "* ";
                }
                //choice 4
             
                if (Validator.isEmpty(txtChoice4.Text))
                {
                    vlSubjectList10.Text = "Required Field";
                    validator1 = false;
                }
                else
                {
                    vlSubjectList10.Text = "* ";
                }

                //txtCorrect
                if (Validator.isEmpty(txtCorrect.Text))
                {
                    vlSubjectList2.Text = "Required Field";
                    validator1 = false;
                }
                else
                {
                    vlSubjectList2.Text = "* ";
                }
            }
            if ((Validator.isEmpty(txtCorrect.Text)) && (Convert.ToInt32(Label10.Text) > 1))
            {
                vlSubjectList2.Text = "Required Field";
                validator1 = false;
            }
            else
            {
                vlSubjectList2.Text = "* ";
            }

            if (fileUpload.HasFile)
            {
                lblNotification.Text = "* Upload the selected image first";
                validator1 = false;
            }
            return validator1;
        }
        #endregion
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

        #region "voids and procedures"
        //Set the visible property of * icon for every required fields 
        void RequiredIcon()
        {
            //vlSubjectList4.Visible = true;
            vlSubjectList5.Visible = true;
            //vlSubjectList6.Visible = true;
            //vlSubjectList7.Visible = true;
            vlSubjectList8.Visible = true;
            //vlSubjectList9.Visible = true;
            vlSubjectList10.Visible = true;
            //vlSubjectList11.Visible = true;
            //true or false
            if (identifyer == 1)
            {
                vlSubjectList8.Visible = false;
                //vlSubjectList9.Visible = false;
                vlSubjectList10.Visible = false;
                //vlSubjectList11.Visible = false;

            }
            // Fill in the blanks
            else if (identifyer == 3)
            {
                //vlSubjectList4.Visible = false;
                vlSubjectList5.Visible = false;
                vlSubjectList6.Visible = false;
                //vlSubjectList7.Visible = false;
                vlSubjectList8.Visible = false;
                //vlSubjectList9.Visible = false;
                vlSubjectList10.Visible = false;
                //vlSubjectList11.Visible = false;
            }
        }
        //Load selected question to be edit
        void LoadQuestionDetails()
        {
            QuestionsList = new List<Constructors.QuestionPool>(new Collections().getQuestionPool());
            QuestionsList.ForEach(ql =>
            {
                if (ql.QuestionPoolID.ToString() == Request.QueryString["qid"])
                {
                    //check the picture
                    if (ql.ImageID != 0)
                    {
                        lblFileName.Text = ql.ImageFileName;
                        if (System.IO.File.Exists(Server.MapPath(Defaults.FolderPath + lblFileName.Text)))
                        {
                            loadPicture.ImageUrl = Defaults.ImagePath + ql.ImageFileName;
                            panelpicture.ImageUrl = Defaults.ImagePath + ql.ImageFileName;
                        }
                        else
                        {
                            loadPicture.ImageUrl = Defaults.ImagePath + "NoImage.jpg";
                            panelpicture.ImageUrl = Defaults.ImagePath + "NoImage.jpg";
                        }

                        imgPreView.Enabled = true;
                        imgRemove.Enabled = true;
                        imgRemove.ImageUrl = "~/images/icons/action_stop.gif";
                        imgPreView.ImageUrl = "~/images/icons/page_find.gif";
                    }
                    else
                    {
                        lblFileName.Text = "";
                        imgPreView.Enabled = false;
                        imgRemove.Enabled = false;
                        imgRemove.ImageUrl = "~/images/icons/action_stop_disabled.gif";
                        imgPreView.ImageUrl = "~/images/icons/page_find_disabled.gif";
                        loadPicture.ImageUrl = "~/images/dashboard_icons/attachment.png";
                    }


                    ddlQuarter.Text = ql.Quarter.ToString();// Session["Quarter"].ToString();
                    for (int i = 0; i < cboTopic.Items.Count; i++)
                    {
                        if (cboTopic.Items[i].Value == ql.TopicID.ToString())
                        {
                            cboTopic.Items[i].Selected = true;
                        }
                    }

                    txtQuestion.Text = ql.Question;
                    txtRemarks.Text = ql.CorrectAnswerRemark;
                    txtChoice1.Text = ql.Choice1;
                    txtRemark1.Text = ql.Choice1Remark;
                    txtChoice2.Text = ql.Choice2;
                    txtRemark2.Text = ql.Choice2Remark;
                    txtChoice3.Text = ql.Choice3;
                    txtRemark3.Text = ql.Choice3Remark;
                    txtChoice4.Text = ql.Choice4;
                    txtRemark4.Text = ql.Choice4Remark;
                    ddlQuarter.SelectedItem.Value = ql.Quarter;
                    //check if fill in the blanks
                    if ((Validator.isEmpty(txtChoice1.Text)) && (Validator.isEmpty(txtChoice2.Text)) && (Validator.isEmpty(txtChoice3.Text)) && (Validator.isEmpty(txtChoice4.Text)))
                    {
                        lblQuestionType.Text = "Fill in the blanks";
                        txtChoice1.Visible = false;
                        txtRemark1.Visible = false;
                        txtChoice2.Visible = false;
                        txtRemark2.Visible = false;
                        txtChoice3.Visible = false;
                        txtRemark3.Visible = false;
                        txtChoice4.Visible = false;
                        txtRemark4.Visible = false;
                        cboCorrect.Visible = false;
                        txtCorrect.Visible = true;
                        txtCorrect.Text = ql.CorrectAnswer;
                        identifyer = 3;
                    }
                    //check if True or False
                    else if ((Validator.isEmpty(txtChoice1.Text) == false) && (Validator.isEmpty(txtChoice2.Text) == false) && (Validator.isEmpty(txtChoice3.Text)) && (Validator.isEmpty(txtChoice4.Text)))
                    {
                        lblQuestionType.Text = "True or False";
                        txtChoice3.Visible = false;
                        txtRemark3.Visible = false;
                        txtChoice4.Visible = false;
                        txtRemark4.Visible = false;
                        txtChoice1.ReadOnly = true;
                        txtChoice2.ReadOnly = true;
                        cboCorrect.Visible = true;
                        txtCorrect.Visible = false;
                        SetItemFocus(ql.CorrectAnswer);
                        //Debug.WriteLine(ql.CorrectAnswer);

                        if (cboCorrect.SelectedItem.Text.ToLower() == "true")
                        {
                            ChangeValue();
                        }
                        txtChoice2.Visible = false;
                        txtRemark2.Visible = false;
                        Label7.Visible = false;
                        vlSubjectList6.Visible = false;
                        identifyer = 1;
                    }
                    //check if Multiple Choice
                    else
                    {
                        lblQuestionType.Text = "Multiple Choice";
                        txtCorrect.Visible = true;
                        cboCorrect.Visible = false;
                        txtCorrect.Text = ql.CorrectAnswer;
                        identifyer = 2;
                        ChangeValueMultiple();
                    }
                    return;
                }
            });
            Label10.Text = identifyer.ToString();
            RequiredIcon();
        }
        void SetItemFocus(string Answer)
        {
            for (int i = 0; i < cboCorrect.Items.Count;i++ )
            {
                if (cboCorrect.Items[i].Text == Answer)
                {
                    cboCorrect.Items[i].Selected = true;
                }
            }

        }
        

        //create a site map
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
            SiteMap1.CurrentNode = "Edit Questions";
        }
        #endregion

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
            fileUpload = null;
            lblNotification.Text = "Note: Only Picture / Image Files are allowed.";
            lblFileName.Text = "";
            imgPreView.Enabled = false;
            imgRemove.Enabled = false;
            imgRemove.ImageUrl = "~/images/icons/action_stop_disabled.gif";
            imgPreView.ImageUrl = "~/images/icons/page_find_disabled.gif";
            loadPicture.ImageUrl = "~/images/dashboard_icons/attachment.png";
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
            txtRemark1.Text = "";
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();

            trigger.ControlID = cboCorrect.ClientID;
            UpdatePanel2.Triggers.Add(trigger);
            UpdatePanel3.Triggers.Add(trigger);


        }


        void ChangeValue()
        {
            if (cboCorrect.SelectedItem.Text.ToLower() == "true")
            {
                hidChoice1.Value = txtChoice1.Text;
                hidRemark1.Value = txtRemark1.Text;
                txtChoice1.Text = txtChoice2.Text;
                txtRemark1.Text = txtRemark2.Text;
                txtChoice2.Text = hidChoice1.Value;
                txtRemark2.Text = hidRemark1.Value;
            }
            else
            {
                hidChoice1.Value = txtChoice1.Text;
                hidRemark1.Value = txtRemark1.Text;

                txtChoice1.Text = txtChoice2.Text;
                txtRemark1.Text = txtRemark2.Text;

                txtChoice2.Text = hidChoice1.Value;
                txtRemark2.Text = hidRemark1.Value;
            }
        }

        void ChangeValueMultiple()
        {
            if (txtChoice1.Text == txtCorrect.Text)
            {
                txtChoice1.Text = txtChoice2.Text;
                txtRemark1.Text = txtRemark2.Text;

                txtChoice2.Text = txtChoice3.Text;
                txtRemark2.Text = txtRemark3.Text;

                txtChoice3.Text = txtChoice4.Text;
                txtRemark3.Text = txtRemark4.Text;
            }
            else if (txtChoice2.Text == txtCorrect.Text)
            {
                txtChoice2.Text = txtChoice3.Text;
                txtRemark2.Text = txtRemark3.Text;

                txtChoice3.Text = txtChoice4.Text;
                txtRemark3.Text = txtRemark4.Text;
            }
            else if (txtChoice3.Text == txtCorrect.Text)
            {
                txtChoice3.Text = txtChoice4.Text;
                txtRemark3.Text = txtRemark4.Text;
            }

            txtChoice4.Visible = false;
            txtRemark4.Visible = false;

        }
    }
}
