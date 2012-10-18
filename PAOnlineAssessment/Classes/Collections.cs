using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using PAOnlineAssessment.Classes;
using System.Diagnostics;

namespace PAOnlineAssessment.Classes
{
    public class Collections : Constructors 
    {
        
       // Load all users
       public List<Constructors.User> getUsers()
        {
            List<Constructors.User> UserList = new List<Constructors.User>();
            string qry = "SELECT [User].UserID, [User].UserGroupID, UserGroup.Description, [User].Username, [User].Password, [User].Firstname, [User].Lastname, [User].Status, [User].UserCreated, [User].DateCreated, [User].UpdateUser, [User].UpdateDate FROM PaceAssessment.dbo.[User] INNER JOIN PaceAssessment.dbo.UserGroup ON [User].UserGroupID = UserGroup.UserGroupID";
            dr = ExecuteReader(qry);
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    UserList.Add(new User
                    {
                        UserID = (int)dr["UserID"],
                        UserGroupID = (int)dr["UserGroupID"],
                        UserGroupDescription = dr["Description"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Status = dr["Status"].ToString(),
                        DateCreated = dr["DateCreated"].ToString(),
                        UserCreated = dr["UserCreated"].ToString(),
                        UpdateDate = dr["UpdateDate"].ToString(),
                        UpdateUser = dr["UpdateUser"].ToString(),
                    });
                    
                }
                Debug.WriteLine("List<> Row Count: " + UserList.Count.ToString());
            }            
            CloseConnection();
            return UserList;
        }

       // Load all students
       public List<Constructors.StudentAccount> getStudentAccounts()
       {
           List<Constructors.StudentAccount> StudentAccountList = new List<StudentAccount>();
           string qry = "SELECT StudentID, StudentNumber, Firstname, Lastname, Password, EmailAddress, EmailVerified, AdminVerified, Status, UserCreated, DateCreated, LastUpdateUser, LastUpdateDate, LevelID FROM [PaceAssessment].dbo.[Student]";           
           dr = ExecuteReader(qry);
           if (dr.HasRows == true)
           {
               while (dr.Read())
               {
                   StudentAccountList.Add(new StudentAccount
                   {
                       StudentID = (int)dr["StudentID"],
                       StudentNumber = dr["StudentNumber"].ToString(),
                       Firstname = dr["Firstname"].ToString(),
                       Lastname = dr["Lastname"].ToString(),
                       Password = dr["Password"].ToString(),
                       EmailAddress = dr["EmailAddress"].ToString(),
                       EmailVerified = dr["EmailVerified"].ToString(),
                       AdminVerified = dr["AdminVerified"].ToString(),
                       Status = dr["Status"].ToString(),
                       UserCreated = dr["UserCreated"].ToString(),
                       DateCreated = dr["DateCreated"].ToString(),
                       LastUpdateUser = dr["LastUpdateUser"].ToString(),
                       LastUpdateDate = dr["LastUpdateDate"].ToString(),
                       LevelID = (int)dr["LevelID"]
                   });
               }
               Debug.WriteLine("Student Accounts Total Rows: " + StudentAccountList.Count.ToString());               
           }           
           CloseConnection();

           return StudentAccountList;
       }
        //Load all registration
       public List<Constructors.RegistrationTerm> getRegistrationTerm()
       {
           List<RegistrationTerm> RegistrationTermList = new List<RegistrationTerm>();
           string qry = "SELECT * FROM PaceAssessment.dbo.RegistrationTermView";
           dr = ExecuteReader(qry);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   RegistrationTermList.Add(new RegistrationTerm 
                   {
                       SchoolYear = dr["SchoolYear"].ToString(),
                       LevelID = (int)dr["LevelID"],
                       SectionID = (int)dr["SectionID"],
                       SectionDescription = dr["SectionDescription"].ToString(),
                       LevelDescription = dr["LevelDescription"].ToString(),
                       SubjectID = (int)dr["SubjectID"],
                       SubjectDescription = dr["SubjectDescription"].ToString()
                       });
               }
           }           
           CloseConnection();
           return RegistrationTermList;
       }

        //Load all subject
       public List<Constructors.Subject> getSubjectList()
       {
           List<Subject> SubjectList = new List<Subject>();
           string qry = "SELECT * FROM PaceRegistration.dbo.Subject";
           dr = ExecuteReader(qry);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   SubjectList.Add(new Subject 
                   {
                       SubjectID = (int)dr["SubjectID"],
                       LevelID = (int)dr["LevelID"],
                       Description = dr["Description"].ToString(),
                       SubjectWeight = float.Parse(dr["SubjectWeight"].ToString()),
                       DateCreated = dr["DateCreated"].ToString(),
                       UserCreated = dr["UserCreated"].ToString(),
                       LastUpdateDate = dr["LastUpdateDate"].ToString(),
                       LastUpdateUser = dr["LastUpdateUser"].ToString()
                   });
               }
           }
           
           CloseConnection();
           return SubjectList;
       }
        // Load all Questions
       public List<Constructors.QuestionPool> getQuestionPool()
       {
           List<QuestionPool> QuestionPoolList = new List<QuestionPool>();
           string qry = "select * from [PaceAssessment].dbo.QuestionPoolView";
           dr = ExecuteReader(qry);

           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   QuestionPoolList.Add(new QuestionPool 
                   {
                       QuestionPoolID = (int)dr["QuestionPoolID"],
                       SubjectID = (int)dr["SubjectID"],
                       SubjectDescription = dr["SubjectDescription"].ToString(),
                       LevelID = (int)dr["LevelID"],
                       LevelDescription = dr["LevelDescription"].ToString(),
                       Question = dr["Question"].ToString(),
                       CorrectAnswer = dr["CorrectAnswer"].ToString(),
                       CorrectAnswerRemark = dr["CorrectAnswerRemark"].ToString(),
                       Choice1 = dr["Choice1"].ToString(),
                       Choice1Remark = dr["Choice1Remark"].ToString(),
                       Choice2 = dr["Choice2"].ToString(),
                       Choice2Remark = dr["Choice2Remark"].ToString(),
                       Choice3 = dr["Choice3"].ToString(),
                       Choice3Remark = dr["Choice3Remark"].ToString(),
                       Choice4 = dr["Choice4"].ToString(),
                       Choice4Remark = dr["Choice4Remark"].ToString(),
                       Status = dr["Status"].ToString(),
                       UserCreated = dr["UserCreated"].ToString(),
                       DateCreated = dr["DateCreated"].ToString(),
                       LastUpdateUser = dr["LastUpdateUser"].ToString(),
                       LastUpdateDate = dr["LastUpdateDate"].ToString(),
                       Quarter = dr["Quarter"].ToString(),
                       ImageID = (int)dr["ImageID"],
                       ImageFileName = dr["FileName"].ToString(),
                       TopicID = (int)dr["TopicID"],
                   });
               }
           }
           CloseConnection();
           return QuestionPoolList;
       }
        // Load all assessment type
       public List<Constructors.AssessmentType> getAssessmentType()
       {
           List<AssessmentType> AssessmentTypeList = new List<AssessmentType>();
           string qry = "SELECT * FROM [PaceAssessment].dbo.[AssessmentType]";
           dr = ExecuteReader(qry);
           if (dr.HasRows == true)
           {
               while (dr.Read())
               {
                   AssessmentTypeList.Add(new AssessmentType 
                   {
                       AssessmentTypeID = (int)dr["AssessmentTypeID"],
                       Description = dr["Description"].ToString(),
                       Status = dr["Status"].ToString(),
                       UserCreated = dr["UserCreated"].ToString(),
                       DateCreated = dr["DateCreated"].ToString(),
                       LastUpdateUser = dr["LastUpdateUser"].ToString(),
                       LastUpdateDate = dr["LastUpdateDate"].ToString()

                   });
               }
           }
           CloseConnection();
           return AssessmentTypeList;
       }
        // Load all assessment
       public List<Constructors.Assessment> getAssessment()
       {
           List<Assessment> AssessmentList = new List<Assessment>();
           string qry = "SELECT * FROM [PaceAssessment].dbo.[Assessment]";
           dr = ExecuteReader(qry);
           if (dr.HasRows == true)
           {
               while (dr.Read())
               {
                   AssessmentList.Add(new Assessment
                   {
                       AssessmentID = (int)dr["AssessmentID"],
                       AssessmentTypeID = (int)dr["AssessmentTypeID"],
                       UserID = (int)dr["UserID"],
                       LevelID = (int)dr["LevelID"],
                       SubjectID = (int)dr["SubjectID"],
                       Title = dr["Title"].ToString(),
                       Introduction = dr["Introduction"].ToString(),
                       Schedule = dr["Schedule"].ToString(),
                       ScheduleStatus = dr["ScheduleStatus"].ToString(),
                       DateStart = dr["DateStart"].ToString(),
                       DateEnd = dr["DateEnd"].ToString(),
                       TimeStart = dr["TimeStart"].ToString(),
                       TimeEnd = dr["TimeEnd"].ToString(),
                       Status = dr["Status"].ToString(),
                       UserCreated = dr["UserCreated"].ToString(),
                       DateCreated = dr["DateCreated"].ToString(),
                       LastUpdateUser = dr["LastUpdateUser"].ToString(),
                       LastUpdateDate = dr["LastUpdateDate"].ToString(),
                       Quarter = dr["Quarter"].ToString(),
                       RandomQuestion = dr["RandomQuestion"].ToString(),
                       RandomAnswer = dr["RandomAnswer"].ToString(),
                       MakeUp = dr["MakeUp"].ToString(),
                       NoMakeUp = dr["NoMakeUp"].ToString(),
                       SchoolYear = dr["SchoolYear"].ToString(),
                   });
               }
           }
           CloseConnection();
           return AssessmentList;
       }
        //Load all assessment details
       public List<Constructors.AssessmentDetails> getAssessmentDetails()
       {
           List<AssessmentDetails> AssessmentDetailsList = new List<AssessmentDetails>();
           string qry = "SELECT * FROM [PaceAssessment].dbo.[AssessmentDetails]";
           dr = ExecuteReader(qry);
           if (dr.HasRows == true)
           {
               while (dr.Read())
               {
                   AssessmentDetailsList.Add(new AssessmentDetails
                   {
                       AssessmentID = (int)dr["AssessmentID"],
                       QuestionPoolID = (int)dr["QuestionPoolID"],
                       Points = (int)dr["Points"]
                   });
               }
           }
           CloseConnection();
           return AssessmentDetailsList;
       }
        // Load all assessment feedback
       public List<Constructors.AssessmentFeedback> getAssessmentFeedback()
       {
           List<AssessmentFeedback> AssessmentFeedbackList = new List<AssessmentFeedback>();
           string qry = "SELECT * FROM [PaceAssessment].dbo.[AssessmentFeedback]";
           dr = ExecuteReader(qry);
           if (dr.HasRows == true)
           {
               while (dr.Read())
               {
                   AssessmentFeedbackList.Add(new AssessmentFeedback
                   {
                       AssessmentID = (int)dr["AssessmentID"],
                       GradeBoundary = dr["GradeBoundary"].ToString(),
                       Feedback = dr["Feedback"].ToString()
                   });
               }
           }
           CloseConnection();
           return AssessmentFeedbackList;
       }
        // Load all assessment view
       public List<Constructors.AssessmentView> getAssessmentView()
       {
           List<Constructors.AssessmentView> AssessmentViewList = new List<AssessmentView>();
           string qry = "SELECT * FROM PaceAssessment.dbo.AssessmentView";
           dr = ExecuteReader(qry);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   AssessmentViewList.Add(new AssessmentView 
                   {
                       AssessmentID = (int)dr["AssessmentID"],
                       AssessmentTypeID = (int)dr["AssessmentTypeID"],
                       AssessmentTypeDescription = dr["AssessmentTypeDescription"].ToString(),
                       UserID = (int)dr["UserID"],
                       TeacherFirstname = dr["TeacherFirstname"].ToString(),
                       TeacherLastname = dr["TeacherLastname"].ToString(),
                       LevelID = (int)dr["LevelID"],
                       LevelDescription = dr["LevelDescription"].ToString(),
                       SubjectID = (int)dr["SubjectID"],
                       SubjectDescription = dr["SubjectDescription"].ToString(),
                       Title = dr["Title"].ToString(),
                       Introduction = dr["Introduction"].ToString(),
                       Schedule = dr["Schedule"].ToString(),
                       ScheduleStatus = dr["ScheduleStatus"].ToString(),
                       DateStart = dr["DateStart"].ToString(),
                       DateEnd = dr["DateEnd"].ToString(),
                       TimeStart = dr["TimeStart"].ToString(),
                       TimeEnd = dr["TimeEnd"].ToString(),
                       Status = dr["Status"].ToString(),
                       UserCreated = dr["UserCreated"].ToString(),
                       DateCreated = dr["DateCreated"].ToString(),
                       LastUpdateUser =dr["LastUpdateUser"].ToString(),
                       LastUpdateDate = dr["LastUpdateDate"].ToString(),
                       Quarter = dr["Quarter"].ToString(),
                       RandomQuestions = dr["RandomQuestion"].ToString(),
                       RandomAnswer = dr["RandomAnswer"].ToString(),
                       MakeUp = dr["MakeUp"].ToString(),
                       NoMakeUp = dr["NoMakeUp"].ToString(),
                       SchoolYear = dr["SchoolYear"].ToString(),
                   });
               }
           }
           CloseConnection();
           return AssessmentViewList;
       }
        // Load all grading view
       public List<Constructors.GradingView> getGradingView()
       {
           List<Constructors.GradingView> GradingViewList = new List<GradingView>();
           string qry = "SELECT [GradingID],[TeacherID],[AssignedName],[LevelID],[SectionID],[Level-Section],[SubjectID],[Description],[SchoolYear],[DateCreated],[UserCreated],[AdvisoryTeacher],[AdvisoryTeacherID] FROM [PaceRegistration].[dbo].[GradingView]";
           dr = ExecuteReader(qry);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   GradingViewList.Add(new GradingView
                   {
                       GradingID = (int)dr["GradingID"],
                       TeacherID = (int)dr["TeacherID"],
                       LevelID = (int)dr["LevelID"],
                       SectionID = (int)dr["SectionID"],
                       SubjectID = (int)dr["SubjectID"],
                       AdvisoryTeacherID = dr["AdvisoryTeacherID"].ToString(),
                       AssignedName = dr["AssignedName"].ToString(),
                       LevelSection = dr["Level-Section"].ToString(),
                       Description = dr["Description"].ToString(),
                       SchoolYear = dr["SchoolYear"].ToString(),
                       DateCreated = dr["DateCreated"].ToString(),
                       UserCreated = dr["UserCreated"].ToString(),
                       AdvisoryTeacher = dr["AdvisoryTeacher"].ToString(),
                       
                   });
               }
           }
           CloseConnection();
           return GradingViewList;
       }

        // Load all student answers
       public List<Constructors.StudentAnswers> getStudentAnswersList()
       {
           List<Constructors.StudentAnswers> StudentAnswersList = new List<StudentAnswers>();

           string qry = "SELECT StudentAnswerID, StudentID, SchoolYear, AssessmentID, QuestionPoolID, SelectedAnswer, LastUpdateUser, LastUpdateDate FROM [PaceAssessment].dbo.[StudentAnswers]";
           dr = ExecuteReader(qry);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   StudentAnswersList.Add(new StudentAnswers 
                   {
                       StudentAnswerID = (int)dr["StudentAnswerID"],
                       StudentID = (int)dr["StudentID"],
                       SchoolYear = dr["SchoolYear"].ToString(),
                       AssessmentID = (int)dr["AssessmentID"],
                       QuestionPoolID = (int)dr["QuestionPoolID"],
                       SelectedAnswer = dr["SelectedAnswer"].ToString(),
                       LastUpdateUser = dr["LastUpdateUser"].ToString(),
                       LastUpdateDate = dr["LastUpdateDate"].ToString()
                   });
               }
           }


           CloseConnection();
           return StudentAnswersList;
       }
        // Load all student registration view
       public List<Constructors.StudentRegistrationView> getStudentRegistrationView()
       {
           List<StudentRegistrationView> StudentList = new List<StudentRegistrationView>();
           string qry = "Select * From PaceRegistration.dbo.StudentRegistrationView";
           dr = ExecuteReader(qry);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   StudentList.Add(new StudentRegistrationView
                   {
                       StudentID = (int)dr["StudentID"],
                       SectionID = (int)dr["SectionID"],
                       CurrentLevelID = (int)dr["CurrentLevelID"],
                       PaymentTermsID = (int)dr["PaymentTermsID"],
                       LevelID = (int)dr["LevelID"],
                       RegistrationID = (int)dr["RegistrationID"],
                       // SummerWorkshopID = (int)dr["SummerWorkshopID"],
                       StudentNumber = dr["StudentNumber"].ToString(),
                       FirstName = dr["FirstName"].ToString(),
                       MiddleName = dr["MiddleName"].ToString(),
                       LastName = dr["LastName"].ToString(),
                       Discount = dr["Discount"].ToString(),
                       StudentStatus = dr["StudentStatus"].ToString(),
                       Gender = dr["Gender"].ToString(),
                       SchoolYear = dr["SchoolYear"].ToString(),
                       RegistrationStatus = dr["RegistrationStatus"].ToString(),
                       ReservationDate = dr["ReservationDate"].ToString(),
                       RegistrationDate = dr["RegistrationDate"].ToString(),
                       UserReserved = dr["UserReserved"].ToString(),
                       UserRegistered = dr["UserRegistered"].ToString(),
                       UserSummerRegistered = dr["UserSummerRegistered"].ToString(),
                       ReservationPayment = dr["ReservationPayment"].ToString(),
                       RegistrationPayment = dr["RegistrationPayment"].ToString(),
                       SummerRegistrationPayment = dr["SummerRegistrationPayment"].ToString(),
                       LevelDescription = dr["LevelDescription"].ToString(),
                       SectionDescription = dr["SectionDescription"].ToString(),
                       isOldStudent = dr["isOldStudent"].ToString(),
                       Address = dr["Address"].ToString(),
                       Birthday = dr["Birthday"].ToString(),
                       AdvisoryTeacher = dr["AdvisoryTeacher"].ToString(),
                       SummerRegistrationDate = dr["SummerRegistrationDate"].ToString()
                   });
               }
           }
           CloseConnection();
           return StudentList;
       }
        // get assessment type id
       public List<Constructors.Assessment> getAssessmentTypeID(int SubjectID, string SchoolYear)
       {
           List<Constructors.Assessment> AssessmentType = new List<Assessment>();
           try
           {
               string qry = "Select DISTINCT a.AssessmentTypeID From PaceAssessment.dbo.Assessment a WHERE a.SubjectID='" + SubjectID.ToString() + "' and a.SchoolYear='" + SchoolYear + "'";
               dr = ExecuteReader(qry);
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       AssessmentType.Add(new Assessment
                       {
                           AssessmentTypeID = (int)dr["AssessmentTypeID"],
                       });
                   }
               }
           }
           catch
           {
               AssessmentType = null;
           }

           CloseConnection();
           return AssessmentType;
       }
        // Load Quarter
       public List<Constructors.Quarter> getQuarter()
       {
           List<Constructors.Quarter> QuarterList = new List<Quarter>();
           string sql = "Select * From PaceAssessment.dbo.Quarter";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   QuarterList.Add(new Quarter
                   {
                       Quarters = dr["Quarter"].ToString(),
                       DateFrom = dr["DateFrom"].ToString(),
                       DateTo = dr["DateTo"].ToString(),
                       SchoolYear = dr["SchoolYear"].ToString(),
                       LastUpdateDate = dr["LastUpdateDate"].ToString(),
                       LastUpdateUser = dr["LastUpdateUser"].ToString(),
                       isCurrentSY = dr["isCurrentSY"].ToString(),
                   });
               }
           }
           CloseConnection();
           return QuarterList;
       }
        //get the current quarter that match the date
       public String CurrentQuarter()
       {
           //set the 1st quarter default
           string quarter = "1st";
           Collections cls = new Collections();
           List<Constructors.Quarter> QuarterList = new List<Quarter>(cls.getQuarter());
           QuarterList.ForEach(q =>
           {
               if (Convert.ToDateTime(q.DateFrom) <= DateTime.Now && Convert.ToDateTime(q.DateTo) >= DateTime.Now && q.isCurrentSY == "YES")
               {
                   quarter = q.Quarters;
               }
           });
           CloseConnection();
           return quarter;
       }

        // Load all levels
       public List<Constructors.Levels> GetLevels()
       {
           List<Constructors.Levels> GetLevels = new List<Levels>();
           string sql = "Select * From PaceRegistration.dbo.Level";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   GetLevels.Add(new Levels
                   {
                       LevelID = (int)dr["LevelID"],
                       LevelDescription = dr["Description"].ToString(),
                       Status = dr["Status"].ToString()
                   });
               }
           }
           CloseConnection();
           return GetLevels;
       }

        // Load all sections
       public List<Constructors.Sections> GetSection()
       {
           List<Constructors.Sections> GetSection = new List<Sections>();
           string sql = "Select * From PaceRegistration.dbo.Section";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   GetSection.Add(new Sections
                   {
                       SectionID = (int)dr["SectionID"],
                       LevelID = (int)dr["LevelID"],
                       UserID = (int)dr["UserID"],
                       SectionDescription = dr["Description"].ToString(),
                       Status = dr["Status"].ToString(),

                   });
               }
           }
           CloseConnection();
           return GetSection;
       }
        // Load all parents
       public List<Constructors.ParentView> GetParent()
       {
           List<Constructors.ParentView> GetParent = new List<ParentView>();
           string sql = "Select * From PaceAssessment.dbo.ParentView";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   GetParent.Add(new ParentView
                   {
                       UserID = (int)dr["UserID"],
                       UserGroupID = (int)dr["UserGroupID"],
                       ParentID = (int)dr["ParentID"],
                       YearLevel = (int)dr["YearLevel"],
                       Section = (int)dr["Section"],
                       Username = dr["Username"].ToString(),
                       Password = dr["Password"].ToString(),
                       Firstname = dr["Firstname"].ToString(),
                       Lastname = dr["Lastname"].ToString(),
                       CFirstname = dr["ChildFirstname"].ToString(),
                       CLastname = dr["ChildLastname"].ToString(),
                       Status = dr["Status"].ToString()
                   });
               }
           }
           CloseConnection();
           return GetParent;
       }
        //Load all child
       public List<Constructors.ParentChildGrades> GetChild()
       {
           List<Constructors.ParentChildGrades> Childs = new List<ParentChildGrades>();
           string sql = "Select * From PaceAssessment.dbo.ParentChild";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   Childs.Add(new ParentChildGrades
                   {
                       ChildID = (int)dr["ChildID"],
                       StudentID = (int)dr["StudentID"],
                       ParentUserID = (int)dr["ParentUserID"],
                       LevelID = (int)dr["YearLevel"],
                       SectionID = (int)dr["Section"],
                       Status = dr["Status"].ToString()
                   });
               }
           }

           CloseConnection();
           return Childs;
       }
        //get the number of days assessment can be viewed
       public List<Constructors.DisplaySettings> GetDays()
       {
           List<Constructors.DisplaySettings> oDisplaySettings = new List<DisplaySettings>();
           string sql = "Select * From PaceAssessment.dbo.Settings";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   oDisplaySettings.Add(new DisplaySettings
                   {
                       Days = (int)dr["Days"],
                       Registration = dr["Registration"].ToString(),

                   });
               }
           }
           CloseConnection();
           return oDisplaySettings;
       }

        // function that load all topics
       public List<Constructors.Topics> GetTopic()
       {
           List<Constructors.Topics> _Topics = new List<Topics>();
           string sql = "Select * From PaceAssessment.dbo.Topics";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _Topics.Add(new Topics
                   {
                       TopicID = (int)dr["TopicID"],
                       LevelID = (int)dr["LevelID"],
                       SubjectID = (int)dr["SubjectID"],
                       Description = (string)dr["Description"],
                       Status = (string)dr["Status"]

                   });
               }
           }
           CloseConnection();
           return _Topics;
       }
       // function that load all usergroup
       public List<Constructors.Usergroup> GetUsergroup()
       {
           List<Constructors.Usergroup> _Usergroup = new List<Usergroup>();
           string sql = "Select * From PaceAssessment.dbo.Usergroup";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _Usergroup.Add(new Usergroup
                   {
                       UserGroupID = (int)dr["UserGroupID"],
                       Description = (string)dr["Description"],
                       AccessRights = (string)dr["AccessRights"],
                       Status = (string)dr["Status"]
                   });
               }
           }
           CloseConnection();
           return _Usergroup;
       }
       //  function that load all requirement header
       public List<Constructors.RequirementHeader> GetRequirementHeader()
       {
           List<Constructors.RequirementHeader> _Requirement = new List<RequirementHeader>();
           string sql = "Select [RequirementHeaderID], [Description] From PaceRegistration.dbo.[RequirementHeader]";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _Requirement.Add(new RequirementHeader 
                   {
                       RequirementHeaderID = Convert.ToInt32(dr["RequirementHeaderID"]),
                       RequirementDescription = dr["Description"].ToString()
                   });
               }
           }
           CloseConnection();
           return _Requirement;
       }
        // function that load all requirement
       public List<Constructors.Requirement> GetRequirement()
       {
           List<Constructors.Requirement> _Requirement = new List<Requirement>();
           string sql = "Select * From PaceRegistration.dbo.Requirement";
           dr = ExecuteReader(sql);

           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _Requirement.Add(new Requirement
                   {
                       RequirementID = Convert.ToInt32(dr["RequirementID"].ToString()),
                       RequirementHeaderID = Convert.ToInt32(dr["RequirementHeaderID"].ToString()),
                       GradingID = Convert.ToInt32(dr["GradingID"].ToString()),
                       Quarter = Convert.ToInt32(dr["Quarter"].ToString()),
                       DropLowest = dr["DropLowest"].ToString(),
                       Percent = Convert.ToDecimal(dr["Percent"].ToString())
                   });
               
               }
           }

           CloseConnection();
           return _Requirement;
       }

       // function that load all requirement view
       public List<Constructors.RequirementView> GetRequirementView()
       {
           List<Constructors.RequirementView> _RequirementView = new List<RequirementView>();
           string sql = "Select * From PaceRegistration.dbo.RequirementView";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _RequirementView.Add(new RequirementView
                   {
                       GradingID = Convert.ToInt32(dr["GradingID"].ToString()),
                       RequirementHeaderID = Convert.ToInt32(dr["RequirementHeaderID"].ToString()),
                       RequirementID = Convert.ToInt32(dr["RequirementID"].ToString()),
                       Quarter = Convert.ToInt32(dr["Quarter"].ToString()),
                       Description = dr["Description"].ToString(),
                       DropLowest = dr["DropLowest"].ToString(),
                       Percent = dr["Percent"].ToString(),
                       SchoolYear = dr["SchoolYear"].ToString(),
                       Status = dr["Status"].ToString()
                   });
               }
           }

           CloseConnection();
           return _RequirementView;
       }
       // function that load all requirement subitems
       public List<Constructors.RequirementSubitem> GetRequirementSubItems()
       {
           List<Constructors.RequirementSubitem> _subitems = new List<RequirementSubitem>();
           string sql = "Select * From PaceRegistration.dbo.RequirementSubitems";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _subitems.Add(new RequirementSubitem 
                   {
                        RequirementSubitemID = Convert.ToInt32(dr["RequirementSubitemID"].ToString()),
                        RequirementID = Convert.ToInt32(dr["RequirementID"].ToString()),
                        TotalPoints = Convert.ToInt32(dr["TotalPoints"].ToString())
                   
                   });
               }
            
           }
           CloseConnection();
           return _subitems;
       }

        //function that load all student grades
       public List<Constructors.StudentGrades> GetStudentGrades()
       {
           List<Constructors.StudentGrades> _studentgrade = new List<StudentGrades>();
           string sql = "SELECT PaceRegistration.dbo.StudentGrades.RequirementSubitemID, PaceRegistration.dbo.StudentGrades.RegistrationID, PaceRegistration.dbo.StudentGrades.Score, PaceRegistration.dbo.RequirementSubitems.RequirementID, PaceRegistration.dbo.RequirementSubitems.TotalPoints FROM PaceRegistration.dbo.StudentGrades INNER JOIN PaceRegistration.dbo.RequirementSubitems ON PaceRegistration.dbo.StudentGrades.RequirementSubitemID = PaceRegistration.dbo.RequirementSubitems.RequirementSubitemID";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _studentgrade.Add(new StudentGrades
                   {
                       RequirementID = (int)dr["RequirementID"],
                       RequirementSubitemID = (int)dr["RequirementSubitemID"],
                       RegistrationID = (int)dr["RegistrationID"],
                       Score = dr["Score"].ToString(),
                       TotalPoints = (int)dr["TotalPoints"],
                   });
               }
           }
           else
           {
               _studentgrade = null;
           }

           CloseConnection();
           return _studentgrade;
       }
        // function that load all parent accounts
       public List<Constructors.ParentAccounts> GetParentAcount()
       {
           List<Constructors.ParentAccounts> _parents = new List<ParentAccounts>();
           string sql = "Select * From PaceAssessment.dbo.[User] Where UserGroupID=3";
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _parents.Add(new ParentAccounts 
                   {
                        ParentID = Convert.ToInt32(dr["UserID"].ToString()),
                        UserGroupID = Convert.ToInt32(dr["UserGroupID"].ToString()),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        Status = dr["Status"].ToString()
                   });
               }
           }

           CloseConnection();
           return _parents;
       }
       //  function that load all parent childs
       public List<Constructors.ParentChilds> GetParentChild(int ParentID)
       {
           List<Constructors.ParentChilds> _child = new List<ParentChilds>();
           string sql = "Select * From PaceAssessment.dbo.[Parent] Where ParentUserID=" + ParentID;
           dr = ExecuteReader(sql);
           if (dr.HasRows)
           {
               while (dr.Read())
               {
                   _child.Add(new ParentChilds 
                   {
                       ParentID = Convert.ToInt32(dr["ParentID"].ToString()),
                       Firstname = dr["ChildFirstname"].ToString(),
                       Lastname = dr["ChildLastname"].ToString(),
                       ParentUserID = ParentID,
                       YearLevel = dr["YearLevel"].ToString(),
                       Section = dr["Section"].ToString(),
                   });
               }
           }

           CloseConnection();
           return _child;
       }
       /////////////////////
       //-----------------//
       //--- Web Forms ---//
       //-----------------//
       /////////////////////
       
       public GlobalForms getDefaultForms()
       {
           GlobalForms GForms = new GlobalForms();
           GForms.frm_questionpool_maintenance_main = "~/assessment/questionpool_maintenance_main.aspx";
           GForms.frm_questionpool_maintenance_upload = "~/assessment/questionpool_maintenance_upload.aspx";
           GForms.frm_admin_dashboard = "~/maintenance/admin_dashboard.aspx";
           GForms.frm_pending_student_accounts = "~/maintenance/pending_student_accounts.aspx";
           GForms.frm_student_maintenance_main = "~/maintenance/student_maintenance_main.aspx";
           GForms.frm_user_maintenance_main = "~/maintenance/user_maintenance_main.aspx";
           GForms.frm_activate_email = "~/registration/activate_email.aspx";
           GForms.frm_signup = "~/registration/signup.aspx";
           GForms.frm_default_dashboard = "~/default_dashboard.aspx";
           GForms.frm_forgot_password = "~/forgot_password.aspx";
           GForms.frm_login = "~/login.aspx";
           GForms.frm_logout = "~/logout.aspx";
           GForms.frm_instructor_dashboard = "~/instructor/instructor_dashboard.aspx";
           GForms.frm_index = "~/index.aspx";
           GForms.frm_assessmenttype_maintenance_main = "~/assessment/assessmenttype_maintenance_main.aspx";
           GForms.frm_assessmenttype_maintenance_addupdate = "~/assessment/assessmenttype_maintenance_addupdate.aspx";
           GForms.frm_questionpool_maintenance_manual = "~/assessment/questionpool_maintenance_manual.aspx";
           GForms.frm_questionpool_maintenance_update = "~/assessment/questionpool_maintenance_update.aspx";
           GForms.frm_student_dashboard = "~/student/student_dashboard.aspx";
           GForms.frm_change_password = "~/student/change_password.aspx";
           GForms.frm_student_maintenance_addupdate = "~/maintenance/student_maintenance_addupdate.aspx";
           GForms.frm_available_assessments = "~/student/available_assessments.aspx";
           GForms.frm_quizcreation_addupdate = "~/assessment/quizcreation_addupdate.aspx";
           GForms.frm_quizcreation_main = "~/assessment/quizcreation_main.aspx";
           GForms.frm_quizcreation_update = "~/assessment/quizcreation_update.aspx";
           GForms.frm_instructor_subjects = "~/instructor/instructor_subjects.aspx";
           GForms.frm_take_assessment = "~/student/take_assessment.aspx";
           GForms.frm_preview_assessment = "~/assessment/preview_assessment.aspx";
           GForms.frm_review_assessment = "~/student/review_assessment.aspx";
           GForms.frm_review_assessment_list = "~/student/review_assessment_list.aspx";
           GForms.frm_assessment_success = "~/student/assessment_success.aspx";
           GForms.frm_history_assessment_main = "~/student/history_assessment_main.aspx";
           GForms.frm_history_assessment_view = "~/student/history_assessment_view.aspx";
           GForms.frm_assessment_admin_add = "~/assessment/assessment_admin_add.aspx";
           GForms.frm_assessment_admin_update = "~/assessment/assessment_admin_update.aspx";
           GForms.frm_instructor_studentsview = "~/instructor/instructor_studentsview.aspx";
           GForms.frm_advisers_studentsview = "~/instructor/advisers_studentsview.aspx";
           GForms.frm_student_gradesview = "~/student/student_gradesview.aspx";
           GForms.frm_makeup_exam_list = "~/instructor/makeup_exam_list.aspx";
           GForms.frm_quarter_maintenance = "~/maintenance/quarter_maintenance.aspx";
           GForms.frm_quarter_edit = "~/maintenance/quarter_edit.aspx";
           GForms.frm_school_year_maintenance = "~/maintenance/school_year_maintenance.aspx";
           GForms.frm_parent_change_password = "~/parent/change_password.aspx";
           GForms.frm_parent_dashboard = "~/parent/parent_dashboard.aspx";
           GForms.frm_parent_edit_profile = "~/parent/edit_profile.aspx";
           GForms.frm_parent_select_child = "~/parent/parent_select_child.aspx";
           GForms.frm_parent_view_grades = "~/parent/parent_view_grades.aspx";
           GForms.frm_topic_maintenance_main = "~/maintenance/topic_maintenance_main.aspx";
           GForms.frm_usergroup_maintenance_main = "~/maintenance/usergroup_maintenance_main.aspx";
           return GForms;
       }

    }
}
